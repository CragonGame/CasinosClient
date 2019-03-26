using System.Collections.Generic;
using UnityEngine;

public enum _ePayType
{
    wx = 0,
    alipay,
    iap,
}

public enum _ePayOptionType
{
    QueryInventory,
    PurchaseProduct,
    ConsumeProduct,
}

public class Pay
{

#if UNITY_EDITOR || UNITY_STANDALONE
    //-------------------------------------------------------------------------
    public Pay()
    {
    }

    //-------------------------------------------------------------------------
    public void payInit(string beecloud_id, string beecloud_secret, string wechat_id)
    {
    }

    //-------------------------------------------------------------------------
    public void useTestMode(bool use_testmode)
    {
    }

    //-------------------------------------------------------------------------
    public void pay(string bill_title, _ePayType pay_type,
        int bill_totalfee, string bill_num, string buy_id, string url_scheme)
    {
    }

#elif UNITY_ANDROID
     //-------------------------------------------------------------------------
    public AndroidJavaClass mAndroidJavaClassPay;
    public AndroidJavaObject mAndroidJavaObjectPay;

    //-------------------------------------------------------------------------
    public Pay()
    {
        mAndroidJavaClassPay = new AndroidJavaClass("com.Pay.Pay.Pay");
    }

    //-------------------------------------------------------------------------
    public void payInit(string beecloud_id, string beecloud_secret, string wechat_id)
    {
        Debug.Log("wechat_id   " + wechat_id);
        if (mAndroidJavaObjectPay == null)
        {
            mAndroidJavaObjectPay = mAndroidJavaClassPay.CallStatic<AndroidJavaObject>("Instance", beecloud_id, beecloud_secret, wechat_id);
        }

    }

    //-------------------------------------------------------------------------
    public void useTestMode(bool use_testmode)
    {
        if (mAndroidJavaObjectPay != null)
        {
            mAndroidJavaClassPay.CallStatic("useTestMode", use_testmode);
        }
    }

    //-------------------------------------------------------------------------
    public void pay(string bill_title, int pay_type, int bill_totalfee, string bill_num, string buy_id, string url_scheme)
    {
        if (mAndroidJavaObjectPay != null)
        {
            mAndroidJavaClassPay.CallStatic("pay", bill_title, pay_type, bill_totalfee, bill_num, buy_id);
        }
    }
#elif UNITY_IPHONE || UNITY_IOS

    //-------------------------------------------------------------------------
    public Pay()
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
#endif

}