//using System;
//using System.Collections.Generic;
//using UnityEngine;

//public class AsyncAssetLoadGroup
//{
//    //-------------------------------------------------------------------------
//    public bool IsCancel { get; private set; }
//    AsyncAssetLoaderMgr AsyncAssetLoaderMgr { get; set; }
//    Dictionary<string, AssetBundle> MapAssetBundle { get; set; }
//    Dictionary<string, Action<string, AssetBundle>> MapLoadAssetBundleCallBack { get; set; }
//    Dictionary<LoaderTicket, Action<List<AssetBundle>>> MapLoaderTicket { get; set; }
//    List<LoaderTicket> ListLoaderAssetBundlesDone { get; set; }

//    //-------------------------------------------------------------------------
//    public AsyncAssetLoadGroup(AsyncAssetLoaderMgr mgr)
//    {
//        IsCancel = false;
//        AsyncAssetLoaderMgr = mgr;
//        MapAssetBundle = new Dictionary<string, AssetBundle>();
//        MapLoadAssetBundleCallBack = new Dictionary<string, Action<string, AssetBundle>>();
//        MapLoaderTicket = new Dictionary<LoaderTicket, Action<List<AssetBundle>>>();
//        ListLoaderAssetBundlesDone = new List<LoaderTicket>();
//    }

//    //-------------------------------------------------------------------------
//    public void asyncLoadBundle(string bundle_path, _eAsyncAssetLoadType loader_type, Action<string, AssetBundle> loaded_action)
//    {
//        AssetBundle asset_bundle = null;
//        if (MapAssetBundle.TryGetValue(bundle_path, out asset_bundle))
//        {
//            loaded_action(bundle_path, asset_bundle);
//            return;
//        }
//        else
//        {
//            Action<string, AssetBundle> action = null;
//            if (!MapLoadAssetBundleCallBack.TryGetValue(bundle_path, out action))
//            {
//                action = loaded_action;
//            }
//            else
//            {
//                action += loaded_action;
//            }
//        }

//        if (loader_type == _eAsyncAssetLoadType.LocalBundle)
//        {
//            AsyncAssetLoaderMgr.LocalBundleAsyncLoader.getAssetBundle(bundle_path, _loadAssetBundleCallBack);
//        }
//        else if (loader_type == _eAsyncAssetLoadType.WWWBundle)
//        {
//            AsyncAssetLoaderMgr.WWWBundleAsyncLoader.getAssetBundle(bundle_path, _loadAssetBundleCallBack);
//        }

//        //return null;
//    }

//    //-------------------------------------------------------------------------
//    public LoaderTicket asyncLoadAsset(string bundle_path, string asset_name, _eAsyncAssetLoadType loader_type,
//        Action<LoaderTicket, string, UnityEngine.Object> loaded_action)
//    {
//        LoaderTicket tick = null;
//        if (loader_type == _eAsyncAssetLoadType.LocalBundleAsset || loader_type == _eAsyncAssetLoadType.LocalRawAsset)
//        {
//            tick = AsyncAssetLoaderMgr.LocalAssetAsyncLoader.getAsset(bundle_path, asset_name, loader_type, loaded_action);
//        }
//        else if (loader_type == _eAsyncAssetLoadType.WWWBundleAsset || loader_type == _eAsyncAssetLoadType.WWWRawAsset)
//        {
//            tick = AsyncAssetLoaderMgr.WWWAssetAsyncLoader.getAsset(bundle_path, asset_name, loader_type, loaded_action);
//        }

//        return tick;
//    }

//    //-------------------------------------------------------------------------
//    // 异步加载WWW下载回来的资源（新）
//    public LoaderTicket LoadWWWAsync(string url, Action<string, UnityEngine.Networking.UnityWebRequest> loaded_action)
//    {
//        LoaderTicket tick = AsyncAssetLoaderMgr.WWWAssetAsyncLoader.LoadAsync(url, loaded_action);
//        return tick;
//    }

//    //-------------------------------------------------------------------------
//    public LoaderTicket asyncLoadLocalBundle(List<string> list_bundle_path, _eAsyncAssetLoadType loader_type,
//       Action<List<AssetBundle>> loaded_action)
//    {
//        LoaderTicket loader_ticket = new LoaderTicket();
//        Dictionary<string, AssetBundle> map_needloadab = new Dictionary<string, AssetBundle>();
//        loader_ticket.MapNeedLoadAssetBundle = map_needloadab;
//        Dictionary<string, AssetBundle> map_loadedab = new Dictionary<string, AssetBundle>();
//        loader_ticket.MapLoadedAssetBundle = map_loadedab;
//        foreach (var i in list_bundle_path)
//        {
//            AssetBundle asset_bundle = null;
//            if (MapAssetBundle.TryGetValue(i, out asset_bundle))
//            {
//                map_loadedab[i] = asset_bundle;
//            }
//            else
//            {
//                map_needloadab[i] = null;
//                if (loader_type == _eAsyncAssetLoadType.LocalBundle)
//                {
//                    AsyncAssetLoaderMgr.LocalBundleAsyncLoader.getAssetBundle(i, _loadAssetBundleCallBack);
//                }
//                else if (loader_type == _eAsyncAssetLoadType.WWWBundle)
//                {
//                    AsyncAssetLoaderMgr.WWWBundleAsyncLoader.getAssetBundle(i, _loadAssetBundleCallBack);
//                }
//            }
//        }

//        if (loader_ticket.MapLoadedAssetBundle.Count == list_bundle_path.Count)
//        {
//            List<AssetBundle> list_bundle = new List<AssetBundle>();
//            foreach (var i in loader_ticket.MapLoadedAssetBundle)
//            {
//                list_bundle.Add(i.Value);
//            }
//            loaded_action(list_bundle);
//        }
//        else
//        {
//            MapLoaderTicket[loader_ticket] = loaded_action;
//        }

//        return loader_ticket;
//    }

//    //-------------------------------------------------------------------------
//    public void releaseAssetBundle()
//    {
//        foreach (var i in MapAssetBundle)
//        {
//            if (i.Value != null)
//            {
//                i.Value.Unload(false);
//            }
//        }

//        MapAssetBundle.Clear();
//    }

//    //-------------------------------------------------------------------------
//    public void destroy()
//    {
//        releaseAssetBundle();
//        MapLoadAssetBundleCallBack.Clear();
//        MapLoaderTicket.Clear();
//        ListLoaderAssetBundlesDone.Clear();
//        IsCancel = true;
//    }

//    //-------------------------------------------------------------------------
//    void _loadAssetBundleCallBack(string bundle_path, AssetBundle asset_bundle)
//    {
//        MapAssetBundle[bundle_path] = asset_bundle;
//        Action<string, AssetBundle> action = null;
//        if (MapLoadAssetBundleCallBack.TryGetValue(bundle_path, out action))
//        {
//            action(bundle_path, asset_bundle);
//            MapLoadAssetBundleCallBack.Remove(bundle_path);
//        }

//        foreach (var i in MapLoaderTicket)
//        {
//            Dictionary<string, AssetBundle> map_needloadab = i.Key.MapNeedLoadAssetBundle;
//            Dictionary<string, AssetBundle> map_loadedab = i.Key.MapLoadedAssetBundle;
//            if (map_needloadab.ContainsKey(bundle_path))
//            {
//                map_needloadab.Remove(bundle_path);
//                map_loadedab[bundle_path] = asset_bundle;
//                if (map_needloadab.Count <= 0)
//                {
//                    List<AssetBundle> list_bundle = new List<AssetBundle>();
//                    foreach (var ab in map_loadedab)
//                    {
//                        list_bundle.Add(ab.Value);
//                    }
//                    i.Value(list_bundle);
//                    ListLoaderAssetBundlesDone.Add(i.Key);
//                }
//            }
//        }

//        foreach (var i in ListLoaderAssetBundlesDone)
//        {
//            MapLoaderTicket.Remove(i);
//        }

//        ListLoaderAssetBundlesDone.Clear();
//    }
//}