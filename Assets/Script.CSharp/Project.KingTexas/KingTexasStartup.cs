// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
#if UNITY_EDITOR || KINGTEXAS_VIEW
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Networking;

    public class KingTexasStartup : MonoBehaviour
    {
        //---------------------------------------------------------------------
        CasinosContext Context { get; set; }

        //---------------------------------------------------------------------
        void Awake()
        {
            bool use_persistent = true;
#if UNITY_EDITOR
            CasinosEditorConfig casinos_editor_cfg = new CasinosEditorConfig();
            use_persistent = casinos_editor_cfg.CfgUserSettings.UseTmpDirRes;
#endif

            //KingTexasCasinosListener listener = new KingTexasCasinosListener();

            Context = new CasinosContext(use_persistent,
                "KingTexasListener",
                "Resources.KingTexas/Ui/",
                "Resources.KingTexasRaw/",
                "Resources.KingTexas/",
                "Texas");

            Context.Launch();
        }

        //---------------------------------------------------------------------
        void Update()
        {
            if (!Context.Pause)
            {
                Context.Update(Time.deltaTime);
            }
        }

        //---------------------------------------------------------------------
        void OnDestroy()
        {
            Context.Close();
        }

        //-------------------------------------------------------------------------
        void OnApplicationPause(bool pause)
        {
#if UNITY_EDITOR
            Context.Pause = pause;
#endif

            //Context.ActionOnApplicationPause?.Invoke(pause);
        }

        //-------------------------------------------------------------------------
        void OnApplicationFocus(bool focusStatus)
        {
        }
#endif
    }
}