/**
 * BCRefundOrder.java
 * <p/>
 * Created by xuanzhui on 2015/8/3.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

/**
 * 退款订单信息
 * @see BCOrder
 */
public class BCRefundOrder extends BCOrder {
    /**
     * 以下命名需要和restful API匹配
     * 以便于Gson反序列化
     * 请忽略命名规则
     */
    //退款号
    private String refund_no;

    //退款金额, 单位为分
    private Integer refund_fee;

    //退款受理是否完成--true表示渠道退款请求已经受理完成，但是并不代表接受退款
    private Boolean finish;

    //是否接受并且退款成功--如果为true表示渠道接受退款请求并且已经完成退款，意味着refundFinish肯定为true
    private Boolean result;

    /**
     * @return  退款号
     */
    public String getRefundNum() {
        return refund_no;
    }

    /**
     * @return  退款金额, 单位为分
     */
    public Integer getRefundFee() {
        return refund_fee;
    }

    /**
     * 退款受理是否完成
     * @return  true表示渠道退款请求已经受理完成，但是并不代表接受退款
     */
    public Boolean isRefundFinished() {
        return finish;
    }

    /**
     *  是否接受并且退款成功
     * @return  如果为true表示渠道接受退款请求并且已经完成退款，意味着getRefundFinish肯定为true
     */
    public Boolean getRefundResult() {
        return result;
    }

    /**
     * @return  退款创建时间, 毫秒时间戳, 13位
     */
    public Long getRefundCreatedTime() {
        return create_time;
    }

}
