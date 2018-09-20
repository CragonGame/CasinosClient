using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class DCLevels
{
#if UNITY_EDITOR
#elif UNITY_ANDROID
	private static string JAVA_CLASS = "com.dataeye.plugin.DCLevels";
	private static AndroidJavaObject levels = new AndroidJavaClass(JAVA_CLASS);
#elif UNITY_IPHONE
	[DllImport("__Internal")]
	private static extern void dcLevelsBegin(string levelId);
	[DllImport("__Internal")]
	private static extern void dcLevelsComplete(string levelId);
	[DllImport("__Internal")]
	private static extern void dcLevelsFail(string levelId, string failPoint);
#endif
	
	public static void begin(string levelId)
	{
		if(levelId == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		levels.CallStatic("begin", levelId);
#elif UNITY_IPHONE
		dcLevelsBegin(levelId);
#elif UNITY_WP8
		DataEyeWP8.DCLevels.begin(levelId);
#endif
	}
	
	public static void complete(string levelId)
	{
		if(levelId == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		levels.CallStatic("complete", levelId);
#elif UNITY_IPHONE
		dcLevelsComplete(levelId);
#elif UNITY_WP8
		DataEyeWP8.DCLevels.complete(levelId);
#endif
	}
	
	public static void fail(string levelId, string failPoint)
	{
		if(levelId == null || failPoint == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		levels.CallStatic("fail", levelId, failPoint);
#elif UNITY_IPHONE
		dcLevelsFail(levelId, failPoint);
#elif UNITY_WP8
		DataEyeWP8.DCLevels.fail(levelId, failPoint);
#endif
	}
}

