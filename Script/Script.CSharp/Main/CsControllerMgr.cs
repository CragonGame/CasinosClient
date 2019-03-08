using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CsControllerMgr
{
    //-------------------------------------------------------------------------
    Dictionary<string, CsViewFactory> MapViewFactory { get; set; } = new Dictionary<string, CsViewFactory>();
    Dictionary<string, CsView> MapView { get; set; } = new Dictionary<string, CsView>();

    //-------------------------------------------------------------------------
    public void RegViewFactory(CsViewFactory factory)
    {
        MapViewFactory[factory.GetName()] = factory;
    }

    //-------------------------------------------------------------------------
    public void Create()
    {
    }

    //-------------------------------------------------------------------------
    public void Destroy()
    {
    }
}
