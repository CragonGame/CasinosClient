-- 通杀动画，ViewDesktopH持有

ItemUiDesktopHTongSha = {
    AutoDestroyTm = 3
}

function ItemUiDesktopHTongSha:new(o, com_ui)
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
    return o
end

function ItemUiDesktopHTongSha:showEffect(show_end)
    self.ActionShowEnd = show_end
    self.ComUi.visible = true
    self.AniTongSha:Play()
    CS.Casinos.CasinosContext.Instance:play("AllWinEffect", CS.Casinos._eSoundLayer.LayerNormal)
    self:_cancelTask()
    local t = CS.Casinos.FTMgr.Instance:startTask(ItemUiDesktopHTongSha.AutoDestroyTm)
    self.FTaskerHideSelf = CS.Casinos.FTMgr.Instance:whenAll(nil,
            function()
                self:_hideSelf(map_param)
            end
    , t)
end

function ItemUiDesktopHTongSha:destroy()
    self:_cancelTask()
end

function ItemUiDesktopHTongSha:reset()
    self:_cancelTask()
    self.ComUi.visible = false
end

function ItemUiDesktopHTongSha:_hideSelf(map_param)
    self.ComUi.visible = false
    if (self.ActionShowEnd ~= nil)
    then
        self.ActionShowEnd()
    end
end

function ItemUiDesktopHTongSha:_cancelTask()
    if (self.FTaskerHideSelf ~= nil)
    then
        self.FTaskerHideSelf:cancelTask()
        self.FTaskerHideSelf = nil
    end
end

function ItemUiDesktopHTongSha:_onClick()
    self.ComUi.visible = false
end