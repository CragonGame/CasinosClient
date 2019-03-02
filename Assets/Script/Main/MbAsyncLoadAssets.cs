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
        Dictionary<string, AssetBundle> MapAssetBundle { get; set; } = new Dictionary<string, AssetBundle>();

        //---------------------------------------------------------------------
        public void LocalLoadAssetBundleAsync(string url, Action<AssetBundle> cb)
        {
            //AssetBundle.LoadFromFile

            StartCoroutine(_localLoadAssetBundleAsync(url, cb));
        }

        //---------------------------------------------------------------------
        public void LocalLoadTextureFromAbAsync(string url, string name, Action<Texture> cb)
        {
            StartCoroutine(_localLoadTextureFromAbAsync(url, name, cb));
        }

        //---------------------------------------------------------------------
        public void WWWLoadTextAsync(string url, Action<string> cb)
        {
            StartCoroutine(_wwwLoadTextAsync(url, cb));
        }

        //---------------------------------------------------------------------
        public void WWWLoadTextureAsync(string url, Action<Texture> cb)
        {
            StartCoroutine(_wwwLoadTextureAsync(url, cb));
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
        IEnumerator _localLoadTextureFromAbAsync(string url, string name, Action<Texture> cb)
        {
            MapAssetBundle.TryGetValue(url, out AssetBundle ab);

            // 判定是否加载Ab
            if (ab == null)
            {
                AssetBundleCreateRequest abcr = AssetBundle.LoadFromFileAsync(url);
                yield return abcr;
                ab = abcr.assetBundle;
                MapAssetBundle[url] = ab;
            }

            AssetBundleRequest abr = ab.LoadAssetAsync<Texture2D>(name);
            yield return abr;

            if (cb != null) cb.Invoke((Texture2D)abr.asset);
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
        IEnumerator _wwwLoadTextureAsync(string url, Action<Texture> cb)
        {
            using (UnityWebRequest www_request = UnityWebRequest.Get(url))
            {
                www_request.downloadHandler = new DownloadHandlerTexture();
                yield return www_request.SendWebRequest();

                if (www_request.isHttpError || www_request.isNetworkError)
                {
                    BuglyAgent.PrintLog(LogSeverity.LogError, www_request.error);
                }
                else
                {
                    if (www_request.responseCode == 200)
                    {
                        if (cb != null) cb.Invoke(((DownloadHandlerTexture)www_request.downloadHandler).texture);
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