-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
MoneyType = {
    CNY = 0, -- 人民币
    USD = 1, -- 美元
    EUR = 2, -- 欧元
    NGN = 3, -- 尼日利亚，奈拉
    GHC = 4, -- 加纳，塞地
}

WalletResult = {
    Success = 0,
    False = 1,
    Timeout = 2,
}

---------------------------------------
WalletRechargeRequest = {}

function WalletRechargeRequest:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.MoneyType = MoneyType.CNY
    o.Amount = 0
    o.Channel = nil

    return o
end

function WalletRechargeRequest:getData4Pack()
    local t = {}
    table.insert(t, self.MoneyType)
    table.insert(t, self.Amount)
    table.insert(t, self.Channel)

    return t
end

function WalletRechargeRequest:setData(data)
    self.MoneyType = data[1]
    self.Amount = data[2]
    self.Channel = data[3]
end

---------------------------------------
WalletRechargeNotify = {}

function WalletRechargeNotify:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Result = WalletResult.Success
    o.Request = nil
    return o
end

function WalletRechargeNotify:setData(data)
    self.Result = data[1]
    local r = data[2]
    local w_r = WalletRechargeRequest:new(nil)
    w_r:setData(r)
    self.Request = w_r
end

---------------------------------------
WalletWithdrawRequest = {}

function WalletWithdrawRequest:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.MoneyType = MoneyType.CNY
    o.Amount = 0
    o.Channel = nil
    return o
end

function WalletWithdrawRequest:getData4Pack()
    local t = {}
    table.insert(t, self.MoneyType)
    table.insert(t, self.Amount)
    table.insert(t, self.Channel)
    return t
end

function WalletWithdrawRequest:setData(data)
    self.MoneyType = data[1]
    self.Amount = data[2]
    self.Channel = data[3]
end

---------------------------------------
WalletWithdrawNotify = {}

function WalletWithdrawNotify:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Result = WalletResult.Success
    o.Request = nil
    return o
end

function WalletWithdrawNotify:setData(data)
    self.Result = data[1]
    local r = data[2]
    local w_r = WalletWithdrawRequest:new(nil)
    w_r:setData(r)
    self.Request = w_r
end