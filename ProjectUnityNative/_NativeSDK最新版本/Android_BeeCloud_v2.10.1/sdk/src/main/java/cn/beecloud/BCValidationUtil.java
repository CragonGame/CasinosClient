/**
 * BCValidationUtil.java
 * <p>
 * Created by xuanzhui on 2015/7/27.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud;

import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.JsonSyntaxException;

import java.io.UnsupportedEncodingException;
import java.util.HashMap;
import java.util.Map;

import cn.beecloud.async.BCCallback;
import cn.beecloud.entity.BCCardVerifyResult;
import cn.beecloud.entity.BCPayReqParams;
import cn.beecloud.entity.BCRestfulCommonResult;

/**
 * 校验类
 */
public class BCValidationUtil {
    private static final String TAG = "BCValidationUtil";

    /**
     * 判断string是否为空
     * @param str 被校验字符串
     */
    private static boolean isValidString(String str) {
        return str != null && str.length() != 0;
    }

    /**
     * 判断string是否只包含数字和字母, 且长度8-32
     */
    private static boolean isValidBillNum(String str) {
        return str != null && str.length() >= 8
                && str.length() <= 32 && str.matches("[A-Za-z0-9]+");
    }

    /**
     * 判断字符串是否为有效的正整数
     * @param str 被校验字符串
     */
    static boolean isStringValidPositiveInt(String str) {
        boolean res = false;

        if (isValidString(str)) {

            int length = str.length();
            int i = 0;

            for (; i < length; i++) {
                char c = str.charAt(i);
                if (c <= '/' || c >= ':') {
                    break;
                }
            }

            if (i == length)
                res = true;
        }

        return res;
    }

    /**
     * 判断字符串是否为url
     * @param str 被校验字符串
     */
    static boolean isStringValidURL(String str) {
        return isValidString(str) &&
                (str.toLowerCase().startsWith("http://") ||
                        str.toLowerCase().startsWith("https://"));
    }

    /**
     * 判断订单长度是否在有效长度以内, 汉字以2个字节计, 总长度最大为32
     * @param str   订单标题
     * @return true代表长度有效
     */
    private static boolean isValidBillTitleLength(String str) {
        if (!isValidString(str))
            return false;

        boolean res = false;

        try {
            byte[] bytes = str.getBytes("GBK");

            if (bytes.length <= 32)
                res = true;
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
        }

        return res;
    }

    /**
     * 校验bill参数
     * 设置公用参数
     *
     * @param billTitle       商品描述, 32个字节内, 汉字以2个字节计
     * @param billTotalFee    支付金额，以分为单位，必须是正整数
     * @param billNum         商户自定义订单号
     * @param parameters      用于存储公用信息
     * @param optional        为扩展参数，可以传入任意数量的key/value对来补充对业务逻辑的需求
     * @return 返回校验失败信息, 为null则表明校验通过
     */
    static String prepareParametersForPay(final String billTitle, final Integer billTotalFee,
                                                 final String billNum, final Map<String, String> optional,
                                                 BCPayReqParams parameters) {

        if (!BCValidationUtil.isValidBillTitleLength(billTitle)) {
            return "parameters: 不合法的参数-订单标题长度不合法, 32个字节内, 汉字以2个字节计";
        }

        if (billTotalFee == null || billTotalFee <= 0) {
            return "parameters: billTotalFee " + billTotalFee +
                    " 格式不正确, 必须是以分为单位的正整数, 比如100表示1元";
        }

        if (!BCValidationUtil.isValidBillNum(billNum))
            return "parameters: 订单号必须是长度8~32位字母和/或数字组合成的字符串";

        parameters.title = billTitle;
        parameters.totalFee = billTotalFee;
        parameters.billNum = billNum;
        parameters.optional = optional;

        return null;
    }

    // 鉴权
    public static void verifyCardFactors(final String name, final String idNum, final String cardNum,
                                   final String mobile, final BCCallback callback) {
        if (callback == null) {
            Log.e(TAG, "请初始化callback");
            return;
        }

        BCCache.executorService.execute(new Runnable() {
            @Override
            public void run() {
                Map<String, Object> reqMap = new HashMap<>();
                reqMap.put("name", name);
                reqMap.put("id_no", idNum);
                if (cardNum != null)
                    reqMap.put("card_no", cardNum);
                if (mobile != null)
                    reqMap.put("mobile", mobile);

                BCHttpClientUtil.attachAppSign(reqMap);

                BCHttpClientUtil.Response response = BCHttpClientUtil
                        .httpPost(BCHttpClientUtil.getAuthUrl(), reqMap);

                if (response.code == 200 || (response.code >= 400 && response.code < 500)) {
                    //反序列化json
                    Gson gson = new Gson();

                    try {
                        callback.done(gson.fromJson(response.content, BCCardVerifyResult.class));
                    } catch (JsonSyntaxException ex) {
                        callback.done(new BCCardVerifyResult(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                BCRestfulCommonResult.APP_INNER_FAIL,
                                "JsonSyntaxException or Network Error:" + response.code + " # " + response.content));
                    }
                } else {
                    callback.done(new BCCardVerifyResult(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL,
                            "Network Error:" + response.code + " # " + response.content));
                }
            }
        });
    }

    /**
     * 二要素鉴权
     * @param name  身份证姓名
     * @param idNum 身份证号
     * @param callback 回调入口
     */
    public static void verifyCardFactors(final String name, final String idNum, final BCCallback callback) {
        verifyCardFactors(name, idNum, null, null, callback);
    }
}
