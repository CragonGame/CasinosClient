-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewAbout = class(ViewBase)

---------------------------------------
function ViewAbout:ctor()
    --o = o or {}
    --setmetatable(o, self)
    --self.__index = self
    --o.ViewMgr = nil
    --o.GoUi = nil
    --o.ComUi = nil
    --o.Panel = nil
    --o.UILayer = nil
    --o.InitDepth = nil
    --o.ViewKey = nil
    self.Tween = nil
    --return o
end

---------------------------------------
function ViewAbout:OnCreate()
    self.Tween = ViewHelper:PopUi(self.ComUi, self.ViewMgr.LanMgr:getLanValue("About"))
    self.ControllerAbout = self.ComUi:GetController("ControllerAbout")
    local com_linkAbout = self.ComUi:GetChild("ComAboutLink")
    com_linkAbout.onClick:Add(
            function()
                self:onClickAbout()
            end
    )
    local com_linkPrivacy = self.ComUi:GetChild("ComPrivacyLink")
    com_linkPrivacy.onClick:Add(
            function()
                self:onClickPrivacy()
            end
    )
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
end

---------------------------------------
function ViewAbout:OnDestroy()
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end
end

---------------------------------------
function ViewAbout:_onClickBtnClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewAbout:onClickAbout()
    self.ControllerAbout:SetSelectedIndex(0)
end

---------------------------------------
function ViewAbout:onClickPrivacy()
    self.ControllerAbout:SetSelectedIndex(1)
end

---------------------------------------
ViewAboutFactory = class(ViewFactory)

---------------------------------------
function ViewAboutFactory:CreateView()
    local view = ViewAbout:new()
    return view
end