ItemMatchType = {}

function ItemMatchType:new(o,com,match_type,match_title,view_matchlobby)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	o.MatchType = match_type
	o.ViewMatchLobby = view_matchlobby
	o.Com = com
	o.GControllerIsSelected = o.Com:GetController("ControllerIsSelected")
	o.GLoaderMatchType = o.Com:GetChild("LoaderMatchType").asLoader
	o.GTextMatchTitle = o.Com:GetChild("TextMatchTitle").asTextField
	o.Com.onClick:Add(
		function()
			o:onClickSelf()
		end
	)
	o.GLoaderMatchType.url = CS.FairyGUI.UIPackage.GetItemURL("MatchLobby","MatchType"..match_type)
	o.GTextMatchTitle.text = match_title
	o:BeSelectedOrNot(false)
	return o
end

function ItemMatchType:BeSelectedOrNot(selected)
	if(selected)
	then
		self.GControllerIsSelected.selectedIndex = 1
		local color_temp = CS.UnityEngine.Color.white
		self.GLoaderMatchType.color = color_temp
		self.GTextMatchTitle.color = color_temp
	else
		self.GControllerIsSelected.selectedIndex = 0
		local color_temp = CS.UnityEngine.Color(0.6,0.66,0.8)
		self.GLoaderMatchType.color = color_temp
		self.GTextMatchTitle.color = color_temp
	end
end

function ItemMatchType:onClickSelf()
	self.ViewMatchLobby:setCurrentMatchType(self)
end
