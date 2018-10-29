using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace io.openinstall.unity
{
    #if UNITY_IPHONE

    public class IOSOpenInstallImpl : IOpenInstall
    {
        [DllImport("__Internal")]
        private static extern void _openInstallGetInstall(int s);

        [DllImport("__Internal")]
        private static extern void _openInstallRegisterWakeUpHanndler();

        [DllImport("__Internal")]
        private static extern void _openInstallReportRegister();

        [DllImport("__Internal")]
        private static extern void _openInstallReportEffectPoint(string pointID,long pointValue);

        public override void getInstall(int s){

            _openInstallGetInstall(s);
        }


        public override void registerWakeupCallback()
        {

            _openInstallRegisterWakeUpHanndler();
        }

        public override void reportRegister()
        {
            _openInstallReportRegister();

        }

        public override void reportEffectPoint(string pointID,long pointValue)
        {
            _openInstallReportEffectPoint(pointID,pointValue);
        }

    }
    #endif

}
