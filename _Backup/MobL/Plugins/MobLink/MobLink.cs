using UnityEngine;
using System;
using System.Collections;

namespace com.moblink.unity3d
{
    public class MobLink : MonoBehaviour
    {

        public delegate void GetMobIdHandler(string mobId);
        public delegate void RestoreSceneHandler(MobLinkScene scene);

        private static event GetMobIdHandler onGetMobId;
        private static event RestoreSceneHandler onRestoreScene;

        private static bool isInit;
        private static MobLink _instance;
        private static MobLinkImpl moblinkUtils;

        void Awake()
        {
            if (!isInit)
            {
#if UNITY_ANDROID
                moblinkUtils = new AndroidMobLinkImpl();
#elif UNITY_IPHONE
				moblinkUtils = new iOSMobLinkImpl();
#endif
                isInit = true;
            }

            if (_instance != null)
            {
                DestroyObject(_instance.gameObject);
            }
            _instance = this;

            DontDestroyOnLoad(this.gameObject);
        }

        public static void setRestoreSceneListener(com.moblink.unity3d.MobLink.RestoreSceneHandler sceneHander)
        {
            moblinkUtils.setRestoreSceneListener();
            onRestoreScene += sceneHander;
        }

        public static void getMobId(MobLinkScene scene, GetMobIdHandler modIdHandler)
        {
            onGetMobId += modIdHandler;
            moblinkUtils.GetMobId(scene);
        }

        private void _MobIdCallback(string data)
        {
            // 解析出mobId
            Hashtable json = (Hashtable)MiniJSON.jsonDecode(data);
            string modId = json["mobid"].ToString();

            onGetMobId(modId);
            onGetMobId = null;
        }

        private void _RestoreCallBack(string data)
        {            
            Hashtable res = (Hashtable)MiniJSON.jsonDecode(data);
            if (res == null || res.Count <= 0)
            {         
                return;
            }

            string path = string.Empty;
            var p = res["path"];
            if (p != null)
            {
                path = p.ToString();
            }
            string source = string.Empty;
            var s = res["source"];
            if (s != null)
            {
                source = s.ToString();
            }
            Hashtable customParams = null;
            var c_p = res["params"];
            if (c_p != null)
            {
                customParams = (Hashtable)c_p;
            }            
            MobLinkScene scene = new MobLinkScene(path, source, customParams);
            onRestoreScene(scene);
        }
    }

}


