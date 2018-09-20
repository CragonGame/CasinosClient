// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;

    public class StatefulSelector : BehaviorComponent
    {
        //---------------------------------------------------------------------
        private BehaviorComponent[] _Behaviors;
        private int _LastBehavior = 0;

        //---------------------------------------------------------------------
        // Selects among the given behavior components (stateful on running) 
        // Performs an OR-Like behavior and will "fail-over" to each successive component until Success is reached or Failure is certain
        // -Returns Success if a behavior component returns Success
        // -Returns Running if a behavior component returns Running
        // -Returns Failure if all behavior components returned Failure
        public StatefulSelector(BehaviorTree bt, params BehaviorComponent[] behaviors)
            : base(bt)
        {
            this._Behaviors = behaviors;
        }

        //---------------------------------------------------------------------
        public override BehaviorReturnCode Behave()
        {
            for (; _LastBehavior < _Behaviors.Length; _LastBehavior++)
            {
                try
                {
                    switch (_Behaviors[_LastBehavior].Behave())
                    {
                        case BehaviorReturnCode.Failure:
                            continue;
                        case BehaviorReturnCode.Success:
                            _LastBehavior = 0;
                            ReturnCode = BehaviorReturnCode.Success;
                            return ReturnCode;
                        case BehaviorReturnCode.Running:
                            ReturnCode = BehaviorReturnCode.Running;
                            return ReturnCode;
                        default:
                            continue;
                    }
                }
                catch (Exception e)
                {
                    EbLog.Error(e.ToString());
                    continue;
                }
            }

            _LastBehavior = 0;
            ReturnCode = BehaviorReturnCode.Failure;
            return ReturnCode;
        }
    }
}