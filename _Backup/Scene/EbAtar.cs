// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;

    public enum _eEbAstarStepState
    {
        NONE,
        OPEN,
        CLOSE,
    }

    public struct EbAstarRoundStep
    {
        public EbAstarStep node;
        public float cost;
    }

    public struct EbAstarPos
    {
        public int x;
        public int y;
    }

    public class EbAstarStep : EbHeapNode
    {
        //---------------------------------------------------------------------
        public _eEbAstarStepState State = _eEbAstarStepState.NONE;
        public float G;
        public float H;
        public EbDoubleLinkNode2<EbAstarStep> AttachNode = new EbDoubleLinkNode2<EbAstarStep>();
        public EbAstarPos Pos;
        public float Cost = 0.0f;
        List<EbAstarRoundStep> _roundNode = new List<EbAstarRoundStep>();
        EbAstarStep mParent;
        public EbAstarStep Parent { get { return mParent; } set { mParent = value; } }
        public List<EbAstarRoundStep> RoundSteps { get { return _roundNode; } }
        public bool IsOpen { get { return (State == _eEbAstarStepState.OPEN); } }
        public bool IsClose { get { return (State == _eEbAstarStepState.CLOSE); } }

        //---------------------------------------------------------------------
        public static float Distance(EbAstarStep from, EbAstarStep to)
        {
            float delta_x = from.Pos.x - to.Pos.x;
            float delta_y = from.Pos.y - to.Pos.y;
            return (float)Math.Sqrt(delta_x * delta_x + delta_y * delta_y);
        }

        //---------------------------------------------------------------------
        public EbAstarStep()
        {
            AttachNode.Data = this;
        }

        //---------------------------------------------------------------------
        public void Clear()
        {
            G = 0;
            mParent = null;
            State = _eEbAstarStepState.NONE;
        }
    }

    public class EbAstarDestChecker
    {
        //---------------------------------------------------------------------
        public virtual bool isDest(EbAstarStep step)
        {
            return true;
        }
    }

    public class EbAstar<TDestChecker> where TDestChecker : EbAstarDestChecker, new()
    {
        //---------------------------------------------------------------------
        uint mStepCnt;
        uint mStepCntMax = 10000;
        EbAstarStep mStepStart;
        EbAstarStep mStepDest;
        EbAstarStep mStepCurrent;
        EbAstarStep mStepBest;
        EbMinHeap<EbAstarStep> mOpenHeap;
        EbDoubleLinkList2<EbAstarStep> mOpenList = new EbDoubleLinkList2<EbAstarStep>();
        EbDoubleLinkList2<EbAstarStep> mCloseList = new EbDoubleLinkList2<EbAstarStep>();
        public uint StepCnt { get { return mStepCnt; } }
        public uint StepCntMax { get { return mStepCntMax; } set { mStepCntMax = value; } }
        public TDestChecker DestChecker;

        //---------------------------------------------------------------------
        public EbAstar(uint heap_size)
        {
            mOpenHeap = new EbMinHeap<EbAstarStep>(heap_size);
            DestChecker = new TDestChecker();
        }

        //---------------------------------------------------------------------
        public bool Search(EbAstarStep src, EbAstarStep dest)
        {
            src.G = 0;
            src.H = EbAstarStep.Distance(src, dest);
            src.Key = src.G + src.H;

            mStepStart = src;
            mStepBest = src;
            mStepStart.Clear();
            mStepDest = dest;

            mStepCnt = 0;

            _AddToOpen(src);

            while (mOpenHeap.Size > 0)
            {
                mStepCurrent = mOpenHeap.Pop();
                if (DestChecker.isDest(mStepCurrent))
                {
                    mStepBest = mStepCurrent;
                    return true;
                }
                if (mStepCnt > mStepCntMax)
                {
                    return false;
                }
                if (mStepCurrent.H < mStepBest.H)
                {
                    mStepBest = mStepCurrent;
                }
                ++mStepCnt;

                _NewStep(mStepCurrent);
                mStepCurrent.AttachNode.Detach();
                _AddToClose(mStepCurrent);
            }
            return false;
        }

        //---------------------------------------------------------------------
        public void MakeRoute(List<EbAstarStep> list_step)
        {
            int dir = 1 + (1 << 2);
            EbAstarStep next = mStepBest;
            if (next == null)
            {
                return;
            }
            while (true)
            {
                EbAstarStep fromStep = next;
                next = next.Parent;
                if (next != null)
                {
                    EbAstarStep toStep = next;
                    int deltaX = toStep.Pos.x - fromStep.Pos.x + 1;
                    int deltaY = (toStep.Pos.y - fromStep.Pos.y + 1) << 2;
                    int delta = deltaY + deltaX;
                    if (delta != dir)
                    {
                        list_step.Insert(0, fromStep);
                        dir = delta;
                    }
                }
                else
                {
                    list_step.Insert(0, fromStep);
                    break;
                }
            }
        }

        //---------------------------------------------------------------------
        public void Reset()
        {
            EbDoubleLinkNode2<EbAstarStep> iter = mOpenList.GetHead();
            while (!mOpenList.IsEnd(iter))
            {
                iter.Data.Clear();
                EbDoubleLinkList2<EbAstarStep>.Next(ref iter);
            }
            mOpenList.Clear();

            iter = mCloseList.GetHead();
            while (!mCloseList.IsEnd(iter))
            {
                iter.Data.Clear();
                EbDoubleLinkList2<EbAstarStep>.Next(ref iter);
            }
            mCloseList.Clear();

            mOpenHeap.Clear();
        }

        //---------------------------------------------------------------------
        void _NewStep(EbAstarStep step)
        {
            for (int idx = 0; idx < step.RoundSteps.Count; ++idx)
            {
                EbAstarRoundStep roundStep = step.RoundSteps[idx];
                EbAstarStep childStep = roundStep.node;
                if (childStep.IsClose)
                {
                    continue;
                }
                float newG = roundStep.cost + step.G;
                if (childStep.IsOpen)
                {
                    if (childStep.G > newG)
                    {
                        childStep.G = newG;
                        childStep.Key = childStep.G + childStep.H;
                        childStep.Parent = step;
                    }
                }
                else
                {
                    childStep.G = newG;
                    childStep.H = EbAstarStep.Distance(childStep, mStepDest);
                    childStep.Key = childStep.G + childStep.H;
                    childStep.Parent = step;
                    _AddToOpen(childStep);
                }
            }
        }

        //---------------------------------------------------------------------
        void _AddToOpen(EbAstarStep step)
        {
            mOpenList.PushBack(step.AttachNode);
            step.State = _eEbAstarStepState.OPEN;
            mOpenHeap.Push(step);
        }

        //---------------------------------------------------------------------
        void _AddToClose(EbAstarStep step)
        {
            step.State = _eEbAstarStepState.CLOSE;
            mCloseList.PushBack(step.AttachNode);
        }
    }
}