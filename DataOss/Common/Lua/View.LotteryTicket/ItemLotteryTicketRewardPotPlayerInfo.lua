-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ItemLotteryTicketRewardPotPlayerInfo = {}

---------------------------------------
function ItemLotteryTicketRewardPotPlayerInfo:new(o, view_lottery, com, player_info, tm)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.Context = Context
    o.ViewLotteryTicket = view_lottery
    o.GCoPlayerInfo = com
    o.GTextNickName = o.GCoPlayerInfo:GetChild("NickName").asTextField
    o.GTextGolds = o.GCoPlayerInfo:GetChild("Golds").asTextField
    o.GTextTm = o.GCoPlayerInfo:GetChild("Time").asTextField
    o:refreshPlayerInfo(player_info)
    o.GTextTm.text = tm
    return o
end

---------------------------------------
function ItemLotteryTicketRewardPotPlayerInfo:refreshPlayerInfo(player_info)
    self.GTextNickName.text = CS.Casinos.UiHelper.addEllipsisToStr(player_info.Nickname, 27, 8)
    self.GTextGolds.text = UiChipShowHelper:getGoldShowStr(player_info.WinGold, self.ViewLotteryTicket.ViewMgr.LanMgr.LanBase)
end