ViewReportPlayer = ViewBase:new()

function ViewReportPlayer:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self	
	self.ViewMgr = nil
	self.GoUi = nil
	self.ComUi = nil
	self.Panel = nil
	self.UILayer = nil
	self.InitDepth = nil
	self.ViewKey = nil

    return o
end

function ViewReportPlayer:onCreate()
	ViewHelper:PopUi(self.ComUi)
	local com_shade = self.ComUi:GetChild("ComShade").asCom
    com_shade.onClick:Add(
		function()
			self:onClickClose()
		end
	)
    local com_Head_icon = self.ComUi:GetChild("HeadIcon").asCom
    self.ViewHeadIcon = ViewHeadIcon:new(nil,com_Head_icon)
    self.GTextPlayerIndividualSignature = self.ComUi:GetChild("PlayerIndividualSignature").asTextField
    self.GTextID = self.ComUi:GetChild("ID").asTextField
    self.GTextNickName = self.ComUi:GetChild("NickName").asTextField
    self.GTextAddress = self.ComUi:GetChild("Address").asTextField
    self.GTextChip = self.ComUi:GetChild("Chip").asTextField
    self.GTextState = self.ComUi:GetChild("State").asTextField
    self.GTextLevel = self.ComUi:GetChild("TextLevel").asTextField
    self.GProBarLevelValue = self.ComUi:GetChild("ProgressLevel").asProgress
    self.mMapItem = {}
    self.GListContent = self.ComUi:GetChild("ContentList").asList
	for i = 1,4 do
		local com_btn = self.GListContent:AddItemFromPool().asCom
        local item = ItemReportPlayerOperate:new(nil,com_btn,self)
		self.mMapItem[i - 1] = item
	end
end

function ViewReportPlayer:setReportPlayerInfo(player_info)
	local player_icon = player_info.PlayerInfoCommon.IconName
    local player_accountid = player_info.PlayerInfoCommon.AccountId
    local player_signature = player_info.PlayerInfoMore.IndividualSignature
    local player_nickname = player_info.PlayerInfoCommon.NickName
    local ip_address = player_info.PlayerInfoMore.IPAddress
    local et_guid = player_info.PlayerInfoCommon.PlayerGuid
    local id = tostring(player_info.PlayerInfoCommon.PlayerId)
    local playerGold = player_info.PlayerInfoMore.Gold
    local level_ex = self.ViewMgr.LanMgr:getLanValue("Level:")
    local level = CS.Casinos.CasinosContext.Instance:AppendStrWithSB(level_ex,tostring(player_info.PlayerInfoMore.Level))
    self.GTextLevel.text = level
    self.ViewHeadIcon:setPlayerInfo(player_icon, player_accountid, 0)
    self.GTextPlayerIndividualSignature.text = player_signature
    self.GTextNickName.text = player_nickname
    self.GTextChip.text = UiChipShowHelper:getGoldShowStr(playerGold, self.ViewMgr.LanMgr.LanBase)
    self.GProBarLevelValue.value = self:setCurrentExppro(player_info.PlayerInfoMore.Level, player_info.PlayerInfoMore.Exp) * 100
    local state = CasinoHelper:TranslateFriendStateEx(player_info)
    local friend_state_str = CasinoHelper:TranslateFriendState(state)
    self.GTextState.text = friend_state_str
    self.GTextID.text = "ID: " .. id
    local address = ip_address
	if(address == nil or address == "")
	then
		address = self.ViewMgr.LanMgr:getLanValue("Unknown")
	end
    address = self.ViewMgr.LanMgr:getLanValue("Address") .. address
    self.GTextAddress.text = address

    local index = 0
	local array = CS.System.Enum.GetValues(typeof(CS.Casinos.ReportPlayerType))
	for i = 1,array.Length do
		local temp = array[i - 1]
		local item_report_player_operate = self.mMapItem[index]
		item_report_player_operate:setReportType(CS.Casinos.ReportPlayerType.__CastFrom(i - 1),et_guid)
		index = index + 1
	end
end

function ViewReportPlayer:onClickClose()
	self.ViewMgr:destroyView(self)
end

function ViewReportPlayer:setCurrentExppro(level_cur,exp_cur)
	local level_next = level_cur + 1
    local tb_actorlevel_cur = CS.Casinos.CasinosContext.Instance.TbDataMgrLua:GetData("ActorLevel",level_cur)
    local tb_actorlevel_next = CS.Casinos.CasinosContext.Instance.TbDataMgrLua:GetData("ActorLevel",level_next)
    if (tb_actorlevel_next == nil)
	then
		return 1
	end

    local exp_total = tb_actorlevel_next.Experience - tb_actorlevel_cur.Experience
    if (exp_total <= 0)
	then
		--EbLog.Error("CellActor._onPropExperienceChanged() Error: exp_total<=0 level_cur=" + level_cur);
        return 0
	end

    return exp_cur * 1 / exp_total
end
					

			

ViewReportPlayerFactory = ViewFactory:new()

function ViewReportPlayerFactory:new(o,ui_package_name,ui_component_name,
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

function ViewReportPlayerFactory:createView()	
	local view = ViewReportPlayer:new(nil)	
	return view
end