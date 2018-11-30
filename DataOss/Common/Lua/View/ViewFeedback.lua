-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewFeedback = class(ViewBase)

---------------------------------------
function ViewFeedback:ctor()
    self.Tween = nil
end

---------------------------------------
function ViewFeedback:OnCreate()
    self.Tween = ViewHelper:PopUi(self.ComUi, self.ViewMgr.LanMgr:getLanValue("Feedback"))
    self.ControllerIM = self.ViewMgr.ControllerMgr:GetController("IM")
    self.ControllerActor = self.ViewMgr.ControllerMgr:GetController("Actor")
    self.ControllerPlayer = self.ViewMgr.ControllerMgr:GetController("Player")
    self.ViewPool = self.ViewMgr:GetView("Pool")
    local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
    local btn_close = com_bg:GetChild("BtnClose").asButton
    btn_close.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
    self.GListChatContent = self.ComUi:GetChild("ListChatContent").asList
    self.GListChatContent:SetVirtual()
    self.GListChatContent.itemRenderer = function(a, b)
        self:RenderListItemChatContent(a, b)
    end
    local com_input = self.ComUi:GetChild("ComInput").asCom
    self.GTextInputSendText = com_input:GetChild("TextInput").asTextInput
    self.GBtnSend = com_input:GetChild("Lan_Btn_Send").asButton
    self.GBtnSend.onClick:Add(
            function()
                self:onClickBtnSendMsg()
            end
    )

    self.ChatRecordInitWidth = self.GListChatContent.width - 30
    self.ViewMgr:BindEvListener("EvEntityReceiveFeedbackChat", self)
    self.ViewMgr:BindEvListener("EvEntityReceiveFeedbackChats", self)
    self:showRecord()
end

---------------------------------------
function ViewFeedback:OnDestroy()
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ViewFeedback:OnHandleEv(ev)
    if (ev.EventName == "EvEntityReceiveFeedbackChat") then
        self:setCurrentChatMsg(ev.chat_msg)
    elseif (ev.EventName == "EvEntityReceiveFeedbackChats") then
        local list_chatrecv = ev.list_allchats
        if (list_chatrecv ~= nil) then
            self.GListChatContent.numItems = #list_chatrecv
        else
            self.GListChatContent.numItems = 0
        end
    end
end

---------------------------------------
function ViewFeedback:showRecord()
    local list_chat = self.ControllerIM.IMFeedback:getListChatShow()
    self.GListChatContent.numItems = #list_chat
    self.GListChatContent.scrollPane:ScrollBottom()
end

---------------------------------------
function ViewFeedback:setCurrentChatMsg(chat_record)
    local ev = self.ViewMgr:GetEv("EvUiFeedbackConfirmRead")
    if (ev == nil) then
        ev = EvUiFeedbackConfirmRead:new(nil)
    end
    self.ViewMgr:SendEv(ev)
    self:showRecord()
end

---------------------------------------
function ViewFeedback:onClickBtnSendMsg()
    if (self.GTextInputSendText.text == nil or self.GTextInputSendText.text == "") then
        ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("SendNoEmpty"))
        return
    end
    local c_m = ChatMsg:new(nil)
    c_m.recver_guid = ""
    c_m.recver_nickname = ""
    c_m.sender_guid = self.ControllerPlayer.Guid
    c_m.sender_nickname = self.ControllerActor.PropNickName:get()
    c_m.msg = self.GTextInputSendText.text
    local ev = self.ViewMgr:GetEv("EvUiSendFeedbackMsg")
    if (ev == nil) then
        ev = EvUiSendFeedbackMsg:new(nil)
    end
    ev.chat_msg = c_m:getData4Pack()
    self.ViewMgr:SendEv(ev)
    self.GTextInputSendText.text = ""
end

---------------------------------------
function ViewFeedback:_onClickBtnClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewFeedback:RenderListItemChatContent(index, obj)
    local list_chat = self.ControllerIM.IMFeedback:getListChatShow()
    if (list_chat ~= nil) then
        if (#list_chat > index) then
            local com = CS.Casinos.LuaHelper.GObjectCastToGCom(obj)
            local item = self.ViewPool:getItemChatEx(com)
            local record = list_chat[index + 1]
            local is_self = record.sender_guid == self.ControllerPlayer.Guid
            local sender_name_color = ""
            if (is_self) then
                sender_name_color = "0000FF"
            end
            if (is_self) then
                item:setChat(record.sender_nickname, record.msg, record.sender_viplevel,
                        record.dt, sender_name_color, "FFFFFF", false, self.ChatRecordInitWidth,
                        CS.Casinos._eChatItemType.NormalChat, true, true, false, record.is_tm)
            else
                item:setChat(record.sender_nickname, record.msg, record.sender_viplevel,
                        record.dt, sender_name_color, "FFFFFF", true, self.ChatRecordInitWidth,
                        CS.Casinos._eChatItemType.NormalChat, true, true, false, record.is_tm)
            end
        end
    end
end

---------------------------------------
ViewFeedbackFactory = class(ViewFactory)

---------------------------------------
function ViewFeedbackFactory:CreateView()
    local view = ViewFeedback:new()
    return view
end