using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class DCCoin
{
#if UNITY_EDITOR
#elif UNITY_ANDROID
	private static string JAVA_CLASS = "com.dataeye.DCCoin";
	private static AndroidJavaObject coin = new AndroidJavaObject(JAVA_CLASS);
#elif UNITY_IPHONE
	[DllImport("__Internal")]
	private static extern void dcSetCoinNum(long totalCoin, string coinType);
	[DllImport("__Internal")]
	private static extern void dcLost(string reason, string coinType, long lost, long left);
	[DllImport("__Internal")]
	private static extern void dcLostInLevel(string reason, string coinType, long lost, long left, string levelId);
	[DllImport("__Internal")]
	private static extern void dcGain(string reason, string coinType, long gain, long left);
	[DllImport("__Internal")]
	private static extern void dcGainInLevel(string reason, string coinType, long gain, long left, string levelId);
#endif
	
	public static void setCoinNum(long num, string coinType)
	{
		if(coinType == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		coin.CallStatic("setCoinNum", num, coinType);
#elif UNITY_IPHONE
		dcSetCoinNum(num, coinType);
#elif UNITY_WP8
		DataEyeWP8.DCCoin.setCoinNum(num, coinType);
#endif
	}
	
	public static void lost(string id, string coinType, long lost, long left)
	{
		if(id == null || coinType == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		coin.CallStatic("lost", id, coinType, lost, left);
#elif UNITY_IPHONE
		dcLost(id, coinType, lost, left);
#elif UNITY_WP8
		DataEyeWP8.DCCoin.lost(id, coinType, lost, left);
#endif
	}
	
	public static void lostInLevel(string id, string coinType, long lost, long left, string levelId)
	{
		if(id == null || coinType == null || levelId == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		coin.CallStatic("lostInLevel", id, coinType, lost, left, levelId);
#elif UNITY_IPHONE
		dcLostInLevel(id, coinType, lost, left, levelId);
#elif UNITY_WP8
		DataEyeWP8.DCCoin.lostInLevel(id, coinType, lost, left, levelId);
#endif
	}

	public static void gain(string id, string coinType, long gain, long left)
	{
		if(id == null || coinType == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		coin.CallStatic("gain", id, coinType, gain, left);
#elif UNITY_IPHONE
		dcGain(id, coinType, gain, left);
#elif UNITY_WP8
		DataEyeWP8.DCCoin.gain(id, coinType, gain, left);
#endif
	}

	public static void gainInLevel(string id, string coinType, long gain, long left, string levelId)
	{
		if(id == null || coinType == null || levelId == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		coin.CallStatic("gainInLevel", id, coinType, gain, left, levelId);
#elif UNITY_IPHONE
		dcGainInLevel(id, coinType, gain, left, levelId);
#elif UNITY_WP8
		DataEyeWP8.DCCoin.gainInLevel(id, coinType, gain, left, levelId);
#endif
	}
};
