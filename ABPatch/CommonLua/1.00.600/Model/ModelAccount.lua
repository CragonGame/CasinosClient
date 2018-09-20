GetPhoneCodeReson = {
    Register = 0,
    ResetPwd = 1,
}


AttachWechatInfo ={}
function AttachWechatInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.union_id = nil
    o.open_id = nil
    o.nick_name = nil
    o.headimgurl = nil

    return o
end

function AttachWechatInfo:setData(data)
    self.union_id = data[1]
    print("AttachWechatInfo  union_id " .. data[1])
    self.open_id = data[2]
    print("AttachWechatInfo  open_id " .. data[2])
    self.nick_name = data[3]
    print("AttachWechatInfo  nick_name " .. data[3])
    self.headimgurl = data[4]
    print("AttachWechatInfo  headimgurl " .. data[4])
end


LoginAccountInfos ={}
function LoginAccountInfos:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.LastLoginType = 0
    o.TLoginAccountInfo = {}

    return o
end

function LoginAccountInfos:setData(data)
    self.LastLoginType = data["LastLoginType"]
    local t_a = data["TLoginAccountInfo"]
    if t_a ~= nil then
        self.TLoginAccountInfo = {}
        for i, v in pairs(t_a) do
            local a_info = LoginAccountInfo:new(nil)
            a_info:setData(v)
            self.TLoginAccountInfo[i] = a_info
        end
    end
end

LoginAccountInfo ={}
function LoginAccountInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.LoginType = 1
    o.AccName = nil
    o.Phone = nil
    o.Pwd = nil

    return o
end

function LoginAccountInfo:setData(data)
    self.LoginType = data["LoginType"]
    self.AccName = data["AccName"]
    self.Phone = data["Phone"]
    self.Pwd = data["Pwd"]
end

ClientLoginAppRequest = {}

function ClientLoginAppRequest:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.acc_id = nil
    o.acc_name = nil
    o.token = nil
    o.nick_name = nil
    o.channel_id = nil
    o.platform = nil
    o.lan = nil

    return o
end

function ClientLoginAppRequest:getData4Pack()
    local t = {}
    table.insert(t, self.acc_id)
    table.insert(t, self.acc_name)
    table.insert(t, self.token)
    table.insert(t, self.nick_name)
    table.insert(t, self.channel_id)
    table.insert(t, self.platform)
    table.insert(t, self.lan)
    return t
end

ClientLoginAppNotify = {}

function ClientLoginAppNotify:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.result = 0

    return o
end

function ClientLoginAppNotify:setData(data)
    self.result = data[1]
end

ClientEnterWorldRequest = {}

function ClientEnterWorldRequest:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.acc_id = nil
    o.acc_name = nil
    o.token = nil
    o.invite_id = nil

    return o
end

function ClientEnterWorldRequest:getData4Pack()
    local t = {}
    table.insert(t, self.acc_id)
    table.insert(t, self.acc_name)
    table.insert(t, self.token)
    table.insert(t, self.invite_id)

    return t
end

ClientEnterWorldNotify = {}

function ClientEnterWorldNotify:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.result = 0
    o.player_guid = nil
    o.player_data = nil
    o.player_play_state = nil
    o.attach_wechat = nil

    return o
end

function ClientEnterWorldNotify:setData(data)
    self.result = data[1]
    self.player_guid = data[2]
    self.player_data = data[3]
    local player_state = data[4]
    if (player_state ~= nil)
    then
        local state = PlayerPlayState1:new(nil)
        state:setData(player_state)
        self.player_play_state = state
    end
    local we_chat = data[5]
    if (we_chat ~= nil)
    then
        local we_chat1 = AttachWechatInfo:new(nil)
        we_chat1:setData(we_chat)
        self.attach_wechat = we_chat1
    end
end

PlayerPlayState1 = {}

function PlayerPlayState1:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.CasinosModule = 0
    o.DesktopType = 0
    o.DesktopGuid = nil
    o.UserData1 = nil
    o.UserData2 = nil

    return o
end

function PlayerPlayState1:setData(data)
    self.CasinosModule = data[1]
    self.DesktopType = data[2]
    self.DesktopGuid = data[3]
    self.UserData1 = data[4]
    self.UserData2 = data[5]
end