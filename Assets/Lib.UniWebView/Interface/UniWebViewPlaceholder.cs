#if UNITY_EDITOR_WIN || (!UNITY_EDITOR_OSX && !UNITY_STANDALONE_OSX && !UNITY_IOS && !UNITY_ANDROID)

using UnityEngine;

public class UniWebViewInterface {
    
    private static bool alreadyLoggedWarning = false;

    public static void SetLogLevel(int level) { CheckPlatform(); }
    public static void Init(string name, int x, int y, int width, int height) { CheckPlatform(); }
    public static void Destroy(string name) { CheckPlatform(); }
    public static void Load(string name, string url, bool skipEncoding) { CheckPlatform(); }
    public static void LoadHTMLString(string name, string html, string baseUrl, bool skipEncoding) { CheckPlatform(); }
    public static void Reload(string name) { CheckPlatform(); }
    public static void Stop(string name) { CheckPlatform(); }
    public static string GetUrl(string name) { CheckPlatform(); return ""; }
    public static void SetFrame(string name, int x, int y, int width, int height) { CheckPlatform(); }
    public static void SetPosition(string name, int x, int y) { CheckPlatform(); }
    public static void SetSize(string name, int width, int height) { CheckPlatform(); }
    public static bool Show(string name, bool fade, int edge, float duration, string identifier) { CheckPlatform(); return false; }
    public static bool Hide(string name, bool fade, int edge, float duration, string identifier) { CheckPlatform(); return false; }
    public static bool AnimateTo(string name, int x, int y, int width, int height, float duration, float delay, string identifier) { CheckPlatform(); return false; }
    public static void AddJavaScript(string name, string jsString, string identifier) { CheckPlatform(); }
    public static void EvaluateJavaScript(string name, string jsString, string identifier) { CheckPlatform(); }
    public static void AddUrlScheme(string name, string scheme) { CheckPlatform(); }
    public static void RemoveUrlScheme(string name, string scheme) { CheckPlatform(); }
    public static void AddSslExceptionDomain(string name, string domain) { CheckPlatform(); }
    public static void RemoveSslExceptionDomain(string name, string domain) { CheckPlatform(); }
    public static void SetHeaderField(string name, string key, string value) { CheckPlatform(); }
    public static void SetUserAgent(string name, string userAgent) { CheckPlatform(); }
    public static string GetUserAgent(string name) { CheckPlatform(); return ""; }
    public static void SetAllowAutoPlay(bool flag) { CheckPlatform(); }
    public static void SetAllowInlinePlay(bool flag) { CheckPlatform(); }
    public static void SetAllowJavaScriptOpenWindow(bool flag) { CheckPlatform(); }
    public static void SetJavaScriptEnabled(bool flag) { CheckPlatform(); }
    public static void CleanCache(string name) { CheckPlatform(); }
    public static void ClearCookies() { CheckPlatform(); }
    public static void SetCookie(string url, string cookie, bool skipEncoding) { CheckPlatform(); }
    public static string GetCookie(string url, string key, bool skipEncoding) { CheckPlatform(); return ""; }
    public static void ClearHttpAuthUsernamePassword(string host, string realm) { CheckPlatform(); }
    public static void SetBackgroundColor(string name, float r, float g, float b, float a) { CheckPlatform(); }
    public static void SetWebViewAlpha(string name, float alpha) { CheckPlatform(); }
    public static float GetWebViewAlpha(string name) { CheckPlatform(); return 0.0f; }
    public static void SetShowSpinnerWhileLoading(string name, bool show) { CheckPlatform(); }
    public static void SetSpinnerText(string name, string text) { CheckPlatform(); }
    public static bool CanGoBack(string name) { CheckPlatform(); return false; }
    public static bool CanGoForward(string name) { CheckPlatform(); return false; }
    public static void GoBack(string name) { CheckPlatform(); }
    public static void GoForward(string name) { CheckPlatform(); }
    public static void SetOpenLinksInExternalBrowser(string name, bool flag) { CheckPlatform(); }
    public static void SetHorizontalScrollBarEnabled(string name, bool enabled) { CheckPlatform(); }
    public static void SetVerticalScrollBarEnabled(string name, bool enabled) { CheckPlatform(); }
    public static void SetBouncesEnabled(string name, bool enabled) { CheckPlatform(); }
    public static void SetZoomEnabled(string name, bool enabled) { CheckPlatform(); }
    public static void SetShowToolbar(string name, bool show, bool animated, bool onTop, bool adjustInset) { CheckPlatform(); }
    public static void SetToolbarDoneButtonText(string name, string text) { CheckPlatform(); }
    public static void SetWebContentsDebuggingEnabled(bool enabled) { CheckPlatform(); }
    public static void Print(string name) { CheckPlatform(); }
    
    public static void CheckPlatform() {
        if (!alreadyLoggedWarning) {
            alreadyLoggedWarning = true;
            Debug.LogWarning("UniWebView only supports iOS/Android/macOS Editor. You current platform " + Application.platform + " is not supported.");
        }
    }
}
#endif