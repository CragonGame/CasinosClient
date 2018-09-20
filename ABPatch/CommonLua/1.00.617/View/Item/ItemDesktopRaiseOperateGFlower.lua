ItemDesktopRaiseOperateGFlower = {}

function ItemDesktopRaiseOperateGFlower:New(o,ui_operate,com,tb_operateid)
	o = o or {}
    setmetatable(o, self)
    self.__index = self
	o.UiOperate = ui_operate
    o.GCom = com
    o.ControllerEnable = GCom:GetController("ControllerEnable")
    o.TbOperateId = tb_operateid
    o.GTextRaiseValue = com:GetChild("TextRaiseValue").asTextField
	local tb_operate = CasinosContext.Instance.TbDataMgrLua:GetData("TbDataDesktopGFlowerBetOperate",tb_operateid)
    o.GTextRaiseValue.text = CS.Casinos.CasinosContext.Instance.UiChipShowHelper:getGoldShowStr(tb_operate.BetOperate,
                                CS.Casinos.CasinosContext.Instance.LanMgr.LanBase, false)
    com.enabled = false
    o.ControllerEnable.selectedIndex = 1
    com.onClick:Add(
		function()
			o:onClickItem()
		end
	)
	return o
end

function ItemDesktopRaiseOperateGFlower:updateBetOperateEnabled(self_gold,max_single_bet)
	local enabled = false
    if (self.OperateGold >= self_gold and self.OperateGold >= max_single_bet * 2)
	then
		enabled = true
	end
    self.GCom.enabled = enabled
	if(enabled)
	then
		self.ControllerEnable.selectedIndex = 0
	else
		self.ControllerEnable.selectedIndex = 1
	end
end

function ItemDesktopRaiseOperateGFlower:setPlayerReadCard(is_read,self_gold,max_single_bet)
	local tb_operate = CasinosContext.Instance.TbDataMgrLua:GetData("TbDataDesktopGFlowerBetOperate",self.TbOperateId)
    local operate_gold = tb_operate.BetOperate
    if (is_read)
	then
		operate_gold = operate_gold * 2
	end
    self.OperateGold = operate_gold
    self.GTextRaiseValue.text = CS.Casinos.CasinosContext.Instance.UiChipShowHelper:getGoldShowStr(operate_gold,
               CS.Casinos.CasinosContext.Instance.LanMgr.LanBase, false)
    self:updateBetOperateEnabled(self_gold, max_single_bet)
end

function ItemDesktopRaiseOperateGFlower:onClickItem()
	self.UiOperate:raise(self.OperateGold)
end