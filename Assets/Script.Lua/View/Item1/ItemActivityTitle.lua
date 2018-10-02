-- Copyright(c) Cragon. All rights reserved.
-- 活动对话框左侧的一条Item

ItemActivityTitle = {}

function ItemActivityTitle:new(o,com,item_activity,view_activitycenter)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	o.Com = com
	o.Com.onClick:Add(
		function()
			o:onClickSelf()
		end
	)
	o.ActivityCenter = view_activitycenter
	o.ItemActivity = item_activity
	o.GTextTitle = com:GetChild("TextTitle")
	o.GControllerIsSelected = com:GetController("ControllerSelected")
	o.GLoaderActType = com:GetChild("LoaderType").asLoader
	o.GTextTitle.text = o.ItemActivity.Title
	o.GLoaderActType.icon = CS.FairyGUI.UIPackage.GetItemURL("ActivityCenter","ActivityType"..o.ItemActivity.Type)
	o:BeSelectedOrNot(false)
	return o
end

function ItemActivityTitle:BeSelectedOrNot(selected)
	if(selected)
	then
		self.GControllerIsSelected.selectedIndex = 1
	else
		self.GControllerIsSelected.selectedIndex = 0
	end
end

function ItemActivityTitle:onClickSelf()
	self.ActivityCenter:SetCurrentSelectItem(self)
end
