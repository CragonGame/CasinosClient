/**
 * BCSubscription.java
 * <p/>
 * Created by xuanzhui on 2016/7/28.
 * Copyright (c) 2016 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

import java.util.Map;

public class BCSubscription {
    /**
     * 以下命名需要和restful API匹配
     * 以便于Gson反序列化
     * 请忽略命名规则
     */
    // 订阅记录的唯一标识
    private String id;

    // 订阅的buyer ID，可以是用户email，也可以是商户系统中的用户ID
    private String buyer_id;

    // 订阅计划id
    private String plan_id;

    // 由{订阅用户银行名称、订阅用户银行卡号、订阅用户身份证姓名、订阅用户身份证号、订阅用户银行预留手机号}的组合确定
    // 实际发起订阅请求时可以直接使用该id，或者以上的组合
    private String card_id;

    // 订阅用户银行名称
    private String bank_name;

    // 订阅用户银行卡号
    private String card_no;

    // 订阅用户身份证姓名
    private String id_name;

    // 订阅用户身份证号
    private String id_no;

    // 订阅用户银行预留手机号
    private String mobile;

    // 每次扣款总额为 订阅计划的金额 * amount，订阅系统默认为1
    private Double amount;

    // 优惠标识id
    private String coupon_id;

    // 试用截止时间戳
    private Long trial_end;

    // map类型附加信息
    private Map<String, Object> optional;

    // 订阅用户银行卡号后四位，查询时返回
    private String last4;

    // 订阅状态，查询时返回
    private String status;

    // 订阅是否已经生效，查询时返回
    private Boolean valid;

    /**
     * @return 订阅记录的唯一标识
     */
    public String getId() {
        return id;
    }

    /**
     * @return 订阅的buyer ID，可以是用户email，也可以是商户系统中的用户ID
     */
    public String getBuyerId() {
        return buyer_id;
    }

    /**
     * @param buyerId 订阅的buyer ID，可以是用户email，也可以是商户系统中的用户ID
     */
    public void setBuyerId(String buyerId) {
        this.buyer_id = buyerId;
    }

    /**
     * @return 订阅计划id
     */
    public String getPlanId() {
        return plan_id;
    }

    /**
     * @param planId 订阅计划id
     */
    public void setPlanId(String planId) {
        this.plan_id = planId;
    }

    /**
     * @return 由{订阅用户银行名称、订阅用户银行卡号、订阅用户身份证姓名、订阅用户身份证号、订阅用户银行预留手机号}的组合确定
                实际发起订阅请求时可以直接使用该id，或者以上的组合
     */
    public String getCardId() {
        return card_id;
    }

    /**
     * @param cardId 由{订阅用户银行名称、订阅用户银行卡号、订阅用户身份证姓名、订阅用户身份证号、订阅用户银行预留手机号}的组合确定
                    实际发起订阅请求时可以直接使用该id，或者以上的组合
     */
    public void setCardId(String cardId) {
        this.card_id = cardId;
    }

    /**
     * @return 订阅用户银行名称
     */
    public String getBankName() {
        return bank_name;
    }

    /**
     * @param bankName 订阅用户银行名称
     */
    public void setBankName(String bankName) {
        this.bank_name = bankName;
    }

    /**
     * @return 订阅用户银行卡号
     */
    public String getCardNum() {
        return card_no;
    }

    /**
     * @param cardNum 订阅用户银行卡号
     */
    public void setCardNum(String cardNum) {
        this.card_no = cardNum;
    }

    /**
     * @return 订阅用户身份证姓名
     */
    public String getIdName() {
        return id_name;
    }

    /**
     * @param idName 订阅用户身份证姓名
     */
    public void setIdName(String idName) {
        this.id_name = idName;
    }

    /**
     * @return 订阅用户身份证号
     */
    public String getIdNum() {
        return id_no;
    }

    /**
     * @param idNum 订阅用户身份证号
     */
    public void setIdNum(String idNum) {
        this.id_no = idNum;
    }

    /**
     * @return 订阅用户银行预留手机号
     */
    public String getMobile() {
        return mobile;
    }

    /**
     * @param mobile 订阅用户银行预留手机号
     */
    public void setMobile(String mobile) {
        this.mobile = mobile;
    }

    /**
     * @return 每次扣款总额为 订阅计划的金额 * amount，订阅系统默认为1
     */
    public Double getAmount() {
        return amount;
    }

    /**
     * @param amount 设置后，每次扣款总额为 订阅计划的金额 * amount，订阅系统默认为1
     */
    public void setAmount(Double amount) {
        this.amount = amount;
    }

    /**
     * @return 优惠标识id
     */
    public String getCouponId() {
        return coupon_id;
    }

    /**
     * @return 试用截止时间戳
     */
    public Long getTrialEnd() {
        return trial_end;
    }

    /**
     * @param trialEnd 试用截止时间戳
     */
    public void setTrialEnd(Long trialEnd) {
        this.trial_end = trialEnd;
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
     * @return 订阅用户银行卡号后四位
     */
    public String getLast4() {
        return last4;
    }

    /**
     * @return 订阅状态，例如waiting
     */
    public String getStatus() {
        return status;
    }

    /**
     * @return 订阅是否已经生效，为true代表计划开始后就定期扣费
     */
    public boolean isValid() {
        return valid != null && valid;
    }

}
