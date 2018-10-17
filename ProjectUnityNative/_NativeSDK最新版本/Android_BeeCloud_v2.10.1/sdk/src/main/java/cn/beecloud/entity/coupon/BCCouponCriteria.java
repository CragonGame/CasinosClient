package cn.beecloud.entity.coupon;

import cn.beecloud.entity.BCBaseCriteria;

/**
 * Created by xuanzhui on 2017/8/17.
 */

public class BCCouponCriteria extends BCBaseCriteria {
    private String user_id;
    private String template_id;
    private Integer status;
    private Integer limit_fee;

    @Override
    public void setCountOnly(Boolean countOnly) {
        throw new UnsupportedOperationException();
    }

    /**
     * @param userId 如果提供则限制领券的用户ID
     */
    public void setUserId(String userId) {
        this.user_id = userId;
    }

    /**
     * @param templateId 如果提供则限制优惠券的模板ID
     */
    public void setTemplateId(String templateId) {
        this.template_id = templateId;
    }

    /**
     * @param status 如果提供则限制优惠券的状态，1表示已经核销的优惠券
     */
    public void setStatus(Integer status) {
        this.status = status;
    }

    /**
     * @param limitFee 一般传入订单金额，返回满足限额的优惠券，比如传入11000，返回满100元减10元的优惠券
     */
    public void setLimitFee(Integer limitFee) {
        this.limit_fee = limitFee;
    }
}
