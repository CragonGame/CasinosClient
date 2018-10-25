-- Copyright(c) Cragon. All rights reserved.
-- 好友上线左上角提示

ViewFriendOnLine = ViewBase:new()

function ViewFriendOnLine:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self	
	o.ViewMgr = nil
	o.GoUi = nil
	o.ComUi = nil
	o.Panel = nil
	o.UILayer = nil
	o.InitDepth = nil
	o.ViewKey = nil

    return o
end

function ViewFriendOnLine:OnCreate()
	self.GTxetNickName = self.ComUi:GetChild("NickName").asTextField
    local co_headicon = self.ComUi:GetChild("CoHeadIcon").asCom
    self.HeadIcon = ViewHeadIcon:new(nil,co_headicon, 
										function()
											self:onClickHeadIcon()
										end
									 )
	self.ViewMgr:BindEvListener("EvEntityFriendOnlineStateChange",self)

end

function ViewFriendOnLine:OnDestroy()
	self.ViewMgr:UnbindEvListener(self)
end

function ViewFriendOnLine:OnHandleEv(ev)
	if(ev.EventName == "EvEntityFriendOnlineStateChange")
	then
		if (ev.player_info.PlayerInfoMore.OnlineState == PlayerOnlineState.Online)
		then
			self:setFriendInfo(ev.player_info)
            self:initMove()
		end
	end
end

function ViewFriendOnLine:setFriendInfo(player_info)
	self.GTxetNickName.text = CS.Casinos.UiHelper.addEllipsisToStr(player_info.PlayerInfoCommon.NickName,21,6)
    self.HeadIcon:setPlayerInfo(player_info.PlayerInfoCommon.IconName, player_info.PlayerInfoCommon.AccountId, player_info.PlayerInfoCommon.VIPLevel)
end

function ViewFriendOnLine:initMove()
	local trans = self.ComUi:GetTransition("MoveFromTopToTop")
    trans:Play(
		function()
			self:onPlayEnd()
		end
	)
end

function ViewFriendOnLine:onPlayEnd()
	self.ViewMgr:DestroyView(self)
end

function ViewFriendOnLine:onClickHeadIcon()
	self.ViewMgr:CreateView("PlayerInfo")
end
		

			

ViewFriendOnLineFactory = ViewFactory:new()

function ViewFriendOnLineFactory:new(o,ui_package_name,ui_component_name,
	ui_layer,is_single,fit_screen)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self
	self.PackageName = ui_package_name
	self.ComponentName = ui_component_name
	self.UILayer = ui_layer
	self.IsSingle = is_single
	self.FitScreen = fit_screen
    return o
end

function ViewFriendOnLineFactory:CreateView()
	local view = ViewFriendOnLine:new(nil)	
	return view
end