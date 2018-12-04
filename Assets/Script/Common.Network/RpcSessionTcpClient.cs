using System;
using GameCloud.Unity.Common;

public class RpcSessionTcpClient : RpcSession
{
    //-------------------------------------------------------------------------
    TcpClient TcpClient;

    //-------------------------------------------------------------------------
    public RpcSessionTcpClient()
    {
        TcpClient = new TcpClient();
        TcpClient.OnSocketReceive += _onSocketReceive;
        TcpClient.OnSocketConnected += _onSocketConnected;
        TcpClient.OnSocketClosed += _onSocketClosed;
        TcpClient.OnSocketError += _onSocketError;
    }

    //-------------------------------------------------------------------------
    public override bool IsConnect()
    {
        if (TcpClient == null) return false;

        return TcpClient.IsConnected;
    }

    //-------------------------------------------------------------------------
    public override void Connect(string ip, int port)
    {
        if (TcpClient == null)
        {
            TcpClient = new TcpClient();
            TcpClient.OnSocketReceive += _onSocketReceive;
            TcpClient.OnSocketConnected += _onSocketConnected;
            TcpClient.OnSocketClosed += _onSocketClosed;
            TcpClient.OnSocketError += _onSocketError;
        }

        TcpClient.Connect(ip, port);
    }

    //-------------------------------------------------------------------------
    public override void Send(ushort method_id, byte[] data)
    {
        if (TcpClient != null)
        {
            TcpClient.Send(method_id, data);
        }
    }

    //-------------------------------------------------------------------------
    public override void OnRecv(ushort method_id, byte[] data)
    {
    }

    //-------------------------------------------------------------------------
    public override void Close()
    {
        if (TcpClient != null)
        {
            TcpClient.Close();
            TcpClient = null;
        }
    }

    //-------------------------------------------------------------------------
    public override void Update(float elapsed_tm)
    {
        if (TcpClient != null)
        {
            TcpClient.Update(elapsed_tm);
        }
    }

    //-------------------------------------------------------------------------
    void _onSocketReceive(byte[] data, int len)
    {
        //bool is_little_endian = BitConverter.IsLittleEndian;
        ushort method_id = BitConverter.ToUInt16(data, 0);
        byte[] buf = null;
        if (data.Length > sizeof(ushort))
        {
            ushort data_len = (ushort)(data.Length - sizeof(ushort));
            buf = new byte[data_len];
            Array.Copy(data, sizeof(ushort), buf, 0, data_len);
        }
        else buf = new byte[0];

        Casinos.CasinosContext.Instance.NetMgr.LuaOnRpcMethod(method_id, buf);
    }

    //-------------------------------------------------------------------------
    void _onSocketError(object rec, SocketErrorEventArgs args)
    {
        //TcpClient = null;

        if (OnSocketError != null) OnSocketError.Invoke(this, args);
    }

    //-------------------------------------------------------------------------
    void _onSocketConnected(object client, EventArgs args)
    {
        if (OnSocketConnected != null) OnSocketConnected.Invoke(this, args);
    }

    //-------------------------------------------------------------------------
    void _onSocketClosed(object client, EventArgs args)
    {
        //TcpClient = null;

        if (OnSocketClosed != null) OnSocketClosed.Invoke(this, args);
    }
}

public class RpcSessionFactoryTcpClient : RpcSessionFactory
{
    //-------------------------------------------------------------------------
    public override RpcSession CreateRpcSession()
    {
        return new RpcSessionTcpClient();
    }
}