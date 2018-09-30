-- Copyright(c) Cragon. All rights reserved.
-- 与ControllerPlayer对应，响应福利按钮消息（ViewDesktop也响应福利消息）
-- 好友列表
-- 推荐好友列表
-- 本人信息
-- 3个主按钮(粒子特效)，右下方一排功能按钮
-- 父层小红点处理，首次查询+后续消息响应(响应小红点相关的业务消息)

---------------------------------------
ViewMain = ViewBase:new()

---------------------------------------
function ViewMain:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.ViewMgr = nil
    self.GoUi = nil
    self.ComUi = nil
    self.Panel = nil
    self.UILayer = nil
    self.InitDepth = nil
    self.ViewKey = nil
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.PokerGirlMain = "PokerGirMain"
    return o
end

---------------------------------------
function ViewMain:onCreate()
    self.GTransitionShow = self.ComUi:GetTransition("TransitionShow")
    self.GTransitionShow:Play()
    self.ListFriendInfo = {}
    self.ListFriendInfoRecommend = {}
    self.MapFriend = {}
    self.ListHeadIconFriend = {}
    self.ListHeadIconRecommond = {}
    self.ControllerActor = self.ViewMgr.ControllerMgr:GetController("Actor")
    self.ControllerPlayer = self.ViewMgr.ControllerMgr:GetController("Player")
    self.ControllerIM = self.ViewMgr.ControllerMgr:GetController("IM")
    self.ControllerBag = self.ViewMgr.ControllerMgr:GetController("Bag")
    self.ControllerLotteryTicket = self.ViewMgr.ControllerMgr:GetController("LotteryTicket")
    self.ControllerLotteryTicket = self.ViewMgr.ControllerMgr:GetController("LotteryTicket")
    self.ControllerLogin = self.ViewMgr.ControllerMgr:GetController("Login")
    local btn_desktop = self.ComUi:GetChild("BtnDeskTop").asButton
    btn_desktop.onClick:Add(
            function()
                self:onClickBtnDeskTop()
            end
    )
    local btn_desktoph = self.ComUi:GetChild("BtnDeskTopH").asButton
    btn_desktoph.onClick:Add(
            function()
                self:onClickBtnDesktopH()
            end
    )

    local btn_match = self.ComUi:GetChild("BtnMatch").asButton
    btn_match.onClick:Add(
            function()
                self:onClickBtnMatch()
            end
    )

    local btn_circleofcardfriends = self.ComUi:GetChild("BtnCircleOfCardFriends").asButton
    btn_circleofcardfriends.onClick:Add(
            function()
                self:onClickBtnCircleOfCardFriends()
            end
    )

    local btn_goldtree = self.ComUi:GetChild("BtnGoldTree").asButton
    btn_goldtree.onClick:Add(
            function()
                self:onClickBtnGoldTree()
            end
    )

    --[[local btn_wa = self.ComUi:GetChild("BtnWa").asButton
    btn_wa.onClick:Add(
        function()
            self:onClickBtnWa()
        end
    )]]

    local btn_activity = self.ComUi:GetChild("BtnActivityCenter").asButton
    btn_activity.onClick:Add(
            function()
                self:onClickBtnActivity()
            end
    )
    local show_goldtree = false
    if (self.CasinosContext.ShowGoldTree == true and self.ControllerActor.PropEnableGrow:get() == true) then
        show_goldtree = true
    end
    ViewHelper:setGObjectVisible(show_goldtree, btn_goldtree)

    local gold_par_parent = btn_goldtree:GetChild("GoldTreeParParent").asGraph
    local p_helper = ParticleHelper:new(nil)
    local g_t_particle = p_helper:GetParticel("goldtreepar.ab")
    local prefab_g_t_particle = g_t_particle:LoadAsset("GoldTreePar")
    local g_p = CS.UnityEngine.GameObject.Instantiate(prefab_g_t_particle)
    gold_par_parent:SetNativeObject(CS.FairyGUI.GoWrapper(g_p))

    local com_more = nil
    if (self.CasinosContext.NeedHideClientUi) then
        com_more = self.ComUi:GetChild("ComMoreHideRank").asCom
        btn_match.visible = false
        --btn_wa.visible = false
    else
        btn_match.visible = true
        --btn_wa.visible = true
        com_more = self.ComUi:GetChild("ComMore").asCom
        local btn_ranking = com_more:GetChild("BtnRank").asButton
        btn_ranking.onClick:Add(
                function()
                    self:onClickBtnRanking()
                end
        )
    end
    if (com_more ~= nil) then
        com_more.visible = true
    end

    local c_m_s = com_more:GetController("ChipIconSolustion")
    c_m_s.selectedIndex = ChipIconSolustion

    self.ControllerMoreBtn = self.ComUi:GetController("ControllerMoreBtn")
    self.ControllerMoreBtn.selectedIndex = 0
    local btn_mail = self.ComUi:GetChild("BtnMail").asButton
    btn_mail.onClick:Add(
            function()
                self:onClickBtnMail()
            end
    )
    self.ComShadeMore = self.ComUi:GetChild("ComShadeMore").asCom
    self.ComShadeMore.visible = false
    self.ComShadeMore.onClick:Add(
            function()
                self:onClickBtnMore()
            end
    )
    self.ComMsgTips = self.ComUi:GetChild("ComMsgTips").asCom
    self.TransitionNewMsg = self.ComMsgTips:GetTransition("TransitionNewMsg")
    self.GTextMsgNum = self.ComMsgTips:GetChild("TextMsgTips").asTextField
    self.ComMailTips = self.ComUi:GetChild("ComMailTips").asCom
    self.TransitionNewMail = self.ComMailTips:GetTransition("TransitionNewMsg")
    self.GTextMailNum = self.ComMailTips:GetChild("TextMsgTips").asTextField
    self.ComMoreTips = self.ComUi:GetChild("ComMoreTips").asCom
    self.TransitionComMore = self.ComMoreTips:GetTransition("TransitionNewMsg")
    self.GTextComMoreNum = self.ComMoreTips:GetChild("TextMsgTips").asTextField
    local btn_shop = nil
    if ChipIconSolustion == 0 then
        btn_shop = self.ComUi:GetChild("BtnShop").asButton
    else
        btn_shop = com_more:GetChild("BtnShop").asButton
        local btn_purse = self.ComUi:GetChild("BtnPurse").asButton
        btn_purse.onClick:Add(
                function()
                    self:onClickBtnPurse()
                end)
    end
    btn_shop.onClick:Add(
            function()
                self:onClickBtnShop()
            end
    )
    local btn_friend = com_more:GetChild("BtnFriend").asButton
    btn_friend.onClick:Add(
            function()
                self:onClickBtnFriend()
            end
    )
    local btn_set = com_more:GetChild("BtnSet").asButton
    btn_set.onClick:Add(
            function()
                self:onClickBtnSet()
            end
    )
    local btn_bag = com_more:GetChild("BtnBag").asButton
    btn_bag.onClick:Add(
            function()
                self:onClickBtnChat()
            end
    )
    self.ComBagTips = com_more:GetChild("ComBagTips").asCom
    self.TransitionBagTips = self.ComBagTips:GetTransition("TransitionNewMsg")
    local btn_Chatfriend = self.ComUi:GetChild("BtnChatFriend").asButton
    btn_Chatfriend.onClick:Add(
            function()
                self:onClickBtnChatFriend()
            end
    )
    local btn_bank = com_more:GetChild("BtnBank").asButton
    btn_bank.onClick:Add(
            function()
                self:onClickBtnBank()
            end
    )
    local btn_feedback = com_more:GetChild("BtnFeedback").asButton
    btn_feedback.onClick:Add(
            function()
                self:onClickBtnFeedback()
            end
    )
    self.ComFeedbackTips = btn_feedback:GetChild("ComMsgTips").asCom
    self.TransitionFeedbackTips = self.ComFeedbackTips:GetTransition("TransitionNewMsg")
    local btn_more = self.ComUi:GetChild("BtnMore").asButton
    btn_more.onClick:Add(
            function()
                self:onClickBtnMore()
            end
    )

    self.BtnRegister = self.ComUi:GetChild("BtnRegister").asButton
    self.BtnRegister.onClick:Add(
            function()
                ViewHelper:UiShowMsgBox(self.ViewMgr.LanMgr:getLanValue("GuestRegisterTips"),
                        function()
                            CS.Casinos.CasinosContext.Instance:Disconnect()
                            local login = self.ViewMgr:getView("Login")
                            if login ~= nil then
                                login:_onClickBtnShowRegister()
                            end
                        end)
            end)
    local show_register = false
    if CS.Casinos.CasinosContext.Instance.LoginType == CS.Casinos._eLoginType.Guest then
        show_register = true
    end
    ViewHelper:setGObjectVisible(show_register, self.BtnRegister)

    local is_firstrecharge = self.ControllerActor.PropIsFirstRecharge:get()
    local com_recharge_first = self.ComUi:GetChild("ComRechargeFirst").asCom
    com_recharge_first.onClick:Add(
            function()
                self:onClickComRechargeFirst()
            end
    )
    local show_first = false
    if (self.CasinosContext.ClientShowFirstRecharge == true and is_firstrecharge == true) then
        show_first = true
    else
        self.BtnRegister.position = com_recharge_first.position
    end
    com_recharge_first.visible = show_first

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
    self.TransitionShowReward = self.ComUi:GetTransition("TransitionShowReward")
    self.ComWelfareIcon = self.ComUi:GetChild("ComWelfareIcon").asCom
    self.ComWelfareIcon.onClick:Add(
            function()
                self.ComShadeReward.visible = true
                self.TransitionShowReward:Play()
            end)

    local com_lottryticket = self.ComUi:GetChild("ComLotteryTicket").asCom
    com_lottryticket.onClick:Add(
            function()
                self:onClickBtnLotteryTicket()
            end
    )
    self.GTextLotteryTicketTips = com_lottryticket:GetChild("LotteryTicketTips").asTextField
    self:setLotteryTicketInfo(self.ControllerLotteryTicket.LotteryTicketState, self.ControllerLotteryTicket.BetStateTm)
    local com_playerinfo = self.ComUi:GetChild("ComPlayerInfoSelf").asCom
    local c_p = com_playerinfo:GetController("ChipIconSolustion")
    c_p.selectedIndex = ChipIconSolustion
    self.UiPlayerInfoSelf = UiMainPlayerInfo:new(com_playerinfo, false,
            function()
                self:onClickComPlayerInfoSelf()
            end
    )
    self.BtnInviteFriend = self.ComUi:GetChild("BtnInviteFriend").asButton
    self.BtnInviteFriend.visible = true
    self.BtnInviteFriend.onClick:Add(
            function()
                self:onClickComPlayerInfoCurrentFriend()
            end
    )
    local com_friendInfo = self.ComUi:GetChild("ComPlayerInfoFriend").asCom
    local c_f = com_friendInfo:GetController("ChipIconSolustion")
    c_f.selectedIndex = ChipIconSolustion
    local controller_showdiamondIcon = com_friendInfo:GetController("ControllerShowDiamondIcon")
    controller_showdiamondIcon:SetSelectedIndex(1)
    self.UiPlayerInfoCurrentFriend = UiMainPlayerInfo:new(com_friendInfo, true,
            function()
                self:onClickComPlayerInfoCurrentFriend()
            end
    )
    self.TransitionShowMore = self.ComUi:GetTransition("TansitionShowComMore")
    self.GTextPlayerNum = self.ComUi:GetChild("TextPlayerNum").asTextField
    ViewHelper:setGObjectVisible(false, self.GTextPlayerNum)
    self.GListFriend = self.ComUi:GetChild("ListFriend").asList
    self.GListFriend:SetVirtual()
    self.GListFriend.itemRenderer = function(index, item)
        self:rendererFriend(index, item)
    end
    self.GListRecommend = self.ComUi:GetChild("ListRecommend").asList
    self.GListRecommend:SetVirtual()
    self.GListRecommend.itemRenderer = function(index, item)
        self:rendererFriendRecommend(index, item)
    end
    self.GLoaderWaiting = self.ComUi:GetChild("Wating").asLoader
    local view_shootingtext = self.ViewMgr:getView("ShootingText")
    if (view_shootingtext == nil)
    then
        view_shootingtext = self.ViewMgr:createView("ShootingText")
        view_shootingtext:init(true, false, true)
    end

    self.ViewMgr:bindEvListener("EvEntityNotifyDeleteFriend", self)
    self.ViewMgr:bindEvListener("EvEntityGoldChanged", self)
    self.ViewMgr:bindEvListener("EvEntityDiamondChanged", self)
    self.ViewMgr:bindEvListener("EvEntityRecommendPlayerList", self)
    self.ViewMgr:bindEvListener("EvEntityPlayerInfoChanged", self)
    self.ViewMgr:bindEvListener("EvEntitySetOnLinePlayerNum", self)
    self.ViewMgr:bindEvListener("EvEntityReceiveFriendSingleChat", self)
    self.ViewMgr:bindEvListener("EvEntityReceiveFriendChats", self)
    self.ViewMgr:bindEvListener("EvEntityUnreadChatsChanged", self)
    self.ViewMgr:bindEvListener("EvGetPicUpLoadSuccess", self)
    self.ViewMgr:bindEvListener("EvEntityRefreshFriendList", self)
    self.ViewMgr:bindEvListener("EvEntityRefreshFriendInfo", self)
    self.ViewMgr:bindEvListener("EvEntityFriendOnlineStateChange", self)
    self.ViewMgr:bindEvListener("EvEntityMailListInit", self)
    self.ViewMgr:bindEvListener("EvEntityMailAdd", self)
    self.ViewMgr:bindEvListener("EvEntityMailDelete", self)
    self.ViewMgr:bindEvListener("EvEntityMailUpdate", self)
    self.ViewMgr:bindEvListener("EvUiClickVip", self)
    self.ViewMgr:bindEvListener("EvEntityGetLotteryTicketDataSuccess", self)
    self.ViewMgr:bindEvListener("EvEntityLotteryTicketGameEndStateSimple", self)
    self.ViewMgr:bindEvListener("EvEntityLotteryTicketUpdateTm", self)
    self.ViewMgr:bindEvListener("EvEntityRefreshLeftOnlineRewardTm", self)
    self.ViewMgr:bindEvListener("EvEntityCanGetOnlineReward", self)
    self.ViewMgr:bindEvListener("EvEntityCanGetTimingReward", self)
    self.ViewMgr:bindEvListener("EvEntityIsFirstRechargeChanged", self)
    self.ViewMgr:bindEvListener("EvEntityFriendGoldChange", self)
    self.ViewMgr:bindEvListener("EvRequestGetTimingReward", self)
    self.ViewMgr:bindEvListener("EvOnGetOnLineReward", self)
    self.ViewMgr:bindEvListener("EvEntityReceiveFeedbackChat", self)
    self.ViewMgr:bindEvListener("EvEntityReceiveFeedbackChats", self)
    self.ViewMgr:bindEvListener("EvEntityBagAddItem", self)

    self.PosGetChipEffectParticle = self.ComUi:GetChild("ComPosGetChipParticle").asCom.position
    local ab_particle_desktop = p_helper:GetParticel("btndesktopparticle.ab")
    local prefab_particle_desktop = ab_particle_desktop:LoadAsset("BtnDeskTopParticle")
    self.ParticleDeskTop = CS.UnityEngine.GameObject.Instantiate(prefab_particle_desktop)
    local holder_destopParticle = btn_desktop:GetChild("HolderParticle").asGraph
    holder_destopParticle:SetNativeObject(CS.FairyGUI.GoWrapper(self.ParticleDeskTop))

    local ab_particle_match = p_helper:GetParticel("btnmatchparticle.ab")
    local prefab_particle_match = ab_particle_match:LoadAsset("BtnMatchParticle")
    self.ParticleMatch = CS.UnityEngine.GameObject.Instantiate(prefab_particle_match)
    local holder_matchParticle = btn_match:GetChild("HolderParticle").asGraph
    holder_matchParticle:SetNativeObject(CS.FairyGUI.GoWrapper(self.ParticleMatch))

    local image_bg = self.ComUi:GetChild("ImageMote").asImage
    if (self.CasinosContext.NeedHideClientUi == false)
    then
        image_bg.visible = false
        local ab_mainmarry = p_helper:GetSpine("Spine/mainmarry.ab")
        local atlas = ab_mainmarry:LoadAsset("Mary.atlas")
        local texture = ab_mainmarry:LoadAsset("Mary")
        local json = ab_mainmarry:LoadAsset("MaryJson")

        self.PlayerAnim = CS.Casinos.SpineHelper.LoadResourcesPrefab(atlas, texture, json, "Spine/Skeleton")
        self.PlayerAnim.transform.localScale = CS.Casinos.LuaHelper.GetVector3(81, 81, 1000)
        self.PlayerAnim:Initialize(false)
        self.PlayerAnim.loop = true
        self.PlayerAnim.AnimationName = "animation"
        self.MoteRender = self.PlayerAnim.transform.gameObject:GetComponent("MeshRenderer")
        self.MoteRender.sortingOrder = 315
        self.HolderMote = self.ComUi:GetChild("HolderMote").asGraph
        self.HolderMote:SetNativeObject(CS.FairyGUI.GoWrapper(self.PlayerAnim.transform.gameObject))
    else
        image_bg.visible = true
        btn_match.visible = false
        self.ParticleMatch.transform.gameObject:SetActive(false)
    end

    local bg = self.ComUi:GetChild("Bg")
    if (bg ~= nil)
    then
        ViewHelper:makeUiBgFiteScreen(ViewMgr.STANDARD_WIDTH, ViewMgr.STANDARD_HEIGHT, self.ComUi.width, self.ComUi.height, bg.width, bg.height, bg, BgAttachMode.Center)
    end

    --self.ComHeadIconCenter = self.ComUi:GetChild("ComPosEnd").asCom
    local btn_console = self.ComUi:GetChild("BtnConsole").asButton
    btn_console.visible = self.ControllerPlayer.IsGm
    local com_console = self.ComUi:GetChild("ComConsole")
    btn_console.onClick:Add(
            function()
                if (com_console.visible == true)
                then
                    com_console.visible = false
                else
                    com_console.visible = true
                end
            end
    )
    local text_console = com_console:GetChild("TextConsole").asTextField
    local btn_send = com_console:GetChild("BtnSend").asButton
    btn_send.onClick:Add(
            function()
                local ev = self.ViewMgr:getEv("EvConsoleCmd")
                if (ev == nil)
                then
                    ev = EvConsoleCmd:new(nil)
                end
                local splitStr = LuaHelper:SplitStr(text_console.text, " ")
                ev.ListParam = splitStr
                self.ViewMgr:sendEv(ev)
            end
    )
    self.ChipIconSolustion = self.ComUi:GetController("ChipIconSolustion")
    self.ChipIconSolustion.selectedIndex = ChipIconSolustion

    local need_checkid = self.ControllerLogin:needCheckIdCard()
    if need_checkid then
        local id_card = self.ViewMgr:getView("IdCardCheck")
        if (id_card == nil)
        then
            id_card = self.ViewMgr:createView("IdCardCheck")

        end
    end
    self:setHaveFeedback()
end

---------------------------------------
function ViewMain:onUpdate(tm)
end

---------------------------------------
function ViewMain:onDestroy()
    if (self.CasinosContext.NeedHideClientUi == false)
    then
        CS.UnityEngine.GameObject.Destroy(self.PlayerAnim.transform.gameObject)
    end
    CS.UnityEngine.GameObject.Destroy(self.ParticleDeskTop)
    CS.UnityEngine.GameObject.Destroy(self.ParticleMatch)
    self.ViewMgr:unbindEvListener(self)
    local view_shootingtext = self.ViewMgr:getView("ShootingText")
    self.ViewMgr:destroyView(view_shootingtext)
end

---------------------------------------
function ViewMain:Close()
    self.GTransitionShow:PlayReverse()
end

---------------------------------------
function ViewMain:onHandleEv(ev)
    if (ev ~= nil)
    then
        if (ev.EventName == "EvEntityNotifyDeleteFriend")
        then
            if (ev.friend_etguid == self.CurrentFriendItem.PlayerInfoCommon.PlayerGuid)
            then
                self.CurrentFriendItem = nil
            end
            self:setFriendInfo(ev.map_friend)
        elseif (ev.EventName == "EvEntityGoldChanged")
        then
            self.UiPlayerInfoSelf:updatePlayerGold(UiChipShowHelper:getGoldShowStr(self.ControllerActor.PropGoldAcc:get(), self.ViewMgr.LanMgr.LanBase, true, 2))
        elseif (ev.EventName == "EvEntityDiamondChanged")
        then
            self.UiPlayerInfoSelf:updatePlayerDiamond(UiChipShowHelper:getGoldShowStr(self.ControllerActor.PropDiamond:get(), self.ViewMgr.LanMgr.LanBase, true, 2))
        elseif (ev.EventName == "EvEntityRecommendPlayerList")
        then
            self:setRecommandFriendInfo(ev.list_recommend)
        elseif (ev.EventName == "EvEntityPlayerInfoChanged")
        then
            self:setPlayerInfo()
        elseif (ev.EventName == "EvEntitySetOnLinePlayerNum")
        then
            self:setOnlineNum(ev.online_num)
        elseif (ev.EventName == "EvEntityReceiveFriendSingleChat")
        then
            if (ev.chat_msg.sender_guid ~= self.ControllerPlayer.Guid)
            then
                local all_unreadchat_count = self.ControllerIM.IMChat:getAllNewChatCount()
                self:setNewChatCount(all_unreadchat_count)
            end
        elseif (ev.EventName == "EvEntityReceiveFriendChats")
        then
            local all_unreadchat_count = self.ControllerIM.IMChat:getAllNewChatCount()
            self:setNewChatCount(all_unreadchat_count)
        elseif (ev.EventName == "EvEntityUnreadChatsChanged")
        then
            local all_unreadchat_count = self.ControllerIM.IMChat:getAllNewChatCount()
            self:setNewChatCount(all_unreadchat_count)
        elseif (ev.EventName == "EvGetPicUpLoadSuccess")
        then
            local icon = self.ControllerActor.PropAccountId:get()
            self.UiPlayerInfoSelf:refreshHeadIcon(self.ControllerActor.PropIcon:get(), icon)
        elseif (ev.EventName == "EvEntityRefreshFriendList")
        then
            if (self.CurrentFriendItem ~= nil)
            then
                local player_info = self.ControllerIM.IMFriendList:getFriendInfo(self.CurrentFriendItem.PlayerInfoCommon.PlayerGuid)
                self:refreshCurrentFriendInfo(player_info)
            end

            self:setFriendInfo(ev.map_friendinfo)
        elseif (ev.EventName == "EvEntityRefreshFriendInfo")
        then
            if (self.CurrentFriendItem ~= nil and self.CurrentFriendItem.PlayerInfoCommon.PlayerGuid == ev.player_info.PlayerInfoCommon.PlayerGuid)
            then
                self:refreshCurrentFriendInfo(ev.player_info)
            end
        elseif (ev.EventName == "EvEntityFriendGoldChange")
        then
            local friend_guid = ev.friend_guid
            local friend_gold = ev.current_gold
            if (self.CurrentFriendItem ~= nil and self.CurrentFriendItem.PlayerInfoCommon.PlayerGuid == friend_guid)
            then
                local str_gold = UiChipShowHelper:getGoldShowStr(friend_gold, self.ViewMgr.LanMgr.LanBase, true, 2)
                self.UiPlayerInfoCurrentFriend:updatePlayerGold(str_gold)
            end
        elseif (ev.EventName == "EvEntityFriendOnlineStateChange")
        then
            local player_guid = ev.player_info.PlayerInfoCommon.PlayerGuid
            if (self.CurrentFriendItem ~= nil and self.CurrentFriendItem.PlayerInfoCommon.PlayerGuid == player_guid)
            then
                self:refreshCurrentFriendInfo(ev.player_info)
            end

            local head_icon = nil
            if (self.MapFriend[player_guid] ~= nil)
            then
                head_icon = self.MapFriend[player_guid]
                local player_info = head_icon:getFriendInfo()
                if (player_info.PlayerInfoCommon.PlayerGuid == player_guid)
                then
                    head_icon:setFriendInfo(ev.player_info,
                            function(ev)
                                self:onClickFriendIcon(ev)
                            end
                    )
                end
            end
        elseif (ev.EventName == "EvEntityMailListInit")
        then
            self:setNewRecord()
        elseif (ev.EventName == "EvEntityMailAdd")
        then
            self:setNewRecord()
        elseif (ev.EventName == "EvEntityMailDelete")
        then
            self:setNewRecord()
        elseif (ev.EventName == "EvEntityMailUpdate")
        then
            self:setNewRecord()
        elseif (ev.EventName == "EvUiClickVip")
        then
            local player_info = self.ViewMgr:createView("PlayerInfo")
            player_info:setVIPInfo()
        elseif (ev.EventName == "EvEntityGetLotteryTicketDataSuccess")
        then
            self:setLotteryTicketInfo(ev.lotteryticket_data.State, ev.lotteryticket_data.StateLeftTm)
        elseif (ev.EventName == "EvEntityLotteryTicketGameEndStateSimple")
        then
            self.GTextLotteryTicketTips.text = self.ViewMgr.LanMgr:getLanValue("Settlement")
        elseif (ev.EventName == "EvEntityLotteryTicketUpdateTm")
        then
            self:updateLotteryTickTm(ev.tm)
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
        elseif (ev.EventName == "EvEntityIsFirstRechargeChanged")
        then
            local com_recharge_first = self.ComUi:GetChild("ComRechargeFirst").asCom
            com_recharge_first.visible = false
            self.BtnRegister.position = com_recharge_first.position
        elseif (ev.EventName == "EvRequestGetTimingReward" or ev.EventName == "EvOnGetOnLineReward")
        then
            self.ComShadeReward.visible = false
            self.TransitionShowReward:PlayReverse()
        elseif (ev.EventName == "EvEntityReceiveFeedbackChat")
        then
            self:setHaveFeedback()
        elseif (ev.EventName == "EvEntityReceiveFeedbackChats")
        then
            self:setHaveFeedback()
        elseif (ev.EventName == "EvEntityBagAddItem")
        then
            self:setNewItem()
        end
    end
end

---------------------------------------
function ViewMain:updateLotteryTickTm(tm)
    if (tm > 0)
    then
        self.GTextLotteryTicketTips.text = tm .. self.ViewMgr.LanMgr:getLanValue("S")
    else
        self.GTextLotteryTicketTips.text = self.ViewMgr.LanMgr:getLanValue("Settlement")
    end
end

---------------------------------------
function ViewMain:setPlayerInfo()
    local name = self.ControllerActor.PropNickName:get()
    local gold_acc = self.ControllerActor.PropGoldAcc:get()
    local diamond = self.ControllerActor.PropDiamond:get()

    local str_goldacc = UiChipShowHelper:getGoldShowStr(gold_acc, self.ViewMgr.LanMgr.LanBase, true, 2)
    local str_diamond = UiChipShowHelper:getGoldShowStr(diamond, self.ViewMgr.LanMgr.LanBase, true, 2)
    local icon = self.ControllerActor.PropIcon:get()
    local acc_id = self.ControllerActor.PropAccountId:get()
    local vip_level = self.ControllerActor.PropVIPLevel:get()
    self.UiPlayerInfoSelf:setPlayerInfo(name, str_goldacc, str_diamond, icon, acc_id, vip_level, true)
end

---------------------------------------
function ViewMain:setOnlineNum(num)
    local online_player = self.ViewMgr.LanMgr:getLanValue("OnlinePlayer")
    local tip = self.CasinosContext:AppendStrWithSB(tostring(num), " ", online_player)
    self.GTextPlayerNum.text = tip
end

---------------------------------------
function ViewMain:setFriendInfo(map_friend)
    self.ListFriendInfo = {}
    self.MapFriend = {}
    local lua_helper = LuaHelper:new(nil)
    if (map_friend == nil or lua_helper:GetTableCount(map_friend) == 0)
    then
        self.UiPlayerInfoCurrentFriend:hidePlayerInfo(true)
        self.BtnInviteFriend.visible = true
        self.ListHeadIconFriend = {}
        self.GListFriend.numItems = 0
        return
    end

    self.BtnInviteFriend.visible = false
    for key, value in pairs(map_friend) do
        table.insert(self.ListFriendInfo, value)
    end
    --table.sort(self.ListFriendInfo,
    --        function(f1, f2)
    --            local sort_f1 = CS.Casinos.LuaHelper.EnumCastToInt(f1.PlayerInfoMore.OnlineState)
    --            local sort_f2 = CS.Casinos.LuaHelper.EnumCastToInt(f2.PlayerInfoMore.OnlineState)
    --            if (sort_f1 ~= sort_f2)
    --            then
    --                return sort_f1 > sort_f2
    --            else
    --                if (f1.PlayerInfoMore.OnlineState == PlayerOnlineState.Online)
    --                then
    --                    local player_playstate1 = f1.PlayerPlayState
    --                    local player_playstate2 = f2.PlayerPlayState
    --                    local is_playindesk1 = player_playstate1 ~= nil and (player_playstate1.DesktopGuid ~= nil and player_playstate1.DesktopGuid ~= "")
    --                    local is_playindesk2 = player_playstate2 ~= nil and (player_playstate2.DesktopGuid ~= nil and player_playstate2.DesktopGuid ~= "")
    --                    if (is_playindesk1)
    --                    then
    --                        sort_f1 = 1
    --                    else
    --                        sort_f1 = -1
    --                    end
    --                    if (is_playindesk2)
    --                    then
    --                        sort_f2 = 1
    --                    else
    --                        sort_f2 = -1
    --                    end
    --                    if (sort_f1 ~= sort_f2)
    --                    then
    --                        return sort_f1 > sort_f2
    --                    else
    --                        return f1.PlayerInfoMore.Gold > f2.PlayerInfoMore.Gold
    --                    end
    --                else
    --                    return f1.PlayerInfoMore.Gold > f2.PlayerInfoMore.Gold
    --                end
    --            end
    --        end
    --)

    self.ListHeadIconFriend = {}
    self.GListFriend.numItems = #self.ListFriendInfo
end

---------------------------------------
function ViewMain:setNewChatCount(chat_count)
    self.NewFriendChatCount = chat_count
    self:setNewRecord()
end

---------------------------------------
function ViewMain:setRecommandFriendInfo(list_friend)
    self.GLoaderWaiting.visible = false
    self.ListFriendInfoRecommend = {}
    for key, value in pairs(list_friend) do
        table.insert(self.ListFriendInfoRecommend, value)
    end
    self.GListRecommend.numItems = #self.ListFriendInfoRecommend
end

---------------------------------------
function ViewMain:setCurrentFriendInfo(friend_item)
    self.CurrentFriendItem = friend_item
    local item_ico = ""
    local icon_resource_name = ""
    if (self.CurrentFriendItem == nil)
    then
        self.BtnInviteFriend.visible = true
        self.UiPlayerInfoCurrentFriend:hidePlayerInfo(true)
        return
    end

    self.BtnInviteFriend.visible = false
    local friend_state = self.ControllerIM.IMFriendList:getFriendStateStr(self.CurrentFriendItem.PlayerInfoCommon.PlayerGuid)
    self.UiPlayerInfoCurrentFriend:setPlayerInfo(self.CurrentFriendItem.PlayerInfoCommon.NickName,
            UiChipShowHelper:getGoldShowStr(self.CurrentFriendItem.PlayerInfoMore.Gold, self.ViewMgr.LanMgr.LanBase, true, 2),
            friend_state, self.CurrentFriendItem.PlayerInfoCommon.IconName, self.CurrentFriendItem.PlayerInfoCommon.AccountId,
            self.CurrentFriendItem.PlayerInfoCommon.VIPLevel,
            self.CurrentFriendItem.PlayerInfoMore.OnlineState == PlayerOnlineState.Online)
end

---------------------------------------
function ViewMain:setLotteryTicketInfo(state, left_tm)
    local tips = ""
    if (state == LotteryTicketStateEnum.Bet)
    then
        tips = math.ceil(left_tm) .. self.ViewMgr.LanMgr:getLanValue("S")
    else
        tips = self.ViewMgr.LanMgr:getLanValue("Settlement")
    end
    self.GTextLotteryTicketTips.text = tips
end

---------------------------------------
function ViewMain:setNewRecord()
    local have_newMail = false
    if (self.ControllerIM:haveNewMail())
    then
        have_newMail = true
    end

    if (have_newMail == false)
    then
        ViewHelper:setGObjectVisible(false, self.ComMailTips)
    else
        ViewHelper:setGObjectVisible(true, self.ComMailTips)
        if (self.TransitionNewMail.playing == false)
        then
            self.TransitionNewMail:Play()
        end

        self.CasinosContext:Play(self.NewMsgSound, CS.Casinos._eSoundLayer.LayerReplace)
    end

    local have_newMsg = false
    if (self.NewFriendChatCount > 0)
    then
        have_newMsg = true
    end

    if (have_newMsg == false)
    then
        ViewHelper:setGObjectVisible(false, self.ComMsgTips)
    else
        ViewHelper:setGObjectVisible(true, self.ComMsgTips)
        self.GTextMsgNum.text = tostring(self.NewFriendChatCount)
        if (self.TransitionNewMsg.playing == false)
        then
            self.TransitionNewMsg:Play()
        end
        self.CasinosContext:Play(self.NewMsgSound, CS.Casinos._eSoundLayer.LayerReplace)
    end
end

---------------------------------------
function ViewMain:setNewReward()
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

---------------------------------------
function ViewMain:setHaveFeedback()
    local have = self.ControllerIM.IMFeedback.HaveNewMsg
    if (have == false)
    then
        ViewHelper:setGObjectVisible(false, self.ComFeedbackTips)
    else
        ViewHelper:setGObjectVisible(true, self.ComFeedbackTips)
        if (self.TransitionFeedbackTips.playing == false)
        then
            self.TransitionFeedbackTips:Play()
        end
    end
    self:haveMoreTips()
end

---------------------------------------
function ViewMain:setNewItem()
    local have = self.ControllerBag.HaveNewItem
    if (have == false)
    then
        ViewHelper:setGObjectVisible(false, self.ComBagTips)
    else
        ViewHelper:setGObjectVisible(true, self.ComBagTips)
        if (self.TransitionBagTips.playing == false)
        then
            self.TransitionBagTips:Play()
        end
    end
    self:haveMoreTips()
end

---------------------------------------
function ViewMain:haveMoreTips()
    local have_msg = self.ControllerIM.IMFeedback.HaveNewMsg
    local have_item = self.ControllerBag.HaveNewItem
    if (have_msg or have_item)
    then
        ViewHelper:setGObjectVisible(true, self.ComMoreTips)
        if (self.TransitionComMore.playing == false)
        then
            self.TransitionComMore:Play()
        end
    else
        ViewHelper:setGObjectVisible(false, self.ComMoreTips)
    end
end

---------------------------------------
function ViewMain:onClickComForestParty()
    local ev = self.ViewMgr:getEv("EvUiForestPartyEnterDesktop")
    if (ev == nil)
    then
        ev = EvUiForestPartyEnterDesktop:new(nil)
    end
end

---------------------------------------
function ViewMain:onClickRecommendFriend(ev)
    local item_friend_recommend = nil
    local sender = CS.Casinos.LuaHelper.EventDispatcherCastToGComponent(ev.sender)
    for key, value in pairs(self.ListHeadIconRecommond) do
        if (value.Com == sender)
        then
            item_friend_recommend = value
        end
    end
    local ev = self.ViewMgr:getEv("EvUiClickChooseFriend")
    if (ev == nil)
    then
        ev = EvUiClickChooseFriend:new(nil)
    end
    ev.friend_info = item_friend_recommend:getFriendInfo()
    ev.is_choosechat = false
    ev.is_recommand = true
    self.ViewMgr:sendEv(ev)
end

---------------------------------------
function ViewMain:onClickBtnLotteryTicket()
    local ev = self.ViewMgr:getEv("EvEntityRequestGetLotteryTicketData")
    if (ev == nil)
    then
        ev = EvEntityRequestGetLotteryTicketData:new(nil)
    end
    self.ViewMgr:sendEv(ev)
    self.ViewMgr:createView("LotteryTicket")
end

---------------------------------------
function ViewMain:onClickFriendIcon(ev)
    local item_friend = nil
    local sender = CS.Casinos.LuaHelper.EventDispatcherCastToGComponent(ev.sender)
    for key, value in pairs(self.ListHeadIconFriend) do
        if (value.Com == sender)
        then
            item_friend = value
        end
    end
    local friend_info = item_friend:getFriendInfo()
    if (friend_info.PlayerInfoCommon.PlayerGuid == self.CurrentFriendItem.PlayerInfoCommon.PlayerGuid)
    then
        return
    end
    self:setCurrentFriendInfo(friend_info)
end

---------------------------------------
function ViewMain:onClickComPlayerInfoCurrentFriend()
    local is_recommand = false

    if (self.CurrentFriendItem == nil)
    then
        is_recommand = true
    end

    local ev = self.ViewMgr:getEv("EvUiClickChooseFriend")
    if (ev == nil)
    then
        ev = EvUiClickChooseFriend:new(nil)
    end
    ev.friend_info = self.CurrentFriendItem
    ev.is_choosechat = false
    ev.is_recommand = is_recommand
    self.ViewMgr:sendEv(ev)
end

---------------------------------------
function ViewMain:onClickComPlayerInfoSelf()
    local player_info = self.ViewMgr:createView("PlayerInfo")
end

---------------------------------------
function ViewMain:onClickBtnShop()
    local ev = self.ViewMgr:getEv("EvUiClickShop")
    if (ev == nil)
    then
        ev = EvUiClickShop:new(nil)
    end
    self.ViewMgr:sendEv(ev)
end

---------------------------------------
function ViewMain:onClickBtnPurse()
    self.ViewMgr:createView("Purse")
end

---------------------------------------
function ViewMain:onClickBtnFriend()
    local ev = self.ViewMgr:getEv("EvUiClickFriend")
    if (ev == nil)
    then
        ev = EvUiClickFriend:new(nil)
    end
    self.ViewMgr:sendEv(ev)
end

---------------------------------------
function ViewMain:onClickBtnMail()
    self.ViewMgr:createView("Mail")
end

---------------------------------------
function ViewMain:onClickBtnDeskTop()
    self:hideMote()
    self.GTransitionShow:PlayReverse(
            function()
                local view_lobby = self.ViewMgr:createView("ClassicModel")
                view_lobby:setLobbyModel()
            end
    )
end

---------------------------------------
function ViewMain:onClickBtnDesktopH()
    self:hideMote()
    self.GTransitionShow:PlayReverse(
            function()
                local ev = self.ViewMgr:getEv("EvUiClickDesktopHundred")
                if (ev == nil)
                then
                    ev = EvUiClickDesktopHundred:new(nil)
                end
                ev.factory_name = "Texas"
                self.ViewMgr:sendEv(ev)
            end
    )
end

---------------------------------------
function ViewMain:onClickBtnMatch()
    self:hideMote()
    self.GTransitionShow:PlayReverse(
            function()
                self.ViewMgr:createView("MatchLobby")
            end
    )
end

---------------------------------------
function ViewMain:onClickBtnCircleOfCardFriends()
    self:hideMote()
    self.GTransitionShow:PlayReverse(
            function()
                self.ViewMgr:createView("Club")
            end
    )
end

---------------------------------------
function ViewMain:onClickBtnRanking()
    local view_ranking = self.ViewMgr:getView("Ranking")
    if (view_ranking == nil)
    then
        self.ViewMgr:createView("Ranking")
    else
        self.ViewMgr:destroyView(view_ranking)
    end
end

---------------------------------------
function ViewMain:onClickBtnMore()
    local index = self.ControllerMoreBtn.selectedIndex
    if (index == 0)
    then
        self.ControllerMoreBtn.selectedIndex = 1
        self.ComShadeMore.visible = true
        self.TransitionShowMore:Play()
    else
        self.ControllerMoreBtn.selectedIndex = 0
        self.ComShadeMore.visible = false
        self.TransitionShowMore:PlayReverse()
    end
end

---------------------------------------
function ViewMain:onClickBtnSet()
    local view_edit = self.ViewMgr:getView("Edit")
    if (view_edit == nil)
    then
        self.ViewMgr:createView("Edit")
    else
        self.ViewMgr:destroyView(view_edit)
    end
end

---------------------------------------
function ViewMain:onClickBtnGoldTree()
    self.ViewMgr:createView("GoldTree")
end

---------------------------------------
function ViewMain:onClickBtnWa()
    ViewHelper:UiShowInfoSuccess("施工中，敬请期待")
end

---------------------------------------
function ViewMain:onClickBtnChat()
    self.ViewMgr:createView("Bag")
    local ev = self.ViewMgr:getEv("EvOpenBag")
    if (ev == nil)
    then
        ev = EvOpenBag:new(nil)
    end
    self.ViewMgr:sendEv(ev)
    self:setNewItem()
end

---------------------------------------
function ViewMain:onClickBtnChatFriend()
    local ev = self.ViewMgr:getEv("EvUiClickChatmsg")
    if (ev == nil)
    then
        ev = EvUiClickChatmsg:new(nil)
    end
    self.ViewMgr:sendEv(ev)
end

---------------------------------------
function ViewMain:onClickBtnBank()
    self.ViewMgr:createView("Bank")
end

---------------------------------------
function ViewMain:onClickBtnFeedback()
    ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("Testing"))
end

---------------------------------------
function ViewMain:onClickComRechargeFirst()
    self.ViewMgr:createView("RechargeFirst")
end

---------------------------------------
function ViewMain:rendererFriend(index, item)
    local l = #self.ListFriendInfo
    if (l > index)
    then
        local friend_info = self.ListFriendInfo[index + 1]
        local head_icon = ItemHeadIcon:new(nil, item)
        head_icon:setFriendInfo(friend_info,
                function(ev)
                    self:onClickFriendIcon(ev)
                end
        )
        self.MapFriend[friend_info.PlayerInfoCommon.PlayerGuid] = head_icon
        if (self.CurrentFriendItem == nil)
        then
            self.CurrentFriendItem = friend_info
            self:setCurrentFriendInfo(self.CurrentFriendItem)
        end
        table.insert(self.ListHeadIconFriend, head_icon)
    end
end

---------------------------------------
function ViewMain:rendererFriendRecommend(index, item)
    if (#self.ListFriendInfoRecommend > index)
    then
        local friend_info = self.ListFriendInfoRecommend[index + 1]
        local head_icon = ItemHeadIcon:new(nil, item)
        head_icon:setFriendInfo(friend_info,
                function(ev)
                    self:onClickRecommendFriend(ev)
                end
        )
        table.insert(self.ListHeadIconRecommond, head_icon)
    end
end

---------------------------------------
function ViewMain:refreshCurrentFriendInfo(player_info)
    if (player_info ~= nil)
    then
        self.CurrentFriendItem = player_info
        local friend_state = self.ControllerIM.IMFriendList:getFriendStateStr(self.CurrentFriendItem.PlayerInfoCommon.PlayerGuid)
        self.UiPlayerInfoCurrentFriend:setPlayerInfo1(player_info.PlayerInfoCommon.NickName,
                UiChipShowHelper:getGoldShowStr(player_info.PlayerInfoMore.Gold, self.ViewMgr.LanMgr.LanBase),
                friend_state,
                player_info.PlayerInfoMore.OnlineState == PlayerOnlineState.Online)
    end
end

---------------------------------------
function ViewMain:onClickBtnActivity()
    self.ViewMgr:createView("ActivityCenter")
end

---------------------------------------
function ViewMain:hideMote()
    if (self.CasinosContext.NeedHideClientUi == false)
    then
        self.MoteRender.transform.gameObject:SetActive(false)
    end
end

---------------------------------------
function ViewMain:ShowGetChipEffect()
    --local view_getChipEffect = self.ViewMgr:createView("GetChipEffect")
    --local pos = self.ComUi:TransformPoint(CS.UnityEngine.Vector2(self.ComHeadIconCenter.position.x,self.ComHeadIconCenter.position.y),view_getChipEffect.ComUi)
    --view_getChipEffect:ShowEffect(nil,pos)
end

---------------------------------------
ViewMainFactory = ViewFactory:new()

---------------------------------------
function ViewMainFactory:new(o, ui_package_name, ui_component_name, ui_layer, is_single, fit_screen)
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
function ViewMainFactory:createView()
    local view = ViewMain:new(nil)
    return view
end