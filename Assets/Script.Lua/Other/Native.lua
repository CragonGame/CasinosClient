-- Copyright (c) Cragon. All rights reserved.

---------------------------------------
Native = {}

---------------------------------------
function Native:new(o, view_mgr, listner)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    if (self.Instance == nil)
    then
        self.Instance = o
        self.ViewMgr = view_mgr
        self.Listner = listner

        CS.BuglyAgent.ConfigAutoQuitApplication(true)
        CS.BuglyAgent.InitWithAppId(CS.Casinos.CasinosContext.Instance.BuglyAppId)
        CS.BuglyAgent.EnableExceptionHandler()
        if CS.Casinos.CasinosContext.Instance.IsEditor == false then
            CS.ThirdPartyLogin.Instantce():initLogin(CS.Casinos.CasinosContext.Instance.WeChatAppId)
            CS.Push.Instant():initPush(CS.Casinos.CasinosContext.Instance.PushAppId, CS.Casinos.CasinosContext.Instance.PushAppKey, CS.Casinos.CasinosContext.Instance.PushAppSecret)
        end
        CS.DataEye.initWithAppIdAndChannelId(CS.Casinos.CasinosContext.Instance.DataEyeId, "")
        CS.ShareSDKReceiver.instance(CS.Casinos.CasinosContext.Instance.ShareSDKAppKey, CS.Casinos.CasinosContext.Instance.ShareSDKAppSecret)
        --print("ShareSDK ", CS.Casinos.CasinosContext.Instance.ShareSDKAppKey, CS.Casinos.CasinosContext.Instance.ShareSDKAppSecret)
        CS.ShareSDKReceiver.mShareSDK.devInfo = CS.cn.sharesdk.unity3d.DevInfoSet()
        CS.ShareSDKReceiver.mShareSDK.devInfo.wechat = CS.cn.sharesdk.unity3d.WeChat()
        if CS.Casinos.CasinosContext.Instance.UnityAndroid then
            CS.ShareSDKReceiver.mShareSDK.devInfo.wechat.AppId = CS.Casinos.CasinosContext.Instance.WeChatAppId
            CS.ShareSDKReceiver.mShareSDK.devInfo.wechat.AppSecret = WeChatAppSecret
            CS.ShareSDKReceiver.mShareSDK.devInfo.wechat.BypassApproval = false
        elseif CS.Casinos.CasinosContext.Instance.UnityIOS then
            CS.ShareSDKReceiver.mShareSDK.devInfo.wechat.app_id = CS.Casinos.CasinosContext.Instance.WeChatAppId
            CS.ShareSDKReceiver.mShareSDK.devInfo.wechat.app_secret = WeChatAppSecret
            --print("ShareSDK WeiXin ", CS.Casinos.CasinosContext.Instance.WeChatAppId, WeChatAppSecret)
        end

        CS.ShareSDKReceiver.mShareSDK:init()
        --CS.MobLinkReceiver.instance()
        --if CS.Casinos.CasinosContext.Instance.IsEditor == false then
        --    CS.com.moblink.unity3d.MobLink.setRestoreSceneListener(function(scene)
        --        self:OnRestoreScene(scene)
        --    end)
        --end
        local open_install = CS.OpenInstallReceiver.instance()
        open_install.OpenInstallResultCallBack = function(result, is_install)
            self:OpenInstallResult(result, is_install)
        end
        CS.OpenInstall.Instant()
    end
    self.PlayerQRCodeTexture = CS.UnityEngine.Texture2D(190, 190)
    self.ShareUrl = nil

    CS.Pay.Instant()
    if CS.Casinos.CasinosContext.Instance.IsEditor == false then
        local secrete = BeeCloudLiveSecret
        if PayUseTestMode then
            secrete = BeeCloudTestSecret
        end
        CS.Pay.payInit(BeeCloudId, secrete, WeChatAppId)
        CS.Pay.useTestMode(PayUseTestMode)
    end

    CS.Casinos.CasinosContext.Instance.NativeAPIMsgReceiverListner.ActionGetPicSuccess = function(getpic_result)
        self:ActionGetPicSuccess(getpic_result)
    end
    CS.Casinos.CasinosContext.Instance.NativeAPIMsgReceiverListner.ActionGetPicSuccessWithBytes = function(pic_data)
        self:ActionGetPicSuccessWithBytes(pic_data)
    end
    CS.Casinos.CasinosContext.Instance.NativeAPIMsgReceiverListner.ActionPayWithIAPFailed = function(result)
        self:ActionPayWithIAPFailed(result)
    end
    CS.Casinos.CasinosContext.Instance.NativeAPIMsgReceiverListner.ActionPayWithIAPSuccess = function(purchase)
        self:ActionPayWithIAPSuccess(purchase)
    end

    CS.Casinos.CasinosContext.Instance.NativeAPIMsgReceiverListner.ActionPayResult = function(pay_result)
        self:ActionPayResult(pay_result)
    end

    CS.Casinos.CasinosContext.Instance.NativeAPIMsgReceiverListner.ActionLoginFailed = function(fail_type)
        self:ActionLoginFailed(fail_type)
    end
    CS.Casinos.CasinosContext.Instance.NativeAPIMsgReceiverListner.ActionLoginSuccess = function(param, real_token)
        self:ActionLoginSuccess(param, real_token)
    end
    return self.Instance
end

---------------------------------------
function Native:ShareContent(platform_type, text, pic_path, title, url, share_type)
    local share_content = CS.cn.sharesdk.unity3d.ShareContent()
    --share_content:SetText("this is a test string.")
    --share_content:SetImageUrl("https://f1.webshare.mob.com/code/demo/img/1.jpg")
    --share_content:SetTitle("test title")
    --share_content:SetUrl("http://www.baidu.com")
    --share_content:SetShareType(CS.cn.sharesdk.unity3d.ContentType.Image)
    share_content:SetText(text)
    share_content:SetImagePath(pic_path)--"http://ww3.sinaimg.cn/mw690/be159dedgw1evgxdt9h3fj218g0xctod.jpg"
    share_content:SetTitle(title)
    share_content:SetUrl(url)--http://qjsj.youzu.com/jycs/
    share_content:SetShareType(share_type)
    CS.ShareSDKReceiver.mShareSDK:ShareContent(platform_type, share_content)
end

---------------------------------------
function Native:GetMobId(player_id)
    local c = CS.System.Collections.Hashtable()
    c:Add("PlayerId", player_id)
    local scene = CS.com.moblink.unity3d.MobLinkScene("", "", c)
    if CS.Casinos.CasinosContext.Instance.IsEditor == false then
        print("GetMobId " .. player_id)
        CS.com.moblink.unity3d.MobLink.getMobId(scene, function(mob_id)
            self:OnMobIdHandler(mob_id)
        end)
    end
end

---------------------------------------
function Native:OnMobIdHandler(mobid)
    --local url = CS.Casinos.CasinosContext.Config.ConfigUrl
    --print("OnMobIdHandler  " .. mobid)
    self.ShareUrl = 'https://www.cragon.cn/gpdz.html?mobid=' .. mobid --'https://www.cragon.cn/gpdz1.html?mobid=' .. mobid  'http://cragon-king.oss-cn-shanghai.aliyuncs.com/Test.html?mobid=' .. mobid
    local t = CS.UnityEngine.Texture2D(256, 256)
    local colors = CS.QRCodeMaker.createQRCode(self.ShareUrl, t.width, t.height)
    t:SetPixels32(colors)
    t:Apply()
    self.PlayerQRCodeTexture:SetPixels(t:GetPixels(32, 32, 190, 190))
    self.PlayerQRCodeTexture:Apply()
end

---------------------------------------
function Native:CreateShareUrlAndQRCode(player_id)
    --local url = CS.Casinos.CasinosContext.Config.ConfigUrl
    --print("CreateShareUrlAndQRCode  " .. player_id)
    self.ShareUrl = 'https://www.cragon.cn/gpdz.html?PlayerId=' .. player_id --'https://www.cragon.cn/gpdz1.html?mobid=' .. mobid  'http://cragon-king.oss-cn-shanghai.aliyuncs.com/Test.html?mobid=' .. mobid
    local t = CS.UnityEngine.Texture2D(256, 256)
    local colors = CS.QRCodeMaker.createQRCode(self.ShareUrl, t.width, t.height)
    t:SetPixels32(colors)
    t:Apply()
    self.PlayerQRCodeTexture:SetPixels(t:GetPixels(32, 32, 190, 190))
    self.PlayerQRCodeTexture:Apply()
end

---------------------------------------
function Native:OnRestoreScene(scene)
    local params = scene.customParams
    --print("OnRestoreScene")
    if params == nil then
        return
    end

    local player_id = CS.Casinos.LuaHelper.GetHashTableValue(params, "PlayerId") --params["PlayerId"]
    --print("player_id  " .. player_id)
    local invite_payerid = "InvitePlayerId"
    if (CS.UnityEngine.PlayerPrefs.HasKey(invite_payerid) == false)
    then
        local t = {}
        t["PlayerId"] = player_id
        t["IsNew"] = true
        local t_encode = self.Listner.Json.encode(t)
        --print("t_encode  " .. t_encode)
        CS.UnityEngine.PlayerPrefs.SetString(invite_payerid, t_encode)
    end
end

---------------------------------------
function Native:OpenInstallResult(result, is_install)
    if CS.System.String.IsNullOrEmpty(result) then
        --print("OpenInstallResult i null")
        return
    end

    --print("result  " .. result .. "  is_install  " .. tostring(is_install))
    if is_install then
        local t_result = self.Listner.Json.decode(result)
        local data = t_result["Data"]
        local channel_code = t_result["ChannelCode"]
        local invite_payerid = "InvitePlayerId"
        if (CS.UnityEngine.PlayerPrefs.HasKey(invite_payerid) == false) then
            local player_id = data["PlayerId"]
            if player_id ~= nil then
                local t = {}
                t["PlayerId"] = player_id
                t["IsNew"] = true
                local t_encode = self.Listner.Json.encode(t)
                CS.UnityEngine.PlayerPrefs.SetString(invite_payerid, t_encode)
            end
        end
    else
    end
end

---------------------------------------
function Native:ActionGetPicSuccess(getpic_result)
    ViewHelper:UiEndWaiting()
    if (CS.System.String.IsNullOrEmpty(getpic_result) == false) then
        ViewHelper:UiBeginWaiting(self.ViewMgr.LanMgr:getLanValue("UploadPic"), 5)
    end
end

---------------------------------------
function Native:ActionGetPicSuccessWithBytes(pic_data)
    local ev = self.ViewMgr:getEv("EvGetPicSuccess")
    if (ev == nil) then
        ev = EvGetPicSuccess:new(nil)
    end
    ev.pic_data = pic_data
    self.ViewMgr:sendEv(ev)
end

---------------------------------------
function Native:ActionPayWithIAPFailed(result)
    ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("BuyFail") .. result)
end

---------------------------------------
function Native:ActionPayWithIAPSuccess(purchase)
    local ev = self.ViewMgr:getEv("EvPayWithIAPSuccess")
    if (ev == nil) then
        ev = EvPayWithIAPSuccess:new(nil)
    end
    ev.purchase = purchase
    self.ViewMgr:sendEv(ev)
end

---------------------------------------
function Native:ActionPayResult(pay_result)
    if (pay_result.Length <= 0) then
        ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("BuyItemFailed"))
        return
    else
        local real_result = pay_result[0]
        if real_result == "success" then
            ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("PaySuccess"))
        elseif real_result == "fail" then
            ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("PayFailed"))
        elseif real_result == "cancel" then
            ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("PayCanceled"))
        elseif real_result == "invalid" then
            ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("PayAppNotInstall"))
        elseif real_result == "unknown" then
            ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("PayUnknownError"))
        end
    end
end

---------------------------------------
function Native:ActionLoginFailed(fail_type)
    local native = Native.Instance
    ViewHelper:UiEndWaiting()
    local tips = ""
    if fail_type == "ERR_COMM" then
        tips = native.ViewMgr.LanMgr:getLanValue("ERRCOMM")
    elseif fail_type == "ERR_USER_CANCEL" then
        tips = native.ViewMgr.LanMgr:getLanValue("ERRUSERCANCEL")
    elseif fail_type == "ERR_SENT_FAILED" then
        tips = native.ViewMgr.LanMgr:getLanValue("ERRSENTFAILED")
    elseif fail_type == "ERR_AUTH_DENIED" then
        tips = native.ViewMgr.LanMgr:getLanValue("ERRAUTHDENIED")
    elseif fail_type == "ERR_UNSUPPORT" then
        tips = native.ViewMgr.LanMgr:getLanValue("ERRUNSUPPORT")
    elseif fail_type == "ERR_NOTINSTALLEDWECHAT" then
        ClientWechatIsInstalled = false
    else
        if fail_type == "-2" then
            tips = native.ViewMgr.LanMgr:getLanValue("ERRUSERCANCEL")
        else
            local t = native.ViewMgr.LanMgr:getLanValue("ERRUNKNOWN")
            tips = string.format(t, fail_type)
        end
    end
    if (CS.System.String.IsNullOrEmpty(tips) == false) then
        ViewHelper:UiShowPermanentPosMsg(tips)
    end
end

---------------------------------------
function Native:ActionLoginSuccess(param, real_token)
    if (param == "Login") then
        local ev = self.ViewMgr:getEv("EvUiLoginSuccessEx")
        if (ev == nil) then
            ev = EvUiLoginSuccessEx:new(nil)
        end
        ev.token = real_token
        self.ViewMgr:sendEv(ev)
    end
end