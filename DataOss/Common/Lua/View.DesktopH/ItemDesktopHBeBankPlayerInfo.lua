-- Copyright(c) Cragon. All rights reserved.
-- 申请上庄列表中的一个Item；被ViewDesktopHBankList持有

---------------------------------------
ItemDesktopHBeBankPlayerInfo = {}

---------------------------------------
function ItemDesktopHBeBankPlayerInfo:new(o, com, player_info, view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ViewMgr = view_mgr
    o.GCoPlayerInfo = com
    o.GTextNickName = o.GCoPlayerInfo:GetChild("NickName").asTextField
    o.GTextGolds = o.GCoPlayerInfo:GetChild("Golds").asTextField
    local co_headicon = o.GCoPlayerInfo:GetChild("CoHeadIcon")
    if (co_headicon ~= nil) then
        o.UiHeadIcon = ViewHeadIcon:new(nil, co_headicon.asCom)
    end
    o:refreshPlayerInfo(player_info)
    return o
end

---------------------------------------
function ItemDesktopHBeBankPlayerInfo:refreshPlayerInfo(player_info)
    self.GTextNickName.text = CS.Casinos.UiHelper.addEllipsisToStr(player_info.PlayerInfoCommon.NickName, 30, 9)
    self.GTextGolds.text = self.ViewMgr.LanMgr:getLanValue(player_info.Gold, self.ViewMgr.LanMgr.LanBase)
    if (self.UiHeadIcon ~= nil) then
        self.UiHeadIcon:setPlayerInfo(player_info.PlayerInfoCommon.IconName,
                player_info.PlayerInfoCommon.AccountId, player_info.PlayerInfoCommon.VIPLevel)
    end
end