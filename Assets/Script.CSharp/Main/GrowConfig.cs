// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using UnityEngine;
    using System;
    using System.Collections;
    using GameCloud.Unity.Common;

    public class GrowConfig
    {
        public float GrowTotalTm { get; set; }
        public float GrowOneTm { get; set; }
        public long GrowGoldPerLevelPerTime { get; set; }

        //-------------------------------------------------------------------------
        public GrowConfig()
        {
            //GrowTotalTm = float.Parse(CasinosContext.Instance.GetCommonValue(_eTbDataCommonKey.GrowTotalTm));
            //GrowOneTm = float.Parse(CasinosContext.Instance.GetCommonValue(_eTbDataCommonKey.GrowOneTm));
            //GrowGoldPerLevelPerTime = long.Parse(CasinosContext.Instance.GetCommonValue(_eTbDataCommonKey.GrowGoldPerLevelPerTime));            
        }

        //-------------------------------------------------------------------------
        public long getCurrentLevelMaxGetGold(int current_level)
        {
            return (long)(GrowGoldPerLevelPerTime * current_level * (GrowTotalTm / GrowOneTm));
        }
    }
}
