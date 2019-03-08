// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PathMgr
    {
        //---------------------------------------------------------------------
        public string DirRawRoot { get; set; }// PersistentData/Common/Raw/
        public string DirCsRoot { get; set; }// PersistentData/Common/Cs/
        public string DirAbRoot { get; set; }// Resources.KingTexas/，需动态计算
        public string DirAbUi { get; set; }// Resources.KingTexas/Ui/，需动态计算
        public string DirAbCard { get; set; }// Resources.KingTexas/Cards/，需动态计算
        public string DirAbAudio { get; set; }// Resources.KingTexas/Audio，需动态计算
        public string DirAbItem { get; set; }// Resources.KingTexas/Item/，需动态计算
        public string DirAbParticle { get; set; }// Resources.KingTexas/Particle/，需动态计算

        //---------------------------------------------------------------------
        public PathMgr()
        {

        }
    }
}
