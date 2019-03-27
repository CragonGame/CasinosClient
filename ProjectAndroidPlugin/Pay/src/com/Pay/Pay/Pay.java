package com.Pay.Pay;

import java.util.HashMap;
import java.util.Map;

import org.json.JSONObject;

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
//	static String mResultReceiver;	

	// -------------------------------------------------------------------------
	public static Pay Instance(String beecloud_id,String beecloud_secret,String wechat_id) {
		if (mPay == null) {
			mPay = new Pay(beecloud_id,beecloud_secret,wechat_id);
		}
		return mPay;
	}

	// -------------------------------------------------------------------------
	private Pay(String beecloud_id,String beecloud_secret,String wechat_id) {
		mAndroidToUnityMsgBridge = AndroidToUnityMsgBridge
				.Instance();
		this.mUnityActivity = mAndroidToUnityMsgBridge.getActivity();
		if(beecloud_id != null && beecloud_id.isEmpty() == false)
		{
			Log.e("Pay", "beecloud_id ::" + beecloud_id);
			BeeCloud.setAppIdAndSecret(beecloud_id,beecloud_secret);
			String initInfo = BCPay.initWechatPay(this.mUnityActivity, wechat_id);
	        if (initInfo != null) {
	        	 String total_result = "wechat_notinstall"; 	            
	 	         Pay.sendToUnity(false,total_result,total_result,10000);
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
	public static void sendToUnity(boolean is_success,String result,String msg,int err_code) {
		Log.e("Pay","____UnityPayResultMethord__"+UnityPayResultMethord+"___sendToUnityResult__"+result);
		
		Map<String,Object> map = new HashMap<String,Object>(); 
		map.put("ret", is_success); 					// 结果
		map.put("native_type", 4);						// 来自哪里
		map.put("result", result);						// 处理返回的结果
		map.put("msg", msg);							// 结果描述
		map.put("err_code", err_code);					// 错误代码
		
		JSONObject json = new JSONObject(map);
		
		String ret_str = json.toString();
		
		mAndroidToUnityMsgBridge.sendMsgToUnity(ret_str);
	}
}
