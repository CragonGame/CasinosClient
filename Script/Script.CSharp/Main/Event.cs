// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System;
    using System.Collections.Generic;

    public class Event
    {
        //---------------------------------------------------------------------
        public void Broadcast()
        {
            string name = GetType().Name;
            Context.Instance.EventMgr._broadcastEvent(name, this);
        }
    }

    public abstract class EventListener
    {
        //---------------------------------------------------------------------
        List<string> ListBindEvent { get; set; } = new List<string>();

        //---------------------------------------------------------------------
        public void _addBindEvent(string ev_name)
        {
            ListBindEvent.Add(ev_name);
        }

        //---------------------------------------------------------------------
        public List<string> _getAllBindEvent()
        {
            return ListBindEvent;
        }

        //---------------------------------------------------------------------
        public void _clearAllBindEvent()
        {
            ListBindEvent.Clear();
        }

        //---------------------------------------------------------------------
        public abstract void HandleEvent(Event ev);
    }
}
