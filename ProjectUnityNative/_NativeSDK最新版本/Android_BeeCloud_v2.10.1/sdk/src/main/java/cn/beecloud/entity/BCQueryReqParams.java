/**
 * BCQueryReqParams.java
 *
 * Created by xuanzhui on 2015/7/29.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

import com.google.gson.Gson;

import java.io.UnsupportedEncodingException;
import java.net.URLEncoder;
import java.util.HashMap;
import java.util.Map;

import cn.beecloud.BCException;

/**
 * 查询订单(支付|退款)参数类
 * 继承于BCReqParams
 * @see cn.beecloud.entity.BCReqParams
 */
public class BCQueryReqParams extends BCReqParams {

    /**
     * 商户订单号
     * 发起支付时填写的订单号
     * 可以为null
     */
    public String billNum;

    /**
     * 支付结果过滤,
     * true表示只返回支付成功的订单,
     * 只在查询支付订单时有效,
     * 可为null,
     * 默认返回全部
     */
    public Boolean payResult;

    /**
     * 商户退款单号
     * 发起退款时填写的退款单号
     * 仅在退款查询时候有效
     * 查询退款状态时为必填, 其它可以为null
     */
    public String refundNum;

    /**
     * 是否需要返回返回渠道详细信息,
     * true表示返回,
     * 可为null,
     * 默认不返回详细信息
     */
    public Boolean needDetail;

    /**
     * 订单生成的时间,
     * 对于支付查询, 此处表示支付订单的生成时间;
     * 对于退款查询, 此处表示退款订单的生成时间
     * 毫秒时间戳, 13位
     * 可以为null
     */
    public Long startTime;

    /**
     * 订单结束时间
     * 对于支付查询, 此处表示支付订单的结束时间;
     * 对于退款查询, 此处表示退款订单的结束时间
     * 毫秒时间戳, 13位
     * 可以为null
     */
    public Long endTime;

    /**
     * 查询起始位置
     * 默认为0. 设置为10表示忽略满足条件的前10条数据
     * 可以为null
     */
    public Integer skip;

    /**
     * 查询的条数
     * 默认为10, 最大为50. 设置为10表示只返回满足条件的10条数据
     * 可以为null
     */
    public Integer limit;

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
     * 构造函数
     * @param channel       支付渠道类型
     * @throws BCException  父类构造有可能抛出异常
     */
    public BCQueryReqParams(BCChannelTypes channel) throws BCException {
        super(channel);
    }

    /**
     * 将实例转化成符合后台请求的键值对
     * 用于以json方式post请求
     */
    public Map<String, Object> transToQueryReqMapParams() {
        Map<String, Object> params = new HashMap<String, Object>();
        params.put("app_id", getAppId());
        params.put("timestamp", getTimestamp());
        params.put("app_sign", getAppSign());

        //全渠道查询不需要传给服务端channel
        if (channel != BCChannelTypes.ALL)
            params.put("channel", channel.name());

        if (billNum != null)
            params.put("bill_no", billNum);

        if (payResult != null)
            params.put("spay_result", payResult);

        if (needDetail != null)
            params.put("need_detail", needDetail);

        if (refundNum != null)
            params.put("refund_no", refundNum);

        if (startTime != null)
            params.put("start_time", startTime);

        if (endTime != null)
            params.put("end_time", endTime);

        if (skip != null)
            params.put("skip", skip);

        if (limit != null)
            params.put("limit", limit);

        if (needApproval != null)
            params.put("need_approval", needApproval);

        return params;
    }

    /**
     * 将成员变量(包含父类成员)转化成{"key_a":1,"key_b":"value_b"}格式json字符串
     * 并通过URL encode转码
     * @return 转码后的json串
     */
    public String transToEncodedJsonString() {

        Gson gson = new Gson();
        String paramStr = gson.toJson(this.transToQueryReqMapParams());

        try {
            paramStr = URLEncoder.encode(paramStr, "UTF-8");
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
        }

        return paramStr;
    }
}
