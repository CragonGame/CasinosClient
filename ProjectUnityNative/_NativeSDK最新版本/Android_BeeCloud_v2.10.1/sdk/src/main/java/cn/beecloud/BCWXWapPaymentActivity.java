package cn.beecloud;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.net.Uri;
import android.net.http.SslError;
import android.os.Bundle;
import android.util.Log;
import android.webkit.SslErrorHandler;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.LinearLayout;

import com.google.gson.JsonSyntaxException;

import cn.beecloud.entity.BCPayResult;
import cn.beecloud.entity.BCQueryBillResult;
import cn.beecloud.entity.BCQueryReqParams;
import cn.beecloud.entity.BCReqParams;

public class BCWXWapPaymentActivity extends Activity {
    private static final String TAG = "BCWXWapPaymentActivity";
    boolean firstIn = true;
    WebView webView;
    String url;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        //根布局参数
        LinearLayout.LayoutParams layoutParamsRoot =
                new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT,
                        LinearLayout.LayoutParams.MATCH_PARENT);

        //根布局
        LinearLayout layoutRoot = new LinearLayout(this);
        layoutRoot.setLayoutParams(layoutParamsRoot);
        layoutRoot.setOrientation(LinearLayout.VERTICAL);
        layoutRoot.setBackgroundColor(0xFFF5F5F5);

        webView = new WebView(this);

        LinearLayout.LayoutParams webViewLayoutParams =
                new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT,
                        LinearLayout.LayoutParams.MATCH_PARENT);

        layoutRoot.addView(webView, webViewLayoutParams);

        setContentView(layoutRoot);

        WebSettings webSettings = webView.getSettings();
        webSettings.setJavaScriptEnabled(true);
        // 设置可以支持缩放
        webSettings.setSupportZoom(true);
        webSettings.setUseWideViewPort(true);
        //设置默认加载的可视范围是大视野范围
        webSettings.setLoadWithOverviewMode(true);
        // 设置出现缩放工具
        webSettings.setBuiltInZoomControls(true);

        webView.setWebViewClient(new WebViewClient() {
            @Override
            public boolean shouldOverrideUrlLoading(WebView view, String url) {
                // 如下方案可在非微信内部WebView的H5页面中调出微信支付
                if (url.startsWith("weixin://wap/pay?")) {
                    Intent intent = new Intent();
                    intent.setAction(Intent.ACTION_VIEW);
                    intent.setData(Uri.parse(url));
                    startActivity(intent);

                    return true;
                }
                return super.shouldOverrideUrlLoading(view, url);
            }
        });

        url = getIntent().getStringExtra("url");

        webView.loadUrl(url);
    }

    public void onResume() {
        super.onResume();

        synchronized (this) {
            if (firstIn) {
                firstIn = false;
                return;
            }
        }

        final ProgressDialog loadingDialog = new ProgressDialog(this);
        loadingDialog.setMessage("处理中，请稍候...");
        loadingDialog.setIndeterminate(true);
        loadingDialog.setCancelable(false);
        loadingDialog.show();

        BCCache.executorService.execute(new Runnable() {
            @Override
            public void run() {
                boolean found = false;

                //总共查3次
                for (int i = 0; i < 3; i++) {
                    BCQueryReqParams bcQueryReqParams;
                    try {
                        bcQueryReqParams = new BCQueryReqParams(BCReqParams.BCChannelTypes.ALL);
                    } catch (BCException e) {
                        Log.w(TAG, e.getMessage() + "");
                        return;
                    }

                    String queryURL = BCHttpClientUtil.getBillQueryURL() + "/"
                            + BCCache.getInstance().billID + "?para=";

                    BCHttpClientUtil.Response response = BCHttpClientUtil.httpGet(queryURL +
                            bcQueryReqParams.transToEncodedJsonString());

                    if (response.code == 200 || (response.code >= 400 && response.code < 500)) {
                        try {
                            BCQueryBillResult billResult = BCQueryBillResult.transJsonToResultObject(response.content);
                            // 如果查询到成功退出
                            if (billResult.getResultCode() == 0 &&
                                    billResult.getBill().getPayResult() != null &&
                                    billResult.getBill().getPayResult()) {
                                if (BCPay.payCallback != null) {
                                    BCPay.payCallback.done(new BCPayResult(
                                            BCPayResult.RESULT_SUCCESS,
                                            BCPayResult.APP_PAY_SUCC_CODE,
                                            BCPayResult.RESULT_SUCCESS,
                                            "用户支付成功",
                                            BCCache.getInstance().billID));
                                }

                                found = true;
                                break;
                            }
                        } catch (JsonSyntaxException ex) {
                            Log.w(TAG, ex.getMessage() + "");
                        }
                    }

                    try {
                        // 0.5s后继续查
                        Thread.sleep(500);
                    } catch (InterruptedException e) {
                        Log.w(TAG, e.getMessage() + "");
                    }
                }

                if (!found) {
                    // 如果一直没有查询到支付结果
                    if (BCPay.payCallback != null) {
                        BCPay.payCallback.done(new BCPayResult(
                                BCPayResult.RESULT_CANCEL,
                                BCPayResult.APP_PAY_CANCEL_CODE,
                                BCPayResult.RESULT_CANCEL,
                                "用户未支付，或服务器通信延迟，如果用户确认已支付，请注意webhook推送",
                                BCCache.getInstance().billID));
                    }
                }

                loadingDialog.dismiss();
                finish();
            }
        });
    }
}
