-- Copyright(c) Cragon. All rights reserved.
-- 邮件详情

---------------------------------------
ViewMailDetail = ViewBase:new()

---------------------------------------
function ViewMailDetail:new()
    local o = {}
    setmetatable(o, { __index = self })
    o.ViewMgr = nil
    o.GoUi = nil
    o.ComUi = nil
    o.Panel = nil
    o.UILayer = nil
    o.InitDepth = nil
    o.ViewKey = nil
    o.Tween = nil
    return o
end

---------------------------------------
function ViewMailDetail:OnCreate()
    local co_close = self.ComUi:GetChild("ComBgAndClose").asCom
    local com_shade = co_close:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
    local co_detail = self.ComUi:GetChild("CoMailRealDetail").asCom
    self.GTextTm = co_detail:GetChild("MailTm").asTextField
    self.GTextContent = co_detail:GetChild("MailContent").asTextField
    self.GTextAttachmentTitle = co_detail:GetChild("Lan_Text_Accessory").asTextField
    self.GListAttachment = co_detail:GetChild("AttachMentList").asList
    self.GBtnConfirm = self.ComUi:GetChild("BtnConfirm").asButton
    self.GBtnConfirm.onClick:Add(
            function()
                self:onClickConfirm()
            end
    )
    local text_btn_title = self.GBtnConfirm:GetChild("BtnTitle")
    if (text_btn_title ~= nil) then
        self.GTextBtnTitle = text_btn_title.asTextField
        self.BtnTitleUseImage = false
    else
        self.BtnTitleUseImage = true
    end
    self.HaveAttachment = false
end

---------------------------------------
function ViewMailDetail:OnDestroy()
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end
end

---------------------------------------
function ViewMailDetail:setMail(mail)
    self.MailClient = mail
    self.Tween = ViewHelper:PopUi(self.ComUi, mail.Title)
    self.GTextContent.text = mail.Content
    if mail.CreateTime ~= nil then
        local d_tm = CS.System.DateTime.Parse(mail.CreateTime)
        self.GTextTm.text = CS.Casinos.UiHelper.getLocalTmToString(d_tm)
    end
    local btn_enabled = false
    if (mail.MailState == MailState.ReadAndRecvAttachments) then
        btn_enabled = false
    else
        btn_enabled = true
    end
    self.GBtnConfirm.enabled = btn_enabled
    if (mail.Attachment ~= nil) then
        if (self.BtnTitleUseImage) then
            local loader_btn_title = self.ComUi:GetChild("Lan_Image_GetAccessory").asLoader
            loader_btn_title.visible = true
        else
            self.GTextBtnTitle.text = self.ViewMgr.LanMgr:getLanValue("GetAttachments")
        end

        if (mail.Attachment.Gold > 0) then
            self.HaveAttachment = true
            local com = CS.FairyGUI.UIPackage.CreateObject("MailDetail", "CoMailAttachment").asCom
            self.GListAttachment:AddChild(com)
            ItemAttachment:new(nil, com, nil, mail.Attachment.Gold, 0, self.ViewMgr)
        end
        if (mail.Attachment.Diamond > 0) then
            self.HaveAttachment = true
            local com = CS.FairyGUI.UIPackage.CreateObject("MailDetail", "CoMailAttachment").asCom
            self.GListAttachment:AddChild(com)
            ItemAttachment:new(nil, com, nil, 0, mail.Attachment.Diamond, self.ViewMgr)
        end
        if (mail.Attachment.ListItem ~= nil) then
            self.HaveAttachment = true
            local count = #mail.Attachment.ListItem
            for i = 1, count do
                local item = mail.Attachment.ListItem[i]
                local com = CS.FairyGUI.UIPackage.CreateObject("MailDetail", "CoMailAttachment").asCom
                self.GListAttachment:AddChild(com)
                ItemAttachment:new(nil, com, item, 0, 0, self.ViewMgr)
            end
        end
    end
    if self.HaveAttachment == false then
        if (self.BtnTitleUseImage) then
            local loader_btn_title = self.ComUi:GetChild("Lan_Image_Ensure").asLoader
            loader_btn_title.visible = true
        else
            self.GTextBtnTitle.text = self.ViewMgr.LanMgr:getLanValue("Confirm")
        end
    end
    self.GTextAttachmentTitle.visible = self.HaveAttachment
    self.GListAttachment.visible = self.HaveAttachment
end

---------------------------------------
function ViewMailDetail:_onClickBtnClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewMailDetail:onClickConfirm()
    if (self.HaveAttachment) then
        local ev = self.ViewMgr.GetEv("EvUiRequestMailRecvAttachment")
        if (ev == nil) then
            ev = EvUiRequestMailRecvAttachment:new(nil)
        end
        ev.mail_guid = self.MailClient.MailGuid
        self.ViewMgr:SendEv(ev)
    end
    self:_onClickBtnClose()
end

---------------------------------------
ViewMailDetailFactory = ViewFactory:new()

---------------------------------------
function ViewMailDetailFactory:new(o, ui_package_name, ui_component_name, ui_layer, is_single, fit_screen)
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
function ViewMailDetailFactory:CreateView()
    local view = ViewMailDetail:new()
    return view
end