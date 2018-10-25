-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewMail = ViewBase:new()

---------------------------------------
function ViewMail:new(o)
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
function ViewMail:OnCreate()
    ViewHelper:PopUi(self.ComUi, self.ViewMgr.LanMgr:getLanValue("Mail"))
    self.ViewMgr:BindEvListener("EvEntityMailListInit", self)
    self.ViewMgr:BindEvListener("EvEntityMailAdd", self)
    self.ViewMgr:BindEvListener("EvEntityMailDelete", self)
    self.ViewMgr:BindEvListener("EvEntityMailUpdate", self)
    self.ControllerIM = self.ViewMgr.ControllerMgr:GetController("IM")
    local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
    local btn_close = com_bg:GetChild("BtnClose").asButton
    btn_close.onClick:Add(
            function()
                self:onClickBtnClose()
            end
    )
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:onClickBtnClose()
            end
    )
    self.ControllerIfHaveMail = self.ComUi:GetController("ControllerIfHaveMail")
    self.ControllerHaveNewMail = self.ComUi:GetController("ControllerHaveNewMail")
    local have_new_mail = self.ControllerIM:haveNewMail()
    self:setHaveNewMail(have_new_mail)
    self.GListMail = self.ComUi:GetChild("ListMail").asList
    local list_mail = self.ControllerIM:getMails()
    local have_newmail = self.ControllerIM:haveNewMail()
    self:setMail(#list_mail > 0, have_newmail)
end

---------------------------------------
function ViewMail:OnDestroy()
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ViewMail:OnHandleEv(ev)
    if (ev ~= nil) then
        if (ev.EventName == "EvEntityMailListInit") then
            self:setMail(#ev.list_mail > 0, ev.have_newmail)
        elseif (ev.EventName == "EvEntityMailAdd") then
            self:setMail(#ev.list_mail > 0, ev.have_newmail)
        elseif (ev.EventName == "EvEntityMailDelete") then
            self:setMail(#ev.list_mail > 0, ev.have_newmail)
        elseif (ev.EventName == "EvEntityMailUpdate") then
            self:setMail(#ev.list_mail > 0, ev.have_newmail)
        end
    end
end

---------------------------------------
function ViewMail:setMail(have_mail, have_new_mail)
    if (have_mail) then
        self.ControllerIfHaveMail.selectedIndex = 1
    else
        self.ControllerIfHaveMail.selectedIndex = 0
    end
    self:setMailList()
    self:setHaveNewMail(have_new_mail)
end

---------------------------------------
function ViewMail:onClickBtnClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewMail:setHaveNewMail(have_new_mail)
    if (have_new_mail) then
        self.ControllerHaveNewMail.selectedIndex = 0
    else
        self.ControllerHaveNewMail.selectedIndex = 1
    end
end

---------------------------------------
function ViewMail:setMailList()
    self.GListMail:RemoveChildrenToPool()
    local list_mail = self.ControllerIM:getMails()
    for i = 1, #list_mail do
        local com = self.GListMail:AddItemFromPool()
        local mail = list_mail[i]
        local item_mail = ItemMail:new(nil, com, self.ViewMgr, i)
        item_mail:setMail(mail)
    end
end

---------------------------------------
ViewMailFactory = ViewFactory:new()

---------------------------------------
function ViewMailFactory:new(o, ui_package_name, ui_component_name,
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
function ViewMailFactory:CreateView()
    local view = ViewMail:new(nil)
    return view
end