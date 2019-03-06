using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using MessagePack;

[MessagePackObject]
public class A
{
    [Key(0)]
    public int Aa { get; set; }
    [Key(1)]
    public string Bb { get; set; }
};

public class TestMsgPack
{
    //-------------------------------------------------------------------------
    public static void Run()
    {
        Debug.Log("TestMsgPack.Run() ~~~~~~~~~~~~~11");

        A a = new A();
        a.Aa = 100;
        a.Bb = "aaaaaa";

        byte[] bytes = MessagePackSerializer.Serialize(a);
        Debug.Log("MessagePackSerializer.Serialize() bytes.Length=" + bytes.Length);

        A a1 = MessagePackSerializer.Deserialize<A>(bytes);
        Debug.Log("a1.Aa=" + a1.Aa + "  a1.Bb=" + a1.Bb);

        //string str = MessagePackSerializer.ToJson(a);
        //Debug.Log(str);

        //MessagePackSerializer.FromJson<A>(str);
        //Debug.Log("a1.Aa=" + a1.Aa + "  a1.Bb=" + a1.Bb);
    }
}
