/**
 * BCSmsResult.java
 * <p/>
 * Created by xuanzhui on 2016/7/28.
 * Copyright (c) 2016 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

public class BCSmsResult extends BCRestfulCommonResult {
    private String sms_id;

    public BCSmsResult(Integer resultCode, String resultMsg, String errDetail) {
        super(resultCode, resultMsg, errDetail);
    }

    /**
     * @return 验证码的标识符
     */
    public String getSmsId() {
        return sms_id;
    }
}
