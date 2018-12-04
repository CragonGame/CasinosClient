-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ItemDesktopHRewardPotPlayerInfo = {
    ViewMgr = nil
}

---------------------------------------
function ItemDesktopHRewardPotPlayerInfo:new(o, com)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Com = com
    local co_headicon = o.Com:GetChild("CoHeadIcon").asCom
    o.ViewHeadIcon = ViewHeadIcon:new(nil, co_headicon)
    o.GTextNickName = o.Com:GetChild("NickName").asTextField
    o.GTextGold = o.Com:GetChild("Golds").asTextField
    if (o.ViewMgr == nil) then
        o.ViewMgr = ViewMgr
    end
    return o
end

---------------------------------------
function ItemDesktopHRewardPotPlayerInfo:setDesktopHRewardPotPlayerInfo(player_info)
    self.GTextNickName.text = CS.Casinos.UiHelper.addEllipsisToStr(player_info.nick_name, 33, 10)
    self.GTextGold.text = UiChipShowHelper:GetGoldShowStr(player_info.win_gold, self.ViewMgr.LanMgr.LanBase, false)
    self.ViewHeadIcon:SetPlayerInfo(player_info.icon, player_info.account_id, 0)
end