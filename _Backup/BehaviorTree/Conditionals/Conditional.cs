// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Conditional : BehaviorComponent
    {
        //---------------------------------------------------------------------
        private Func<BehaviorTree, object[], Boolean> mFuncBool;
        private object[] mListParam;

        //---------------------------------------------------------------------
        // Returns a return code equivalent to the test 
        // -Returns Success if true
        // -Returns Failure if false
        // <param name="test">the value to be tested</param>
        public Conditional(BehaviorTree bt, Func<BehaviorTree, object[], Boolean> func_bool, params object[] list_param)
            : base(bt)
        {
            mFuncBool = func_bool;
            mListParam = list_param;
        }

        //---------------------------------------------------------------------
        public override BehaviorReturnCode Behave()
        {
            try
            {
                switch (mFuncBool.Invoke(mBehaviorTree, mListParam))
                {
                    case true:
                        ReturnCode = BehaviorReturnCode.Success;
                        return ReturnCode;
                    case false:
                        ReturnCode = BehaviorReturnCode.Failure;
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
