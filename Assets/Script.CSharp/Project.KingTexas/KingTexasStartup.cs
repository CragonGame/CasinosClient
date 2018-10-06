// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using UnityEngine;

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

            Context = new CasinosContext(use_persistent,
                "Resources.KingTexas/Ui/",
                "Resources.KingTexasRaw/",
                "Resources.KingTexas/");

            Context.Launch();
        }

        //---------------------------------------------------------------------
        void Update()
        {
            Context.Update(Time.deltaTime);
        }

        //---------------------------------------------------------------------
        void OnDestroy()
        {
            Context.Close();
        }

        //-------------------------------------------------------------------------
        void OnApplicationPause(bool pause)
        {
            Context.OnApplicationPause(pause);
        }

        //-------------------------------------------------------------------------
        void OnApplicationFocus(bool focusStatus)
        {
            Context.OnApplicationFocus(focusStatus);
        }
    }
}