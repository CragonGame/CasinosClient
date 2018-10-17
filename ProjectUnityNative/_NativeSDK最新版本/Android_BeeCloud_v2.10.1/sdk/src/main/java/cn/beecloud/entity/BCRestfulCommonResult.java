/**
 * BCRestfulCommonResult.java
 *
 * Created by xuanzhui on 2015/7/29.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

import cn.beecloud.async.BCResult;

/**
 * 服务端返回的通用结果信息
 *
 * @see cn.beecloud.async.BCResult
 */
public class BCRestfulCommonResult implements BCResult {
    /**
     * APP内部错误编号
     */
    public static final Integer APP_INNER_FAIL_NUM = -1;
    /**
     * APP内部错误
     */
    public static final String APP_INNER_FAIL = "APP_INNER_FAIL";

    /**
     * 以下命名需要和restful API匹配
     * 以便于Gson反序列化
     * 请忽略命名规则
     */
    //返回码, 0为正常
    protected Integer result_code;

    //返回信息, OK为正常
    protected String result_msg;

    //具体错误信息
    protected String err_detail;

    /**
     * @return  0表示请求成功, 其他为错误编号
     */
    public Integer getResultCode() {
        return result_code;
    }

    /**
     * @return  OK表示请求成功, 其他为错误信息
     */
    public String getResultMsg() {
        return result_msg;
    }

    /**
     * @return  详细错误信息
     */
    public String getErrDetail() {
        return err_detail;
    }

    /**
     * 无参构造
     */
    public BCRestfulCommonResult(){}

    /**
     * 构造函数
     * @param resultCode    返回码
     * @param resultMsg     返回信息
     * @param errDetail     具体错误信息
     */
    public BCRestfulCommonResult(Integer resultCode, String resultMsg, String errDetail) {
        this.result_code = resultCode;
        this.result_msg = resultMsg;
        this.err_detail = errDetail;
    }

}
