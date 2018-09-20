// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    // 装饰节点，随机次数限制，每次满足随机结果后才开始执行一次行为节点
    public class DecoratorRandom : BehaviorComponent
    {
        //---------------------------------------------------------------------
        private float mProbability;
        private Func<float> mRandomFunction;
        private BehaviorComponent mBehavior;

        //---------------------------------------------------------------------
        // randomly executes the behavior
        // <param name="probability">probability of execution</param>
        // <param name="randomFunction">function that determines probability to execute</param>
        // <param name="behavior">behavior to execute</param>
        public DecoratorRandom(BehaviorTree bt, float probability, Func<float> func_random, BehaviorComponent behavior)
            : base(bt)
        {
            mProbability = probability;
            mRandomFunction = func_random;
            mBehavior = behavior;
        }

        //---------------------------------------------------------------------
        public override BehaviorReturnCode Behave()
        {
            try
            {
                if (mRandomFunction.Invoke() <= mProbability)
                {
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
