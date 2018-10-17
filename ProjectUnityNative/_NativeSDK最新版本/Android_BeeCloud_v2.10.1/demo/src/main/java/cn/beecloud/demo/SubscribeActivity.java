package cn.beecloud.demo;

import android.app.Activity;
import android.content.Context;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.List;

import cn.beecloud.BCPay;
import cn.beecloud.BCQuery;
import cn.beecloud.async.BCCallback;
import cn.beecloud.async.BCResult;
import cn.beecloud.demo.util.DisplayUtils;
import cn.beecloud.entity.BCPlan;
import cn.beecloud.entity.BCPlanCriteria;
import cn.beecloud.entity.BCPlanListResult;
import cn.beecloud.entity.BCSmsResult;
import cn.beecloud.entity.BCSubscription;
import cn.beecloud.entity.BCSubscriptionBanksResult;
import cn.beecloud.entity.BCSubscriptionResult;

public class SubscribeActivity extends Activity {
    private static final String TAG = "SubscribeActivity";

    InputMethodManager inputMethodManager;

    String errMsg;

    Spinner planSpinner;
    ArrayAdapter planAdapter;
    List<String> planSpinnerData;
    List<BCPlan> planList;
    String selectPlanId;

    Spinner bankSpinner;
    ArrayAdapter bankAdapter;
    List<String> bankList;
    String selectBankName;

    EditText buyerIdView;
    EditText cardNoView;
    EditText idNameView;
    EditText idNoView;
    EditText mobileView;
    EditText smsCodeView;

    String smsId;

    private Handler mHandler = new Handler(new Handler.Callback() {
        @Override
        public boolean handleMessage(Message msg) {
            if (msg.what == 1) {
                planAdapter.notifyDataSetChanged();
            } else if (msg.what == 2) {
                bankAdapter.notifyDataSetChanged();
            } else if (msg.what == 3) {
                Toast.makeText(SubscribeActivity.this, "发送成功", Toast.LENGTH_SHORT).show();
            } else if (msg.what == 4) {
                Toast.makeText(SubscribeActivity.this, "订阅请求成功，请注意接收webhook推送的审核结果", Toast.LENGTH_SHORT).show();
            } else if (msg.what == 99) {
                Toast.makeText(SubscribeActivity.this, errMsg, Toast.LENGTH_SHORT).show();
            }
            return true;
        }
    });

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_subscribe);
        DisplayUtils.initBack(this);

        inputMethodManager = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);

        buyerIdView = (EditText) findViewById(R.id.buyer_id_view);
        cardNoView = (EditText) findViewById(R.id.card_no_view);
        idNameView = (EditText) findViewById(R.id.id_name_view);
        idNoView = (EditText) findViewById(R.id.id_no_view);
        mobileView = (EditText) findViewById(R.id.mobile_view);
        smsCodeView = (EditText) findViewById(R.id.sms_view);

        planSpinner = (Spinner) findViewById(R.id.plan_id_view);
        planSpinnerData = new ArrayList<>();
        planAdapter = new ArrayAdapter<>(this,android.R.layout.simple_spinner_item, planSpinnerData);
        planAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        planSpinner.setAdapter(planAdapter);
        planSpinner.setOnItemSelectedListener(new Spinner.OnItemSelectedListener(){
            public void onItemSelected(AdapterView<?> arg0, View arg1, int position, long arg3) {
                selectPlanId = planList.get(position).getId();
                Log.w(TAG, "plan id: " + selectPlanId);
            }

            public void onNothingSelected(AdapterView<?> arg0) {
            }
        });

        bankSpinner = (Spinner) findViewById(R.id.bank_view);
        bankList = new ArrayList<>();
        bankAdapter = new ArrayAdapter<>(this,android.R.layout.simple_spinner_item, bankList);
        bankAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        bankSpinner.setAdapter(bankAdapter);
        bankSpinner.setOnItemSelectedListener(new Spinner.OnItemSelectedListener(){
            public void onItemSelected(AdapterView<?> arg0, View arg1, int position, long arg3) {
                selectBankName = bankList.get(position);
                Log.w(TAG, "bank name: " + selectBankName);
            }

            public void onNothingSelected(AdapterView<?> arg0) {
            }
        });

        reqPlans();
        reqBanks();
    }

    void reqPlans() {
        // 参照API添加查询通用限制条件
        BCPlanCriteria criteria = new BCPlanCriteria();
        // 比如限制最多返回8条记录
        criteria.setLimit(8);
        // 比如只返回计划名包含"plan"的
        criteria.setNameWithSubstring("plan");

        BCQuery.getInstance().queryPlans(criteria,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        BCPlanListResult listResult = (BCPlanListResult) result;
                        Message msg = mHandler.obtainMessage();

                        if (listResult.getResultCode() != 0) {
                            Log.e(TAG, listResult.getResultMsg());
                            Log.e(TAG, listResult.getErrDetail());
                            msg.what = 99;
                            errMsg = listResult.getResultMsg();
                        } else {
                            msg.what = 1;
                            planList = listResult.getPlans();
                            planSpinnerData.clear();
                            for (BCPlan plan : planList) {
                                if (plan.isValid())
                                    planSpinnerData.add(plan.getName());
                            }
                        }

                        mHandler.sendMessage(msg);
                    }
                });
    }

    void reqBanks() {
        BCQuery.getInstance().subscriptionSupportedBanks(
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        BCSubscriptionBanksResult banksResult = (BCSubscriptionBanksResult) result;
                        Message msg = mHandler.obtainMessage();

                        if (banksResult.getResultCode() != 0) {
                            Log.e(TAG, banksResult.getResultMsg());
                            Log.e(TAG, banksResult.getErrDetail());
                            msg.what = 99;
                            errMsg = banksResult.getResultMsg();
                        } else {
                            msg.what = 2;

                            bankList.clear();

                            for (String s : banksResult.getCommonBanks())
                                bankList.add(s);

                            Log.w(TAG, "更详细的banks：" + banksResult.getBanks());
                        }

                        mHandler.sendMessage(msg);
                    }
                });
    }

    @Override
    public boolean onTouchEvent(MotionEvent event) {
        if(event.getAction() == MotionEvent.ACTION_DOWN &&
                getCurrentFocus()!=null &&
                getCurrentFocus().getWindowToken()!=null){

            inputMethodManager.hideSoftInputFromWindow(
                    getCurrentFocus().getWindowToken(), InputMethodManager.HIDE_NOT_ALWAYS);
        }
        return super.onTouchEvent(event);
    }

    protected void onDestroy() {
        super.onDestroy();
        BCPay.clear();
    }

    public void sendSms(View v) {
        BCPay.getInstance(this).sendSmsCode(mobileView.getText().toString(),
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        BCSmsResult smsResult = (BCSmsResult) result;
                        Message message = mHandler.obtainMessage();

                        if (smsResult.getResultCode() != 0) {
                            message.what = 99;
                            errMsg = smsResult.getResultMsg();
                            Log.w(TAG, smsResult.getResultMsg());
                            Log.w(TAG, smsResult.getErrDetail());
                        } else {
                            message.what = 3;
                            smsId = smsResult.getSmsId();
                            Log.w(TAG, "sms id: " + smsId);
                        }

                        mHandler.sendMessage(message);
                    }
                });
    }

    public void subscribe(View v) {
        final BCSubscription subscription = new BCSubscription();
        subscription.setBuyerId(buyerIdView.getText().toString());
        subscription.setPlanId(selectPlanId);
        subscription.setBankName(selectBankName);
        subscription.setCardNum(cardNoView.getText().toString());
        subscription.setIdName(idNameView.getText().toString());
        subscription.setIdNum(idNoView.getText().toString());
        subscription.setMobile(mobileView.getText().toString());

        BCPay.getInstance(this).subscribe(subscription, smsId, smsCodeView.getText().toString(), null,
                new BCCallback() {
                    @Override
                    public void done(BCResult result) {
                        BCSubscriptionResult subscriptionResult = (BCSubscriptionResult) result;

                        Message message = mHandler.obtainMessage();

                        if (subscriptionResult.getResultCode() != 0) {
                            errMsg = subscriptionResult.getResultMsg();
                            Log.w(TAG, subscriptionResult.getResultMsg());
                            Log.w(TAG, subscriptionResult.getErrDetail());
                            message.what = 99;
                        } else {
                            message.what = 4;
                            // 返回的subscription
                            BCSubscription subscription = subscriptionResult.getSubscription();
                            Log.w(TAG, "注意保存唯一标识符subscription id: " + subscription.getId());
                        }

                        mHandler.sendMessage(message);
                    }
                });
    }
}
