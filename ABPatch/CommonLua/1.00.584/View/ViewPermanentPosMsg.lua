ViewPermanentPosMsg = ViewBase:new()

function ViewPermanentPosMsg:new(o)
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

function ViewPermanentPosMsg:onCreate()	
	local text = self.ComUi:GetChild("MsgText")
	if(text ~= nil)
	then
		self.GTextMsg = text.asTextField
	end

	self.AutoDestroyTm = 3
	self.Tm = 0
	self.ComUi.touchable = false
end

function ViewPermanentPosMsg:onDestroy()

end

function ViewPermanentPosMsg:onUpdate(tm)
	self.Tm = self.Tm + tm
    if (self.Tm >= self.AutoDestroyTm)
	then
		self.ViewMgr:destroyView(self)
	end
end

function ViewPermanentPosMsg:onHandleEv(ev)	
end
        
function ViewPermanentPosMsg:showInfo(info)
    self.GTextMsg.text = info
end



ViewPermanentPosMsgFactory = ViewFactory:new()

function ViewPermanentPosMsgFactory:new(o,ui_package_name,ui_component_name,
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

function ViewPermanentPosMsgFactory:createView()	
	local view = ViewPermanentPosMsg:new(nil)	
	return view
end