/**
 * BCPayReqParams.java
 *
 * Created by xuanzhui on 2015/7/27.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

import java.util.HashMap;
import java.util.Map;

import cn.beecloud.BCException;
import cn.beecloud.BeeCloud;

/**
 * 支付参数类
 * 继承于BCReqParams
 * @see cn.beecloud.entity.BCReqParams
 */
public class BCPayReqParams extends BCReqParams {

    /**
     * 订单总金额
     * 须是正整数, 单位为分
     */
    public Integer totalFee;

    /**
     * 商户订单号
     * 32个字符内, 数字或字母组合, 请自行确保在商户系统中唯一 ,同一订单号不可重复提交, 否则会造成订单重复
     */
    public String billNum;

    /**
     * 购买人在商户系统的ID，传入该信息会在dashboard显示用户行为分析
     */
    public String buyerId;

    /**
     * 卡券ID
     */
    public String couponId;

    /**
     * 订单标题
     * UTF8编码格式, 32个字节内, 最长支持16个汉字
     */
    public String title;

    /**
     * 授权码
     */
    public String authCode;

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
     * PayPal token;
     */
    public String accessToken;

    /**
     * PayPal currency
     */
    public String currency;

    /**
     * baidu timeout(seconds)
     */
    public Integer billTimeout;

    /**
     * 异步回调地址
     */
    public String notifyUrl;

    /**
     * BC_EXPRESS支付卡号
     */
    public String cardNum;

    /**
     * 附加数据
     * 用户自定义的参数, 将会在webhook通知中原样返回, 该字段主要用于商户携带订单的自定义数据
     */
    public Map<String, String> optional;

    /**
     * 附加数据
     * 用于后期统计，目前只支持key为category的分类统计
     */
    public Map<String, String> analysis;

    /**
     * 支付宝内嵌二维码类型
     * 注: 二维码类型含义
     * "0": 订单码-简约前置模式, 对应 iframe 宽度不能小于 600px, 高度不能小于 300px
     * "1": 订单码-前置模式, 对应 iframe 宽度不能小于 300px, 高度不能小于 600px
     * "3": 订单码-迷你前置模式, 对应 iframe 宽度不能小于 75px, 高度不能小于 75px
     */
    public String qrPayMode;

    /**
     * 部分网页支付类型的必填参数
     * 同步返回页面
     * 支付渠道处理完请求后,当前页面自动跳转到商户网站里指定页面的http路径
     */
    public String returnUrl;

    /**
     * 构造函数
     * @param channel       支付渠道类型
     * @throws BCException  父类构造有可能抛出异常
     */
    public BCPayReqParams(BCChannelTypes channel) throws BCException {
        super(channel);
    }

    /**
     * 将实例转化成符合后台请求的键值对
     * 用于以json方式post请求
     */
    public Map<String, Object> transToBillReqMapParams(){
        Map<String, Object> params = new HashMap<String, Object>(8);

        params.put("app_id", getAppId());
        params.put("timestamp", getTimestamp());
        params.put("app_sign", getAppSign());
        params.put("channel", channel.name());
        params.put("total_fee", totalFee);
        params.put("bill_no", billNum);
        params.put("title", title);

        if (buyerId != null) {
            params.put("buyer_id", buyerId);
        }

        if (couponId != null) {
            params.put("coupon_id", couponId);
        }

        if (accessToken != null)
            params.put("access_token",accessToken);

        if (currency != null)
            params.put("currency", currency);

        if (notifyUrl != null)
            params.put("notify_url", notifyUrl);

        if (optional !=null && optional.size() != 0)
            params.put("optional", optional);

        if (analysis !=null && analysis.size() != 0)
            params.put("analysis", analysis);

        Map<String, String> bcAnalysis = new HashMap<>();
        bcAnalysis.put("sdk_version", "ANDROID_" + BeeCloud.BEECLOUD_ANDROID_SDK_VERSION);
        params.put("bc_analysis", bcAnalysis);

        if (qrPayMode != null)
            params.put("qr_pay_mode", qrPayMode);

        if (returnUrl != null)
            params.put("return_url", returnUrl);

        if (cardNum != null)
            params.put("card_no", cardNum);

        if (billTimeout != null)
            params.put("bill_timeout", billTimeout);

        if (authCode!=null && authCode.length() != 0)
            params.put("auth_code", authCode);

        if (terminalId!=null && terminalId.length() != 0)
            params.put("terminal_id", terminalId);

        if (storeId!=null && storeId.length() != 0)
            params.put("store_id", storeId);

        return params;
    }
}
