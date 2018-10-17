/**
 * BCQuery.java
 *
 * Created by xuanzhui on 2015/7/29.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud;

import android.util.Log;

import com.google.gson.JsonSyntaxException;

import java.util.HashMap;
import java.util.Map;

import cn.beecloud.async.BCCallback;
import cn.beecloud.entity.BCBillStatus;
import cn.beecloud.entity.BCPlanCriteria;
import cn.beecloud.entity.BCPlanListResult;
import cn.beecloud.entity.BCQueryBillResult;
import cn.beecloud.entity.BCQueryBillsResult;
import cn.beecloud.entity.BCQueryCountResult;
import cn.beecloud.entity.BCQueryRefundResult;
import cn.beecloud.entity.BCQueryRefundsResult;
import cn.beecloud.entity.BCQueryReqParams;
import cn.beecloud.entity.BCRefundStatus;
import cn.beecloud.entity.BCReqParams;
import cn.beecloud.entity.BCRestfulCommonResult;
import cn.beecloud.entity.BCSubscriptionBanksResult;
import cn.beecloud.entity.BCSubscriptionCriteria;
import cn.beecloud.entity.BCSubscriptionListResult;

/**
 * 订单查询接口
 * 单例模式
 */
public class BCQuery {
    private static final String TAG = "BCQuery";

    private static BCQuery instance;
    private BCQuery() {
    }

    //查询订单类型-支付订单, 退款订单, 支付订单条数, 退款订单条数
    enum QueryOrderType{QUERY_BILLS, QUERY_REFUNDS, QUERY_BILLS_COUNT, QUERY_REFUNDS_COUNT}

    /**
     * 唯一获取BCQuery实例的入口
     * @return  BCQuery实例
     */
    public synchronized static BCQuery getInstance() {
        if (instance == null) {
            instance = new BCQuery();
        }
        return instance;
    }

    private void doErrCallBack(final QueryOrderType operation, final Integer errCode, final String errMsg,
                                final String errDetail, final BCCallback callback){
        switch (operation){
            case QUERY_BILLS:
                callback.done(new BCQueryBillsResult(errCode, errMsg, errDetail));
                break;
            case QUERY_REFUNDS:
                callback.done(new BCQueryRefundsResult(errCode, errMsg, errDetail));
                break;
            case QUERY_BILLS_COUNT:
            case QUERY_REFUNDS_COUNT:
                callback.done(new BCQueryCountResult(errCode, errMsg, errDetail));
        }
    }

    /**
     * 查询订单主入口
     * @param channel       支付渠道类型
     * @param operation     发起的操作类型
     * @param billNum       发起支付时填写的订单号, 可为null
     * @param payResult     true表示只返回成功的支付订单, 可为null
     * @param refundNum     退款的单号, 可为null
     * @param needApproval  true表示只返回预退款, 可为null, 默认返回全部退款订单
     * @param startTime     订单生成时间, 毫秒时间戳, 13位, 可为null
     * @param endTime       订单完成时间, 毫秒时间戳, 13位, 可为null
     * @param skip          忽略的记录个数, 默认为0, 设置为10表示忽略满足条件的前10条数据, 可为null
     * @param limit         本次抓取的记录数, 默认为10, [10, 50]之间, 设置为10表示只返回满足条件的10条数据, 可为null
     * @param needDetail    true表示需要返回商户返回的详细信息, 可为null, 默认不返回详细信息
     * @param callback      回调函数
     */
    protected void queryOrdersAsync(final BCReqParams.BCChannelTypes channel,
                                    final QueryOrderType operation,
                                    final String billNum, final Boolean payResult,
                                    final String refundNum, final Boolean needApproval,
                                    final Long startTime, final Long endTime,
                                    final Integer skip, final Integer limit,
                                    final Boolean needDetail, final BCCallback callback) {
        if (callback == null) {
            Log.w(TAG, "请初始化callback");
            return;
        }

        BCCache.executorService.execute(new Runnable() {
            @Override
            public void run() {
                if (BCCache.getInstance().isTestMode &&
                        (operation == QueryOrderType.QUERY_REFUNDS ||
                         operation == QueryOrderType.QUERY_REFUNDS_COUNT)) {
                    doErrCallBack(operation, BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL,
                            "该功能暂不支持测试模式",
                            callback);

                    return;
                }

                if (channel == null) {
                    doErrCallBack(operation, BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL,
                            "Channel should not be null, set it to ALL if you want to query all records.",
                            callback);

                    return;
                }

                BCQueryReqParams bcQueryReqParams;
                try {
                    bcQueryReqParams = new BCQueryReqParams(channel);
                } catch (BCException e) {
                    doErrCallBack(operation, BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL, e.getMessage(), callback);
                    return;
                }

                //common
                bcQueryReqParams.billNum = billNum;
                bcQueryReqParams.startTime = startTime;
                bcQueryReqParams.endTime = endTime;

                String queryURL = null;

                switch (operation) {
                    case QUERY_BILLS:
                        queryURL = BCHttpClientUtil.getBillsQueryURL();
                        bcQueryReqParams.payResult = payResult;
                        bcQueryReqParams.needDetail = needDetail;
                        bcQueryReqParams.skip = skip;
                        bcQueryReqParams.limit = limit;
                        break;
                    case QUERY_REFUNDS:
                        queryURL = BCHttpClientUtil.getRefundsQueryURL();
                        bcQueryReqParams.refundNum = refundNum;
                        bcQueryReqParams.needDetail = needDetail;
                        bcQueryReqParams.skip = skip;
                        bcQueryReqParams.limit = limit;
                        bcQueryReqParams.needApproval = needApproval;
                        break;
                    case QUERY_BILLS_COUNT:
                        queryURL = BCHttpClientUtil.getBillsCountQueryURL();
                        bcQueryReqParams.payResult = payResult;
                        break;
                    case QUERY_REFUNDS_COUNT:
                        queryURL = BCHttpClientUtil.getRefundsCountQueryURL();
                        bcQueryReqParams.refundNum = refundNum;
                        bcQueryReqParams.needApproval = needApproval;
                }

                //Log.w("BCQuery",queryURL + bcQueryReqParams.transToEncodedJsonString());

                BCHttpClientUtil.Response response = BCHttpClientUtil.httpGet(queryURL +
                        bcQueryReqParams.transToEncodedJsonString());

                if (response.code == 200 || (response.code >= 400 && response.code < 500)) {
                    String ret = response.content;
                    //Log.w("BCQuery", ret);

                    switch (operation) {
                        case QUERY_BILLS:
                            try {
                                callback.done(BCQueryBillsResult.transJsonToResultObject(ret));
                            } catch (JsonSyntaxException ex) {
                                callback.done(new BCQueryBillsResult(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                        BCRestfulCommonResult.APP_INNER_FAIL,
                                        "JsonSyntaxException or Network Error:" + response.code + " # " + response.content));
                            }
                            break;
                        case QUERY_REFUNDS:
                            try {
                                callback.done(BCQueryRefundsResult.transJsonToResultObject(ret));
                            } catch (JsonSyntaxException ex) {
                                callback.done(new BCQueryRefundsResult(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                        BCRestfulCommonResult.APP_INNER_FAIL,
                                        "JsonSyntaxException or Network Error:" + response.code + " # " + response.content));
                            }
                            break;
                        case QUERY_BILLS_COUNT:
                        case QUERY_REFUNDS_COUNT:
                            try {
                                callback.done(BCQueryCountResult.transJsonToObject(ret));
                            } catch (JsonSyntaxException ex) {
                                callback.done(new BCQueryCountResult(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                        BCRestfulCommonResult.APP_INNER_FAIL,
                                        "JsonSyntaxException or Network Error:" + response.code + " # " + response.content));
                            }
                            break;
                        default:
                            doErrCallBack(operation, BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                    BCRestfulCommonResult.APP_INNER_FAIL, "Invalid channel", callback);
                    }

                } else {
                    doErrCallBack(operation, BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL,
                            "Network Error:" + response.code + " # " + response.content, callback);
                }
            }
        });

    }

    /**
     * 查询支付订单主入口
     * @param channel       支付渠道类型, 若为ALL则查询全部订单
     * @param billNum       发起支付时填写的订单号, 可为null
     * @param startTime     支付订单生成时间, 毫秒时间戳, 13位, 可为null
     * @param endTime       支付订单完成时间, 毫秒时间戳, 13位, 可为null
     * @param skip          忽略的记录个数, 默认为0, 设置为10表示忽略满足条件的前10条数据, 可为null
     * @param limit         本次抓取的记录数, 默认为10, [10, 50]之间, 设置为10表示只返回满足条件的10条数据, 可为null
     * @param callback      回调函数
     */
    public void queryBillsAsync(final BCReqParams.BCChannelTypes channel, final String billNum,
                                final Long startTime, final Long endTime,
                                final Integer skip, final Integer limit, final BCCallback callback) {
        queryOrdersAsync(channel, QueryOrderType.QUERY_BILLS, billNum,
                null, null, null, startTime, endTime, skip, limit, null, callback);
    }

    /**
     * 根据支付渠道获取订单
     * @param channel       支付渠道, 若为ALL则查询全部订单
     * @param callback      回调接口
     */
    public void queryBillsAsync(final BCReqParams.BCChannelTypes channel, final BCCallback callback) {
        queryBillsAsync(channel, null, null, null, null, null, callback);
    }

    /**
     * 根据支付渠道和订单号获取订单
     * @param channel       支付渠道, 若为ALL则查询全部订单
     * @param billNum       订单号
     * @param callback      回调接口
     */
    public void queryBillsAsync(final BCReqParams.BCChannelTypes channel, final String billNum, final BCCallback callback) {
        queryBillsAsync(channel, billNum, null, null, null, null, callback);
    }

    /**
     * 查询支付订单
     * @param queryParams    @see cn.beecloud.BCQuery.QueryParams 查询参数
     * @param callback       回调接口
     */
    public void queryBillsAsync(final QueryParams queryParams, final BCCallback callback) {
        if (queryParams == null) {
            Log.w(TAG, "查询参数queryParams不可为null");
            return;
        }

        queryOrdersAsync(queryParams.channel,
                QueryOrderType.QUERY_BILLS,
                queryParams.billNum,
                queryParams.payResult,
                null,
                null,
                queryParams.startTime,
                queryParams.endTime,
                queryParams.skip,
                queryParams.limit,
                queryParams.needDetail,
                callback);
    }

    /**
     * 查询退款订单主入口
     * @param channel       支付渠道类型, 若为ALL则查询全部订单
     * @param billNum       发起支付时填写的订单号, 可为null
     * @param refundNum     发起退款时填写的订单号, 可为null
     * @param startTime     退款订单生成时间, 毫秒时间戳, 13位, 可为null
     * @param endTime       退款订单完成时间, 毫秒时间戳, 13位, 可为null
     * @param skip          忽略的记录个数, 默认为0, 设置为10表示忽略满足条件的前10条数据, 可为null
     * @param limit         本次抓取的记录数, 默认为10, [10, 50]之间, 设置为10表示只返回满足条件的10条数据, 可为null
     * @param callback      回调函数
     */
    public void queryRefundsAsync(final BCReqParams.BCChannelTypes channel, final String billNum, final String refundNum,
                                final Long startTime, final Long endTime,
                                final Integer skip, final Integer limit, final BCCallback callback) {
        queryOrdersAsync(channel, QueryOrderType.QUERY_REFUNDS, billNum,
                null, refundNum, null, startTime, endTime, skip, limit, null, callback);
    }

    /**
     * 根据支付渠道获取退款订单列表
     * @param channel       支付渠道, 若为ALL则查询全部订单
     * @param callback      回调接口
     */
    public void queryRefundsAsync(final BCReqParams.BCChannelTypes channel, final BCCallback callback) {
        queryRefundsAsync(channel, null, null, null, null, null, null, callback);
    }

    /**
     * 根据支付渠道和(支付订单号|退款单号)获取退款订单列表
     * @param channel       支付渠道, 若为ALL则查询全部订单
     * @param billNum       支付订单号, 可为null
     * @param refundNum     退款单号, 可为null
     * @param callback      回调接口
     */
    public void queryRefundsAsync(final BCReqParams.BCChannelTypes channel, final String billNum,
                                  final String refundNum, final BCCallback callback) {
        queryRefundsAsync(channel, billNum, refundNum, null, null, null, null, callback);
    }

    /**
     * 查询退款订单列表
     * @param queryParams   @see cn.beecloud.BCQuery.QueryParams 查询参数
     * @param callback      回调接口
     */
    public void queryRefundsAsync(final QueryParams queryParams, final BCCallback callback) {
        if (queryParams == null) {
            Log.w(TAG, "查询参数queryParams不可为null");
            return;
        }

        queryOrdersAsync(queryParams.channel,
                QueryOrderType.QUERY_REFUNDS,
                queryParams.billNum,
                null,
                queryParams.refundNum,
                queryParams.needApproval,
                queryParams.startTime,
                queryParams.endTime,
                queryParams.skip,
                queryParams.limit,
                queryParams.needDetail,
                callback);
    }

    /**
     * 查询支付订单数目
     * @param queryParams    @see cn.beecloud.BCQuery.QueryParams 查询参数
     * @param callback       回调接口
     */
    public void queryBillsCountAsync(final QueryParams queryParams, final BCCallback callback) {
        if (queryParams == null) {
            Log.w(TAG, "查询参数queryParams不可为null");
            return;
        }

        queryOrdersAsync(queryParams.channel,
                QueryOrderType.QUERY_BILLS_COUNT,
                queryParams.billNum,
                queryParams.payResult,
                null,
                null,
                queryParams.startTime,
                queryParams.endTime,
                null,
                null,
                null,
                callback);
    }

    /**
     * 查询退款订单数目
     * @param queryParams   @see cn.beecloud.BCQuery.QueryParams 查询参数
     * @param callback      回调接口
     */
    public void queryRefundsCountAsync(final QueryParams queryParams, final BCCallback callback) {
        if (queryParams == null) {
            Log.w(TAG, "查询参数queryParams不可为null");
            return;
        }

        queryOrdersAsync(queryParams.channel,
                QueryOrderType.QUERY_REFUNDS_COUNT,
                queryParams.billNum,
                null,
                queryParams.refundNum,
                queryParams.needApproval,
                queryParams.startTime,
                queryParams.endTime,
                null,
                null,
                null,
                callback);
    }

    /**
     * 获取退款状态信息
     * @param channel       支付的渠道, 目前支持微信WX、YEE、KUAIQIAN、BD
     * @param refundNum     退款单号
     * @param callback      回调入口
     */
    public void queryRefundStatusAsync(final BCReqParams.BCChannelTypes channel, final String refundNum,
                                       final BCCallback callback) {
        if (callback == null) {
            Log.w(TAG, "请初始化callback");
            return;
        }

        BCCache.executorService.execute(new Runnable() {
            @Override
            public void run() {
                if (BCCache.getInstance().isTestMode) {
                    callback.done(new BCRefundStatus(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL, "该功能暂不支持测试模式", null));
                    return;
                }

                if (refundNum == null) {
                    callback.done(new BCRefundStatus(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL, "refundNum不能为null", null));
                    return;
                }

                if (!(channel.equals(BCReqParams.BCChannelTypes.WX) ||
                        channel.equals(BCReqParams.BCChannelTypes.YEE) ||
                        channel.equals(BCReqParams.BCChannelTypes.KUAIQIAN) ||
                        channel.equals(BCReqParams.BCChannelTypes.BD))) {
                    callback.done(new BCRefundStatus(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL, "退款状态查询的channel参数只能是WX、YEE、KUAIQIAN或BD", null));
                    return;
                }

                BCQueryReqParams bcQueryReqParams;
                try {
                    bcQueryReqParams = new BCQueryReqParams(channel);
                } catch (BCException e) {
                    callback.done(new BCRefundStatus(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL, e.getMessage(), null));
                    return;
                }

                bcQueryReqParams.refundNum = refundNum;

                String queryURL = BCHttpClientUtil.getRefundStatusURL();

                //Log.w("BCQuery", queryURL + bcQueryReqParams.transToEncodedJsonString());

                BCHttpClientUtil.Response response = BCHttpClientUtil.httpGet(queryURL +
                        bcQueryReqParams.transToEncodedJsonString());

                if (response.code == 200 || (response.code >= 400 && response.code < 500)) {

                    String ret = response.content;
                    try {
                        callback.done(BCRefundStatus.transJsonToObject(ret));
                    } catch (JsonSyntaxException ex) {
                        callback.done(new BCRefundStatus(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                BCRestfulCommonResult.APP_INNER_FAIL,
                                "JsonSyntaxException or Network Error:" + response.code + " # " + response.content));
                    }

                } else {
                    callback.done(new BCRefundStatus(
                            BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL,
                            "Network Error:" + response.code + " # " + response.content, null));
                }
            }
        });
    }

    /**
     * 根据ID查询支付订单
     * @param id        支付订单唯一标识，注意此处不是订单号
     * @param callback  回调入口
     */
    public void queryBillByIDAsync(final String id, final BCCallback callback) {
        if (callback == null) {
            Log.w(TAG, "请初始化callback");
            return;
        }

        BCCache.executorService.execute(new Runnable() {
            @Override
            public void run() {

                if (id == null) {
                    callback.done(new BCQueryBillResult(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL, "id NPE!"));

                    return;
                }

                BCQueryReqParams bcQueryReqParams;
                try {
                    bcQueryReqParams = new BCQueryReqParams(BCReqParams.BCChannelTypes.ALL);
                } catch (BCException e) {
                    callback.done(new BCQueryBillResult(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL, e.getMessage()));
                    return;
                }

                String queryURL = BCHttpClientUtil.getBillQueryURL() + "/"
                        + id + "?para=";

                //Log.w("BCQuery", queryURL + bcQueryReqParams.transToEncodedJsonString());

                BCHttpClientUtil.Response response = BCHttpClientUtil.httpGet(queryURL +
                        bcQueryReqParams.transToEncodedJsonString());

                if (response.code == 200 || (response.code >= 400 && response.code < 500)) {
                    try {
                        callback.done(BCQueryBillResult.transJsonToResultObject(response.content));
                    } catch (JsonSyntaxException ex) {
                        callback.done(new BCQueryBillResult(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                BCRestfulCommonResult.APP_INNER_FAIL,
                                "JsonSyntaxException or Network Error:" + response.code + " # " + response.content));
                    }
                } else {
                    callback.done(new BCQueryBillResult(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL,
                            "Network Error:" + response.code + " # " + response.content));
                }
            }
        });
    }

    /**
     * 根据ID查询退款订单
     * @param id        退款订单唯一标识，注意此处不是退款单号
     * @param callback  回调入口
     */
    public void queryRefundByIDAsync(final String id, final BCCallback callback) {
        if (callback == null) {
            Log.w(TAG, "请初始化callback");
            return;
        }

        BCCache.executorService.execute(new Runnable() {
            @Override
            public void run() {
                if (BCCache.getInstance().isTestMode) {
                    callback.done(new BCQueryRefundResult(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL, "该功能暂不支持测试模式"));

                    return;
                }

                if (id == null) {
                    callback.done(new BCQueryRefundResult(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL, "id NPE!"));

                    return;
                }

                BCQueryReqParams bcQueryReqParams;
                try {
                    bcQueryReqParams = new BCQueryReqParams(BCReqParams.BCChannelTypes.ALL);
                } catch (BCException e) {
                    callback.done(new BCQueryRefundResult(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL, e.getMessage()));
                    return;
                }

                String queryURL = BCHttpClientUtil.getRefundQueryURL() + "/"
                        + id + "?para=";

                //Log.w("BCQuery", queryURL + bcQueryReqParams.transToEncodedJsonString());

                BCHttpClientUtil.Response response = BCHttpClientUtil.httpGet(queryURL +
                        bcQueryReqParams.transToEncodedJsonString());

                if (response.code == 200 || (response.code >= 400 && response.code < 500)) {
                    try {
                        callback.done(BCQueryRefundResult.transJsonToResultObject(response.content));
                    } catch (JsonSyntaxException ex) {
                        callback.done(new BCQueryRefundResult(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                BCRestfulCommonResult.APP_INNER_FAIL,
                                "JsonSyntaxException or Network Error:" + response.code + " # " + response.content));
                    }
                } else {
                    callback.done(new BCQueryRefundResult(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL,
                            "Network Error:" + response.code + " # " + response.content));
                }
            }
        });
    }

    /**
     * 查询线下订单状态
     *
     * @param channelType     目前只支持WX_NATIVE, WX_SCAN, ALI_OFFLINE_QRCODE, ALI_SCAN
     *                        @see cn.beecloud.entity.BCReqParams.BCChannelTypes
     * @param billNum         订单号
     * @param callback        支付完成后的回调函数
     */
    public void queryOfflineBillStatusAsync(final BCReqParams.BCChannelTypes channelType,
                                          final String billNum,
                                          final BCCallback callback) {

        if (callback == null) {
            Log.w(TAG, "请初始化callback");
            return;
        }

        BCCache.executorService.execute(new Runnable() {
            @Override
            public void run() {
                if (BCCache.getInstance().isTestMode){
                    callback.done(new BCBillStatus(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL,
                            "该功能暂不支持测试模式"));
                    return;
                }

                //校验并准备公用参数
                BCReqParams parameters;
                try {
                    parameters = new BCReqParams(channelType);
                } catch (BCException e) {
                    callback.done(new BCBillStatus(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL,
                            e.getMessage()));
                    return;
                }

                if (billNum == null ||
                        billNum.length() == 0){
                    callback.done(new BCBillStatus(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL,
                            "invalid bill number"));
                    return;
                }

                String queryURL = BCHttpClientUtil.getOfflineBillStatusURL();

                Map<String, Object> reqMap = parameters.transToReqMapParams();
                reqMap.put("bill_no", billNum);

                BCHttpClientUtil.Response response = BCHttpClientUtil.httpPost(queryURL, reqMap);

                if (response.code == 200 || (response.code >= 400 && response.code < 500)) {

                    //返回后台结果
                    try {
                        callback.done(BCBillStatus.transJsonToObject(response.content));
                    } catch (JsonSyntaxException ex) {
                        callback.done(new BCBillStatus(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                                BCRestfulCommonResult.APP_INNER_FAIL,
                                "JsonSyntaxException or Network Error:" + response.code + " # " + response.content));
                    }

                } else {
                    callback.done(new BCBillStatus(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL,
                            "Network Error:" + response.code + " # " + response.content));
                }

            }
        });
    }

    /**
     * 获取订阅支付支持的银行列表
     * @param callback  回调入口
     */
    public void subscriptionSupportedBanks(final BCCallback callback) {
        if (callback == null) {
            Log.e(TAG, "请初始化callback");
            return;
        }

        BCCache.executorService.execute(new Runnable() {
            @Override
            public void run() {
                Map<String, Object> reqMap = new HashMap<>();
                callback.done(BCHttpClientUtil.queryRestObjects(
                        BCHttpClientUtil.getSubscriptionBanksUrl(), reqMap,
                        BCSubscriptionBanksResult.class, false, true));
            }
        });
    }

    /**
     * 查询订阅计划列表
     * @param criteria 订阅计划的查询条件，可以为null
     * @param callback  回调入口
     */
    public void queryPlans(final BCPlanCriteria criteria, final BCCallback callback) {
        if (callback == null) {
            Log.e(TAG, "请初始化callback");
            return;
        }

        BCCache.executorService.execute(new Runnable() {
            @Override
            public void run() {
                Map<String, Object> reqMap;
                if (criteria == null)
                    reqMap = new HashMap<>();
                else
                    reqMap = BCHttpClientUtil.objectToMap(criteria);

                callback.done(BCHttpClientUtil.queryRestObjects(BCHttpClientUtil.getPlanUrl(),
                        reqMap, BCPlanListResult.class, false, true));
            }
        });
    }

    /**
     * 查询订阅列表
     * @param criteria 查询订阅的条件，可以为null
     * @param callback  回调入口
     */
    public void querySubscriptions(final BCSubscriptionCriteria criteria, final BCCallback callback) {
        if (callback == null) {
            Log.e(TAG, "请初始化callback");
            return;
        }

        BCCache.executorService.execute(new Runnable() {
            @Override
            public void run() {
                if (BCCache.getInstance().isTestMode) {
                    callback.done(new BCSubscriptionListResult(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                            BCRestfulCommonResult.APP_INNER_FAIL, "该功能暂不支持测试模式"));
                    return;
                }

                Map<String, Object> reqMap;
                if (criteria == null)
                    reqMap = new HashMap<>();
                else
                    reqMap = BCHttpClientUtil.objectToMap(criteria);

                callback.done(BCHttpClientUtil.queryRestObjects(BCHttpClientUtil.getSubscriptionUrl(),
                        reqMap, BCSubscriptionListResult.class, false, true));
            }
        });
    }

    /**
     * 查询订单和退款的参数类
     */
    public static class QueryParams {
        /**
         * 支付渠道类型
         */
        public BCReqParams.BCChannelTypes channel;

        /**
         * 发起支付时填写的订单号, 可为null
         */
        public String billNum;

        /**
         * 支付结果过滤, true表示只返回支付成功的订单(订单是否退款对该标志位无影响),
         * 只在查询支付订单时有效, 可为null, 默认返回全部
         */
        public Boolean payResult;

        /**
         * 退款的单号, 查询退款订单的时候填写, 可为null
         */
        public String refundNum;

        /**
         * 标识退款记录是否为预退款,
         * 仅在查询退款相关记录时用到,
         * true表示查询预退款,
         * false表示查询直接退款的订单,
         * 可为null, 默认查询全部退款订单,
         * 如果发起了预退款, 然后正式退款已经完成, 此时设为true将查不到该记录,
         * 如果是查询支付订单相关记录, 该参数会被忽略
         */
        public Boolean needApproval;

        /**
         * 查询起始时间, 毫秒时间戳, 13位, 可为null
         */
        public Long startTime;

        /**
         * 结束时间, 毫秒时间戳, 13位, 可为null
         */
        public Long endTime;

        /**
         * 跳过的记录个数, 默认为0, 设置为10表示跳过满足条件的前10条数据, 可为null,
         * 如果是查询订单数目, 该参数会被忽略
         */
        public Integer skip;

        /**
         * 本次抓取的记录数, 默认为10, [10, 50]之间, 设置为10表示只返回满足条件的10条数据, 可为null,
         * 如果是查询订单数目, 该参数会被忽略
         */
        public Integer limit;

        /**
         * 是否需要返回返回渠道详细信息, true表示返回,
         * 可为null, 默认不返回详细信息
         * 如果是查询订单数目, 该参数会被忽略
         */
        public Boolean needDetail;
    }
}
