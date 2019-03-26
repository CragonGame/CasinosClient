// /**
//  *　　　　　　　　┏┓　　　┏┓+ +
//  *　　　　　　　┏┛┻━━━┛┻┓ + +
//  *　　　　　　　┃　　　　　　　┃ 　
//  *　　　　　　　┃　　　━　　　┃ ++ + + +
//  *　　　　　　 ████━████ ┃+
//  *　　　　　　　┃　　　　　　　┃ +
//  *　　　　　　　┃　　　┻　　　┃
//  *　　　　　　　┃　　　　　　　┃ + +
//  *　　　　　　　┗━┓　　　┏━┛
//  *　　　　　　　　　┃　　　┃　　　　　　　　　　　
//  *　　　　　　　　　┃　　　┃ + + + +
//  *　　　　　　　　　┃　　　┃　　　　Code is far away from bug with the animal protecting　　　　　　　
//  *　　　　　　　　　┃　　　┃ + 　　　　神兽保佑,代码无bug　　
//  *　　　　　　　　　┃　　　┃
//  *　　　　　　　　　┃　　　┃　　+　　　　　　　　　
//  *　　　　　　　　　┃　 　　┗━━━┓ + +
//  *　　　　　　　　　┃ 　　　　　　　┣┓
//  *　　　　　　　　　┃ 　　　　　　　┏┛
//  *　　　　　　　　　┗┓┓┏━┳┓┏┛ + + + +
//  *　　　　　　　　　　┃┫┫　┃┫┫
//  *　　　　　　　　　　┗┻┛　┗┻┛+ + + +
//  *
//  *
//  * ━━━━━━感觉萌萌哒━━━━━━
//  * 
//  * 说明：
//  *       用于Android 和IOS 平台的粘贴板的读写访问
//  *       文件列表：
//  *              Android:
//  *               BlankOperationClipboard.jar 文件一个
//  *              IOS:
//  *               BlankOperationClipboard.h 文件一个
//  *               BlankOperationClipboard.mm 文件一个
//  * 文件名：BlankOperationClipboard.cs
//  * 创建时间：2016年07月14日 
//  * 创建人：Blank Alian
//  */
using UnityEngine;

#if UNITY_IOS
using System.Runtime.InteropServices;
#endif


public class BlankOperationClipboard : MonoBehaviour
{
#if UNITY_EDITOR || UNITY_STANDALONE

    // 获取粘贴板的值
    public string GetValue()
    {
        return "";
    }

    // 设置粘贴板的值
    public void SetValue(string text)
    {

    }

#elif UNITY_ANDROID
    
    public AndroidJavaClass mAndoridJavaClassNativeOperation;
    public AndroidJavaObject mAndroidClipboard;
    public BlankOperationClipboard()
    {
        mAndoridJavaClassNativeOperation = new AndroidJavaClass("com.Native.Clipboardoperation.NativeOperation");
        if (mAndroidClipboard == null)
        {
            mAndroidClipboard = mAndoridJavaClassNativeOperation.CallStatic<AndroidJavaObject>("Instantce");
        }
    }

    // 获取粘贴板的值
    public string GetValue()
    {
        return mAndroidClipboard.CallStatic<string>("GetClipBoard");
    }

    // 设置粘贴板的值
    public void SetValue(string text)
    {
        mAndroidClipboard.CallStatic("SetClipBoard", text);
    }

#elif UNITY_IPHONE || UNITY_IOS

    public string GetValue()
    {
        return GetClipBoard();
    }

    public void SetValue(string text)
    {
        SetClipBoard (text);
    }

    #region DllImport
	[DllImport("__Internal")]
	private static extern string GetClipBoard ();
	
	[DllImport("__Internal")]
	private static extern void SetClipBoard (string text);
    #endregion
#endif

}
