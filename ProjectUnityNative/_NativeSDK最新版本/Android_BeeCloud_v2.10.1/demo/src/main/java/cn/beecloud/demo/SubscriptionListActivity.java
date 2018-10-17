package cn.beecloud.demo;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.List;

import cn.beecloud.BCPay;
import cn.beecloud.BCQuery;
import cn.beecloud.async.BCCallback;
import cn.beecloud.async.BCResult;
import cn.beecloud.demo.util.DisplayUtils;
import cn.beecloud.entity.BCObjectIdResult;
import cn.beecloud.entity.BCSubscription;
import cn.beecloud.entity.BCSubscriptionCriteria;
import cn.beecloud.entity.BCSubscriptionListResult;

public class SubscriptionListActivity extends Activity {
    public static final String TAG = "SubscriptionsActivity";

    ListView listView;
    SubscriptionAdapter adapter;

    String errMsg;

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
            loadingDialog.dismiss();
            if (msg.what == 1) {
                adapter.setSubscriptions(subscriptions);
                adapter.notifyDataSetChanged();
            } else {
                Toast.makeText(SubscriptionListActivity.this, errMsg, Toast.LENGTH_SHORT).show();
            }
            return true;
        }
    });
    private List<BCSubscription> subscriptions;

    private ProgressDialog loadingDialog;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_subscription_list);

        DisplayUtils.initBack(this);

        listView = (ListView) findViewById(R.id.listView);

        loadingDialog = new ProgressDialog(SubscriptionListActivity.this);
        loadingDialog.setMessage("正在请求服务器, 请稍候...");
        loadingDialog.setIndeterminate(true);
        loadingDialog.setCancelable(true);

        DisplayUtils.initBack(this);

        subscriptions = new ArrayList<>();
        adapter = new SubscriptionAdapter(
                SubscriptionListActivity.this, subscriptions);
        listView.setAdapter(adapter);

        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, final int position, long id) {
                final BCSubscription subscription = subscriptions.get(position);

                /*
                if (!subscription.isValid()) {
                    Log.w(TAG, "你可以先判断订阅是否处于有效状态再取消订阅");
                    return;
                }*/

                AlertDialog.Builder builder = new AlertDialog.Builder(SubscriptionListActivity.this);
                builder.setTitle("提示");
                builder.setMessage("请确认您将取消订阅支付");

                builder.setNegativeButton("确定",
                        new DialogInterface.OnClickListener() {
                            @Override
                            public void onClick(DialogInterface dialog, int which) {
                                dialog.dismiss();

                                loadingDialog.show();

                                BCPay.getInstance(SubscriptionListActivity.this)
                                        .cancelSubscription(subscription.getId(), new BCCallback() {
                                    @Override
                                    public void done(BCResult result) {
                                        BCObjectIdResult idResult = (BCObjectIdResult) result;
                                        Message message = mHandler.obtainMessage();
                                        if (idResult.getResultCode() == 0) {
                                            Log.w(TAG, "取消订阅成功，唯一标识符：" + idResult.getId());
                                            errMsg = "取消订阅成功";
                                        } else {
                                            errMsg = idResult.getResultCode() + " # " +
                                                    idResult.getResultMsg() + " # " +
                                                    idResult.getErrDetail();
                                        }
                                        message.what = 2;
                                        mHandler.sendMessage(message);
                                    }
                                });
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
        });

        reqSubscriptions();
    }

    void reqSubscriptions() {
        loadingDialog.show();

        // 参照API添加查询通用限制条件
        BCSubscriptionCriteria criteria = new BCSubscriptionCriteria();
        // 比如限制最多返回10条记录
        criteria.setLimit(10);
        // 比如只返回订阅用户id是ohmy的记录
        //criteria.setBuyerId("ohmy");

        BCQuery.getInstance().querySubscriptions(criteria, new BCCallback() {
            @Override
            public void done(BCResult result) {
                BCSubscriptionListResult listResult = (BCSubscriptionListResult) result;
                Message message = mHandler.obtainMessage();

                if (listResult.getResultCode() == 0) {
                    subscriptions = listResult.getSubscriptions();
                    message.what = 1;
                } else {
                    errMsg = listResult.getResultCode() + " # " +
                            listResult.getResultMsg() + " # " +
                            listResult.getErrDetail();
                    message.what = 2;
                }
                mHandler.sendMessage(message);
            }
        });
    }

    protected void onDestroy() {
        super.onDestroy();
        BCPay.clear();
    }
}
