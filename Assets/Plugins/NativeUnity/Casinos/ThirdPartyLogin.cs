using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

#if UNITY_IOS
using System.Runtime.InteropServices;
#endif

public enum _eLoginFaildType
{
    ERR_OK = 0,
    ERR_COMM = -1,
    ERR_USER_CANCEL = -2,
    ERR_SENT_FAILED = -3,
    ERR_AUTH_DENIED = -4,
    ERR_UNSUPPORT = -5,
    ERR_NOTINSTALLEDWECHAT = 10000,
}

//-------------------------------------------------------------------------
public enum _eThirdPartyLoginType
{
    WeChat,
}

//-------------------------------------------------------------------------
public enum _eThirdPartyLoginParam
{
    Login,
    Bind,
}

public class ThirdPartyLogin
{
#if UNITY_EDITOR || UNITY_STANDALONE
    //-------------------------------------------------------------------------
    public ThirdPartyLogin()
    {
    }

    //-------------------------------------------------------------------------
    public void Init(string app_id)
    {
    }

    //-------------------------------------------------------------------------
    public void Login(_eThirdPartyLoginType login_type, string state, string param)
    {
    }

#elif UNITY_ANDROID
        //-------------------------------------------------------------------------
    public AndroidJavaClass mAndoridJavaClassThirdPartyLogin;
    public AndroidJavaObject mAndroidThirdPartyLogin;

    //-------------------------------------------------------------------------
    public ThirdPartyLogin()
    {
        mAndoridJavaClassThirdPartyLogin = new AndroidJavaClass("com.ThirdPartyLogin.ThirdPartyLogin.ThirdPartyLogin");
    }

    //-------------------------------------------------------------------------
    public void Init(string app_id)
    {
        if (mAndroidThirdPartyLogin == null)
        {
            mAndroidThirdPartyLogin = mAndoridJavaClassThirdPartyLogin.CallStatic<AndroidJavaObject>("Instantce",
                app_id);
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

#elif UNITY_IPHONE || UNITY_IOS

    //-------------------------------------------------------------------------
    public void Init(string app_id)
    {
        init(app_id);
    }

    //-------------------------------------------------------------------------
    public void Login(_eThirdPartyLoginType login_type, string state,string param)
    {
        login(login_type,state,param);
    }

    //-------------------------------------------------------------------------
    #region DllImport
    [DllImport ("__Internal")]
    private static extern void init(string app_id);
    [DllImport ("__Internal")]
    private static extern void login(_eThirdPartyLoginType login_type, string state,string param);
    #endregion
#endif

}