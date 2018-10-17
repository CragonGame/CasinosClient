/**
 * BCPayResult.java
 *
 * Created by xuanzhui on 2015/7/27.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
*/
package cn.beecloud.entity;

import cn.beecloud.async.BCResult;

/**
 * 支付结果返回类
 *
 * 不直接继承BCRestfulCommonResult, 因为需要对服务端和app端返回的结果封装
 * 包含支付结果
 * 错误分类, 支付成功或用户取消时非有效字段
 * 详细信息
 *
 * @see cn.beecloud.async.BCResult
 */
public class BCPayResult implements BCResult {
    //result包含支付成功、取消支付、支付失败
    private String result;
    //针对支付失败的情况，提供失败代码
    private Integer errCode;
    //针对支付失败的情况，提供失败原因
    private String errMsg;
    //提供详细的支付信息，比如原生的支付宝返回信息
    private String detailInfo;
    //成功发起支付后返回支付表记录唯一标识
    private String id;
    //返回网页渠道的重定向链接或者是提交表单
    private String url;
    private String html;

    public static final int APP_PAY_SUCC_CODE = 0;
    public static final int APP_PAY_CANCEL_CODE = -1;

    public static final int APP_INTERNAL_PARAMS_ERR_CODE = -10;
    public static final int APP_INTERNAL_NETWORK_ERR_CODE = -11;
    public static final int APP_INTERNAL_THIRD_CHANNEL_ERR_CODE = -12;
    public static final int APP_INTERNAL_EXCEPTION_ERR_CODE = -13;

    /**
     * 表示支付成功
     */
    public static final String RESULT_SUCCESS = "SUCCESS";

    /**
     * 表示用户取消支付
     */
    public static final String RESULT_CANCEL = "CANCEL";

    /**
     * 表示支付失败
     */
    public static final String RESULT_FAIL = "FAIL";

    /**
     * 表示支付结果状态未知
     */
    public static final String RESULT_UNKNOWN = "UNKNOWN";

    /**
     * 表示支付中，未获取确认信息
     */
    public static final String RESULT_PAYING_UNCONFIRMED = "RESULT_PAYING_UNCONFIRMED";

    /**
     * 针对银联，存在插件不存在需要安装的问题
     */
    public static final String FAIL_PLUGIN_NOT_INSTALLED = "FAIL_PLUGIN_NOT_INSTALLED";

    /**
     * 针对银联，存在插件需要升级的问题
     */
    public static final String FAIL_PLUGIN_NEED_UPGRADE = "FAIL_PLUGIN_NEED_UPGRADE";

    /**
     * 网络问题造成的支付失败
     */
    public static final String FAIL_NETWORK_ISSUE = "FAIL_NETWORK_ISSUE";

    /**
     * 参数不合法造成的支付失败
     */
    public static final String FAIL_INVALID_PARAMS = "FAIL_INVALID_PARAMS";

    /**
     * 从第三方app支付渠道返回的错误信息
     */
    public static final String FAIL_ERR_FROM_CHANNEL = "FAIL_ERR_FROM_CHANNEL";

    /**
     * 支付过程中的Exception
     */
    public static final String FAIL_EXCEPTION = "FAIL_EXCEPTION";

    /**
     * 构造函数
     * @param result        包含支付成功, 用户取消支付, 支付失败
     * @param errMsg        支付失败的分类错误信息
     * @param detailInfo    详细的支付结果信息, 对于错误显示详细的错误信息
     */
    public BCPayResult(String result, Integer errCode,
                       String errMsg, String detailInfo) {
        this.result = result;
        this.id = null;
        this.errCode = errCode;
        this.errMsg = errMsg;
        this.detailInfo = detailInfo;
    }

    /**
     * 构造函数
     * @param result        包含支付成功, 用户取消支付, 支付失败
     * @param errMsg        支付失败的分类错误信息
     * @param detailInfo    详细的支付结果信息, 对于错误显示详细的错误信息
     * @param id            成功发起支付后返回支付表记录唯一标识
     */
    public BCPayResult(String result, Integer errCode, String errMsg, String detailInfo, String id) {
        this.result = result;
        this.errCode = errCode;
        this.errMsg = errMsg;
        this.detailInfo = detailInfo;
        this.id = id;
    }

    /**
     * 构造函数
     * @param result        包含支付成功, 用户取消支付, 支付失败
     * @param errMsg        支付失败的分类错误信息
     * @param detailInfo    详细的支付结果信息, 对于错误显示详细的错误信息
     * @param id            成功发起支付后返回支付表记录唯一标识
     * @param url           网页类型的支付请求重定向地址
     * @param html          网页类型的支付请求提交表单
     */
    public BCPayResult(String result, Integer errCode, String errMsg, String detailInfo, String id,
                       String url, String html) {
        this.result = result;
        this.errCode = errCode;
        this.errMsg = errMsg;
        this.detailInfo = detailInfo;
        this.id = id;
        this.url = url;
        this.html = html;
    }

    /**
     * @return  支付结果
     */
    public String getResult() {
        return result;
    }

    /**
     * @return  错误代号
     */
    public Integer getErrCode() {
        return errCode;
    }

    /**
     * @return  支付失败的分类错误信息
     */
    public String getErrMsg() {
        return errMsg;
    }

    /**
     * @return  详细的支付结果信息, 对于错误显示详细的错误信息
     */
    public String getDetailInfo() {
        return detailInfo;
    }

    /**
     * @return 成功发起支付后返回支付表记录唯一标识
     */
    public String getId() {
        return id;
    }

    /**
     * @return 网页类型的支付请求重定向地址
     */
    public String getUrl() {
        return url;
    }

    /**
     * @return 网页类型的支付请求提交表单
     */
    public String getHtml() {
        return html;
    }
}
