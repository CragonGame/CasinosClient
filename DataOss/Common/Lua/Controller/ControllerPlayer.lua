-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ControllerPlayer = ControllerBase:new(nil)

---------------------------------------
function ControllerPlayer:new(o, controller_mgr, controller_data, guid)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.Context = Context
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.ControllerData = controller_data
    self.ControllerMgr = controller_mgr
    self.Guid = guid
    self.ControllerName = "Player"
    self.ViewMgr = ViewMgr:new(nil)
    self.TopStarBundleId = "com.QuLing.TexasPoker"
    self.TimerUpdate = nil
    self.GetOnlinePlayerNumTimeElapsed = 0
    self.MC = CommonMethodType
    return o
end

---------------------------------------
function ControllerPlayer:OnCreate()
    self.ViewMgr:BindEvListener("EvUiClickHelp", self)
    self.ViewMgr:BindEvListener("EvUiClickEdit", self)
    self.ViewMgr:BindEvListener("EvUiClickLogin", self)
    self.ViewMgr:BindEvListener("EvEntityResetPwdSuccess", self)
    self.ViewMgr:BindEvListener("EvUiClickChangePlayerNickName", self)
    self.ViewMgr:BindEvListener("EvUiClickRefreshIPAddress", self)
    self.ViewMgr:BindEvListener("EvUiClickChangePlayerIndividualSignature", self)
    self.ViewMgr:BindEvListener("EvUiClickChangePlayerProfileSkin", self)
    self.ViewMgr:BindEvListener("EvUiReportFriend", self)
    self.ViewMgr:BindEvListener("EvUiClickChipTransaction", self)
    self.ViewMgr:BindEvListener("EvUiClickConfirmChipTransaction", self)
    self.ViewMgr:BindEvListener("EvUiCreateMainUi", self)
    self.ViewMgr:BindEvListener("EvUiRequestGetRankPlayerInfo", self)
    self.ViewMgr:BindEvListener("EvUiRequestBankWithdraw", self)
    self.ViewMgr:BindEvListener("EvUiRequestBankDeposit", self)
    self.ViewMgr:BindEvListener("EvCreateGiftShop", self)
    self.ViewMgr:BindEvListener("EvRequestGetPlayerModuleData", self)
    self.ViewMgr:BindEvListener("EvRequestGetOnLineReward", self)
    self.ViewMgr:BindEvListener("EvViewRequestGetTimingReward", self)
    self.ViewMgr:BindEvListener("EvViewOnGetOnLineReward", self)
    self.ViewMgr:BindEvListener("EvUiChangeLan", self)
    self.ViewMgr:BindEvListener("EvUiCloseActivityPopUpBox", self)
    self.ViewMgr:BindEvListener("EvUiRequestGetReceiverAddress", self)
    self.ViewMgr:BindEvListener("EvUiRequestEditReceiverAddress", self)
    self.ViewMgr:BindEvListener("EvConsoleCmd", self)
    self.ViewMgr:BindEvListener("EvClickShare", self)
    self.ViewMgr:BindEvListener("EvGetPicSuccess", self)
    self.ControllerActor = self.ControllerMgr:GetController("Actor")
    self.ControllerDesktopTexas = self.ControllerMgr:GetController("DesktopTexas")
    self.ControllerDesktopH = self.ControllerMgr:GetController("DesktopH")
    self.ControllerLobby = self.ControllerMgr:GetController("Lobby")
    self.ControllerActivity = self.ControllerMgr:GetController("Activity")
    self.ControllerUCenter = self.ControllerMgr:GetController("UCenter")
    self.ControllerTrade = self.ControllerMgr:GetController("Trade")
    self.OnlinePlayerNum = 0
    self.GetOtherPlayerInfoTicket = 1
    self.GetOnlinePlayerNumTimeElapsed = 0

    local login = self.ControllerMgr:GetController("Login")
    login:canDestroyViewLogin()

    self.OnLineReward = OnLineReward:new(nil, self.ControllerMgr.ViewMgr)
    self.TimingReward = TimingReward:new(nil, self.ControllerMgr.ViewMgr)

    self:createMainUi()

    self.QueneHotActivity = {}
    self:CheckNeedShowHotActivity()

    if (self.CasinosContext.UnityIOS == true) then
        self:initStoreItem()
    end

    self.ControllerMgr.RPC = self.ControllerMgr.RPC
    self.ControllerMgr.RPC:RPC0(self.MC.PlayerClientInitDoneRequest)
    self:requestGetOnlinePlayerNum()

    local c_login = self.ControllerMgr:GetController("Login")
    local player_play_state = c_login:GetClientEnterWorldNotify().player_play_state
    if (player_play_state ~= nil) then
        ViewHelper:UiBeginWaiting(self.ViewMgr.LanMgr:getLanValue("GetTable"))
        if (player_play_state.DesktopType == DesktopTypeEx.Desktop) then
            self.ControllerMgr.RPC:RPC0(self.MC.DesktopSnapshotRequest)
        else
            self.ControllerMgr.RPC:RPC0(self.MC.DesktopHRequestSnapshot)
        end
    end

    local ev = self.ControllerMgr.ViewMgr:GetEv("EvEntityPlayerInitDone")
    if (ev == nil) then
        ev = EvEntityPlayerInitDone:new(nil)
    end
    self.ControllerMgr.ViewMgr:SendEv(ev)

    self.TimerUpdate = self.CasinosContext.TimerShaft:RegisterTimer(1000, self, self._timerUpdate)

    -- 请求获取收货地址响应
    self.ControllerMgr.RPC:RegRpcMethod2(self.MC.PlayerRequestGetAddressResult, function(result, address)
        self:s2cPlayerRequestGetAddressResult(result, address)
    end)
    -- 请求编辑收货地址响应
    self.ControllerMgr.RPC:RegRpcMethod2(self.MC.PlayerRequestEditAddressResult, function(result, address)
        self:s2cPlayerRequestEditAddressResult(result, address)
    end)
    -- GM初始化通知
    self.ControllerMgr.RPC:RegRpcMethod0(self.MC.PlayerGMInitNotify, function()
        self:s2cPlayerGMInitNotify()
    end)
    -- Client配置改变通知
    self.ControllerMgr.RPC:RegRpcMethod0(self.MC.PlayerUpdateClientConfigNotify, function()
        self:s2cPlayerUpdateClientConfigNotify()
    end)
    -- 玩家升级通知
    self.ControllerMgr.RPC:RegRpcMethod1(self.MC.PlayerLevelupNotify, function(level_new)
        self:s2cPlayerLevelupNotify(level_new)
    end)
    -- 响应改昵称
    self.ControllerMgr.RPC:RegRpcMethod1(self.MC.PlayerChangeNickNameNotify, function(r)
        self:s2cPlayerChangeNickNameNotify(r)
    end)
    -- 响应刷新IP所在地
    self.ControllerMgr.RPC:RegRpcMethod1(self.MC.PlayerRefreshIpAddressNotify, function(ip_address)
        self:s2cPlayerRefreshIpAddressNotify(ip_address)
    end)
    -- 响应改签名
    self.ControllerMgr.RPC:RegRpcMethod1(self.MC.PlayerChangeIndividualSignatureNotify, function(r)
        self:s2cPlayerChangeIndividualSignatureNotify(r)
    end)
    -- 响应修改语言，参数：string lan
    self.ControllerMgr.RPC:RegRpcMethod1(self.MC.PlayerChangeLanNotify, function(lan)
        self:s2cPlayerChangeLanNotify(lan)
    end)
    -- 举报玩家
    self.ControllerMgr.RPC:RegRpcMethod1(self.MC.PlayerReportPlayerNotify, function(report)
        self:s2cPlayerReportPlayerNotify(report)
    end)
    -- 响应赠送玩家筹码，查询可赠送范围
    self.ControllerMgr.RPC:RegRpcMethod3(self.MC.PlayerGiveChipQueryRangeRequestResult, function(r, give_gold_min, give_gold_max)
        self:s2cPlayerGiveChipQueryRangeRequestResult(r, give_gold_min, give_gold_max)
    end)
    -- 响应赠送玩家筹码
    self.ControllerMgr.RPC:RegRpcMethod1(self.MC.PlayerGiveChipRequestResult, function(r)
        self:s2cPlayerGiveChipRequestResult(r)
    end)
    -- 响应赠送玩家筹码
    self.ControllerMgr.RPC:RegRpcMethod1(self.MC.PlayerBankDepositNotify, function(bank_notify)
        self:s2cPlayerBankDepositNotify(bank_notify)
    end)
    -- 从银行取筹码
    self.ControllerMgr.RPC:RegRpcMethod1(self.MC.PlayerBankWithdrawNotify, function(bank_notify)
        self:s2cPlayerBankWithdrawNotify(bank_notify)
    end)
    -- 玩家每日首次登陆通知
    self.ControllerMgr.RPC:RegRpcMethod1(self.MC.PlayerDailyFirstLoginNotify, function(daily_reward_tbid)
        self:s2cPlayerDailyFirstLoginNotify(daily_reward_tbid)
    end)
    -- 获取每日奖励
    self.ControllerMgr.RPC:RegRpcMethod1(self.MC.PlayerGetDailyRewardNotify, function(r)
        self:s2cPlayerGetDailyRewardNotify(r)
    end)
    -- 获取在线奖励
    self.ControllerMgr.RPC:RegRpcMethod2(self.MC.PlayerGetOnlineRewardRequestResult, function(result, reward)
        self:s2cPlayerGetOnlineRewardRequestResult(result, reward)
    end)
    -- 在线奖励推送
    self.ControllerMgr.RPC:RegRpcMethod3(self.MC.PlayerGetOnlineRewardNotify, function(online_reward_state, left_reward_second, next_reward)
        self:s2cPlayerGetOnlineRewardNotify(online_reward_state, left_reward_second, next_reward)
    end)
    -- 响应获取其他玩家信息
    self.ControllerMgr.RPC:RegRpcMethod2(self.MC.PlayerGetPlayerInfoOtherNotify, function(player_info, ticket)
        self:s2cPlayerGetPlayerInfoOtherNotify(player_info, ticket)
    end)
    -- 获取在线玩家数
    self.ControllerMgr.RPC:RegRpcMethod1(self.MC.PlayerGetOnlinePlayerNumNotify, function(num)
        self:s2cPlayerGetOnlinePlayerNumNotify(num)
    end)
    self.ControllerMgr.RPC:RegRpcMethod1(CommonMethodType.PlayerRecvInvitePlayerEnterDesktopNotify, function(r)
        self:OnPlayerRecvInvitePlayerEnterDesktopNotify(r)
    end)
    self.ControllerMgr.RPC:RegRpcMethod1(CommonMethodType.PlayerGetCasinosModuleDataWithFactoryNameNotify, function(r)
        self:OnPlayerGetCasinosModuleDataWithFactoryNameNotify(r)
    end)
    self.ControllerMgr.RPC:RegRpcMethod1(CommonMethodType.PlayerGetTimingRewardNotify, function(r)
        self:OnPlayerGetTimingRewardNotify(r)
    end)
    self.ControllerMgr.RPC:RegRpcMethod2(CommonMethodType.PlayerGetTimingRewardRequestResult, function(r1, r2)
        self:OnPlayerGetTimingRewardRequestResult(r1, r2)
    end)
    self.ControllerMgr.RPC:RegRpcMethod1(CommonMethodType.PlayerOpenUrlNotify, function(r)
        self:OnPlayerOpenUrlNotify(r)
    end)
    self.ControllerMgr.RPC:RegRpcMethod1(CommonMethodType.PlayerDevConsoleCmdNotify, function(r)
        self:OnPlayerDevConsoleCmdNotify(r)
    end)
end

---------------------------------------
function ControllerPlayer:OnDestroy()
    if (self.TimerUpdate ~= nil) then
        self.TimerUpdate:Close()
        self.TimerUpdate = nil
    end
    self.ViewMgr:UnbindEvListener(self)
    self:DestroyMainUi()
    self.ControllerMgr.ViewMgr:DestroyAllView()
    self.CasinosContext:StopAllSceneSound()
end

---------------------------------------
function ControllerPlayer:OnHandleEv(ev)
    if (ev.EventName == "EvUiClickHelp") then
    elseif (ev.EventName == "EvUiClickEdit") then
    elseif (ev.EventName == "EvUiClickLogin") then
        local title = self.ControllerMgr.LanMgr:getLanValue("ReturnLogin")
        local tips = self.ControllerMgr.LanMgr:getLanValue("ExitCanLogin")
        local msg_box = self.ControllerMgr.ViewMgr:CreateView("MsgBox")
        msg_box:showMsgBox1(title, tips,
                function(bo)
                    if (bo) then
                        ViewHelper:UiBeginWaiting(self.ControllerMgr.LanMgr:getLanValue("Logouting"))
                        self.CasinosContext.NetMgr:Disconnect()
                    end
                end
        )
    elseif (ev.EventName == "EvEntityResetPwdSuccess") then
        self.CasinosContext.NetMgr:Disconnect()
    elseif (ev.EventName == "EvUiClickChangePlayerNickName") then
        self:requestChangeNickName(ev.new_name)
    elseif (ev.EventName == "EvUiClickRefreshIPAddress") then
        self:requestRefreshIpAddress()
    elseif (ev.EventName == "EvUiClickChangePlayerIndividualSignature") then
        self:requestChangeIndividualSignature(ev.new_individual_signature)
    elseif (ev.EventName == "EvUiClickChangePlayerProfileSkin") then
        self:requestChangePlayerProfileSkin(ev.skin_id)
    elseif (ev.EventName == "EvUiReportFriend") then
        local et_guid = ev.friend_etguid
        local report_type = ev.report_type
        self:requestReportPlayer(et_guid, report_type)
    elseif (ev.EventName == "EvUiClickChipTransaction") then
        self:requestGiveGoldQueryRange()
    elseif (ev.EventName == "EvUiClickConfirmChipTransaction") then
        self:requestGivePlayerGold(ev.send_target_etguid, ev.chip)
    elseif (ev.EventName == "EvUiCreateMainUi") then
        self:createMainUi()
    elseif (ev.EventName == "EvUiRequestGetRankPlayerInfo") then
        self:requestGetPlayerInfoOther(ev.player_guid)
    elseif (ev.EventName == "EvUiRequestBankWithdraw") then
        self:requestBankWithdraw(ev.withdraw_chip)
    elseif (ev.EventName == "EvUiRequestBankDeposit") then
        self:requestBankDeposit(ev.deposit_chip)
    elseif (ev.EventName == "EvCreateGiftShop") then
        local can_creategiftshop = true
        if (ev.not_indesktop == false and ev.is_tmp_gift) then
            if (self.ControllerDesktopTexas.DesktopBase ~= nil) then
                if (self.ControllerDesktopTexas.DesktopBase.MePlayer.IsInGame == false) then
                    can_creategiftshop = false
                end
            else
                if (self.ControllerDesktopH.DesktopHBase == nil) then
                    can_creategiftshop = false
                end
            end
        end
        if (can_creategiftshop) then
            local gift_shop = self.ControllerMgr.ViewMgr:CreateView("GiftShop")
            gift_shop:setGiftShopInfo(ev.is_tmp_gift, self.Guid, ev.to_player_etguid)
        else
            ViewHelper:UiShowInfoFailed(self.ControllerMgr.LanMgr:getLanValue("SitTableSendGift"))
        end
    elseif (ev.EventName == "EvRequestGetPlayerModuleData") then
        self.ControllerMgr.RPC:RPC1(self.MC.PlayerGetCasinosModuleDataWithFactoryNameRequest, ev.factory_name)
    elseif (ev.EventName == "EvRequestGetOnLineReward") then
        self.ControllerMgr.RPC:RPC0(self.MC.PlayerGetOnlineRewardRequest)
    elseif (ev.EventName == "EvViewOnGetOnLineReward") then
        self.OnLineReward:onGetReward()
    elseif (ev.EventName == "EvViewRequestGetTimingReward") then
        local can_get = self.TimingReward:onGetReward()
        if can_get then
            self.ControllerMgr.RPC:RPC0(self.MC.PlayerGetTimingRewardRequest)
        end
    elseif (ev.EventName == "EvUiChangeLan") then
        ViewHelper:UiBeginWaiting(self.ControllerMgr.LanMgr:getLanValue("ChangeLaning"), 10)
        self.ControllerMgr.RPC:RPC1(self.MC.PlayerChangeLanRequest, ev.lan)
    elseif (ev.EventName == "EvUiCloseActivityPopUpBox") then
        CS.UnityEngine.PlayerPrefs.SetString(self.ControllerActivity.CurrentActID, "true")
        self:CreateViewActivityPopUpBox()
    elseif (ev.EventName == "EvUiRequestGetReceiverAddress") then
        self:RequestGetAddress()
    elseif (ev.EventName == "EvUiRequestEditReceiverAddress") then
        local address = ev.Address
        self:RequestEditAddress(address)
    elseif (ev.EventName == "EvConsoleCmd") then
        self:requestConsoleCmd(ev.ListParam)
    elseif (ev.EventName == "EvClickShare") then
        local share_type = ev.ShareType
        local share = self.ControllerMgr.ViewMgr:CreateView("Share")
        share:SetPlayerInfo(self.ControllerActor.PropNickName:get(), self.ControllerActor.PropAccountId:get(), share_type)
    elseif (ev.EventName == "EvGetPicSuccess") then
        self:OnGetPicSuccess(ev.pic_data)
    end
end

---------------------------------------
function ControllerPlayer:s2cPlayerLevelupNotify(level_new)
end

---------------------------------------
function ControllerPlayer:s2cPlayerDailyFirstLoginNotify(daily_reward_tbid)
    local map_tbdata = self.ControllerMgr.TbDataMgr:GetMapData("DailyReward")
    local view_daily_reward = self.ControllerMgr.ViewMgr:CreateView("DailyReward")
    view_daily_reward:setRewardInfo(map_tbdata, daily_reward_tbid,
            function(bo)
                if (bo) then
                    self.ControllerMgr.RPC:RPC0(self.MC.PlayerGetDailyRewardRequest)
                end
            end
    )
end

---------------------------------------
function ControllerPlayer:OnPlayerLeaveDesktopNotify()
    ViewHelper:UiEndWaiting()
    local controller_desk = self.ControllerMgr:GetController("DesktopTexas")
    controller_desk:clearDesktop(true)
    self:requestGetOnlinePlayerNum()
end

---------------------------------------
function ControllerPlayer:s2cPlayerGetOnlinePlayerNumNotify(num)
    self.OnlinePlayerNum = num
    local ev = self.ControllerMgr.ViewMgr:GetEv("EvEntitySetOnLinePlayerNum")
    if (ev == nil) then
        ev = EvEntitySetOnLinePlayerNum:new(nil)
    end
    ev.online_num = self.OnlinePlayerNum
    self.ControllerMgr.ViewMgr:SendEv(ev)
end

---------------------------------------
function ControllerPlayer:s2cPlayerGetPlayerInfoOtherNotify(player_info, ticket)
    ViewHelper:UiEndWaiting()
    local p_i = PlayerInfo:new(nil)
    p_i:setData(player_info)
    local ev = self.ControllerMgr.ViewMgr:GetEv("EvEntityGetPlayerInfoOther")
    if (ev == nil) then
        ev = EvEntityGetPlayerInfoOther:new(nil)
    end
    ev.player_info = p_i
    ev.ticket = ticket
    self.ControllerMgr.ViewMgr:SendEv(ev)
end

---------------------------------------
function ControllerPlayer:OnPlayerChangeProfileSkinNotify(profileskin_tableid)
    ViewHelper:UiShowInfoSuccess(self.ControllerMgr.LanMgr:getLanValue("SkinSuccess"))
end

---------------------------------------
function ControllerPlayer:s2cPlayerReportPlayerNotify(report)
    ViewHelper:UiShowInfoSuccess(self.ControllerMgr.LanMgr:getLanValue("ReportSuccess"))
end

---------------------------------------
function ControllerPlayer:PlayerInvitePlayerEnterDesktopRequestResult(r)
    if (r == ProtocolResult.Success) then
        ViewHelper:UiShowInfoSuccess(self.ControllerMgr.LanMgr:getLanValue("InviteFriendToTable"))
    end
end

---------------------------------------
function ControllerPlayer:OnPlayerRecvInvitePlayerEnterDesktopNotify(invite1)
    local invite = InvitePlayerEnterDesktop:new(nil)
    invite:setData(invite1)
    local desktop_helper = self.ControllerDesktopTexas:GetDesktopHelperBase(invite.desktop_filter.FactoryName)
    if (desktop_helper == nil) then
        return
    end
    local desktopinfo_format = desktop_helper:GetDesktopInfoFormat(self.ControllerMgr.RPC.MessagePack, self.ControllerMgr.TbDataMgr,
            invite.desktop_filter, self.ControllerMgr.LanMgr.LanBase)
    local msg_box = self.ControllerMgr.ViewMgr:CreateView("MsgBox")
    local tips = self.CasinosContext:AppendStrWithSB(self.ControllerMgr.LanMgr:getLanValue("Player"),
            invite.player_nickname, "\n", desktopinfo_format.DesktopFormat, "\n",
            self.ControllerMgr.LanMgr:getLanValue("Player"), tostring(invite.player_num), "/", desktopinfo_format.Param)
    msg_box:showMsgBox1(self.ControllerMgr.LanMgr:getLanValue("InviteFriendPlay"), tips,
            function(accept)
                if (accept) then
                    if (self.ControllerDesktopH.DesktopHBase == nil) then
                        if (self.ControllerDesktopTexas.DesktopBase == nil) then
                            self.ControllerLobby:RequestEnterDesktop(invite.desktop_guid, false, 255, invite.desktop_filter:getData4Pack())
                        else
                            local msg_boxex = self.ControllerMgr.ViewMgr:CreateView("MsgBox")
                            msg_boxex:showMsgBox1(self.ControllerMgr.LanMgr:getLanValue("InviteFriendPlay"),
                                    self.ControllerMgr.LanMgr:getLanValue("LeaveAndEnterNew"),
                                    function(confirm)
                                        self.ControllerLobby:RequestEnterDesktop(invite.desktop_guid, false, 255, invite.desktop_filter:getData4Pack())
                                    end
                            )
                        end
                    else
                        self.ControllerDesktopH:receiveInvitePlayerEnterDesktop(invite.desktop_guid, invite.desktop_filter)
                    end
                end
            end
    )
end

---------------------------------------
function ControllerPlayer:OnPlayerGetTimingRewardNotify(invite1)
    local reward = TimingRewardData:new(nil)
    reward:setData(invite1)
    self.TimingReward:setTimingRewardData(reward)
end

---------------------------------------
function ControllerPlayer:OnPlayerGetTimingRewardRequestResult(result, reward_gold)
    if result == ProtocolResult.Success then
        ViewHelper:UiShowInfoSuccess(string.format(self.ControllerMgr.LanMgr:getLanValue("GetRewardSuccess"), tostring(reward_gold)))
    end
end

---------------------------------------
function ControllerPlayer:OnPlayerOpenUrlNotify(url)
    self.ControllerMgr.UniWebView:Load(url)
    self.ControllerMgr.UniWebView:Show()
    self.ControllerMgr.UniWebView:SetShowToolbar(true, false, false)
end

---------------------------------------
function ControllerPlayer:OnPlayerDevConsoleCmdNotify(notify)
    if notify ~= nil then
        ViewHelper:UiShowInfoSuccess(notify)
    end
end

---------------------------------------
function ControllerPlayer:s2cPlayerGiveChipQueryRangeRequestResult(r, give_gold_min, give_gold_max)
    local is_success = false
    if (r == ProtocolResult.Success) then
        is_success = true
    else
        local msg = ""
        if (r == ProtocolResult.GiveChipBeyondTheLimit) then
            local tips = self.ControllerMgr.LanMgr:getLanValue("MoreThanEachDayNoSend")
            msg = string.format(tips, self.ViewMgr.LanMgr:getLanValue("Chip"))
            ViewHelper:UiShowInfoFailed(msg)
        elseif (r == ProtocolResult.GiveChipNotEnoughChip) then
            local tips = self.ControllerMgr.LanMgr:getLanValue("CarrySmallNoSend")
            msg = string.format(tips, self.ViewMgr.LanMgr:getLanValue("Chip"))
            ViewHelper:UiShowInfoFailed(msg)
        end
    end
    local ev = self.ControllerMgr.ViewMgr:GetEv("EvEntityPlayerGiveChipQueryRangeRequestResult")
    if (ev == nil) then
        ev = EvEntityPlayerGiveChipQueryRangeRequestResult:new(nil)
    end
    ev.give_chip_max = give_gold_max
    ev.give_chip_min = give_gold_min
    ev.is_success = is_success
    self.ControllerMgr.ViewMgr:SendEv(ev)
end

---------------------------------------
function ControllerPlayer:s2cPlayerGiveChipRequestResult(r)
    local msg = ""
    if (r == ProtocolResult.Success) then
        msg = string.format(self.ControllerMgr.LanMgr:getLanValue("GiveSuccess"), self.ViewMgr.LanMgr:getLanValue("Chip"))
        ViewHelper:UiShowInfoSuccess(msg)
    else
        if (r == ProtocolResult.GiveChipNotEnoughChip) then
            msg = self.ControllerMgr.LanMgr:getLanValue("HaveTooLittleFail")
        elseif (r == ProtocolResult.GiveChipMoreThanMine) then
            msg = self.ControllerMgr.LanMgr:getLanValue("GiveMoreIOwnFail")
        elseif (r == ProtocolResult.GiveChipTooSmall) then
            msg = self.ControllerMgr.LanMgr:getLanValue("GiveSmallFail")
        elseif (r == ProtocolResult.GiveChipBeyondTheLimit) then
            msg = self.ControllerMgr.LanMgr:getLanValue("GiveMoreFail")
        else
            msg = self.ControllerMgr.LanMgr:getLanValue("GivePlayerFail")
        end

        msg = string.format(msg, self.ViewMgr.LanMgr:getLanValue("Chip"))
        ViewHelper:UiShowInfoFailed(msg)
    end
end

---------------------------------------
function ControllerPlayer:s2cPlayerBankDepositNotify(bank_notify)
    local data = BankNotify:new(nil)
    data:setData(bank_notify)
    if (data.result == ProtocolResult.Success) then
        self.ControllerActor.PropGoldAcc:set(data.acc_golds)
        self.ControllerActor.PropGoldBank:set(data.bank_golds)
        ViewHelper:UiShowInfoSuccess(self.ControllerMgr.LanMgr:getLanValue("SaveSuccess"))
    else
        ViewHelper:UiShowInfoFailed(self.ControllerMgr.LanMgr:getLanValue("SaveFail"))
    end
end

---------------------------------------
function ControllerPlayer:s2cPlayerBankWithdrawNotify(bank_notify)
    local data = BankNotify:new(nil)
    data:setData(bank_notify)
    if (data.result == ProtocolResult.Success) then
        self.ControllerActor.PropGoldAcc:set(data.acc_golds)
        self.ControllerActor.PropGoldBank:set(data.bank_golds)
        ViewHelper:UiShowInfoSuccess(self.ControllerMgr.LanMgr:getLanValue("WithdrawalSuccess"))
    else
        ViewHelper:UiShowInfoFailed(self.ControllerMgr.LanMgr:getLanValue("WithdrawalFail"))
    end
end

---------------------------------------
function ControllerPlayer:s2cPlayerGetDailyRewardNotify(r)
    if (r == ProtocolResult.Success) then
        ViewHelper:UiShowInfoSuccess(self.ControllerMgr.LanMgr:getLanValue("GetDailyReward"))
    else
        ViewHelper:UiShowInfoFailed(self.ControllerMgr.LanMgr:getLanValue("GetDailyRewardFail"))
    end
end

---------------------------------------
function ControllerPlayer:s2cPlayerGetOnlineRewardRequestResult(result, reward)
    if (result == ProtocolResult.Success) then
        --[[local view_main = self.ControllerMgr.ViewMgr:GetView("Main")
        if(view_main ~= nil)
        then
            view_main:ShowGetChipEffect()
        end]]
        ViewHelper:UiShowInfoSuccess(string.format(self.ControllerMgr.LanMgr:getLanValue("GetOnlinReward"), tostring(reward)))
    else
        ViewHelper:UiShowInfoFailed(self.ControllerMgr.LanMgr:getLanValue("GetOnlinRewardFail"))
    end
end

---------------------------------------
function ControllerPlayer:s2cPlayerGetOnlineRewardNotify(online_reward_state, left_reward_second, next_reward)
    self.OnLineReward:setOnlineRewardState(online_reward_state, left_reward_second, next_reward)
end

---------------------------------------
function ControllerPlayer:s2cPlayerChangeNickNameNotify(r)
    self.ControllerActor.PropNickName:set(r)
end

---------------------------------------
function ControllerPlayer:s2cPlayerRefreshIpAddressNotify(ip_address)
    if (ip_address ~= nil and ip_address ~= "") then
        self.ControllerActor.PropIpAddress:set(ip_address)
    end
    ViewHelper:UiShowInfoSuccess(self.ControllerMgr.LanMgr:getLanValue("IPRefresh"))
end

---------------------------------------
function ControllerPlayer:s2cPlayerChangeIndividualSignatureNotify(r)
    self.ControllerActor.PropIndividualSignature:set(r)
end

---------------------------------------
function ControllerPlayer:s2cPlayerUpdateClientConfigNotify()
    --[[CS.UnityEngine.Debug.Log("OnPlayerUpdateClientConfigNotify")
    self.CasinosContext:loadDbConfig(
        function(need_reloaddb,
            function()
            end
        )
        end,
        true,
        function()
            local ev = self.ControllerMgr.ViewMgr:GetEv("EvEntityUpdateClientConfig")
            if(ev == nil)
            then
                ev = EvEntityUpdateClientConfig:new(nil)
            end
            self.ControllerMgr.ViewMgr:SendEv(ev)
        end
    )--]]
end

---------------------------------------
function ControllerPlayer:OnPlayerGetCasinosModuleDataWithFactoryNameNotify(player_moduledata)
    local p_d = PlayerModuleData:new(nil)
    p_d:setData(player_moduledata)
    local ev = self.ControllerMgr.ViewMgr:GetEv("EvEntityGetPlayerModuleDataSuccess")
    if (ev == nil) then
        ev = EvEntityGetPlayerModuleDataSuccess:new(nil)
    end
    ev.player_moduledata = p_d
    self.ControllerMgr.ViewMgr:SendEv(ev)
end

---------------------------------------
function ControllerPlayer:s2cPlayerGMInitNotify()
    self.IsGm = true
end

---------------------------------------
function ControllerPlayer:s2cPlayerChangeLanNotify(lan)
    ViewHelper:UiEndWaiting()
    local tips = self.ControllerMgr.LanMgr:getLanValue(CS.Casinos.IItemLan.ChangeSuccessTipsKey)
    ViewHelper:UiShowInfoSuccess(tips)
    self.ControllerMgr.LanMgr:setLan(lan)
    self:createMainUi()
    local controller_im = self.ControllerMgr:GetController("IM")
    controller_im:setMainUiIMInfo()
end

---------------------------------------
function ControllerPlayer:OnCommonMethordResponse(map_param)
end

---------------------------------------
function ControllerPlayer:OnCommonMethordNotify(map_param)
end

---------------------------------------
function ControllerPlayer:requestGetOnlinePlayerNum()
    self.ControllerMgr.RPC:RPC0(self.MC.PlayerGetOnlinePlayerNumRequest)
end

---------------------------------------
function ControllerPlayer:requestGetPlayerInfoOther(player_guid)
    ViewHelper:UiBeginWaiting(self.ControllerMgr.LanMgr:getLanValue("GetPlayerInfo"))
    self.GetOtherPlayerInfoTicket = self.GetOtherPlayerInfoTicket + 1
    self.ControllerMgr.RPC:RPC2(self.MC.PlayerGetPlayerInfoOtherRequest, player_guid, self.GetOtherPlayerInfoTicket)
    return self.GetOtherPlayerInfoTicket
end

---------------------------------------
function ControllerPlayer:requestChangePlayerProfileSkin(profileskin_tbid)
    --self.SessionBinderPlayer:RequestChangePlayerProfileSkin(profileskin_tbid)
end

---------------------------------------
function ControllerPlayer:requestChangeNickName(nick_name)
    self.ControllerMgr.RPC:RPC1(self.MC.PlayerChangeNickNameRequest, nick_name)
end

---------------------------------------
function ControllerPlayer:requestRefreshIpAddress()
    self.ControllerMgr.RPC:RPC0(self.MC.PlayerRefreshIpAddressRequest)
end

---------------------------------------
function ControllerPlayer:requestChangeIndividualSignature(sign)
    self.ControllerMgr.RPC:RPC1(self.MC.PlayerChangeIndividualSignatureRequest, sign)
end

---------------------------------------
function ControllerPlayer:requestReportPlayer(player_guid, report_type)
    local report = ReportPlayer:new(nil)
    report.player_guid = player_guid
    report.report_type = report_type
    self.ControllerMgr.RPC:RPC1(self.MC.PlayerReportPlayerRequest, report:getData4Pack())
end

---------------------------------------
function ControllerPlayer:requestGiveGoldQueryRange()
    self.ControllerMgr.RPC:RPC0(self.MC.PlayerGiveChipQueryRangeRequest)
end

---------------------------------------
function ControllerPlayer:requestGivePlayerGold(to_player_guid, give_gold)
    self.ControllerMgr.RPC:RPC2(self.MC.PlayerGiveChipRequest, to_player_guid, give_gold)
end

---------------------------------------
function ControllerPlayer:requestBankDeposit(bank_deposit)
    self.ControllerMgr.RPC:RPC1(self.MC.PlayerBankDepositRequest, bank_deposit)
end

---------------------------------------
function ControllerPlayer:requestBankWithdraw(bank_withdraw)
    self.ControllerMgr.RPC:RPC1(self.MC.PlayerBankWithdrawRequest, bank_withdraw)
end

---------------------------------------
function ControllerPlayer:createMainUi()
    local index = math.random(1, 2)
    local bgm = string.format("MainBg%s", index)
    self.CasinosContext:Play(bgm, CS.Casinos._eSoundLayer.Background)
    local view_main = self.ControllerMgr.ViewMgr:CreateView("Main")
    view_main:SetPlayerInfo()
    view_main:setOnlineNum(self.OnlinePlayerNum)
end

---------------------------------------
function ControllerPlayer:DestroyMainUi()
    local view_main = self.ControllerMgr.ViewMgr:GetView("Main")
    self.ControllerMgr.ViewMgr:DestroyView(view_main)
end

---------------------------------------
function ControllerPlayer:getDesktopChat()
    local map_chat = {}
    if (self.ControllerDesktopTexas.DesktopBase ~= nil) then
        local index = #self.ControllerDesktopTexas.ListDesktopChat - 1
        for key, value in pairs(self.ControllerDesktopTexas.ListDesktopChat) do
            map_chat[index] = value
            index = index - 1
        end
    else
        if (self.ControllerDesktopH.DesktopHBase ~= nil) then
            local index = #self.ControllerDesktopH.ListDesktopChat - 1
            for key, value in pairs(self.ControllerDesktopH.ListDesktopChat) do
                map_chat[index] = value
                index = index - 1
            end
        end
    end
    return map_chat
end

---------------------------------------
function ControllerPlayer:initStoreItem()
    local t_sku = {}
    local map_tbdata = self.ControllerMgr.TbDataMgr:GetMapData("UnitBilling")
    for key, value in pairs(map_tbdata) do
        table.insert(t_sku, value.StoreSKU)
    end
    local first_recharge_sku = TbDataHelper:GetCommonValue("FirstRechargeStoreSKU")
    if (first_recharge_sku ~= nil and first_recharge_sku ~= "") then
        table.insert(t_sku, first_recharge_sku)
    end
    -- TODO
    --CS.Pay.Instant():initInventory(t_sku)
end

---------------------------------------
function ControllerPlayer:OnGetPicSuccess(pic_data)
    local account_id = self.ControllerActor.PropAccountId:get()
    self.ControllerUCenter:RequestUploadProfileImage(self.Context.Cfg.UCenterAppId, account_id, pic_data,
            function(status, response, error)
                --self:uploadProfileImageCallBack(status, response, error)
                if (status == UCenterResponseStatus.Error) then
                    local msg_t = {}
                    table.insert(msg_t, self.ControllerMgr.LanMgr:getLanValue("UploadPicFailed"))
                    table.insert(msg_t, "! ErrorCode: ")
                    table.insert(msg_t, error.code)
                    table.insert(msg_t, " Msg: ")
                    table.insert(msg_t, error.message)
                    local msg = table.concat(msg_t)
                    ViewHelper:UiShowInfoFailed(msg)
                else
                    local ev = self.ControllerMgr.ViewMgr:GetEv("EvGetPicUpLoadSuccess")
                    if (ev == nil) then
                        ev = EvGetPicUpLoadSuccess:new(nil)
                    end
                    self.ControllerMgr.ViewMgr:SendEv(ev)
                end
                ViewHelper:UiEndWaiting()
            end)
end

---------------------------------------
function ControllerPlayer:CheckNeedShowHotActivity()
    if (CS.UnityEngine.PlayerPrefs.HasKey(self.ControllerActivity.CurrentActID)) then
        if (CS.UnityEngine.PlayerPrefs.GetString(self.ControllerActivity.CurrentActID) == "true") then
            return
        else
            self:ShowHotActivity()
        end
    else
        CS.UnityEngine.PlayerPrefs.SetString(self.ControllerActivity.CurrentActID, "false")
        self:ShowHotActivity()
    end
end

---------------------------------------
function ControllerPlayer:ShowHotActivity()
    local list_activity = self.ControllerActivity.ListActivity
    if (#list_activity > 0) then
        for i = 1, #list_activity do
            local temp = list_activity[i]
            if (temp.Type == "Hot") then
                table.insert(self.QueneHotActivity, temp)
            end
        end
        self:CreateViewActivityPopUpBox()
    end
end

---------------------------------------
function ControllerPlayer:CreateViewActivityPopUpBox()
    if (#self.QueneHotActivity > 0) then
        local temp = table.remove(self.QueneHotActivity, 1)
        local view_activitypopupbox = self.ControllerMgr.ViewMgr:CreateView("ActivityPopup")
        view_activitypopupbox:SetActivityInfo(temp)
    end
end

---------------------------------------
--function ControllerPlayer:uploadProfileImageCallBack(status, response, error)
--    if (status == UCenterResponseStatus.Error) then
--        local msg_t = {}
--        table.insert(msg_t, self.ControllerMgr.LanMgr:getLanValue("UploadPicFailed"))
--        table.insert(msg_t, "! ErrorCode: ")
--        table.insert(msg_t, error.code)
--        table.insert(msg_t, " Msg: ")
--        table.insert(msg_t, error.message)
--        local msg = table.concat(msg_t)
--        ViewHelper:UiShowInfoFailed(msg)
--    else
--        local ev = self.ControllerMgr.ViewMgr:GetEv("EvGetPicUpLoadSuccess")
--        if (ev == nil) then
--            ev = EvGetPicUpLoadSuccess:new(nil)
--        end
--        self.ControllerMgr.ViewMgr:SendEv(ev)
--    end
--    ViewHelper:UiEndWaiting()
--end

---------------------------------------
-- 控制台命令
function ControllerPlayer:requestConsoleCmd(list_param)
    self.ControllerMgr.RPC:RPC1(self.MC.PlayerDevConsoleCmdRequest2, list_param)
end

---------------------------------------
-- 请求获取收货地址
function ControllerPlayer:RequestGetAddress()
    self.ControllerMgr.RPC:RPC0(self.MC.PlayerRequestGetAddress)
end

---------------------------------------
-- 请求编辑收货地址
function ControllerPlayer:RequestEditAddress(address)
    self.ControllerMgr.RPC:RPC1(self.MC.PlayerRequestEditAddress, address:getData4Pack())
end

---------------------------------------
-- 响应获取收货地址
function ControllerPlayer:s2cPlayerRequestGetAddressResult(result, address)
    local data_address = PlayerAddress:new(nil)
    data_address:setData(address)
    if (result == ProtocolResult.Success) then
        local ev = self.ControllerMgr.ViewMgr:GetEv("EvEntityResponseGetReceiverAddress")
        if (ev == nil) then
            ev = EvEntityResponseGetReceiverAddress:new(nil)
        end

        ev.Address = data_address
        self.ControllerMgr.ViewMgr:SendEv(ev)
    elseif (result == ProtocolResult.Failed) then
        local msg_box = self.ControllerMgr.ViewMgr:CreateView("MsgBox")
        msg_box:showMsgBox1("", self.ControllerMgr.LanMgr:getLanValue("GetAddressFailed"))
    end
end

---------------------------------------
-- 响应编辑收货地址
function ControllerPlayer:s2cPlayerRequestEditAddressResult(result, address)
    --[[if (result == ProtocolResult.Success)
    then
        local msg_box = self.ControllerMgr.ViewMgr:CreateView("MsgBox")
        msg_box:showMsgBox1("", "信息提交成功")]]
    if (result == ProtocolResult.Failed) then
        local msg_box = self.ControllerMgr.ViewMgr:CreateView("MsgBox")
        msg_box:showMsgBox1("", self.ControllerMgr.LanMgr:getLanValue("UploadAddressFailed"))
    end
end

---------------------------------------
-- 定时更新
function ControllerPlayer:_timerUpdate(tm)
    self.GetOnlinePlayerNumTimeElapsed = self.GetOnlinePlayerNumTimeElapsed + tm
    if (self.GetOnlinePlayerNumTimeElapsed >= 5) then
        self.GetOnlinePlayerNumTimeElapsed = 0
        self:requestGetOnlinePlayerNum()
    end
    if (self.OnLineReward ~= nil) then
        self.OnLineReward:Update()
    end
end

---------------------------------------
ControllerPlayerFactory = ControllerFactory:new()

---------------------------------------
function ControllerPlayerFactory:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.ControllerName = "Player"
    return o
end

---------------------------------------
function ControllerPlayerFactory:createController(controller_mgr, controller_data, guid)
    local controller = ControllerPlayer:new(nil, controller_mgr, controller_data, guid)
    return controller
end