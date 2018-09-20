PlayerInfoCommon = {} -- 玩家信息，最简公共
function PlayerInfoCommon:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.PlayerGuid = nil
    o.NickName = nil
    o.AccountId = nil
    o.IconName = nil -- 头像
    o.VIPLevel = 0
    o.PlayerId = 0

    return o
end

function PlayerInfoCommon:setData(data)
    self.PlayerGuid = data[1]
    self.NickName = data[2]
    self.AccountId = data[3]
    self.IconName = data[4]
    self.VIPLevel = data[5]
    self.PlayerId = data[6]
end

PlayerInfoMore = {} -- 玩家详细信息
function PlayerInfoMore:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Level = 0
    o.Exp = 0
    o.PlayerId = 0
    o.IndividualSignature = nil
    o.IPAddress = nil
    o.Gold = 0
    o.Diamond = 0
    o.VipLevel = 0
    o.ListItemData = nil
    o.OnlineState = 0
    o.LogoutDt = nil

    return o
end

function PlayerInfoMore:setData(data)
    self.Level = data[1]
    self.Exp = data[2]
    self.PlayerId = data[3]
    self.IndividualSignature = data[4]
    self.IPAddress = data[5]
    self.Gold = data[6]
    self.Diamond = data[7]
    self.VipLevel = data[8]
    local l_i = data[9]
    if l_i ~= nil then
        local t_i = {}
        for i, v in pairs(l_i) do
            local i_d = ItemData1:new(nil)
            i_d:setData(v)
            table.insert(t_i,i_d)
        end
        self.ListItemData = t_i
    end
    self.OnlineState = data[10]
    self.LogoutDt = data[11]
end

PlayerPlayState = {} -- 玩家动态信息
function PlayerPlayState:new(o)
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

function PlayerPlayState:setData(data)
    self.CasinosModule = data[1]
    self.DesktopType = data[2]
    self.DesktopGuid = data[3]
    self.UserData1 = data[4]
    self.UserData2 = data[5]
end

function PlayerPlayState:getData4Pack()
    local p_d = {}
    table.insert(p_d, self.CasinosModule)
    table.insert(p_d, self.DesktopType)
    if self.DesktopGuid ~= nil then
        table.insert(p_d, self.DesktopGuid)
    end
    if self.UserData1 ~= nil then
        table.insert(p_d, self.UserData1)
    end
    if self.UserData2 ~= nil then
        table.insert(p_d, self.UserData2)
    end

    return p_d
end

PlayerModuleData = {} -- 玩家玩法相关数据
function PlayerModuleData:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.FactoryName = nil
    o.Data = nil

    return o
end

function PlayerModuleData:setData(data)
    self.FactoryName = data[1]
    self.Data = data[2]
end

PlayerInfo = {} -- 玩家信息，通用
function PlayerInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.PlayerInfoCommon = nil
    o.PlayerInfoMore = nil
    o.PlayerPlayState = nil
    o.PlayerModuleData = nil

    return o
end

function PlayerInfo:setData(data)
    local t_c =data[1]
    if t_c ~= nil then
        local t_c_ex = PlayerInfoCommon:new(nil)
        t_c_ex:setData(t_c)
        self.PlayerInfoCommon = t_c_ex
    end
    local t_m = data[2]
    if t_m ~= nil then
        local t_m_ex = PlayerInfoMore:new(nil)
        t_m_ex:setData(t_m)
        self.PlayerInfoMore = t_m_ex
    end
    local t_s = data[3]
    if t_s ~= nil then
        local t_s_ex = PlayerPlayState:new(nil)
        t_s_ex:setData(t_s)
        self.PlayerPlayState = t_s_ex
    end
    local t_d = data[4]
    if t_d ~= nil then
        local t_d_ex = PlayerModuleData:new(nil)
        t_d_ex:setData(t_d)
        self.PlayerModuleData = t_d_ex
    end
end

function PlayerInfo:getData4Pack()
    local p_d = {}
    if self.PlayerInfoCommon ~= nil then
        table.insert(p_d, self.PlayerInfoCommon:getData4Pack())
    end
    if self.PlayerInfoMore ~= nil then
        table.insert(p_d, self.PlayerInfoMore:getData4Pack())
    end
    if self.PlayerPlayState ~= nil then
        table.insert(p_d, self.PlayerPlayState:getData4Pack())
    end
    if self.PlayerModuleData ~= nil then
        table.insert(p_d, self.PlayerModuleData:getData4Pack())
    end

    return p_d
end

PlayerInDesktop = {} -- 玩家所在桌，桌子Filter与Guid
function PlayerInDesktop:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.DesktopFilter = nil
    o.DesktopGuid = nil

    return o
end

function PlayerInDesktop:setData(data)
    local f = data[1]
    if f ~= nil then
        local t_f = DesktopFilter:new(nil)
        t_f:setData(f)
        self.DesktopFilter = t_f
    end
    self.DesktopGuid = data[2]
end