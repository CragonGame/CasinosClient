// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;

    public class EbHeapNode
    {
        public float Key;
    }

    internal class EbMinHeap<T> where T : EbHeapNode, new()
    {
        //---------------------------------------------------------------------
        uint mSize;
        T[] mListData;
        public bool IsEmpty { get { return (mSize == 0); } }
        public uint Size { get { return mSize; } }

        //---------------------------------------------------------------------
        public EbMinHeap(uint size)
        {
            mListData = new T[size];
        }

        //---------------------------------------------------------------------
        public void Push(T kT)
        {
            if (mSize < mListData.Length - 1)
            {
                ++mSize;
                mListData[mSize] = kT;
                uint pos = mSize;
                while (pos > 1)
                {
                    uint parent = _getParent(pos);
                    if (mListData[parent].Key > mListData[pos].Key)
                    {
                        _swap(ref mListData[pos], ref mListData[parent]);
                        pos = parent;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        //---------------------------------------------------------------------
        public void FastPush(T kT)
        {
            ++mSize;
            mListData[mSize] = kT;
            uint pos = mSize;
            while (pos > 1)
            {
                uint parent = _getParent(pos);
                if (mListData[parent].Key > mListData[pos].Key)
                {
                    _swap(ref mListData[pos], ref mListData[parent]);
                    pos = parent;
                }
                else
                {
                    break;
                }
            }
        }

        //---------------------------------------------------------------------
        public T Pop()
        {
            if (mSize > 0)
            {
                _swap(ref mListData[1], ref mListData[mSize]);
                --mSize;
                _siftdown(1);
                return mListData[mSize + 1];
            }
            else
            {
                return null;
            }
        }

        //---------------------------------------------------------------------
        public void Clear()
        {
            mSize = 0;
        }

        //---------------------------------------------------------------------
        uint _getLeft(uint idx) { return idx << 1; }// k*2

        //---------------------------------------------------------------------
        uint _getRight(uint idx) { return (idx << 1) + 1; }// k*2+1

        //---------------------------------------------------------------------
        uint _getParent(uint idx) { return (idx >> 1); }// k/2

        //---------------------------------------------------------------------
        static void _swap(ref T one, ref T other) { T tmp = one; one = other; other = tmp; }

        //---------------------------------------------------------------------
        void _siftdown(uint pos)
        {
            uint posMod = pos;
            uint left = _getLeft(posMod);
            while (left <= mSize)
            {
                uint t = left;// int t= pos<<1; 
                uint right = _getRight(posMod);
                if (right <= mSize && mListData[right].Key < mListData[t].Key)
                {
                    t = right;
                }
                if (mListData[t].Key < mListData[posMod].Key)
                {
                    _swap(ref mListData[t], ref mListData[posMod]);
                    posMod = t;
                    left = _getLeft(posMod);
                }
                else
                {
                    break;
                }
            }
        }
    }
}
