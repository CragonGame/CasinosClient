/**
 * BCQueryBillResult.java
 * <p/>
 * Created by xuanzhui on 2015/10/30.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

public class BCQueryBillResult extends BCRestfulCommonResult {
    BCBillOrder pay;

    /**
     * @return  支付结果
     */
    public BCBillOrder getBill() {
        return pay;
    }

    /**
     * 构造函数
     * @param resultCode    返回码
     * @param resultMsg     返回信息
     * @param errDetail     具体错误信息
     */
    public BCQueryBillResult(Integer resultCode, String resultMsg, String errDetail) {
        super(resultCode, resultMsg, errDetail);
    }

    /**
     * 构造函数
     * @param resultCode    返回码
     * @param resultMsg     返回信息
     * @param errDetail     具体错误信息
     * @param pay           订单数据
     */
    public BCQueryBillResult(Integer resultCode, String resultMsg, String errDetail, BCBillOrder pay) {
        super(resultCode, resultMsg, errDetail);
        this.pay = pay;
    }

    /**
     * 将json串转化为BCQueryBillResult实例
     * @param jsonStr   json串
     * @return          BCQueryBillResult实例
     */
    public static BCQueryBillResult transJsonToResultObject(String jsonStr){
        //反序列化json
        Gson res = new Gson();

        return res.fromJson(jsonStr,new TypeToken<BCQueryBillResult>() {}.getType() );
    }
}
