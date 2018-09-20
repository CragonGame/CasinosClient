// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class EbFsm : EbState
    {
        //---------------------------------------------------------------------
        Dictionary<string, EbState> mMapState = new Dictionary<string, EbState>();
        Queue<IEbEvent> mQueEvent = new Queue<IEbEvent>();
        List<EbState> mQueCurrentState = new List<EbState>();
        List<EbState> mQueTmpState = new List<EbState>();
        bool mbRattleOn = false;
        bool mbDestroy = false;

        //---------------------------------------------------------------------
        public EbFsm()
        {
            _defState("EbFsm", 0);
            mMapState[_getStateName()] = this;
        }

        //---------------------------------------------------------------------
        public void setupFsm()
        {
            // 建立所有状态父子关系
            foreach (var it in mMapState)
            {
                EbState state = it.Value;
                var parent_state_name = state._getParentStateName();
                if (mMapState.ContainsKey(parent_state_name))
                {
                    var parent_state = mMapState[parent_state_name];
                    state._setParentState(parent_state);
                    parent_state._addChildState(state);
                }
            }

            // 初始化当前状态列表
            _initFsm(this);
        }

        //---------------------------------------------------------------------
        public void destroyFsm()
        {
            _releaseFsm();
        }

        //---------------------------------------------------------------------
        public void addState(EbState state)
        {
            mMapState[state._getStateName()] = state;
        }

        //---------------------------------------------------------------------
        public List<EbState> getCurrentStateList()
        {
            return mQueCurrentState;
        }

        //---------------------------------------------------------------------
        public void processEvent(string ev_name)
        {
            IEbEvent ev = new EbEvent0(ev_name);
            mQueEvent.Enqueue(ev);
            _rattleOn();
        }

        //---------------------------------------------------------------------
        public void processEvent<P1>(string ev_name, P1 p1)
        {
            IEbEvent ev = new EbEvent1<P1>(ev_name, p1);
            mQueEvent.Enqueue(ev);
            _rattleOn();
        }

        //---------------------------------------------------------------------
        public void processEvent<P1, P2>(string ev_name, P1 p1, P2 p2)
        {
            IEbEvent ev = new EbEvent2<P1, P2>(ev_name, p1, p2);
            mQueEvent.Enqueue(ev);
            _rattleOn();
        }

        //---------------------------------------------------------------------
        public void processEvent<P1, P2, P3>(string ev_name, P1 p1, P2 p2, P3 p3)
        {
            IEbEvent ev = new EbEvent3<P1, P2, P3>(ev_name, p1, p2, p3);
            mQueEvent.Enqueue(ev);
            _rattleOn();
        }

        //---------------------------------------------------------------------
        public void processEvent<P1, P2, P3, P4>(string ev_name, P1 p1, P2 p2, P3 p3, P4 p4)
        {
            IEbEvent ev = new EbEvent4<P1, P2, P3, P4>(ev_name, p1, p2, p3, p4);
            mQueEvent.Enqueue(ev);
            _rattleOn();
        }

        //---------------------------------------------------------------------
        public void processEvent<P1, P2, P3, P4, P5>(string ev_name, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
        {
            IEbEvent ev = new EbEvent5<P1, P2, P3, P4, P5>(ev_name, p1, p2, p3, p4, p5);
            mQueEvent.Enqueue(ev);
            _rattleOn();
        }

        //---------------------------------------------------------------------
        public void postEvent(string ev_name)
        {
            IEbEvent ev = new EbEvent0(ev_name);
            mQueEvent.Enqueue(ev);
        }

        //---------------------------------------------------------------------
        public void postEvent<P1>(string ev_name, P1 p1)
        {
            IEbEvent ev = new EbEvent1<P1>(ev_name, p1);
            mQueEvent.Enqueue(ev);
        }

        //---------------------------------------------------------------------
        public void postEvent<P1, P2>(string ev_name, P1 p1, P2 p2)
        {
            IEbEvent ev = new EbEvent2<P1, P2>(ev_name, p1, p2);
            mQueEvent.Enqueue(ev);
        }

        //---------------------------------------------------------------------
        public void postEvent<P1, P2, P3>(string ev_name, P1 p1, P2 p2, P3 p3)
        {
            IEbEvent ev = new EbEvent3<P1, P2, P3>(ev_name, p1, p2, p3);
            mQueEvent.Enqueue(ev);
        }

        //---------------------------------------------------------------------
        public void postEvent<P1, P2, P3, P4>(string ev_name, P1 p1, P2 p2, P3 p3, P4 p4)
        {
            IEbEvent ev = new EbEvent4<P1, P2, P3, P4>(ev_name, p1, p2, p3, p4);
            mQueEvent.Enqueue(ev);
        }

        //---------------------------------------------------------------------
        public void postEvent<P1, P2, P3, P4, P5>(string ev_name, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
        {
            IEbEvent ev = new EbEvent5<P1, P2, P3, P4, P5>(ev_name, p1, p2, p3, p4, p5);
            mQueEvent.Enqueue(ev);
        }

        //---------------------------------------------------------------------
        bool _initFsm(EbState state)
        {
            bool is_initstate = state._isInitState();
            if (!is_initstate) return false;

            state.enter();

            mQueCurrentState.Add(state);

            var map_childstate = state._getMapChildState();
            if (map_childstate.ContainsKey(0))
            {
                var v = map_childstate[0];
                foreach (var itc in v)
                {
                    if (_initFsm(itc)) return true;
                }
            }
            return true;
        }

        //---------------------------------------------------------------------
        void _releaseFsm()
        {
            mbDestroy = true;

            while (mQueCurrentState.Count > 0)
            {
                EbState s = mQueCurrentState[mQueCurrentState.Count - 1];
                s.exit();
                mQueCurrentState.RemoveAt(mQueCurrentState.Count - 1);
            }
        }

        //---------------------------------------------------------------------
        void _rattleOn()
        {
            if (mbRattleOn || mbDestroy) return;
            mbRattleOn = true;

            while (mQueEvent.Count > 0)
            {
                IEbEvent ev = mQueEvent.Dequeue();

                for (int i = mQueCurrentState.Count - 1; i >= 0; i--)
                {
                    var it = mQueCurrentState[i];

                    EbState state = it;

                    if (!state._isBindEvent(ev.name)) continue;

                    string next_state_name = state._onEvent(ev);

                    if (string.IsNullOrEmpty(next_state_name)) continue;
                    if (mbDestroy) break;

                    EbState next_state = null;
                    if (mMapState.ContainsKey(next_state_name))
                    {
                        next_state = mMapState[next_state_name];
                    }

                    if (next_state == null) break;

                    EbState p = next_state._getParentState();

                    _exitChildState(p);

                    _enterState(next_state);

                    break;
                }
            }
            mbRattleOn = false;
        }

        //---------------------------------------------------------------------
        bool _isDirectLine(EbState parent, EbState child)
        {
            if (parent == child) return true;

            Dictionary<int, List<EbState>> map_childstate = parent._getMapChildState();
            if (map_childstate.ContainsKey(0))
            {
                var v = map_childstate[0];
                foreach (var itc in v)
                {
                    if (_isDirectLine(itc, child)) return true;
                }

            }

            return false;
        }

        //---------------------------------------------------------------------
        void _enterState(EbState next_state)
        {
            next_state.enter();
            mQueCurrentState.Add(next_state);

            var map_childstate = next_state._getMapChildState();
            foreach (var i in map_childstate)
            {
                if (i.Value == null) continue;

                foreach (var s in i.Value)
                {
                    if (s._isInitState())
                    {
                        _enterState(s);
                        break;
                    }
                }
            }
        }

        //---------------------------------------------------------------------
        void _exitChildState(EbState cur_state)
        {
            if (mQueCurrentState.Count == 0) return;

            EbState s = mQueCurrentState[mQueCurrentState.Count - 1];
            if (s != cur_state)
            {
                s.exit();

                mQueCurrentState.Remove(s);

                _exitChildState(cur_state);
            }
        }
    }
}
