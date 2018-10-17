/**
 * BCPlanListResult.java
 * 用于plan按添加查询
 * Created by xuanzhui on 2016/7/28.
 * Copyright (c) 2016 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

import java.util.List;

public class BCPlanListResult extends BCRestfulCommonResult {
    private Integer total_count;
    private List<BCPlan> plans;

    public BCPlanListResult(Integer resultCode, String resultMsg, String errDetail) {
        super(resultCode, resultMsg, errDetail);
    }

    /**
     * @return 订阅计划列表
     */
    public List<BCPlan> getPlans() {
        return plans;
    }

    /**
     * @return 满足条件的列表个数
     */
    public Integer getTotalCount() {
        if (total_count != null)
            return total_count;
        else if (plans != null)
            return plans.size();
        else
            return 0;
    }
}
