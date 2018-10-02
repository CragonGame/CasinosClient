using System;
using GameCloud.Unity.Common;

public class RpcSessionTcpClient : RpcSession
{
    //-------------------------------------------------------------------------
    TcpClient tcpSocket;

    //-------------------------------------------------------------------------
    public RpcSessionTcpClient()
    {
        tcpSocket = new TcpClient();
        tcpSocket.OnSocketReceive += _onSocketReceive;
        tcpSocket.OnSocketConnected += _onSocketConnected;
        tcpSocket.OnSocketClosed += _onSocketClosed;
        tcpSocket.OnSocketError += _onSocketError;
    }

    //-------------------------------------------------------------------------
    public override bool isConnect()
    {
        if (tcpSocket == null) return false;

        return tcpSocket.IsConnected;
    }

    //-------------------------------------------------------------------------
    public override void connect(string ip, int port)
    {
        if (tcpSocket == null)
        {
            tcpSocket = new TcpClient();
            tcpSocket.OnSocketReceive += _onSocketReceive;
            tcpSocket.OnSocketConnected += _onSocketConnected;
            tcpSocket.OnSocketClosed += _onSocketClosed;
            tcpSocket.OnSocketError += _onSocketError;
        }

        tcpSocket.connect(ip, port);
    }

    //-------------------------------------------------------------------------
    public override void send(ushort method_id, byte[] data)
    {
        if (tcpSocket != null)
        {
            tcpSocket.send(method_id, data);
        }
    }

    //-------------------------------------------------------------------------
    public override void onRecv(ushort method_id, byte[] data)
    {
    }

    //-------------------------------------------------------------------------
    public override void close()
    {
        if (tcpSocket != null)
        {
            tcpSocket.close();
            tcpSocket = null;
        }
    }

    //-------------------------------------------------------------------------
    public override void update(float elapsed_tm)
    {
        if (tcpSocket != null)
        {
            tcpSocket.update(elapsed_tm);
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

        Casinos.CasinosContext.Instance.NetBridge.LuaOnRpcMethod(method_id, buf);
    }

    //-------------------------------------------------------------------------
    void _onSocketError(object rec, SocketErrorEventArgs args)
    {
        //this.tcpSocket = null;

        OnSocketError?.Invoke(this, args);
    }

    //-------------------------------------------------------------------------
    void _onSocketConnected(object client, EventArgs args)
    {
        OnSocketConnected?.Invoke(this, args);
    }

    //-------------------------------------------------------------------------
    void _onSocketClosed(object client, EventArgs args)
    {
        //this.tcpSocket = null;

        OnSocketClosed?.Invoke(this, args);
    }
}

public class RpcSessionFactoryTcpClient : RpcSessionFactory
{
    //-------------------------------------------------------------------------
    public override RpcSession createRpcSession()
    {
        return new RpcSessionTcpClient();
    }
}