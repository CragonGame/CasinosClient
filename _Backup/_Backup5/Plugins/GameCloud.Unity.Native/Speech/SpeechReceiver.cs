using System;
using System.Collections.Generic;
using UnityEngine;

public class SpeechReceiver : MonoBehaviour
{
    //-------------------------------------------------------------------------
    static string mSpeechReceiverName;
    static SpeechReceiver mSpeechReceiver;
    public ISpeechListener SpeechListener { get; set; }

    //-------------------------------------------------------------------------
    public static SpeechReceiver instance()
    {
        mSpeechReceiverName = (typeof(SpeechReceiver)).Name;
        GameObject msg_receiver = GameObject.Find(mSpeechReceiverName);
        if (msg_receiver == null)
        {
            msg_receiver = new GameObject(mSpeechReceiverName);
            mSpeechReceiver = msg_receiver.AddComponent<SpeechReceiver>();
            GameObject.DontDestroyOnLoad(msg_receiver);
        }
        else
        {
            mSpeechReceiver = msg_receiver.GetComponent<SpeechReceiver>();
        }

        return mSpeechReceiver;
    }

    //-------------------------------------------------------------------------
    public void speechResult(_eSpeechResult result_code, string most_possibleresult)
    {
        if (SpeechListener!=null)
        {
            SpeechListener.speechResult(result_code, most_possibleresult); ;
        }

        Debug.Log("speechResult::result_code:: " + (_eSpeechResult)result_code + "   most_possibleresult::" + most_possibleresult);
    }
}