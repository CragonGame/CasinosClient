// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;

    public class EbRouteNode
    {
        //---------------------------------------------------------------------
        public static readonly uint NULL_EVENT = 0XFFFFFFFF;
        public EbVector3 _pos;
        public uint _eventId;

        //---------------------------------------------------------------------
        public static bool HasEvent(uint event_id)
        {
            return (event_id != NULL_EVENT);
        }
    }

    public class EbRouteAlloc<T> where T : new()
    {
        //---------------------------------------------------------------------
        EbDoubleLinkList2<T> mListNode = new EbDoubleLinkList2<T>();

        //---------------------------------------------------------------------
        public EbDoubleLinkNode2<T> New()
        {
            if (mListNode.IsEmpty())
            {
                return new EbDoubleLinkNode2<T>(new T());
            }
            else
            {
                EbDoubleLinkNode2<T> node = mListNode.GetHead();
                node.Detach();
                return node;
            }
        }

        //---------------------------------------------------------------------
        public void Delete(EbDoubleLinkNode2<T> node)
        {
            if (node != null)
            {
                mListNode.PushBack(node);
            }
        }

        //---------------------------------------------------------------------
        public void Delete(EbDoubleLinkList2<T> nodes)
        {
            mListNode.PushBack(nodes);
        }

        //---------------------------------------------------------------------
        public void Clear()
        {
            mListNode.Clear();
        }
    }

    public class EbRoute
    {
        //---------------------------------------------------------------------
        EbVector3 mPrePos;
        EbVector3 mCurrentPos;
        EbRouteNode mNextNode;
        EbVector3 mDir;
        EbDoubleLinkList2<EbRouteNode> mListNode = new EbDoubleLinkList2<EbRouteNode>();
        static EbRouteAlloc<EbRouteNode> mNodeAlloc = new EbRouteAlloc<EbRouteNode>();
        //ViEventAsynList _endCallbackList = new ViEventAsynList();

        //---------------------------------------------------------------------
        public EbVector3 Position { get { return mCurrentPos; } }
        public EbVector3 Direction { get { return mDir; } }
        //public ViEventAsynList EndCallbackList { get { return _endCallbackList; } }

        //---------------------------------------------------------------------
        public static float GetLength(List<EbVector3> list_pos)
        {
            if (list_pos.Count <= 1)
            {
                return 0.0f;
            }
            float len = 0.0f;
            EbVector3 pos_pre = list_pos[0];
            for (int i = 1; i < list_pos.Count; ++i)
            {
                len += Distance(list_pos[i], pos_pre);
                pos_pre = list_pos[i];
            }
            return len;
        }

        //---------------------------------------------------------------------
        public static float GetLength(EbVector3 pos_start, List<EbVector3> list_pos)
        {
            float len = 0.0f;
            EbVector3 pos_pre = pos_start;
            for (int idx = 0; idx < list_pos.Count; ++idx)
            {
                len += Distance(list_pos[idx], pos_pre);
                pos_pre = list_pos[idx];
            }
            return len;
        }

        //---------------------------------------------------------------------
        public static float Distance(EbVector3 pos1, EbVector3 pos2)
        {
            float delta_x = pos1.x - pos2.x;
            float delta_z = pos1.z - pos2.z;
            return (float)Math.Sqrt((delta_x * delta_x) + (delta_z * delta_z));
        }

        //---------------------------------------------------------------------
        public void Append(EbVector3 pos)
        {
            EbDoubleLinkNode2<EbRouteNode> node = mNodeAlloc.New();
            node.Data._eventId = EbRouteNode.NULL_EVENT;
            node.Data._pos = pos;
            mListNode.PushBack(node);
        }

        //---------------------------------------------------------------------
        public void Append(EbVector3 pos, uint event_id)
        {
            EbDoubleLinkNode2<EbRouteNode> node = mNodeAlloc.New();
            node.Data._eventId = event_id;
            node.Data._pos = pos;
            mListNode.PushBack(node);
        }

        //---------------------------------------------------------------------
        public bool OnTick(float delta_tm, float speed)
        {
            return _updateDistance(delta_tm * speed);
        }

        //---------------------------------------------------------------------
        public void Start(EbVector3 pos)
        {
            if (mListNode.IsEmpty())
            {
                _clearState();
                return;
            }
            mPrePos = pos;
            mCurrentPos = mPrePos;
            mNextNode = _lerpToNextNode();
        }

        //---------------------------------------------------------------------
        public void Reset()
        {
            _clearState();
            _clearNodeList();
            //_endCallbackList.Clear();
        }

        //---------------------------------------------------------------------
        public bool GetNextPosition(ref EbVector3 pos)
        {
            if (mNextNode != null)
            {
                pos = mNextNode._pos;
                return true;
            }
            else
            {
                return false;
            }
        }

        //---------------------------------------------------------------------
        public bool GetDestPosition(ref EbVector3 pos)
        {
            if (mListNode.IsEmpty())
            {
                return false;
            }
            else
            {
                pos = mNextNode._pos;
                return true;
            }
        }

        //---------------------------------------------------------------------
        public float Length()
        {
            float len = 0.0f;
            EbVector3 pos_pre = mCurrentPos;
            EbDoubleLinkNode2<EbRouteNode> iter = mListNode.GetHead();
            while (!mListNode.IsEnd(iter))
            {
                EbRouteNode value = iter.Data;
                EbDoubleLinkList2<EbRouteNode>.Next(ref iter);
                len += Distance(value._pos, pos_pre);
                pos_pre = value._pos;
            }
            return len;
        }

        //---------------------------------------------------------------------
        public bool IsEnd()
        {
            return (mNextNode == null);
        }

        //---------------------------------------------------------------------
        bool _updateDistance(float distance)
        {
            if (mNextNode == null)
            {
                return false;
            }
            EbVector3 nextPos = mNextNode._pos;
            float distRight = Distance(mCurrentPos, nextPos);
            if (distRight > distance)
            {
                mCurrentPos += mDir * distance;
            }
            else
            {
                float newDistance = distance - distRight;
                mCurrentPos = nextPos;
                mPrePos = nextPos;

                //if (EbRouteNode.HasEvent(_nextNode._eventId))
                //{
                //    _endCallbackList.Invoke(_nextNode._eventId);
                //}

                mNodeAlloc.Delete(mListNode.GetHead());
                mNextNode = _lerpToNextNode();
                if (mNextNode != null)
                {
                    _updateDistance(newDistance);
                }
                else
                {
                    _clearState();
                }
            }
            return true;
        }

        //---------------------------------------------------------------------
        EbRouteNode _lerpToNextNode()
        {
            if (mListNode.IsNotEmpty())
            {
                EbRouteNode pNode = mListNode.GetHead().Data;
                mDir = pNode._pos - mPrePos;
                mDir.y = 0.0f;
                mDir.normalize();
                return pNode;
            }
            else
            {
                return null;
            }
        }

        //---------------------------------------------------------------------
        void _clearNodeList()
        {
            mNodeAlloc.Delete(mListNode);
        }

        //---------------------------------------------------------------------
        void _clearState()
        {
            mNextNode = null;
            mPrePos = mCurrentPos;
        }
    }

    public class EbSampleRoute
    {
        //---------------------------------------------------------------------
        class Listener
        {
            public void OnEnd(uint eventId)
            {

            }
            //public ViAsynCallback _endCallbackNode = new ViAsynCallback();
        }

        //---------------------------------------------------------------------
        public static void Test()
        {
            Listener listener = new Listener();
            EbRoute route = new EbRoute();
            route.Append(new EbVector3(10, 0, 0));
            route.Append(new EbVector3(20, 0, 0));
            route.Append(new EbVector3(10, 0, 0), 1);
            route.Start(new EbVector3(0, 0, 0));
            float len = route.Length();
            //route.EndCallbackList.Attach(listener._endCallbackNode, listener.OnEnd);
            float time = 0.0f;
            while (time < 10.0f)
            {
                route.OnTick(0.3f, 10.0f);
                time += 0.3f;
                //ViAsynCallback.Update();
            }
        }
    }
}