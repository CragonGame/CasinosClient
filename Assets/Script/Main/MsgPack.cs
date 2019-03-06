using System;
using System.Collections.Generic;
using UnityEngine;

public class MsgPack
{
    //-------------------------------------------------------------------------
    public static byte[] Serialize<T>(T obj)
    {
        return MessagePack.MessagePackSerializer.Serialize(obj);
    }

    //-------------------------------------------------------------------------
    public static T Deserialize<T>(byte[] bytes) where T : new()
    {
        return MessagePack.MessagePackSerializer.Deserialize<T>(bytes);
    }
};
