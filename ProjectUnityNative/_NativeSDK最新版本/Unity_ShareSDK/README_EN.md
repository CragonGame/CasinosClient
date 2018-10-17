# New-Unity-For-ShareSDK
### This is a UnityPackage for ShareSDK.It's an convenient tool for you to quickly implement SNS Share feature on your Unity Project on iOS/Android.

**supported original ShareSDK version:**

- [Android](https://github.com/MobClub/ShareSDK-for-Android) - V3.2.1
- [iOS](https://github.com/MobClub/ShareSDK-for-iOS) - V4.1.4

**Document Language :** **[中文](https://github.com/MobClub/New-Unity-For-ShareSDK)** | **English**

- - - - - - - - - - - -

### *Integration of general part*

#### Step 1 : Download ShareSDK.unitypackage
Download this git(master),import the ShareSDK.unitypackage to your Unity project.
Please notice that this operation could cover your original existed files!

#### Step 2 : Set up script and configurations
Make sure that the 'ShareSDK' component was added to your GameObject(such as 'Main Camera').Click'Add Component' from the right-hand side bar, and choose 'ShareSDK'.
![](https://lh3.googleusercontent.com/-rmrjMgkzmGw/W5kKylTphXI/AAAAAAAABwg/9-Dr--MShCgr2T0xVgfQ44brTSqiEAXSgCHMYCw/I/1.png)

Then you will see some configurations under the ShareSDK Script. 
You should setup your own configuration by editing the fields.Setup Your MOBAppKey/MOBAppSecret and other SNS's config which you want, such as Facebook's key/secret, Wechat's, Twitter's etc.

**Setup Mob AppKey/Secret.**
![](https://lh3.googleusercontent.com/-KGH-XMjhjwk/W5kKyETfF_I/AAAAAAAABwY/DkK2Bb8HhqYEINRytORIffBC5t-I9uOWQCHMYCw/I/1.1.png)

**Setup SNS's Config**
![](https://lh3.googleusercontent.com/-ultUnBrwpvQ/W5kKyGs4cKI/AAAAAAAABwc/CbYWZT9rQXcQtyTDCRJUDpuqlN7hwPweQCHMYCw/I/1.2.png)

Please differentiate the compiler environment between  Android or the iOS, cause some SNS's fields are different.

Your could also setup the SNS's config in the file 'ShareSDKDevInfo.cs'.**The effect is the same as the Mentioned above.**

**Setup SNS's Config by change the string VALUE (NOT THE KEY) - example:**
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

#### Step 3 : Code for Sharing and Authorization
Please import Name Space first :
```
using cn.sharesdk.unity3d;
```
and declare the Class 'ShareSDK'
```
private ShareSDK ssdk;
```

##### About Sharing
i.Customize the sharing information :
```
ShareContent content = new ShareContent();
content.SetText("this is a test string.");
content.SetImageUrl("https://f1.webshare.mob.com/code/demo/img/1.jpg");
content.SetTitle("test title");
content.SetShareType(ContentType.Image);
```
ii.If you need, you can customize the ShareContent for some detail platform, such as setup SinaWeibo:
```
ShareContent customizeShareParams = new ShareContent();
customizeShareParams.SetText("Sina share content");
customizeShareParams.SetImageUrl("http://git.oschina.net/alexyu.yxj/MyTmpFiles/raw/master/kmk_pic_fld/small/107.JPG");
customizeShareParams.SetShareType(ContentType.Image);
customizeShareParams.SetObjectID("SinaID");
content.SetShareContentCustomize(PlatformType.SinaWeibo, customizeShareParams);
```
iii.Define the callback, then setup the shareHandler.

set up handler:
```
ssdk.shareHandler = ShareResultHandler;
```
define callback:
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
    print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
    }
    else if (state == ResponseState.Cancel) 
    {
    print ("cancel !");
    }
}
```

iv.Pass the content params, and Share. 

```
//Share by the menu
ssdk.ShowPlatformList (null, content, 100, 100);

//share by the content editor
ssdk.ShowShareContentEditor (PlatformType.SinaWeibo, content);

//share directly
ssdk.ShareContent (PlatformType.SinaWeibo, content);
```

##### About Authorization
i. Set the auth call back :
```
ssdk.authHandler = AuthResultHandler;
```
define callback:
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
ii. now you can make an Authorization:
```
ssdk.Authorize(reqID, PlatformType.SinaWeibo);
```

##### About Get User's information

i. Set the call back :
```
sdk.showUserHandler = GetUserInfoResultHandler;
```
define callback:
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

ii. now you can get the user's info:
```
ssdk.GetUserInfo(reqID, PlatformType.SinaWeibo);
```

### *About iOS*
If you finished setup ShareSDK for your project, you can just build a Xcode Project, then test it.That's All!

### *About Android*

**AndroidManifest.xml**
Please make sure that the Package Name was changed to your own's.Setup the value of Mob-AppKey/Mob-AppSecret as same as 'MOBAppKey/MOBAppSecret' which mentioned in Step 2.

Now you can build your .apk and test it!


-------

**Please refer to the Demo.cs in the git and know the detail usage**
**Finally, if you have any other questions, please contact us on 7x24 hours.We will provide FREE Technical supports:**
**Service QQ : 4006852216** 
**Email : support@mob.com**

