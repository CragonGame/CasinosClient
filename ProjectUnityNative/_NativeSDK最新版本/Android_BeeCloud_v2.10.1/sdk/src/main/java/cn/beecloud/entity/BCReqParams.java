/**
 * BCReqParams.java
 *
 * Created by xuanzhui on 2015/7/29.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

import java.util.Date;
import java.util.HashMap;
import java.util.Map;

import cn.beecloud.BCCache;
import cn.beecloud.BCException;
import cn.beecloud.BCSecurityUtil;

/**
 * 向服务端请求的基类
 * 包含请求的公用参数
 */
public class BCReqParams {

    //BeeCloud应用APPID
    //BeeCloud的唯一标识
    private String appId;

    //签名生成时间
    //时间戳, 毫秒数
    private Long timestamp;

    //加密签名
    //算法: md5(app_id+timestamp+app_secret), 32位16进制格式, 不区分大小写
    private String appSign;

    /**
     * 渠道类型
     * 根据不同场景选择不同的支付方式
     */
    public BCChannelTypes channel;

    /**
     * 请求类型
     */
    public enum ReqType{
        //正常的APP方式支付
        PAY,
        //查询操作
        QUERY,
        //二维码请求
        QRCODE,
        //线下类型
        OFFLINE,
        //线下扫码支付
        OFFLINE_PAY}

    /**
     * 渠道支付类型
     */
    public enum BCChannelTypes {
        /**
         * 所有渠道
         * 仅用于查询订单
         */
        ALL,

        /**
         * 微信所有渠道
         * 仅用于查询订单
         */
        WX,

        /**
         * 微信公众号二维码支付
         * 用于生成扫码和查询订单
         */
        WX_NATIVE,

        /**
         * 微信公众号支付
         * 仅用于查询订单
         */
        WX_JSAPI,

        /**
         * 微信手机原生APP支付
         */
        WX_APP,

        /**
         * 微信被扫
         */
        WX_SCAN,

        /**
         * 支付宝所有渠道
         * 仅用于查询订单
         */
        ALI,

        /**
         * 支付宝手机原生APP支付
         */
        ALI_APP,

        /**
         * 支付宝PC网页支付
         * 仅用于查询订单
         */
        ALI_WEB,

        /**
         * 支付宝内嵌二维码支付
         * 用于生成扫码和查询订单
         */
        ALI_QRCODE,

        /**
         * 支付宝线下二维码支付
         * 用于线下实体店扫码和查询订单
         */
        ALI_OFFLINE_QRCODE,

        /**
         * ALI被扫
         */
        ALI_SCAN,

        /**
         * 支付宝移动网页支付
         * 仅用于查询订单
         */
        ALI_WAP,

        /**
         * 银联所有渠道
         * 仅用于查询订单
         */
        UN,

        /**
         * 银联手机原生APP支付
         */
        UN_APP,

        /**
         * 银联PC网页支付
         * 仅用于查询订单
         */
        UN_WEB,

        /**
         * BeeCloud快捷渠道
         */
        BC,

        /**
         * BeeCloud 银联快捷支付
         */
        BC_APP,

        BC_WX_APP,
        BC_ALI_APP,

        /**
         * BeeCloud 微信Wap
         */
        BC_WX_WAP,

        /**
         * BeeCloud网关支付
         */
        BC_GATEWAY,

        /**
         * BeeCloud快捷支付
         */
        BC_EXPRESS,
        BC_ALI_SCAN,
        BC_WX_SCAN,
        BC_ALI_QRCODE,
        /**
         * BeeCloud微信扫码支付
         */
        BC_NATIVE,

        /**
         * for PayPal query
         */
        PAYPAL,

        /**
         * for PayPal test env
         */
        PAYPAL_SANDBOX,

        /**
         * for PayPal live(production) env
         */
        PAYPAL_LIVE,

        /**
         * 百度钱包APP支付
         */
        BD_APP,

        /**
         * 百度PC网页支付
         * 仅用于查询订单
         */
        BD_WEB,

        /**
         * 百度WAP网页支付
         * 仅用于查询订单
         */
        BD_WAP,

        /**
         * 百度所有渠道,
         * 用于查询
         */
        BD,

        /**
         * 京东移动网页支付
         * 仅用于查询订单
         */
        JD_WAP,

        /**
         * 京东PC网页支付
         * 仅用于查询订单
         */
        JD_WEB,

        /**
         * 京东
         * 仅用于查询订单
         */
        JD,

        /**
         * 易宝移动网页支付
         * 仅用于查询订单
         */
        YEE_WAP,

        /**
         * 易宝PC网页支付
         * 仅用于查询订单
         */
        YEE_WEB,

        /**
         * 易宝充值卡支付
         * 仅用于查询订单
         */
        YEE_NOBANKCARD,

        /**
         * 易宝
         * 仅用于查询订单
         */
        YEE,

        /**
         * 快钱移动网页支付
         * 仅用于查询订单
         */
        KUAIQIAN_WAP,

        /**
         * 快钱PC网页支付
         * 仅用于查询订单
         */
        KUAIQIAN_WEB,

        /**
         * 快钱
         * 仅用于查询订单
         */
        KUAIQIAN;

        /**
         * 判断是否为有效线下扫码支付渠道类型
         *
         * @param channel 支付渠道类型
         * @return true表示有效
         */
        public static boolean isValidOfflinePayChannelType(BCChannelTypes channel) {
            return channel == WX_SCAN ||
                    channel == ALI_SCAN ||
                    channel == BC_WX_SCAN ||
                    channel == BC_ALI_SCAN;
        }

        /**
         * 判断是否为有效线下渠道类型
         *
         * @param channel 支付渠道类型
         * @return true表示有效
         */
        public static boolean isValidOfflineChannelType(BCChannelTypes channel) {
            return channel == WX_NATIVE ||
                    channel == BC_NATIVE ||
                    channel == ALI_OFFLINE_QRCODE ||
                    channel == BC_ALI_QRCODE ||
                    channel == WX_SCAN ||
                    channel == ALI_SCAN ||
                    channel == BC_WX_SCAN ||
                    channel == BC_ALI_SCAN;
        }

        /**
         * 判断是否为有效的生成扫码的支付渠道类型
         *
         * @param channel 支付渠道类型
         * @return true表示有效
         */
        public static boolean isValidQRCodeReqChannelType(BCChannelTypes channel) {
            return channel == WX_NATIVE ||
                    channel == BC_NATIVE ||
                    channel == ALI_QRCODE ||
                    channel == ALI_OFFLINE_QRCODE ||
                    channel == BC_ALI_QRCODE;
        }

        /**
         * @param channel 支付渠道类型
         * @return 实际的渠道支付名
         */
        public static String getTranslatedChannelName(String channel) {
            if (channel.equals(WX.name()))
                return "微信支付";
            else if (channel.equals(WX_NATIVE.name()))
                return "微信公众号二维码支付";
            else if (channel.equals(WX_JSAPI.name()))
                return "微信公众号支付";
            else if (channel.equals(WX_APP.name()))
                return "微信手机原生APP支付";
            else if (channel.equals(WX_SCAN.name()))
                return "微信被扫";
            else if (channel.equals(BC_WX_WAP.name()))
                return "微信WAP支付";
            else if (channel.equals(BC_WX_APP.name()))
                return "BeeCloud微信APP支付";
            else if (channel.equals(ALI.name()))
                return "支付宝支付";
            else if (channel.equals(ALI_APP.name()))
                return "支付宝手机原生APP支付";
            else if (channel.equals(ALI_WEB.name()))
                return "支付宝PC网页支付";
            else if (channel.equals(ALI_QRCODE.name()))
                return "支付宝内嵌二维码支付";
            else if (channel.equals(ALI_OFFLINE_QRCODE.name()))
                return "支付宝线下二维码支付";
            else if (channel.equals(ALI_SCAN.name()))
                return "支付宝被扫";
            else if (channel.equals(ALI_WAP.name()))
                return "支付宝移动网页支付";
            else if (channel.equals(UN.name()))
                return "银联支付";
            else if (channel.equals(UN_APP.name()))
                return "银联手机原生APP支付";
            else if (channel.equals(UN_WEB.name()))
                return "银联PC网页支付";
            else if (channel.equals(PAYPAL.name()))
                return "PAYPAL";
            else if (channel.equals(PAYPAL_SANDBOX.name()))
                return "PAYPAL SANDBOX";
            else if (channel.equals(PAYPAL_LIVE.name()))
                return "PAYPAL LIVE";
            else if (channel.equals(BD_APP.name()))
                return "百度钱包APP支付";
            else if (channel.equals(BD_WEB.name()))
                return "百度PC网页支付";
            else if (channel.equals(BD_WAP.name()))
                return "百度WAP网页支付";
            else if (channel.equals(BD.name()))
                return "百度钱包支付";
            else if (channel.equals(JD.name()))
                return "京东支付";
            else if (channel.equals(JD_WAP.name()))
                return "京东移动网页支付";
            else if (channel.equals(JD_WEB.name()))
                return "京东PC网页支付";
            else if (channel.equals(YEE.name()))
                return "易宝支付";
            else if (channel.equals(YEE_WAP.name()))
                return "易宝移动网页支付";
            else if (channel.equals(YEE_WEB.name()))
                return "易宝PC网页支付";
            else if (channel.equals(YEE_NOBANKCARD.name()))
                return "易宝充值卡支付";
            else if (channel.equals(KUAIQIAN.name()))
                return "快钱支付";
            else if (channel.equals(KUAIQIAN_WAP.name()))
                return "快钱移动网页支付";
            else if (channel.equals(KUAIQIAN_WEB.name()))
                return "快钱PC网页支付";
            else if (channel.equals(BC.name()))
                return "BeeCloud快捷支付";
            else if (channel.equals(BC_APP.name()))
                return "BeeCloud APP支付";
            else if (channel.equals(BC_GATEWAY.name()))
                return "BeeCloud网关支付";
            else if (channel.equals(BC_EXPRESS.name()))
                return "BeeCloud快捷支付";
            else if (channel.equals(BC_NATIVE.name()))
                return "BeeCloud微信扫码支付";
            else if (channel.equals(BC_ALI_SCAN.name()))
                return "BeeCloud支付宝被扫";
            else if (channel.equals(BC_WX_SCAN.name()))
                return "BeeCloud微信被扫";
            else if (channel.equals(BC_ALI_QRCODE.name()))
                return "BeeCloud支付宝线下二维码支付";
            else
                return "其他支付类型";
        }
    }

    /**
     * BeeCloud的唯一标识
     * @return  BeeCloud应用APPID
     */
    public String getAppId() {
        return appId;
    }

    /**
     * 时间戳, 毫秒数
     * @return  签名生成时间
     */
    public Long getTimestamp() {
        return timestamp;
    }

    /**
     * 算法: md5(app_id+timestamp+app_secret), 32位16进制格式, 不区分大小写
     * @return  加密签名
     */
    public String getAppSign() {
        return appSign;
    }

    /**
     * 初始化参数
     * @param channel   渠道类型
     */
    public BCReqParams(BCChannelTypes channel) throws BCException{
        BCCache mCache = BCCache.getInstance();

        if (mCache.appId == null || mCache.secret == null) {
            throw new BCException("parameters: 请通过BeeCloud初始化appId和secret");
        } else {
            appId = mCache.appId;
            timestamp = (new Date()).getTime();
            appSign = BCSecurityUtil.getMessageMD5Digest(appId +
                    timestamp + mCache.secret);
            this.channel = channel;
        }
    }

    /**
     * 将实例转化成符合后台请求的键值对
     * 用于以json方式post请求
     */
    public Map<String, Object> transToReqMapParams() {
        Map<String, Object> params = new HashMap<String, Object>(8);

        params.put("app_id", getAppId());
        params.put("timestamp", getTimestamp());
        params.put("app_sign", getAppSign());
        params.put("channel", channel.name());

        return params;
    }
}
