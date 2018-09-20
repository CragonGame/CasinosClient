using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

#if UNITY_IPHONE
public class IOSFun : INativeFun
{    
    //-------------------------------------------------------------------------
    public IOSFun()
    {       
    }

    //-------------------------------------------------------------------------
    public string getCountryCode()
    {        
        return getCountryCodeIos();
    }

    //-------------------------------------------------------------------------
    public void installAPK(string file_path)
    {      
    }

#region DllImport
        //-------------------------------------------------------------------------
        [DllImport("__Internal")]
        private static extern string getCountryCodeIos();
#endregion
}
#endif