using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CsViewMgr
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

    //-------------------------------------------------------------------------
    public T CreateView<T>() where T : CsView
    {
        var name = nameof(T);
        MapViewFactory.TryGetValue(name, out CsViewFactory factory);
        if (factory == null)
        {
            Debug.LogError("CsViewMgr.CreateView() 指定ViewName=" + name + "没有注册！");
            return null;
        }

        var view = factory.CreateView();
        view.Create();

        return (T)view;
    }

    //-------------------------------------------------------------------------
    public void Destroy<T>() where T : CsView
    {
        var name = nameof(T);
        MapViewFactory.TryGetValue(name, out CsViewFactory factory);
        if (factory == null)
        {
            Debug.LogError("CsViewMgr.Destroy() 指定ViewName=" + name + "没有注册！");
            return;
        }

        MapView.TryGetValue(name, out CsView view);

        if (view == null)
        {
            return;
        }

        view.Destory();
        MapView.Remove(name);
    }
}
