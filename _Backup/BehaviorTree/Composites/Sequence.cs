// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Sequence : BehaviorComponent
    {
        //---------------------------------------------------------------------
        private BehaviorComponent[] _behaviors;

        //---------------------------------------------------------------------
        // attempts to run the behaviors all in one cycle
        // -Returns Success when all are successful
        // -Returns Failure if one behavior fails or an error occurs
        // -Returns Running if any are running
        public Sequence(BehaviorTree bt, params BehaviorComponent[] behaviors)
            : base(bt)
        {
            _behaviors = behaviors;
        }

        //---------------------------------------------------------------------
        public override BehaviorReturnCode Behave()
        {
			//add watch for any running behaviors
			bool anyRunning = false;

            for(int i = 0; i < _behaviors.Length;i++)
            {
                try
                {
                    switch (_behaviors[i].Behave())
                    {
                        case BehaviorReturnCode.Failure:
                            ReturnCode = BehaviorReturnCode.Failure;
                            return ReturnCode;
                        case BehaviorReturnCode.Success:
                            continue;
                        case BehaviorReturnCode.Running:
							anyRunning = true;
                            continue;
                        default:
                            ReturnCode = BehaviorReturnCode.Success;
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

			//if none running, return success, otherwise return running
            ReturnCode = !anyRunning ? BehaviorReturnCode.Success : BehaviorReturnCode.Running;
            return ReturnCode;
        }
    }
}