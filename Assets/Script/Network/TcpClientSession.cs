using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using GameCloud.Unity.Common;

public class TcpClientSession : IDisposable
{
    //-------------------------------------------------------------------------
    readonly int MAX_RECEIVE_LEN = 8192;
    Socket Socket;
    int Port = 0;
    EndPoint RemoteEndPoint;
    string Host = string.Empty;
    volatile bool Disposed = false;
    Queue<byte[]> QueSending = new Queue<byte[]>();
    Queue<IPAddress> QueIPAddress = new Queue<IPAddress>();
    int SendLength = 0;
    volatile bool Connected;
    bool IsHost;

    //-------------------------------------------------------------------------
    public bool IsConnected { get { return (Socket == null || Disposed) ? false : Connected; } }
    public OnSocketReceive OnDataReceived { get; set; }
    public OnSocketConnected OnConnected { get; set; }
    public OnSocketClosed OnClosed { get; set; }
    public OnSocketError OnError { get; set; }

    //-------------------------------------------------------------------------
    public TcpClientSession(EndPoint remote_endpoint)
    {
        RemoteEndPoint = remote_endpoint;
    }

    //-------------------------------------------------------------------------
    public TcpClientSession(string host, int port)
    {
        Host = host;
        Port = port;
    }

    //-------------------------------------------------------------------------
    ~TcpClientSession()
    {
        Dispose(false);
    }

    //-------------------------------------------------------------------------
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    //-------------------------------------------------------------------------
    protected void Dispose(bool disposing)
    {
        if (!Disposed)
        {
            if (disposing)
            {
                try
                {
                    if (Socket != null)
                    {
                        Socket.Shutdown(SocketShutdown.Both);
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    try
                    {
                        if (Socket != null)
                        {
                            Socket.Close();
                            Socket = null;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                _raiseClosed();
            }

            Disposed = true;
        }
    }

    //-------------------------------------------------------------------------
    public void Update()
    {
        try
        {
            if (Socket == null || Disposed || !Connected) return;

            if (Socket.Poll(0, SelectMode.SelectError))
            {
                _raiseError(new Exception("SocketError"));
                Dispose();
                return;
            }

            if (Socket.Poll(0, SelectMode.SelectRead))
            {
                byte[] recv_buf = new byte[MAX_RECEIVE_LEN];
                int num = Socket.Receive(recv_buf, MAX_RECEIVE_LEN, SocketFlags.None);
                if (num > 0) _raiseDataReceived(recv_buf, num);
            }

            if (Socket.Poll(0, SelectMode.SelectWrite))
            {
                if (QueSending.Count > 0)
                {
                    byte[] buf = QueSending.Peek();
                    int send_num = Socket.Send(buf, SendLength, buf.Length - SendLength, SocketFlags.None);
                    if (send_num > 0)
                    {
                        SendLength += send_num;
                        if (SendLength >= buf.Length)
                        {
                            QueSending.Dequeue();
                            SendLength = 0;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _raiseError(ex);
            Dispose();
        }
    }

    //-------------------------------------------------------------------------
    public void Connect(IPAddress[] array_ipaddress, bool is_host)
    {
        IsHost = is_host;
        foreach (var i in array_ipaddress)
        {
            QueIPAddress.Enqueue(i);
        }

        if (QueIPAddress.Count > 0)
        {
            _doConnect();
        }
    }

    //-------------------------------------------------------------------------
    public void Send(byte[] buf)
    {
        QueSending.Enqueue(buf);
    }

    //-------------------------------------------------------------------------
    void _doConnect()
    {
        if (QueIPAddress.Count <= 0 || Disposed)
        {
            return;
        }

        IPAddress connect_ipaddress = QueIPAddress.Dequeue();
        try
        {
            if (Disposed) return;

            SendLength = 0;

            if (connect_ipaddress.AddressFamily == AddressFamily.InterNetworkV6)
            {
                Socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
            }
            else if (connect_ipaddress.AddressFamily == AddressFamily.InterNetwork)
            {
                Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);

            Connected = false;

            if (IsHost)
            {
                Socket.BeginConnect(Host, Port, new AsyncCallback(_onConnect), Socket);
            }
            else
            {
                var ip_endpoint = RemoteEndPoint as IPEndPoint;
                Socket.BeginConnect(ip_endpoint.Address, ip_endpoint.Port, new AsyncCallback(_onConnect), Socket);
            }
        }
        catch (Exception ex)
        {
            _raiseError(ex);
        }
    }

    //-------------------------------------------------------------------------
    void _onConnect(IAsyncResult result)
    {
        try
        {
            Socket socket = (Socket)result.AsyncState;
            socket.EndConnect(result);

            if (socket.Connected)
            {
#if !__IOS__
                Socket.NoDelay = true;
#endif
                Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);

                Connected = true;
                _raiseConnected();
            }
            else
            {
                Connected = false;
                _raiseClosed();
            }
        }
        catch (Exception ex)
        {
            _raiseError(ex);
            _raiseClosed();
        }
    }

    //---------------------------------------------------------------------
    void _raiseDataReceived(byte[] data, int len)
    {
        if (OnDataReceived != null) OnDataReceived.Invoke(data, len);
    }

    //---------------------------------------------------------------------
    void _raiseConnected()
    {
        QueIPAddress.Clear();

        if (OnConnected != null) OnConnected.Invoke(null, EventArgs.Empty);
    }

    //---------------------------------------------------------------------
    void _raiseClosed()
    {
        if (OnClosed != null) OnClosed.Invoke(null, EventArgs.Empty);
    }

    //---------------------------------------------------------------------
    void _raiseError(Exception e)
    {
        if (QueIPAddress.Count > 0)
        {
            _doConnect();
        }

        if (OnError != null) OnError.Invoke(null, new SocketErrorEventArgs(e));
    }
}

//byte[] KeepAliveOptionValues;
//KeepAliveOptionValues = new byte[sizeof(uint) * 3];
//// whether enable KeepAlive
//BitConverter.GetBytes((uint)1).CopyTo(KeepAliveOptionValues, 0);
//// how long will start first keep alive
//BitConverter.GetBytes((uint)(3 * 1000)).CopyTo(KeepAliveOptionValues, sizeof(uint));
//// keep alive interval
//BitConverter.GetBytes((uint)(1 * 1000)).CopyTo(KeepAliveOptionValues, sizeof(uint) * 2);
//KeepAliveOptionValues = new byte[sizeof(uint) * 3];
////m_KeepAliveOptionOutValues = new byte[m_KeepAliveOptionValues.Length];
//// whether enable KeepAlive
//BitConverter.GetBytes((uint)1).CopyTo(KeepAliveOptionValues, 0);
//// how long will start first keep alive
//BitConverter.GetBytes((uint)(3 * 1000)).CopyTo(KeepAliveOptionValues, sizeof(uint));
//// keep alive interval
//BitConverter.GetBytes((uint)(1 * 1000)).CopyTo(KeepAliveOptionValues, sizeof(uint) * 2);
//byte[] m_KeepAliveOptionOutValues;
//m_KeepAliveOptionOutValues = new byte[m_KeepAliveOptionValues.Length];
//bool SupportSocketIOControlByCodeEnum;
//try
//{
//    mSocket.IOControl(IOControlCode.KeepAliveValues, null, null);
//    SupportSocketIOControlByCodeEnum = true;
//}
//catch (NotSupportedException)
//{
//    SupportSocketIOControlByCodeEnum = false;
//}
//catch (NotImplementedException)
//{
//    SupportSocketIOControlByCodeEnum = false;
//}
//catch (Exception)
//{
//    SupportSocketIOControlByCodeEnum = true;
//}

//if (!SupportSocketIOControlByCodeEnum)
//{
//    mSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, m_KeepAliveOptionValues);
//}
//else
//{
//    mSocket.IOControl(IOControlCode.KeepAliveValues, m_KeepAliveOptionValues, m_KeepAliveOptionOutValues);
//}