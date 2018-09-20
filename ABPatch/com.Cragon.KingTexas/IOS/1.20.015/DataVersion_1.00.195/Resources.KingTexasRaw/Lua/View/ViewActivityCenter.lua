ViewActivityCenter = ViewBase:new()

function ViewActivityCenter:new(o)
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

function ViewActivityCenter:onCreate()
	ViewHelper:PopUi(self.ComUi,self.ViewMgr.LanMgr:getLanValue("Activity"))
	self.CasinosContext = CS.Casinos.CasinosContext.Instance
	self.ControllerActivity = self.ViewMgr.ControllerMgr:GetController("Activity")
	self.GListActivityTitle = self.ComUi:GetChild("ListActivityTitle").asList
	self.GLoaderCurrentActContent = self.ComUi:GetChild("LoaderActContent").asLoader
	self.GTextCurrentActContent = self.ComUi:GetChild("TextActContent").asTextField
	local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
	local btn_close = com_bg:GetChild("BtnClose").asButton
	btn_close.onClick:Add(
		function()
			self:onClickBtnClose()
		end
	)
	local com_shade = com_bg:GetChild("ComShade").asCom
	com_shade.onClick:Add(
		function()
			self:onClickBtnClose()
		end
	)
	self.BtnShare =  self.ComUi:GetChild("BtnShare").asCom
	self.BtnShare.onClick:Add(
			function()
				self:onClickShare()
			end)
	self.QRCode = self.ComUi:GetChild("ImageOrCode").asImage
	self:setActivityTitleList()
end

function ViewActivityCenter:SetCurrentSelectItem(item)
	if(self.CurrentSelectItem == item)
	then
		return
	else
		if(self.CurrentSelectItem ~= nil)
		then
			self.CurrentSelectItem:BeSelectedOrNot(false)
		end
		self.CurrentSelectItem = item
		local show_share = false
		local show_qrcode = true
		if self.CurrentSelectItem.ItemActivity.IsShare then
			show_share = true
			show_qrcode = false
		end

		self.CurrentSelectItem:BeSelectedOrNot(true)
		local content_text = self.CurrentSelectItem.ItemActivity.ContenText
		local content_image = self.CurrentSelectItem.ItemActivity.ContentImage
		local show_content = false
		if(content_text ~= nil)
		then
			show_content = true
			self.GTextCurrentActContent.text = content_text
		end
		ViewHelper:setGObjectVisible(show_content,self.GTextCurrentActContent)
		ViewHelper:setGObjectVisible(false,self.GLoaderCurrentActContent)
		ViewHelper:setGObjectVisible(false,self.BtnShare)
		if(content_image ~= nil)
		then
			local t = {}
			table.insert(t,CS.Casinos.CasinosContext.Instance.Config.ConfigUrl)
			table.insert(t,"/Activity/")
			table.insert(t,content_image)
			table.insert(t,".png")
			local t_str = table.concat(t)
			CS.Casinos.HeadIconMgr.Instant:asyncLoadIcon(content_image,t_str,content_image, nil,
					function(ex,tick)
						if (ex ~= nil and self.GLoaderCurrentActContent ~= nil and self.GLoaderCurrentActContent.displayObject ~= nil and self.GLoaderCurrentActContent.displayObject.gameObject ~= nil)
						then
							ViewHelper:setGObjectVisible(show_share,self.BtnShare)
							ViewHelper:setGObjectVisible(show_qrcode,self.QRCode)
							ViewHelper:setGObjectVisible(false,self.GTextCurrentActContent)
							ViewHelper:setGObjectVisible(true,self.GLoaderCurrentActContent)
							local texture = CS.Casinos.LuaHelper.UnityObjectCastToTexture(ex,true)
							self.GLoaderCurrentActContent.texture = CS.FairyGUI.NTexture(texture)
						end
					end
			)
		end
	end
end

function ViewActivityCenter:setActivityTitleList()
	local list_act = self.ControllerActivity.ListActivity
	if(#list_act == 0)
	then
		return
	else
		for i = 1, #list_act do
			local com = self.GListActivityTitle:AddItemFromPool().asCom
			local item = list_act[i]
			local item_activitytile = ItemActivityTitle:new(nil,com,item,self)
			if(i == 1)
			then
				self:SetCurrentSelectItem(item_activitytile)
			end
		end
	end
end

function ViewActivityCenter:onClickBtnClose()
	self.ViewMgr:destroyView(self)
end

function ViewActivityCenter:onClickShare()
	print("onClickShare")
	local ev = self.ViewMgr:getEv("EvClickShare")
	if(ev == nil)
	then
		ev = EvClickShare:new(nil)
	end
	self.ViewMgr:sendEv(ev)
end


ViewActivityCenterFactory = ViewFactory:new()

function ViewActivityCenterFactory:new(o,ui_package_name,ui_component_name,
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

function ViewActivityCenterFactory:createView()	
	local view = ViewActivityCenter:new(nil)	
	return view
end