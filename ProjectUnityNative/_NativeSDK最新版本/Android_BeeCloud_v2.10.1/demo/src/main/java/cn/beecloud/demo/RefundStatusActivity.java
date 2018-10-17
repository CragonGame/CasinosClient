/**
 * RefundStatusActivity.java
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
import android.widget.TextView;
import android.widget.Toast;

import cn.beecloud.BCQuery;
import cn.beecloud.async.BCCallback;
import cn.beecloud.async.BCResult;
import cn.beecloud.demo.util.DisplayUtils;
import cn.beecloud.entity.BCRefundStatus;
import cn.beecloud.entity.BCReqParams;

/**
 * 用于展示退款状态
 */
public class RefundStatusActivity extends Activity {
    public static final String TAG = "BillListActivity";

    TextView txtRefundStatus;

    private ProgressDialog loadingDialog;
    private Handler mHandler;
    private String refundStatus;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_refund_status);

        DisplayUtils.initBack(this);

        txtRefundStatus = (TextView) findViewById(R.id.txtRefundStatus);

        loadingDialog = new ProgressDialog(RefundStatusActivity.this);
        loadingDialog.setMessage("正在请求服务器, 请稍候...");
        loadingDialog.setIndeterminate(true);
        loadingDialog.setCancelable(true);

        // Defines a Handler object that's attached to the UI thread.
        // 通过Handler.Callback()可消除内存泄漏警告
        mHandler = new Handler(new Handler.Callback() {
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

                    txtRefundStatus.setText(
                            BCRefundStatus.RefundStatus.getTranslatedRefundStatus(refundStatus));
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

                final BCRefundStatus bcQueryResult = (BCRefundStatus) bcResult;

                //resultCode为0表示请求成功
                if (bcQueryResult.getResultCode() == 0) {

                    //返回的退款信息
                    refundStatus = bcQueryResult.getRefundStatus();

                    if (refundStatus == null){

                        RefundStatusActivity.this.runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                Toast.makeText(RefundStatusActivity.this, "没有查询到相关信息", Toast.LENGTH_LONG).show();
                            }
                        });

                    }else{
                        Message msg = mHandler.obtainMessage();
                        msg.what = 1;
                        mHandler.sendMessage(msg);
                    }

                } else {

                    RefundStatusActivity.this.runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            Toast.makeText(RefundStatusActivity.this, "err code:" + bcQueryResult.getResultCode() +
                                    "; err msg: " + bcQueryResult.getResultMsg() +
                                    "; err detail: " + bcQueryResult.getErrDetail(), Toast.LENGTH_LONG).show();
                        }
                    });

                }

            }
        };

        loadingDialog.show();
        BCQuery.getInstance().queryRefundStatusAsync(
                BCReqParams.BCChannelTypes.WX,     //目前仅支持WX、YEE、KUAIQIAN、BD
                "20150812436857",                   //必须是微信的退款单号
                bcCallback);                            //回调入口
    }

}
