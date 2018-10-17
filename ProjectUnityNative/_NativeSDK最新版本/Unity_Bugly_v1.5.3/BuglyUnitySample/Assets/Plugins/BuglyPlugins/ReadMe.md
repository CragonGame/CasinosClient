# Bugly Unity Plugin Guide

------

# **Bugly Unity Plugin**

> * Modified: **2015.03.28**
> * Unity Plugin Version: **1.4.3**
> * iOS SDK Version: **1.4.8+**
> * Android SDK Version: **1.2.9+**

------

## Package Contents

### Bugly Unity Plugin Package Contains
> * BuglySDK
> 	* Editor
> 	* Android
> 	* iOS
	 - `Bugly.framework`
	 - `libBuglyBridge.a`
> 
> * Plugins
> 	* Android
> > libs
> > > `buglyagent.jar`
> 
> 	* BuglyPlugins
> > `BuglyAgent.cs`
> >
> > `BuglyInit.cs`
> 
> * Samples
>

> **Notes:**
> 
> - `BuglyAgent.cs` - Script of interface method 
> 
> - `BuglyInit.cs` - Script binding the gameobject to initialize the sdk
> 
> - `Editor` - Scripts to update the xcode project
> 
> - `Bugly.framework` - The sdk for iOS, you could replace it by the latest version 
> 
> - Samples - Sample project of using Bugly Unity Plugin
>

------


## Getting Started with Bugly Unity Plugin

### Download

You can go to the [Bugly](http://bugly.qq.com/whitebook) download the unity plugin and native sdk for iOS or Android.

* Download the Bugly Unity Plugin
>
 
* Download the iOS SDK
> 

* Download the Android SDK
>


### Install

* Double click the `bugly_unity_*.unitypackage` file and import the scripts into the unity project.
> * Select Assets/Plugins into the unity project
> * Copy the Android SDK into the unity project
> 

* Call the interface method in the first loaded script of the project, details see the `Samples`
> * Init the sdk in the first loaded script as following
>  
>  ```
>  BuglyAgent.InitWithAppId("You App Id") 
>  ```
>

* Copy iOS SDK (`Bugly.framework`) into the associated Xcode project and add some dependencies libraries.
> * Drag the **Bugly.framework** into the Xcode project and select the copy trigger
> * Open the Xcode project configuration window and select the **Build Phases** tab, add libraries in **Link Binary With Libraries** as following:
> > Security.framework
> >
> > libz.dylib
> > 

### Samples


```c

public class Welcome : MonoBehaviour {	
    void InitBuglySDK(){
		// enable debug log, please set FALSE in Release version.
		BuglyAgent.ConfigDebugMode (true);
		
		#if UNITY_IPHONE || UNITY_IOS
		BuglyAgent.InitWithAppId ("Your App ID");
		#elif UNITY_ANDROID
		BuglyAgent.InitWithAppId ("Your App ID");
		#endif

		// If you do not need call 'InitWithAppId(string)' to initialize the sdk(may be you has initialized the sdk it associated Android or iOS project),
		// please call this method to enable c# exception handler only.
		BuglyAgent.EnableExceptionHandler ();
	}
    
	// Use this for initialization
	void Start () {
        InitBuglySDK();
	}
}

```


------


## APIs

- **BuglyAgent.InitWithAppId(string)**  
> 初始化Bugly，传入Bugly网站注册获得的App ID。
> 
> 启用native code(Obj-C、C/C++、Java)异常、C#异常捕获上报，如果你已经在相应的iOS或Android工程中初始化Bugly，那么你只需调用`BuglyAgent.EnableExceptionHandler`开启C#异常捕获上报即可。
> 
 
- **BuglyAgent.EnableExceptionHandler()**
> 启动C#异常日志捕获上报，默认自动上报级别LogError，那么LogError、LogException的异常日志都会自动捕获上报。
>
> 日志级别定义参考LogSeverity : {LogDebug、LogWarning、LogAssert、LogError、LogException}
> 


- **BuglyAgent.RegisterLogCallback(BuglyAgent.LogCallbackDelegate)**
> 注册LogCallbackDelegate回调方法， 处理系统的日志。
>
> 如果你的应用需要调用Application.RegisterLogCallback(LogCallback)等注册日志回调，你可以使用此方法进行替换。
> 

- **BuglyAgent.ConfigAutoReportLogLevel(LogSeverity)** 
> 设置自动上报日志信息的级别，默认LogError，则>=LogError的日志都会自动捕获上报。
>
> 日志级别的定义有LogDebug、LogWarning、LogAssert、LogError、LogException等
> 

- **BuglyAgent.ReportException (System.Exception, string)** 
> 上报已捕获C#异常，输入参数异常对象，附加描述信息
> 

- **BuglyAgent.ReportException (string, string, string)** 
> 上报自定义错误信息，输入错误名称、错误原因、错误堆栈等信息
> 

- **BuglyAgent.SetUserId (string)**
> 设置用户标识，如果不设置，默认为10000 。
>
> 在初始化之后调用
> 

- **BuglyAgent.ConfigDebugMode (bool)**
> 开启本地调试日志打印，默认关闭
>
> 注意：在发布版本中请务必关闭调试日志打印功能
> 

- **BuglyAgent.ConfigDefault (string, string, string, long)**
> 修改应用默认配置信息：渠道号、版本、用户标识等。
>
> 在初始化之前调用
> 
> 渠道号默认值为空，
> 
> 版本默认值
> 
> - Android应用默认读取AndroidManifest.xml中的android:versionName
> - iOS应用默认读取Info.plist文件中CFBundleShortVersionString和CFBundleVersion，拼接为CFBundleShortVersionString(CFBundleVersion)格式，例如1.0.1(10)
> 
> 用户标识默认值10000
> 


- **BuglyAgent.ConfigAutoQuitApplication (bool)**
> 配置是否在捕获上报C#异常信息后就立即退出应用，避免后续产生更多非预期的C#的异常。
>
> 在初始化之前调用
>


------


## Change Logs

#### Change Log (2015/08/06):
* Modify the interface class to **`BuglyAgent.cs`**
* Modify initialize method to **`BuglyAgent.InitWithAppId`**
* Add **`BuglyAgent.LogCallbackDelegate`** to replace  register `Application.LogCallback`
* Add interface **`BuglyAgent.ReportException(Exception, string)`** to report customized c# exception
* Add interface **`BuglyAgent.ReportException (string, string, string)`** to report customized error


#### Change Log (2015/06/08):
* Add config to exit the application after catch c# exception> 
* Add Editor settings to update the xcode project
* Fix the problem about the init method


#### Change Log (2015/04/21):
* Add log callback method `LogCallbackDelegate`


#### Change Log (2015/03/15):
* Modify the interface of `IBugly`
* Fix the bugs


#### Change Log (2015/01/21):
* Add method `EnableExceptionHandler`
* Modify the interface of the SDK (Android & iOS)
 

#### Change Log (2014/12/29):
* iOS SDK change the interface
* Modify the plugin interface method


#### Change Log (2014/12/12):
* Android SDK change the interface
* Add method `HandleException`

#### Change Log (2014/11/20):
* Android SDK integrated
* Public class method `BuglyUnity` exposed

#### Change Log (2014/11/18):
* iOS SDK integrated


------



## FAQ
1. 初始化SDK后，为什么仍然无法捕获上报C#异常?
> 答: 如果无法捕获上报C#异常，可以检查下面两项排查：
> 1. 检查是否有其他存在注册Application.RegisterLogCallback(LogCallback)的逻辑，由于系统默认的LogCallback是单播实现，
> 所以只能维持一个回调实例，你可以调用BuglyAgent.RegisterLogCallback(BuglyAgent.LogCallbackDelegate)方法来替代日志回调的注册。
>
> 2. 检查对应平台的SDK组件是否已集成到项目。
>


2. 为什么发生C#异常后，应用直接崩溃?
> 答: 如果遇到此场景，你可以把脚本中调用的InitWithAppId方法使用EnableExceptionHandler替代，并在对应的Android或iOS工程中初始化SDK的组件。




------

##Contact US
* Web: **http://bugly.qq.com/contact**

* Email: **bugly@tencent.com**
* QQ: **800014972**
* WeChat: **Bugly**
