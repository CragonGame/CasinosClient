// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using UnityEngine;

    public class PathMgr
    {
        //---------------------------------------------------------------------
        public string DirRawRoot { get; set; }// PersistentData/Common/Raw/
        public string DirCsRoot { get; set; }// PersistentData/Common/Cs/
        public string DirAbRoot { get; set; }// Resources.KingTexas/，需动态计算
        public string DirAbLaunch { get; set; }// Resources.KingTexasLaunch/，需动态计算
        public string DirAbUi { get; set; }// Resources.KingTexas/Ui/，需动态计算
        public string DirAbCard { get; set; }// Resources.KingTexas/Cards/，需动态计算
        public string DirAbAudio { get; set; }// Resources.KingTexas/Audio，需动态计算
        public string DirAbItem { get; set; }// Resources.KingTexas/Item/，需动态计算
        public string DirAbParticle { get; set; }// Resources.KingTexas/Particle/，需动态计算
        StringBuilder Sb { get; set; }

        //---------------------------------------------------------------------
        public PathMgr()
        {
            Sb = Context.Instance.Sb;
            var cfg = Context.Instance.Config;

            if (cfg.IsEditorDebug)
            {
                string p = Path.Combine(System.Environment.CurrentDirectory, "./DataOss/");
                var di = new DirectoryInfo(p);
                string p1 = di.FullName.Replace('\\', '/');
                p1 += cfg.Platform;

                DirAbRoot = p1 + "/Resources.KingTexas/";
                DirAbLaunch = p1 + "/Resources.KingTexasLaunch/";
            }
            else
            {
                Sb.Clear();
                Sb.Append(Application.persistentDataPath);
                Sb.Append("/");
                Sb.Append(cfg.Platform);
                Sb.Append("/Resources.KingTexas/");
                DirAbRoot = Sb.ToString();

                Sb.Clear();
                Sb.Append(Application.persistentDataPath);
                Sb.Append("/");
                Sb.Append(cfg.Platform);
                Sb.Append("/Resources.KingTexasLaunch/");
                DirAbLaunch = Sb.ToString();
            }

            Sb.Clear();
            Sb.Append(Application.persistentDataPath);
            Sb.Append("/Common/Raw/");
            DirRawRoot = Sb.ToString();

            Sb.Clear();
            Sb.Append(Application.persistentDataPath);
            Sb.Append("/Common/Cs/");
            DirCsRoot = Sb.ToString();

            DirAbUi = DirAbRoot + "Ui/";
            DirAbCard = DirAbRoot + "Cards/";
            DirAbAudio = DirAbRoot + "Audio/";
            DirAbItem = DirAbRoot + "Item/";
            DirAbParticle = DirAbRoot + "Particle/";

            //Debug.Log("DirAbRoot=" + DirAbRoot);
            //Debug.Log("DirAbLaunch=" + DirAbLaunch);
            //Debug.Log("DirRawRoot=" + DirRawRoot);
            //Debug.Log("DirCsRoot=" + DirCsRoot);
        }
    }
}
