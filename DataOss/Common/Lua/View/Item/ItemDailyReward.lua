-- Copyright(c) Cragon. All rights reserved.
-- 每日首次登陆奖励的一格

---------------------------------------
ItemDailyReward = {}

---------------------------------------
function ItemDailyReward:new(o, co_dailyreward, tb_reward, current_day, reward_info, show_icon, view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.GCoDailyReward = co_dailyreward
    local day = tb_reward.Id
    show_icon = true
    if (day == 0) then
        day = 7
    end
    o.GObjAlreadyGet = o.GCoDailyReward:GetChild("ImageAlreadyGet")
    o.GObjAlreadyGet.visible = (day < current_day)
    local reward_title = o.GCoDailyReward:GetChild("RewardTitle").asTextField
    reward_title.text = reward_info.title
    local reward_value = o.GCoDailyReward:GetChild("RewardValue").asTextField
    reward_value.text = tostring(tb_reward.Reward .. "\nVIP+" .. tb_reward.VIPExtraReward)
    if (show_icon)
    then
        local loader = o.GCoDailyReward:GetChild("ChipsIcon").asLoader
        loader.icon = reward_info.icon_url
    end
    return o
end

---------------------------------------
function ItemDailyReward:setAlreadyGet()
    self.GObjAlreadyGet.visible = true
end