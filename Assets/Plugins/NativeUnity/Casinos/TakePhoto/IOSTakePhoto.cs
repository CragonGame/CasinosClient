using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

#if UNITY_IPHONE
public class IOSTakePhoto : INativeTakePhoto
{
    //-------------------------------------------------------------------------
    public void takeNewPhoto(int photo_width, int photo_height, string photo_name, string photo_finalpath)
    {
        takeNewPhoto_ios("NativeReceiver", "getPicSuccess",
            "getPicFail", photo_width, photo_height, photo_name);
    }

    //-------------------------------------------------------------------------
    public void takeExistPhoto(int photo_width, int photo_height, string photo_name, string photo_finalpath)
    {
        takeExistPhoto_ios("NativeReceiver", "getPicSuccess",
            "getPicFail", photo_width, photo_height, photo_name);
    }

    //-------------------------------------------------------------------------
#region DllImport
    [DllImport("__Internal")]
    private static extern void takeNewPhoto_ios(string msg_recivername, string take_photosuccessmsgname,
       string take_photofailmsgname, int photo_width, int photo_height, string store_photopath);
    [DllImport("__Internal")]
    private static extern void takeExistPhoto_ios(string msg_recivername, string take_photosuccessmsgname,
        string take_photofailmsgname, int photo_width, int photo_height, string store_photopath);
#endregion
}
#endif