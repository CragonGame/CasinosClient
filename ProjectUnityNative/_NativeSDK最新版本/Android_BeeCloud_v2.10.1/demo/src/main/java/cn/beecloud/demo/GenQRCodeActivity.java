package cn.beecloud.demo;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import java.util.HashMap;
import java.util.Map;

import cn.beecloud.BCCache;
import cn.beecloud.BCOfflinePay;
import cn.beecloud.BCPay;
import cn.beecloud.BCQuery;
import cn.beecloud.async.BCCallback;
import cn.beecloud.async.BCResult;
import cn.beecloud.demo.util.BillUtils;
import cn.beecloud.demo.util.DisplayUtils;
import cn.beecloud.entity.BCBillStatus;
import cn.beecloud.entity.BCQRCodeResult;
import cn.beecloud.entity.BCQueryBillResult;
import cn.beecloud.entity.BCReqParams;
import cn.beecloud.entity.BCRevertStatus;

public class GenQRCodeActivity extends Activity {

    private static final int REQ_QRCODE_CODE=1;
    private static final int NOTIFY_RESULT = 10;
    private static final int ERR_CODE = 99;

    ProgressDialog loadingDialog;

    String billNum;
    String billId;

    String type;
    BCReqParams.BCChannelTypes channelType;
    String billTitle;

    Bitmap qrCodeBitMap;
    String notify;
    String errMsg;

    ImageView qrcodeImg;
    Button btnQueryResult;
    Button btnRevert;

    private Handler mHandler= new Handler(new Handler.Callback() {

        @Override
        public boolean handleMessage(Message msg) {
            switch (msg.what) {
                case REQ_QRCODE_CODE:

                    qrcodeImg.setImageBitmap(qrCodeBitMap);

                    break;

                case NOTIFY_RESULT:
                    Toast.makeText(GenQRCodeActivity.this, notify, Toast.LENGTH_LONG).show();
                    break;

                case ERR_CODE:
                    Toast.makeText(GenQRCodeActivity.this, errMsg, Toast.LENGTH_LONG).show();
            }

            return true;
        }
    });

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_gen_qrcode);

        Intent intent = getIntent();
        type = intent.getStringExtra("type");

        btnRevert = (Button) findViewById(R.id.btnRevert);

        //对于二维码，微信使用 WX_NATIVE 作为channel参数
        //支付宝使用ALI_OFFLINE_QRCODE
        if (type.equals("WX")) {
            channelType = BCReqParams.BCChannelTypes.WX_NATIVE;
            billTitle = "安卓微信二维码测试";
        } else if (type.equals("ALI")) {
            channelType = BCReqParams.BCChannelTypes.ALI_OFFLINE_QRCODE;
            billTitle = "安卓支付宝线下二维码测试";
        } else {
            channelType = BCReqParams.BCChannelTypes.valueOf(type);
            billTitle = "安卓" + type + "二维码测试";
            btnRevert.setVisibility(View.GONE);
            TextView revertTip = (TextView) findViewById(R.id.revertTip);
            revertTip.setVisibility(View.GONE);
        }

        DisplayUtils.initBack(this);

        loadingDialog = new ProgressDialog(this);
        loadingDialog.setMessage("处理中，请稍候...");
        loadingDialog.setIndeterminate(true);
        loadingDialog.setCancelable(true);

        qrcodeImg = (ImageView) this.findViewById(R.id.qrcodeImg);
        btnQueryResult = (Button) findViewById(R.id.btnQueryResult);

        initQueryButton();

        initRevertBtn();

        reqQrCode();
    }

    void reqQrCode(){

        loadingDialog.show();

        Map<String, String> optional=new HashMap<String, String>();
        optional.put("用途", "测试二维码");
        optional.put("testEN", "value哈哈");

        //初始化回调入口
        BCCallback callback = new BCCallback() {
            @Override
            public void done(BCResult bcResult) {

                //此处关闭loading界面
                loadingDialog.dismiss();

                final BCQRCodeResult bcqrCodeResult = (BCQRCodeResult) bcResult;

                Message msg = mHandler.obtainMessage();

                //resultCode为0表示请求成功
                if (bcqrCodeResult.getResultCode() == 0) {
                    billId = bcqrCodeResult.getId();

                    //如果你设置了生成二维码参数为true那么此处可以获取二维码
                    qrCodeBitMap = bcqrCodeResult.getQrCodeBitmap();

                    //否则通过 bcqrCodeResult.getQrCodeRawContent() 获取二维码的内容，自己去生成对应的二维码

                    msg.what = REQ_QRCODE_CODE;
                } else {
                    errMsg = "err code:" + bcqrCodeResult.getResultCode() +
                            "; err msg: " + bcqrCodeResult.getResultMsg() +
                            "; err detail: " + bcqrCodeResult.getErrDetail();

                    /**
                     * 你发布的项目中不需要做如下判断，此处由于支付宝政策原因，
                     * 不再提供支付宝支付的测试功能，所以给出提示说明
                     */
                    if (BCCache.getInstance().appId.equals("c5d1cba1-5e3f-4ba0-941d-9b0a371fe719") &&
                            type.equals("ALI") &&
                            !bcqrCodeResult.getErrDetail().equals("该功能暂不支持测试模式")) {
                        errMsg = "支付失败：由于支付宝政策原因，故不再提供支付宝支付的测试功能，给您带来的不便，敬请谅解";
                    }

                    msg.what = ERR_CODE;
                }

                mHandler.sendMessage(msg);
            }
        };

        billNum = BillUtils.genBillNum();

        if (channelType == BCReqParams.BCChannelTypes.BC_NATIVE) {
            // BeeCloud微信二维码支付并不是严格意义上的线下支付
            BCPay.getInstance(GenQRCodeActivity.this).reqBCNativeAsync(
                    billTitle,  //商品描述
                    1,          //总金额, 以分为单位, 必须是正整数
                    billNum,          //流水号
                    optional,            //扩展参数
                    true,                   //是否生成二维码的bitmap
                    380,                   //二维码的尺寸, 以px为单位, 如果为null则默认为360
                    callback);
        } else {
            BCOfflinePay.PayParams payParam = new BCOfflinePay.PayParams();

            payParam.channelType = channelType;
            payParam.billTitle = billTitle; //商品描述
            payParam.billTotalFee = 1; //总金额, 以分为单位, 必须是正整数
            payParam.billNum = billNum;         //流水号
            payParam.optional = optional;   //扩展参数
            payParam.genQRCode = true;      //是否生成二维码的bitmap
            payParam.qrCodeWidth = 380;                   //二维码的尺寸, 以px为单位, 如果为null则默认为360

            // 商家自定义的消费者Id，传入将分析用户行为
            payParam.buyerId = "merchant-buyer-id";

            BCOfflinePay.getInstance().reqQRCodeAsync(
                    payParam,
                    callback
            );
        }
    }

    void initQueryButton() {
        btnQueryResult.setOnClickListener(new View.OnClickListener() {
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

    void initRevertBtn() {
        btnRevert.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //微信二维码无法撤销
                if (channelType == BCReqParams.BCChannelTypes.WX_NATIVE) {
                    Message msg = mHandler.obtainMessage();
                    msg.what = NOTIFY_RESULT;
                    notify = "微信二维码无法撤销";
                    mHandler.sendMessage(msg);
                    return;
                }

                loadingDialog.setMessage("订单撤销中，请稍候...");
                loadingDialog.show();

                BCOfflinePay.getInstance().reqRevertBillAsync(
                        channelType,
                        billNum,
                        new BCCallback() {
                            @Override
                            public void done(BCResult result) {
                                loadingDialog.dismiss();

                                BCRevertStatus revertStatus = (BCRevertStatus) result;

                                Message msg = mHandler.obtainMessage();

                                if (revertStatus.getResultCode() == 0 &&
                                        revertStatus.getRevertStatus()) {
                                    msg.what = NOTIFY_RESULT;
                                    notify = "撤销成功";
                                } else {

                                    msg.what = ERR_CODE;
                                    errMsg = "撤销失败：" + revertStatus.getResultCode() + " # " +
                                            revertStatus.getResultMsg() + " # " +
                                            revertStatus.getErrDetail();
                                }

                                mHandler.sendMessage(msg);
                            }
                        }
                );
            }
        });

    }
}
