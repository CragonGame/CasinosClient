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
import cn.beecloud.entity.BCPayResult;
import cn.beecloud.entity.BCQRCodeResult;
import cn.beecloud.entity.BCReqParams;

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
                BCPay.PayParams payParams = new BCPay.PayParams();
                payParams.channelType = BCReqParams.BCChannelTypes.ALI_QRCODE;
                payParams.billTitle = "支付宝内嵌二维码支付测试";
                payParams.billTotalFee = 1;
                payParams.billNum = BillUtils.genBillNum();
                payParams.optional = mapOptional;
                payParams.returnUrl = "https://beecloud.cn/";
                payParams.qrPayMode = "1";
                BCPay.getInstance(QRCodeEntryActivity.this).reqPaymentAsync(payParams,
                        new BCCallback() {     //回调入口
                            @Override
                            public void done(BCResult bcResult) {

                                //此处关闭loading界面
                                loadingDialog.dismiss();

                                final BCPayResult bcPayResult = (BCPayResult) bcResult;

                                //resultCode为0表示请求成功
                                if (bcPayResult.getErrCode() == 0) {
                                    aliQRURL = bcPayResult.getUrl();
                                    aliQRHtml = bcPayResult.getHtml();
                                    Log.w(Tag, "ali qrcode url: " + aliQRURL);
                                    Log.w(Tag, "ali qrcode html: " + aliQRHtml);

                                    Message msg = mHandler.obtainMessage();
                                    msg.what = 2;
                                    mHandler.sendMessage(msg);

                                } else {

                                    QRCodeEntryActivity.this.runOnUiThread(new Runnable() {
                                        @Override
                                        public void run() {

                                            String toastMsg = "err code:" + bcPayResult.getErrCode() +
                                                    "; err msg: " + bcPayResult.getErrMsg() +
                                                    "; err detail: " + bcPayResult.getDetailInfo();

                                            Toast.makeText(QRCodeEntryActivity.this, toastMsg, Toast.LENGTH_LONG).show();
                                        }
                                    });

                                }

                            }
                        });
            }
        });

        Button btnReqBCQRCode = (Button)findViewById(R.id.btnReqBCQRCode);
        btnReqBCQRCode.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(QRCodeEntryActivity.this, GenQRCodeActivity.class);
                intent.putExtra("type", "BC_NATIVE");
                startActivity(intent);
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

    public void reqBCAliOffCode(View view) {
        Intent intent = new Intent(QRCodeEntryActivity.this, GenQRCodeActivity.class);
        intent.putExtra("type", "BC_ALI_QRCODE");
        startActivity(intent);
    }

    public void reqBCWXScan(View view) {
        Intent intent = new Intent(QRCodeEntryActivity.this, PayViaAuthCodeActivity.class);
        intent.putExtra("type", "BC_WX_SCAN");
        startActivity(intent);
    }

    public void reqBCAliScan(View view) {
        Intent intent = new Intent(QRCodeEntryActivity.this, PayViaAuthCodeActivity.class);
        intent.putExtra("type", "BC_ALI_SCAN");
        startActivity(intent);
    }
}
