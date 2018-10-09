using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using SuperSocket.ProtoBase;
using GameCloud.Unity.Common;

enum eSocketEventType : byte
{
    Null = 0,
    Connected,
    Closed,
    Error,
}

struct SocketEvent
{
    public eSocketEventType type;
    public object client;
    public EventArgs args;
}

struct SocketRecvData
{
    public byte[] data;
    public int len;
}

sealed class BufferState : BufferBaseState { }

public class TcpClient : IPackageHandler<BufferedPackageInfo<ushort>>
{
    //---------------------------------------------------------------------
    static readonly ushort HeadLength = 2;
    TcpClientSession mSession;
    string mIpOrHost;
    int mPort;
    DefaultPipelineProcessor<BufferedPackageInfo<ushort>> mPipelineProcessor;
    Queue<SocketRecvData> mRecQueue = new Queue<SocketRecvData>();
    Queue<SocketEvent> mSocketEvent = new Queue<SocketEvent>();
    object mLockWorker = new object();

    //---------------------------------------------------------------------
    public bool IsConnected { get { return (mSession == null) ? false : mSession.IsConnected; } }
    public OnSocketReceive OnSocketReceive { get; set; }
    public OnSocketConnected OnSocketConnected { get; set; }
    public OnSocketClosed OnSocketClosed { get; set; }
    public OnSocketError OnSocketError { get; set; }

    //---------------------------------------------------------------------
    public TcpClient()
    {
        mPipelineProcessor = new DefaultPipelineProcessor<BufferedPackageInfo<ushort>>(
            this, new TcpClientReceiveFilter(), 40960);
        mPipelineProcessor.NewReceiveBufferRequired += _newReceiveBufferRequired;
    }

    //---------------------------------------------------------------------
    public void Handle(BufferedPackageInfo<ushort> package)
    {
        if (OnSocketReceive != null)
        {
            byte[] data = new byte[package.Key];
            _copyTo(package.Data, data, HeadLength, package.Key);

            OnSocketReceive(data, data.Length);
        }
    }

    //---------------------------------------------------------------------
    public void connect(string ip_or_host, int port)
    {
        mIpOrHost = ip_or_host;
        mPort = port;

        IPAddress ip_address = null;
        bool is_host = !IPAddress.TryParse(mIpOrHost, out ip_address);

        IPHostEntry host_info = null;
        if (is_host)
        {
            host_info = Dns.GetHostEntry(mIpOrHost);
        }
        else
        {
            host_info = Dns.GetHostEntry(ip_address);
        }

        IPAddress[] ary_IP = host_info.AddressList;

        if (is_host)
        {
            mSession = new TcpClientSession(mIpOrHost, mPort);
        }
        else
        {
            EndPoint server_address = new IPEndPoint(IPAddress.Parse(mIpOrHost), mPort);
            mSession = new TcpClientSession(server_address);
        }

        mSession.DataReceived += _onReceive;
        mSession.Connected += _onConnected;
        mSession.Closed += _onClosed;
        mSession.Error += _onError;

        mSession.connect(ary_IP, is_host);
    }

    //---------------------------------------------------------------------
    public void update(float elapsed_tm)
    {
        if (mSession != null)
        {
            mSession.update();
        }

        while (true)
        {
            SocketEvent socket_event;
            socket_event.type = eSocketEventType.Null;
            socket_event.client = null;
            socket_event.args = null;

            lock (mLockWorker)
            {
                if (mSocketEvent.Count > 0)
                {
                    socket_event = mSocketEvent.Dequeue();
                }
            }

            if (socket_event.type == eSocketEventType.Null) break;

            switch (socket_event.type)
            {
                case eSocketEventType.Connected:
                    {
                        if (OnSocketConnected != null) OnSocketConnected.Invoke(null, socket_event.args);
                    }
                    break;
                case eSocketEventType.Closed:
                    {
                        if (OnSocketClosed != null) OnSocketClosed.Invoke(null, socket_event.args);
                    }
                    break;
                case eSocketEventType.Error:
                    {
                        if (OnSocketError != null) OnSocketError.Invoke(null, (SocketErrorEventArgs)socket_event.args);
                    }
                    break;
            }
        }
    }

    //---------------------------------------------------------------------
    public void close()
    {
        if (mSession != null)
        {
            mSession.Dispose();
            mSession = null;

            if (OnSocketClosed != null) OnSocketClosed.Invoke(null, EventArgs.Empty);
        }
    }

    //---------------------------------------------------------------------
    public void send(ushort method_id, byte[] data)
    {
        ushort data_len = 0;
        byte[] send_buf = null;
        if (data == null)
        {
            data_len = sizeof(ushort);
            send_buf = new byte[sizeof(ushort) + data_len];
        }
        else
        {
            data_len = (ushort)(sizeof(ushort) + data.Length);
            send_buf = new byte[sizeof(ushort) + data_len];
        }
        Array.Copy(BitConverter.GetBytes(data_len), 0, send_buf, 0, sizeof(ushort));
        Array.Copy(BitConverter.GetBytes(method_id), 0, send_buf, sizeof(ushort), sizeof(ushort));
        if (data != null && data.Length > 0) Array.Copy(data, 0, send_buf, sizeof(ushort) * 2, data.Length);
        mSession.send(send_buf);
    }

    //---------------------------------------------------------------------
    public void send(byte[] buf)
    {
        send(buf, (ushort)buf.Length);
    }

    //---------------------------------------------------------------------
    public void send(byte[] buf, ushort length)
    {
        byte[] send_buf = new byte[HeadLength + length];
        Array.Copy(BitConverter.GetBytes((ushort)length), send_buf, HeadLength);
        Array.Copy(buf, 0, send_buf, HeadLength, length);
        mSession.send(send_buf);
    }

    //---------------------------------------------------------------------
    void _onReceive(byte[] data, int len)
    {
        lock (mLockWorker)
        {
            SocketRecvData recv_data;
            recv_data.data = data;
            recv_data.len = len;
            mRecQueue.Enqueue(recv_data);
        }

        while (true)
        {
            SocketRecvData recv_data;
            recv_data.data = null;
            recv_data.len = 0;
            lock (mLockWorker)
            {
                if (mRecQueue.Count > 0) recv_data = mRecQueue.Dequeue();
            }

            if (recv_data.data == null) break;

            var segment = new ArraySegment<byte>(recv_data.data, 0, recv_data.len);
            mPipelineProcessor.Process(segment, new BufferState());
        }
    }

    //---------------------------------------------------------------------
    void _onConnected(object client, EventArgs args)
    {
        lock (mLockWorker)
        {
            SocketEvent socket_event;
            socket_event.type = eSocketEventType.Connected;
            socket_event.client = client;
            socket_event.args = args;

            mSocketEvent.Enqueue(socket_event);
        }
    }

    //---------------------------------------------------------------------
    void _onClosed(object client, EventArgs args)
    {
        lock (mLockWorker)
        {
            SocketEvent socket_event;
            socket_event.type = eSocketEventType.Closed;
            socket_event.client = client;
            socket_event.args = args;

            mSocketEvent.Enqueue(socket_event);
        }
    }

    //---------------------------------------------------------------------
    void _onError(object client, SocketErrorEventArgs args)
    {
        lock (mLockWorker)
        {
            SocketEvent socket_event;
            socket_event.type = eSocketEventType.Error;
            socket_event.client = client;
            socket_event.args = args;

            mSocketEvent.Enqueue(socket_event);
        }
    }

    //---------------------------------------------------------------------
    void _newReceiveBufferRequired(object client, EventArgs args)
    {
        //EbLog.Error("NewReceiveBufferRequired");
    }

    //-------------------------------------------------------------------------
    void _copyTo(IList<ArraySegment<byte>> package_data, byte[] data, int src_offset, int length)
    {
        var inner_offset = src_offset;
        var rest_length = length;
        var dest_cur_index = 0;

        for (var i = 0; i < package_data.Count; i++)
        {
            var segment = package_data[i];

            int cur_segment_len = 0;
            if (segment.Count <= inner_offset)
            {
                inner_offset -= segment.Count;
                continue;
            }

            cur_segment_len = segment.Count - inner_offset;

            var this_length = Math.Min(rest_length, cur_segment_len);
            Array.Copy(segment.Array, segment.Offset + inner_offset, data, dest_cur_index, this_length);

            inner_offset = 0;

            dest_cur_index += this_length;
            rest_length -= this_length;

            if (rest_length <= 0) break;
        }
    }
}