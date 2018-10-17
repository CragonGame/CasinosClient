/**
 * BCQueryRefundResult.java
 * <p/>
 * Created by xuanzhui on 2015/10/30.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

public class BCQueryRefundResult extends BCRestfulCommonResult{
    BCRefundOrder refund;

    /**
     * @return  退款结果
     */
    public BCRefundOrder getRefund() {
        return refund;
    }

    /**
     * 构造函数
     * @param resultCode    返回码
     * @param resultMsg     返回信息
     * @param errDetail     具体错误信息
     */
    public BCQueryRefundResult(Integer resultCode, String resultMsg, String errDetail) {
        super(resultCode, resultMsg, errDetail);
    }

    /**
     * 构造函数
     * @param resultCode    返回码
     * @param resultMsg     返回信息
     * @param errDetail     具体错误信息
     * @param refund        退款数据
     */
    public BCQueryRefundResult(Integer resultCode, String resultMsg, String errDetail, BCRefundOrder refund) {
        super(resultCode, resultMsg, errDetail);
        this.refund = refund;
    }

    /**
     * 将json串转化为BCQueryRefundResult实例
     * @param jsonStr   json串
     * @return          BCQueryBillResult实例
     */
    public static BCQueryRefundResult transJsonToResultObject(String jsonStr){
        //反序列化json
        Gson res = new Gson();

        return res.fromJson(jsonStr,new TypeToken<BCQueryRefundResult>() {}.getType() );
    }
}
