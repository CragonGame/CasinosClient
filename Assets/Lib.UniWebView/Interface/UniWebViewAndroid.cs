#if UNITY_ANDROID && !UNITY_EDITOR

using UnityEngine;

public class UniWebViewInterface {
    private static readonly AndroidJavaClass plugin;
    private static bool correctPlatform = Application.platform == RuntimePlatform.Android;
    
    static UniWebViewInterface() {
        var go = new GameObject("UniWebViewAndroidStaticListener");
        go.AddComponent<UniWebViewAndroidStaticListener>();
        plugin = new AndroidJavaClass("com.onevcat.uniwebview.UniWebViewInterface");
        CheckPlatform();
        plugin.CallStatic("prepare");
    }

    public static void SetLogLevel(int level) {
        CheckPlatform();
        plugin.CallStatic("setLogLevel", level); 
    }

    public static void Init(string name, int x, int y, int width, int height) {
        CheckPlatform();
        plugin.CallStatic("init", name, x, y, width, height);
    }

    public static void Destroy(string name) {
        CheckPlatform();
        plugin.CallStatic("destroy", name);
    }

    public static void Load(string name, string url, bool skipEncoding) {
        CheckPlatform();
        plugin.CallStatic("load", name, url);
    }

    public static void LoadHTMLString(string name, string html, string baseUrl, bool skipEncoding) {
        CheckPlatform();
        plugin.CallStatic("loadHTMLString", name, html, baseUrl);
    }

    public static void Reload(string name) {
        CheckPlatform();
        plugin.CallStatic("reload", name);
    }

    public static void Stop(string name) {
        CheckPlatform();
        plugin.CallStatic("stop", name);
    }

    public static string GetUrl(string name) {
        CheckPlatform();
        return plugin.CallStatic<string>("getUrl", name);
    }

    public static void SetFrame(string name, int x, int y, int width, int height) {
        CheckPlatform();
        plugin.CallStatic("setFrame", name, x, y, width, height);
    }

    public static void SetPosition(string name, int x, int y) {
        CheckPlatform();
        plugin.CallStatic("setPosition", name, x, y);
    }

    public static void SetSize(string name, int width, int height) {
        CheckPlatform();
        plugin.CallStatic("setSize", name, width, height);
    }

    public static bool Show(string name, bool fade, int edge, float duration, string identifier) {
        CheckPlatform();
        return plugin.CallStatic<bool>("show", name, fade, edge, duration, identifier);
    }

    public static bool Hide(string name, bool fade, int edge, float duration, string identifier) {
        CheckPlatform();
        return plugin.CallStatic<bool>("hide", name, fade, edge, duration, identifier);
    }

    public static bool AnimateTo(string name, int x, int y, int width, int height, float duration, float delay, string identifier) {
        CheckPlatform();
        return plugin.CallStatic<bool>("animateTo", name, x, y, width, height, duration, delay, identifier);
    }

    public static void AddJavaScript(string name, string jsString, string identifier) {
        CheckPlatform();
        plugin.CallStatic("addJavaScript", name, jsString, identifier);
    }

    public static void EvaluateJavaScript(string name, string jsString, string identifier) {
        CheckPlatform();
        plugin.CallStatic("evaluateJavaScript", name, jsString, identifier);
    }

    public static void AddUrlScheme(string name, string scheme) {
        CheckPlatform();
        plugin.CallStatic("addUrlScheme", name, scheme);
    }

    public static void RemoveUrlScheme(string name, string scheme) {
        CheckPlatform();
        plugin.CallStatic("removeUrlScheme", name, scheme);
    }

    public static void AddSslExceptionDomain(string name, string domain) {
        CheckPlatform();
        plugin.CallStatic("addSslExceptionDomain", name, domain);
    }

    public static void RemoveSslExceptionDomain(string name, string domain) {
        CheckPlatform();
        plugin.CallStatic("removeSslExceptionDomain", name, domain);
    }

    public static void AddPermissionTrustDomain(string name, string domain) {
        CheckPlatform();
        plugin.CallStatic("addPermissionTrustDomain", name, domain);
    }

    public static void RemovePermissionTrustDomain(string name, string domain) {
        CheckPlatform();
        plugin.CallStatic("removePermissionTrustDomain", name, domain);
    }

    public static void SetHeaderField(string name, string key, string value) {
        CheckPlatform();
        plugin.CallStatic("setHeaderField", name, key, value);
    }

    public static void SetUserAgent(string name, string userAgent) {
        CheckPlatform();
        plugin.CallStatic("setUserAgent", name, userAgent);
    }

    public static string GetUserAgent(string name) {
        CheckPlatform();
        return plugin.CallStatic<string>("getUserAgent", name);
    }

    public static void SetAllowAutoPlay(bool flag) {
        CheckPlatform();
        plugin.CallStatic("setAllowAutoPlay", flag);
    }

    public static void SetAllowJavaScriptOpenWindow(bool flag) {
        CheckPlatform();
        plugin.CallStatic("setAllowJavaScriptOpenWindow", flag);
    }

    public static void SetJavaScriptEnabled(bool enabled) {
        CheckPlatform();
        plugin.CallStatic("setJavaScriptEnabled", enabled);
    }

    public static void CleanCache(string name) {
        CheckPlatform();
        plugin.CallStatic("cleanCache", name);
    }

    public static void ClearCookies() {
        CheckPlatform();
        plugin.CallStatic("clearCookies");
    }

    public static void SetCookie(string url, string cookie, bool skipEncoding) {
        CheckPlatform();
        plugin.CallStatic("setCookie", url, cookie);
    }

    public static string GetCookie(string url, string key, bool skipEncoding) {
        CheckPlatform();
        return plugin.CallStatic<string>("getCookie", url, key);
    }

    public static void ClearHttpAuthUsernamePassword(string host, string realm) {
        CheckPlatform();
        plugin.CallStatic("clearHttpAuthUsernamePassword", host, realm);
    }

    public static void SetBackgroundColor(string name, float r, float g, float b, float a) {
        CheckPlatform();
        plugin.CallStatic("setBackgroundColor", name, r, g, b, a);
    }

    public static void SetWebViewAlpha(string name, float alpha) {
        CheckPlatform();
        plugin.CallStatic("setWebViewAlpha", name, alpha);
    }

    public static float GetWebViewAlpha(string name) {
        CheckPlatform();
        return plugin.CallStatic<float>("getWebViewAlpha", name);
    }

    public static void SetShowSpinnerWhileLoading(string name, bool show) {
        CheckPlatform();
        plugin.CallStatic("setShowSpinnerWhileLoading", name, show);
    }

    public static void SetSpinnerText(string name, string text) {
        CheckPlatform();
        plugin.CallStatic("setSpinnerText", name, text);
    }

    public static bool CanGoBack(string name) {
        CheckPlatform();
        return plugin.CallStatic<bool>("canGoBack", name);
    }

    public static bool CanGoForward(string name) {
        CheckPlatform();
        return plugin.CallStatic<bool>("canGoForward", name);
    }

    public static void GoBack(string name) {
        CheckPlatform();
        plugin.CallStatic("goBack", name);
    }
    public static void GoForward(string name) {
        CheckPlatform();
        plugin.CallStatic("goForward", name);
    }

    public static void SetOpenLinksInExternalBrowser(string name, bool flag) {
        CheckPlatform();
        plugin.CallStatic("setOpenLinksInExternalBrowser", name, flag);
    }

    public static void SetHorizontalScrollBarEnabled(string name, bool enabled) {
        CheckPlatform();
        plugin.CallStatic("setHorizontalScrollBarEnabled", name, enabled);
    }

    public static void SetVerticalScrollBarEnabled(string name, bool enabled) {
        CheckPlatform();
        plugin.CallStatic("setVerticalScrollBarEnabled", name, enabled);
    }

    public static void SetBouncesEnabled(string name, bool enabled) {
        CheckPlatform();
        plugin.CallStatic("setBouncesEnabled", name, enabled);
    }

    public static void SetZoomEnabled(string name, bool enabled) {
        CheckPlatform();
        plugin.CallStatic("setZoomEnabled", name, enabled);
    }

    public static void SetBackButtonEnabled(string name, bool enabled) {
        CheckPlatform();
        plugin.CallStatic("setBackButtonEnabled", name, enabled);
    }

    public static void SetUseWideViewPort(string name, bool use) {
        CheckPlatform();
        plugin.CallStatic("setUseWideViewPort", name, use);
    }

    public static void SetLoadWithOverviewMode(string name, bool overview) {
        CheckPlatform();
        plugin.CallStatic("setLoadWithOverviewMode", name, overview);
    }

    public static void SetImmersiveModeEnabled(string name, bool enabled) {
        CheckPlatform();
        plugin.CallStatic("setImmersiveModeEnabled", name, enabled);
    }

    public static void SetWebContentsDebuggingEnabled(bool enabled) {
        CheckPlatform();
        plugin.CallStatic("setWebContentsDebuggingEnabled", enabled);
    }

    public static void ShowWebViewDialog(string name, bool show) {
        CheckPlatform();
        plugin.CallStatic("showWebViewDialog", name, show);
    }

    public static void Print(string name) {
        CheckPlatform();
        plugin.CallStatic("print", name);
    }

    public static void CheckPlatform() {
        if (!correctPlatform) {
            throw new System.InvalidOperationException("Method can only be performed on Android.");
        }
    }
}
#endif