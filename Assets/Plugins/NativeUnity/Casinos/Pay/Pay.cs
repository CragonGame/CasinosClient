using System.Collections.Generic;
using UnityEngine;

public class Pay
{
    //-------------------------------------------------------------------------
    static Pay mPay;
    static IPay mIPay;
    string mMsg = "";
    const string OPENIAB_EVENT_RECEIVER = "OpenIABEventManager";

    //-------------------------------------------------------------------------
    public Pay()
    {
        // Listen to all events for illustration purposes
        //OpenIABEventManager.billingSupportedEvent += billingSupportedEvent;
        //OpenIABEventManager.billingNotSupportedEvent += billingNotSupportedEvent;
        //OpenIABEventManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
        //OpenIABEventManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
        //OpenIABEventManager.purchaseSucceededEvent += purchaseSucceededEvent;
        //OpenIABEventManager.purchaseFailedEvent += purchaseFailedEvent;
        //OpenIABEventManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
        //OpenIABEventManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;

#if UNITY_ANDROID && !UNITY_EDITOR
        mIPay = new AndroidPay();
        Debug.Log("AndroidPay::");
#elif UNITY_IOS
        mIPay = new IOSPay();
        Debug.Log("IOSPay::");
#else
        //Debug.LogError("Do not supported on this platform. ");
#endif
    }

    //-------------------------------------------------------------------------
    public static Pay Instant()
    {
        if (mPay == null)
        {
            mPay = new Pay();
        }

//#if UNITY_IOS
//        GameObject openiab_msg_receiver = GameObject.Find(OPENIAB_EVENT_RECEIVER);
//        if (openiab_msg_receiver == null)
//        {
//            openiab_msg_receiver = new GameObject(OPENIAB_EVENT_RECEIVER);
//            openiab_msg_receiver.AddComponent<OpenIABEventManager>();
//            GameObject.DontDestroyOnLoad(openiab_msg_receiver);
//        }
//#endif

        return mPay;
    }

    //-------------------------------------------------------------------------
    public static void payInit(string beecloud_id, string beecloud_secret, string wechat_id)
    {
        mIPay.payInit(beecloud_id, beecloud_secret, wechat_id);
    }

    //-------------------------------------------------------------------------
    public static void useTestMode(bool use_testmode)
    {
        mIPay.useTestMode(use_testmode);
    }

    ////-------------------------------------------------------------------------
    //public static void payWithChargeData(string buy_item_sku, string charge_data, _ePayType pay_type)
    //{
    //    if (pay_type == _ePayType.iap)
    //    {
    //        OpenIAB.purchaseProduct(buy_item_sku);
    //    }
    //    else
    //    {
    //        mIPay.payWithChargeData(charge_data, (int)pay_type);
    //    }
    //}

    //-------------------------------------------------------------------------
    public static void pay(string buy_item_sku, string bill_title, _ePayType pay_type,
        int bill_totalfee, string bill_num, string buy_id, string url_scheme)
    {
        if (pay_type == _ePayType.iap)
        {
            //OpenIAB.purchaseProduct(buy_item_sku);
        }
        else
        {
            mIPay.pay(bill_title, (int)pay_type, bill_totalfee, bill_num, buy_id, url_scheme);
        }
    }

    //-------------------------------------------------------------------------
    //public void initInventory(List<string> list_inventorysku)
    //{
    //    foreach (var i in list_inventorysku)
    //    {
    //        OpenIAB.mapSku(i, OpenIAB_iOS.STORE, i);
    //    }

    //    var options = new Options();
    //    OpenIAB.init(options);
    //}

    //-------------------------------------------------------------------------
    //private void billingSupportedEvent()
    //{
    //    //_isInitialized = true;
    //    Debug.Log("billingSupportedEvent");
    //}

    ////-------------------------------------------------------------------------
    //private void billingNotSupportedEvent(string error)
    //{
    //    Debug.Log("billingNotSupportedEvent: " + error);
    //}

    ////-------------------------------------------------------------------------
    //private void queryInventorySucceededEvent(Inventory inventory)
    //{
    //    Debug.Log("queryInventorySucceededEvent: " + inventory);
    //    if (inventory != null)
    //    {
    //        mMsg = inventory.ToString();
    //        mInventory = inventory;
    //    }

    //    PayReceiver.instance().PayResultIPA(_ePayOptionType.QueryInventory, true, inventory);
    //}

    ////-------------------------------------------------------------------------
    //private void queryInventoryFailedEvent(string error)
    //{
    //    Debug.Log("queryInventoryFailedEvent Failed: " + error);
    //    mMsg = error;

    //    PayReceiver.instance().PayResultIPA(_ePayOptionType.QueryInventory, false, mMsg);
    //}

    ////-------------------------------------------------------------------------
    //private void purchaseSucceededEvent(Purchase purchase)
    //{
    //    Debug.Log("purchaseSucceededEvent: " + purchase);
    //    mMsg = "PURCHASED Succeeded:" + purchase.ToString();

    //    PayReceiver.instance().PayResultIPA(_ePayOptionType.PurchaseProduct, true, purchase);
    //}

    ////-------------------------------------------------------------------------
    //private void purchaseFailedEvent(int errorCode, string errorMessage)
    //{
    //    Debug.Log("purchaseFailedEvent: " + errorMessage);
    //    mMsg = "Purchase Failed: " + errorMessage;

    //    PayReceiver.instance().PayResultIPA(_ePayOptionType.PurchaseProduct, false, errorMessage);
    //}

    ////-------------------------------------------------------------------------
    //private void consumePurchaseSucceededEvent(Purchase purchase)
    //{
    //    Debug.Log("consumePurchaseSucceededEvent: " + purchase);
    //    mMsg = "CONSUMED Succeeded: " + purchase.ToString();

    //    PayReceiver.instance().PayResultIPA(_ePayOptionType.ConsumeProduct, true, purchase);
    //}

    ////-------------------------------------------------------------------------
    //private void consumePurchaseFailedEvent(string error)
    //{
    //    Debug.Log("consumePurchaseFailedEvent: " + error);
    //    mMsg = "Consume Failed: " + error;

    //    PayReceiver.instance().PayResultIPA(_ePayOptionType.ConsumeProduct, false, error);
    //}
}