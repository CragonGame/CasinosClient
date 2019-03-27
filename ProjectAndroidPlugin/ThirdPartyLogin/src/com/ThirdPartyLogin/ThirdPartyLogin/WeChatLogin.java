package com.ThirdPartyLogin.ThirdPartyLogin;

import android.app.Activity;
import android.util.Log;

import com.tencent.mm.opensdk.modelmsg.SendAuth.Req;
//import com.tencent.mm.sdk.openapi.SendMessageToWX;
import com.tencent.mm.opensdk.openapi.WXAPIFactory;
import com.tencent.mm.opensdk.openapi.IWXAPI;

// -------------------------------------------------------------------------
public class WeChatLogin implements IThirdPartyLogin {
	Activity mUnityActivity;	
	IWXAPI mAPI;
	int ERR_NOTINSTALLEDWECHAT = 10000;
	
	// -------------------------------------------------------------------------
	public WeChatLogin(Activity activity)
	{
		mUnityActivity = activity;
	}

	// -------------------------------------------------------------------------
	@Override
	public void Login(String state) {
		String app_id = ThirdPartyLogin.Instantce.getAppId();
		String login_param = ThirdPartyLogin.Instantce.getLoginParam();
		// Log.e("WeChatLogin", app_id);
		mAPI=WXAPIFactory.createWXAPI(mUnityActivity, app_id, true);
		mAPI.registerApp(app_id);
		Boolean b= mAPI.isWXAppInstalled();
		// Log.e("WeChatLogin", "isWXAppInstalled__"+b);
		
		if(!b)
		{
			ThirdPartyLogin.sendToUnity(false, "",login_param,ERR_NOTINSTALLEDWECHAT);
		}
		else
		{
		    // send oauth request 
			Req req = new Req();
		    req.scope = "snsapi_userinfo";
		    req.state = state;
		    Boolean send_success = mAPI.sendReq(req);
		    // Log.e("WeChatLogin", "send_success__"+send_success);
		}
	}
}
