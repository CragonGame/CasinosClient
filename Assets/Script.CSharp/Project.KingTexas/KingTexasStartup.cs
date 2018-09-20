// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
#if UNITY_EDITOR || KINGTEXAS_VIEW
    using UnityEngine;

    public class KingTexasStartup : MonoBehaviour
    {
        //---------------------------------------------------------------------
        void Awake()
        {
            bool use_persistent = true;
#if UNITY_EDITOR
            CasinosEditorConfig casinos_editor_cfg = new CasinosEditorConfig();
            use_persistent = casinos_editor_cfg.CfgUserSettings.UseTmpDirRes;
#endif

            KingTexasCasinosListener listener = new KingTexasCasinosListener();

            var casinos_context = new CasinosContext(listener,
                use_persistent,
                "KingTexasListener",
                "Resources.KingTexas/Ui/",
                "Resources.KingTexasRaw/",
                "Resources.KingTexas/",
                "Texas");

            casinos_context.RegLuaFilePath(
                "Launch/",
                "Launch",
                "PreViewMgr",
                "PreViewFactory",
                "PreViewBase",
                "PreViewLoading",
                "PreViewMsgBox");
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

            if (context.MainCLua != null && context.ProjectListener != null)
            {
                context.ActionOnApplicationPause(pause);
            }
        }

        //-------------------------------------------------------------------------
        void OnApplicationFocus(bool focusStatus)
        {
        }
    }
#endif
}