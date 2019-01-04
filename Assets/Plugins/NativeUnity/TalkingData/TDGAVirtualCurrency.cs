using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

public class TDGAVirtualCurrency {
#if UNITY_IPHONE
	[DllImport ("__Internal")]
	private static extern void tdgaOnChargeRequst(string orderId, string iapId, double currencyAmount, 
			string currencyType, double virtualCurrencyAmount, string paymentType);
	
	[DllImport ("__Internal")]
	private static extern void tdgaOnChargSuccess(string orderId);
	
	[DllImport ("__Internal")]
	private static extern void tdgaOnReward(double virtualCurrencyAmount, string reason);
#elif UNITY_ANDROID
	private static string JAVA_CLASS = "com.tendcloud.tenddata.TDGAVirtualCurrency";
	static AndroidJavaClass agent = null;
	private static AndroidJavaClass GetAgent() {
		if (agent == null) {
			agent = new AndroidJavaClass(JAVA_CLASS);
		}
		return agent;
	}
#endif
	
	public static void OnChargeRequest(string orderId, string iapId, double currencyAmount,
			string currencyType, double virtualCurrencyAmount, string paymentType) {
		//if the platform is real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor) {
#if UNITY_IPHONE
			tdgaOnChargeRequst(orderId, iapId, currencyAmount, currencyType, virtualCurrencyAmount, paymentType);
#elif UNITY_ANDROID
			GetAgent().CallStatic("onChargeRequest", orderId, iapId, currencyAmount, 
					currencyType, virtualCurrencyAmount, paymentType);
#elif UNITY_WP8
			TalkingDataGAWP.TDGAVirtualCurrency.onChargeRequest(orderId, iapId, currencyAmount, 
					currencyType, virtualCurrencyAmount, paymentType);
#endif
		}
	}
	
	public static void OnChargeSuccess(string orderId) {
		//if the platform is real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor) {
#if UNITY_IPHONE
			tdgaOnChargSuccess(orderId);
#elif UNITY_ANDROID
			GetAgent().CallStatic("onChargeSuccess", orderId);
#elif UNITY_WP8
			TalkingDataGAWP.TDGAVirtualCurrency.onChargeSuccess(orderId);
#endif
		}
	}
	
	public static void OnReward(double virtualCurrencyAmount, string reason) {
		//if the platform is real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor) {
#if UNITY_IPHONE
			tdgaOnReward(virtualCurrencyAmount, reason);
#elif UNITY_ANDROID
			GetAgent().CallStatic("onReward", virtualCurrencyAmount, reason);
#elif UNITY_WP8
			TalkingDataGAWP.TDGAVirtualCurrency.onReward(virtualCurrencyAmount, reason);
#endif
		}
	}
}
