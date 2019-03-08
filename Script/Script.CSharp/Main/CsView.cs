using System;
using System.Collections.Generic;
using FairyGUI;

public abstract class CsView
{
    //-------------------------------------------------------------------------
    public GComponent GCom { get; set; }

    //-------------------------------------------------------------------------
    public abstract void Create();

    //-------------------------------------------------------------------------
    public abstract void Destory();

    //-------------------------------------------------------------------------
    public abstract void HandleEvent(CsEvent ev);
}

public abstract class CsViewFactory
{
    //-------------------------------------------------------------------------
    public abstract string GetName();

    //-------------------------------------------------------------------------
    public abstract CsView CreateView();
}

public class CsViewFactory<T> : CsViewFactory where T : CsView, new()
{
    //-------------------------------------------------------------------------
    public override string GetName()
    {
        return nameof(T);
    }

    //-------------------------------------------------------------------------
    public override CsView CreateView()
    {
        return new T();
    }
}
