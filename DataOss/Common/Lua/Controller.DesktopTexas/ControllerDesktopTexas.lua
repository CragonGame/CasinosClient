-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ControllerDesktopTexas = class(ControllerBase)

---------------------------------------
function ControllerDesktopTexas:ctor(controller_data, controller_name)
    self.TimerUpdate = nil
end

---------------------------------------
function ControllerDesktopTexas:OnCreate()
    self.ControllerMgr.ViewMgr:BindEvListener("EvUiRequestLockSystemChat", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvUiRequestChangeDesk", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvUiClickFlod", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvUiClickCheck", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvUiClickCall", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvUiClickRaise", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvUiClickSeat", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvUiClickOB", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvUiClickWaitWhile", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvUiClickPlayerReturn", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvUiClickAutoAction", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvUiClickCancelAutoAction", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvUiCreateExchangeChip", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvUiRequestLockPlayerChat", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvUiRequestLockAllDesktopPlayer", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvUiRequestLockAllSpectator", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvUiInviteFriendPlayTogether", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvUiSendMsg", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvUiSetUnSendDesktopMsg", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvUiDesktopClickLockChat", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvUiClickShowCard", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvUiMTTCreateRebuyOrAddOn", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvEntityPlayerEnterDesktopH", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvEntityGetDesktopData", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvEntityMTTPlayerRebuy", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvEntityMTTPlayerAddon", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvEntityMatchGameOver", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvEntityDesktopPlayerLeaveChair", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvEntitySetMatchDetailedInfo", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvUpdatePlayerScore", self)

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
function ControllerDesktopTexas:OnDestroy()
    if (self.TimerUpdate ~= nil) then
        self.TimerUpdate:Close()
        self.TimerUpdate = nil
    end
    self.ControllerMgr.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ControllerDesktopTexas:OnHandleEv(ev)
    if (ev.EventName == "EvUiRequestLockSystemChat") then
        self.LockSysChat = ev.requestLock
    elseif (ev.EventName == "EvEntityPlayerEnterDesktopH") then
        self:clearDesktop(false)
    elseif (ev.EventName == "EvUiRequestChangeDesk") then
        local rpc = self.ControllerMgr.RPC
        rpc:RPC0(CommonMethodType.DesktopPlayerChangeDeskRequest)
    elseif (ev.EventName == "EvEntityGetDesktopData") then
        if (self.DesktopBase == nil) then
            return
        end
        local rpc = self.ControllerMgr.RPC
        rpc:RPC0(CommonMethodType.DesktopSnapshotRequest)
    end

    if (self.DesktopBase ~= nil) then
        self.DesktopBase:OnHandleEv(ev)
    end
end

---------------------------------------
function ControllerDesktopTexas:_timerUpdate(elapsed_tm)
    if (self.DesktopBase ~= nil) then
        self.DesktopBase:Update(elapsed_tm)
    end
end

---------------------------------------
function ControllerDesktopTexas:s2cDesktopSnapshotNotify(snapshot_notify)
    ViewHelper:UiEndWaiting()

    if (self.TimerUpdate == nil) then
        self.TimerUpdate = self.CasinosContext.TimerShaft:RegisterTimer(33, self, self._timerUpdate)
    end

    local desktop_data1 = snapshot_notify[1]
    local desktop_data = DesktopSnapshotData:new(nil)
    desktop_data:setData(desktop_data1)
    if (desktop_data1 == nil or desktop_data.DesktopData == nil) then
        local ev = self.ControllerMgr.ViewMgr:GetEv("EvEntityPlayerEnterDesktopFailed")
        if (ev == nil) then
            ev = EvEntityPlayerEnterDesktopFailed:new(nil)
        end
        self.ControllerMgr.ViewMgr:SendEv(ev)
        ViewHelper:UiShowInfoFailed(self.ControllerMgr.LanMgr:getLanValue("EnterTableFailed"))
    else
        local ev = self.ControllerMgr.ViewMgr:GetEv("EvEntityPlayerEnterDesktop")
        if (ev == nil) then
            ev = EvEntityPlayerEnterDesktop:new(nil)
        end
        self.ControllerMgr.ViewMgr:SendEv(ev)
        local is_init = snapshot_notify[2]
        if (is_init) then
            self:createDesktop(desktop_data.FactoryName)
        else
            if (self.DesktopBase == nil) then
                is_init = true
                self:createDesktop(desktop_data.FactoryName)
            end
        end

        self.DesktopBase:SetDesktopSnapshotData(desktop_data, is_init)
    end
end

---------------------------------------
function ControllerDesktopTexas:s2cPlayerDesktopPlayerEnterNotify(player_data)
    if (self.DesktopBase ~= nil) then
        self.DesktopBase:PlayerEnter(player_data)
    end
end

---------------------------------------
function ControllerDesktopTexas:s2cPlayerDesktopPlayerLeaveNotify(player_guid)
    if (self.DesktopBase ~= nil) then
        self.DesktopBase:PlayerLeave(player_guid)
    end
end

---------------------------------------
function ControllerDesktopTexas:s2cPlayerDesktopPlayerSitdown(sitdown_data)
    if (self.DesktopBase ~= nil) then
        self.DesktopBase:PlayerSitdown(sitdown_data)
    end
end

---------------------------------------
function ControllerDesktopTexas:s2cPlayerDesktopPlayerOb(player_guid)
    if (self.DesktopBase ~= nil) then
        self.DesktopBase:PlayerOb(player_guid)
    end
end

---------------------------------------
function ControllerDesktopTexas:s2cPlayerDesktopPlayerWaitWhile(player_guid)
    if (self.DesktopBase ~= nil) then
        self.DesktopBase:PlayerWaitWhile(player_guid)
    end
end

---------------------------------------
function ControllerDesktopTexas:s2cPlayerDesktopPlayerReturn(return_data)
    if (self.DesktopBase ~= nil) then
        self.DesktopBase:PlayerReturn(return_data)
    end
end

---------------------------------------
function ControllerDesktopTexas:s2cDesktopPlayerGiftChangeNotify(data)
    if (self.DesktopBase ~= nil) then
        self.DesktopBase:DesktopPlayerGiftChangeNotify(data)
    end
end

---------------------------------------
function ControllerDesktopTexas:s2cDesktopBuyAndSendItemNotify(data)
    if (self.DesktopBase ~= nil) then
        self.DesktopBase:DesktopBuyAndSendItemNotify(data)
    end
end

---------------------------------------
function ControllerDesktopTexas:s2cPlayerDesktopChat(msg1)
    if (self.DesktopBase ~= nil) then
        local msg = ChatMsg:new(nil)
        msg:setData(msg1)
        self.DesktopBase:DesktopChat(msg)
    end
end

---------------------------------------
function ControllerDesktopTexas:OnPlayerInvitePlayerEnterDesktopRequestResult(r)
    if (r == ProtocolResult.Success) then
        ViewHelper:UiShowInfoSuccess(self.ControllerMgr.LanMgr:getLanValue("InviteFriendToTable"))
    end
end

---------------------------------------
function ControllerDesktopTexas:s2cPlayerDesktopUser(info_user)
    if (self.DesktopBase ~= nil) then
        self.DesktopBase:DesktopUser(info_user)
    end
end

---------------------------------------
function ControllerDesktopTexas:OnMatchTexasRequestRebuyResult(r)
    if (self.DesktopBase == nil) then
        return
    end

    if (CS.System.String.IsNullOrEmpty(self.DesktopBase.DesktopGuid)) then
        return
    end

    local rebuy = BMatchTexasRebuyResponse:new(nil)
    rebuy:setData(r)
    --if rebuy.Result == ProtocolResult.Success then
    --    local view_mgr = self.ControllerMgr.ViewMgr
    --    local ev = view_mgr:GetEv("EvEntityMTTPlayerRebuy")
    --    if (ev == nil)
    --    then
    --        ev = EvEntityMTTPlayerRebuy:new(nil)
    --    end
    --    ev.RebuyInfo = rebuy
    --    view_mgr:SendEv(ev)
    --end
end

---------------------------------------
function ControllerDesktopTexas:OnMatchTexasRequestAddonResult(r)
    if (self.DesktopBase == nil) then
        return
    end

    if (CS.System.String.IsNullOrEmpty(self.DesktopBase.DesktopGuid)) then
        return
    end

    local addon = BMatchTexasAddonResponse:new(nil)
    addon:setData(r)

    --if addon.Result == ProtocolResult.Success then
    --    local view_mgr = self.ControllerMgr.ViewMgr
    --    local ev = view_mgr:GetEv("EvEntityMTTPlayerAddon")
    --    if (ev == nil)
    --    then
    --        ev = EvEntityMTTPlayerAddon:new(nil)
    --    end
    --    ev.AddonInfo = rebuy
    --    view_mgr:SendEv(ev)
    --end
end

---------------------------------------
function ControllerDesktopTexas:OnMatchTexasGameOverNotify(r)
    if (self.DesktopBase == nil) then
        return
    end

    if (CS.System.String.IsNullOrEmpty(self.DesktopBase.DesktopGuid)) then
        return
    end

    local game_over = BMatchTexasPlayerFinishedNotify:new(nil)
    game_over:setData(r)
    if game_over.Result == ProtocolResult.Success then
        local view_mgr = self.ControllerMgr.ViewMgr
        local ev = view_mgr:GetEv("EvEntityMatchGameOver")
        if (ev == nil)
        then
            ev = EvEntityMatchGameOver:new(nil)
        end
        ev.game_over = game_over
        view_mgr:SendEv(ev)
    end
end

---------------------------------------
function ControllerDesktopTexas:requestInvitePlayerEnterDesktop(friend_guid, desktop_guid, desktop_filter, player_num)
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
function ControllerDesktopTexas:OnPlayerLeaveDesktopNotify()
    ViewHelper:UiEndWaiting()
    self:clearDesktop(true)
    self.ControllerPlayer:RequestGetOnlinePlayerNum()
end

---------------------------------------
function ControllerDesktopTexas:clearDesktop(need_createmainui)
    if (self.TimerUpdate ~= nil) then
        self.TimerUpdate:Close()
        self.TimerUpdate = nil
    end
    if (self.DesktopBase ~= nil) then
        self.DesktopBase:OnDestroy(need_createmainui)
        self.DesktopBase = nil
    end
    self.ListDesktopChat = {}
end

---------------------------------------
function ControllerDesktopTexas:addDesktopMsg(sender_etguid, sender_name, sender_viplevel, chat_content)
    if ((sender_etguid == nil or sender_etguid == "") and self.LockSysChat) then
        return
    end
    local chat_info = ChatTextInfo:new(nil)
    chat_info.chat_content = chat_content
    chat_info.sender_etguid = sender_etguid
    chat_info.sender_name = sender_name
    chat_info.sender_viplevel = sender_viplevel
    table.insert(self.ListDesktopChat, chat_info)

    if (#self.ListDesktopChat > 50) then
        table.remove(self.ListDesktopChat, 1)
    end

    local ev = self.ControllerMgr.ViewMgr:GetEv("EvEntityRecvChatFromDesktop")
    if (ev == nil) then
        ev = EvEntityRecvChatFromDesktop:new(nil)
    end
    ev.chat_info = chat_info
    self.ControllerMgr.ViewMgr:SendEv(ev)
end

---------------------------------------
function ControllerDesktopTexas:createDesktop(desktop_factory_name)
    self.ControllerPlayer:DestroyMainUi()
    if (self.DesktopBase ~= nil) then
        self:clearDesktop(false)
    end

    local desktop_factory = self:GetDesktopBaseFactory(desktop_factory_name)
    self.DesktopBase = desktop_factory:CreateDesktop(self.ControllerMgr)
end

---------------------------------------
function ControllerDesktopTexas:RequestSendMsg(chat_msg)
    local rpc = self.ControllerMgr.RPC
    rpc:RPC1(CommonMethodType.DesktopChatRequest, chat_msg)
end

---------------------------------------
function ControllerDesktopTexas:RequestPlayerWaitWhile()
    local rpc = self.ControllerMgr.RPC
    rpc:RPC0(CommonMethodType.DesktopPlayerWaitWhileRequest)
end

---------------------------------------
function ControllerDesktopTexas:RequestPlayerOb()
    local rpc = self.ControllerMgr.RPC
    rpc:RPC0(CommonMethodType.DesktopPlayerObRequest)
end

---------------------------------------
function ControllerDesktopTexas:RequestPlayerReturn(data)
    local rpc = self.ControllerMgr.RPC
    local m = {}
    m["Stack"] = tostring(data)
    rpc:RPC1(CommonMethodType.DesktopPlayerReturnRequest, m)
end

---------------------------------------
function ControllerDesktopTexas:RequestPlayerSitdown(sitdown_info)
    local rpc = self.ControllerMgr.RPC
    rpc:RPC1(CommonMethodType.DesktopPlayerSitdownRequest, sitdown_info:getData4Pack())
end

---------------------------------------
function ControllerDesktopTexas:UserRequest(fac_name, method_info)
    local rpc = self.ControllerMgr.RPC
    local user = MethodInfoDesktopUser:new(nil)
    user.FactoryName = fac_name
    user.data = self.ControllerMgr:PackData(method_info)
    local d = user:getData4Pack()
    rpc:RPC1(CommonMethodType.DesktopUserRequest, d)
end

---------------------------------------
function ControllerDesktopTexas:MatchTexasRequestRebuy(match_guid)
    local rpc = self.ControllerMgr.RPC
    rpc:RPC1(CommonMethodType.MatchTexasRequestRebuy, match_guid)
end

---------------------------------------
function ControllerDesktopTexas:MatchTexasRequestAddon(match_guid)
    local rpc = self.ControllerMgr.RPC
    rpc:RPC1(CommonMethodType.MatchTexasRequestAddon, match_guid)
end

---------------------------------------
function ControllerDesktopTexas:MatchTexasRequestGiveUpRebuyOrAddon(match_guid)
    local rpc = self.ControllerMgr.RPC
    rpc:RPC1(CommonMethodType.MatchTexasRequestGiveUpRebuyOrAddon, match_guid)
end

---------------------------------------
function ControllerDesktopTexas:regDesktopBaseFactory(desktop_fac)
    self.MapDesktopBaseFac[desktop_fac:GetName()] = desktop_fac
end

---------------------------------------
function ControllerDesktopTexas:GetDesktopBaseFactory(fac_name)
    return self.MapDesktopBaseFac[fac_name]
end

---------------------------------------
function ControllerDesktopTexas:GetDesktopHelperBase(fac_name)
    return self.MapDesktopHelper[fac_name]
end

---------------------------------------
ControllerDesktopTexasFactory = class(ControllerFactory)

function ControllerDesktopTexasFactory:GetName()
    return 'DesktopTexas'
end

function ControllerDesktopTexasFactory:CreateController(controller_data)
    local ctrl_name = self:GetName()
    local ctrl = ControllerDesktopTexas:new(controller_data, ctrl_name)
    return ctrl
end