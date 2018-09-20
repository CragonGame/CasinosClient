-- Copyright(c) Cragon. All rights reserved.

ControllerUCenter = ControllerBase:new(nil)

function ControllerUCenter:new(o, controller_mgr, controller_data, guid)
    o = o or {}
    setmetatable(o, self)
    self.__index = self

    if (self.Instance == nil)
    then
        self.ControllerName = "Login"
        self.ControllerData = controller_data
        self.ControllerMgr = controller_mgr
        self.Guid = guid
        self.WWWGetPhoneVerificationCode = nil
        self.WWWRegister = nil
        self.WWWLogin = nil
        self.WWWResetPasswordWithPhone = nil
        self.WWWWeChatAutoLogin = nil
        self.WWWWechatLogin = nil
        self.WWWWechatBind = nil
        self.WWWWechatUnbind = nil
        self.WWWGuestAccess = nil
        self.WWWGuestConvert = nil
        self.WWWResetPassword = nil
        self.WWWUploadProfileImage = nil
        self.WWWGetAppConfig = nil
        self.WWWPayCreateCharge = nil
        self.WWWCheckIdCard = nil
        self.WWWNigWebpayRequestUrl = nil
        self.WWWNigWebpayQuery = nil
        self.WWWQuicktellerTransfers = nil
        self.WWWQuicktellerTransfersQuery = nil
        self.GetPhoneVerificationCodeHandler = nil
        self.RegisterHandler = nil
        self.LoginHandler = nil
        self.ResetPasswordWithPhoneHandler = nil
        self.WeChatAutoLoginHandler = nil
        self.WechatLoginHandler = nil
        self.WechatBindHandler = nil
        self.WechatUnbindHandler = nil
        self.GuestAccessHandler = nil
        self.GuestConvertHandler = nil
        self.ResetPasswordHandler = nil
        self.UploadProfileImageHandler = nil
        self.GetAppConfigHandler = nil
        self.PayCreateChargeHandler = nil
        self.CheckIdCardHandler = nil
        self.NigWebpayRequestUrlHandler = nil
        self.NigWebpayQueryHandler = nil
        self.QuicktellerTransfersHandler = nil
        self.QuicktellerTransfersQueryHandler = nil
        self.UCenterDomain = nil
        self.Instance = o
    end

    return self.Instance
end

function ControllerUCenter:onCreate()
    --print("ControllerUCenter:onCreate")
    self.UCenterDomain = CS.Casinos.CasinosContext.Instance.UserConfig.Current.UCenterDomain
end

function ControllerUCenter:onDestroy()
end

function ControllerUCenter:onUpdate(tm)
    if (self.WWWGetPhoneVerificationCode ~= nil)
    then
        local r = self:_checkResponse(self.WWWGetPhoneVerificationCode, self.GetPhoneVerificationCodeHandler)
        if (r == true)
        then
            self.WWWGetPhoneVerificationCode = nil
            self.GetPhoneVerificationCodeHandler = nil
        end
    end

    if (self.WWWRegister ~= nil)
    then
        local r = self:_checkResponse(self.WWWRegister, self.RegisterHandler)
        if (r == true)
        then
            self.WWWRegister = nil
            self.RegisterHandler = nil
        end
    end

    if (self.WWWLogin ~= nil)
    then
        local r = self:_checkResponse(self.WWWLogin, self.LoginHandler)
        if (r == true)
        then
            self.WWWLogin = nil
            self.LoginHandler = nil
        end
    end

    if (self.WWWResetPasswordWithPhone ~= nil)
    then
        local r = self:_checkResponse(self.WWWResetPasswordWithPhone, self.ResetPasswordWithPhoneHandler)
        if (r == true)
        then
            self.WWWResetPasswordWithPhone = nil
            self.ResetPasswordWithPhoneHandler = nil
        end
    end

    if (self.WWWWeChatAutoLogin ~= nil)
    then
        local r = self:_checkResponse(self.WWWWeChatAutoLogin, self.WeChatAutoLoginHandler)
        if (r == true)
        then
            self.WWWWeChatAutoLogin = nil
            self.WeChatAutoLoginHandler = nil
        end
    end

    if (self.WWWWechatLogin ~= nil)
    then
        local r = self:_checkResponse(self.WWWWechatLogin, self.WechatLoginHandler)
        if (r == true)
        then
            self.WWWWechatLogin = nil
            self.WechatLoginHandler = nil
        end
    end

    if (self.WWWWechatBind ~= nil)
    then
        local r = self:_checkResponse(self.WWWWechatBind, self.WechatBindHandler)
        if (r == true)
        then
            self.WWWWechatBind = nil
            self.WechatBindHandler = nil
        end
    end

    if (self.WWWWechatUnbind ~= nil)
    then
        local r = self:_checkResponse(self.WWWWechatUnbind, self.WechatUnbindHandler)
        if (r == true)
        then
            self.WWWWechatUnbind = nil
            self.WechatUnbindHandler = nil
        end
    end

    if (self.WWWGuestAccess ~= nil)
    then
        local r = self:_checkResponse(self.WWWGuestAccess, self.GuestAccessHandler)
        if (r == true)
        then
            self.WWWGuestAccess = nil
            self.GuestAccessHandler = nil
        end
    end

    if (self.WWWGuestConvert ~= nil)
    then
        local r = self:_checkResponse(self.WWWGuestConvert, self.GuestConvertHandler)
        if (r == true)
        then
            self.WWWGuestConvert = nil
            self.GuestConvertHandler = nil
        end
    end

    if (self.WWWResetPassword ~= nil)
    then
        local r = self:_checkResponse(self.WWWResetPassword, self.ResetPasswordHandler)
        if (r == true)
        then
            self.WWWResetPassword = nil
            self.ResetPasswordHandler = nil
        end
    end

    if (self.WWWUploadProfileImage ~= nil)
    then
        local r = self:_checkResponse(self.WWWUploadProfileImage, self.UploadProfileImageHandler)
        if (r == true)
        then
            self.WWWUploadProfileImage = nil
            self.UploadProfileImageHandler = nil
        end
    end

    if (self.WWWGetAppConfig ~= nil)
    then
        local r = self:_checkResponse(self.WWWGetAppConfig, self.GetAppConfigHandler)
        if (r == true)
        then
            self.WWWGetAppConfig = nil
            self.GetAppConfigHandler = nil
        end
    end

    if (self.WWWPayCreateCharge ~= nil)
    then
        local r = self:_checkResponse(self.WWWPayCreateCharge, self.PayCreateChargeHandler)
        if (r == true)
        then
            self.WWWPayCreateCharge = nil
            self.PayCreateChargeHandler = nil
        end
    end

    if (self.WWWCheckIdCard ~= nil)
    then
        local r = self:_checkResponse(self.WWWCheckIdCard, self.CheckIdCardHandler)
        if (r == true)
        then
            self.WWWCheckIdCard = nil
            self.CheckIdCardHandler = nil
        end
    end

    if (self.WWWNigWebpayRequestUrl ~= nil)
    then
        local r = self:_checkResponse(self.WWWNigWebpayRequestUrl, self.NigWebpayRequestUrlHandler)
        if (r == true)
        then
            self.WWWNigWebpayRequestUrl = nil
            self.NigWebpayRequestUrlHandler = nil
        end
    end

    if (self.WWWNigWebpayQuery ~= nil)
    then
        local r = self:_checkResponse(self.WWWNigWebpayQuery, self.NigWebpayQueryHandler)
        if (r == true)
        then
            self.WWWNigWebpayQuery = nil
            self.NigWebpayQueryHandler = nil
        end
    end

    if (self.WWWQuicktellerTransfers ~= nil)
    then
        local r = self:_checkResponse(self.WWWQuicktellerTransfers, self.QuicktellerTransfersHandler)
        if (r == true)
        then
            self.WWWQuicktellerTransfers = nil
            self.QuicktellerTransfersHandler = nil
        end
    end

    if (self.WWWQuicktellerTransfersQuery ~= nil)
    then
        local r = self:_checkResponse(self.WWWQuicktellerTransfersQuery, self.QuicktellerTransfersQueryHandler)
        if (r == true)
        then
            self.WWWQuicktellerTransfersQuery = nil
            self.QuicktellerTransfersQueryHandler = nil
        end
    end
end

function ControllerUCenter:onHandleEv(ev)

end

function ControllerUCenter:getPhoneVerificationCode(request, handler)
    if (self.WWWGetPhoneVerificationCode ~= nil)
    then
        return
    end

    self.GetPhoneVerificationCodeHandler = handler

    local http_url = self:_genUrl("getphoneverificationcode")
    local param = self.ControllerMgr.Listener.Json.encode(request)
    local bytes = CS.Casinos.LuaHelper.string2BytesByUTF8(param)-- Encoding.UTF8.GetBytes(param)
    local headers = self:_genHeader(bytes.Length)

    self.WWWGetPhoneVerificationCode = CS.UnityEngine.WWW(http_url, bytes, headers)
end

function ControllerUCenter:register(request, handler)
    if (self.WWWRegister ~= nil)
    then
        return
    end

    self.RegisterHandler = handler--new Action<UCenterResponseStatus, AccountRegisterResponse, UCenterError>(handler)

    local http_url = self:_genUrl("register")
    local param = self.ControllerMgr.Listener.Json.encode(request)
    local bytes = CS.Casinos.LuaHelper.string2BytesByUTF8(param)-- Encoding.UTF8.GetBytes(param)
    local headers = self:_genHeader(bytes.Length)

    self.WWWRegister = CS.UnityEngine.WWW(http_url, bytes, headers)
end

function ControllerUCenter:login(request, handler)
    if (self.WWWLogin ~= nil)
    then
        return
    end

    self.LoginHandler = handler--new Action<UCenterResponseStatus, AccountLoginResponse, UCenterError>(handler)

    local http_url = self:_genUrl("login")
    local param = self.ControllerMgr.Listener.Json.encode(request)
    local bytes = CS.Casinos.LuaHelper.string2BytesByUTF8(param)-- Encoding.UTF8.GetBytes(param)
    local headers = self:_genHeader(bytes.Length)

    self.WWWLogin = CS.UnityEngine.WWW(http_url, bytes, headers)
end

function ControllerUCenter:resetPasswordWithPhone(request, handler)
    if (self.WWWResetPasswordWithPhone ~= nil)
    then
        return
    end

    self.ResetPasswordWithPhoneHandler = handler--new Action<UCenterResponseStatus, AccountLoginResponse, UCenterError>(handler)

    local http_url = self:_genUrl("resetpasswordbyphone")
    local param = self.ControllerMgr.Listener.Json.encode(request)
    local bytes = CS.Casinos.LuaHelper.string2BytesByUTF8(param)-- Encoding.UTF8.GetBytes(param)
    local headers = self:_genHeader(bytes.Length)

    self.WWWResetPasswordWithPhone = CS.UnityEngine.WWW(http_url, bytes, headers)
end

function ControllerUCenter:wechatAutoLogin(request, handler)
    if (self.WWWWeChatAutoLogin ~= nil)
    then
        return
    end

    self.WeChatAutoLoginHandler = handler--new Action<UCenterResponseStatus, AccountLoginResponse, UCenterError>(handler)

    local http_url = self:_genUrl("wechatautologin")
    local param = self.ControllerMgr.Listener.Json.encode(request)
    local bytes = CS.Casinos.LuaHelper.string2BytesByUTF8(param)-- Encoding.UTF8.GetBytes(param)
    local headers = self:_genHeader(bytes.Length)

    self.WWWWeChatAutoLogin = CS.UnityEngine.WWW(http_url, bytes, headers)
end

function ControllerUCenter:wechatLogin(request, handler)
    if (self.WWWWechatLogin ~= nil)
    then
        return
    end

    self.WechatLoginHandler = handler--new Action<UCenterResponseStatus, AccountLoginResponse, UCenterError>(handler)

    local http_url = self:_genUrl("wechatlogin")
    local param = self.ControllerMgr.Listener.Json.encode(request)
    local bytes = CS.Casinos.LuaHelper.string2BytesByUTF8(param)-- Encoding.UTF8.GetBytes(param)
    local headers = self:_genHeader(bytes.Length)

    self.WWWWechatLogin = CS.UnityEngine.WWW(http_url, bytes, headers)
end

function ControllerUCenter:wechatBind(request, handler)
    if (self.WWWWechatBind ~= nil)
    then
        return
    end

    self.WechatBindHandler = handler--new Action<UCenterResponseStatus, AccountLoginResponse, UCenterError>(handler)

    local http_url = self:_genUrl("wechatattach")
    local param = self.ControllerMgr.Listener.Json.encode(request)
    print("wechatBind  " .. param)
    local bytes = CS.Casinos.LuaHelper.string2BytesByUTF8(param)-- Encoding.UTF8.GetBytes(param)
    local headers = self:_genHeader(bytes.Length)

    self.WWWWechatBind = CS.UnityEngine.WWW(http_url, bytes, headers)
end

function ControllerUCenter:wechatUnbind(request, handler)
    if (self.WWWWechatUnbind ~= nil)
    then
        return
    end

    self.WechatUnbindHandler = handler--new Action<UCenterResponseStatus, AccountLoginResponse, UCenterError>(handler)

    local http_url = self:_genUrl("wechatdeattach")
    local param = self.ControllerMgr.Listener.Json.encode(request)
    local bytes = CS.Casinos.LuaHelper.string2BytesByUTF8(param)-- Encoding.UTF8.GetBytes(param)
    local headers = self:_genHeader(bytes.Length)

    self.WWWWechatUnbind = CS.UnityEngine.WWW(http_url, bytes, headers)
end

function ControllerUCenter:guestAccess(request, handler)
    if (self.WWWGuestAccess ~= nil)
    then
        return
    end

    self.GuestAccessHandler = handler--new Action<UCenterResponseStatus, GuestAccessResponse, UCenterError>(handler)

    local http_url = self:_genUrl("guestaccess")
    local param = self.ControllerMgr.Listener.Json.encode(request)
    local bytes = CS.Casinos.LuaHelper.string2BytesByUTF8(param)-- Encoding.UTF8.GetBytes(param)
    local headers = self:_genHeader(bytes.Length)

    self.WWWGuestAccess = CS.UnityEngine.WWW(http_url, bytes, headers)
end

function ControllerUCenter:guestConvert(request, handler)
    if (self.WWWGuestConvert ~= nil)
    then
        return
    end

    self.GuestConvertHandler = handler--new Action<UCenterResponseStatus, GuestConvertResponse, UCenterError>(handler)

    local http_url = self:_genUrl("guestconvert")
    local param = self.ControllerMgr.Listener.Json.encode(request)
    local bytes = CS.Casinos.LuaHelper.string2BytesByUTF8(param)-- Encoding.UTF8.GetBytes(param)
    local headers = self:_genHeader(bytes.Length)

    self.WWWGuestConvert = CS.UnityEngine.WWW(http_url, bytes, headers)
end

function ControllerUCenter:resetPassword(request, handler)
    if (self.WWWResetPassword ~= nil)
    then
        return
    end

    self.ResetPasswordHandler = handler--new Action<UCenterResponseStatus, AccountResetPasswordResponse, UCenterError>(handler)

    local http_url = self:_genUrl("resetpassword")
    local param = self.ControllerMgr.Listener.Json.encode(request)
    local bytes = CS.Casinos.LuaHelper.string2BytesByUTF8(param)-- Encoding.UTF8.GetBytes(param)
    local headers = self:_genHeader(bytes.Length)

    self.WWWResetPassword = CS.UnityEngine.WWW(http_url, bytes, headers)
end

function ControllerUCenter:uploadProfileImage(app_id, account_id, bytes, handler)
    if (self.WWWUploadProfileImage ~= nil)
    then
        return
    end

    self.UploadProfileImageHandler = handler--new Action<UCenterResponseStatus, AccountUploadProfileImageResponse, UCenterError>(handler)

    local sb = {}
    table.insert(sb, account_id)
    table.insert(sb, "/")
    table.insert(sb, app_id)
    table.insert(sb, "/upload")
    local url_format = table.concat(sb)
    local http_url = self:_genUrl(url_format)
    local www_form = CS.UnityEngine.WWWForm()
    www_form:AddField("frameCount", tostring(CS.UnityEngine.Time.frameCount))
    www_form:AddBinaryData("file", bytes, "profile.jpg", "image/jpg")
    self.WWWUploadProfileImage = CS.UnityEngine.WWW(http_url, www_form)
end

function ControllerUCenter:payCreateCharge(payment_info, handler)
    if (self.WWWPayCreateCharge ~= nil)
    then
        return
    end

    self.PayCreateChargeHandler = handler--new Action<UCenterResponseStatus, PaymentResponse, UCenterError>(handler)

    local http_url = self:_genPayUrl("createcharge")
    local param = self.ControllerMgr.Listener.Json.encode(payment_info)
    local bytes = CS.Casinos.LuaHelper.string2BytesByUTF8(param)-- Encoding.UTF8.GetBytes(param)
    local headers = self:_genHeader(bytes.Length)

    self.WWWPayCreateCharge = CS.UnityEngine.WWW(http_url, bytes, headers)
end

function ControllerUCenter:checkCardAndName(request, handler)
    if (self.WWWCheckIdCard ~= nil)
    then
        return
    end

    self.CheckIdCardHandler = handler--new Action<UCenterResponseStatus, AccountLoginResponse, UCenterError>(handler)

    local http_url = self:_genIdCardUrl("checkcardandname")
    local param = self.ControllerMgr.Listener.Json.encode(request)
    local bytes = CS.Casinos.LuaHelper.string2BytesByUTF8(param)-- Encoding.UTF8.GetBytes(param)
    local headers = self:_genHeader(bytes.Length)

    self.WWWCheckIdCard = CS.UnityEngine.WWW(http_url, bytes, headers)
end

function ControllerUCenter:nigWebpayRequestUrl(handler)
    if (self.WWWNigWebpayRequestUrl ~= nil)
    then
        return
    end

    self.NigWebpayRequestUrlHandler = handler--new Action<UCenterResponseStatus, AccountLoginResponse, UCenterError>(handler)

    local http_url = self:_genApiUrl("nigeriawebpay")
    local form = CS.UnityEngine.WWWForm()

    self.WWWNigWebpayRequestUrl = CS.UnityEngine.WWW(http_url,form)
end

function ControllerUCenter:nigWebpayQuery(handler)
    if (self.WWWNigWebpayQuery ~= nil)
    then
        return
    end

    self.NigWebpayQueryHandler = handler--new Action<UCenterResponseStatus, AccountLoginResponse, UCenterError>(handler)

    local http_url = self:_genApiUrl("nigeriawebpayquery")

    self.WWWNigWebpayQuery = CS.UnityEngine.WWW(http_url)
end

function ControllerUCenter:quicktellerTransfers(request,handler)
    if (self.WWWQuicktellerTransfers ~= nil)
    then
        return
    end

    self.QuicktellerTransfersHandler = handler--new Action<UCenterResponseStatus, AccountLoginResponse, UCenterError>(handler)

    local http_url = self:_genApiUrl("paynigeria/quickteller/payments/transfers")
    local param = self.ControllerMgr.Listener.Json.encode(request)
    local bytes = CS.Casinos.LuaHelper.string2BytesByUTF8(param)-- Encoding.UTF8.GetBytes(param)
    local headers = self:_genHeader(bytes.Length)

    self.WWWQuicktellerTransfers = CS.UnityEngine.WWW(http_url,bytes, headers)
end

function ControllerUCenter:quicktellerTransfersQuery(handler)
    if (self.WWWQuicktellerTransfersQuery ~= nil)
    then
        return
    end

    self.QuicktellerTransfersQueryHandler = handler--new Action<UCenterResponseStatus, AccountLoginResponse, UCenterError>(handler)

    local http_url = self:_genApiUrl("paynigeria/quickteller/payments/transfersquery")
    local form = CS.UnityEngine.WWWForm()

    self.WWWQuicktellerTransfersQuery = CS.UnityEngine.WWW(http_url,form)
end

function ControllerUCenter:ParseUCenterErrorCode(error_code)
    local error_msg = ""
    if (error_code == UCenterErrorCode.NoError)
    then

    elseif (error_code == UCenterErrorCode.ClientError)
    then
        error_msg = "UCenterEClientError"
    elseif (error_code == UCenterErrorCode.InvalidAccountName)
    then
        error_msg = "UCenterEInvalidAccountName"
    elseif (error_code == UCenterErrorCode.InvalidAccountPassword)
    then
        error_msg = "UCenterEInvalidAccountPassword"
    elseif (error_code == UCenterErrorCode.InvalidAccountEmail)
    then
        error_msg = "UCenterEInvalidAccountEmail"
    elseif (error_code == UCenterErrorCode.InvalidAccountPhone)
    then
        error_msg = "UCenterEInvalidAccountPhone"
    elseif (error_code == UCenterErrorCode.DeviceInfoNull)
    then
        error_msg = "UCenterEDeviceInfoNull"
    elseif (error_code == UCenterErrorCode.DeviceIdNull)
    then
        error_msg = "UCenterEDeviceIdNull"
    elseif (error_code == UCenterErrorCode.AccountPasswordUnauthorized)
    then
        error_msg = "UCenterEAccountPasswordUnauthorized"
    elseif (error_code == UCenterErrorCode.AccountTokenUnauthorized)
    then
        error_msg = "UCenterEAccountTokenUnauthorized"
    elseif (error_code == UCenterErrorCode.AppTokenUnauthorized)
    then
        error_msg = "UCenterEAppTokenUnauthorized"
    elseif (error_code == UCenterErrorCode.AccountDisabled)
    then
        error_msg = "UCenterEAccountDisabled"
    elseif (error_code == UCenterErrorCode.AccountOAuthTokenUnauthorized)
    then
        error_msg = "UCenterEAccountOAuthTokenUnauthorized"
    elseif (error_code == UCenterErrorCode.PhoneVerificationCodeError)
    then
        error_msg = "UCenterEPhoneVerificationCodeError"
    elseif (error_code == UCenterErrorCode.PayUnauthorized)
    then
        error_msg = "UCenterEPayUnauthorized"
    elseif (error_code == UCenterErrorCode.AccountNotExist)
    then
        error_msg = "UCenterEAccountNotExist"
    elseif (error_code == UCenterErrorCode.AppNotExists)
    then
        error_msg = "UCenterEAppNotExists"
    elseif (error_code == UCenterErrorCode.OrderNotExists)
    then
        error_msg = "UCenterEOrderNotExists"
    elseif (error_code == UCenterErrorCode.AccountNameAlreadyExist)
    then
        error_msg = "UCenterEAccountNameAlreadyExist"
    elseif (error_code == UCenterErrorCode.AppNameAlreadyExist)
    then
        error_msg = "UCenterEAppNameAlreadyExist"
    elseif (error_code == UCenterErrorCode.WechatAccountCanntAttachWechat)
    then
        error_msg = "UCenterEWechatAccountCanntAttachWechat"
    elseif (error_code == UCenterErrorCode.AttachWechatExists)
    then
        error_msg = "UCenterEAttachWechatExists"
    elseif (error_code == UCenterErrorCode.AttachWechatAttachCountMax)
    then
        error_msg = "UCenterEAttachWechatAttachCountMax"

    elseif (error_code == UCenterErrorCode.InternalDatabaseError)
    then
        error_msg = "UCenterEInternalDatabaseError"
    elseif (error_code == UCenterErrorCode.InternalHttpServerError)
    then
        error_msg = "UCenterEInternalHttpServerError"
    elseif (error_code == UCenterErrorCode.ServiceUnavailable)
    then
        error_msg = "UCenterEServiceUnavailable"
    else
        error_msg = "UCenterEUnknow"
    end

    return self.ControllerMgr.LanMgr:getLanValue(error_msg)
end

function ControllerUCenter:_genHeader(content_len)
    local headers = {}
    headers["Accept"] = "application/x-www-form-urlencoded"
    headers["Content-Type"] = "application/json charset=utf-8"
    --headers["Content-Length"] = tostring(content_len)
    headers["host"] = self:_getHostName()
    headers["User-Agent"] = ""

    return headers
end

function ControllerUCenter:_getHostName()
    local host = self.UCenterDomain

    host = string.gsub(host, "https://", "")--host:Replace("https://", "")
    host = string.gsub(host, "http://", "")--host:Replace("http://", "")

    --if (host:EndsWith("/"))
    --then
    --    host = host:TrimEnd('/')
    --end

    return host
end

function ControllerUCenter:_genUrl(api)
    local http_url = nil
    local index = string.find(self.UCenterDomain, "/", -1)
    local l = string.len(self.UCenterDomain)

    if (index == l)
    then
        http_url = string.format("%sapi/accounts/%s", self.UCenterDomain, api)
    else
        http_url = string.format("%s/api/accounts/%s", self.UCenterDomain, api)
    end

    return http_url
end

function ControllerUCenter:_genPayUrl(api)
    local http_url = nil
    local index = string.find(self.UCenterDomain, "/", -1)
    local l = string.len(self.UCenterDomain)

    if (index == l)
    then
        http_url = string.format("%sapi/pay/%s", self.UCenterDomain, api)
    else
        http_url = string.format("%s/api/pay/%s", self.UCenterDomain, api)
    end

    return http_url
end

function ControllerUCenter:_genIdCardUrl(api)
    local http_url = nil
    local index = string.find(self.UCenterDomain, "/", -1)
    local l = string.len(self.UCenterDomain)

    if (index == l)
    then
        http_url = string.format("%sapi/idcard/%s", self.UCenterDomain, api)
    else
        http_url = string.format("%s/api/idcard/%s", self.UCenterDomain, api)
    end

    return http_url
end

function ControllerUCenter:_genApiUrl(api)
    local http_url = nil
    local index = string.find(self.UCenterDomain, "/", -1)
    local l = string.len(self.UCenterDomain)

    if (index == l)
    then
        http_url = string.format("%sapi/%s", self.UCenterDomain, api)
    else
        http_url = string.format("%s/api/%s", self.UCenterDomain, api)
    end

    return http_url
end

function ControllerUCenter:_checkResponse(www, handler)
    if (www ~= nil)
    then
        if (www.isDone)
        then
            local response = nil

            if (CS.System.String.IsNullOrEmpty(www.error))
            then
                response = self.ControllerMgr.Listener.Json.decode(www.text)
            else
                print(www.url)
                print(www.error)
            end

            www = nil

            if (handler ~= nil)
            then
                if (response ~= nil)
                then
                    handler(response.status, response.result, response.error)
                else
                    local error = UCenterError:new(nil)
                    error.ErrorCode = UCenterErrorCode.ServiceUnavailable
                    error.Message = ""
                    handler(UCenterResponseStatus.Error, nil, error)
                end

                handler = nil
            end

            return true
        end
    end

    return false
end

ControllerUCenterFactory = ControllerFactory:new()

function ControllerUCenterFactory:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.ControllerName = "UCenter"
    return o
end

function ControllerUCenterFactory:createController(controller_mgr, controller_data, guid)
    local controller = ControllerUCenter:new(nil, controller_mgr, controller_data, guid)
    controller:onCreate()
    return controller
end