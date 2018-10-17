/**
 * ShoppingCartActivity.java
 *
 * Created by xuanzhui on 2015/7/29.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.demo;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ListView;
import android.widget.Toast;

import com.unionpay.UPPayAssistEx;

import java.util.Date;
import java.util.HashMap;
import java.util.Map;

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

    //支付结果返回入口
    BCCallback bcCallback = new BCCallback() {
        @Override
        public void done(final BCResult bcResult) {
            final BCPayResult bcPayResult = (BCPayResult)bcResult;
            //此处关闭loading界面
            loadingDialog.dismiss();

            //根据你自己的需求处理支付结果
            //需要注意的是，此处如果涉及到UI的更新，请在UI主进程或者Handler操作
            ShoppingCartActivity.this.runOnUiThread(new Runnable() {
                @Override
                public void run() {

                    String result = bcPayResult.getResult();

                    /*
                      注意！
                      所有支付渠道建议以服务端的状态金额为准，此处返回的RESULT_SUCCESS仅仅代表手机端支付成功
                    */
                    if (result.equals(BCPayResult.RESULT_SUCCESS)) {
                        Toast.makeText(ShoppingCartActivity.this, "用户支付成功", Toast.LENGTH_LONG).show();

                    } else if (result.equals(BCPayResult.RESULT_CANCEL))
                        Toast.makeText(ShoppingCartActivity.this, "用户取消支付", Toast.LENGTH_LONG).show();
                    else if (result.equals(BCPayResult.RESULT_FAIL)) {
                        String toastMsg = "支付失败, 原因: " + bcPayResult.getErrCode() +
                                " # " + bcPayResult.getErrMsg() +
                                " # " + bcPayResult.getDetailInfo();

                        /**
                         * 你发布的项目中不应该出现如下错误，此处由于支付宝政策原因，
                         * 不再提供支付宝支付的测试功能，所以给出提示说明
                         */
                        if (bcPayResult.getErrMsg().equals("PAY_FACTOR_NOT_SET") &&
                                bcPayResult.getDetailInfo().startsWith("支付宝参数")) {
                            toastMsg = "支付失败：由于支付宝政策原因，故不再提供支付宝支付的测试功能，给您带来的不便，敬请谅解";
                        }

                        /**
                         * 以下是正常流程，请按需处理失败信息
                         */

                        Toast.makeText(ShoppingCartActivity.this, toastMsg, Toast.LENGTH_LONG).show();
                        Log.e(TAG, toastMsg);

                        if (bcPayResult.getErrMsg().equals(BCPayResult.FAIL_PLUGIN_NOT_INSTALLED)) {
                            //银联需要重新安装控件
                            Message msg = mHandler.obtainMessage();
                            msg.what = 1;
                            mHandler.sendMessage(msg);
                        }

                    } else if (result.equals(BCPayResult.RESULT_UNKNOWN)) {
                        //可能出现在支付宝8000返回状态
                        Toast.makeText(ShoppingCartActivity.this, "订单状态未知", Toast.LENGTH_LONG).show();
                    } else {
                        Toast.makeText(ShoppingCartActivity.this, "invalid return", Toast.LENGTH_LONG).show();
                    }

                    /*
                    if (bcPayResult.getId() != null) {
                        //你可以把这个id存到你的订单中，下次直接通过这个id查询订单
                        Log.w(TAG, "bill id retrieved : " + bcPayResult.getId());

                        //根据ID查询，此处只是演示如何通过id查询订单，并非支付必要部分
                        getBillInfoByID(bcPayResult.getId());
                    }
                    */
                }
            });
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
            if (msg.what == 1) {
                //如果用户手机没有安装银联支付控件,则会提示用户安装
                AlertDialog.Builder builder = new AlertDialog.Builder(ShoppingCartActivity.this);
                builder.setTitle("提示");
                builder.setMessage("完成支付需要安装或者升级银联支付控件，是否安装？");

                builder.setNegativeButton("确定",
                        new DialogInterface.OnClickListener() {
                            @Override
                            public void onClick(DialogInterface dialog, int which) {
                                UPPayAssistEx.installUPPayPlugin(ShoppingCartActivity.this);
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
            return true;
        }
    });

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_shopping_cart);

        // 推荐在主Activity或application里的onCreate函数中初始化BeeCloud.
        BeeCloud.setSandbox(true);
        BeeCloud.setAppIdAndSecret("c5d1cba1-5e3f-4ba0-941d-9b0a371fe719",
                "4bfdd244-574d-4bf3-b034-0c751ed34fee");

        // 如果用到微信支付，在用到微信支付的Activity的onCreate函数里调用以下函数.
        // 第二个参数需要换成你自己的微信AppID.
        String initInfo = BCPay.initWechatPay(ShoppingCartActivity.this, "wxf1aa465362b4c8f1");
        if (initInfo != null) {
            Toast.makeText(this, "微信初始化失败：" + initInfo, Toast.LENGTH_LONG).show();
        }

        payMethod = (ListView) this.findViewById(R.id.payMethod);
        Integer[] payIcons = new Integer[]{R.drawable.wechat, R.drawable.alipay,
                R.drawable.unionpay, R.drawable.baidupay, R.drawable.scan};
        final String[] payNames = new String[]{"微信支付", "支付宝支付",
                "银联在线", "百度钱包", "二维码支付"};
        String[] payDescs = new String[]{"使用微信支付，以人民币CNY计费", "使用支付宝支付，以人民币CNY计费",
                "使用银联在线支付，以人民币CNY计费", "使用百度钱包支付，以人民币CNY计费",
                "通过扫描二维码支付"};
        PayMethodListItem adapter = new PayMethodListItem(this, payIcons, payNames, payDescs);
        payMethod.setAdapter(adapter);

        // 如果调起支付太慢, 可以在这里开启动画, 以progressdialog为例
        loadingDialog = new ProgressDialog(ShoppingCartActivity.this);
        loadingDialog.setMessage("启动第三方支付，请稍候...");
        loadingDialog.setIndeterminate(true);
        loadingDialog.setCancelable(true);

        payMethod.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                switch (position) {

                    case 0: //微信
                        loadingDialog.show();
                        //对于微信支付, 手机内存太小会有OutOfResourcesException造成的卡顿, 以致无法完成支付
                        //这个是微信自身存在的问题
                        Map<String, String> mapOptional = new HashMap<String, String>();

                        mapOptional.put("testkey1", "测试value值1");

                        if (BCPay.isWXAppInstalledAndSupported() &&
                                BCPay.isWXPaySupported()) {

                            BCPay.getInstance(ShoppingCartActivity.this).reqWXPaymentAsync(
                                    "安卓微信支付测试",               //订单标题
                                    1,                           //订单金额(分)
                                    BillUtils.genBillNum(),  //订单流水号
                                    mapOptional,            //扩展参数(可以null)
                                    bcCallback);            //支付完成后回调入口

                        } else {
                            Toast.makeText(ShoppingCartActivity.this,
                                    "您尚未安装微信或者安装的微信版本不支持", Toast.LENGTH_LONG).show();
                            loadingDialog.dismiss();
                        }
                        break;

                    case 1: //支付宝支付
                        loadingDialog.show();

                        mapOptional = new HashMap<String, String>();
                        mapOptional.put("客户端", "安卓");
                        mapOptional.put("consumptioncode", "consumptionCode");
                        mapOptional.put("money", "2");

                        BCPay.getInstance(ShoppingCartActivity.this).reqAliPaymentAsync(
                                "安卓支付宝支付测试",
                                1,
                                BillUtils.genBillNum(),
                                mapOptional,
                                bcCallback);

                        break;

                    case 2: //银联支付
                        loadingDialog.show();

                        /*  你可以通过如下方法发起支付，或者PayParams的方式
                        BCPay.getInstance(ShoppingCartActivity.this).reqUnionPaymentAsync("银联支付测试",
                                1,
                                BillUtils.genBillNum(),
                                null,
                                bcCallback);*/


                        BCPay.PayParams payParam = new BCPay.PayParams();

                        payParam.channelType = BCReqParams.BCChannelTypes.UN_APP;

                        //商品描述, 32个字节内, 汉字以2个字节计
                        payParam.billTitle = "安卓银联支付测试";

                        //支付金额，以分为单位，必须是正整数
                        payParam.billTotalFee = 1;

                        //商户自定义订单号
                        payParam.billNum = BillUtils.genBillNum();

                        BCPay.getInstance(ShoppingCartActivity.this).reqPaymentAsync(payParam,
                                bcCallback);


                        break;
                    case 3: //通过百度钱包支付
                        loadingDialog.show();

                        mapOptional = new HashMap<String, String>();
                        mapOptional.put("goods desc", "商品详细描述");

                        Map<String, String> analysis;

                        //通过创建PayParam的方式发起支付
                        //你也可以通过reqBaiduPaymentAsync的方式支付
                        //BCPay.PayParam
                        payParam = new BCPay.PayParams();
                        /*
                        *  支付渠道，此处以百度钱包为例，实际支付允许
                        *  BCReqParams.BCChannelTypes.WX_APP，
                        *  BCReqParams.BCChannelTypes.ALI_APP，
                        *  BCReqParams.BCChannelTypes.UN_APP，
                        *  BCReqParams.BCChannelTypes.BD_APP
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
                    case 4:
                        Intent intent = new Intent(ShoppingCartActivity.this, QRCodeEntryActivity.class);
                        startActivity(intent);
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
