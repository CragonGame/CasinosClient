using UnityEngine;
using System.Collections;

//public enum _ePayType
//{
//    wx = 0,
//    alipay,
//    iap,
//}

//public enum _ePayOptionType
//{
//    QueryInventory,
//    PurchaseProduct,
//    ConsumeProduct,
//}

public interface IPay
{
    //-------------------------------------------------------------------------
    void payInit(string beecloud_id, string beecloud_secret, string wechat_id);

    //-------------------------------------------------------------------------
    void useTestMode(bool use_testmode);

    ////-------------------------------------------------------------------------
    //void payWithChargeData(string charge_data, int pay_type);

    //-------------------------------------------------------------------------
    void pay(string bill_title, int pay_type, int bill_totalfee, string bill_num, string buy_id, string url_scheme);
}