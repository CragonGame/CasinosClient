/**
 * RefundOrdersActivity.java
 *
 * Created by xuanzhui on 2015/8/5.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.demo;

import android.app.Activity;
import android.app.ProgressDialog;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.Toast;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

import cn.beecloud.BCQuery;
import cn.beecloud.async.BCCallback;
import cn.beecloud.async.BCResult;
import cn.beecloud.demo.util.DisplayUtils;
import cn.beecloud.entity.BCQueryCountResult;
import cn.beecloud.entity.BCQueryRefundResult;
import cn.beecloud.entity.BCQueryRefundsResult;
import cn.beecloud.entity.BCRefundOrder;
import cn.beecloud.entity.BCReqParams;

/**
 * 用于展示退款订单查询
 */
public class RefundOrdersActivity extends Activity {

    public static final String TAG = "RefundOrdersActivity";

    Spinner channelChooser;
    ListView listViewRefundOrder;

    private List<BCRefundOrder> refundOrders;

    private ProgressDialog loadingDialog;

    private Handler mHandler = new Handler(new Handler.Callback() {
        /**
         * Callback interface you can use when instantiating a Handler to
         * avoid having to implement your own subclass of Handler.
         *
         * handleMessage() defines the operations to perform when the
         * Handler receives a new Message to process.
         */
        @Override
        public boolean handleMessage(Message msg) {
            if (msg.what == 1) {

                RefundOrdersAdapter adapter = new RefundOrdersAdapter(
                        RefundOrdersActivity.this, refundOrders);
                listViewRefundOrder.setAdapter(adapter);
            }
            return true;
        }
    });

    //回调入口
    final BCCallback bcCallback = new BCCallback() {
        @Override
        public void done(BCResult bcResult) {

            //此处关闭loading界面
            loadingDialog.dismiss();

            final BCQueryRefundsResult bcQueryRefundsResult = (BCQueryRefundsResult) bcResult;

            //resultCode为0表示请求成功
            //count包含返回的订单个数
            if (bcQueryRefundsResult.getResultCode() == 0) {

                //订单列表
                refundOrders = bcQueryRefundsResult.getRefunds();

                Log.i(BillListActivity.TAG, "bill count: " + bcQueryRefundsResult.getCount());

            } else {
                //订单列表
                refundOrders = null;

                RefundOrdersActivity.this.runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        Toast.makeText(RefundOrdersActivity.this, "err code:" + bcQueryRefundsResult.getResultCode() +
                                "; err msg: " + bcQueryRefundsResult.getResultMsg() +
                                "; err detail: " + bcQueryRefundsResult.getErrDetail(), Toast.LENGTH_LONG).show();
                    }
                });

            }

            Message msg = mHandler.obtainMessage();
            msg.what = 1;
            mHandler.sendMessage(msg);
        }
    };

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_refund_orders);

        DisplayUtils.initBack(this);

        loadingDialog = new ProgressDialog(RefundOrdersActivity.this);
        loadingDialog.setMessage("正在请求服务器, 请稍候...");
        loadingDialog.setIndeterminate(true);
        loadingDialog.setCancelable(true);

        listViewRefundOrder = (ListView) findViewById(R.id.listViewRefundOrder);

        initSpinner();
    }


    void initSpinner() {
        channelChooser = (Spinner) this.findViewById(R.id.channelChooser);

        ArrayAdapter<String> adapter = new ArrayAdapter<String>(
                this,android.R.layout.simple_spinner_item,
                new String[]{"微信", "支付宝", "银联", "BeeCloud", "百度", "PayPal", "全渠道", "通过ID查询", "退款订单总数"});
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        channelChooser.setAdapter(adapter);

        channelChooser.setSelected(false);

        channelChooser.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                // 如果调起支付太慢，可以在这里开启动画，表示正在loading
                //以progressdialog为例
                loadingDialog.show();

                SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm");

                Date startTime, endTime;
                try {
                    startTime = sdf.parse("2015-10-01 00:00");
                    endTime = sdf.parse("2015-10-31 23:59");
                } catch (ParseException e) {
                    startTime = new Date();
                    endTime = new Date();
                    e.printStackTrace();
                }

                BCQuery.QueryParams params;

                switch (position) {
                    case 0: //微信
                        BCQuery.getInstance().queryRefundsAsync(
                                BCReqParams.BCChannelTypes.WX,  //此处表示微信支付的查询
                                bcCallback);
                        break;
                    case 1: //支付宝
                        BCQuery.getInstance().queryRefundsAsync(
                                BCReqParams.BCChannelTypes.ALI, //渠道，此处表示ALI客户端渠道
                                "883dafee43b54c68a1dc7cf24f705463",     //支付订单号
                                "20150812436857",                     //商户退款流水号
                                bcCallback);
                        break;
                    case 2: //银联

                        BCQuery.getInstance().queryRefundsAsync(
                                BCReqParams.BCChannelTypes.UN_APP,          //渠道, 此处表示银联手机APP客户端支付
                                null,                                   //支付订单号
                                null,                                   //商户退款流水号
                                startTime.getTime(),                    //起始时间
                                endTime.getTime(),                      //结束时间
                                2,                                      //跳过满足条件的前2条数据
                                15,                                     //最多返回满足条件的15条数据
                                bcCallback);
                        break;
                    case 3: //BeeCloud
                        params = new BCQuery.QueryParams();
                        params.channel = BCReqParams.BCChannelTypes.BC;

                        BCQuery.getInstance().queryRefundsAsync(params,
                                bcCallback);

                        break;
                    case 4: //百度
                        //以下演示通过PayParams发起请求
                        params = new BCQuery.QueryParams();
                        params.channel = BCReqParams.BCChannelTypes.BD;

                        //以下参数按需设置，都是‘且’的关系

                        //限制支付订单号
                        //params.billNum = null;

                        //限制退款订单号
                        //params.refundNum = null;

                        //限制起始时间
                        params.startTime = startTime.getTime();

                        //限制结束时间
                        params.endTime = endTime.getTime();

                        //跳过满足条件的数目
                        params.skip = 10;

                        //最多返回的数目
                        params.limit = 20;

                        //是否获取渠道返回的信息信息
                        params.needDetail = true;

                        BCQuery.getInstance().queryRefundsAsync(params,
                                bcCallback);

                        break;
                    case 5: //PayPal
                        params = new BCQuery.QueryParams();
                        params.channel = BCReqParams.BCChannelTypes.PAYPAL;
                        BCQuery.getInstance().queryRefundsAsync(params,
                                bcCallback);

                        break;
                    case 6: //全部的渠道类型
                        params = new BCQuery.QueryParams();
                        params.channel = BCReqParams.BCChannelTypes.ALL;
                        BCQuery.getInstance().queryRefundsAsync(params,
                                bcCallback);
                        break;
                    case 7: //通过ID查询
                        //注意此处的ID是列标识符，在调用退款(服务端)接口时返回，非订单号
                        BCQuery.getInstance().queryRefundByIDAsync("24035a33-6be2-4d55-b37c-84aaf2c5aeac",
                                new BCCallback() {
                                    @Override
                                    public void done(BCResult result) {
                                        loadingDialog.dismiss();

                                        final BCQueryRefundResult refundResult = (BCQueryRefundResult) result;

                                        //resultCode为0表示请求成功
                                        //count包含返回的订单个数
                                        if (refundResult.getResultCode() == 0) {

                                            //订单列表
                                            refundOrders.clear();

                                            //refundResult.getRefund() returns BCRefundOrder object
                                            refundOrders.add(refundResult.getRefund());

                                        } else {
                                            //订单列表
                                            refundOrders = null;

                                            RefundOrdersActivity.this.runOnUiThread(new Runnable() {
                                                @Override
                                                public void run() {
                                                    Toast.makeText(RefundOrdersActivity.this,
                                                            "err code:" + refundResult.getResultCode() +
                                                            "; err msg: " + refundResult.getResultMsg() +
                                                            "; err detail: " + refundResult.getErrDetail(), Toast.LENGTH_LONG).show();
                                                }
                                            });

                                        }

                                        Message msg = mHandler.obtainMessage();
                                        msg.what = 1;
                                        mHandler.sendMessage(msg);
                                    }
                                });
                        break;
                    case 8:
                        params = new BCQuery.QueryParams();

                        //以下为可用的限制参数
                        //渠道类型
                        params.channel = BCReqParams.BCChannelTypes.ALL;

                        //支付单号
                        //params.billNum = "your bill number";
                        //退款单号
                        //params.refundNum = "your refund number";

                        //限制起始时间
                        params.startTime = startTime.getTime();

                        //限制结束时间
                        params.endTime = endTime.getTime();

                        BCQuery.getInstance().queryRefundsCountAsync(params, new BCCallback() {
                            @Override
                            public void done(BCResult result) {
                                if (loadingDialog.isShowing())
                                    loadingDialog.dismiss();

                                final BCQueryCountResult countResult = (BCQueryCountResult) result;

                                if (countResult.getResultCode() == 0) {
                                    //显示获取到的订单总数
                                    RefundOrdersActivity.this.runOnUiThread(new Runnable() {
                                        @Override
                                        public void run() {
                                            Toast.makeText(RefundOrdersActivity.this,
                                                    "退款订单总数:" + countResult.getCount(),
                                                    Toast.LENGTH_LONG).show();
                                        }
                                    });
                                } else {
                                    RefundOrdersActivity.this.runOnUiThread(new Runnable() {
                                        @Override
                                        public void run() {
                                            Toast.makeText(RefundOrdersActivity.this,
                                                    "err code:" + countResult.getResultCode() +
                                                            "; err msg: " + countResult.getResultMsg() +
                                                            "; err detail: " + countResult.getErrDetail(),
                                                    Toast.LENGTH_LONG).show();
                                        }
                                    });

                                }
                            }
                        });
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });
    }
}
