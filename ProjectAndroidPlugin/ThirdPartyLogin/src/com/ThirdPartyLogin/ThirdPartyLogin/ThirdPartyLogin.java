package com.ThirdPartyLogin.ThirdPartyLogin;

import java.util.HashMap;
import java.util.Map;

import org.json.JSONObject;

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
	public static void sendToUnity(Boolean is_success,String token,String login_param,int errCode) {	
		
		if (mAndroidToUnityMsgBridge == null) {			
			mAndroidToUnityMsgBridge = AndroidToUnityMsgBridge.Instance();
		}
		
		Map<String,Object> map = new HashMap<String,Object>(); 
		map.put("ret", is_success); 					// 结果
		map.put("native_type", 3);						// 来自哪里
		map.put("token", token);						// 微信token
		map.put("login_param", login_param);			// 登录参数返回login_param
		map.put("err_code", errCode);					// 错误代码
		
		JSONObject json = new JSONObject(map);
		
		String ret_str = json.toString();
		
		if (is_success) {
			Log.e("ThirdPartyLogin", "sendToUnity::is_success::" + ret_str);
		} else {
			Log.e("ThirdPartyLogin", "sendToUnity::is_fail::"+ret_str);
		}
		mAndroidToUnityMsgBridge.sendMsgToUnity(ret_str);
	}

	// -------------------------------------------------------------------------
	public enum LoginType
	{
		WeChat
	}
}