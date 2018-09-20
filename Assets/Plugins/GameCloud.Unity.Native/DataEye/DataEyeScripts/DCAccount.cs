using System;
using System.Runtime.InteropServices;
using UnityEngine;

public enum DCAccountType
{
	DC_Anonymous = 0,
	DC_Registered,
	DC_SianWeibo,
	DC_QQ,
	DC_TencentWeibo,
	DC_ND91,
	DC_Type1,
	DC_Type2,
	DC_Type3,
	DC_Type4,
	DC_Type5,
	DC_Type6,
	DC_Type7,
	DC_Type8,
	DC_Type9,
	DC_Type10
};

public enum DCGender
{
	DC_UNKNOWN = 0,
	DC_MALE,
	DC_FEMALE
};

public class DCAccount
{
#if UNITY_EDITOR
#elif UNITY_ANDROID
	private static string JAVA_CLASS = "com.dataeye.DCAccount";
	private static AndroidJavaObject account = new AndroidJavaObject(JAVA_CLASS);
#elif UNITY_IPHONE
	[DllImport("__Internal")]
	private static extern void dcLogin(string accountId);
	[DllImport("__Internal")]
	private static extern void dcLogin1(string accountId, string gameServer);
	[DllImport("__Internal")]
	private static extern void dcLogout();
	[DllImport("__Internal")]
	private static extern string dcGetAccountId();
	[DllImport("__Internal")]
	private static extern void dcSetAccountType(int type);
	[DllImport("__Internal")]
	private static extern void dcSetLevel(int level);
	[DllImport("__Internal")]
	private static extern void dcSetAge(int age);
	[DllImport("__Internal")]
	private static extern void dcSetGender(int gender);
	[DllImport("__Internal")]
	private static extern void dcSetGameServer(string gameServer);
	[DllImport("__Internal")]
	private static extern void dcAddTag(string tag, string subTag);
	[DllImport("__Internal")]
	private static extern void dcRemoveTag(string tag, string subTag);
#endif
	
	public static void login(string accountId)
	{
		if(accountId == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		account.CallStatic("login", accountId);
#elif UNITY_IPHONE
		dcLogin(accountId);
#elif UNITY_WP8
		DataEyeWP8.DCAccount.login(accountId);
#endif
	}
	
	public static void login(string accountId, string gameServer)
	{
		if(accountId == null || gameServer == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		account.CallStatic("login", accountId, gameServer);
#elif UNITY_IPHONE
		dcLogin1(accountId, gameServer);
#elif UNITY_WP8
		DataEyeWP8.DCAccount.login(accountId, gameServer);
#endif
	}
	
	public static void logout()
	{
#if UNITY_EDITOR
#elif UNITY_ANDROID
		account.CallStatic("logout");
#elif UNITY_IPHONE
		dcLogout();
#elif UNITY_WP8
		DataEyeWP8.DCAccount.logout();
#endif
	}
	
	public static string getAccountId()
	{
#if UNITY_EDITOR
		return null;
#elif UNITY_ANDROID
		return account.CallStatic<string>("getAccountId");
#elif UNITY_IPHONE
		return dcGetAccountId();
#elif UNITY_WP8
		return DataEyeWP8.DCAccount.getAccountId();
#else
		return null;
#endif
	}
	
	public static void setAccountType(DCAccountType type)
	{
#if UNITY_EDITOR
#elif UNITY_ANDROID
		account.CallStatic("setAccountType", (int)type);
#elif UNITY_IPHONE
		dcSetAccountType((int)type);
#elif UNITY_WP8
		DataEyeWP8.DCAccount.setAccountType((DataEyeWP8.DCAccountType)type);
#endif
	}
	
	public static void setLevel(int level)
	{
#if UNITY_EDITOR
#elif UNITY_ANDROID
		account.CallStatic("setLevel", level);
#elif UNITY_IPHONE
		dcSetLevel(level);
#elif UNITY_WP8
		DataEyeWP8.DCAccount.setLevel(level);
#endif
	}
	
	public static void setGender(DCGender gender)
	{
#if UNITY_EDITOR
#elif UNITY_ANDROID
		account.CallStatic("setGender", (int)gender);
#elif UNITY_IPHONE
		dcSetGender((int)gender);
#elif UNITY_WP8
		DataEyeWP8.DCAccount.setGender((DataEyeWP8.DCGender)gender);
#endif
	}
	
	public static void setAge(int age)
	{
#if UNITY_EDITOR
#elif UNITY_ANDROID
		account.CallStatic("setAge", age);
#elif UNITY_IPHONE
		dcSetAge(age);
#elif UNITY_WP8
		DataEyeWP8.DCAccount.setAge(age);
#endif
	}
	
	public static void setGameServer(string server)
	{
		if(server == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		account.CallStatic("setGameServer", server);
#elif UNITY_IPHONE
		dcSetGameServer(server);
#elif UNITY_WP8
		DataEyeWP8.DCAccount.setGameServer(server);
#endif
	}
	
	public static void addTag(string tag, string subTag)
	{
		if(tag == null || subTag == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		account.CallStatic("addTag", tag, subTag);
#elif UNITY_IPHONE
		dcAddTag(tag, subTag);
#elif UNITY_WP8
		DataEyeWP8.DCAccount.addTag(tag, subTag);
#endif
	}
	
	public static void removeTag(string tag, string subTag)
	{
		if(tag == null || subTag == null)
		{
			return;
		}
#if UNITY_EDITOR
#elif UNITY_ANDROID
		account.CallStatic("removeTag", tag, subTag);
#elif UNITY_IPHONE
		dcRemoveTag(tag, subTag);
#elif UNITY_WP8
		DataEyeWP8.DCAccount.removeTag(tag, subTag);
#endif
	}
};