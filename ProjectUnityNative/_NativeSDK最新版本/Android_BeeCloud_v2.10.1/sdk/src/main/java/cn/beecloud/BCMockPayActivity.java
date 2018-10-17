package cn.beecloud;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.graphics.drawable.GradientDrawable;
import android.os.Build;
import android.os.Bundle;
import android.util.Log;
import android.view.Gravity;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.TextView;

import com.google.gson.Gson;
import com.google.gson.JsonSyntaxException;
import com.google.gson.reflect.TypeToken;

import java.lang.reflect.Type;
import java.util.Map;

import cn.beecloud.entity.BCPayResult;

public class BCMockPayActivity extends Activity {
    private static final String TAG = "BCMockPayActivity";

    TextView cancelView;
    TextView billTitleView;
    TextView billFeeView;
    Button payButton;
    //支付订单的唯一标识符
    String id;

    private ProgressDialog loadingDialog;

    @Override
    @SuppressWarnings("deprecation")
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        int mainOrange = 0xFFFF6C2C;

        //根布局参数
        LinearLayout.LayoutParams layoutParamsRoot =
                new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT,
                        LinearLayout.LayoutParams.MATCH_PARENT);
        //根布局
        LinearLayout layoutRoot = new LinearLayout(this);
        layoutRoot.setLayoutParams(layoutParamsRoot);
        layoutRoot.setOrientation(LinearLayout.VERTICAL);
        layoutRoot.setBackgroundColor(0xFFF5F5F5);

        //标题
        LinearLayout.LayoutParams titleLayoutParams =
                new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT,
                        dip2px(this, 50));

        RelativeLayout titleLayout = new RelativeLayout(this);
        titleLayout.setLayoutParams(titleLayoutParams);
        titleLayout.setBackgroundColor(mainOrange);
        layoutRoot.addView(titleLayout);

        //取消
        RelativeLayout.LayoutParams cancelLayoutParams =
                new RelativeLayout.LayoutParams(dip2px(this, 70),
                        RelativeLayout.LayoutParams.MATCH_PARENT);
        cancelLayoutParams.addRule(RelativeLayout.ALIGN_PARENT_LEFT);
        cancelView = new TextView(this);
        cancelView.setText("取消");
        cancelView.setTextColor(Color.WHITE);
        cancelView.setTextSize(15);
        cancelView.setGravity(Gravity.CENTER);
        titleLayout.addView(cancelView, cancelLayoutParams);

        TextView titleView = new TextView(this);
        titleView.setText("确认交易");
        titleView.setBackgroundColor(mainOrange);
        titleView.setTextColor(Color.WHITE);
        titleView.setTextSize(18);
        titleView.setGravity(Gravity.CENTER);

        RelativeLayout.LayoutParams layoutParamsTitleInfo =
                new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WRAP_CONTENT,
                        RelativeLayout.LayoutParams.MATCH_PARENT);
        layoutParamsTitleInfo.addRule(RelativeLayout.CENTER_IN_PARENT);
        titleLayout.addView(titleView, layoutParamsTitleInfo);

        //订单标题
        billTitleView = new TextView(this);
        billTitleView.setTextColor(Color.BLACK);
        billTitleView.setTextSize(16);
        billTitleView.setGravity(Gravity.BOTTOM|Gravity.CENTER_HORIZONTAL);

        LinearLayout.LayoutParams billTitleLayoutParams =
                new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT,
                        dip2px(this, 50));
        layoutRoot.addView(billTitleView, billTitleLayoutParams);

        //支付金额
        billFeeView = new TextView(this);
        billFeeView.setTextColor(Color.BLACK);
        billFeeView.setTextSize(36);
        billFeeView.setGravity(Gravity.CENTER);

        LinearLayout.LayoutParams billFeeLayoutParams =
                new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT,
                        dip2px(this, 80));
        layoutRoot.addView(billFeeView, billFeeLayoutParams);

        //收款方
        LinearLayout.LayoutParams receiverLayoutParams =
                new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT,
                        dip2px(this, 50));
        RelativeLayout receiverLayout = new RelativeLayout(this);
        receiverLayout.setBackgroundColor(Color.WHITE);
        layoutRoot.addView(receiverLayout, receiverLayoutParams);

        RelativeLayout.LayoutParams receiverLabelLayoutParams =
                new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WRAP_CONTENT,
                        RelativeLayout.LayoutParams.WRAP_CONTENT);
        receiverLabelLayoutParams.leftMargin = dip2px(this, 20);
        receiverLabelLayoutParams.addRule(RelativeLayout.ALIGN_PARENT_LEFT);
        receiverLabelLayoutParams.addRule(RelativeLayout.CENTER_VERTICAL, RelativeLayout.TRUE);

        TextView receiverLabel = new TextView(this);
        receiverLabel.setText("收款方");
        receiverLabel.setTextColor(0xFF979797);
        receiverLabel.setTextSize(14);

        receiverLayout.addView(receiverLabel, receiverLabelLayoutParams);

        RelativeLayout.LayoutParams receiverNameLayoutParams =
                new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WRAP_CONTENT,
                        RelativeLayout.LayoutParams.WRAP_CONTENT);
        receiverNameLayoutParams.rightMargin = dip2px(this, 20);
        receiverNameLayoutParams.addRule(RelativeLayout.ALIGN_PARENT_RIGHT);
        receiverNameLayoutParams.addRule(RelativeLayout.CENTER_VERTICAL, RelativeLayout.TRUE);

        TextView receiverName = new TextView(this);
        receiverName.setText("比可科技");
        receiverName.setTextColor(Color.BLACK);
        receiverName.setTextSize(14);

        receiverLayout.addView(receiverName, receiverNameLayoutParams);

        //支付按钮
        LinearLayout.LayoutParams buttonsLayoutParams =
                new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT,
                        dip2px(this, 50));
        buttonsLayoutParams.leftMargin = dip2px(this, 30);
        buttonsLayoutParams.rightMargin = dip2px(this, 30);
        buttonsLayoutParams.topMargin = dip2px(this, 45);

        payButton = new Button(this);
        payButton.setText("立即支付");
        payButton.setTextColor(Color.WHITE);
        payButton.setTextSize(18);

        //圆角
        GradientDrawable shape = new GradientDrawable();
        shape.setShape(GradientDrawable.RECTANGLE);
        shape.setCornerRadii(new float[]{8, 8, 8, 8, 8, 8, 8, 8});
        shape.setColor(mainOrange);

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.JELLY_BEAN) {
            //Android系统大于等于API16，使用setBackground
            payButton.setBackground(shape);
        } else {
            //Android系统小于API16，使用setBackgroundDrawable
            payButton.setBackgroundDrawable(shape);
        }

        layoutRoot.addView(payButton, buttonsLayoutParams);

        //底部说明
        LinearLayout.LayoutParams tipLayoutParams =
                new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WRAP_CONTENT,
                        LinearLayout.LayoutParams.WRAP_CONTENT);
        tipLayoutParams.gravity = Gravity.CENTER;
        tipLayoutParams.leftMargin = dip2px(this, 20);
        tipLayoutParams.rightMargin = dip2px(this, 20);
        tipLayoutParams.topMargin = dip2px(this, 50);

        TextView tip = new TextView(this);
        tip.setText("本页面模拟支付环境，不会发生实际交易");
        tip.setTextColor(0xFF9E9E9E);
        tip.setTextSize(12);

        layoutRoot.addView(tip, tipLayoutParams);

        setContentView(layoutRoot);

        setExtraParams();

        initButtonBehaviors();

        loadingDialog = new ProgressDialog(this);
        loadingDialog.setMessage("处理中，请稍候...");
        loadingDialog.setIndeterminate(true);
        loadingDialog.setCancelable(true);
    }

    void setExtraParams() {
        Intent intent = getIntent();
        id = intent.getStringExtra("id");

        String billTitle = intent.getStringExtra("billTitle");
        if (billTitle == null)
            billTitle = "订单标题";
        billTitleView.setText(billTitle);

        String billFee = "￥" + (intent.getIntExtra("billTotalFee", 0) / 100.0);
        billFeeView.setText(billFee);
    }

    void initButtonBehaviors() {
        cancelView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (BCPay.payCallback != null) {
                    BCPay.payCallback.done(new BCPayResult(BCPayResult.RESULT_CANCEL,
                            BCPayResult.APP_PAY_CANCEL_CODE, BCPayResult.RESULT_CANCEL,
                            BCPayResult.RESULT_CANCEL, BCCache.getInstance().billID));
                } else {
                    Log.e(TAG, "Callback should not be null");
                }
                finish();
            }
        });

        payButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                loadingDialog.show();
                //发送模拟的异步通知
                BCCache.executorService.execute(new Runnable() {
                    @Override
                    public void run() {
                        String notifyUrl = BCHttpClientUtil.getNotifyPayResultSandboxUrl()
                                + "/" + BCCache.getInstance().billID;
                        BCHttpClientUtil.Response response = BCHttpClientUtil.httpGet(notifyUrl);

                        loadingDialog.dismiss();
                        if (response.code == 200 || (response.code >= 400 && response.code < 500)) {
                            String ret = response.content;

                            //反序列化json
                            Gson gson = new Gson();
                            Type type = new TypeToken<Map<String, Object>>(){}.getType();
                            Map<String, Object> responseMap;

                            try {
                                responseMap = gson.fromJson(ret, type);
                            } catch (JsonSyntaxException ex) {
                                if (BCPay.payCallback != null) {
                                    BCPay.payCallback.done(new BCPayResult(BCPayResult.RESULT_FAIL,
                                            BCPayResult.APP_INTERNAL_EXCEPTION_ERR_CODE,
                                            BCPayResult.FAIL_EXCEPTION,
                                            "JsonSyntaxException or Network Error:"
                                                    + response.code + " # " + response.content));
                                } else {
                                    Log.e(TAG, "Callback should not be null");
                                }
                                return;
                            }

                            //判断后台返回结果
                            Integer resultCode = ((Double) responseMap.get("result_code")).intValue();
                            if (resultCode == 0) {
                                if (BCPay.payCallback != null) {
                                    BCPay.payCallback.done(new BCPayResult(BCPayResult.RESULT_SUCCESS,
                                            BCPayResult.APP_PAY_SUCC_CODE, BCPayResult.RESULT_SUCCESS,
                                            BCPayResult.RESULT_SUCCESS, BCCache.getInstance().billID));
                                } else {
                                    Log.e(TAG, "Callback should not be null");
                                }
                            } else {
                                if (BCPay.payCallback != null) {
                                    BCPay.payCallback.done(new BCPayResult(BCPayResult.RESULT_FAIL,
                                            resultCode,
                                            String.valueOf(responseMap.get("result_msg")),
                                            String.valueOf(responseMap.get("err_detail"))));
                                } else {
                                    Log.e(TAG, "Callback should not be null");
                                }
                            }
                        } else {
                            if (BCPay.payCallback != null) {
                                BCPay.payCallback.done(new BCPayResult(BCPayResult.RESULT_FAIL,
                                        BCPayResult.APP_INTERNAL_NETWORK_ERR_CODE,
                                        BCPayResult.FAIL_NETWORK_ISSUE,
                                        "Network Error:" + response.code + " # " + response.content));
                            } else {
                                Log.e(TAG, "Callback should not be null");
                            }
                        }

                        finish();
                    }
                });
            }
        });
    }

    @Override
    public void onBackPressed() {
        if (BCPay.payCallback != null) {
            BCPay.payCallback.done(new BCPayResult(BCPayResult.RESULT_CANCEL,
                    BCPayResult.APP_PAY_CANCEL_CODE, BCPayResult.RESULT_CANCEL,
                    BCPayResult.RESULT_CANCEL, BCCache.getInstance().billID));

            super.onBackPressed();
        } else {
            Log.e(TAG, "Callback should not be null");
        }
    }

    /**
     * 根据手机的分辨率从 dp 的单位 转成为 px(像素)
     */
    public static int dip2px(Context context, float dpValue) {
        final float scale = context.getResources().getDisplayMetrics().density;
        return (int) (dpValue * scale + 0.5f);
    }
}
