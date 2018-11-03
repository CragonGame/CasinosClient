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
    o.FTaskerHideSelf = nil
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

    local t = CS.Casinos.FTMgr.Instance:startTask(UiDesktopHTongPei.AutoHideTm)
    self.FTaskerHideSelf = CS.Casinos.FTMgr.Instance:whenAll(nil,
            function(map_param)
                --self:_hideSelf(map_param)
                self:Reset()
            end, t)
end

---------------------------------------
function UiDesktopHTongPei:Reset()
    self.ComUi.visible = false
    if (self.FTaskerHideSelf ~= nil) then
        self.FTaskerHideSelf:cancelTask()
        self.FTaskerHideSelf = nil
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

---------------------------------------
--function UiDesktopHTongPei:_hideSelf(map_param)
--    self.ComUi.visible = false
--    if (self.ActionShowEnd ~= nil) then
--        self.ActionShowEnd()
--        self.ActionShowEnd = nil
--    end
--end

---------------------------------------
--function UiDesktopHTongPei:_onClick()
--    self.ComUi.visible = false
--end