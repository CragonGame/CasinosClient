PhoneCountryCode = {
    AR = { Code = "54", Name = "阿根廷" },
    AE = { Code = "971", Name = "阿拉伯联合酋长国" },
    EG = { Code = "20", Name = "埃及" },
    IE = { Code = "353", Name = "爱尔兰" },
    EE = { Code = "372", Name = "爱沙尼亚" },
    AT = { Code = "43", Name = "奥地利" },
    AU = { Code = "61", Name = "澳大利亚" },
    MO = { Code = "853", Name = "澳门" },
    PY = { Code = "595", Name = "巴拉圭" },
    BH = { Code = "973", Name = "巴林" },
    BR = { Code = "55", Name = "巴西" },
    BY = { Code = "375", Name = "白俄罗斯" },
    BG = { Code = "359", Name = "保加利亚" },
    BE = { Code = "32", Name = "比利时" },
    IS = { Code = "354", Name = "冰岛" },
    PL = { Code = "48", Name = "波兰" },
    BO = { Code = "591", Name = "玻利维亚" },
    DK = { Code = "45", Name = "丹麦" },
    DE = { Code = "49", Name = "德国" },
    RU = { Code = "7", Name = "俄罗斯" },
    EC = { Code = "593", Name = "厄瓜多尔" },
    FR = { Code = "33", Name = "法国" },
    PH = { Code = "63", Name = "菲律宾" },
    FI = { Code = "358", Name = "芬兰" },
    CO = { Code = "57", Name = "哥伦比亚" },
    KZ = { Code = "7", Name = "哈萨克斯坦" },
    KR = { Code = "82", Name = "韩国" },
    NL = { Code = "31", Name = "荷兰" },
    HN = { Code = "504", Name = "洪都拉斯" },
    CA = { Code = "1", Name = "加拿大" },
    KH = { Code = "855", Name = "柬埔寨" },
    CZ = { Code = "420", Name = "捷克" },
    QA = { Code = "974", Name = "卡塔尔" },
    KW = { Code = "965", Name = "科威特" },
    LT = { Code = "370", Name = "立陶宛" },
    LU = { Code = "352", Name = "卢森堡" },
    RO = { Code = "40", Name = "罗马尼亚" },
    MT = { Code = "356", Name = "马耳他" },
    MY = { Code = "60", Name = "马来西亚" },
    US = { Code = "1", Name = "美国" },
    BD = { Code = "880", Name = "孟加拉国" },
    PE = { Code = "51", Name = "秘鲁" },
    MX = { Code = "52", Name = "墨西哥" },
    ZA = { Code = "27", Name = "南非" },
    NI = { Code = "505", Name = "尼加拉瓜" },
    NO = { Code = "47", Name = "挪威" },
    PT = { Code = "351", Name = "葡萄牙" },
    JP = { Code = "81", Name = "日本" },
    SE = { Code = "46", Name = "瑞典" },
    CH = { Code = "41", Name = "瑞士" },
    SV = { Code = "503", Name = "萨尔瓦多" },
    RS = { Code = "381", Name = "塞尔维亚" },
    CY = { Code = "357", Name = "塞浦路斯" },
    SA = { Code = "966", Name = "沙特阿拉伯" },
    SM = { Code = "378", Name = "圣马力诺" },
    SK = { Code = "421", Name = "斯洛伐克" },
    SI = { Code = "386", Name = "斯洛文尼亚" },
    TW = { Code = "886", Name = "台湾省" },
    TH = { Code = "66", Name = "泰国" },
    TR = { Code = "90", Name = "土耳其" },
    GT = { Code = "502", Name = "危地马拉" },
    VE = { Code = "58", Name = "委内瑞拉" },
    BN = { Code = "673", Name = "文莱" },
    UA = { Code = "380", Name = "乌克兰" },
    UZ = { Code = "998", Name = "乌兹别克斯坦" },
    GR = { Code = "30", Name = "希腊" },
    ES = { Code = "34", Name = "西班牙" },
    HK = { Code = "852", Name = "香港" },
    SG = { Code = "65", Name = "新加坡" },
    NZ = { Code = "64", Name = "新西兰" },
    HU = { Code = "36", Name = "匈牙利" },
    AM = { Code = "374", Name = "亚美尼亚" },
    IL = { Code = "972", Name = "以色列" },
    IT = { Code = "39", Name = "意大利" },
    IN = { Code = "91", Name = "印度" },
    ID = { Code = "62", Name = "印度尼西亚" },
    GB = { Code = "44", Name = "英国" },
    JO = { Code = "962", Name = "约旦" },
    VN = { Code = "84", Name = "越南" },
    CL = { Code = "56", Name = "智利" },
    CN = { Code = "86", Name = "中国" },
}

ControllerLogin = ControllerBase:new(nil)

function ControllerLogin:new(o, controller_mgr, controller_data, guid)
    o = o or {}
    setmetatable(o, self)
    self.__index = self

    if (self.Instance == nil)
    then
        self.ControllerName = "Login"
        self.ControllerData = controller_data
        self.ControllerMgr = controller_mgr
        self.Guid = guid
        self.Password = nil
        self.RemeberPwd = false
        self.RequestThirdPartyLogin = false
        self.BindingWeChat = false
        self.AutoLogin = false
        self.AutoLoginTm = 0
        self.ViewMgr = ViewMgr:new(nil)
        self.AccId = nil
        self.Acc = nil
        self.Pwd = nil
        self.Token = nil
        self.GatewayIp = nil
        self.GatewayPort = 5882
        self.ClientEnterWorldNotify = nil
        self.ShowKickOutInfo = false
        self.LoginAccountInfoKey = "LoginAccountInfo2"
        self.Instance = o
    end

    return self.Instance
end

function ControllerLogin:onCreate()
    self.ControllerUCenter = self.ControllerMgr:GetController("UCenter")
    local c = CS.Casinos.CasinosContext.Instance
    self.ViewMgr:bindEvListener("EvUiLogin", self)
    self.ViewMgr:bindEvListener("EvUiLoginSuccessEx", self)
    self.ViewMgr:bindEvListener("EvUiLoginClickBtnRegister", self)
    self.ViewMgr:bindEvListener("EvEntityPlayerInitDone", self)
    self.ViewMgr:bindEvListener("EvUiRequestResetPwd", self)
    self.ViewMgr:bindEvListener("EvUiChooseUCenter", self)
    self.ViewMgr:bindEvListener("EvUiChooseGateWay", self)
    self.ViewMgr:bindEvListener("EvUiRequestGetPhoneCode", self)
    self.ViewMgr:bindEvListener("EvCheckIdCard", self)
    self.ViewMgr:bindEvListener("EvBindWeChat", self)
    self.ViewMgr:bindEvListener("EvUnbindWeChat", self)

    self:_init(true)
    c.NetBridge:blindTable(self)
    local rpc = self.ControllerMgr.RPC
    local m_c = CommonMethodType
    rpc:RegRpcMethod0(m_c.AccountGatewayConnected, function()
        self:OnAccountGatewayConnected()
    end)
    rpc:RegRpcMethod1(m_c.AccountLoginAppResponse, function(login_response)
        self:OnAccountLoginAppResponse(login_response)
    end)
    rpc:RegRpcMethod1(m_c.AccountEnterWorldResponse, function(enterworld_notify)
        self:OnAccountEnterWorldResponse(enterworld_notify)
    end)
    rpc:RegRpcMethod1(m_c.AccountLogoutNotify, function(protocal_result)
        self:OnAccountLogoutNotify(protocal_result)
    end)
    rpc:RegRpcMethod1(m_c.AccountUpdateDataFromUCenterNotify, function(result)
        self:OnAccountUpdateDataFromUCenterNotify(result)
    end)
end

function ControllerLogin:onDestroy()
    self.ViewMgr:unbindEvListener(self)
end

function ControllerLogin:onUpdate(tm)
    if (self.RequestThirdPartyLogin)
    then
        self.RequestThirdPartyLogin = false
        if (CS.Casinos.CasinosContext.Instance.LoginType == CS.Casinos._eLoginType.WeiXin or self.BindingWeChat)
        then
            CS.Casinos.CasinosContext.Instance:setNativeOperate(1)
            CS.ThirdPartyLogin.Instantce():login(CS._eThirdPartyLoginType.WeChat,
                    CS.Casinos.CasinosContext.Instance.Config.WeChatState, "Login")
        end
    end

    if self.AutoLogin then
        self.AutoLoginTm = self.AutoLoginTm+tm
        if self.AutoLoginTm >=5 then
            self.AutoLoginTm = 0
            self.AutoLogin = false
            local view_login = self.ViewMgr:getView("Login")
            if view_login ~= nil then
                view_login:_switch2LoginMain()
            end
        end
    end
end

function ControllerLogin:onHandleEv(ev)
    if (ev ~= nil)
    then
        if (ev.EventName == "EvUiLogin")
        then
            local login_type = ev.login_type
            CS.Casinos.CasinosContext.Instance.LoginType = login_type
            self.LoginType = login_type
            self.Acc = ev.acc
            self.Pwd = ev.pwd
            local remeber_pwd = ev.remeber_pwd
            self.RemeberPwd = remeber_pwd
            self.Phone = ev.phone

            MainC:LoadConfig(true,function(bo)
                self:_clickCheckDataCallBack(bo)
            end)
        elseif (ev.EventName == "EvUiLoginClickBtnRegister")
        then
            if (self.ControllerUCenter.WWWRegister ~= null)
            then
                local info = self.ControllerMgr.LanMgr:getLanValue("Registering")
                ViewHelper:UiShowInfoFailed(info)
            else
                local acc = ev.AccountName
                --print(acc)
                local pwd = ev.Password
                --print(pwd)
                local super_pwd = ev.SuperPassword
                --print(super_pwd)
                local remeber_pwd = ev.remeber_pwd
                self.Password = pwd
                self.RemeberPwd = remeber_pwd
                local email = ev.Email
                local identity = ev.Identity
                local phone = ev.Phone
                self.Phone = phone
                local name = ev.Name
                local device_info = self.getDeviceInfo()
                local register_acc_data = AccountRegisterInfo:new(nil)--CS.GameCloud.UCenter.Common.Portable.Models.AppClient.AccountRegisterInfo()
                register_acc_data.AccountName = acc
                register_acc_data.Password = pwd
                register_acc_data.SuperPassword = super_pwd
                register_acc_data.Email = email
                register_acc_data.Identity = identity
                register_acc_data.Phone = ev.FormatPhone
                register_acc_data.Name = name
                register_acc_data.PhoneVerificationCode = ev.PhoneVerificationCode
                register_acc_data.Device = device_info
                register_acc_data.AppId = CS.Casinos.CasinosContext.Instance.Config.AppId
                self:requestRegister(register_acc_data)
            end
        elseif (ev.EventName == "EvUiRequestGetPhoneCode")
        then
            --获取验证码
            local phone = ev.Phone
            local request = GetPhoneVerificationCodeRequest:new(nil)
            request.Phone = phone
            self.ControllerUCenter:getPhoneVerificationCode(request,
                    function(status, response, error)
                        self:onUCenterPhoneVerificationCode(status, response, error)
                    end)
        elseif (ev.EventName == "EvUiRequestResetPwd")
        then
            local phone_code = ev.phone_code
            local new_pwd = ev.new_pwd
            self.Password = new_pwd
            self.Phone = ev.phone
            self:resetPwd(ev.formatphone, phone_code, new_pwd)
        elseif (ev.EventName == "EvUiChooseUCenter") then
            CS.Casinos.CasinosContext.Instance.UserConfig.Current.UCenterDomain = ev.ucenter
            self.ControllerUCenter.UCenterDomain = ev.ucenter
        elseif (ev.EventName == "EvUiChooseGateWay") then
            CS.Casinos.CasinosContext.Instance.UserConfig.Current.GatewayIp = ev.gateway
        elseif (ev.EventName == "EvUiLoginSuccessEx") then
            if self.BindingWeChat == false then
                self:weChatLogin(ev.token,CS.Casinos.CasinosContext.Instance.Config.WeChatAppId)
            else
                local request = AccountWeChatBindRequest:new(nil)
                request.WechatMpAppId = WeChatPublicAppId
                request.Code = ev.token
                request.AccountId = self.AccId
                request.Token = self.Token
                self.ControllerUCenter:wechatBind(request,
                        function(status, response, error)
                            self:onUCenterBindWeChat(status, response, error)
                        end)
            end
            self.BindingWeChat = false
        elseif (ev.EventName == "EvCheckIdCard") then
            self:checkIdCard(ev.id_card,ev.name)
        elseif(ev.EventName == "EvBindWeChat") then
            ViewHelper:UiBeginWaiting(self.ControllerMgr.LanMgr:getLanValue("BindingWeChat"), 10)
            self.RequestThirdPartyLogin = true
            self.BindingWeChat = true
        elseif(ev.EventName == "EvUnbindWeChat") then
            print("EvUnbindWeChat")
            local open_id = nil
            if self.ControllerActor ~= nil then
                open_id = self.ControllerActor.WeChatOpenId:get()
            end
            local request = AccountWeChatUnbindRequest:new(nil)
            request.WechatMpAppId = WeChatPublicAppId
            request.OpenId = open_id
            request.AccountId = self.AccId
            request.Token = self.Token
            self.ControllerUCenter:wechatUnbind(request,
                    function(status, response, error)
                        self:onUCenterUnbindWeChat(status, response, error)
                    end)
        end
    end
end

function ControllerLogin:getModle()
end

function ControllerLogin:requestLogin(acc, pwd, phone, email, phone_verification_code)
    CS.Casinos.CasinosContext.Instance.LoginType = 0
    self.Password = pwd
    ViewHelper:UiBeginWaiting(self.ControllerMgr.LanMgr:getLanValue("Logining"))
    local request = AccountLoginInfo:new(nil)--CS.GameCloud.UCenter.Common.Portable.Models.AppClient.AccountLoginInfo()
    request.AccountName = acc
    request.Phone = phone
    request.Email = email
    request.Password = pwd
    request.PhoneVerificationCode = phone_verification_code
    request.Device = self:getDeviceInfo()
    self.ControllerUCenter:login(request,
            function(status, response, error)
                self:onUCenterLogin(status, response, error)
            end)
    --CS.Casinos.CasinosContext.Instance.WrapMgr.WrapClientUCenter:requestLogin(request)
end

function ControllerLogin:requestGuestAccess()
    CS.Casinos.CasinosContext.Instance.LoginType = 1
    ViewHelper:UiBeginWaiting(self.ControllerMgr.LanMgr:getLanValue("Logining"))
    local guest_accessinfo = GuestAccessInfo:new(nil)--CS.GameCloud.UCenter.Common.Portable.Models.AppClient.GuestAccessInfo()
    guest_accessinfo.AppId = CS.Casinos.CasinosContext.Instance.Config.AppId
    guest_accessinfo.Device = self:getDeviceInfo()
    --print("requestGuestAccess_" .. guest_accessinfo.Device.Id)
    -- print(guest_accessinfo.AppId.."  "..guest_accessinfo.Device.Name)
    self.ControllerUCenter:guestAccess(guest_accessinfo,
            function(status, response, error)
                self:onUCenterGuestAccess(status, response, error)
            end)
    --CS.Casinos.CasinosContext.Instance.WrapMgr.WrapClientUCenter:requestGuestAccess(guest_accessinfo)
end

function ControllerLogin:requestRegister(register_acc_data)
    ViewHelper:UiBeginWaiting(self.ControllerMgr.LanMgr:getLanValue("Registering"))
    self.ControllerUCenter:register(register_acc_data,
            function(status, response, error)
                self:onUCenterRegister(status, response, error)
            end)
    --CS.Casinos.CasinosContext.Instance.WrapMgr.WrapClientUCenter:requestRegister(register_acc_data)
end

function ControllerLogin:weChatLogin(token, app_id)
    CS.Casinos.CasinosContext.Instance.LoginType = 2
    local c_login = ControllerLogin:new(nil)
    ViewHelper:UiBeginWaiting(c_login.ControllerMgr.LanMgr:getLanValue("Logining"))
    local wechat_info = AccountWeChatOAuthInfo:new(nil)--CS.GameCloud.UCenter.Common.Portable.Models.AppClient.AccountWeChatOAuthInfo()
    wechat_info.Code = token
    wechat_info.AppId = app_id
    c_login.ControllerUCenter:wechatLogin(wechat_info,
            function(status, response, error)
                c_login:onUCenterLogin(status, response, error)
            end)
    --CS.Casinos.CasinosContext.Instance.WrapMgr.WrapClientUCenter:requestWeChatLogin(wechat_info)
end

function ControllerLogin:resetPwd( phone, phone_code, new_pwd)
    --local c_login = ControllerLogin:new(nil)
    ViewHelper:UiBeginWaiting(self.ControllerMgr.LanMgr:getLanValue("ResetPwding"))
    local resetPwd_info = AccountResetPasswordByPhoneRequest:new(nil)--CS.GameCloud.UCenter.Common.Portable.Models.AppClient.AccountResetPasswordInfo()
    resetPwd_info.AccountName = ""
    resetPwd_info.Phone = phone
    resetPwd_info.Email = ""
    resetPwd_info.NewPassword = new_pwd
    resetPwd_info.PhoneVerificationCode = phone_code
    self.ControllerUCenter:resetPasswordWithPhone(resetPwd_info,
            function(status, response, error)
                self:onUCenterResetPasswordWithPhone(status, response, error)
            end)
    --CS.Casinos.CasinosContext.Instance.WrapMgr.WrapClientUCenter:requestResetPassword(resetPwd_info)
end

function ControllerLogin:checkIdCard(id_card,name)
    local r = CheckCardAndNameRequest:new(nil)
    r.AccountId = self.AccId
    r.CardNo = id_card
    r.Token = self.Token
    r.RealName = name
    self.ControllerUCenter:checkCardAndName(r,
            function(status, response, error)
                self:onUCenterCheckIdCard(status, response, error)
            end)
end

function ControllerLogin:onUCenterPhoneVerificationCode(status, response, error)
    if (status == UCenterResponseStatus.Success)
    then
        ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("GetPhoneCodeSuccess"))
    else
        if (error ~= nil)
        then
            ViewHelper:UiEndWaiting()
            local error_msg = self.ControllerUCenter:ParseUCenterErrorCode(error.code)
            ViewHelper:UiShowInfoFailed(error_msg)
        end
    end
end

function ControllerLogin:onUCenterRegister(status, response, error)
    --local c = CS.Casinos.CasinosContext.Instance
    --local c_login = ControllerLogin:new(nil)
    if (status == UCenterResponseStatus.Success)
    then
        local info = self.ControllerMgr.LanMgr:getLanValue("RegisterSuccessful")
        ViewHelper:UiShowInfoSuccess(info)

        self.AccId = response.accountId
        self.Acc = response.accountName

        -- 切换显示为登录对话框
        local view_login = self.ViewMgr:getView("Login")
        if (view_login ~= nil)
        then
            view_login:Switch2DlgLogin(self.Phone, self.Password)
        end

        -- 自动请求登录
        self:requestLogin(response.accountName, self.Password, response.phone, "", "")
    else
        if (error ~= nil)
        then
            ViewHelper:UiEndWaiting()
            local error_msg = self.ControllerUCenter:ParseUCenterErrorCode(error.code)
            ViewHelper:UiShowInfoFailed(error_msg)
        end
    end
end

function ControllerLogin:onUCenterLogin(status, response, error)
    --local c_login = ControllerLogin:new(nil)
    local c = CS.Casinos.CasinosContext.Instance
    if (status == UCenterResponseStatus.Success)
    then
        ViewHelper:UiBeginWaiting(self.ControllerMgr.LanMgr:getLanValue("LoginSuccessful"))
        self.AccId = response.accountId
        self.Acc = response.accountName
        self.Token = response.token
        self.Identity = response.identity
        print("onUCenterLogin")
        -- 保存登录信息
        local infos = LoginAccountInfos:new(nil)
        if (CS.UnityEngine.PlayerPrefs.HasKey(self.LoginAccountInfoKey))
        then
            local s = CS.UnityEngine.PlayerPrefs.GetString(self.LoginAccountInfoKey)
            local d = self.ControllerMgr.Listener.Json.decode(s)
            infos:setData(d)
        end

        local login_type = 0
        local acc_name = ""
        local phone = ""
        local pwd = ""
        if (c.LoginType == CS.Casinos._eLoginType.Acc)
        then
            pwd = self.Password
            if (self.RemeberPwd == false)
            then
                pwd = ""
            end
            login_type = 0
            acc_name = self.Phone
            phone = response.phone
        elseif (c.LoginType == CS.Casinos._eLoginType.Guest)
        then
            login_type = 1
        elseif (c.LoginType == CS.Casinos._eLoginType.WeiXin)
        then
            login_type = 2
            acc_name = response.accountName
        end

        infos.LastLoginType = login_type
        local s_login_type =tostring(login_type)
        local a_info = infos.TLoginAccountInfo[s_login_type]
        if a_info == nil then
            a_info = LoginAccountInfo:new(nil)
        end
        a_info.LoginType = login_type
        a_info.AccName = acc_name
        a_info.Phone = phone
        a_info.Pwd = pwd
        infos.TLoginAccountInfo[s_login_type] = a_info

        local t_encode = self.ControllerMgr.Listener.Json.encode(infos)
        CS.UnityEngine.PlayerPrefs.SetString(self.LoginAccountInfoKey, t_encode)

        -- DataEye登陆
        -- CoApp.CoDataEye.login(Acc, AccId);
        CS.DataEye.login(self.Acc .. "_" .. self.AccId)
        c.NetBridge:connectBase(
                c.UserConfig.Current.GatewayIp,
                c.UserConfig.Current.GatewayPort)
    else
        if (error ~= nil)
        then
            ViewHelper:UiEndWaiting()
            local error_msg = self.ControllerUCenter:ParseUCenterErrorCode(error.code)
            ViewHelper:UiShowInfoFailed(error_msg)
            local view_login = self.ViewMgr:getView("Login")
            view_login:_switch2LoginMain()
        end
    end
end

function ControllerLogin:onUCenterGuestAccess(status, response, error)
    ViewHelper:UiEndWaiting()
    --local c_login = ControllerLogin:new(nil)
    local c = CS.Casinos.CasinosContext.Instance
    if (status == UCenterResponseStatus.Success)
    then
        print("onUCenterGuestAccess")
        ViewHelper:UiBeginWaiting(self.ControllerMgr.LanMgr:getLanValue("LoginSuccessful"))
        self.AccId = response.accountId
        self.Acc = response.accountName
        self.Token = response.token

        local infos = LoginAccountInfos:new(nil)
        if (CS.UnityEngine.PlayerPrefs.HasKey(self.LoginAccountInfoKey))
        then
            local s = CS.UnityEngine.PlayerPrefs.GetString(self.LoginAccountInfoKey)
            local d = self.ControllerMgr.Listener.Json.decode(s)
            infos:setData(d)
        end

        infos.LastLoginType = 1
        local a_info = infos.TLoginAccountInfo["1"]
        if a_info == nil then
            a_info = LoginAccountInfo:new(nil)
        end
        a_info.LoginType = 1
        a_info.AccName = ""
        a_info.Phone = ""
        a_info.Pwd = ""
        infos.TLoginAccountInfo["1"] = a_info

        local t_encode = self.ControllerMgr.Listener.Json.encode(infos)
        CS.UnityEngine.PlayerPrefs.SetString(self.LoginAccountInfoKey, t_encode)

        -- DataEye登陆
        --CasinosContext.Instance.CoDataEye.login(c.CoNetMonitor.Acc, c.CoNetMonitor.AccId);
        CS.DataEye.login(self.Acc .. "_" .. self.AccId)
        c.NetBridge:connectBase(
                c.UserConfig.Current.GatewayIp,
                c.UserConfig.Current.GatewayPort)
    else
        if (error ~= nil)
        then
            local error_msg = self.ControllerUCenter:ParseUCenterErrorCode(error.code)
            ViewHelper:UiShowInfoFailed(error_msg)
        end
    end
end

function ControllerLogin:onUCenterResetPasswordWithPhone(status, response, error)
    ViewHelper:UiEndWaiting()
    --local c_login = ControllerLogin:new(nil)
    if (status == UCenterResponseStatus.Success)
    then
        local info = self.ControllerMgr.LanMgr:getLanValue("ResetPwdSuccessful")
        ViewHelper:UiShowInfoSuccess(info)

        self.AccId = response.accountId
        self.Acc = response.accountName

        local view_login = self.ViewMgr:getView("Login")
        view_login:Switch2DlgLogin(self.Phone, self.Password)

        self:requestLogin(response.accountName, self.Password, response.phone, "", "")
    else
        if (error ~= nil)
        then
            local error_msg = self.ControllerUCenter:ParseUCenterErrorCode(error.code)
            ViewHelper:UiShowInfoFailed(error_msg)
        end
    end
end

function ControllerLogin:onUCenterCheckIdCard(status, response, error)
    if (response.error_code == 0)
    then
        self.Identity = response.result.cardNo
        ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("CheckIdSuccess"))
    else
        if (response ~= nil)
        then
            ViewHelper:UiEndWaiting()
            --local error_msg = self.ControllerUCenter:ParseUCenterErrorCode(response.code)
            ViewHelper:UiShowInfoFailed(response.reason)
        end
    end
end

function ControllerLogin:onUCenterBindWeChat(status, response, error)
    if (status == UCenterResponseStatus.Success)
    then
        if (response == UCenterErrorCode.NoError)
        then
            ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("BindWeChatSuccess"))
            self.ControllerMgr.RPC:RPC0(CommonMethodType.AccountUpdateDataFromUCenterRequest)
        end
    else
        ViewHelper:UiEndWaiting()
        ViewHelper:UiShowInfoFailed(self.ControllerUCenter:ParseUCenterErrorCode(error.code))
    end
end

function ControllerLogin:onUCenterUnbindWeChat(status, response, error)
    if (status == UCenterResponseStatus.Success)
    then
        print("onUCenterUnbindWeChat")
        if (response == UCenterErrorCode.NoError)
        then
            ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("UnbindWeChatSuccess"))
            local ev = self.ViewMgr:getEv("EvUnBindWeChatSuccess")
            if(ev == nil)
            then
                ev = EvUnBindWeChatSuccess:new(nil)
            end
            ev.IsSuccess = true
            self.ViewMgr:sendEv(ev)
        end
    else
        ViewHelper:UiEndWaiting()
        ViewHelper:UiShowInfoFailed(self.ControllerUCenter:ParseUCenterErrorCode(error.code))
    end
end

function ControllerLogin:onUCenterGuestConvert(status, response, error)
    --[[  var c = CasinosContext.Instance;

        UiHelperCasinos.UiEndWaiting();

        if (status == UCenterResponseStatus.Success)
        {
            string info = "游客帐号转正成功";
            UiHelperCasinos.UiShowInfoSuccess(info);

            c.CoNetMonitor.AccId = response.AccountId;
            c.CoNetMonitor.Acc = response.AccountName;

            c.PlayerPrefs.ClearLoginGuestInfo();

            _requestLogin(response.AccountName, Password);
        }
        else if (error != null)
        {
            string error_msg = CasinosContext.Instance.ParseUCenterErrorCode(error.code);
            UiHelperCasinos.UiShowInfoFailed(error_msg);
        }--]]
end

function ControllerLogin:OnAccountGatewayConnected()
    print("OnAccountGatewayConnected")
    local login_request = ClientLoginAppRequest:new(nil)
    login_request.acc_id = self.AccId
    login_request.acc_name = self.Acc
    login_request.token = self.Token
    local nick_name = CS.Casinos.LuaHelper.getDeviceName()
    local e = string.find(nick_name, "unknown")
    if (e ~= nil)
    then
        nick_name = "ac" .. CS.UnityEngine.Random.Range(100000, 999999)
    end
    nick_name = ViewHelper:subStrToTargetLength(nick_name, 9)
    login_request.nick_name = nick_name
    local channel_name = CS.Casinos.CasinosContext.Instance:GetChannelName()
    login_request.channel_id = channel_name
    login_request.platform = CS.Casinos.CasinosContext.Instance:getPlatformName(true)
    self.ControllerMgr.RPC:RPC1(CommonMethodType.AccountLoginAppRequest, login_request:getData4Pack())
end

function ControllerLogin:OnAccountLoginAppResponse(login_response)
    -- 请求进入游戏世界
    print("OnAccountLoginAppResponse")
    local enterworld_request = ClientEnterWorldRequest:new(nil)
    enterworld_request.acc_id = self.AccId
    enterworld_request.acc_name = self.Acc
    enterworld_request.token = self.Token
    local invite_payerid = "InvitePlayerId"
    if (CS.UnityEngine.PlayerPrefs.HasKey(invite_payerid))
    then
        local s = CS.UnityEngine.PlayerPrefs.GetString(invite_payerid)
        print("s   " .. s)
        local t_decode = self.ControllerMgr.Listener.Json.decode(s)
        local new = t_decode["IsNew"]
        print("new   " .. tostring(new))
        if new then
            enterworld_request.invite_id = t_decode["PlayerId"]
        end
    end
    self.ControllerMgr.RPC:RPC1(CommonMethodType.AccountEnterWorldRequest, enterworld_request:getData4Pack())
end

function ControllerLogin:OnAccountEnterWorldResponse(enterworld_notify1)
    ViewHelper:UiEndWaiting()
    print("OnAccountEnterWorldResponse")
    local enterworld_notify = ClientEnterWorldNotify:new(nil)
    enterworld_notify:setData(enterworld_notify1)

    if (enterworld_notify.result ~= ProtocolResult.Success
            or enterworld_notify.player_data == nil)
    then
        -- 进入游戏世界失败，则断开连接
        local s = self.ControllerMgr.LanMgr:getLanValue("EnterGameFailed")
        if (enterworld_notify ~= nil)
        then
            s = table.concat({ s, "，ErrorCode=", enterworld_notify.result })
        end
        ViewHelper:UiShowInfoFailed(s)

        self:_disconnect()
    else
        local invite_payerid = "InvitePlayerId"
        if (CS.UnityEngine.PlayerPrefs.HasKey(invite_payerid))
        then
            local s = CS.UnityEngine.PlayerPrefs.GetString(invite_payerid)
            local t_decode = self.ControllerMgr.Listener.Json.decode(s)
            t_decode["IsNew"] = false
            local t_encode = self.ControllerMgr.Listener.Json.encode(t_decode)
            CS.UnityEngine.PlayerPrefs.SetString(invite_payerid, t_encode)
        end
        self.ClientEnterWorldNotify = enterworld_notify
        self.ControllerMgr:CreatePlayerControllers(self.ClientEnterWorldNotify.player_data, self.ClientEnterWorldNotify.player_guid)
    end
end

function ControllerLogin:OnAccountLogoutNotify(protocal_result)
    if (protocal_result == ProtocolResult.LogoutNewLogin)
    then
        self.ShowKickOutInfo = true
    end

    self:_disconnect()
end

function ControllerLogin:OnAccountUpdateDataFromUCenterNotify(result)
    if result ~= nil then
        local we_chat1 = AttachWechatMp:new(nil)
        we_chat1:setData(result)

        local ev = self.ViewMgr:getEv("EvBindWeChatSuccess")
        if(ev == nil)
        then
            ev = EvBindWeChatSuccess:new(nil)
        end
        ev.IsSuccess = true
        ev.WeChatOpenId = we_chat1.open_id
        ev.WeChatName = we_chat1.nick_name
        self.ViewMgr:sendEv(ev)
    end
end

function ControllerLogin:getClientEnterWorldNotify()
    return self.ClientEnterWorldNotify
end

function ControllerLogin:entityPlayerInitDone()
    self.ClientEnterWorldNotify = nil
end

function ControllerLogin:OnSocketClose()
    ControllerLogin.ControllerMgr:DestroyPlayerControllers()
    ControllerLogin:_init(false)
    if (ControllerLogin.ShowKickOutInfo)
    then
        ControllerLogin.ShowKickOutInfo = false
        local info = ControllerLogin.ControllerMgr.LanMgr:getLanValue("AlreadyLogin")
        ViewHelper:UiShowInfoFailed(info)
    end
end

function ControllerLogin:canDestroyViewLogin()
    self.AutoLoginTm = 0
    self.AutoLogin = false
    local view_login = self.ViewMgr:getView("Login")
    self.ViewMgr:destroyView(view_login)
end

function ControllerLogin:getDeviceInfo()
    local device_info = DeviceInfo:new(nil)--CS.GameCloud.UCenter.Common.Portable.Models.AppClient.DeviceInfo()
    device_info.Id = CS.Casinos.LuaHelper.getDeviceUniqueIdentifier()
    device_info.Model = CS.Casinos.LuaHelper.getDeviceModel()
    device_info.Name = CS.Casinos.LuaHelper.getDeviceName()
    device_info.OperationSystem = CS.Casinos.LuaHelper.getDeviceOperatingSystem()
    device_info.Type = CS.Casinos.LuaHelper.getDevicedeviceType()

    return device_info;
end

function ControllerLogin:_disconnect()
    self.AccId = nil
    self.Token = nil
    c.NetBridge:disconnect()
end

function ControllerLogin:_init(is_init)
    local c = CS.Casinos.CasinosContext.Instance
    local data_version = c.Config.InitDataVersion
    local data_version_key = CS.Casinos.CasinosContext.LocalDataVersionKey
    if (CS.UnityEngine.PlayerPrefs.HasKey(data_version_key))
    then
        data_version = CS.UnityEngine.PlayerPrefs.GetString(data_version_key)
    end

    -- 显示登录界面
    ViewHelper:UiEndWaiting()
    local pre_loading = c:getPreView("PreLoading")
    c:destroyPreView(pre_loading)
    local acc = ""
    local pwd = ""
    local view_login = self.ViewMgr:createView("Login")
    self.RequestThirdPartyLogin = false
    if (CS.UnityEngine.PlayerPrefs.HasKey(self.LoginAccountInfoKey))
    then
        local s = CS.UnityEngine.PlayerPrefs.GetString(self.LoginAccountInfoKey)
        local t_decode = self.ControllerMgr.Listener.Json.decode(s)
        local infos = LoginAccountInfos:new(nil)
        infos:setData(t_decode)
        local a_info_last_login = infos.TLoginAccountInfo[tostring(infos.LastLoginType)]

        if is_init then
            self.AutoLogin = true
            view_login:Switch2Logining()
            if (a_info_last_login.LoginType == 0)
            then
                acc = a_info_last_login.AccName
                pwd = a_info_last_login.Pwd
                self.RemeberPwd = true
                self.Phone = a_info_last_login.AccName
                self.Password = pwd
                print("auto requestLogin")
                self:requestLogin("", pwd, a_info_last_login.Phone, "", "")
            elseif (a_info_last_login.LoginType == 1)
            then
                print("auto requestGuestAccess")
                self:requestGuestAccess()
            elseif (a_info_last_login.LoginType == 2)
            then
                CS.Casinos.CasinosContext.Instance.LoginType = 2
                local r = AccountWeChatAutoLoginRequest:new(nil)
                r.AppId = CS.Casinos.CasinosContext.Instance.Config.WeChatAppId
                r.OpenId = a_info_last_login.AccName
                print("auto wechatAutoLogin")
                self.ControllerUCenter:wechatAutoLogin(r,
                        function(status, response, error)
                            self:onUCenterLogin(status, response, error)
                        end)
            end
        else
            local a_info_acc_login = infos.TLoginAccountInfo["0"]
            if (a_info_acc_login ~= nil)
            then
                acc = a_info_acc_login.AccName
                pwd = a_info_acc_login.Pwd
            end
        end
    end
    view_login:SetVersionAndServerStateInfo(CS.UnityEngine.Application.version, data_version,
            c.ServerIsInvalid, c.ServerStateInfo)
    view_login:SetAccPwd(acc, pwd)
    self.ViewMgr:createView("Pool")
    c:play("background", CS.Casinos._eSoundLayer.Background)
end

function ControllerLogin:needCheckIdCard()
    if CS.Casinos.CasinosContext.Instance.LoginType == CS.Casinos._eLoginType.Guest then
        return false
    end

    if self.Identity == nil or #self.Identity == 0 then
        return true
    end

    return false
end

function ControllerLogin:_clickCheckDataCallBack(bo)
    if bo == false then
        if (self.ControllerUCenter.WWWLogin ~= nil)
        then
            local info = self.ControllerMgr.LanMgr:getLanValue("Logining")
            ViewHelper:UiShowInfoFailed(info)
        else
            if (self.LoginType == 1)
            then
                self:requestGuestAccess()
            end

            if (self.LoginType == 0)
            then
                self:requestLogin("", self.Pwd, self.Acc, "", "")
            end

            if (self.LoginType == 2)
            then
                ViewHelper:UiBeginWaiting(self.ControllerMgr.LanMgr:getLanValue("WeChatLogining"), 10)
                self.RequestThirdPartyLogin = true
            end
        end
    else
        local view_login = self.ViewMgr:getView("Login")
        self.ViewMgr:destroyView(view_login)
        local view_pool = self.ViewMgr:getView("Pool")
        self.ViewMgr:destroyView(view_pool)
    end
end

function ControllerLogin:_autoCheckDataCallBack(bo)
    if bo == false then
        if (self.ControllerUCenter.WWWLogin ~= nil)
        then
            local info = self.ControllerMgr.LanMgr:getLanValue("Logining")
            ViewHelper:UiShowInfoFailed(info)
        else
            if (self.LoginType == 1)
            then
                self:requestGuestAccess()
            end

            if (self.LoginType == 0)
            then
                self:requestLogin("", self.Pwd, self.Acc, "", "")
            end

            if (self.LoginType == 2)
            then
                ViewHelper:UiBeginWaiting(self.ControllerMgr.LanMgr:getLanValue("WeChatLogining"), 10)
                self.RequestThirdPartyLogin = true
            end
        end
    end
end

ControllerLoginFactory = ControllerFactory:new()

function ControllerLoginFactory:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.ControllerName = "Login"
    return o
end

function ControllerLoginFactory:createController(controller_mgr, controller_data, guid)
    local controller = ControllerLogin:new(nil, controller_mgr, controller_data, guid)
    controller:onCreate()
    return controller
end