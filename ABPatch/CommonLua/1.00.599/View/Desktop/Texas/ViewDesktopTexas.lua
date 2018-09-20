ViewDesktopTexas = ViewBase:new()

function ViewDesktopTexas:new(o)
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
    o.UiChipMgr = nil
    o.DesktopBase = nil
    o.UiDesktopChatParent = nil
    o.MoivePKCard = nil
    o.Desktop = nil
    o.UiPot = nil
    o.ActionWaitingTime = nil
    o.GTextLotteryTicketTips = nil
    o.DealerEx = nil
    o.GBtnShop = nil
    o.GBtnMsg = nil
    o.GBtnNotice = nil
    --o.GBtnDesktopHints = nil
    o.GBtnFriend = nil
    o.GBtnLockChat = nil
    o.GBtnChat = nil
    o.GBtnMenu = nil
    o.GLoaderNetwork = nil
    o.GLoaderDealerGirl = nil
    o.GTextTm = nil
    o.GTextMsgNum = nil
    o.GGroupCard = nil
    --o.GGroupCardTypeTips = nil
    --o.GTextCardTypeTips = nil
    --o.GGroupActionTips = nil
    --o.GTextActionTips = nil
    o.GComMsgTips = nil
    o.GTextMsgTips = nil
    o.GComCommunityCard1 = nil
    o.GComCommunityCard2 = nil
    o.GComCommunityCard3 = nil
    o.GComCommunityCard4 = nil
    o.GComCommunityCard5 = nil
    o.GComDealer = nil
    o.TransitionNewMsg = nil
    o.ItemChatDesktop = nil
    o.MapAllUiChairInfo = nil
    o.MapAllValidPlayerSeat = nil
    o.MapValidNoPlayerSeat = nil
    o.ListSelfAndCommonCard = nil
    o.ListWinnerPlayerInfo = nil
    o.ListAllPlayer = nil
    o.NewFriendChatCount = 0
    o.ClearDesktopTm = 0
    o.CheckTimeTime = 0
    o.ViewMTTTexas = nil
    self.SeatAndInviteTitle = "ComSeatAndInvite"
    self.SeatPlayerParentTitle = "ComSeatPlayerParent"
    self.ChairTitle = "ComSeat"
    self.PokerGirlDesk = "DeskGirl"

    return o
end

function ViewDesktopTexas:onCreate()
    self.ViewMgr:bindEvListener("EvEntityReceiveFriendSingleChat", self)
    self.ViewMgr:bindEvListener("EvEntityReceiveFriendChats", self)
    self.ViewMgr:bindEvListener("EvEntityUnreadChatsChanged", self)
    self.ViewMgr:bindEvListener("EvEntityMailListInit", self)
    self.ViewMgr:bindEvListener("EvEntityMailAdd", self)
    self.ViewMgr:bindEvListener("EvEntityMailDelete", self)
    self.ViewMgr:bindEvListener("EvEntityMailUpdate", self)
    self.ViewMgr:bindEvListener("EvEntityRecvChatFromDesktop", self)
    self.ViewMgr:bindEvListener("EvEntityDesktopSnapshotNotify", self)
    self.ViewMgr:bindEvListener("EvEntityDesktopIdleNotify", self)
    self.ViewMgr:bindEvListener("EvEntityDesktopPreFlopNotify", self)
    self.ViewMgr:bindEvListener("EvEntityDesktopFlopNotify", self)
    self.ViewMgr:bindEvListener("EvEntityDesktopTurnNotify", self)
    self.ViewMgr:bindEvListener("EvEntityDesktopRiverNotify", self)
    self.ViewMgr:bindEvListener("EvEntityDesktopShowdownNotify", self)
    self.ViewMgr:bindEvListener("EvEntityDesktopGameEndNotifyTexas", self)
    self.ViewMgr:bindEvListener("EvEntityGetLotteryTicketDataSuccess", self)
    self.ViewMgr:bindEvListener("EvEntityLotteryTicketGameEndStateSimple", self)
    self.ViewMgr:bindEvListener("EvEntityLotteryTicketUpdateTm", self)
    self.ViewMgr:bindEvListener("EvUiPotMainChanged", self)
    self.ViewMgr:bindEvListener("EvEntityMTTPlayerRebuyOrAddonRefresh", self)
    self.ViewMgr:bindEvListener("EvEntitySelfIsOB", self)
    self.ViewMgr:bindEvListener("EvEntityMTTUpdateRealtimeInfo", self)
    self.ViewMgr:bindEvListener("EvEntityMTTUpdateRaiseBlindTm", self)
    self.ViewMgr:bindEvListener("EvMTTPauseChanged", self)
    self.ViewMgr:bindEvListener("EvEntityMatchGameOver", self)
    self.ViewMgr:bindEvListener("EvEntityRefreshLeftOnlineRewardTm", self)
    self.ViewMgr:bindEvListener("EvEntityCanGetOnlineReward", self)
    self.ViewMgr:bindEvListener("EvEntityCanGetTimingReward", self)
    self.ViewMgr:bindEvListener("EvClickShowReward", self)
    self.ViewMgr:bindEvListener("EvRequestGetTimingReward", self)
    self.ViewMgr:bindEvListener("EvOnGetOnLineReward", self)

    self.ControllerLotteryTicket = self.ViewMgr.ControllerMgr:GetController("LotteryTicket")
    self.ControllerIM = self.ViewMgr.ControllerMgr:GetController("IM")
    self.ControllerDesktop = self.ViewMgr.ControllerMgr:GetController("Desk")
    self.ControllerPlayer = self.ViewMgr.ControllerMgr:GetController("Player")
    self.UiChipMgr = UiChipMgrEx:new(nil, false)
    self.UiDesktopChatParent = self.ViewMgr:createView("DesktopChatParent")
    local ui_shootingtext = self.ViewMgr:getView("ShootingText")
    if (ui_shootingtext == nil)
    then
        ui_shootingtext = self.ViewMgr:createView("ShootingText")
        ui_shootingtext:init(true, false, false)
    end

    self.GBtnMsg = self.ComUi:GetChild("BtnMsg").asButton
    self.GBtnMsg.onClick:Add(
            function()
                self:_onClickFriendChat()
            end
    )
    self.GBtnNotice = self.ComUi:GetChild("BtnNotice").asButton
    self.GBtnNotice.onClick:Add(
            function()
                self:_onClickNotice()
            end
    )
    --self.GBtnDesktopHints = self.ComUi:GetChild("BtnDesktopHints").asButton
    --self.GBtnDesktopHints.onClick:Add(
    --        function()
    --            self:_onClickHelp()
    --        end
    --)
    self.GBtnFriend = self.ComUi:GetChild("BtnFriend").asButton
    self.GBtnFriend.onClick:Add(
            function()
                self:_onClickFriend()
            end
    )
    self.GBtnLockChat = self.ComUi:GetChild("BtnLockChat").asButton
    self.GBtnLockChat.onClick:Add(
            function()
                self:_onClickLockChat()
            end
    )
    self.GBtnChat = self.ComUi:GetChild("BtnChat").asButton
    self.GBtnChat.onClick:Add(
            function()
                self:_onClickDesktopChat()
            end
    )

    self.GLoaderNetwork = self.ComUi:GetChild("LoaderNetWork").asLoader
    self.GLoaderDealerGirl = self.ComUi:GetChild("LoaderDealerGirl").asLoader
    self.GTextTm = self.ComUi:GetChild("TextTime").asTextField
    local com_lottryticket = self.ComUi:GetChild("ComLotteryTicket")
    if (com_lottryticket ~= nil)
    then
        local com_lottryticket1 = com_lottryticket.asCom
        com_lottryticket.onClick:Add(
                function()
                    self:_onClickBtnLotteryTicket()
                end
        )
        self.GTextLotteryTicketTips = com_lottryticket1:GetChild("LotteryTicketTips").asTextField
        self:_setLotteryTicketInfo(self.ControllerLotteryTicket.LotteryTicketState, self.ControllerLotteryTicket.BetStateTm)
    end

    self.GGroupCard = self.ComUi:GetChild("GroupCard").asGroup
    --self.GGroupCardTypeTips = self.ComUi:GetChild("GroupCardTypeTips").asGroup
    --self.GTextCardTypeTips = self.ComUi:GetChild("TextCardTypeTips").asTextField
    --self.GGroupActionTips = self.ComUi:GetChild("GroupActionTips").asGroup
    --self.GTextActionTips = self.ComUi:GetChild("TextActionTips").asTextField
    self.TextDesktopDescribe = self.ComUi:GetChild("TextDesktopDescribe").asTextField
    self.GComMsgTips = self.ComUi:GetChild("ComMsgTips").asCom
    self.GTextMsgTips = self.GComMsgTips:GetChild("TextMsgTips").asTextField
    self.GComCommunityCard1 = self.ComUi:GetChild("ComCard1").asCom
    self.GComCommunityCard2 = self.ComUi:GetChild("ComCard2").asCom
    self.GComCommunityCard3 = self.ComUi:GetChild("ComCard3").asCom
    self.GComCommunityCard4 = self.ComUi:GetChild("ComCard4").asCom
    self.GComCommunityCard5 = self.ComUi:GetChild("ComCard5").asCom
    self.GComDealer = self.ComUi:GetChild("ComDealer").asCom
    self.DealerEx = DealerEx:new(nil, self, self, self.GComCommunityCard1,
            self.GComCommunityCard2, self.GComCommunityCard3, self.GComCommunityCard4,
            self.GComCommunityCard5)
    self.TransitionNewMsg = self.ComUi:GetTransition("TransitionNewMsg")
    self.MapAllUiChairInfo = {}
    self.MapAllValidPlayerSeat = {}
    self.MapValidNoPlayerSeat = {}
    for i = 0, 8 do
        local chair = self.ComUi:GetChild(self.ChairTitle .. i).asCom
        local seat_invite = self.ComUi:GetChild(self.SeatAndInviteTitle .. i).asCom
        local seat_player_parent = self.ComUi:GetChild(self.SeatPlayerParentTitle .. i).asCom
        local image_sit = seat_invite:GetChild("ImagePlayerSit").asImage
        local image_invite = seat_invite:GetChild("ImagePlayerInvite").asImage
        local chair_info = _tChairInfo:new(nil, chair, seat_invite, seat_player_parent, image_sit, image_invite, i)
        self.MapAllUiChairInfo[i] = chair_info
    end
    self.ListSelfAndCommonCard = CS.Casinos.LuaHelper.GetNewCardList()
    self.ListWinnerPlayerInfo = {}
    self.ListAllPlayer = {}
    self.ComUi.onClick:Add(
            function()
                local ev = self.ViewMgr:getEv("EvUiClickDesktop")
                if (ev == nil)
                then
                    ev = EvUiClickDesktop:new(nil)
                end
                self.ViewMgr:sendEv(ev)
            end
    )
    self.ComWaitingBegine = self.ComUi:GetChild("ComWaitingBegine").asCom
    self.ComMeWin = self.ComUi:GetChild("ComMeWin").asCom
    self.TransitionMeWin = self.ComMeWin:GetTransition("t0")
    local p_helper = ParticleHelper:new(nil)
    local parent_selfwin = self.ComMeWin:GetChild("ParentSelfWin").asGraph
    local particle2 = p_helper:GetParticel("selfwin.ab")
    local p_2 = CS.UnityEngine.Object.Instantiate(particle2:LoadAsset("SelfWin"))
    parent_selfwin:SetNativeObject(CS.FairyGUI.GoWrapper(p_2))

    --local bg = self.ComUi:GetChild("Bg")
    --if (bg ~= nil)
    --then
    --    ViewHelper:makeUiBgFiteScreen(bg, self.ComUi.width, self.ComUi.height, bg.width, bg.height)
    --end
    local btn_chatfriend_temp = self.ComUi:GetChild("BtnChatFriend")
    if (btn_chatfriend_temp ~= nil)
    then
        local btn_chatfriend = btn_chatfriend_temp.asButton
        btn_chatfriend_temp.onClick:Add(
                function()
                    self:_onClickBtnChatFriend()
                end
        )
    end

    self.MapViewDesktopTypeBaseFac = {}
    self:regViewDesktopTypeBaseFactory(ViewTexasClassicTypeFactory:new(nil))
    self:regViewDesktopTypeBaseFactory(ViewTexasMTTTypeFactory:new(nil))
    self.UiPot = ViewPotTexasPoker:new(nil, self)

    self.ComShadeReward = self.ComUi:GetChild("ComShadeReward").asCom
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
    self.CanGetOnLineReward = self.ControllerPlayer.OnLineReward:IfCanGetReward()
    self.ViewOnlineReward:setCanGetReward(self.CanGetOnLineReward)
    local co_timingreward = com_reward:GetChild("ComPushReward").asCom
    self.ViewTimingReward = ViewTimingReward:new(nil, co_timingreward, self.ViewMgr)
    self.CanGetTimingReward = self.ControllerPlayer.TimingReward:IfCanGetReward()
    self.ViewTimingReward:setCanGetReward(self.CanGetTimingReward)
    self.ComRewardTips = self.ComUi:GetChild("ComRewardTips").asCom
    self.TransitionNewReward = self.ComRewardTips:GetTransition("TransitionNewMsg")
    self:setNewReward()
    self.TransitionShowReward = self.ComUi:GetTransition("TransitionReward")
end

function ViewDesktopTexas:onDestroy()
    self.ViewMgr:unbindEvListener(self)
    if (self.UiChipMgr ~= nil)
    then
        self.UiChipMgr:destroy()
    end

    if (self.UiDesktopChatParent ~= nil)
    then
        self.ViewMgr:destroyView(self.UiDesktopChatParent)
    end

    local ui_shootingtext = self.ViewMgr:getView("ShootingText")
    if (ui_shootingtext ~= nil)
    then
        self.ViewMgr:destroyView(ui_shootingtext)
    end

    if (self.DealerEx ~= nil)
    then
        self.DealerEx:destroy()
        self.DealerEx = nil
    end
end

function ViewDesktopTexas:onUpdate(elapsed_tm)
    if (CS.Casinos.CasinosContext.Instance.Pause)
    then
        return
    end

    if (self.UiChipMgr ~= nil)
    then
        self.UiChipMgr:update(elapsed_tm)
    end
    self.CheckTimeTime = self.CheckTimeTime + elapsed_tm
    if (self.CheckTimeTime >= 60)
    then
        self:_showCurrentLocalTm()
        self.CheckTimeTime = 0
    end

    if (self.ClearDesktopTm > 0)
    then
        self.ClearDesktopTm = self.ClearDesktopTm - elapsed_tm
        if (self.ClearDesktopTm <= 0)
        then
            --self:resetComminityShow()
            self.DealerEx:resetCommonCardType(
                    function()
                        if self.Desktop.DesktopState == TexasDesktopState.GameEnd then
                            self.UiPot:resetPot()
                            ViewHelper:setGObjectVisible(true, self.ComWaitingBegine)
                        end
                    end)
        end
    end

    if (self.DealerEx ~= nil)
    then
        self.DealerEx:update(elapsed_tm)
    end

    if (self.ItemChatDesktop ~= nil)
    then
        self.ItemChatDesktop:update(elapsed_tm)
    end
end

function ViewDesktopTexas:onHandleEv(ev)
    if self.ViewDesktopTypeBase ~= nil then
        self.ViewDesktopTypeBase:onHandleEv(ev)
    end

    if (ev ~= nil)
    then
        if (ev.EventName == "EvEntityReceiveFriendSingleChat")
        then
            if (ev.chat_msg.sender_guid ~= self.ControllerIM.Guid)
            then
                local all_unreadchat_count = self.ControllerIM.IMChat:getAllNewChatCount()
                self:_setNewChatCount(all_unreadchat_count)
            end
        elseif (ev.EventName == "EvEntityReceiveFriendChats")
        then
            local all_unreadchat_count = self.ControllerIM.IMChat:getAllNewChatCount()
            self:_setNewChatCount(all_unreadchat_count)
        elseif (ev.EventName == "EvEntityUnreadChatsChanged")
        then
            local all_unreadchat_count = self.ControllerIM.IMChat:getAllNewChatCount()
            self:_setNewChatCount(all_unreadchat_count)
        elseif (ev.EventName == "EvEntityMailListInit")
        then
            self:_haveNewChatOrMailRecord()
        elseif (ev.EventName == "EvEntityMailAdd")
        then
            self:_haveNewChatOrMailRecord()
        elseif (ev.EventName == "EvEntityMailDelete")
        then
            self:_haveNewChatOrMailRecord()
        elseif (ev.EventName == "EvEntityMailUpdate")
        then
            self:_haveNewChatOrMailRecord()
        elseif (ev.EventName == "EvEntityRecvChatFromDesktop")
        then
            local chat_info = ev.chat_info
            if (CS.System.String.IsNullOrEmpty(chat_info.sender_etguid) == false)
            then
                local seat_info = self.Desktop:getSeatByGuid(chat_info.sender_etguid)
                if (seat_info == nil)
                then
                    if (self.ItemChatDesktop == nil)
                    then
                        local co_chatname = "CoChatRight"
                        self.ItemChatDesktop = self.UiDesktopChatParent:addChat(co_chatname, self.ComUi, self.GBtnChat.xy)
                        self.ItemChatDesktop:setChatTextAndSortingOrder(chat_info, self.ComUi.sortingOrder)
                    else
                        self.ItemChatDesktop:setChatTextAndSortingOrder(chat_info, self.ComUi.sortingOrder)
                    end
                end
            end
        elseif (ev.EventName == "EvEntityDesktopIdleNotify")
        then
            self:_deskIdle()
        elseif (ev.EventName == "EvEntityDesktopPreFlopNotify")
        then
            self:_preflopBegin(ev.pot_total, ev.list_pot)
        elseif (ev.EventName == "EvEntityDesktopFlopNotify")
        then
            self:_flop(ev.first_card, ev.second_card, ev.third_card, ev.bet_player_count)
        elseif (ev.EventName == "EvEntityDesktopTurnNotify")
        then
            self:_turn(ev.turn_card, ev.bet_player_count)
        elseif (ev.EventName == "EvEntityDesktopRiverNotify")
        then
            self:_river(ev.river_card, ev.bet_player_count)
        elseif (ev.EventName == "EvEntityDesktopShowdownNotify")
        then
            self:_showDown(ev.desktop_showdown)
        elseif (ev.EventName == "EvEntityDesktopGameEndNotifyTexas")
        then
            self:_gameEnd(ev.list_winner)
        elseif (ev.EventName == "EvEntityGetLotteryTicketDataSuccess")
        then
            self:_setLotteryTicketInfo(ev.lotteryticket_data.State, ev.lotteryticket_data.StateLeftTm)
        elseif (ev.EventName == "EvEntityLotteryTicketGameEndStateSimple")
        then
            if (self.GTextLotteryTicketTips ~= nil)
            then
                self.GTextLotteryTicketTips.text = self.ViewMgr.LanMgr:getLanValue("Settlement")
            end
        elseif (ev.EventName == "EvEntityLotteryTicketUpdateTm")
        then
            self:updateLotteryTickTm(ev.tm)
        elseif (ev.EventName == "EvUiPotMainChanged")
        then
            self.UiPot:showAllPotValue(ev.pot_mian)
        elseif (ev.EventName == "EvEntityRefreshLeftOnlineRewardTm")
        then
            self.ViewOnlineReward:setLeftTm(ev.left_reward_second)
        elseif (ev.EventName == "EvEntityCanGetOnlineReward")
        then
            self.ViewOnlineReward:setCanGetReward(ev.can_getreward)
            self.CanGetOnLineReward = ev.can_getreward
            self:setNewReward()
        elseif (ev.EventName == "EvEntityCanGetTimingReward")
        then
            self.ViewTimingReward:setCanGetReward(ev.can_getreward)
            self.CanGetTimingReward = ev.can_getreward
            self:setNewReward()
        elseif (ev.EventName == "EvClickShowReward")
        then
            self.ComShadeReward.visible = true
            self.TransitionShowReward:Play()
        elseif (ev.EventName == "EvRequestGetTimingReward" or ev.EventName == "EvOnGetOnLineReward")
        then
            self.ComShadeReward.visible = false
            self.TransitionShowReward:PlayReverse()
        end
    end
end

function ViewDesktopTexas:setDesktopSnapshotData(desktop, desktop_data, is_init, desktoptype_facname)
    self.DesktopBase = desktop
    local desktop_texas = desktop
    local snapshot_data = desktop_data
    ViewHelper:setGObjectVisible(false, self.GGroupCard)
    ViewHelper:setGObjectVisible(true, self.TextDesktopDescribe)
    self.UiPot:resetPot()
    --ViewHelper:setGObjectVisible(false, self.GGroupActionTips)
    --ViewHelper:setGObjectVisible(false, self.GGroupCardTypeTips)
    if (is_init)
    then
        local viewdesktoptype_factory = self:GetViewDesktopTypeBaseFactory(desktoptype_facname)
        self.ViewDesktopTypeBase = viewdesktoptype_factory:CreateViewDesktopType(self)
        self.Desktop = desktop_texas
        local network_sign = self:_getNewWorkSignName()
        self.GLoaderNetwork.icon = CS.FairyGUI.UIPackage.GetItemURL("Desktop", network_sign)
        self:_showCurrentLocalTm()
        self.ActionWaitingTime = desktop_texas.ActionWaitingTm
        local poker_girl_path = CS.Casinos.CasinosContext.Instance:AppendStrWithSB(CS.Casinos.CasinosContext.Instance.ABResourcePathTitle,
                "PokerGirl/", string.lower(self.PokerGirlDesk), ".ab")
        self.GLoaderDealerGirl.icon = CS.Casinos.CasinosContext.Instance.PathMgr:combinePersistentDataPath(poker_girl_path)
    end
    self:_showCurrentScreenshot(snapshot_data.desktop_state, snapshot_data.goto_endstate)

    if self.ViewDesktopTypeBase ~= nil then
        self.ViewDesktopTypeBase:setSnapShot(snapshot_data, is_init)
    end

    self:_setNewChatCount(self.ControllerIM.IMChat:getAllNewChatCount())

    if self.DesktopBase.DesktopState ~= TexasDesktopState.GameEnd then
        self.UiPot:setSnapShotData(snapshot_data.pot_total, snapshot_data.list_pot)
    end

    local tips = self.DesktopBase.DesktopTypeBase.DesktopTips
    if desktop_texas.IsPrivate then
        local t = {}
        table.insert(t, self.ViewMgr.LanMgr:getLanValue("Private"))
        table.insert(t," ")
        table.insert(t, tips)
        if self.ViewDesktopTypeBase.Ante > 0 then
            table.insert(t,":")
            table.insert(t, self.ViewDesktopTypeBase.Ante)
        end
        tips = table.concat(t)
    else
        local t = {}
        table.insert(t, tips)
        if self.ViewDesktopTypeBase.Ante > 0 then
            table.insert(t,":")
            table.insert(t, self.ViewDesktopTypeBase.Ante)
        end
        tips = table.concat(t)
    end
    self.TextDesktopDescribe.text = tips
end

function ViewDesktopTexas:playerSeatInDesk(seat_index)
    local chair_info = self:_getSeatInfoFromNoPlayerSeat(seat_index)
    if (chair_info ~= nil)
    then
        self.MapValidNoPlayerSeat[seat_index] = nil
    end
    self.ViewDesktopTypeBase:_checkSeat()
end

function ViewDesktopTexas:playerLeaveDesk(seat_index)
    self:_playerLeaveSeat(seat_index)
end

function ViewDesktopTexas:playerOB(seat_index)
    self:_playerLeaveSeat(seat_index)
    --if (self:_meIsSeat() == false)
    --then
    --    ViewHelper:setGObjectVisible(false, self.GTextActionTips)
    --end
end

function ViewDesktopTexas:playerBuyAndSendItem(map_items)
    if (map_items == nil)
    then
        return
    end

    for k, v in pairs(map_items) do
        local item_data = ItemData1:new(nil)
        item_data:setData(v)
        local tb_item = self.ViewMgr.TbDataMgr:GetData("Item", item_data.item_tbid)
        local seat_info = self.Desktop:getSeatByGuid(k)
        if (seat_info ~= nil)
        then
            if (tb_item.UnitType == "GiftTmp")
            then
                seat_info.player_texas:setGift(item_data, true)
            elseif (tb_item.UnitType == "MagicExpression")
            then
                seat_info.player_texas:sendMagicExp(item_data)
            end
        end
    end
end

function ViewDesktopTexas:setCurrentSeatActor(list_player)
    self.MapValidNoPlayerSeat = {}

    for k, v in pairs(self.MapAllValidPlayerSeat) do
        local have_actor = false
        for p_k, p_v in pairs(list_player) do
            if (k == p_v.UiSeatIndex)
            then
                have_actor = true
            end
        end
        if (have_actor == false)
        then
            self.MapValidNoPlayerSeat[k] = v
        end
    end

    self.ViewDesktopTypeBase:_checkSeat()
end

function ViewDesktopTexas:showCardTips(best_hand_type, list_cards_type_data, list_cards_all_data, tips, show_tips, is_end)
    --ViewHelper:setGObjectVisible(show_tips, self.GGroupCardTypeTips)
    --if (show_tips)
    --then
    --    self.GTextCardTypeTips.text = tips
    --end
    local rank_type = best_hand_type--CS.Casinos.HandRankTypeTexas.__CastFrom()
    local need_showhighlight = rank_type ~= CS.Casinos.HandRankTypeTexas.HighCard and rank_type ~= CS.Casinos.HandRankTypeTexas.None
    local t_list_cards_data = CS.Casinos.LuaHelper.ListToLuatable(list_cards_type_data)
    local t_list_cards_all_data = CS.Casinos.LuaHelper.ListToLuatable(list_cards_all_data)
    self.DealerEx:showCommonCardType(need_showhighlight, t_list_cards_data, t_list_cards_all_data, is_end)
end

function ViewDesktopTexas:setActionTips(tips)
    --if (CS.System.String.IsNullOrEmpty(tips))
    --then
    --    ViewHelper:setGObjectVisible(false, self.GGroupActionTips)
    --else
    --    ViewHelper:setGObjectVisible(true, self.GGroupActionTips)
    --    ViewHelper:setGObjectVisible(true, self.GTextActionTips)
    --    self.GTextActionTips.text = tips
    --end
end

function ViewDesktopTexas:resetCardTypeTips()
end

function ViewDesktopTexas:commonCardShowEnd()
    local ev = self.ViewMgr:getEv("EvCommonCardShowEnd")
    if (ev == nil)
    then
        ev = EvCommonCardShowEnd:new(nil)
    end
    self.ViewMgr:sendEv(ev)

    self:showCommonCardType(false)
end

function ViewDesktopTexas:commonCardDealEnd()
    local ev = self.ViewMgr:getEv("EvCommonCardDealEnd")
    if (ev == nil)
    then
        ev = EvCommonCardDealEnd:new(nil)
    end
    self.ViewMgr:sendEv(ev)
end

function ViewDesktopTexas:showCommonCardType(is_gameend)
    self.ListSelfAndCommonCard:Clear()
    if (self.Desktop.MeP ~= nil and self.Desktop.MeP.FirstCard ~= nil)
    then
        self.ListSelfAndCommonCard:Add(self.Desktop.MeP.FirstCard)
        self.ListSelfAndCommonCard:Add(self.Desktop.MeP.SecondCard)
    end
    for i, v in pairs(self.Desktop.CommunityCards) do
        self.ListSelfAndCommonCard:Add(v)
    end

    local best_hand = CS.Casinos.CardTypeHelperTexas.GetBestHand(self.ListSelfAndCommonCard)
    local hand_type = best_hand.RankType
    local card_type_str = ""
    local show_cardtype_tips = true
    if (hand_type == CS.Casinos.HandRankTypeTexas.None or hand_type == CS.Casinos.HandRankTypeTexas.HighCard)
    then
        show_cardtype_tips = false
    else
        card_type_str = self.ViewMgr.LanMgr:getLanValue( CS.Casinos.LuaHelper.ParseHandRankTypeTexasToStr(hand_type))
    end

    self:showCardTips(best_hand.RankType, best_hand.RankTypeCards, best_hand.Cards, card_type_str, show_cardtype_tips, is_gameend)

    if (self.Desktop.MeP ~= nil and self.Desktop.MeP.UiDesktopPlayerInfo ~= nil)
    then
        local player_info = self.Desktop.MeP.UiDesktopPlayerInfo
        player_info:showHandCardHighLight(best_hand, card_type_str)
    end
    self.ListSelfAndCommonCard:Clear()
end

function ViewDesktopTexas:getUiSeatInfo(seat_index)
    local chair_info = self.MapAllValidPlayerSeat[seat_index]
    return chair_info
end

function ViewDesktopTexas:playerSendChipsToPotDone()
    self.UiPot:showPotValue()
end

function ViewDesktopTexas:resetComminityShow()
    --ViewHelper:setGObjectVisible(false, self.GGroupCardTypeTips)
    --ViewHelper:setGObjectVisible(false, self.GGroupActionTips)
    --self.GTextCardTypeTips.text = ""
    --self.GTextActionTips.text = ""
end

function ViewDesktopTexas:_meIsSeat()
    local me_seated = false
    if (self.Desktop.MeP ~= nil)
    then
        local me_seatindex = self.Desktop.MeP.UiSeatIndex
        me_seated = self.Desktop:isValidSeatIndex(me_seatindex)
    end

    return me_seated
end

function ViewDesktopTexas:_showCurrentScreenshot(current_state, gotoend_state)
    self.UiChipMgr:resetChips()
    self.DealerEx:clearQueue()
    if (current_state == TexasDesktopState.PreFlop or current_state == TexasDesktopState.Flop
            or current_state == TexasDesktopState.Turn or current_state == TexasDesktopState.River)
    then
        self.ClearDesktopTm = 0
        self:_showDesktopScreenshot(current_state, false)
    elseif (current_state == TexasDesktopState.GameEnd)
    then
        ViewHelper:setGObjectVisible(true, self.ComWaitingBegine)
        --self.ClearDesktopTm = self.Desktop.ReBeginTm - 3
        --self:_showDesktopScreenshot(gotoend_state, true)
    end
end

function ViewDesktopTexas:_playerLeaveSeat(seat_index)
    local seat_logic = seat_index
    if (self.Desktop.SeatNum == 5)
    then
        seat_logic = math.floor(seat_index / 2)
    end

    if (self.Desktop:isValidSeatIndex(seat_logic) == false)
    then
        return
    end

    local seat_info = self.MapAllValidPlayerSeat[seat_index]
    if (seat_info ~= nil)
    then
        self.MapValidNoPlayerSeat[seat_index] = seat_info
        self.ViewDesktopTypeBase:_checkSeat()
    end
end

function ViewDesktopTexas:_setNewChatCount(chat_count)
    self.NewFriendChatCount = chat_count
    self:_haveNewChatOrMailRecord()
end

function ViewDesktopTexas:_haveNewChatOrMailRecord()
    local com_mailTips_temp = self.ComUi:GetChild("ComMailTips")
    if (com_mailTips_temp ~= nil)
    then
        local com_mailTips = com_mailTips_temp.asCom
        if (self.ControllerIM:haveNewMail())
        then
            ViewHelper:setGObjectVisible(true, com_mailTips)
            local transition_newMsg = com_mailTips:GetTransition("TransitionNewMsg")
            transition_newMsg:Play()
            CS.Casinos.CasinosContext.Instance:play("NewMessage", CS.Casinos._eSoundLayer.LayerReplace)
        else
            ViewHelper:setGObjectVisible(false, com_mailTips)
        end
    end
    if (self.NewFriendChatCount > 0)
    then
        ViewHelper:setGObjectVisible(true, self.GComMsgTips)
        self.TransitionNewMsg:Play()
        self.GTextMsgTips.text = tostring(self.NewFriendChatCount)
        CS.Casinos.CasinosContext.Instance:play("NewMessage", CS.Casinos._eSoundLayer.LayerReplace)
    else
        ViewHelper:setGObjectVisible(false, self.GComMsgTips)
    end
end

function ViewDesktopTexas:_showCurrentLocalTm()
    local tm = self:_getCurrentLocalTime()
    self.GTextTm.text = tm
end

function ViewDesktopTexas:_onClickDesktopChat()
    local ui_chat = self.ViewMgr:createView("Chat")
    ui_chat:init(_eUiChatType.Desktop)
end

function ViewDesktopTexas:_onClickFriend()
    local ev = self.ViewMgr:getEv("EvUiClickFriend")
    if (ev == nil)
    then
        ev = EvUiClickFriend:new(nil)
    end
    self.ViewMgr:sendEv(ev)
end

--function ViewDesktopTexas:_onClickHelp()
--    self.ViewMgr:createView("DesktopHintsTexas")
--end

function ViewDesktopTexas:_onClickLockChat()
    local ev = self.ViewMgr:getEv("EvUiDesktopClickLockChat")
    if (ev == nil)
    then
        ev = EvUiDesktopClickLockChat:new(nil)
    end
    self.ViewMgr:sendEv(ev)
end

function ViewDesktopTexas:_onClickFriendChat()
    local ev = self.ViewMgr:getEv("EvUiClickChatmsg")
    if (ev == nil)
    then
        ev = EvUiClickChatmsg:new(nil)
    end
    self.ViewMgr:sendEv(ev)
end

function ViewDesktopTexas:_onClickNotice()
    self.ViewMgr:createView("Notice")
end

function ViewDesktopTexas:_onClickBtnChatFriend()
    self.ViewMgr:createView("ChatFriend")
end

function ViewDesktopTexas:_onClickChair(context)
    local s = CS.Casinos.LuaHelper.EventDispatcherCastToGComponent(context.sender)
    for k, v in pairs(self.MapValidNoPlayerSeat) do
        if (s == v.GComSitOrInvite)
        then
            if (self:_meIsSeat() == false)
            then
                local index = v.ChairIndex
                if (self.Desktop.SeatNum == 5)
                then
                    index = math.ceil(index / 2)
                end
                local ev = self.ViewMgr:getEv("EvUiClickSeat")
                if (ev == nil)
                then
                    ev = EvUiClickSeat:new(nil)
                end
                ev.seat_index = index
                self.ViewMgr:sendEv(ev)
            else
                local ev = self.ViewMgr:getEv("EvUiClickInviteFriendPlay")
                if (ev == nil)
                then
                    ev = EvUiClickInviteFriendPlay:new(nil)
                end
                self.ViewMgr:sendEv(ev)
            end
        end
    end
end

function ViewDesktopTexas:_deskIdle()
    ViewHelper:setGObjectVisible(false, self.GGroupCard)
end

function ViewDesktopTexas:_preflopBegin(pot_total, list_pot)
    ViewHelper:setGObjectVisible(false, self.ComWaitingBegine)
    self.UiPot:resetPot()
    self.UiPot:setSnapShotData(pot_total, list_pot)
    self:_dealCard()
    self.ViewDesktopTypeBase:preflopBegin()
    --self:resetComminityShow()
end

function ViewDesktopTexas:_flop(first_card, second_card, third_card, bet_player_count)
    self.DealerEx:showCommonCard(first_card, self.GComCommunityCard1, bet_player_count)
    self.DealerEx:showCommonCard(second_card, self.GComCommunityCard2, bet_player_count)
    self.DealerEx:showCommonCard(third_card, self.GComCommunityCard3, bet_player_count)
    self.DealerEx:resetCard(self.GComCommunityCard4)
    self.DealerEx:resetCard(self.GComCommunityCard5)
end

function ViewDesktopTexas:_turn(turn_card, bet_player_count)
    self.DealerEx:showCommonCard(turn_card, self.GComCommunityCard4, bet_player_count)
    self.DealerEx:resetCard(self.GComCommunityCard5)
end

function ViewDesktopTexas:_river(river_card, bet_player_count)
    self.DealerEx:showCommonCard(river_card, self.GComCommunityCard5, bet_player_count)
end

function ViewDesktopTexas:_showDown(desktop_showdown)
    local carddata_left = desktop_showdown.list_carddata_left
    local left_cards = #carddata_left
    if left_cards == 5 then
        local card_d1 = carddata_left[1]
        local card_d2 = carddata_left[2]
        local card_d3 = carddata_left[3]
        local card_d4 = carddata_left[4]
        local card_d5 = carddata_left[5]
        local card1 = CS.Casinos.Card(card_d1[1], card_d1[2])
        local card2 = CS.Casinos.Card(card_d2[1], card_d2[2])
        local card3 = CS.Casinos.Card(card_d3[1], card_d3[2])
        local card4 = CS.Casinos.Card(card_d4[1], card_d4[2])
        local card5 = CS.Casinos.Card(card_d5[1], card_d5[2])
        self.DealerEx:showCommonCard(card1, self.GComCommunityCard1, 1)
        self.DealerEx:showCommonCard(card2, self.GComCommunityCard2, 1)
        self.DealerEx:showCommonCard(card3, self.GComCommunityCard3, 1)
        self.DealerEx:showCommonCard(card4, self.GComCommunityCard4, 1)
        self.DealerEx:showCommonCard(card5, self.GComCommunityCard5, 1)
    elseif left_cards == 2 then
        local card_d1 = carddata_left[1]
        local card_d2 = carddata_left[2]
        local card1 = CS.Casinos.Card(card_d1[1], card_d1[2])
        local card2 = CS.Casinos.Card(card_d2[1], card_d2[2])
        self.DealerEx:showCommonCard(card1, self.GComCommunityCard4, 1)
        self.DealerEx:showCommonCard(card2, self.GComCommunityCard5, 1)
    elseif left_cards == 1 then
        local card_d1 = carddata_left[1]
        local card1 = CS.Casinos.Card(card_d1[1], card_d1[2])
        self.DealerEx:showCommonCard(card1, self.GComCommunityCard5, 1)
    elseif left_cards == 0 then
        --self:commonCardShowEnd()
        --self.DealerEx:hideCommonCardType()
    end
end

function ViewDesktopTexas:_gameEnd(list_winner)
    self.ListWinnerPlayerInfo = {}
    self.DealerEx:hideCommonCardType()
    self.ClearDesktopTm = self.Desktop.ReBeginTm - 3.7
    for i, v in pairs(list_winner) do
        local w = _WinnerPlayerInfo:new(v.player_guid, v.win_chip, v.map_win_pot)
        self.ListWinnerPlayerInfo[i] = w
    end

    self.DealerEx:giveWinnerChips(self.ListWinnerPlayerInfo,
            function()
                --self.UiPot:resetPot()
                --self.DealerEx:resetCommonCardType(
                --        function()
                --            ViewHelper:setGObjectVisible(true, self.ComWaitingBegine)
                --        end)
                --ViewHelper:setGObjectVisible(false, self.GGroupActionTips)
                --ViewHelper:setGObjectVisible(false, self.GGroupCardTypeTips)
            end
    )
end

function ViewDesktopTexas:_clearDeskTop()
    self.MapValidNoPlayerSeat = {}
end

function ViewDesktopTexas:_onClickBtnLotteryTicket()
    local ev = self.ViewMgr:getEv("EvEntityRequestGetLotteryTicketData")
    if (ev == nil)
    then
        ev = EvEntityRequestGetLotteryTicketData:new(nil)
    end
    self.ViewMgr:sendEv(ev)

    self.ViewMgr:createView("LotteryTicket")
end

function ViewDesktopTexas:_dealCard()
    ViewHelper:setGObjectVisible(true, self.GGroupCard)
    self.ClearDesktopTm = 0
    self.DealerEx:clearCurrentResetCard()
    self.ListAllPlayer = {}
    for k, v in pairs(self.Desktop:getAllValidPlayer()) do
        local player = v
        if ((player.PlayerDataDesktop.DesktopPlayerState == TexasDesktopPlayerState.InGame
                and player.PlayerDataDesktop.PlayerActionType ~= PlayerActionTypeTexas.Fold))
        then
            self.ListAllPlayer[k] = player
        end
    end

    --[[self.ListAllPlayer.Sort((DesktopPlayerBase actor1, DesktopPlayerBase actor2) =>
    {
        if (actor1.UiSeatIndex < actor2.UiSeatIndex)
        then
            return -1                
        elseif (actor1.UiSeatIndex > actor2.UiSeatIndex)
        then
            return 1                
        else                
            return 0
        end
    })]]--
    self.DealerEx:dealPlayerCard(self.ListAllPlayer,
            function()
                self:_dealCardDone()
            end
    , function(player)
                self:_dealOnePlayerCard(player)
            end
    )
    self.ListAllPlayer = {}
end

function ViewDesktopTexas:_dealOnePlayerCard(player)
    if (player == nil or player.UiDesktopPlayerInfo == nil)
    then
        return
    end

    local player_texas = player
    local card_one_clone = self.DealerEx:getCardDealing()
    self.UiDesktopChatParent.ComUi:AddChild(card_one_clone.GComCard)
    local card_two_clone = self.DealerEx:getCardDealing()
    self.UiDesktopChatParent.ComUi:AddChild(card_two_clone.GComCard)
    local player_info = player_texas.UiDesktopPlayerInfo
    local seat_widget = player_info.PlayerSeatWidgetControllerEx.SeatWidget
    local one_p = seat_widget.GImageCardFirst.displayObject.gameObject.transform.localPosition
    local one_p2 = CS.Casinos.LuaHelper.GetVector2(one_p.x,-one_p.y)
    local one_to_p = player_info.ComUi:TransformPoint(one_p2, self.ComUi)
    card_one_clone:init(self.GComDealer.xy, one_to_p, seat_widget.GImageCardFirst.size, seat_widget.GImageCardFirst.rotation,
            0.5, "fapai",
            function()
                if (player ~= nil and player.UiDesktopPlayerInfo ~= nil)
                then
                    player_info:dealCardDone()
                end

                self.DealerEx:cardObjDealingEnqueue(card_one_clone)
            end
    )
    card_one_clone:deal()
    local two_p = seat_widget.GImageCardSecond.displayObject.gameObject.transform.localPosition
    local two_p2 = CS.Casinos.LuaHelper.GetVector2(two_p.x,-two_p.y)
    local two_to_p = player_info.ComUi:TransformPoint(two_p2, self.ComUi)
    card_two_clone:init(self.GComDealer.xy, two_to_p, seat_widget.GImageCardSecond.size, seat_widget.GImageCardSecond.rotation,
            0.5, "fapai",
            function()
                self.DealerEx:cardObjDealingEnqueue(card_two_clone)
            end
    )
    card_two_clone:deal()
end

function ViewDesktopTexas:_dealCardDone()
    if (self.Desktop.DesktopState == TexasDesktopState.PreFlop)
    then
        self.DealerEx:dealCommonCard(nil, self.GComCommunityCard1)
        self.DealerEx:dealCommonCard(nil, self.GComCommunityCard2)
        self.DealerEx:dealCommonCard(nil, self.GComCommunityCard3)
        self.DealerEx:resetCard(self.GComCommunityCard4)
        self.DealerEx:resetCard(self.GComCommunityCard5)
    end
end

function ViewDesktopTexas:_getSeatInfoFromNoPlayerSeat(chair_index)
    local seat_info = self.MapValidNoPlayerSeat[chair_index]
    return seat_info
end

function ViewDesktopTexas:_showDesktopScreenshot(goto_endstate, is_gameend)
    if (goto_endstate == TexasDesktopState.PreFlop)
    then
        ViewHelper:setGObjectVisible(true, self.GGroupCard)
        self:_showPreflopScreenshot()
        self:showCommonCardType(is_gameend)
    elseif (goto_endstate == TexasDesktopState.Flop)
    then
        ViewHelper:setGObjectVisible(true, self.GGroupCard)
        self:_showFlopScreenshot()
        self.DealerEx:resetCard(self.GComCommunityCard4)
        self.DealerEx:resetCard(self.GComCommunityCard5)
        self:showCommonCardType(is_gameend)
    elseif (goto_endstate == TexasDesktopState.Turn)
    then
        ViewHelper:setGObjectVisible(true, self.GGroupCard)
        self:_showTurnScreenshot()
        self.DealerEx:resetCard(self.GComCommunityCard5)
        self:showCommonCardType(is_gameend)
    elseif (goto_endstate == TexasDesktopState.River)
    then
        ViewHelper:setGObjectVisible(true, self.GGroupCard)
        self:_showRiverScreenshot()
        self:showCommonCardType(is_gameend)
    end
end

function ViewDesktopTexas:_showPreflopScreenshot()
    self.DealerEx:showCommonCardScreenshot(nil, self.GComCommunityCard1)
    self.DealerEx:showCommonCardScreenshot(nil, self.GComCommunityCard2)
    self.DealerEx:showCommonCardScreenshot(nil, self.GComCommunityCard3)
    self.DealerEx:resetCard(self.GComCommunityCard4)
    self.DealerEx:resetCard(self.GComCommunityCard5)
end

function ViewDesktopTexas:_showFlopScreenshot()
    self.DealerEx:showCommonCardScreenshot(self.Desktop.CommunityCards[1], self.GComCommunityCard1)
    self.DealerEx:showCommonCardScreenshot(self.Desktop.CommunityCards[2], self.GComCommunityCard2)
    self.DealerEx:showCommonCardScreenshot(self.Desktop.CommunityCards[3], self.GComCommunityCard3)
end

function ViewDesktopTexas:_showTurnScreenshot()
    self:_showFlopScreenshot()
    self.DealerEx:showCommonCardScreenshot(self.Desktop.CommunityCards[4], self.GComCommunityCard4)
end

function ViewDesktopTexas:_showRiverScreenshot()
    self:_showTurnScreenshot()
    self.DealerEx:showCommonCardScreenshot(self.Desktop.CommunityCards[5], self.GComCommunityCard5)
end

function ViewDesktopTexas:updateLotteryTickTm(tm)
    if (self.GTextLotteryTicketTips == nil)
    then
        return
    end

    if (tm > 0)
    then
        self.GTextLotteryTicketTips.text = tm .. self.ViewMgr.LanMgr:getLanValue( "S")
    else
        self.GTextLotteryTicketTips.text = self.ViewMgr.LanMgr:getLanValue( "Settlement")
    end
end

function ViewDesktopTexas:_setLotteryTicketInfo(state, left_tm)
    local tips = ""
    if (state == LotteryTicketStateEnum.Bet)
    then
        local tm = math.ceil(left_tm)
        tips = tm .. self.ViewMgr.LanMgr:getLanValue("S")
    else
        tips = self.ViewMgr.LanMgr:getLanValue( "Settlement")
    end
    if (self.GTextLotteryTicketTips ~= nil)
    then
        self.GTextLotteryTicketTips.text = tips
    end
end

function ViewDesktopTexas:getPlayer(player_guid)
    local l = self.DesktopBase:GetDesktopPlayerByGuid(player_guid)
    return l
end

function ViewDesktopTexas:_getNewWorkSignName()
    local net_work_sign = ""
    if (CS.UnityEngine.Application.internetReachability == CS.UnityEngine.NetworkReachability.NotReachable)
    then
        net_work_sign = "NoNetwork"
    elseif (CS.UnityEngine.Application.internetReachability == CS.UnityEngine.NetworkReachability.ReachableViaCarrierDataNetwork)
    then
        net_work_sign = "MoblieNetWork"
    elseif (CS.UnityEngine.Application.internetReachability == CS.UnityEngine.NetworkReachability.ReachableViaLocalAreaNetwork)
    then
        net_work_sign = "Wifi"
    end

    return net_work_sign
end

function ViewDesktopTexas:_getCurrentLocalTime()
    local now = CS.Casinos.LuaHelper.GetNowFormat("HH:mm")
    return now
end

function ViewDesktopTexas:regViewDesktopTypeBaseFactory(desktop_fac)
    self.MapViewDesktopTypeBaseFac[desktop_fac:GetName()] = desktop_fac
end

function ViewDesktopTexas:GetViewDesktopTypeBaseFactory(fac_name)
    return self.MapViewDesktopTypeBaseFac[fac_name]
end

function ViewDesktopTexas:showMeWin()
    ViewHelper:setGObjectVisible(true, self.ComMeWin)
    CS.Casinos.CasinosContext.Instance:play("ying", CS.Casinos._eSoundLayer.LayerNormal)
    self.TransitionMeWin:Play(function()
        ViewHelper:setGObjectVisible(false, self.ComMeWin)
    end)
end

function ViewDesktopTexas:setNewReward()
    local have_newreward = false
    if (self.CanGetOnLineReward or self.CanGetTimingReward)
    then
        have_newreward = true
    end

    if (have_newreward == false)
    then
        ViewHelper:setGObjectVisible(false, self.ComRewardTips)
    else
        ViewHelper:setGObjectVisible(true, self.ComRewardTips)
        if (self.TransitionNewReward.playing == false)
        then
            self.TransitionNewReward:Play()
        end
    end
end

ViewDesktopTexasFactory = ViewFactory:new()

function ViewDesktopTexasFactory:new(o, ui_package_name, ui_component_name,
                                     ui_layer, is_single, fit_screen)
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

function ViewDesktopTexasFactory:createView()
    local view = ViewDesktopTexas:new(nil)
    return view
end