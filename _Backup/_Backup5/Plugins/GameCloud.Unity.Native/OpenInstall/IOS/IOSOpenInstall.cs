using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

#if UNITY_IOS
public class IOSOpenInstall : IOpenInstall
{
    //-------------------------------------------------------------------------
    public IOSOpenInstall()
    {
        GetInstallData();
    }

    //-------------------------------------------------------------------------
    public void GetWakeUp()
    {
    }

    #region DllImport
    [DllImport("__Internal")]
    private static extern void GetInstallData();
    #endregion
}
#endif