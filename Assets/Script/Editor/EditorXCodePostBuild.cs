// Copyright(c) Cragon. All rights reserved.

#if UNITY_IOS || UNITY_EDITOR

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
        [PostProcessBuild]
        public static void OnPostprocessBuild(BuildTarget build_target, string path)
        {
            if(build_target != BuildTarget.iOS) return;

                string proj_path = PBXProject.GetPBXProjectPath(path);
                PBXProject proj = new PBXProject();
                proj.ReadFromString(File.ReadAllText(proj_path));

                // 获取当前项目名字
                string target = proj.TargetGuidByName(PBXProject.GetUnityTargetName());

                // 对所有的编译配置设置选项
                proj.SetBuildProperty(target, "ENABLE_BITCODE", "NO");

                // 添加依赖库
                proj.AddFrameworkToProject(target, "PushKit.framework", false);
                proj.AddFrameworkToProject(target, "UserNotifications.framework", false);
                proj.AddFrameworkToProject(target, "StoreKit.framework", false);
                proj.AddFrameworkToProject(target, "MobileCoreServices.framework", false);
                proj.AddFrameworkToProject(target, "libicucore.tbd", false);
                proj.AddFrameworkToProject(target, "libz.1.2.5.tbd", false);
                proj.AddFrameworkToProject(target, "libsqlite3.tbd", false);
                proj.AddFrameworkToProject(target, "libz.tbd", false);
                proj.AddFrameworkToProject(target, "libc++.tbd", false);
                
                proj.AddFrameworkToProject(target, "libz.dylib", false);// 用于对上报数据进行压缩
                proj.AddFrameworkToProject(target, "libc++.dylib", false);// libc++库依赖
                proj.AddFrameworkToProject(target, "Security.framework", false);// 用于存储keychain
                proj.AddFrameworkToProject(target, "SystemConfiguration.framework", false);// 用于读取异常发生时的系统信息
                proj.AddFrameworkToProject(target, "JavaScriptCore.framework", true);// 设置为Optional

                // 设置签名
                //proj.SetBuildProperty (target, "CODE_SIGN_IDENTITY", "iPhone Distribution: _______________");
                //proj.SetBuildProperty (target, "PROVISIONING_PROFILE", "********-****-****-****-************"); 

                // 保存工程
                proj.WriteToFile(proj_path);

                // 修改plist
                string plist_path = path + "/Info.plist";
                PlistDocument plist = new PlistDocument();
                plist.ReadFromString(File.ReadAllText(plist_path));
                //PlistElementDict root_dict = plist.root;

                // 语音所需要的声明，iOS10必须
                //root_dict.SetString("NSContactsUsageDescription", "是否允许此游戏使用麦克风？");

                // 保存plist
                plist.WriteToFile(plist_path);
        }
    }
}

#endif