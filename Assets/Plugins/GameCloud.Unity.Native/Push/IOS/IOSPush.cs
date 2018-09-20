using UnityEngine;
using System.Collections;
using GTPush;

#if UNITY_IPHONE
public class IOSPush : IPush
{
    //-------------------------------------------------------------------------
    public IOSPush()
    {
    }

    public void initPush(string appId, string appKey, string appSecret)
    {
        GTPushBinding.StartSDK(appId, appKey, appSecret);
        GTPushBinding.setListenerGameObject(PushReceiver.instance().gameObject.name);
        GTPushBinding.registerUserNotification();
    }
}
#endif