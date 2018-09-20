ItemLobbyDesk = {}

function ItemLobbyDesk:new(o, item, desk_info, on_click_createdesktop, view_mgr)
	o = o or {}
	setmetatable(o, self)
	self.__index = self
	o.ViewMgr = view_mgr
	o.NineSeatParent = "NineSeatSeat"
	o.FiveSeatParent = "FiveSeatSeat"
	o.MapItemPlayerInfo = {}
	o.Item = CS.Casinos.LuaHelper.GObjectCastToGCom(item)
	o.DeskTopInfo = desk_info
	o.ControllerDesk = o.Item:GetController("ControllerDesk")
	o.GBtnCreateLableL = o.Item:GetChild("BtnCreateTableL").asButton
	o.GBtnCreateLableL.onClick:Add(on_click_createdesktop)
	o.GBtnCreateLableR = o.Item:GetChild("BtnCreateTableR").asButton
	o.GBtnCreateLableR.onClick:Add(on_click_createdesktop)
	o.GComPlay = o.Item:GetChild("BtnPlay").asCom
	o.GComPlay.onClick:Set(
			function()
				o:onClickBtnPlay()
			end
	)
	o.GLoaderPlayOrView = o.GComPlay:GetChild("LoaderPlayOrView").asLoader
	o.GTextBlindBetTips = o.Item:GetChild("TextBlindBetTips").asTextField
	o.Item.onClick:Set(
			function(a)
				o:onClickSelf(a)
			end
	)
	o.IsBig = false
	return o
end

function ItemLobbyDesk:init()
	self:setDesktopFilter()
	local controller_actor = self.ViewMgr.ControllerMgr:GetController("Actor")
	local CasinosContext = CS.Casinos.CasinosContext.Instance
	local gold_acc = controller_actor.PropGoldAcc:get()
	local tb_desktop = CasinosContext.TbDataMgrLua:GetData("DesktopInfoTexas", self.DeskTopInfo.desktop_tableid)
    local desk_play_chips
    if self.DeskTopInfo.ante >0 then
        desk_play_chips = UiChipShowHelper:getGoldShowStr(tb_desktop.SmallBlind, self.ViewMgr.LanMgr.LanBase)
                .. "/" .. UiChipShowHelper:getGoldShowStr(tb_desktop.BigBlind, self.ViewMgr.LanMgr.LanBase) .. ","
                .. UiChipShowHelper:getGoldShowStr(self.DeskTopInfo.ante, self.ViewMgr.LanMgr.LanBase)
    else
        desk_play_chips = UiChipShowHelper:getGoldShowStr(tb_desktop.SmallBlind, self.ViewMgr.LanMgr.LanBase)
                .. "/" .. UiChipShowHelper:getGoldShowStr(tb_desktop.BigBlind, self.ViewMgr.LanMgr.LanBase)
    end

	self.GTextBlindBetTips.text = desk_play_chips
	if (self.DeskTopInfo.seat_num == TexasDesktopSeatNum.Five)
	then
		self.ControllerDesk:SetSelectedIndex(0)
		self.GGroupSeats = self.Item:GetChild("GroupSeatFive").asGroup
		self.SeatParentName = self.FiveSeatParent
		self.SeatCount = 5
	elseif (self.DeskTopInfo.seat_num == TexasDesktopSeatNum.Unlimited)
	then
	elseif (self.DeskTopInfo.seat_num == TexasDesktopSeatNum.Nine)
	then
		self.ControllerDesk:SetSelectedIndex(1)
		self.GGroupSeats = self.Item:GetChild("GroupSeatNine").asGroup
		self.SeatParentName = self.NineSeatParent
		self.SeatCount = 9
	end
	self.CanPlay = TexasHelper:enoughChip4DesktopBetMin(CasinosContext.TbDataMgrLua, gold_acc,
			self.DeskTopInfo.desktop_tableid)
	local play_or_view = nil
	if (self.DeskTopInfo:isFull() or self.CanPlay == false)
	then
		play_or_view = "View"
	else
		play_or_view = "Play"
	end
	self.GLoaderPlayOrView.url = CS.FairyGUI.UIPackage.GetItemURL("ClassicModel", play_or_view)
	if (self.DeskTopInfo.list_seat_player ~= nil)
	then
		for i, v in pairs(self.DeskTopInfo.list_seat_player) do
			local temp = DesktopPlayerInfo:new(nil)
			temp.seat_index = v[1]
			temp.player_guid = v[2]
			temp.nick_name = v[3]
			temp.account_id = v[4]
			temp.chip = v[5]
			temp.icon = v[6]
			temp.is_bot = v[7]
			local seat_name = self.SeatParentName .. tostring(temp.seat_index)
			local seat = self.Item:GetChildInGroup(self.GGroupSeats, seat_name).asCom
			local itemPlayerInfo = ItemLobbyDeskPlayInfo:new(nil, seat, temp, self.ViewMgr.LanMgr)
			itemPlayerInfo:ShowHeadIconUseTween()
			self.MapItemPlayerInfo[seat_name] = itemPlayerInfo
		end
	end
end

function ItemLobbyDesk:recycleSeats()
	self.GBtnCreateLableL.visible = false
	self.GBtnCreateLableR.visible = false
	for key, value in pairs(self.MapItemPlayerInfo) do
		value:reset()
	end
end

function ItemLobbyDesk:SwitchBig(big)
	if (big)
	then
		self:toBeCenter()
	else
		self:NotBeCenter(true)
	end
end

function ItemLobbyDesk:ShowCreateBtnL()
	self.GBtnCreateLableL.visible = true
end

function ItemLobbyDesk:ShowCreateBtnR()
	self.GBtnCreateLableR.visible = true
end

function ItemLobbyDesk:NotBeCenter(usetween)
	self.GComPlay.visible = false
	for key, value in pairs(self.MapItemPlayerInfo) do
		value:HideHeadIcon()
	end
	if (usetween)
	then
		self.Item:TweenScale(CS.UnityEngine.Vector2.one, 0.25)
	else
		self.Item:SetScale(1, 1)
	end
end

function ItemLobbyDesk:toBeCenter()
	self.GComPlay.visible = true
	for key, value in pairs(self.MapItemPlayerInfo) do
		value:ShowHeadIconUseTween()
	end
	self.Item:TweenScale(CS.UnityEngine.Vector2.one * 1.1, 0.25)
end

function ItemLobbyDesk:setDesktopFilter()
	local desktop_filter = DesktopFilterTexas:new(nil)
	desktop_filter.desktop_tableid = self.DeskTopInfo.desktop_tableid
	desktop_filter.game_speed = self.DeskTopInfo.game_speed
	desktop_filter.is_seat_full = false
	desktop_filter.is_vip = self.DeskTopInfo.is_vip
	desktop_filter.seat_num = self.DeskTopInfo.seat_num
	self.DesktopFilter = DesktopFilter:new(nil)
	self.DesktopFilter.FactoryName = "Texas"
	self.DesktopFilter.IncludeFull = false
	local d_p = desktop_filter:getData4Pack()
	local desktop_filter_bytes = self.ViewMgr:packData(d_p)
	self.DesktopFilter.FilterData = desktop_filter_bytes
end

function ItemLobbyDesk:onClickBtnPlay()
	if (self.DeskTopInfo:isFull() == false and self.CanPlay)
	then
		local view_mgr = ViewMgr:new(nil)
		local ev = view_mgr:getEv("EvUiClickPlayInDesk")
		if (ev == nil)
		then
			ev = EvUiClickPlayInDesk:new(nil)
		end
		ev.desk_etguid = self.DeskTopInfo.desktop_etguid
		ev.seat_index = 255
		ev.desktop_filter = self.DesktopFilter
		view_mgr:sendEv(ev)
	else
		self:clickEnterDesk()
	end
end

function ItemLobbyDesk:clickEnterDesk()
	local view_mgr = ViewMgr:new(nil)
	local ev = view_mgr:getEv("EvUiClickViewInDesk")
	if (ev == nil)
	then
		ev = EvUiClickViewInDesk:new(nil)
	end
	ev.desk_etguid = self.DeskTopInfo.desktop_etguid
	ev.seat_index = 255
	ev.desktop_filter = self.DesktopFilter
	view_mgr:sendEv(ev)
end

function ItemLobbyDesk:onClickSelf(e_c)
	if (e_c.inputEvent.isDoubleClick)
	then
		self:clickEnterDesk()
	end
end