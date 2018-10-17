/**
 * BCPlanCriteria.java
 * 查询计划的限制类，所有字段皆为选填
 * Created by xuanzhui on 2016/8/5.
 * Copyright (c) 2016 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

public class BCPlanCriteria extends BCBaseCriteria {
    private String name_with_substring;
    private String interval;
    private Integer interval_count;
    private Integer trial_days;

    /**
     * 限制计划名中包含的字符串
     */
    public void setNameWithSubstring(String nameWithSubstring) {
        this.name_with_substring = nameWithSubstring;
    }

    /**
     * 查找的计划周期，可以是day, week, month, year
     */
    public void setInterval(String interval) {
        this.interval = interval;
    }

    /**
     * 扣款周期间隔数
     */
    public void setIntervalCount(Integer intervalCount) {
        this.interval_count = intervalCount;
    }

    /**
     * 试用天数
     */
    public void setTrialDays(Integer trialDays) {
        this.trial_days = trialDays;
    }
}
