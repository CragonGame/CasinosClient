# Release Note

### 3.10.0 (9 Feb, 2019)

#### Add

* A method to turn off automatically prompt alert showing when received an HTTP auth challenge from server. Use `SetAllowHTTPAuthPopUpWindow` to control the behavior.
* Support open third party app with links of corresponding URL schemes. Now you can use a link to open other apps as long as it was registered.

#### Fix

* Performance issues when using some sync getter APIs on Android native side.

### 3.9.2 (10 Jan, 2019)

#### Fix

* A problem that `SetZoomEnabled` not works correctly on iOS 12.
* Now `CanGoBack`, `CanGoForward` and `Url` getters also work for single page app page.

### 3.9.1 (18 Dec, 2018)

#### Fix

* A potential issue causes file chooser not response correctly when user dismiss the permission prompt without choosing.
* Remove JavaScript Interface support for suppressing security warning from Google.

### 3.9.0 (30 Nov, 2018)

#### Add

* A parameter for `Load` method to customize the read access URL for local file loading. It helps to load local resources under a different URL other than the current load page.

#### Fix

* A problem that read access URL not encoded correctly when special characters contained.
* An issue causes crash when changing screen orientation by code when closing the web view with a toolbar displayed.

### 3.8.1 (24 Oct, 2018)

#### Fix

* Fix a potential crash when reference `RectTransform` is used to determine web view frame, while there is no cavans for some reason on the transform.

### 3.8.0 (5 Sep, 2018)

#### Add

* A method to allow file access from file URLs. This could solve some problem when request in a cross origin way from local pages. However, by setting it to true may cause some potential security issue, so make your choice at the risk.

#### Fix

* A problem causing immersive mode flickering in Android 8.x.
* Post request on back button would work correctly.

### 3.7.1 (26 Jun, 2018)

#### Fix

* A typo on "OnOrientationChanged" event. It was "OnOreintationChanged" and now we correct this issue in both code and documentation.

### 3.7.0 (14 Jun, 2018)

::: danger
From Unity 2018, Gradle is used as the default build system, so we updated the integration method to make UniWebView works better in the new build system. If you are upgrading UniWebView from an earlier version, please refer to the [Adapting to AAR File](http://docs.uniwebview.com/guide/adapting-to-aar-file.html) documentation.
:::

#### Add

* Compatible with Unity 2018.1 and gradle build system. Please note that you need some migration if you want to upgrade from an earlier version.

### 3.6.1 (26 Apr, 2018)

#### Fix

* Compatible with Unity 2017.4. Now videos could be played correctly on Android devices in project exported by Unity 2017.4.

---

### 3.6.0 (3 Apr, 2018)

#### Add

* OnWebContentProcessTerminatedDelegate event is added. This event will be raised when iOS system stops loading the web content for some reason (Usually due to memory pressure). Prior UniWebView versions will try to refresh the page for you automatically. However, that would not fix the problem in most cases. This event provides a chance for you to free as much as resources and do reloading/handling as you need.

#### Fix

* As issue which causes web view frame not set properly when using prefab with a pre-set reference rectangle transform.
* Some other minor internal fix.

---

### 3.5.2 (28 Feb, 2018)

#### Fix

* An issue which causes POST data from HTML form is missing when a customized header field is set on iOS.
* Now customized header fields will be also added to image download requests on Android.

---

### 3.5.1 (31 Jan, 2018)

#### Fix

* A potential issue which may cause game unresponsive when switching back to foreground when a web view's game object or its parent objects are inactive.
* An issue causes web view cannot be added to correct view in the first game runloop while splash screen is disabled.

---

### 3.5.0 (22 Jan, 2018)

#### Add

* Images from Internet now could be downloaded to Download folder in Android.
* More consistent toolbar layout mechanism for toolbar on iOS. Now Auto Layout is used for layouting toolbar, and `adjustInset` option works better.

#### Fix

* An issue which crashes macOS Editor when loading a local file in macOS 10.11 or earlier.
* iOS context menu (action bar) now could cover toolbar properly.
* A regression that makes setting text for toolbar done button title not working.
* No need to add support-v4 package anymore to support uploading files to server.
* Toolbar intersection with web view will now respect your frame setting first, then adjust its own height or top anchor.

---

### 3.4.2 (28 Dec, 2017)

#### Fix

* Use new message sending method between native and C# script. Now all messages could be received even when the web view game object is inactive.
* Prevent `Init` with an empty name for security.

---

### 3.4.1 (12 Dec, 2017)

#### Fix

* Fix an issue on Android that getting some web view properties inside web view events may freeze the loading process.
* The toolbar will not show automatically when you are using a prefab with "Use Toolbar" on. Now the toolbar will follow the show and hide state of the web view itself.
* Fix an error in Editor when stop playing mode while a web view is being shown.
* Upgrade to Android build SDK to API Level 26. (But still support from Android 5.0.)

---

### 3.4.0 (7 Nov, 2017)

UniWebView now requires Xcode 9 with iOS SDK 11 to build. If you are still using Xcode 8 and iOS 10 SDK, please use UniWebView 3.3.x instead.

#### Add

* Better cookie management for iOS 11. On iOS 11, now UniWebView uses the newly added HTTP cookie store to get more stable cookie states.
* Update web view layout and toolbar for iPhone X screen.
* A method for printing current web view to a printer or air printer.

#### Fix

* A potential issue which may cause cookie lost during 301 or 302 redirecting.

---

### 3.3.2 (6 Oct, 2017)

#### Fix

* A crash when games are built against iOS 11 SDK and setting screen orientation with Unity API while the web view showing.

---

### 3.3.1 (6 Oct, 2017)

#### Fix

* A crash when setting screen orientation with Unity API while the web view showing.
* Compatible with iOS 11 SDK and Xcode 9.
* Due to a Unity High Sierra support issue, UniWebView 3 now require Unity 5.6.3 at least.

---

### 3.3.0 (9 Sep, 2017)

#### Add

* Support HTTP Basic and HTTP Digest authentication. A native pop-up will be displayed when there is a challenge to ask users to provide user name and password.
* Support "intent" and "market" URL scheme. Now any valid "intent://" URL will be handled on Android. If target intent is not found, and there is no fallback URL provided, UniWebView will try to open the application page on Play Market. The "market://" URL will be navigated to Play Market.

#### Fix

* A problem which caused hidden web view also receiving touch event on Android when multiple web views are used.
* Fix an issue which causes crash when uploding a camera captured photo on some Android devices.
* Improvement on loading performance a bit on Android.

---

### 3.2.0 (3 Sep, 2017)

#### Add

* An option to URL or HTML string loading APIs to skip encoding, it is useful when you need to encode the url yourself instead of using the default encoding rule in UniWebView.
* An option to cookie setting and getting APIs to skip encoding.

#### Fix

* An issue on Android which cause the default user agent cannot be retrieved correctly before the first request.
* A memory leak on macOS Editor that web view window not get closed correctly.
* More accurate scaling calculating in Zoomed Display mode on iOS.
* Event touch could be handled correctly in Android when multiple web views shown now.
* The URL encoding will not be applied to an already encoded input URL now.
* The local file loading will not be loaded again in current view. This prevents a wrong behavior when loading a local page containing iframe tag.
* The UniWebViewMessage will now respect the encoded url parameters and queries.

---

### 3.1.4 (17 Aug, 2017)

#### Fix

* Fix a problem which could cause web view alpha value not correct in some situation after transition with fade animation.
* Fix position issue when showing web view with both edge and fade animation.

---

### 3.1.3 (9 Aug, 2017)

#### Fix

* A linking issue when running with a lower iOS SDK in Xcode.

---

### 3.1.2 (6 Aug, 2017)

#### Fix

* Now `SetWebContentsDebuggingEnabled` also works for macOS Editor build. Set it to true and you could right click in the web view in editor to show an inspector for debugging purpose.
* Fix a flickering of navigation bar when loading a new page with immersive mode on Android.
* A compiling error when build iOS target on a Windows editor.
* A problem that built-in some schemes (`mailto`, `sms` and `tel`) not handled correctly on iOS and macOS.
* Fix some deprecated methods of build system for Android.

---

### 3.1.1 (21 Jul, 2017)

#### Fix

* Local file URL with special charactors now can be corrected encoded when loading.
* Avoid unhandled Show method calling right after a Hide method. Now, the Hide method will be ignore if the web view is already hidden.
* All three kinds of render mode for canvas (`ScreenSpaceOverlay`, `ScreenSpaceCamera` and `WorldSpace`) are now supported when using `RectTransform` to determin web view position and size.
* Array query in `UniWebViewMessage` should work as expected. Now a query like "?a[]=1&a[]=2" will be parsed to "1,2" in the result message.

---

### 3.1.0 (14 Jul, 2017)

#### Add

* A helper method `UniWebViewHelper.PersistentDataURLForPath` to return a url string for files under `persistentDataPath`.
* A method to enable user resizing for web view windows on macOS Editor.
* Upgrade to new build toolchain to get better optimized binary for both iOS and Android targets.

#### Fix

* An issue which might cause url encoding returns wrong result when the original url contains space for other special characters.

---

### 3.0.1 (3 Jul, 2017)

#### Fix

* Setting cookies from JavaScript now could work correctly.
* Allowing back compatibility for mixed content loading in Android.
* Stopping loading now could trigger page loading error event.
* Fully bitcode support is now enabled for iOS build.

---

### 3.0.0 (27 Jun, 2017)

::: danger
3.0.0 is a major update of UniWebView. We rewote the whole software from scratch to bring your experience of using a web view in Unity to a next level. Be causion it is not compatible with UniWebView 2, there are quite a few breaking changes in this version. For migration from UniWebView 2 in detail, visit our [Migration Guide](http://docs.uniwebview.com/#/migration-guide) in documentation. To know the highlight of the version, check [this page](http://docs.uniwebview.com/#/version-highlight) for more.
:::

#### Add

* Better way to set frame of the web view. You can also use a reference `RectTransform` to set position and size.
* Use `WKWebView` instead of `UIWebView` on iOS.
* New pop-up style Unity Editor support on macOS. It is a fully functional tool for debugging purpose.
* A brandly new way to setup bridging between Unity and Cocoa native. Now there is no message sending delay.
* You can now set the position of toolbar to top or bottom on iOS.
* A leveled logger to log all UniWebView related information. See [UniWebViewLogger](http://docs.uniwebview.com/#/latest/api/uniwebviewlogger.html) documentation for more.
* Use a payload based callback API like transition and JavaScript related methods. It takes more data. See [UniWebViewNativeResultPayload](http://docs.uniwebview.com/#/latest/api/uniwebviewnativeresultpayload.html).
* SSL exception for white listed domain. It is useful for a untrusted certification but you still want to access.
* A method to get current web view HTML content as a string.

#### Fix

* UniWebView Android no longer requires to be the main activity.
* Uploading now could work properly on all supported devices.
* The url scheme based message now supports UTF-8 and url encoding directly.
* Now the customized header field will be existing for all requests until removed, not only the first one.

---

### 2.11.1 (22 May, 2017)

#### Fix

* A performance issue which causes the camera scene gets slow and laggy when used with Vuforia.

---

### 2.11.0 (28 Apr, 2017)

#### Add

* An API to ignore SSL cert error for a certain host in Android.
* Add Firebase as built-in third party jar supporting.

#### Fix

* An icon used to demonstrate UniWebView.

---

### 2.10.0 (28 Feb, 2017)

#### Add

* An API to set allowing video autoplay. Call SetAllowAutoPlay on the web view for it. Please note that you also need to add an "autoplay" property in the video tag to enable autoplay in the web page.
* An API to enable inline video play for iOS. By default, iOS video play will be poped to full-screen. By calling SetAllowInlinePlay with a true flag, you can play video inline on iOS. Please note you also need to add an "inline" property in youe video tag to support it.
* SetCookie and GetCookie methods to set and get a cookie for a specified URL and key.

#### Fix

* A potential issue that cleaning cookie did not work in some case. Since the logic of current cleaning cookie is not correct, we also marked the original CleanCookie method to be obsoleted. You should now use SetCookie method to set a cookie value to empty for cleaning purpose.

---

### 2.9.2 (31 Jan, 2017)

#### Fix

* Precompiled jar file for Google VR activity.
* An issue which causes web view cannot be opened correctly in some old Android devices.

---

### 2.9.1 (7 Nov, 2016)

#### Fix

* Add a null check for Android web view. This prevented a potential crash when navigating from javascript

---

### 2.9.0 (29 Sep, 2016)

#### Add

* Android remote web content debugging support. Now you can debug your web page in Android with Chrome. Just set `SetWebContentsDebuggingEnabled` with true to enable it.
* Ability for loading page with overview mode for Android.

#### Fix

* Now all key down events are forwarded to Unity side by OnKeyDown callback in Android, even for TV remote controller and real device keys.

---

### 2.8.0 (29 Jul, 2016)

#### Add

* An option (`openLinksInExternalBrowser`) that all the links should be opened in an external browser or not. It will be useful if you want to open a URL in Mobile Safari or Chrome, instead of opening it in UniWebView. Default is false.
* Now you can set the Done button title from Unity. Use `SetDoneButtonText` to customize the button title.
* LGC SDK built-in support.

---

### 2.7.1 (3 Apr, 2016)

#### Fix

* Now the keyboard will also be dismissed in iOS when you call Hide on the web view with the keyboard showing.
* A potential problem of activity type casting which might cause the app crashes when you use UniWebView with some other third party activity.
* Add "singleTop" to launch mode in Android to prevent crash from multiple activity racing.

---

### 2.7.0 (11 Mar, 2016)

#### Add

* Add APIs for setting visibility of vertical and horizontal scroll bar of the web view.

#### Fix

* Update pre-compiled jar file for Prime31 and CardBoard, to be compatible to their latest SDKs.
* Return an empty string for asset path string API, so it will compile even you switch to a platform that UniWebView does not support.
* Fix a problem which prevents you from settings background color to transparent ones. It is a regression in version 2.6.0 and now it will not show a black background color anymore.
* Not disabling hardware accelerating when Settings background color with alpha for Android. This would improve the performance of using a transparent background a lot.
* A potential issue that will fail in type casting in Android activity type. This would improve the compatibility when working with other third party packages which tries to convert the Unity activity.

---

### 2.6.0 (15 Feb, 2016)

#### Add

* Rewrite video playing for Android web view. Now you could expect a better and stable video playback for both Youtube and Vimeo.

#### Fix

* Remove custom activity which is not needed anymore.

---

### 2.5.2 (27 Jan, 2016)

#### Fix

* Use UnityPlayerActivity instead. This makes UniWebView compatible with Unity 5.4 (current in beta).

---

### 2.5.1 (22 Jan, 2016)

#### Fix

* A crash issue in editor when you use AddUrlScheme.

---

### 2.5.0 (31 Dec, 2015)

#### Add

* Support for adding customize header field.
* A helper method to generate streaming asset url for local pages.
* Add AppFlyer jar file.

#### Fix

* Hide transition callback will work correctly for Android now.
* Improve performance for OS X Editor version.

---

### 2.4.1 (11 Dec, 2015)

#### Fix

* Compatible with Unity 5.3.0's new OS X OpenGL Core for editor.
* Clean some debug log.
* Update default page in demo scene.
* Set default user agent of Editor version to an iPhone agent. So you could get the mobile version of web view in Editor.

::: danger
Notice: Unity dropped Windows Phone 8 support officially from 5.3. UniWebView will not continue development for Windows Phone as well from this version.
:::

Reason: The ammount of our Windows Phone users is very few compared to iOS and Android (it is less than 1% share according to report).
However, if we want to to support Windows Store App with Windows Phone 8.1 SDK or Windows 10 SDK, it would take even more time than either of other platforms.

Since the Windows Phone SDK itself is not backwards compatible, the transition from Windows 8 to 8.1 or 10 means we need to rewrite all code in the package.

We have only limited resource. To make sure we could build the asset in a better quality for most of our users, we decided to focus back to iOS and Android only from next version.

The current Windows Phone 8 support (UniWebViewWP.dll) will be kept in later versions of UniWebView for a while, but we are sorry that it doesn't seem to get updated anymore.

---

### 2.4.0 (4 Dec, 2015)

#### Add

* Built-in show/hide transition. You could now show or hide the webview with a transition effect. Currently we support fade in/out and transition from/to one of screen edges. Please take a look at reference of Show() and Hide() for more.
  Add: Recreate new demo scenes as a better tutorial. Now most of the useful features are contained in the demo scenes, with fully commented script to show how to use them.
* Now you can use "ESC" in OS X Editor to go back or close the web view.

#### Fix

* The color of iOS toolbar is now white. This fixed an issue that the clear background tool bar will become black if no web page is underneath.
* Fix an issue which cause show tool bar property not working in some situation.
* Adjust the behavior of insets, it is a preparation for later feature like insets translating. The API of insets remains the same now.

#### Remove

::: danger
Notice: Unity will drop Windows Phone 8 support officially from 5.3. UniWebView will not continue development for Windows Phone as well from the next version.
:::

Reason: The ammount of our Windows Phone users is very few compared to iOS and Android (it is less than 1% share according to report).
However, if we want to to support Windows Store App with Windows Phone 8.1 SDK or Windows 10 SDK, it would take even more time than either of other platforms.

Since the Windows Phone SDK itself is not backwards compatible, the transition from Windows 8 to 8.1 or 10 means we need to rewrite all code in the package.

We have only limited resource. To make sure we could build the asset in a better quality for most of our users, we decided to focus back to iOS and Android only from next version.

The current Windows Phone 8 support (UniWebViewWP.dll) will be kept in later versions of UniWebView for a while, but we are sorry that it doesn't seem to get updated anymore.

---

### 2.3.1 (5 Nov, 2015)

#### Fix

* An issue that script is missing in the prefab if you import the asset in a new version of Unity.
* Compatibility for Unity 5.2.2 editor.

---

### 2.3.0 (13 Oct, 2015)

#### Add

* SMS link support for Android devices.

---

### 2.2.5 (8 Oct, 2015)

#### Fix

* Compatibility for Unity 5.2.1 editor.

---

### 2.2.4 (4 Oct, 2015)

#### Fix

* A status bar truncating issue which may appears on some Android devices when user show/hide the soft keyboard for several times.

---

### 2.2.3 (27 Sep, 2015)

* An API to add trusted sites for Android. You will need to add your site url to the white list if you need to request the protected resources of Android devices (such as microphone or camera).
* Add third party support for Craftar.

#### Fix

* A vulnerability that accept SSL connect even an error hanppes. Now UniWebView will use the default behavior of system and reject all untrusted SSL connection. It will protect your web content from possible man-in-the-middle attacks. At the same time, it means you have to use a valid certification even if you are in test environment.

* A null exception when inputing in a text field in Editor.

---

### 2.2.2 (12 Sep, 2015)

#### Fix

* An issue in Unity 5.2 which causes the webview not showing correctly in Unity Editor.
* Alert compatibility for iOS in Unity 5.2.

---

### 2.2.1 (4 Sep, 2015)

#### Add

* PIs to get navigation state of current web view. Now you can use CanGoBack() and CanGoForward() to determine whether there is a page to go back or forward.

#### Fix

* Optimize performance for 64-bit OSX Editor version.

---

### 2.2.0 (22 Aug, 2015)

#### Add

* An API to set background color for the web view.

#### Fix

* The video will paused when app switched to background in Android devices now.
* An issue causes the loading indicator not removed in Windows Phone 8 devices when dismiss the webview.
* Add Neatplugin and RigidFace to default supported third party jar.

---

### 2.1.3 (30 Apr, 2015)

#### Fix

* File chooser for Android 5.0+.

---

### 2.1.2 (28 Apr, 2015)

#### Fix

* An issue on Android which causes screen remaining black when user taps back button when playing a fullscreen Youtube video.

---

### 2.1.1 (26 Apr, 2015)

#### Add

* API for setting and getting alpha value.

#### Fix

* An issue which causes spinner not hidden when webview dismissed by done button.

---

### 2.1.0 (22 Apr, 2015)

#### Add

* Add immersive mode support for Android for API Level 19 and above.
* Google CardBoard support. You can now find a pre-built CardBoard jar library.

#### Fix

* A crash which causes crash when setting insets to an invalid value.

---

### 2.0.0 (15 Mar, 2015)

#### Add

* Support for Windows Phone 8 and 8.1. Please note you need to add WebBrowser capatibility in Windows Phone's manifest file after exported from Unity.
* Support for Unity 5. If you upgrade from UniWebView 1.x, you need to remove the old package and reimport again.
* 64 bit support for Mac Editor version, since Unity 5 is now a 64-bit app.

#### Fix

* An issue which may cause the cache can not be cleaned completed in iOS.
* A serialize issue which may cause editor crash in some occation.

::: danger
Notice: This version is not compatible with Unity 4.x. If you need to use UniWebView on Unity 4.x, please use UniWebView 1.x instead. You can find more information about the earlier version on Asset Store: http://assetstore.unity3d.com/jp/#!/content/12476
:::

---

### 1.9.0 (10 Feb, 2015)

::: warning
1.9.x will be the last version support Unity 4.x. Due to the huge difference between Unity 4 and 5, we decide to make a major update as well. Please keep an eye on our website (http://uniwebview.onevcat.com) to know more information about Unity 5 compatible version.
:::

#### Fix

* An issue cause Mac Editor can not be used in 64bit environment.
* Removed source folder and zipped them to avoid a potential compile error.
* Removed a deprecated method to avoid compile error.

---

### 1.8.1 (9 Nov, 2014)

#### Fix

* Improve performance for Mac Editor a lot.
* An issue which cause spinner can not hide when the webview removed by code from Unity.
* Update Android build target.

---

### 1.8.0 (5 Nov, 2014)

#### Fix

Add: A method to change the user agent of the webview. Now you can use the SetUserAgent method to set customized user agent for the webview.
Add: The Mac Editor now supports for Xcode 6.1 and OSX Yosemite.

* An orientation issue when you set game as a landscape one, while the webview can not rotated to portrait on iOS 8.

---

### 1.7.3 (8 Oct, 2014)

#### Fix

* A issue which may cause crash on some Android device with system version 2.x.

---

### 1.7.2

#### Add

* An API for Android to enable wide view port support. Call SetUseWideViewPort(true) before loading any webpage containing viewport meta tag on Android to make it avaliable.

#### Fix

* Now the viewport support for Android is disabled by default. Use the new added SetUseWideViewPort API to enable it if you are using viewport tag in your webpage.

---

### 1.7.1 (7 Oct, 2014)

#### Add

* Insets setting for portrait and landscape mode. You can now set different portrait and landscape inset easier. If you need show your web content in both orientation, you might want to use the new `InsetsForScreenOreitation` event. See demo code for the usage.

#### Fix

* Viewport issue for some Android devices. Now the html's viewport should work correctly.
* Webview truncated issue when the y position set to 0 on some Android devices. This is an advanced fix continuing for version 1.6.1.
* An issue which causes auto-rotation not work on iOS 8 in some situation.
* The position of toolbar and spinner should be correct on iOS 8 now.
* A build error which stops the exported project compiling in Xcode 5 and iOS 7 SDK.

---

### 1.7.0 (26 Sep, 2014)

::: danger
This version dropped support for Unity 4.1.4. Now UniWebView starts from 4.2.2. And 1.7.x will be the last versions support Unity version below 4.4 (because there is a compile error in iOS 8 SDK in the exported Xcode project, which is annoying and makes the development harder).
:::

#### Add

* UniWebViewHelper.screenHeight and UniWebViewHelper.screenWidth. You can retrive the screen size in "point" instead of "pixel" with these two new API. See the migrate guide below for more.
* "tel:" link support for Android. Now you can open the telephone UI by a link like "tel:12345678".
* An example in demo file, to show how to handle the auto-orientation, with keeping the layout unchanged in both portrait and landscope.
* iOS 8 and 5.5 inch iPhone 6 Plus support.

#### Fix

* The auto-orientation issue on iOS 8. Now it works for both iOS 8 SDK.
* Trim the url leading and trailing spaces, making the behaviors the same for iOS and Android.

#### Deprecate

* UniWebViewHelper.RunningOnRetinaIOS() is now deprecated due to 3x size of iPhone 6 Plus. Use UniWebViewHelper.screenHeight and UniWebViewHelper.screenWidth to decide the insets for retina display now. See the migrate guide below for more.

#### Remove

* Remove the long-ago deprecated Dismiss().
* Remove support for Unity 4.1.4.

> Migrate Guide - If you are using UniWebViewHelper.RunningOnRetinaIOS() to decide the insets of UniWebView, you may want to look at the Migrate Guide:

Due to introduction of iPhone 6 Plus and its new @3x scale, the old way would not work well. Now, all screen-size-based insets calculation for UniWebView should NOT use RunningOnRetinaIOS anymore. Instead you should use UniWebViewHelper.screenHeight and UniWebViewHelper.screenWidth and specify the point directly. For example, if you want to show a webview taking the upper half of screen, before version 1.7.0, it might be something like this:

    int uiFactor = UniWebViewHelper.RunningOnRetinaIOS() ? 2 : 1;
    int bottomInset = Screen.height / ( 2 * uiFactor );
    _webView.insets = new UniWebViewEdgeInsets(0,0,bottomInset,0);

Right now, with the new API, you can just use this:

    int bottomInset = (int)(UniWebViewHelper.screenHeight * 0.5f);
    _webView.insets = new UniWebViewEdgeInsets(0,0,bottomInset,0);

No more ui factor and device-specified calculation needed.

To make sure you can notice this change, the usage of RunningOnRetinaIOS() will cause an error now. You can just delete all usage of that, and change your insets code like the example above.

---

### 1.6.1 (2 Sep, 2014)

#### Add

* A precompiled package for Vuforia's QCARPlayerNativeActivity activity. You can find it under the /UniWebView/Source/ThirdPartyJar folder.

#### Fix

* An issue which cause the webview can not be full screen on some Android tablet.
* Drop support for Android 2.2.x and earlier. So you can recompile the jar file with Android SDK 18 and later.

---

### 1.5.4 (29 Jul, 2014)

#### Fix

* Keyboard overlap issue when text filed on Android. Now the web content will be scrolled up automatically.

---

### 1.5.3 (16 Jul, 2014)

#### Fix

* A bug which causes an unexpected behavior when starting from other activities in Android.

---

### 1.5.2 (19 Jun, 2014)

#### Fix

* A bug when setting insets in some Android devices.

---

### 1.5.0 (1 Jun, 2014)

#### Add

* New download feature support for Android.
* Compatibility with Unity 4.5.

#### Fix

* Update Android recomplie guide for Unity 4.5.
* Rotation problem when playing full screen video on iOS. Now the layout can work perfectly.

---

### 1.4.2 (30 Apr, 2014)

#### Fix

* Fix errors when export from Unity for the platforms not supported in UniWebView. Be causion, this fix does not make UniWebView supporting for other platforms than iOS and Android. It just disabled UniWebView code and suspended the errors.

---

### 1.4.1 (31 Mar, 2014)

#### Add

* An API for adding some javascript snippet to Android and iOS in runtime. Now you can dynamically add js code to your page when the game is running, instead of defining it in the web page before loading the web content.
* API for cleaning a specified cookie, instead of cleaning them all.
* A key down event on Android while the webview is actived.

#### Fix

* Improved the performance for Mac Editor.
* Some other small issues.

> The parameters of LoadBeginDelegate is changed from 1.4.0. There is now an url parameter in the delegate. If you are using webview.currentUrl in the corresponding event, now please use this parameter intead of that. The currentUrl is updated only when web page loading finished or failed now.

---

### 1.4.0 (12 Mar, 2014)

#### Add

* Add an API for clean cookies for the webview.
* Add an API for stop current loading of the webview.

#### Fix

* Change the logic when loading webview, now UniWebView will send all loading begin event back to Unity.
* An issue which cause Youtube video can not be automatically played in iOS.

---

### 1.3.0 (11 Feb, 2014)

#### Add

#### Fix

New: Customize url schemes. You can now add and remove the url schemes UniWebView is using. By using new API of AddUrlScheme and RemoveUrlScheme, you can now integrate some more third party service eaiser.
New: Support for location service of Android.

* Some memory leak on iOS.

---

### 1.2.6 (21 Jan, 2014)

#### Add

* Add bouncesEnable propert to Android. You can set the bounce effect for Android as well. Default is false, which means not show the bounce effect on Android, and not bounce the webview on iOS.
* Add a refresh button in iOS's built-in tool bar.
* Add zoom feature to both iOS and Android. Now you can set the zoomEnable property to let your users zoom-in or zoom-out webview by a pinch gesture.
* Add Reload method, you can reload the current page now from code.

#### Fix

* Ignore the ssl check in Andoird, it is useful when you are testing your page with a certification you create yourself. Otherwise, you can not get pass from it.
* Log order issue on Android. Now the create and show events can be logged correctly.

---

### 1.2.5 (9 Jan, 2014)

#### Add

* Add a bouncesEnable property to control the bounces when scroll the webview in iOS. The bounces effect is turned off by default, if you want it back, set this property to true.

#### Fix

* A problem causing the OnComplete event can not be raised correctly when load fails.
* A problem which causes the name of webview game object being appended instead of replaced.
* A problem with hiding the webview. Now the webview area could be clicked through correctly when hidden.

#### Deprecate

* `Dismiss()` is now deprecated, use `Hide()` to hide the web view.

---

### 1.2.3 (25 Dec, 2013)

#### Add

* Support for "mailto" link for Android.

#### Fix

* A potential issue which may cause unity scene turning to black when switching back to game while webview opened. It might shows on some old Android devices.
* A problem which causes the source can not get compiled in Mono Develop.
* Update the Dismiss method to let it work as designed in Mac Editor and Android devices. It now behavious as making the webview hidden instead of close and destory it. You can use Show to make it visible again after dismissing it.

---

### 1.2.2 (18 Dec, 2013)

#### Add

* An option to disable the back button on Android device. It is useful when you don't want users dismiss the web view (for example you want to use web view as part of your UI).

#### Fix

* Change the local file loading back to previous because the new method can not handle base url very well.
* An issue which caused current url can not be retrived properly for Android.

---

### 1.2.1 (12 Dec, 2013)

#### Add

* A property to get the current page url. You can know on which page now when you using UniWebView.
* An event when the page begins to load. Not only the first page load from code, but also all the page loading by clicking links on the page.

#### Fix

* A mistake in transparent setting method in Android.
* Background can be setting to transparent now in Android, from 2.x to 4.x (and later I think).
* Make the local html load easier to use. Now just set the correct streaming asset path, and load. No more platform-specified step.

---

### 1.2.0 (7 Dec, 2013)

#### Add

* A spinner is added to webview, which will show when the webpage is under loading. The spinner is on by default, if you don't want it, you can set it not show by the SetShowSpinnerWhenLoading method of UniWebView.
* We also added a text label in the spinner, to show some information to your users.
* Added an OnEvalJavaScriptFinished event for eval js script. Now EvaluatingJavaScript will not return a value, but raise the event. So you can use the same API for both iOS and Android.
* Local file load for Android 4.x.

#### Fix

* Rewrote all the Android native code, to make it working properly under Unity 4.3.
* A problem which may cause unity game scene disappeared when game goes to background with webview opened in some old Android devices.

---

### 1.1.7 (2 Dec, 2013)

#### Add

* Support for loading local file in Android.

#### Fix

* A bug in the demo which causes an exception when you click the web page of the demo.
* A problem which causes the plugin not working sometimes in Editor.

---

### 1.1.6 (28 Nov, 2013)

#### Fix

* A problem the file input tag can not trigger a file chooser in Android.

---

### 1.1.3 (21 Nov, 2013)

#### Add

* Add a method to the webview for loading from a html string.
* Add some demo script to tell demostrate how to load a local html file.

#### Fix

* A problem when users close the webview with soft keyboard showing in Android.

---

### 1.1.2 (16 Nov, 2013)

#### Add

* A better and more intersting demo scene to explain how to make the UniWebView work.

#### Fix

* A problem which cause the keyboard not working on Android.
* A rotation issue which cause the webview can not rotate correctly in iOS.

---

### 1.1.0 (9 Nov, 2013)

#### Add

* Add Youtube video playing support for Android.
* Add a tool bar to iOS.
* The users can use back button (Android) or a native toolbar (iOS) to close the webview now.
* Add an event to control whether the webview can be closed or not.

#### Fix

* Update the demo scene to make it clearer and more interesting.

---

### 1.0.1 (1 Nov, 2013)

Init release


