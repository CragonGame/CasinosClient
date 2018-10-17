/**
 * BCUnionPaymentActivity.java
 *
 * Created by xuanzhui on 2015/7/27.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud;

import android.app.Activity;
import android.content.Intent;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.util.Log;

import com.unionpay.UPPayAssistEx;

import cn.beecloud.entity.BCPayResult;

/**
 * 用于银联支付
 */
public class BCUnionPaymentActivity extends Activity {
    @Override
    public void onStart(){
        super.onStart();

        Bundle extras = getIntent().getExtras();
        if (extras != null) {
            String tn= extras.getString("tn");
            UPPayAssistEx.startPay(this, null, null, tn, "00");
        } else {
            finish();
        }
    }

    /**
     * 处理银联手机支付控件返回的支付结果
     */
    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        String result = null;
        int errCode = -99;
        String errMsg = null;
        String detailInfo = "银联支付:";

        //银联插件内部升级会出现data为null的情况
        if (data == null) {
            result = BCPayResult.RESULT_FAIL;
            errCode = BCPayResult.APP_INTERNAL_THIRD_CHANNEL_ERR_CODE;
            errMsg = BCPayResult.FAIL_ERR_FROM_CHANNEL;
            detailInfo += "invalid pay result";
        } else {
            /*
             * 支付控件返回字符串:success、fail、cancel 分别代表支付成功，支付失败，支付取消
             */
            String str = data.getExtras().getString("pay_result");

            if (str == null) {
                result = BCPayResult.RESULT_FAIL;
                errCode = BCPayResult.APP_INTERNAL_THIRD_CHANNEL_ERR_CODE;
                errMsg = BCPayResult.FAIL_ERR_FROM_CHANNEL;
                detailInfo += "invalid pay result";
            } else if (str.equalsIgnoreCase("success")) {
                result = BCPayResult.RESULT_SUCCESS;
                errCode = BCPayResult.APP_PAY_SUCC_CODE;
                errMsg = BCPayResult.RESULT_SUCCESS;
                detailInfo += "支付成功！";
            } else if (str.equalsIgnoreCase("fail")) {
                result = BCPayResult.RESULT_FAIL;
                errCode = BCPayResult.APP_INTERNAL_THIRD_CHANNEL_ERR_CODE;
                errMsg = BCPayResult.RESULT_FAIL;
                detailInfo += "支付失败！";
            } else if (str.equalsIgnoreCase("cancel")) {
                result = BCPayResult.RESULT_CANCEL;
                errCode = BCPayResult.APP_PAY_CANCEL_CODE;
                errMsg = BCPayResult.RESULT_CANCEL;
                detailInfo += "用户取消了支付";
            }
        }

        if (BCPay.payCallback != null) {
            BCPay.payCallback.done(new BCPayResult(result, errCode, errMsg,
                    detailInfo, BCCache.getInstance().billID));
        } else {
            Log.e("BCUnionPaymentActivity", "BCPay payCallback NPE");
        }

        this.finish();
    }
}
