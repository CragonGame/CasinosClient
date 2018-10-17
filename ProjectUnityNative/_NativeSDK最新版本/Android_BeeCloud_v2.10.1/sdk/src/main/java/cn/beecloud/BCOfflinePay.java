/**
 * BCOfflinePay.java
 * <p/>
 * Created by xuanzhui on 2015/9/9.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud;

import android.graphics.Bitmap;
import android.graphics.Color;
import android.os.Looper;
import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.JsonSyntaxException;
import com.google.gson.reflect.TypeToken;
import com.google.zxing.BarcodeFormat;
import com.google.zxing.EncodeHintType;
import com.google.zxing.MultiFormatWriter;
import com.google.zxing.WriterException;
import com.google.zxing.common.BitMatrix;

import java.lang.reflect.Type;
import java.util.EnumMap;
import java.util.HashMap;
import java.util.Map;

import cn.beecloud.async.BCCallback;
import cn.beecloud.entity.BCPayReqParams;
import cn.beecloud.entity.BCPayResult;
import cn.beecloud.entity.BCQRCodeResult;
import cn.beecloud.entity.BCReqParams;
import cn.beecloud.entity.BCRestfulCommonResult;
import cn.beecloud.entity.BCRevertStatus;

/**
 * 线下扫码
 */
public class BCOfflinePay {
    private static final String TAG = "BCOfflinePay";

    private static BCOfflinePay instance;

    private BCOfflinePay() {
    }

    /**
     * 唯一获取BCPay实例的入口
     *
     * @return BCPay实例
     */
    public synchronized static BCOfflinePay getInstance() {
        if (instance == null) {
            instance = new BCOfflinePay();
        }
        return instance;
    }

    /**
     * 将string转化成对应的bitmap
     *
     * @param contentsToEncode 原始字符串
     * @param imageWidth       生成的图片宽度, 以px为单位
     * @param imageHeight      生成的图片高度, 以px为单位
     * @param marginSize       生成的图片中二维码到图片边缘的留边
     * @param color            二维码图片的前景色
     * @param colorBack        二维码图片的背景色
     * @return Bitmap                   QR Code图片
     * @throws WriterException       zxing无法生成QR Code
     * @throws IllegalStateException 本函数不应该在UI主进程调用, 通过使用AsyncTask或者新建进程
     */
    public static Bitmap generateBitmap(String contentsToEncode,
                                        int imageWidth, int imageHeight,
                                        int marginSize, int color, int colorBack)
            throws WriterException, IllegalStateException {

        if (Looper.myLooper() == Looper.getMainLooper()) {
            throw new IllegalStateException("Should not be invoked from the UI thread");
        }

        Map<EncodeHintType, Object> hints = new EnumMap<EncodeHintType, Object>(EncodeHintType.class);
        hints.put(EncodeHintType.MARGIN, marginSize);
        hints.put(EncodeHintType.CHARACTER_SET, "UTF-8");

        MultiFormatWriter writer = new MultiFormatWriter();
        BitMatrix result = writer.encode(contentsToEncode, BarcodeFormat.QR_CODE, imageWidth, imageHeight, hints);

        final int width = result.getWidth();
        final int height = result.getHeight();
        int[] pixels = new int[width * height];
        for (int y = 0; y < height; y++) {
            int offset = y * width;
            for (int x = 0; x < width; x++) {
                pixels[offset + x] = result.get(x, y) ? color : colorBack;
            }
        }

        Bitmap bitmap = Bitmap.createBitmap(width, height, Bitmap.Config.ARGB_8888);
        bitmap.setPixels(pixels, 0, width, 0, 0, width, height);
        return bitmap;
    }

    /**
     * 生成二维码接口
     *
     * @param channelType  生成扫码的类型  对于支付手机APP端目前只支持WX_NATIVE, ALI_OFFLINE_QRCODE
     * @param billTitle    商品描述, 32个字节内, 汉字以2个字节计
     * @param billTotalFee 支付金额，以分为单位，必须是正整数
     * @param billNum      商户自定义订单号
     * @param optional     为扩展参数，可以传入任意数量的key/value对来补充对业务逻辑的需求
     * @param genQRCode    是否生成QRCode Bitmap
     * @param qrCodeWidth  如果生成, QRCode的宽度, null则使用默认参数
     * @param callback     支付完成后的回调函数
     * @see cn.beecloud.entity.BCReqParams.BCChannelTypes
     */
    public void reqQRCodeAsync(final BCReqParams.BCChannelTypes channelType,
                               final String billTitle, final Integer billTotalFee,
                               final String billNum, final Map<String, String> optional,
                               final Boolean genQRCode, final Integer qrCodeWidth,
                               final BCCallback callback) {
        reqQRCodeAsync(channelType, billTitle, billTotalFee, billNum,
                null, null, null, optional, null, genQRCode, qrCodeWidth, callback);
    }

    // 生成二维码总接口
    private void reqQRCodeAsync(final BCReqParams.BCChannelTypes channelType,
                                final String billTitle, final Integer billTotalFee,
                                final String billNum, final String notifyUrl,
                                final String buyerId, final String couponId,
                                final Map<String, String> optional,
                                final Map<String, String> analysis,
                                final Boolean genQRCode, final Integer qrCodeWidth,
                                final BCCallback callback) {

        if (callback == null) {
            Log.w(TAG, "请初始化callback");
            return;
        }

        BCCache.executorService.execute(new Runnable() {
            @Override
            public void run() {
                if (BCCache.getInstance().isTestMode) {
                    callback.done(new BCQRCodeResult(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL, "该功能暂不支持测试模式"));
                    return;
                }

                //校验并准备公用参数
                BCPayReqParams parameters;
                try {
                    parameters = new BCPayReqParams(channelType);
                } catch (BCException e) {
                    callback.done(new BCQRCodeResult(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL, e.getMessage()));
                    return;
                }

                String paramValidRes = BCValidationUtil.prepareParametersForPay(billTitle, billTotalFee,
                        billNum, optional, parameters);

                if (paramValidRes != null) {
                    callback.done(new BCQRCodeResult(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL,
                            paramValidRes));
                    return;
                }

                if (notifyUrl != null) {
                    parameters.notifyUrl = notifyUrl;
                }

                parameters.buyerId = buyerId;
                parameters.analysis = analysis;
                parameters.couponId = couponId;

                String qrCodeReqURL = BCHttpClientUtil.getBillOfflinePayURL();
                if (channelType == BCReqParams.BCChannelTypes.BC_NATIVE ||
                        channelType == BCReqParams.BCChannelTypes.BC_ALI_QRCODE)
                    qrCodeReqURL = BCHttpClientUtil.getBillPayURL();

                BCHttpClientUtil.Response response = BCHttpClientUtil
                        .httpPost(qrCodeReqURL, parameters.transToBillReqMapParams());

                if (response.code == 200 || (response.code >= 400 && response.code < 500)) {
                    String ret = response.content;
                    //反序列化json
                    Gson res = new Gson();

                    Type type = new TypeToken<Map<String, Object>>() {
                    }.getType();

                    Map<String, Object> responseMap;
                    try {
                        responseMap = res.fromJson(ret, type);
                    } catch (JsonSyntaxException ex) {
                        callback.done(new BCQRCodeResult(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                BCRestfulCommonResult.APP_INNER_FAIL,
                                "JsonSyntaxException or Network Error:"
                                        + response.code + " # " + response.content));
                        return;
                    }

                    //判断后台返回结果
                    Integer resultCode = ((Double) responseMap.get("result_code")).intValue();
                    if (resultCode == 0) {

                        String content = null;
                        Bitmap qrBitmap = null;
                        int imgSize = BCQRCodeResult.DEFAULT_QRCODE_WIDTH;

                        if (responseMap.get("code_url") != null) {
                            content = (String) responseMap.get("code_url");
                        }

                        if (genQRCode && content != null) {

                            if (qrCodeWidth != null)
                                imgSize = qrCodeWidth;

                            try {
                                qrBitmap = BCOfflinePay.generateBitmap(content, imgSize,
                                        imgSize, 0,
                                        Color.BLACK, Color.WHITE);
                            } catch (WriterException e) {
                                callback.done(new BCQRCodeResult(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                        BCRestfulCommonResult.APP_INNER_FAIL, e.getMessage()));
                                return;
                            }
                        }

                        String id = null;
                        if (responseMap.get("id") != null)
                            id = String.valueOf(responseMap.get("id"));

                        callback.done(new BCQRCodeResult(resultCode,
                                String.valueOf(responseMap.get("result_msg")),
                                String.valueOf(responseMap.get("err_detail")),
                                imgSize, imgSize,
                                content, qrBitmap,
                                null, id));

                    } else {
                        //返回服务端传回的错误信息
                        callback.done(new BCQRCodeResult(resultCode,
                                String.valueOf(responseMap.get("result_msg")),
                                String.valueOf(responseMap.get("err_detail"))));
                    }

                } else {
                    callback.done(new BCQRCodeResult(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL,
                            "Network Error:" + response.code + " # " + response.content));
                }

            }
        });
    }

    /**
     * 生成二维码总接口
     *
     * @param payParam 支付要素
     * @param callback 支付完成后的回调函数
     */
    public void reqQRCodeAsync(final PayParams payParam,
                               final BCCallback callback) {
        reqQRCodeAsync(payParam.channelType,
                payParam.billTitle,
                payParam.billTotalFee,
                payParam.billNum,
                payParam.notifyUrl,
                payParam.buyerId,
                payParam.couponId,
                payParam.optional,
                payParam.analysis,
                payParam.genQRCode,
                payParam.qrCodeWidth,
                callback);
    }

    /**
     * 线下支付接口
     *
     * @param channelType  生成扫码的类型  对于支付手机APP端目前只支持WX_SCAN, ALI_SCAN
     * @param billTitle    商品描述, 32个字节内, 汉字以2个字节计
     * @param billTotalFee 支付金额，以分为单位，必须是正整数
     * @param billNum      商户自定义订单号
     * @param optional     为扩展参数，可以传入任意数量的key/value对来补充对业务逻辑的需求
     * @param authCode     授权码
     * @param terminalId   机具终端编号, 支付宝条码(ALI_SCAN)的选填参数
     * @param storeId      商户门店编号, 支付宝条码(ALI_SCAN)的选填参数
     * @param callback     支付完成后的回调函数
     * @see cn.beecloud.entity.BCReqParams.BCChannelTypes
     */
    public void reqOfflinePayAsync(final BCReqParams.BCChannelTypes channelType,
                                   final String billTitle, final Integer billTotalFee,
                                   final String billNum, final Map<String, String> optional,
                                   final String authCode, final String terminalId,
                                   final String storeId, final BCCallback callback) {
        reqOfflinePayAsync(channelType, billTitle, billTotalFee, billNum, null, null, null,
                optional, null, authCode, terminalId, storeId, callback);
    }

    // 线下支付总接口
    private void reqOfflinePayAsync(final BCReqParams.BCChannelTypes channelType,
                                    final String billTitle, final Integer billTotalFee,
                                    final String billNum, final String notifyUrl,
                                    final String buyerId, final String couponId,
                                    final Map<String, String> optional,
                                    final Map<String, String> analysis,
                                    final String authCode, final String terminalId,
                                    final String storeId, final BCCallback callback) {

        if (callback == null) {
            Log.w(TAG, "请初始化callback");
            return;
        }

        BCCache.executorService.execute(new Runnable() {
            @Override
            public void run() {
                if (BCCache.getInstance().isTestMode) {
                    callback.done(new BCPayResult(BCPayResult.RESULT_FAIL,
                            BCPayResult.APP_INTERNAL_PARAMS_ERR_CODE,
                            BCPayResult.FAIL_INVALID_PARAMS,
                            "该功能暂不支持测试模式"));
                    return;
                }

                //校验并准备公用参数
                BCPayReqParams parameters;
                try {
                    parameters = new BCPayReqParams(channelType);
                } catch (BCException e) {
                    callback.done(new BCPayResult(BCPayResult.RESULT_FAIL,
                            BCPayResult.APP_INTERNAL_PARAMS_ERR_CODE,
                            BCPayResult.FAIL_INVALID_PARAMS,
                            e.getMessage()));
                    return;
                }

                String paramValidRes = BCValidationUtil.prepareParametersForPay(billTitle, billTotalFee,
                        billNum, optional, parameters);

                if (paramValidRes != null) {
                    callback.done(new BCPayResult(BCPayResult.RESULT_FAIL,
                            BCPayResult.APP_INTERNAL_PARAMS_ERR_CODE,
                            BCPayResult.FAIL_INVALID_PARAMS,
                            paramValidRes));
                    return;
                }

                if (authCode == null || authCode.length() == 0) {
                    callback.done(new BCPayResult(BCPayResult.RESULT_FAIL,
                            BCPayResult.APP_INTERNAL_PARAMS_ERR_CODE,
                            BCPayResult.FAIL_INVALID_PARAMS,
                            "授权码不能为空"));
                    return;
                }

                parameters.authCode = authCode;

                if (notifyUrl != null) {
                    parameters.notifyUrl = notifyUrl;
                }

                if (terminalId != null)
                    parameters.terminalId = terminalId;

                if (storeId != null)
                    parameters.storeId = storeId;

                parameters.buyerId = buyerId;
                parameters.couponId = couponId;
                parameters.analysis = analysis;

                String qrCodeReqURL = BCHttpClientUtil.getBillOfflinePayURL();

                BCHttpClientUtil.Response response = BCHttpClientUtil
                        .httpPost(qrCodeReqURL, parameters.transToBillReqMapParams());

                if (response.code == 200 || (response.code >= 400 && response.code < 500)) {
                    String ret = response.content;

                    //反序列化json
                    Gson res = new Gson();

                    Type type = new TypeToken<Map<String, Object>>() {
                    }.getType();
                    Map<String, Object> responseMap;
                    try {
                        responseMap = res.fromJson(ret, type);
                    } catch (JsonSyntaxException ex) {
                        callback.done(new BCPayResult(BCPayResult.RESULT_FAIL,
                                BCPayResult.APP_INTERNAL_EXCEPTION_ERR_CODE,
                                BCPayResult.FAIL_EXCEPTION,
                                "JsonSyntaxException or Network Error:"
                                        + response.code + " # " + response.content));
                        return;
                    }

                    //判断后台返回结果
                    Integer resultCode = ((Double) responseMap.get("result_code")).intValue();
                    if (resultCode == 0) {
                        String id = null;
                        if (responseMap.get("id") != null)
                            id = String.valueOf(responseMap.get("id"));

                        callback.done(new BCPayResult(BCPayResult.RESULT_SUCCESS,
                                BCPayResult.APP_PAY_SUCC_CODE,
                                BCPayResult.RESULT_SUCCESS,
                                BCPayResult.RESULT_SUCCESS,
                                id));

                    } else {
                        //返回服务端传回的错误信息
                        String serverMsg = String.valueOf(responseMap.get("result_msg"));
                        String serverDetail = String.valueOf(responseMap.get("err_detail"));

                        callback.done(new BCPayResult(BCPayResult.RESULT_FAIL,
                                resultCode,
                                serverMsg,
                                serverDetail));
                    }

                } else {
                    callback.done(new BCPayResult(BCPayResult.RESULT_FAIL,
                            BCPayResult.APP_INTERNAL_NETWORK_ERR_CODE,
                            BCPayResult.FAIL_NETWORK_ISSUE,
                            "Network Error:" + response.code + " # " + response.content));
                }

            }
        });
    }

    /**
     * 线下支付总接口
     *
     * @param payParam 支付要素
     * @param callback 支付完成后的回调函数
     */
    public void reqOfflinePayAsync(final PayParams payParam, final BCCallback callback) {
        reqOfflinePayAsync(payParam.channelType,
                payParam.billTitle,
                payParam.billTotalFee,
                payParam.billNum,
                payParam.notifyUrl,
                payParam.buyerId,
                payParam.couponId,
                payParam.optional,
                payParam.analysis,
                payParam.authCode,
                payParam.terminalId,
                payParam.storeId,
                callback);
    }

    /**
     * 生成微信支付二维码
     *
     * @param billTitle    商品描述, 32个字节内, 汉字以2个字节计
     * @param billTotalFee 支付金额，以分为单位，必须是正整数
     * @param billNum      商户自定义订单号
     * @param optional     为扩展参数，可以传入任意数量的key/value对来补充对业务逻辑的需求
     * @param genQRCode    是否生成QRCode Bitmap
     * @param qrCodeWidth  如果生成, QRCode的宽度, null则使用默认参数
     * @param callback     支付完成后的回调函数
     */
    public void reqWXNativeQRCodeAsync(final String billTitle, final Integer billTotalFee,
                                       final String billNum, final Map<String, String> optional,
                                       final Boolean genQRCode, final Integer qrCodeWidth,
                                       final BCCallback callback) {
        reqQRCodeAsync(BCReqParams.BCChannelTypes.WX_NATIVE, billTitle, billTotalFee,
                billNum, optional, genQRCode, qrCodeWidth, callback);
    }

    /**
     * 生成支付宝线下支付二维码
     *
     * @param billTitle    商品描述, 32个字节内, 汉字以2个字节计
     * @param billTotalFee 支付金额，以分为单位，必须是正整数
     * @param billNum      商户自定义订单号
     * @param optional     为扩展参数，可以传入任意数量的key/value对来补充对业务逻辑的需求
     * @param genQRCode    是否生成QRCode Bitmap
     * @param qrCodeWidth  如果生成, QRCode的宽度, null则使用默认参数
     * @param callback     支付完成后的回调函数
     */
    public void reqAliOfflineQRCodeAsync(final String billTitle, final Integer billTotalFee,
                                         final String billNum, final Map<String, String> optional,
                                         final Boolean genQRCode, final Integer qrCodeWidth,
                                         final BCCallback callback) {
        reqQRCodeAsync(BCReqParams.BCChannelTypes.ALI_OFFLINE_QRCODE, billTitle, billTotalFee,
                billNum, optional, genQRCode, qrCodeWidth, callback);
    }

    /**
     * 撤销订单
     *
     * @param channelType 目前只支持WX_SCAN, ALI_OFFLINE_QRCODE, ALI_SCAN
     * @param billNum     订单号
     * @param callback    支付完成后的回调函数
     * @see cn.beecloud.entity.BCReqParams.BCChannelTypes
     */
    public void reqRevertBillAsync(final BCReqParams.BCChannelTypes channelType,
                                   final String billNum,
                                   final BCCallback callback) {

        if (callback == null) {
            Log.w(TAG, "请初始化callback");
            return;
        }

        BCCache.executorService.execute(new Runnable() {
            @Override
            public void run() {
                if (BCCache.getInstance().isTestMode) {
                    callback.done(new BCRevertStatus(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL, "该功能暂不支持测试模式"));
                    return;
                }

                //校验并准备公用参数
                BCReqParams parameters;
                try {
                    parameters = new BCReqParams(channelType);
                } catch (BCException e) {
                    callback.done(new BCRevertStatus(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL,
                            e.getMessage()));
                    return;
                }

                if (billNum == null ||
                        billNum.length() == 0) {
                    callback.done(new BCRevertStatus(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL,
                            "invalid bill number"));
                    return;
                }

                String revertURL = BCHttpClientUtil.getBillOfflinePayURL() + "/" + billNum;

                Map<String, Object> reqMap = parameters.transToReqMapParams();
                reqMap.put("method", "REVERT");

                BCHttpClientUtil.Response response = BCHttpClientUtil
                        .httpPost(revertURL, reqMap);

                if (response.code == 200 || (response.code >= 400 && response.code < 500)) {

                    //返回后台结果
                    callback.done(BCRevertStatus.transJsonToObject(response.content));

                } else {
                    callback.done(new BCRevertStatus(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL,
                            "Network Error:" + response.code + " # " + response.content));
                }

            }
        });
    }

    /**
     * 外部支付参数实例
     */
    public static class PayParams {
        /**
         * 只允许
         * BCReqParams.BCChannelTypes.WX_NATIVE，
         * BCReqParams.BCChannelTypes.ALI_OFFLINE_QRCODE，
         * BCReqParams.BCChannelTypes.WX_SCAN，
         * BCReqParams.BCChannelTypes.ALI_SCAN
         */
        public BCReqParams.BCChannelTypes channelType;

        /**
         * 商品描述, 32个字节内, 汉字以2个字节计
         */
        public String billTitle;

        /**
         * 支付金额，以分为单位，必须是正整数
         */
        public Integer billTotalFee;

        /**
         * 商户自定义订单号
         */
        public String billNum;

        /**
         * 消费者在商户系统的ID，传入该信息会在dashboard显示用户行为分析
         */
        public String buyerId;

        /**
         * 卡券ID，用于营销
         */
        public String couponId;

        /**
         * 扩展参数，可以传入任意数量的key/value对来补充对业务逻辑的需求，可以为null
         */
        public Map<String, String> optional;

        /**
         * 用于生成二维码请求
         * 是否生成QRCode Bitmap
         */
        public Boolean genQRCode;

        /**
         * 用于生成二维码请求
         * 如果genQRCode为true, 生成的QRCode宽度值, 以px为单位, null则使用默认参数360
         */
        public Integer qrCodeWidth;

        /**
         * 以扫描用户微信或支付宝授权码方式发起支付时的必要参数
         */
        public String authCode;

        public String notifyUrl;

        /**
         * 机具终端编号, 支付宝条码(ALI_SCAN)的选填参数,
         * 若机具商接入, terminalId(机具终端编号)必填, storeId(商户门店编号)选填
         */
        public String terminalId;

        /**
         * 商户门店编号, 支付宝条码(ALI_SCAN)的选填参数,
         * 若系统商接入, storeId(商户的门店编号)必填, terminalId(机具终端编号)选填
         */
        public String storeId;

        /**
         * 扩展参数，用于分析
         */
        public Map<String, String> analysis;
    }
}
