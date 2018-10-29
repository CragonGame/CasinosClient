using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace io.openinstall.unity
{

#if UNITY_ANDROID
	public class AndroidOpenInstall : IOpenInstall {
		
		private AndroidJavaObject currentActivity;
		private AndroidJavaClass openinstallClass;


		public AndroidOpenInstall(){
			AndroidJavaClass jclass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = jclass.GetStatic<AndroidJavaObject>("currentActivity");

            openinstallClass = new AndroidJavaClass("com.fm.openinstall.OpenInstall");
		}

        // 获取安装数据
        public override void getInstall(int s) 
		{
			AndroidJavaObject callback = new AndroidJavaObject ("io.openinstall.unity.OiInstallCallback");

            // Android接收ms
			openinstallClass.CallStatic("getInstall", callback, s*1000);
        }
        // 拉起回调
        public override void registerWakeupCallback()
        {
            AndroidJavaClass wakeupCallback = new AndroidJavaClass("io.openinstall.unity.OiWakeupCallback");
			wakeupCallback.CallStatic("registerWakeup");
        }
        // 注册上报
        public override void reportRegister()
        {
			openinstallClass.CallStatic("reportRegister");
        }
        // 效果点上报
        public override void reportEffectPoint(string pointID, long pointValue)
        {
			openinstallClass.CallStatic("reportEffectPoint", pointID, pointValue);
        }

	}
#endif
}
