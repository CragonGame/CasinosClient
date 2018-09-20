// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PartialSelector : BehaviorComponent
    {
        //---------------------------------------------------------------------
        protected BehaviorComponent[] mBehaviors;
        private short _selections = 0;
        private short _selLength = 0;

        //---------------------------------------------------------------------
        // Selects among the given behavior components (one evaluation per Behave call)
        // Performs an OR-Like behavior and will "fail-over" to each successive component until Success is reached or Failure is certain
        // -Returns Success if a behavior component returns Success
        // -Returns Running if a behavior component returns Failure or Running
        // -Returns Failure if all behavior components returned Failure or an error has occured
        public PartialSelector(BehaviorTree bt, params BehaviorComponent[] behaviors)
            : base(bt)
        {
            mBehaviors = behaviors;
            _selLength = (short)mBehaviors.Length;
        }

        //---------------------------------------------------------------------
        public override BehaviorReturnCode Behave()
        {
            while (_selections < _selLength)
            {
                try
                {
                    switch (mBehaviors[_selections].Behave())
                    {
                        case BehaviorReturnCode.Failure:
                            _selections++;
                            ReturnCode = BehaviorReturnCode.Running;
                            return ReturnCode;
                        case BehaviorReturnCode.Success:
                            _selections = 0;
                            ReturnCode = BehaviorReturnCode.Success;
                            return ReturnCode;
                        case BehaviorReturnCode.Running:
                            ReturnCode = BehaviorReturnCode.Running;
                            return ReturnCode;
                        default:
                            _selections++;
                            ReturnCode = BehaviorReturnCode.Failure;
                            return ReturnCode;
                    }
                }
                catch (Exception e)
                {
                    EbLog.Error(e.ToString());
                    _selections++;
                    ReturnCode = BehaviorReturnCode.Failure;
                    return ReturnCode;
                }
            }

            _selections = 0;
            ReturnCode = BehaviorReturnCode.Failure;
            return ReturnCode;
        }
    }
}