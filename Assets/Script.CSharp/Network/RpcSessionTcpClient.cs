using System;
using GameCloud.Unity.Common;

public class RpcSessionTcpClient : RpcSession
{
    //-------------------------------------------------------------------------
    TcpClient tcpSocket;

    //-------------------------------------------------------------------------
    public RpcSessionTcpClient()
    {
        this.tcpSocket = new TcpClient();
        this.tcpSocket.OnSocketReceive += _onSocketReceive;
        this.tcpSocket.OnSocketConnected += _onSocketConnected;
        this.tcpSocket.OnSocketClosed += _onSocketClosed;
        this.tcpSocket.OnSocketError += _onSocketError;
    }

    //-------------------------------------------------------------------------
    public override bool isConnect()
    {
        if (this.tcpSocket == null) return false;

        return this.tcpSocket.IsConnected;
    }

    //-------------------------------------------------------------------------
    public override void connect(string ip, int port)
    {
        if (this.tcpSocket == null)
        {
            this.tcpSocket = new TcpClient();
            this.tcpSocket.OnSocketReceive += _onSocketReceive;
            this.tcpSocket.OnSocketConnected += _onSocketConnected;
            this.tcpSocket.OnSocketClosed += _onSocketClosed;
            this.tcpSocket.OnSocketError += _onSocketError;
        }

        this.tcpSocket.connect(ip, port);
    }

    //-------------------------------------------------------------------------
    public override void send(ushort method_id, byte[] data)
    {
        if (this.tcpSocket != null)
        {
            this.tcpSocket.send(method_id, data);
        }
    }

    //-------------------------------------------------------------------------
    public override void onRecv(ushort method_id, byte[] data)
    {
    }

    //-------------------------------------------------------------------------
    public override void close()
    {
        if (this.tcpSocket != null)
        {
            this.tcpSocket.close();
            this.tcpSocket = null;
        }
    }

    //-------------------------------------------------------------------------
    public override void update(float elapsed_tm)
    {
        if (this.tcpSocket != null)
        {
            this.tcpSocket.update(elapsed_tm);
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

        //if (method_id >= Casinos.CasinosContext.Instance.CSharpLastMethodId)
        //{          
        //    Casinos.CasinosContext.Instance.NetBridge.LuaOnRpcMethod(method_id, buf);
        //}
        //else
        //{
        //    onRpcMethod(method_id, buf);
        //}
    }

    //-------------------------------------------------------------------------
    void _onSocketError(object rec, SocketErrorEventArgs args)
    {
        //this.tcpSocket = null;

        if (OnSocketError != null)
        {
            OnSocketError(this, args);
        }
    }

    //-------------------------------------------------------------------------
    void _onSocketConnected(object client, EventArgs args)
    {
        if (OnSocketConnected != null)
        {
            OnSocketConnected(this, args);
        }
    }

    //-------------------------------------------------------------------------
    void _onSocketClosed(object client, EventArgs args)
    {
        //this.tcpSocket = null;

        if (OnSocketClosed != null)
        {
            OnSocketClosed(this, args);
        }
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