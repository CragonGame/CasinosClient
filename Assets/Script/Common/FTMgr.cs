// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public delegate void AllTaskDoneCallBack(Dictionary<byte, object> map_param);

    public class FTMgr
    {
        //---------------------------------------------------------------------
        List<FTasker> ListTaskerAll { get; set; }
        List<FTasker> ListTaskerCopy { get; set; }
        List<FTasker> ListTaskerDone { get; set; }
        public static FTMgr Instance { get; private set; }

        //---------------------------------------------------------------------
        public FTMgr()
        {
            Instance = this;
            ListTaskerAll = new List<FTasker>();
            ListTaskerCopy = new List<FTasker>();
            ListTaskerDone = new List<FTasker>();
        }

        //---------------------------------------------------------------------
        public void Update(float tm)
        {
            ListTaskerCopy.Clear();
            ListTaskerCopy.AddRange(ListTaskerAll);

            foreach (var i in ListTaskerCopy)
            {
                if (i == null) continue;

                if (i.isAllDone())
                {
                    ListTaskerDone.Add(i);
                }
                else
                {
                    i.update(tm);
                }
            }

            foreach (var i in ListTaskerDone)
            {
                ListTaskerAll.Remove(i);
            }

            ListTaskerDone.Clear();
        }

        //---------------------------------------------------------------------
        public FTask startTask(float fixed_time = 0)
        {
            FTask task = new FTask(fixed_time);
            return task;
        }

        //---------------------------------------------------------------------
        public void startTask(FTasker f_tasker)
        {
            ListTaskerAll.Add(f_tasker);
        }

        //---------------------------------------------------------------------
        public FTasker whenAll(Dictionary<byte, object> map_param, AllTaskDoneCallBack alltask_done_callback, params FTask[] task)
        {
            FTasker f_tasker = new FTasker(map_param, alltask_done_callback, task);
            ListTaskerAll.Add(f_tasker);
            return f_tasker;
        }
    }
}