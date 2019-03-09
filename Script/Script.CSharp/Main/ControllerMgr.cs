// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ControllerMgr
    {
        //-------------------------------------------------------------------------
        public ViewMgr ViewMgr { get; private set; }
        Dictionary<string, ControllerFactory> MapControllerFactory { get; set; } = new Dictionary<string, ControllerFactory>();
        Dictionary<string, Controller> MapController { get; set; } = new Dictionary<string, Controller>();

        //-------------------------------------------------------------------------
        public void RegControllerFactory(ControllerFactory factory)
        {
            MapControllerFactory[factory.GetName()] = factory;
        }

        //-------------------------------------------------------------------------
        public void Create()
        {
            ViewMgr = Context.Instance.ViewMgr;
        }

        //-------------------------------------------------------------------------
        public void Destroy()
        {
        }

        //-------------------------------------------------------------------------
        public T CreateController<T>() where T : Controller, new()
        {
            string name = typeof(T).Name;

            MapController.TryGetValue(name, out Controller controller);
            if (controller != null) return (T)controller;

            MapControllerFactory.TryGetValue(name, out ControllerFactory factory);
            if (factory == null)
            {
                string s = string.Format("ControllerMgr.CreateController()失败！ 指定ControllerName={0}没有注册！", name);
                Debug.LogError(s);
            }

            controller = factory.CreateController();
            controller.ViewMgr = ViewMgr;
            MapController[name] = controller;
            controller.Create();

            return (T)controller;
        }

        //-------------------------------------------------------------------------
        public T GetControler<T>() where T : Controller, new()
        {
            string name = typeof(T).Name;
            MapController.TryGetValue(name, out Controller controller);
            if (controller == null) return null;
            return (T)controller;
        }
    }
}
