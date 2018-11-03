-- Copyright(c) Cragon. All rights reserved.
-- 通杀动画，ViewDesktopH持有

---------------------------------------
UiDesktopHTongSha = {
    AutoDestroyTm = 3
}

---------------------------------------
function UiDesktopHTongSha:new(o, com_ui)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ComUi = com_ui
    o.ComUi.visible = false
    o.AniTongSha = o.ComUi:GetTransition("AniTongSha")
    o.ComUi.onClick:Add(
            function()
                o:_onClick()
            end
    )
    o.ActionShowEnd = nil
    o.FTaskerHideSelf = nil
    o.CasinosContext = CS.Casinos.CasinosContext.Instance
    return o
end

---------------------------------------
function UiDesktopHTongSha:ShowEffect(show_end)
    self.ActionShowEnd = show_end
    self.ComUi.visible = true
    self.AniTongSha:Play()
    self.CasinosContext:Play("AllWinEffect", CS.Casinos._eSoundLayer.LayerNormal)
    self:_cancelTask()
    local t = CS.Casinos.FTMgr.Instance:startTask(UiDesktopHTongSha.AutoDestroyTm)
    self.FTaskerHideSelf = CS.Casinos.FTMgr.Instance:whenAll(nil,
            function()
                self:_hideSelf(map_param)
            end, t)
end

---------------------------------------
function UiDesktopHTongSha:Destroy()
    self:_cancelTask()
end

---------------------------------------
function UiDesktopHTongSha:Reset()
    self:_cancelTask()
    self.ComUi.visible = false
end

---------------------------------------
function UiDesktopHTongSha:_hideSelf(map_param)
    self.ComUi.visible = false
    if (self.ActionShowEnd ~= nil) then
        self.ActionShowEnd()
    end
end

---------------------------------------
function UiDesktopHTongSha:_cancelTask()
    if (self.FTaskerHideSelf ~= nil) then
        self.FTaskerHideSelf:cancelTask()
        self.FTaskerHideSelf = nil
    end
end

---------------------------------------
function UiDesktopHTongSha:_onClick()
    self.ComUi.visible = false
end