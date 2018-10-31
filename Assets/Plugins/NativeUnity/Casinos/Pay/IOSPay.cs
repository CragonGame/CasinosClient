using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

#if UNITY_IPHONE
public class IOSPay : IPay
{
    //-------------------------------------------------------------------------
    public IOSPay()
    {
    }

    //-------------------------------------------------------------------------
    public void payInit(string beecloud_id, string beecloud_secret, string wechat_id)
    {
        payInitIos(beecloud_id,beecloud_secret,wechat_id);
    }

     //-------------------------------------------------------------------------
    public void useTestMode(bool use_testmode)
    {
       useTestModeIos(use_testmode);
    }

    ////-------------------------------------------------------------------------
    //public void payWithChargeData(string charge_data, int pay_type)
    //{
    //    //payIos(charge_data,pay_type);
    //}

    //-------------------------------------------------------------------------
    public void pay(string bill_title, int pay_type, int bill_totalfee, string bill_num, string buy_id,string url_scheme)
    {
        payIos(bill_title,pay_type,bill_totalfee,bill_num,buy_id,url_scheme);
    }

#region DllImport
        //-------------------------------------------------------------------------
        [DllImport("__Internal")]
        private static extern void payInitIos(string beecloud_id, string beecloud_secret, string wechat_id);
        //-------------------------------------------------------------------------
        [DllImport("__Internal")]
        private static extern void useTestModeIos(bool use_testmode);
        ////-------------------------------------------------------------------------
        //[DllImport("__Internal")]
        //private static extern void payIos(string charge_data, int pay_type);
        //-------------------------------------------------------------------------
        [DllImport("__Internal")]
        private static extern void payIos(string bill_title, int pay_type, int bill_totalfee, string bill_num, string buy_id,string url_scheme);
#endregion
}
#endif