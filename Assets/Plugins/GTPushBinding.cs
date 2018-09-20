using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace GTPush
{
	public class GTPushBinding : MonoBehaviour
	{

		#if UNITY_ANDROID
		private static AndroidJavaObject _plugin;

		static  GTPushBinding ()
		{
			using (AndroidJavaClass gtPushClass = new AndroidJavaClass ("com.getui.getuiunity.GTPushBridge")) {
				_plugin = gtPushClass.CallStatic<AndroidJavaObject> ("getInstance");
			}
		}

		/**
		* 初始化推送服务，
		* 在调用其他方法之前需要最先调用该方法
		* @param gameObject 游戏对象名称
		*/
		public static void initPush (string gameObject)
		{
			_plugin.Call ("initPush", gameObject);
		}

		/**
		* 设置推送模式
		*/
		public static  void setPushMode (bool turnOn){
			_plugin.Call ("setPushMode", turnOn);
		}

		/**
		* 获取个推SDK版本号
		*/
		public static string getVersion ()
		{
			string version = _plugin.Call<string> ("getVersion");
			return version;
		}

		/**
		* 获取用户标记ID
		* 注意：如果紧跟initPush后调用，在第一次调用时候可能返回空
		*/
		public static string  getClientId ()
		{
			string clientId = _plugin.Call<string> ("getClientId");
			return clientId;
		}

		public static bool isPushTurnOn ()
		{
			bool turnOn = _plugin.Call<bool> ("isPushTurnOn");
			Debug.Log ("GTPushBinding isPushTurnOn = " + turnOn);
			return turnOn;
		}

		/**
		* 开启推送服务
		* 在initPush后默认已经开启推送服务，不需要再调用turnOnPush，
		* 只有在调用 turnOffPush 后，需要再开启推送时再调用此方法
		*/
		public static void turnOnPush ()
		{
			_plugin.Call ("turnOnPush");
		}

		/**
		* 关闭推送服务
		*/
		public static void turnOffPush ()
		{
			_plugin.Call ("turnOffPush");
		}

		/**
		* 设置标签
		* @param tags 若有多个标签，用逗号隔开
		* @return 设置标签结果，0表示设置成功，其他表示错误，具体请查看个推官网的错误码说明
		*/
		public static bool setTag (string tags)
		{
			int result = _plugin.Call<int> ("setTag", tags);
			if(result == 0)
				return true;
			else
				return false;
		}


		/**
		* 绑定别名
    */
		public static bool bindAlias (string alias)
		{
			bool result = _plugin.Call<bool> ("bindAlias", alias);
			return result;
		}

		/**
		* 取消绑定别名
		*/
		public static bool unBindAlias (string alias)
		{
			bool result = _plugin.Call<bool> ("unBindAlias", alias);
			return result;
		}


		/**
		*  设置渠道
		*  备注：Android不支持此功能，此处为空实现
		*  @param aChannelId 渠道值，可以为空值
		*/
		public static void setChannelId (string aChannelId)
		{
			//Empty
		}

		/*
		* 备注：Android不支持此功能，此处为空实现
		*/
		public static  void destroy (){
			//Empty
		}

		public static  void resume (){
			 _plugin.Call ("turnOnPush");
		}

		public static  void voipRegistration (){
			//Empty
		}

		#endif

		#if UNITY_IPHONE
		/* Interface to native implementation */

		[DllImport ("__Internal")]
		private static extern void _StartSDK (string appId, string appKey,string appSecret);

		[DllImport ("__Internal")]
		private static extern void _setListenerGameObject (string gameObjectName);

		[DllImport ("__Internal")]
		private static extern void _registerUserNotification ();

		[DllImport ("__Internal")]
		private static extern string _clientId ();

		[DllImport ("__Internal")]
		private static extern void _registerDeviceToken (string token);

		[DllImport ("__Internal")]
		private static extern void _destroy ();

		[DllImport ("__Internal")]
		private static extern int _status ();

		[DllImport ("__Internal")]
		private static extern void _resume ();

		//1.5.0+
		[DllImport ("__Internal")]
		private static extern void _setChannelId (string aChannelId);

		[DllImport ("__Internal")]
		private static extern void _bindAlias (string alias,string sequenceNum);

		[DllImport ("__Internal")]
		private static extern void _unBindAlias (string alias,string sequenceNum);

		[DllImport ("__Internal")]
		private static extern string _version ();

		[DllImport ("__Internal")]
		private static extern bool _setTag (string tags);

		[DllImport ("__Internal")]
		private static extern void _setPushMode (bool isValue);

		[DllImport ("__Internal")]
		private static extern void _voipRegistration ();

		/*************************************************************************************/
		/* Public interface for use inside C# / JS code */

		/*
		* 注册远程通知，兼容 iOS 8
		*/
		public static void registerUserNotification(){
			if (Application.platform != RuntimePlatform.OSXEditor){
				_registerUserNotification ();
			}
		}

		/**
		*  启动个推SDK
		*
		*  @param appid     设置app的个推appId，此appId从个推网站获取
		*  @param appKey    设置app的个推appKey，此appKey从个推网站获取
		*  @param appSecret 设置app的个推appSecret，此appSecret从个推网站获取
		*/
		public static void StartSDK(string appId, string appKey,string appSecret)
		{
			// Call plugin only when running on real device
			// 必须使用 RuntimePlatform.OSXEditor
			if (Application.platform != RuntimePlatform.OSXEditor){
				_StartSDK (appId, appKey, appSecret);
			}
		}

		/**
		*
		*  设置接收 UnitySendMessage 的 GameObject，如果不设置则默认设为 "Main Camera"
		*
		* <param name="gameObjectName">Game object name.</param>
		*/
		public static  void setListenerGameObject (string gameObjectName){
			if (Application.platform != RuntimePlatform.OSXEditor){
				_setListenerGameObject(gameObjectName);
			}
		}

		/*
		* 获取SDK运行状态
		* 0,  正在启动
		* 1,  启动
		* 2   停止
		*
		*/

		public static bool isPushTurnOn(){
			if (Application.platform != RuntimePlatform.OSXEditor) {
				int status = _status ();
				if (status == 1) {
					return true;
				} else
					return false;
			} else
				return false;
		}

		public static string getClientId()
		{
			// Call plugin only when running on real device
			if (Application.platform != RuntimePlatform.OSXEditor){
				return _clientId();
			}else
				return " ";
		}

		public static void registerDeviceToken (string token)
		{
			// Call plugin only when running on real device
			if (Application.platform != RuntimePlatform.OSXEditor){
				_registerDeviceToken(token);
			}
		}

		/**
	     *  设置渠道
	     *  备注：SDK可以未启动就调用该方法
	     *
	     *  SDK-1.5.0+
	     *
	     *  @param aChannelId 渠道值，可以为空值
	     */

		public static void setChannelId (string aChannelId)
		{
			// Call plugin only when running on real device
			if (Application.platform != RuntimePlatform.OSXEditor){
				_setChannelId (aChannelId);
			}
		}

		public static  void destroy (){
			// Call plugin only when running on real device
			if (Application.platform != RuntimePlatform.OSXEditor){
				_destroy();
			}
		}

		public static  void resume (){
			// Call plugin only when running on real device
			if (Application.platform != RuntimePlatform.OSXEditor){
				_resume();
			}
		}

		/**
		 *  绑定别名功能:后台可以根据别名进行推送
		 *
		 *  @param alias 别名字符串
		 *  @param aSn   绑定序列码, 不为nil
		 */

		public static  void bindAlias (string alias){
			if (Application.platform != RuntimePlatform.OSXEditor){
				_bindAlias(alias,"1");
			}
		}

		/**
		 *  取消绑定别名功能
		 *
		 *  @param alias 别名字符串
		 *  @param aSn   绑定序列码, 不为nil
		 */

		public static  void unBindAlias (string alias){
			// Call plugin only when running on real device
			if (Application.platform != RuntimePlatform.OSXEditor){
				_unBindAlias(alias,"1");
			}
		}

		public static string getVersion()
		{
			// Call plugin only when running on real device
			if (Application.platform != RuntimePlatform.OSXEditor){
				return _version();
			}else
				return " ";
		}

		/*
		* 设置开启和关闭推送，默认开启消息推送
		* true：开启 false：关闭
		*
		* <param name="isValue">If set to <c>true</c> is value.</param>
		*/

		public static  void setPushMode (bool isValue){
			// Call plugin only when running on real device
			if (Application.platform != RuntimePlatform.OSXEditor){
				_setPushMode(isValue);
			}
		}

		/*
		* tags数组，以字符串形式传入，并以”,“分隔每个tag，如："tag1,tag2,tag3"
		*
		* <param name="tags">Tags.</param>
		*/

		public static  bool setTag (string tags){
			if (Application.platform != RuntimePlatform.OSXEditor) {
				return _setTag (tags);
			} else
				return false;
		}

		public static  void voipRegistration (){
			// Call plugin only when running on real device
			if (Application.platform != RuntimePlatform.OSXEditor){
				_voipRegistration();
			}
		}
		#endif
	
	}
}
