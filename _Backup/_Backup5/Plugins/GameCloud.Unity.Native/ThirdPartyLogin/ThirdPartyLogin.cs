using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ThirdPartyLogin
{
    //-------------------------------------------------------------------------
    static ThirdPartyLogin mThirdPartyLogin;
    static IThirdPartyLogin mIThirdPartyLogin;

    //-------------------------------------------------------------------------
    public ThirdPartyLogin()
    {
#if UNITY_ANDROID
        mIThirdPartyLogin = new AndroidThirdPartyLogin();
#elif UNITY_IOS
		mIThirdPartyLogin = new IOSThirdPartyLogin();
#else
        Debug.LogError("Do not supported on this platform. ");
#endif
    }

    //-------------------------------------------------------------------------
    public static ThirdPartyLogin Instantce()
    {
        if (mThirdPartyLogin == null)
        {
            mThirdPartyLogin = new ThirdPartyLogin();
        }

        return mThirdPartyLogin;
    }

    //-------------------------------------------------------------------------
    public void initLogin(string app_id)
    {
        mIThirdPartyLogin.Init(app_id);
    }

    //-------------------------------------------------------------------------
    public void login(_eThirdPartyLoginType login_type, string state, string param)
    {
        mIThirdPartyLogin.Login(login_type, state, param);
    }
}