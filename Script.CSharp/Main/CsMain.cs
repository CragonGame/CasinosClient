using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsMain
{
    //-------------------------------------------------------------------------
    static CsContext CsContext { get; set; }

    //-------------------------------------------------------------------------
    public static void Launch()
    {
        Debug.Log("CsMain.Launch()");

        CsContext = new CsContext();
        CsContext.Create();
        CsContext.Destroy();
    }
}