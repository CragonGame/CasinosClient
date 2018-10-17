package cn.beecloud.entity.coupon;

import cn.beecloud.entity.BCRestfulCommonResult;

/**
 * Created by xuanzhui on 2017/8/17.
 * 根据ID查询结果
 */

public class BCCouponTemplateResult extends BCRestfulCommonResult {
    private BCCouponTemplate coupon_template;

    public BCCouponTemplateResult(Integer resultCode, String resultMsg, String errDetail) {
        super(resultCode, resultMsg, errDetail);
    }

    /**
     * @return 卡券模板
     */
    public BCCouponTemplate getCouponTemplate() {
        return coupon_template;
    }
}
