/**
 * BCQueryCountResult.java
 * <p/>
 * Created by xuanzhui on 2015/11/27.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

public class BCQueryCountResult extends BCRestfulCommonResult {
    private Integer count;

    public BCQueryCountResult(Integer resultCode, String resultMsg, String errDetail) {
        super(resultCode, resultMsg, errDetail);
    }

    /**
     * @return  查询到的订单数目
     */
    public Integer getCount() {
        return count;
    }

    /**
     * 将json串转化为BCQueryCountResult实例
     * @param jsonStr   json串
     * @return          BCQueryCountResult实例
     */
    public static BCQueryCountResult transJsonToObject(String jsonStr) {
        //反序列化json
        Gson res = new Gson();

        return res.fromJson(jsonStr,new TypeToken<BCQueryCountResult>(){}.getType() );
    }
}
