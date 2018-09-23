// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;

    // 定时事件节点
    public class EbTimeEvent
    {
        //---------------------------------------------------------------------
        public ulong mExpires;// 事件时间
        public delegate void del(object data);
        public del onTime;
        public object mData;// 用户数据
    }

    public class EbTimeWheel
    {
        //---------------------------------------------------------------------
        public List<EbDoubleLinkList<EbTimeEvent>> mListTimerSpoke = null;
        public int mSpokeCount;
        public int mLastSpokeIndex;

        //---------------------------------------------------------------------
        public EbTimeWheel(int spoke_count)
        {
            mLastSpokeIndex = -1;
            mSpokeCount = spoke_count;
            mListTimerSpoke = new List<EbDoubleLinkList<EbTimeEvent>>(spoke_count);

            for (int i = 0; i < spoke_count; ++i)
            {
                EbDoubleLinkList<EbTimeEvent> list_event = new EbDoubleLinkList<EbTimeEvent>();
                mListTimerSpoke.Add(list_event);
            }
        }

        //---------------------------------------------------------------------
        public void Destroy()
        {
            foreach (EbDoubleLinkList<EbTimeEvent> time_event_node in mListTimerSpoke)
            {
                time_event_node.Destroy();
            }
        }

        //---------------------------------------------------------------------
        // 取得某个时间槽下的所有事件
        public EbDoubleLinkList<EbTimeEvent> GetSpoke(int index)
        {
            return mListTimerSpoke[index];
        }

        //---------------------------------------------------------------------
        // 取得时间槽的事件队列头
        public EbDoubleLinkNode<EbTimeEvent> GetSpokeHead(int index)
        {
            return mListTimerSpoke[index].Head();
        }
    }

    public class EbTimer
    {
        //---------------------------------------------------------------------
        public TimerShaft TimerShaft { get; private set; }
        public EbTimeEvent TimeEvent { get; private set; }
        public EbDoubleLinkNode<EbTimeEvent> TimeNode { get; private set; }
        Action FuncCb { get; set; }
        ulong TmSpan { get; set; }
        ulong LastTimeJeffies { get; set; } = 0;
        bool Closed { get; set; } = false;

        //---------------------------------------------------------------------
        public EbTimer(TimerShaft timer_shaft, ulong tm, Action cb)
        {
            TimerShaft = timer_shaft;
            TmSpan = tm;
            FuncCb = cb;
            LastTimeJeffies = TimerShaft.GetTimeJeffies() + TmSpan;
            Closed = false;

            var time_ev = new EbTimeEvent()
            {
                mExpires = TimerShaft.GetTimeJeffies() + TmSpan,
                onTime = _onTimer,
                mData = null
            };
            TimerShaft.AddTimer(time_ev);
        }

        //---------------------------------------------------------------------
        public void Close()
        {
            Closed = true;
        }

        //---------------------------------------------------------------------
        void _onTimer(object data)
        {
            if (Closed) return;

            LastTimeJeffies += TmSpan;
            var delta_tm = LastTimeJeffies - TimerShaft.GetTimeJeffies();

            var time_ev = new EbTimeEvent()
            {
                mExpires = TimerShaft.GetTimeJeffies() + delta_tm,
                onTime = _onTimer,
                mData = null
            };
            TimerShaft.AddTimer(time_ev);

            FuncCb();
        }
    }

    public class TimerShaft
    {
        //---------------------------------------------------------------------
        Dictionary<EbTimeEvent, EbDoubleLinkNode<EbTimeEvent>> MapEventNode { get; set; }
        EbDoubleLinkList<EbTimeEvent> ExcuteList { get; set; }

        public EbTimeWheel mWheel1 = null;
        public EbTimeWheel mWheel2 = null;
        public EbTimeWheel mWheel3 = null;
        public EbTimeWheel mWheel4 = null;
        public EbTimeWheel mWheel5 = null;
        public ulong mTimeJeffies;
        public ulong mLastJeffies;

        readonly ulong MIN_WHEEL = 6;
        readonly ulong MAX_WHEEL = 8;
        readonly ulong MIN_WHEEL_SIZE = (1 << 6);// 64
        readonly ulong MAX_WHEEL_SIZE = (1 << 8);// 256
        readonly ulong MIN_WHEEL_MASK = (64 - 1);
        readonly ulong MAX_WHEEL_MASK = (256 - 1);

        //---------------------------------------------------------------------
        public TimerShaft()
        {
            MapEventNode = new Dictionary<EbTimeEvent, EbDoubleLinkNode<EbTimeEvent>>();
            ExcuteList = new EbDoubleLinkList<EbTimeEvent>();

            mWheel1 = new EbTimeWheel((int)MAX_WHEEL_SIZE);
            mWheel2 = new EbTimeWheel((int)MIN_WHEEL_SIZE);
            mWheel3 = new EbTimeWheel((int)MIN_WHEEL_SIZE);
            mWheel4 = new EbTimeWheel((int)MIN_WHEEL_SIZE);
            mWheel5 = new EbTimeWheel((int)MIN_WHEEL_SIZE);
        }

        //---------------------------------------------------------------------
        public EbTimer RegisterTimer(ulong tm, Action cb)
        {
            EbTimer timer = new EbTimer(this, tm, cb);
            return timer;
        }

        //---------------------------------------------------------------------
        // 添加一个时间事件
        public void AddTimer(EbTimeEvent time_event)
        {
            EbDoubleLinkNode<EbTimeEvent> timer_node = new EbDoubleLinkNode<EbTimeEvent>();
            timer_node.mObject = time_event;

            MapEventNode[time_event] = timer_node;

            _addTimer(timer_node);
        }

        //---------------------------------------------------------------------
        // 删除一个时间事件
        public int DelTimer(EbTimeEvent time_event)
        {
            if (MapEventNode.ContainsKey(time_event))
            {
                EbDoubleLinkNode<EbTimeEvent> timer_node = MapEventNode[time_event];
                MapEventNode.Remove(time_event);

                _delTimer(timer_node);
            }

            return 0;
        }

        //---------------------------------------------------------------------
        // 修改一个时间事件的时间
        public int ModTimer(EbTimeEvent time_event, ulong expires)
        {
            DelTimer(time_event);
            time_event.mExpires = expires;
            AddTimer(time_event);

            return 0;
        }

        //---------------------------------------------------------------------
        // 根据时间，执行时间事件
        public void ProcessTimer(ulong jeffies)
        {
            ExcuteList.Destroy();

            while (jeffies >= mTimeJeffies)
            {
                ulong index = mTimeJeffies & MAX_WHEEL_MASK;

                if (index == 0
                    && _cascadeTimer(mWheel2, 0) == 0
                    && _cascadeTimer(mWheel3, 1) == 0
                    && _cascadeTimer(mWheel4, 2) == 0)
                {
                    _cascadeTimer(mWheel5, 3);
                }

                mTimeJeffies++;

                ExcuteList.AddTailList(mWheel1.GetSpoke((int)index));
            }

            EbDoubleLinkNode<EbTimeEvent> head, curr, next;
            head = ExcuteList.Head();
            curr = head.next;

            while (curr != head)
            {
                next = curr.next;

                //_delTimer(curr);

                DelTimer(curr.mObject);

                var node = curr;
                curr = next;

                // 调用委托
                node.mObject.onTime?.Invoke(node.mObject.mData);
            }
        }

        //---------------------------------------------------------------------
        // 取得已经执行的时间
        public ulong GetTimeJeffies()
        {
            return mTimeJeffies;
        }

        //---------------------------------------------------------------------
        // 调整时间轮上的事件
        int _cascadeTimer(EbTimeWheel timer_wheel, int wheel_index)
        {
            int index = (int)((mTimeJeffies >> (Convert.ToInt32(MAX_WHEEL) + wheel_index * Convert.ToInt32(MIN_WHEEL))) & MIN_WHEEL_MASK);

            EbDoubleLinkNode<EbTimeEvent> head, curr, next;
            head = timer_wheel.GetSpokeHead(index);
            curr = head.next;

            while (curr != head)
            {
                next = curr.next;

                _delTimer(curr);
                _addTimer(curr);

                curr = next;
            }

            return index;
        }

        //---------------------------------------------------------------------
        // 添加事件
        void _addTimer(EbDoubleLinkNode<EbTimeEvent> timer)
        {
            ulong expires = timer.mObject.mExpires;
            ulong idx = expires - mTimeJeffies;

            EbDoubleLinkList<EbTimeEvent> list_timer_spoke = null;

            if (idx < MAX_WHEEL_SIZE)
            {
                // 第1个轮子
                ulong i = expires & MAX_WHEEL_MASK;
                list_timer_spoke = mWheel1.mListTimerSpoke[(int)i];
            }
            else if (idx < (ulong)1 << Convert.ToInt32(MAX_WHEEL + MIN_WHEEL))
            {
                // 第2个轮子                
                ulong i = expires >> Convert.ToInt32(MAX_WHEEL) & MIN_WHEEL_MASK;
                list_timer_spoke = mWheel2.mListTimerSpoke[(int)i];
            }
            else if (idx < (ulong)1 << Convert.ToInt32(MAX_WHEEL + 2 * MIN_WHEEL))
            {
                // 第3个轮子
                ulong i = expires >> Convert.ToInt32(MAX_WHEEL + MIN_WHEEL) & MIN_WHEEL_MASK;
                list_timer_spoke = mWheel3.mListTimerSpoke[(int)i];
            }
            else if (idx < (ulong)1 << Convert.ToInt32(MAX_WHEEL + 3 * MIN_WHEEL))
            {
                // 第4个轮子
                ulong i = expires >> Convert.ToInt32(MAX_WHEEL + 2 * MIN_WHEEL) & MIN_WHEEL_MASK;
                list_timer_spoke = mWheel4.mListTimerSpoke[(int)i];
            }
            else if ((long)idx < 0)
            {
                // Can happen if you add a timer with expires == jiffies,
                // or you set a timer to go off in the past
                list_timer_spoke = mWheel1.mListTimerSpoke[(int)(mTimeJeffies & MAX_WHEEL_MASK)];
            }
            else
            {
                // If the timeout is larger than 0xffffffff on 64-bit
                // architectures then we use the maximum timeout:
                if (idx > 0xffffffffUL)
                {
                    idx = 0xffffffffUL;
                    expires = idx + mTimeJeffies;
                }
                ulong i = expires >> Convert.ToInt32(MAX_WHEEL + 3 * MIN_WHEEL) & MIN_WHEEL_MASK;
                list_timer_spoke = mWheel5.mListTimerSpoke[(int)i];
            }

            // Timers are FIFO:
            list_timer_spoke.AddTailNode(timer);
        }

        //---------------------------------------------------------------------
        // 删除事件
        int _delTimer(EbDoubleLinkNode<EbTimeEvent> timer)
        {
            EbDoubleLinkList<EbTimeEvent>.DelNode(timer);
            return 0;
        }
    }
}