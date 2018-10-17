/**
 * BCHttpClientUtil.java
 * <p/>
 * Created by xuanzhui on 2015/7/27.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud;

import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.JsonSyntaxException;

import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.lang.reflect.Constructor;
import java.lang.reflect.Field;
import java.net.URLEncoder;
import java.security.KeyManagementException;
import java.security.NoSuchAlgorithmException;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.TimeUnit;

import javax.net.ssl.X509TrustManager;

import cn.beecloud.entity.BCRestfulCommonResult;
import okhttp3.FormBody;
import okhttp3.MediaType;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.ResponseBody;
import okhttp3.internal.platform.Platform;

/**
 * 网络请求工具类
 */
class BCHttpClientUtil {

    //主机地址
    public static final String BEECLOUD_HOST = "https://api.beecloud.cn";

    private static final String TAG = "BCHttpClientUtil";

    //Rest API版本号
    private static final String HOST_API_VERSION = "/2/";

    //订单支付部分URL 和 获取扫码信息
    private static final String BILL_PAY_URL = "rest/bill";

    //测试模式对应的订单支付部分URL和获取扫码信息URL
    private static final String BILL_PAY_SANDBOX_URL = "rest/sandbox/bill";

    //测试模式的异步通知(通知服务端支付成功)
    private static final String NOTIFY_PAY_RESULT_SANDBOX_URL = "rest/sandbox/notify";

    //支付订单列表查询部分URL
    private static final String BILLS_QUERY_URL = "rest/bills?para=";

    //支付订单列表查询部分测试模式URL
    private static final String BILLS_QUERY_SANDBOX_URL = "rest/sandbox/bills?para=";

    //支付订单数目查询部分URL
    private static final String BILLS_COUNT_QUERY_URL = "rest/bills/count?para=";

    //支付订单数目查询部分测试模式URL
    private static final String BILLS_COUNT_QUERY_SANDBOX_URL = "rest/sandbox/bills/count?para=";

    //退款订单查询部分URL
    private static final String REFUND_QUERY_URL = "rest/refund";

    //退款订单列表查询部分URL
    private static final String REFUNDS_QUERY_URL = "rest/refunds?para=";

    //退款订单数目查询部分URL
    private static final String REFUNDS_COUNT_QUERY_URL = "rest/refunds/count?para=";

    //退款订单查询部分URL
    private static final String REFUND_STATUS_QUERY_URL = "rest/refund/status?para=";

    //线下支付
    private static final String BILL_OFFLINE_PAY_URL = "rest/offline/bill";

    //线下订单查询
    private static final String OFFLINE_BILL_STATUS_URL = "rest/offline/bill/status";

    private static final String PLAN_URL = "plan";
    private static final String SUBSCRIPTION_URL = "subscription";

    private final static Gson GSON = new Gson();

    /**
     * 随机获取主机, 并加入API版本号
     */
    private static String getRootHost() {
        return BEECLOUD_HOST + HOST_API_VERSION;
    }

    /**
     * @return 支付请求URL
     */
    public static String getBillPayURL() {
        if (BCCache.getInstance().isTestMode) {
            return getRootHost() + BILL_PAY_SANDBOX_URL;
        } else {
            return getRootHost() + BILL_PAY_URL;
        }
    }

    public static String getNotifyPayResultSandboxUrl() {
        return getRootHost() + NOTIFY_PAY_RESULT_SANDBOX_URL
                + "/" + BCCache.getInstance().appId;
    }

    /**
     * @return 获取扫码信息URL
     */
    public static String getQRCodeReqURL() {
        return getRootHost() + BILL_PAY_URL;
    }

    /**
     * @return 查询支付订单部分URL
     */
    public static String getBillQueryURL() {
        if (BCCache.getInstance().isTestMode) {
            return getRootHost() + BILL_PAY_SANDBOX_URL;
        } else {
            return getRootHost() + BILL_PAY_URL;
        }
    }

    /**
     * @return 查询支付订单列表URL
     */
    public static String getBillsQueryURL() {
        if (BCCache.getInstance().isTestMode) {
            return getRootHost() + BILLS_QUERY_SANDBOX_URL;
        } else {
            return getRootHost() + BILLS_QUERY_URL;
        }
    }

    /**
     * @return 查询支付订单数目URL
     */
    public static String getBillsCountQueryURL() {
        if (BCCache.getInstance().isTestMode) {
            return getRootHost() + BILLS_COUNT_QUERY_SANDBOX_URL;
        } else {
            return getRootHost() + BILLS_COUNT_QUERY_URL;
        }
    }

    /**
     * @return 查询退款订单部分URL
     */
    public static String getRefundQueryURL() {
        return getRootHost() + REFUND_QUERY_URL;
    }

    /**
     * @return 查询退款订单列表URL
     */
    public static String getRefundsQueryURL() {
        return getRootHost() + REFUNDS_QUERY_URL;
    }

    /**
     * @return 查询退款订单数目URL
     */
    public static String getRefundsCountQueryURL() {
        return getRootHost() + REFUNDS_COUNT_QUERY_URL;
    }

    /**
     * @return 查询退款订单状态URL
     */
    public static String getRefundStatusURL() {
        return getRootHost() + REFUND_STATUS_QUERY_URL;
    }

    /**
     * @return 线下支付
     */
    public static String getBillOfflinePayURL() {
        return getRootHost() + BILL_OFFLINE_PAY_URL;
    }

    /**
     * @return 线下订单查询
     */
    public static String getOfflineBillStatusURL() {
        return getRootHost() + OFFLINE_BILL_STATUS_URL;
    }

    /**
     * @return 退款
     */
    public static String getRefundUrl() {
        return getRootHost() + "rest/refund";
    }

    public static String getAuthUrl() {
        return getRootHost() + "auth";
    }

    public static String getPlanUrl() {
        return getRootHost() + PLAN_URL;
    }

    public static String getSubscriptionUrl() {
        return getRootHost() + SUBSCRIPTION_URL;
    }

    public static String getSubscriptionBanksUrl() {
        return getRootHost() + "subscription_banks";
    }

    public static String getSmsCodeUrl() {
        return getRootHost() + "sms";
    }

    /**
     * http get 请求
     *
     * @param url 请求uri，如有参数在原始url后面通过 ?k1=v1&k2=v2 连接
     * @return BCHttpClientUtil.Response请求结果实例
     */
    public static Response httpGet(String url) {

        Response response = new Response();

        OkHttpClient client = new OkHttpClient.Builder()
                .connectTimeout(BCCache.getInstance().connectTimeout, TimeUnit.MILLISECONDS)
                .build();

        Request request = new Request.Builder()
                .url(url)
                .build();

        proceedRequest(client, request, response);

        return response;
    }

    private static void proceedRequest(OkHttpClient client, Request request, Response response) {
        try {
            okhttp3.Response temp = client.newCall(request).execute();
            response.code = temp.code();
            ResponseBody body = temp.body();
            response.content = body.string();
        } catch (IOException e) {
            e.printStackTrace();
            Log.w(TAG, e.getMessage() == null ? " " : e.getMessage());
            response.code = -1;
            response.content = e.getMessage();
        }
    }

    /**
     * http post 请求
     *
     * @param url     请求url
     * @param jsonStr post参数
     * @return BCHttpClientUtil.Response请求结果实例
     */
    public static Response httpPost(String url, String jsonStr) {
        Response response = new Response();

        MediaType JSON = MediaType.parse("application/json; charset=utf-8");
        OkHttpClient client = new OkHttpClient.Builder()
                .connectTimeout(BCCache.getInstance().connectTimeout, TimeUnit.MILLISECONDS)
                .build();

        RequestBody body = RequestBody.create(JSON, jsonStr);
        Request request = new Request.Builder()
                .url(url)
                .post(body)
                .build();

        proceedRequest(client, request, response);

        return response;
    }

    /**
     * http delete 请求
     *
     * @param url 请求uri，如有参数在原始url后面通过 ?k1=v1&k2=v2 连接
     * @return BCHttpClientUtil.Response请求结果实例
     */
    public static Response httpDelete(String url) {

        Response response = new Response();

        OkHttpClient client = new OkHttpClient.Builder()
                .connectTimeout(BCCache.getInstance().connectTimeout, TimeUnit.MILLISECONDS)
                .build();

        Request request = new Request.Builder()
                .url(url)
                .delete()
                .build();

        proceedRequest(client, request, response);

        return response;
    }

    /**
     * http post 请求
     *
     * @param url  请求url
     * @param para post参数
     * @return BCHttpClientUtil.Response请求结果实例
     */
    public static Response httpPost(String url, Map<String, Object> para) {
        Gson gson = new Gson();
        String param = gson.toJson(para);

        return httpPost(url, param);
    }

    // 所有super class包含的非空字段
    static Map<String, Object> objectToMap(Object object) {
        Map<String, Object> map = new HashMap<>();

        Class cls = object.getClass();
        while (cls != null) {
            for (Field field : cls.getDeclaredFields()) {
                field.setAccessible(true);

                Object value = null;
                try {
                    value = field.get(object);
                } catch (IllegalAccessException e) {
                    e.printStackTrace();
                }

                if (value != null)
                    map.put(field.getName(), value);
            }

            cls = cls.getSuperclass();
        }

        return map;
    }

    static void attachAppSign(Map<String, Object> target) {
        BCCache mCache = BCCache.getInstance();
        target.put("app_id", mCache.appId);
        long timestamp = System.currentTimeMillis();
        target.put("timestamp", timestamp);
        target.put("app_sign", BCSecurityUtil.getMessageMD5Digest(mCache.appId +
                timestamp + mCache.secret));
    }

    static String map2UrlQueryString(Map<String, Object> map) {
        StringBuilder sb = new StringBuilder();
        for (HashMap.Entry<String, Object> e : map.entrySet()) {
            try {
                sb.append(e.getKey());
                sb.append('=');
                sb.append(URLEncoder.encode(String.valueOf(e.getValue()), "UTF-8"));
            } catch (UnsupportedEncodingException e1) {
                e1.printStackTrace();
            }
            sb.append('&');
        }
        if (sb.length() == 0)
            return "";
        else
            return sb.substring(0, sb.length() - 1);
    }

    // para={} 模式
    static String map2ParaEncodeString(Map<String, Object> map) {
        if (map == null) {
            return "";
        }

        String paramStr = GSON.toJson(map);

        try {
            paramStr = URLEncoder.encode(paramStr, "UTF-8");
        } catch (UnsupportedEncodingException e) {
            Log.e("BCHttpClientUtil", "UnsupportedEncodingException");
            paramStr = "";
        }

        return "para=" + paramStr;
    }

    static BCRestfulCommonResult setCommonResult(Class<? extends BCRestfulCommonResult> className, Integer code, String errMsg, String detail) {
        BCRestfulCommonResult object = null;
        try {
            Constructor<? extends BCRestfulCommonResult> ctor = className.getConstructor(Integer.class, String.class, String.class);
            object = ctor.newInstance(code, errMsg, detail);
        } catch (Exception e) {
            e.printStackTrace();
        }

        return object;
    }

    //===================== BeeCloud Rest Object CURD =====================
    static BCRestfulCommonResult dealWithResult(Response response,
                                                Class<? extends BCRestfulCommonResult> classType) {
        if (response.content == null || response.content.length() == 0) {
            return setCommonResult(classType, BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                    BCRestfulCommonResult.APP_INNER_FAIL,
                    "JsonSyntaxException or Network Error:" + response.code + " # " + response.content);
        }

        if (response.code == 200 || (response.code >= 400 && response.code < 500)) {
            //反序列化json
            Gson gson = new Gson();

            try {
                return gson.fromJson(response.content, classType);
            } catch (JsonSyntaxException ex) {
                return setCommonResult(classType, BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                        BCRestfulCommonResult.APP_INNER_FAIL,
                        "JsonSyntaxException or Network Error:" + response.code + " # " + response.content);
            }
        } else {
            return setCommonResult(classType, BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                    BCRestfulCommonResult.APP_INNER_FAIL,
                    "Network Error:" + response.code + " # " + response.content);
        }
    }

    static BCRestfulCommonResult addRestObject(String url, Map<String, Object> reqParam,
                                               Class<? extends BCRestfulCommonResult> classType,
                                               boolean testModeNotSupport) {
        if (testModeNotSupport && BCCache.getInstance().isTestMode) {
            return setCommonResult(classType, BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                    BCRestfulCommonResult.APP_INNER_FAIL, "该功能暂不支持测试模式");
        }

        BCHttpClientUtil.attachAppSign(reqParam);
        BCHttpClientUtil.Response response = BCHttpClientUtil
                .httpPost(url, reqParam);

        return dealWithResult(response, classType);
    }

    static BCRestfulCommonResult deleteRestObject(String url, String id, Map<String, Object> reqParam,
                                                  Class<? extends BCRestfulCommonResult> classType,
                                                  boolean testModeNotSupport) {
        if (testModeNotSupport && BCCache.getInstance().isTestMode) {
            return setCommonResult(classType, BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                    BCRestfulCommonResult.APP_INNER_FAIL, "该功能暂不支持测试模式");
        }

        BCHttpClientUtil.attachAppSign(reqParam);
        String reqURL = url + "/" + id + "?" + BCHttpClientUtil.map2UrlQueryString(reqParam);

        BCHttpClientUtil.Response response = BCHttpClientUtil
                .httpDelete(reqURL);

        return dealWithResult(response, classType);
    }

    static BCRestfulCommonResult queryRestObjectById(String url, String id,
                                                  Class<? extends BCRestfulCommonResult> classType,
                                                  boolean paraQueryMode, boolean testModeNotSupport) {
        if (testModeNotSupport && BCCache.getInstance().isTestMode) {
            return setCommonResult(classType, BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                    BCRestfulCommonResult.APP_INNER_FAIL, "该功能暂不支持测试模式");
        }

        Map<String, Object> reqParam = new HashMap<>();
        BCHttpClientUtil.attachAppSign(reqParam);

        String queryStr;
        if (paraQueryMode) {
            queryStr = BCHttpClientUtil.map2ParaEncodeString(reqParam);
        } else {
            queryStr = BCHttpClientUtil.map2UrlQueryString(reqParam);
        }

        String reqURL = url + "/" + id + "?" + queryStr;
        BCHttpClientUtil.Response response = BCHttpClientUtil
                .httpGet(reqURL);

        return dealWithResult(response, classType);
    }

    static BCRestfulCommonResult queryRestObjects(String url, Map<String, Object> reqParam,
                                                  Class<? extends BCRestfulCommonResult> classType,
                                                  boolean paraQueryMode, boolean testModeNotSupport) {
        if (testModeNotSupport && BCCache.getInstance().isTestMode) {
            return setCommonResult(classType, BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                    BCRestfulCommonResult.APP_INNER_FAIL, "该功能暂不支持测试模式");
        }

        BCHttpClientUtil.attachAppSign(reqParam);

        String queryStr;
        if (paraQueryMode) {
            queryStr = BCHttpClientUtil.map2ParaEncodeString(reqParam);
        } else {
            queryStr = BCHttpClientUtil.map2UrlQueryString(reqParam);
        }

        String reqURL = url + "?" + queryStr;
        BCHttpClientUtil.Response response = BCHttpClientUtil
                .httpGet(reqURL);

        return dealWithResult(response, classType);
    }

    //===================== BeeCloud Rest Object CURD =====================

    public static class Response {
        public Integer code;
        public String content;
    }
}
