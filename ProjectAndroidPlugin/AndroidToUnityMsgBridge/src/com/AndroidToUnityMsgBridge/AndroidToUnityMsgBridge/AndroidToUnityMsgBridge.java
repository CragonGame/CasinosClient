package com.AndroidToUnityMsgBridge.AndroidToUnityMsgBridge;

import java.lang.reflect.Field;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.HashMap;
import java.util.Map;

import org.json.JSONArray;
import org.json.JSONObject;

import android.app.Activity;
import android.util.Log;

public class AndroidToUnityMsgBridge {
	// -------------------------------------------------------------------------
	public Activity mUnityPlayerActivity;
	private static AndroidToUnityMsgBridge mAndroidToUnityMsgBride;
	private Class<?> mUnityPlayerClass;
	private Field mUnityActivityField;
	private Method mAndroidToUnitySendMsgMethod;
	private String mUnityMsgReciver="NativeReceiver";
	private String mUnityMsgCallBackFun = "CallBackResult";

	// -------------------------------------------------------------------------
	public static AndroidToUnityMsgBridge Instance() {
		if (mAndroidToUnityMsgBride == null) {
			mAndroidToUnityMsgBride = new AndroidToUnityMsgBridge();
		}

		return mAndroidToUnityMsgBride;
	}

	// -------------------------------------------------------------------------
	private AndroidToUnityMsgBridge() {
		try {
			this.mUnityPlayerClass = Class
					.forName("com.unity3d.player.UnityPlayer");
			this.mUnityActivityField = this.mUnityPlayerClass
					.getField("currentActivity");
			this.mAndroidToUnitySendMsgMethod = this.mUnityPlayerClass
					.getMethod("UnitySendMessage", new Class[] { String.class,
							String.class, String.class });
		} catch (ClassNotFoundException e) {
			Log.e("AndroidToUnityMsgBridge",
					"Could not find UnityPlayer class:" + e.getMessage());
		} catch (NoSuchFieldException e1) {
			Log.e("AndroidToUnityMsgBridge",
					"Could not find currentActivity field: " + e1.getMessage());
		} catch (NoSuchMethodException e2) {
			Log.e("AndroidToUnityMsgBridge",
					"Could not find UnitySendMessageMethod: " + e2.getMessage());
		} catch (Exception e3) {
			Log.e("AndroidToUnityMsgBridge",
					"Unkown exception : " + e3.getMessage());
		}

		this.getActivity();
	}

	// -------------------------------------------------------------------------
	public Activity getActivity() {
		if (this.mUnityPlayerActivity == null) {
			try {
				this.mUnityPlayerActivity = (Activity) this.mUnityActivityField
						.get(this.mUnityPlayerClass);
			} catch (IllegalArgumentException e) {
				Log.e("AndroidToUnityMsgBridge",
						"Error getting currentActivity: " + e.getMessage());
			} catch (IllegalAccessException e1) {
				Log.e("AndroidToUnityMsgBridge",
						"Error getting currentActivity: " + e1.getMessage());
			}
		}

		return this.mUnityPlayerActivity;
	}

	// -------------------------------------------------------------------------
	public void sendMsgToUnity(String msg_name, String msg_param) {
		if (mAndroidToUnitySendMsgMethod != null) {
			try {
				this.mAndroidToUnitySendMsgMethod.invoke(null, new Object[] {
						this.mUnityMsgReciver, msg_name, msg_param });
			} catch (IllegalAccessException e) {
				Log.e("AndroidToUnityMsgBridge",
						"Could not find UnitySendMessageMethod: "
								+ e.getMessage());
			} catch (IllegalArgumentException e1) {
				Log.e("AndroidToUnityMsgBridge",
						"Could not find UnitySendMessageMethod: "
								+ e1.getMessage());
			} catch (InvocationTargetException e2) {
				Log.e("AndroidToUnityMsgBridge",
						"Could not find UnitySendMessageMethod: "
								+ e2.getMessage());
			}
		} else {
			Log.e("AndroidToUnityMsgBridge", "UnitySendMessage: "
					+ this.mUnityMsgReciver + ": " + msg_name + ": "
					+ msg_param);
		}
	}
	
	// -------------------------------------------------------------------------
		public void sendMsgToUnity(Boolean is_success,int native_type,String msg_param) {
			if (mAndroidToUnitySendMsgMethod != null) {
				try {
					Map<String,Object> map = new HashMap<String,Object>(); 
					map.put("ret", is_success); 			// 结果
					map.put("native_type", native_type);	// 来自哪里
					map.put("return_param", msg_param);		// 处理返回的结果
					
					JSONObject json =new JSONObject(map);
					
					this.mAndroidToUnitySendMsgMethod.invoke(null, new Object[] {
							this.mUnityMsgReciver, this.mUnityMsgCallBackFun, json.toString() });
					
					// 调试打开，发布去掉
					Log.e("AndroidToUnityMsgBridge","sendMsgToUnity:"+json.toString());
					
				} catch (IllegalAccessException e) {
					Log.e("AndroidToUnityMsgBridge",
							"Could not find UnitySendMessageMethod: "
									+ e.getMessage());
				} catch (IllegalArgumentException e1) {
					Log.e("AndroidToUnityMsgBridge",
							"Could not find UnitySendMessageMethod: "
									+ e1.getMessage());
				} catch (InvocationTargetException e2) {
					Log.e("AndroidToUnityMsgBridge",
							"Could not find UnitySendMessageMethod: "
									+ e2.getMessage());
				}
			} else {
				Log.e("AndroidToUnityMsgBridge", "UnitySendMessage: "
						+ this.mUnityMsgReciver + ": " + this.mUnityMsgCallBackFun + ": "
						+ msg_param);
			}
		}
}
