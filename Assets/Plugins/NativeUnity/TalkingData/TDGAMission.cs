using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

public class TDGAMission {
#if UNITY_IPHONE
	[DllImport ("__Internal")]
	private static extern void tdgaOnBegin(string missionId);
	
	[DllImport ("__Internal")]
	private static extern void tdgaOnCompleted(string missionId);
	
	[DllImport ("__Internal")]
	private static extern void tdgaOnFailed(string missionId, string failedCause);
#elif UNITY_ANDROID
	private static string JAVA_CLASS = "com.tendcloud.tenddata.TDGAMission";
	static AndroidJavaClass agent = null;
	private static AndroidJavaClass GetAgent() {
		if (agent == null) {
			agent = new AndroidJavaClass(JAVA_CLASS);
		}
		return agent;
	}
#endif
	
	public static void OnBegin(string missionId) {
		//if the platform is real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor) {
#if UNITY_IPHONE
			tdgaOnBegin(missionId);
#elif UNITY_ANDROID
			GetAgent().CallStatic("onBegin", missionId);
#elif UNITY_WP8
			TalkingDataGAWP.TDGAMission.onBegin(missionId);
#endif
		}
	}
	
	public static void OnCompleted(string missionId) {
		//if the platform is real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor) {
#if UNITY_IPHONE
			tdgaOnCompleted(missionId);
#elif UNITY_ANDROID
			GetAgent().CallStatic("onCompleted", missionId);
#elif UNITY_WP8
			TalkingDataGAWP.TDGAMission.onCompleted(missionId);
#endif
		}
	}
	
	public static void OnFailed(string missionId, string failedCause) {
		//if the platform is real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor) {
#if UNITY_IPHONE
			tdgaOnFailed(missionId, failedCause);
#elif UNITY_ANDROID
			GetAgent().CallStatic("onFailed", missionId, failedCause);
#elif UNITY_WP8
			TalkingDataGAWP.TDGAMission.onFailed(missionId, failedCause);
#endif
		}
	}
}
