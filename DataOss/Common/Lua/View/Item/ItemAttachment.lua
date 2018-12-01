-- Copyright(c) Cragon. All rights reserved.
-- 邮件附件的一个附件

---------------------------------------
ItemAttachment = {}

---------------------------------------
function ItemAttachment:new(o, com, item_data, gold, diamond, view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.Context = Context
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.ViewMgr = view_mgr
    o.GTextAttachmentContent = com:GetChild("ItemContent").asTextField
    o.GLoaderAttachment = com:GetChild("GLoaderItem").asLoader
    local content = ""
    local icon_url = ""
    if (item_data ~= nil) then
        local i_d = ItemData1:new(nil)
        i_d:setData(item_data)
        local item = Item:new(nil, self.ViewMgr.TbDataMgr, i_d)
        local tb_item = self.ViewMgr.TbDataMgr:GetData("Item", i_d.item_tbid)
        if item.UnitLink == "WechatRedEnvelopes" then
            content = item.UnitLink .. self.ViewMgr.LanMgr:getLanValue("Yuan") .. tb_item.Name
        else
            content = tb_item.Name .. "*" .. i_d.count
        end
        icon_url = self.CasinosContext.PathMgr.DirAbItem .. string.lower(tb_item.Icon) .. ".ab"
    end
    if (gold > 0) then
        content = self.ViewMgr.LanMgr:getLanValue("Chip") .. "\n" ..
                UiChipShowHelper:GetGoldShowStr(gold, self.ViewMgr.LanMgr.LanBase)
        icon_url = CS.FairyGUI.UIPackage.GetItemURL("Common", "ChipIcon" .. self.Context.Cfg.ChipIconSolustion)
        o.GLoaderAttachment.color = CS.UnityEngine.Color(0.38, 0.89, 1)
    end
    if (diamond > 0) then
        content = self.ViewMgr.LanMgr:getLanValue("Coin") .. "\n" ..
                UiChipShowHelper:GetGoldShowStr(diamond, self.ViewMgr.LanMgr.LanBase)
        icon_url = CS.FairyGUI.UIPackage.GetItemURL("Common", "DiamondIcon" .. self.Context.Cfg.ChipIconSolustion)
    end
    o.GTextAttachmentContent.text = content
    o.GLoaderAttachment.icon = icon_url
    return o
end