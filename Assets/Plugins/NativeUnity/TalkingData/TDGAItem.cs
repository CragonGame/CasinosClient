using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

public class TDGAItem {
#if UNITY_IPHONE
	[DllImport ("__Internal")]
	private static extern void tdgaOnPurchase(string item, int itemNumber, double priceInVirtualCurrency);
	
	[DllImport ("__Internal")]
	private static extern void tdgaOnUse(string item, int itemNumber);
#elif UNITY_ANDROID
	private static string JAVA_CLASS = "com.tendcloud.tenddata.TDGAItem";
	static AndroidJavaClass agent = null;
	private static AndroidJavaClass GetAgent() {
		if (agent == null) {
			agent = new AndroidJavaClass(JAVA_CLASS);
		}
		return agent;
	}
#endif
	
	public static void OnPurchase(string item, int itemNumber, double priceInVirtualCurrency) {
		//if the platform is real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor) {
#if UNITY_IPHONE
			tdgaOnPurchase(item, itemNumber, priceInVirtualCurrency);
#elif UNITY_ANDROID
			GetAgent().CallStatic("onPurchase", item, itemNumber, priceInVirtualCurrency);
#elif UNITY_WP8
			TalkingDataGAWP.TDGAItem.onPurchase(item, itemNumber, priceInVirtualCurrency);
#endif
		}
	}
	
	public static void OnUse(string item, int itemNumber) {
		//if the platform is real device
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor) {
#if UNITY_IPHONE
			tdgaOnUse(item, itemNumber);
#elif UNITY_ANDROID
			GetAgent().CallStatic("onUse", item, itemNumber);
#elif UNITY_WP8
			TalkingDataGAWP.TDGAItem.onUse(item, itemNumber);
#endif
		}
	}
}
