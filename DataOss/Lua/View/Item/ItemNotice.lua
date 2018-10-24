-- Copyright(c) Cragon. All rights reserved.
-- 已废弃，未使用。合并到ItemChatEx

---------------------------------------
ItemNotice = {}

---------------------------------------
function ItemNotice:New(o, com)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Com = com
    local co_vip = o.Com:GetChild("CoVIP")
    if (co_vip ~= nil)
    then
        o.ViewVIPSign = ViewVIPSign:new(nil, self.Com)
    end
    o.GRichTextNickName = o.Com:GetChild("NickName").asRichTextField
    o.GRichTextContent = o.Com:GetChild("Context").asRichTextField
    o.NickNameInitX = o.GRichTextNickName.x
    o.ContextInitX = o.GRichTextContent.x
    return o
end

---------------------------------------
function ItemNotice:setText(title_color, title, context_color, context, vip_level)
    self.GRichTextNickName.color = title_color
    self.GRichTextContent.width = self.GRichTextContent.initWidth
    self.GRichTextContent.text = CS.ChatParser.inst:Parse(context)
    self.GRichTextContent.width = self.GRichTextContent.textWidth
    self.GRichTextContent.color = context_color
    CS.Casinos.CasinosContext.Instance:ClearSB()
    CS.Casinos.CasinosContext.Instance.SB:Append(CS.ChatParser.inst:Parse(title))
    if (self.ViewVIPSign ~= nil)
    then
        self.ViewVIPSign:setVIPLevel(vip_level)
    else
        if (vip_level > 0)
        then
            CS.Casinos.CasinosContext.Instance.SB:Append("(VIP)")
        end
    end
    CS.Casinos.CasinosContext.Instance.SB:Append(":")
    self.GRichTextNickName.text = CS.Casinos.CasinosContext.Instance.SB:ToString()
    CS.Casinos.CasinosContext.Instance:ClearSB()
    local nickname_x = self.NickNameInitX
    local content_x = self.ContextInitX
    if (vip_level == 0)
    then
        nickname_x = 0
        content_x = 0
    end
    self.GRichTextNickName.x = nickname_x
    self.GRichTextContent.x = content_x
    self.Com.height = self.GRichTextNickName.height + self.GRichTextContent.height
end