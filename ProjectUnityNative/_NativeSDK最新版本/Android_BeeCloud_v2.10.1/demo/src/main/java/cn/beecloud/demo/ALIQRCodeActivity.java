/**
 * ALIQRCodeActivity.java
 *
 * Created by xuanzhui on 2015/8/6.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.demo;

import android.app.Activity;
import android.net.http.SslError;
import android.os.Bundle;
import android.webkit.SslErrorHandler;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;

import cn.beecloud.demo.util.DisplayUtils;

/**
 * 用于展示Ali内嵌二维码
 */
public class ALIQRCodeActivity extends Activity {
    private WebView aliQRCode;
    private String aliQRURL;
    private String aliQRHtml;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_aliqrcode);

        DisplayUtils.initBack(this);

        aliQRCode = (WebView) findViewById(R.id.aliQRCode);
        WebSettings webSettings = aliQRCode.getSettings();
        webSettings.setJavaScriptEnabled(true);
        // 设置可以支持缩放
        webSettings.setSupportZoom(true);
        webSettings.setUseWideViewPort(true);
        //设置默认加载的可视范围是大视野范围
        webSettings.setLoadWithOverviewMode(true);
        // 设置出现缩放工具
        webSettings.setBuiltInZoomControls(true);


        aliQRCode.setWebViewClient(new WebViewClient() {
            @Override
            public boolean shouldOverrideUrlLoading(WebView view, String url) {
                // Here put your code
                //Log.w("My Webview", url);

                // return true; //Indicates WebView to NOT load the url;
                return false; //Allow WebView to load url
            }

            //必须重写
            @Override
            public void onReceivedSslError(WebView view, SslErrorHandler handler, SslError error) {
                handler.proceed(); // Ignore SSL certificate errors
            }
        });

        aliQRHtml = getIntent().getStringExtra("aliQRHtml");
        aliQRURL = getIntent().getStringExtra("aliQRURL");

        //可以
        //aliQRCode.loadUrl(aliQRURL);

        //或者
        aliQRCode.loadData(aliQRHtml, "text/html", "utf-8");

    }
}
