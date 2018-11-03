-- Copyright(c) Cragon. All rights reserved.
-- 结算对话框中的5个Item中的一个

---------------------------------------
ItemDesktopHGameEndWinPlayer = {}

---------------------------------------
function ItemDesktopHGameEndWinPlayer:new(o, com, player_info, rank, view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.GCoPlayerInfo = com
    local co_headicon = o.GCoPlayerInfo:GetChild("CoHeadIcon").asCom
    o.UiHeadIcon = ViewHeadIcon:new(nil, co_headicon)
    o.UiHeadIcon:setPlayerInfo(player_info.icon, player_info.account_id, player_info.vip_level)
    o.GTextNickName = o.GCoPlayerInfo:GetChild("NickName").asTextField
    o.GTextGolds = o.GCoPlayerInfo:GetChild("WinGold").asTextField
    o.GTextRank = o.GCoPlayerInfo:GetChild("Rank").asTextField
    o.GTextNickName.text = CS.Casinos.UiHelper.addEllipsisToStr(player_info.nick_name, 15, 4)
    o.GTextGolds.text = UiChipShowHelper:getGoldShowStr(player_info.win_gold, view_mgr.LanMgr.LanBase, false)
    o.GTextRank.text = tostring(rank)
    return o
end