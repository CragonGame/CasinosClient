-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ControllerDesk = ControllerBase:new(nil)

---------------------------------------
function ControllerDesk:new(o, controller_mgr, controller_data, guid)
    o = o or {}
    setmetatable(o, self)
    self.__index = self

    o.ControllerData = controller_data
    o.ControllerMgr = controller_mgr
    o.Guid = guid

    return o
end

---------------------------------------
function ControllerDesk:onCreate()
    self.ControllerMgr.ViewMgr:bindEvListener("EvUiRequestLockSystemChat", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvEntityPlayerEnterDesktopH", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvUiRequestChangeDesk", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvEntityGetDesktopData", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvUiClickFlod", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvUiClickCheck", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvUiClickCall", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvUiClickRaise", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvUiClickSeat", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvUiClickOB", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvUiClickWaitWhile", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvUiClickPlayerReturn", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvUiClickAutoAction", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvUiClickCancelAutoAction", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvUiCreateExchangeChip", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvUiRequestLockPlayerChat", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvUiRequestLockAllDesktopPlayer", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvUiRequestLockAllSpectator", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvUiInviteFriendPlayTogether", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvUiSendMsg", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvUiSetUnSendDesktopMsg", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvUiDesktopClickLockChat", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvUiClickShowCard", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvEntityMTTPlayerRebuy", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvEntityMTTPlayerAddon", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvUiMTTCreateRebuyOrAddOn", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvEntityMatchGameOver", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvEntityDesktopPlayerLeaveChair", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvEntitySetMatchDetailedInfo", self)
    self.ControllerMgr.ViewMgr:bindEvListener("EvUpdatePlayerScore", self)

    local rpc = self.ControllerMgr.RPC
    local m_c = CommonMethodType
    rpc:RegRpcMethod1(m_c.DesktopUserNotify, function(info_user)
        self:s2cPlayerDesktopUser(info_user)
    end)
    rpc:RegRpcMethod1(m_c.DesktopSnapshotNotify, function(snapshot_notify)
        self:s2cDesktopSnapshotNotify(snapshot_notify)
    end)
    rpc:RegRpcMethod1(m_c.DesktopPlayerEnterNotify, function(player_data)
        self:s2cPlayerDesktopPlayerEnterNotify(player_data)
    end)
    rpc:RegRpcMethod1(m_c.DesktopPlayerLeaveNotify, function(player_guid)
        self:s2cPlayerDesktopPlayerLeaveNotify(player_guid)
    end)
    rpc:RegRpcMethod1(m_c.DesktopPlayerSitdownNotify, function(sitdown_data)
        self:s2cPlayerDesktopPlayerSitdown(sitdown_data)
    end)
    rpc:RegRpcMethod1(m_c.DesktopPlayerObNotify, function(player_guid)
        self:s2cPlayerDesktopPlayerOb(player_guid)
    end)
    rpc:RegRpcMethod1(m_c.DesktopPlayerWaitWhileNotify, function(player_guid)
        self:s2cPlayerDesktopPlayerWaitWhile(player_guid)
    end)
    rpc:RegRpcMethod1(m_c.DesktopPlayerReturnNotify, function(return_data)
        self:s2cPlayerDesktopPlayerReturn(return_data)
    end)
    rpc:RegRpcMethod1(m_c.DesktopPlayerGiftChangeNotify, function(data)
        self:s2cDesktopPlayerGiftChangeNotify(data)
    end)
    rpc:RegRpcMethod1(m_c.DesktopBuyAndSendItemNotify, function(data)
        self:s2cDesktopBuyAndSendItemNotify(data)
    end)
    rpc:RegRpcMethod1(m_c.DesktopChatNotify, function(msg)
        self:s2cPlayerDesktopChat(msg)
    end)
    rpc:RegRpcMethod0(m_c.PlayerLeaveDesktopNotify, function()
        self:OnPlayerLeaveDesktopNotify()
    end)
    rpc:RegRpcMethod1(m_c.PlayerInvitePlayerEnterDesktopRequestResult, function(r)
        self:OnPlayerInvitePlayerEnterDesktopRequestResult(r)
    end)

    rpc:RegRpcMethod1(m_c.MatchTexasRequestRebuyResult, function(r)
        self:OnMatchTexasRequestRebuyResult(r)
    end)
    rpc:RegRpcMethod1(m_c.MatchTexasRequestAddonResult, function(r)
        self:OnMatchTexasRequestAddonResult(r)
    end)
    rpc:RegRpcMethod1(m_c.MatchTexasPlayerFinishedNotify, function(r)
        self:OnMatchTexasGameOverNotify(r)
    end)

    self.ControllerPlayer = self.ControllerMgr:GetController("Player")
    self.ListDesktopChat = {}
    self.LockSysChat = false
    self.MapDesktopBaseFac = {}
    self:regDesktopBaseFactory(DesktopTexasFactory:new(nil))
    self.MapDesktopHelper = {}
    local t_fac = DesktopHelperTexasFactory:new(nil)
    self.MapDesktopHelper[t_fac:GetName()] = t_fac:CreateDesktopHelper()
end

---------------------------------------
function ControllerDesk:onDestroy()
    self.ControllerMgr.ViewMgr:unbindEvListener(self)
end

---------------------------------------
function ControllerDesk:onUpdate(tm)
    if (self.DesktopBase ~= nil)
    then
        self.DesktopBase:onUpdate(tm)
    end
end

---------------------------------------
function ControllerDesk:onHandleEv(ev)
    if (ev.EventName == "EvUiRequestLockSystemChat")
    then
        self.LockSysChat = ev.requestLock
    elseif (ev.EventName == "EvEntityPlayerEnterDesktopH")
    then
        self:clearDesktop(false)
    elseif (ev.EventName == "EvUiRequestChangeDesk")
    then
        local rpc = self.ControllerMgr.RPC
        rpc:RPC0(CommonMethodType.DesktopPlayerChangeDeskRequest)
    elseif (ev.EventName == "EvEntityGetDesktopData")
    then
        if (self.DesktopBase == nil)
        then
            return
        end
        local rpc = self.ControllerMgr.RPC
        rpc:RPC0(CommonMethodType.DesktopSnapshotRequest)
    end

    if (self.DesktopBase ~= nil)
    then
        self.DesktopBase:onHandleEv(ev)
    end
end

---------------------------------------
function ControllerDesk:s2cDesktopSnapshotNotify(snapshot_notify)
    ViewHelper:UiEndWaiting()
    local desktop_data1 = snapshot_notify[1]
    local desktop_data = DesktopSnapshotData:new(nil)
    desktop_data:setData(desktop_data1)
    if (desktop_data1 == nil or desktop_data.DesktopData == nil)
    then
        local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityPlayerEnterDesktopFailed")
        if (ev == nil)
        then
            ev = EvEntityPlayerEnterDesktopFailed:new(nil)
        end
        self.ControllerMgr.ViewMgr:sendEv(ev)
        ViewHelper:UiShowInfoFailed(self.ControllerMgr.LanMgr:getLanValue("EnterTableFailed"))
    else
        local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityPlayerEnterDesktop")
        if (ev == nil)
        then
            ev = EvEntityPlayerEnterDesktop:new(nil)
        end
        self.ControllerMgr.ViewMgr:sendEv(ev)
        local is_init = snapshot_notify[2]
        if (is_init)
        then
            self:createDesktop(desktop_data.FactoryName)
        else
            if (self.DesktopBase == nil)
            then
                is_init = true
                self:createDesktop(desktop_data.FactoryName)
            end
        end

        self.DesktopBase:SetDesktopSnapshotData(desktop_data, is_init)
    end
end

---------------------------------------
function ControllerDesk:s2cPlayerDesktopPlayerEnterNotify(player_data)
    if (self.DesktopBase ~= nil)
    then
        self.DesktopBase:PlayerEnter(player_data)
    end
end

---------------------------------------
function ControllerDesk:s2cPlayerDesktopPlayerLeaveNotify(player_guid)
    if (self.DesktopBase ~= nil)
    then
        self.DesktopBase:PlayerLeave(player_guid)
    end
end

---------------------------------------
function ControllerDesk:s2cPlayerDesktopPlayerSitdown(sitdown_data)
    if (self.DesktopBase ~= nil)
    then
        self.DesktopBase:PlayerSitdown(sitdown_data)
    end
end

---------------------------------------
function ControllerDesk:s2cPlayerDesktopPlayerOb(player_guid)
    if (self.DesktopBase ~= nil)
    then
        self.DesktopBase:PlayerOb(player_guid)
    end
end

---------------------------------------
function ControllerDesk:s2cPlayerDesktopPlayerWaitWhile(player_guid)
    if (self.DesktopBase ~= nil)
    then
        self.DesktopBase:PlayerWaitWhile(player_guid)
    end
end

---------------------------------------
function ControllerDesk:s2cPlayerDesktopPlayerReturn(return_data)
    if (self.DesktopBase ~= nil)
    then
        self.DesktopBase:PlayerReturn(return_data)
    end
end

---------------------------------------
function ControllerDesk:s2cDesktopPlayerGiftChangeNotify(data)
    if (self.DesktopBase ~= nil)
    then
        self.DesktopBase:DesktopPlayerGiftChangeNotify(data)
    end
end

---------------------------------------
function ControllerDesk:s2cDesktopBuyAndSendItemNotify(data)
    if (self.DesktopBase ~= nil)
    then
        self.DesktopBase:DesktopBuyAndSendItemNotify(data)
    end
end

---------------------------------------
function ControllerDesk:s2cPlayerDesktopChat(msg1)
    if (self.DesktopBase ~= nil)
    then
        local msg = ChatMsg:new(nil)
        msg:setData(msg1)
        self.DesktopBase:DesktopChat(msg)
    end
end

---------------------------------------
function ControllerDesk:OnPlayerInvitePlayerEnterDesktopRequestResult(r)
    if (r == ProtocolResult.Success)
    then
        ViewHelper:UiShowInfoSuccess(self.ControllerMgr.LanMgr:getLanValue("InviteFriendToTable"))
    end
end

---------------------------------------
function ControllerDesk:s2cPlayerDesktopUser(info_user)
    if (self.DesktopBase ~= nil)
    then
        self.DesktopBase:DesktopUser(info_user)
    end
end

---------------------------------------
function ControllerDesk:OnMatchTexasRequestRebuyResult(r)
    if (self.DesktopBase == nil)
    then
        return
    end

    if (CS.System.String.IsNullOrEmpty(self.DesktopBase.DesktopGuid))
    then
        return
    end

    local rebuy = BMatchTexasRebuyResponse:new(nil)
    rebuy:setData(r)
    --if rebuy.Result == ProtocolResult.Success then
    --    local view_mgr = self.ControllerMgr.ViewMgr
    --    local ev = view_mgr:getEv("EvEntityMTTPlayerRebuy")
    --    if (ev == nil)
    --    then
    --        ev = EvEntityMTTPlayerRebuy:new(nil)
    --    end
    --    ev.RebuyInfo = rebuy
    --    view_mgr:sendEv(ev)
    --end
end

---------------------------------------
function ControllerDesk:OnMatchTexasRequestAddonResult(r)
    if (self.DesktopBase == nil)
    then
        return
    end

    if (CS.System.String.IsNullOrEmpty(self.DesktopBase.DesktopGuid))
    then
        return
    end

    local addon = BMatchTexasAddonResponse:new(nil)
    addon:setData(r)

    --if addon.Result == ProtocolResult.Success then
    --    local view_mgr = self.ControllerMgr.ViewMgr
    --    local ev = view_mgr:getEv("EvEntityMTTPlayerAddon")
    --    if (ev == nil)
    --    then
    --        ev = EvEntityMTTPlayerAddon:new(nil)
    --    end
    --    ev.AddonInfo = rebuy
    --    view_mgr:sendEv(ev)
    --end
end

---------------------------------------
function ControllerDesk:OnMatchTexasGameOverNotify(r)
    if (self.DesktopBase == nil)
    then
        return
    end

    if (CS.System.String.IsNullOrEmpty(self.DesktopBase.DesktopGuid))
    then
        return
    end

    local game_over = BMatchTexasPlayerFinishedNotify:new(nil)
    game_over:setData(r)
    if game_over.Result == ProtocolResult.Success then
        local view_mgr = self.ControllerMgr.ViewMgr
        local ev = view_mgr:getEv("EvEntityMatchGameOver")
        if (ev == nil)
        then
            ev = EvEntityMatchGameOver:new(nil)
        end
        ev.game_over = game_over
        view_mgr:sendEv(ev)
    end
end

---------------------------------------
function ControllerDesk:requestInvitePlayerEnterDesktop(friend_guid, desktop_guid, desktop_filter, player_num)
    local invite = InvitePlayerEnterDesktop:new(nil)
    invite.player_guid = friend_guid
    invite.player_nickname = ""
    invite.player_accid = ""
    invite.desktop_guid = desktop_guid
    invite.desktop_filter = desktop_filter
    invite.player_num = player_num

    self.ControllerMgr.RPC:RPC1(CommonMethodType.PlayerInvitePlayerEnterDesktopRequest, invite:getData4Pack())
end

---------------------------------------
function ControllerDesk:OnPlayerLeaveDesktopNotify()
    ViewHelper:UiEndWaiting()
    self:clearDesktop(true)
    self.ControllerPlayer:requestGetOnlinePlayerNum()
end

---------------------------------------
function ControllerDesk:clearDesktop(need_createmainui)
    if (self.DesktopBase ~= nil)
    then
        self.DesktopBase:onDestroy(need_createmainui)
        self.DesktopBase = nil
    end

    self.ListDesktopChat = {}
end

---------------------------------------
function ControllerDesk:addDesktopMsg(sender_etguid, sender_name, sender_viplevel, chat_content)
    if ((sender_etguid == nil or sender_etguid == "") and self.LockSysChat)
    then
        return
    end
    local chat_info = ChatTextInfo:new(nil)
    chat_info.chat_content = chat_content
    chat_info.sender_etguid = sender_etguid
    chat_info.sender_name = sender_name
    chat_info.sender_viplevel = sender_viplevel
    table.insert(self.ListDesktopChat, chat_info)

    if (#self.ListDesktopChat > 50)
    then
        table.remove(self.ListDesktopChat, 1)
    end

    local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityRecvChatFromDesktop")
    if (ev == nil)
    then
        ev = EvEntityRecvChatFromDesktop:new(nil)
    end
    ev.chat_info = chat_info
    self.ControllerMgr.ViewMgr:sendEv(ev)
end

---------------------------------------
function ControllerDesk:createDesktop(desktop_factory_name)
    self.ControllerPlayer:destroyMainUi()
    if (self.DesktopBase ~= nil)
    then
        self:clearDesktop(false)
    end

    local desktop_factory = self:GetDesktopBaseFactory(desktop_factory_name)
    self.DesktopBase = desktop_factory:CreateDesktop(self.ControllerMgr)
end

---------------------------------------
function ControllerDesk:RequestSendMsg(chat_msg)
    local rpc = self.ControllerMgr.RPC
    rpc:RPC1(CommonMethodType.DesktopChatRequest, chat_msg)
end

---------------------------------------
function ControllerDesk:RequestPlayerWaitWhile()
    local rpc = self.ControllerMgr.RPC
    rpc:RPC0(CommonMethodType.DesktopPlayerWaitWhileRequest)
end

---------------------------------------
function ControllerDesk:RequestPlayerOb()
    local rpc = self.ControllerMgr.RPC
    rpc:RPC0(CommonMethodType.DesktopPlayerObRequest)
end

---------------------------------------
function ControllerDesk:RequestPlayerReturn(data)
    local rpc = self.ControllerMgr.RPC
    local m = {}
    m["Stack"] = tostring(data)
    rpc:RPC1(CommonMethodType.DesktopPlayerReturnRequest, m)
end

---------------------------------------
function ControllerDesk:RequestPlayerSitdown(sitdown_info)
    local rpc = self.ControllerMgr.RPC
    rpc:RPC1(CommonMethodType.DesktopPlayerSitdownRequest, sitdown_info:getData4Pack())
end

---------------------------------------
function ControllerDesk:UserRequest(fac_name, method_info)
    local rpc = self.ControllerMgr.RPC
    local user = MethodInfoDesktopUser:new(nil)
    user.FactoryName = fac_name
    user.data = self.ControllerMgr:packData(method_info)
    local d = user:getData4Pack()
    rpc:RPC1(CommonMethodType.DesktopUserRequest, d)
end

---------------------------------------
function ControllerDesk:MatchTexasRequestRebuy(match_guid)
    local rpc = self.ControllerMgr.RPC
    rpc:RPC1(CommonMethodType.MatchTexasRequestRebuy, match_guid)
end

---------------------------------------
function ControllerDesk:MatchTexasRequestAddon(match_guid)
    local rpc = self.ControllerMgr.RPC
    rpc:RPC1(CommonMethodType.MatchTexasRequestAddon, match_guid)
end

---------------------------------------
function ControllerDesk:MatchTexasRequestGiveUpRebuyOrAddon(match_guid)
    local rpc = self.ControllerMgr.RPC
    rpc:RPC1(CommonMethodType.MatchTexasRequestGiveUpRebuyOrAddon, match_guid)
end

---------------------------------------
function ControllerDesk:regDesktopBaseFactory(desktop_fac)
    self.MapDesktopBaseFac[desktop_fac:GetName()] = desktop_fac
end

---------------------------------------
function ControllerDesk:GetDesktopBaseFactory(fac_name)
    return self.MapDesktopBaseFac[fac_name]
end

---------------------------------------
function ControllerDesk:GetDesktopHelperBase(fac_name)
    return self.MapDesktopHelper[fac_name]
end

---------------------------------------
ControllerDeskFactory = ControllerFactory:new()

---------------------------------------
function ControllerDeskFactory:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.ControllerName = "Desktop"
    return o
end

---------------------------------------
function ControllerDeskFactory:createController(controller_mgr, controller_data, guid)
    local controller = ControllerDesk:new(nil, controller_mgr, controller_data, guid)
    return controller
end