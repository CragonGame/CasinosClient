// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RandomSelector : BehaviorComponent
    {
        //---------------------------------------------------------------------
        private BehaviorComponent[] _Behaviors;
        // use current milliseconds to set random seed
        private Random _Random = new Random(DateTime.Now.Millisecond);

        //---------------------------------------------------------------------
        // Randomly selects and performs one of the passed behaviors
        // -Returns Success if selected behavior returns Success
        // -Returns Failure if selected behavior returns Failure
        // -Returns Running if selected behavior returns Running
        public RandomSelector(BehaviorTree bt, params BehaviorComponent[] behaviors)
            : base(bt)
        {
            _Behaviors = behaviors;
        }

        //---------------------------------------------------------------------
        public override BehaviorReturnCode Behave()
        {
            _Random = new Random(DateTime.Now.Millisecond);

            try
            {
                switch (_Behaviors[_Random.Next(0, _Behaviors.Length - 1)].Behave())
                {
                    case BehaviorReturnCode.Failure:
                        ReturnCode = BehaviorReturnCode.Failure;
                        return ReturnCode;
                    case BehaviorReturnCode.Success:
                        ReturnCode = BehaviorReturnCode.Success;
                        return ReturnCode;
                    case BehaviorReturnCode.Running:
                        ReturnCode = BehaviorReturnCode.Running;
                        return ReturnCode;
                    default:
                        ReturnCode = BehaviorReturnCode.Failure;
                        return ReturnCode;
                }
            }
            catch (Exception e)
            {
                EbLog.Error(e.ToString());
                ReturnCode = BehaviorReturnCode.Failure;
                return ReturnCode;
            }
        }
    }
}