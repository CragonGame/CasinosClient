ItemSnowBallRewardPlayerNum = {}

function ItemSnowBallRewardPlayerNum:new(o,com,data,view_snowBallReward)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	o.Id = data.Id
	o.ViewSnowBallReward = view_snowBallReward
	com.onClick:Clear()
	com.onClick:Add(
		function()
			o:onClickSelf()
		end
	)
	o.GControllerIsSelected = com:GetController("ControllerSelected")
	o.GTextSignUpNum = com:GetChild("TextSignUpNum").asTextField
	local temp = {}
	temp[1] = tostring(data.MinPlayerNum)
	temp[2] = "-"
	temp[3] = tostring(data.MaxPlayerNum)
	temp[4] = view_snowBallReward.ViewMgr.LanMgr:getLanValue("People")
	o.GTextSignUpNum.text = table.concat(temp)
	o.GTextRewardNum = com:GetChild("TextRewardNum").asTextField
	local temp1 = {}
	temp1[1] = view_snowBallReward.ViewMgr.LanMgr:getLanValue("Reward")
	temp1[2] = tostring(data.RewardNum)
	temp1[3] = view_snowBallReward.ViewMgr.LanMgr:getLanValue("People")
	o.GTextRewardNum.text = table.concat(temp1)
	o:BeSelectOrNot(false)
	return o
end

function ItemSnowBallRewardPlayerNum:BeSelectOrNot(selected)
	if(selected)
	then
		self.GControllerIsSelected.selectedIndex = 1
		self.GTextSignUpNum.color = CS.UnityEngine.Color.white
		self.GTextRewardNum.color = CS.UnityEngine.Color(1,0.85,0.48)
	else
		self.GControllerIsSelected.selectedIndex = 0
		local color_temp = CS.UnityEngine.Color(0.32,0.39,0.65)
		self.GTextSignUpNum.color = color_temp
		self.GTextRewardNum.color = color_temp
	end
end

function ItemSnowBallRewardPlayerNum:onClickSelf()
	self.ViewSnowBallReward:SetCurrentItemSnowBallRewardPlayerNum(self)
end
