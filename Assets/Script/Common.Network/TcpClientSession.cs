using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using GameCloud.Unity.Common;

public class TcpClientSession : IDisposable
{
    //-------------------------------------------------------------------------
    readonly int MAX_RECEIVE_LEN = 8192;
    Socket mSocket;
    int mPort;
    EndPoint mRemoteEndPoint;
    string mHost;
    byte[] m_KeepAliveOptionValues;
    byte[] m_KeepAliveOptionOutValues;
    volatile bool mDisposed;
    Queue<byte[]> mQueSending = new Queue<byte[]>();
    Queue<IPAddress> mQueIPAddress = new Queue<IPAddress>();
    int mSendLength = 0;
    volatile bool mConnected;
    bool mIsHost;

    //-------------------------------------------------------------------------
    public bool IsConnected { get { return (mSocket == null || mDisposed) ? false : mConnected; } }
    public OnSocketReceive DataReceived { get; set; }
    public OnSocketConnected Connected { get; set; }
    public OnSocketClosed Closed { get; set; }
    public OnSocketError Error { get; set; }

    //-------------------------------------------------------------------------
    public TcpClientSession(EndPoint remote_endpoint)
    {
        mRemoteEndPoint = remote_endpoint;

        m_KeepAliveOptionValues = new byte[sizeof(uint) * 3];
        m_KeepAliveOptionOutValues = new byte[m_KeepAliveOptionValues.Length];
        // whether enable KeepAlive
        BitConverter.GetBytes((uint)1).CopyTo(m_KeepAliveOptionValues, 0);
        // how long will start first keep alive
        BitConverter.GetBytes((uint)(3 * 1000)).CopyTo(m_KeepAliveOptionValues, sizeof(uint));
        // keep alive interval
        BitConverter.GetBytes((uint)(1 * 1000)).CopyTo(m_KeepAliveOptionValues, sizeof(uint) * 2);
    }

    //-------------------------------------------------------------------------
    public TcpClientSession(string host, int port)
    {
        mHost = host;
        mPort = port;

        m_KeepAliveOptionValues = new byte[sizeof(uint) * 3];
        m_KeepAliveOptionOutValues = new byte[m_KeepAliveOptionValues.Length];
        // whether enable KeepAlive
        BitConverter.GetBytes((uint)1).CopyTo(m_KeepAliveOptionValues, 0);
        // how long will start first keep alive
        BitConverter.GetBytes((uint)(3 * 1000)).CopyTo(m_KeepAliveOptionValues, sizeof(uint));
        // keep alive interval
        BitConverter.GetBytes((uint)(1 * 1000)).CopyTo(m_KeepAliveOptionValues, sizeof(uint) * 2);
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
        if (!mDisposed)
        {
            if (disposing)
            {
                try
                {
                    if (mSocket != null)
                    {
                        mSocket.Shutdown(SocketShutdown.Both);
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    try
                    {
                        if (mSocket != null)
                        {
                            mSocket.Close();
                            mSocket = null;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                _raiseClosed();
            }

            mDisposed = true;
        }
    }

    //-------------------------------------------------------------------------
    public void update()
    {
        try
        {
            if (mSocket == null || mDisposed || !mConnected) return;

            if (mSocket.Poll(0, SelectMode.SelectError))
            {
                _raiseError(new Exception("SocketError"));
                Dispose();
                return;
            }

            if (mSocket.Poll(0, SelectMode.SelectRead))
            {
                byte[] recv_buf = new byte[MAX_RECEIVE_LEN];
                int num = mSocket.Receive(recv_buf, MAX_RECEIVE_LEN, SocketFlags.None);
                if (num > 0) _raiseDataReceived(recv_buf, num);
            }

            if (mSocket.Poll(0, SelectMode.SelectWrite))
            {
                if (mQueSending.Count > 0)
                {
                    byte[] buf = mQueSending.Peek();
                    int send_num = mSocket.Send(buf, mSendLength, buf.Length - mSendLength, SocketFlags.None);
                    if (send_num > 0)
                    {
                        mSendLength += send_num;
                        if (mSendLength >= buf.Length)
                        {
                            mQueSending.Dequeue();
                            mSendLength = 0;
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
    public void connect(IPAddress[] array_ipaddress, bool is_host)
    {
        mIsHost = is_host;
        foreach (var i in array_ipaddress)
        {
            mQueIPAddress.Enqueue(i);
        }

        if (mQueIPAddress.Count > 0)
        {
            _doConnect();
        }
    }

    //-------------------------------------------------------------------------
    public void send(byte[] buf)
    {
        mQueSending.Enqueue(buf);
    }

    //-------------------------------------------------------------------------
    void _doConnect()
    {
        if (mQueIPAddress.Count <= 0 || mDisposed)
        {
            return;
        }

        IPAddress connect_ipaddress = mQueIPAddress.Dequeue();
        try
        {
            if (mDisposed) return;

            mSendLength = 0;

            if (connect_ipaddress.AddressFamily == AddressFamily.InterNetworkV6)
            {
                mSocket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
            }
            else if (connect_ipaddress.AddressFamily == AddressFamily.InterNetwork)
            {
                mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            mSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            mSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);

            mConnected = false;

            if (mIsHost)
            {
                mSocket.BeginConnect(mHost, mPort, new AsyncCallback(_onConnect), mSocket);
            }
            else
            {
                var ip_endpoint = mRemoteEndPoint as IPEndPoint;
                mSocket.BeginConnect(ip_endpoint.Address, ip_endpoint.Port, new AsyncCallback(_onConnect), mSocket);
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

#if !__IOS__
                mSocket.NoDelay = true;
#endif
                mSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);

                mConnected = true;
                _raiseConnected();
            }
            else
            {
                mConnected = false;
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
        if (DataReceived != null) DataReceived.Invoke(data, len);
    }

    //---------------------------------------------------------------------
    void _raiseConnected()
    {
        mQueIPAddress.Clear();

        if (Connected != null) Connected.Invoke(null, EventArgs.Empty);
    }

    //---------------------------------------------------------------------
    void _raiseClosed()
    {
        if (Closed != null) Closed.Invoke(null, EventArgs.Empty);
    }

    //---------------------------------------------------------------------
    void _raiseError(Exception e)
    {
        if (mQueIPAddress.Count > 0)
        {
            _doConnect();
        }

        if (Error != null) Error.Invoke(null, new SocketErrorEventArgs(e));
    }
}
