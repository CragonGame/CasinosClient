-- Copyright(c) Cragon. All rights reserved.
-- 一封邮件

ItemMail = {}

function ItemMail:new(o,com,view_mgr,index)
	o = o or {}
    setmetatable(o, self)
    self.__index = self
	o.ViewMgr = view_mgr
	o.Com = com
	o.Index = index
	o.Com.onClick:Clear()
	o.Com.onClick:Add(
		function()
			o:onClickOperate()
		end
	)
    o.GTextMailTitle = o.Com:GetChild("MailTitle").asTextField
    o.GTextMailContent = o.Com:GetChild("MailContent").asTextField
    o.GTextMailTm = o.Com:GetChild("MailTm").asTextField
    o.GImageNewSign = o.Com:GetChild("NewSign").asImage
    local controller_read_temp = o.Com:GetController("ControllerRead")
    if (controller_read_temp ~= nil)
	then
		o.ControllerRead = controller_read_temp
	end
	return o
end

function ItemMail:setMail(mail_client)
	self.MailClient = mail_client
    self.GTextMailTitle.text = CS.Casinos.UiHelper.addEllipsisToStr(mail_client.Title,48,15)
    self.GTextMailContent.text = CS.Casinos.UiHelper.addEllipsisToStr(mail_client.Content,84,27)
	if mail_client.CreateTime~= nil then
		local d_tm = CS.System.DateTime.Parse(mail_client.CreateTime)
		self.GTextMailTm.text = CS.Casinos.UiHelper.getLocalTmToString(d_tm)
	end
    if (mail_client.MailState == MailState.New)
	then
		self.GImageNewSign.visible = true
        if (self.ControllerRead ~= nil)
		then
			self.ControllerRead:SetSelectedIndex(1)
		end
	else
		self.GImageNewSign.visible = false
        if (self.ControllerRead ~= nil)
		then
			self.ControllerRead:SetSelectedIndex(0)
		end
	end
end

function ItemMail:onClickOperate()
	local ev = self.ViewMgr:getEv("EvUiRequestMailRead")
	if(ev == nil)
	then
		ev = EvUiRequestMailRead:new(nil)
	end
	ev.mail_guid = self.MailClient.MailGuid
	self.ViewMgr:sendEv(ev)
    local detail = self.ViewMgr:createView("MailDetail")
    detail:setMail(self.MailClient)
end