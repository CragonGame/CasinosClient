ItemAttachment = {}

function ItemAttachment:new(o,com,item_data,gold,diamond,view_mgr)
	o = o or {}
	setmetatable(o, self)
	self.__index = self
	self.CasinosContext = CS.Casinos.CasinosContext.Instance
	self.ViewMgr = view_mgr
	o.GTextAttachmentContent = com:GetChild("ItemContent").asTextField
	o.GLoaderAttachment = com:GetChild("GLoaderItem").asLoader
	local content = ""
	local icon_url = ""
	if (item_data ~= nil)
	then
		local i_d = ItemData1:new(nil)
		i_d:setData(item_data)
		local tb_item = self.ViewMgr.TbDataMgr:GetData("Item",i_d.item_tbid)
		content = tb_item.Name .. "*" .. i_d.count
		icon_url = self.CasinosContext.PathMgr:combinePersistentDataPath(ViewHelper:getABItemResourceTitlePath() .. string.lower(tb_item.Icon) .. ".ab")
	end
	if (gold > 0)
	then
		content = self.ViewMgr.LanMgr:getLanValue("Chip").."\n"..
				UiChipShowHelper:getGoldShowStr(gold, self.ViewMgr.LanMgr.LanBase)
		icon_url = CS.FairyGUI.UIPackage.GetItemURL("Common","ChipIcon"..ChipIconSolustion)
		o.GLoaderAttachment.color = CS.UnityEngine.Color(0.38,0.89,1)
	end
	if (diamond > 0)
	then
		content = self.ViewMgr.LanMgr:getLanValue("Coin").."\n"..
				UiChipShowHelper:getGoldShowStr(diamond, self.ViewMgr.LanMgr.LanBase)
		icon_url = CS.FairyGUI.UIPackage.GetItemURL("Common","DiamondIcon"..ChipIconSolustion)
	end
	o.GTextAttachmentContent.text = content
	o.GLoaderAttachment.icon = icon_url
	return o
end