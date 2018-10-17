package cn.beecloud.entity;

import java.util.List;

/**
 * Created by xuanzhui on 2017/6/28.
 * 查询商家消费者信息
 */

public class BCQueryMerchantBuyerResult extends BCRestfulCommonResult {
    private List<BCMerchantBuyer> users;

    public List<BCMerchantBuyer> getUsers() {
        return users;
    }
}
