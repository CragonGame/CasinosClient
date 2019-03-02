//using System;
//using System.Collections.Generic;
//using UnityEngine;

////-------------------------------------------------------------------------
//public enum _eAsyncAssetLoadType
//{
//    LocalBundle,
//    WWWBundle,
//    LocalBundleAsset,
//    WWWBundleAsset,
//    LocalRawAsset,// 目前未实现，可能只能用WWW加载
//    WWWRawAsset,
//}

////-------------------------------------------------------------------------
//public class LoaderTicket
//{
//    public string UserData;
//    public Dictionary<string, AssetBundle> MapNeedLoadAssetBundle;
//    public Dictionary<string, AssetBundle> MapLoadedAssetBundle;
//}

//public class AsyncAssetLoaderMgr
//{
//    //-------------------------------------------------------------------------  
//    public WWWAsyncLoader WWWAsyncLoader { get; private set; }
//    public WWWBundleAsyncLoader WWWBundleAsyncLoader { get; private set; }
//    public WWWAssetAsyncLoader WWWAssetAsyncLoader { get; private set; }
//    public LocalBundleAsyncLoader LocalBundleAsyncLoader { get; private set; }
//    public LocalAssetAsyncLoader LocalAssetAsyncLoader { get; private set; }

//    //-------------------------------------------------------------------------
//    public AsyncAssetLoaderMgr()
//    {
//        WWWAsyncLoader = new WWWAsyncLoader(this);
//        WWWBundleAsyncLoader = new WWWBundleAsyncLoader(this);
//        WWWAssetAsyncLoader = new WWWAssetAsyncLoader(this);
//        LocalBundleAsyncLoader = new LocalBundleAsyncLoader(this);
//        LocalAssetAsyncLoader = new LocalAssetAsyncLoader(this);
//    }

//    //-------------------------------------------------------------------------
//    public AsyncAssetLoadGroup CreateAsyncAssetLoadGroup()
//    {
//        return new AsyncAssetLoadGroup(this);
//    }

//    //-------------------------------------------------------------------------
//    public void Update(float time)
//    {
//        WWWAsyncLoader.update(time);
//        LocalBundleAsyncLoader.update(time);
//    }

//    //-------------------------------------------------------------------------
//    public void Close()
//    {
//        WWWAsyncLoader.Close();
//        WWWBundleAsyncLoader.Close();
//        WWWAssetAsyncLoader.Close();
//        LocalBundleAsyncLoader.Close();
//        LocalAssetAsyncLoader.Close();
//    }
//}