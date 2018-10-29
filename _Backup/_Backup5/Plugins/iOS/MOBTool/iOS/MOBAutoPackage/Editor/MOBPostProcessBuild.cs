using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;
using cn.mob.unity3d.sdkporter;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

public class MOBPostProcessBuild 
{

	//[PostProcessBuild]
	#if UNITY_IOS
	[PostProcessBuildAttribute(88)]
	public static void onPostProcessBuild(BuildTarget target,string targetPath)
	{
		if (target != BuildTarget.iOS) 
		{
			Debug.LogWarning ("Target is not iPhone. XCodePostProcess will not run");
			return;
		}
		//拉取配置文件中的数据
		MOBXCodeEditorModel xcodeModel = new  MOBXCodeEditorModel ();
		xcodeModel.LoadMobpds ();
		//导入文件
		PBXProject xcodeProj = new PBXProject();
		string xcodeProjPath = targetPath + "/Unity-iPhone.xcodeproj/project.pbxproj";
		xcodeProj.ReadFromFile(xcodeProjPath);
		//xcode Target
		string xcodeTargetGuid = xcodeProj.TargetGuidByName("Unity-iPhone");


		//plist路径targetPath
		string plistPath = targetPath + "/Info.plist";
		PlistDocument plistDocument = new PlistDocument();
		plistDocument.ReadFromFile(plistPath);
		PlistElementDict plistElements = plistDocument.root.AsDict();

		//添加 MOBAppkey MOBAppSecret
		AddAPPKey (plistElements);
		//添加权限 等 
		AddPermissions (xcodeModel,plistElements);
		//添加白名单 
		AddLSApplicationQueriesSchemes (xcodeModel,plistElements);
		//添加 URLSchemes
		AddURLSchemes (xcodeModel,plistElements);
		//添加 InfoPlistSet
		AddInfoPlistSet(xcodeModel,plistElements);
		//写入 info.plist
		plistDocument.WriteToFile(plistPath);

		//添加 BuildSettings
		AddBuildSettings (xcodeModel,xcodeProj,xcodeTargetGuid);

		//根据配置文件加载资源及xcode设置
		bool hasMobFramework = false;
		foreach (MOBPathModel pathModel in xcodeModel.folders) 
		{
			AddFramework (pathModel.filePath,targetPath,xcodeTargetGuid,pathModel,xcodeProj,ref hasMobFramework);
			AddStaticLibrary (pathModel.filePath,targetPath,xcodeTargetGuid,pathModel,xcodeProj);
			AddHeader (pathModel.filePath,targetPath,xcodeTargetGuid,pathModel,xcodeProj);
			AddBundle (pathModel.filePath,targetPath,xcodeTargetGuid,pathModel,xcodeProj);
			AddOtherFile (pathModel.filePath,targetPath,xcodeTargetGuid,pathModel,xcodeProj,xcodeModel.fileFlags);
		}
		//加入系统Framework
		AddSysFrameworks (xcodeModel,xcodeProj,xcodeTargetGuid,targetPath);
		//加入 xcodeModel.frameworks 中指定的 framework
		AddXcodeModelFrameworks (xcodeModel,xcodeProj,xcodeTargetGuid,targetPath);
		xcodeProj.WriteToFile(xcodeProjPath);
	}

	//添加系统Framework
	private static void AddSysFrameworks(MOBXCodeEditorModel xcodeModel,PBXProject xcodeProj,string xcodeTargetGuid,string xcodeTargetPath)
	{
		foreach (string sysFramework in xcodeModel.sysFrameworks) 
		{
			xcodeProj.AddFrameworkToProject (xcodeTargetGuid,sysFramework,false);
		}
	}

	//添加 xcodeModel.frameworks 中指定的 framework shareSDK子平台的framework
	private static void AddXcodeModelFrameworks(MOBXCodeEditorModel xcodeModel,PBXProject xcodeProj,string xcodeTargetGuid,string xcodeTargetPath)
	{
		foreach (MOBPathModel pathModel in xcodeModel.frameworks) 
		{
			if (pathModel.filePath.Contains (".framework") && !pathModel.filePath.Contains ("MOBFoundation.framework")) 
			{
				string frameworkPath = pathModel.filePath.Replace(pathModel.rootPath, "");
				//			Debug.Log("frameworkPath" + frameworkPath);
				string savePath = xcodeTargetPath + frameworkPath;

				int tempIndex = frameworkPath.LastIndexOf ("/");

				string saveFrameworkPath = frameworkPath.Substring (0,tempIndex);

				//将 framework copy到指定目录
				DirectoryInfo frameworkInfo = new DirectoryInfo(pathModel.filePath);
				DirectoryInfo saveFrameworkInfo = new DirectoryInfo(savePath);
				CopyAll (frameworkInfo,saveFrameworkInfo);
				//将 framework 加入 proj中
				xcodeProj.AddFileToBuild(xcodeTargetGuid, xcodeProj.AddFile(savePath, "MOB"+frameworkPath, PBXSourceTree.Sdk));
				//将 build setting 设置
				xcodeProj.AddBuildProperty(xcodeTargetGuid, "FRAMEWORK_SEARCH_PATHS", "$(SRCROOT)"+saveFrameworkPath);
			}
			else 
			{
				Debug.LogWarning (pathModel.filePath +" no framework");
			}
		}
	}

	//添加 BuildSettings
	private static void AddBuildSettings(MOBXCodeEditorModel xcodeModel , PBXProject xcodeProj,string xcodeTargetGuid)
	{
		Hashtable buildSettings = xcodeModel.buildSettings;
		foreach (string key in buildSettings.Keys)	
		{
			ArrayList settings = (ArrayList)buildSettings[key];
			foreach (string setStr in settings)
			{
				xcodeProj.AddBuildProperty(xcodeTargetGuid, key, setStr);
			}
		}
	}
	//添加其他资源
	private static void AddOtherFile(string secondFilePath,string xcodeTargetPath,string xcodeTargetGuid,MOBPathModel pathModel,PBXProject xcodeProj,Hashtable fileFlags)
	{
		string[] secondDirectories = Directory.GetFiles(secondFilePath, "*", SearchOption.AllDirectories);
		foreach (string lastFilePath in secondDirectories) 
		{
			if (!lastFilePath.Contains (".framework") && 
				!lastFilePath.Contains (".a") && 
				!lastFilePath.Contains (".h") && 
				!lastFilePath.Contains (".bundle") &&
				!lastFilePath.Contains (".DS_Store") &&
				!lastFilePath.Contains (".meta")) 
			{
//				Debug.Log("lastFilePath" + lastFilePath);
				string otherFilePath = lastFilePath.Replace(pathModel.rootPath, "");
				int index = otherFilePath.LastIndexOf ("/");
				//项目目录
				string saveOtherFilePath = otherFilePath.Substring (0, index);
				string fileName = otherFilePath.Substring (index+1);
				//存放的本地目录
				string saveDirectory = xcodeTargetPath + saveOtherFilePath;
				if (!Directory.Exists(saveDirectory))
				{
					Directory.CreateDirectory(saveDirectory);
				}
				//将其他文件拷贝到指定目录
				FileInfo fileInfo = new FileInfo(lastFilePath);
				string savePath = xcodeTargetPath + otherFilePath;
				fileInfo.CopyTo (savePath,true);
				//将.a 加入 proj中
				Debug.Log(fileName);
				if (fileFlags.ContainsKey (fileName)) 
				{
					string flag = (string)fileFlags[fileName];
					Debug.Log(flag);
					xcodeProj.AddFileToBuildWithFlags (xcodeTargetGuid,xcodeProj.AddFile(savePath, "MOB"+otherFilePath, PBXSourceTree.Source),flag);
				} 
				else 
				{
					xcodeProj.AddFileToBuild(xcodeTargetGuid, xcodeProj.AddFile(savePath, "MOB"+otherFilePath, PBXSourceTree.Source));
				}
			}
		}
	}
	//添加 .h文件
	private static void AddHeader(string secondFilePath,string xcodeTargetPath,string xcodeTargetGuid,MOBPathModel pathModel,PBXProject xcodeProj)
	{
		string[] secondDirectories = Directory.GetFiles(secondFilePath, "*.h", SearchOption.AllDirectories);
		ArrayList savePathArray = new ArrayList ();
		foreach (string lastFilePath in secondDirectories) 
		{
//			Debug.Log("lastFilePath" + lastFilePath);
			//如果路径中带有 .framework 则不导入
			if (!lastFilePath.Contains (".framework"))
			{
				string headerPath = lastFilePath.Replace(pathModel.rootPath, "");
				int index = headerPath.LastIndexOf ("/");
				//项目目录
				string saveHeaderPath = headerPath.Substring (0, index);
				//存放的本地目录
				string saveDirectory = xcodeTargetPath + saveHeaderPath;
				if (!Directory.Exists(saveDirectory))
				{
					Directory.CreateDirectory(saveDirectory);
				}
				//将.h copy到指定目录
				FileInfo fileInfo = new FileInfo(lastFilePath);
				string savePath = xcodeTargetPath + headerPath;
				fileInfo.CopyTo (savePath,true);
				//将.h 加入 proj中
				xcodeProj.AddFileToBuild(xcodeTargetGuid, xcodeProj.AddFile(savePath, "MOB"+headerPath, PBXSourceTree.Source));
				if(!savePathArray.Contains(saveHeaderPath))
				{
					savePathArray.Add (saveHeaderPath);
					//将 build setting 设置
					xcodeProj.AddBuildProperty(xcodeTargetGuid, "HEADER_SEARCH_PATHS", "$(SRCROOT)"+saveHeaderPath);
				}
			}
		}
	}

	//添加 .a文件
	private static void AddStaticLibrary(string secondFilePath,string xcodeTargetPath,string xcodeTargetGuid,MOBPathModel pathModel,PBXProject xcodeProj)
	{
		string[] secondDirectories = Directory.GetFiles(secondFilePath, "*.a", SearchOption.AllDirectories);
		foreach (string lastFilePath in secondDirectories) 
		{
//			Debug.Log("lastFilePath" + lastFilePath);
			string staticLibraryPath = lastFilePath.Replace(pathModel.rootPath, "");
			int index = staticLibraryPath.LastIndexOf ("/");
			//项目目录
			string saveStaticLibraryPath = staticLibraryPath.Substring (0, index);
			//存放的本地目录
			string saveDirectory = xcodeTargetPath + saveStaticLibraryPath;
			if (!Directory.Exists(saveDirectory))
			{
				Directory.CreateDirectory(saveDirectory);
			}
			//将.a copy到指定目录
			FileInfo fileInfo = new FileInfo(lastFilePath);
			string savePath = xcodeTargetPath + staticLibraryPath;
			fileInfo.CopyTo (savePath,true);
			//将.a 加入 proj中
			xcodeProj.AddFileToBuild(xcodeTargetGuid, xcodeProj.AddFile(savePath, "MOB"+staticLibraryPath, PBXSourceTree.Sdk));
			//将 build setting 设置
			xcodeProj.AddBuildProperty(xcodeTargetGuid, "LIBRARY_SEARCH_PATHS", "$(SRCROOT)"+saveStaticLibraryPath);
		}
	}

	//添加 bundle
	private static void AddBundle(string secondFilePath,string xcodeTargetPath,string xcodeTargetGuid,MOBPathModel pathModel,PBXProject xcodeProj)
	{
		SearchOption searchOption;
		if (secondFilePath.Contains ("/ShareSDK/")) //shareSDK
		{
			searchOption = SearchOption.TopDirectoryOnly;
		} 
		else 
		{
			searchOption = SearchOption.AllDirectories;
		}
		string[] secondDirectories = Directory.GetDirectories(secondFilePath, "*.bundle", searchOption);
		foreach (string lastFilePath in secondDirectories) 
		{
//			Debug.Log("lastFilePath" + lastFilePath);
			string bundlePath = lastFilePath.Replace(pathModel.rootPath, "");
//			Debug.Log("bundlePath" + bundlePath);
			string savePath = xcodeTargetPath + bundlePath;
			//将 framework copy到指定目录
			DirectoryInfo bundleInfo = new DirectoryInfo(lastFilePath);
			DirectoryInfo saveBundleInfo = new DirectoryInfo(savePath);
			CopyAll (bundleInfo,saveBundleInfo);
			//将 framework 加入 proj中
			xcodeProj.AddFileToBuild(xcodeTargetGuid, xcodeProj.AddFile(savePath, "MOB"+bundlePath, PBXSourceTree.Sdk));
		}
	}

	//添加 Framework
	private static void AddFramework(string secondFilePath,string xcodeTargetPath,string xcodeTargetGuid, MOBPathModel pathModel,PBXProject xcodeProj, ref bool hasMobFramework)
	{
		SearchOption searchOption;
		if (secondFilePath.Contains ("/ShareSDK/")) //shareSDK
		{
			searchOption = SearchOption.TopDirectoryOnly;
		} 
		else 
		{
			searchOption = SearchOption.AllDirectories;
		}
		string[] secondDirectories = Directory.GetDirectories(secondFilePath, "*.framework", searchOption);
		foreach (string lastFilePath in secondDirectories) 
		{
//			Debug.Log("lastFilePath" + lastFilePath);

			int index = lastFilePath.LastIndexOf ("/");

			//framework 名称
			string frameworkName = lastFilePath.Substring (index+1);
//			Debug.Log("frameworkName" + frameworkName);
			bool isMOBFoundation = (frameworkName == "MOBFoundation.framework" );
			if(!isMOBFoundation || (isMOBFoundation && !hasMobFramework))
			{
				if(isMOBFoundation && !hasMobFramework)
				{
//					Debug.Log("isMOBFoundation " + lastFilePath);
					hasMobFramework = true;
				}
				string frameworkPath = lastFilePath.Replace(pathModel.rootPath, "");
				//			Debug.Log("frameworkPath" + frameworkPath);
				string savePath = xcodeTargetPath + frameworkPath;

				int tempIndex = frameworkPath.LastIndexOf ("/");

				string saveFrameworkPath = frameworkPath.Substring (0,tempIndex);

				//将 framework copy到指定目录AddURLSchemes
				DirectoryInfo frameworkInfo = new DirectoryInfo(lastFilePath);
				DirectoryInfo saveFrameworkInfo = new DirectoryInfo(savePath);
				CopyAll (frameworkInfo,saveFrameworkInfo);
				//将 framework 加入 proj中
				xcodeProj.AddFileToBuild(xcodeTargetGuid, xcodeProj.AddFile(savePath, "MOB"+frameworkPath, PBXSourceTree.Sdk));
				//将 build setting 设置
				xcodeProj.AddBuildProperty(xcodeTargetGuid, "FRAMEWORK_SEARCH_PATHS", "$(SRCROOT)"+saveFrameworkPath);
			}
		}
	}

	//拷贝目录下的所有文件并剔除.meta
	private static void CopyAll(DirectoryInfo source, DirectoryInfo target)
	{
		if (source.FullName.ToLower() == target.FullName.ToLower())
		{
			return;
		}
		// Check if the target directory exists, if not, creatAddURLSchemesAddURLSchemese it.
		if (!Directory.Exists(target.FullName))
		{
			Directory.CreateDirectory(target.FullName);
		}
		// Copy each file into it's new directory.
		foreach (FileInfo fi in source.GetFiles())
		{
			
			if(!fi.Name.EndsWith(".meta"))
			{
				string name = fi.Name;
				if(name.Contains(".mobjs"))
				{
					name = name.Replace (".mobjs",".js");
				}
				fi.CopyTo(Path.Combine(target.ToString(), name), true);
			}
		}
		// Copy each subdirectory using recursion.
		foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
		{
			DirectoryInfo nextTargetSubDir =
				target.CreateSubdirectory(diSourceSubDir.Name);
			CopyAll(diSourceSubDir, nextTargetSubDir);
		}
	}

	//在info.plist中添加 MOBAppkey MOBAppSecret
	private static void AddAPPKey(PlistElementDict plistElements)
	{
		var files = System.IO.Directory.GetFiles(Application.dataPath , "MOB.keypds", System.IO.SearchOption.AllDirectories);
		string filePath = files [0];
		FileInfo projectFileInfo = new FileInfo( filePath );
		if (projectFileInfo.Exists) 
		{
			StreamReader sReader = projectFileInfo.OpenText();
			string contents = sReader.ReadToEnd();
			sReader.Close();
			sReader.Dispose();
			Hashtable datastore = (Hashtable)MiniJSON.jsonDecode( contents );
			string appKey = (string)datastore["MobAppKey"];
			string appSecret = (string)datastore["MobAppSecret"];
			plistElements.SetString("MOBAppkey",appKey);
			plistElements.SetString("MOBAppSecret",appSecret);
		} 
		else 
		{
			Debug.LogWarning ("MOB.keypds no find");
		}
	}

	//在info.plist中添加 必要的权限设置
	private static void AddPermissions(MOBXCodeEditorModel xcodeModel,PlistElementDict plistElements)
	{
		Hashtable permissions = xcodeModel.permissions;
		foreach (string key in permissions.Keys)
		{
			string value = (string)permissions[key];
			plistElements.SetString(key,value);
		}
	}

	//在info.plist中添加 白名单
	private static void AddLSApplicationQueriesSchemes(MOBXCodeEditorModel xcodeModel,PlistElementDict plistElements)
	{
		ArrayList LSApplicationQueriesSchemes = xcodeModel.LSApplicationQueriesSchemes;
		PlistElementArray elementArray = plistElements.CreateArray ("LSApplicationQueriesSchemes");
		foreach (string str in LSApplicationQueriesSchemes)
		{
			elementArray.AddString (str);
		}
	}

	//在info.plist中添加 URLSchemes
	private static void AddURLSchemes(MOBXCodeEditorModel xcodeModel,PlistElementDict plistElements)
	{
		ArrayList URLSchemes = xcodeModel.URLSchemes;
		PlistElementArray elementArray = plistElements.CreateArray ("CFBundleURLTypes");
		foreach (Hashtable scheme in URLSchemes)
		{
			PlistElementDict dict = elementArray.AddDict ();
			dict.SetString ("CFBundleURLName",(string)scheme["CFBundleURLName"]);
			PlistElementArray urlArray = dict.CreateArray ("CFBundleURLSchemes");
			ArrayList schemes = (ArrayList)scheme ["CFBundleURLSchemes"];
			foreach (string schemeStr in schemes)
			{
				urlArray.AddString (schemeStr);
			}
		}
	}

	//在info.plist中添加 infoPlistSet
	private static void AddInfoPlistSet(MOBXCodeEditorModel xcodeModel,PlistElementDict plistElements)
	{
		Hashtable infoPlistSets = xcodeModel.infoPlistSet;
		foreach (string key in infoPlistSets.Keys)
		{
			var value = infoPlistSets[key];
			if(value.GetType().Equals(typeof(string)))
			{
				plistElements.SetString (key,(string)value);
			}
			else if(value.GetType().Equals(typeof(Hashtable)))
			{
				Hashtable temp = (Hashtable)value;
				PlistElementDict dict = plistElements.CreateDict (key);
				foreach (string tempKey in temp.Keys)
				{
					//暂时只支持1层dict
					dict.SetString (tempKey,(string)temp[tempKey]);
				}
			}
		}
	}
	#endif
}