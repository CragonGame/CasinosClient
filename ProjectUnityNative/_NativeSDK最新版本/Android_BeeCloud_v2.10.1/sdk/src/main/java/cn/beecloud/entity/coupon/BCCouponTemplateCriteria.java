package cn.beecloud.entity.coupon;

import cn.beecloud.entity.BCBaseCriteria;

/**
 * Created by xuanzhui on 2017/8/17.
 * 卡券模板查询条件
 */

public class BCCouponTemplateCriteria extends BCBaseCriteria {
    private String name;

    @Override
    public void setCountOnly(Boolean countOnly) {
        throw new UnsupportedOperationException();
    }

    /**
     * @param name 如果提供则限制模板名
     */
    public void setName(String name) {
        this.name = name;
    }
}
