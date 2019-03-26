package com.Native.Clipboardoperation;

import com.AndroidToUnityMsgBridge.AndroidToUnityMsgBridge.AndroidToUnityMsgBridge;
import android.app.Activity;
import android.content.ClipboardManager;
import android.content.Context;


public class NativeOperation {
	public static NativeOperation mNativeOperation;
	private static AndroidToUnityMsgBridge mAndroidToUnityMsgBridge;	
	private static Activity mUnityActivity;
	
	// -------------------------------------------------------------------------
	public static NativeOperation Instantce() {	
		if (mNativeOperation == null) {
			mAndroidToUnityMsgBridge = AndroidToUnityMsgBridge
					.Instance();
			mNativeOperation = new NativeOperation();
			mUnityActivity = mAndroidToUnityMsgBridge.getActivity();
		}

		return mNativeOperation;
	}
	
	// -------------------------------------------------------------------------
	@SuppressWarnings("deprecation")
	public static String GetClipBoard(){
	   ClipboardManager cmb = (ClipboardManager) mUnityActivity.getSystemService(Context.CLIPBOARD_SERVICE); 
	   String text = cmb.getText().toString().trim();
	   return text;
	}
	
	// ------------------------------------------------------------------------
	@SuppressWarnings("deprecation")
	public static void SetClipBoard(String text ){
		ClipboardManager cmb = (ClipboardManager) mUnityActivity.getSystemService(Context.CLIPBOARD_SERVICE); 
		cmb.setText(text);
	}
}
