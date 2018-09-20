// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RootSelector : PartialSelector
    {
        //---------------------------------------------------------------------
        //private BehaviorComponent[] mBehaviors;
        private Func<int> mFuncIndex;

        //---------------------------------------------------------------------
        // The selector for the root node of the behavior tree
        // <param name="index">an index representing which of the behavior branches to perform</param>
        // <param name="behaviors">the behavior branches to be selected from</param>
        public RootSelector(BehaviorTree bt, Func<int> func_index, params BehaviorComponent[] behaviors)
            : base(bt, behaviors)
        {
            mFuncIndex = func_index;
            //mBehaviors = behaviors;
        }

        //---------------------------------------------------------------------
        public override BehaviorReturnCode Behave()
        {
            try
            {
                switch (mBehaviors[mFuncIndex.Invoke()].Behave())
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
                        ReturnCode = BehaviorReturnCode.Running;
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