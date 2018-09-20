// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;

    public enum BehaviorReturnCode
    {
        Failure,
        Success,
        Running
    }

    public delegate BehaviorReturnCode BehaviorReturn();

    public class BehaviorTree
    {
        //---------------------------------------------------------------------
        private RootSelector mRoot;
        private BehaviorReturnCode mReturnCode;
        private Blackboard mBlackboard = new Blackboard();

        //---------------------------------------------------------------------
        public BehaviorReturnCode ReturnCode
        {
            get { return mReturnCode; }
            set { mReturnCode = value; }
        }

        public Blackboard Blackboard { get { return mBlackboard; } }

        //---------------------------------------------------------------------
        public BehaviorTree()
        {
        }

        //---------------------------------------------------------------------
        public void setRoot(RootSelector root)
        {
            mRoot = root;
        }

        //---------------------------------------------------------------------
        public void setRoot(BehaviorComponent behavior)
        {
            RootSelector root = new RootSelector(this, delegate() { return 0; }, behavior);
            mRoot = root;
        }

        //---------------------------------------------------------------------
        public BehaviorReturnCode Behave()
        {
            try
            {
                switch (mRoot.Behave())
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
                ReturnCode = BehaviorReturnCode.Failure;
                return ReturnCode;
            }
        }
    }
}
