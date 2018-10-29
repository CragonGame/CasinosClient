using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using GTPush;

#if UNITY_IPHONE
using NotificationServices = UnityEngine.iOS.NotificationServices;
using NotificationType = UnityEngine.iOS.NotificationType;
#endif

//别名action类型
public struct  BindAliasActionType
{
	public static string kGtResponseBindType = "bindAlias"; // 绑定
	public static string kGtResponseUnBindType = "unbindAlias";//解绑
}

public class GetuiPushDemo : MonoBehaviour {
	const string appId = "IbmKW6ssr99lveCtPMG9dA";
	const string appKey = "RhhDAohWe16Ejdgmw7roB1";
	const string appSecret = "ytJMQpWxEQ7HhnFMHcjZV2";
	bool tokenSent;

	// Use this for initialization
	void Start () {

		#if UNITY_IPHONE
		tokenSent = false;
		GTPushBinding.StartSDK (appId,appKey,appSecret);
		GTPushBinding.setListenerGameObject (this.gameObject.name);
		GTPushBinding.registerUserNotification ();
		// 注册 VoIP 通知
		GTPushBinding.voipRegistration();
		#endif

		#if UNITY_ANDROID
		GTPushBinding.initPush (this.gameObject.name);
		#endif

		#if (UNITY_IPHONE || UNITY_ANDROID)
		Debug.Log ("getui sdk version is : " + GTPushBinding.getVersion());
		Debug.Log ("isPushTurnOn is : " + GTPushBinding.isPushTurnOn());

		#endif
	}

	// Update is called once per frame
	void Update () {
		#if UNITY_IPHONE
		if (!tokenSent) {
			byte[] token = NotificationServices.deviceToken;
			if (token != null) {
				// send token to a provider
				tokenSent = true;
				string deviceToken = System.BitConverter.ToString(token).Replace("-","");
				GTPushBinding.registerDeviceToken (deviceToken);
				Debug.Log ("deviceToken is : " + deviceToken +  " cid is : "+GTPushBinding.getClientId() + " version is : " + GTPushBinding.getVersion());
			}
		}
		#endif
	}

	//
	//GetuiSDK 回调
	//

	/**
 *  SDK登入成功返回clientId
 *
 *  @param clientId 标识用户的clientId
 *  说明:启动GeTuiSdk后，SDK会自动向个推服务器注册SDK，当成功注册时，SDK通知应用注册成功。
 *  注意: 注册成功仅表示推送通道建立，如果appid/appkey/appSecret等验证不通过，依然无法接收到推送消息，请确保验证信息正确。
 */
	public void onReceiveClientId(string clientId){
		Debug.Log ("GeTuiSdkDidRegisterClient clientId : " + clientId);
		#if (UNITY_IPHONE || UNITY_ANDROID)
		GTPushBinding.setTag ("ge,tui");
		GTPushBinding.bindAlias ("getui");
		GTPushBinding.unBindAlias ("getui");
		#endif
	
	}

	/**
 *  SDK通知收到个推推送的透传消息
 *
 *  @param payloadData 推送消息内容
 *  @param taskId      推送消息的任务id
 *  @param msgId       推送消息的messageid
 *  @param offLine     是否是离线消息，YES.是离线消息
 *  @param appId       应用的appId
 */
	public void onReceiveMessage(string payloadJsonData){
		Debug.Log ("GeTuiSdkDidReceivePayloadData payload JsonData : " + payloadJsonData);
	}

	public void onNotificationMessageArrived(string msg){
		Debug.Log ("whb,onNotificationMessageArrived : " + msg);
	}

	public void onNotificationMessageClicked(string  msg){
		Debug.Log ("whb,onNotificationMessageClicked : " + msg);
	}
	#if UNITY_IPHONE
	/**
 *  SDK设置关闭推送模式回调
 *
 *  @param isModeOn true：开启 false：关闭
 */
	public void GeTuiSdkDidSetPushMode(string isModeOn){
		Debug.Log ("GeTuiSdkDidSetPushMode isModeOn : " + isModeOn);
	}
	/**
 *  SDK遇到错误消息返回error
 *
 *  @param error SDK内部发生错误，通知第三方，返回错误
 */
	public void GeTuiSdkDidOccurError(string error){
		Debug.Log ("GeTuiSdkDidOccurError error : " + error);
	}

	/**
	 *  SDK运行状态通知
	 *
	 *  @param message 返回SDK运行状态
	 */
	public void GeTuiSDkDidNotifySdkState(string state){
		Debug.Log ("GeTuiSDkDidNotifySdkState state : " + state);
	}

	/**
	 *  SDK绑定、解绑回调
	 *
	 *  @param action       回调动作类型 kGtResponseBindType 或 kGtResponseUnBindType
	 *  @param result    成功返回 YES, 失败返回 NO
	 *  @param sequenceNum          返回请求的序列码
	 *  @param error       成功返回nil, 错误返回相应error信息
	 */

	public void GeTuiSdkDidAliasAction(string message){
		Debug.Log ("GeTuiSdkDidAliasAction message : " + message);
	}

	// VoIP 推送消息回调
	public void onReceiveVoIPMessage(string message){
		Debug.Log ("onReceiveVoIPMessage message : " + message);
	}
	#endif
}
