using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsContext
{
    System.Action<string> TestActionDelegate;

    //-------------------------------------------------------------------------
    public static CsContext Instance { get; private set; }

    //-------------------------------------------------------------------------
    public CsContext()
    {
        Instance = this;
        Debug.Log("CsContext.CsContext()");
    }

    //-------------------------------------------------------------------------
    public void Create()
    {
        Debug.Log("CsContext.Create()");

        TestActionDelegate = AA;
        TestActionDelegate("aaa");
    }

    //-------------------------------------------------------------------------
    public void Destroy()
    {
        Debug.Log("CsContext.Destroy()");
    }

    //-------------------------------------------------------------------------
    public void AA(string s)
    {
        Debug.Log("CsContext.AA() " + s);
    }
}