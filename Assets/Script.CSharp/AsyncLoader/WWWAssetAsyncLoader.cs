using System;
using System.Collections.Generic;
using UnityEngine;

public class WWWAssetAsyncLoader
{
    //-------------------------------------------------------------------------
    AsyncAssetLoaderMgr AsyncAssetLoaderMgr { get; set; }
    Dictionary<string, AssetBundle> MapBundle { get; set; }
    Dictionary<string, UnityEngine.Object> MapBundleAsset { get; set; }
    Dictionary<string, Dictionary<LoaderTicket, Action<LoaderTicket, string, UnityEngine.Object>>> MapLoaderTicketAndCallBack { get; set; }

    //-------------------------------------------------------------------------
    public WWWAssetAsyncLoader(AsyncAssetLoaderMgr mgr)
    {
        AsyncAssetLoaderMgr = mgr;
        MapBundle = new Dictionary<string, AssetBundle>();
        MapBundleAsset = new Dictionary<string, UnityEngine.Object>();
        MapLoaderTicketAndCallBack = new Dictionary<string, Dictionary<LoaderTicket, Action<LoaderTicket, string, UnityEngine.Object>>>();
    }

    //-------------------------------------------------------------------------
    public LoaderTicket LoadAsync(string url, Action<string, WWW> loaded_action)
    {
        var loader_ticket = AsyncAssetLoaderMgr.WWWAsyncLoader.getIsDoneWWW(url, loaded_action);
        return loader_ticket;
    }

    //-------------------------------------------------------------------------
    public LoaderTicket getAsset(string asset_path, string asset_name, _eAsyncAssetLoadType loader_type,
        Action<LoaderTicket, string, UnityEngine.Object> bundle_asset_callback)
    {
        LoaderTicket loader_ticket = null;
        UnityEngine.Object bundle_asset = null;
        string asset_key = asset_path + asset_name;
        if (MapBundleAsset.TryGetValue(asset_key, out bundle_asset))
        {
            bundle_asset_callback(loader_ticket, asset_key, bundle_asset);
        }
        else
        {
            if (loader_type == _eAsyncAssetLoadType.WWWBundleAsset)
            {
                if (MapBundle.ContainsKey(asset_path))
                {
                    var asset_bundle = MapBundle[asset_path];
                    bundle_asset = asset_bundle.LoadAsset(asset_name);
                    bundle_asset_callback(loader_ticket, asset_key, bundle_asset);
                }
                else
                {
                    loader_ticket = AsyncAssetLoaderMgr.WWWBundleAsyncLoader.getAssetBundle(asset_path, _loadAssetBundleCallBack);
                }
            }
            else
            {
                loader_ticket = AsyncAssetLoaderMgr.WWWAsyncLoader.getIsDoneWWW(asset_path, _loadAssetWWWCallBack);
            }

            if (loader_ticket != null)
            {
                loader_ticket.UserData = asset_name;
                Dictionary<LoaderTicket, Action<LoaderTicket, string, UnityEngine.Object>> map_ticketandcallback = null;
                if (!MapLoaderTicketAndCallBack.TryGetValue(asset_path, out map_ticketandcallback))
                {
                    map_ticketandcallback = new Dictionary<LoaderTicket, Action<LoaderTicket, string, UnityEngine.Object>>();
                    MapLoaderTicketAndCallBack[asset_path] = map_ticketandcallback;
                }
                map_ticketandcallback[loader_ticket] = bundle_asset_callback;
            }
        }

        return loader_ticket;
    }

    //-------------------------------------------------------------------------
    void _loadAssetBundleCallBack(string asset_path, AssetBundle asset_bundle)
    {
        if (asset_bundle != null)
        {
            MapBundle[asset_path] = asset_bundle;
            Dictionary<LoaderTicket, Action<LoaderTicket, string, UnityEngine.Object>> map_ticketandcallback = null;
            if (MapLoaderTicketAndCallBack.TryGetValue(asset_path, out map_ticketandcallback))
            {
                foreach (var ticket in map_ticketandcallback)
                {
                    string asset_name = ticket.Key.UserData;
                    string asset_key = asset_path + asset_name;
                    var asset = asset_bundle.LoadAsset(asset_name);
                    if (!MapBundleAsset.ContainsKey(asset_key))
                    {
                        MapBundleAsset[asset_key] = asset;
                    }

                    ticket.Value(ticket.Key, asset_key, asset);
                }

                map_ticketandcallback.Clear();
                MapLoaderTicketAndCallBack.Remove(asset_path);
            }
        }
    }

    //-------------------------------------------------------------------------
    void _loadAssetWWWCallBack(string asset_path, WWW www)
    {
        if (www != null && www.isDone)
        {
            Dictionary<LoaderTicket, Action<LoaderTicket, string, UnityEngine.Object>> map_ticketandcallback = null;
            if (MapLoaderTicketAndCallBack.TryGetValue(asset_path, out map_ticketandcallback))
            {
                foreach (var ticket in map_ticketandcallback)
                {
                    string asset_name = ticket.Key.UserData;
                    string asset_key = asset_path + asset_name;
                    UnityEngine.Object load_asset = null;
                    if (!MapBundleAsset.ContainsKey(asset_key))
                    {
                        if (string.IsNullOrEmpty(www.error))
                        {
                            if (www.texture != null)
                            {
                                load_asset = www.texture;
                            }
                            else if (www.GetAudioClip() != null)
                            {
                                load_asset = www.GetAudioClip();
                            }
                            else if (www.assetBundle != null)
                            {
                                load_asset = www.assetBundle;
                            }
                            MapBundleAsset[asset_key] = load_asset;
                        }
                    }

                    MapBundleAsset.TryGetValue(asset_key, out load_asset);
                    ticket.Value(ticket.Key, asset_key, load_asset);
                }

                map_ticketandcallback.Clear();
                MapLoaderTicketAndCallBack.Remove(asset_path);
            }
        }
    }
}