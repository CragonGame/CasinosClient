using System;
using UnityEngine;

public class NativeReceiver : MonoBehaviour
{
    public Action<string> Action { get; set; }

    // 处理结果返回json串
    public void CallBackResult(string json_ret)
    {
        Action(json_ret);
    }
}