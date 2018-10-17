package cn.beecloud;

import org.junit.Assert;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.powermock.api.mockito.PowerMockito;
import org.powermock.core.classloader.annotations.PrepareForTest;
import org.powermock.modules.junit4.PowerMockRunner;

import java.text.SimpleDateFormat;
import java.util.Locale;
import java.util.Map;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.TimeUnit;

import cn.beecloud.async.BCCallback;
import cn.beecloud.async.BCResult;
import cn.beecloud.entity.BCBillOrder;
import cn.beecloud.entity.BCBillStatus;
import cn.beecloud.entity.BCQueryBillResult;
import cn.beecloud.entity.BCQueryBillsResult;
import cn.beecloud.entity.BCQueryCountResult;
import cn.beecloud.entity.BCQueryRefundResult;
import cn.beecloud.entity.BCQueryRefundsResult;
import cn.beecloud.entity.BCRefundOrder;
import cn.beecloud.entity.BCRefundStatus;
import cn.beecloud.entity.BCReqParams;
import cn.beecloud.entity.BCRestfulCommonResult;

@RunWith(PowerMockRunner.class)
@PrepareForTest(BCHttpClientUtil.class)
public class BCQueryTest {

    BCQuery query;
    CountDownLatch latch;

    @BeforeClass
    public static void setUpBeforeClass() throws Exception {
        BeeCloud.setAppIdAndSecret("c5d1cba1-5e3f-4ba0-941d-9b0a371fe719",
                "39a7a518-9ac8-4a9e-87bc-7885f33cf18c");
    }

    @Before
    public void setUp() throws Exception {

        query = BCQuery.getInstance();

        //for child thread async
        latch = new CountDownLatch(1);
    }

    /**
     * #1
     * 测试没有传channel的情况
     * @throws Exception
     */
    @Test
    public void testQueryBillsAsyncChannelNull() throws Exception {
        query.queryBillsAsync(null, null, null, null, null, null, new BCCallback() {
            @Override
            public void done(BCResult result) {
                Assert.assertTrue(result instanceof BCQueryBillsResult);

                BCQueryBillsResult billsResult = (BCQueryBillsResult) result;

                Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM, billsResult.getResultCode());
                latch.countDown();
            }
        });

        //最多等2s
        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #2
     * 测试response为400等网络异常
     * @throws Exception
     */
    @Test
    public void testQueryBillsAsyncNetworkError() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 400;
        response.content = "wrong";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpGet", String.class)).toReturn(response);

        query.queryBillsAsync(BCReqParams.BCChannelTypes.ALL, null, null, null, 2, 10,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof  BCQueryBillsResult);

                        BCQueryBillsResult billsResult = (BCQueryBillsResult) result;

                        Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM, billsResult.getResultCode());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #3
     * 正常response为200的情况
     * @throws Exception
     */
    @Test
    public void testQueryBillsAsyncSucc() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        //please note this content is fake, there should be more content for bills, here just keep one record for the limit of space
        response.content = "{\"result_msg\":\"OK\",\"err_detail\":\"\",\"result_code\":0,\"bills\":[{\"spay_result\":false,\"create_time\":1447658025661,\"total_fee\":1,\"channel\":\"UN\",\"trade_no\":\"\",\"bill_no\":\"bc1447657931\",\"optional\":\"{\\\"test\\\":\\\"willreturn\\\"}\",\"revert_result\":false,\"title\":\"你的订单标题\",\"sub_channel\":\"UN_WEB\",\"refund_result\":false}]}";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpGet", String.class)).toReturn(response);

        query.queryBillsAsync(BCReqParams.BCChannelTypes.ALL, null, null, null, 2, 10,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQueryBillsResult);

                        BCQueryBillsResult billsResult = (BCQueryBillsResult) result;

                        Assert.assertEquals((Integer) 0, billsResult.getResultCode());
                        //最多可以获取到limit条数据
                        Assert.assertTrue(billsResult.getCount() == billsResult.getBills().size());

                        BCBillOrder billOrder = billsResult.getBills().get(0);

                        Assert.assertEquals("bc1447657931", billOrder.getBillNum());
                        Assert.assertEquals("你的订单标题", billOrder.getTitle());
                        Assert.assertEquals((Integer) 1, billOrder.getTotalFee());
                        Assert.assertEquals("UN", billOrder.getChannel());
                        Assert.assertEquals("UN_WEB", billOrder.getSubChannel());
                        Assert.assertFalse(billOrder.getPayResult());
                        Assert.assertFalse(billOrder.getRevertResult());
                        Assert.assertEquals((Long) 1447658025661L, billOrder.getCreatedTime());
                        Assert.assertEquals("{\"test\":\"willreturn\"}", billOrder.getOptional());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #4
     * 测试根据支付渠道获取订单
     * @throws Exception
     */
    @Test
    public void testQueryBillsAsyncByChannel() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        //please note this content is fake, there should be content for bills, here delete the record for the limit of space
        response.content = "{\"result_msg\":\"OK\",\"err_detail\":\"\",\"result_code\":0,\"bills\":[{\"bill_no\":\"bc1447657931\"}]}";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpGet", String.class)).toReturn(response);

        query.queryBillsAsync(BCReqParams.BCChannelTypes.ALL,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQueryBillsResult);

                        BCQueryBillsResult billsResult = (BCQueryBillsResult) result;

                        Assert.assertEquals((Integer) 0, billsResult.getResultCode());
                        Assert.assertEquals((int) billsResult.getCount(), billsResult.getBills().size());
                        BCBillOrder billOrder = billsResult.getBills().get(0);
                        Assert.assertEquals("bc1447657931", billOrder.getBillNum());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #5
     * 测试根据支付渠道和订单号获取订单
     * @throws Exception
     */
    @Test
    public void testQueryBillsAsyncByChannelBillNum() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        //please note this content is fake, there should be content for bills, here delete the record for the limit of space
        response.content = "{\"result_msg\":\"OK\",\"err_detail\":\"\",\"result_code\":0,\"bills\":[{\"bill_no\":\"bc1447657931\"}]}";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpGet", String.class)).toReturn(response);

        query.queryBillsAsync(BCReqParams.BCChannelTypes.ALL,
                "billnum",
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQueryBillsResult);

                        BCQueryBillsResult billsResult = (BCQueryBillsResult) result;

                        Assert.assertEquals((Integer) 0, billsResult.getResultCode());
                        Assert.assertEquals((int) billsResult.getCount(), billsResult.getBills().size());
                        BCBillOrder billOrder = billsResult.getBills().get(0);
                        Assert.assertEquals("bc1447657931", billOrder.getBillNum());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #6
     * 测试通过BCQuery.QueryParams方式获取订单
     * @throws Exception
     */
    @Test
    public void testQueryBillsAsyncByBean() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        //please note this content is fake, there should be content for bills, here delete the record for the limit of space
        response.content = "{\"result_msg\":\"OK\",\"err_detail\":\"\",\"result_code\":0,\"bills\":[{\"bill_no\":\"bc1447657931\"}]}";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpGet", String.class)).toReturn(response);
        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm", Locale.CHINA);

        BCQuery.QueryParams queryParams = new BCQuery.QueryParams();
        queryParams.channel = BCReqParams.BCChannelTypes.WX;
        queryParams.startTime = sdf.parse("2015-10-01 00:00").getTime();

        query.queryBillsAsync(queryParams,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQueryBillsResult);

                        BCQueryBillsResult billsResult = (BCQueryBillsResult) result;

                        Assert.assertEquals((Integer) 0, billsResult.getResultCode());
                        Assert.assertEquals((int) billsResult.getCount(), billsResult.getBills().size());
                        BCBillOrder billOrder = billsResult.getBills().get(0);
                        Assert.assertEquals("bc1447657931", billOrder.getBillNum());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #7
     * test mode
     * @throws Exception
     */
    @Test
    public void testQueryBillsAsyncTestMode() throws Exception {
        BeeCloud.setSandbox(true);

        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        //please note this content is fake, there should be content for bills, here delete the record for the limit of space
        response.content = "{\"result_msg\":\"OK\",\"err_detail\":\"\",\"result_code\":0,\"bills\":[{\"bill_no\":\"bc1447657931\"}]}";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpGet", String.class)).toReturn(response);

        BCQuery.QueryParams queryParams = new BCQuery.QueryParams();
        queryParams.channel = BCReqParams.BCChannelTypes.WX;

        query.queryBillsAsync(queryParams,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQueryBillsResult);

                        BCQueryBillsResult billsResult = (BCQueryBillsResult) result;

                        Assert.assertEquals((Integer) 0, billsResult.getResultCode());
                        Assert.assertEquals((int) billsResult.getCount(), billsResult.getBills().size());
                        BCBillOrder billOrder = billsResult.getBills().get(0);
                        Assert.assertEquals("bc1447657931", billOrder.getBillNum());

                        BeeCloud.setSandbox(false);
                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #1
     * 测试没有传channel的情况
     * @throws Exception
     */
    @Test
    public void testQueryRefundsAsyncChannelNull() throws Exception {
        query.queryRefundsAsync(null, null, null, null, null, null, null, new BCCallback() {
            @Override
            public void done(BCResult result) {
                Assert.assertTrue(result instanceof BCQueryRefundsResult);

                BCQueryRefundsResult refundsResult = (BCQueryRefundsResult) result;

                Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM, refundsResult.getResultCode());
                latch.countDown();
            }
        });

        //最多等2s
        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #2
     * 测试response为400等网络异常
     * @throws Exception
     */
    @Test
    public void testQueryRefundsAsyncNetworkError() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 400;
        response.content = "wrong";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpGet", String.class)).toReturn(response);

        query.queryRefundsAsync(BCReqParams.BCChannelTypes.ALL, null, null, null, null, 2, 10,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQueryRefundsResult);

                        BCQueryRefundsResult refundsResult = (BCQueryRefundsResult) result;

                        Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM, refundsResult.getResultCode());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #3
     * 正常response为200的情况
     * @throws Exception
     */
    @Test
    public void testQueryRefundsAsyncSucc() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        //please note this content is fake, there should be more content for refunds, here just keep one record for the limit of space
        response.content = "{\"result_msg\":\"OK\",\"err_detail\":\"\",\"result_code\":0,\"refunds\":[{\"result\":false,\"create_time\":1447430833318,\"refund_no\":\"201511141447430832000\",\"total_fee\":1,\"refund_fee\":1,\"channel\":\"WX\",\"bill_no\":\"20151113132244266\",\"finish\":false,\"optional\":\"\",\"title\":\"2015-10-21 Release\",\"sub_channel\":\"WX_APP\"}]}";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpGet", String.class)).toReturn(response);

        query.queryRefundsAsync(BCReqParams.BCChannelTypes.ALL, null, null, null, null, 2, 10,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQueryRefundsResult);

                        BCQueryRefundsResult refundsResult = (BCQueryRefundsResult) result;

                        Assert.assertEquals((Integer) 0, refundsResult.getResultCode());
                        Assert.assertTrue(refundsResult.getCount() == refundsResult.getRefunds().size());

                        BCRefundOrder refundOrder = refundsResult.getRefunds().get(0);

                        Assert.assertEquals("20151113132244266", refundOrder.getBillNum());
                        Assert.assertEquals("201511141447430832000", refundOrder.getRefundNum());
                        Assert.assertEquals("2015-10-21 Release", refundOrder.getTitle());
                        Assert.assertEquals((Integer)1, refundOrder.getTotalFee());
                        Assert.assertEquals((Integer)1, refundOrder.getRefundFee());
                        Assert.assertEquals("WX", refundOrder.getChannel());
                        Assert.assertEquals("WX_APP", refundOrder.getSubChannel());
                        Assert.assertFalse(refundOrder.isRefundFinished());
                        Assert.assertFalse(refundOrder.getRefundResult());
                        Assert.assertEquals((Long) 1447430833318L, refundOrder.getRefundCreatedTime());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #4
     * 测试根据支付渠道获取退款订单
     * @throws Exception
     */
    @Test
    public void testQueryRefundsAsyncByChannel() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        //please note this content is fake, there should be content for refunds, here delete the record for the limit of space
        response.content = "{\"result_msg\":\"OK\",\"err_detail\":\"\",\"result_code\":0,\"refunds\":[{\"refund_no\":\"201511141447430832000\"}]}";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpGet", String.class)).toReturn(response);

        query.queryRefundsAsync(BCReqParams.BCChannelTypes.ALL,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQueryRefundsResult);

                        BCQueryRefundsResult refundsResult = (BCQueryRefundsResult) result;

                        Assert.assertEquals((Integer) 0, refundsResult.getResultCode());
                        Assert.assertEquals((int) refundsResult.getCount(), refundsResult.getRefunds().size());
                        BCRefundOrder refundOrder = refundsResult.getRefunds().get(0);
                        Assert.assertEquals("201511141447430832000", refundOrder.getRefundNum());
                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #5
     * 测试根据支付渠道和(支付订单号|退款单号)获取退款订单列表
     * @throws Exception
     */
    @Test
    public void testQueryRefundsAsyncByChannelBillRefundNum() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        //please note this content is fake, there should be content for refunds, here delete the record for the limit of space
        response.content = "{\"result_msg\":\"OK\",\"err_detail\":\"\",\"result_code\":0,\"refunds\":[{\"refund_no\":\"201511141447430832000\"}]}";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpGet", String.class)).toReturn(response);

        query.queryRefundsAsync(BCReqParams.BCChannelTypes.ALL,
                "billnum",
                "refundnum",
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQueryRefundsResult);

                        BCQueryRefundsResult refundsResult = (BCQueryRefundsResult) result;

                        Assert.assertEquals((Integer)0, refundsResult.getResultCode());
                        Assert.assertEquals((int) refundsResult.getCount(), refundsResult.getRefunds().size());
                        BCRefundOrder refundOrder = refundsResult.getRefunds().get(0);
                        Assert.assertEquals("201511141447430832000", refundOrder.getRefundNum());
                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #6
     * 测试根据BCQuery.QueryParams获取退款订单列表
     * @throws Exception
     */
    @Test
    public void testQueryRefundsAsyncByBean() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        //please note this content is fake, there should be content for refunds, here delete the record for the limit of space
        response.content = "{\"result_msg\":\"OK\",\"err_detail\":\"\",\"result_code\":0,\"refunds\":[{\"refund_no\":\"201511141447430832000\"}]}";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpGet", String.class)).toReturn(response);

        BCQuery.QueryParams queryParams = new BCQuery.QueryParams();
        queryParams.channel = BCReqParams.BCChannelTypes.UN;
        queryParams.refundNum = "refundnum";
        queryParams.billNum = "billnum";

        query.queryRefundsAsync(queryParams,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQueryRefundsResult);

                        BCQueryRefundsResult refundsResult = (BCQueryRefundsResult) result;

                        Assert.assertEquals((Integer) 0, refundsResult.getResultCode());
                        Assert.assertEquals((int) refundsResult.getCount(), refundsResult.getRefunds().size());
                        BCRefundOrder refundOrder = refundsResult.getRefunds().get(0);
                        Assert.assertEquals("201511141447430832000", refundOrder.getRefundNum());
                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #7
     * test mode暂不支持
     * @throws Exception
     */
    @Test
    public void testQueryRefundsAsyncTestMode() throws Exception {
        BeeCloud.setSandbox(true);

        query.queryRefundsAsync(BCReqParams.BCChannelTypes.ALL,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQueryRefundsResult);

                        BCQueryRefundsResult refundsResult = (BCQueryRefundsResult) result;

                        Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                refundsResult.getResultCode());
                        Assert.assertEquals("该功能暂不支持测试模式",
                                refundsResult.getErrDetail());

                        BeeCloud.setSandbox(false);
                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #1
     * 测试channel非支持的情况
     * @throws Exception
     */
    @Test
    public void testQueryRefundStatusAsyncChannelInvalid() throws Exception {
        query.queryRefundStatusAsync(BCReqParams.BCChannelTypes.ALI,
                "refundnum",
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCRefundStatus);

                        BCRefundStatus status = (BCRefundStatus) result;

                        Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM, status.getResultCode());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #2
     * 输入退款单号为null的情况
     * @throws Exception
     */
    @Test
    public void testQueryRefundStatusAsyncRefundNumInvalid() throws Exception {
        query.queryRefundStatusAsync(BCReqParams.BCChannelTypes.WX,
                null,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCRefundStatus);

                        BCRefundStatus status = (BCRefundStatus) result;

                        Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM, status.getResultCode());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #3
     * 网络请求400等异常情况
     * @throws Exception
     */
    @Test
    public void testQueryRefundStatusAsyncNetworkError() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 400;
        response.content = "wrong";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpGet", String.class)).toReturn(response);

        query.queryRefundStatusAsync(BCReqParams.BCChannelTypes.WX,
                "refundnum",
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCRefundStatus);

                        BCRefundStatus status = (BCRefundStatus) result;

                        Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM, status.getResultCode());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #4
     * 网络请求200
     * @throws Exception
     */
    @Test
    public void testQueryRefundStatusAsyncSucc() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        response.content = "{\"result_msg\":\"OK\",\"refund_status\":\"SUCCESS\",\"resultCode\":0,\"errMsg\":\"OK:\",\"err_detail\":\"\",\"result_code\":0}";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpGet", String.class)).toReturn(response);

        query.queryRefundStatusAsync(BCReqParams.BCChannelTypes.WX,
                "refundnum",
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCRefundStatus);

                        BCRefundStatus status = (BCRefundStatus) result;

                        Assert.assertEquals((Integer)0, status.getResultCode());
                        Assert.assertTrue(BCRefundStatus.RefundStatus.REFUND_STATUS_SUCCESS
                                .equals(status.getRefundStatus()));

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #5
     * test mode
     * @throws Exception
     */
    @Test
    public void testQueryRefundStatusAsyncTestMode() throws Exception {
        BeeCloud.setSandbox(true);

        query.queryRefundStatusAsync(BCReqParams.BCChannelTypes.WX,
                "refundnum",
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCRefundStatus);

                        BCRefundStatus status = (BCRefundStatus) result;

                        Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                status.getResultCode());
                        Assert.assertEquals("该功能暂不支持测试模式",
                                status.getErrDetail());

                        BeeCloud.setSandbox(false);
                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #1
     * 测试传入id为null的情况
     * @throws Exception
     */
    @Test
    public void testQueryBillByIDAsyncParamInvalid() throws Exception {

        query.queryBillByIDAsync(null, new BCCallback() {
            @Override
            public void done(BCResult result) {
                //传出的应是BCQueryBillResult类型
                Assert.assertTrue(result instanceof BCQueryBillResult);
                BCQueryBillResult billResult = (BCQueryBillResult) result;

                //返回的结果应该是-1
                Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM, billResult.getResultCode());

                latch.countDown();
            }
        });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #2
     * 模拟网络异常response为400等
     * @throws Exception
     */
    @Test
    public void testQueryBillByIDAsyncNetworkError() throws Exception {

        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 400;
        response.content = "wrong";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpGet", String.class)).toReturn(response);

        query.queryBillByIDAsync("billid", new BCCallback() {
            @Override
            public void done(BCResult result) {
                Assert.assertTrue(result instanceof BCQueryBillResult);

                BCQueryBillResult billResult = (BCQueryBillResult) result;

                Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM, billResult.getResultCode());
                //释放
                latch.countDown();
            }
        });

        //等待
        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #3
     * 模拟response为200
     * @throws Exception
     */
    @Test
    public void testQueryBillByIDAsyncSucc() throws Exception {

        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        response.content = "{\"result_msg\":\"OK\",\"err_detail\":\"\",\"pay\":{\"spay_result\":false,\"create_time\":1447643289722,\"total_fee\":1,\"channel\":\"WX\",\"trade_no\":\"\",\"bill_no\":\"20151116110810464\",\"optional\":\"{\\\"testkey1\\\":\\\"测试value值1\\\"}\",\"revert_result\":false,\"title\":\"安卓微信支付测试\",\"sub_channel\":\"WX_APP\",\"message_detail\":\"\",\"refund_result\":false},\"result_code\":0}";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpGet", String.class)).toReturn(response);

        query.queryBillByIDAsync("billid", new BCCallback() {
            @Override
            public void done(BCResult result) {
                Assert.assertTrue(result instanceof BCQueryBillResult);

                BCQueryBillResult billResult = (BCQueryBillResult) result;

                Assert.assertEquals((Integer)0, billResult.getResultCode());

                BCBillOrder billOrder = billResult.getBill();

                Assert.assertEquals("20151116110810464", billOrder.getBillNum());
                Assert.assertEquals("安卓微信支付测试", billOrder.getTitle());
                Assert.assertTrue(billOrder.getTradeNum() == null || billOrder.getTradeNum().length() == 0);
                Assert.assertEquals((Integer)1, billOrder.getTotalFee());
                Assert.assertEquals("WX", billOrder.getChannel());
                Assert.assertEquals("WX_APP", billOrder.getSubChannel());
                Assert.assertFalse(billOrder.getPayResult());
                Assert.assertFalse(billOrder.getRevertResult());
                Assert.assertEquals((Long) 1447643289722L, billOrder.getCreatedTime());
                Assert.assertEquals("{\"testkey1\":\"测试value值1\"}", billOrder.getOptional());
                //释放
                latch.countDown();
            }
        });

        //等待
        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #4
     * test mode
     * @throws Exception
     */
    @Test
    public void testQueryBillByIDAsyncTestMode() throws Exception {
        BeeCloud.setSandbox(true);

        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        response.content = "{\"result_msg\":\"OK\",\"err_detail\":\"\",\"pay\":{\"spay_result\":false,\"create_time\":1447643289722,\"total_fee\":1,\"channel\":\"WX\",\"trade_no\":\"\",\"bill_no\":\"20151116110810464\",\"optional\":\"{\\\"testkey1\\\":\\\"测试value值1\\\"}\",\"revert_result\":false,\"title\":\"安卓微信支付测试\",\"sub_channel\":\"WX_APP\",\"message_detail\":\"\",\"refund_result\":false},\"result_code\":0}";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpGet", String.class)).toReturn(response);

        query.queryBillByIDAsync("billid", new BCCallback() {
            @Override
            public void done(BCResult result) {
                Assert.assertTrue(result instanceof BCQueryBillResult);

                BCQueryBillResult billResult = (BCQueryBillResult) result;

                Assert.assertEquals((Integer)0, billResult.getResultCode());

                BCBillOrder billOrder = billResult.getBill();

                Assert.assertEquals("20151116110810464", billOrder.getBillNum());
                Assert.assertEquals("安卓微信支付测试", billOrder.getTitle());
                Assert.assertTrue(billOrder.getTradeNum() == null || billOrder.getTradeNum().length() == 0);
                Assert.assertEquals((Integer) 1, billOrder.getTotalFee());
                Assert.assertEquals("WX", billOrder.getChannel());
                Assert.assertEquals("WX_APP", billOrder.getSubChannel());
                Assert.assertFalse(billOrder.getPayResult());
                Assert.assertFalse(billOrder.getRevertResult());
                Assert.assertEquals((Long) 1447643289722L, billOrder.getCreatedTime());
                Assert.assertEquals("{\"testkey1\":\"测试value值1\"}", billOrder.getOptional());

                BeeCloud.setSandbox(false);
                latch.countDown();
            }
        });

        //等待
        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #1
     * 测试传入id为null的情况
     * @throws Exception
     */
    @Test
    public void testQueryRefundByIDAsyncParamInvalid() throws Exception {

        query.queryRefundByIDAsync(null, new BCCallback() {
            @Override
            public void done(BCResult result) {
                //传出的应是BCQueryRefundResult类型
                Assert.assertTrue(result instanceof BCQueryRefundResult);
                BCQueryRefundResult refundResult = (BCQueryRefundResult) result;

                //返回的结果应该是-1
                Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM, refundResult.getResultCode());

                latch.countDown();
            }
        });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #2
     * 模拟response为400等网络异常
     * @throws Exception
     */
    @Test
    public void testQueryRefundByIDAsyncNetworkError() throws Exception {

        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 400;
        response.content = "wrong";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpGet", String.class)).toReturn(response);

        query.queryRefundByIDAsync("refundid", new BCCallback() {
            @Override
            public void done(BCResult result) {
                Assert.assertTrue(result instanceof BCQueryRefundResult);

                BCQueryRefundResult refundResult = (BCQueryRefundResult) result;

                Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM, refundResult.getResultCode());
                //释放
                latch.countDown();
            }
        });

        //等待
        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #3
     * 模拟response为200
     * @throws Exception
     */
    @Test
    public void testQueryRefundByIDAsyncSucc() throws Exception {

        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        response.content = "{\"result_msg\":\"OK\",\"err_detail\":\"\",\"result_code\":0,\"refund\":{\"result\":false,\"create_time\":1446531122439,\"refund_no\":\"2015110362316\",\"total_fee\":1,\"refund_fee\":1,\"channel\":\"WX\",\"bill_no\":\"ccd378d8823640a68e220949686e5922\",\"finish\":false,\"optional\":\"{\\\"test\\\":\\\"test\\\"}\",\"title\":\"demo测试\",\"sub_channel\":\"WX_NATIVE\",\"message_detail\":\"\"}}";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpGet", String.class)).toReturn(response);

        query.queryRefundByIDAsync("refundid", new BCCallback() {
            @Override
            public void done(BCResult result) {
                Assert.assertTrue(result instanceof BCQueryRefundResult);

                BCQueryRefundResult refundResult = (BCQueryRefundResult) result;

                Assert.assertEquals((Integer)0, refundResult.getResultCode());

                BCRefundOrder refundOrder = refundResult.getRefund();

                Assert.assertEquals("ccd378d8823640a68e220949686e5922", refundOrder.getBillNum());
                Assert.assertEquals("2015110362316", refundOrder.getRefundNum());
                Assert.assertEquals("demo测试", refundOrder.getTitle());
                Assert.assertEquals((Integer)1, refundOrder.getTotalFee());
                Assert.assertEquals((Integer)1, refundOrder.getRefundFee());
                Assert.assertEquals("WX", refundOrder.getChannel());
                Assert.assertEquals("WX_NATIVE", refundOrder.getSubChannel());
                Assert.assertFalse(refundOrder.isRefundFinished());
                Assert.assertFalse(refundOrder.getRefundResult());
                Assert.assertEquals((Long)1446531122439L, refundOrder.getRefundCreatedTime());
                Assert.assertEquals("{\"test\":\"test\"}", refundOrder.getOptional());

                //释放
                latch.countDown();
            }
        });

        //等待
        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #4
     * test mode
     * @throws Exception
     */
    @Test
    public void testQueryRefundByIDAsyncTestMode() throws Exception {
        BeeCloud.setSandbox(true);

        query.queryRefundByIDAsync("refundid",
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQueryRefundResult);

                        BCQueryRefundResult refundResult = (BCQueryRefundResult) result;

                        Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                refundResult.getResultCode());
                        Assert.assertEquals("该功能暂不支持测试模式",
                                refundResult.getErrDetail());

                        BeeCloud.setSandbox(false);
                        latch.countDown();
                    }
        });

        //等待
        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #1
     * 测试不支持的channel
     * @throws Exception
     */
    @Test
    public void testQueryOfflineBillStatusAsyncChannelInvalid() throws Exception {
        query.queryOfflineBillStatusAsync(BCReqParams.BCChannelTypes.WX_APP,
                null,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCBillStatus);

                        BCBillStatus status = (BCBillStatus) result;

                        Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM, status.getResultCode());
                        Assert.assertEquals("invalid bill number", status.getErrDetail());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #2
     * billNum为null的情况
     * @throws Exception
     */
    @Test
    public void testQueryOfflineBillStatusAsyncBillNumInvalid() throws Exception {
        query.queryOfflineBillStatusAsync(BCReqParams.BCChannelTypes.WX_SCAN,
                null,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCBillStatus);

                        BCBillStatus status = (BCBillStatus) result;

                        Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM, status.getResultCode());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #3
     * 网络请求400等异常情况
     * @throws Exception
     */
    @Test
    public void testQueryOfflineBillStatusAsyncNetworkError() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 400;
        response.content = "wrong";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpPost",
                String.class, Map.class)).toReturn(response);

        query.queryOfflineBillStatusAsync(BCReqParams.BCChannelTypes.WX_SCAN,
                "fakeid",
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCBillStatus);

                        BCBillStatus status = (BCBillStatus) result;

                        Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM, status.getResultCode());
                        //释放
                        latch.countDown();
                    }
                });

        //等待
        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #4
     * 模拟请求成功的情况
     * @throws Exception
     */
    @Test
    public void testQueryOfflineBillStatusAsyncSucc() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        response.content = "{\"result_msg\":\"OK\",\"submit_msg\":\"OK\",\"resultCode\":0,\"errMsg\":\"OK:\",\"err_detail\":\"\",\"result_code\":0,\"did_submit\":true,\"pay_result\":true}";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpPost",
                String.class, Map.class)).toReturn(response);

        query.queryOfflineBillStatusAsync(BCReqParams.BCChannelTypes.WX_NATIVE,
                "fakeid",
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCBillStatus);

                        BCBillStatus status = (BCBillStatus) result;

                        Assert.assertEquals((Integer)0, status.getResultCode());
                        Assert.assertTrue(status.getPayResult());
                        //释放
                        latch.countDown();
                    }
                });

        //等待
        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #5
     * test mode
     * @throws Exception
     */
    @Test
    public void testQueryOfflineBillStatusAsyncTestMode() throws Exception {
        BeeCloud.setSandbox(true);

        query.queryOfflineBillStatusAsync(BCReqParams.BCChannelTypes.WX_NATIVE,
                "fakeid",
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCBillStatus);

                        BCBillStatus status = (BCBillStatus) result;

                        Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                status.getResultCode());
                        Assert.assertEquals("该功能暂不支持测试模式",
                                status.getErrDetail());

                        BeeCloud.setSandbox(false);
                        latch.countDown();
                    }
                });

        //等待
        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #1
     * 测试channel为null
     * @throws Exception
     */
    @Test
    public void testQueryBillsCountAsyncChannelInvalid() throws Exception {
        BCQuery.QueryParams queryParams = new BCQuery.QueryParams();
        queryParams.channel = null;

        query.queryBillsCountAsync(queryParams,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQueryCountResult);

                        BCQueryCountResult countResult = (BCQueryCountResult) result;

                        Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM, countResult.getResultCode());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #2
     * 网络请求400等异常情况
     * @throws Exception
     */
    @Test
    public void testQueryBillsCountAsyncNetworkError() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 400;
        response.content = "wrong";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpGet", String.class)).toReturn(response);

        BCQuery.QueryParams params = new BCQuery.QueryParams();
        params.channel = BCReqParams.BCChannelTypes.ALL;

        query.queryBillsCountAsync(params,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQueryCountResult);

                        BCQueryCountResult countResult = (BCQueryCountResult) result;

                        Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM, countResult.getResultCode());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #3
     * 网络请求200
     * @throws Exception
     */
    @Test
    public void testQueryBillsCountAsyncSucc() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        response.content = "{\"result_msg\":\"OK\",\"err_detail\":\"\",\"count\":826,\"result_code\":0}";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpGet", String.class)).toReturn(response);

        BCQuery.QueryParams params = new BCQuery.QueryParams();
        params.channel = BCReqParams.BCChannelTypes.ALL;
        params.payResult = Boolean.TRUE;

        query.queryBillsCountAsync(params,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQueryCountResult);

                        BCQueryCountResult countResult = (BCQueryCountResult) result;

                        Assert.assertEquals((Integer)0, countResult.getResultCode());
                        Assert.assertEquals((Integer)826, countResult.getCount());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #1
     * 测试channel为null
     * @throws Exception
     */
    @Test
    public void testQueryRefundsCountAsyncChannelInvalid() throws Exception {
        BCQuery.QueryParams queryParams = new BCQuery.QueryParams();
        queryParams.channel = null;

        query.queryRefundsCountAsync(queryParams,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQueryCountResult);

                        BCQueryCountResult countResult = (BCQueryCountResult) result;

                        Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM, countResult.getResultCode());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #2
     * 网络请求400等异常情况
     * @throws Exception
     */
    @Test
    public void testQueryRefundsCountAsyncNetworkError() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 400;
        response.content = "wrong";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpGet", String.class)).toReturn(response);

        BCQuery.QueryParams params = new BCQuery.QueryParams();
        params.channel = BCReqParams.BCChannelTypes.ALL;

        query.queryRefundsCountAsync(params,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQueryCountResult);

                        BCQueryCountResult countResult = (BCQueryCountResult) result;

                        Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM, countResult.getResultCode());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #3
     * 网络请求200
     * @throws Exception
     */
    @Test
    public void testQueryRefundsCountAsyncSucc() throws Exception {
        final BCHttpClientUtil.Response response = new BCHttpClientUtil.Response();
        response.code = 200;
        response.content = "{\"result_msg\":\"OK\",\"err_detail\":\"\",\"count\":826,\"result_code\":0}";

        //mock
        PowerMockito.stub(PowerMockito.method(BCHttpClientUtil.class, "httpGet", String.class)).toReturn(response);

        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm", Locale.CHINA);

        BCQuery.QueryParams queryParams = new BCQuery.QueryParams();
        queryParams.channel = BCReqParams.BCChannelTypes.WX;
        queryParams.startTime = sdf.parse("2015-10-01 00:00").getTime();

        query.queryRefundsCountAsync(queryParams,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQueryCountResult);

                        BCQueryCountResult countResult = (BCQueryCountResult) result;

                        Assert.assertEquals((Integer)0, countResult.getResultCode());
                        Assert.assertEquals((Integer)826, countResult.getCount());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }

    /**
     * #4
     * test mode
     * @throws Exception
     */
    @Test
    public void testQueryRefundsCountAsyncTestMode() throws Exception {
        BeeCloud.setSandbox(true);

        BCQuery.QueryParams queryParams = new BCQuery.QueryParams();
        queryParams.channel = BCReqParams.BCChannelTypes.WX;

        query.queryRefundsCountAsync(queryParams,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        Assert.assertTrue(result instanceof BCQueryCountResult);

                        BCQueryCountResult countResult = (BCQueryCountResult) result;

                        Assert.assertEquals(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                countResult.getResultCode());
                        Assert.assertEquals("该功能暂不支持测试模式",
                                countResult.getErrDetail());

                        latch.countDown();
                    }
                });

        latch.await(2000, TimeUnit.MILLISECONDS);
    }
}
