/**
 * BCRefundResult.java
 * <p/>
 * Created by xuanzhui on 2016/6/21.
 * Copyright (c) 2016 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

public class BCRefundResult extends BCRestfulCommonResult {
    //成功发起预退款或者直接退款后返回退款表记录唯一标识
    protected String id;

    public String getId() {
        return id;
    }

    public BCRefundResult(Integer resultCode, String resultMsg, String errDetail) {
        super(resultCode, resultMsg, errDetail);
    }

    public BCRefundResult(Integer resultCode, String resultMsg, String errDetail, String id) {
        super(resultCode, resultMsg, errDetail);
        this.id = id;
    }
}
