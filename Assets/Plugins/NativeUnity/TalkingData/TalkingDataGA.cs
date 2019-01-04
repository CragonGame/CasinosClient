// SDK Version: 3.2.14

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.Threading;

public class TalkingDataGA {
#if UNITY_IPHONE
	[DllImport ("__Internal")]
	private static extern void tdgaOnStart(string appId, string channelId);
	
	[DllImport ("__Internal")]
	private static extern void tdgaOnEvent(string eventId, string []keys, string []stringValues, double []numberValues, int count);
	
	[DllImport ("__Internal")]
	private static extern string tdgaGetDeviceId();
	
	[DllImport ("__Internal")]
	private static extern void tdgaSetSDKFramework(int tag);
	
	[DllImport ("__Internal")]
	private static extern void tdgaSetVerboseLogDisabled();
	
	[DllImport ("__Internal")]
	private static extern void tdgaSetDeviceToken(string deviceToken);
	
	[DllImport ("__Internal")]
	private static extern void tdgaHandlePushMessage(string message);
	
	private static bool hasTokenBeenObtained = false;
#elif UNITY_ANDROID
	//init static class --save memory/space
	private static AndroidJavaClass agent;
	private static AndroidJavaClass unityClass;
	
	private static string JAVA_CLASS = "com.tendcloud.tenddata.TalkingDataGA";
	private static string UNTIFY_CLASS = "com.unity3d.player.UnityPlayer";

	public static void AttachCurrentThread() {
		AndroidJNI.AttachCurrentThread();
	}
	
	public static void DetachCurrentThread() {
		AndroidJNI.DetachCurrentThread();
	}
#endif
	private static string deviceId = null;

	public static string GetDeviceId() {
		//if the platform is real device
		if (deviceId == null && Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor) {
#if UNITY_IPHONE
			deviceId = tdgaGetDeviceId();
#elif UNITY_ANDROID
			if (agent == null) {
				agent = new AndroidJavaClass(JAVA_CLASS);
			}
			AndroidJavaObject activity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
			deviceId = agent.CallStatic<string>("getDeviceId", activity);
#elif UNITY_WP8
			deviceId = TalkingDataGAWP.TalkingDataGA.getDeviceID();
#endif
		}
		return deviceId;
	}
	
	public static void OnStart(string appID, string channelId) {
		//if the platform is real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor) {
#if UNITY_IPHONE
//			tdgaSetSDKFramework(2);
			tdgaOnStart(appID, channelId);
#elif UNITY_ANDROID
			AndroidJavaClass dz = new AndroidJavaClass("com.tendcloud.tenddata.game.dz");
			dz.SetStatic<int>("a", 2);
			if (agent == null) {
				agent = new AndroidJavaClass(JAVA_CLASS);
			}
			// agent.SetStatic<int>("sPlatformType", 2);
			unityClass = new AndroidJavaClass(UNTIFY_CLASS);
			AndroidJavaObject activity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
			agent.CallStatic("init", activity, appID, channelId);
			agent.CallStatic("onResume", activity);
#elif UNITY_WP8
			TalkingDataGAWP.TalkingDataGA.init(appID, channelId);
#endif
		}
	}
	
	public static void OnEnd() {
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor) {
#if UNITY_ANDROID
			if (agent != null) {
				AndroidJavaObject activity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
				agent.CallStatic("onPause", activity);
				agent = null;
				unityClass = null;
			}
#endif
		}
	}
	
	public static void OnKill() {
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor) {
#if UNITY_ANDROID
			if (agent != null) {
				AndroidJavaObject activity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
				agent.CallStatic("onKill", activity);
				agent = null;
				unityClass = null;
			}
#endif
		}
	}
	
	public static void OnEvent(string actionId, Dictionary<string, object> parameters) {
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor) {
#if UNITY_IPHONE
			if (parameters != null && parameters.Count > 0) {
				int count = parameters.Count;
				string []keys = new string[count];
				string []stringValues = new string[count];
				double []numberValues = new double[count];
				int index = 0;
				foreach (KeyValuePair<string, object> kvp in parameters) {
					if (kvp.Value is string) {
						keys[index] = kvp.Key;
						stringValues[index] = (string)kvp.Value;
					} else {
						try {
							double tmp = System.Convert.ToDouble(kvp.Value);
							numberValues[index] = tmp;
							keys[index] = kvp.Key;
						} catch(System.Exception) {
							count--;
							continue;
						}
					}
					index++;
				}
				
				tdgaOnEvent(actionId, keys, stringValues, numberValues, count);
			} else {
				tdgaOnEvent(actionId, null, null, null, 0);
			}
#elif UNITY_ANDROID
			if (parameters != null && parameters.Count > 0) {
				int count = parameters.Count;
				AndroidJavaObject map = new AndroidJavaObject("java.util.HashMap", count);
				IntPtr method_Put = AndroidJNIHelper.GetMethodID(map.GetRawClass(), 
						"put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
				object[] args = new object[2];
				foreach (KeyValuePair<string, object> kvp in parameters) {
					args[0] = new AndroidJavaObject("java.lang.String", kvp.Key);
					if (typeof(System.String).IsInstanceOfType(kvp.Value)) {
						args[1] = new AndroidJavaObject("java.lang.String", kvp.Value);
					} else {
						args[1] = new AndroidJavaObject("java.lang.Double", ""+kvp.Value);
					}
					AndroidJNI.CallObjectMethod(map.GetRawObject(), method_Put, AndroidJNIHelper.CreateJNIArgArray(args));
				}
				
				if (agent != null) {
					agent.CallStatic("onEvent", actionId, map);
				}
			} else {
				if (agent != null) {
					agent.CallStatic("onEvent", actionId, null);
				}
			}
#elif UNITY_WP8
				TalkingDataGAWP.TalkingDataGA.onEvent(actionId, parameters);
#endif
		}
	}
	
	public static void SetVerboseLogDisabled() {
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor) {
#if UNITY_IPHONE
			tdgaSetVerboseLogDisabled();
#elif UNITY_ANDROID
			if (agent == null) {
				agent = new AndroidJavaClass(JAVA_CLASS);
			}
			agent.CallStatic("setVerboseLogDisabled");
#elif UNITY_WP8
#endif
		}
	}

#if UNITY_IPHONE
#if UNITY_5 || UNITY_5_6_OR_NEWER
	public static void SetDeviceToken() {
		if (!hasTokenBeenObtained) {
			byte[] byteToken = UnityEngine.iOS.NotificationServices.deviceToken;
			if(byteToken != null) {
				string deviceToken = System.BitConverter.ToString(byteToken).Replace("-","");
				tdgaSetDeviceToken(deviceToken);
				hasTokenBeenObtained = true;
			}
		}
	}
	
	public static void HandlePushMessage() {
		UnityEngine.iOS.RemoteNotification[] notifications = UnityEngine.iOS.NotificationServices.remoteNotifications;
		if (notifications != null) {
			UnityEngine.iOS.NotificationServices.ClearRemoteNotifications();
			foreach (UnityEngine.iOS.RemoteNotification rn in notifications) {
				foreach (DictionaryEntry de in rn.userInfo) {
					if (de.Key.ToString().Equals("sign")) {
						string sign = de.Value.ToString();
						tdgaHandlePushMessage(sign);
					}
				}
			}
		}
	}
#else
	public static void SetDeviceToken() {
		if (!hasTokenBeenObtained) {
			byte[] byteToken = NotificationServices.deviceToken;
			if(byteToken != null) {
				string deviceToken = System.BitConverter.ToString(byteToken).Replace("-","");
				tdgaSetDeviceToken(deviceToken);
				hasTokenBeenObtained = true;
			}
		}
	}

	public static void HandlePushMessage() {
		RemoteNotification[] notifications = NotificationServices.remoteNotifications;
		if (notifications != null) {
			NotificationServices.ClearRemoteNotifications();
			foreach (RemoteNotification rn in notifications) {
				foreach (DictionaryEntry de in rn.userInfo) {
					if (de.Key.ToString().Equals("sign")) {
						string sign = de.Value.ToString();
						tdgaHandlePushMessage(sign);
					}
				}
			}
		}
	}
#endif
#endif
}
