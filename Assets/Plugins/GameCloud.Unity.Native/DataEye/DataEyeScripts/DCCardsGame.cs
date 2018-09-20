using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class DCCardsGame
{
#if UNITY_EDITOR
#elif UNITY_ANDROID
	private static string JAVA_CLASS = "com.dataeye.plugin.DCCardsGame";
	private static AndroidJavaObject cardgame = new AndroidJavaObject(JAVA_CLASS);
#elif UNITY_IPHONE
	[DllImport("__Internal")]
	private static extern void dcCardGamePlay(string roomId, string roomType, string coinType, long lostOrGainCoin, long tax, long left);
	[DllImport("__Internal")]
	private static extern void dcCardGameLost(string roomId, string roomType, string coinType, long lost, long left);
	[DllImport("__Internal")]
	private static extern void dcCardGameGain(string roomId, string roomType, string coinType, long gain, long left);
#endif
	
	public static void play(string roomId, string roomType, string coinType, long lostOrGainCoin, long tax, long left)
	{
		if(roomId == null || roomType == null || coinType == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		cardgame.CallStatic("play", roomId, roomType, coinType, lostOrGainCoin, tax, left);
#elif UNITY_IPHONE
		dcCardGamePlay(roomId, roomType, coinType, lostOrGainCoin, tax, left);
#elif UNITY_WP8
		DataEyeWP8.DCCardsGame.play(roomId, roomType, coinType, lostOrGainCoin, tax, left);
#endif
	}
	
	public static void lost(string roomId, string roomType, string coinType, long lost, long left)
	{
		if(roomId == null || roomType == null || coinType == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		cardgame.CallStatic("lost", roomId, roomType, coinType, lost, left);
#elif UNITY_IPHONE
		dcCardGameLost(roomId, roomType, coinType, lost, left);
#elif UNITY_WP8
		DataEyeWP8.DCCardsGame.lost(roomId, roomType, coinType, lost, left);
#endif
	}
	
	public static void gain(string roomId, string roomType, string coinType, long gain, long left)
	{
		if(roomId == null || roomType == null || coinType == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		cardgame.CallStatic("gain", roomId, roomType, coinType, gain, left);
#elif UNITY_IPHONE
		dcCardGameGain(roomId, roomType, coinType, gain, left);
#elif UNITY_WP8
		DataEyeWP8.DCCardsGame.gain(roomId, roomType, coinType, gain, left);
#endif
	}
}

