// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    // 装饰节点，对行为节点返回的结果取反
    public class DecoratorInverter : BehaviorComponent
    {
        //---------------------------------------------------------------------
        private BehaviorComponent mBehavior;

        //---------------------------------------------------------------------
        // inverts the given behavior
        // -Returns Success on Failure or Error
        // -Returns Failure on Success 
        // -Returns Running on Running
        public DecoratorInverter(BehaviorTree bt, BehaviorComponent behavior)
            : base(bt)
        {
            mBehavior = behavior;
        }

        //---------------------------------------------------------------------
        public override BehaviorReturnCode Behave()
        {
            try
            {
                switch (mBehavior.Behave())
                {
                    case BehaviorReturnCode.Failure:
                        ReturnCode = BehaviorReturnCode.Success;
                        return ReturnCode;
                    case BehaviorReturnCode.Success:
                        ReturnCode = BehaviorReturnCode.Failure;
                        return ReturnCode;
                    case BehaviorReturnCode.Running:
                        ReturnCode = BehaviorReturnCode.Running;
                        return ReturnCode;
                }
            }
            catch (Exception e)
            {
                ReturnCode = BehaviorReturnCode.Success;
                return ReturnCode;
            }

            ReturnCode = BehaviorReturnCode.Success;
            return ReturnCode;
        }
    }
}
