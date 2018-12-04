-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
IMMailBox = {}

---------------------------------------
function IMMailBox:new(o, co_im)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.ControllerIM = co_im
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.FullMailCount = 20
    o.HaveNewMail = false
    o.ListMail = {}
    return o
end

---------------------------------------
function IMMailBox:getMails()
    return self.ListMail
end

---------------------------------------
function IMMailBox:haveNewMail()
    return self.HaveNewMail
end

---------------------------------------
function IMMailBox:OnIMMailListInitNotify(list_mail)
    for i, v in pairs(list_mail) do
        local m_c = MailClient:new(nil)
        m_c:setData(v)
        table.insert(self.ListMail, m_c)
    end
    self:updateNewMailSign()
    local ev = self.ControllerIM.ViewMgr:GetEv("EvEntityMailListInit")
    if (ev == nil) then
        ev = EvEntityMailListInit:new(nil)
    end
    ev.list_mail = self.ListMail
    ev.have_newmail = self.HaveNewMail
    self.ControllerIM.ViewMgr:SendEv(ev)
end

---------------------------------------
function IMMailBox:OnIMMailAddNotify(mail)
    local m_c = MailClient:new(nil)
    m_c:setData(mail)
    table.insert(self.ListMail, 1, m_c)
    self.HaveNewMail = true
    local ev = self.ControllerIM.ViewMgr:GetEv("EvEntityMailAdd")
    if (ev == nil) then
        ev = EvEntityMailAdd:new(nil)
    end
    ev.list_mail = self.ListMail
    ev.have_newmail = self.HaveNewMail
    self.ControllerIM.ViewMgr:SendEv(ev)
end

---------------------------------------
function IMMailBox:OnIMMailDeleteNotify(mail_guid)
    local current_mail = nil
    local current_mail_key = nil
    for key, value in pairs(self.ListMail) do
        if (value.MailGuid == mail_guid) then
            current_mail = value
            current_mail_key = key
            break
        end
    end
    if (current_mail ~= nil) then
        if #self.ListMail > self.FullMailCount then
            ViewHelper:UiShowInfoSuccess(self.ControllerIM.ControllerMgr.LanMgr:getLanValue("MailBoxFullDeleteMailTips"))
        end
        table.remove(self.ListMail, current_mail_key)
    end
    self:updateNewMailSign()
    local ev = self.ControllerIM.ViewMgr:GetEv("EvEntityMailDelete")
    if (ev == nil) then
        ev = EvEntityMailDelete:new(nil)
    end
    ev.list_mail = self.ListMail
    ev.have_newmail = self.HaveNewMail
    self.ControllerIM.ViewMgr:SendEv(ev)
end

---------------------------------------
function IMMailBox:OnIMMailUpdateNotify(mail)
    local m_c = MailClient:new(nil)
    m_c:setData(mail)
    local current_mail = nil
    for key, value in pairs(self.ListMail) do
        if (value.MailGuid == m_c.MailGuid) then
            current_mail = value
        end
    end
    if (current_mail ~= nil) then
        current_mail.Attachment = m_c.Attachment
        current_mail.MailState = m_c.MailState
    end
    self:updateNewMailSign()
    local ev = self.ControllerIM.ViewMgr:GetEv("EvEntityMailUpdate")
    if (ev == nil) then
        ev = EvEntityMailUpdate:new(nil)
    end
    ev.list_mail = self.ListMail
    ev.have_newmail = self.HaveNewMail
    self.ControllerIM.ViewMgr:SendEv(ev)
end

---------------------------------------
function IMMailBox:OnIMMailOperateRequestResult(result)
    local m_r = MailOperateRequestResult:new(nil)
    m_r:setData(result)
    if (m_r.OperateType == MailOperateType.Read or m_r.OperateType == MailOperateType.Delete) then
        return
    end
    local operate_title = self:getOperateTitle(m_r.OperateType)
    self.CasinosContext:ClearSB()
    local tips = ""
    local sb = self.CasinosContext.SB
    if (m_r.Result == ProtocolResult.Success) then
        sb:Append(operate_title)
        sb:Append(self.ControllerIM.ControllerMgr.LanMgr:getLanValue("Success"))
        sb:Append("!")
        local current_mail = nil
        for key, value in pairs(self.ListMail) do
            if (value.MailGuid == m_r.MailGuid) then
                current_mail = value
            end
        end
        if (current_mail ~= nil and current_mail.Attachment ~= nil and current_mail.MailState ~= MailState.ReadAndRecvAttachments) then
            sb:Append(self.ControllerIM.ControllerMgr.LanMgr:getLanValue("Get"))
            if (current_mail.SenderType == MailSenderType.Player) then
                sb:Append(current_mail.SenderNickname)
            else
                sb:Append(self.ControllerIM.ControllerMgr.LanMgr:getLanValue("Sys"))
            end
            sb:Append(self.ControllerIM.ControllerMgr.LanMgr:getLanValue("Send"))
            if (current_mail.Attachment.Gold > 0) then
                sb:Append(self.ControllerIM.ControllerMgr.LanMgr:getLanValue("Chip"))
                tips = sb:ToString()
                local golds = UiChipShowHelper:GetGoldShowStr(current_mail.Attachment.Gold, self.ControllerIM.ControllerMgr.LanMgr.LanBase)
                sb.Length = 0
                sb:Append(tips)
                sb:Append(golds)
            end
            if (current_mail.Attachment.Diamond > 0) then
                sb:Append(self.ViewMgr.LanMgr:getLanValue("Coin"))
                tips = sb.ToString()
                local diamond = UiChipShowHelper:GetGoldShowStr(current_mail.Attachment.Diamond, self.ControllerIM.ControllerMgr.LanMgr.LanBase)
                sb.Length = 0
                sb:Append(tips)
                sb:Append(diamond)
            end
            if (current_mail.Attachment.ListItem ~= nil) then
                for i, v in pairs(current_mail.Attachment.ListItem) do
                    local temp = v
                    local tb_item = self.ControllerIM.ControllerMgr.TbDataMgr:GetData("Item", temp[2])
                    sb:Append(self.ControllerIM.ControllerMgr.LanMgr:getLanValue(tb_item.Name))
                    sb:Append("*")
                    sb:Append(temp[3])
                end
            end
            sb:Append(self.ControllerIM.ControllerMgr.LanMgr:getLanValue("Success"))
            sb:Append("!")
        end
    else
        if (m_r.Result == ProtocolResult.BagFull) then
            sb:Append(self.ControllerIM.ControllerMgr.LanMgr:getLanValue("BagFullTips"))
        end
        sb:Append(operate_title)
        sb:Append(self.ControllerIM.ControllerMgr.LanMgr:getLanValue("Failed"))
        sb:Append("!")
    end

    ViewHelper:UiShowInfoSuccess(sb:ToString())
end

---------------------------------------
function IMMailBox:RequestOperateMail(mail_guid, operate)
    self.ControllerIM.ControllerMgr.Rpc:RPC2(CommonMethodType.IMMailOperateRequest, mail_guid, operate)
end

---------------------------------------
function IMMailBox:getOperateTitle(operate_type)
    local title = ""
    if (operate_type == MailOperateType.None) then
    elseif (operate_type == MailOperateType.Read) then
        title = self.ControllerIM.ControllerMgr.LanMgr:getLanValue("ReadMail")
    elseif (operate_type == MailOperateType.RecvAttachment) then
        title = self.ControllerIM.ControllerMgr.LanMgr:getLanValue("TakeAttachment")
    elseif (operate_type == MailOperateType.Delete) then
        title = self.ControllerIM.ControllerMgr.LanMgr:getLanValue("DelMail")
    end
    return title
end

---------------------------------------
function IMMailBox:updateNewMailSign()
    for key, value in pairs(self.ListMail) do
        if (value.MailState == MailState.New) then
            self.HaveNewMail = true
            break
        else
            self.HaveNewMail = false
        end
    end
end