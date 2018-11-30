-- Copyright(c) Cragon. All rights reserved.
-- 普通桌聊天文字气泡父节点。表情气泡在ViewDesktopPlayerInfo内部管理

---------------------------------------
ViewDesktopChatParent = ViewBase:new()

---------------------------------------
function ViewDesktopChatParent:new(o)
    o = o or {}
    setmetatable(o, self)
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

---------------------------------------
function ViewDesktopChatParent:OnCreate()
    self.ComUi.touchable = false
end

---------------------------------------
function ViewDesktopChatParent:addChat(co_chat_name, parent_relative, pos_relative)
    local co_chat = CS.FairyGUI.UIPackage.CreateObject("Common", co_chat_name).asCom
    local p = CS.Casinos.LuaHelper.GetVector2(pos_relative.x, pos_relative.y)
    local pos = parent_relative:TransformPoint(p, self.ComUi)
    local item_chat = ItemChatDeskTop:new(nil, co_chat)
    self.ComUi:AddChild(co_chat)
    co_chat.xy = pos
    return item_chat
end

---------------------------------------
function ViewDesktopChatParent:resetChatPos(chat, parent_relative, pos_relative)
    local p = CS.Casinos.LuaHelper.GetVector2(pos_relative.x, pos_relative.y)
    local pos = parent_relative:TransformPoint(p, self.ComUi)
    chat.GCoChat.xy = pos
end

---------------------------------------
function ViewDesktopChatParent:destroyChat(chat)
    self.ComUi:RemoveChild(chat.GCoChat)
    chat.GCoChat:Dispose()
    CS.UnityEngine.GameObject.Destroy(chat.GCoChat.displayObject.gameObject)
end

---------------------------------------
ViewDesktopChatParentFactory = class(ViewFactory)

---------------------------------------
function ViewDesktopChatParentFactory:CreateView()
    local view = ViewDesktopChatParent:new(nil)
    return view
end