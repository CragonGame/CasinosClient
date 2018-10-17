/**
 * BCSubscriptionCriteria.java
 * 查询订阅的限制类，所有字段皆为选填
 * Created by xuanzhui on 2016/8/5.
 * Copyright (c) 2016 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

public class BCSubscriptionCriteria extends BCBaseCriteria {
    private String buyer_id;
    private String plan_id;
    private String card_id;

    public void setBuyerId(String buyerId) {
        this.buyer_id = buyerId;
    }

    public void setPlanId(String planId) {
        this.plan_id = planId;
    }

    public void setCardId(String cardId) {
        this.card_id = cardId;
    }
}
