// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IEbAction
    {
        //---------------------------------------------------------------------
        string action(IEbEvent ev);
    }

    public class EbAction : IEbAction
    {
        //---------------------------------------------------------------------
        public delegate string delegateSignature(IEbEvent ev);
        delegateSignature mHandler;

        //---------------------------------------------------------------------
        public EbAction(delegateSignature handler)
        {
            mHandler = handler;
        }

        //---------------------------------------------------------------------
        public string action(IEbEvent ev)
        {
            return mHandler(ev);
        }
    }

    public abstract class EbState
    {
        //---------------------------------------------------------------------
        protected Dictionary<string, IEbAction> mMapAction = new Dictionary<string, IEbAction>();
        protected Dictionary<int, List<EbState>> mMapChildState = new Dictionary<int, List<EbState>>();
        protected string mStrParentStateName = "";
        protected string mStrStateName = "";
        protected bool mbInitState;
        protected int miStateGroup;
        protected EbState mpParentState;

        //---------------------------------------------------------------------
        public virtual void enter()
        {
        }

        //---------------------------------------------------------------------
        public virtual void exit()
        {
        }

        //---------------------------------------------------------------------
        public string _onEvent(IEbEvent ev)
        {
            if (mMapAction.ContainsKey(ev.name))
            {
                return mMapAction[ev.name].action(ev);
            }
            else
            {
                return "";
            }
        }

        //---------------------------------------------------------------------
        public void _defState(string state_name, string parent_state_name,
            int state_group, bool init_state)
        {
            mStrStateName = state_name;
            miStateGroup = state_group;
            mStrParentStateName = parent_state_name;
            mpParentState = null;
            mbInitState = init_state;
        }

        //---------------------------------------------------------------------
        public void _defState(string state_name, int state_group)
        {
            mStrStateName = state_name;
            miStateGroup = state_group;
            mStrParentStateName = "";
            mpParentState = null;
            mbInitState = true;
        }

        //---------------------------------------------------------------------
        public bool _isBindEvent(string event_name)
        {
            return mMapAction.ContainsKey(event_name);
        }

        //---------------------------------------------------------------------
        public void _bindAction(string event_name, IEbAction act)
        {
            mMapAction[event_name] = act;
        }

        //---------------------------------------------------------------------
        public void _addChildState(EbState child)
        {
            int group = child._getStateGroup();
            if (mMapChildState.ContainsKey(group))
            {
                mMapChildState[group].Add(child);
            }
            else
            {
                List<EbState> v = new List<EbState>();
                v.Add(child);
                mMapChildState[group] = v;
            }
        }

        //---------------------------------------------------------------------
        public void _setParentState(EbState parent)
        {
            mpParentState = parent;
        }

        //---------------------------------------------------------------------
        public int _getStateGroup()
        {
            return miStateGroup;
        }

        //---------------------------------------------------------------------
        public string _getStateName()
        {
            return mStrStateName;
        }

        //---------------------------------------------------------------------
        public string _getParentStateName()
        {
            return mStrParentStateName;
        }

        //---------------------------------------------------------------------
        public EbState _getParentState()
        {
            return mpParentState;
        }

        //---------------------------------------------------------------------
        public bool _isInitState()
        {
            return mbInitState;
        }

        //---------------------------------------------------------------------
        public Dictionary<int, List<EbState>> _getMapChildState()
        {
            return mMapChildState;
        }
    }
}