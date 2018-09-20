ViewSnowBallReward = ViewBase:new()

function ViewSnowBallReward:new(o)
    o = o or {}
    setmetatable(o,self)
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

function ViewSnowBallReward:onCreate()
	ViewHelper:PopUi(self.ComUi,"滚雪球赛奖励说明")
	local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
	local btn_close = com_bg:GetChild("BtnClose").asButton
	btn_close.onClick:Add(
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

	self.GListPlayerNum = self.ComUi:GetChild("ListPlayerNum").asList
	self.GListRewardInfo = self.ComUi:GetChild("ListRewardInfo").asList
	self.TbDataMgr = CS.Casinos.CasinosContext.Instance.TbDataMgrLua
	self.ListItemSnowBallRewardPlayerNum = {}
	self:setListPlayerNum()
	self:SetCurrentItemSnowBallRewardPlayerNum(self.ListItemSnowBallRewardPlayerNum[1])
end

function ViewSnowBallReward:setListPlayerNum()
	local table_playerNum = self.TbDataMgr:GetMapData("TexasSnowBallRewardPlayerNum")
	for i = 1,#table_playerNum do
		local com = self.GListPlayerNum:AddItemFromPool()
		local data = table_playerNum[i]
		local item = ItemSnowBallRewardPlayerNum:new(nil,com,data,self)
		table.insert(self.ListItemSnowBallRewardPlayerNum,item)
	end
end

function ViewSnowBallReward:SetCurrentItemSnowBallRewardPlayerNum(item)
	if(self.CurrentItemSnowBallRewardPlayerNum == item)
	then
		return
	else
		if(self.CurrentItemSnowBallRewardPlayerNum ~= nil)
		then
			self.CurrentItemSnowBallRewardPlayerNum:BeSelectOrNot(false)
		end
		self.CurrentItemSnowBallRewardPlayerNum = item
		self.CurrentItemSnowBallRewardPlayerNum:BeSelectOrNot(true)
		self:setListRewardInfo(self.CurrentItemSnowBallRewardPlayerNum.Id)
	end
end

function ViewSnowBallReward:setListRewardInfo(reward_id)
	self.GListRewardInfo:RemoveChildrenToPool()
	local list_info = TbDataHelper:GetTexasSnowBallRewradInfoByTableId(reward_id)
	for i = 1,#list_info do
		local com = self.GListRewardInfo:AddItemFromPool()
		local data = list_info[i]
		ItemSnowBallRewardInfo:new(nil,com,data)
	end
end

function ViewSnowBallReward:onClickBtnClose()
	self.ViewMgr:destroyView(self)
end


ViewSnowBallRewardFactory = ViewFactory:new()

function ViewSnowBallRewardFactory:new(o,ui_package_name,ui_component_name,
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

function ViewSnowBallRewardFactory:createView()	
	local view = ViewSnowBallReward:new(nil)	
	return view
end