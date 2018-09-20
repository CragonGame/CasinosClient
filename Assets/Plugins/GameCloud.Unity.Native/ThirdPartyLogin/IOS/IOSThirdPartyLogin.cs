using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

#if UNITY_IPHONE
public class IOSThirdPartyLogin : IThirdPartyLogin
{
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
    [DllImport("__Internal")]
    private static extern void init(string app_id);
    [DllImport("__Internal")]
    private static extern void login(_eThirdPartyLoginType login_type, string state,string param);
#endregion
}
#endif
