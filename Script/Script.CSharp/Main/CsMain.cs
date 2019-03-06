using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsMain
{
    //-------------------------------------------------------------------------
    static CsContext CsContext { get; set; }

    //-------------------------------------------------------------------------
    public static void Create()
    {
        CsContext = new CsContext();
        CsContext.Create();
    }

    //-------------------------------------------------------------------------
    public static void Destroy()
    {
        CsContext.Destroy();
    }

    //-------------------------------------------------------------------------
    public static void Update()
    {
        CsContext.Update();
    }
}