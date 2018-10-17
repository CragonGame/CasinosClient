package cn.beecloud;

import junit.framework.Assert;

import org.junit.BeforeClass;
import org.junit.Test;

import java.util.LinkedList;
import java.util.List;
import java.util.UUID;

import cn.beecloud.entity.BCQueryMerchantBuyerResult;
import cn.beecloud.entity.BCRestfulCommonResult;

/**
 * Created by xuanzhui on 2017/6/27.
 */

public class BCMerchantBuyerServiceTest {
    private BCMerchantBuyerService userService = new BCMerchantBuyerService();

    @BeforeClass
    public static void setUpBeforeClass() {
        BeeCloud.setAppIdAndSecret("c37d661d-7e61-49ea-96a5-68c34e83db3b",
                "c37d661d-7e61-49ea-96a5-68c34e83db3b");
    }

    @Test
    public void testAddMerchantUser() {
        String userId = UUID.randomUUID().toString().replace("-", "");
        System.out.println(userId);
        BCRestfulCommonResult commonResult = userService.addMerchantBuyer(userId);
        System.out.println(commonResult.getResultMsg());
        Assert.assertTrue(commonResult.getResultCode() == 0);
    }

    @Test
    public void testBatchAddMerchantUsers() {
        List<String> userIdList = new LinkedList<>();
        for (int i = 0; i < 2; i++) {
            userIdList.add(UUID.randomUUID().toString().replace("-", ""));
        }

        BCRestfulCommonResult commonResult = userService.batchAddMerchantBuyers("test@beecloud.cn",
                userIdList);
        System.out.println(commonResult.getResultMsg());
        Assert.assertTrue(commonResult.getResultCode() == 0);
    }

    @Test
    public void testGetMerchantUsers() {
        BCQueryMerchantBuyerResult result = userService.getMerchantBuyers("test@beecloud.cn", null, null);
        Assert.assertTrue(result.getResultCode() == 0);
        if (result.getUsers() != null && result.getUsers().size() != 0) {
            System.out.println(result.getUsers().get(0).getBuyerId());
        }
    }
}
