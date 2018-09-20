package com.Pay.Pay;

import com.pingplusplus.android.Pingpp;
import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;

public class PayActivity extends Activity {
	boolean mAlreadyPay = false;

	// -------------------------------------------------------------------------
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		Log.e("PayActivity", "onCreate");		

		_getSavedInstanceState(savedInstanceState);

		if (!mAlreadyPay) {
			String pay_data = this.getIntent().getStringExtra("Pay");
			Log.e("PayActivity", "pay::" + pay_data);
			Pingpp.createPayment(PayActivity.this, pay_data);
		}
	}

	// -------------------------------------------------------------------------
	/**
	 * onActivityResult 获得支付结果，如果支付成功，服务器会收到ping++ 服务器发送的异步通知。 最终支付成功根据异步通知为准
	 */
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		//支付页面返回处理
	    if (requestCode == Pingpp.REQUEST_CODE_PAYMENT) {
	        if (resultCode == Activity.RESULT_OK) {
	            String result = data.getExtras().getString("pay_result");
	            /* 处理返回值
	             * "success" - 支付成功
	             * "fail"    - 支付失败
	             * "cancel"  - 取消支付
	             * "invalid" - 支付插件未安装（一般是微信客户端未安装的情况）
	             * "unknown" - app进程异常被杀死(一般是低内存状态下,app进程被杀死)
	             */	            
	            String errorMsg = data.getExtras().getString("error_msg"); // 错误信息
	            String extraMsg = data.getExtras().getString("extra_msg"); // 错误信息
	            //showMsg(result, errorMsg, extraMsg);
	            String total_result = result+";"+errorMsg+";"+extraMsg;
	            Log.e("PayActivity", "PayResult::" + total_result);
	            Pay.sendToUnity(total_result);
	            this.mAlreadyPay = true;
	        }
	    }
	    
	    finish();
	}

	// -------------------------------------------------------------------------
	@Override
	protected void onSaveInstanceState(Bundle outState) {
		Log.e("PayActivity", "onSaveInstanceState");

		outState.putBoolean("AlreadyPay", this.mAlreadyPay);
	}

	// -------------------------------------------------------------------------
	@Override
	protected void onRestoreInstanceState(Bundle savedInstanceState) {
		Log.e("PayActivity", "onRestoreInstanceState");

		_getSavedInstanceState(savedInstanceState);
	}

	// -------------------------------------------------------------------------
	void _getSavedInstanceState(Bundle savedInstanceState) {
		if (savedInstanceState != null) {
			this.mAlreadyPay = savedInstanceState.getBoolean("AlreadyPay");
		}
	}
}
