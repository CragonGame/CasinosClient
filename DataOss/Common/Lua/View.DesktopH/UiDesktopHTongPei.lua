-- Copyright(c) Cragon. All rights reserved.
-- 通赔动画，ViewDesktopH持有

---------------------------------------
UiDesktopHTongPei = {}

---------------------------------------
function UiDesktopHTongPei:new(o, com_ui, ui_desktoph)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.AutoHideTm = 3
    o.ComUi = com_ui
    o.ComUi.visible = false
    o.UiDesktopH = ui_desktoph
    o.GoTongPei = nil
    o.ActionShowEnd = nil
    o.DelayHideSelf = nil
    o.TweenTongPeiMoveY = nil
    o.CasinosContext = CS.Casinos.CasinosContext.Instance
    o.ComUi.onClick:Add(
            function()
                o:Reset()
            end)
    return o
end

---------------------------------------
function UiDesktopHTongPei:ShowEffect(show_end)
    local image_name = self.UiDesktopH.UiDesktopHBase:GetTongPeiImageName()
    if (self.GoTongPei == nil) then
        self.GoTongPei = self.ComUi:GetChild(image_name)
    end
    self.ActionShowEnd = show_end
    self.ComUi.visible = true
    self.GoTongPei:SetXY(self.ComUi.width / 2 - self.GoTongPei.width / 2, -self.GoTongPei.height)
    self.TweenTongPeiMoveY = self.GoTongPei:TweenMoveY(self.ComUi.height / 2 - self.GoTongPei.height / 2, 0.5)
    self.CasinosContext:Play("AllFailedEffect", CS.Casinos._eSoundLayer.LayerNormal)
    self.DelayHideSelf = self.CasinosContext.DelayMgr:Delay(self.AutoHideTm,
            function()
                self:Reset()
            end)
end

---------------------------------------
function UiDesktopHTongPei:Reset()
    self.ComUi.visible = false
    if (self.DelayHideSelf ~= nil) then
        self.DelayHideSelf:Kill(false)
        self.DelayHideSelf = nil
    end
    if (self.TweenTongPeiMoveY ~= nil) then
        self.TweenTongPeiMoveY:Kill(false)
        self.TweenTongPeiMoveY = nil
    end
    if (self.ActionShowEnd ~= nil) then
        self.ActionShowEnd()
        self.ActionShowEnd = nil
    end
end