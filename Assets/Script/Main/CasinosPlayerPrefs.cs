// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using UnityEngine;

    // 登录游客信息
    public class LoginGuestInfo
    {
        public string account_id;
        public string account_name;
        public string pwd;
    }

    // 登录帐号信息
    public class LoginAccountInfo
    {
        public string account_name;
        public string pwd;
    }

    public class CasinosPlayerPrefs
    {
        //---------------------------------------------------------------------
        const string KeyLoginGuestInfo = "LoginGuestInfo";
        const string KeyLoginAccountInfo = "LoginAccountInfo";

        //---------------------------------------------------------------------
        public LoginGuestInfo LoginGuestInfo { get; set; }
        public LoginAccountInfo LoginAccountInfo { get; set; }

        //---------------------------------------------------------------------
        // 加载登录游客信息
        public LoginGuestInfo LoadLoginGuestInfo()
        {
            if (PlayerPrefs.HasKey(KeyLoginGuestInfo))
            {
                string s = PlayerPrefs.GetString(KeyLoginGuestInfo);
                //LoginGuestInfo = EbTool.jsonDeserialize<LoginGuestInfo>(s);
            }
            else
            {
                LoginGuestInfo = new LoginGuestInfo();
            }

            return LoginGuestInfo;
        }

        //---------------------------------------------------------------------
        // 保存登录游客信息
        public void SaveLoginGuestInfo(LoginGuestInfo login_guestinfo)
        {
            LoginGuestInfo = login_guestinfo;

            //string s = EbTool.jsonSerialize(LoginGuestInfo);
            //PlayerPrefs.SetString(KeyLoginGuestInfo, s);
        }

        //---------------------------------------------------------------------
        // 清除登录游客信息
        public void ClearLoginGuestInfo()
        {
            PlayerPrefs.DeleteKey(KeyLoginGuestInfo);
        }

        //---------------------------------------------------------------------
        // 加载登录帐号信息
        public LoginAccountInfo LoadLoginAccountInfo()
        {
            if (PlayerPrefs.HasKey(KeyLoginAccountInfo))
            {
                string s = PlayerPrefs.GetString(KeyLoginAccountInfo);
                //LoginAccountInfo = EbTool.jsonDeserialize<LoginAccountInfo>(s);
            }
            else
            {
                LoginAccountInfo = new LoginAccountInfo();
            }

            return LoginAccountInfo;
        }

        //---------------------------------------------------------------------
        // 保存登录帐号信息
        public void SaveLoginAccountInfo(string acc, string pwd)
        {
            var login_accinfo = new LoginAccountInfo();
            login_accinfo.account_name = acc;
            login_accinfo.pwd = pwd;

            LoginAccountInfo = login_accinfo;

            //string s = EbTool.jsonSerialize(LoginAccountInfo);
            //PlayerPrefs.SetString(KeyLoginAccountInfo, s);
        }
    }
}