ViewPlayerInfo = ViewBase:new()

function ViewPlayerInfo:new(o)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	self.MaxNickNameLength = 9
	o.ViewMgr = nil
	o.GoUi = nil
	o.ComUi = nil
	o.Panel = nil
	o.UILayer = nil
	o.InitDepth = nil
	o.ViewKey = nil

	return o
end

function ViewPlayerInfo:onCreate()
	ViewHelper:PopUi(self.ComUi)
	self.CasinosContext = CS.Casinos.CasinosContext.Instance
	self.ControllerActor = self.ViewMgr.ControllerMgr:GetController("Actor")
	self.ControllerBag = self.ViewMgr.ControllerMgr:GetController("Bag")
	self.ControllerPlayer = self.ViewMgr.ControllerMgr:GetController("Player")
	local wechat_id = self.ControllerActor.WeChatOpenId:get()
	self.NeedBindWeChat = CS.System.String.IsNullOrEmpty(wechat_id)
	local group = self.ComUi:GetChild("WeChatBlinded").asGroup
	local select_index = 0
	self.ControllerBtn = self.ComUi:GetController("ControllerBtn")
	self.ControllerBtn.selectedIndex = select_index
	local btn_login = self.ComUi:GetChildInGroup(group,"BtnLogin").asButton
	btn_login.onClick:Add(
			function()
				self:onClickBtnLogin()
			end
	)
	local btn_gift = self.ComUi:GetChildInGroup(group,"Gift").asCom
	btn_gift.onClick:Add(
			function()
				self:onClickGiftShop()
			end
	)
	local btn_property = self.ComUi:GetChildInGroup(group,"Property").asCom
	btn_property.onClick:Add(
			function()
				self:onClickBtnProperty()
			end
	)
	local btn_moreCoin = self.ComUi:GetChildInGroup(group,"MoreCoins").asButton
	btn_moreCoin.onClick:Add(
			function()
				self:onClickMoreCoin()
			end
	)
	local btn_inviteFriend = self.ComUi:GetChildInGroup(group,"InviteFriend").asButton
	btn_inviteFriend.onClick:Add(
			function()
				self:onClickInviteFriend()
			end
	)
	local btn_moreChips = self.ComUi:GetChild("MoreChips").asButton
	btn_moreChips.onClick:Add(
			function()
				self:onClickMoreChips()
			end
	)
	local btn_bindwechat = self.ComUi:GetChild("BindWeChat").asButton
	btn_bindwechat.onClick:Add(
			function()
				self:onClickBindWeChat()
			end
	)
	self.TextBindWeChatTilte = self.ComUi:GetChild("TextBindWeChatTitle").asTextField
	self:setWeChatTitle()

	local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
	local com_shade = com_bg:GetChild("ComShade").asCom
	com_shade.onClick:Add(
			function()
				self:onClickClose()
			end
	)
	local btn_adress = self.ComUi:GetChild("BtnAddress").asButton
	btn_adress.onClick:Add(
			function()
				self:onClickResetAddress()
			end
	)
	local btn_giftHead = self.ComUi:GetChild("BtnGift").asButton
	btn_giftHead.onClick:Add(
			function()
				self:onClickGiftShop()
			end
	)
	local co_headicon = self.ComUi:GetChild("PlayerIcon").asCom
	self.ViewHeadIcon = ViewHeadIcon:new(nil,co_headicon,
			function()
				self:onClickHeadIcon()
			end
	)
	self.GImageGift = self.ComUi:GetChild("GiftBg").asImage
	self.GInputIndividualSignature = self.ComUi:GetChild("IndividualSignature").asTextInput
	self.GInputIndividualSignature.onChanged:Add(
			function()
				self:changeIndividualSignature()
			end
	)
	self.GInputNickName = self.ComUi:GetChild("NickName").asTextInput
	self.GInputNickName.onFocusOut:Add(
			function()
				self:changeNickName()
			end

	)
	local nickname_tips = self.ViewMgr.LanMgr:getLanValue("ClickNickname")
	local btn_editsign = self.ComUi:GetChild("BtnEidtSign").asButton
	btn_editsign.onClick:Add(
			function()
				self.GInputIndividualSignature:RequestFocus()
			end
	)
	local btn_editname = self.ComUi:GetChild("BtnEidtName").asButton
	btn_editname.onClick:Add(
			function()
				self.GInputNickName:RequestFocus()
			end
	)
	self.ChangeNameNeedItemTbId = TbDataHelper:GetCommonValue("ChangeNameItemId")
	if(self.ChangeNameNeedItemTbId ~= nil and self.ChangeNameNeedItemTbId ~= "")
	then
		local changename_itemtbid = 0
		local n = tonumber(self.ChangeNameNeedItemTbId)
		if(n)
		then
			changename_itemtbid = n
		end
		if (changename_itemtbid ~= 0)
		then
			local f = self.ViewMgr.LanMgr:getLanValue("NeedRenameCard")
			nickname_tips = nickname_tips .. f
		end
	end
	--self.GTextNickNameTips = self.ComUi:GetChild("Lan_Text_ClickNickname").asTextField
	--self.GTextNickNameTips.text = nickname_tips
	self.HornItemTbId = TbDataHelper:GetCommonValue("IMMarqueeCostItemId")
	self.GTextChip = self.ComUi:GetChild("Chip").asTextField
	self.GTextCion = self.ComUi:GetChild("Cion").asTextField
	self.GTextId = self.ComUi:GetChild("ID").asTextField
	self.GTextAddress = self.ComUi:GetChild("Address").asTextField
	self.GTextLevel = self.ComUi:GetChild("TextLevel").asTextField
	self.GProBarExp = self.ComUi:GetChild("Exp").asProgress
	self.GTextDoneGameNum = self.ComUi:GetChild("GameDone").asTextField
	self.GTextWinGamePersent = self.ComUi:GetChild("WinGame").asTextField
	self.GTextJoinInGameTime = self.ComUi:GetChild("JoinInGame").asTextField
	self.GTextPoint = self.ComUi:GetChild("TextPoint").asTextField
	self:initPlayerInfo()
	self.ChipIconSolustion = self.ComUi:GetController("ChipIconSolustion")
	self.ChipIconSolustion.selectedIndex = ChipIconSolustion
	local ev = self.ViewMgr:getEv("EvRequestGetPlayerModuleData")
	if(ev == nil)
	then
		ev = EvRequestGetPlayerModuleData:new(nil)
	end
	ev.factory_name = "Texas"
	self.ViewMgr:sendEv(ev)
	self.ViewMgr:bindEvListener("EvEntityGoldChanged",self)
	self.ViewMgr:bindEvListener("EvEntityDiamondChanged",self)
	self.ViewMgr:bindEvListener("EvGetPicUpLoadSuccess",self)
	self.ViewMgr:bindEvListener("EvEntityCurrentTmpGiftChange",self)
	self.ViewMgr:bindEvListener("EvEntityGetPlayerModuleDataSuccess",self)
	self.ViewMgr:bindEvListener("EvEntityPlayerInfoChanged",self)
	self.ViewMgr:bindEvListener("EvBindWeChatSuccess",self)
	self.ViewMgr:bindEvListener("EvUnBindWeChatSuccess",self)
end

function ViewPlayerInfo:onDestroy()
	self.ViewMgr:unbindEvListener(self)
end

function ViewPlayerInfo:onHandleEv(ev)
	if(ev.EventName == "EvEntityGoldChanged")
	then
		self.GTextChip.text = UiChipShowHelper:getGoldShowStr(self.ControllerActor.PropGoldAcc:get(), self.ViewMgr.LanMgr.LanBase, false)
	elseif(ev.EventName == "EvEntityDiamondChanged")
	then
		self.GTextCion.text = UiChipShowHelper:getGoldShowStr(self.ControllerActor.PropDiamond:get(), self.ViewMgr.LanMgr.LanBase, false)
	elseif(ev.EventName == "EvGetPicUpLoadSuccess")
	then
		self.ViewHeadIcon:setIcon(self.ControllerActor.PropIcon:get(), self.ControllerActor.PropAccountId:get())
		local tips = self.ViewMgr.LanMgr:getLanValue("IconSuccess")
		ViewHelper:UiShowInfoSuccess(tips)
	elseif(ev.EventName == "EvEntityCurrentTmpGiftChange")
	then
		self:setGift()
	elseif(ev.EventName == "EvEntityGetPlayerModuleDataSuccess")
	then
		local player_moduledata = ev.player_moduledata
		local player_moduledata_texas1 = self.ViewMgr:unpackData(player_moduledata.Data)
		local player_moduledata_texas =  PlayerModuleDataTexas:new(nil)
		player_moduledata_texas:setData(player_moduledata_texas1)
		self.GTextDoneGameNum.text = tostring(player_moduledata_texas.GameTotal)
		if(player_moduledata_texas.GameTotal == 0)
		then
			self.GTextWinGamePersent.text = "0" .. "%"
		else
			self.GTextWinGamePersent.text = string.format("%0.0f",(player_moduledata_texas.GameWin / player_moduledata_texas.GameTotal) * 100) .. "%"
		end
	elseif(ev.EventName == "EvEntityPlayerInfoChanged")
	then
		self:initPlayerInfo()
	elseif(ev.EventName == "EvEntityPointChanged")
	then
		self.GTextPoint.text = self.ControllerActor.PropPoint:get()
	elseif(ev.EventName == "EvBindWeChatSuccess")
	then
		self.NeedBindWeChat = false
		self:setWeChatTitle()
	elseif(ev.EventName == "EvUnBindWeChatSuccess")
	then
		self.NeedBindWeChat = true
		self:setWeChatTitle()
	end
end

function ViewPlayerInfo:initPlayerInfo()
	self.ViewHeadIcon:setPlayerInfo(self.ControllerActor.PropIcon:get(), self.ControllerActor.PropAccountId:get(),
			self.ControllerActor.PropVIPLevel:get())
	self.GInputNickName.text = CS.Casinos.UiHelper.addEllipsisToStr(self.ControllerActor.PropNickName:get(),18,5)
	self.GInputIndividualSignature.text = self.ControllerActor.PropIndividualSignature:get()
	self.GTextLevel.text = tostring(self.ControllerActor.PropLevel:get())
	self.GProBarExp.value = self:getCurrentExppro(self.ControllerActor.PropLevel:get(), self.ControllerActor.PropExperience:get())
	self.GTextChip.text = UiChipShowHelper:getGoldShowStr(self.ControllerActor.PropGoldAcc:get(), self.ViewMgr.LanMgr.LanBase, false)
	self.GTextCion.text = UiChipShowHelper:getGoldShowStr(self.ControllerActor.PropDiamond:get(), self.ViewMgr.LanMgr.LanBase, false)
	self.GTextId.text = "ID" .. " " .. CS.Casinos.UiHelperCasinos.FormatPlayerActorId(self.ControllerActor.PropActorId:get())
	self.GTextJoinInGameTime.text = CS.Casinos.LuaHelper.DataTimeToString(self.ControllerActor.PropJoinDateTime:get(),"yyyy.MM.dd")
	self.GTextPoint.text = self.ControllerActor.PropPoint:get()

	local address = self.ControllerActor.PropIpAddress:get()
	if(address == nil or address == "")
	then
		address = self.ViewMgr.LanMgr:getLanValue("Unknown")
	end
	self.GTextAddress.text = self.ViewMgr.LanMgr:getLanValue("Address") .. " " .. address
	self:setGift()
end

function ViewPlayerInfo:changeNickName()
	if(self.ChangeNameNeedItemTbId ~= nil and self.ChangeNameNeedItemTbId ~= "")
	then
		local changename_itemtbid = 0
		local n = tonumber(self.ChangeNameNeedItemTbId)
		if(n)
		then
			changename_itemtbid = n
		end
		if (changename_itemtbid ~= 0)
		then
			local changename_count = self.ControllerBag:getAlreadyHaveItemCount(changename_itemtbid)
			if (changename_count <= 0)
			then
				ViewHelper:UiShowMsgBox(self.ViewMgr.LanMgr:getLanValue("RenameCardInsufficient"),
						function()
							local ui_shop = self.ViewMgr:createView("Shop")
							ui_shop:showItem()
						end
				)
				return
			end
		end
		local nick_name = self.GInputNickName.text
		if(nick_name == nil or nick_name == "")
		then
			ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("NicknameNotNull"))
			return
		end
		ViewHelper:UiBeginWaiting(self.ViewMgr.LanMgr:getLanValue("ChangeInfo"))
		--nick_name = ViewHelper:subStrToTargetLength(nick_name, self.MaxNickNameLength)
		local ev = self.ViewMgr:getEv("EvUiClickChangePlayerNickName")
		if(ev == nil)
		then
			ev = EvUiClickChangePlayerNickName:new(nil)
		end
		ev.new_name = nick_name
		self.ViewMgr:sendEv(ev)
	end
end

function ViewPlayerInfo:changeIndividualSignature()
	ViewHelper:UiBeginWaiting(self.ViewMgr.LanMgr:getLanValue("ChangeInfo"))
	local ev = self.ViewMgr:getEv("EvUiClickChangePlayerIndividualSignature")
	if(ev == nil)
	then
		ev = EvUiClickChangePlayerIndividualSignature:new(nil)
	end
	ev.new_individual_signature = self.GInputIndividualSignature.text
	self.ViewMgr:sendEv(ev)
end

function ViewPlayerInfo:setGift()
	if (self.ControllerBag.CurrentGift ~= nil)
	then
		self.ViewHeadIcon:setGift(self.ControllerBag.CurrentGift.ItemData.item_tbid)
		self.GImageGift.visible = false
	else
		self.ViewHeadIcon:setGift(0)
		self.GImageGift.visible = true
	end
end

function ViewPlayerInfo:getCurrentExppro(level_cur,exp_cur)
	local level_next = level_cur + 1
	local tb_actorlevel_cur = self.CasinosContext.TbDataMgrLua:GetData("TbDataActorLevel",level_cur)
	local tb_actorlevel_next = self.CasinosContext.TbDataMgrLua:GetData("TbDataActorLevel",level_next)
	if (tb_actorlevel_next == nil)
	then
		return 1
	end
	local exp_total = tb_actorlevel_next.Experience - tb_actorlevel_cur.Experience
	if (exp_total <= 0)
	then
		return 0
	end
	return exp_cur * 1.0 / exp_total
end

function ViewPlayerInfo:onClickInviteFriend()
	local ui_friend = self.ViewMgr:createView("Friend")
	ui_friend:setCurrentRecommandFriend(nil)
end

function ViewPlayerInfo:onClickMoreCoin()
	local shop = self.ViewMgr:createView("Shop")
	shop:showDiamond()
end

function ViewPlayerInfo:onClickMoreChips()
	local shop = self.ViewMgr:createView("Shop")
	shop:showGold()
end

function ViewPlayerInfo:onClickHeadIcon()
	local view_icon = self.ViewMgr:getView("TakePhoto")
	if (view_icon == nil)
	then
		self.ViewMgr:createView("TakePhoto")
	end
end

function ViewPlayerInfo:onClickResetAddress()
	local ev = self.ViewMgr:getEv("EvUiClickRefreshIPAddress")
	if(ev == nil)
	then
		ev = EvUiClickRefreshIPAddress:new(nil)
	end
	self.ViewMgr:sendEv(ev)
end

function ViewPlayerInfo:onClickGiftShop()
	local ev = self.ViewMgr:getEv("EvCreateGiftShop")
	if(ev == nil)
	then
		ev = EvCreateGiftShop:new(nil)
	end
	ev.is_tmp_gift = true
	ev.not_indesktop = true
	ev.to_player_etguid = self.ControllerPlayer.Guid
	self.ViewMgr:sendEv(ev)
end

function ViewPlayerInfo:onClickBtnProperty()
	local ev = self.ViewMgr:getEv("EvCreateGiftShop")
	if(ev == nil)
	then
		ev = EvCreateGiftShop:new(nil)
	end
	ev.is_tmp_gift = false
	ev.not_indesktop = true
	ev.to_player_etguid = self.ControllerPlayer.Guid
	self.ViewMgr:sendEv(ev)
end

function ViewPlayerInfo:onClickBtnLogin()
	local ev = self.ViewMgr:getEv("EvUiClickLogin")
	if(ev == nil)
	then
		ev = EvUiClickLogin:new(nil)
	end
	self.ViewMgr:sendEv(ev)
end

function ViewPlayerInfo:onClickBindWeChat()
	if self.NeedBindWeChat then
		local ev = self.ViewMgr:getEv("EvBindWeChat")
		if(ev == nil)
		then
			ev = EvBindWeChat:new(nil)
		end
		self.ViewMgr:sendEv(ev)
	else
		local ev = self.ViewMgr:getEv("EvUnbindWeChat")
		if(ev == nil)
		then
			ev = EvUnbindWeChat:new(nil)
		end
		self.ViewMgr:sendEv(ev)
	end
end

function ViewPlayerInfo:onClickClose()
	self.ViewMgr:destroyView(self)
end

function ViewPlayerInfo:setWeChatTitle()
	local title = self.ViewMgr.LanMgr:getLanValue("UnBindWeChatTitle")
	if self.NeedBindWeChat then
		title = self.ViewMgr.LanMgr:getLanValue("BindWeChatTitle")
	end
	self.TextBindWeChatTilte.text = title
end



ViewPlayerInfoFactory = ViewFactory:new()

function ViewPlayerInfoFactory:new(o,ui_package_name,ui_component_name,
								   ui_layer,is_single,fit_screen)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	self.PackageName = ui_package_name
	self.ComponentName = ui_component_name
	self.UILayer = ui_layer
	self.IsSingle = is_single
	self.FitScreen = fit_screen
	return o
end

function ViewPlayerInfoFactory:createView()
	local view = ViewPlayerInfo:new(nil)
	return view
end