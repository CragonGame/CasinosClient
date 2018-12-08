// Copyright(c) Cragon. All rights reserved.

#if UNITY_IOS && UNITY_EDITOR

namespace Casinos
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using UnityEditor;
    using UnityEditor.Callbacks;
    using UnityEditor.iOS.Xcode;
    using UnityEngine;

    public static class EditorXCodePostBuild
    {
        //---------------------------------------------------------------------
        [PostProcessBuild]
        public static void OnPostprocessBuild(BuildTarget build_target, string path)
        {
            if (build_target != BuildTarget.iOS) return;

            string proj_path = PBXProject.GetPBXProjectPath(path);
            PBXProject proj = new PBXProject();
            proj.ReadFromString(File.ReadAllText(proj_path));

            // 获取当前项目名字
            string target = proj.TargetGuidByName(PBXProject.GetUnityTargetName());

            // 对所有的编译配置设置选项
            proj.SetBuildProperty(target, "ENABLE_BITCODE", "NO");

            // 添加依赖库
            proj.AddFrameworkToProject(target, "libc++.dylib", false);
            proj.AddFrameworkToProject(target, "libsqlite3.dylib", false);
            proj.AddFrameworkToProject(target, "libz.dylib", false);

            proj.AddFrameworkToProject(target, "libc++.tbd", false);
            proj.AddFrameworkToProject(target, "libicucore.tbd", false);
            proj.AddFrameworkToProject(target, "libsqlite3.tbd", false);
            proj.AddFrameworkToProject(target, "libz.tbd", false);
            proj.AddFrameworkToProject(target, "libz.1.2.5.tbd", false);

            proj.AddFrameworkToProject(target, "Accelerate.framework", false);
            proj.AddFrameworkToProject(target, "AudioToolbox.framework", false);
            proj.AddFrameworkToProject(target, "AVFoundation.framework", false);
            proj.AddFrameworkToProject(target, "CFNetwork.framework", false);
            proj.AddFrameworkToProject(target, "CoreLocation.framework", false);
            proj.AddFrameworkToProject(target, "CoreMedia.framework", false);
            proj.AddFrameworkToProject(target, "CoreMotion.framework", false);
            proj.AddFrameworkToProject(target, "CoreTelephony.framework", false);
            proj.AddFrameworkToProject(target, "CoreVideo.framework", false);
            proj.AddFrameworkToProject(target, "JavaScriptCore.framework", true);// 设置为Optional
            proj.AddFrameworkToProject(target, "MessageUI.framework", false);
            proj.AddFrameworkToProject(target, "MobileCoreServices.framework", false);
            proj.AddFrameworkToProject(target, "PushKit.framework", true);
            proj.AddFrameworkToProject(target, "Security.framework", false);// 用于存储keychain
            proj.AddFrameworkToProject(target, "SystemConfiguration.framework", false);// 用于读取异常发生时的系统信息
            proj.AddFrameworkToProject(target, "UserNotifications.framework", true);

            // 设置签名
            //proj.SetBuildProperty (target, "CODE_SIGN_IDENTITY", "iPhone Distribution: _______________");
            //proj.SetBuildProperty (target, "PROVISIONING_PROFILE", "********-****-****-****-************");
            //string debugConfig = proj.BuildConfigByName(target, "Debug");
            //string releaseConfig = proj.BuildConfigByName(target, "Release");

            //proj.SetBuildPropertyForConfig(debugConfig, "PROVISIONING_PROFILE", "证书");
            //proj.SetBuildPropertyForConfig(releaseConfig, "PROVISIONING_PROFILE", "证书");
            //proj.SetBuildPropertyForConfig(debugConfig, "PROVISIONING_PROFILE(Deprecated)", "证书");
            //proj.SetBuildPropertyForConfig(releaseConfig, "PROVISIONING_PROFILE(Deprecated)", "证书");

            //proj.SetBuildPropertyForConfig(debugConfig, "CODE_SIGN_IDENTITY", "自己的证书");
            //proj.SetBuildPropertyForConfig(releaseConfig, "CODE_SIGN_IDENTITY", "自己的证书");

            //proj.SetTeamId(target, "Team ID(自己的teamid)");

            //proj.AddCapability(target, PBXCapabilityType.AssociatedDomains);

            // 保存工程
            proj.WriteToFile(proj_path);

            ProjectCapabilityManager proj_capability_mgr = new ProjectCapabilityManager(proj_path, "KingNative.entitlements", PBXProject.GetUnityTargetName());
            proj_capability_mgr.AddAssociatedDomains(new string[] { "applinks:znc4d4.openinstall.io" });// OpenInstall
            proj_capability_mgr.AddPushNotifications(false);
            proj_capability_mgr.AddBackgroundModes(BackgroundModesOptions.RemoteNotifications);
            proj_capability_mgr.WriteToFile();

            // 修改plist
            string plist_path = path + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plist_path));
            PlistElementDict root_dict = plist.root;
            root_dict.SetString("com.openinstall.APP_KEY", "znc4d4");// OpenInstall
            // NativeToolkit
            root_dict.SetString("NSPhotoLibraryUsageDescription", "Requires access to the Photo Library");
            root_dict.SetString("NSPhotoLibraryAddUsageDescription", "Requires access to the Photo Library");
            root_dict.SetString("NSCameraUsageDescription", "Requires access to the Camera");
            root_dict.SetString("NSContactsUsageDescription", "Requires access to Contacts");
            root_dict.SetString("NSLocationAlwaysUsageDescription", "Requires access to Location");
            root_dict.SetString("NSLocationWhenInUseUsageDescription", "Requires access to Location");
            root_dict.SetString("NSLocationAlwaysAndWhenInUseUsageDescription", "Requires access to Location");

            // 保存plist
            plist.WriteToFile(plist_path);
        }
    }
}

#endif