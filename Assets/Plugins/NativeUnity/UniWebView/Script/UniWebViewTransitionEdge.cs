//
//  UniWebViewTransitionEdge.cs
//  Created by Wang Wei(@onevcat) on 2017-05-04.
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

/// <summary>
/// An enum to identify transition edge from or to when the UniWebView
/// transition happens. You can specify an edge in Show() or Hide() methods of web view.
/// </summary>
public enum UniWebViewTransitionEdge
{
    /// <summary>
    /// No transition when showing or hiding.
    /// </summary>
    None = 0,
    /// <summary>
    /// Transit the web view from/to top.
    /// </summary>
    Top,
    /// <summary>
    /// Transit the web view from/to left.
    /// </summary>
    Left,
    /// <summary>
    /// Transit the web view from/to bottom.
    /// </summary>
    Bottom,
    /// <summary>
    /// Transit the web view from/to right.
    /// </summary>
    Right
}
