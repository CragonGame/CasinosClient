-- Copyright(c) Cragon. All rights reserved.
-- 他人详细信息

---------------------------------------
ViewPlayerProfile = class(ViewBase)

---------------------------------------
function ViewPlayerProfile:ctor()
    self.Context = Context
    self.Tween = nil
end

---------------------------------------
function ViewPlayerProfile:OnCreate()
    self.Tween = ViewHelper:PopUi(self.ComUi)
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.ControllerPlayer = self.ControllerMgr:GetController("Player")
    self.ControllerDesktop = self.ControllerMgr:GetController("DesktopTexas")
    self.ControllerDesktopH = self.ControllerMgr:GetController("DesktopH")
    self.ControllerIM = self.ControllerMgr:GetController("IM")
    self.ControllerActor = self.ControllerMgr:GetController("Actor")
    self.ViewPool = self.ViewMgr:GetView("Pool")
    self.ListGift = {}
    local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:onClickClose()
            end
    )
    local btn_stand = self.ComUi:GetChild("BtnStandup")
    if (btn_stand ~= nil)
    then
        self.GBtnStandup = btn_stand.asButton
    end

    local co_headicon = self.ComUi:GetChild("CoHeadIcon").asCom
    self.UiHeadIcon = ViewHeadIcon:new(nil, co_headicon,
            function()
                ViewHelper:UiBeginWaiting(self.ViewMgr.LanMgr:getLanValue("GetBigPic"))
                local icon_name = self.PlayerInfo.PlayerInfoCommon.IconName
                if (icon_name ~= nil and icon_name ~= "")
                then
                    CS.Casinos.HeadIconMgr.Instant:asyncLoadIcon(icon_name .. "_Big", Context:CalcBotIconUrl(false, icon_name), icon_name, nil,
                            function(ex, tick)
                                ViewHelper:UiEndWaiting()
                                if (ex ~= nil)
                                then
                                    local ui_iconbig = self.ViewMgr:CreateView("HeadIconBig")
                                    local texture = CS.Casinos.LuaHelper.UnityObjectCastToTexture(ex, true)
                                    ui_iconbig:SetIcon(texture)
                                end
                            end
                    )
                else
                    local icon_resource_name = ""
                    local player_icon, icon_resource_name = CS.Casinos.HeadIconMgr.getIconName(false, self.PlayerInfo.PlayerInfoCommon.AccountId, icon_resource_name)
                    CS.Casinos.HeadIconMgr.Instant:asyncLoadIcon(icon_resource_name .. "_Big", self.Context.Cfg.PlayerIconDomain .. player_icon, icon_resource_name, nil,
                            function(ex, tick)
                                ViewHelper:UiEndWaiting()
                                if (ex ~= nil)
                                then
                                    local ui_iconbig = self.ViewMgr:CreateView("HeadIconBig")
                                    local texture = CS.Casinos.LuaHelper.UnityObjectCastToTexture(ex, true)
                                    ui_iconbig:SetIcon(texture)
                                end
                            end
                    )
                end
            end
    )
    self.GTextPlayerNickName = self.ComUi:GetChild("NickName").asTextField
    self.GTextPlayerGolds = self.ComUi:GetChild("Chips").asTextField
    self.GTextPlayerLevel = self.ComUi:GetChild("GTextPlayerLevel").asTextField
    self.GProPlayerLevel = self.ComUi:GetChild("ProgressLevel").asProgress
    self.GTextPlayerSign = self.ComUi:GetChild("Sign").asTextField
    self.GTextPlayerIP = self.ComUi:GetChild("Address").asTextField
    self.GTextId = self.ComUi:GetChild("ID").asTextField
    self.GGroupStand = self.ComUi:GetChild("GroupStand").asGroup
    ViewHelper:SetGObjectVisible(false, self.GGroupStand)
    self.GGroupMagicExp = self.ComUi:GetChild("GroupMagicExp").asGroup
    ViewHelper:SetGObjectVisible(false, self.GGroupMagicExp)
    self.GListMagicExp = self.ComUi:GetChild("ListMagicExp").asList
    for key, value in pairs(self.ViewMgr.TbDataMgr:GetMapData("UnitMagicExpression")) do
        local co_magicexp = CS.FairyGUI.UIPackage.CreateObject("PlayerProfile", "ComMagicExp").asCom
        self.GListMagicExp:AddChild(co_magicexp)
        ItemMagicExpIcon:new(nil, self, co_magicexp, key)
    end
    self.GListProfileItem = self.ComUi:GetChild("ListItem").asList
    self.GListProfileItem:SetVirtual()
    self.GListProfileItem.itemRenderer = function(index, obj)
        self:RenderListItem(index, obj)
    end
    self.GComReportPlayer = self.ComUi:GetChild("ComReportPlayer").asCom
    local list_report = self.GComReportPlayer:GetChild("ContentList").asList
    for key, value in pairs(ReportPlayerType) do
        local com_report = list_report:AddItemFromPool().asCom
        local item = ItemReportPlayerOperate:new(nil, com_report, self)
        item:setReportType(value, et_guid)
    end
    self.ChipIconSolustion = self.ComUi:GetController("ChipIconSolustion")
    self.ChipIconSolustion.selectedIndex = self.Context.Cfg.ChipIconSolustion
    self.ViewMgr:BindEvListener("EvEntityGetPlayerInfoOther", self)
    self.ViewMgr:BindEvListener("EvEntityBagAddItem", self)
    self.ViewMgr:BindEvListener("EvEntityBagDeleteItem", self)
end

---------------------------------------
function ViewPlayerProfile:OnDestroy()
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end
    self.ViewMgr:UnbindEvListener(self)
    self.ViewPool:itemGiftAllEnque()
end

---------------------------------------
function ViewPlayerProfile:OnHandleEv(ev)
    if (ev.EventName == "EvEntityGetPlayerInfoOther") then
        if (ev.player_info.PlayerInfoCommon.PlayerGuid == self.PlayerGuid) then
            self:SetPlayerInfo(ev.player_info)
        end
    elseif (ev.EventName == "EvEntityBagAddItem") then
        if (self.PlayerInfo ~= nil and self.PlayerInfo.PlayerInfoCommon.PlayerGuid == self.ControllerPlayer.Guid) then
            table.insert(self.ListGift, ev.item)
            self:sortAndShowGifts()
        end
    elseif (ev.EventName == "EvEntityBagDeleteItem") then
        if (self.PlayerInfo ~= nil and self.PlayerInfo.PlayerInfoCommon.PlayerGuid == self.ControllerPlayer.Guid) then
            local gift = nil
            local gift_key = nil
            for key, value in pairs(self.ListGift) do
                if (value.ItemData.item_objid == ev.item_objid) then
                    gift = value
                    gift_key = key
                end
            end
            if (gift ~= nil) then
                table.remove(self.ListGift, gift_key)
            end
            self:sortAndShowGifts()
        end
    end
end

---------------------------------------
function ViewPlayerProfile:RequestRefreshByPlayerGuid(player_profile_type, player_guid, getplayerinfo_callback)
    self.PlayerProfileType = player_profile_type
    self.PlayerGuid = player_guid
    self.GetPlayerInfoCallBack = getplayerinfo_callback
    self.ControllerPlayer:requestGetPlayerInfoOther(self.PlayerGuid)
end

---------------------------------------
function ViewPlayerProfile:SendMagicExpression(exp_tbid)
    if (self.PlayerInfo.PlayerInfoCommon.PlayerGuid == self.ControllerPlayer.Guid) then
        ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("NotGiveSelfMagic"))
        return
    end

    if (self.PlayerProfileType == CS.Casinos._ePlayerProfileType.Desktop and self.ControllerDesktop.DesktopBase.MePlayer.IsInGame == false) then
        ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("SitTableSendMagic"))
        return
    end
    local ev = self:GetEv("EvUiBuyItem")
    if (ev == nil) then
        ev = EvUiBuyItem:new(nil)
    end
    ev.to_etguid = self.PlayerGuid
    ev.item_id = exp_tbid
    self:SendEv(ev)
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewPlayerProfile:reportFriend(friend_etguid, report_type)
    local ev = self:GetEv("EvUiReportFriend")
    if (ev == nil) then
        ev = EvUiReportFriend:new(nil)
    end
    ev.friend_etguid = friend_etguid
    ev.report_type = report_type
    self:SendEv(ev)
end

---------------------------------------
function ViewPlayerProfile:SetPlayerInfo(player_info)
    self.PlayerInfo = player_info
    local hide_standupbtn = true
    if ((self.PlayerInfo.PlayerInfoCommon.PlayerGuid == self.ControllerPlayer.Guid) and (self.PlayerProfileType == CS.Casinos._ePlayerProfileType.DesktopH)) then
        if (self.ControllerDesktopH.SeatIndex ~= 255) then
            hide_standupbtn = false
        end
    end
    if (hide_standupbtn == false) then
        self.GBtnStandup.onClick:Add(
                function()
                    self:onClickBtnStandup()
                end
        )
        ViewHelper:SetGObjectVisible(true, self.GGroupStand)
    else
        ViewHelper:SetGObjectVisible(false, self.GGroupStand)
    end
    local show_magic_exp = false
    if (self.PlayerProfileType ~= CS.Casinos._ePlayerProfileType.Ranking) then
        show_magic_exp = true
    end
    ViewHelper:SetGObjectVisible(show_magic_exp, self.GGroupMagicExp)

    self.GCoGift = self.ComUi:GetChild("CoGift").asCom
    self.GCoGift.onClick:Add(
            function()
                self:onClickGift()
            end
    )
    self.GCoGiftHome = self.ComUi:GetChild("CoGiftHome").asCom
    self.GCoGiftHome.onClick:Add(
            function()
                self:onClickGiftHome()
            end
    )
    self.GCoLockChat = self.ComUi:GetChild("CoLockChat").asCom
    self.ControllerLockChat = self.GCoLockChat:GetController("ControllerLockChat")
    self.GCoLockChat.onClick:Add(
            function()
                self:onClickComLockChat()
            end
    )
    if (self.PlayerProfileType == CS.Casinos._ePlayerProfileType.Desktop) then
        local desktop = self.ViewMgr:GetView("DesktopTexas")
        local is_lock = false
        if (desktop.Desktop.MapSeatPlayerChatIsLock[self.PlayerGuid] ~= nil) then
            is_lock = desktop.Desktop.MapSeatPlayerChatIsLock[self.PlayerGuid]
        end
        self.IsLocked = is_lock
        if (self.IsLocked) then
            self.ControllerLockChat:SetSelectedIndex(1)
        else
            self.ControllerLockChat:SetSelectedIndex(0)
        end
    end
    self.GCoFriend = self.ComUi:GetChild("CoFriend").asCom
    self.GCoFriend.onClick:Add(
            function()
                self:onClickFriend()
            end
    )
    self.ControllerFriend = self.GCoFriend:GetController("ControllerFriend")
    self.GCoSendGold = self.ComUi:GetChild("CoSendChip").asCom
    if (self.ViewMgr:GetView("DesktopH") ~= nil) then
        self.GCoSendGold.enabled = false
        self.GCoGift.enabled = false
    end
    self.GCoSendGold.onClick:Add(
            function()
                self:onClickSendGold()
            end
    )
    self.ControllerSendGold = self.GCoSendGold:GetController("ControllerSendGold")
    self.GCoReport = self.ComUi:GetChild("CoReport").asCom
    self.GCoReport.onClick:Add(
            function()
                self:onClickReport()
            end
    )

    local loader = CS.Casinos.LuaHelper.GLoaderCastToGLoaderEx(self.UiHeadIcon.GLoaderPlayerIcon)
    loader.LoaderDoneCallBack = function(bo)
        self:loadIconDone(bo)
    end
    self.UiHeadIcon:SetPlayerInfo(player_info.PlayerInfoCommon.IconName, player_info.PlayerInfoCommon.AccountId, player_info.PlayerInfoMore.VipLevel)
    self.GTextPlayerNickName.text = CS.Casinos.UiHelper.addEllipsisToStr(self.PlayerInfo.PlayerInfoCommon.NickName, 21, 6)
    self.GTextPlayerGolds.text = UiChipShowHelper:GetGoldShowStr(self.PlayerInfo.PlayerInfoMore.Gold, self.ViewMgr.LanMgr.LanBase, false)
    self.GTextPlayerSign.text = self.PlayerInfo.PlayerInfoMore.IndividualSignature
    self.GTextId.text = "ID: " .. CS.Casinos.LuaHelper.FormatPlayerActorId(self.PlayerInfo.PlayerInfoMore.PlayerId)

    local address = self.ViewMgr.LanMgr:getLanValue("Address")
    local real_address = self.PlayerInfo.PlayerInfoMore.IPAddress
    if (real_address == nil or real_address == "") then
        real_address = self.ViewMgr.LanMgr:getLanValue("Unknown")
    end
    address = self.CasinosContext:AppendStrWithSB(address, ": ", real_address)
    self.GTextPlayerIP.text = address
    self.GTextPlayerLevel.text = player_info.PlayerInfoMore.Level
    self.GProPlayerLevel.value = self:getCurrentExppro(player_info.PlayerInfoMore.Level, player_info.PlayerInfoMore.Exp) * 100

    self.IsFriend = self.ControllerIM:isFriend(player_info.PlayerInfoCommon.PlayerGuid)
    if (self.IsFriend) then
        self.ControllerFriend.selectedIndex = 1
    else
        self.ControllerFriend.selectedIndex = 0
    end
    if (player_info.PlayerInfoCommon.PlayerGuid == self.ControllerPlayer.Guid) then
        self.IsSelf = true
        self.GCoLockChat.enabled = false
        self.GCoFriend.enabled = false
        self.GCoReport.enabled = false
    end
    if (self.IsSelf) then
        self.ControllerSendGold.selectedIndex = 1
    else
        self.ControllerSendGold.selectedIndex = 0
    end
    if (self.PlayerInfo.PlayerInfoMore.ListItemData == nil) then
        return
    end

    self.ListGift = {}
    for i, v in pairs(self.PlayerInfo.PlayerInfoMore.ListItemData) do
        local item = Item:new(nil, self.ViewMgr.TbDataMgr, v)
        if (item.UnitLink.UnitType ~= "GiftNormal") then
        else
            table.insert(self.ListGift, item)
        end
    end

    self:sortAndShowGifts()
    if (self.PlayerProfileType == CS.Casinos._ePlayerProfileType.Ranking) then
        self:disabledAllProfileBtn()
    end

    local is_friend = self.ControllerIM:isFriend(self.PlayerInfo.PlayerInfoCommon.PlayerGuid)
    local btn_addfriend_title = self.ViewMgr.LanMgr:getLanValue("AddFriend")
    if (is_friend) then
        btn_addfriend_title = self.ViewMgr.LanMgr:getLanValue("DeleteFriend1")
    end
    local text_friend = self.GCoFriend:GetChild("TextFriend").asTextField
    text_friend.text = btn_addfriend_title
end

---------------------------------------
function ViewPlayerProfile:loadIconDone(is_success)
    if (self.GetPlayerInfoCallBack ~= nil and self.UiHeadIcon.GLoaderPlayerIcon.texture ~= nil) then
        self.GetPlayerInfoCallBack(self.PlayerInfo, self.UiHeadIcon.GLoaderPlayerIcon.texture.nativeTexture)
    end
end

---------------------------------------
function ViewPlayerProfile:disabledAllProfileBtn()
    self.GCoLockChat.enabled = false
    self.GCoFriend.enabled = false
    self.GCoReport.enabled = false
    self.GCoGift.enabled = false
    self.GCoGiftHome.enabled = false
    self.GCoSendGold.enabled = false
end

---------------------------------------
function ViewPlayerProfile:sortAndShowGifts()
    table.sort(self.ListGift,
            function(x, y)
                local price_typex = CS.Casinos.LuaHelper.EnumCastToInt(x.TbDataItem.PriceType)
                local price_typey = CS.Casinos.LuaHelper.EnumCastToInt(y.TbDataItem.PriceType)
                if (price_typex ~= price_typey) then
                    return price_typex > price_typey
                else
                    local price_x = x.TbDataItem.Price
                    local price_y = y.TbDataItem.Price
                    if (price_x ~= price_y) then
                        return price_x > price_y
                    else
                        local gift_normalx = x.UnitLink
                        local gift_normaly = y.UnitLink
                        return gift_normalx.CreateTime > gift_normaly.CreateTime
                    end
                end
            end
    )
    self.GListProfileItem.numItems = #self.ListGift
end

---------------------------------------
function ViewPlayerProfile:RenderListItem(index, obj)
    if (self.ListGift ~= nil and #self.ListGift > index)
    then
        local item = self.ListGift[index + 1]
        if (item.UnitLink.UnitType == "GiftNormal") then
            local com = CS.Casinos.LuaHelper.GObjectCastToGCom(obj)
            local ui_item = self.ViewPool:getItemGift(com)
            local unit_giftnormal = item.UnitLink
            ui_item:init(com, self.ViewMgr.LanMgr)
            ui_item:SetGift(item.TbDataItem.Id, false, self.ControllerPlayer.Guid == unit_giftnormal.GivePlayerEtGuid,
                    self.ControllerPlayer.Guid, unit_giftnormal.GiveBy, self.PlayerInfo.PlayerInfoCommon.PlayerGuid, item)
        end
    end
end

---------------------------------------
function ViewPlayerProfile:getCurrentExppro(level_cur, exp_cur)
    local level_next = level_cur + 1

    local tb_actorlevel_cur = self.ViewMgr.TbDataMgr:GetData("TbDataActorLevel", level_cur)
    local tb_actorlevel_next = self.ViewMgr.TbDataMgr:GetData("TbDataActorLevel", level_next)
    if (tb_actorlevel_next == nil) then
        return 1
    end

    local exp_total = tb_actorlevel_next.Experience - tb_actorlevel_cur.Experience
    if (exp_total <= 0) then
        print("CellActor._onPropExperienceChanged() Error: exp_total<=0 level_cur=" .. level_cur)
        return 0
    end

    return exp_cur * 1 / exp_total
end

---------------------------------------
function ViewPlayerProfile:onClickGift()
    local ev = self:GetEv("EvCreateGiftShop")
    if (ev == nil) then
        ev = EvCreateGiftShop:new(nil)
    end
    ev.is_tmp_gift = true
    ev.to_player_etguid = self.PlayerInfo.PlayerInfoCommon.PlayerGuid
    self:SendEv(ev)
end

---------------------------------------
function ViewPlayerProfile:onClickGiftHome()
    local ev = self:GetEv("EvCreateGiftShop")
    if (ev == nil) then
        ev = EvCreateGiftShop:new(nil)
    end
    ev.is_tmp_gift = false
    ev.to_player_etguid = self.PlayerInfo.PlayerInfoCommon.PlayerGuid
    self:SendEv(ev)
end

---------------------------------------
function ViewPlayerProfile:onClickFriend()
    local ev = self:GetEv("EvUiRequestFriendAddOrRemove")
    if (ev == nil) then
        ev = EvUiRequestFriendAddOrRemove:new(nil)
    end
    if (self.IsFriend) then
        ev.is_add = false
    else
        ev.is_add = true
    end
    ev.friend_guid = self.PlayerInfo.PlayerInfoCommon.PlayerGuid
    ev.friend_nickname = self.PlayerInfo.PlayerInfoCommon.NickName
    self:SendEv(ev)
end

---------------------------------------
function ViewPlayerProfile:onClickSendGold()
    if (self.IsSelf) then
        local ev = self:GetEv("EvUiCreateExchangeChip")
        if (ev == nil) then
            ev = EvUiCreateExchangeChip:new(nil)
        end
        self:SendEv(ev)
    else
        local ui_chiptransaction = self.ViewMgr:CreateView("ChipOperate")
        ui_chiptransaction:setChipsInfo(self.ControllerActor.PropGoldAcc:get(), 0, 0, CS.Casinos._eChipOperateType.Transaction, self.PlayerInfo.PlayerInfoCommon.PlayerGuid, nil)
        local ev = self:GetEv("EvUiClickChipTransaction")
        if (ev == nil) then
            ev = EvUiClickChipTransaction:new(nil)
        end
        ev.send_target_etguid = self.PlayerInfo.PlayerInfoCommon.PlayerGuid
        self:SendEv(ev)
    end
end

---------------------------------------
function ViewPlayerProfile:onClickReport()
    if (self.GComReportPlayer.visible == true) then
        self:HideComReportPlayer()
    else
        self:ShowComReportPlayer()
    end
end

---------------------------------------
function ViewPlayerProfile:onClickBtnStandup()
    local ev = self:GetEv("EvViewDesktopHStandup")
    if (ev == nil) then
        ev = EvUiDesktopHStandUp:new(nil)
    end
    self:SendEv(ev)
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewPlayerProfile:onClickClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewPlayerProfile:onClickComLockChat()
    if (self.IsLocked) then
        self.IsLocked = false
    else
        self.IsLocked = true
    end
    local ev = self:GetEv("EvUiRequestLockPlayerChat")
    if (ev == nil) then
        ev = EvUiRequestLockPlayerChat:new(nil)
    end
    ev.player_guid = self.PlayerGuid
    ev.requestLock = self.IsLocked
    self:SendEv(ev)
    if (self.IsLocked) then
        self.ControllerLockChat:SetSelectedIndex(1)
    else
        self.ControllerLockChat:SetSelectedIndex(0)
    end
end

---------------------------------------
function ViewPlayerProfile:HideComReportPlayer()
    self.GComReportPlayer.visible = false
end

---------------------------------------
function ViewPlayerProfile:ShowComReportPlayer()
    self.GComReportPlayer.visible = true
end

---------------------------------------
ViewPlayerProfileFactory = class(ViewFactory)

---------------------------------------
function ViewPlayerProfileFactory:CreateView()
    local view = ViewPlayerProfile:new()
    return view
end