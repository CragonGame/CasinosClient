ViewDesktopHCardType = ViewBase:new()

function ViewDesktopHCardType:new(o)
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
	o.GCoCardType = nil

    return o
end

function ViewDesktopHCardType:onCreate()
	 self.ComUi.onClick:Add(
		function()
			self:_onClickCoCardType()
		end
	 )
end

function ViewDesktopHCardType:showCardType(co_cardtype)        
   self.GCoCardType = co_cardtype
   self.ComUi:AddChild(self.GCoCardType)
   local pos = CS.UnityEngine.Vector3()
   pos.x = -self.GCoCardType.width
   pos.y = self.ComUi.height / 2 - self.GCoCardType.height / 2
   pos.z = 1
   self.GCoCardType.position = pos
   self.GCoCardType:TweenMoveX(0, 0.5)
end

function ViewDesktopHCardType:_onClickCoCardType()  
	self.GCoCardType:TweenMoveX(-self.GCoCardType.width, 0.5):OnComplete(
		function()
			self.ViewMgr:destroyView(self)
		end
	)   
end



ViewDesktopHCardTypeFactory = ViewFactory:new()

function ViewDesktopHCardTypeFactory:new(o,ui_package_name,ui_component_name,
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

function ViewDesktopHCardTypeFactory:createView()	
	local view = ViewDesktopHCardType:new(nil)	
	return view
end