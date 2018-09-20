// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;

    public class StatefulSequence : BehaviorComponent
    {
        //---------------------------------------------------------------------
        private BehaviorComponent[] _Behaviors;
        private int _LastBehavior = 0;

        //---------------------------------------------------------------------
        // attempts to run the behaviors all in one cycle (stateful on running)
        // -Returns Success when all are successful
        // -Returns Failure if one behavior fails or an error occurs
        // -Does not Return Running
        public StatefulSequence(BehaviorTree bt, params BehaviorComponent[] behaviors)
            : base(bt)
        {
            this._Behaviors = behaviors;
        }

        //---------------------------------------------------------------------
        public override BehaviorReturnCode Behave()
        {
            // start from last remembered position
            for (; _LastBehavior < _Behaviors.Length; _LastBehavior++)
            {
                try
                {
                    switch (_Behaviors[_LastBehavior].Behave())
                    {
                        case BehaviorReturnCode.Failure:
                            _LastBehavior = 0;
                            ReturnCode = BehaviorReturnCode.Failure;
                            return ReturnCode;
                        case BehaviorReturnCode.Success:
                            continue;
                        case BehaviorReturnCode.Running:
                            ReturnCode = BehaviorReturnCode.Running;
                            return ReturnCode;
                        default:
                            _LastBehavior = 0;
                            ReturnCode = BehaviorReturnCode.Success;
                            return ReturnCode;
                    }
                }
                catch (Exception e)
                {
                    EbLog.Error(e.ToString());
                    _LastBehavior = 0;
                    ReturnCode = BehaviorReturnCode.Failure;
                    return ReturnCode;
                }
            }

            _LastBehavior = 0;
            ReturnCode = BehaviorReturnCode.Success;
            return ReturnCode;
        }
    }
}