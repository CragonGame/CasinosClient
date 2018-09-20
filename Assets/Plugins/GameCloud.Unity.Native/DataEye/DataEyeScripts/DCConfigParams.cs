using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class DCConfigParams
{
	public static string DATAEYE_CONFIG_PARAMS_UPDATE_COMPLETE = @"DataeyeConfigParamsComplete";
#if UNITY_EDITOR
#elif UNITY_ANDROID
	private static string JAVA_CLASS = "com.dataeye.DCUnityConfigParams";
	private static AndroidJavaObject config = new AndroidJavaClass(JAVA_CLASS);
#elif UNITY_IPHONE
	[DllImport("__Internal")]
	private static extern void dcUpdate(); 
	[DllImport("__Internal")]
	private static extern void dcSetCallBack(string objectName, string functionName); 
	[DllImport("__Internal")]
	private static extern string dcGetParamString(string key, string defaultValue);
	[DllImport("__Internal")]
	private static extern int dcGetParamInt(string key, int defaultValue);
	[DllImport("__Internal")]
	private static extern long dcGetParamLong(string key, long defaultValue);
	[DllImport("__Internal")]
	private static extern bool dcGetParamBool(string key, bool defaultValue);
#endif
	
	public DCConfigParams ()
	{
	}
	
	public static void update()
	{
#if UNITY_EDITOR
#elif UNITY_ANDROID
		config.CallStatic("update");
#elif UNITY_IPHONE
		dcUpdate();
#elif UNITY_WP8
		DataEyeWP8.DCConfigParams.update();
#endif
	}
	
	public static void setUpdateCallBack(string objectName, string functionName)
	{
		if(objectName == null || functionName == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		config.CallStatic("setUpdateSuccessCallBack", objectName, functionName);
#elif UNITY_IPHONE
		dcSetCallBack(objectName, functionName);
#endif
	}
	
	public static string getParameterString(string key, string defaultValue)
	{
		if(key == null || defaultValue == null)
		{
			return defaultValue;
		}
#if UNITY_EDITOR
		return defaultValue;
#elif UNITY_ANDROID
		return config.CallStatic<string>("getParameterString", key, defaultValue);
#elif UNITY_IPHONE
		return dcGetParamString(key, defaultValue);
#elif UNITY_WP8
		return DataEyeWP8.DCConfigParams.getParameterString(key, defaultValue);
#else
		return defaultValue;
#endif
	}
	
	public static int getParameterInt(string key, int defaultValue)
	{
		if(key == null)
		{
			return defaultValue;
		}
#if UNITY_EDITOR
		return defaultValue;
#elif UNITY_ANDROID
		return config.CallStatic<int>("getParameterInt", key, defaultValue);
#elif UNITY_IPHONE
		return dcGetParamInt(key, defaultValue);
#elif UNITY_WP8
		return DataEyeWP8.DCConfigParams.getParameterInt(key, defaultValue);
#else
		return defaultValue;
#endif
	}
	
	public static long getParameterLong(string key, long defaultValue)
	{
		if(key == null)
		{
			return defaultValue;
		}
#if UNITY_EDITOR
		return defaultValue;
#elif UNITY_ANDROID
		return config.CallStatic<int>("getParameterLong", key, defaultValue);
#elif UNITY_IPHONE
		return dcGetParamLong(key, defaultValue);
#elif UNITY_WP8
		return DataEyeWP8.DCConfigParams.getParameterLong(key, defaultValue);
#else
		return defaultValue;
#endif
	}
	
	public static bool getParameterBoolean(string key, Boolean defaultValue)
	{
		if(key == null)
		{
			return defaultValue;
		}
#if UNITY_EDITOR
		return defaultValue;
#elif UNITY_ANDROID
		return config.CallStatic<bool>("getParameterBoolean", key, defaultValue);
#elif UNITY_IPHONE
		return dcGetParamBool(key, defaultValue);
#elif UNITY_WP8
		return DataEyeWP8.DCConfigParams.getParameterBool(key, defaultValue);
#else
		return defaultValue;
#endif
	}
}

