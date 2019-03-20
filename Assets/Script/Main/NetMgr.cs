// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using GameCloud.Unity.Common;

    public class NetMgr
    {
        //---------------------------------------------------------------------
        RpcSession RpcSession { get; set; }
        public Action<ushort, byte[]> OnRpcMethod { get; set; }
        public Action OnSocketConnected { get; set; }
        public Action OnSocketClosed { get; set; }
        public Action OnSocketError { get; set; }

        //---------------------------------------------------------------------
        public NetMgr()
        {
            var rpc_session_factory = new RpcSessionFactoryTcpClient();
            RpcSession = rpc_session_factory.CreateRpcSession(this);
        }

        //---------------------------------------------------------------------
        public void Close()
        {
            OnRpcMethod = null;
        }

        //---------------------------------------------------------------------
        public void Update(float tm)
        {
            RpcSession.Update(tm);
        }

        //---------------------------------------------------------------------
        public void Connect(string ip, int port)
        {
            RpcSession.Close();
            RpcSession.OnSocketConnected = _onSocketConnected;
            RpcSession.OnSocketClosed = _onSocketClosed;
            RpcSession.OnSocketError = _onSocketError;
            RpcSession.Connect(ip, port);
        }

        //---------------------------------------------------------------------
        public void Disconnect()
        {
            RpcSession.Close();
        }

        //---------------------------------------------------------------------
        public void Send(ushort method_id, byte[] data)
        {
            RpcSession.Send(method_id, data);
        }

        //---------------------------------------------------------------------
        void _onSocketConnected(object client, EventArgs args)
        {
            OnSocketConnected();
        }

        //---------------------------------------------------------------------
        void _onSocketClosed(object client, EventArgs args)
        {
            _onSocketClose();
        }

        //---------------------------------------------------------------------
        void _onSocketError(object rec, SocketErrorEventArgs args)
        {
            //if (args != null && args.Exception != null)
            //{
            //    BuglyAgent.PrintLog(LogSeverity.Log, args.Exception.Message);
            //    BuglyAgent.ReportException(args.Exception, args.Exception.Message);
            //}

            OnSocketError();

            _onSocketClose();
        }

        //---------------------------------------------------------------------
        void _onSocketClose()
        {
            //CasinosContext.Instance.LuaMgr._CSharpCallOnSocketClose();
            OnSocketClosed();
        }
    }
}