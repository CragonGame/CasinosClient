package cn.beecloud.entity.coupon;

/**
 * Created by xuanzhui on 2017/8/17.
 * 卡券模板
 */

public class BCCouponTemplate {
    private String id;
    private String name;
    private int type;
    private int limit_fee;
    private float discount;
    private int total_count;
    private int max_count_per_user;
    private int deliver_count;
    private int use_count;
    private int expiry_type;
    private Long start_time;
    private Long end_time;
    private int delivery_valid_days;
    private int status;
    private String mch_account;
    private String app_id;
    private long created_at;
    private long updated_at;

    /**
     * @return 卡券模板ID
     */
    public String getId() {
        return id;
    }

    /**
     * @return 卡券模板名称
     */
    public String getName() {
        return name;
    }

    /**
     * @return 卡券类型，0表示满减，1表示折扣
     */
    public int getType() {
        return type;
    }

    /**
     * @return 限制满足额度的条件，以分为单位，0表示不限额，比如满100减10元，返回为10000
     */
    public int getLimitFee() {
        return limit_fee;
    }

    /**
     * @return 如果type为0，存储优惠金额，以元为单位，比如10表示减10元；如果type为1，存储折扣比例，例如0.9表示9折
     */
    public float getDiscount() {
        return discount;
    }

    /**
     * @return 卡券总数，0表示不限制
     */
    public int getTotalCount() {
        return total_count;
    }

    /**
     * @return 每个人最多可以领取的卡券数量，0表示不限制
     */
    public int getMaxCountPerUser() {
        return max_count_per_user;
    }

    /**
     * @return 分发的卡券数量
     */
    public int getDeliverCount() {
        return deliver_count;
    }

    /**
     * @return 使用卡券的数量
     */
    public int getUseCount() {
        return use_count;
    }

    /**
     * @return 卡券有效期类型，1表示根据模板起止时间判断，2表示卡券发放日期后多少天内有效
     */
    public int getExpiryType() {
        return expiry_type;
    }

    /**
     * @return 卡券开始时间，可能为null
     */
    public Long getStartTime() {
        return start_time;
    }

    /**
     * @return 卡券结束时间，可能为null
     */
    public Long getEndTime() {
        return end_time;
    }

    /**
     * @return 卡券发放日期后多少天内有效，在expiry_type为2时有效
     */
    public int getDeliveryValidDays() {
        return delivery_valid_days;
    }

    /**
     * @return 0表示未开启，1表示正常使用，-1表示停止使用
     */
    public int getStatus() {
        return status;
    }

    /**
     * @return 卡券所属商家账户
     */
    public String getMchAccount() {
        return mch_account;
    }

    /**
     * @return 卡券所属应用
     */
    public String getAppId() {
        return app_id;
    }

    /**
     * @return 创建时间
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
}
