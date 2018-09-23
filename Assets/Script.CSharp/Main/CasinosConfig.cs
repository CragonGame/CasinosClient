// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using UnityEngine;

    public class CasinosConfig
    {
        //---------------------------------------------------------------------
        public string Platform { get; private set; }// Android, iOS, PC
        public string VersionBundle { get; private set; }

        //---------------------------------------------------------------------
        public CasinosConfig(_eEditorRunSourcePlatform editor_mode_runsources_platform)
        {
            switch (editor_mode_runsources_platform)
            {
                case _eEditorRunSourcePlatform.Android:
                    Platform = "ANDROID";
                    break;
                case _eEditorRunSourcePlatform.IOS:
                    Platform = "IOS";
                    break;
                case _eEditorRunSourcePlatform.PC:
                    Platform = "PC";
                    break;
                default:
                    break;
            }

            VersionBundle = Application.version;
        }
    }
}