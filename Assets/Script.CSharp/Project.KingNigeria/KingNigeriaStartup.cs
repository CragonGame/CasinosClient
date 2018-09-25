// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
#if UNITY_EDITOR || KINGTEXAS_VIEW
    using UnityEngine;

    public class KingNigeriaStartup : MonoBehaviour
    {
        //---------------------------------------------------------------------
        void Awake()
        {
            //KingNigeriaCasinosListener listener = new KingNigeriaCasinosListener();

            //var casinos_context = new CasinosContext(listener,
            //    "KingNigeriaListener",
            //    "Resources.KingNigeria/Ui/",
            //    "Resources.KingNigeriaRaw/",
            //    "Resources.KingNigeria/",
            //    "Texas");

            //casinos_context.regLuaFilePath(
            //    "Resources.KingNigeriaRaw/PreData/",
            //    "Launch",
            //    "PreViewMgr",
            //    "PreViewFactory",
            //    "PreViewBase",
            //    "PreViewLoading",
            //    "PreViewMsgBox");
        }

        //---------------------------------------------------------------------
        void Update()
        {
            var context = CasinosContext.Instance;
            if (context == null || context.Pause)
            {
                return;
            }

            context.Update(Time.deltaTime);
        }

        //---------------------------------------------------------------------
        void OnDestroy()
        {
            CasinosContext.Instance.Close();
        }

        //-------------------------------------------------------------------------
        void OnApplicationPause(bool pause)
        {
            var context = CasinosContext.Instance;

#if UNITY_EDITOR
            if (context == null) return;
            context.Pause = pause;
#endif

            //if (context.MainCLua != null && context.ProjectListener != null)
            //{
            //    if (context.ActionOnApplicationPause != null)
            //    {
            //        context.ActionOnApplicationPause(pause);
            //    }
            //}
        }

        //-------------------------------------------------------------------------
        void OnApplicationFocus(bool focusStatus)
        {
        }
    }
#endif
}