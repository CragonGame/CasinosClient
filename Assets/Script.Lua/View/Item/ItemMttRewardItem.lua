-- Copyright(c) Cragon. All rights reserved.
-- 赛事结束后的比赛结果全屏界面中的一个奖励Item

ItemMttRewardItem = {}

function ItemMttRewardItem:new(o,com,item_id,gold,diamond,view_mgr)
	o = o or {}
    setmetatable(o, self)
    self.__index = self
	self.CasinosContext = CS.Casinos.CasinosContext.Instance
	self.ViewMgr = view_mgr
	o.GTextAttachmentContent = com:GetChild("GiftName").asTextField
	o.GTextRewardTitle = com:GetChild("GiftValue").asTextField
    o.GLoaderAttachment = com:GetChild("LoaderGift").asLoader
    local content = ""
    local icon_url = ""
    if (item_id ~= 0)
	then
		local tb_item = self.ViewMgr.TbDataMgr:GetData("Item",item_id)
        content = tb_item.Name
        icon_url = self.CasinosContext.PathMgr:combinePersistentDataPath(ViewHelper:getABItemResourceTitlePath() .. string.lower(tb_item.Icon) .. ".ab")
	end
    if (gold > 0)
	then
		content = self.ViewMgr.LanMgr:getLanValue("Chip").."X"..
				  UiChipShowHelper:getGoldShowStr(gold, self.ViewMgr.LanMgr.LanBase)
        icon_url = CS.FairyGUI.UIPackage.GetItemURL("Common","ChipIcon")
		o.GLoaderAttachment.color = CS.UnityEngine.Color(0.38,0.89,1)
	end
    if (diamond > 0)
	then
		content = self.ViewMgr.LanMgr:getLanValue("Coin").."X"..
				  UiChipShowHelper:getGoldShowStr(diamond, self.ViewMgr.LanMgr.LanBase)
        icon_url = CS.FairyGUI.UIPackage.GetItemURL("Common","Diamond")
	end
    o.GTextAttachmentContent.text = content
	o.GTextRewardTitle.text = self.ViewMgr.LanMgr:getLanValue("InMailBox")
    o.GLoaderAttachment.icon = icon_url
	return o
end