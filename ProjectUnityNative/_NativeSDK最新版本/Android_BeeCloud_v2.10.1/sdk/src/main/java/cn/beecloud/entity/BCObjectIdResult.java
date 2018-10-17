/**
 * BCObjectIdResult.java
 * 用于更新和删除的结果
 * Created by xuanzhui on 2016/7/28.
 * Copyright (c) 2016 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

public class BCObjectIdResult extends BCRestfulCommonResult {
    // 操作对象的标识符
    private String id;

    public BCObjectIdResult(Integer resultCode, String resultMsg, String errDetail) {
        super(resultCode, resultMsg, errDetail);
    }

    /**
     * @return 操作对象的标识符
     */
    public String getId() {
        return id;
    }
}
