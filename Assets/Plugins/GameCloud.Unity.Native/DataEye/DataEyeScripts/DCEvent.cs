using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
#if UNITY_EDITOR
#elif UNITY_IPHONE
using LitJson;
#endif

public class DCEvent
{
#if UNITY_EDITOR
#elif UNITY_ANDROID
	private static string JAVA_CLASS = "com.dataeye.DCEvent";
	private static AndroidJavaObject dcEvent = new AndroidJavaObject(JAVA_CLASS);
#elif UNITY_IPHONE
	[DllImport("__Internal")]
	private static extern void dcOnEventBeforeLogin(string eventId, string json, long duration);
	[DllImport("__Internal")]
	private static extern void dcOnEventDuration(string eventId, long duration);
	[DllImport("__Internal")]
	private static extern void dcOnEventLabelDuration(string eventId, string label, long duration);
	[DllImport("__Internal")]
	private static extern void dcOnEventMapDuration(string eventId,  string json, long duration);
	[DllImport("__Internal")]
	private static extern void dcOnEventCount(string eventId, int count);
	[DllImport("__Internal")]
	private static extern void dcOnEvent(string eventId);
	[DllImport("__Internal")]
	private static extern void dcOnEventLabel(string eventId, string label);
	[DllImport("__Internal")]
	private static extern void dcOnEventMap(string eventId, string json);
	[DllImport("__Internal")]
	private static extern void dcOnEventBegin(string eventId);
	[DllImport("__Internal")]
	private static extern void dcOnEventBeginMap(string eventId, string data);
	[DllImport("__Internal")]
	private static extern void dcOnEventBeginFlag(string eventId, string data, string flag);
	[DllImport("__Internal")]
	private static extern void dcOnEventEnd(string eventId);
	[DllImport("__Internal")]
	private static extern void dcOnEventEndFlag(string eventId, string flag);
#endif
	
	public static void onEventBeforeLogin(string eventId, Dictionary<string, string> dictionary, long duration)
	{
		if(eventId == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		AndroidJavaObject map = dicToMap(dictionary);
		dcEvent.CallStatic("onEventBeforeLogin", eventId, map, duration);
#elif UNITY_IPHONE
		string json = LitJson.JsonMapper.ToJson(dictionary);
		dcOnEventBeforeLogin(eventId, json, duration);
#elif UNITY_WP8
		DataEyeWP8.DCEvent.onEventBeforeLogin(eventId, dictionary, duration);
#endif
	}
	
	public static void onEventCount(string eventId, int count)
	{
		if(eventId == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		dcEvent.CallStatic("onEventCount", eventId, count);
#elif UNITY_IPHONE
		dcOnEventCount(eventId, count);
#elif UNITY_WP8
		DataEyeWP8.DCEvent.onEventCount(eventId, count);
#endif
	}
	
	public static void onEvent(string eventId)
	{
		if(eventId == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		dcEvent.CallStatic("onEvent", eventId);
#elif UNITY_IPHONE
		dcOnEvent(eventId);
#elif UNITY_WP8
		DataEyeWP8.DCEvent.onEvent(eventId);
#endif
	}
	
	public static void onEvent(string eventId, string label)
	{
		if(eventId == null || label == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		dcEvent.CallStatic("onEvent", eventId, label);
#elif UNITY_IPHONE
		dcOnEventLabel(eventId, label);
#elif UNITY_WP8
		DataEyeWP8.DCEvent.onEvent(eventId, label);
#endif
	}
	
	public static void onEvent(string eventId, Dictionary<string, string> dictionary)
	{
		if(eventId == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		AndroidJavaObject map = dicToMap(dictionary);
		dcEvent.CallStatic("onEvent", eventId, map);
#elif UNITY_IPHONE
		string json = LitJson.JsonMapper.ToJson(dictionary);
		if(json == null)
		{
			return;
		}
		dcOnEventMap(eventId, json);
#elif UNITY_WP8
		DataEyeWP8.DCEvent.onEvent(eventId, dictionary);
#endif
	}

	public static void onEventDuration(string eventId, long duration)
	{
		if(eventId == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		dcEvent.CallStatic("onEventDuration", eventId, duration);
#elif UNITY_IPHONE
		dcOnEventDuration(eventId, duration);
#elif UNITY_WP8
		DataEyeWP8.DCEvent.onEventDuration(eventId, duration);
#endif
	}
	
	public static void onEventDuration(string eventId, string label, long duration)
	{
		if(eventId == null || label == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		dcEvent.CallStatic("onEventDuration", eventId, label, duration);
#elif UNITY_IPHONE
		dcOnEventLabelDuration(eventId, label, duration);
#elif UNITY_WP8
		DataEyeWP8.DCEvent.onEventDuration(eventId, label, duration);
#endif
	}
	
	public static void onEventDuration(string eventId, Dictionary<string, string> dictionary, long duration)
	{
		if(eventId == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		AndroidJavaObject map = dicToMap(dictionary);
		Debug.Log("entry eventDuration3");
		dcEvent.CallStatic("onEventDuration", eventId, map, duration);
#elif UNITY_IPHONE
		string json = LitJson.JsonMapper.ToJson(dictionary);
		if(json == null)
		{
			return;
		}
		dcOnEventMapDuration(eventId, json, duration);
#elif UNITY_WP8
		DataEyeWP8.DCEvent.onEventDuration(eventId, dictionary, duration);
#endif
	}
	
	public static void onEventBegin(string eventId)
	{
		if(eventId == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		dcEvent.CallStatic("onEventBegin", eventId);
#elif UNITY_IPHONE
		dcOnEventBegin(eventId);
#elif UNITY_WP8
		DataEyeWP8.DCEvent.onEventBegin(eventId);
#endif
	}
	
	public static void onEventBegin(string eventId, Dictionary<string, string> dictionary)
	{
		if(eventId == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		AndroidJavaObject map = dicToMap(dictionary);
		dcEvent.CallStatic("onEventBegin", eventId, map);
#elif UNITY_IPHONE
		string json = LitJson.JsonMapper.ToJson(dictionary);
		if(json == null)
		{
			return;
		}
		dcOnEventBeginMap(eventId, json);
#elif UNITY_WP8
		DataEyeWP8.DCEvent.onEventBegin(eventId, dictionary);
#endif
	}
	
	public static void onEventEnd(string eventId)
	{
		if(eventId == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		dcEvent.CallStatic("onEventEnd", eventId);
#elif UNITY_IPHONE
		dcOnEventEnd(eventId);
#elif UNITY_WP8
		DataEyeWP8.DCEvent.onEventEnd(eventId);
#endif
	}
	
	public static void onEventBegin(string eventId, Dictionary<string, string> dictionary, string flag)
	{
		if(eventId == null || flag == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		AndroidJavaObject map = dicToMap(dictionary);
		dcEvent.CallStatic("onEventBegin", eventId, map, flag);
#elif UNITY_IPHONE
		string json = LitJson.JsonMapper.ToJson(dictionary);
		if(json == null)
		{
			return;
		}
		dcOnEventBeginFlag(eventId, json, flag);
#elif UNITY_WP8
		DataEyeWP8.DCEvent.onEventBegin(eventId, dictionary, flag);
#endif
	}
	
	public static void onEventEnd(string eventId, string flag)
	{
		if(eventId == null || flag == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		dcEvent.CallStatic("onEventEnd", eventId, flag);
#elif UNITY_IPHONE
		dcOnEventEndFlag(eventId, flag);
#elif UNITY_WP8
		DataEyeWP8.DCEvent.onEventEnd(eventId, flag);
#endif
	}

#if UNITY_EDITOR
#elif UNITY_ANDROID
	public static AndroidJavaObject dicToMap(Dictionary<string, string> dictionary)
	{
		if(dictionary == null)
		{
			return null;
		}
		AndroidJavaObject map = new AndroidJavaObject("java.util.HashMap");
		foreach(KeyValuePair<string, string> pair in dictionary)
		{
			map.Call<string>("put", pair.Key, pair.Value);
		}
		return map;
	}
#endif
}

