/**
 * BCPlan.java
 * 订阅计划
 * Created by xuanzhui on 2016/7/28.
 * Copyright (c) 2016 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

import java.util.Map;

public class BCPlan {
    /**
     * 订阅计划的唯一标识
     */
    private String id;

    /**
     * 扣款单价
     */
    private Integer fee;

    /**
     * 扣款周期，只可以是：day, week, month or year
     */
    private String interval;

    /**
     * 计划名
     */
    private String name;

    /**
     * ISO货币名，如'CNY'代表人民币
     */
    private String currency;

    /**
     * 扣款周期间隔数，例如interval为month，这边设置为3，
     *                      那么每3个月扣款，订阅系统默认1
     */
    private Integer interval_count;

    /**
     * 试用天数
     */
    private Integer trial_days;

    /**
     * map类型附加信息
     */
    private Map<String, Object> optional;

    /**
     * 计划是否生效
     */
    private Boolean valid;

    /**
     * @return 订阅计划的唯一标识
     */
    public String getId() {
        return id;
    }

    /**
     * @return 扣款单价，分为单位
     */
    public Integer getFee() {
        return fee;
    }

    /**
     * @param fee 扣款单价，分为单位
     */
    public void setFee(Integer fee) {
        this.fee = fee;
    }

    /**
     * @return 扣款周期:day, week, month or year.
     */
    public String getInterval() {
        return interval;
    }

    /**
     * @param interval 扣款周期: 只可以是字符串 day, week, month or year.
     */
    public void setInterval(String interval) {
        this.interval = interval;
    }

    /**
     * @return 计划名
     */
    public String getName() {
        return name;
    }

    /**
     * @param name 计划名
     */
    public void setName(String name) {
        this.name = name;
    }

    /**
     * @return ISO货币名，如'CNY'
     */
    public String getCurrency() {
        return currency;
    }

    /**
     * @param currency ISO货币名，如'CNY'
     */
    public void setCurrency(String currency) {
        this.currency = currency;
    }

    /**
     * @return 扣款周期间隔数
     */
    public Integer getIntervalCount() {
        return interval_count;
    }

    /**
     * @param intervalCount 扣款周期间隔数，例如interval为month，这边设置为3，
     *                      那么每3个月扣款，订阅系统默认1
     */
    public void setIntervalCount(Integer intervalCount) {
        this.interval_count = intervalCount;
    }

    /**
     * @return 试用天数
     */
    public Integer getTrialDays() {
        return trial_days;
    }

    /**
     * @param trialDays 试用天数
     */
    public void setTrialDays(Integer trialDays) {
        this.trial_days = trialDays;
    }

    /**
     * @return map类型附加信息
     */
    public Map<String, Object> getOptional() {
        return optional;
    }

    /**
     * @param optional map类型附加信息
     */
    public void setOptional(Map<String, Object> optional) {
        this.optional = optional;
    }

    /**
     * @return 计划是否生效
     */
    public boolean isValid() {
        return valid != null && valid;
    }
}
