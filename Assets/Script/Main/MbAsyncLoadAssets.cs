// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Networking;

    public class MbAsyncLoadAssets : MonoBehaviour
    {
        //---------------------------------------------------------------------
        public void LocalLoadAssetBundleAsync(string url, Action<AssetBundle> cb)
        {
            StartCoroutine(_localLoadAssetBundleAsync(url, cb));
        }

        //---------------------------------------------------------------------
        public void WWWLoadTextAsync(string url, Action<string> cb)
        {
            StartCoroutine(_wwwLoadTextAsync(url, cb));
        }

        //---------------------------------------------------------------------
        public void WWWLoadAssetBundleAsync(string url, Action<AssetBundle> cb)
        {
            StartCoroutine(_wwwLoadAssetBundleAsync(url, cb));
        }

        //---------------------------------------------------------------------
        IEnumerator _localLoadAssetBundleAsync(string url, Action<AssetBundle> cb)
        {
            using (UnityWebRequest www_request = UnityWebRequest.Get(url))
            {
                yield return www_request.SendWebRequest();

                if (www_request.isHttpError || www_request.isNetworkError)
                {
                    BuglyAgent.PrintLog(LogSeverity.LogError, www_request.error);
                }
                else
                {
                    if (www_request.responseCode == 200)
                    {
                        AssetBundle ab = DownloadHandlerAssetBundle.GetContent(www_request);
                        if (cb != null) cb.Invoke(ab);
                    }
                }
            }
        }

        //---------------------------------------------------------------------
        IEnumerator _wwwLoadTextAsync(string url, Action<string> cb)
        {
            using (UnityWebRequest www_request = UnityWebRequest.Get(url))
            {
                yield return www_request.SendWebRequest();

                if (www_request.isHttpError || www_request.isNetworkError)
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
        IEnumerator _wwwLoadAssetBundleAsync(string url, Action<AssetBundle> cb)
        {
            using (UnityWebRequest www_request = UnityWebRequest.Get(url))
            {
                yield return www_request.SendWebRequest();

                if (www_request.isHttpError || www_request.isNetworkError)
                {
                    BuglyAgent.PrintLog(LogSeverity.LogError, www_request.error);
                }
                else
                {
                    if (www_request.responseCode == 200)
                    {
                        AssetBundle ab = DownloadHandlerAssetBundle.GetContent(www_request);
                        if (cb != null) cb.Invoke(ab);
                    }
                }
            }
        }
    }
}

//---------------------------------------------------------------------
//IEnumerator _wwwDownloadList(List<string> list_url, Action<WWW[]> cb)
//{
//    int n = 0;
//    WWW[] arr = new WWW[list_url.Count];
//    foreach (var i in list_url)
//    {
//        WWW www = new WWW(i);
//        arr[n] = www;
//        n++;
//    }
//    yield return arr;
//    if (cb != null) cb.Invoke(arr);
//}