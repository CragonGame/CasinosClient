package cn.beecloud.entity.coupon;

/**
 * Created by xuanzhui on 2017/8/17.
 * 卡券
 */

public class BCCoupon {
    private String id;
    private BCCouponTemplate template;
    private String user_id;
    private String app_id;
    private int status;
    private long created_at;
    private long updated_at;
    private long start_time;
    private long end_time;
    private Long use_time;

    /**
     * @return 卡券ID
     */
    public String getId() {
        return id;
    }

    /**
     * @return 卡券模板
     */
    public BCCouponTemplate getTemplate() {
        return template;
    }

    /**
     * @return 卡券分发的用户ID，和下单的buyer_id匹配
     */
    public String getUserId() {
        return user_id;
    }

    /**
     * @return 卡券所属应用
     */
    public String getAppId() {
        return app_id;
    }

    /**
     * @return 0表示未使用，1表示已使用（核销）
     */
    public int getStatus() {
        return status;
    }

    /**
     * @return 分发时间
     */
    public long getCreatedAt() {
        return created_at;
    }

    /**
     * @return 更新时间
     */
    public long getUpdatedAt() {
        return updated_at;
    }

    /**
     * @return 有效期开始时间
     */
    public long getStartTime() {
        return start_time;
    }

    /**
     * @return 有效期结束时间
     */
    public long getEndTime() {
        return end_time;
    }

    /**
     * @return 使用时间，可能为null
     */
    public Long getUseTime() {
        return use_time;
    }
}
