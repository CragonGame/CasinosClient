package cn.beecloud;

import java.util.HashMap;
import java.util.Map;

import cn.beecloud.entity.coupon.BCCouponCriteria;
import cn.beecloud.entity.coupon.BCCouponResult;
import cn.beecloud.entity.coupon.BCCouponTemplateCriteria;
import cn.beecloud.entity.coupon.BCCouponTemplateResult;
import cn.beecloud.entity.coupon.BCCouponTemplatesResult;
import cn.beecloud.entity.coupon.BCCouponsResult;

/**
 * Created by xuanzhui on 2017/8/17.
 * 用于营销卡券
 */

public class BCSalesService {

    public BCCouponResult createCoupon(String templateId, String userId) {
        Map<String, Object> reqParams = new HashMap<>();
        reqParams.put("template_id", templateId);
        reqParams.put("user_id", userId);

        String url = BCHttpClientUtil.BEECLOUD_HOST + "/2/rest/coupon";

        return (BCCouponResult) BCHttpClientUtil.addRestObject(url, reqParams, BCCouponResult.class, true);
    }

    public BCCouponResult queryCoupon(String id) {
        String url = BCHttpClientUtil.BEECLOUD_HOST + "/2/rest/coupon";

        return (BCCouponResult) BCHttpClientUtil.queryRestObjectById(url, id, BCCouponResult.class,
                true, true);
    }

    public BCCouponsResult queryCoupons(BCCouponCriteria couponCriteria) {
        String url = BCHttpClientUtil.BEECLOUD_HOST + "/2/rest/coupon";

        return (BCCouponsResult) BCHttpClientUtil.queryRestObjects(url,
                BCHttpClientUtil.objectToMap(couponCriteria), BCCouponsResult.class,
                true, true);
    }

    public BCCouponTemplateResult queryCouponTemplate(String id) {
        String url = BCHttpClientUtil.BEECLOUD_HOST + "/2/rest/coupon/template";

        return (BCCouponTemplateResult) BCHttpClientUtil.queryRestObjectById(url, id,
                BCCouponTemplateResult.class, true, true);
    }

    public BCCouponTemplatesResult queryCouponTemplates(BCCouponTemplateCriteria templateCriteria) {
        String url = BCHttpClientUtil.BEECLOUD_HOST + "/2/rest/coupon/template";

        return (BCCouponTemplatesResult) BCHttpClientUtil.queryRestObjects(url,
                BCHttpClientUtil.objectToMap(templateCriteria), BCCouponTemplatesResult.class,
                true, true);
    }
}
