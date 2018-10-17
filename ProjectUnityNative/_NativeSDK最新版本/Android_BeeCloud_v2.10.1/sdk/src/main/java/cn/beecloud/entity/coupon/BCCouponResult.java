package cn.beecloud.entity.coupon;

import cn.beecloud.entity.BCRestfulCommonResult;

/**
 * Created by xuanzhui on 2017/8/17.
 * 卡券创建和根据ID查询的结果
 */

public class BCCouponResult extends BCRestfulCommonResult {
    private BCCoupon coupon;

    /**
     * @return  卡券
     */
    public BCCoupon getCoupon() {
        return coupon;
    }

    public BCCouponResult(Integer resultCode, String resultMsg, String errDetail) {
        super(resultCode, resultMsg, errDetail);
    }
}
