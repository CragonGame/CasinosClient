//using System;
//using System.Collections.Generic;
//using UnityEngine;

//public class WWWBundleAsyncLoader
//{
//    //-------------------------------------------------------------------------
//    AsyncAssetLoaderMgr AsyncAssetLoaderMgr { get; set; }
//    Dictionary<string, Action<string, AssetBundle>> MapLoaderTicketAndCallBack { get; set; }

//    //-------------------------------------------------------------------------
//    public WWWBundleAsyncLoader(AsyncAssetLoaderMgr mgr)
//    {
//        AsyncAssetLoaderMgr = mgr;
//        MapLoaderTicketAndCallBack = new Dictionary<string, Action<string, AssetBundle>>();
//    }

//    //-------------------------------------------------------------------------
//    public void Close()
//    {
//        MapLoaderTicketAndCallBack.Clear();
//    }

//    //-------------------------------------------------------------------------
//    public LoaderTicket getAssetBundle(string asset_path, Action<string, AssetBundle> asset_bundle_callback)
//    {
//        LoaderTicket loader_ticket = AsyncAssetLoaderMgr.WWWAsyncLoader.getIsDoneWWW(asset_path, _loadAssetBundleWWWCallBack);

//        Action<string, AssetBundle> ticketandcallback = null;
//        if (!MapLoaderTicketAndCallBack.TryGetValue(asset_path, out ticketandcallback))
//        {
//            ticketandcallback = asset_bundle_callback;
//            MapLoaderTicketAndCallBack[asset_path] = ticketandcallback;
//        }
//        else
//        {
//            ticketandcallback += asset_bundle_callback;
//        }

//        return loader_ticket;
//    }

//    //-------------------------------------------------------------------------
//    void _loadAssetBundleWWWCallBack(string asset_path, UnityEngine.Networking.UnityWebRequest www)
//    {
//        if (www != null && www.isDone)
//        {
//            Action<string, AssetBundle> ticketandcallback = null;
//            if (MapLoaderTicketAndCallBack.TryGetValue(asset_path, out ticketandcallback))
//            {
//                //ticketandcallback(asset_path, www.assetBundle);
//            }
//        }
//    }
//}