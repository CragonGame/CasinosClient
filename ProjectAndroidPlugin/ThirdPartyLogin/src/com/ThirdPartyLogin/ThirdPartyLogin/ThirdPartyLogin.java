package com.ThirdPartyLogin.ThirdPartyLogin;

import android.app.Activity;
import android.util.Log;

import com.AndroidToUnityMsgBridge.AndroidToUnityMsgBridge.AndroidToUnityMsgBridge;

// -------------------------------------------------------------------------
public class ThirdPartyLogin {
	public static ThirdPartyLogin Instantce;
	static AndroidToUnityMsgBridge mAndroidToUnityMsgBridge;
	static String mAppId;
	Activity mUnityActivity;
	IThirdPartyLogin mIThirdPartyLogin;
	String mLoginParam;
		
	// -------------------------------------------------------------------------
	public static ThirdPartyLogin Instantce(String app_id) {			
		mAppId = app_id;
		if(Instantce==null)
		{
			mAndroidToUnityMsgBridge = AndroidToUnityMsgBridge.Instance();
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
			mAndroidToUnityMsgBridge = AndroidToUnityMsgBridge.Instance();
		}
		if (is_success) {
			Log.e("ThirdPartyLogin", "sendToUnity::is_success::" + info);
			mAndroidToUnityMsgBridge.sendMsgToUnity(is_success,3,info);
		} else {
			Log.e("ThirdPartyLogin", "sendToUnity::is_fail::"+info);
			mAndroidToUnityMsgBridge.sendMsgToUnity(is_success,3,info);
		}
	}

	// -------------------------------------------------------------------------
	public enum LoginType
	{
		WeChat
	}
}