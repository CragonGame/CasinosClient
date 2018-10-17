# New-Unity-For-ShareSDK
### 这是一个基于ShareSDK功能的扩展的Unity插件。使用此插件能够帮助您，在您的Unity应用/游戏里面便捷快速地实现社交分享功能。

**原生SDK版本支持:**

- [Android](https://github.com/MobClub/ShareSDK-for-Android) - V3.2.1
- [iOS](https://github.com/MobClub/ShareSDK-for-iOS) - V4.1.4

**文档语言 :** **中文** | **[English](https://github.com/MobClub/New-Unity-For-ShareSDK/blob/master/README_EN.md)**

- - - - - - - - - - - -

### *通用集成部分*

#### 步骤 1 : 下载 ShareSDK.unitypackage
下载 ShareSDK.unitypackage文件，双击并导入相关文件。
注意该操作可能会覆盖您原来已经存在的文件！

#### 步骤 2 ：挂接ShareSDK脚本并配置平台信息
选择好需要挂接的GameObject(例如Main Camera),在右侧栏中点击Add Component，选择Share SDK 进行挂接。
![](http://wiki.mob.com/wp-content/uploads/2015/09/step1.jpg)

挂接后会发现提供了当前支持的平台和及其配置信息。可以直接在此处修改你所需要的平台的配置信息。需要注意的是当前的编译环境是Android还是iOS，其字段名称是不同的哦！
![](https://lh3.googleusercontent.com/-jNoaTtDaWsI/W5kKg2hkujI/AAAAAAAABwQ/qUCfdCDp6i8Psy3TusEM7eZkXPuwDodIwCHMYCw/I/cn1.png)

除了可以上图处设定配置信息，也可以在ShareSDKDevInfo.cs文件中配置所需的平台信息，效果都是一样的。
**您可以直接通过修改里面的字符串值来设置平台信息,例如:**
```
public class SinaWeiboDevInfo : DevInfo 
{
    #if UNITY_ANDROID
    public const int type = (int) PlatformType.SinaWeibo;
    public string SortId = "1";
    public string AppKey = "568898243";
    public string AppSecret = "38a4f8204cc784f81f9f0daaf31e02e3";
    public string RedirectUrl = "http://www.sharesdk.cn";
    public string ShareByAppClient = "false";
    #elif UNITY_IPHONE
    public const int type = (int) PlatformType.SinaWeibo;
    public string app_key = "568898243"; //
    public string app_secret ="38a4f8204cc784f81f9f0daaf31e02e3";
    public string redirect_uri = "http://www.sharesdk.cn";
    public string auth_type = "both";
    #endif
}
```

#### 步骤 3 : 编写代码，实现分享/授权功能
请先导入命名空间 :
```
using cn.sharesdk.unity3d;
```
声明类 'ShareSDK'
```
private ShareSDK ssdk;
```
##### 分享
分享步骤:

i.定制分享信息
```
ShareContent content = new ShareContent();
content.SetText("this is a test string.");
content.SetImageUrl("https://f1.webshare.mob.com/code/demo/img/1.jpg");
content.SetTitle("test title");
content.SetShareType(ContentType.Image);
```

ii.如有需要可以单独定制对应的深交平台的分享内容,例如设定微博的:
```
ShareContent customizeShareParams = new ShareContent();
customizeShareParams.SetText("Sina share content");
customizeShareParams.SetImageUrl("http://git.oschina.net/alexyu.yxj/MyTmpFiles/raw/master/kmk_pic_fld/small/107.JPG");
customizeShareParams.SetShareType(ContentType.Image);
customizeShareParams.SetObjectID("SinaID");
content.SetShareContentCustomize(PlatformType.SinaWeibo, customizeShareParams);
```

iii.制定分享的回调
```
ssdk.shareHandler = ShareResultHandler;
```
以下为回调的定义:
```
void ShareResultHandler (int reqID, ResponseState state, PlatformType type, Hashtable result)
{
	if (state == ResponseState.Success)
	{
		print ("share result :");
		print (MiniJSON.jsonEncode(result));
	}
	else if (state == ResponseState.Fail)
	{
		print ("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
	}
	else if (state == ResponseState.Cancel) 
	{
		print ("cancel !");
	}
}
```

iv.传入分享参数，选择你喜欢的方式进行分享 

```
//Share by the menu
ssdk.ShowPlatformList (null, content, 100, 100);

//share by the content editor
ssdk.ShowShareContentEditor (PlatformType.SinaWeibo, content);

//share directly
ssdk.ShareContent (PlatformType.SinaWeibo, content);
```

##### 授权
i. 设定授权的回调 :
```
ssdk.authHandler = AuthResultHandler;
```
以下为回调的定义:
```
void AuthResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
{
    if (state == ResponseState.Success)
    {
    print ("authorize success !");
    }
    else if (state == ResponseState.Fail)
    {
    print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
    }
    else if (state == ResponseState.Cancel) 
    {
    print ("cancel !");
    }
}
```
ii. 进行授权:
```
ssdk.Authorize(reqID, PlatformType.SinaWeibo);
```

##### 获取用户信息

i. 设定回调 :
```
sdk.showUserHandler = GetUserInfoResultHandler;
```
以下为回调的定义:
```
void GetUserInfoResultHandler (int reqID, ResponseState state, PlatformType type, Hashtable result)
{
    if (state == ResponseState.Success)
    {
    print ("get user info result :");
    print (MiniJSON.jsonEncode(result));
    }
    else if (state == ResponseState.Fail)
    {
    print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
    }
    else if (state == ResponseState.Cancel) 
    {
    print ("cancel !");
    }
}
```

ii. 获取用户信息:
```
ssdk.GetUserInfo(reqID, PlatformType.SinaWeibo);
```

### *关于 iOS*
完成上述步骤后即可导出Xcode项目进行测试,并且不再需要在Xcode项目中进行其他操作。

### *关于 Android*
**AndroidManifest.xml**
注意目录下的AndroidManifest.xml中的包名（package）改成您自己的项目的包名。并确Mob-AppKey/Mob-AppSecret的设置，请设置成您在我们官网上申请得到配置。

-------
**更多详细用法或代码演示，请直接参考本项目中的Demo.cs文件**
**如果您集成中遇到任何需要我们帮助的地方，请联系我们，我们提供7x24小时的免费技术支持**
**客服QQ : 4006852216**
**Email : support@mob.com**

