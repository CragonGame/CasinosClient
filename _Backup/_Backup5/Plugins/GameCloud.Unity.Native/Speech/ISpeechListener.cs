using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ISpeechListener
{
    //-------------------------------------------------------------------------
	void speechResult(_eSpeechResult result_code, string most_possibleresult);
}

//-------------------------------------------------------------------------
public enum _eSpeechResult
{
    None,
    ReadyForSpeech,
    BeginningOfSpeech,
    EndOfSpeech,
    FinalResults,
    PartialResults,
    Error,
}