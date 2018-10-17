/**
 * BCSubscriptionBanksResult.java
 * 用于查询订阅支持的银行列表
 * Created by xuanzhui on 2016/7/28.
 * Copyright (c) 2016 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

import java.util.List;

public class BCSubscriptionBanksResult extends BCRestfulCommonResult {
    private List<String> banks;
    private List<String> common_banks;

    public BCSubscriptionBanksResult(Integer resultCode, String resultMsg, String errDetail) {
        super(resultCode, resultMsg, errDetail);
    }

    /**
     * @return 所有支持的银行，包含getCommonBanks返回的常用银行
     */
    public List<String> getBanks() {
        return banks;
    }

    /**
     * @return 支持的常用银行
     */
    public List<String> getCommonBanks() {
        return common_banks;
    }
}
