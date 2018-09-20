using UnityEngine;
using System.Collections;

public class AndroidPay : IPay
{
    //-------------------------------------------------------------------------
    public AndroidJavaClass mAndroidJavaClassPay;
    public AndroidJavaObject mAndroidJavaObjectPay;

    //-------------------------------------------------------------------------
    public AndroidPay()
    {
        mAndroidJavaClassPay = new AndroidJavaClass("com.Pay.Pay.Pay");        
    }

    //-------------------------------------------------------------------------
    public void payInit(string beecloud_id, string beecloud_secret, string wechat_id)
    {
        Debug.Log("wechat_id   "+ wechat_id);
        if (mAndroidJavaObjectPay == null)
        {
            mAndroidJavaObjectPay = mAndroidJavaClassPay.CallStatic<AndroidJavaObject>("Instance", "PayReceiver",beecloud_id,beecloud_secret,wechat_id);
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

    ////-------------------------------------------------------------------------
    //public void payWithChargeData(string charge_data, int pay_type)
    //{
    //    if (mAndroidJavaObjectPay != null)
    //    {
    //        mAndroidJavaClassPay.Call("payWithChargeData", charge_data);
    //    }
    //    else
    //    {
    //        Debug.LogError("AndroidJavaObject Is Null");
    //    }
    //}

    //-------------------------------------------------------------------------
    public void pay(string bill_title, int pay_type, int bill_totalfee, string bill_num, string buy_id, string url_scheme)
    {        
        if (mAndroidJavaObjectPay != null)
        {            
            mAndroidJavaClassPay.CallStatic("pay", bill_title, pay_type, bill_totalfee, bill_num, buy_id);
        }
    }
}

//-------------------------------------------------------------------------
public enum _eAndroidPay
{
    pay
}
