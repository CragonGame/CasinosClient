using UnityEngine;
using System.Collections;
using GTPush;

#if UNITY_ANDROID
public class AndroidPush : IPush
{
    //-------------------------------------------------------------------------
    public AndroidPush()
    {
    }

    public void initPush(string appId, string appKey, string appSecret)
    {
        GTPushBinding.initPush(PushReceiver.instance().gameObject.name);
    }
}
#endif