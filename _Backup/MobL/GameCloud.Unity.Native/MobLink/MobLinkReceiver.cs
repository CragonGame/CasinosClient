using cn.sharesdk.unity3d;
using com.moblink.unity3d;
using GTPush;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MobLinkReceiver : MonoBehaviour
{
    //-------------------------------------------------------------------------
    public static MobLinkReceiver mMobLinkReceiver;
    public static MobLink mMobLink;
    static string mOpenInstallReceiverName;      
    bool tokenSent = false;

    //-------------------------------------------------------------------------
    public static MobLinkReceiver instance()
    {
        mOpenInstallReceiverName = (typeof(MobLink)).Name;
        GameObject msg_receiver = GameObject.Find(mOpenInstallReceiverName);
        if (msg_receiver == null)
        {
            msg_receiver = new GameObject(mOpenInstallReceiverName);
            mMobLinkReceiver = msg_receiver.AddComponent<MobLinkReceiver>();
            mMobLink = msg_receiver.AddComponent<MobLink>();           
            GameObject.DontDestroyOnLoad(msg_receiver);
        }
        else
        {
            mMobLinkReceiver = msg_receiver.GetComponent<MobLinkReceiver>();
            mMobLink = msg_receiver.GetComponent<MobLink>();
        }

        return mMobLinkReceiver;
    }
}