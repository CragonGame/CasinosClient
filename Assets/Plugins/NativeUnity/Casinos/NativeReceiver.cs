using System;
using System.Collections.Generic;
using UnityEngine;

public class NativeReceiver : MonoBehaviour
{
    //-------------------------------------------------------------------------
    static string mNativeReceiverName;
    static NativeReceiver mNativeAPIMsgReceiver;
    public ITakePhotoReceiverListener TakePhotoReceiverListener { get; set; }
    public IAudioControlListener AudioControlListener { get; set; }

    //-------------------------------------------------------------------------
    public static NativeReceiver instance()
    {
        mNativeReceiverName = (typeof(NativeReceiver)).Name;
        GameObject msg_receiver = GameObject.Find(mNativeReceiverName);
        if (msg_receiver == null)
        {
            msg_receiver = new GameObject(mNativeReceiverName);
            mNativeAPIMsgReceiver = msg_receiver.AddComponent<NativeReceiver>();
            GameObject.DontDestroyOnLoad(msg_receiver);
        }
        else
        {
            mNativeAPIMsgReceiver = msg_receiver.GetComponent<NativeReceiver>();
        }

        return mNativeAPIMsgReceiver;
    }

    //-------------------------------------------------------------------------
    public void ReceiveError(string error)
    {
        Debug.Log("ReceiveError::" + error);
    }

    //-------------------------------------------------------------------------
    public void audioChanged(string chang)
    {
        if (AudioControlListener != null)
        {
            AudioControlListener.audioChanged(chang);
        }
    }

    //-------------------------------------------------------------------------
    public void getPicSuccess(string getpic_result)
    {
        if (TakePhotoReceiverListener != null)
        {
            TakePhotoReceiverListener.getPicSuccess(getpic_result);
        }
    }

    //-------------------------------------------------------------------------
    public void getPicFail(string fail)
    {
        if (TakePhotoReceiverListener != null)
        {
            TakePhotoReceiverListener.getPicFail(fail);
        }
    }
}