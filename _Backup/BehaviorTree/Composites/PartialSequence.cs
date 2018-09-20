// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PartialSequence : BehaviorComponent
    {
        //---------------------------------------------------------------------
        protected BehaviorComponent[] _Behaviors;
        private short _sequence = 0;
        private short _seqLength = 0;

        //---------------------------------------------------------------------
        // Performs the given behavior components sequentially (one evaluation per Behave call)
        // Performs an AND-Like behavior and will perform each successive component
        // -Returns Success if all behavior components return Success
        // -Returns Running if an individual behavior component returns Success or Running
        // -Returns Failure if a behavior components returns Failure or an error is encountered
        public PartialSequence(BehaviorTree bt, params BehaviorComponent[] behaviors)
            : base(bt)
        {
            _Behaviors = behaviors;
            _seqLength = (short) _Behaviors.Length;
        }

        //---------------------------------------------------------------------
        public override BehaviorReturnCode Behave()
        {
            // while you can go through them, do so
            while (_sequence < _seqLength)
            {
                try
                {
                    switch (_Behaviors[_sequence].Behave())
                    {
                        case BehaviorReturnCode.Failure:
                            _sequence = 0;
                            ReturnCode = BehaviorReturnCode.Failure;
                            return ReturnCode;
                        case BehaviorReturnCode.Success:
                            _sequence++;
                            ReturnCode = BehaviorReturnCode.Running;
                            return ReturnCode;
                        case BehaviorReturnCode.Running:
                            ReturnCode = BehaviorReturnCode.Running;
                            return ReturnCode;
                    }
                }
                catch (Exception e)
                {
                    EbLog.Error(e.ToString());
                    _sequence = 0;
                    ReturnCode = BehaviorReturnCode.Failure;
                    return ReturnCode;
                }
            }

            _sequence = 0;
            ReturnCode = BehaviorReturnCode.Success;
            return ReturnCode;
        }
    }
}