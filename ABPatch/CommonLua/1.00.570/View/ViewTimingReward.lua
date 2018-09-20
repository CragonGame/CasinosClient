ViewTimingReward = {}

function ViewTimingReward:new(o,co_timingreward,view_mgr)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
	self.ViewMgr = view_mgr
	o.GCoTimingReward = co_timingreward
	o.ControllerTimingReward = co_timingreward:GetController("ControllerCanGet")
	o.CanGetReward = false
    o.GCoTimingReward.onClick:Add(
		function()
			o:onClickTimingReward()
		end
	)
    return o
end

function ViewTimingReward:setCanGetReward(can_get_reward)
	self.CanGetReward = can_get_reward
	local index = 0
	if can_get_reward then
		index = 1
	end
	self.ControllerTimingReward.selectedIndex = index
end

function ViewTimingReward:onClickTimingReward()
	if self.CanGetReward then
		local ev = self.ViewMgr:getEv("EvRequestGetTimingReward")
		if(ev == nil)
		then
			ev = EvRequestGetTimingReward:new(nil)
		end
		self.ViewMgr:sendEv(ev)
	end
end