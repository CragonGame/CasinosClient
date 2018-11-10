-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ControllerUCenter = ControllerBase:new(nil)

---------------------------------------
function ControllerUCenter:new(o, controller_mgr, controller_data, guid)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Context = Context
    o.CasinosContext = CS.Casinos.CasinosContext.Instance
    o.ControllerName = "Login"
    o.ControllerData = controller_data
    o.ControllerMgr = controller_mgr
    o.Guid = guid
    o.UCenterDomain = o.Context.Cfg.UCenterDomain
    o.MbHelper = nil
    return o
end

---------------------------------------
function ControllerUCenter:OnCreate()
    --print("ControllerUCenter:OnCreate")
    self.MbHelper = self.CasinosContext.Config.GoMain:GetComponent("Casinos.MbHelper")
end

---------------------------------------
function ControllerUCenter:OnDestroy()
    --print("ControllerUCenter:OnDestroy")
end

---------------------------------------
function ControllerUCenter:OnHandleEv(ev)
end

---------------------------------------
-- 请求获取手机验证码
function ControllerUCenter:RequestGetPhoneVerificationCode(request, handler)
    local http_url = self:_genUrl("getphoneverificationcode")
    local param = self.ControllerMgr.Json.encode(request)
    --print(http_url)
    self.MbHelper:PostUrl(http_url, param,
            function(http_statuscode, response_data)
                self:_onResponse(http_statuscode, response_data, handler)
            end
    )
end

---------------------------------------
-- 请求注册
function ControllerUCenter:RequestRegister(request, handler)
    local http_url = self:_genUrl("register")
    local param = self.ControllerMgr.Json.encode(request)
    --print(http_url)
    self.MbHelper:PostUrl(http_url, param,
            function(http_statuscode, response_data)
                self:_onResponse(http_statuscode, response_data, handler)
            end
    )
end

---------------------------------------
-- 请求登录
function ControllerUCenter:RequestLogin(request, handler)
    local http_url = self:_genUrl("login")
    local param = self.ControllerMgr.Json.encode(request)
    --print(http_url)
    self.MbHelper:PostUrl(http_url, param,
            function(http_statuscode, response_data)
                self:_onResponse(http_statuscode, response_data, handler)
            end
    )
end

---------------------------------------
-- 请求通过手机验证码重置密码
function ControllerUCenter:RequestResetPasswordWithPhone(request, handler)
    local http_url = self:_genUrl("resetpasswordbyphone")
    local param = self.ControllerMgr.Json.encode(request)
    --print(http_url)
    self.MbHelper:PostUrl(http_url, param,
            function(http_statuscode, response_data)
                self:_onResponse(http_statuscode, response_data, handler)
            end
    )
end

---------------------------------------
-- 请求微信自动登录
function ControllerUCenter:RequestWechatAutoLogin(request, handler)
    local http_url = self:_genUrl("wechatautologin")
    local param = self.ControllerMgr.Json.encode(request)
    --print(http_url)
    self.MbHelper:PostUrl(http_url, param,
            function(http_statuscode, response_data)
                self:_onResponse(http_statuscode, response_data, handler)
            end
    )
end

---------------------------------------
-- 请求微信登录
function ControllerUCenter:RequestWechatLogin(request, handler)
    local http_url = self:_genUrl("wechatlogin")
    local param = self.ControllerMgr.Json.encode(request)
    --print(http_url)
    self.MbHelper:PostUrl(http_url, param,
            function(http_statuscode, response_data)
                self:_onResponse(http_statuscode, response_data, handler)
            end
    )
end

---------------------------------------
-- 请求微信绑定
function ControllerUCenter:RequestWechatBind(request, handler)
    local http_url = self:_genUrl("wechatattach")
    local param = self.ControllerMgr.Json.encode(request)
    --print(http_url)
    self.MbHelper:PostUrl(http_url, param,
            function(http_statuscode, response_data)
                self:_onResponse(http_statuscode, response_data, handler)
            end
    )
end

---------------------------------------
-- 请求微信解绑
function ControllerUCenter:RequestWechatUnbind(request, handler)
    local http_url = self:_genUrl("wechatdeattach")
    local param = self.ControllerMgr.Json.encode(request)
    --print(http_url)
    self.MbHelper:PostUrl(http_url, param,
            function(http_statuscode, response_data)
                self:_onResponse(http_statuscode, response_data, handler)
            end
    )
end

---------------------------------------
-- 请求游客访问
function ControllerUCenter:RequestGuestAccess(request, handler)
    local http_url = self:_genUrl("guestaccess")
    local param = self.ControllerMgr.Json.encode(request)
    --print(http_url)
    self.MbHelper:PostUrl(http_url, param,
            function(http_statuscode, response_data)
                self:_onResponse(http_statuscode, response_data, handler)
            end
    )
end

---------------------------------------
-- 请求游客转正
function ControllerUCenter:RequestGuestConvert(request, handler)
    local http_url = self:_genUrl("guestconvert")
    local param = self.ControllerMgr.Json.encode(request)
    --print(http_url)
    self.MbHelper:PostUrl(http_url, param,
            function(http_statuscode, response_data)
                self:_onResponse(http_statuscode, response_data, handler)
            end
    )
end

---------------------------------------
-- 请求重置密码
function ControllerUCenter:RequestResetPassword(request, handler)
    local http_url = self:_genUrl("resetpassword")
    local param = self.ControllerMgr.Json.encode(request)
    --print(http_url)
    self.MbHelper:PostUrl(http_url, param,
            function(http_statuscode, response_data)
                self:_onResponse(http_statuscode, response_data, handler)
            end
    )
end

---------------------------------------
-- 请求上传头像
function ControllerUCenter:RequestUploadProfileImage(app_id, account_id, bytes, handler)
    local uri = account_id .. '/' .. app_id .. '/upload'
    local http_url = self:_genUrl(uri)
    local www_form = CS.UnityEngine.WWWForm()
    www_form:AddField("frameCount", tostring(60))
    www_form:AddBinaryData("file", bytes, "profile.jpg", "image/jpg")
    --print(http_url)
    self.MbHelper:PostUrlWithFormData(http_url, www_form,
            function(http_statuscode, response_data)
                self:_onResponse(http_statuscode, response_data, handler)
            end
    )
end

---------------------------------------
-- 请求支付
function ControllerUCenter:RequestPayCreateCharge(payment_info, handler)
    local http_url = self:_genPayUrl("createcharge")
    local param = self.ControllerMgr.Json.encode(payment_info)
    --print(http_url)
    self.MbHelper:PostUrl(http_url, param,
            function(http_statuscode, response_data)
                self:_onResponse(http_statuscode, response_data, handler)
            end
    )
end

---------------------------------------
-- 请求身份证实名认证（国内）
function ControllerUCenter:RequestCheckCardAndName(request, handler)
    local http_url = self:_genIdCardUrl("checkcardandname")
    local param = self.ControllerMgr.Json.encode(request)
    --print(http_url)
    self.MbHelper:PostUrl(http_url, param,
            function(http_statuscode, response_data)
                self:_onResponse(http_statuscode, response_data, handler)
            end
    )
end

---------------------------------------
-- 请求尼日利亚Webpay支付
--function ControllerUCenter:RequestNigWebpayRequestUrl(handler)
--    self.NigWebpayRequestUrlHandler = handler--new Action<UCenterResponseStatus, AccountLoginResponse, UCenterError>(handler)
--    local http_url = self:_genApiUrl("nigeriawebpay")
--    local form = CS.UnityEngine.WWWForm()
--    self.WWWNigWebpayRequestUrl = CS.UnityEngine.WWW(http_url, form)
--    --print(http_url)
--    --self.MbKingTexasHelper:PostUrl(http_url, param,
--    --        function(http_statuscode, response_data)
--    --            self:_onResponse(http_statuscode, response_data, handler)
--    --        end
--    --)
--end

---------------------------------------
-- 请求尼日利亚Webpay查询
--function ControllerUCenter:RequestNigWebpayQuery(handler)
--    self.NigWebpayQueryHandler = handler--new Action<UCenterResponseStatus, AccountLoginResponse, UCenterError>(handler)
--    local http_url = self:_genApiUrl("nigeriawebpayquery")
--    self.WWWNigWebpayQuery = CS.UnityEngine.WWW(http_url)
--end

---------------------------------------
-- 请求尼日利亚Quickteller支付
--function ControllerUCenter:RequestQuicktellerTransfers(request, handler)
--    self.QuicktellerTransfersHandler = handler--new Action<UCenterResponseStatus, AccountLoginResponse, UCenterError>(handler)
--    local http_url = self:_genApiUrl("paynigeria/quickteller/payments/transfers")
--    local param = self.ControllerMgr.Json.encode(request)
--    local bytes = CS.Casinos.LuaHelper.string2BytesByUTF8(param)-- Encoding.UTF8.GetBytes(param)
--    local headers = self:_genHeader(bytes.Length)
--    self.WWWQuicktellerTransfers = CS.UnityEngine.WWW(http_url, bytes, headers)
--end

---------------------------------------
-- 请求尼日利亚Quickteller查询
--function ControllerUCenter:RequestQuicktellerTransfersQuery(handler)
--    self.QuicktellerTransfersQueryHandler = handler--new Action<UCenterResponseStatus, AccountLoginResponse, UCenterError>(handler)
--    local http_url = self:_genApiUrl("paynigeria/quickteller/payments/transfersquery")
--    local form = CS.UnityEngine.WWWForm()
--    self.WWWQuicktellerTransfersQuery = CS.UnityEngine.WWW(http_url, form)
--end

---------------------------------------
-- 解析Http错误码
function ControllerUCenter:ParseUCenterHttpStatusCode(http_statuscode)
    local error_msg = ""
    if (http_statuscode == 200) then
    else
        error_msg = string.format('Error, HttpStatusCode: %s', http_statuscode)
    end
    --return self.ControllerMgr.LanMgr:getLanValue(error_msg)
    return error_msg
end

---------------------------------------
-- 解析UCenter错误码
function ControllerUCenter:ParseUCenterErrorCode(error_code)
    local error_msg = ""
    if (error_code == UCenterErrorCode.NoError) then
    elseif (error_code == UCenterErrorCode.ClientError) then
        error_msg = "UCenterEClientError"
    elseif (error_code == UCenterErrorCode.InvalidAccountName) then
        error_msg = "UCenterEInvalidAccountName"
    elseif (error_code == UCenterErrorCode.InvalidAccountPassword) then
        error_msg = "UCenterEInvalidAccountPassword"
    elseif (error_code == UCenterErrorCode.InvalidAccountEmail) then
        error_msg = "UCenterEInvalidAccountEmail"
    elseif (error_code == UCenterErrorCode.InvalidAccountPhone) then
        error_msg = "UCenterEInvalidAccountPhone"
    elseif (error_code == UCenterErrorCode.DeviceInfoNull) then
        error_msg = "UCenterEDeviceInfoNull"
    elseif (error_code == UCenterErrorCode.DeviceIdNull) then
        error_msg = "UCenterEDeviceIdNull"
    elseif (error_code == UCenterErrorCode.AccountPasswordUnauthorized) then
        error_msg = "UCenterEAccountPasswordUnauthorized"
    elseif (error_code == UCenterErrorCode.AccountTokenUnauthorized) then
        error_msg = "UCenterEAccountTokenUnauthorized"
    elseif (error_code == UCenterErrorCode.AppTokenUnauthorized) then
        error_msg = "UCenterEAppTokenUnauthorized"
    elseif (error_code == UCenterErrorCode.AccountDisabled) then
        error_msg = "UCenterEAccountDisabled"
    elseif (error_code == UCenterErrorCode.AccountOAuthTokenUnauthorized) then
        error_msg = "UCenterEAccountOAuthTokenUnauthorized"
    elseif (error_code == UCenterErrorCode.PhoneVerificationCodeError) then
        error_msg = "UCenterEPhoneVerificationCodeError"
    elseif (error_code == UCenterErrorCode.PayUnauthorized) then
        error_msg = "UCenterEPayUnauthorized"
    elseif (error_code == UCenterErrorCode.AccountNotExist) then
        error_msg = "UCenterEAccountNotExist"
    elseif (error_code == UCenterErrorCode.AppNotExists) then
        error_msg = "UCenterEAppNotExists"
    elseif (error_code == UCenterErrorCode.OrderNotExists) then
        error_msg = "UCenterEOrderNotExists"
    elseif (error_code == UCenterErrorCode.AccountNameAlreadyExist) then
        error_msg = "UCenterEAccountNameAlreadyExist"
    elseif (error_code == UCenterErrorCode.AppNameAlreadyExist) then
        error_msg = "UCenterEAppNameAlreadyExist"
    elseif (error_code == UCenterErrorCode.WechatAccountCanntAttachWechat) then
        error_msg = "UCenterEWechatAccountCanntAttachWechat"
    elseif (error_code == UCenterErrorCode.AttachWechatExists) then
        error_msg = "UCenterEAttachWechatExists"
    elseif (error_code == UCenterErrorCode.AttachWechatAttachCountMax) then
        error_msg = "UCenterEAttachWechatAttachCountMax"
    elseif (error_code == UCenterErrorCode.InternalDatabaseError) then
        error_msg = "UCenterEInternalDatabaseError"
    elseif (error_code == UCenterErrorCode.InternalHttpServerError) then
        error_msg = "UCenterEInternalHttpServerError"
    elseif (error_code == UCenterErrorCode.ServiceUnavailable) then
        error_msg = "UCenterEServiceUnavailable"
    else
        error_msg = "UCenterEUnknow"
    end
    return self.ControllerMgr.LanMgr:getLanValue(error_msg)
end

---------------------------------------
function ControllerUCenter:_onResponse(http_statuscode, response_data, handler)
    --print(http_statuscode)
    --print(response_data)
    if (handler == nil) then
        return
    end

    if (http_statuscode ~= 200) then
        local error = UCenterError:new(nil)
        error.ErrorCode = UCenterErrorCode.ServiceUnavailable
        error.Message = ""
        handler(http_statuscode, UCenterResponseStatus.Error, nil, error)
        return
    end

    local response = self.ControllerMgr.Json.decode(response_data)
    if (response ~= nil) then
        handler(http_statuscode, response.status, response.result, response.error)
    else
        local error = UCenterError:new(nil)
        error.ErrorCode = UCenterErrorCode.ServiceUnavailable
        error.Message = ""
        handler(http_statuscode, UCenterResponseStatus.Error, nil, error)
    end
end

---------------------------------------
function ControllerUCenter:_genHeader(content_len)
    local headers = {}
    headers["Accept"] = "application/x-www-form-urlencoded"
    headers["Content-Type"] = "application/json charset=utf-8"
    headers["host"] = self:_getHostName()
    headers["User-Agent"] = ""
    --headers["Content-Length"] = tostring(content_len)
    return headers
end

---------------------------------------
function ControllerUCenter:_getHostName()
    local host = self.UCenterDomain
    host = string.gsub(host, "https://", "")--host:Replace("https://", "")
    host = string.gsub(host, "http://", "")--host:Replace("http://", "")
    return host
end

---------------------------------------
function ControllerUCenter:_genUrl(api)
    local http_url = string.format("%s/api/accounts/%s", self.UCenterDomain, api)
    return http_url
end

---------------------------------------
function ControllerUCenter:_genPayUrl(api)
    local http_url = string.format("%s/api/pay/%s", self.UCenterDomain, api)
    return http_url
end

---------------------------------------
function ControllerUCenter:_genIdCardUrl(api)
    local http_url = string.format("%s/api/idcard/%s", self.UCenterDomain, api)
    return http_url
end

---------------------------------------
function ControllerUCenter:_genApiUrl(api)
    local http_url = string.format("%s/api/%s", self.UCenterDomain, api)
    return http_url
end

---------------------------------------
ControllerUCenterFactory = ControllerFactory:new()

---------------------------------------
function ControllerUCenterFactory:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.ControllerName = "UCenter"
    return o
end

---------------------------------------
function ControllerUCenterFactory:CreateController(controller_mgr, controller_data, guid)
    local controller = ControllerUCenter:new(nil, controller_mgr, controller_data, guid)
    controller:OnCreate()
    return controller
end