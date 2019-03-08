using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsContext
{
    //-------------------------------------------------------------------------
    public static CsContext Instance { get; private set; }
    public CsControllerMgr ControllerMgr { get; private set; }
    public CsViewMgr ViewMgr { get; private set; }

    //-------------------------------------------------------------------------
    public CsContext()
    {
        Instance = this;
    }

    //-------------------------------------------------------------------------
    public void Create()
    {
        Debug.Log("CsContext.Create()");

        ControllerMgr = new CsControllerMgr();

        ViewMgr = new CsViewMgr();
        ViewMgr.RegViewFactory(new CsViewFactory<Cs.ViewLaunch>());
        ViewMgr.Create();

        // 先显示Loading界面
        ViewMgr.CreateView<Cs.ViewLaunch>();

        //TestJson.Run();
        //TestMsgPack.Run();
        //TestProtobuf.Run();
    }

    //-------------------------------------------------------------------------
    public void Destroy()
    {
        if (ViewMgr != null)
        {
            ViewMgr.Destroy();
            ViewMgr = null;
        }

        if (ControllerMgr != null)
        {
            ControllerMgr.Destroy();
            ControllerMgr = null;
        }

        Debug.Log("CsContext.Destroy()");
    }

    //-------------------------------------------------------------------------
    public void Update()
    {
    }
}