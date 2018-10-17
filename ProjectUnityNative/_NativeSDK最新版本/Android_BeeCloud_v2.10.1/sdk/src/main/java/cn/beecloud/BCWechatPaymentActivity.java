/**
 * BCWechatPaymentActivity.java
 *
 * Created by xuanzhui on 2015/7/27.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;

import com.tencent.mm.opensdk.modelbase.BaseReq;
import com.tencent.mm.opensdk.modelbase.BaseResp;
import com.tencent.mm.opensdk.openapi.IWXAPI;
import com.tencent.mm.opensdk.openapi.IWXAPIEventHandler;
import com.tencent.mm.opensdk.openapi.WXAPIFactory;

import cn.beecloud.entity.BCPayResult;

/*
 * 微信支付结果接收类
 */
public class BCWechatPaymentActivity extends Activity implements IWXAPIEventHandler {
    private static final String TAG = "BCWechatPaymentActivity";

    private IWXAPI wxAPI;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        Log.i(TAG, "into weixin return activity");

        try {
            String wxAppId = BCCache.getInstance().wxAppId;
            if (wxAppId != null && wxAppId.length() > 0) {
                wxAPI = WXAPIFactory.createWXAPI(this, wxAppId);
                wxAPI.handleIntent(getIntent(), this);
            } else {
                Log.e(TAG, "Error: wxAppId 不合法 BCWechatPaymentActivity: " + wxAppId);

                if (BCPay.payCallback != null) {
                    BCPay.payCallback.done(
                            new BCPayResult(BCPayResult.RESULT_FAIL,
                                BCPayResult.APP_INTERNAL_PARAMS_ERR_CODE,
                                BCPayResult.FAIL_INVALID_PARAMS,
                                "wxAppId 不合法"));
                }

                this.finish();

            }
        } catch (Exception ex) {
            Log.e(TAG, ex.getMessage()==null ? "未知错误，初始化回调入口失败" : ex.getMessage());

            if (BCPay.payCallback != null) {
                BCPay.payCallback.done(
                        new BCPayResult(BCPayResult.RESULT_FAIL,
                            BCPayResult.APP_INTERNAL_EXCEPTION_ERR_CODE,
                            BCPayResult.FAIL_EXCEPTION,
                            "微信回调入口初始化失败"));
            }

            this.finish();
        }
    }

    @Override
    protected void onNewIntent(Intent intent) {
        super.onNewIntent(intent);
        try {
            setIntent(intent);
            if (wxAPI != null) {
                wxAPI.handleIntent(intent, this);
            }
        } catch (Exception ex) {
            if (BCPay.payCallback != null) {
                BCPay.payCallback.done(
                        new BCPayResult(BCPayResult.RESULT_FAIL,
                                BCPayResult.APP_INTERNAL_EXCEPTION_ERR_CODE,
                                BCPayResult.FAIL_EXCEPTION,
                                "微信回调入口初始化失败"));
            }

            this.finish();
        }
    }

    /**
     * 微信发送请求到第三方应用时，会回调到该方法
     */
    @Override
    public void onReq(BaseReq baseReq) {

    }

    /**
     * 第三方应用发送到微信的请求处理后的响应结果，会回调到该方法
     */
    @Override
    public void onResp(BaseResp baseResp) {

        Log.i(TAG, "onPayFinish, result code = " + baseResp.errCode);

        String result = BCPayResult.RESULT_FAIL;
        int errCode = BCPayResult.APP_INTERNAL_THIRD_CHANNEL_ERR_CODE;
        String errMsg = BCPayResult.FAIL_ERR_FROM_CHANNEL;

        String detailInfo = "WECHAT_PAY: ";
        switch (baseResp.errCode) {
            case BaseResp.ErrCode.ERR_OK:
                result = BCPayResult.RESULT_SUCCESS;
                errCode = BCPayResult.APP_PAY_SUCC_CODE;
                errMsg = BCPayResult.RESULT_SUCCESS;
                detailInfo += "用户支付已成功";
                break;
            case BaseResp.ErrCode.ERR_USER_CANCEL:
                result = BCPayResult.RESULT_CANCEL;
                errCode = BCPayResult.APP_PAY_CANCEL_CODE;
                errMsg = BCPayResult.RESULT_CANCEL;
                detailInfo += "用户取消";
                break;
            case BaseResp.ErrCode.ERR_AUTH_DENIED:
                detailInfo += "发送被拒绝";
                break;
            case BaseResp.ErrCode.ERR_COMM:
                detailInfo += "一般错误，微信Debug版本常见错误，请查看工单http://help.beecloud.cn/hc/kb/article/157111";
                break;
            case BaseResp.ErrCode.ERR_UNSUPPORT:
                detailInfo += "不支持错误";
                break;
            case BaseResp.ErrCode.ERR_SENT_FAILED:
                detailInfo += "发送失败";
                break;
            default:
                detailInfo += "支付失败";
        }

        if (BCPay.payCallback != null) {
            BCPay.payCallback.done(new BCPayResult(result,
                    errCode,
                    errMsg,
                    detailInfo,
                    BCCache.getInstance().billID));
        }
        this.finish();
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        if (wxAPI != null)
            wxAPI.detach();
    }
}
