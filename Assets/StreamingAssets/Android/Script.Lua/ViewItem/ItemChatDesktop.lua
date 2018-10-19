-- Copyright(c) Cragon. All rights reserved.
-- 普通桌，百人桌的聊天文字气泡。根据锚点和头像在屏幕中的位置，决定放左边还是右边

---------------------------------------
ItemChatDeskTop = { ShowTm = 0, ShowChatTm = 2 }

---------------------------------------
function ItemChatDeskTop:new(o, co_chat)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.GCoChat = co_chat
    o.GGroupNormalLength = co_chat:GetChild("GroupNormalLength").asGroup
    o.GRichTextMsgNormalLength = co_chat:GetChildInGroup(o.GGroupNormalLength, "ChatMsg").asRichTextField
    o.GGroupMaxLength = co_chat:GetChild("GroupMaxLength").asGroup;
    o.GRichTextMsgMaxLength = co_chat:GetChildInGroup(o.GGroupMaxLength, "ChatMsg").asRichTextField
    return o
end

---------------------------------------
function ItemChatDeskTop:update(tm)
    if (self.ShowTm > 0) then
        self.ShowTm = self.ShowTm - tm
        if (self.ShowTm <= 0) then
            self:reset()
        end
    end
end

---------------------------------------
function ItemChatDeskTop:setChatTextAndSortingOrder(chat_info, sorting_order)
    self.GCoChat.visible = true
    self.GCoChat.sortingOrder = sorting_order
    self.ShowTm = self.ShowChatTm
    self.GGroupNormalLength.visible = true
    self.GRichTextMsgNormalLength.text = CS.ChatParser.inst:Parse(chat_info.chat_content)
    if (self.GRichTextMsgNormalLength.width > 478) then
        self.GGroupNormalLength.visible = false
        self.GGroupMaxLength.visible = true
        self.GRichTextMsgMaxLength.text = CS.ChatParser.inst:Parse(chat_info.chat_content)
        if (self.GRichTextMsgMaxLength.richTextField.textField.lines.Count > 3) then
            local text = ""
            local sb = CS.Casinos.CasinosContext.Instance.SB
            local line_4 = self.GRichTextMsgMaxLength.richTextField.textField.lines[3]
            local new_text = self.GRichTextMsgMaxLength.text:Substring(0, line_4.charIndex - 1)
            CS.Casinos.CasinosContext.Instance:ClearSB()
            sb:Append(new_text)
            sb:Append("...")
            text = sb:ToString()
            self.GRichTextMsgMaxLength.text = text
            CS.Casinos.CasinosContext.Instance:ClearSB()
        end
    end
end

---------------------------------------
function ItemChatDeskTop:reset()
    self.ShowTm = 0
    self.GCoChat.visible = false
    self.GRichTextMsgNormalLength.text = nil
    self.GRichTextMsgMaxLength.text = nil
    self.GGroupNormalLength.visible = false
    self.GGroupMaxLength.visible = false
end
