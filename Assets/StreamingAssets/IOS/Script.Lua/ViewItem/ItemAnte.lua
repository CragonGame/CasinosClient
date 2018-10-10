-- Copyright(c) Cragon. All rights reserved.
-- 废弃

ItemAnte = {}

function ItemAnte:new(o)
	o = o or {}
	setmetatable(o,self)
	self.__index = self

	return o
end

function ItemAnte:setAnte(com,lan_mgr,ante)
	local btn_ante = com.asButton
	btn_ante.title = UiChipShowHelper:getGoldShowStr(ante, lan_mgr.LanBase)
	self.Ante = ante
	--com.onClick:Add(
	--		function()
	--			self:onClick()
	--		end
	--)
end

function ItemAnte:onClick()
	local view_mgr = ViewMgr:new(nil)
	local view_playerprofile = view_mgr:getView("PlayerProfile")
	if(self.IsBuy == false and view_playerprofile ~= nil)
	then
		return
	end
	local gift_detail = view_mgr:createView("GiftDetail")
	gift_detail:setGift(self.ItemId, self.IsBuy, self.IsMine, self.ToGuid, self.FromName, self.GiftBelong, self.Item)
end