//
//  UniWebViewNativeListener.cs
//  Created by Wang Wei(@onevcat) on 2017-04-11.
//
//  This file is a part of UniWebView Project (https://uniwebview.com)
//  By purchasing the asset, you are allowed to use this code in as many as projects 
//  you want, only if you publish the final products under the name of the same account
//  used for the purchase. 
//
//  This asset and all corresponding files (such as source code) are provided on an 
//  “as is” basis, without warranty of any kind, express of implied, including but not 
//  limited to the warranties of merchantability, fitness for a particular purpose, and 
//  noninfringement. In no event shall the authors or copyright holders be liable for any 
//  claim, damages or other liability, whether in action of contract, tort or otherwise, 
//  arising from, out of or in connection with the software or the use of other dealing in the software.
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// A listener script for message sent from native side of UniWebView.
/// Normally this component will be attached to a sub `GameObject` under the `UniWebView` one.
/// It will be added automatically and destroyed as needed. So there is rarely a need for you 
/// to manipulate on this class.
/// </summary>
public class UniWebViewNativeListener: MonoBehaviour {
    
    private static Dictionary<String, UniWebViewNativeListener> listeners = new Dictionary<String, UniWebViewNativeListener>();
    public static void AddListener(UniWebViewNativeListener target) {
        listeners.Add(target.Name, target);
    }

    public static void RemoveListener(String name) {
        listeners.Remove(name);
    }

    public static UniWebViewNativeListener GetListener(String name) {
        UniWebViewNativeListener result = null;
        if (listeners.TryGetValue(name, out result)) {
            return result;
        } else {
            return null;
        }
    }

    /// <summary>
    /// The web view holder of this listener.
    /// It will be linked to original web view so you should never set it yourself.
    /// </summary>
    [HideInInspector]
    public UniWebView webView;

    /// <summary>
    /// Name of current listener. This is a UUID string by which native side could use to find 
    /// the message destination.
    /// </summary>
    public string Name {
        get {
            return gameObject.name;
        }
    }

    public void PageStarted(string url) {
        UniWebViewLogger.Instance.Info("Page Started Event. Url: " + url);
        webView.InternalOnPageStarted(url);
    }

    public void PageFinished(string result) {
        UniWebViewLogger.Instance.Info("Page Finished Event. Url: " + result);
        var payload = JsonUtility.FromJson<UniWebViewNativeResultPayload>(result);
        webView.InternalOnPageFinished(payload);
    }

    public void PageErrorReceived(string result) {
        UniWebViewLogger.Instance.Info("Page Error Received Event. Result: " + result);
        var payload = JsonUtility.FromJson<UniWebViewNativeResultPayload>(result);
        webView.InternalOnPageErrorReceived(payload);
    }

    public void ShowTransitionFinished(string identifer) {
        UniWebViewLogger.Instance.Info("Show Transition Finished Event. Identifier: " + identifer);
        webView.InternalOnShowTransitionFinished(identifer);
    }

    public void HideTransitionFinished(string identifer) {
        UniWebViewLogger.Instance.Info("Hide Transition Finished Event. Identifier: " + identifer);
        webView.InternalOnHideTransitionFinished(identifer);
    }

    public void AnimateToFinished(string identifer) {
        UniWebViewLogger.Instance.Info("Animate To Finished Event. Identifier: " + identifer);
        webView.InternalOnAnimateToFinished(identifer);
    }

    public void AddJavaScriptFinished(string result) {
        UniWebViewLogger.Instance.Info("Add JavaScript Finished Event. Result: " + result);
        var payload = JsonUtility.FromJson<UniWebViewNativeResultPayload>(result);
        webView.InternalOnAddJavaScriptFinished(payload);
    }

    public void EvalJavaScriptFinished(string result) {
        UniWebViewLogger.Instance.Info("Eval JavaScript Finished Event. Result: " + result);
        
        var payload = JsonUtility.FromJson<UniWebViewNativeResultPayload>(result);
        webView.InternalOnEvalJavaScriptFinished(payload);
    }

    public void MessageReceived(string result) {
        UniWebViewLogger.Instance.Info("Message Received Event. Result: " + result);
        webView.InternalOnMessageReceived(result);
    }

    public void WebViewKeyDown(string keyCode) {
        UniWebViewLogger.Instance.Info("Web View Key Down: " + keyCode);
        int code;
        if (int.TryParse(keyCode, out code)) {
            webView.InternalOnWebViewKeyDown(code);
        } else {
            UniWebViewLogger.Instance.Critical("Failed in converting key code: " + keyCode);
        }
    }

    public void WebViewDone(string param) {
        UniWebViewLogger.Instance.Info("Web View Done Event.");
        webView.InternalOnShouldClose();
    }

    public void WebContentProcessDidTerminate(string param) {
        UniWebViewLogger.Instance.Info("Web Content Process Terminate Event.");
        webView.InternalWebContentProcessDidTerminate();
    }
}

/// <summary>
/// A payload reveived from native side. It contains information to identify the message sender,
/// as well as some necessary field to bring data from native side to Unity.
/// </summary>
[System.Serializable]
public class UniWebViewNativeResultPayload {
    /// <summary>
    /// The identifier bound to this payload. It would be used internally to identify the callback.
    /// </summary>
    public string identifier;
    /// <summary>
    /// The result code contained in this payload. Generally, "0" means the operation finished without
    /// problem, while a non-zero value means somethings goes wrong.
    /// </summary>
    public string resultCode;
    /// <summary>
    /// Return value or data from native. You should look at 
    /// corresponding APIs to know what exactly contained in this.
    /// </summary>
    public string data;
}