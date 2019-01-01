//
//  UniWebViewHelper.cs
//  Created by Wang Wei(@onevcat) on 2017-04-11.
//
//  This file is a part of UniWebView Project (https://uniwebview.com)
//  By purchasing the asset, you are allowed to use this code in as many as projects 
//  you want, only if you publish the final products under the name of the same account
//  used for the purchase. 
//
//  This asset and all corresponding files (such as source code) are provided on an 
//  “as is” basis, without warranty of any kind, express of implied, including but not 
//  limited to the warranties of merchantability, fitness for a particular purpose, and 
//  noninfringement. In no event shall the authors or copyright holders be liable for any 
//  claim, damages or other liability, whether in action of contract, tort or otherwise, 
//  arising from, out of or in connection with the software or the use of other dealing in the software.
//
using UnityEngine;
using System.IO;

/// <summary>
/// Provides some helper utility methods for UniWebView.
/// </summary>
public class UniWebViewHelper {
    /// <summary>
    /// Get the local streaming asset path for a given file path related to the StreamingAssets folder.
    /// 
    /// This method will help you to concat a URL string for a file under your StreamingAssets folder for different platforms.
    /// <param name="path">The relative path to the Assets/StreamingAssets of your file. 
    /// For example, if you placed a html file under Assets/StreamingAssets/www/index.html, you should pass `www/index.html` as parameter.
    /// </param>
    /// <returns>The path you could use as the url for the web view.</returns>
    public static string StreamingAssetURLForPath(string path)
    {
#if (UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX || UNITY_IOS) && !UNITY_EDITOR_WIN
        return Path.Combine("file://" + Application.streamingAssetsPath, path);
#elif UNITY_ANDROID
        return Path.Combine("file:///android_asset/", path);
#else
        UniWebViewLogger.Instance.Critical("The current build target is not supported.");
        return string.Empty;
#endif
    }

    /// <summary>
    /// Get the local persistent data path for a given file path related to the data folder of your host app.
    /// 
    /// This method will help you to concat a URL string for a file under you stored in the `persistentDataPath`.
    /// </summary>
    /// <param name="path">
    /// The relative path to the persistent data path of your file.
    /// </param>
    /// <returns>The path you could use as the url for the web view.</returns>
    public static string PersistentDataURLForPath(string path)
    {
        return Path.Combine("file://" + Application.persistentDataPath, path);
    }
}
