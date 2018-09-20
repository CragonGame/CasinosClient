using GTPush;
using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_IPHONE
using NotificationServices = UnityEngine.iOS.NotificationServices;
using NotificationType = UnityEngine.iOS.NotificationType;
#endif

public class PushReceiver : MonoBehaviour
{
    //-------------------------------------------------------------------------
    static string mPushReceiverName;
    static PushReceiver mPushReceiver;    
    public Action<string> OnReceiveMessage;
    public Action<string> OnGeTuiSDkDidNotifySdkState;
    public Action<string> OnGeTuiSdkDidAliasAction;
	public Action<string> OnNotificationMessageArrived;
	public Action<string> OnNotificationMessageClicked;
	public Action<string> OnGeTuiSdkDidSetPushMode;
	public Action<string> OnGeTuiSdkDidOccurError;
    //bool tokenSent = false;

    //-------------------------------------------------------------------------
    public static PushReceiver instance()
    {
        mPushReceiverName = (typeof(PushReceiver)).Name;
        GameObject msg_receiver = GameObject.Find(mPushReceiverName);
        if (msg_receiver == null)
        {
            msg_receiver = new GameObject(mPushReceiverName);
            mPushReceiver = msg_receiver.AddComponent<PushReceiver>();
            GameObject.DontDestroyOnLoad(msg_receiver);
        }
        else
        {
            mPushReceiver = msg_receiver.GetComponent<PushReceiver>();
        }

        return mPushReceiver;
    }

    //-------------------------------------------------------------------------
    public void onReceiveClientId(string clientId)
    {
        Debug.Log("GeTuiSdkDidRegisterClient clientId : " + clientId);
#if UNITY_IOS
        if (!tokenSent)
        {
            byte[] token = NotificationServices.deviceToken;
            if (token != null)
            {
                // send token to a provider
                tokenSent = true;
                string deviceToken = System.BitConverter.ToString(token).Replace("-", "");
                GTPushBinding.registerDeviceToken(deviceToken);
                Debug.Log("deviceToken is : " + deviceToken + " cid is : " + GTPushBinding.getClientId() + " version is : " + GTPushBinding.getVersion());
            }
        }
#endif

//#if (UNITY_IPHONE || UNITY_ANDROID)
//        GTPushBinding.setTag("ge,tui");
//        GTPushBinding.bindAlias("getui");
//        GTPushBinding.unBindAlias("getui");
//#endif
    }

    //-------------------------------------------------------------------------
    public void onReceiveMessage(string payloadJsonData)
    {
        Debug.Log("GeTuiSdkDidReceivePayloadData payload JsonData : " + payloadJsonData);
        if (OnReceiveMessage != null)
        {
            OnReceiveMessage(payloadJsonData);
        }
    }

	//-------------------------------------------------------------------------
	public void onNotificationMessageArrived(string msg)
	{
		if (OnNotificationMessageArrived != null) {
			OnNotificationMessageArrived (msg);
		}
	}

	//-------------------------------------------------------------------------
	public void onNotificationMessageClicked(string msg)
	{
		if (OnNotificationMessageClicked != null) {
			OnNotificationMessageClicked (msg);
		}
	}

	//-------------------------------------------------------------------------
	public void GeTuiSdkDidSetPushMode(string isModeOn)
	{
		if (OnGeTuiSdkDidSetPushMode != null) {
			OnGeTuiSdkDidSetPushMode (isModeOn);
		}
	}

	//-------------------------------------------------------------------------
	public void GeTuiSdkDidOccurError(string error)
	{
		Debug.Log("GeTuiSdkDidOccurError error : " + error);
		if (OnGeTuiSdkDidOccurError != null) {
			OnGeTuiSdkDidOccurError (error);
		}
	}

    //-------------------------------------------------------------------------
    public void GeTuiSDkDidNotifySdkState(string state)
    {
        Debug.Log("GeTuiSDkDidNotifySdkState state : " + state);
        if (OnGeTuiSDkDidNotifySdkState != null)
        {
            OnGeTuiSDkDidNotifySdkState(state);
        }
    }

    //-------------------------------------------------------------------------
    public void GeTuiSdkDidAliasAction(string message)
    {    
        if (OnGeTuiSdkDidAliasAction != null)
        {
            OnGeTuiSdkDidAliasAction(message);
        }
    }
}