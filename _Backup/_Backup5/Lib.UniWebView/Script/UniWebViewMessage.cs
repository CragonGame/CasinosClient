//
//  UniWebViewMessage.cs
//  Created by Wang Wei(@onevcat) on 2017-05-12.
//
//  This file is a part of UniWebView Project (https://uniwebview.com)
//  By purchasing the asset, you are allowed to use this code in as many as projects 
//  you want, only if you publish the final products under the name of the same account
//  used for the purchase. 
//
//  This asset and all corresponding files (such as source code) are provided on an 
//  “as is” basis, without warranty of any kind, express of implied, including but not l
//  imited to the warranties of merchantability, fitness for a particular purpose, and 
//  noninfringement. In no event shall the authors or copyright holders be liable for any 
//  claim, damages or other liability, whether in action of contract, tort or otherwise, 
//  arising from, out of or in connection with the software or the use of other dealing in the software.
//
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A structure represents a message from webview.
/// </summary>
public struct UniWebViewMessage {
    /// <summary>
    /// Gets the raw message. It is the original url which initialized this message.
    /// </summary>
    public string RawMessage {get; private set;}

    /// <summary>
    /// The url scheme of this UniWebViewMessage. "uniwebview" was added to message scheme list
    /// by default. You can add your own scheme by using `UniWebView.AddUrlScheme`.
    /// </summary>
    public string Scheme {get; private set;}

    /// <summary>
    /// The path of this UniWebViewMessage.
    /// This will be the decoded value for the path of original url.
    /// </summary>
    public string Path {get; private set;}

    /// <summary>
    /// The arguments of this UniWebViewMessage.
    ///
    /// When received url "uniwebview://yourPath?param1=value1&param2=value2", 
    /// the args is a Dictionary with: Args["param1"] = value1, Args["param2"] = value2
    /// 
    /// Both the key and valud will be url decoded from the original url.
    /// </summary>
    public Dictionary<string, string> Args{get; private set;}

    /// <summary>
    /// Initializes a new instance of the `UniWebViewMessage` struct.
    /// </summary>
    /// <param name="rawMessage">Raw message which will be parsed to a UniWebViewMessage.</param>
    public UniWebViewMessage(string rawMessage): this() {
        UniWebViewLogger.Instance.Debug("Try to parse raw message: " + rawMessage);
        this.RawMessage = WWW.UnEscapeURL(rawMessage);
        
        string[] schemeSplit = rawMessage.Split(new string[] {"://"}, System.StringSplitOptions.None);
        if (schemeSplit.Length >= 2) {
            this.Scheme = schemeSplit[0];
            UniWebViewLogger.Instance.Debug("Get scheme: " + this.Scheme);

            string pathAndArgsString = "";
            int index = 1;
            while (index < schemeSplit.Length) {
                pathAndArgsString = string.Concat(pathAndArgsString, schemeSplit[index]);
                index++;
            }
            UniWebViewLogger.Instance.Verbose("Build path and args string: " + pathAndArgsString);
            
            string[] split = pathAndArgsString.Split("?"[0]);
            
            this.Path = WWW.UnEscapeURL(split[0].TrimEnd('/'));
            this.Args = new Dictionary<string, string>();
            if (split.Length > 1) {
                foreach (string pair in split[1].Split("&"[0])) {
                    string[] elems = pair.Split("="[0]);
                    if (elems.Length > 1) {
                        var key = WWW.UnEscapeURL(elems[0]);
                        if (Args.ContainsKey(key)) {
                            var existingValue = Args[key];
                            Args[key] = existingValue + "," + WWW.UnEscapeURL(elems[1]);
                        } else {
                            Args[key] = WWW.UnEscapeURL(elems[1]);
                        }
                        UniWebViewLogger.Instance.Debug("Get arg, key: " + key + " value: " + Args[key]);
                    }
                }
            }
        } else {
            UniWebViewLogger.Instance.Critical("Bad url scheme. Can not be parsed to UniWebViewMessage: " + rawMessage);
        }
    }
}