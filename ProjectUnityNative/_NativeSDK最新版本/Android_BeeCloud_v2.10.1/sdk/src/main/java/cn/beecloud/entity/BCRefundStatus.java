/**
 * BCQueryRefundStatusResult.java
 *
 * Created by xuanzhui on 2015/8/3.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

/**
 * 用于查询退款状态
 * @see BCRestfulCommonResult
 */
public class BCRefundStatus extends BCRestfulCommonResult {

    /**
     * 以下命名需要和restful API匹配
     * 以便于Gson反序列化
     * 请忽略命名规则
     */
    private String refund_status;

    /**
     * 退款状态
     */
    public String getRefundStatus() {
        return refund_status;
    }

    public BCRefundStatus(){}

    /**
     * @param resultCode    返回码
     * @param resultMsg     返回信息
     * @param errDetail     具体错误信息
     */
    public BCRefundStatus(Integer resultCode, String resultMsg, String errDetail) {
        super(resultCode, resultMsg, errDetail);
    }

    /**
     * @param resultCode    返回码
     * @param resultMsg     返回信息
     * @param errDetail     具体错误信息
     * @param refundStatus  退款状态
     */
    public BCRefundStatus(Integer resultCode, String resultMsg, String errDetail, String refundStatus) {
        super(resultCode, resultMsg, errDetail);
        this.refund_status = refundStatus;
    }

    /**
     * 将json串转化为BCQueryRefundStatusResult实例
     * @param jsonStr   json串
     * @return          BCQueryRefundStatusResult实例
     */
    public static BCRefundStatus transJsonToObject(String jsonStr) {
        Gson gson = new Gson();

        return gson.fromJson(jsonStr, new TypeToken<BCRefundStatus>() {}.getType());
    }

    /**
     * 退款状态
     */
    public static class RefundStatus{
        /**
         * 退款成功
         */
        public static final String REFUND_STATUS_SUCCESS = "SUCCESS";

        /**
         * 退款失败
         */
        public static final String REFUND_STATUS_FAIL = "FAIL";

        /**
         * 退款处理中
         */
        public static final String REFUND_STATUS_PROCESSING = "PROCESSING";

        /**
         * 未确定
         */
        public static final String REFUND_STATUS_NOTSURE = "NOTSURE";

        /**
         * 转入代发
         */
        public static final String REFUND_STATUS_CHANGE = "CHANGE";

        /**
         * @param refundStatus  类中的static变量
         * @return              转化过的详细退款状态
         */
        public static String getTranslatedRefundStatus(String refundStatus){
            if (refundStatus.equals(REFUND_STATUS_SUCCESS))
                return "退款成功";
            else if (refundStatus.equals(REFUND_STATUS_FAIL))
                return "退款失败";
            else if (refundStatus.equals(REFUND_STATUS_PROCESSING))
                return "退款处理中";
            else if (refundStatus.equals(REFUND_STATUS_NOTSURE))
                return "未确定，需要商户原退款单号重新发起";
            else if (refundStatus.equals(REFUND_STATUS_CHANGE))
                return "转入代发，退款到银行发现用户的卡作废或者冻结了，导致原路退款银行卡失败，资金回流到商户的现金帐号，需要商户人工干预，通过线下或者财付通转账的方式进行退款";
            else
                return "";
        }
    }
}
