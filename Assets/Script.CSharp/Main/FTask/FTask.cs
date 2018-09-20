// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections;

    public class FTask
    {
        //-------------------------------------------------------------------------
        bool IsDone { get; set; }
        float FixedTime { get; set; }

        //-------------------------------------------------------------------------
        public FTask(float fixed_time)
        {
            IsDone = false;
            FixedTime = fixed_time;
        }

        //-------------------------------------------------------------------------
        public void startAutoTask(float fixed_time)
        {
            IsDone = false;
            FixedTime = fixed_time;
        }

        //-------------------------------------------------------------------------
        public void update(float tm)
        {
            if (FixedTime > 0)
            {
                FixedTime -= tm;

                if (FixedTime <= 0)
                {
                    IsDone = true;
                }
            }
        }

        //-------------------------------------------------------------------------
        public bool Done()
        {
            return IsDone;
        }

        //-------------------------------------------------------------------------
        public void setTaskDone()
        {
            IsDone = true;
        }
    }
}