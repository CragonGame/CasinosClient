/**
 * BCBillStatus.java
 * <p/>
 * Created by xuanzhui on 2015/9/22.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

/**
 * 用于订单状态查询
 */
public class BCBillStatus extends BCRestfulCommonResult {
    private Boolean pay_result;

    /**
     * 构造函数
     *
     * @param resultCode 返回码
     * @param resultMsg  返回信息
     * @param errDetail  具体错误信息
     */
    public BCBillStatus(Integer resultCode, String resultMsg, String errDetail) {
        super(resultCode, resultMsg, errDetail);
    }

    /**
     * 构造函数
     *
     * @param resultCode 返回码
     * @param resultMsg  返回信息
     * @param errDetail  具体错误信息
     * @param payResult  支付结果
     */
    public BCBillStatus(Integer resultCode, String resultMsg, String errDetail, Boolean payResult) {
        super(resultCode, resultMsg, errDetail);
        this.pay_result = pay_result;
    }

    /**
     * @return  true表示支付成功
     */
    public Boolean getPayResult() {
        return pay_result;
    }

    /**
     * 将json串转化为BCBillStatus实例
     * @param jsonStr   json串
     * @return          BCBillStatus实例
     */
    public static BCBillStatus transJsonToObject(String jsonStr){
        //反序列化json
        Gson res = new Gson();

        return res.fromJson(jsonStr,new TypeToken<BCBillStatus>() {}.getType() );
    }
}
