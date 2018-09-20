using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using GameCloud.Unity.Common;

public class EntityFileStreamUnity3D : EbFileStream
{
    //---------------------------------------------------------------------
    public override bool load(string file_name)
    {
        WWW w = new WWW(file_name);
        while (!w.isDone) { }
        mData = w.bytes;
        mDataStr = System.Text.Encoding.Default.GetString(mData);
        return true;
    }

    //---------------------------------------------------------------------
    public override bool save(string file_name, byte[] data)
    {
        return false;
    }
}
