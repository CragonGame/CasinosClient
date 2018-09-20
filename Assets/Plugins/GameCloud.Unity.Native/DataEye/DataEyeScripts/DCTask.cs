using System;
using System.Runtime.InteropServices;
using UnityEngine;

public enum DCTaskType
{
	DC_GuideLine = 1,
	DC_MainLine,
	DC_BranchLine,
	DC_Daily,
	DC_Activity,
	DC_Other
};

public class DCTask
{
#if UNITY_EDITOR
#elif UNITY_ANDROID
	private static string JAVA_CLASS = "com.dataeye.DCTask";
	private static AndroidJavaObject task = new AndroidJavaClass(JAVA_CLASS);
#elif UNITY_IPHONE
	[DllImport("__Internal")]
	private static extern void dcTaskBegin(string taskId, int taskType);
	[DllImport("__Internal")]
	private static extern void dcTaskComplete(string taskId);
	[DllImport("__Internal")]
	private static extern void dcTaskFail(string taskId, string reason);
#endif
	
	public static void begin(string taskId, DCTaskType taskType)
	{
		if(taskId == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		task.CallStatic("begin", taskId, (int)taskType);
		Debug.Log("begin");
#elif UNITY_IPHONE
		dcTaskBegin(taskId, (int)taskType);
#elif UNITY_WP8
		DataEyeWP8.DCTask.begin(taskId, (DataEyeWP8.DCTaskType)taskType);
#endif
	}
	
	public static void complete(string taskId)
	{
		if(taskId == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		task.CallStatic("complete", taskId);
#elif UNITY_IPHONE
		dcTaskComplete(taskId);
#elif UNITY_WP8
		DataEyeWP8.DCTask.complete(taskId);
#endif
	}
	
	public static void fail(string taskId, string reason)
	{
		if(taskId == null || reason == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		task.CallStatic("fail", taskId, reason);
#elif UNITY_IPHONE
		dcTaskFail(taskId, reason);
#elif UNITY_WP8
		DataEyeWP8.DCTask.fail(taskId, reason);
#endif
	}
}

