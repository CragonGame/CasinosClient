ViewFriend = ViewBase:new()

function ViewFriend:new(o)
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

    return o
end

function ViewFriend:onCreate()
    ViewHelper:PopUi(self.ComUi,self.ViewMgr.LanMgr:getLanValue("Friend"))
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.ControllerActor = self.ViewMgr.ControllerMgr:GetController("Actor")
    self.ControllerIM = self.ViewMgr.ControllerMgr:GetController("IM")
    self.ControllerFriendOrSearchControl = self.ComUi:GetController("FriendOrSearchControl")
    self.ControllerSearch = self.ComUi:GetController("SearchController")
    self.ControllerFriend = self.ComUi:GetController("FriendController")
    local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
    self.GBtnClose = com_bg:GetChild("BtnClose").asButton
    self.GBtnClose.onClick:Add(
            function()
                self:onClickBtnClose()
            end
    )
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:onClickBtnClose()
            end
    )
    self.GBtnTopFriend = self.ComUi:GetChild("BtnTabFriend").asButton
    self.GBtnTopFriend.onClick:Add(
            function()
                self:onClickBtnTopFriend()
            end
    )
    self.GBtnTopSearch = self.ComUi:GetChild("BtnTabSearch").asButton
    self.GBtnTopSearch.onClick:Add(
            function()
                self:onClickBtnTopSearch()
            end
    )
    local com_friend_detail_friendinfo = self.ComUi:GetChild("FriendDetail").asCom
    self.ViewShowFriendDetailFriend = ViewShowFriendDetail:new(nil, com_friend_detail_friendinfo, true)
    local com_friendrecommend_detail_friendinfo = self.ComUi:GetChild("FriendRecommendDetail").asCom
    self.ViewShowFriendDetailFriendRecommend = ViewShowFriendDetail:new(nil, com_friendrecommend_detail_friendinfo, false)
    self.GTextInputSerch = self.ComUi:GetChild("TextSearch").asTextInput
    self.GTextInputSerch.promptText = self.ViewMgr.LanMgr:getLanValue("SearchPlayerTips")
    self.GTextID = self.ComUi:GetChild("TextID").asTextField
    self.GBtnSearch = self.ComUi:GetChild("Lan_Btn_Search").asButton
    self.GBtnSearch.onClick:Add(
            function()
                self:onClickSearch()
            end
    )
    self.GListFriend = self.ComUi:GetChild("ListFriend").asList
    self.GListFriend:SetVirtual()
    self.GListFriend.itemRenderer = function(a, b)
        self:rendererItemFriend(a, b)
    end
    self.GListSearchFriend = self.ComUi:GetChild("ListFriendRecommend").asList
    self.GListSearchFriend:SetVirtual()
    self.GListSearchFriend.itemRenderer = function(a, b)
        self:rendererItemFriendRecommend(a, b)
    end
    self.ListPlayerInfoFriend = {}
    self.ListPlayerInfoFriendRecommend = {}
    self.MapFriend = {}
    self.CurrentFriendRemommendInfo = nil
    self.CurrentFriendInfo = nil
    local str_id = tostring(self.ControllerActor.PropActorId:get())
    local str_id_1 = string.sub(str_id, 1, 2)
    local str_id_2 = string.sub(str_id, 3, 5)
    local str_id_3 = string.sub(str_id, 6, 7)
    self.GTextID.text = self.ViewMgr.LanMgr:getLanValue("My") .. "ID:" .. str_id_1 .. "-" .. str_id_2 .. "-" .. str_id_3
    self.ViewMgr:bindEvListener("EvEntityNotifyDeleteFriend", self)
    self.ViewMgr:bindEvListener("EvEntityFriendOnlineStateChange", self)
    self.ViewMgr:bindEvListener("EvEntityRefreshFriendList", self)
    self.ViewMgr:bindEvListener("EvEntityRefreshFriendInfo", self)
    self.ViewMgr:bindEvListener("EvLoadPlayerIconSuccess", self)
    self.ViewMgr:bindEvListener("EvEntityFindFriend", self)
    self.ViewMgr:bindEvListener("EvEntityFriendGoldChange", self)
end

function ViewFriend:onDestroy()
    self.ViewMgr:unbindEvListener(self)
end

function ViewFriend:onHandleEv(ev)
    if (ev.EventName == "EvEntityNotifyDeleteFriend")
    then
        self:deleteFriendInfo(ev.friend_etguid, ev.map_friend)
    elseif (ev.EventName == "EvEntityFriendOnlineStateChange")
    then
        local player_info = ev.player_info
        local player_guid = player_info.PlayerInfoCommon.PlayerGuid
        if (self.CurrentFriendInfo ~= nil and
                self.CurrentFriendInfo.PlayerInfoCommon.PlayerGuid == player_guid)
        then
            self:setCurrentFriend(player_info)
        end
        local item = nil
        if (self.MapFriend[player_guid] ~= nil)
        then
            item = self.MapFriend[player_guid]
            local player_infoex = item:getFriendInfo()
            if (player_infoex.PlayerInfoCommon.PlayerGuid == player_guid)
            then
                item:setFriendInfo(self, ev.player_info, true)
            end
        end
    elseif (ev.EventName == "EvEntityRefreshFriendList")
    then
        if (self.CurrentFriendInfo ~= nil)
        then
            self:setFriends(self.CurrentFriendInfo)
        end
    elseif (ev.EventName == "EvEntityRefreshFriendInfo")
    then
        local player_info = ev.player_info
        if (self.CurrentFriendInfo ~= nil and
                player_info.PlayerInfoCommon.PlayerGuid == self.CurrentFriendInfo.PlayerInfoCommon.PlayerGuid)
        then
            self.CurrentFriendInfo = player_info
            self:setCurrentFriend(self.CurrentFriendInfo)
        end
    elseif (ev.EventName == "EvEntityFriendGoldChange")
    then
		local friend_guid = ev.friend_guid
        local friend_gold =ev.current_gold
        if (self.CurrentFriendInfo ~= nil and
                friend_guid == self.CurrentFriendInfo.PlayerInfoCommon.PlayerGuid)
        then
            self.CurrentFriendInfo.PlayerInfoMore.Gold = friend_gold
            self.ViewShowFriendDetailFriend:updatePlayerGold(friend_gold)
        end
    elseif (ev.EventName == "EvLoadPlayerIconSuccess")
    then
        if (self.CurrentFriendInfo ~= nil and self.CurrentFriendInfo.PlayerInfoCommon.PlayerGuid == ev.et_guid)
        then
            self.ViewShowFriendDetailFriend.ViewHeadIcon:setIcon3(ev.fariy_t)
        elseif (self.CurrentFriendRemommendInfo ~= nil and
                self.CurrentFriendRemommendInfo.PlayerInfoCommon.PlayerGuid == ev.et_guid)
        then
            self.ViewShowFriendDetailFriendRecommend.ViewHeadIcon:setIcon3(ev.fariy_t)
        end
    elseif (ev.EventName == "EvEntityFindFriend")
    then
        self:setRecommandFriend(ev.list_friend_item)
    end
end

function ViewFriend:setFriends(current_friend)
    self.ListPlayerInfoFriend = {}
    self.MapFriend = {}
    self.ControllerFriendOrSearchControl.selectedPage = "Friend"
    local map_friend = self.ControllerIM.IMFriendList.MapFriendList
    if (map_friend == nil or LuaHelper:GetTableCount(map_friend) == 0)
    then
        self.ControllerFriend.selectedPage = "NoFriend"
        return
    end
    self.ControllerFriend.selectedPage = "HaveFriend"
    for key, value in pairs(map_friend) do
        table.insert(self.ListPlayerInfoFriend, value)
    end
    table.sort(self.ListPlayerInfoFriend,
            function(f1, f2)
                local sort_f1 = CS.Casinos.LuaHelper.EnumCastToInt(f1.PlayerInfoMore.OnlineState)
                local sort_f2 = CS.Casinos.LuaHelper.EnumCastToInt(f2.PlayerInfoMore.OnlineState)
                if (sort_f1 ~= sort_f2)
                then
                    return sort_f1 > sort_f2
                else
                    local player_playstate1 = f1.PlayerPlayState
                    local player_playstate2 = f2.PlayerPlayState
                    local is_playindesk1 = player_playstate1 ~= nil and (player_playstate1.DesktopGuid ~= nil and player_playstate1.DesktopGuid ~= "")
                    local is_playindesk2 = player_playstate2 ~= nil and (player_playstate2.DesktopGuid ~= nil and player_playstate2.DesktopGuid ~= "")
                    if (is_playindesk1)
                    then
                        sort_f1 = 1
                    else
                        sort_f1 = -1
                    end
                    if (is_playindesk2)
                    then
                        sort_f2 = 1
                    else
                        sort_f2 = -1
                    end
                    if (sort_f1 ~= sort_f2)
                    then
                        return sort_f1 > sort_f2
                    else
                        return f1.PlayerInfoMore.Gold > f2.PlayerInfoMore.Gold
                    end
                end
            end
    )

    local index = 0
    if (current_friend ~= nil)
    then
        self.CurrentFriendInfo = current_friend
        for key, value in pairs(self.ListPlayerInfoFriend) do
            if (value.PlayerInfoCommon.PlayerGuid == self.CurrentFriendInfo.PlayerInfoCommon.PlayerGuid)
            then
                index = key - 1
                break
            end
        end
    else
        self.CurrentFriendInfo = self.ListPlayerInfoFriend[1]
    end

    self.GListFriend.numItems = #self.ListPlayerInfoFriend
    self.GListFriend:ScrollToView(index)
end

function ViewFriend:setCurrentFriend(friend_item)
    self.ControllerFriendOrSearchControl.selectedPage = "Friend";
    self.CurrentFriendInfo = friend_item
    if (self.CurrentFriendInfo == nil)
    then
        self.ControllerFriend.selectedPage = "NoFriend"
    else
        local contains_friend_item = false
        for key, value in pairs(self.ListPlayerInfoFriend) do
            if (value == friend_item)
            then
                contains_friend_item = true
                break
            end
        end
        if (contains_friend_item == false)
        then
            table.insert(self.ListPlayerInfoFriend, friend_item)
        end
        self.ControllerFriend.selectedPage = "HaveFriend"
        for key, value in pairs(self.MapFriend) do
            value:isSelected(false)
        end
        local item = nil
        for key, value in pairs(self.MapFriend) do
            if (key == friend_item.PlayerInfoCommon.PlayerGuid)
            then
                item = self.MapFriend[key]
                local player_infoex = item:getFriendInfo()
                if (player_infoex.PlayerInfoCommon.PlayerGuid == friend_item.PlayerInfoCommon.PlayerGuid)
                then
                    item:isSelected(true)
                    break
                end
            end
        end
        self:showCurrentFriendDetail(friend_item)
    end
end

function ViewFriend:getCurrentFriendInfo()
    return self.CurrentFriendInfo
end

function ViewFriend:setRecommandFriend(list_friend)
    self.ListPlayerInfoFriendRecommend = {}
    self.CurrentFriendRemommendInfo = nil
    self.ControllerFriendOrSearchControl.selectedPage = "Search"
    if (#list_friend == 0)
    then
        ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("NotSearchFriend"))
        self.ControllerSearch.selectedPage = "NoSearch"
        return
    end
    self.ControllerSearch.selectedPage = "HaveResult"
    for i = 1, #list_friend do
        table.insert(self.ListPlayerInfoFriendRecommend, list_friend[i])
    end
    self.GListSearchFriend.numItems = #self.ListPlayerInfoFriendRecommend
end

function ViewFriend:getCurrentRecommandInfo()
    return self.CurrentFriendRemommendInfo
end

function ViewFriend:setCurrentRecommandFriend(friend_item)
    self.ControllerFriendOrSearchControl.selectedPage = "Search"
    self.CurrentFriendRemommendInfo = friend_item
    if (self.CurrentFriendRemommendInfo == nil)
    then
        self.ControllerSearch.selectedPage = "NoSearch"
    else
        local contains_friend_item = false
        for key, value in pairs(self.ListPlayerInfoFriendRecommend) do
            if (value == friend_item)
            then
                contains_friend_item = true
                break
            end
        end
        if (contains_friend_item == false)
        then
            table.insert(self.ListPlayerInfoFriendRecommend, friend_item)
        end
        self.ControllerSearch.selectedPage = "HaveResult"
        self.GListSearchFriend.numItems = #self.ListPlayerInfoFriendRecommend
        self:showCurrentRecommendFriendDetail(self.CurrentFriendRemommendInfo)
    end
end

function ViewFriend:deleteFriendInfo(friend_etguid, map_friend)
    local delete_friend = nil
    local delete_friend_key = nil
    for key, value in pairs(self.ListPlayerInfoFriend) do
        if (value.PlayerInfoCommon.PlayerGuid == friend_etguid)
        then
            delete_friend = value
            delete_friend_key = key
            break
        end
    end
    if (delete_friend ~= nil)
    then
        table.remove(self.ListPlayerInfoFriend, delete_friend_key)
        if (friend_etguid == self.CurrentFriendInfo.PlayerInfoCommon.PlayerGuid)
        then
            self.CurrentFriendInfo = nil
            local a = 1
        end
        self.MapFriend = nil
        self.GListFriend.numItems = #self.ListPlayerInfoFriend
    end
    if (#self.ListPlayerInfoFriend <= 0)
    then
        self.ControllerFriend.selectedPage = "NoFriend"
    end
end

function ViewFriend:showCurrentFriendDetail(friend_item)
    self.ViewShowFriendDetailFriend:setFriendInfo(self.CurrentFriendInfo)
end

function ViewFriend:showCurrentRecommendFriendDetail(friend_item)
    self.ViewShowFriendDetailFriendRecommend:setFriendInfo(self.CurrentFriendRemommendInfo)
end

function ViewFriend:onClickSearch()
    if (self.GTextInputSerch.text == nil or self.GTextInputSerch.text == "")
    then
        return
    end
    local ev = self.ViewMgr:getEv("EvUiFindFriend")
    if (ev == nil)
    then
        ev = EvUiFindFriend:new(nil)
    end
    ev.search_filter = self.GTextInputSerch.text
    self.ViewMgr:sendEv(ev)
end

function ViewFriend:rendererItemFriend(index, item)
    if (#self.ListPlayerInfoFriend > index)
    then
        local friend_info = self.ListPlayerInfoFriend[index + 1]
        local com = CS.Casinos.LuaHelper.GObjectCastToGCom(item)
        local friend_item = ItemShowFriendSimple:new(nil, com,self)
        if (self.CurrentFriendInfo == nil)
        then
            self.CurrentFriendInfo = friend_info
        end
        local is_current_friend = false
        if (self.CurrentFriendInfo == friend_info)
        then
            is_current_friend = true
        end
        friend_item:setFriendInfo(self, friend_info, true)
        friend_item:isSelected(is_current_friend)
        self.MapFriend[friend_info.PlayerInfoCommon.PlayerGuid] = friend_item
        if (is_current_friend)
        then
            self:showCurrentFriendDetail(self.CurrentFriendInfo)
        end
    end
end

function ViewFriend:rendererItemFriendRecommend(index, item)
    if (#self.ListPlayerInfoFriendRecommend > index)
    then
        local friend_info = self.ListPlayerInfoFriendRecommend[index + 1]
        local com = CS.Casinos.LuaHelper.GObjectCastToGCom(item)
        local friend_item = ItemShowFriendSimple:new(nil, com,self)
        if (self.CurrentFriendRemommendInfo == nil)
        then
            self.CurrentFriendRemommendInfo = friend_info
        end
        local is_current_friend = false
        if (self.CurrentFriendRemommendInfo == friend_info)
        then
            is_current_friend = true
        end
        friend_item:setFriendInfo(self, friend_info, false)
        friend_item:isSelected(is_current_friend)
        if (is_current_friend)
        then
            self:showCurrentRecommendFriendDetail(self.CurrentFriendRemommendInfo)
        end
    end
end

function ViewFriend:onClickBtnTopFriend()
    self.ControllerFriendOrSearchControl.selectedPage = "Friend"
    self:setFriends(self.CurrentFriendInfo)
end

function ViewFriend:onClickBtnTopSearch()
    self.ControllerFriendOrSearchControl.selectedPage = "Search"
end

function ViewFriend:onClickBtnClose()
    self.ViewMgr:destroyView(self)
end

ViewShowFriendDetail = {}

function ViewShowFriendDetail:new(o, com_friend, is_friend_detail)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.IsFriendDetail = is_friend_detail
    o.GTextUnderWrite = com_friend:GetChild("TextUnderwrite").asTextField
    local head_icon = com_friend:GetChild("HeadIcon").asCom
    o.UiHeadIcon = ViewHeadIcon:new(nil, head_icon,
            function()
                o:onClickCommonHeadIcon()
            end
    )
    local image_vip_temp = com_friend:GetChild("ImgVip")
    if (image_vip_temp ~= nil)
    then
        o.GImageVip = image_vip_temp.asImage
    end
    o.GTextNickName = com_friend:GetChild("NickName").asTextField
    o.GTextChip = com_friend:GetChild("Chip").asTextField
    o.GTextState = com_friend:GetChild("State").asTextField
    o.GTxetDetailID = com_friend:GetChild("ID").asTextField
    o.GTextAddress = com_friend:GetChild("Address").asTextField
    o.GTextLevel = com_friend:GetChild("TextLevel").asTextField
    o.GProBarLevelValue = com_friend:GetChild("ProgressLevel").asProgress
    o.GBtnJoin = com_friend:GetChild("Lan_Btn_JoinDesk").asButton
    o.GBtnJoin.onClick:Add(
            function()
                o:onClickBtnJoin()
            end
    )
    o.GImageJoin = o.GBtnJoin:GetChild("Bg").asImage
    o.GBtnEmail = com_friend:GetChild("Lan_Btn_Message").asButton
    o.GBtnEmail.onClick:Add(
            function()
                o:onClickBtnEmail()
            end
    )
    o.GImageEmail = o.GBtnEmail:GetChild("Bg").asImage
    o.GBtnPresented = com_friend:GetChild("Lan_Btn_Presented").asButton
    o.GBtnPresented.onClick:Add(
            function()
                o:onClickBtnPresented()
            end
    )
    o.GBtnAddFriend = com_friend:GetChild("BtnAddFriend").asButton
    o.GBtnAddFriend.onClick:Add(
            function()
                o:onClickAddFriend()
            end
    )
    o.GBtnFeedBack = com_friend:GetChild("Lan_Btn_Report").asButton
    o.GBtnFeedBack.onClick:Add(
            function()
                o:onClickReport()
            end
    )
    o.GIMageRemoveFriend = com_friend:GetChild("RemoveFriend").asImage
    o.CasinosContext = CS.Casinos.CasinosContext.Instance
    o.ViewMgr = ViewMgr:new(nil)
    o.ControllerActor = o.ViewMgr.ControllerMgr:GetController("Actor")
	o.GComReportPlayer = com_friend:GetChild("ComReportPlayer").asCom 
	local list_report = o.GComReportPlayer:GetChild("ContentList").asList
	for key,value in pairs(ReportPlayerType)do
		local com_report = list_report:AddItemFromPool().asCom
		local item = ItemReportPlayerOperate:new(nil,com_report,o)
		item:setReportType(value,et_guid)
	end
    return o
end

function ViewShowFriendDetail:setFriendInfo(friend_item)
    local controller_mgr = ControllerMgr:new(nil)
    local controller_im = controller_mgr:GetController("IM")
    self.CurrentFriendInfo = friend_item
    self.UiHeadIcon:setPlayerInfo(self.CurrentFriendInfo.PlayerInfoCommon.IconName, self.CurrentFriendInfo.PlayerInfoCommon.AccountId, self.CurrentFriendInfo.PlayerInfoCommon.VIPLevel)
    self.GTextNickName.text = self.CurrentFriendInfo.PlayerInfoCommon.NickName
    self.GTextChip.text = UiChipShowHelper:getGoldShowStr(self.CurrentFriendInfo.PlayerInfoMore.Gold, self.ViewMgr.LanMgr.LanBase)
    local friend_state_str = ""
    if (self.IsFriendDetail)
    then
        friend_state_str = controller_im.IMFriendList:getFriendStateStr(self.CurrentFriendInfo.PlayerInfoCommon.PlayerGuid)
    else
        local state = CasinoHelper:TranslateFriendStateEx(self.CurrentFriendInfo)
        friend_state_str = CasinoHelper:TranslateFriendState(state)
    end
    self.GTextState.text = friend_state_str
    self.GTxetDetailID.text = "ID:" .. CS.Casinos.UiHelperCasinos.FormatPlayerActorId(self.CurrentFriendInfo.PlayerInfoMore.PlayerId)
    local address = self.CurrentFriendInfo.PlayerInfoMore.IPAddress
    if (address == nil or address == "")
    then
        address = self.ViewMgr.LanMgr:getLanValue("Unknown")
    end
    address = self.ViewMgr.LanMgr:getLanValue("Address") .. address
    self.GTextAddress.text = address
    if (self.GImageVip ~= nil)
    then
        if (self.CurrentFriendInfo.PlayerInfoMore.VipLevel > 0)
        then
            self.GImageVip.visible = true
        else
            self.GImageVip.visible = false
        end
    end
    self.GTextUnderWrite.text = self.CurrentFriendInfo.PlayerInfoMore.IndividualSignature
    self.GTextLevel.text = tostring(self.CurrentFriendInfo.PlayerInfoMore.Level)
    self.GProBarLevelValue.value = self:setCurrentExppro(self.CurrentFriendInfo.PlayerInfoMore.Level, self.CurrentFriendInfo.PlayerInfoMore.Exp) * 100
    local friend_state = controller_im.IMFriendList:getFriendState(self.CurrentFriendInfo.PlayerInfoCommon.PlayerGuid)
    local friend_indesktop = false
    if (friend_state == _eFriendStateClient.TexasDesktopClassic or
            friend_state == _eFriendStateClient.TexasDesktopMustBet or
            friend_state == _eFriendStateClient.GFlowerDesktopNormal)
    then
        friend_indesktop = true
    end

    if (self.IsFriendDetail)
    then
        if (friend_item.PlayerInfoMore.OnlineState == PlayerOnlineState.Online)
        then
            if (friend_item.PlayerPlayState == nil)
            then
                self.GImageJoin.color = CS.UnityEngine.Color.gray
            end
        end
    else
        self.GImageEmail.color = CS.UnityEngine.Color.gray
        self.GBtnEmail.enabled = false
    end
    if (friend_indesktop)
    then
        self.GImageJoin.color = CS.UnityEngine.Color.white
    else
        self.GImageJoin.color = CS.UnityEngine.Color.gray
    end
    self.GBtnJoin.enabled = friend_indesktop

    local is_friend = controller_im:isFriend(self.CurrentFriendInfo.PlayerInfoCommon.PlayerGuid)
    ViewHelper:setGObjectVisible(is_friend, self.GIMageRemoveFriend)
    local btn_addfriend_title = self.ViewMgr.LanMgr:getLanValue("AddFriend")
    if (is_friend)
    then
        btn_addfriend_title = self.ViewMgr.LanMgr:getLanValue("DeleteFriend1")
    end
    self.GBtnAddFriend.title = btn_addfriend_title
    if (self.IsFriendDetail == false)
    then
        if (is_friend)
        then
            self.GBtnAddFriend.enabled = false
            ViewHelper:setGObjectVisible(false, self.GIMageRemoveFriend)
        else
            self.GBtnAddFriend.enabled = true
        end
    end
end

function ViewShowFriendDetail:updatePlayerGold(gold)
    self.CurrentFriendInfo.PlayerInfoMore.Gold = gold
    self.GTextChip.text = UiChipShowHelper:getGoldShowStr(gold, self.ViewMgr.LanMgr.LanBase)
end

function ViewShowFriendDetail:setCurrentExppro(level_cur, exp_cur)
    local level_next = level_cur + 1
    local tb_actorlevel_cur = self.CasinosContext.TbDataMgrLua:GetData("ActorLevel", level_cur)
    local tb_actorlevel_next = self.CasinosContext.TbDataMgrLua:GetData("ActorLevel", level_next)
    if (tb_actorlevel_next == nil)
    then
        return 1
    end

    local exp_total = tb_actorlevel_next.Experience - tb_actorlevel_cur.Experience
    if (exp_total <= 0)
    then
        print("CellActor._onPropExperienceChanged() Error: exp_total<=0 level_cur=" .. level_cur)
        return 0
    end

    return exp_cur * 1.0 / exp_total
end

function ViewShowFriendDetail:onClickCommonHeadIcon()
    ViewHelper:UiBeginWaiting(self.ViewMgr.LanMgr:getLanValue("GetBigPic"))
    local icon_name = self.CurrentFriendInfo.PlayerInfoCommon.IconName
    if (icon_name ~= nil and icon_name ~= "")
    then
        CS.Casinos.HeadIconMgr.Instant:asyncLoadIcon(icon_name .. "_Big", CS.Casinos.HeadIconMgr.getIconURL(false, icon_name), icon_name, nil,
                function(ex, tick)
                    ViewHelper:UiEndWaiting()
                    if (ex ~= nil)
                    then
                        local ui_iconbig = self.ViewMgr:createView("HeadIconBig")
                        ex = CS.Casinos.LuaHelper.UnityObjectCastToTexture(ex,true)
                        ui_iconbig:setIcon(ex)
                    end
                end
        )
    else
        local icon_resource_name = ""
        local player_icon, icon_resource_name = CS.Casinos.HeadIconMgr.getIconName(false, self.CurrentFriendInfo.PlayerInfoCommon.AccountId, icon_resource_name)
        CS.Casinos.HeadIconMgr.Instant:asyncLoadIcon(icon_resource_name .. "_Big", self.CasinosContext.UserConfig.Current.PlayerIconDomain .. player_icon, icon_resource_name, nil,
                function(ex, tick)
                    ViewHelper:UiEndWaiting()
                    if (ex ~= nil)
                    then
                        local ui_iconbig = self.ViewMgr:createView("HeadIconBig")
                        ex = CS.Casinos.LuaHelper.UnityObjectCastToTexture(ex,true)
                        ui_iconbig:setIcon(ex)
                    end
                end
        )
    end
end



function ViewShowFriendDetail:onClickBtnJoin()
    if (self.IsFriendDetail == false)
    then
        return
    end
    if (self.CurrentFriendInfo.PlayerInfoMore.OnlineState == PlayerOnlineState.Offline)
    then
        return
    elseif (self.CurrentFriendInfo.PlayerInfoMore.OnlineState == PlayerOnlineState.Online)
    then
        if (self.CurrentFriendInfo.PlayerPlayState == nil)
        then
            return
        else
            if (self.CurrentFriendInfo.PlayerPlayState.DesktopType == DesktopTypeEx.Desktop)
            then
                if (self.CurrentFriendInfo.PlayerPlayState.UserData2 ~= nil and self.CurrentFriendInfo.PlayerPlayState.UserData2 ~= "")
                then
                    local ev = self.ViewMgr:getEv("EvUiClickViewInDesk")
                    if (ev == nil)
                    then
                        ev = EvUiClickViewInDesk:new(nil)
                    end
                    ev.desk_etguid = self.CurrentFriendInfo.PlayerPlayState.DesktopGuid
                    ev.desktop_filter = self.ViewMgr:unpackData(self.CurrentFriendInfo.PlayerPlayState.UserData2)-- CS.LuaHelper.JsonDeserializeDesktopFilter(self.CurrentFriendInfo.PlayerPlayState.UserData2)
                    ev.seat_index = 255
                    self.ViewMgr:sendEv(ev)
                else
                    ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("PlayerNotInTable"))
                end
            elseif (self.CurrentFriendInfo.PlayerPlayState.DesktopType == DesktopTypeEx.DesktopH)
            then
                local ev = self.ViewMgr:getEv("EvUiClickDesktopHundred")
                if (ev == nil)
                then
                    ev = EvUiClickDesktopHundred:new(nil)
                end
                ev.factory_name = self.CurrentFriendInfo.PlayerPlayState.CasinosModule:ToString()
                self.ViewMgr:sendEv(ev)
            end
        end
    end
end

function ViewShowFriendDetail:onClickBtnEmail()
    if (self.IsFriendDetail == false)
    then
        return
    end
    local ev = self.ViewMgr:getEv("EvUiClickChooseFriend")
    if (ev == nil)
    then
        ev = EvUiClickChooseFriend:new(nil)
    end
    ev.friend_info = self.CurrentFriendInfo
    ev.is_choosechat = true
    self.ViewMgr:sendEv(ev)
end

function ViewShowFriendDetail:onClickBtnPresented()
    local ui_chiptransaction = self.ViewMgr:createView("ChipOperate")
    ui_chiptransaction:setChipsInfo(self.ControllerActor.PropGoldAcc:get(), 0, 0, CS.Casinos._eChipOperateType.Transaction,
            self.CurrentFriendInfo.PlayerInfoCommon.PlayerGuid, nil)
    local ev = self.ViewMgr:getEv("EvUiClickChipTransaction")
    if (ev == nil)
    then
        ev = EvUiClickChipTransaction:new(nil)
    end
    ev.send_target_etguid = self.CurrentFriendInfo.PlayerInfoCommon.PlayerGuid
    self.ViewMgr:sendEv(ev)
end

function ViewShowFriendDetail:onClickAddFriend()
    local ev = self.ViewMgr:getEv("EvUiRequestFriendAddOrRemove")
    if (ev == nil)
    then
        ev = EvUiRequestFriendAddOrRemove:new(nil)
    end
    if (self.IsFriendDetail)
    then
        ev.is_add = false
    else
        ev.is_add = true
    end
    ev.friend_guid = self.CurrentFriendInfo.PlayerInfoCommon.PlayerGuid
    ev.friend_nickname = self.CurrentFriendInfo.PlayerInfoCommon.NickName
    self.ViewMgr:sendEv(ev)
end

function ViewShowFriendDetail:onClickReport()
	if(self.GComReportPlayer.visible == true)
	then
		self:HideComReportPlayer()
	else
		self:ShowComReportPlayer()
	end
end

function ViewShowFriendDetail:HideComReportPlayer()
	self.GComReportPlayer.visible = false
end

function ViewShowFriendDetail:ShowComReportPlayer()
	self.GComReportPlayer.visible = true
end


ViewFriendFactory = ViewFactory:new()

function ViewFriendFactory:new(o, ui_package_name, ui_component_name,
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

function ViewFriendFactory:createView()
    local view = ViewFriend:new(nil)
    return view
end