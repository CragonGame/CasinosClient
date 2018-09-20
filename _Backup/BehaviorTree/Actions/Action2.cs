// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Action2 : BehaviorComponent
    {
        //---------------------------------------------------------------------
        public Action2(BehaviorTree bt)
            : base(bt)
        {
        }

        //---------------------------------------------------------------------
        public override BehaviorReturnCode Behave()
        {
            return BehaviorReturnCode.Success;
        }
    }
}
