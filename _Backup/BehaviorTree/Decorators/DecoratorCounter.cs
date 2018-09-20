// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    // 装饰节点，固定次数限制，满足次数后开始执行行为节点
    public class DecoratorCounter : BehaviorComponent
    {
        //---------------------------------------------------------------------
        private int mMaxCount;
        private int mCounter = 0;
        private BehaviorComponent mBehavior;

        //---------------------------------------------------------------------
        // executes the behavior based on a counter
        // -each time Counter is called the counter increments by 1
        // -Counter executes the behavior when it reaches the supplied maxCount
        public DecoratorCounter(BehaviorTree bt, int max_count, BehaviorComponent behavior)
            : base(bt)
        {
            mMaxCount = max_count;
            mBehavior = behavior;
        }

        //---------------------------------------------------------------------
        public override BehaviorReturnCode Behave()
        {
            try
            {
                if (mCounter < mMaxCount)
                {
                    mCounter++;
                    ReturnCode = BehaviorReturnCode.Running;
                    return BehaviorReturnCode.Running;
                }
                else
                {
                    mCounter = 0;
                    ReturnCode = mBehavior.Behave();
                    return ReturnCode;
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