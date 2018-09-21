// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    public class CasinosConfig
    {
        //---------------------------------------------------------------------
        public string Platform { get; private set; }// Android, iOS, PC

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
        }
    }
}