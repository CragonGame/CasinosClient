// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    public class EbDoubleLinkNode<TObject>
    {
        //-------------------------------------------------------------------------
        public EbDoubleLinkNode<TObject> next = null;
        public EbDoubleLinkNode<TObject> prev = null;
        public TObject mObject;
    };

    public class EbDoubleLinkList<TObject>
    {
        //-------------------------------------------------------------------------
        public EbDoubleLinkNode<TObject> mpHead = null;

        //-------------------------------------------------------------------------
        public EbDoubleLinkList()
        {
            mpHead = new EbDoubleLinkNode<TObject>();
            Init();
        }

        //-------------------------------------------------------------------------
        public void Destroy()
        {
            EbDoubleLinkNode<TObject> pnode = mpHead.next;
            do
            {
                if (pnode != mpHead)
                {
                    // 删除结点
                    DelNode(pnode);

                    pnode = mpHead.next;
                }
            } while (pnode != mpHead);
        }

        //-------------------------------------------------------------------------
        // 初始化队列
        public void Init()
        {
            mpHead.next = mpHead;
            mpHead.prev = mpHead;
        }

        //-------------------------------------------------------------------------
        // 向对头添加一个节点
        public void AddNode(EbDoubleLinkNode<TObject> pnew)
        {
            _addNode(pnew, mpHead, mpHead.next);
        }

        //-------------------------------------------------------------------------
        // 向队尾添加一个节点
        public void AddTailNode(EbDoubleLinkNode<TObject> pnew)
        {
            _addNode(pnew, mpHead.prev, mpHead);
        }

        //-------------------------------------------------------------------------
        // 删除一个节点
        public static void DelNode(EbDoubleLinkNode<TObject> entry)
        {
            _delNode(entry.prev, entry.next);
            entry.next = null;
            entry.prev = null;
        }

        //-------------------------------------------------------------------------
        // 替换一个节点
        public void ReplaceNode(EbDoubleLinkNode<TObject> old, EbDoubleLinkNode<TObject> pnew)
        {
            pnew.next = old.next;
            pnew.next.prev = pnew;
            pnew.prev = old.prev;
            pnew.prev.next = pnew;
        }

        //-------------------------------------------------------------------------
        // 最后一个节点
        public bool IsLast(EbDoubleLinkNode<TObject> list)
        {
            return list.next == mpHead;
        }

        //-------------------------------------------------------------------------
        // 队列为空
        public bool Empty()
        {
            return mpHead.next == mpHead;
        }

        //-------------------------------------------------------------------------
        // 将队列移到新的队列中
        // 相当于原来的list复制到新的list,原来的恢复初始值
        public void MoveList(EbDoubleLinkList<TObject> pnew_list)
        {
            if (!pnew_list.Empty())
            {
                pnew_list.Init();
                pnew_list.mpHead.next = mpHead.next;
                pnew_list.mpHead.prev = mpHead.prev;
                Init();
            }
        }

        //-------------------------------------------------------------------------
        // 添加队列到本队列（队头）
        public void AddList(EbDoubleLinkList<TObject> plist)
        {
            if (!plist.Empty())
            {
                EbDoubleLinkNode<TObject> add_list_first = plist.FirstNode();
                EbDoubleLinkNode<TObject> add_list_last = plist.LastNode();
                plist.Init();

                EbDoubleLinkNode<TObject> list_first = FirstNode();
                mpHead.next = add_list_first;
                add_list_first.prev = mpHead;
                add_list_last.next = list_first;
                list_first.prev = add_list_last;
            }
        }

        //-------------------------------------------------------------------------
        // 添加队列到本队列（队尾）
        public void AddTailList(EbDoubleLinkList<TObject> plist)
        {
            if (!plist.Empty())
            {
                EbDoubleLinkNode<TObject> add_list_first = plist.FirstNode();
                EbDoubleLinkNode<TObject> add_list_last = plist.LastNode();
                plist.Init();

                EbDoubleLinkNode<TObject> list_last = LastNode();
                list_last.next = add_list_first;
                add_list_first.prev = list_last;
                add_list_last.next = mpHead;
                mpHead.prev = add_list_last;
            }
        }

        //-------------------------------------------------------------------------
        // 取得第一节点指针
        public EbDoubleLinkNode<TObject> FirstNode()
        {
            return mpHead.next;
        }

        //-------------------------------------------------------------------------
        // 取得最后一个节点
        public EbDoubleLinkNode<TObject> LastNode()
        {
            return mpHead.prev;
        }

        //-------------------------------------------------------------------------
        // 取得队头
        public EbDoubleLinkNode<TObject> Head()
        {
            return mpHead;
        }

        //-------------------------------------------------------------------------
        // 取得下一个节点
        public EbDoubleLinkNode<TObject> NextNode(EbDoubleLinkNode<TObject> pnode)
        {
            return pnode.next;
        }

        //-------------------------------------------------------------------------
        // 取得上一个节点
        public EbDoubleLinkNode<TObject> PrevNode(EbDoubleLinkNode<TObject> pnode)
        {
            return pnode.prev;
        }

        //-------------------------------------------------------------------------
        // 取得队列节点个数
        public uint GetNodeCount()
        {
            EbDoubleLinkNode<TObject> pnode = mpHead.next;
            uint count = 0;
            do
            {
                if (pnode != mpHead)
                {
                    count++;
                    pnode = mpHead.next;
                }
            } while (pnode != mpHead);

            return count;
        }

        //-------------------------------------------------------------------------
        // 添加一个节点
        void _addNode(EbDoubleLinkNode<TObject> pnew, EbDoubleLinkNode<TObject> prev, EbDoubleLinkNode<TObject> next)
        {
            next.prev = pnew;
            pnew.next = next;
            pnew.prev = prev;
            prev.next = pnew;
        }

        //-------------------------------------------------------------------------
        // 删除一个节点
        static void _delNode(EbDoubleLinkNode<TObject> prev, EbDoubleLinkNode<TObject> next)
        {
            if (prev != null && next != null)
            {
                next.prev = prev;
                prev.next = next;
            }
        }
    }
}