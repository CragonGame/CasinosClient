// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System;
    using System.Collections.Generic;

    public class EventMgr
    {
        //---------------------------------------------------------------------
        Dictionary<string, HashSet<EventListener>> MapEvAndListener { get; set; } = new Dictionary<string, HashSet<EventListener>>();
        Dictionary<string, Queue<Event>> MapEvPool { get; set; } = new Dictionary<string, Queue<Event>>();// 不同类型事件的内存池

        //---------------------------------------------------------------------
        public void ListenEvent<T>(EventListener listener) where T : Event
        {
            string name = typeof(T).Name;
            MapEvAndListener.TryGetValue(name, out HashSet<EventListener> set_listener);

            if (set_listener == null)
            {
                set_listener = new HashSet<EventListener>();
                MapEvAndListener[name] = set_listener;
            }

            listener._addBindEvent(name);
            set_listener.Add(listener);
        }

        //---------------------------------------------------------------------
        public void UnListenAllEvent(EventListener listener)
        {
            List<string> list_bindevent = listener._getAllBindEvent();
            foreach (var i in list_bindevent)
            {
                MapEvAndListener.TryGetValue(i, out HashSet<EventListener> set_listener);
                if (set_listener != null)
                {
                    set_listener.Remove(listener);
                }
            }
        }

        //---------------------------------------------------------------------
        public T GenEvent<T>() where T : Event, new()
        {
            string name = typeof(T).Name;
            MapEvPool.TryGetValue(name, out Queue<Event> que_ev);
            if (que_ev == null)
            {
                T ev = new T();
                return ev;
            }
            else
            {
                return (T)que_ev.Dequeue();
            }
        }

        //---------------------------------------------------------------------
        public void _broadcastEvent(string ev_name, Event ev)
        {
            MapEvAndListener.TryGetValue(ev_name, out HashSet<EventListener> set_listener);
            if (set_listener != null)
            {
                foreach (var i in set_listener)
                {
                    i.HandleEvent(ev);
                }
            }

            MapEvPool.TryGetValue(ev_name, out Queue<Event> que_ev);
            if (que_ev == null)
            {
                que_ev = new Queue<Event>();
                MapEvPool[ev_name] = que_ev;
            }

            que_ev.Enqueue(ev);
        }
    }
}
