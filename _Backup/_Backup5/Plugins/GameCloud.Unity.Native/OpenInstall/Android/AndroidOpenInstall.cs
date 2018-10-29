using UnityEngine;
using System.Collections;

public class AndroidOpenInstall : IOpenInstall
{
    //-------------------------------------------------------------------------
    public AndroidJavaClass mAndroidJavaClassOpenInstall;
    public AndroidJavaObject mAndroidJavaObjectOpenInstall;

    //-------------------------------------------------------------------------
    public AndroidOpenInstall()
    {
        mAndroidJavaClassOpenInstall = new AndroidJavaClass("com.OpenInstall.OpenInstall.NativeOpenInstall"); 
        if (mAndroidJavaObjectOpenInstall == null)
        {
            mAndroidJavaObjectOpenInstall = mAndroidJavaClassOpenInstall.CallStatic<AndroidJavaObject>("Instance", "OpenInstallReceiver");
        }
    }

    //-------------------------------------------------------------------------
    public void GetWakeUp()
    {
        if (mAndroidJavaObjectOpenInstall != null)
        {
            mAndroidJavaObjectOpenInstall.Call("GetWakeUp");
        }
    }
}