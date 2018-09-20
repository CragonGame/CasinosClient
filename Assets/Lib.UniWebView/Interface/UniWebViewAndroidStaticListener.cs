
// #if UNITY_ANDROID && !UNITY_EDITOR
using System;
using System.Reflection;
using UnityEngine;

public class UniWebViewAndroidStaticListener: MonoBehaviour {
    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    void OnJavaMessage(string message) {
        // {listener_name}@{method_name}@parameters
        string[] parts = message.Split("@"[0]);
        if (parts.Length < 3) {
            Debug.Log("Not enough parts for receiving a message.");
            return;
        }

        var listener = UniWebViewNativeListener.GetListener(parts[0]);
        if (listener == null) {
            Debug.Log("Unable to find listener");
            return;
        }
        
        MethodInfo methodInfo = typeof(UniWebViewNativeListener).GetMethod(parts[1]);
        if (methodInfo == null) {
            Debug.Log("Cannot find correct method to invoke: " + parts[1]);
        }
        
        var leftLength = parts.Length - 2;
        var left = new string[leftLength];
        for (int i = 0; i < leftLength; i++) {
            left[i] = parts[i + 2];
        }
        methodInfo.Invoke(listener, new object[] { String.Join("@", left) });
    }
}

// #endif