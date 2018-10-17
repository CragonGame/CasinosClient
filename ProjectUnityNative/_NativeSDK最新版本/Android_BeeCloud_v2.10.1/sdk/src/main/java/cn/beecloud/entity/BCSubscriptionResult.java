/**
 * BCSubscriptionResult.java
 * 用于创建subscription的结果和根据id查询
 * Created by xuanzhui on 2016/7/28.
 * Copyright (c) 2016 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

public class BCSubscriptionResult extends BCRestfulCommonResult {
    private BCSubscription subscription;

    public BCSubscriptionResult(Integer resultCode, String resultMsg, String errDetail) {
        super(resultCode, resultMsg, errDetail);
    }

    /**
     * @return 订阅信息
     */
    public BCSubscription getSubscription() {
        return subscription;
    }
}
