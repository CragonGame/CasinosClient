using UnityEngine;
using System.Collections;
using System;

public class AndroidSpeech : ISpeech
{
    public AndroidJavaClass mAndroidJavaClassSpeech;
    public AndroidJavaObject mAndroidSpeech;
    string mAppId;
    _eSpeechLanguage mSpeechLanguage;

    //-------------------------------------------------------------------------
    public AndroidSpeech(string app_id, _eSpeechLanguage speech_language)
    {
        mAndroidJavaClassSpeech = new AndroidJavaClass("com.XFSpeech.XFSpeech.XFSpeech");
        mAppId = app_id;
        mSpeechLanguage = speech_language;
    }

    //-------------------------------------------------------------------------
    public void startSpeech()
    {
        _initJavaObject();

        mAndroidSpeech.Call(_eAndroidBaiduSpeechMsg.startSpeech.ToString());
    }

    //-------------------------------------------------------------------------
    public void cancelSpeech()
    {
        _initJavaObject();

        mAndroidSpeech.Call(_eAndroidBaiduSpeechMsg.cancelSpeech.ToString());
    }

    //-------------------------------------------------------------------------
    void _initJavaObject()
    {
        if (mAndroidSpeech == null)
        {
            mAndroidSpeech = mAndroidJavaClassSpeech.CallStatic<AndroidJavaObject>("Instantce",
                typeof(SpeechReceiver).Name, mAppId, mSpeechLanguage.ToString());
        }
    }
}

//-------------------------------------------------------------------------
public enum _eAndroidBaiduSpeechMsg
{
    startSpeech,
    cancelSpeech
}
