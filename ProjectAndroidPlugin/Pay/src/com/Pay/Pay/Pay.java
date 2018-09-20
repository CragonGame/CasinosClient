package com.Pay.Pay;

import android.content.Intent;
import android.util.Log;
import android.widget.Toast;
import android.app.Activity;
import cn.beecloud.BCPay;
import cn.beecloud.BeeCloud;

import com.AndroidToUnityMsgBridge.AndroidToUnityMsgBridge.AndroidToUnityMsgBridge;

public class Pay {
	// -------------------------------------------------------------------------
	private static Pay mPay;
	private static AndroidToUnityMsgBridge mAndroidToUnityMsgBridge;
	private Activity mUnityActivity;	
	private static String UnityPayResultMethord = "PayResult";
	static String mResultReceiver;	

	// -------------------------------------------------------------------------
	public static Pay Instance(String pay_resultreceiver,String beecloud_id,String beecloud_secret,String wechat_id) {
		if (mPay == null) {
			mPay = new Pay(pay_resultreceiver,beecloud_id,beecloud_secret,wechat_id);
		}
		return mPay;
	}

	// -------------------------------------------------------------------------
	private Pay(String pay_resultreceiver,String beecloud_id,String beecloud_secret,String wechat_id) {
		mAndroidToUnityMsgBridge = AndroidToUnityMsgBridge
				.Instance();
		mResultReceiver = pay_resultreceiver;
		this.mUnityActivity = mAndroidToUnityMsgBridge.getActivity();
		if(beecloud_id != null && beecloud_id.isEmpty() == false)
		{
			Log.e("Pay", "beecloud_id ::" + beecloud_id);
			BeeCloud.setAppIdAndSecret(beecloud_id,beecloud_secret);
			String initInfo = BCPay.initWechatPay(this.mUnityActivity, wechat_id);
	        if (initInfo != null) {
	        	 String total_result = "wechat_notinstall; ; "; 	            
	 	         Pay.sendToUnity(total_result);
	        }
		}
	}
	
	// -------------------------------------------------------------------------
	public static void useTestMode(boolean use_testmode)
	{
		BeeCloud.setSandbox(use_testmode);
	}
	
//	
//	// -------------------------------------------------------------------------
//	public static void payWithChargeData(String pay_chargedata) {
//		Intent intent = new Intent(mPay.mUnityActivity,
//				com.Pay.Pay.PayActivity.class);		
//		intent.putExtra("Pay", pay_chargedata);		
//		mPay.mUnityActivity.startActivity(intent);		
//	}
//	
	// -------------------------------------------------------------------------
	public static void pay(String bill_title,int channel_type,int bill_totalfee,String bill_num,String buy_id) {		
		Intent intent = new Intent(mPay.mUnityActivity,
				com.Pay.Pay.BeeCloudPayActivity.class);		
		intent.putExtra("ChannelType", channel_type);
		intent.putExtra("BillTitle", bill_title);
		intent.putExtra("BillTotalFee", bill_totalfee);
		intent.putExtra("BillNum", bill_num);
		intent.putExtra("BuyId", buy_id);
		mPay.mUnityActivity.startActivity(intent);		
	}

	// -------------------------------------------------------------------------
	public static void sendToUnity(String result) {
		Log.e("Pay", "mResultReceiver__"+mResultReceiver+"____UnityPayResultMethord__"+UnityPayResultMethord+"___sendToUnityResult__"+result);

		mAndroidToUnityMsgBridge.sendMsgToUnity(mResultReceiver,UnityPayResultMethord,result);
	}
}
