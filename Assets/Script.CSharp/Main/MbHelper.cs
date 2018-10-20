// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Networking;

    public class MbHelper : MonoBehaviour
    {
        //---------------------------------------------------------------------
        public void SendUrl(string url, Action<string> cb)
        {
            StartCoroutine(_sendUrl(url, cb));
        }

        //---------------------------------------------------------------------
        public void PostUrl(string url, string post_data, Action<int, string> cb)
        {
            StartCoroutine(_postUrl(url, post_data, cb));
        }

        //---------------------------------------------------------------------
        public void PostUrlWithFormData(string url, WWWForm form_data, Action<int, string> cb)
        {
            StartCoroutine(_postUrlWithFormData(url, form_data, cb));
        }

        //---------------------------------------------------------------------
        public void WWWDownload(string url, Action<WWW> cb)
        {
            StartCoroutine(_wwwDownload(url, cb));
        }

        //---------------------------------------------------------------------
        public void WWWDownloadList(List<string> list_url, Action<WWW[]> cb)
        {
            StartCoroutine(_wwwDownloadList(list_url, cb));
        }

        //---------------------------------------------------------------------
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
                    if (www_request.responseCode == 200)
                    {
                        if (cb != null) cb.Invoke(www_request.downloadHandler.text);
                    }
                }
            }
        }

        //---------------------------------------------------------------------
        IEnumerator _postUrl(string url, string post_data, Action<int, string> cb)
        {
            using (UnityWebRequest www_request = new UnityWebRequest(url, "POST"))
            {
                byte[] post_bytes = System.Text.Encoding.UTF8.GetBytes(post_data);
                www_request.uploadHandler = new UploadHandlerRaw(post_bytes);
                www_request.downloadHandler = new DownloadHandlerBuffer();
                www_request.SetRequestHeader("Content-Type", "application/json");

                yield return www_request.SendWebRequest();

                int http_statuscode = (int)www_request.responseCode;
                if (www_request.isNetworkError)
                {
                    BuglyAgent.PrintLog(LogSeverity.LogError, www_request.error);
                }

                if (cb != null) cb.Invoke(http_statuscode, www_request.downloadHandler.text);
            }
        }

        //---------------------------------------------------------------------
        IEnumerator _postUrlWithFormData(string url, WWWForm form_data, Action<int, string> cb)
        {
            using (UnityWebRequest www_request = UnityWebRequest.Post(url, form_data))
            {
                www_request.downloadHandler = new DownloadHandlerBuffer();
                yield return www_request.SendWebRequest();

                int http_statuscode = (int)www_request.responseCode;
                if (www_request.isNetworkError)
                {
                    BuglyAgent.PrintLog(LogSeverity.LogError, www_request.error);
                }

                if (cb != null) cb.Invoke(http_statuscode, www_request.downloadHandler.text);
            }
        }

        //---------------------------------------------------------------------
        IEnumerator _wwwDownload(string url, Action<WWW> cb)
        {
            WWW www = new WWW(url);

            yield return www;

            if (cb != null) cb.Invoke(www);
        }

        //---------------------------------------------------------------------
        IEnumerator _wwwDownloadList(List<string> list_url, Action<WWW[]> cb)
        {
            int n = 0;
            WWW[] arr = new WWW[list_url.Count];
            foreach (var i in list_url)
            {
                WWW www = new WWW(i);
                arr[n] = www;
                n++;
            }

            yield return arr;

            if (cb != null) cb.Invoke(arr);
        }
    }
}