// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class BehaviorComponent
    {
        //---------------------------------------------------------------------
        protected BehaviorReturnCode ReturnCode;
        protected BehaviorTree mBehaviorTree;

        //---------------------------------------------------------------------
        public BehaviorTree BehaviorTree { get { return mBehaviorTree; } }

        //---------------------------------------------------------------------
        public BehaviorComponent(BehaviorTree bt) { mBehaviorTree = bt; }

        //---------------------------------------------------------------------
        public abstract BehaviorReturnCode Behave();
    }
}
