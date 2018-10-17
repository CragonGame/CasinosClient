package cn.beecloud.entity;

/**
 * Created by xuanzhui on 2017/6/28.
 * 商家消费者
 */

public class BCMerchantBuyer {
    private String merchant;
    private String app_id;
    private String buyer_id;
    private String buyer_type;
    private long createdat;
    private long updatedat;

    private String name;
    private String id_no;
    private String card_no;
    private String mobile;
    private String bank_name;
    private String card_type;

    /**
     * @return 商户的标识，email或者手机号
     */
    public String getMerchant() {
        return merchant;
    }

    /**
     * @return 商户的app标识
     */
    public String getAppId() {
        return app_id;
    }

    /**
     * @return 用户的ID
     */
    public String getBuyerId() {
        return buyer_id;
    }

    /**
     * @return 用户类型（等级）。0 - 一般用户；1 - 认证用户，需通过二要素验证；2 - 可提现用户，需通过四要素认证
     */
    public String getBuyerType() {
        return buyer_type;
    }

    /**
     * @return 该用户创建时间
     */
    public long getCreatedAt() {
        return createdat;
    }

    /**
     * @return 用户信息修改时间
     */
    public long getUpdatedAt() {
        return updatedat;
    }

    /**
     * @return 用户姓名，可能会null
     */
    public String getName() {
        return name;
    }

    /**
     * @return 用户身份证号，可能会null
     */
    public String getIdNo() {
        return id_no;
    }

    /**
     * @return 提现卡号，可能会null
     */
    public String getCardNo() {
        return card_no;
    }

    /**
     * @return 银行卡绑定的手机号，可能会null
     */
    public String getMobile() {
        return mobile;
    }

    /**
     * @return 银行全称，可能会null
     */
    public String getBankName() {
        return bank_name;
    }

    /**
     * @return 提现卡类型。0 - 借记卡，1 - 信用卡，可能会null
     */
    public String getCardType() {
        return card_type;
    }
}
