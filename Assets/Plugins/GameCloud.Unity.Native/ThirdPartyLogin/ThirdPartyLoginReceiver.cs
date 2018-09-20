using System;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPartyLoginReceiver : MonoBehaviour
{
    //-------------------------------------------------------------------------
    static string mThirdPartyLoginReceiverName;
    static ThirdPartyLoginReceiver mThirdPartyLoginReceiver;
    public IThirdPartyLoginReceiverListener ThirdPartyLoginReceiverListener { get; set; }    

    //-------------------------------------------------------------------------
    public static ThirdPartyLoginReceiver instance()
    {
        mThirdPartyLoginReceiverName = (typeof(ThirdPartyLoginReceiver)).Name;
        GameObject msg_receiver = GameObject.Find(mThirdPartyLoginReceiverName);
        if (msg_receiver == null)
        {
            msg_receiver = new GameObject(mThirdPartyLoginReceiverName);
            mThirdPartyLoginReceiver = msg_receiver.AddComponent<ThirdPartyLoginReceiver>();
            GameObject.DontDestroyOnLoad(msg_receiver);
        }
        else
        {
            mThirdPartyLoginReceiver = msg_receiver.GetComponent<ThirdPartyLoginReceiver>();
        }

        return mThirdPartyLoginReceiver;
    }
    
    //-------------------------------------------------------------------------
    public void loginSuccess(string token)
    {
        if (ThirdPartyLoginReceiverListener != null)
        {
            ThirdPartyLoginReceiverListener.loginSuccess(token);
        }
    }

    //-------------------------------------------------------------------------
    public void loginFail(string fail_type)
    {
        if (ThirdPartyLoginReceiverListener != null)
        {
            ThirdPartyLoginReceiverListener.loginFail(fail_type);
        }
    }
}
