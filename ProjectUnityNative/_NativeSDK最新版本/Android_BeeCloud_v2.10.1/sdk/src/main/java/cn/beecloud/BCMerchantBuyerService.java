package cn.beecloud;

import android.util.Log;

import com.google.gson.Gson;

import java.io.UnsupportedEncodingException;
import java.net.URLEncoder;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import cn.beecloud.entity.BCQueryMerchantBuyerResult;
import cn.beecloud.entity.BCRestfulCommonResult;

/**
 * Created by xuanzhui on 2017/6/27.
 * 用于商家消费者系统
 */

public class BCMerchantBuyerService {
    /**
     * 单个消费者注册接口
     * @param buyerId 商户系统内自定义的消费者ID，和下单参数buyerId相对应
      */
    public BCRestfulCommonResult addMerchantBuyer(String buyerId) {
        Map<String, Object> reqMap = new HashMap<>();
        reqMap.put("buyer_id", buyerId);
        String url = BCHttpClientUtil.BEECLOUD_HOST + "/2/rest/user";
        return BCHttpClientUtil.addRestObject(url, reqMap, BCRestfulCommonResult.class, true);
    }

    /**
     * 消费者批量导入接口
     * @param merchant 商户的注册账户
     * @param buyerIdList 商户系统内自定义的批量ID列表
     */
    public BCRestfulCommonResult batchAddMerchantBuyers(String merchant, List<String> buyerIdList) {
        Map<String, Object> reqMap = new HashMap<>();
        reqMap.put("email", merchant);
        reqMap.put("buyer_ids", buyerIdList);
        String url = BCHttpClientUtil.BEECLOUD_HOST + "/2/rest/users";
        return BCHttpClientUtil.addRestObject(url, reqMap, BCRestfulCommonResult.class, true);
    }

    /**
     * 商家消费者批量查询接口
     * @param merchant  商户的注册账户，可以为null，此时直接查询和appID关联的商家用户
     * @param startTime 起始时间。该接口会返回此时间戳之后创建的用户。毫秒时间戳 13位
     * @param endTime   结束时间。该接口会返回此时间戳之前创建的用户。毫秒时间戳 13位
     */
    public BCQueryMerchantBuyerResult getMerchantBuyers(String merchant, Long startTime, Long endTime) {
        Map<String, Object> reqMap = new HashMap<>();
        if (merchant != null) {
            reqMap.put("email", merchant);
        }
        if (startTime != null) {
            reqMap.put("start_time", startTime);
        }
        if (endTime != null) {
            reqMap.put("end_time", endTime);
        }

        BCHttpClientUtil.attachAppSign(reqMap);
        Gson gson = new Gson();
        String paramStr = null;
        try {
            paramStr = URLEncoder.encode(gson.toJson(reqMap), "UTF-8");
        } catch (UnsupportedEncodingException ex) {
            Log.e("MerchantUser", "unexpected exception: " + ex.getMessage());
        }

        String url = BCHttpClientUtil.BEECLOUD_HOST + "/2/rest/users?para=" + paramStr;

        BCHttpClientUtil.Response response = BCHttpClientUtil.httpGet(url);
        return (BCQueryMerchantBuyerResult) BCHttpClientUtil.dealWithResult(response,
                BCQueryMerchantBuyerResult.class);
    }
}
