// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Networking;

    public class MbHelper : MonoBehaviour
    {
        //-------------------------------------------------------------------------
        public void SendUrl(string url, Action<string> cb)
        {
            StartCoroutine(_sendUrl(url, cb));
        }

        //-------------------------------------------------------------------------
        public void PostUrl(string url, string post_data, Action<string> cb)
        {
            StartCoroutine(_postUrl(url, post_data, cb));
        }

        //-------------------------------------------------------------------------
        IEnumerator _sendUrl(string url, Action<string> cb)
        {
            using (UnityWebRequest www_request = UnityWebRequest.Get(url))
            {
                yield return www_request.SendWebRequest();
                if (www_request.error != null)
                {
                    BuglyAgent.PrintLog(LogSeverity.LogError, www_request.error);
                }
                else
                {
                    // 200表示接受成功
                    if (www_request.responseCode == 200)
                    {
                        //Debug.Log(www.downloadHandler.text);
                        cb?.Invoke(www_request.downloadHandler.text);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------
        IEnumerator _postUrl(string url, string post_data, Action<string> cb)
        {
            using (UnityWebRequest www_request = new UnityWebRequest(url, "POST"))
            {
                byte[] post_bytes = System.Text.Encoding.UTF8.GetBytes(post_data);
                www_request.uploadHandler = new UploadHandlerRaw(post_bytes);
                www_request.downloadHandler = new DownloadHandlerBuffer();
                www_request.SetRequestHeader("Content-Type", "application/json");
                yield return www_request.SendWebRequest();// UnityWebRequestAsyncOperation

                if (www_request.isNetworkError)
                {
                    BuglyAgent.PrintLog(LogSeverity.LogError, www_request.error);
                }
                else
                {
                    if (www_request.responseCode == 200)
                    {
                        //Debug.Log(www_request.downloadHandler.text);
                        cb?.Invoke(www_request.downloadHandler.text);
                    }
                }
            }
        }
    }
}