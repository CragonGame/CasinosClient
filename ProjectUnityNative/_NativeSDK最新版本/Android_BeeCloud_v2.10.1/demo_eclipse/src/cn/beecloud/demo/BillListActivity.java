/**
 * BillListActivity.java
 *
 * Created by xuanzhui on 2015/7/29.
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
import java.util.Locale;

import cn.beecloud.BCQuery;
import cn.beecloud.async.BCCallback;
import cn.beecloud.async.BCResult;
import cn.beecloud.demo.util.DisplayUtils;
import cn.beecloud.entity.BCBillOrder;
import cn.beecloud.entity.BCQueryBillsResult;
import cn.beecloud.entity.BCQueryCountResult;
import cn.beecloud.entity.BCReqParams;

/**
 * 用于展示订单查询
 */
public class BillListActivity extends Activity {
    public static final String TAG = "BillListActivity";

    Spinner channelChooser;
    ListView listViewOrder;

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

                BillListAdapter adapter = new BillListAdapter(
                        BillListActivity.this, bills);
                listViewOrder.setAdapter(adapter);
            }
            return true;
        }
    });
    private List<BCBillOrder> bills;

    private ProgressDialog loadingDialog;

    //回调入口
    final BCCallback bcCallback = new BCCallback() {
        @Override
        public void done(BCResult bcResult) {

            //此处关闭loading界面
            loadingDialog.dismiss();

            final BCQueryBillsResult bcQueryResult = (BCQueryBillsResult) bcResult;

            //resultCode为0表示请求成功
            //count包含返回的订单个数
            if (bcQueryResult.getResultCode() == 0) {

                //订单列表
                bills = bcQueryResult.getBills();

                Log.i(BillListActivity.TAG, "bill count: " + bcQueryResult.getCount());

            } else {
                //订单列表
                bills = null;

                BillListActivity.this.runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        Toast.makeText(BillListActivity.this, "err code:" + bcQueryResult.getResultCode() +
                                "; err msg: " + bcQueryResult.getResultMsg() +
                                "; err detail: " + bcQueryResult.getErrDetail(), Toast.LENGTH_LONG).show();
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
        setContentView(R.layout.activity_bill_list);

        loadingDialog = new ProgressDialog(BillListActivity.this);
        loadingDialog.setMessage("正在请求服务器, 请稍候...");
        loadingDialog.setIndeterminate(true);
        loadingDialog.setCancelable(true);

        DisplayUtils.initBack(this);

        listViewOrder = (ListView) findViewById(R.id.listViewOrder);

        initSpinner();
    }

    void initSpinner() {
        channelChooser = (Spinner) this.findViewById(R.id.channelChooser);

        ArrayAdapter<String> adapter = new ArrayAdapter<String>(
                this,android.R.layout.simple_spinner_item,
                new String[]{"微信", "支付宝", "银联", "百度", "PayPal", "全渠道", "订单总数"});
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        channelChooser.setAdapter(adapter);

        channelChooser.setSelected(false);

        channelChooser.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                // 如果调起支付太慢，可以在这里开启动画，表示正在loading
                //以progressdialog为例
                loadingDialog.show();

                SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm", Locale.CHINA);

                Date startTime;
                //Date endTime;
                try {
                    startTime = sdf.parse("2015-10-01 00:00");
                    //endTime = sdf.parse("2015-12-01 23:59");
                } catch (ParseException e) {
                    startTime = new Date();
                    //endTime = new Date();
                    e.printStackTrace();
                }

                BCQuery.QueryParams params;

                switch (position) {
                    case 0: //微信
                        BCQuery.getInstance().queryBillsAsync(
                                BCReqParams.BCChannelTypes.WX,  //此处表示微信支付的查询
                                bcCallback);
                        break;
                    case 1: //支付宝
                        BCQuery.getInstance().queryBillsAsync(
                                BCReqParams.BCChannelTypes.ALI_APP, //渠道，此处表示ALI客户端渠道
                                //"20150820102712150", //此处表示限制订单号
                                bcCallback);
                        break;
                    case 2: //银联

                        BCQuery.getInstance().queryBillsAsync(
                                BCReqParams.BCChannelTypes.UN_APP,          //渠道, 此处表示银联手机APP客户端支付
                                null,                                   //订单号
                                startTime.getTime(),                    //起始时间
                                null,                                   //结束时间
                                2,                                      //跳过满足条件的前2条数据
                                15,                                     //最多返回满足条件的15条数据
                                bcCallback);
                        break;
                    case 3: //百度
                        //以下演示通过PayParams发起请求
                        params = new BCQuery.QueryParams();
                        params.channel = BCReqParams.BCChannelTypes.BD;

                        //以下参数按需设置，都是‘且’的关系

                        //限制支付订单号
                        //params.billNum = null;

                        //只返回成功的订单
                        params.payResult = Boolean.TRUE;

                        //限制起始时间
                        params.startTime = startTime.getTime();

                        //限制结束时间
                        //params.endTime = endTime.getTime();

                        //跳过满足条件的数目
                        //params.skip = 10;

                        //最多返回的数目
                        params.limit = 20;

                        //是否获取渠道返回的详细信息
                        params.needDetail = true;

                        BCQuery.getInstance().queryBillsAsync(params,
                                bcCallback);

                        break;
                    case 4: //PayPal
                        params = new BCQuery.QueryParams();
                        params.channel = BCReqParams.BCChannelTypes.PAYPAL;
                        BCQuery.getInstance().queryBillsAsync(params,
                                bcCallback);

                        break;
                    case 5: //全部的渠道类型
                        params = new BCQuery.QueryParams();
                        params.channel = BCReqParams.BCChannelTypes.ALL;

                        //跳过满足条件的数目
                        params.skip = 10;

                        //最多返回的数目
                        params.limit = 20;

                        BCQuery.getInstance().queryBillsAsync(params,
                                bcCallback);
                        break;

                    case 6:
                        params = new BCQuery.QueryParams();

                        //以下为可用的限制参数
                        //渠道类型
                        params.channel = BCReqParams.BCChannelTypes.ALL;

                        //支付单号
                        //params.billNum = "your bill number";

                        //订单是否支付成功
                        params.payResult = Boolean.TRUE;

                        //限制起始时间
                        params.startTime = startTime.getTime();

                        //限制结束时间
                        //params.endTime = endTime.getTime();

                        BCQuery.getInstance().queryBillsCountAsync(params, new BCCallback() {
                            @Override
                            public void done(BCResult result) {
                                if (loadingDialog.isShowing())
                                    loadingDialog.dismiss();

                                final BCQueryCountResult countResult = (BCQueryCountResult) result;

                                if (countResult.getResultCode() == 0) {
                                    //显示获取到的订单总数
                                    BillListActivity.this.runOnUiThread(new Runnable() {
                                        @Override
                                        public void run() {
                                            Toast.makeText(BillListActivity.this,
                                                    "订单总数:" + countResult.getCount(),
                                                    Toast.LENGTH_LONG).show();
                                        }
                                    });
                                } else {
                                    BillListActivity.this.runOnUiThread(new Runnable() {
                                        @Override
                                        public void run() {
                                            Toast.makeText(BillListActivity.this,
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