-- 刚进游戏主动弹出的活动对话框。弹多个时，由ControllerActive控制依次弹出。

ViewActivityPopUpBox = ViewBase:new()

function ViewActivityPopUpBox:new(o)
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

function ViewActivityPopUpBox:onCreate()
	ViewHelper:PopUi(self.ComUi)
	self.CasinosContext = CS.Casinos.CasinosContext.Instance                                                                                                                                                                                                     
	self.GLoaderContent = self.ComUi:GetChild("LoaderContent").asLoader
	self.GTextContent = self.ComUi:GetChild("TextContent").asTextField
	local btn_close = self.ComUi:GetChild("BtnClose").asButton
	btn_close.onClick:Add(
		function()
			self:onClickBtnClose()
		end
	)
	local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
	local com_shade = com_bg:GetChild("ComShade").asCom
	com_shade.onClick:Add(
		function()
			self:onClickBtnClose()
		end
	)
end

function ViewActivityPopUpBox:onDestroy()
	local ev = self.ViewMgr:getEv("EvUiCloseActivityPopUpBox")
	if(ev == nil)
	then
		ev = EvUiCloseActivityPopUpBox:new(nil)
	end
	self.ViewMgr:sendEv(ev)
end

function ViewActivityPopUpBox:onClickBtnClose()
	self.ViewMgr:destroyView(self)
end

function ViewActivityPopUpBox:SetActivityInfo(item)
	if(item.ContenText ~= nil)
	then
		self.GTextContent.text = item.ContenText
	end
	if(item.ContentImage ~= nil)
	then
		local content_image = item.ContentImage
		local t = {}
		table.insert(t,CS.Casinos.CasinosContext.Instance.Config.ConfigUrl)
		table.insert(t,"/Activity/")
		table.insert(t,content_image)
		table.insert(t,".png")
		local t_str = table.concat(t)
		CS.Casinos.HeadIconMgr.Instant:asyncLoadIcon(content_image,t_str,content_image, nil,
				function(ex,tick)
					if (ex ~= nil and self.GLoaderContent ~= nil and self.GLoaderContent.displayObject ~= nil and self.GLoaderContent.displayObject.gameObject ~= nil)
					then
						local texture = CS.Casinos.LuaHelper.UnityObjectCastToTexture(ex,true)
						self.GLoaderContent.texture = CS.FairyGUI.NTexture(texture)
					end
				end
		)
	end
end


ViewActivityPopUpBoxFactory = ViewFactory:new()

function ViewActivityPopUpBoxFactory:new(o,ui_package_name,ui_component_name,
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

function ViewActivityPopUpBoxFactory:createView()	
	local view = ViewActivityPopUpBox:new(nil)	
	return view
end
