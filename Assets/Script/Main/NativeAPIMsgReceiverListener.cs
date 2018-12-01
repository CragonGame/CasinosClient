// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.IO;
    using UnityEngine;
    //using OnePF;// 内购的一个库

    public class NativeAPIMsgReceiverListener
        : ITakePhotoReceiverListener
        , IPayReceiverListener
        , IAudioControlListener
        , IThirdPartyLoginReceiverListener
    {
        //---------------------------------------------------------------------
        public Action<string> ActionGetPicSuccess;
        public Action<byte[]> ActionGetPicSuccessWithBytes;
        public Action<string> ActionPayWithIAPFailed;
        //public Action<Purchase> ActionPayWithIAPSuccess;
        public Action<string[]> ActionPayResult;
        public Action<string> ActionLoginFailed;
        public Action<string, string> ActionLoginSuccess;

        //---------------------------------------------------------------------
        public NativeAPIMsgReceiverListener()
        {
            _initNativeMsgReceiverListener();
        }

        //---------------------------------------------------------------------
        public void Close()
        {
            ActionGetPicSuccess = null;
            ActionGetPicSuccessWithBytes = null;
            ActionPayWithIAPFailed = null;
            ActionPayResult = null;
            ActionLoginFailed = null;
            ActionLoginSuccess = null;
        }

        //---------------------------------------------------------------------
        public void audioChanged(string chang)
        {
            Debug.Log("audioChanged");
        }

        //---------------------------------------------------------------------
        public void getPicFail(string fail)
        {
            // 加载图片失败
            //CasinosContext.Instance.UiEndWaiting(CasinosContext.Instance.ViewHelper);
            Debug.Log("加载图片失败");
        }

        //---------------------------------------------------------------------
        public void getPicSuccess(string getpic_result)
        {
            if (ActionGetPicSuccess != null)
            {
                ActionGetPicSuccess(getpic_result);
            }
            if (string.IsNullOrEmpty(getpic_result))
            {
                Debug.Log("加载图片失败,未获取到图片");
            }
            else
            {
#if UNITY_ANDROID
                string file_path = "/photos/GetPicDefaultName.jpg";
                string p_ex = CasinosContext.Instance.PathMgr.CombinePersistentDataPath(file_path);
                byte[] all_bytes = null;
                if (File.Exists(p_ex))
                {
                    all_bytes = File.ReadAllBytes(p_ex);
                    if (ActionGetPicSuccessWithBytes != null)
                    {
                        ActionGetPicSuccessWithBytes(all_bytes);
                    }
                }
#elif UNITY_IOS                         
                if (ActionGetPicSuccessWithBytes != null)
                {
                    ActionGetPicSuccessWithBytes(Convert.FromBase64String(getpic_result));
                }
#endif
            }
        }

        //---------------------------------------------------------------------
        public void PayResultIPA(_ePayOptionType option_type, bool is_success, object result)
        {
            //    switch (option_type)
            //    {
            //        case _ePayOptionType.QueryInventory:
            //            break;
            //        case _ePayOptionType.PurchaseProduct:
            //            //CasinosContext.Instance.UiEndWaiting(CasinosContext.Instance.ViewHelper);
            //            if (is_success)
            //            {
            //                Purchase purchase = (Purchase)result;
            //                if (ActionPayWithIAPSuccess != null)
            //                {
            //                    ActionPayWithIAPSuccess(purchase);
            //                }
            //            }
            //            else
            //            {
            //                if (ActionPayWithIAPFailed != null)
            //                {
            //                    ActionPayWithIAPFailed(result.ToString());
            //                }
            //            }
            //            break;
            //        case _ePayOptionType.ConsumeProduct:
            //            break;
            //        default:
            //            break;
            //    }
        }

        //---------------------------------------------------------------------
        public void PayResult(string result)
        {
            //"success" - 支付成功 "fail" - 支付失败 "cancel" - 取消支付 "invalid" - 支付插件未安装（一般是微信客户端未安装的情况）"unknown" - app进程异常被杀死(一般是低内存状态下, app进程被杀死)
            if (!string.IsNullOrEmpty(result))
            {
                var pay_result = result.Split(new char[] { ';' });
                if (ActionPayResult != null)
                {
                    ActionPayResult(pay_result);
                }
            }
        }

        //---------------------------------------------------------------------
        public void loginSuccess(string token)
        {
            //CasinosContext.Instance.UiEndWaiting(CasinosContext.Instance.ViewHelper);

            string msg = token;
            //var splite_index = msg.IndexOf("|");
            var param_and_token = msg.Split(new char[] { '|' }, 2);
            string param = string.Empty;
            string real_token = string.Empty;
            if (param_and_token != null && param_and_token.Length == 2)
            {
                param = param_and_token[0];
                real_token = param_and_token[1];
            }

            if (ActionLoginSuccess != null)
            {
                ActionLoginSuccess(param, real_token);
            }
        }

        //---------------------------------------------------------------------
        public void loginFail(string fail_type)
        {
            if (ActionLoginFailed != null)
            {
                ActionLoginFailed(fail_type);
            }
        }

        //---------------------------------------------------------------------
        void _initNativeMsgReceiverListener()
        {
            var native_receiver = NativeReceiver.Instance();
            native_receiver.TakePhotoReceiverListener = this;
            native_receiver.AudioControlListener = this;
            var pay_receiver = PayReceiver.instance();
            pay_receiver.PayReceiverListener = this;
            var thirdparty_login_receiver = ThirdPartyLoginReceiver.instance();
            thirdparty_login_receiver.ThirdPartyLoginReceiverListener = this;

//#if (UNITY_IPHONE || UNITY_ANDROID) && !UNITY_EDITOR
//            ThirdPartyLogin.Instantce().initLogin(CasinosContext.Instance.Config.WeChatAppId);
//#endif
            //PushReceiver.instance();
            //OpenInstallReceiver.instance();
        }

        //---------------------------------------------------------------------
        //void uploadProfileImageCallBack(UCenterResponseStatus status, AccountUploadProfileImageResponse response, UCenterError error)
        //{
        //    if (status == UCenterResponseStatus.Error)
        //    {
        //        var msg = CasinosContext.Instance.parseLanStr("上传图片失败", "UploadPicFailed") + "! ErrorCode: " + error.ErrorCode + " Msg: " + error.Message;

        //        CasinosContext.Instance.UiShowInfoFailed(CasinosContext.Instance.ViewHelper, msg);
        //    }
        //    else
        //    {
        //        CasinosContext.Instance.GetDefaultEventPublisher().genEvent<EvGetPicUpLoadSuccess>().send(null);
        //    }

        //    CasinosContext.Instance.UiEndWaiting(CasinosContext.Instance.ViewHelper);
        //}
    }
}