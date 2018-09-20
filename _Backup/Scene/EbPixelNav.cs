// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;

    class EbPixelNavigateDestChecker0 : EbAstarDestChecker
    {
        //-------------------------------------------------------------------------
        public EbAstarStep Dest;

        //-------------------------------------------------------------------------
        public override bool isDest(EbAstarStep step)
        {
            return (Object.ReferenceEquals(step, Dest));
        }
    }

    class EbPixelNavigateDestChecker1 : EbAstarDestChecker
    {
        //-------------------------------------------------------------------------
        public EbAstarStep Dest;
        public float Diff;

        //-------------------------------------------------------------------------
        public override bool isDest(EbAstarStep step)
        {
            return (EbAstarStep.Distance(step, Dest) < Diff);
        }
    }

    public struct EbPixelNavigatePickData
    {
        //-------------------------------------------------------------------------
        public int blockX;
        public int blockY;
        public int lastFreeX;
        public int lastFreeY;
    }

    public class EbPixelNav
    {
        //---------------------------------------------------------------------
        int mSizeX;
        int mSizeY;
        EbAstarStep[] mListStep;
        List<EbVector2> mRoute = new List<EbVector2>();
        List<EbAstarStep> mStepPool = new List<EbAstarStep>();
        EbAstar<EbPixelNavigateDestChecker0> mAstar0;
        EbAstar<EbPixelNavigateDestChecker1> mAstar1;

        //---------------------------------------------------------------------
        public List<EbVector2> Route { get { return mRoute; } }
        public int SizeX { get { return mSizeX; } }
        public int SizeY { get { return mSizeY; } }

        //---------------------------------------------------------------------
        public EbPixelNav(uint heap_size)
        {
            mAstar0 = new EbAstar<EbPixelNavigateDestChecker0>(heap_size);
            mAstar1 = new EbAstar<EbPixelNavigateDestChecker1>(heap_size);
        }

        //---------------------------------------------------------------------
        public void blockStart(int size_x, int size_y)
        {
            mSizeX = size_x;
            mSizeY = size_y;
            mListStep = new EbAstarStep[mSizeX * mSizeY];

            for (int y = 0; y < mSizeY; ++y)
            {
                for (int x = 0; x < mSizeX; ++x)
                {
                    EbAstarStep step = new EbAstarStep();
                    step.Cost = 0f;
                    step.Pos.x = x;
                    step.Pos.y = y;
                    mListStep[x + y * mSizeX] = step;
                }
            }
        }

        //---------------------------------------------------------------------
        public void setBlock(int x, int y)
        {
            if (x < mSizeX && y < mSizeY)
            {
                mListStep[x + y * mSizeX].Cost = 1f;
            }
        }

        //---------------------------------------------------------------------
        public void blockEnd()
        {
            for (int y = 1; y < mSizeY - 1; ++y)
            {
                for (int x = 1; x < mSizeX - 1; ++x)
                {
                    _fresh(x, y);
                }
            }
        }

        //---------------------------------------------------------------------
        public bool isBlock(int x, int y)
        {
            return isValue(x, y, 1f);
        }

        //---------------------------------------------------------------------
        public bool isValue(int x, int y, float cost)
        {
            if (x < mSizeX && y < mSizeY && x >= 0 && y >= 0)
            {
                return (mListStep[x + y * mSizeX].Cost == cost);
            }
            else
            {
                return false;
            }
        }

        //---------------------------------------------------------------------
        public bool getRound(int iRootX, int iRootY, float fCost, int iRange, ref int iRoundX, ref int iRoundY)
        {
            if (isValue(iRootX, iRootY, fCost))
            {
                iRoundX = iRootX;
                iRoundY = iRootY;
                return true;
            }
            for (int iRangeIdx = 1; iRangeIdx < iRange; ++iRangeIdx)
            {
                // iSrcX - iRangeIdx
                for (int iIdx = -iRangeIdx; iIdx <= iRangeIdx; ++iIdx)
                {
                    int iX = iRootX - iRangeIdx;
                    int iY = iRootY + iIdx;
                    if (isValue(iX, iY, fCost))
                    {
                        iRoundX = iX;
                        iRoundY = iY;
                        return true;
                    }
                }
                // iSrcX + iRangeIdx
                for (int iIdx = -iRangeIdx; iIdx <= iRangeIdx; ++iIdx)
                {
                    int iX = iRootX + iRangeIdx;
                    int iY = iRootY + iIdx;
                    if (isValue(iX, iY, fCost))
                    {
                        iRoundX = iX;
                        iRoundY = iY;
                        return true;
                    }
                }
                // iSrcY - iRangeIdx
                for (int iIdx = -iRangeIdx; iIdx <= iRangeIdx; ++iIdx)
                {
                    int iX = iRootX + iIdx;
                    int iY = iRootY - iRangeIdx;
                    if (isValue(iX, iY, fCost))
                    {
                        iRoundX = iX;
                        iRoundY = iY;
                        return true;
                    }
                }
                // iSrcY + iRangeIdx
                for (int iIdx = -iRangeIdx; iIdx <= iRangeIdx; ++iIdx)
                {
                    int iX = iRootX + iIdx;
                    int iY = iRootY + iRangeIdx;
                    if (isValue(iX, iY, fCost))
                    {
                        iRoundX = iX;
                        iRoundY = iY;
                        return true;
                    }
                }
            }
            return false;
        }

        //---------------------------------------------------------------------
        public bool getRoundInRange1(int iRootX, int iRootY, float fCost, ref int iRoundX, ref int iRoundY)
        {
            if (isValue(iRootX, iRootY, fCost))
            {
                iRoundX = iRootX;
                iRoundY = iRootY;
                return true;
            }
            for (int iY = iRootY - 1; iY <= iRootY + 1; ++iY)
            {
                for (int iX = iRootX - 1; iX <= iRootX + 1; ++iX)
                {
                    if (isValue(iX, iY, fCost))
                    {
                        iRoundX = iX;
                        iRoundY = iY;
                        return true;
                    }
                }
            }
            return false;
        }

        //---------------------------------------------------------------------
        public bool search(int src_x, int src_y, int dst_x, int dst_y, uint max_step)
        {
            mRoute.Clear();

            if ((src_x < mSizeX && src_y < mSizeY) == false || (dst_x < mSizeX && dst_y < mSizeY) == false)
            {
                return false;
            }

            getRound(src_x, src_y, 0f, 100, ref src_x, ref src_y);
            if (src_x == dst_x && src_y == dst_y)
            {
                mRoute.Add(new EbVector2(src_x, src_y));
                mRoute.Add(new EbVector2(dst_x, dst_y));
                return true;
            }

            if (!pick(src_x, src_y, dst_x, dst_y))
            {
                mRoute.Add(new EbVector2(src_x, src_y));
                mRoute.Add(new EbVector2(dst_x, dst_y));
                return true;
            }

            EbAstarStep src_step = mListStep[src_x + src_y * mSizeX];
            EbAstarStep dst_step = mListStep[dst_x + dst_y * mSizeX];
            mAstar0.DestChecker.Dest = dst_step;
            mAstar0.StepCntMax = max_step;
            bool reach_dest = mAstar0.Search(src_step, dst_step);
            mStepPool.Clear();
            mAstar0.MakeRoute(mStepPool);
            mAstar0.Reset();
            clipRoute(mStepPool, mRoute);

            return reach_dest;
        }

        //---------------------------------------------------------------------
        public bool search(int src_x, int src_y, int dst_x, int dst_y, float diff, uint max_step)
        {
            if ((src_x < mSizeX && src_y < mSizeY) == false || (dst_x < mSizeX && dst_y < mSizeY) == false)
            {
                return false;
            }

            getRound(src_x, src_y, 0f, 100, ref src_x, ref src_y);
            EbAstarStep src_step = mListStep[src_x + src_y * mSizeX];
            EbAstarStep dst_step = mListStep[dst_x + dst_y * mSizeX];
            mAstar1.DestChecker.Dest = dst_step;
            mAstar1.DestChecker.Diff = diff;
            mAstar1.StepCntMax = max_step;
            bool reach_dst = mAstar1.Search(src_step, dst_step);
            mRoute.Clear();
            mStepPool.Clear();
            mAstar1.MakeRoute(mStepPool);
            mAstar1.Reset();
            clipRoute(mStepPool, mRoute);
            return reach_dst;
        }

        //---------------------------------------------------------------------
        public void clipRoute(List<EbAstarStep> complex, List<EbVector2> simple)
        {
            if (complex.Count <= 2)
            {
                for (int idx = 0; idx < complex.Count; ++idx)
                {
                    EbAstarStep pre = complex[idx];
                    simple.Add(new EbVector2(pre.Pos.x, pre.Pos.y));
                }
                return;
            }

            EbAstarStep from = complex[0];
            simple.Add(new EbVector2(from.Pos.x, from.Pos.y));
            for (int idx = 1; idx < complex.Count; ++idx)
            {
                EbAstarStep pre = complex[idx - 1];
                EbAstarStep current = complex[idx];
                if (pick(from.Pos.x, from.Pos.y, current.Pos.x, current.Pos.y))
                {
                    simple.Add(new EbVector2(pre.Pos.x, pre.Pos.y));
                    from = pre;
                }
            }
            EbAstarStep back = complex[complex.Count - 1];
            simple.Add(new EbVector2(back.Pos.x, back.Pos.y));
        }

        //---------------------------------------------------------------------
        public bool pick(int fromX, int fromY, int destX, int destY)
        {
            EbPixelNavigatePickData data = new EbPixelNavigatePickData();
            return Pick(fromX, fromY, destX, destY, 1f, ref data);
        }

        //---------------------------------------------------------------------
        public bool Pick(int fromX, int fromY, int destX, int destY, float value, ref EbPixelNavigatePickData result)
        {
            int deltaX = destX - fromX;
            int deltaY = destY - fromY;
            if (deltaX == 0 && deltaY == 0)
            {
                return false;
            }
            float absDeltaX = Math.Abs((float)deltaX);
            float absDeltaY = Math.Abs((float)deltaY);
            if (absDeltaX >= absDeltaY)
            {
                if (deltaX < 0)
                {
                    return _pickXNegative(fromX, fromY, destX, destY, value, ref result);
                }
                else
                {
                    return _pickXPositive(fromX, fromY, destX, destY, value, ref result);
                }
            }
            else
            {
                if (deltaY < 0)
                {
                    return _pickYNegative(fromX, fromY, destX, destY, value, ref result);
                }
                else
                {
                    return _pickYPositive(fromX, fromY, destX, destY, value, ref result);
                }
            }
        }

        //---------------------------------------------------------------------
        bool _pickXNegative(int fromX, int fromY, int destX, int destY, float value, ref EbPixelNavigatePickData result)
        {
            int deltaX = destX - fromX;
            int deltaY = destY - fromY;

            float fromCenterX = (float)fromX + 0.5f;
            float fromCenterY = (float)fromY + 0.5f;

            float fDeltaX = -1.0f;
            float fDeltaY = (float)deltaY / Math.Abs((float)deltaX);

            float pickX = fromCenterX;
            float pickY = fromCenterY;

            for (int idx = fromX; idx > destX; --idx)
            {
                pickX += fDeltaX;
                pickY += fDeltaY;

                int newCellX = (int)pickX;
                int newCellY = (int)pickY;
                if (mListStep[newCellX + newCellY * mSizeX].Cost == value)
                {
                    result.blockX = newCellX;
                    result.blockY = newCellY;
                    result.lastFreeX = (int)(pickX - fDeltaX);
                    result.lastFreeY = (int)(pickY - fDeltaY);
                    return true;
                }
            }
            if (mListStep[destX + destY * mSizeX].Cost == value)
            {
                result.blockX = destX;
                result.blockY = destY;
                result.lastFreeX = (int)(pickX);
                result.lastFreeY = (int)(pickY);
                return true;
            }
            return false;
        }

        //---------------------------------------------------------------------
        bool _pickXPositive(int fromX, int fromY, int destX, int destY, float value, ref EbPixelNavigatePickData result)
        {
            int deltaX = destX - fromX;
            int deltaY = destY - fromY;

            float fromCenterX = (float)fromX + 0.5f;
            float fromCenterY = (float)fromY + 0.5f;

            float fDeltaX = 1.0f;
            float fDeltaY = (float)deltaY / Math.Abs((float)deltaX);

            float pickX = fromCenterX;
            float pickY = fromCenterY;

            for (int idx = fromX; idx < destX; ++idx)
            {
                pickX += fDeltaX;
                pickY += fDeltaY;

                int newCellX = (int)pickX;
                int newCellY = (int)pickY;
                if (mListStep[newCellX + newCellY * mSizeX].Cost == value)
                {
                    result.blockX = newCellX;
                    result.blockY = newCellY;
                    result.lastFreeX = (int)(pickX - fDeltaX);
                    result.lastFreeY = (int)(pickY - fDeltaY);
                    return true;
                }
            }
            if (mListStep[destX + destY * mSizeX].Cost == value)
            {
                result.blockX = destX;
                result.blockY = destY;
                result.lastFreeX = (int)(pickX);
                result.lastFreeY = (int)(pickY);
                return true;
            }
            return false;
        }

        //---------------------------------------------------------------------
        bool _pickYNegative(int fromX, int fromY, int destX, int destY, float value, ref EbPixelNavigatePickData result)
        {
            int deltaX = destX - fromX;
            int deltaY = destY - fromY;

            float fromCenterX = (float)fromX + 0.5f;
            float fromCenterY = (float)fromY + 0.5f;

            float fDeltaX = (float)deltaX / Math.Abs((float)deltaY);
            float fDeltaY = -1.0f;
            float pickX = fromCenterX;
            float pickY = fromCenterY;

            for (int idx = fromY; idx > destY; --idx)
            {
                pickX += fDeltaX;
                pickY += fDeltaY;

                int newCellX = (int)pickX;
                int newCellY = (int)pickY;
                if (mListStep[newCellX + newCellY * mSizeX].Cost == value)
                {
                    result.blockX = newCellX;
                    result.blockY = newCellY;
                    result.lastFreeX = (int)(pickX - fDeltaX);
                    result.lastFreeY = (int)(pickY - fDeltaY);
                    return true;
                }
            }
            if (mListStep[destX + destY * mSizeX].Cost == value)
            {
                result.blockX = destX;
                result.blockY = destY;
                result.lastFreeX = (int)(pickX);
                result.lastFreeY = (int)(pickY);
                return true;
            }
            return false;
        }

        //---------------------------------------------------------------------
        bool _pickYPositive(int fromX, int fromY, int destX, int destY, float value, ref EbPixelNavigatePickData result)
        {
            int deltaX = destX - fromX;
            int deltaY = destY - fromY;

            float fromCenterX = (float)fromX + 0.5f;
            float fromCenterY = (float)fromY + 0.5f;

            float fDeltaX = (float)deltaX / Math.Abs((float)deltaY);
            float fDeltaY = 1.0f;
            float pickX = fromCenterX;
            float pickY = fromCenterY;

            for (int idx = fromY; idx < destY; ++idx)
            {
                pickX += fDeltaX;
                pickY += fDeltaY;

                int newCellX = (int)pickX;
                int newCellY = (int)pickY;
                if (mListStep[newCellX + newCellY * mSizeX].Cost == value)
                {
                    result.blockX = newCellX;
                    result.blockY = newCellY;
                    result.lastFreeX = (int)(pickX - fDeltaX);
                    result.lastFreeY = (int)(pickY - fDeltaY);
                    return true;
                }
            }
            if (mListStep[destX + destY * mSizeX].Cost == value)
            {
                result.blockX = destX;
                result.blockY = destY;
                result.lastFreeX = (int)(pickX);
                result.lastFreeY = (int)(pickY);
                return true;
            }
            return false;
        }

        //---------------------------------------------------------------------
        void _fresh(int x, int y)
        {
            EbAstarStep step = mListStep[x + y * mSizeX];
            if (step.Cost > 0f)
            {
                return;
            }
            for (int yIdx = y - 1; yIdx < y + 2; ++yIdx)
            {
                for (int xIdx = x - 1; xIdx < x + 2; ++xIdx)
                {
                    if (xIdx == x && yIdx == y)
                    {
                        continue;
                    }
                    EbAstarStep roundStep = mListStep[xIdx + yIdx * mSizeX];
                    if (roundStep.Cost == 0f)
                    {
                        EbAstarRoundStep node = new EbAstarRoundStep();
                        node.node = roundStep;
                        
                        float deltaX = x - xIdx;
                        float deltaY = y - yIdx;
                        node.cost = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
                        step.RoundSteps.Add(node);
                    }
                    else
                    {
                        roundStep.Cost = 1f;
                    }
                }
            }
        }
    }
}