// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class FTasker
    {
        //-------------------------------------------------------------------------
        bool IsAllDone { get; set; }
        bool CancelTask { get; set; }
        List<FTask> ListFTask { get; set; }
        List<FTask> ListDoneTask { get; set; }
        AllTaskDoneCallBack AllTaskDoneCallBack { get; set; }
        Dictionary<byte, object> MapParam { get; set; }

        //-------------------------------------------------------------------------
        public FTasker()
        {
            ListFTask = new List<FTask>();
            ListDoneTask = new List<FTask>();
        }

        //-------------------------------------------------------------------------
        public FTasker(Dictionary<byte, object> map_param, AllTaskDoneCallBack alltask_done_callback, params FTask[] task)
        {
            MapParam = map_param;
            AllTaskDoneCallBack = alltask_done_callback;
            ListFTask = new List<FTask>();
            foreach (var i in task)
            {
                ListFTask.Add(i);
            }
            ListDoneTask = new List<FTask>();
            IsAllDone = false;
            CancelTask = false;
        }

        //-------------------------------------------------------------------------
        public void whenAll(Dictionary<byte, object> map_param, AllTaskDoneCallBack alltask_done_callback, params FTask[] task)
        {
            MapParam = map_param;              
            AllTaskDoneCallBack = alltask_done_callback;
            foreach (var i in task)
            {
                ListFTask.Add(i);
            }
            IsAllDone = false;
            CancelTask = false;
        }

        //-------------------------------------------------------------------------
        public void update(float tm)
        {
            foreach (var i in ListFTask)
            {
                if (i.Done())
                {
                    ListDoneTask.Add(i);
                }
                else
                {
                    i.update(tm);
                }
            }

            if (ListDoneTask.Count == ListFTask.Count)
            {
                IsAllDone = true;
                if (!CancelTask && AllTaskDoneCallBack != null)
                {
                    AllTaskDoneCallBack(MapParam);
                    AllTaskDoneCallBack = null;
                }
                ListDoneTask.Clear();
                ListFTask.Clear();
            }
        }

        //-------------------------------------------------------------------------
        public bool isAllDone()
        {
            return IsAllDone;
        }

        //-------------------------------------------------------------------------
        public void cancelTask()
        {
            CancelTask = true;
            ListFTask.Clear();
            ListDoneTask.Clear();
        }
    }
}