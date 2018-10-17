/**
 * BCQueryRefundOrderResult.java
 * <p/>
 * Created by xuanzhui on 2015/8/19.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import java.util.List;

public class BCQueryRefundsResult extends BCRestfulCommonResult {
    //退款订单列表
    private List<BCRefundOrder> refunds;

    /**
     * @return 实际返回订单结果数量
     */
    public Integer getCount() {
        return refunds.size();
    }

    /**
     * @return 退款订单列表
     */
    public List<BCRefundOrder> getRefunds() {
        return refunds;
    }

    /**
     * 无参构造
     */
    public BCQueryRefundsResult() {
    }

    /**
     * 构造函数
     *
     * @param resultCode 返回码
     * @param resultMsg  返回信息
     * @param errDetail  具体错误信息
     */
    public BCQueryRefundsResult(Integer resultCode, String resultMsg, String errDetail) {
        super(resultCode, resultMsg, errDetail);
    }

    /**
     * 将json串转化为BCQueryRefundOrderResult实例
     * @param jsonStr   json串
     * @return          BCQueryRefundOrderResult实例
     */
    public static BCQueryRefundsResult transJsonToResultObject(String jsonStr){
        //反序列化json
        Gson res = new Gson();

        return res.fromJson(jsonStr,new TypeToken<BCQueryRefundsResult>() {}.getType() );
    }
}
