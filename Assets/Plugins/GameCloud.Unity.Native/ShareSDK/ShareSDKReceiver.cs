using cn.sharesdk.unity3d;
using GTPush;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ShareSDKReceiver : MonoBehaviour
{
    //-------------------------------------------------------------------------
    public static ShareSDKReceiver mOpenInstallReceiver;
    public static ShareSDK mShareSDK;
    static string mOpenInstallReceiverName;        
    //bool tokenSent = false;

    //-------------------------------------------------------------------------
    public static ShareSDKReceiver instance(string key, string secret)
    {
        mOpenInstallReceiverName = (typeof(ShareSDKReceiver)).Name;
        GameObject msg_receiver = GameObject.Find(mOpenInstallReceiverName);
        if (msg_receiver == null)
        {
            msg_receiver = new GameObject(mOpenInstallReceiverName);
            mOpenInstallReceiver = msg_receiver.AddComponent<ShareSDKReceiver>();
            mShareSDK = msg_receiver.AddComponent<ShareSDK>();
            mShareSDK.appKey = key;
            mShareSDK.appSecret = secret;
            GameObject.DontDestroyOnLoad(msg_receiver);
        }
        else
        {
            mOpenInstallReceiver = msg_receiver.GetComponent<ShareSDKReceiver>();
            mShareSDK = msg_receiver.GetComponent<ShareSDK>();
        }

        return mOpenInstallReceiver;
    }
}