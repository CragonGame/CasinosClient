using UnityEngine;
using System.Collections;

public class NativeSpeech
{
    //-------------------------------------------------------------------------
    static NativeSpeech mNativeSpeech;
    static ISpeech mISpeech;

    //-------------------------------------------------------------------------
    public NativeSpeech()
    {
    }

    //-------------------------------------------------------------------------
    public static NativeSpeech Instantce()
    {
        if (mNativeSpeech == null)
        {
            mNativeSpeech = new NativeSpeech();
        }

        return mNativeSpeech;
    }

    //-------------------------------------------------------------------------
    public void initSpeech(string app_id, _eSpeechLanguage speech_language)
    {
#if UNITY_ANDROID
        mISpeech = new AndroidSpeech(app_id, speech_language);
        Debug.Log("AndroidSpeech::");
#elif UNITY_IOS
		mISpeech = new IOSSpeech(app_id,speech_language);
        Debug.Log("IOSSpeech::");
#else
        Debug.LogError("Do not supported on this platform. ");
#endif
    }

    //-------------------------------------------------------------------------
    public void startSpeech()
    {
        Debug.Log("startSpeech::");
        mISpeech.startSpeech();
    }

    //-------------------------------------------------------------------------
    public void cancelSpeech()
    {
        Debug.Log("cancelSpeech::");
        mISpeech.cancelSpeech();
    }
}

//-------------------------------------------------------------------------
public enum _eSpeechLanguage
{
    zh_cn,
    en_us,
}
