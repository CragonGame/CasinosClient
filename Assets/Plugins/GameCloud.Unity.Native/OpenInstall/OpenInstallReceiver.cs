using System;
using System.Collections.Generic;
using UnityEngine;

public class OpenInstallReceiver : MonoBehaviour
{
    //-------------------------------------------------------------------------
    static string mOpenInstallReceiverName;
    static OpenInstallReceiver mOpenInstallReceiver;
    public IOpenInstallReceiverListener OpenInstallReceiverListener { get; set; }
    public Action<string,bool> OpenInstallResultCallBack { get; set; }

    //-------------------------------------------------------------------------
    public static OpenInstallReceiver instance()
    {
        mOpenInstallReceiverName = (typeof(OpenInstallReceiver)).Name;
        GameObject msg_receiver = GameObject.Find(mOpenInstallReceiverName);
        if (msg_receiver == null)
        {
            msg_receiver = new GameObject(mOpenInstallReceiverName);
            mOpenInstallReceiver = msg_receiver.AddComponent<OpenInstallReceiver>();
            GameObject.DontDestroyOnLoad(msg_receiver);
        }
        else
        {
            mOpenInstallReceiver = msg_receiver.GetComponent<OpenInstallReceiver>();
        }

        return mOpenInstallReceiver;
    }
    
    //-------------------------------------------------------------------------
    public void OpenInstallWakeUpResult(string result)
    {        
        if (OpenInstallResultCallBack != null)
        {
            OpenInstallResultCallBack(result,false);
        }

        if (OpenInstallReceiverListener != null)
        {
            OpenInstallReceiverListener.OpenInstallResult(result, false);
        }
    }

    //-------------------------------------------------------------------------
    public void OpenInstallInstallResult(string result)
    {        
        if (OpenInstallResultCallBack != null)
        {
            OpenInstallResultCallBack(result,true);
        }

        if (OpenInstallReceiverListener != null)
        {
            OpenInstallReceiverListener.OpenInstallResult(result,true);
        }
    }
}