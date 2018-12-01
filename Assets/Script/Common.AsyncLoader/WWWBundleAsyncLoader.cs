using System;
using System.Collections.Generic;
using UnityEngine;

public class WWWBundleAsyncLoader
{
    //-------------------------------------------------------------------------
    AsyncAssetLoaderMgr AsyncAssetLoaderMgr { get; set; }
    Dictionary<string, Action<string, AssetBundle>> MapLoaderTicketAndCallBack { get; set; }

    //Dictionary<string, AssetBundle> MapAssetBundle { get; set; }
    //List<LoaderTicket> ListCallBackDoneTicket { get; set; }

    //-------------------------------------------------------------------------
    public WWWBundleAsyncLoader(AsyncAssetLoaderMgr mgr)
    {
        AsyncAssetLoaderMgr = mgr;
        MapLoaderTicketAndCallBack = new Dictionary<string, Action<string, AssetBundle>>();

        //MapAssetBundle = new Dictionary<string, AssetBundle>();
        //ListCallBackDoneTicket = new List<LoaderTicket>();
    }

    //-------------------------------------------------------------------------
    public void Close()
    {
        MapLoaderTicketAndCallBack.Clear();
    }

    //-------------------------------------------------------------------------
    public LoaderTicket getAssetBundle(string asset_path, Action<string, AssetBundle> asset_bundle_callback)
    {
        LoaderTicket loader_ticket = AsyncAssetLoaderMgr.WWWAsyncLoader.getIsDoneWWW(asset_path, _loadAssetBundleWWWCallBack);

        Action<string, AssetBundle> ticketandcallback = null;
        if (!MapLoaderTicketAndCallBack.TryGetValue(asset_path, out ticketandcallback))
        {
            ticketandcallback = asset_bundle_callback;
            MapLoaderTicketAndCallBack[asset_path] = ticketandcallback;
        }
        else
        {
            ticketandcallback += asset_bundle_callback;
        }

        return loader_ticket;
    }

    ////-------------------------------------------------------------------------
    //public void releaseAssetBundle(LoaderTicket loader_ticket, string asset_path)
    //{
    //    Action<string, AssetBundle> ticketandcallback = null;
    //    if (MapLoaderTicketAndCallBack.TryGetValue(asset_path, out ticketandcallback))
    //    {
    //        map_ticketandcallback.Remove(loader_ticket);
    //        if (map_ticketandcallback.Count == 0)
    //        {
    //            AssetBundle asset_bundle = null;
    //            if (MapAssetBundle.TryGetValue(asset_path, out asset_bundle))
    //            {
    //                MapAssetBundle.Remove(asset_path);
    //                asset_bundle.Unload(false);
    //            }
    //        }
    //    }
    //}

    //-------------------------------------------------------------------------
    void _loadAssetBundleWWWCallBack(string asset_path, WWW www)
    {
        if (www != null && www.isDone)
        {
            //MapAssetBundle[asset_path] = www.assetBundle;

            Action<string, AssetBundle> ticketandcallback = null;
            if (MapLoaderTicketAndCallBack.TryGetValue(asset_path, out ticketandcallback))
            {
                ticketandcallback(asset_path, www.assetBundle);

                //foreach (var ticket in map_ticketandcallback)
                //{
                //    ticket.Value(asset_path, www.assetBundle);
                //    //ListCallBackDoneTicket.Add(ticket.Key);
                //}

                //foreach (var callback_done_ticket in ListCallBackDoneTicket)
                //{
                //    map_ticketandcallback[callback_done_ticket] = null;
                //}
                //ListCallBackDoneTicket.Clear();
            }
        }
    }
}