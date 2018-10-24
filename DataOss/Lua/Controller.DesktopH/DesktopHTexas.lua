-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
DesktopHTexas = DesktopHBase:new(nil)

---------------------------------------
function DesktopHTexas:new(o, controller_desktoph, factory_name)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.ControllerDesktopH = controller_desktoph
    self.FactoryName = factory_name
    self.MapTbGoldPercent = {}
    self.MaxTbGoldPercent = {}
    self.ListOperate = {}
    self.ListOperateId = {}

    self:_setGoldPercent()
    local tb_datamgr = TbDataMgr:new(nil)
    local map_tbdesktophoperate = tb_datamgr:GetMapData("DesktopHBetOperateTexas")
    for k, v in pairs(map_tbdesktophoperate) do
        self.ListOperate[v] = v
    end

    for k, v in pairs(self.ListOperate) do
        self.ListOperateId[v.Id] = v.Id
    end

    return o
end

---------------------------------------
function DesktopHTexas:onDestroy()
end

---------------------------------------
function DesktopHTexas:onHandleEvent(ev)
end

---------------------------------------
function DesktopHTexas:InitDesktopH(desktoph_data)
end

---------------------------------------
function DesktopHTexas:refreshDesktopH(desktoph_data)
end

---------------------------------------
function DesktopHTexas:SeatPlayerChanged(sitdown_data)
end

---------------------------------------
function DesktopHTexas:BankPlayerChanged()
end

---------------------------------------
function DesktopHTexas:DesktopHChat(msg)
    local m = ChatMsg:new(nil)
    m:setData(msg)
    if (CS.System.String.IsNullOrEmpty(m.sender_guid))
    then
        return
    end

    self.ControllerDesktopH:addDesktopMsg(m.sender_guid, m.sender_nickname, m.sender_viplevel, m.msg)
end

---------------------------------------
function DesktopHTexas:getMaxBetpotIndex()
    local tb_datamgr = TbDataMgr:new(nil)
    local map_betpotindex = tb_datamgr:GetMapData("DesktopHBetPotTexas")
    local l = LuaHelper:GetTableCount(map_betpotindex)
    --local sort_result = map_betpotindex.OrderByDescending(x => x.Key).ToList()
    return map_betpotindex[l - 1].Id
end

---------------------------------------
function DesktopHTexas:getBetOperateId()
    return self.ListOperateId
end

---------------------------------------
function DesktopHTexas:getMaxGoldPecent()
    return self.MaxTbGoldPercent.GoldPercent
end

---------------------------------------
function DesktopHTexas:getMaxCannotBetPecent()
    local bet_percent = TbDataHelper.Instance:GetCommonValue("DesktopHMaxBetPercentTexas")
    return bet_percent
end

---------------------------------------
function DesktopHTexas:getOperateGold(operate_id)
    local tb_datamgr = TbDataMgr:new(nil)
    local bet_operate = tb_datamgr:GetData("DesktopHBetOperateTexas", operate_id)
    return bet_operate.OperateGolds
end

---------------------------------------
function DesktopHTexas:getWinOrLoosePercent(card_type)
    local percent = 0
    local type = CS.Casinos.LuaHelper.ProtobufDeserializeHandRankTypeTexasH(card_type)
    for k, v in pairs(self.MapTbGoldPercent) do
        if (v.HandRankTypeTexasH == type)
        then
            percent = v.GoldPercent
            break
        end
    end

    return percent
end

---------------------------------------
--function DesktopHTexas:getGameReusltTips(card_type, self_betgolds)
--end

---------------------------------------
function DesktopHTexas:_setGoldPercent()
    local tb_datamgr = TbDataMgr:new(nil)
    self.MapTbGoldPercent = tb_datamgr:GetMapData("CfigTexasDesktopHGoldPercent")

    local max_percent = 0
    for k, v in pairs(self.MapTbGoldPercent) do
        if (v.GoldPercent > max_percent)
        then
            max_percent = v.GoldPercent
            self.MaxTbGoldPercent = v
        end
    end
end

---------------------------------------
DesktopHTexasFactory = {}

---------------------------------------
function DesktopHTexasFactory:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self

    return o
end

---------------------------------------
function DesktopHTexasFactory:GetName()
    return "Texas"
end

---------------------------------------
function DesktopHTexasFactory:CreateDesktop(controller_desktoph, factory_name)
    local t = DesktopHTexas:new(nil, controller_desktoph, factory_name)
    return t
end        