// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;

    public delegate void OnSocketReceive(byte[] data, int len);
    public delegate void OnSocketConnected(object client, EventArgs args);
    public delegate void OnSocketClosed(object client, EventArgs args);
    public delegate void OnSocketError(object rec, SocketErrorEventArgs args);

    public class SocketErrorEventArgs : EventArgs
    {
        public Exception Exception { get; private set; }

        public SocketErrorEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }

    public abstract class RpcSlotBase
    {
        //---------------------------------------------------------------------
        public abstract void onRpcMethod(byte[] buf);
    }

    public class RpcSlot : RpcSlotBase
    {
        //---------------------------------------------------------------------
        public Action action;

        //---------------------------------------------------------------------
        public RpcSlot(Action a)
        {
            action = a;
        }

        //---------------------------------------------------------------------
        public override void onRpcMethod(byte[] buf)
        {
            action();
        }
    }

    public class RpcSlot<T1> : RpcSlotBase
    {
        //---------------------------------------------------------------------
        public Action<T1> action;

        //---------------------------------------------------------------------
        public RpcSlot(Action<T1> a)
        {
            action = a;
        }

        //---------------------------------------------------------------------
        public override void onRpcMethod(byte[] buf)
        {
            //if (buf == null)
            //{
            //    return;
            //}

            //using (MemoryStream s = new MemoryStream(buf))
            //{
            //    try
            //    {
            //        var t1 = ProtoBuf.Serializer.Deserialize<T1>(s);

            //        action(t1);
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //}
        }
    }

    public class RpcSlot<T1, T2> : RpcSlotBase
    {
        //---------------------------------------------------------------------
        public Action<T1, T2> action;

        //---------------------------------------------------------------------
        public RpcSlot(Action<T1, T2> a)
        {
            action = a;
        }

        //---------------------------------------------------------------------
        public override void onRpcMethod(byte[] buf)
        {
            //if (buf == null || buf.Length == 0)
            //{
            //    return;
            //}

            //using (MemoryStream s = new MemoryStream(buf))
            //{
            //    try
            //    {
            //        var t1 = ProtoBuf.Serializer.DeserializeWithLengthPrefix<T1>(s, ProtoBuf.PrefixStyle.Fixed32);
            //        var t2 = ProtoBuf.Serializer.DeserializeWithLengthPrefix<T2>(s, ProtoBuf.PrefixStyle.Fixed32);

            //        action(t1, t2);
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //}
        }
    }

    public class RpcSlot<T1, T2, T3> : RpcSlotBase
    {
        //---------------------------------------------------------------------
        public Action<T1, T2, T3> action;

        //---------------------------------------------------------------------
        public RpcSlot(Action<T1, T2, T3> a)
        {
            action = a;
        }

        //---------------------------------------------------------------------
        public override void onRpcMethod(byte[] buf)
        {
            //if (buf == null || buf.Length == 0)
            //{
            //    return;
            //}

            //using (MemoryStream s = new MemoryStream(buf))
            //{
            //    try
            //    {
            //        var t1 = ProtoBuf.Serializer.DeserializeWithLengthPrefix<T1>(s, ProtoBuf.PrefixStyle.Fixed32);
            //        var t2 = ProtoBuf.Serializer.DeserializeWithLengthPrefix<T2>(s, ProtoBuf.PrefixStyle.Fixed32);
            //        var t3 = ProtoBuf.Serializer.DeserializeWithLengthPrefix<T3>(s, ProtoBuf.PrefixStyle.Fixed32);

            //        action(t1, t2, t3);
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //}
        }
    }

    public class RpcSlot<T1, T2, T3, T4> : RpcSlotBase
    {
        //---------------------------------------------------------------------
        public Action<T1, T2, T3, T4> action;

        //---------------------------------------------------------------------
        public RpcSlot(Action<T1, T2, T3, T4> a)
        {
            action = a;
        }

        //---------------------------------------------------------------------
        public override void onRpcMethod(byte[] buf)
        {
            //if (buf == null || buf.Length == 0)
            //{
            //    return;
            //}

            //using (MemoryStream s = new MemoryStream(buf))
            //{
            //    try
            //    {
            //        var t1 = ProtoBuf.Serializer.DeserializeWithLengthPrefix<T1>(s, ProtoBuf.PrefixStyle.Fixed32);
            //        var t2 = ProtoBuf.Serializer.DeserializeWithLengthPrefix<T2>(s, ProtoBuf.PrefixStyle.Fixed32);
            //        var t3 = ProtoBuf.Serializer.DeserializeWithLengthPrefix<T3>(s, ProtoBuf.PrefixStyle.Fixed32);
            //        var t4 = ProtoBuf.Serializer.DeserializeWithLengthPrefix<T4>(s, ProtoBuf.PrefixStyle.Fixed32);

            //        action(t1, t2, t3, t4);
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //}
        }
    }

    public abstract class RpcSession
    {
        //---------------------------------------------------------------------
        // key=method_id
        Dictionary<ushort, RpcSlotBase> mMapRpcSlot = new Dictionary<ushort, RpcSlotBase>();

        //---------------------------------------------------------------------
        public OnSocketConnected OnSocketConnected { get; set; }
        public OnSocketClosed OnSocketClosed { get; set; }
        public OnSocketError OnSocketError { get; set; }

        //---------------------------------------------------------------------
        public abstract bool isConnect();

        public abstract void connect(string ip, int port);

        //---------------------------------------------------------------------
        public abstract void send(ushort method_id, byte[] data);

        //---------------------------------------------------------------------
        public abstract void onRecv(ushort method_id, byte[] data);

        //---------------------------------------------------------------------
        public abstract void close();

        //---------------------------------------------------------------------
        public abstract void update(float elapsed_tm);

        //---------------------------------------------------------------------
        public void rpc(ushort method_id)
        {
            send(method_id, null);
        }

        //---------------------------------------------------------------------
        public void rpc<T1>(ushort method_id, T1 obj1)
        {
            //byte[] data = null;

            //using (MemoryStream s = new MemoryStream())
            //{
            //    try
            //    {
            //        ProtoBuf.Serializer.Serialize<T1>(s, obj1);
            //        data = s.ToArray();
            //    }
            //    catch (Exception ex)
            //    {
            //        return;
            //    }
            //}

            //send(method_id, data);
        }

        //---------------------------------------------------------------------
        public void rpc<T1, T2>(ushort method_id, T1 obj1, T2 obj2)
        {
            //byte[] data = null;

            //using (MemoryStream s = new MemoryStream())
            //{
            //    try
            //    {
            //        ProtoBuf.Serializer.SerializeWithLengthPrefix<T1>(s, obj1, ProtoBuf.PrefixStyle.Fixed32);
            //        ProtoBuf.Serializer.SerializeWithLengthPrefix<T2>(s, obj2, ProtoBuf.PrefixStyle.Fixed32);
            //        data = s.ToArray();
            //    }
            //    catch (Exception ex)
            //    {
            //        return;
            //    }
            //}

            //send(method_id, data);
        }

        //---------------------------------------------------------------------
        public void rpc<T1, T2, T3>(ushort method_id, T1 obj1, T2 obj2, T3 obj3)
        {
            //byte[] data = null;

            //using (MemoryStream s = new MemoryStream())
            //{
            //    try
            //    {
            //        ProtoBuf.Serializer.SerializeWithLengthPrefix<T1>(s, obj1, ProtoBuf.PrefixStyle.Fixed32);
            //        ProtoBuf.Serializer.SerializeWithLengthPrefix<T2>(s, obj2, ProtoBuf.PrefixStyle.Fixed32);
            //        ProtoBuf.Serializer.SerializeWithLengthPrefix<T3>(s, obj3, ProtoBuf.PrefixStyle.Fixed32);
            //        data = s.ToArray();
            //    }
            //    catch (Exception ex)
            //    {
            //        return;
            //    }
            //}

            //send(method_id, data);
        }

        //---------------------------------------------------------------------
        public void rpc<T1, T2, T3, T4>(ushort method_id, T1 obj1, T2 obj2, T3 obj3, T4 obj4)
        {
            //byte[] data = null;

            //using (MemoryStream s = new MemoryStream())
            //{
            //    try
            //    {
            //        ProtoBuf.Serializer.SerializeWithLengthPrefix<T1>(s, obj1, ProtoBuf.PrefixStyle.Fixed32);
            //        ProtoBuf.Serializer.SerializeWithLengthPrefix<T2>(s, obj2, ProtoBuf.PrefixStyle.Fixed32);
            //        ProtoBuf.Serializer.SerializeWithLengthPrefix<T3>(s, obj3, ProtoBuf.PrefixStyle.Fixed32);
            //        ProtoBuf.Serializer.SerializeWithLengthPrefix<T4>(s, obj4, ProtoBuf.PrefixStyle.Fixed32);
            //        data = s.ToArray();
            //    }
            //    catch (Exception ex)
            //    {
            //        return;
            //    }
            //}

            //send(method_id, data);
        }

        //---------------------------------------------------------------------
        public void defRpcMethod(ushort method_id, Action action)
        {
            _defRpcMethod(method_id, action);
        }

        //---------------------------------------------------------------------
        public void defRpcMethod<T1>(ushort method_id, Action<T1> action)
        {
            _defRpcMethod<T1>(method_id, action);
        }

        //---------------------------------------------------------------------
        public void defRpcMethod<T1, T2>(ushort method_id, Action<T1, T2> action)
        {
            _defRpcMethod<T1, T2>(method_id, action);
        }

        //---------------------------------------------------------------------
        public void defRpcMethod<T1, T2, T3>(ushort method_id, Action<T1, T2, T3> action)
        {
            _defRpcMethod<T1, T2, T3>(method_id, action);
        }

        //---------------------------------------------------------------------
        public void defRpcMethod<T1, T2, T3, T4>(ushort method_id, Action<T1, T2, T3, T4> action)
        {
            _defRpcMethod<T1, T2, T3, T4>(method_id, action);
        }

        //---------------------------------------------------------------------
        public void onRpcMethod(ushort method_id, byte[] data)
        {
            onRecv(method_id, data);

            RpcSlotBase rpc_slot = null;
            mMapRpcSlot.TryGetValue(method_id, out rpc_slot);
            if (rpc_slot == null)
            {
                return;
            }

            rpc_slot.onRpcMethod(data);
        }

        //---------------------------------------------------------------------
        void _defRpcMethod(ushort method_id, Action a)
        {
            RpcSlot rpc_slot = new RpcSlot(a);
            mMapRpcSlot[method_id] = rpc_slot;
        }

        //---------------------------------------------------------------------
        void _defRpcMethod<T1>(ushort method_id, Action<T1> a)
        {
            RpcSlot<T1> rpc_slot = new RpcSlot<T1>(a);
            mMapRpcSlot[method_id] = rpc_slot;
        }

        //---------------------------------------------------------------------
        void _defRpcMethod<T1, T2>(ushort method_id, Action<T1, T2> a)
        {
            RpcSlot<T1, T2> rpc_slot = new RpcSlot<T1, T2>(a);
            mMapRpcSlot[method_id] = rpc_slot;
        }

        //---------------------------------------------------------------------
        void _defRpcMethod<T1, T2, T3>(ushort method_id, Action<T1, T2, T3> a)
        {
            RpcSlot<T1, T2, T3> rpc_slot = new RpcSlot<T1, T2, T3>(a);
            mMapRpcSlot[method_id] = rpc_slot;
        }

        //---------------------------------------------------------------------
        void _defRpcMethod<T1, T2, T3, T4>(ushort method_id, Action<T1, T2, T3, T4> a)
        {
            RpcSlot<T1, T2, T3, T4> rpc_slot = new RpcSlot<T1, T2, T3, T4>(a);
            mMapRpcSlot[method_id] = rpc_slot;
        }

        //---------------------------------------------------------------------
        void _clear()
        {
            mMapRpcSlot.Clear();
        }
    }

    public abstract class RpcSessionFactory
    {
        //---------------------------------------------------------------------
        public abstract RpcSession createRpcSession();
    }
}
