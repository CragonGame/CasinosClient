/**
 * BCCardVerifyResult.java
 * <p>
 * Created by xuanzhui on 2016/9/6.
 * Copyright (c) 2016 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

public class BCCardVerifyResult extends BCRestfulCommonResult {
    private String card_id;
    private Boolean auth_result;
    private String auth_msg;

    public String getCardId() {
        return card_id;
    }

    public boolean getAuthResult() {
        return auth_result != null && auth_result;
    }

    public String getAuthMsg() {
        return auth_msg;
    }

    public BCCardVerifyResult(Integer resultCode, String resultMsg, String errDetail) {
        super(resultCode, resultMsg, errDetail);
    }

    public BCCardVerifyResult(Integer resultCode, String resultMsg, String errDetail,
                              String cardId, Boolean authResult, String authMsg) {
        super(resultCode, resultMsg, errDetail);
        this.card_id = cardId;
        this.auth_result = authResult;
        this.auth_msg = authMsg;
    }
}
