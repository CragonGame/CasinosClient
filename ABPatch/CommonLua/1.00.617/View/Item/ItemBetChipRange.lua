ItemBetChipRange = {}

function ItemBetChipRange:new(o,item,eb_data,index)
	o = o or {}
    setmetatable(o, self)
    self.__index = self
	o.Item = item
    o.EbData = eb_data
    o.GTextBetChipRange = o.Item:GetChild("BetChipRange").asTextField
    o.CurrentIndex = index
    o.ViewMgr = ViewMgr:new(nil)
    local tb_datadektop = o.EbData
    local  tips = UiChipShowHelper:getGoldShowStr(tb_datadektop.SmallBlind, o.ViewMgr.LanMgr.LanBase) .. "/" ..
            UiChipShowHelper:getGoldShowStr(tb_datadektop.BigBlind, o.ViewMgr.LanMgr.LanBase)
    o.GTextBetChipRange.text = tips
	return o
end

function ItemBetChipRange:getEbData()
	return self.EbData
end