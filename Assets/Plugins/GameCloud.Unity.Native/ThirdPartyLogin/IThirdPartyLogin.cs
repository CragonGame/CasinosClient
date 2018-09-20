using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThirdPartyLogin
{
    //-------------------------------------------------------------------------
    void Init(string app_id);

    //-------------------------------------------------------------------------
    void Login(_eThirdPartyLoginType login_type, string state, string param);
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
