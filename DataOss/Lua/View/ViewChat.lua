-- Copyright(c) Cragon. All rights reserved.
-- 普通桌，百人桌的桌内群聊

---------------------------------------
ViewChat = ViewBase:new()

---------------------------------------
function ViewChat:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.PlayTanMuKey = "PlayTanMu"
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
function ViewChat:onCreate()
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.ControllerPlayer = self.ViewMgr.ControllerMgr:GetController("Player")
    self.ControllerActor = self.ViewMgr.ControllerMgr:GetController("Actor")
    self.ViewPool = self.ViewMgr:getView("Pool")
    self.MapChat = self.ControllerPlayer:getDesktopChat()
    local co_shade = self.ComUi:GetChild("CoShade").asCom
    co_shade.onClick:Add(
            function()
                self:closeSelf()
            end
    )
    self.ControllerScreenHalf = self.ComUi:GetController("ControllerScreenHalf")
    self.GGroupScreen = self.ComUi:GetChild("GroupScreen").asGroup
    self.GGroupScreenHalf = self.ComUi:GetChild("GroupScreenHalf").asGroup
    self.ViewMgr:bindEvListener("EvEntityRecvChatFromDesktopH", self)
    self.ViewMgr:bindEvListener("EvEntityRecvChatFromDesktop", self)
end

---------------------------------------
function ViewChat:onDestroy()
    if (self.GTextInputChat.text ~= nil and #self.GTextInputChat.text > 0) then
        local ev = self.ViewMgr:getEv("EvUiSetUnSendDesktopMsg")
        if (ev == nil) then
            ev = EvUiSetUnSendDesktopMsg:new(nil)
        end
        ev.text = self.GTextInputChat.text
        self.ViewMgr:sendEv(ev)
    end
    self.ViewMgr:unbindEvListener(self)
end

---------------------------------------
function ViewChat:onHandleEv(ev)
    if (ev.EventName == "EvEntityRecvChatFromDesktopH") then
        self.MapChat = self.ControllerPlayer:getDesktopChat()
        self.GListChat.numItems = LuaHelper:GetTableCount(self.MapChat)
        self.GListChat.scrollPane:ScrollTop()
    elseif (ev.EventName == "EvEntityRecvChatFromDesktop") then
        self.MapChat = self.ControllerPlayer:getDesktopChat()
        self.GListChat.numItems = LuaHelper:GetTableCount(self.MapChat)
        self.GListChat.scrollPane:ScrollTop()
    end
end

---------------------------------------
function ViewChat:setChatMsg(msg)
    self.GTextInputChat.text = msg
    self:onClickSendChat()
end

---------------------------------------
function ViewChat:init(chat_type)
    self.UiChatType = chat_type
    local group_parent = self.GGroupScreen
    if (chat_type == _eUiChatType.Desktop) then
        self.ControllerScreenHalf.selectedIndex = 1
        self.GListShortCutChat = self.ComUi:GetChild("ListShortCutChatAll").asList
    elseif (chat_type == _eUiChatType.DesktopH) then
        group_parent = self.GGroupScreenHalf
        self.ControllerScreenHalf.selectedIndex = 0
        local co_switch = self.ComUi:GetChild("CoSwitch").asCom
        co_switch.onClick:Add(
                function()
                    self:onClickSwtichTanMu()
                end
        )
        self.ControllerTanMuSwitch = co_switch:GetController("ControllerSwitch")
        local use_tanmu = true
        if (CS.UnityEngine.PlayerPrefs.HasKey(self.PlayTanMuKey)) then
            success, use_tanmu = CS.System.Boolean.TryParse(CS.UnityEngine.PlayerPrefs.GetString(self.PlayTanMuKey))
        end
        self:swtichController(self.ControllerTanMuSwitch, use_tanmu)
        self.GListShortCutChat = self.ComUi:GetChild("ListShortCutChatHalf").asList
    end
    self.GListChat = self.ComUi:GetChildInGroup(group_parent, "ListChat").asList
    self.GListChat:SetVirtual()
    self.GListChat.itemRenderer = function(a, b)
        self:RenderListItem(a, b)
    end
    self.GListChat.numItems = LuaHelper:GetTableCount(self.MapChat)
    self.GListChat.onClick:Add(
            function()
                self:closeSelf()
            end
    )
    local co_input = self.ComUi:GetChildInGroup(group_parent, "CoChatInput").asCom
    self.GTextInputChat = co_input:GetChild("TextInput").asTextInput
    local btn_send = self.ComUi:GetChildInGroup(group_parent, "Lan_Btn_Send").asButton
    btn_send.onClick:Add(
            function()
                self:onClickSendChat()
            end
    )
    self:setPresetMsg()
end

---------------------------------------
function ViewChat:RenderListItem(index, obj)
    local com = CS.Casinos.LuaHelper.GObjectCastToGCom(obj)
    local item = self.ViewPool:getItemChatEx(com)
    local chat_info
    if (self.MapChat[index] ~= nil) then
        chat_info = self.MapChat[index]
        if (chat_info.sender_etguid == "") then
            item:setChat(chat_info.sender_name, chat_info.chat_content, chat_info.sender_viplevel,
                    CS.System.DateTime.MinValue, "131D41", "FFFFFF", true, self.GListChat.width - 50, CS.Casinos._eChatItemType.NormalChat,
                    true, true, true, false, false)
        else
            local show_left = true
            if chat_info.sender_etguid == self.ControllerActor.Guid then
                show_left = false
            end
            item:setChat(chat_info.sender_name, chat_info.chat_content, chat_info.sender_viplevel,
                    CS.System.DateTime.MinValue, "131D41", "FFFFFF", show_left, self.GListChat.width - 50, CS.Casinos._eChatItemType.NormalChat, true, true)
        end
    end
end

---------------------------------------
function ViewChat:setPresetMsg()
    for key, value in pairs(self.CasinosContext.TbDataMgrLua:GetMapData("PresetMsg"))
    do
        local msg = value
        if (self.UiChatType == _eUiChatType.Desktop and msg.PresetMsgType == PresetMsgType.DesktopH) then
        elseif (self.UiChatType == _eUiChatType.DesktopH and msg.PresetMsgType == PresetMsgType.Desktop) then
        else
            local co_presetmsg = self.GListShortCutChat:AddItemFromPool().asCom
            local msg_content = self.ViewMgr.LanMgr:getLanValue(msg.PresetMsg)
            local preset_msg = ItemChatPresetMsg:new(nil, co_presetmsg, msg_content, self.ViewMgr)
        end
    end
end

---------------------------------------
function ViewChat:onClickSwtichTanMu()
    local use_tanmu = self.ControllerTanMuSwitch.selectedIndex == 0
    if (use_tanmu) then
        use_tanmu = false
    else
        use_tanmu = true
    end
    self:swtichController(self.ControllerTanMuSwitch, use_tanmu)
    if (use_tanmu) then
        CS.UnityEngine.PlayerPrefs.SetString(self.PlayTanMuKey, "true")
    else
        CS.UnityEngine.PlayerPrefs.SetString(self.PlayTanMuKey, "false")
    end
end

---------------------------------------
function ViewChat:swtichController(controller, use_tanmu)
    if (use_tanmu) then
        controller.selectedIndex = 0
    else
        controller.selectedIndex = 1
    end
end

---------------------------------------
function ViewChat:onClickSendChat()
    if (self.GTextInputChat.text == nil or self.GTextInputChat.text == "") then
        ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("SendNoEmpty"))
        return
    end
    local can_chat = false
    if (self.UiChatType == _eUiChatType.Desktop) then
        if (self.ControllerActor.PropVIPLevel:get() >= DesktopCanChatVIPLimit) then
            can_chat = true
        end
    elseif (self.UiChatType == _eUiChatType.DesktopH) then
        if (self.ControllerActor.PropVIPLevel:get() >= DesktopHCanChatVIPLimit) then
            can_chat = true
        else
            ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("OnlyVIPCanSend"))
        end
    end

    local ev = self.ViewMgr:getEv("EvUiSendMsg")
    if (ev == nil) then
        ev = EvUiSendMsg:new(nil)
    end
    local chat_msg = self:getChatMsg()
    ev.chat_msg = chat_msg:getData4Pack()
    self.ViewMgr:sendEv(ev)
    self.GTextInputChat.text = ""
    self:closeSelf()
end

---------------------------------------
function ViewChat:closeSelf()
    self.ViewMgr:destroyView(self)
end

---------------------------------------
function ViewChat:getChatMsg()
    local c_m = ChatMsg:new(nil)
    c_m.recver_guid = ""
    c_m.recver_nickname = ""
    c_m.sender_guid = self.ControllerPlayer.Guid
    c_m.sender_nickname = self.ControllerActor.PropNickName:get()
    c_m.msg = self.GTextInputChat.text
    return c_m
end

---------------------------------------
ViewChatFactory = ViewFactory:new()

---------------------------------------
function ViewChatFactory:new(o, ui_package_name, ui_component_name,
                             ui_layer, is_single, fit_screen)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.PackageName = ui_package_name
    self.ComponentName = ui_component_name
    self.UILayer = ui_layer
    self.IsSingle = is_single
    self.FitScreen = fit_screen
    return o
end

---------------------------------------
function ViewChatFactory:createView()
    local view = ViewChat:new(nil)
    return view
end