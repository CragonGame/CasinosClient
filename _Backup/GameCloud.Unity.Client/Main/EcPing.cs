using UnityEngine;
using System.Collections;
using GameCloud.Unity.Common;

public class EcPing
{
    //-------------------------------------------------------------------------
    Ping mPing = null;
    float mPingTm = 5f;
    bool mStart = false;
    string mIp = "";

    //-------------------------------------------------------------------------
    public int PingTime { get; private set; }

    //-------------------------------------------------------------------------
    public void start(string ip)
    {
        mStart = true;
        mIp = ip;

        if (mPing != null)
        {
            mPing.DestroyPing();
            mPing = null;
        }
        mPing = new Ping(mIp);
    }

    //-------------------------------------------------------------------------
    public void stop()
    {
        mStart = false;

        if (mPing != null)
        {
            mPing.DestroyPing();
            mPing = null;
        }
    }

    //-------------------------------------------------------------------------
    public void update(float elapsed_tm)
    {
        if (!mStart) return;

        if (mPing != null && mPing.isDone)
        {
            PingTime = mPing.time;

            if (mPing != null)
            {
                mPing.DestroyPing();
                mPing = null;
                mPingTm = 5f;
            }
        }
        else if (mPing == null)
        {
            mPingTm -= elapsed_tm;
            if (mPingTm < 0f)
            {
                mPingTm = 5f;
                mPing = new Ping(mIp);
            }
        }
    }
}
