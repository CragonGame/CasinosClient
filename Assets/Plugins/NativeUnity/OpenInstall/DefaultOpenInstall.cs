using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace io.openinstall.unity
{
    public class DefaultOpenInstall : IOpenInstall
    {
        // 获取安装数据
        public override void getInstall(int s)
        {
            Debug.Log("OpenInstallUnity Unsupported Platform");
        }
        // 拉起回调
        public override void registerWakeupCallback()
        {
            Debug.Log("OpenInstallUnity Unsupported Platform");
        }
        // 注册上报
        public override void reportRegister()
        {
            Debug.Log("OpenInstallUnity Unsupported Platform");
        }
        // 效果点上报
        public override void reportEffectPoint(string pointID, long pointValue)
        {
            Debug.Log("OpenInstallUnity Unsupported Platform");
        }

    }
}
