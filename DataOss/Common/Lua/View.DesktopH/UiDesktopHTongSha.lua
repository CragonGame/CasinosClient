-- Copyright(c) Cragon. All rights reserved.
-- 通杀动画，ViewDesktopH持有

---------------------------------------
UiDesktopHTongSha = {}

---------------------------------------
function UiDesktopHTongSha:new(o, com_ui)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.AutoHideTm = 3
    o.ComUi = com_ui
    o.ComUi.visible = false
    o.AniTongSha = o.ComUi:GetTransition("AniTongSha")
    o.ActionShowEnd = nil
    o.FTaskerHideSelf = nil
    o.TweenTongShaMoveY = nil
    o.CasinosContext = CS.Casinos.CasinosContext.Instance
    o.ComUi.onClick:Add(
            function()
                o:Reset()
            end
    )
    return o
end

---------------------------------------
function UiDesktopHTongSha:ShowEffect(show_end)
    self.ActionShowEnd = show_end
    self.ComUi.visible = true
    self.AniTongSha:Play()
    self.CasinosContext:Play("AllWinEffect", CS.Casinos._eSoundLayer.LayerNormal)
    --self:_cancelTask()
    local t = CS.Casinos.FTMgr.Instance:startTask(UiDesktopHTongSha.AutoHideTm)
    self.FTaskerHideSelf = CS.Casinos.FTMgr.Instance:whenAll(nil,
            function()
                self:Reset()
            end, t)
end

---------------------------------------
function UiDesktopHTongSha:Reset()
    self.ComUi.visible = false
    if (self.FTaskerHideSelf ~= nil) then
        self.FTaskerHideSelf:cancelTask()
        self.FTaskerHideSelf = nil
    end
    if (self.TweenTongShaMoveY ~= nil) then
        self.TweenTongShaMoveY:Kill(false)
        self.TweenTongShaMoveY = nil
    end
    if (self.ActionShowEnd ~= nil) then
        self.ActionShowEnd()
        self.ActionShowEnd = nil
    end
end