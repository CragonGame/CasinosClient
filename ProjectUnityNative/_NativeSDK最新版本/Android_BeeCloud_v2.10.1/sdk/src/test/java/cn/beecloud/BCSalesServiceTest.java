package cn.beecloud;

import junit.framework.Assert;

import org.junit.BeforeClass;
import org.junit.Test;

import java.util.List;

import cn.beecloud.entity.coupon.BCCoupon;
import cn.beecloud.entity.coupon.BCCouponCriteria;
import cn.beecloud.entity.coupon.BCCouponResult;
import cn.beecloud.entity.coupon.BCCouponTemplate;
import cn.beecloud.entity.coupon.BCCouponTemplateCriteria;
import cn.beecloud.entity.coupon.BCCouponTemplateResult;
import cn.beecloud.entity.coupon.BCCouponTemplatesResult;
import cn.beecloud.entity.coupon.BCCouponsResult;

/**
 * Created by xuanzhui on 2017/8/17.
 */

public class BCSalesServiceTest {
    private BCSalesService couponService = new BCSalesService();

    @BeforeClass
    public static void setUpBeforeClass() {
        BeeCloud.setAppIdAndSecret("c37d661d-7e61-49ea-96a5-68c34e83db3b",
                "c37d661d-7e61-49ea-96a5-68c34e83db3b");
    }

    @Test
    public void testCreateCoupon() {
//        BCCouponResult couponResult = couponService.createCoupon("15fb6192-d642-4dc7-9188-eb9367704a41",
//                "xz-test-buyer1");
//        System.out.println(couponResult.getErrDetail());
//        Assert.assertTrue(couponResult.getResultCode() == 0);
//        BCCoupon coupon = couponResult.getCoupon();
//        System.out.println(coupon.getId());     // bbbf835d-f6b0-484f-bb6e-8e6082d4a35f
//        System.out.println(coupon.getAppId());
//        System.out.println(coupon.getTemplate().getName());
    }

    @Test
    public void testQueryCoupon() {
        BCCouponResult couponResult = couponService.queryCoupon("79578fba-7330-4f4f-a063-3e97de28783a");
        Assert.assertTrue(couponResult.getResultCode() == 0);
        BCCoupon coupon = couponResult.getCoupon();
        System.out.println(coupon.getAppId());
        System.out.println(coupon.getTemplate().getName());
    }

    @Test
    public void testQueryCoupons() {
        BCCouponCriteria couponCriteria = new BCCouponCriteria();
        couponCriteria.setStatus(1);
        BCCouponsResult couponsResult = couponService.queryCoupons(couponCriteria);
        Assert.assertTrue(couponsResult.getResultCode() == 0);
        List<BCCoupon> coupons = couponsResult.getCoupons();
        System.out.println(coupons.size());

        if (coupons.size() > 0) {
            BCCoupon coupon = coupons.get(0);
            System.out.println(coupon.getAppId());
            System.out.println(coupon.getTemplate().getName());
        }
    }

    @Test
    public void testQueryCouponTemplate() {
        BCCouponTemplateResult templateResult = couponService.queryCouponTemplate("b7bf7c8e-321b-4451-8b3a-6fd630836641");
        Assert.assertTrue(templateResult.getResultCode() == 0);
        BCCouponTemplate template = templateResult.getCouponTemplate();
        System.out.println(template.getAppId());
        System.out.println(template.getName());
    }

    @Test
    public void testQueryCouponTemplates() {
        BCCouponTemplateCriteria templateCriteria = new BCCouponTemplateCriteria();
        templateCriteria.setName("111111111111");
        BCCouponTemplatesResult templatesResult = couponService.queryCouponTemplates(templateCriteria);
        Assert.assertTrue(templatesResult.getResultCode() == 0);
        List<BCCouponTemplate> couponTemplates = templatesResult.getCouponTemplates();
        System.out.println(couponTemplates.size());

        if (couponTemplates.size() > 0) {
            BCCouponTemplate couponTemplate = couponTemplates.get(0);
            Assert.assertTrue(couponTemplate.getName().equals("111111111111"));
        }
    }
}
