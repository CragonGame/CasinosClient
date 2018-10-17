package io.openinstall.unityplugin;

import com.fm.openinstall.OpenInstall;
import com.unity3d.player.UnityPlayerActivity;

import android.content.Intent;
import android.os.Bundle;
import io.openinstall.unity.OiWakeupCallback;

public class OiUnityActivity extends UnityPlayerActivity {

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		OpenInstall.getWakeUp(getIntent(), wakeupCallback);
	}
	
	@Override
	protected void onNewIntent(Intent intent){
		super.onNewIntent(intent);
		OpenInstall.getWakeUp(intent, wakeupCallback);
		
	}
	
	OiWakeupCallback wakeupCallback = new OiWakeupCallback();
	
}
