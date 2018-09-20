ViewDesktopChatParent = ViewBase:new()

function ViewDesktopChatParent:new(o)
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

function ViewDesktopChatParent:onCreate()
	self.ComUi.touchable = false
end

function ViewDesktopChatParent:addChat(co_chat_name, parent_relative, pos_relative)
	local co_chat = CS.FairyGUI.UIPackage.CreateObject("Common", co_chat_name).asCom
	local p = CS.Casinos.LuaHelper.GetVector2(pos_relative.x,pos_relative.y)
    local pos = parent_relative:TransformPoint(p, self.ComUi)
    local item_chat = ItemChatDeskTop:new(nil,co_chat)
    self.ComUi:AddChild(co_chat)
	co_chat.xy = pos
    return item_chat
end

function ViewDesktopChatParent:resetChatPos(chat, parent_relative, pos_relative)
	local p = CS.Casinos.LuaHelper.GetVector2(pos_relative.x,pos_relative.y)
	local pos = parent_relative:TransformPoint(p, self.ComUi)
	chat.GCoChat.xy = pos
end

function ViewDesktopChatParent:destroyChat(chat)
	self.ComUi:RemoveChild(chat.GCoChat)
    chat.GCoChat:Dispose()
    CS.UnityEngine.GameObject.Destroy(chat.GCoChat.displayObject.gameObject)
end



ViewDesktopChatParentFactory = ViewFactory:new()

function ViewDesktopChatParentFactory:new(o,ui_package_name,ui_component_name,
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

function ViewDesktopChatParentFactory:createView()	
	local view = ViewDesktopChatParent:new(nil)	
	return view
end