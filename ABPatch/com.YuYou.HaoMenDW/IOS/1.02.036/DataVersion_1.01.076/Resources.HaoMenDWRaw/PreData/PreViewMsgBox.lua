PreViewMsgBox = PreViewBase:new()

function PreViewMsgBox:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
	self.ViewMgr = nil
	self.GoUi = nil
	self.ComUi = nil
	self.Panel = nil
	self.UILayer = nil
	self.InitDepth = nil
	self.ViewKey = nil
	if(self.Instance==nil)
	then
		self.Instance = o
	end
		
    return self.Instance
end

function PreViewMsgBox:onCreate()	 
	 self.ActionOk = nil
     self.ActionCancel = nil

	 local co_mommonbg = self.ComUi:GetChild("CommonMsgBox")
	 if(co_mommonbg ~= nil)
	 then
		local obj_cancel = co_mommonbg:GetChild("BtnClose")	 
		if(obj_cancel ~= nil)
		then
			local btn_cancel = obj_cancel.asButton
			btn_cancel.onClick:Add(self._onClickBtnCancel)	
		end    

		local obj_ok = co_mommonbg:GetChild("Confirm")	 
		if(obj_ok ~= nil)
		then
			local btn_ok = obj_ok.asButton
			btn_ok.onClick:Add(self._onClickBtnOK)
		end
	 end	
	  
	 local text = self.ComUi:GetChild("TextInfo")	 
	 if(text ~= nil)
	 then
		self.TextTips = text.asTextField		
	 end
end

function PreViewMsgBox:onDestroy()		
end

function PreViewMsgBox:onUpdate(tm)		
end

function PreViewMsgBox:onHandleEv(ev)	
end

function PreViewMsgBox.showMsgBox(info,ok,cancel)
	local view = PreViewMsgBox:new(nil)
	if(view.TextTips ~= nil)
	then
		view.TextTips.text = info
	end    

    view.ActionOk = ok
    view.ActionCancel = cancel
end

function PreViewMsgBox.showMsgBoxEx(info,ok)
	local view = PreViewMsgBox:new(nil)
    if(view.TextTips ~= nil)
	then
		view.TextTips.text = info
	end    

    view.ActionOk = ok
end

function PreViewMsgBox._onClickBtnOK()	
	local view = PreViewMsgBox:new(nil)
    if (view.ActionOk ~= nil)
    then
        view.ActionOk()
    end         

	view.PreViewMgr.destroyView(view)    
end

function PreViewMsgBox._onClickBtnCancel()	
	local view = PreViewMsgBox:new(nil)	
    if (view.ActionCancel ~= nil)
    then
        view.ActionCancel()
    end         

    view.PreViewMgr.destroyView(view)
end
        
PreViewMsgBoxFactory = PreViewFactory:new()

function PreViewMsgBoxFactory:new(o,ui_package_name,ui_component_name,
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

function PreViewMsgBoxFactory:createView()	
	local view = PreViewMsgBox:new(nil)	
	return view
end