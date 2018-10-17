/**
 * BCRevertStatus.java
 * <p/>
 * Created by xuanzhui on 2015/9/22.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

import com.google.gson.Gson;
import com.google.gson.JsonSyntaxException;
import com.google.gson.reflect.TypeToken;

/**
 * 用于订单撤销状态查询
 */
public class BCRevertStatus extends BCRestfulCommonResult {
    private Boolean revert_status;

    /**
     * 构造函数
     *
     * @param resultCode 返回码
     * @param resultMsg  返回信息
     * @param errDetail  具体错误信息
     */
    public BCRevertStatus(Integer resultCode, String resultMsg, String errDetail) {
        super(resultCode, resultMsg, errDetail);
    }

    /**
     * 构造函数
     *
     * @param resultCode 返回码
     * @param resultMsg  返回信息
     * @param errDetail  具体错误信息
     * @param revertStatus  撤销订单结果
     */
    public BCRevertStatus(Integer resultCode, String resultMsg, String errDetail, Boolean revertStatus) {
        super(resultCode, resultMsg, errDetail);
        this.revert_status = revert_status;
    }

    /**
     * @return  true表示撤销成功
     */
    public Boolean getRevertStatus() {
        return revert_status;
    }

    /**
     * 将json串转化为BCRevertStatus实例
     * @param jsonStr   json串
     * @return          BCRevertStatus实例
     */
    public static BCRevertStatus transJsonToObject(String jsonStr){
        //反序列化json
        Gson res = new Gson();
        BCRevertStatus status;
        try {
            status = res.fromJson(jsonStr,new TypeToken<BCRevertStatus>() {}.getType() );
        } catch (JsonSyntaxException ex) {
            status = new BCRevertStatus(BCRestfulCommonResult.APP_INNER_FAIL_NUM,
                    BCRestfulCommonResult.APP_INNER_FAIL,
                    "JsonSyntaxException or Network Error:" + jsonStr);
        }
        return status;
    }
}
