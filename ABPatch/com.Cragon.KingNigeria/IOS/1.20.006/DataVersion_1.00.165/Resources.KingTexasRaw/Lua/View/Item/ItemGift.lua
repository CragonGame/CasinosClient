ItemGift = {}

function ItemGift:new(o)
	o = o or {}
    setmetatable(o,self)
    self.__index = self

	return o
end

function ItemGift:init(com,lan_mgr)
	self.LanMgr = lan_mgr
	self.CasinosContext = CS.Casinos.CasinosContext.Instance
	self.Com = com
	self.GLoaderIcon = self.Com:GetChild("LoaderGift").asLoader
	local name = self.Com:GetChild("GiftName")
	if (name ~= nil)
	then
		self.GTextGiftName = name.asTextField
	end
	local value = self.Com:GetChild("GiftValue")
	if (value ~= nil)
	then
		self.GTextGiftValue = value.asTextField
	end
	local count = self.Com:GetChild("ItemCount")
	if (count ~= nil)
	then
		self.GTextItemCount = count.asTextField
	end
	self.ControllerBg = self.Com:GetController("ConttollerBg")
end

function ItemGift:setGift(gift_id,is_buygift,is_mine,to_etguid,from_name,gift_belong,item)
	self.ItemId = gift_id
    self.IsBuy = is_buygift
    self.IsMine = is_mine
    self.ToGuid = to_etguid
    self.FromName = from_name
    self.GiftBelong = gift_belong
    self.Item = item
    local tb_gift = self.CasinosContext.TbDataMgrLua:GetData("Item",gift_id)
    if (tb_gift.UnitType == "GiftTmp")
	then
		self.ControllerBg.selectedIndex = 0
	elseif(tb_gift.UnitType == "GiftNormal")
	then
		self.ControllerBg.selectedIndex = 1
	end
    self.GLoaderIcon.icon = self.CasinosContext.PathMgr:combinePersistentDataPath(
        CS.Casinos.UiHelperCasinos:getABItemResourceTitlePath() .. string.lower(tb_gift.Icon) .. ".ab")
    if (self.GTextGiftName ~= nil)
	then
		self.GTextGiftName.text = self.LanMgr:getLanValue(tb_gift.Name, tb_gift.Name)
	end
    if (self.GTextGiftValue ~= nil)
	then
		local price = UiChipShowHelper:getGoldShowStr(tb_gift.Price, self.LanMgr.LanBase, false)
		if(CS.Casinos.PriceType.__CastFrom(tb_gift.PriceType) == CS.Casinos.PriceType.Chip)
		then
		elseif(CS.Casinos.PriceType.__CastFrom(tb_gift.PriceType) == CS.Casinos.PriceType.Gold)
		then
			price = price .. self.LanMgr:getLanValue("钻石", "Diamonds")
		elseif(tb_gift.PriceType == 4) -- 积分
		then
			price = price .. self.LanMgr:getLanValue("万积分", "万积分")
		end
        self.GTextGiftValue.text = price
	end
    if (item ~= nil and item.ItemData ~= nil and self.GTextItemCount ~= nil)
	then
		self.GTextItemCount.text = "*" .. tostring(item.ItemData.count)
	end
    self.Com.onClick:Add(
		function()
			self:onClick()
		end
	)
end

function ItemGift:onClick()
	local view_mgr = ViewMgr:new(nil)
	local view_playerprofile = view_mgr:getView("PlayerProfile")
	if(self.IsBuy == false and view_playerprofile ~= nil)
	then
		return
	end
	local gift_detail = view_mgr:createView("GiftDetail")
    gift_detail:setGift(self.ItemId, self.IsBuy, self.IsMine, self.ToGuid, self.FromName, self.GiftBelong, self.Item)
end

function ItemGift:reset()
	self.LanMgr = nil
	self.CasinosContext = nil
	self.Com = nil
	self.GLoaderIcon = nil
	self.GTextGiftName = nil
	self.GTextGiftValue = nil
	self.GTextItemCount = nil
	self.ControllerBg = nil
end