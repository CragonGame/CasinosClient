using System;
using System.Runtime.InteropServices;
using UnityEngine;

public enum DCReportMode
{
	DC_DEFAULT = 1,
	DC_AFTER_LOGIN
};

public class DCAgent : MonoBehaviour
{

	static DCAgent instance;

#if UNITY_EDITOR
#elif UNITY_ANDROID
	private static string JAVA_CLASS = "com.dataeye.DCAgent";
	private static string UNITY_CLASS = "com.unity3d.player.UnityPlayer";
	private static AndroidJavaObject agent = new AndroidJavaClass(JAVA_CLASS);
#elif UNITY_IPHONE
	[DllImport("__Internal")]
	private static extern void dcOnStart(string appId, string channelId); 
	[DllImport("__Internal")]
	private static extern void dcSetDebugMode(bool mode); 
	[DllImport("__Internal")]
	private static extern void dcSetReportMode(int mode);
	[DllImport("__Internal")]
	private static extern void dcSetUploadTime(UInt32 time);
	[DllImport("__Internal")]
	private static extern void dcSetVersion(string version);
	[DllImport("__Internal")]
	private static extern void dcReportError(string title, string error);
	[DllImport("__Internal")]
	private static extern void dcUploadNow();
	[DllImport("__Internal")]
	private static extern string dcGetUID();
#endif

	static DCAgent()
	{
	}

	public static DCAgent getInstance()
	{
		if(!instance)
		{
			GameObject go = new GameObject("DCAgent");
			DontDestroyOnLoad(go);
			instance = go.AddComponent<DCAgent>();
		}
		return instance;
	}

	public void initWithAppIdAndChannelId(string appId, string channelId)
	{
		if(appId == null || channelId == null)
		{
			return;
		}
//		DCAgent.setUploadInterval(90);
//		DCAgent.setReportMode(DCReportMode.DC_AFTER_LOGIN);
//		DCAgent.setDebugMode(true);
#if UNITY_EDITOR
#elif UNITY_ANDROID
		//if you want set appID and channelId through code, you can use function initConfig
		//if params appId is different from the DC_APPID value in androidmanifest.xml, SDK
		//would take androidmanifest.xml as stand
		instance.initConfig(appId, channelId);
		instance.onResume();
#elif UNITY_IPHONE
		instance.onStart(appId, channelId);
#endif
	}
	
	void OnApplicationPause(bool pauseStatus)
	{
#if UNITY_EDITOR
#elif UNITY_ANDROID
		if(pauseStatus)
		{
			instance.onPause();
		}
		else
		{
			instance.onResume();
		}
#endif
	}

	public static void setReportMode(DCReportMode mode)
	{
#if UNITY_EDITOR
#elif UNITY_ANDROID
		agent.CallStatic("setReportMode", (int)mode);
#elif UNITY_IPHONE
		dcSetReportMode((int)mode);
#elif UNITY_WP8
		DataEyeWP8.DCAgent.setReportMode((DataEyeWP8.DCReportMode)mode);
#endif
	}

	public static void setUploadInterval(UInt32 time)
	{
#if UNITY_EDITOR
#elif UNITY_ANDROID
		agent.CallStatic("setUploadInterval", (int)time);
#elif UNITY_IPHONE
		dcSetUploadTime(time);
#elif UNITY_WP8
		DataEyeWP8.DCAgent.setUploadInterval(time);
#endif
	}
	
	public static void setVersion(string appVersion)
	{
		if(appVersion == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		agent.CallStatic("setVersion", appVersion);
#elif UNITY_IPHONE
		dcSetVersion(appVersion);
#elif UNITY_WP8
		DataEyeWP8.DCAgent.setVersion(appVersion);
#endif
	}

	public static void reportError(string titile, string error)
	{
		if(titile == null || error == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		agent.CallStatic("reportError", titile, error);
#elif UNITY_IPHONE
		dcReportError(titile, error);
#elif UNITY_WP8
		DataEyeWP8.DCAgent.reportError(titile, error);
#endif
	}
	
#if UNITY_EDITOR
#elif UNITY_ANDROID
	public static void attachCurrentThread()
	{
		AndroidJNI.AttachCurrentThread();
	}
	
	public static void detachCurrentThread()
	{
		AndroidJNI.DetachCurrentThread();
	}

	void initConfig(string appId, string channelId)
	{
		AndroidJavaClass unityClass = new AndroidJavaClass(UNITY_CLASS);
		AndroidJavaObject activity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
		agent.CallStatic("initConfig", activity, appId, channelId);
	}

	void onResume()
	{
		AndroidJavaClass unityClass = new AndroidJavaClass(UNITY_CLASS);
		AndroidJavaObject activity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
		agent.CallStatic("onResume", activity);
	}
	
	void onPause()
	{
		AndroidJavaClass unityClass = new AndroidJavaClass(UNITY_CLASS);
		AndroidJavaObject activity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
		agent.CallStatic("onPause", activity);
	}
	
	public static void onKillProcessOrExit()
	{
		agent.CallStatic("onKillProcessOrExit");
	}
#endif

#if UNITY_EDITOR
#elif UNITY_IPHONE
	void onStart(string appId, string channelId)
	{
#if UNITY_IPHONE
		dcOnStart(appId, channelId);
#endif
	}
#endif

	public static void setDebugMode(bool mode)
	{
#if UNITY_EDITOR
#elif UNITY_ANDROID
		agent.CallStatic("setDebugMode", mode);
#elif UNITY_IPHONE
		dcSetDebugMode(mode);
#elif UNITY_WP8
		//DataEyeWP8.DCAgent.setDebugMode(mode);
#endif
	}

	public static void uploadNow()
	{
#if UNITY_EDITOR
#elif UNITY_ANDROID
		agent.CallStatic("uploadNow");
#elif UNITY_IPHONE
		dcUploadNow();
#elif UNITY_WP8
		DataEyeWP8.DCAgent.uploadNow();
#endif
	}

	public static string getUID()
	{
#if UNITY_EDITOR
		return null;
#elif UNITY_ANDROID
		return agent.CallStatic<string>("getUID");
#elif UNITY_IPHONE
		return dcGetUID();
#elif UNITY_WP8
		return DataEyeWP8.DCAgent.getUID();
#else
		return null;
#endif
	}
	
}

