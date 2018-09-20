using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class DCVirtualCurrency
{
#if UNITY_EDITOR
#elif UNITY_ANDROID
	private static string JAVA_CLASS = "com.dataeye.DCVirtualCurrency";
	private static AndroidJavaObject virtualCurrency = new AndroidJavaObject(JAVA_CLASS);
#elif UNITY_IPHONE
	[DllImport("__Internal")]
	private static extern void dcPaymentSuccess(string orderId, string iapId, double currencyAmount, string currencyType, string paymentType);
	[DllImport("__Internal")]
	private static extern void dcPaymentSuccessInLevel(string orderId, string iapId, double currencyAmount, string currencyType, string paymentType, string levelId);
#endif

	public static void paymentSuccess(string orderId, string iapId, double currencyAmount, string currencyType, string paymentType)
	{
		if(orderId == null || iapId == null || currencyType == null || paymentType == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		virtualCurrency.CallStatic("paymentSuccess", orderId, iapId, currencyAmount, currencyType, paymentType);
#elif UNITY_IPHONE
		dcPaymentSuccess(orderId, iapId, currencyAmount, currencyType, paymentType);
#elif UNITY_WP8
		DataEyeWP8.DCVirtualCurrency.paymentSuccess(orderId, iapId, currencyAmount, currencyType, paymentType);
#endif
	}

	public static void paymentSuccessInLevel(string orderId, string iapId, double currencyAmount, string currencyType, string paymentType, string levelId)
	{
		if(orderId == null || iapId == null || currencyType == null || paymentType == null || levelId == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		virtualCurrency.CallStatic("paymentSuccessInLevel", orderId, iapId, currencyAmount, currencyType, paymentType, levelId);
#elif UNITY_IPHONE
		dcPaymentSuccessInLevel(orderId, iapId, currencyAmount, currencyType, paymentType, levelId);
#elif UNITY_WP8
		DataEyeWP8.DCVirtualCurrency.paymentSuccessInLevel(orderId, iapId, currencyAmount, currencyType, paymentType, levelId);
#endif
	}
};
