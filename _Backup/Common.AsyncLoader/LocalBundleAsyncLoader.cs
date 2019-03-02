//using System;
//using System.Collections.Generic;
//using UnityEngine;

//public class LocalBundleAsyncLoader
//{
//    //-------------------------------------------------------------------------
//    AsyncAssetLoaderMgr AsyncAssetLoaderMgr { get; set; }
//    Dictionary<string, AssetBundle> MapAssetBundle { get; set; }
//    Dictionary<string, AssetBundleCreateRequest> MapAssetBundleCreateRequest { get; set; }
//    Dictionary<string, Action<string, AssetBundle>> MapLoaderTicketAndCallBack { get; set; }

//    //-------------------------------------------------------------------------
//    public LocalBundleAsyncLoader(AsyncAssetLoaderMgr mgr)
//    {
//        AsyncAssetLoaderMgr = mgr;
//        MapAssetBundle = new Dictionary<string, AssetBundle>();
//        MapAssetBundleCreateRequest = new Dictionary<string, AssetBundleCreateRequest>();
//        MapLoaderTicketAndCallBack = new Dictionary<string, Action<string, AssetBundle>>();
//    }

//    //-------------------------------------------------------------------------
//    public void Close()
//    {
//        MapAssetBundle.Clear();
//        MapAssetBundleCreateRequest.Clear();
//        MapLoaderTicketAndCallBack.Clear();
//    }

//    //-------------------------------------------------------------------------
//    public void update(float tm)
//    {
//        foreach (var i in MapAssetBundleCreateRequest)
//        {
//            AssetBundleCreateRequest assetbundle_createrequest = i.Value;
//            if (assetbundle_createrequest != null)
//            {
//                if (assetbundle_createrequest.isDone)
//                {
//                    MapAssetBundle[i.Key] = i.Value.assetBundle;
//                }
//            }
//        }

//        foreach (var i in MapAssetBundle)
//        {
//            MapAssetBundleCreateRequest.Remove(i.Key);

//            Action<string, AssetBundle> ticketandcallback = null;
//            if (MapLoaderTicketAndCallBack.TryGetValue(i.Key, out ticketandcallback))
//            {
//                ticketandcallback(i.Key, i.Value);
//                MapLoaderTicketAndCallBack.Remove(i.Key);
//            }
//        }
//        MapAssetBundle.Clear();
//    }

//    //-------------------------------------------------------------------------
//    public LoaderTicket getAssetBundle(string bundle_path, Action<string, AssetBundle> asset_bundle_callback)
//    {
//        LoaderTicket loader_ticket = new LoaderTicket();
//        AssetBundleCreateRequest assetbundle_createrequest = null;
//        if (!MapAssetBundleCreateRequest.TryGetValue(bundle_path, out assetbundle_createrequest))
//        {
//            assetbundle_createrequest = AssetBundle.LoadFromFileAsync(bundle_path);
//            MapAssetBundleCreateRequest[bundle_path] = assetbundle_createrequest;
//        }

//        Action<string, AssetBundle> ticketandcallback = null;
//        if (!MapLoaderTicketAndCallBack.TryGetValue(bundle_path, out ticketandcallback))
//        {
//            ticketandcallback = asset_bundle_callback;
//            MapLoaderTicketAndCallBack[bundle_path] = ticketandcallback;
//        }
//        else
//        {
//            ticketandcallback += asset_bundle_callback;
//        }

//        return loader_ticket;
//    }
//}