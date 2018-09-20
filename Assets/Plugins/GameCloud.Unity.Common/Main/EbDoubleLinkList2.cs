// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;

    public class EbDoubleLinkNode2<T>
    {
        //-------------------------------------------------------------------------
        public T Data;
        internal EbDoubleLinkNode2<T> _pre;
        internal EbDoubleLinkNode2<T> _next;

        //-------------------------------------------------------------------------
        public EbDoubleLinkNode2()
        {
        }

        //-------------------------------------------------------------------------
        public EbDoubleLinkNode2(T data)
        {
            Data = data;
        }

        //-------------------------------------------------------------------------
        public bool IsAttach()
        {
            return (_pre != null);
        }

        //-------------------------------------------------------------------------
        public void Detach()
        {
            if (_pre != null)
            {
                _pre._next = _next;
                _next._pre = _pre;
                _pre = null;
                _next = null;
            }
        }
    }

    public class EbDoubleLinkList2<T>
    {
        //-------------------------------------------------------------------------
        internal EbDoubleLinkNode2<T> _root = new EbDoubleLinkNode2<T>();

        //-------------------------------------------------------------------------
        public EbDoubleLinkList2()
        {
            _Init();
        }

        //-------------------------------------------------------------------------
        public static void Next(ref EbDoubleLinkNode2<T> node)
        {
            node = node._next;
        }

        //-------------------------------------------------------------------------
        public static void Pre(ref EbDoubleLinkNode2<T> node)
        {
            node = node._pre;
        }

        //-------------------------------------------------------------------------
        public static void PushAfter(EbDoubleLinkNode2<T> before, EbDoubleLinkNode2<T> node)
        {
            node.Detach();
            _PushAfter(before, node);
        }

        //-------------------------------------------------------------------------
        public static void PushBefore(EbDoubleLinkNode2<T> after, EbDoubleLinkNode2<T> node)
        {
            node.Detach();
            _PushBefore(after, node);
        }

        //-------------------------------------------------------------------------
        public static void PushAfter(EbDoubleLinkNode2<T> before, EbDoubleLinkList2<T> list)
        {
            if (before.IsAttach() == false)
            {
                return;
            }
            if (list.IsEmpty())
            {
                return;
            }
            _PushAfter(before, list);
        }

        //-------------------------------------------------------------------------
        public static void PushBefore(EbDoubleLinkNode2<T> after, EbDoubleLinkList2<T> list)
        {
            if (after.IsAttach() == false)
            {
                return;
            }
            if (list.IsEmpty())
            {
                return;
            }
            _PushBefore(after, list);
        }

        //-------------------------------------------------------------------------
        public bool IsEmpty()
        {
            return _root._next == _root;
        }

        //-------------------------------------------------------------------------
        public bool IsNotEmpty()
        {
            return _root._next != _root;
        }

        //-------------------------------------------------------------------------
        public bool IsEnd(EbDoubleLinkNode2<T> node)
        {
            return node == _root;
        }

        //-------------------------------------------------------------------------
        public EbDoubleLinkNode2<T> GetHead()
        {
            return _root._next;
        }

        //-------------------------------------------------------------------------
        public EbDoubleLinkNode2<T> GetTail()
        {
            return _root._pre;
        }

        //-------------------------------------------------------------------------
        public void PushBack(EbDoubleLinkNode2<T> node)
        {
            node.Detach();
            _PushBefore(_root, node);
        }

        //-------------------------------------------------------------------------
        public void PushFront(EbDoubleLinkNode2<T> node)
        {
            node.Detach();
            _PushAfter(_root, node);
        }

        //-------------------------------------------------------------------------
        public void PushBack(EbDoubleLinkList2<T> list)
        {
            _PushBefore(_root, list);
        }

        //-------------------------------------------------------------------------
        public void PushFront(EbDoubleLinkList2<T> list)
        {
            _PushAfter(_root, list);
        }

        //-------------------------------------------------------------------------
        public void SetValue(T value)
        {
            EbDoubleLinkNode2<T> next = _root._next;
            while (next != _root)
            {
                next.Data = value;
                next = next._next;
            }
        }

        //-------------------------------------------------------------------------
        public void Clear()
        {
            EbDoubleLinkNode2<T> next = _root._next;
            while (next != _root)
            {
                EbDoubleLinkNode2<T> nextCopy = next._next;
                next._pre = null;
                next._next = null;
                next = nextCopy;
            }
            _Init();
        }

        //-------------------------------------------------------------------------
        public void ClearAndClearContent()
        {
            EbDoubleLinkNode2<T> next = _root._next;
            while (next != _root)
            {
                EbDoubleLinkNode2<T> nextCopy = next._next;
                next._pre = null;
                next._next = null;
                next.Data = default(T);
                next = nextCopy;
            }
            _Init();
        }

        //-------------------------------------------------------------------------
        static void _PushAfter(EbDoubleLinkNode2<T> before, EbDoubleLinkNode2<T> node)
        {
            EbDoubleLinkNode2<T> next = before._next;
            _Link(before, node);
            _Link(node, next);
        }

        //-------------------------------------------------------------------------
        static void _PushBefore(EbDoubleLinkNode2<T> after, EbDoubleLinkNode2<T> node)
        {
            EbDoubleLinkNode2<T> pre = after._pre;
            _Link(pre, node);
            _Link(node, after);
        }

        //-------------------------------------------------------------------------
        static void _PushAfter(EbDoubleLinkNode2<T> before, EbDoubleLinkList2<T> list)
        {
            if (list.IsEmpty())
            {
                return;
            }
            EbDoubleLinkNode2<T> first = list._root._next;
            EbDoubleLinkNode2<T> back = list._root._pre;
            EbDoubleLinkNode2<T> next = before._next;
            _Link(before, first);
            _Link(back, next);
            list._Init();
        }

        //-------------------------------------------------------------------------
        static void _PushBefore(EbDoubleLinkNode2<T> after, EbDoubleLinkList2<T> list)
        {
            if (list.IsEmpty())
            {
                return;
            }
            EbDoubleLinkNode2<T> first = list._root._next;
            EbDoubleLinkNode2<T> back = list._root._pre;
            EbDoubleLinkNode2<T> pre = after._pre;
            _Link(pre, first);
            _Link(back, after);
            list._Init();
        }

        //-------------------------------------------------------------------------
        static void _Link(EbDoubleLinkNode2<T> pre, EbDoubleLinkNode2<T> next)
        {
            pre._next = next;
            next._pre = pre;
        }

        //-------------------------------------------------------------------------
        private void _Init()
        {
            _Link(_root, _root);
        }
    }

    public class EbSampleDoubleLinkList2
    {
        //-------------------------------------------------------------------------
        public static void Test()
        {
            EbDoubleLinkList2<int> list = new EbDoubleLinkList2<int>();
            EbDoubleLinkNode2<int> node1 = new EbDoubleLinkNode2<int>();
            node1.Data = 1;
            EbDoubleLinkNode2<int> node2 = new EbDoubleLinkNode2<int>();
            node2.Data = 2;
            list.PushBack(node1);
            list.PushBack(node2);

            {// 正向迭代
                EbDoubleLinkNode2<int> iter = list.GetHead();
                while (!list.IsEnd(iter))
                {
                    //int value = iter.Data;
                    EbDoubleLinkList2<int>.Next(ref iter);
                }
            }

            {// 反向迭代
                EbDoubleLinkNode2<int> iter = list.GetTail();
                while (!list.IsEnd(iter))
                {
                    //int value = iter.Data;
                    EbDoubleLinkList2<int>.Pre(ref iter);
                }
            }
        }
    }
}
