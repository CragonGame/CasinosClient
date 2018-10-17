/**
 * BCSubscriptionListResult.java
 * 用于subscription按条件查询
 * Created by xuanzhui on 2016/7/28.
 * Copyright (c) 2016 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

import java.util.List;

public class BCSubscriptionListResult extends BCRestfulCommonResult {
    private Integer total_count;
    private List<BCSubscription> subscriptions;

    public BCSubscriptionListResult(Integer resultCode, String resultMsg, String errDetail) {
        super(resultCode, resultMsg, errDetail);
    }

    /**
     * @return 订阅列表
     */
    public List<BCSubscription> getSubscriptions() {
        return subscriptions;
    }

    /**
     * @return 满足条件的列表个数
     */
    public Integer getTotalCount() {
        if (total_count != null)
            return total_count;
        else if (subscriptions != null)
            return subscriptions.size();
        else
            return 0;
    }
}
