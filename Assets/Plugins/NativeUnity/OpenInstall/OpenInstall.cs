using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace io.openinstall.unity
{

    public class OpenInstall : MonoBehaviour
    {

        private static bool isInit;
        private static OpenInstall instance;
        private static IOpenInstall openInstall;

        private static event OpenInstallDelegate installDelegate;
        private static event OpenInstallDelegate wakeupDelegate;

        private void Awake()
        {
            if (!isInit)
            {
#if UNITY_ANDROID
                openInstall = new AndroidOpenInstall();
#elif UNITY_IPHONE
                openInstall = new IOSOpenInstallImpl();
#else
                openInstall = new DefaultOpenInstall();
#endif
                isInit = true;
            }
            if(instance != null)
            {
                Destroy(instance);
            }
            instance = this;
            DontDestroyOnLoad(instance);

        }


        // 获取安装数据
        public void GetInstall(int s, OpenInstallDelegate openinstallDelegate)
        {
            Debug.Log("OpenInstallUnity getInstall");
           
            installDelegate = openinstallDelegate;

            openInstall.getInstall(s);
        }

        // 安装数据回调方法
        public void _installCallback(string result)
        {
            Debug.Log("OpenInstallUnity _installCallback : " + result);
            if (result == null)
            {
                return;
            }
            if (installDelegate == null)
            {
                Debug.Log("OpenInstallUnity _installCallback : installDelegate is null !");
                return;
            }
            OpenInstallData data = JsonUtility.FromJson<OpenInstallData>(result);
            installDelegate(data);
        }

        // 注册拉起回调
        public void RegisterWakeupHandler(OpenInstallDelegate openinstallDelegate)
        {
            Debug.Log("OpenInstallUnity registerWakeupHandler");

            wakeupDelegate = openinstallDelegate;

            openInstall.registerWakeupCallback();
        }

        // 拉起数据回调方法 
        public void _wakeupCallback(string result)
        {
            Debug.Log("OpenInstallUnity _wakeupCallback : " + result);
            if (result == null)
            {
                return;
            }
            if (wakeupDelegate == null)
            {
                Debug.Log("OpenInstallUnity _wakeupCallback : wakeupDelegate is null !");
                return;
            }
            OpenInstallData data = JsonUtility.FromJson<OpenInstallData>(result);
            wakeupDelegate(data);
        }

        // 注册上报
        public void ReportRegister()
        {
            Debug.Log("OpenInstallUnity reportRegister");
            openInstall.reportRegister();
        }

        // 效果点上报
        public void ReportEffectPoint(string pointID, long pointValue)
        {
            Debug.Log("OpenInstallUnity reportEffectPoint");
            openInstall.reportEffectPoint(pointID, pointValue);
        }


        public delegate void OpenInstallDelegate(OpenInstallData openInstallData);

    }

}