using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class DCItem
{
#if UNITY_EDITOR
#elif UNITY_ANDROID
	private static string JAVA_CLASS = "com.dataeye.DCItem";
	private static AndroidJavaObject item = new AndroidJavaClass(JAVA_CLASS);
#elif UNITY_IPHONE
	[DllImport("__Internal")]
	private static extern void dcItemBuy(string itemId, string itemType, int itemCount, long virturalCurrency, string currencyType, string consumePoint);
	[DllImport("__Internal")]
	private static extern void dcItemBuyInLevel(string itemId, string itemType, int itemCount, long virturalCurrency, string currencyType, string consumePoint, string levelId);
	[DllImport("__Internal")]
	private static extern void dcItemGet(string itemId, string itemType, int itemCount, string reason);
	[DllImport("__Internal")]
	private static extern void dcItemGetInLevel(string itemId, string itemType, int itemCount, string reason, string levelId);
	[DllImport("__Internal")]
	private static extern void dcItemUse(string itemId, string itemType, int itemCount, string reason);
	[DllImport("__Internal")]
	private static extern void dcItemUseInLevel(string itemId, string itemType, int itemCount, string reason, string levelId);
#endif
	
	public static void buy(string itemId, string itemType, int itemCount, long virturalCurrency, string currencyType, string consumePoint)
	{
		if(itemId == null || itemType == null || currencyType == null || consumePoint == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		item.CallStatic("buy", itemId, itemType, itemCount, virturalCurrency, currencyType, consumePoint);
#elif UNITY_IPHONE
		dcItemBuy(itemId, itemType, itemCount, virturalCurrency, currencyType, consumePoint);
#elif UNITY_WP8
		DataEyeWP8.DCItem.buy(itemId, itemType, itemCount, virturalCurrency, currencyType, consumePoint);
#endif
	}
	
	public static void buyInLevel(string itemId, string itemType, int itemCount, long virturalCurrency, string currencyType, string consumePoint, string levelId)
	{
		if(itemId == null || itemType == null || currencyType == null || consumePoint == null || levelId == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		item.CallStatic("buyInLevel", itemId, itemType, itemCount, virturalCurrency, currencyType, consumePoint, levelId);
#elif UNITY_IPHONE
		dcItemBuyInLevel(itemId, itemType, itemCount, virturalCurrency, currencyType, consumePoint, levelId);
#elif UNITY_WP8
		DataEyeWP8.DCItem.buyInLevel(itemId, itemType, itemCount, virturalCurrency, currencyType, consumePoint, levelId);
#endif
	}
	
	public static void get(string itemId, string itemType, int itemCount, string reason)
	{
		if(itemId == null || itemType == null || reason == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		item.CallStatic("get", itemId, itemType, itemCount, reason);
#elif UNITY_IPHONE
		dcItemGet(itemId, itemType, itemCount, reason);
#elif UNITY_WP8
		DataEyeWP8.DCItem.get(itemId, itemType, itemCount, reason);
#endif
	}
	
	public static void getInLevel(string itemId, string itemType, int itemCount, string reason, string levelId)
	{
		if(itemId == null || itemType == null || reason == null || levelId == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		item.CallStatic("getInLevel", itemId, itemType, itemCount, reason, levelId);
#elif UNITY_IPHONE
		dcItemGetInLevel(itemId, itemType, itemCount, reason, levelId);
#elif UNITY_WP8
		DataEyeWP8.DCItem.getInLevel(itemId, itemType, itemCount, reason, levelId);
#endif
	}
	
	public static void consume(string itemId, string itemType, int itemCount, string reason)
	{
		if(itemId == null || itemType == null || reason == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		item.CallStatic("consume", itemId, itemType, itemCount, reason);
#elif UNITY_IPHONE
		dcItemUse(itemId, itemType, itemCount, reason);
#elif UNITY_WP8
		DataEyeWP8.DCItem.consume(itemId, itemType, itemCount, reason);
#endif
	}

	public static void consumeInLevel(string itemId, string itemType, int itemCount, string reason, string levelId)
	{
		if(itemId == null || itemType == null || reason == null || levelId == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		item.CallStatic("consumeInLevel", itemId, itemType, itemCount, reason, levelId);
#elif UNITY_IPHONE
		dcItemUseInLevel(itemId, itemType, itemCount, reason, levelId);
#elif UNITY_WP8
		DataEyeWP8.DCItem.consumeInLevel(itemId, itemType, itemCount, reason, levelId);
#endif
	}
}

