using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsContext
{
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
    }

    //-------------------------------------------------------------------------
    public void Destroy()
    {
        Debug.Log("CsContext.Destroy()");
    }

    //-------------------------------------------------------------------------
    public void Update()
    {
    }

    //-------------------------------------------------------------------------
    public void Test(string s)
    {
        Debug.Log("CsContext.Test() " + s);
    }
}