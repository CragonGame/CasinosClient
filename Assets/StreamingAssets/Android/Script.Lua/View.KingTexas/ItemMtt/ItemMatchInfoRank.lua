-- Copyright(c) Cragon. All rights reserved.

ItemMatchInfoRank = {}

function ItemMatchInfoRank:new(o,com,info,rank,lan_base)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	if(rank <= 3)
	then
		local loader_rank = com:GetChild("LoaderRank").asLoader
		loader_rank.url = CS.FairyGUI.UIPackage.GetItemURL("Common","Rank" .. tostring(rank))
	else
		local text_rank = com:GetChild("TextRank").asTextField
		text_rank.text = tostring(rank)
	end
	local text_name = com:GetChild("TextPlayerName").asTextField
	text_name.text = CS.Casinos.UiHelper.addEllipsisToStr(info.NickName,30,9)
	local text_chip = com:GetChild("TextChipAmount").asTextField
	text_chip.text = UiChipShowHelper:getGoldShowStr(info.Score,lan_base)
	--local loader_headicon = com:GetChild("LoaderHeadIcon").asLoader
	--local temp_table = CS.Casinos.LuaHelper.getIconName(true,info.AccId,info.Icon)
	--local icon = temp_table[1]
	--if(icon ~= nil and string.len(icon) > 0)
	--then
	--   loader_headicon.icon = PlayerIconDomain .. icon
	--end
	return o
end


