DeviceInfo = {}

function DeviceInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Id = nil
    o.Name = nil
    o.Type = nil
    o.Model = nil
    o.OperationSystem = nil

    return o
end

GetPhoneVerificationCodeRequest = {}

function GetPhoneVerificationCodeRequest:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Phone = nil

    return o
end

AccountRegisterInfo = {}

function AccountRegisterInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.AppId = nil
    o.AccountName = nil
    o.Password = nil
    o.SuperPassword = nil
    o.Name = nil
    o.Phone = nil
    o.Email = nil
    o.Identity = nil
    o.PhoneVerificationCode = nil
    o.Gender = 0
    o.Device = nil

    return o
end

AccountLoginInfo = {}

function AccountLoginInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.AccountName = nil
    o.Phone = nil
    o.Email = nil
    o.Password = nil
    o.PhoneVerificationCode = nil
    o.Device = nil

    return o
end

AccountWeChatOAuthInfo = {}

function AccountWeChatOAuthInfo:new(o)
    o = o or {}
    setmetatable(o,
            self)
    self.__index = self
    o.AppId = nil
    o.Code = nil

    return o
end

AccountWeChatAutoLoginRequest = {}

function AccountWeChatAutoLoginRequest:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.AppId = nil
    o.OpenId = nil

    return o
end

GuestAccessInfo = {}

function GuestAccessInfo:new(o)
    o = o or {}
    setmetatable(o,
            self)
    self.__index = self
    o.AppId = nil
    o.Device = nil

    return o
end

GuestConvertInfo = {}

function GuestConvertInfo:new(o)
    o = o or {}
    setmetatable(o,
            self)
    self.__index = self
    o.AppId = nil
    o.AccountId = nil
    o.AccountName = nil
    o.Password = nil
    o.SuperPassword = nil
    o.Name = nil
    o.Phone = nil
    o.Email = nil
    o.Identity = nil
    o.Gender = 0

    return o
end

AccountResetPasswordInfo = {}

function AccountResetPasswordInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.AccountName = nil
    o.Phone = nil
    o.Email = nil
    o.Password = nil
    o.SuperPassword = nil

    return o
end

AccountResetPasswordByPhoneRequest = {}

function AccountResetPasswordByPhoneRequest:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.AccountName = nil
    o.Phone = nil
    o.Email = nil
    o.NewPassword = nil
    o.PhoneVerificationCode = nil

    return o
end

PayRequest = {}

function PayRequest:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.AppId = nil
    o.AccountId = nil
    o.IsFirstRecharge = false
    o.ItemTbId = 0
    o.ItemName = nil
    o.Channel = nil
    o.Amount = nil
    o.Currency = nil

    return o
end

UCenterErrorCode = {
    NoError = 0,

-- Error sends http request on client side
    ClientError = 1,

-- BadRequest - 400
    InvalidAccountName = 400001,
    InvalidAccountPassword = 400002,
    InvalidAccountEmail = 400003,
    InvalidAccountPhone = 400004,
    DeviceInfoNull = 400010,
    DeviceIdNull = 400011,

-- Unauthorized - 401
    AccountPasswordUnauthorized = 401001,
    AccountTokenUnauthorized = 401002,
    AppTokenUnauthorized = 401003,
    AccountDisabled = 401004,
    AccountOAuthTokenUnauthorized = 401005,
    PhoneVerificationCodeError = 401006,-- 手机验证码错误
    PayUnauthorized = 401007,-- 支付签名验证错误

-- NotFound - 404
    AccountNotExist = 404001,
    AppNotExists = 404002,
    OrderNotExists = 404003,

-- Conflict - 409
    AccountNameAlreadyExist = 409001,
    AppNameAlreadyExist = 409002,
    WechatAccountCanntAttachWechat = 409003,-- 微信登陆的账号无需再次绑定微信
    AttachWechatExists = 409004,-- 该账号已经绑定微信，无法重复绑定
    AttachWechatAttachCountMax = 409005,-- 该微信绑定账号次数已经达到上限，无法绑定新账号

-- InternalServerError - 500
    InternalDatabaseError = 500001,
    InternalHttpServerError = 500002,

-- ServiceUnavailable - 503
    ServiceUnavailable = 503001,
}

UCenterResponseStatus = {
    Success = 0,
    Error = 1
}

UCenterError = {}

function UCenterError:new(o)
    o = o or {}
    setmetatable(o,
            self)
    self.__index = self
    o.ErrorCode = 0
    o.Message = nil

    return o
end

UCenterResponse = {}

function UCenterResponse:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Result = nil
    o.Status = 0
    o.Error = nil

    return o
end

AccountType ={
    NormalAccount = 0,
    Guest = 1,
}

AccountStatus = {
    Active = 0,
    Disabled = 1,
}

Gender = {
    Unknown = 0,
    Male = 1,
    Female = 2,
}

AccountRequestResponse = {}

function AccountRequestResponse:new(o)
    o = o or {}
    setmetatable(o,
            self)
    self.__index = self
    o.AccountId = nil
    o.AccountName = nil
    o.AccountType = 0
    o.AccountStatus = 0
    o.Name = nil
    o.ProfileImage = nil
    o.ProfileThumbnail = nil
    o.Gender = 0
    o.Identity = false
    o.Phone = nil
    o.Email = nil

    return o
end

AccountLoginResponse = {}

function AccountLoginResponse:new(o)
    o = o or {}
    setmetatable(o,
            self)
    self.__index = self
    o.Token = nil
    o.LastLoginDateTime = nil
    o.AccountId = nil
    o.AccountName = nil
    o.AccountType = 0
    o.AccountStatus = 0
    o.Name = nil
    o.ProfileImage = nil
    o.ProfileThumbnail = nil
    o.Gender = 0
    o.Identity = false
    o.Phone = nil
    o.Email = nil

    return o
end

GuestAccessResponse = {}

function GuestAccessResponse:new(o)
    o = o or {}
    setmetatable(o,
            self)
    self.__index = self
    o.AccountId = nil
    o.AccountName = nil
    o.AccountType = 0
    o.Token = nil

    return o
end

AppConfigurationResponse = {}

function AppConfigurationResponse:new(o)
    o = o or {}
    setmetatable(o,
            self)
    self.__index = self
    o.AppId = nil
    o.Configuration = nil

    return o
end

PayResponse = {}
function PayResponse:new(o)
    o = o or {}
    setmetatable(o,
            self)
    self.__index = self
    o.ChargeId = nil
    o.AppId = nil
    o.AccountId = nil
    o.IsFirstRecharge = false
    o.ItemTbId = 0
    o.ItemName = nil
    o.Channel = nil
    o.Amount = nil
    o.Currency = nil

    return o
end

Charge = {}
function Charge:new(o)
    o = o or {}
    setmetatable(o,
            self)
    self.__index = self
    o.amount = 0
    o.amount_refunded = 0
    o.amount_settle = 0
    o.app = nil
    o.body = nil
    o.channel = nil
    o.client_ip = nil
    o.created = 0
    o.credential = nil
    o.currency = nil
    o.description = nil
    o.extra = nil
    o.failure_code = nil
    o.failure_msg = nil
    o.id = nil
    o.livemode = false
    o.metadata = nil
    o.object = nil
    o.order_no = nil
    o.paid = false
    o.refunded = false
    o.subject = nil
    o.time_expire = 0
    o.time_paid = 0
    o.time_settle = 0
    o.transaction_no = nil

    return o
end

CheckCardAndNameRequest = {}
function CheckCardAndNameRequest:new(o)
    o = o or {}
    setmetatable(o,
            self)
    self.__index = self
    o.AccountId = nil
    o.Token = nil
    o.CardNo = nil
    o.RealName = nil

    return o
end

IdCardDetails = {}
function IdCardDetails:new(o)
    o = o or {}
    setmetatable(o,
            self)
    self.__index = self
    o.addrCode = nil
    o.birth = nil
    o.sex = 0
    o.length = 0
    o.checkBit = nil
    o.addr = nil

    return o
end

IdCardResult = {}
function IdCardResult:new(o)
    o = o or {}
    setmetatable(o,
            self)
    self.__index = self
    o.realName = nil
    o.cardNo = nil
    o.details = nil

    return o
end

IdCardResponse = {}
function IdCardResponse:new(o)
    o = o or {}
    setmetatable(o,
            self)
    self.__index = self
    o.error_code = nil
    o.reason = nil
    o.result = nil
    o.ordersign = nil

    return o
end

AccountWeChatBindRequest = {}
function AccountWeChatBindRequest:new(o)
    o = o or {}
    setmetatable(o,
            self)
    self.__index = self
    o.ucenterAppId = nil
    o.code = nil
    o.accountId = nil
    o.token = nil

    return o
end

AccountWeChatUnbindRequest = {}
function AccountWeChatUnbindRequest:new(o)
    o = o or {}
    setmetatable(o,
            self)
    self.__index = self
    o.ucenterAppId = nil
    o.openId = nil
    o.accountId = nil
    o.token = nil

    return o
end


NigWebPayRequest = {}
function NigWebPayRequest:new(o)
    o = o or {}
    setmetatable(o,
            self)
    self.__index = self
    o.amount = 0

    return o
end


NigWebPayResponse = {}
function NigWebPayResponse:new(o)
    o = o or {}
    setmetatable(o,
            self)
    self.__index = self
    o.amount = 0
    o.transferId = nil

    return o
end

NigWebPayQueryRequest = {}
function NigWebPayQueryRequest:new(o)
    o = o or {}
    setmetatable(o,
            self)
    self.__index = self
    o.amount = nil
    o.transferId = nil

    return o
end

NigWebPayQueryResponse = {}
function NigWebPayQueryResponse:new(o)
    o = o or {}
    setmetatable(o,
            self)
    self.__index = self
    o.amount = nil
    o.cardNumber = nil
    o.merchantReference = nil
    o.paymentReference = nil
    o.retrievalReferenceNumber = nil
    o.leadBankCbnCode = nil
    o.leadBankName = nil
    o.splitAccounts = nil
    o.transactionDate = nil
    o.responseCode = nil
    o.responseDescription = nil

    return o
end

NigQuicktellerTransRequest = {}
function NigQuicktellerTransRequest:new(o)
    o = o or {}
    setmetatable(o,
            self)
    self.__index = self
    o.toAccountNumber = nil
    o.cbnCode = nil
    o.amount = nil
    o.receiverLastName = nil
    o.receiverOtherName = nil

    return o
end

function NigQuicktellerTransRequest:setData(data)
    self.toAccountNumber = data[1]
    self.cbnCode = data[2]
    self.amount = data[3]
    self.receiverLastName = data[4]
    self.receiverOtherName = data[5]
end

NigQuicktellerTransRespone = {}
function NigQuicktellerTransRespone:new(o)
    o = o or {}
    setmetatable(o,
            self)
    self.__index = self
    o.result = nil
    o.requestRef = nil
    o.request = nil

    return o
end

function NigQuicktellerTransRespone:setData(data)
    self.result = data[1]
    self.requestRef = data[2]
    local r = data[3]
    if r ~= nil then
        local r_r = NigQuicktellerTransRequest:new(nil)
        r_r:setData(r)
        self.request = r_r
    end

end