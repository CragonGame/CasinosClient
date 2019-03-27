package com.Cragon.ZeroPt.wxapi;

import com.ThirdPartyLogin.ThirdPartyLogin.ThirdPartyLogin;
import com.tencent.mm.opensdk.modelbase.BaseReq;
import com.tencent.mm.opensdk.modelbase.BaseResp;
import com.tencent.mm.opensdk.constants.ConstantsAPI;
import com.tencent.mm.opensdk.openapi.IWXAPI;
import com.tencent.mm.opensdk.openapi.IWXAPIEventHandler;
import com.tencent.mm.opensdk.modelmsg.SendAuth;
import com.tencent.mm.opensdk.modelmsg.ShowMessageFromWX;
import com.tencent.mm.opensdk.openapi.WXAPIFactory;
import com.tencent.mm.opensdk.modelmsg.WXAppExtendObject;
import com.tencent.mm.opensdk.modelmsg.WXMediaMessage;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;

public class WXEntryActivity extends Activity implements IWXAPIEventHandler{
	// IWXAPI 是第三方app和微信通信的openapi接口
    private IWXAPI api;
	
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);        
        
        // 通过WXAPIFactory工厂，获取IWXAPI的实例
        String app_id = ThirdPartyLogin.Instantce.getAppId(); 
        Log.e("WXEntryActivity", "onCreate__"+app_id);
        
    	api = WXAPIFactory.createWXAPI(this, app_id, false);
		//注意：
		//第三方开发者如果使用透明界面来实现WXEntryActivity，需要判断handleIntent的返回值，如果返回值为false，
    	//则说明入参不合法未被SDK处理，应finish当前透明界面，避免外部通过传递非法参数的Intent导致停留在透明界面，引起用户的疑惑
        try {        	
        	api.handleIntent(getIntent(), this);
        } catch (Exception e) {
        	Log.e("WXEntryActivity", "onCreate__handleIntent_Error__"+e.getMessage());
        	e.printStackTrace();
        }
    }

	@Override
	protected void onNewIntent(Intent intent) {
		super.onNewIntent(intent);
		
		setIntent(intent);
        api.handleIntent(intent, this);
	}

	// 微信发送请求到第三方应用时，会回调到该方法
	@Override
	public void onReq(BaseReq req) {
		Log.e("WXEntryActivity", "onReq__"+req.getType());
		switch (req.getType()) {
		case ConstantsAPI.COMMAND_GETMESSAGE_FROM_WX:
				
			break;
		case ConstantsAPI.COMMAND_SHOWMESSAGE_FROM_WX:
			ShowMessageFromWX.Req showReq= (ShowMessageFromWX.Req) req;
			WXMediaMessage wxMsg = showReq.message;		
			WXAppExtendObject obj = (WXAppExtendObject) wxMsg.mediaObject;
			
			StringBuffer msg = new StringBuffer(); // 组织一个待显示的消息内容
			msg.append("description: ");
			msg.append(wxMsg.description);
			msg.append("\n");
			msg.append("extInfo: ");
			msg.append(obj.extInfo);
			msg.append("\n");
			msg.append("filePath: ");
			msg.append(obj.filePath);
			
			//Intent intent = new Intent(this, ShowFromWXActivity.class);
			//intent.putExtra(Constants.ShowMsgActivity.STitle, wxMsg.title);
			//intent.putExtra(Constants.ShowMsgActivity.SMessage, msg.toString());
			//intent.putExtra(Constants.ShowMsgActivity.BAThumbData, wxMsg.thumbData);
			//startActivity(intent);
			finish();
			break;
		default:
			break;
		}
	}

	// 第三方应用发送到微信的请求处理后的响应结果，会回调到该方法
	@Override
	public void onResp(BaseResp resp) {		
		Log.e("WXEntryActivity", "errCode__"+resp.errCode);
		
		String login_param = ThirdPartyLogin.Instantce.getLoginParam();
		if(resp.errCode== BaseResp.ErrCode.ERR_OK)
		{
			SendAuth.Resp re= ((SendAuth.Resp)resp);
			String token = re.code;
			Log.e("WXEntryActivity", "token__"+token);		
			ThirdPartyLogin.sendToUnity(true, token,login_param,resp.errCode);
		}
		else	
		{		
			ThirdPartyLogin.sendToUnity(false, "",login_param,resp.errCode);
		}		
		
		finish();
	}	
}
