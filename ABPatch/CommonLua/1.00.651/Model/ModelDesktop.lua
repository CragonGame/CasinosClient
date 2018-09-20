MethodInfoDesktopUser = {}

function MethodInfoDesktopUser:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.FactoryName = nil
    o.data = nil

    return o
end

function MethodInfoDesktopUser:setData(data)
    self.FactoryName = data[1]
    self.data = data[2]
end

function MethodInfoDesktopUser:getData4Pack()
    local t = {}
    table.insert(t, self.FactoryName)
    table.insert(t, self.data)
    return t
end

PrivateDesktopCreateInfo = {}

function PrivateDesktopCreateInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.FactoryName = nil
    o.Data = nil

    return o
end

function PrivateDesktopCreateInfo:getData4Pack()
    local p_d = {}
    table.insert(p_d, self.FactoryName)
    table.insert(p_d, self.Data)

    return p_d
end

DesktopEnterRequest = {}

function DesktopEnterRequest:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.desktop_guid = nil
    o.seat_index = 255
    o.is_ob = false
    o.desktop_filter = {}

    return o
end

function DesktopEnterRequest:getData4Pack()
    local p_d = {}
    table.insert(p_d, self.desktop_guid)
    table.insert(p_d, self.seat_index)
    table.insert(p_d, self.is_ob)
    table.insert(p_d, self.desktop_filter)

    return p_d
end

DesktopNotifyPlayerCurrentGiftChange = {}

function DesktopNotifyPlayerCurrentGiftChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.item_data = {}

    return o
end

DesktopSnapshotData = {}

function DesktopSnapshotData:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.FactoryName = nil
    o.DesktopGuid = nil
    o.DesktopData = nil

    return o
end

function DesktopSnapshotData:setData(data)
    if data ~= nil then
        self.FactoryName = data[1]
        self.DesktopGuid = data[2]
        self.DesktopData = data[3]
    end
end

DesktopSnapshotDataNotify = {}

function DesktopSnapshotDataNotify:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.DesktopSnapshotData = nil
    o.IsInit = false

    return o
end

function DesktopSnapshotDataNotify:setData(data)
    self.DesktopSnapshotData = data[1]
    self.IsInit = data[2]
end

DesktopFilter = {}

function DesktopFilter:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.FactoryName = nil
    o.FilterData = nil
    o.IncludeFull = false

    return o
end

function DesktopFilter:setData(data)
    self.FactoryName = data[1]
    self.FilterData = data[2]
    self.IncludeFull = data[3]
end

function DesktopFilter:getData4Pack()
    local p_d = {}
    table.insert(p_d, self.FactoryName)
    table.insert(p_d, self.FilterData)
    table.insert(p_d, self.IncludeFull)

    return p_d
end

DesktopInfo = {}

function DesktopInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.FactoryName = nil
    o.DesktopGuid = nil
    o.DesktopData = nil

    return o
end

DesktopSitdownRequest = {}

function DesktopSitdownRequest:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.seat_index = 0
    o.user_data1 = nil
    o.user_data2 = nil

    return o
end

function DesktopSitdownRequest:getData4Pack()
    local t = {}
    table.insert(t,self.player_guid)
    table.insert(t,self.seat_index)
    table.insert(t,self.user_data1)
    table.insert(t,self.user_data2)
    return t
end

PlayerSitdownData = {}

function PlayerSitdownData:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.seat_index = nil
    o.user_data1 = nil
    o.user_data2 = nil

    return o
end

function PlayerSitdownData:setData(data)
    self.player_guid = data[1]
    self.seat_index = data[2]
    self.user_data1 = data[3]
    self.user_data2 = data[4]
end

PlayerReturnData = {}

function PlayerReturnData:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.user_data1 = nil
    o.user_data2 = nil

    return o
end

function PlayerReturnData:setData(data)
    self.player_guid = data[1]
    self.user_data1 = data[2]
    self.user_data2 = data[3]
end

DesktopPlayerInfo = {}

function DesktopPlayerInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.seat_index = 255
    o.player_guid = nil
    o.nick_name = nil
    o.account_id = nil
    o.chip = 0
    o.icon = nil
    o.is_bot = false

    return o
end

DesktopPushStackResult = {}

function DesktopPushStackResult:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.result = nil
    o.stack_left = 0
    o.factory_name = nil

    return o
end

InvitePlayerEnterDesktop = {}

function InvitePlayerEnterDesktop:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.player_nickname = nil
    o.player_accid = nil
    o.desktop_guid = nil
    o.desktop_filter = nil
    o.player_num = 0

    return o
end

function InvitePlayerEnterDesktop:setData(data)
    self.player_guid = data[1]
    self.player_nickname = data[2]
    self.player_accid = data[3]
    self.desktop_guid = data[4]
    local filter = DesktopFilter:new(nil)
    filter:setData(data[5])
    self.desktop_filter = filter
    self.player_num = data[6]
end

function InvitePlayerEnterDesktop:getData4Pack()
    local t = {}
    table.insert(t,self.player_guid)
    table.insert(t,self.player_nickname)
    table.insert(t,self.player_accid)
    table.insert(t,self.desktop_guid)
    table.insert(t,self.desktop_filter)
    table.insert(t,self.player_num)

    return t
end

PlayerDataDesktop = {}

function PlayerDataDesktop:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.PlayerGuid = nil
    o.NickName = nil
    o.AccountId = nil
    o.IconName = nil
    o.VIPLevel = nil
    o.Gender = true
    o.PlayerId = 0
    o.PlayerData = nil

    return o
end

function PlayerDataDesktop:setData(data)
    self.PlayerGuid = data[1]
    self.NickName = data[2]
    self.AccountId = data[3]
    self.IconName = data[4]
    self.VIPLevel = data[5]
    self.Gender = data[6]
    self.PlayerId = data[7]
    self.PlayerData = data[8]
end