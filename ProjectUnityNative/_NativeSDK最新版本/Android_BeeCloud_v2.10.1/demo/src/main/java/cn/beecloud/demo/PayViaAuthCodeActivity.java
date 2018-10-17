package cn.beecloud.demo;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.content.res.AssetManager;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.google.zxing.integration.android.IntentIntegrator;
import com.google.zxing.integration.android.IntentResult;

import java.io.File;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.HashMap;
import java.util.Map;

import cn.beecloud.BCCache;
import cn.beecloud.BCOfflinePay;
import cn.beecloud.BCQuery;
import cn.beecloud.async.BCCallback;
import cn.beecloud.async.BCResult;
import cn.beecloud.demo.util.BillUtils;
import cn.beecloud.demo.util.DisplayUtils;
import cn.beecloud.entity.BCBillStatus;
import cn.beecloud.entity.BCPayResult;
import cn.beecloud.entity.BCQueryBillResult;
import cn.beecloud.entity.BCReqParams;

public class PayViaAuthCodeActivity extends Activity {
    private static final String TAG = "PayViaAuthCodeActivity";

    private static final int REQ_OFF_PAY_SUCC = 1;
    private static final int NOTIFY_RESULT = 10;
    private static final int ERR_CODE = 99;

    private ProgressDialog loadingDialog;

    TextView authCodeView;
    Button payBtn;
    Button scanBtn;
    Button queryBtn;

    BCReqParams.BCChannelTypes channelType;
    String type;
    String billTitle;
    String billNum;
    String billId;
    String authCode;
    String errMsg;
    String notify;

    private Handler mHandler = new Handler(new Handler.Callback() {

        @Override
        public boolean handleMessage(Message msg) {
            switch (msg.what) {

                case REQ_OFF_PAY_SUCC:

                    Toast.makeText(PayViaAuthCodeActivity.this,
                            "发起支付成功，请通过查询API确认",
                            Toast.LENGTH_SHORT).show();

                    break;

                case NOTIFY_RESULT:
                    Toast.makeText(PayViaAuthCodeActivity.this,
                            notify,
                            Toast.LENGTH_SHORT).show();

                    break;
                case ERR_CODE:

                    Toast.makeText(PayViaAuthCodeActivity.this, errMsg, Toast.LENGTH_LONG).show();
                    //finish();
            }

            return true;
        }
    });

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_pay_via_auth_code);

        Intent intent = getIntent();
        type = intent.getStringExtra("type");

        //对于二维码，微信使用 WX_NATIVE 作为channel参数
        //支付宝使用ALI_OFFLINE_QRCODE
        if (type.equals("WX")) {
            channelType = BCReqParams.BCChannelTypes.WX_SCAN;
            billTitle = "安卓通过扫描微信付款码支付测试";
        } else if (type.equals("ALI")) {
            channelType = BCReqParams.BCChannelTypes.ALI_SCAN;
            billTitle = "安卓通过扫描支付宝付款码支付测试";
        } else {
            channelType = BCReqParams.BCChannelTypes.valueOf(type);
            billTitle = "安卓" + type + "支付测试";
        }

        DisplayUtils.initBack(this);

        loadingDialog = new ProgressDialog(this);
        loadingDialog.setMessage("处理中，请稍候...");
        loadingDialog.setIndeterminate(true);
        loadingDialog.setCancelable(true);

        authCodeView = (TextView) findViewById(R.id.authCodeView);

        initScanBtn();
        initPayBtn();
        initQueryBtn();
    }

    void initScanBtn() {
        scanBtn = (Button) findViewById(R.id.scanBtn);
        scanBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                if (!preCheck())
                    return;

                IntentIntegrator integrator = new IntentIntegrator(PayViaAuthCodeActivity.this);
                integrator.initiateScan();
            }
        });
    }

    void initPayBtn() {
        payBtn = (Button) findViewById(R.id.payBtn);
        payBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                loadingDialog.show();

                billNum = BillUtils.genBillNum();

                Map<String, String> optional = new HashMap<String, String>();
                optional.put("用途", "测试扫码支付");
                optional.put("testEN", "value恩恩");

                BCCallback callback = new BCCallback() {     //回调入口
                    @Override
                    public void done(BCResult bcResult) {

                        //此处关闭loading界面
                        loadingDialog.dismiss();

                        final BCPayResult payResult = (BCPayResult) bcResult;

                        Message msg = mHandler.obtainMessage();

                        //RESULT_SUCCESS表示请求成功
                        if (payResult.getResult().equals(BCPayResult.RESULT_SUCCESS)) {
                            msg.what = REQ_OFF_PAY_SUCC;
                            billId = payResult.getId();
                        } else {

                            errMsg = "支付失败，请重试；错误信息：" +
                                    "err code:" + payResult.getResult() +
                                    "; err msg: " + payResult.getErrMsg() +
                                    "; err detail: " + payResult.getDetailInfo();

                            /**
                             * 你发布的项目中不需要做如下判断，此处由于支付宝政策原因，
                             * 不再提供支付宝支付的测试功能，所以给出提示说明
                             */
                            if (BCCache.getInstance().appId.equals("c5d1cba1-5e3f-4ba0-941d-9b0a371fe719") &&
                                    type.equals("ALI") &&
                                    !payResult.getDetailInfo().equals("该功能暂不支持测试模式"))
                                errMsg = "支付失败：由于支付宝政策原因，故不再提供支付宝支付的测试功能，给您带来的不便，敬请谅解";

                            msg.what = ERR_CODE;
                        }

                        mHandler.sendMessage(msg);
                    }
                };

                BCOfflinePay.PayParams payParam = new BCOfflinePay.PayParams();
                payParam.channelType = channelType;
                payParam.billTitle = billTitle;  //商品描述
                payParam.billTotalFee = 1;  //总金额, 以分为单位, 必须是正整数
                payParam.billNum = billNum; //流水号
                payParam.optional = optional;   //扩展参数
                payParam.authCode = authCode;   //付款码
                payParam.terminalId = "fake-terminalId";    //若机具商接入terminalId(机具终端编号)必填

                // 商家自定义的消费者Id，传入将分析用户行为
                payParam.buyerId = "merchant-buyer-id";

                BCOfflinePay.getInstance().reqOfflinePayAsync(
                        payParam,
                        callback);

            }
        });
    }

    void initQueryBtn() {
        queryBtn = (Button) findViewById(R.id.queryBtn);

        queryBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                loadingDialog.setMessage("订单查询中，请稍候...");
                loadingDialog.show();

                if (type.startsWith("BC")) {
                    // BC的渠道通过id查询结果
                    BCQuery.getInstance().queryBillByIDAsync(billId, new BCCallback() {
                        @Override
                        public void done(BCResult result) {
                            loadingDialog.dismiss();

                            BCQueryBillResult billStatus = (BCQueryBillResult) result;

                            Message msg = mHandler.obtainMessage();

                            //表示支付成功
                            if (billStatus.getResultCode() == 0 &&
                                    billStatus.getBill().getPayResult()) {
                                msg.what = NOTIFY_RESULT;
                                notify = "支付成功";
                            } else {

                                msg.what = ERR_CODE;
                                errMsg = "支付失败：" + billStatus.getResultCode() + " # " +
                                        billStatus.getResultMsg() + " # " +
                                        billStatus.getErrDetail();
                            }

                            mHandler.sendMessage(msg);
                        }
                    });
                } else {
                    BCQuery.getInstance().queryOfflineBillStatusAsync(
                            channelType,
                            billNum,
                            new BCCallback() {
                                @Override
                                public void done(BCResult result) {
                                    loadingDialog.dismiss();

                                    BCBillStatus billStatus = (BCBillStatus) result;

                                    Message msg = mHandler.obtainMessage();

                                    //表示支付成功
                                    if (billStatus.getResultCode() == 0 &&
                                            billStatus.getPayResult()) {
                                        msg.what = NOTIFY_RESULT;
                                        notify = "支付成功";
                                    } else {

                                        msg.what = ERR_CODE;
                                        errMsg = "支付失败：" + billStatus.getResultCode() + " # " +
                                                billStatus.getResultMsg() + " # " +
                                                billStatus.getErrDetail();
                                    }

                                    mHandler.sendMessage(msg);
                                }
                            }
                    );
                }
            }
        });
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (requestCode == IntentIntegrator.REQUEST_CODE) {
            IntentResult scanResult = IntentIntegrator.parseActivityResult(requestCode, resultCode, data);
            if (scanResult != null) {
                // handle scan result
                Log.d(TAG, scanResult.toString());

                authCode = scanResult.getContents();
                authCodeView.setText("收款码：" + authCode);
            } else {
                // else continue with any other code you need in the method
                Toast.makeText(PayViaAuthCodeActivity.this, "无法获取收款码，请重试", Toast.LENGTH_LONG).show();
            }
        }
    }

    public boolean preCheck() {
        boolean res = appInstalledOrNot("com.google.zxing.client.android");

        if (!res) {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.setTitle("提示");
            builder.setMessage("本示例依赖ZXing扫码APP，是否安装？");

            builder.setNegativeButton("确定",
                    new DialogInterface.OnClickListener() {
                        @Override
                        public void onClick(DialogInterface dialog, int which) {
                            installScannerPlugin();
                            dialog.dismiss();
                        }
                    });

            builder.setPositiveButton("取消",
                    new DialogInterface.OnClickListener() {

                        @Override
                        public void onClick(DialogInterface dialog, int which) {
                            dialog.dismiss();
                        }
                    });
            builder.create().show();
        }

        return res;
    }

    private void installScannerPlugin() {
        AssetManager assetManager = getAssets();

        InputStream in;
        OutputStream out;

        try {
            in = assetManager.open("BarcodeScanner.apk");
            out = new FileOutputStream(Environment.getExternalStorageDirectory()
                    + File.separator + "BarcodeScanner.apk");

            byte[] buffer = new byte[1024];

            int len;
            while ((len = in.read(buffer)) != -1) {
                out.write(buffer, 0, len);
            }

            in.close();

            out.flush();
            out.close();

            Intent intent = new Intent(Intent.ACTION_VIEW);

            intent.setDataAndType(Uri.fromFile(new File(Environment.getExternalStorageDirectory()
                            + File.separator + "BarcodeScanner.apk")),
                    "application/vnd.android.package-archive");

            startActivity(intent);

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private boolean appInstalledOrNot(String uri) {
        PackageManager pm = getPackageManager();
        boolean appInstalled;
        try {
            pm.getPackageInfo(uri, 0);
            appInstalled = true;
        } catch (PackageManager.NameNotFoundException e) {
            appInstalled = false;
        }
        return appInstalled;
    }
}
