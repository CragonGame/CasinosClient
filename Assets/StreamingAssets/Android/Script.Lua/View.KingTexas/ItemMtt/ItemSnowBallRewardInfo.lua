ItemSnowBallRewardInfo = {}

function ItemSnowBallRewardInfo:new(o,com,data)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	local text_rank = com:GetChild("TextRank").asTextField
	local temp = {}
	if(data.EndRank == 0)
	then
		temp[1] = tostring(data.StartRank)
	else
		temp[1] = tostring(data.StartRank)
		temp[2] = "-"
		temp[3] = tostring(data.EndRank)
	end
	text_rank.text = table.concat(temp)
	local text_rewardRatio = com:GetChild("TextRewardRatio").asTextField
	text_rewardRatio.text = tostring(data.RewardRatio) .. "%"
	return o
end




