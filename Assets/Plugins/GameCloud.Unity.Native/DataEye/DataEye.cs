using UnityEngine;
using System.Collections;

public class DataEye
{
    //-------------------------------------------------------------------------
    /// <summary>
    /// DataEye统计初始化
    /// </summary>
    /// <param name="app_id"></param>
    /// <param name="channel_id"></param>
    /// <param name="report_mode"></param>
    public static void initWithAppIdAndChannelId(string app_id, string channel_id, bool open_adtracking = true, DCReportMode report_mode = DCReportMode.DC_AFTER_LOGIN)
    {
        //if (open_adtracking)
        //{
        //    DCAgent.openAdTracking();
        //}

        DCAgent.setReportMode(report_mode);
        DCAgent.getInstance().initWithAppIdAndChannelId(app_id, channel_id);        
    }

    //-------------------------------------------------------------------------
    /// <summary>
    /// DataEye登陆 
    /// </summary>
    /// <param name="acc_id"></param>
    public static void login(string acc_id)
    {
        DCAccount.login(acc_id);
    }

    //-------------------------------------------------------------------------
    /// <summary>
    /// DataEye登出
    /// </summary>
    public static void logout()
    {
        DCAccount.logout();
    }
}
