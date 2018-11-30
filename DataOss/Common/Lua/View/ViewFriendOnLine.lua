-- Copyright(c) Cragon. All rights reserved.
-- 好友上线左上角提示

---------------------------------------
ViewFriendOnLine = class(ViewBase)

---------------------------------------
function ViewFriendOnLine:ctor()
end

---------------------------------------
function ViewFriendOnLine:OnCreate()
    self.GTxetNickName = self.ComUi:GetChild("NickName").asTextField
    local co_headicon = self.ComUi:GetChild("CoHeadIcon").asCom
    self.HeadIcon = ViewHeadIcon:new(nil, co_headicon,
            function()
                self:_onClickHeadIcon()
            end
    )
    self.ViewMgr:BindEvListener("EvEntityFriendOnlineStateChange", self)
end

---------------------------------------
function ViewFriendOnLine:OnDestroy()
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ViewFriendOnLine:OnHandleEv(ev)
    if (ev.EventName == "EvEntityFriendOnlineStateChange") then
        if (ev.player_info.PlayerInfoMore.OnlineState == PlayerOnlineState.Online) then
            self:setFriendInfo(ev.player_info)
            self:InitMove()
        end
    end
end

---------------------------------------
function ViewFriendOnLine:setFriendInfo(player_info)
    self.GTxetNickName.text = CS.Casinos.UiHelper.addEllipsisToStr(player_info.PlayerInfoCommon.NickName, 21, 6)
    self.HeadIcon:SetPlayerInfo(player_info.PlayerInfoCommon.IconName, player_info.PlayerInfoCommon.AccountId, player_info.PlayerInfoCommon.VIPLevel)
end

---------------------------------------
function ViewFriendOnLine:InitMove()
    local trans = self.ComUi:GetTransition("MoveFromTopToTop")
    trans:Play(
            function()
                self:onPlayEnd()
            end
    )
end

---------------------------------------
function ViewFriendOnLine:onPlayEnd()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewFriendOnLine:_onClickHeadIcon()
    self.ViewMgr:CreateView("PlayerInfo")
end

---------------------------------------
ViewFriendOnLineFactory = class(ViewFactory)

---------------------------------------
function ViewFriendOnLineFactory:CreateView()
    local view = ViewFriendOnLine:new()
    return view
end