using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsContext
{
    System.Action<string> TestActionDelegate;
    int Count { get; set; }

    //-------------------------------------------------------------------------
    public static CsContext Instance { get; private set; }

    //-------------------------------------------------------------------------
    public CsContext()
    {
        Instance = this;
    }

    //-------------------------------------------------------------------------
    public void Create()
    {
        Debug.Log("CsContext.Create()");

        TestActionDelegate = Test;
        TestActionDelegate("aaa");
    }

    //-------------------------------------------------------------------------
    public void Destroy()
    {
        Debug.Log("CsContext.Destroy()");
    }

    //-------------------------------------------------------------------------
    public void Update()
    {
        Count++;
        if (Count > 500)
        {
            Count = 0;

            //Debug.Log("CsContext.Update() Count=" + Count);
        }
    }

    //-------------------------------------------------------------------------
    public void Test(string s)
    {
        Debug.Log("CsContext.Test() " + s);
    }
}