/**
 * GenQRCodeActivity.java
 *
 * Created by xuanzhui on 2015/8/6.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.demo;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

import java.util.HashMap;
import java.util.Map;

import cn.beecloud.BCPay;
import cn.beecloud.async.BCCallback;
import cn.beecloud.async.BCResult;
import cn.beecloud.demo.util.BillUtils;
import cn.beecloud.demo.util.DisplayUtils;
import cn.beecloud.entity.BCQRCodeResult;

/**
 * 用于展示如何生成二维码支付
 */
public class QRCodeEntryActivity extends Activity {
    private static final String Tag = "QRCodeEntryActivity";
    private ProgressDialog loadingDialog;

    Button btnReqALIQRCode;

    private Handler mHandler;

    private String aliQRHtml;
    private String aliQRURL;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_qrcode_entry);

        DisplayUtils.initBack(this);

        loadingDialog = new ProgressDialog(QRCodeEntryActivity.this);
        loadingDialog.setMessage("正在请求服务器, 请稍候...");
        loadingDialog.setIndeterminate(true);
        loadingDialog.setCancelable(true);

        btnReqALIQRCode = (Button) findViewById(R.id.btnReqALIQRCode);

        // Defines a Handler object that's attached to the UI thread
        mHandler = new Handler(new Handler.Callback() {

            @Override
            public boolean handleMessage(Message msg) {
                switch (msg.what) {
                    case 2:
                        //建议在新的activity显示ali内嵌二维码
                        Intent intent = new Intent(QRCodeEntryActivity.this, ALIQRCodeActivity.class);
                        intent.putExtra("aliQRURL", aliQRURL);
                        intent.putExtra("aliQRHtml", aliQRHtml);
                        startActivity(intent);
                        break;
                }

                return true;
            }
        });

        btnReqALIQRCode.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                loadingDialog.show();

                Map<String, String> mapOptional = new HashMap<String, String>();

                mapOptional.put("testalikey1", "测试value值1");
                BCPay.getInstance(QRCodeEntryActivity.this).reqAliInlineQRCodeAsync("支付宝内嵌二维码支付测试",   //商品描述
                        1,                                                  //总金额, 以分为单位, 必须是正整数
                        BillUtils.genBillNum(),      //流水号
                        mapOptional,                                        //扩展参数
                        "https://beecloud.cn/",  //支付成功之后的返回url
                        "1",                          /* 注： 二维码类型含义
                                                        * null则支付宝生成默认类型, 不建议
                                                        * "0": 订单码-简约前置模式, 对应 iframe 宽度不能小于 600px, 高度不能小于 300px
                                                        * "1": 订单码-前置模式, 对应 iframe 宽度不能小于 300px, 高度不能小于 600px
                                                        * "3": 订单码-迷你前置模式, 对应 iframe 宽度不能小于 75px, 高度不能小于 75px
                                                        */
                        new BCCallback() {     //回调入口
                            @Override
                            public void done(BCResult bcResult) {

                                //此处关闭loading界面
                                loadingDialog.dismiss();

                                final BCQRCodeResult bcqrCodeResult = (BCQRCodeResult) bcResult;

                                //resultCode为0表示请求成功
                                if (bcqrCodeResult.getResultCode() == 0) {
                                    aliQRURL = bcqrCodeResult.getQrCodeRawContent();
                                    aliQRHtml = bcqrCodeResult.getAliQRCodeHtml();
                                    Log.w(Tag, "ali qrcode url: " + aliQRURL);
                                    Log.w(Tag, "ali qrcode html: " + aliQRHtml);

                                    Message msg = mHandler.obtainMessage();
                                    msg.what = 2;
                                    mHandler.sendMessage(msg);

                                } else {

                                    QRCodeEntryActivity.this.runOnUiThread(new Runnable() {
                                        @Override
                                        public void run() {

                                            String toastMsg = "err code:" + bcqrCodeResult.getResultCode() +
                                                    "; err msg: " + bcqrCodeResult.getResultMsg() +
                                                    "; err detail: " + bcqrCodeResult.getErrDetail();

                                            /**
                                             * 你发布的项目中不应该出现如下错误，此处由于支付宝政策原因，
                                             * 不再提供支付宝支付的测试功能，所以给出提示说明
                                             */
                                            if (bcqrCodeResult.getResultMsg().equals("PAY_FACTOR_NOT_SET") &&
                                                    bcqrCodeResult.getErrDetail().startsWith("支付宝参数")) {
                                                toastMsg = "支付失败：由于支付宝政策原因，故不再提供支付宝支付的测试功能，给您带来的不便，敬请谅解";
                                            }

                                            Toast.makeText(QRCodeEntryActivity.this, toastMsg, Toast.LENGTH_LONG).show();
                                        }
                                    });

                                }

                            }
                        });
            }
        });

        initOfflineReqBtn();
    }

    void initOfflineReqBtn() {
        Button btnReqWXQRCode = (Button)findViewById(R.id.btnReqWXQRCode);
        btnReqWXQRCode.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(QRCodeEntryActivity.this, GenQRCodeActivity.class);
                intent.putExtra("type", "WX");
                startActivity(intent);
            }
        });

        Button btnReqALIOfflineQRCode = (Button)findViewById(R.id.btnReqALIOfflineQRCode);
        btnReqALIOfflineQRCode.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(QRCodeEntryActivity.this, GenQRCodeActivity.class);
                intent.putExtra("type", "ALI");
                startActivity(intent);
            }
        });

        Button btnReqWXScan = (Button)findViewById(R.id.btnReqWXScan);
        btnReqWXScan.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(QRCodeEntryActivity.this, PayViaAuthCodeActivity.class);
                intent.putExtra("type", "WX");
                startActivity(intent);
            }
        });

        Button btnReqALIScan = (Button)findViewById(R.id.btnReqALIScan);
        btnReqALIScan.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(QRCodeEntryActivity.this, PayViaAuthCodeActivity.class);
                intent.putExtra("type", "ALI");
                startActivity(intent);
            }
        });
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        //用到BCPay的activity在结束前，清理context引用
        BCPay.clear();
    }
}
