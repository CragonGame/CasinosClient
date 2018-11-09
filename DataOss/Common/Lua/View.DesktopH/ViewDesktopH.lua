-- Copyright(c) Cragon. All rights reserved.
require('UiDesktopHBanker')
require('UiDesktopHBetOperateItem')
require('UiDesktopHBetPot')
require('UiDesktopHBetPotItem')
require('UiDesktopHCard')
require('UiDesktopHCards')
require('UiDesktopHCardTypeBase')
require('UiDesktopHCardTypeTexas')
require('UiDesktopHChair')
require('UiDesktopHDealer')
require('UiDesktopHFlow')
require('UiDesktopHGold')
require('UiDesktopHGoldPool')
require('UiDesktopHHistroy')
require('UiDesktopHMe')
require('UiDesktopHRewardPot')
require('UiDesktopHSeat')
require('UiDesktopHStandPlayer')
require('UiDesktopHTongPei')
require('UiDesktopHTongSha')

------------------------------------------
ViewDesktopH = ViewBase:new()

---------------------------------------
function ViewDesktopH:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ViewMgr = nil
    o.GoUi = nil
    o.ComUi = nil
    o.Panel = nil
    o.UILayer = nil
    o.InitDepth = nil
    o.ViewKey = nil
    o.UiDesktopHGoldPool = nil
    o.UiDesktopChatParent = nil
    o.UiDesktopHDealer = nil
    o.ControllerPlayer = nil
    o.UiDesktopHBanker = nil
    o.UiDesktopHStandPlayer = nil
    o.UiDesktopHMe = nil
    o.UiDesktopHRewardPot = nil
    o.UiDesktopHBase = nil--ViewDesktopHBase
    o.GCoDealer = nil-- GCom，锚点。庄家头像条中下
    o.GCoDesktopH = nil-- GCom，锚点。可能是屏幕左上角或中间
    o.GCoDesktopHPoolParent = nil-- GCom，锚点。屏幕左上角
    o.GCotips = nil-- 屏幕中间提示的Ui组件
    o.GComMsgTips = nil-- 私聊按钮右上角的小红点
    o.GTextMsgTips = nil
    o.FactoryName = nil
    o.DesktopHGameResult = nil
    o.UiDesktopHTongSha = nil
    o.UiDesktopHTongPei = nil
    o.UiDesktopHFlow = nil
    o.GBtnRepeat = nil
    o.GBtnSetCardType = nil
    o.CoShiShiCai = nil-- 时时彩
    o.GTextLotteryTicketTips = nil-- 时时彩
    o.GTextTm = nil-- 本地时间
    o.MapDesktopHBetPot = nil
    o.MapDesktopHChair = nil
    o.MapDesktopHBetOperate = nil
    o.MapDesktopHBaseFactory = nil-- 只有一个实例
    o.FTaskerShowGameResult = nil-- 做结算动画流程
    o.StateTm = 0
    o.Sound = nil-- 背景音乐
    o.IsTongSha = false-- 为了延后处理通杀通赔
    o.IsTongPei = false
    o.CheckTimeTime = 0-- 定时更新本地时间
    o.CanBet = false-- 是否可以下注
    o.NewFriendChatCount = 0
    o.GCoChairTitle = "CoChair"
    o.UiDesktopHPackageNameTitle = "DesktopH"
    o.UiDesktopHComDesktopHTitle = "CoDesktopH"
    o.UiDesktopHComDesktopHBetPotTitle = "CoDesktopHBetPot"
    o.UiDesktopHComDesktopHHelpTitle = "CoDesktopHHelp"
    o.UiDesktopHComDesktopHKaiRewardPotInfoTitle = "CoDesktopHKaiRewardPotInfo"
    o.UiDesktopHComDesktopHRewardPotPlayerInfoTitle = "CoDesktopHRewardPotPlayerInfo"
    o.UiDesktopHComDesktopHCardTypeTitle = "CoDesktopHCardType"
    o.BetAniX = 10
    o.ShowChatTm = 2
    o.TongShaPackName = "DesktopHTongSha"
    o.TongPeiPackName = "DesktopHTongPei"
    o.ShowTongShaTongPeiTime = 1
    o.MaxUiChipNum = 60
    o.Context = Context
    o.CasinosContext = CS.Casinos.CasinosContext.Instance
    o.TimerUpdate = nil
    return o
end

---------------------------------------
function ViewDesktopH:OnCreate()
    self.ViewMgr:BindEvListener("EvEntityDesktopHBetOperateTypeChange", self)
    self.ViewMgr:BindEvListener("EvEntityDesktopHCurrentBetOperateTypeChange", self)
    self.ViewMgr:BindEvListener("EvEntityDesktopHBet", self)
    self.ViewMgr:BindEvListener("EvEntityDesktopHUpdateBetPotBetInfo", self)
    self.ViewMgr:BindEvListener("EvEntityDesktopHBetFailed", self)
    self.ViewMgr:BindEvListener("EvEntityDesktopHChangeBankerPlayer", self)
    self.ViewMgr:BindEvListener("EvEntityDesktopHBankerPlayerGoldChange", self)
    self.ViewMgr:BindEvListener("EvEntityDesktopHChangeSeatPlayer", self)
    self.ViewMgr:BindEvListener("EvEntityDesktopHSeatPlayerGoldChanged", self)
    self.ViewMgr:BindEvListener("EvEntityGoldChanged", self)
    self.ViewMgr:BindEvListener("EvEntityRecvChatFromDesktopH", self)
    self.ViewMgr:BindEvListener("EvEntityDesktopHReadyState", self)
    self.ViewMgr:BindEvListener("EvEntityDesktopHBetState", self)
    self.ViewMgr:BindEvListener("EvEntityDesktopHGameEndState", self)
    self.ViewMgr:BindEvListener("EvEntityDesktopHGameRestState", self)
    self.ViewMgr:BindEvListener("EvEntityDesktopHBuyItem", self)
    self.ViewMgr:BindEvListener("EvEntityGetLotteryTicketDataSuccess", self)
    self.ViewMgr:BindEvListener("EvEntityLotteryTicketGameEndStateSimple", self)
    self.ViewMgr:BindEvListener("EvEntityLotteryTicketUpdateTm", self)
    self.ViewMgr:BindEvListener("EvEntityReceiveFriendSingleChat", self)
    self.ViewMgr:BindEvListener("EvEntityReceiveFriendChats", self)
    self.ViewMgr:BindEvListener("EvEntityUnreadChatsChanged", self)
    self.ViewMgr:BindEvListener("EvEntityRefreshLeftOnlineRewardTm", self)
    self.ViewMgr:BindEvListener("EvEntityCanGetOnlineReward", self)
    self.ViewMgr:BindEvListener("EvEntityCanGetTimingReward", self)
    self.ViewMgr:BindEvListener("EvViewClickShowReward", self)
    self.ViewMgr:BindEvListener("EvViewRequestGetTimingReward", self)
    self.ViewMgr:BindEvListener("EvViewOnGetOnLineReward", self)

    local controller_mgr = ControllerMgr:new(nil)
    self.ControllerPlayer = controller_mgr:GetController("Player")
    self.ControllerLotteryTicket = controller_mgr:GetController("LotteryTicket")
    self.ControllerDesktopH = controller_mgr:GetController("DesktopH")
    self.ControllerActor = controller_mgr:GetController("Actor")
    self.ControllerIM = controller_mgr:GetController("IM")
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.MapDesktopHBetPot = {}
    self.MapDesktopHChair = {}
    self.MapDesktopHBetOperate = {}
    self.MapDesktopHBaseFactory = {}
    --self.MapDesktopHBaseFactory[CasinosModule.GFlower.ToString()] = new UiDesktopHGFlowerFactory()
    --self.MapDesktopHBaseFactory[CasinosModule.ZhongFB.ToString()] = new UiDesktopHZhongFBFactory()
    --self.MapDesktopHBaseFactory[CasinosModule.NiuNiu.ToString()] = new UiDesktopHNiuNiuFactory()
    self.MapDesktopHBaseFactory["Texas"] = ViewDesktopHTexasFactory:new(nil)
    local btn_menu = self.ComUi:GetChild("BtnMenu").asButton
    btn_menu.onClick:Add(
            function()
                self:_onClickBtnMenu()
            end
    )
    local btn_history = self.ComUi:GetChild("BtnHistory").asButton
    btn_history.onClick:Add(
            function()
                self:_onClickBtnHistory()
            end
    )
    self.GBtnRepeat = self.ComUi:GetChild("BtnRepeat").asButton
    self.GBtnRepeat.onClick:Add(
            function()
                self:_onClickBtnRepeat()
            end
    )
    self.GBtnRepeat.enabled = false
    self.GBtnRepeat.visible = false
    local group_setcardtype = self.ComUi:GetChild("GroupSetCardType").asGroup
    group_setcardtype.visible = self.ControllerPlayer.IsGm
    self.GBtnSetCardType = self.ComUi:GetChild("BtnSetCardType").asButton
    self.GBtnSetCardType.onClick:Add(
            function()
                self:_onClickSetCardType()
            end
    )
    local co_shishicai = self.ComUi:GetChild("CoShiShiCai")
    if (co_shishicai ~= nil) then
        self.CoShiShiCai = co_shishicai.asCom
        self.CoShiShiCai.onClick:Add(
                function()
                    self:_onClickLotteryTicket()
                end
        )
        self.GTextLotteryTicketTips = self.CoShiShiCai:GetChild("LotteryTicketTips").asTextField
        self:_setLotteryTicketInfo(self.ControllerLotteryTicket.LotteryTicketState, self.ControllerLotteryTicket.BetStateTm)
    end
    self.GTextTm = self.ComUi:GetChild("Time").asTextField
    self:_checkTime()

    local co_getreward = self.ComUi:GetChild("ComBetLotteryBox")
    if (co_getreward ~= nil) then
        co_getreward.onClick:Add(
                function()
                    self:_onClickGetBetReward()
                end
        )
    end
    self.GCoDesktopHPoolParent = self.ComUi:GetChild("CoDesktopHPoolParent").asCom
    local desktoph_topparent = self.ComUi:GetChild("CoDesktopHTopParent").asCom
    local com_tongsha = CS.FairyGUI.UIPackage.CreateObject(self.TongShaPackName, self.TongShaPackName).asCom
    com_tongsha.width = self.ComUi.width
    com_tongsha.height = self.ComUi.height
    self.ViewMgr.LanMgr:parseComponent(com_tongsha)
    desktoph_topparent:AddChild(com_tongsha)
    self.UiDesktopHTongSha = UiDesktopHTongSha:new(nil, com_tongsha)
    local com_tongpei = CS.FairyGUI.UIPackage.CreateObject(self.TongPeiPackName, self.TongPeiPackName).asCom
    com_tongpei.width = self.ComUi.width
    com_tongpei.height = self.ComUi.height
    self.ViewMgr.LanMgr:parseComponent(com_tongpei)
    desktoph_topparent:AddChild(com_tongpei)
    self.UiDesktopHTongPei = UiDesktopHTongPei:new(nil, com_tongpei, self)
    local btn_chat = self.ComUi:GetChild("BtnChat").asButton
    btn_chat.onClick:Add(
            function()
                local ui_chat = self.ViewMgr:CreateView("Chat")
                ui_chat:init(_eUiChatType.DesktopH)
            end)

    self.UiDesktopChatParent = self.ViewMgr:CreateView("DesktopChatParent")
    local ui_shootingtext = self.ViewMgr:GetView("ShootingText")-- 弹幕Ui此处创建出来，它自己响应对应消息
    if (ui_shootingtext == nil) then
        ui_shootingtext = self.ViewMgr:CreateView("ShootingText")
        ui_shootingtext:init(true, false, true)
    end
    local btn_chatfriend_temp = self.ComUi:GetChild("BtnChatFriend")
    if (btn_chatfriend_temp ~= nil) then
        --local btn_chatfriend = btn_chatfriend_temp.asButton
        btn_chatfriend_temp.onClick:Add(
                function()
                    self:_onClickBtnChatFriend()
                end
        )
    end
    local btn_friend_temp = self.ComUi:GetChild("BtnFriend")
    if (btn_friend_temp ~= nil) then
        local btn_friend = btn_friend_temp.asButton
        btn_friend.onClick:Add(
                function()
                    self:_onClickBtnFriend()
                end
        )
    end
    local btn_shop_temp = self.ComUi:GetChild("BtnShop")
    if (btn_shop_temp ~= nil) then
        local btn_shop = btn_shop_temp.asButton
        btn_shop.onClick:Add(
                function()
                    self:_onClickBtnShop()
                end
        )
    end
    local com_msgTips_temp = self.ComUi:GetChild("ComMsgTips")
    if (com_msgTips_temp ~= nil) then
        self.GComMsgTips = com_msgTips_temp.asCom
        self.GTextMsgTips = self.GComMsgTips:GetChild("TextMsgTips").asTextField
        self.GComMsgTips.visible = false
    end

    self.ChipIconSolustion = self.ComUi:GetController("ChipIconSolustion")-- 为了兼容Nigeria的莱拉货币
    self.ChipIconSolustion.selectedIndex = self.Context.Cfg.ChipIconSolustion

    self.ComShadeReward = self.ComUi:GetChild("ComShadeReward").asCom-- 为了处理定时奖励和在线奖励显示和隐藏的消息
    self.ComShadeReward.visible = false
    self.ComShadeReward.onClick:Add(
            function()
                self.ComShadeReward.visible = false
                self.TransitionShowReward:PlayReverse()
            end
    )
    local com_reward = self.ComUi:GetChild("ComReward").asCom
    local co_onlinereward = com_reward:GetChild("ComOnlineReward").asCom
    self.ViewOnlineReward = ViewOnlineReward:new(nil, co_onlinereward, self.ViewMgr)
    self.CanGetOnLineReward = self.ControllerPlayer.OnlineReward:IfCanGetReward()
    self.ViewOnlineReward:setCanGetReward(self.CanGetOnLineReward)
    local co_timingreward = com_reward:GetChild("ComPushReward").asCom
    self.ViewTimingReward = ViewTimingReward:new(nil, co_timingreward, self.ViewMgr)
    self.CanGetTimingReward = self.ControllerPlayer.TimingReward:IfCanGetReward()
    self.ViewTimingReward:setCanGetReward(self.CanGetTimingReward)
    self.ComRewardTips = self.ComUi:GetChild("ComRewardTips").asCom
    self.TransitionNewReward = self.ComRewardTips:GetTransition("TransitionNewMsg")
    self:setNewReward()
    self.TransitionShowReward = self.ComUi:GetTransition("TransitionReward")

    self.UiDesktopHFlow = UiDesktopHFlow:new(self)

    self.TimerUpdate = self.CasinosContext.TimerShaft:RegisterTimer(100, self, self._timerUpdate)
end

---------------------------------------
function ViewDesktopH:OnDestroy()
    if (self.TimerUpdate ~= nil) then
        self.TimerUpdate:Close()
        self.TimerUpdate = nil
    end
    if self.UiDesktopHFlow ~= nil then
        self.UiDesktopHFlow:Close()
        self.UiDesktopHFlow = nil
    end
    self.ViewMgr:UnbindEvListener(self)
    self:_cancelTaskAndDestroyUiResult()
    if self.UiDesktopHBanker ~= nil then
        self.UiDesktopHBanker:Destroy()
        self.UiDesktopHBanker = nil
    end
    if self.UiDesktopHMe ~= nil then
        self.UiDesktopHMe:Destroy()
        self.UiDesktopHMe = nil
    end
    if self.UiDesktopHStandPlayer ~= nil then
        self.UiDesktopHStandPlayer:Destroy()
        self.UiDesktopHStandPlayer = nil
    end
    for k, v in pairs(self.MapDesktopHChair) do
        v:Destroy()
    end
    for k, v in pairs(self.MapDesktopHBetPot) do
        v:Destroy()
    end
    if self.UiDesktopHRewardPot ~= nil then
        self.UiDesktopHRewardPot:Destroy()
        self.UiDesktopHRewardPot = nil
    end
    if self.UiDesktopHDealer ~= nil then
        self.UiDesktopHDealer:ResetCard()
        self.UiDesktopHDealer:Destroy()
        self.UiDesktopHDealer = nil
    end
    if self.UiDesktopHGoldPool ~= nil then
        self.UiDesktopHGoldPool:Destroy()
        self.UiDesktopHGoldPool = nil
    end
    if self.UiDesktopHMe ~= nil then
        self.UiDesktopHMe:Destroy()
        self.UiDesktopHMe = nil
    end
    if (self.UiDesktopHTongSha ~= nil) then
        self.UiDesktopHTongSha:Reset()
        self.UiDesktopHTongSha = nil
    end
    if (self.UiDesktopHTongPei ~= nil) then
        self.UiDesktopHTongPei:Reset()
        self.UiDesktopHTongPei = nil
    end

    if (self.UiDesktopChatParent ~= nil) then
        self.ViewMgr:DestroyView(self.UiDesktopChatParent)
        self.UiDesktopChatParent = nil
    end

    local ui_shootingtext = self.ViewMgr:GetView("ShootingText")
    if (ui_shootingtext ~= nil) then
        self.ViewMgr:DestroyView(ui_shootingtext)
    end
end

---------------------------------------
function ViewDesktopH:OnHandleEv(ev)
    if (ev ~= nil) then
        if (ev.EventName == "EvEntityDesktopHBetOperateTypeChange") then
            self:_refreshBetOperate(ev.map_changeoperate)
        elseif (ev.EventName == "EvEntityDesktopHCurrentBetOperateTypeChange") then
            self:_refreshBetOperate()
        elseif (ev.EventName == "EvEntityDesktopHBet") then
            local betpot_index = ev.bet_potindex
            self.UiDesktopHMe:setBetSelfChipsToPot(betpot_index, ev.bet_golds)
            local bet_pot = self:getDesktopHBetPot(betpot_index)
            bet_pot:setBetSelfChipsToPot(ev.already_betgolds)
        elseif (ev.EventName == "EvEntityDesktopHUpdateBetPotBetInfo") then
            local map_betpot_betdeltainfo = ev.map_betpot_betdeltainfo
            if (map_betpot_betdeltainfo ~= nil) then
                for i, v in pairs(map_betpot_betdeltainfo) do
                    local bet_pot = self.MapDesktopHBetPot[i]
                    bet_pot:updateBetPotInfo(v)
                end
            end

            local current_betgolds = 0
            for i, v in pairs(self.MapDesktopHBetPot) do
                current_betgolds = current_betgolds + v.TotalBetPotGold
            end
            self.UiDesktopHBase:setPlayerCurrentBetInfo(current_betgolds)

            local map_standplayer_betdeltainfo = ev.map_standplayer_betdeltainfo
            if (map_standplayer_betdeltainfo ~= nil) then
                for i, v in pairs(map_standplayer_betdeltainfo) do
                    if (v > 0) then
                        self.UiDesktopHStandPlayer:betGolds(i, v)
                    end
                end
            end

            local list_seatplayer_betinfo = ev.list_seatplayer_betinfo
            if (list_seatplayer_betinfo ~= nil) then
                for i, v in pairs(list_seatplayer_betinfo) do
                    if (v.player_guid ~= self.ControllerPlayer.Guid) then
                        local self_chair = self:getDesktopHChairByGuid(v.player_guid)
                        if (self_chair ~= nil) then
                            local t_map_betinfo = v.map_betinfo
                            for i_m, v_m in pairs(t_map_betinfo) do
                                self_chair:betGolds(-1, i_m, v_m)
                            end
                        end
                    end
                end
            end
        elseif (ev.EventName == "EvEntityDesktopHBetFailed") then
            for i, v in pairs(ev.map_self_betgolds) do
                local bet_pot = self.MapDesktopHBetPot[i]
                if (bet_pot ~= nil) then
                    bet_pot:setBetSelfChipsToPot(v)
                end
            end
        elseif (ev.EventName == "EvEntityDesktopHChangeBankerPlayer") then
            self.UiDesktopHBanker:SetBankerInfo(ev.banker_player)
        elseif (ev.EventName == "EvEntityDesktopHBankerPlayerGoldChange") then
            self.UiDesktopHBanker:RefreshBankerInfo(ev.banker_player)
        elseif (ev.EventName == "EvEntityDesktopHChangeSeatPlayer") then
            local change_p = {}
            for i, v in pairs(ev.map_seat_player) do
                local chair = self.MapDesktopHChair[i]
                if (chair ~= nil) then
                    change_p[i] = v
                    chair:playerSeatDown(v)
                end
            end
            for i, v in pairs(self.MapDesktopHChair) do
                local c = change_p[i]
                if (c == nil) then
                    v:playerSeatDown(nil)
                end
            end
        elseif (ev.EventName == "EvEntityDesktopHSeatPlayerGoldChanged") then
            for i, v in pairs(self.MapDesktopHChair) do
                if (v.SeatPlayerInfo ~= nil) then
                    local golds = ev.map_seatplayer_golds[v.SeatPlayerInfo.PlayerInfoCommon.PlayerGuid]
                    if (golds ~= nil and golds > 0) then
                        v:updatePlayerGolds(golds)
                    end
                end
            end
        elseif (ev.EventName == "EvEntityGoldChanged") then
            self.UiDesktopHMe:setGoldChanged(ev.change_reason, ev.delta_gold, ev.user_data)
        elseif (ev.EventName == "EvEntityRecvChatFromDesktopH") then
            local chat_info = ev.chat_info
            local use_tanmu = true
            local show_chat = true
            if (self.Context.Cfg.ShootingTextShowVIPLimit ~= 0) then
                if (chat_info.sender_viplevel >= self.Context.Cfg.ShootingTextShowVIPLimit) then
                    show_chat = true
                else
                    show_chat = false
                end
                use_tanmu = show_chat
            else
                if (CS.UnityEngine.PlayerPrefs.HasKey(ViewChat.PlayTanMuKey)) then
                    success, use_tanmu = CS.System.Boolean.TryParse(CS.UnityEngine.PlayerPrefs.GetString(ViewChat.PlayTanMuKey))
                end
            end

            if (use_tanmu) then
                local ui_shootingtext = self.ViewMgr:GetView("ShootingText")
                if (ui_shootingtext == nil) then
                    ui_shootingtext = self.ViewMgr:CreateView("ShootingText")
                end
                ui_shootingtext:setShootingText(chat_info.sender_name, chat_info.chat_content, chat_info.sender_viplevel)
            end

            if (show_chat) then
                for i, v in pairs(self.MapDesktopHChair) do
                    v:playerChat(chat_info)
                end

                if (self.ControllerDesktopH.BankPlayer.PlayerInfoCommon.PlayerGuid == chat_info.sender_etguid) then
                    self.UiDesktopHBanker:setChatText(chat_info)
                elseif (self.ControllerDesktopH:isSeatPlayer(chat_info.sender_etguid) == false) then
                    self.UiDesktopHStandPlayer:setChatText(chat_info)
                end
            end
        elseif (ev.EventName == "EvEntityDesktopHReadyState") then
            self:_gameReady(ev.left_tm, ev.map_userdata)
        elseif (ev.EventName == "EvEntityDesktopHBetState") then
            self:_betState(ev.map_betrepeatinfo, ev.left_tm, ev.max_total_bet_gold)
        elseif (ev.EventName == "EvEntityDesktopHGameEndState") then
            self:_gameEnd(ev.game_result, ev.map_my_winlooseinfo)
        elseif (ev.EventName == "EvEntityDesktopHGameRestState") then
            self:_gameRest(ev.left_tm)
        elseif (ev.EventName == "EvEntityDesktopHBuyItem") then
            self:_playerBuyItem(ev.sender_guid, ev.map_items)
        elseif (ev.EventName == "EvEntityGetLotteryTicketDataSuccess") then
            self:_setLotteryTicketInfo(ev.lotteryticket_data.State, ev.lotteryticket_data.StateLeftTm)
        elseif (ev.EventName == "EvEntityLotteryTicketGameEndStateSimple") then
            if (self.GTextLotteryTicketTips ~= nil) then
                self.GTextLotteryTicketTips.text = self.ViewMgr.LanMgr:getLanValue("InTheSettlement")
            end
        elseif (ev.EventName == "EvEntityLotteryTicketUpdateTm") then
            self:RefreshLotteryTickLeftTm(ev.tm)
        elseif (ev.EventName == "EvEntityReceiveFriendSingleChat") then
            if (ev.chat_msg.sender_guid ~= self.ControllerPlayer.Guid) then
                local all_unreadchat_count = self.ControllerIM.IMChat:getAllNewChatCount()
                self:_setNewChatCount(all_unreadchat_count)
            end
        elseif (ev.EventName == "EvEntityReceiveFriendChats") then
            local all_unreadchat_count = self.ControllerIM.IMChat:getAllNewChatCount()
            self:_setNewChatCount(all_unreadchat_count)
        elseif (ev.EventName == "EvEntityUnreadChatsChanged") then
            local all_unreadchat_count = self.ControllerIM.IMChat:getAllNewChatCount()
            self:_setNewChatCount(all_unreadchat_count)
        elseif (ev.EventName == "EvEntityRefreshLeftOnlineRewardTm") then
            self.ViewOnlineReward:setLeftTm(ev.left_reward_second)
        elseif (ev.EventName == "EvEntityCanGetOnlineReward") then
            self.ViewOnlineReward:setCanGetReward(ev.can_getreward)
            self.CanGetOnLineReward = ev.can_getreward
            self:setNewReward()
        elseif (ev.EventName == "EvEntityCanGetTimingReward") then
            self.ViewTimingReward:setCanGetReward(ev.can_getreward)
            self.CanGetTimingReward = ev.can_getreward
            self:setNewReward()
        elseif (ev.EventName == "EvViewClickShowReward") then
            self.ComShadeReward.visible = true
            self.TransitionShowReward:Play()
        elseif (ev.EventName == "EvViewRequestGetTimingReward" or ev.EventName == "EvViewOnGetOnLineReward") then
            self.ComShadeReward.visible = false
            self.TransitionShowReward:PlayReverse()
        end
    end
end

---------------------------------------
function ViewDesktopH:InitDesktopH(desktoph_data, map_my_betinfo, map_my_winlooseinfo)
    self.FactoryName = desktoph_data.factory_name
    if (self.UiDesktopHBase == nil) then
        self.UiDesktopHDealer = UiDesktopHDealer:new(nil, self.FactoryName)
        self.UiDesktopHGoldPool = UiDesktopHGoldPool:new(nil)
        self.GCoDealer = self.ComUi:GetChild("CoDealer").asCom
        local co_desktop_parent = self.ComUi:GetChild("ComDesktopParent").asCom
        self.GCoDesktopH = CS.FairyGUI.UIPackage.CreateObject(self:getDesktopBasePackageName(), self.UiDesktopHComDesktopHTitle .. self.FactoryName).asCom
        self.ViewMgr.LanMgr:parseComponent(self.GCoDesktopH)
        self.GCoDesktopH.height = self.ComUi.height
        self.GCoDesktopH.width = self.ComUi.width
        local bg = self.GCoDesktopH:GetChild("bg")
        if (bg ~= nil) then
            ViewHelper:MakeUiBgFiteScreen(ViewMgr.STANDARD_WIDTH, ViewMgr.STANDARD_HEIGHT, self.ComUi.width, self.ComUi.height, bg.width, bg.height, bg, BgAttachMode.Center)
        end
        co_desktop_parent:AddChild(self.GCoDesktopH)
        self.GCotips = self.GCoDesktopH:GetChild("CoTips").asCom
        self.UiDesktopHBase = self.MapDesktopHBaseFactory[self.FactoryName]:CreateViewDesktopHBase(self)
        self.DesktopHGameResult = self.UiDesktopHBase:getGoldOperateList()
        local bg_music = self.UiDesktopHBase:getBgMusic()
        self.CasinosContext:Play(bg_music, CS.Casinos._eSoundLayer.Background)
        local btn_bankplayerlist = self.GCoDesktopH:GetChild("Lan_Btn_BeBank").asButton
        btn_bankplayerlist.onClick:Add(
                function()
                    self:_onClickBtnBeBank()
                end
        )
        local bank_icon = self.GCoDesktopH:GetChild("CoHeadIconBankPlayer").asCom
        local bank_nickname = self.GCoDesktopH:GetChild("NickNameBank").asTextField
        local bank_gold = self.GCoDesktopH:GetChild("GoldAcc").asTextField
        local bank_cardtype = self.GCoDesktopH:GetChild("LoaderBankCardType").asLoader
        local bank_cardtypebg = self.GCoDesktopH:GetChild("CardTypeBg")
        local ban_cardtypebg_image = nil
        if (bank_cardtypebg ~= nil) then
            ban_cardtypebg_image = bank_cardtypebg.asImage
        end
        local bank_cardparent = self.GCoDesktopH:GetChild("CoCardParent").asCom
        local bank_chatparent = self.GCoDesktopH:GetChild("CoChatParentBank").asCom
        self.UiDesktopHBanker = UiDesktopHBanker:new(nil, bank_icon, bank_nickname, bank_gold, bank_cardparent, bank_cardtype, ban_cardtypebg_image, bank_chatparent, self)
        local self_icon = self.ComUi:GetChild("CoHeadIconSelf").asCom
        local self_nickname = self.ComUi:GetChild("NickNameSelf").asTextField
        local self_gold = self.ComUi:GetChild("GoldSelf").asTextField
        self.UiDesktopHMe = UiDesktopHMe:new(nil, self_icon, self_nickname, self_gold, self)
        local stand_player = self.ComUi:GetChild("BtnStandPlayer").asButton
        local co_stand_chatparent = self.ComUi:GetChild("CoChatParentStandPlayer").asCom
        self.UiDesktopHStandPlayer = UiDesktopHStandPlayer:new(nil, stand_player, co_stand_chatparent, self)
        local reward_pot = self.GCoDesktopH:GetChild("CoRewardPot").asCom
        self.UiDesktopHRewardPot = UiDesktopHRewardPot:new(nil, reward_pot, self)

        local glist_betpot = self.GCoDesktopH:GetChild("ListBetPot").asList
        local last_pot_index = self.ControllerDesktopH.DesktopHBase:getMaxBetpotIndex()

        for i = 0, last_pot_index do
            local co_betpot = CS.FairyGUI.UIPackage.CreateObject(self:getDesktopBasePackageName(), self.UiDesktopHComDesktopHBetPotTitle .. i .. self.FactoryName).asCom
            local item_betpot = UiDesktopHBetPotItem:new(nil, self, glist_betpot, co_betpot)
            glist_betpot:AddChild(item_betpot.GCoBetPot)
            glist_betpot:SetBoundsChangedFlag()
            glist_betpot:EnsureBoundsCorrect()
            item_betpot:InitBetPot(i, last_pot_index == i)
            local bet_pot = UiDesktopHBetPot:new(nil, i, item_betpot, self)
            self.MapDesktopHBetPot[i] = bet_pot
        end

        for i, v in pairs(self.MapDesktopHBetPot) do
            v.UiDesktopHBetPotItem:UpdateBetPotCardParentPos()
        end

        local seat_count = self.UiDesktopHBase:getSeatCount()
        for i = 0, seat_count - 1 do
            local co_chair = self.GCoDesktopH:GetChild(self.GCoChairTitle .. i).asCom
            local chair = UiDesktopHChair:new(nil, self, co_chair, i, i >= seat_count / 2)
            self.MapDesktopHChair[i] = chair
        end

        local glist_betoperate = self.ComUi:GetChild("ListBetOperate").asList
        for i, v in pairs(self.UiDesktopHBase:getGoldOperateMap()) do
            local can_operate = self.ControllerDesktopH.MapCanOperateId[i]
            local is_current_operate = false
            if (i == self.ControllerDesktopH.CurrentTbBetOperateId) then
                is_current_operate = true
            end
            local item_reward = glist_betoperate:AddItemFromPool().asCom
            local bet_operate = UiDesktopHBetOperateItem:new(nil, item_reward, self)
            bet_operate:SetOperateInfo(i, v, can_operate, is_current_operate)
            self.MapDesktopHBetOperate[i] = bet_operate
        end

        self.UiDesktopHDealer:createDealerBase(self.FactoryName)
    end

    self.UiDesktopHDealer:ResetCard()
    self.UiDesktopHGoldPool:Reset()
    for i, v in pairs(self.MapDesktopHChair) do
        v:Reset()
    end
    for i, v in pairs(self.MapDesktopHBetPot) do
        v:ResetBetPot()
    end

    self.UiDesktopHBanker:Reset()
    self.UiDesktopHRewardPot:Reset()
    self.UiDesktopHStandPlayer:Reset()
    self.UiDesktopHMe:Reset()
    self.UiDesktopHTongPei:Reset()
    self.UiDesktopHTongSha:Reset()

    self.UiDesktopHBanker:SetBankerInfo(desktoph_data.banker_player)

    if (desktoph_data.map_seatplayer ~= nil) then
        local t_h_seat = desktoph_data.map_seatplayer
        for i, v in pairs(t_h_seat) do
            local chair = self.MapDesktopHChair[i]
            if (chair ~= nil) then
                chair:playerSeatDown(v)
            end
        end
    end
    self.UiDesktopHStandPlayer:initStandPlayer()
    self.UiDesktopHMe:initSelfInfo(true)

    self.UiDesktopHRewardPot:setRewardGolds1(desktoph_data.reward_pot)
    self.StateTm = desktoph_data.left_tm

    if (self.ControllerDesktopH.DesktopHState == _eDesktopHState.Bet) then
        self.CanBet = true
        local current_betgolds = 0
        if (desktoph_data.map_betpot_betinfo ~= nil) then
            local t_h_betpot_info = desktoph_data.map_betpot_betinfo
            for i, v in pairs(t_h_betpot_info) do
                local self_betgolds = 0
                if (map_my_betinfo ~= nil and #map_my_betinfo > 0) then
                    local info = map_my_betinfo[i]
                    if info ~= nil then
                        self_betgolds = info
                    end
                end
                local bet_pot = self.MapDesktopHBetPot[i]
                bet_pot:initBetPotInfo(v, self_betgolds)
                current_betgolds = current_betgolds + v
            end

            self.UiDesktopHBase:setPlayerCurrentBetInfo(current_betgolds)
        end
        self.UiDesktopHBase:updateGameState(self.ControllerDesktopH.DesktopHState,
                desktoph_data.left_tm,
                desktoph_data.max_total_bet_gold,
                nil, true)
    else
        self.CanBet = false
        if (self.ControllerDesktopH.DesktopHState == _eDesktopHState.GameEnd) then
            local t_h_r_betpot_info = desktoph_data.game_result.map_betpot_info
            for i, v in pairs(t_h_r_betpot_info) do
                local self_betgolds = 0
                if (map_my_betinfo ~= nil and #map_my_betinfo > 0) then
                    local info = map_my_betinfo[i]
                    if info ~= nil then
                        self_betgolds = info
                    end
                end
                local bet_pot = self.MapDesktopHBetPot[i]
                bet_pot:initBetPotInfo(v.bet_gold, self_betgolds)
            end

            self:_gameEnd(desktoph_data.game_result, map_my_winlooseinfo, true)
        else
            self.UiDesktopHBase:updateGameState(self.ControllerDesktopH.DesktopHState, desktoph_data.left_tm,
                    desktoph_data.max_total_bet_gold, nil, true)
        end
    end
    self:_setNewChatCount(self.ControllerIM.IMChat:getAllNewChatCount())
end

---------------------------------------
function ViewDesktopH:_timerUpdate(elapsed_tm)
    self.CheckTimeTime = self.CheckTimeTime + elapsed_tm
    if (self.CheckTimeTime >= 60) then
        self:_checkTime()
        self.CheckTimeTime = 0
    end

    if (self.UiDesktopHDealer ~= nil) then
        self.UiDesktopHDealer:Update(elapsed_tm)
    end

    if (self.StateTm > 0) then
        self.StateTm = self.StateTm - elapsed_tm
        if (self.StateTm > 0) then
            self.UiDesktopHBase:setCountDown(math.ceil(self.StateTm))
        else
            self.CanBet = false
            self.GCotips.visible = false
        end
    end

    for i, v in pairs(self.MapDesktopHChair) do
        v:Update(elapsed_tm)
    end

    self.UiDesktopHBanker:Update(elapsed_tm)
    self.UiDesktopHStandPlayer:Update(elapsed_tm)
end

---------------------------------------
function ViewDesktopH:GetMoveIntervalTm(count)
    local delay_t = UiDesktopHGold.MAX_CHIP_MOVE_TM / count
    if (delay_t > UiDesktopHGold.MAX_CHIP_MOVE_INTERVAL_TM) then
        delay_t = UiDesktopHGold.MAX_CHIP_MOVE_INTERVAL_TM
    end
    return delay_t
end

---------------------------------------
function ViewDesktopH:showCardEnd()
    if (self.IsTongSha) then
        self.UiDesktopHTongSha:ShowEffect(
                function()
                    self:_showTongShaTongPeiEnd()
                end)
    elseif (self.IsTongPei) then
        self.UiDesktopHTongPei:ShowEffect(
                function()
                    self:_showTongShaTongPeiEnd()
                end)
    else
        self:_showGameEndGoldAni(2.0, 3.2)
    end
end

---------------------------------------
function ViewDesktopH:dealCard(deal_cardcount, move_cardwidth_percent, map_userdata, auto_showcard)
    self.UiDesktopHDealer:dealCard(deal_cardcount, move_cardwidth_percent, map_userdata, auto_showcard)
end

---------------------------------------
function ViewDesktopH:dealCardAtPos(deal_cardcount, move_cardwidth_percent)
    self.UiDesktopHDealer:dealCardAtPos(deal_cardcount, move_cardwidth_percent)
end

---------------------------------------
function ViewDesktopH:ShowCard(is_onebyone, showcard_offset_tm)
    local is_one = false
    if (is_onebyone ~= nil) then
        is_one = is_onebyone
    end
    local show_card_tm = 0
    if (showcard_offset_tm ~= nil) then
        show_card_tm = showcard_offset_tm
    end
    self.UiDesktopHDealer:ShowCard(is_one, show_card_tm)
end

---------------------------------------
function ViewDesktopH:getDesktopHBetPot(index)
    local bet_pot = self.MapDesktopHBetPot[index]
    return bet_pot
end

---------------------------------------
function ViewDesktopH:getDesktopHChairByIndex(index)
    local chair = self.MapDesktopHChair[index]
    return chair
end

---------------------------------------
function ViewDesktopH:getDesktopHChairByGuid(guid)
    local chair = nil
    for i, v in pairs(self.MapDesktopHChair) do
        if (v.SeatPlayerInfo ~= nil) then
            if (v.SeatPlayerInfo.PlayerInfoCommon.PlayerGuid == guid) then
                chair = v
                break
            end
        end
    end
    return chair
end

---------------------------------------
function ViewDesktopH:getDesktopHChairAll()
    return self.MapDesktopHChair
end

---------------------------------------
function ViewDesktopH:getDesktopBasePackageName()
    return self.UiDesktopHPackageNameTitle .. self.FactoryName
end

---------------------------------------
function ViewDesktopH:createGold(golds, pos)
    local ui_gold = self.UiDesktopHGoldPool:getGoldH()
    ui_gold:setPostion(pos)
    return ui_gold
end

---------------------------------------
function ViewDesktopH:createGolds(list_total_gold, list_new_gold, gold_value, desktoph_betpot, create_count)
    local create_c = 6
    if (create_count ~= nil) then
        create_c = create_count
    end
    for i = 1, create_c do
        self:_createGold(list_total_gold, list_new_gold, desktoph_betpot, gold_value)
    end
end

---------------------------------------
function ViewDesktopH:_createGold(list_total_gold, list_new_gold, desktoph_betpot, gold_operate_value)
    local l = #list_total_gold
    if (l >= self.MaxUiChipNum) then
        local gold_first = table.remove(list_total_gold, 1)
        if (list_new_gold ~= nil) then
            LuaHelper:TableRemoveV(list_new_gold, gold_first)
        end
        gold_first:needDelayEnPool(1)
    end

    local pos = CS.Casinos.LuaHelper.GetVector2(0, 0)
    if (desktoph_betpot ~= nil) then
        pos = desktoph_betpot:getRandomChipPos()
    end
    local ui_gold = self:createGold(gold_operate_value, pos)
    table.insert(list_total_gold, ui_gold)
    if (list_new_gold ~= nil) then
        table.insert(list_new_gold, ui_gold)
    end
end

---------------------------------------
function ViewDesktopH:Bet(bet_index)
    if (self.CanBet == true) then
        local ev = self.ViewMgr:GetEv("EvDesktopHBet")
        if (ev == nil) then
            ev = EvDesktopHBet:new(nil)
        end
        ev.bet_betpot_index = bet_index
        self.ViewMgr:SendEv(ev)
    end
end

---------------------------------------
function ViewDesktopH:RefreshLotteryTickLeftTm(tm)
    if (self.GTextLotteryTicketTips == nil) then
        return
    end

    if (tm > 0) then
        self.GTextLotteryTicketTips.text = (math.ceil(tm) .. self.ViewMgr.LanMgr:getLanValue("S"))
    else
        self.GTextLotteryTicketTips.text = self.ViewMgr.LanMgr:getLanValue("InTheSettlement")
    end
end

---------------------------------------
function ViewDesktopH:_gameReady(left_tm, map_userdata)
    DesktopHGameResult = nil
    self:_cancelTaskAndDestroyUiResult()
    local ui_result = self.ViewMgr:GetView("DesktopHResult")
    if (ui_result ~= nil) then
        self.ViewMgr:DestroyView(ui_result)
    end
    self.CanBet = false
    self.StateTm = left_tm
    self.UiDesktopHBase:updateGameState(_eDesktopHState.Ready, left_tm, 0, map_userdata, false)
end

---------------------------------------
function ViewDesktopH:_betState(map_betrepeatinfo, left_tm, max_total_bet_gold)
    DesktopHGameResult = nil
    self.CanBet = true
    self.StateTm = left_tm
    local gold_sum = 0
    for i, v in pairs(map_betrepeatinfo) do
        gold_sum = gold_sum + v
    end
    self.GBtnRepeat.enabled = gold_sum > 0
    self.UiDesktopHMe:betState()
    self.UiDesktopHStandPlayer:betState()
    self.UiDesktopHBase:updateGameState(_eDesktopHState.Bet, left_tm, max_total_bet_gold, nil, false)
    self.UiDesktopHBase:setPlayerCurrentBetInfo(0)
end

---------------------------------------
function ViewDesktopH:_gameEnd(game_result, map_my_winlooseinfo, is_screenshot)
    self.DesktopHGameResult = game_result
    self:_cancelTaskAndDestroyUiResult()
    self.CanBet = false
    self.StateTm = game_result.left_tm
    self.UiDesktopHBanker:SetBankerCards(game_result.bankerpot_info.list_card)
    local t_map_pumping = game_result.rewardpot_info.map_pumping_gold
    self.UiDesktopHRewardPot:SetRewardGolds(t_map_pumping, game_result.rewardpot_info.gold_after)

    local map_win_rewardpot_gold = game_result.rewardpot_info.map_win_rewardpot_gold
    self.IsTongSha = true
    self.IsTongPei = true
    if (game_result.map_betpot_info ~= nil) then
        local t_mapbetpot_info = game_result.map_betpot_info
        for i, v in pairs(t_mapbetpot_info) do
            if (v ~= nil) then
                if (v.is_win) then
                    self.IsTongSha = false
                else
                    self.IsTongPei = false
                end

                local win_rewardpot_gold = 0
                if (map_win_rewardpot_gold ~= nil and #map_win_rewardpot_gold > 0) then
                    win_rewardpot_gold = map_win_rewardpot_gold[i]
                end
                self.MapDesktopHBetPot[i]:SetGameEndResult(v, win_rewardpot_gold)
            end
        end
    end

    if (game_result.map_seatplayer_winloose_info ~= nil) then
        local t_map_seatplayer_winloose_info = game_result.map_seatplayer_winloose_info
        for i, v in pairs(t_map_seatplayer_winloose_info) do
            if (v ~= nil) then
                local chair = self:getDesktopHChairByGuid(i)
                if (chair ~= nil) then
                    for i_v, v_v in pairs(v) do
                        chair:setSeatPlayerResultInfo(v_v.betpot_index, v_v)
                    end
                end
            end
        end
    end

    if (map_my_winlooseinfo ~= nil) then
        for i, v in pairs(map_my_winlooseinfo) do
            if (v == nil or v.bet_gold == 0) then
            else
                self.UiDesktopHMe:setPlayerSelfResultInfo(i, v)
            end
        end
    end

    if (game_result.map_standplayer_gold ~= nil) then
        local t_map_standplayer_gold = game_result.map_standplayer_gold
        for i, v in pairs(t_map_standplayer_gold) do
            self.UiDesktopHStandPlayer:setOtherStandPlayerResultInfo(i, v)
        end
    end

    local bankplayer_winlooseinfo = game_result.bankerpot_info
    local bank_win_rewardpot_gold = 0
    if (map_win_rewardpot_gold ~= nil and #map_win_rewardpot_gold > 0) then
        bank_win_rewardpot_gold = map_win_rewardpot_gold[255]
    end
    self.UiDesktopHBanker:winRewardPotGolds(bank_win_rewardpot_gold)

    if (self.ControllerDesktopH.IsBankPlayer) then
        local player_winlooseinfo = BDesktopHNotifyGameEndBetPotPlayerWinLooseInfo:new(nil)
        local r = game_result.bankerpot_info.stack_after -
                game_result.bankerpot_info.stack_before - game_result.bankerpot_info.win_rewardpot_gold
        player_winlooseinfo.is_win = r > 0
        player_winlooseinfo.player_guid = self.ControllerPlayer.Guid
        player_winlooseinfo.winloose_gold = math.abs(r)
        player_winlooseinfo.win_rewardpot_gold = bankplayer_winlooseinfo.win_rewardpot_gold
        self.UiDesktopHMe:setPlayerSelfResultInfo(255, player_winlooseinfo)
    end

    self.UiDesktopHBase:updateGameState(_eDesktopHState.GameEnd, game_result.left_tm, 0, nil, is_screenshot)
end

---------------------------------------
function ViewDesktopH:_gameRest(left_tm)
    DesktopHGameResult = nil
    self:_cancelTaskAndDestroyUiResult()
    self.CanBet = false
    self.UiDesktopHDealer:ResetCard()
    self.UiDesktopHGoldPool:Reset()
    self.StateTm = left_tm
    for i, v in pairs(self.MapDesktopHBetPot) do
        v:ResetBetPot()
    end
    for i, v in pairs(self.MapDesktopHChair) do
        v:Reset()
    end

    self.UiDesktopHStandPlayer:Reset()
    self.UiDesktopHMe:Reset()
    self.UiDesktopHBanker:Reset()
    self.UiDesktopHRewardPot:Reset()
    self.UiDesktopHTongPei:Reset()
    self.UiDesktopHTongSha:Reset()
    self.UiDesktopHBase:updateGameState(_eDesktopHState.Rest, left_tm, 0, nil, false)
end

---------------------------------------
function ViewDesktopH:_cancelTaskAndDestroyUiResult()
    if (self.FTaskerShowGameResult ~= nil) then
        self.FTaskerShowGameResult:cancelTask()
        self.FTaskerShowGameResult = nil
    end
end

---------------------------------------
function ViewDesktopH:_showTongShaTongPeiEnd()
    local tm = 2.0
    local give_player_tm = 3.2
    if (self.IsTongPei == true) then
        tm = 0.5
        give_player_tm = 1.7
    end
    self:_showGameEndGoldAni(tm, give_player_tm)
end

---------------------------------------
function ViewDesktopH:_showGameEndGoldAni(betpot_show_win_ani_tm, give_winplayer_gold_ani_tm)
    for i, v in pairs(self.MapDesktopHBetPot) do
        v:showGameEndGoldAni(betpot_show_win_ani_tm, give_winplayer_gold_ani_tm)
    end

    self.UiDesktopHBanker:showGameEndGoldAni()

    local time = 4.2
    if (self.IsTongSha) then
        time = 2.2
    elseif (self.IsTongPei) then
        time = self.UiDesktopHBase:getTongPeiWhenAllShowEndShowGameResultTm()
    end

    local t = CS.Casinos.FTMgr.Instance:startTask(time)
    self.FTaskerShowGameResult = CS.Casinos.FTMgr.Instance:whenAll(nil,
            function(map_param)
                self:_showGameResult(map_param)
            end,
            t)
end

---------------------------------------
function ViewDesktopH:_showGameResult(map_param)
    self.UiDesktopHMe:showGameResult()
    self.FTaskerShowGameResult = nil
end

---------------------------------------
function ViewDesktopH:_playerBuyItem(sender_guid, map_items)
    if (map_items == nil) then
        return
    end

    for i, v in pairs(map_items) do
        local item = Item:new(nil, self.ViewMgr.TbDataMgr, v)

        if (item.UnitLink.UnitType == "GiftTmp") then
            local seat_target = nil
            for chair_i, chair_v in pairs(self.MapDesktopHChair) do
                if (chair_v.Value == nil or chair_v.Value.SeatPlayerInfo == nil) then
                else
                    if (chair.Value.SeatPlayerInfo.PlayerInfoCommon.PlayerGuid == i) then
                        seat_target = chair_v.Value
                        break
                    end

                    if (seat_target ~= nil) then
                        seat_target:sendGift(item)
                    else
                        if (self.UiDesktopHBanker ~= nil and
                                self.UiDesktopHBanker.BankPlayerDataDesktopH.PlayerInfoCommon.PlayerGuid == i) then
                            self.UiDesktopHBanker:sendGift(item)
                        end
                    end
                end
            end
        elseif (item.UnitLink.UnitType == "MagicExpression") then
            if (i == self.UiDesktopHBanker.BankPlayerDataDesktopH.PlayerInfoCommon.PlayerGuid) then
                self.UiDesktopHBanker:SendMagicExpression(sender_guid, item.TbDataItem.Id)
            else
                local chair = self:getDesktopHChairByGuid(i)
                if (chair ~= nil) then
                    chair:SendMagicExpression(sender_guid, item.TbDataItem.Id)
                end
            end
        end
    end
end

---------------------------------------
function ViewDesktopH:_refreshBetOperate(map_changeoperate)
    for i, v in pairs(self.MapDesktopHBetOperate) do
        v:SetIsCurrentOperate(false)
        if (map_changeoperate ~= nil) then
            v:SetCanOperate(false)
        end
    end

    if (map_changeoperate ~= nil) then
        for i, v in pairs(map_changeoperate) do
            local item_operate = self.MapDesktopHBetOperate[i]
            if (item_operate ~= nil) then
                item_operate:SetCanOperate(v)
            end
        end
    end

    local current_operate = self.MapDesktopHBetOperate[self.ControllerDesktopH.CurrentTbBetOperateId]
    if (current_operate ~= nil) then
        current_operate:SetIsCurrentOperate(true)
    end
end

---------------------------------------
function ViewDesktopH:_setLotteryTicketInfo(state, left_tm)
    local tips = ""
    if (state == LotteryTicketStateEnum.Bet) then
        tips = (math.ceil(left_tm) .. self.ViewMgr.LanMgr:getLanValue("S"))
    else
        tips = self.ViewMgr.LanMgr:getLanValue("InTheSettlement")
    end
    self.GTextLotteryTicketTips.text = tips
end

---------------------------------------
function ViewDesktopH:_onClickBtnMenu()
    local menu = self.ViewMgr:CreateView("DesktopHMenu")
    menu:showMenu(self.CanGetOnLineReward or self.CanGetTimingReward)
end

---------------------------------------
function ViewDesktopH:_onClickBtnBeBank()
    local ui_bebanklist = self.ViewMgr:CreateView("DesktopHBankList")
    ui_bebanklist:initBeBankPlayer(self, self.ControllerDesktopH.ListBeBankPlayer,
            self.ControllerDesktopH.BankPlayer, self.ControllerDesktopH.IsBankPlayer)
end

---------------------------------------
function ViewDesktopH:_onClickBtnHistory()
    local ui_history = self.ViewMgr:CreateView("DesktopHHistory")
    ui_history:setHistory(self.ControllerDesktopH.MapBetPotWinlooseRecord)
end

---------------------------------------
function ViewDesktopH:_onClickBtnRepeat()
    local ev = self.ViewMgr:GetEv("EvDesktopHRepeatBet")
    if (ev == nil) then
        ev = EvDesktopHRepeatBet:new(nil)
    end
    self.ViewMgr:SendEv(ev)
    self.GBtnRepeat.enabled = false
end

---------------------------------------
function ViewDesktopH:_onClickSetCardType()
    self.ViewMgr:CreateView("DesktopHSetCardType")
end

---------------------------------------
function ViewDesktopH:_onClickLotteryTicket()
    local ev = self.ViewMgr:GetEv("EvEntityRequestGetLotteryTicketData")
    if (ev == nil) then
        ev = EvEntityRequestGetLotteryTicketData:new(nil)
    end
    self.ViewMgr:SendEv(ev)

    self.ViewMgr:CreateView("LotteryTicket")
end

---------------------------------------
function ViewDesktopH:_onClickGetBetReward()
    self.ViewMgr:CreateView("DesktopHBetReward")
    local ev = self.ViewMgr:GetEv("EvDesktopHInitBetReward")
    if (ev == nil) then
        ev = EvDesktopHInitBetReward:new(nil)
    end
    ev.factory_name = self.FactoryName
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ViewDesktopH:_checkTime()
    local format_str = ""
    local now = CS.System.DateTime.Now
    local now_m = now.Minute
    local now_h = now.Hour
    if (string.len(tostring(now_m)) < 2) then
        format_str = "0" .. now_m
    else
        format_str = now_m
    end
    self.GTextTm.text = now_h .. ":" .. format_str
end

---------------------------------------
function ViewDesktopH:_onClickBtnChatFriend()
    local ev = self.ViewMgr:GetEv("EvUiClickChatmsg")
    if (ev == nil) then
        ev = EvUiClickChatmsg:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ViewDesktopH:_onClickBtnShop()
    self.ViewMgr:CreateView("Shop")
end

---------------------------------------
function ViewDesktopH:_onClickBtnFriend()
    local ev = self.ViewMgr:GetEv("EvUiClickFriend")
    if (ev == nil) then
        ev = EvUiClickFriend:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ViewDesktopH:_haveNewChatOrMailRecord()
    if (self.GComMsgTips ~= nil) then
        if (self.NewFriendChatCount > 0) then
            ViewHelper:SetGObjectVisible(true, self.GComMsgTips)
            self.GTextMsgTips.text = tostring(self.NewFriendChatCount)
            self.CasinosContext:Play("NewMessage", CS.Casinos._eSoundLayer.LayerReplace)
        else
            ViewHelper:SetGObjectVisible(false, self.GComMsgTips)
        end
    end
end

---------------------------------------
function ViewDesktopH:_setNewChatCount(chat_count)
    self.NewFriendChatCount = chat_count
    self:_haveNewChatOrMailRecord()
end

---------------------------------------
function ViewDesktopH:setNewReward()
    local have_newreward = false
    if (self.CanGetOnLineReward or self.CanGetTimingReward) then
        have_newreward = true
    end

    if (have_newreward == false) then
        ViewHelper:SetGObjectVisible(false, self.ComRewardTips)
    else
        ViewHelper:SetGObjectVisible(true, self.ComRewardTips)
        if (self.TransitionNewReward.playing == false) then
            self.TransitionNewReward:Play()
        end
    end
end

---------------------------------------
ViewDesktopHFactory = ViewFactory:new()

---------------------------------------
function ViewDesktopHFactory:new(o, ui_package_name, ui_component_name, ui_layer, is_single, fit_screen)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.PackageName = ui_package_name
    self.ComponentName = ui_component_name
    self.UILayer = ui_layer
    self.IsSingle = is_single
    self.FitScreen = fit_screen
    return o
end

---------------------------------------
function ViewDesktopHFactory:CreateView()
    local view = ViewDesktopH:new(nil)
    return view
end