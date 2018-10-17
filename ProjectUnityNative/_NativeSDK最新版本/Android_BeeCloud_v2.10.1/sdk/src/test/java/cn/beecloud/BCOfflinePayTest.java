package cn.beecloud;

import android.graphics.Bitmap;
import android.os.Looper;

import org.junit.Assert;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.powermock.api.mockito.PowerMockito;
import org.powermock.core.classloader.annotations.PrepareForTest;
import org.powermock.modules.junit4.PowerMockRunner;

import java.util.Map;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.TimeUnit;

import cn.beecloud.async.BCCallback;
import cn.beecloud.async.BCResult;
import cn.beecloud.entity.BCPayResult;
import cn.beecloud.entity.BCQRCodeResult;
import cn.beecloud.entity.BCReqParams;
import cn.beecloud.entity.BCRestfulCommonResult;
import cn.beecloud.entity.BCRevertStatus;

@RunWith(PowerMockRunner.class)
@PrepareForTest({BCHttpClientUtil.class, Looper.class, Bitmap.class})
public class BCOfflinePayTest {
    BCOfflinePay pay;
    CountDownLatch latch;

    @BeforeClass
    public static void setUpBeforeClass() throws Exception {
        BeeCloud.setAppIdAndSecret("c5d1cba1-5e3f-4ba0-941d-9b0a371fe719",
                "39a7a518-9ac8-4a9e-87bc-7885f33cf18c");
    }

    @Before
    public void setUp() throws Exception {
        pay = BCOfflinePay.getInstance();
        latch = new CountDownLatch(1);
    }

    /**
     * #1
     * 模拟参数不合理
     * @throws Exception
     */
    @Test
    public void testReqQRCodeAsyncParamInvalid() throws Exception {
        //依次对应必填参数 订单标题 订单金额 订单流水号
        Object[][] test = new Object[][] {
                new Object[]{null, null, null},
                new Object[]{"试一个很长的title--参数公共返回参数取值及含义参见支付公共返回参数部分", null, null},
                new Object[]{"正常title", null, null},
                new Object[]{"正常title", -1, null},
                new Object[]{"正常title", 3, null},
                new Object[]{"正常title", 3, "123"},

        };

        for (Object[] objects:test) {
            final CountDownLatch localLatch = new CountDownLatch(1);

            pay.reqQRCodeAsync(BCReqParams.BCChannelTypes.ALI_OFFLINE_QRCODE,
                    (String)objects[0],
                    (Integer)objects[1],
                    (String)objects[2],
                    null,
                    Boolean.FALSE,
                    333,
                    new BCCallback() {
                        @Override
                        public void done(BCResult result) {
                            Assert.assertTrue(result instanceof BCQRCodeResult);

                            BCQRCodeResult bcqrCodeResult = (BCQRCodeResult) result;

                            Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                    bcqrCodeResult.getResultCode());
                            Assert.assertTrue(bcqrCodeResult.getErrDetail().startsWith("parameters"));
                            //System.out.println(bcqrCodeResult.getErrDetail());

                            localLatch.countDown();
                        }
                    });

            //最多等待2s
            localLatch.await(2000, TimeUnit.MILLISECONDS);
        }
    }

    /**
     * #2
     * 模拟网络400等异常
     * @throws Exception
     */
    @Test
    public void testReqQRCodeAsyncNetworkError() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 400;
        response.content = "wrong";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpPost",
                String.class, Map.class)).toReturn(response);

        pay.reqQRCodeAsync(BCReqParams.BCChannelTypes.WX_NATIVE,
                "订单标题",
                1,
                "123456789ABCDE",
                null,
                Boolean.FALSE,
                555,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQRCodeResult);

                        BCQRCodeResult bcqrCodeResult = (BCQRCodeResult) result;

                        Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                bcqrCodeResult.getResultCode());
                        //System.out.println(bcqrCodeResult.getErrDetail());
                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #3
     * 模拟网络正常，参数异常
     * @throws Exception
     */
    @Test
    public void testReqQRCodeAsyncErrorFromServer() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        response.content = "{\"result_msg\":\"RUNTIME_ERORR\",\"err_detail\":\"未知错误,请联系BeeCloud\",\"result_code\":16}";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpPost",
                String.class, Map.class)).toReturn(response);

        pay.reqQRCodeAsync(BCReqParams.BCChannelTypes.WX_NATIVE,
                "订单标题",
                1,
                "123456789ABCDE",
                null,
                Boolean.FALSE,
                321,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQRCodeResult);

                        BCQRCodeResult bcqrCodeResult = (BCQRCodeResult) result;

                        Assert.assertEquals((Integer)16,
                                bcqrCodeResult.getResultCode());
                        Assert.assertEquals("RUNTIME_ERORR", bcqrCodeResult.getResultMsg());
                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #4
     * 模拟全部正常情况
     * @throws Exception
     */
    @Test
    public void testReqQRCodeAsyncSucc() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        response.content = "{\"result_msg\":\"OK\",\"code_url\":\"weixin://wxpay/bizpayurl?pr=ESDN0qd\",\"err_detail\":\"\",\"result_code\":0}";

        //mock as junit does not provide real android sdk
        PowerMockito.stub(PowerMockito.method(Looper.class, "myLooper")).toReturn(PowerMockito.mock(Looper.class));
        PowerMockito.stub(PowerMockito.method(Looper.class, "getMainLooper")).toReturn(PowerMockito.mock(Looper.class));

        PowerMockito.stub(PowerMockito.method(Bitmap.class, "createBitmap",
                int.class, int.class, Bitmap.Config.class)).toReturn(PowerMockito.mock(Bitmap.class));

        //mock network
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpPost",
                String.class, Map.class)).toReturn(response);

        pay.reqQRCodeAsync(BCReqParams.BCChannelTypes.WX_NATIVE,
                "订单标题",
                1,
                "123456789ABCDE",
                null,
                Boolean.TRUE,
                null,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQRCodeResult);

                        BCQRCodeResult bcqrCodeResult = (BCQRCodeResult) result;

                        Assert.assertEquals((Integer)0,
                                bcqrCodeResult.getResultCode());

                        Assert.assertEquals("weixin://wxpay/bizpayurl?pr=ESDN0qd",
                                bcqrCodeResult.getQrCodeRawContent());

                        Assert.assertEquals(BCQRCodeResult.DEFAULT_QRCODE_WIDTH,
                                bcqrCodeResult.getQrCodeWidth());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #5
     * 模拟PayParams方式发起的请求
     * @throws Exception
     */
    @Test
    public void testReqQRCodeAsyncByBean() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        response.content = "{\"result_msg\":\"OK\",\"code_url\":\"weixin://wxpay/bizpayurl?pr=ESDN0qd\",\"err_detail\":\"\",\"result_code\":0}";

        //mock network
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpPost",
                String.class, Map.class)).toReturn(response);

        BCOfflinePay.PayParams params = new BCOfflinePay.PayParams();
        params.channelType = BCReqParams.BCChannelTypes.WX_NATIVE;
        params.billTitle = "订单标题";
        params.billTotalFee = 1;
        params.billNum = "123456789ABCDE";
        params.genQRCode = Boolean.FALSE;

        pay.reqQRCodeAsync(params,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQRCodeResult);

                        BCQRCodeResult bcqrCodeResult = (BCQRCodeResult) result;

                        Assert.assertEquals((Integer) 0,
                                bcqrCodeResult.getResultCode());

                        Assert.assertEquals("weixin://wxpay/bizpayurl?pr=ESDN0qd",
                                bcqrCodeResult.getQrCodeRawContent());

                        Assert.assertEquals(null, bcqrCodeResult.getQrCodeBitmap());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #6
     * 模拟测试模式，应该提示暂不支持
     * @throws Exception
     */
    @Test
    public void testReqQRCodeAsyncTestMode() throws Exception {
        BeeCloud.setSandbox(true);

        pay.reqQRCodeAsync(BCReqParams.BCChannelTypes.ALI_OFFLINE_QRCODE,
                "billtitle",
                1,
                "billnum",
                null,
                Boolean.FALSE,
                333,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQRCodeResult);

                        BCQRCodeResult bcqrCodeResult = (BCQRCodeResult) result;

                        Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                bcqrCodeResult.getResultCode());
                        Assert.assertEquals("该功能暂不支持测试模式",
                                bcqrCodeResult.getErrDetail());

                        //revert back
                        BeeCloud.setSandbox(false);
                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * 模拟直接通过reqWXNativeQRCodeAsync请求微信二维码
     * @throws Exception
     */
    @Test
    public void testReqWXNativeQRCodeAsync() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        response.content = "{\"result_msg\":\"OK\",\"code_url\":\"weixin://wxpay/bizpayurl?pr=ESDN0qd\",\"err_detail\":\"\",\"result_code\":0}";

        //mock as junit does not provide real android sdk
        PowerMockito.stub(PowerMockito.method(Looper.class, "myLooper")).toReturn(PowerMockito.mock(Looper.class));
        PowerMockito.stub(PowerMockito.method(Looper.class, "getMainLooper")).toReturn(PowerMockito.mock(Looper.class));

        PowerMockito.stub(PowerMockito.method(Bitmap.class, "createBitmap",
                int.class, int.class, Bitmap.Config.class)).toReturn(PowerMockito.mock(Bitmap.class));

        //mock network
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpPost",
                String.class, Map.class)).toReturn(response);

        pay.reqWXNativeQRCodeAsync("订单标题",
                1,
                "123456789ABCDE",
                null,
                Boolean.TRUE,
                null,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQRCodeResult);

                        BCQRCodeResult bcqrCodeResult = (BCQRCodeResult) result;

                        Assert.assertEquals((Integer) 0,
                                bcqrCodeResult.getResultCode());

                        Assert.assertEquals("weixin://wxpay/bizpayurl?pr=ESDN0qd",
                                bcqrCodeResult.getQrCodeRawContent());

                        Assert.assertEquals(BCQRCodeResult.DEFAULT_QRCODE_WIDTH,
                                bcqrCodeResult.getQrCodeWidth());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * 模拟直接通过reqAliOfflineQRCodeAsync请求支付宝二维码
     * @throws Exception
     */
    @Test
    public void testReqAliOfflineQRCodeAsync() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 400;
        response.content = "wrong";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpPost",
                String.class, Map.class)).toReturn(response);

        pay.reqAliOfflineQRCodeAsync("订单标题",
                1,
                "123456789ABCDE",
                null,
                Boolean.FALSE,
                123,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQRCodeResult);

                        BCQRCodeResult bcqrCodeResult = (BCQRCodeResult) result;

                        Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                bcqrCodeResult.getResultCode());
                        //System.out.println(bcqrCodeResult.getErrDetail());
                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #1
     * 模拟参数问题
     * @throws Exception
     */
    @Test
    public void testReqOfflinePayAsyncParamInvalid() throws Exception {

        Object[][] test = new Object[][] {
                //依次对应必填参数 渠道类型 订单标题 订单金额 订单流水号 收款码
                new Object[]{null, null, null, null, null},
                new Object[]{BCReqParams.BCChannelTypes.ALI_APP, null, null, null, null},
                new Object[]{BCReqParams.BCChannelTypes.ALI_SCAN, null, null, null, null},
                new Object[]{BCReqParams.BCChannelTypes.ALI_SCAN, "试一个很长的title--参数公共返回参数取值及含义参见支付公共返回参数部分", null, null, null},
                new Object[]{BCReqParams.BCChannelTypes.ALI_SCAN, "正常title", null, null, null},
                new Object[]{BCReqParams.BCChannelTypes.ALI_SCAN, "正常title", -1, null, null},
                new Object[]{BCReqParams.BCChannelTypes.ALI_SCAN, "正常title", 3, null, null},
                new Object[]{BCReqParams.BCChannelTypes.WX_SCAN, "正常title", 3, "123", null},
                new Object[]{BCReqParams.BCChannelTypes.WX_SCAN, "正常title", 3, "123456789ABCDE", null},
        };

        for (Object[] objects : test) {
            final CountDownLatch localLatch = new CountDownLatch(1);

            pay.reqOfflinePayAsync((BCReqParams.BCChannelTypes) objects[0],
                    (String) objects[1],
                    (Integer) objects[2],
                    (String) objects[3],
                    null,
                    (String) objects[4],
                    "terminalid",
                    null,
                    new BCCallback() {
                        @Override
                        public void done(BCResult result) {
                            Assert.assertTrue(result instanceof BCPayResult);

                            BCPayResult payResult = (BCPayResult) result;

                            Assert.assertEquals(BCPayResult.RESULT_FAIL,
                                    payResult.getResult());

                            Assert.assertEquals((Integer)BCPayResult.APP_INTERNAL_PARAMS_ERR_CODE,
                                    payResult.getErrCode());

                            Assert.assertEquals(BCPayResult.FAIL_INVALID_PARAMS,
                                    payResult.getErrMsg());
                            //System.out.println(payResult.getDetailInfo());

                            localLatch.countDown();
                        }
                    });

            localLatch.await(2000, TimeUnit.MILLISECONDS);
        }
    }

    /**
     * #2
     * 测试网络400异常情况
     * @throws Exception
     */
    @Test
    public void testReqOfflinePayAsyncNetworkError() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 400;
        response.content = "wrong";

        //mock network
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpPost",
                String.class, Map.class)).toReturn(response);

        pay.reqOfflinePayAsync(BCReqParams.BCChannelTypes.WX_SCAN,
                "订单标题",
                1,
                "123456789ABCDE",
                null,
                "fakecode",
                null,
                null,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCPayResult);

                        BCPayResult payResult = (BCPayResult) result;

                        Assert.assertEquals(BCPayResult.RESULT_FAIL,
                                payResult.getResult());
                        Assert.assertEquals((Integer) BCPayResult.APP_INTERNAL_EXCEPTION_ERR_CODE,
                                payResult.getErrCode());
                        Assert.assertEquals(BCPayResult.FAIL_EXCEPTION,
                                payResult.getErrMsg());
                        //System.out.println(payResult.getDetailInfo());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #3
     * 测试网络200 但是 支付不成功
     * @throws Exception
     */
    @Test
    public void testReqOfflinePayAsyncErrorFromServer() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        response.content = "{\"result_msg\":\"CHANNEL_ERROR\",\"err_detail\":\"AUTHCODEEXPIRE\",\"result_code\":7,\"pay_result\":false}";

        //mock network
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpPost",
                String.class, Map.class)).toReturn(response);

        pay.reqOfflinePayAsync(BCReqParams.BCChannelTypes.WX_SCAN,
                "订单标题",
                1,
                "123456789ABCDE",
                null,
                "fakecode",
                null,
                "storeid",
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCPayResult);

                        BCPayResult payResult = (BCPayResult) result;

                        Assert.assertEquals(BCPayResult.RESULT_FAIL,
                                payResult.getResult());
                        Assert.assertEquals((Integer)7,
                                payResult.getErrCode());
                        Assert.assertEquals("CHANNEL_ERROR",
                                payResult.getErrMsg());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #4
     * 测试全部正常的情况
     * @throws Exception
     */
    @Test
    public void testReqOfflinePayAsyncSucc() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        response.content = "{\"result_msg\":\"OK\",\"err_detail\":\"\",\"result_code\":0,\"pay_result\":true}";

        //mock network
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpPost",
                String.class, Map.class)).toReturn(response);

        pay.reqOfflinePayAsync(BCReqParams.BCChannelTypes.WX_SCAN,
                "订单标题",
                1,
                "123456789ABCDE",
                null,
                "fakecode",
                null,
                "storeid",
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCPayResult);

                        BCPayResult payResult = (BCPayResult) result;

                        Assert.assertEquals(BCPayResult.RESULT_SUCCESS,
                                payResult.getResult());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #5
     * 测试通过PayParams支付
     * @throws Exception
     */
    @Test
    public void testReqOfflinePayAsyncByBean() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        response.content = "{\"result_msg\":\"OK\",\"err_detail\":\"\",\"result_code\":0,\"pay_result\":true}";

        //mock network
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpPost",
                String.class, Map.class)).toReturn(response);

        BCOfflinePay.PayParams params = new BCOfflinePay.PayParams();
        params.channelType = BCReqParams.BCChannelTypes.WX_SCAN;
        params.billTitle = "订单标题";
        params.billTotalFee = 1;
        params.billNum = "123456789ABCDE";
        params.authCode = "fakecode";

        pay.reqOfflinePayAsync(params,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCPayResult);

                        BCPayResult payResult = (BCPayResult) result;

                        Assert.assertEquals(BCPayResult.RESULT_SUCCESS,
                                payResult.getResult());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #6
     * 模拟test mode，应该提示暂不支持
     * @throws Exception
     */
    @Test
    public void testReqOfflinePayAsyncTestMode() throws Exception {
        BeeCloud.setSandbox(true);

        pay.reqOfflinePayAsync(BCReqParams.BCChannelTypes.ALI_SCAN,
                "正常title",
                1,
                "123456789abcde",
                null,
                "authcode",
                "terminalid",
                null,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCPayResult);

                        BCPayResult payResult = (BCPayResult) result;

                        Assert.assertEquals(BCPayResult.RESULT_FAIL,
                                payResult.getResult());
                        Assert.assertEquals((Integer) BCPayResult.APP_INTERNAL_PARAMS_ERR_CODE,
                                payResult.getErrCode());
                        Assert.assertEquals(BCPayResult.FAIL_INVALID_PARAMS,
                                payResult.getErrMsg());
                        Assert.assertEquals("该功能暂不支持测试模式",
                                payResult.getDetailInfo());

                        BeeCloud.setSandbox(false);
                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #1
     * 模拟参数问题
     * @throws Exception
     */
    @Test
    public void testReqRevertBillAsyncParamInvalid() throws Exception {
        Object[][] test = new Object[][]{
                //依次代表渠道类型 订单流水号
                new Object[]{null, null},
                new Object[]{BCReqParams.BCChannelTypes.ALI_APP, null},
                new Object[]{BCReqParams.BCChannelTypes.ALI_SCAN, null},
        };

        for (Object[] objects : test) {
            final CountDownLatch localLatch = new CountDownLatch(1);

            pay.reqRevertBillAsync((BCReqParams.BCChannelTypes)objects[0],
                    (String)objects[1],
                    new BCCallback() {
                        @Override
                        public void done(BCResult result) {
                            Assert.assertTrue(result instanceof BCRevertStatus);

                            BCRevertStatus revertStatus = (BCRevertStatus) result;

                            Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                    revertStatus.getResultCode());

                            //System.out.println(revertStatus.getErrDetail());

                            localLatch.countDown();
                        }
                    });

            localLatch.await(2000, TimeUnit.MILLISECONDS);
        }
    }

    /**
     * #2
     * 模拟网络400等异常
     * @throws Exception
     */
    @Test
    public void testReqRevertBillAsyncNetworkError() throws Exception {
        BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 400;
        response.content = "wrong";

        //mock network
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpPost",
                String.class, Map.class)).toReturn(response);

        pay.reqRevertBillAsync(BCReqParams.BCChannelTypes.WX_SCAN,
                "fakebillnum",
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCRevertStatus);

                        BCRevertStatus revertStatus = (BCRevertStatus) result;

                        Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                revertStatus.getResultCode());
                        //System.out.println(revertStatus.getErrDetail());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #3
     * 模拟全部正常的情况
     * @throws Exception
     */
    @Test
    public void testReqRevertBillAsyncSucc() throws Exception {
        BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        response.content = "{\"result_msg\":\"OK\",\"err_detail\":\"Success\",\"result_code\":0,\"revert_status\":true}";

        //mock network
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpPost",
                String.class, Map.class)).toReturn(response);

        pay.reqRevertBillAsync(BCReqParams.BCChannelTypes.ALI_OFFLINE_QRCODE,
                "fakebillnum",
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCRevertStatus);

                        BCRevertStatus revertStatus = (BCRevertStatus) result;

                        Assert.assertEquals((Integer)0,
                                revertStatus.getResultCode());

                        Assert.assertTrue(revertStatus.getRevertStatus());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #4
     * test mode暂不支持
     * @throws Exception
     */
    @Test
    public void testReqRevertBillAsyncTestMode() throws Exception {
        BeeCloud.setSandbox(true);

        pay.reqRevertBillAsync(BCReqParams.BCChannelTypes.ALI_SCAN,
                "billnum",
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCRevertStatus);

                        BCRevertStatus revertStatus = (BCRevertStatus) result;

                        Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                revertStatus.getResultCode());
                        Assert.assertEquals("该功能暂不支持测试模式",
                                revertStatus.getErrDetail());

                        BeeCloud.setSandbox(false);
                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }
}
