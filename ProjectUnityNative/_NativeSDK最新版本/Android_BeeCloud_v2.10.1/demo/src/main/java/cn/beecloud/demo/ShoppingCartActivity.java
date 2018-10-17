/**
 * ShoppingCartActivity.java
 * <p/>
 * Created by xuanzhui on 2015/7/29.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.demo;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.res.AssetManager;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ListView;
import android.widget.Toast;

import com.unionpay.UPPayAssistEx;

import java.io.File;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;

import cn.beecloud.BCCache;
import cn.beecloud.BCPay;
import cn.beecloud.BCQuery;
import cn.beecloud.BeeCloud;
import cn.beecloud.async.BCCallback;
import cn.beecloud.async.BCResult;
import cn.beecloud.demo.util.BillUtils;
import cn.beecloud.entity.BCBillOrder;
import cn.beecloud.entity.BCPayResult;
import cn.beecloud.entity.BCQueryBillResult;
import cn.beecloud.entity.BCReqParams;


public class ShoppingCartActivity extends Activity {
    private static final String TAG = "ShoppingCartActivity";

    Button btnQueryOrders;

    private ProgressDialog loadingDialog;
    private ListView payMethod;

    private String toastMsg;

    //支付结果返回入口
    BCCallback bcCallback = new BCCallback() {
        @Override
        public void done(final BCResult bcResult) {
            final BCPayResult bcPayResult = (BCPayResult) bcResult;
            //此处关闭loading界面
            loadingDialog.dismiss();

            //根据你自己的需求处理支付结果
            String result = bcPayResult.getResult();

            /*
              注意！
              所有支付渠道建议以服务端的状态金额为准，此处返回的RESULT_SUCCESS仅仅代表手机端支付成功
            */
            Message msg = mHandler.obtainMessage();
            //单纯的显示支付结果
            msg.what = 2;
            if (result.equals(BCPayResult.RESULT_SUCCESS)) {
                toastMsg = "用户支付成功";
            } else if (result.equals(BCPayResult.RESULT_CANCEL)) {
                toastMsg = "用户取消支付";
            } else if (result.equals(BCPayResult.RESULT_FAIL)) {
                toastMsg = "支付失败, 原因: " + bcPayResult.getErrCode() +
                        " # " + bcPayResult.getErrMsg() +
                        " # " + bcPayResult.getDetailInfo();

                /*
                 * 你发布的项目中不应该出现如下错误，此处由于支付宝政策原因，
                 * 不再提供支付宝支付的测试功能，所以给出提示说明
                 */
                if (bcPayResult.getErrMsg().equals("PAY_FACTOR_NOT_SET") &&
                        bcPayResult.getDetailInfo().startsWith("支付宝参数")) {
                    toastMsg = "支付失败：由于支付宝政策原因，故不再提供支付宝支付的测试功能，给您带来的不便，敬请谅解";
                }

                /*
                 * 以下是正常流程，请按需处理失败信息
                 */
                Log.e(TAG, toastMsg);

            } else if (result.equals(BCPayResult.RESULT_UNKNOWN)) {
                //可能出现在支付宝8000返回状态
                toastMsg = "订单状态未知";
            } else {
                toastMsg = "invalid return";
            }

            mHandler.sendMessage(msg);

            /*
            if (bcPayResult.getId() != null) {
                //你可以把这个id存到你的订单中，下次直接通过这个id查询订单
                Log.w(TAG, "bill id retrieved : " + bcPayResult.getId());

                //根据ID查询，此处只是演示如何通过id查询订单，并非支付必要部分
                getBillInfoByID(bcPayResult.getId());
            }
            */
        }
    };

    // Defines a Handler object that's attached to the UI thread.
    // 通过Handler.Callback()可消除内存泄漏警告
    private Handler mHandler = new Handler(new Handler.Callback() {
        /**
         * Callback interface you can use when instantiating a Handler to avoid
         * having to implement your own subclass of Handler.
         *
         * handleMessage() defines the operations to perform when
         * the Handler receives a new Message to process.
         */
        @Override
        public boolean handleMessage(Message msg) {
            switch (msg.what) {
                case 2:
                    Toast.makeText(ShoppingCartActivity.this, toastMsg, Toast.LENGTH_SHORT).show();
                    break;
            }
            return true;
        }
    });

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_shopping_cart);

        // 推荐在主Activity或application里的onCreate函数中初始化BeeCloud.
//        BeeCloud.setSandbox(true);
        BeeCloud.setAppIdAndSecret("you app id",
                "you app secret");

        // 如果用到微信支付，在用到微信支付的Activity的onCreate函数里调用以下函数.
        // 第二个参数需要换成你自己的微信AppID.
        String initInfo = BCPay.initWechatPay(ShoppingCartActivity.this, "wxf1aa465362b4c8f1");
        if (initInfo != null) {
            Toast.makeText(this, "微信初始化失败：" + initInfo, Toast.LENGTH_LONG).show();
        }

        payMethod = (ListView) this.findViewById(R.id.payMethod);
        Integer[] payIcons = new Integer[]{R.drawable.wechat, R.drawable.alipay,
                R.drawable.unionpay, R.drawable.beecloud_logo, R.drawable.baidupay,
                R.drawable.rss, R.drawable.wechat, R.drawable.wechat,
                R.drawable.alipay,R.drawable.scan};
        final String[] payNames = new String[]{"微信支付", "支付宝支付", "银联在线", "BeeCloud支付",
                "百度钱包", "订阅支付", "微信WAP支付", "BeeCloud微信支付",
                "BeeCloud支付宝APP支付", "二维码支付"};
        String[] payDescs = new String[]{"使用微信支付，以人民币CNY计费", "使用支付宝支付，以人民币CNY计费",
                "使用银联在线支付，以人民币CNY计费", "通过BeeCloud快捷支付", "使用百度钱包支付，以人民币CNY计费",
                "通过订阅计划，自动缴费", "使用微信WAP支付，以人民币CNY计费",
                "使用BeeCloud专用微信支付，以人民币CNY计费", "使用支付宝ISV渠道支付，以人民币CNY计费",
                "通过扫描二维码支付"};
        PayMethodListItem adapter = new PayMethodListItem(this, payIcons, payNames, payDescs);
        payMethod.setAdapter(adapter);

        // 如果调起支付太慢, 可以在这里开启动画, 以progressdialog为例
        loadingDialog = new ProgressDialog(ShoppingCartActivity.this);
        loadingDialog.setMessage("处理中，请稍候...");
        loadingDialog.setIndeterminate(true);
        loadingDialog.setCancelable(true);

        payMethod.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                switch (position) {

                    case 0: //微信
                    {
                        loadingDialog.show();
                        //对于微信支付, 手机内存太小会有OutOfResourcesException造成的卡顿, 以致无法完成支付
                        //这个是微信自身存在的问题
                        Map<String, String> mapOptional = new HashMap<String, String>();

                        mapOptional.put("testkey1", "测试value值1");

                        if (BCPay.isWXAppInstalledAndSupported() &&
                                BCPay.isWXPaySupported()) {

                            BCPay.PayParams payParams = new BCPay.PayParams();
                            payParams.channelType = BCReqParams.BCChannelTypes.WX_APP;
                            payParams.billTitle = "安卓微信支付测试";   //订单标题
                            payParams.billTotalFee = 11;    //订单金额(分)
                            payParams.billNum = BillUtils.genBillNum();  //订单流水号
                            payParams.couponId = "bbbf835d-f6b0-484f-bb6e-8e6082d4a35f";    // 优惠券ID
                            payParams.optional = mapOptional;            //扩展参数(可以null)

                            BCPay.getInstance(ShoppingCartActivity.this).reqPaymentAsync(
                                    payParams,
                                    bcCallback);            //支付完成后回调入口

                        } else {
                            Toast.makeText(ShoppingCartActivity.this,
                                    "您尚未安装微信或者安装的微信版本不支持", Toast.LENGTH_LONG).show();
                            loadingDialog.dismiss();
                        }
                        break;
                    }
                    case 1: {//支付宝支付
                        loadingDialog.show();

                        Map<String, String> mapOptional = new HashMap<>();
//                        mapOptional.put("disable_pay_channels", "balance,creditCard");

                        BCPay.PayParams aliParam = new BCPay.PayParams();
                        aliParam.channelType = BCReqParams.BCChannelTypes.ALI_APP;
                        aliParam.billTitle = "测试";
                        aliParam.billTotalFee = 1;
                        aliParam.billNum = BillUtils.genBillNum();
                        aliParam.optional = mapOptional;

                        BCPay.getInstance(ShoppingCartActivity.this).reqPaymentAsync(
                                aliParam, bcCallback);

                        break;
                    }
                    case 2: { //银联支付
                        loadingDialog.show();

                        BCPay.PayParams payParam = new BCPay.PayParams();

                        payParam.channelType = BCReqParams.BCChannelTypes.UN_APP;

                        //商品描述, 32个字节内, 汉字以2个字节计
                        payParam.billTitle = "银联app支付";

                        //支付金额，以分为单位，必须是正整数
                        payParam.billTotalFee = 1;

                        //商户自定义订单号
                        payParam.billNum = BillUtils.genBillNum();

                        // 设置本次订单的异步回调地址（慎重设置，以免造成丢单），不设置则使用APP设置的全局webhook地址
                        payParam.notifyUrl = "https://apihz.beecloud.cn/1/pay/webhook/receiver/c5d1cba1-5e3f-4ba0-941d-9b0a371fe719";

                        // 商家自定义的消费者Id，传入将分析用户行为
                        payParam.buyerId = "merchant-buyer-id";

                        BCPay.getInstance(ShoppingCartActivity.this).reqPaymentAsync(payParam,
                                bcCallback);

                        break;
                    }
                    case 3: //BeeCloud支付
                        loadingDialog.show();

                        BCPay.PayParams bcPayParam = new BCPay.PayParams();

                        bcPayParam.channelType = BCReqParams.BCChannelTypes.BC_APP;

                        //商品描述, 32个字节内, 汉字以2个字节计
                        bcPayParam.billTitle = "安卓BC_APP支付测试";

                        //支付金额，以分为单位，必须是正整数，BC_APP最低1元
                        bcPayParam.billTotalFee = 100;

                        //商户自定义订单号
                        bcPayParam.billNum = BillUtils.genBillNum();

                        BCPay.getInstance(ShoppingCartActivity.this).reqPaymentAsync(bcPayParam,
                                bcCallback);

                        break;
                    case 4: //通过百度钱包支付
                    {
                        loadingDialog.show();

                        Map<String, String> mapOptional = new HashMap<String, String>();
                        mapOptional = new HashMap<>();
                        mapOptional.put("goods desc", "商品详细描述");

                        Map<String, String> analysis;

                        //通过创建PayParam的方式发起支付
                        //你也可以通过reqBaiduPaymentAsync的方式支付
                        BCPay.PayParams payParam = new BCPay.PayParams();

                        /*
                        *  支付渠道，此处以百度钱包为例，实际支付允许
                        *  BCReqParams.BCChannelTypes.WX_APP，
                        *  BCReqParams.BCChannelTypes.ALI_APP，
                        *  BCReqParams.BCChannelTypes.UN_APP，
                        *  BCReqParams.BCChannelTypes.BD_APP，
                        *  BCReqParams.BCChannelTypes.PAYPAL_SANDBOX，
                        *  BCReqParams.BCChannelTypes.PAYPAL_LIVE
                        */
                        payParam.channelType = BCReqParams.BCChannelTypes.BD_APP;

                        //商品描述, 32个字节内, 汉字以2个字节计
                        payParam.billTitle = "安卓Baidu钱包支付测试";

                        //支付金额，以分为单位，必须是正整数
                        payParam.billTotalFee = 1;

                        //商户自定义订单号
                        payParam.billNum = BillUtils.genBillNum();

                        //订单超时时间，以秒为单位，建议不小于360，可以不设置
                        payParam.billTimeout = 360;

                        //扩展参数，可以传入任意数量的key/value对来补充对业务逻辑的需求，可以不设置
                        payParam.optional = mapOptional;

                        //扩展参数，用于后期分析，目前只支持key为category的分类分析，可以不设置
                        analysis = new HashMap<String, String>();
                        analysis.put("category", "BD");
                        payParam.analysis = analysis;

                        BCPay.getInstance(ShoppingCartActivity.this).reqPaymentAsync(payParam,
                                bcCallback);
                        break;
                    }
                    case 5: {//订阅支付
                        /*
                         进入订阅支付的activity
                         */
                        Intent intent = new Intent(ShoppingCartActivity.this, SubscribeActivity.class);
                        startActivity(intent);
                        break;
                    }
                    case 6: {
                        BCPay.PayParams payParam = new BCPay.PayParams();

                        // 微信WAP
                        payParam.channelType = BCReqParams.BCChannelTypes.BC_WX_WAP;

                        //商品描述, 32个字节内, 汉字以2个字节计
                        payParam.billTitle = "安卓WX_WAP支付测试";

                        //支付金额，以分为单位，必须是正整数
                        payParam.billTotalFee = 1;

                        //商户自定义订单号
                        payParam.billNum = BillUtils.genBillNum();

                        BCPay.getInstance(ShoppingCartActivity.this).reqPaymentAsync(payParam,
                                bcCallback);

                        break;
                    }
                    case 7: {
                        BCPay.PayParams payParam = new BCPay.PayParams();

                        // BeeCloud微信APP
                        payParam.channelType = BCReqParams.BCChannelTypes.BC_WX_APP;

                        //商品描述, 32个字节内, 汉字以2个字节计
                        payParam.billTitle = "安卓BC_WX_APP支付测试";

                        //支付金额，以分为单位，必须是正整数
                        payParam.billTotalFee = 1;

                        //商户自定义订单号
                        payParam.billNum = BillUtils.genBillNum();
                        BCPay.getInstance(ShoppingCartActivity.this).reqPaymentAsync(payParam, bcCallback);
                        break;
                    }
                    case 8: {
                        BCPay.PayParams payParam = new BCPay.PayParams();

                        // BeeCLoud支付宝APP
                        payParam.channelType = BCReqParams.BCChannelTypes.BC_ALI_QRCODE;

                        //商品描述, 32个字节内, 汉字以2个字节计
                        payParam.billTitle = "安卓BC_ALI_APP支付测试";

                        //支付金额，以分为单位，必须是正整数
                        payParam.billTotalFee = 2;

                        //商户自定义订单号
                        payParam.billNum = BillUtils.genBillNum();
                        BCPay.getInstance(ShoppingCartActivity.this).reqPaymentAsync(payParam, bcCallback);
                        break;
                    }
                    case 9: {
                        Intent intent = new Intent(ShoppingCartActivity.this, QRCodeEntryActivity.class);
                        startActivity(intent);
                    }
                }
            }
        });

        btnQueryOrders = (Button) findViewById(R.id.btnQueryOrders);
        btnQueryOrders.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(ShoppingCartActivity.this, OrdersEntryActivity.class);
                startActivity(intent);
            }
        });
    }

    void getBillInfoByID(String id) {

        BCQuery.getInstance().queryBillByIDAsync(id,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        BCQueryBillResult billResult = (BCQueryBillResult) result;

                        Log.d(TAG, "------ response info ------");
                        Log.d(TAG, "------getResultCode------" + billResult.getResultCode());
                        Log.d(TAG, "------getResultMsg------" + billResult.getResultMsg());
                        Log.d(TAG, "------getErrDetail------" + billResult.getErrDetail());

                        if (billResult.getResultCode() != 0)
                            return;

                        Log.d(TAG, "------- bill info ------");
                        BCBillOrder billOrder = billResult.getBill();
                        Log.d(TAG, "订单唯一标识符：" + billOrder.getId());
                        Log.d(TAG, "订单号:" + billOrder.getBillNum());
                        Log.d(TAG, "订单金额, 单位为分:" + billOrder.getTotalFee());
                        Log.d(TAG, "渠道类型:" + BCReqParams.BCChannelTypes.getTranslatedChannelName(billOrder.getChannel()));
                        Log.d(TAG, "子渠道类型:" + BCReqParams.BCChannelTypes.getTranslatedChannelName(billOrder.getSubChannel()));
                        Log.d(TAG, "订单是否成功:" + billOrder.getPayResult());

                        if (billOrder.getPayResult())
                            Log.d(TAG, "渠道返回的交易号，未支付成功时，是不含该参数的:" + billOrder.getTradeNum());
                        else
                            Log.d(TAG, "订单是否被撤销，该参数仅在线下产品（例如二维码和扫码支付）有效:"
                                    + billOrder.getRevertResult());

                        Log.d(TAG, "订单创建时间:" + new Date(billOrder.getCreatedTime()));
                        Log.d(TAG, "扩展参数:" + billOrder.getOptional());
                        Log.w(TAG, "订单是否已经退款成功(用于后期查询): " + billOrder.getRefundResult());
                        Log.w(TAG, "渠道返回的详细信息，按需处理: " + billOrder.getMessageDetail());

                    }
                });
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        //使用微信的，在initWechatPay的activity结束时detach
        BCPay.detachWechat();

        //使用百度支付的，在activity结束时detach
        BCPay.detachBaiduPay();

        //清理当前的activity引用
        BCPay.clear();
    }
}
