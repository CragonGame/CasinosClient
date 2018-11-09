// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections.Generic;

    public delegate void DelayEndCallBack(Dictionary<byte, object> map_param);

    public class Delayer
    {
        //---------------------------------------------------------------------
        DelayMgr DelayMgr { get; set; }
        public float LeftTm { get; set; }
        Action Cb { get; set; }
        DelayEndCallBack Cb1 { get; set; }
        Dictionary<byte, object> MapParam1 { get; set; }

        //---------------------------------------------------------------------
        public Delayer(DelayMgr delay_mgr, float delay_tm, Action cb)
        {
            DelayMgr = delay_mgr;
            LeftTm = delay_tm;
            Cb = cb;
        }

        //---------------------------------------------------------------------
        public Delayer(DelayMgr delay_mgr, float delay_tm, DelayEndCallBack cb, Dictionary<byte, object> map_param)
        {
            DelayMgr = delay_mgr;
            LeftTm = delay_tm;
            Cb1 = cb;
            MapParam1 = map_param;
        }

        //---------------------------------------------------------------------
        public bool IsDone()
        {
            return LeftTm <= 0f;
        }

        //---------------------------------------------------------------------
        public void Kill(bool call_cb)
        {
            if (call_cb)
            {
                if (Cb != null) Cb();
                else if (Cb1 != null) Cb1(MapParam1);
            }
            DelayMgr._remove(this);
        }

        //---------------------------------------------------------------------
        public void _onEnd()
        {
            if (Cb != null) Cb();
            else if (Cb1 != null) Cb1(MapParam1);
        }
    }

    public class DelayMgr
    {
        //---------------------------------------------------------------------
        List<Delayer> ListDelayer { get; set; }
        Queue<Delayer> Que2Delete { get; set; }

        //---------------------------------------------------------------------
        public DelayMgr()
        {
            ListDelayer = new List<Delayer>(5);
            Que2Delete = new Queue<Delayer>(5);
        }

        //---------------------------------------------------------------------
        public void Update(float tm)
        {
            if (ListDelayer.Count > 0)
            {
                foreach (var i in ListDelayer)
                {
                    i.LeftTm -= tm;
                    if (i.IsDone()) Que2Delete.Enqueue(i);
                }

                while (Que2Delete.Count > 0)
                {
                    var delayer = Que2Delete.Dequeue();
                    ListDelayer.Remove(delayer);
                    delayer._onEnd();
                }
            }
        }

        //---------------------------------------------------------------------
        public void Close()
        {
            ListDelayer.Clear();
            Que2Delete.Clear();
        }

        //---------------------------------------------------------------------
        public Delayer Delay(float delay_tm, Action cb)
        {
            Delayer delayer = new Delayer(this, delay_tm, cb);
            ListDelayer.Add(delayer);
            return delayer;
        }

        //---------------------------------------------------------------------
        public Delayer Delay1(float delay_tm, DelayEndCallBack cb, Dictionary<byte, object> map_param)
        {
            Delayer delayer = new Delayer(this, delay_tm, cb, map_param);
            ListDelayer.Add(delayer);
            return delayer;
        }

        //---------------------------------------------------------------------
        public void _remove(Delayer delayer)
        {
            ListDelayer.Remove(delayer);
        }
    }
}