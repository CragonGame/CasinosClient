package com.Native.Native;
import java.io.File;

import com.AndroidToUnityMsgBridge.AndroidToUnityMsgBridge.AndroidToUnityMsgBridge;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Build;
import android.support.v4.content.FileProvider;
import android.telephony.TelephonyManager;
import android.util.Log;


public class NativeFun {
	public static NativeFun mNativeFun;
	private static AndroidToUnityMsgBridge mAndroidToUnityMsgBridge;	
	private static Activity mUnityActivity;
	
	// -------------------------------------------------------------------------
	public static NativeFun Instantce() {	
		if (mNativeFun == null) {
			mAndroidToUnityMsgBridge = AndroidToUnityMsgBridge
					.Instance();
			mNativeFun = new NativeFun();
			mUnityActivity = mAndroidToUnityMsgBridge.getActivity();
		}

		return mNativeFun;
	}
	
	// -------------------------------------------------------------------------
	public static String getCountryZipCode() {  
	    String CountryID = "";  	     
	    TelephonyManager manager = (TelephonyManager) mUnityActivity.getSystemService(Context.TELEPHONY_SERVICE);  
	    CountryID = manager.getSimCountryIso().toUpperCase();  
	    Log.d("ss", "CountryID--->>>" + CountryID);  
	    
	    return CountryID.trim();  
	}  
	
	// -------------------------------------------------------------------------
	public static void installAPK(String filePath) {
        Log.i("NativeFun", "开始执行安装: " + filePath);
        File apkFile = new File(filePath);
        Intent intent = new Intent(Intent.ACTION_VIEW);
        intent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
        if (Build.VERSION.SDK_INT >= 24) {            
            intent.setFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION);
            Uri contentUri = FileProvider.getUriForFile(
            		mUnityActivity
                    , mUnityActivity.getApplicationContext().getPackageName() + ".provider"
                    , apkFile);
            intent.setDataAndType(contentUri, "application/vnd.android.package-archive");
        } else {            
            intent.setDataAndType(Uri.fromFile(apkFile), "application/vnd.android.package-archive");
        }
        mUnityActivity.startActivity(intent);
    }
}
