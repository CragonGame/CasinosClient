/**
 * BCOrder.java
 * <p/>
 * Created by xuanzhui on 2015/8/19.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

class BCOrder {
    /**
     * 以下命名需要和restful API匹配
     * 以便于Gson反序列化
     * 请忽略命名规则
     */

    //订单记录的唯一标识
    private String id;

    //订单号
    private String bill_no;

    // 实付金额（使用优惠券的时候是扣除优惠券优惠金额后客户实际支付的金额），单位为分
    private Integer total_fee;

    //渠道类型
    private String channel;

    //子渠道类型
    private String sub_channel;

    //订单标题
    private String title;

    //订单创建时间, 毫秒时间戳, 13位
    protected Long create_time;

    //扩展参数
    private String optional;

    //渠道返回的详细信息
    private String message_detail;

    /**
     * @return  订单记录的唯一标识，可用于查询单笔记录
     */
    public String getId() {
        return id;
    }

    /**
     * @return  订单号
     */
    public String getBillNum() {
        return bill_no;
    }

    /**
     * @return  实付金额（使用优惠券的时候是扣除优惠券优惠金额后客户实际支付的金额），单位为分
     */
    public Integer getTotalFee() {
        return total_fee;
    }

    /**
     * @return  渠道类型
     */
    public String getChannel() {
        return channel;
    }

    /**
     * @return  子渠道类型
     */
    public String getSubChannel() {
        return sub_channel;
    }

    /**
     * @return  订单标题
     */
    public String getTitle() {
        return title;
    }

    /**
     * @return  扩展参数
     */
    public String getOptional() {
        return optional;
    }

    /**
     * @return  渠道返回的详细信息
     */
    public String getMessageDetail() {
        return message_detail;
    }
}
