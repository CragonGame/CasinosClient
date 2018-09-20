ItemNumOperate = {}

function ItemNumOperate:new(o,co,init_num,per_operate_num,use_shortway_shownum,lan_mgr)
	o = o or {}
    setmetatable(o, self)
    self.__index = self
	self.LanMgr = lan_mgr
	o.GCoNumOperate = co
    o.GTextNum = o.GCoNumOperate:GetChild("Num").asTextInput
    o.GTextNum.onChanged:Add(
		function()
			local current_num = tonumber(o.GTextNum.text)
            o.CurrentNum = current_num
		end
	)
    --[[o.GBtnMinue = o.GCoNumOperate:GetChild("BtnMinue").asButton
    o.GBtnMinue.onClick:Add(
		function()
			o:onClickBtnMinue()
		end
	)]]
    --[[o.GBtnPlus = o.GCoNumOperate:GetChild("BtnPlus").asButton
    o.GBtnPlus.onClick:Add(
		function()
			o:onClickBtnPlus()
		end
	)]]
    o.PerOperateNum = per_operate_num
    o.UseShortWayShowNum = use_shortway_shownum
    o.CurrentNum = init_num
    if (o.CurrentNum <= 0)
	then
		o.CurrentNum = 0
	end
    o.GTextNum.text = UiChipShowHelper:getGoldShowStr(init_num, self.LanMgr.LanBase,o.UseShortWayShowNum)
	return o
end

function ItemNumOperate:getCurrentNum()
	return self.CurrentNum
end

function ItemNumOperate:setCurrentNum(num)
	self.CurrentNum = num
    self.GTextNum.text = UiChipShowHelper:getGoldShowStr(self.CurrentNum, self.LanMgr.LanBase,self.UseShortWayShowNum)
end

function ItemNumOperate:_onClickBtnMinue()
	self.CurrentNum = self.CurrentNum - self.PerOperateNum
    if (CurrentNum <= 0)
	then
		self.CurrentNum = 0
	end
    self.GTextNum.text = UiChipShowHelper:getGoldShowStr(self.CurrentNum, self.LanMgr.LanBase,self.UseShortWayShowNum) 
end

function ItemNumOperate:_onClickBtnPlus()
	self.CurrentNum = self.CurrentNum + self.PerOperateNum
    if (CurrentNum <= 0)
	then
		self.CurrentNum = 0
	end
    self.GTextNum.text = UiChipShowHelper:getGoldShowStr(self.CurrentNum, self.LanMgr.LanBase,self.UseShortWayShowNum)
end