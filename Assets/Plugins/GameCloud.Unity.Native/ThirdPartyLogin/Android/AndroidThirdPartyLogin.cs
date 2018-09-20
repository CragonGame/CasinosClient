using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidThirdPartyLogin : IThirdPartyLogin
{
    public AndroidJavaClass mAndoridJavaClassThirdPartyLogin;
    public AndroidJavaObject mAndroidThirdPartyLogin;

    //-------------------------------------------------------------------------
    public AndroidThirdPartyLogin()
    {
        mAndoridJavaClassThirdPartyLogin = new AndroidJavaClass("com.ThirdPartyLogin.ThirdPartyLogin.ThirdPartyLogin");
    }

    //-------------------------------------------------------------------------
    public void Init(string app_id)
    {
        if (mAndroidThirdPartyLogin == null)
        {
            mAndroidThirdPartyLogin = mAndoridJavaClassThirdPartyLogin.CallStatic<AndroidJavaObject>("Instantce",
                app_id, "loginSuccess", "loginFail", "ThirdPartyLoginReceiver");
        }
    }

    //-------------------------------------------------------------------------
    public void Login(_eThirdPartyLoginType login_type, string state, string param)
    {
        if (mAndroidThirdPartyLogin != null)
        {
            mAndroidThirdPartyLogin.Call("Login", (int)_eThirdPartyLoginType.WeChat, state, param);
        }
        else
        {
            Debug.LogError("AndroidThirdPartyLogin Is Null");
        }
    }
}
