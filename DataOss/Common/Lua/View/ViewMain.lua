-- Copyright(c) Cragon. All rights reserved.
-- 与ControllerPlayer对应，响应福利按钮消息（ViewDesktop也响应福利消息）
-- 好友列表
-- 推荐好友列表
-- 本人信息
-- 3个主按钮(粒子特效)，右下方一排功能按钮
-- 父层小红点处理，首次查询+后续消息响应(响应小红点相关的业务消息)

---------------------------------------
ViewMain = class(ViewBase)

---------------------------------------
function ViewMain:ctor()
    self.Context = Context
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.PokerGirlMain = "PokerGirMain"
    self.NewFriendChatCount = 0
end

---------------------------------------
function ViewMain:OnCreate()
    self.GTransitionShow = self.ComUi:GetTransition("TransitionShow")
    self.GTransitionShow:Play()
    self.ListFriendInfo = {}
    self.ListFriendInfoRecommend = {}
    self.MapFriend = {}
    self.ListHeadIconFriend = {}
    self.ListHeadIconRecommond = {}
    self.ControllerActor = self.ControllerMgr:GetController("Actor")
    self.ControllerPlayer = self.ControllerMgr:GetController("Player")
    self.ControllerIM = self.ControllerMgr:GetController("IM")
    self.ControllerBag = self.ControllerMgr:GetController("Bag")
    self.ControllerLotteryTicket = self.ControllerMgr:GetController("LotteryTicket")
    self.ControllerLotteryTicket = self.ControllerMgr:GetController("LotteryTicket")
    self.ControllerLogin = self.ControllerMgr:GetController("Login")
    local btn_desktop = self.ComUi:GetChild("BtnDeskTop").asButton
    btn_desktop.onClick:Add(
            function()
                self:onClickBtnDesktop()
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
    if (self.Context.Cfg.ClientShowGoldTree == true and self.ControllerActor.PropEnableGrow:get() == true) then
        show_goldtree = true
    end
    ViewHelper:SetGObjectVisible(show_goldtree, btn_goldtree)

    --local gold_par_parent = btn_goldtree:GetChild("GoldTreeParParent").asGraph
    --local g_t_particle = ParticleHelper:GetParticel("goldtreepar.ab")
    --local prefab_g_t_particle = g_t_particle:LoadAsset("GoldTreePar")
    --local g_p = CS.UnityEngine.GameObject.Instantiate(prefab_g_t_particle)
    --gold_par_parent:SetNativeObject(CS.FairyGUI.GoWrapper(g_p))

    local com_more = nil
    if (self.Context.Cfg.NeedHideClientUi) then
        com_more = self.ComUi:GetChild("ComMoreHideRank").asCom
        btn_match.visible = false
    else
        btn_match.visible = true
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
    c_m_s.selectedIndex = self.Context.Cfg.ChipIconSolustion

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
    if self.Context.Cfg.ChipIconSolustion == 0 then
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
                            self.CasinosContext.NetMgr:Disconnect()
                            local login = self.ViewMgr:GetView("Login")
                            if login ~= nil then
                                login:_onClickBtnShowRegister()
                            end
                        end)
            end)
    local show_register = false
    if self.CasinosContext.LoginType == CS.Casinos._eLoginType.Guest then
        show_register = true
    end
    ViewHelper:SetGObjectVisible(show_register, self.BtnRegister)

    local is_firstrecharge = self.ControllerActor.PropIsFirstRecharge:get()
    local com_recharge_first = self.ComUi:GetChild("ComRechargeFirst").asCom
    com_recharge_first.onClick:Add(
            function()
                self:onClickComRechargeFirst()
            end
    )
    local show_first = false
    if (self.Context.Cfg.ClientShowFirstRecharge == true and is_firstrecharge == true) then
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

    self.ComRewardTips = self.ComUi:GetChild("ComRewardTips").asCom
    self.TransitionNewReward = self.ComRewardTips:GetTransition("TransitionNewMsg")
    self.TransitionShowReward = self.ComUi:GetTransition("TransitionShowReward")
    self:RefreshRedPointRewardState()
    self.ComWelfareIcon = self.ComUi:GetChild("ComWelfareIcon").asCom
    self.ComWelfareIcon.onClick:Add(
            function()
                self.ViewMgr:CreateView('Reward')
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
    c_p.selectedIndex = self.Context.Cfg.ChipIconSolustion
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
    c_f.selectedIndex = self.Context.Cfg.ChipIconSolustion
    local controller_showdiamondIcon = com_friendInfo:GetController("ControllerShowDiamondIcon")
    controller_showdiamondIcon:SetSelectedIndex(1)
    self.UiPlayerInfoCurrentFriend = UiMainPlayerInfo:new(com_friendInfo, true,
            function()
                self:onClickComPlayerInfoCurrentFriend()
            end
    )
    self.TransitionShowMore = self.ComUi:GetTransition("TansitionShowComMore")
    self.GTextPlayerNum = self.ComUi:GetChild("TextPlayerNum").asTextField
    ViewHelper:SetGObjectVisible(false, self.GTextPlayerNum)
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
    local view_shootingtext = self.ViewMgr:GetView("ShootingText")
    if (view_shootingtext == nil) then
        view_shootingtext = self.ViewMgr:CreateView("ShootingText")
        view_shootingtext:init(true, false, true)
    end

    self.ViewMgr:BindEvListener("EvEntityNotifyDeleteFriend", self)
    self.ViewMgr:BindEvListener("EvEntityGoldChanged", self)
    self.ViewMgr:BindEvListener("EvEntityDiamondChanged", self)
    self.ViewMgr:BindEvListener("EvEntityRecommendPlayerList", self)
    self.ViewMgr:BindEvListener("EvEntityPlayerInfoChanged", self)
    self.ViewMgr:BindEvListener("EvEntitySetOnLinePlayerNum", self)
    self.ViewMgr:BindEvListener("EvEntityReceiveFriendSingleChat", self)
    self.ViewMgr:BindEvListener("EvEntityReceiveFriendChats", self)
    self.ViewMgr:BindEvListener("EvEntityUnreadChatsChanged", self)
    self.ViewMgr:BindEvListener("EvGetPicUpLoadSuccess", self)
    self.ViewMgr:BindEvListener("EvEntityRefreshFriendList", self)
    self.ViewMgr:BindEvListener("EvEntityRefreshFriendInfo", self)
    self.ViewMgr:BindEvListener("EvEntityFriendOnlineStateChange", self)
    self.ViewMgr:BindEvListener("EvEntityMailListInit", self)
    self.ViewMgr:BindEvListener("EvEntityMailAdd", self)
    self.ViewMgr:BindEvListener("EvEntityMailDelete", self)
    self.ViewMgr:BindEvListener("EvEntityMailUpdate", self)
    self.ViewMgr:BindEvListener("EvUiClickVip", self)
    self.ViewMgr:BindEvListener("EvEntityGetLotteryTicketDataSuccess", self)
    self.ViewMgr:BindEvListener("EvEntityLotteryTicketGameEndStateSimple", self)
    self.ViewMgr:BindEvListener("EvEntityLotteryTicketUpdateTm", self)
    self.ViewMgr:BindEvListener("EvEntityIsFirstRechargeChanged", self)
    self.ViewMgr:BindEvListener("EvEntityFriendGoldChange", self)
    self.ViewMgr:BindEvListener("EvEntityReceiveFeedbackChat", self)
    self.ViewMgr:BindEvListener("EvEntityReceiveFeedbackChats", self)
    self.ViewMgr:BindEvListener("EvEntityBagAddItem", self)
    self.ViewMgr:BindEvListener("EvCtrlRewardRedPointStateChange", self)

    self.PosGetChipEffectParticle = self.ComUi:GetChild("ComPosGetChipParticle").asCom.position
    local ab_particle_desktop = ParticleHelper:GetParticel("btndesktopparticle.ab")
    local prefab_particle_desktop = ab_particle_desktop:LoadAsset("BtnDeskTopParticle")
    self.ParticleDeskTop = CS.UnityEngine.GameObject.Instantiate(prefab_particle_desktop)
    local holder_destopParticle = btn_desktop:GetChild("HolderParticle").asGraph
    holder_destopParticle:SetNativeObject(CS.FairyGUI.GoWrapper(self.ParticleDeskTop))

    local ab_particle_match = ParticleHelper:GetParticel("btnmatchparticle.ab")
    local prefab_particle_match = ab_particle_match:LoadAsset("BtnMatchParticle")
    self.ParticleMatch = CS.UnityEngine.GameObject.Instantiate(prefab_particle_match)
    local holder_matchParticle = btn_match:GetChild("HolderParticle").asGraph
    holder_matchParticle:SetNativeObject(CS.FairyGUI.GoWrapper(self.ParticleMatch))

    local image_bg = self.ComUi:GetChild("ImageMote").asImage
    if (self.Context.Cfg.NeedHideClientUi == false) then
        image_bg.visible = false
        local ab_path_prefix = self.CasinosContext.PathMgr.DirAbRoot .. 'Spine/'
        local loadingmarry_anim = self.CasinosContext.SpineMgr:CreateSpineObjFromAb(ab_path_prefix, 'MainMarry', 'Mary.atlas', 'Mary', 'MaryJson', 'Spine/Skeleton')
        loadingmarry_anim.transform.localScale = CS.Casinos.LuaHelper.GetVector3(81, 81, 1000)
        loadingmarry_anim:Initialize(false)
        loadingmarry_anim.loop = true
        loadingmarry_anim.AnimationName = "animation"
        loadingmarry_anim.transform.gameObject.name = "LoadingMote"
        local loadingmarry_render = loadingmarry_anim.transform.gameObject:GetComponent("MeshRenderer")
        loadingmarry_render.sortingOrder = 315
        local loadingmarry_holder = self.ComUi:GetChild("HolderMote").asGraph
        loadingmarry_holder:SetNativeObject(CS.FairyGUI.GoWrapper(loadingmarry_anim.transform.gameObject))
    else
        image_bg.visible = true
        btn_match.visible = false
        self.ParticleMatch.transform.gameObject:SetActive(false)
    end

    local bg = self.ComUi:GetChild("Bg")
    if (bg ~= nil) then
        ViewHelper:MakeUiBgFiteScreen(ViewMgr.STANDARD_WIDTH, ViewMgr.STANDARD_HEIGHT, self.ComUi.width, self.ComUi.height, bg.width, bg.height, bg, BgAttachMode.Center)
    end

    local btn_console = self.ComUi:GetChild("BtnConsole").asButton
    btn_console.visible = self.ControllerPlayer.IsGm
    local com_console = self.ComUi:GetChild("ComConsole")
    btn_console.onClick:Add(
            function()
                if (com_console.visible == true) then
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
                local ev = self.ViewMgr:GetEv("EvConsoleCmd")
                if (ev == nil) then
                    ev = EvConsoleCmd:new(nil)
                end
                local splitStr = LuaHelper:SplitStr(text_console.text, " ")
                ev.ListParam = splitStr
                self.ViewMgr:SendEv(ev)
            end
    )
    self.ChipIconSolustion = self.ComUi:GetController("ChipIconSolustion")
    self.ChipIconSolustion.selectedIndex = self.Context.Cfg.ChipIconSolustion

    local need_checkid = self.ControllerLogin:NeedCheckIdCard()
    if need_checkid then
        local id_card = self.ViewMgr:GetView("IdCardCheck")
        if (id_card == nil) then
            id_card = self.ViewMgr:CreateView("IdCardCheck")
        end
    end
    self:RefreshNewFeedback()
    self:RefreshNewRecord()
    self:RefreshNewItem()
end

---------------------------------------
function ViewMain:OnDestroy()
    if (self.Context.Cfg.NeedHideClientUi == false) then
        --CS.UnityEngine.GameObject.Destroy(self.PlayerAnim.transform.gameObject)
    end
    CS.UnityEngine.GameObject.Destroy(self.ParticleDeskTop)
    CS.UnityEngine.GameObject.Destroy(self.ParticleMatch)
    self.ViewMgr:UnbindEvListener(self)
    local view_shootingtext = self.ViewMgr:GetView("ShootingText")
    self.ViewMgr:DestroyView(view_shootingtext)
end

---------------------------------------
function ViewMain:OnHandleEv(ev)
    if (ev ~= nil) then
        if (ev.EventName == "EvEntityNotifyDeleteFriend") then
            if (ev.friend_etguid == self.CurrentFriendItem.PlayerInfoCommon.PlayerGuid) then
                self.CurrentFriendItem = nil
            end
            self:setFriendInfo(ev.map_friend)
        elseif (ev.EventName == "EvEntityGoldChanged") then
            self.UiPlayerInfoSelf:updatePlayerGold(UiChipShowHelper:GetGoldShowStr(self.ControllerActor.PropGoldAcc:get(), self.ViewMgr.LanMgr.LanBase, true, 2))
        elseif (ev.EventName == "EvEntityDiamondChanged") then
            self.UiPlayerInfoSelf:updatePlayerDiamond(UiChipShowHelper:GetGoldShowStr(self.ControllerActor.PropDiamond:get(), self.ViewMgr.LanMgr.LanBase, true, 2))
        elseif (ev.EventName == "EvEntityRecommendPlayerList") then
            self:setRecommandFriendInfo(ev.list_recommend)
        elseif (ev.EventName == "EvEntityPlayerInfoChanged") then
            self:SetPlayerInfo()
        elseif (ev.EventName == "EvEntitySetOnLinePlayerNum") then
            self:setOnlineNum(ev.online_num)
        elseif (ev.EventName == "EvEntityReceiveFriendSingleChat") then
            if (ev.chat_msg.sender_guid ~= self.ControllerPlayer.Guid) then
                local all_unreadchat_count = self.ControllerIM.IMChat:getAllNewChatCount()
                self:setNewChatCount(all_unreadchat_count)
            end
        elseif (ev.EventName == "EvEntityReceiveFriendChats") then
            local all_unreadchat_count = self.ControllerIM.IMChat:getAllNewChatCount()
            self:setNewChatCount(all_unreadchat_count)
        elseif (ev.EventName == "EvEntityUnreadChatsChanged") then
            local all_unreadchat_count = self.ControllerIM.IMChat:getAllNewChatCount()
            self:setNewChatCount(all_unreadchat_count)
        elseif (ev.EventName == "EvGetPicUpLoadSuccess") then
            local icon = self.ControllerActor.PropAccountId:get()
            self.UiPlayerInfoSelf:refreshHeadIcon(self.ControllerActor.PropIcon:get(), icon)
        elseif (ev.EventName == "EvEntityRefreshFriendList") then
            if (self.CurrentFriendItem ~= nil) then
                local player_info = self.ControllerIM.IMFriendList:getFriendInfo(self.CurrentFriendItem.PlayerInfoCommon.PlayerGuid)
                self:refreshCurrentFriendInfo(player_info)
            end
            self:setFriendInfo(ev.map_friendinfo)
        elseif (ev.EventName == "EvEntityRefreshFriendInfo") then
            if (self.CurrentFriendItem ~= nil and self.CurrentFriendItem.PlayerInfoCommon.PlayerGuid == ev.player_info.PlayerInfoCommon.PlayerGuid) then
                self:refreshCurrentFriendInfo(ev.player_info)
            end
        elseif (ev.EventName == "EvEntityFriendGoldChange") then
            local friend_guid = ev.friend_guid
            local friend_gold = ev.current_gold
            if (self.CurrentFriendItem ~= nil and self.CurrentFriendItem.PlayerInfoCommon.PlayerGuid == friend_guid) then
                local str_gold = UiChipShowHelper:GetGoldShowStr(friend_gold, self.ViewMgr.LanMgr.LanBase, true, 2)
                self.UiPlayerInfoCurrentFriend:updatePlayerGold(str_gold)
            end
        elseif (ev.EventName == "EvEntityFriendOnlineStateChange") then
            local player_guid = ev.player_info.PlayerInfoCommon.PlayerGuid
            if (self.CurrentFriendItem ~= nil and self.CurrentFriendItem.PlayerInfoCommon.PlayerGuid == player_guid) then
                self:refreshCurrentFriendInfo(ev.player_info)
            end

            local head_icon = nil
            if (self.MapFriend[player_guid] ~= nil) then
                head_icon = self.MapFriend[player_guid]
                local player_info = head_icon:getFriendInfo()
                if (player_info.PlayerInfoCommon.PlayerGuid == player_guid) then
                    head_icon:setFriendInfo(ev.player_info,
                            function(ev)
                                self:onClickFriendIcon(ev)
                            end
                    )
                end
            end
        elseif (ev.EventName == "EvEntityMailListInit") then
            self:RefreshNewRecord()
        elseif (ev.EventName == "EvEntityMailAdd") then
            self:RefreshNewRecord()
        elseif (ev.EventName == "EvEntityMailDelete") then
            self:RefreshNewRecord()
        elseif (ev.EventName == "EvEntityMailUpdate") then
            self:RefreshNewRecord()
        elseif (ev.EventName == "EvUiClickVip") then
            local player_info = self.ViewMgr:CreateView("PlayerInfo")
            player_info:setVIPInfo()
        elseif (ev.EventName == "EvEntityGetLotteryTicketDataSuccess") then
            self:setLotteryTicketInfo(ev.lotteryticket_data.State, ev.lotteryticket_data.StateLeftTm)
        elseif (ev.EventName == "EvEntityLotteryTicketGameEndStateSimple") then
            self.GTextLotteryTicketTips.text = self.ViewMgr.LanMgr:getLanValue("Settlement")
        elseif (ev.EventName == "EvEntityLotteryTicketUpdateTm") then
            self:RefreshLotteryTickLeftTm(ev.tm)
        elseif (ev.EventName == "EvEntityIsFirstRechargeChanged") then
            local com_recharge_first = self.ComUi:GetChild("ComRechargeFirst").asCom
            com_recharge_first.visible = false
            self.BtnRegister.position = com_recharge_first.position
        elseif (ev.EventName == "EvEntityReceiveFeedbackChat") then
            self:RefreshNewFeedback()
        elseif (ev.EventName == "EvEntityReceiveFeedbackChats") then
            self:RefreshNewFeedback()
        elseif (ev.EventName == "EvEntityBagAddItem") then
            self:RefreshNewItem()
        elseif (ev.EventName == "EvCtrlRewardRedPointStateChange") then
            if ev.RedPointType == 'Reward' then
                self:RefreshRedPointRewardState()
            end
        end
    end
end

---------------------------------------
function ViewMain:Close()
    self.GTransitionShow:PlayReverse()
end

---------------------------------------
function ViewMain:RefreshLotteryTickLeftTm(tm)
    if (tm > 0) then
        self.GTextLotteryTicketTips.text = tm .. self.ViewMgr.LanMgr:getLanValue("S")
    else
        self.GTextLotteryTicketTips.text = self.ViewMgr.LanMgr:getLanValue("Settlement")
    end
end

---------------------------------------
function ViewMain:SetPlayerInfo()
    local name = self.ControllerActor.PropNickName:get()
    local gold_acc = self.ControllerActor.PropGoldAcc:get()
    local diamond = self.ControllerActor.PropDiamond:get()

    local str_goldacc = UiChipShowHelper:GetGoldShowStr(gold_acc, self.ViewMgr.LanMgr.LanBase, true, 2)
    local str_diamond = UiChipShowHelper:GetGoldShowStr(diamond, self.ViewMgr.LanMgr.LanBase, true, 2)
    local icon = self.ControllerActor.PropIcon:get()
    local acc_id = self.ControllerActor.PropAccountId:get()
    local vip_level = self.ControllerActor.PropVIPLevel:get()
    self.UiPlayerInfoSelf:SetPlayerInfo(name, str_goldacc, str_diamond, icon, acc_id, vip_level, true)
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
    local lua_helper = LuaHelper
    if (map_friend == nil or lua_helper:GetTableCount(map_friend) == 0) then
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

    self.ListHeadIconFriend = {}
    self.GListFriend.numItems = #self.ListFriendInfo
end

---------------------------------------
function ViewMain:setNewChatCount(chat_count)
    self.NewFriendChatCount = chat_count
    self:RefreshNewRecord()
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
    if (self.CurrentFriendItem == nil) then
        self.BtnInviteFriend.visible = true
        self.UiPlayerInfoCurrentFriend:hidePlayerInfo(true)
        return
    end

    self.BtnInviteFriend.visible = false
    local friend_state = self.ControllerIM.IMFriendList:getFriendStateStr(self.CurrentFriendItem.PlayerInfoCommon.PlayerGuid)
    self.UiPlayerInfoCurrentFriend:SetPlayerInfo(self.CurrentFriendItem.PlayerInfoCommon.NickName,
            UiChipShowHelper:GetGoldShowStr(self.CurrentFriendItem.PlayerInfoMore.Gold, self.ViewMgr.LanMgr.LanBase, true, 2),
            friend_state, self.CurrentFriendItem.PlayerInfoCommon.IconName, self.CurrentFriendItem.PlayerInfoCommon.AccountId,
            self.CurrentFriendItem.PlayerInfoCommon.VIPLevel,
            self.CurrentFriendItem.PlayerInfoMore.OnlineState == PlayerOnlineState.Online)
end

---------------------------------------
function ViewMain:setLotteryTicketInfo(state, left_tm)
    local tips = ""
    if (state == LotteryTicketStateEnum.Bet) then
        tips = math.ceil(left_tm) .. self.ViewMgr.LanMgr:getLanValue("S")
    else
        tips = self.ViewMgr.LanMgr:getLanValue("Settlement")
    end
    self.GTextLotteryTicketTips.text = tips
end

---------------------------------------
function ViewMain:RefreshNewRecord()
    local have_newmail = false
    if (self.ControllerIM:haveNewMail()) then
        have_newmail = true
    end

    if (have_newmail == false) then
        ViewHelper:SetGObjectVisible(false, self.ComMailTips)
    else
        ViewHelper:SetGObjectVisible(true, self.ComMailTips)
        if (self.TransitionNewMail.playing == false) then
            self.TransitionNewMail:Play()
        end
        self.CasinosContext:Play(self.NewMsgSound, CS.Casinos._eSoundLayer.LayerReplace)
    end

    local have_newmsg = false
    if (self.NewFriendChatCount > 0) then
        have_newmsg = true
    end

    if (have_newmsg == false) then
        ViewHelper:SetGObjectVisible(false, self.ComMsgTips)
    else
        ViewHelper:SetGObjectVisible(true, self.ComMsgTips)
        self.GTextMsgNum.text = tostring(self.NewFriendChatCount)
        if (self.TransitionNewMsg.playing == false) then
            self.TransitionNewMsg:Play()
        end
        self.CasinosContext:Play(self.NewMsgSound, CS.Casinos._eSoundLayer.LayerReplace)
    end
end

---------------------------------------
function ViewMain:RefreshRedPointRewardState()
    local ctrl_reward = self.ControllerMgr:GetController("Reward")
    if ctrl_reward.RedPointRewardShow == false then
        ViewHelper:SetGObjectVisible(false, self.ComRewardTips)
    else
        ViewHelper:SetGObjectVisible(true, self.ComRewardTips)
        self.TransitionNewReward:Play(-1, 0, nil)
    end
end

---------------------------------------
function ViewMain:RefreshNewFeedback()
    local have = self.ControllerIM.IMFeedback.HaveNewMsg
    if (have == false) then
        ViewHelper:SetGObjectVisible(false, self.ComFeedbackTips)
    else
        ViewHelper:SetGObjectVisible(true, self.ComFeedbackTips)
        if (self.TransitionFeedbackTips.playing == false) then
            self.TransitionFeedbackTips:Play()
        end
    end
    self:haveMoreTips()
end

---------------------------------------
function ViewMain:RefreshNewItem()
    local have = self.ControllerBag.HaveNewItem
    if (have == false) then
        ViewHelper:SetGObjectVisible(false, self.ComBagTips)
    else
        ViewHelper:SetGObjectVisible(true, self.ComBagTips)
        if (self.TransitionBagTips.playing == false) then
            self.TransitionBagTips:Play()
        end
    end
    self:haveMoreTips()
end

---------------------------------------
function ViewMain:haveMoreTips()
    local have_msg = self.ControllerIM.IMFeedback.HaveNewMsg
    local have_item = self.ControllerBag.HaveNewItem
    if (have_msg or have_item) then
        ViewHelper:SetGObjectVisible(true, self.ComMoreTips)
        if (self.TransitionComMore.playing == false) then
            self.TransitionComMore:Play()
        end
    else
        ViewHelper:SetGObjectVisible(false, self.ComMoreTips)
    end
end

---------------------------------------
function ViewMain:onClickComForestParty()
    local ev = self.ViewMgr:GetEv("EvUiForestPartyEnterDesktop")
    if (ev == nil) then
        ev = EvUiForestPartyEnterDesktop:new(nil)
    end
end

---------------------------------------
function ViewMain:onClickRecommendFriend(ev)
    local item_friend_recommend = nil
    local sender = CS.Casinos.LuaHelper.EventDispatcherCastToGComponent(ev.sender)
    for key, value in pairs(self.ListHeadIconRecommond) do
        if (value.Com == sender) then
            item_friend_recommend = value
        end
    end
    local ev = self.ViewMgr:GetEv("EvUiClickChooseFriend")
    if (ev == nil) then
        ev = EvUiClickChooseFriend:new(nil)
    end
    ev.friend_info = item_friend_recommend:getFriendInfo()
    ev.is_choosechat = false
    ev.is_recommand = true
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ViewMain:onClickBtnLotteryTicket()
    local ev = self.ViewMgr:GetEv("EvEntityRequestGetLotteryTicketData")
    if (ev == nil) then
        ev = EvEntityRequestGetLotteryTicketData:new(nil)
    end
    self.ViewMgr:SendEv(ev)
    self.ViewMgr:CreateView("LotteryTicket")
end

---------------------------------------
function ViewMain:onClickFriendIcon(ev)
    local item_friend = nil
    local sender = CS.Casinos.LuaHelper.EventDispatcherCastToGComponent(ev.sender)
    for key, value in pairs(self.ListHeadIconFriend) do
        if (value.Com == sender) then
            item_friend = value
        end
    end
    local friend_info = item_friend:getFriendInfo()
    if (friend_info.PlayerInfoCommon.PlayerGuid == self.CurrentFriendItem.PlayerInfoCommon.PlayerGuid) then
        return
    end
    self:setCurrentFriendInfo(friend_info)
end

---------------------------------------
function ViewMain:onClickComPlayerInfoCurrentFriend()
    local is_recommand = false

    if (self.CurrentFriendItem == nil) then
        is_recommand = true
    end

    local ev = self.ViewMgr:GetEv("EvUiClickChooseFriend")
    if (ev == nil) then
        ev = EvUiClickChooseFriend:new(nil)
    end
    ev.friend_info = self.CurrentFriendItem
    ev.is_choosechat = false
    ev.is_recommand = is_recommand
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ViewMain:onClickComPlayerInfoSelf()
    self.ViewMgr:CreateView("PlayerInfo")
end

---------------------------------------
function ViewMain:onClickBtnShop()
    local ev = self.ViewMgr:GetEv("EvUiClickShop")
    if (ev == nil) then
        ev = EvUiClickShop:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ViewMain:onClickBtnPurse()
    self.ViewMgr:CreateView("Wallet")
end

---------------------------------------
function ViewMain:onClickBtnFriend()
    local ev = self.ViewMgr:GetEv("EvUiClickFriend")
    if (ev == nil) then
        ev = EvUiClickFriend:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ViewMain:onClickBtnMail()
    self.ViewMgr:CreateView("Mail")
end

---------------------------------------
function ViewMain:onClickBtnDesktop()
    self:hideMote()
    self.GTransitionShow:PlayReverse(
            function()
                local view_lobby = self.ViewMgr:CreateView("ClassicModel")
                view_lobby:setLobbyModel()
            end
    )
end

---------------------------------------
function ViewMain:onClickBtnDesktopH()
    self:hideMote()
    self.GTransitionShow:PlayReverse(
            function()
                local ev = self.ViewMgr:GetEv("EvViewClickDesktopH")
                if (ev == nil) then
                    ev = EvUiClickDesktopHundred:new(nil)
                end
                ev.factory_name = "Texas"
                self.ViewMgr:SendEv(ev)
            end
    )
end

---------------------------------------
function ViewMain:onClickBtnMatch()
    self:hideMote()
    self.GTransitionShow:PlayReverse(
            function()
                self.ViewMgr:CreateView("MatchLobby")
            end
    )
end

---------------------------------------
function ViewMain:onClickBtnCircleOfCardFriends()
    self:hideMote()
    self.GTransitionShow:PlayReverse(
            function()
                self.ViewMgr:CreateView("Club")
            end
    )
end

---------------------------------------
function ViewMain:onClickBtnRanking()
    local view_ranking = self.ViewMgr:GetView("Ranking")
    if (view_ranking == nil) then
        self.ViewMgr:CreateView("Ranking")
    else
        self.ViewMgr:DestroyView(view_ranking)
    end
end

---------------------------------------
function ViewMain:onClickBtnMore()
    local index = self.ControllerMoreBtn.selectedIndex
    if (index == 0) then
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
    local view_edit = self.ViewMgr:GetView("Edit")
    if (view_edit == nil) then
        self.ViewMgr:CreateView("Edit")
    else
        self.ViewMgr:DestroyView(view_edit)
    end
end

---------------------------------------
function ViewMain:onClickBtnGoldTree()
    self.ViewMgr:CreateView("GoldTree")
end

---------------------------------------
function ViewMain:onClickBtnWa()
    ViewHelper:UiShowInfoSuccess("施工中，敬请期待")
end

---------------------------------------
function ViewMain:onClickBtnChat()
    self.ViewMgr:CreateView("Bag")
    local ev = self.ViewMgr:GetEv("EvOpenBag")
    if (ev == nil) then
        ev = EvOpenBag:new(nil)
    end
    self.ViewMgr:SendEv(ev)
    self:RefreshNewItem()
end

---------------------------------------
function ViewMain:onClickBtnChatFriend()
    local ev = self.ViewMgr:GetEv("EvUiClickChatmsg")
    if (ev == nil) then
        ev = EvUiClickChatmsg:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ViewMain:onClickBtnBank()
    self.ViewMgr:CreateView("Bank")
end

---------------------------------------
function ViewMain:onClickBtnFeedback()
    ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("Testing"))
end

---------------------------------------
function ViewMain:onClickComRechargeFirst()
    self.ViewMgr:CreateView("RechargeFirst")
end

---------------------------------------
function ViewMain:rendererFriend(index, item)
    local l = #self.ListFriendInfo
    if (l > index) then
        local friend_info = self.ListFriendInfo[index + 1]
        local head_icon = ItemHeadIcon:new(nil, item)
        head_icon:setFriendInfo(friend_info,
                function(ev)
                    self:onClickFriendIcon(ev)
                end
        )
        self.MapFriend[friend_info.PlayerInfoCommon.PlayerGuid] = head_icon
        if (self.CurrentFriendItem == nil) then
            self.CurrentFriendItem = friend_info
            self:setCurrentFriendInfo(self.CurrentFriendItem)
        end
        table.insert(self.ListHeadIconFriend, head_icon)
    end
end

---------------------------------------
function ViewMain:rendererFriendRecommend(index, item)
    if (#self.ListFriendInfoRecommend > index) then
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
    if (player_info ~= nil) then
        self.CurrentFriendItem = player_info
        local friend_state = self.ControllerIM.IMFriendList:getFriendStateStr(self.CurrentFriendItem.PlayerInfoCommon.PlayerGuid)
        self.UiPlayerInfoCurrentFriend:SetPlayerInfo1(player_info.PlayerInfoCommon.NickName,
                UiChipShowHelper:GetGoldShowStr(player_info.PlayerInfoMore.Gold, self.ViewMgr.LanMgr.LanBase),
                friend_state,
                player_info.PlayerInfoMore.OnlineState == PlayerOnlineState.Online)
    end
end

---------------------------------------
function ViewMain:onClickBtnActivity()
    self.ViewMgr:CreateView("ActivityCenter")
end

---------------------------------------
function ViewMain:hideMote()
    if (self.Context.Cfg.NeedHideClientUi == false) then
        --self.MoteRender.transform.gameObject:SetActive(false)
    end
end

---------------------------------------
function ViewMain:ShowGetChipEffect()
    --local view_getChipEffect = self.ViewMgr:CreateView("GetChipEffect")
    --local pos = self.ComUi:TransformPoint(CS.UnityEngine.Vector2(self.ComHeadIconCenter.position.x,self.ComHeadIconCenter.position.y),view_getChipEffect.ComUi)
    --view_getChipEffect:ShowEffect(nil,pos)
end

---------------------------------------
ViewMainFactory = class(ViewFactory)

---------------------------------------
function ViewMainFactory:CreateView()
    local view = ViewMain:new()
    return view
end