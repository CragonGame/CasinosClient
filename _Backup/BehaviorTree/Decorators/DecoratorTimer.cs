// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    // 装饰节点，时间限制，超过指定时间间隔后才开始执行行为节点
    public class DecoratorTimer : BehaviorComponent
    {
        //---------------------------------------------------------------------
        private Func<int> mFuncElapsedTm;
        private BehaviorComponent mBehavior;
        private int mTimeElapsed = 0;
        private int mWaitTime;

        //---------------------------------------------------------------------
        // executes the behavior after a given amount of time in miliseconds has passed
        // <param name="elapsedTimeFunction">function that returns elapsed time</param>
        // <param name="timeToWait">maximum time to wait before executing behavior</param>
        // <param name="behavior">behavior to run</param>
        public DecoratorTimer(BehaviorTree bt, Func<int> func_elapsedtm, int wait_tm, BehaviorComponent behavior)
            : base(bt)
        {
            mFuncElapsedTm = func_elapsedtm;
            mBehavior = behavior;
            mWaitTime = wait_tm;
        }

        //---------------------------------------------------------------------
        public override BehaviorReturnCode Behave()
        {
            try
            {
                mTimeElapsed += mFuncElapsedTm.Invoke();

                if (mTimeElapsed >= mWaitTime)
                {
                    mTimeElapsed = 0;
                    ReturnCode = mBehavior.Behave();
                    return ReturnCode;
                }
                else
                {
                    ReturnCode = BehaviorReturnCode.Running;
                    return BehaviorReturnCode.Running;
                }
            }
            catch (Exception e)
            {
                ReturnCode = BehaviorReturnCode.Failure;
                return BehaviorReturnCode.Failure;
            }
        }
    }
}
