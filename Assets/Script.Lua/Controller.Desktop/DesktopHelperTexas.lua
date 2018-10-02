-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
DesktopHelperTexas = DesktopHelperBase:new(nil)

---------------------------------------
function DesktopHelperTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o
end

---------------------------------------
function DesktopHelperTexas:GetDesktopRedisKeyPrefix(desktop_filter, is_private)
    local sb = {}
    table.insert(sb, "GrainDesktop_Texas")
    if (is_private) then
        table.insert(sb, "_private")
    end

    --local desktop_f = CS.Casinos.LuaHelper.ProtobufDeserializeDesktopFilterTexas(desktop_filter.FilterData)
    --if (desktop_f.seat_num == TexasDesktopSeatNum.Five)
    --then
    --    table.insert(sb, "_5")
    --else
    --    table.insert(sb, "_9")
    --end
    --
    ----if (desktop_f.desktoptype_texas == DesktopTypeTexas.MustBet)
    ----then
    ----    table.insert(sb, "_mustbet")
    ----else
    --    table.insert(sb, "_classic")
    ----end
    --
    --table.insert(sb, "_")
    --table.insert(sb, desktop_f.desktop_tableid)
    local l = table.concat(sb)
    return l
end

---------------------------------------
function DesktopHelperTexas:IsValid()
    return true
end

---------------------------------------
function DesktopHelperTexas:IsFull(desktop_filter, playerCount)
    local is_full = false

    --local desktop_f = CS.Casinos.LuaHelper.ProtobufDeserializeDesktopFilterTexas(desktop_filter.FilterData)
    --if (desktop_f.seat_num == TexasDesktopSeatNum.Five)
    --then
    --    is_full = playerCount > 5
    --elseif (desktop_f.seat_num == TexasDesktopSeatNum.Nine)
    --then
    --    is_full = playerCount > 9
    --end

    return is_full
end

---------------------------------------
--function DesktopHelperTexas:GetPlayerPlayState(filter, desktop_type, desktop_guid)
--    local player_playstate = CS.Casinos.PlayerPlayState()
--    local desktop_f = CS.Casinos.LuaHelper.ProtobufDeserializeDesktopFilterTexas(filter.FilterData)
--    player_playstate.CasinosModule = CasinosModule.__CastFrom(filter.FactoryName)
--    player_playstate.DesktopType = desktop_type
--    player_playstate.DesktopGuid = desktop_guid
--    player_playstate.UserData1 = desktop_f.desktoptype_texas.ToString()
--    player_playstate.UserData2 = CS.Casinos.LuaHelper.JsonSerializeDesktopFilter(filter)
--
--    return player_playstate
--end

---------------------------------------
function DesktopHelperTexas:GetDesktopInfoFormat(m_p, data_mgr, desktop_filter, lan_base)
    local desktopinfo_format = ""
    local desktop_f1 = m_p.unpack(desktop_filter.FilterData)
    local desktop_f = DesktopFilterTexas:new(nil)
    desktop_f:setData(desktop_f1)
    local tb_desktopinfo = data_mgr:GetData("DesktopInfoTexas", desktop_f.desktop_tableid)
    desktopinfo_format = desktopinfo_format .. lan_base:getValue("Bet") .. ":" .. UiChipShowHelper:getGoldShowStr(tb_desktopinfo.SmallBlind, lan_base) .. "/" ..
            UiChipShowHelper:getGoldShowStr(tb_desktopinfo.BigBlind, lan_base)
    local seat_num = 0
    if (desktop_f.seat_num == TexasDesktopSeatNum.Five) then
        seat_num = 5
    elseif (desktop_f.seat_num == TexasDesktopSeatNum.Nine) then
        seat_num = 9
    end
    local format = DesktopFormatInfo:new(nil, desktopinfo_format, tostring(seat_num))
    return format
end

---------------------------------------
DesktopHelperTexasFactory = DesktopBaseFactory:new(nil)

---------------------------------------
function DesktopHelperTexasFactory:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o
end

---------------------------------------
function DesktopHelperTexasFactory:GetName()
    return "Texas"
end

---------------------------------------
function DesktopHelperTexasFactory:CreateDesktopHelper()
    local l = DesktopHelperTexas:new(nil)
    return l
end