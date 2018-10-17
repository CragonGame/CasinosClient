package io.openinstall.unityplugin;

import com.fm.openinstall.OpenInstall;

import android.app.ActivityManager;
import android.app.Application;
import android.content.Context;

public class OiApplication extends Application {

	@Override
	public void onCreate() {
		// TODO Auto-generated method stub
		super.onCreate();
		
		if(isMainProcess()){
			OpenInstall.init(this);
		}
	}
	
	private boolean isMainProcess(){
		 int pid = android.os.Process.myPid();
	        ActivityManager activityManager = (ActivityManager)getSystemService(Context.ACTIVITY_SERVICE);
	        for (ActivityManager.RunningAppProcessInfo appProcess : activityManager.getRunningAppProcesses()) {
	            if (appProcess.pid == pid) {
	                return getApplicationInfo().packageName.equals(appProcess.processName);
	            }
	        }
	        return false;
	}
	
}
