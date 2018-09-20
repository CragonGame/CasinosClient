// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Action1 : BehaviorComponent
    {
        //---------------------------------------------------------------------
        private Func<BehaviorTree, object[], BehaviorReturnCode> mAction;
        private object[] mListParam;

        //---------------------------------------------------------------------
        public Action1(BehaviorTree bt, Func<BehaviorTree, object[], BehaviorReturnCode> action, params object[] list_param)
            : base(bt)
        {
            mAction = action;
            mListParam = list_param;
        }

        //---------------------------------------------------------------------
        public override BehaviorReturnCode Behave()
        {
            try
            {
                switch (mAction.Invoke(mBehaviorTree, mListParam))
                {
                    case BehaviorReturnCode.Success:
                        ReturnCode = BehaviorReturnCode.Success;
                        return ReturnCode;
                    case BehaviorReturnCode.Failure:
                        ReturnCode = BehaviorReturnCode.Failure;
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
