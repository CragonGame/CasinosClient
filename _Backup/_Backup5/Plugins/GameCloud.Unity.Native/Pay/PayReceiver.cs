using System;
using System.Collections.Generic;
using UnityEngine;

public class PayReceiver : MonoBehaviour
{
    //-------------------------------------------------------------------------
    static string mPayReceiverName;
    static PayReceiver mPayReceiver;
    public IPayReceiverListener PayReceiverListener { get; set; }

    //-------------------------------------------------------------------------
    public static PayReceiver instance()
    {
        mPayReceiverName = (typeof(PayReceiver)).Name;
        GameObject msg_receiver = GameObject.Find(mPayReceiverName);
        if (msg_receiver == null)
        {
            msg_receiver = new GameObject(mPayReceiverName);
            mPayReceiver = msg_receiver.AddComponent<PayReceiver>();
            GameObject.DontDestroyOnLoad(msg_receiver);
        }
        else
        {
            mPayReceiver = msg_receiver.GetComponent<PayReceiver>();
        }

        return mPayReceiver;
    }

    //-------------------------------------------------------------------------
    public void PayResultIPA(_ePayOptionType option_type, bool is_success, object result)
    {
        Debug.Log("PayResultIPA::" + result);
        if (PayReceiverListener != null)
        {
            PayReceiverListener.PayResultIPA(option_type, is_success, result);
        }
    }

    //-------------------------------------------------------------------------
    public void PayResult(string result)
    {
        Debug.Log("PayResult::" + result);
        if (PayReceiverListener != null)
        {
            PayReceiverListener.PayResult(result);
        }
    }

    //-------------------------------------------------------------------------
    public void ReceiveError(string error)
    {
        Debug.Log("ReceiveError::" + error);
    }
}