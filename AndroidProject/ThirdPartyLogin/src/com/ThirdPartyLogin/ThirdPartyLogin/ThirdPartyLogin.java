package com.ThirdPartyLogin.ThirdPartyLogin;

import android.app.Activity;
import android.util.Log;

import com.AndroidToUnityMsgBridge.AndroidToUnityMsgBridge.AndroidToUnityMsgBridge;

// -------------------------------------------------------------------------
public class ThirdPartyLogin {
	public static ThirdPartyLogin Instantce;
	static AndroidToUnityMsgBridge mAndroidToUnityMsgBridge;
	static String mResultReceiver;	
	static String mSuccessMethodName;
	static String mFailMethodName;
	static String mAppId;
	Activity mUnityActivity;
	IThirdPartyLogin mIThirdPartyLogin;
	String mLoginParam;
		
	// -------------------------------------------------------------------------
	public static ThirdPartyLogin Instantce(String app_id,String success_methodname, 
			String fail_methodname,String result_receiver) {		
		mResultReceiver = result_receiver;
		mSuccessMethodName = success_methodname;
		mFailMethodName = fail_methodname;		
		mAppId = app_id;
		if(Instantce==null)
		{
			mAndroidToUnityMsgBridge = AndroidToUnityMsgBridge
					.Instance();
			Instantce = new ThirdPartyLogin();			
		}
		
		return Instantce;
	}

	// -------------------------------------------------------------------------
	private ThirdPartyLogin()
	{
		mUnityActivity = mAndroidToUnityMsgBridge.getActivity();		
	}
	
	// -------------------------------------------------------------------------
	public void Login(int login_type,String state,String param)
	{
		mLoginParam = param;
		if(login_type == LoginType.WeChat.ordinal())
		{
			mIThirdPartyLogin = new WeChatLogin(mUnityActivity);
			mIThirdPartyLogin.Login(state);
		}		
	}
	
	// -------------------------------------------------------------------------
	public String getAppId()
	{
		return mAppId;
	}
	
	// -------------------------------------------------------------------------
	public String getLoginParam()
	{
		return mLoginParam;
	}

	// -------------------------------------------------------------------------
	public static void sendToUnity(Boolean is_success, String info) {		
		if (mAndroidToUnityMsgBridge == null) {			
			mAndroidToUnityMsgBridge = AndroidToUnityMsgBridge
					.Instance();
		}
		if (is_success) {
			Log.e("ThirdPartyLogin", "sendToUnity::is_success::" + info);
			mAndroidToUnityMsgBridge.sendMsgToUnity(mResultReceiver,mSuccessMethodName, info);
		} else {
			Log.e("ThirdPartyLogin", "sendToUnity::is_fail::"+info);
			mAndroidToUnityMsgBridge.sendMsgToUnity(mResultReceiver,mFailMethodName, info);
		}
	}

	// -------------------------------------------------------------------------
	public enum LoginType
	{
		WeChat
	}
}