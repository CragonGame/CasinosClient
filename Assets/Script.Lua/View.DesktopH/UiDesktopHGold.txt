-- Copyright(c) Cragon. All rights reserved.
-- 一枚筹码，有多少筹码就有多少个该类的实例

---------------------------------------
UiDesktopHGold = {
    MOVE_CHIP_TM = 0.5,
    MOVE_SOUND = "jettonmoney",
    MAX_CHIP_MOVE_TM = 1,
    MAX_CHIP_MOVE_INTERVAL_TM = 0.05,
}

---------------------------------------
function UiDesktopHGold:new()
    local o = {}
    setmetatable(o, { __index = self })
    o.ViewMgr = nil
    o.ViewDesktopH = nil
    o.GCoGold = nil
    o.ParentSortOrder = 0
    o.SortOrderOffset = 0
    o.MoveEndCallback = nil
    o.MoveStartCallback = nil
    o.TweenMove = nil
    o.MoveSound = nil
    o.AutoEndEnPool = false
    return o
end

---------------------------------------
function UiDesktopHGold:OnCreate()
    self.ViewMgr = ViewMgr
    self.ViewDesktopH = self.ViewMgr:GetView("DesktopH")
    self.GCoGold = CS.FairyGUI.UIPackage.CreateObject(self.ViewDesktopH:getDesktopBasePackageName(), "Gold" .. self.ViewDesktopH.FactoryName)
    self.ViewDesktopH.GCoDesktopHPoolParent:AddChild(self.GCoGold)
    self.ParentSortOrder = self.ViewDesktopH.ComUi.sortingOrder
    self:_reset()
end

---------------------------------------
function UiDesktopHGold:SetGoldSortOrderOffset(offset)
    self.SortOrderOffset = offset
    self.GCoGold.sortingOrder = self.ParentSortOrder + offset
end

---------------------------------------
function UiDesktopHGold:InitMove(from, to, move_time, move_sound, move_end, move_start, auto_end_enpool, delay_tm, fix_pos)
    self.MoveEndCallback = move_end
    self.MoveStartCallback = move_start
    local f_x = 0
    local f_y = 0
    if (fix_pos) then
        f_x = from.x - self.GCoGold.width / 2
        f_y = from.y - self.GCoGold.height / 2
    else
        f_x = from.x
        f_y = from.y
    end
    self.GCoGold:SetXY(f_x, f_y)
    local to1 = CS.Casinos.LuaHelper.GetVector2(to.x - self.GCoGold.width / 2, to.y - self.GCoGold.height / 2)
    self.TweenMove = self.GCoGold:TweenMove(to1, move_time)
    self.TweenMove:SetDelay(delay_tm)
        :OnStart(
            function()
                if (self.MoveStartCallback ~= nil) then
                    self.MoveStartCallback()
                end
                if (CS.System.String.IsNullOrEmpty(self.MoveSound) == false) then
                    CS.Casinos.CasinosContext.Instance:Play(self.MoveSound, CS.Casinos._eSoundLayer.LayerReplace)
                end
            end)
        :OnComplete(
            function()
                if (self.MoveEndCallback ~= nil) then
                    self.MoveEndCallback()
                end
                if (self.AutoEndEnPool) then
                    self.ViewDesktopH.UiDesktopHGoldPool:goldHEnPool(self)
                end
            end)
    self.GCoGold.visible = true
    self.MoveSound = move_sound
    self.AutoEndEnPool = auto_end_enpool
end

---------------------------------------
function UiDesktopHGold:SetPostion(pos)
    local p_x = pos.x - self.GCoGold.width / 2
    local p_y = pos.y - self.GCoGold.height / 2
    self.GCoGold:SetXY(p_x, p_y)
end

---------------------------------------
function UiDesktopHGold:Reset()
    self:_reset()
    self.MoveEndCallback = nil
    self.MoveStartCallback = nil
end

---------------------------------------
function UiDesktopHGold:needDelayEnPool(after_tm)
    --self:_reset1(map_param)
    self.ViewDesktopH.UiDesktopHGoldPool:goldHNeedDelayEnPool(self)
end

---------------------------------------
function UiDesktopHGold:_reset1(map_param)
    if (self.GCoGold ~= nil and self.GCoGold.displayObject.gameObject ~= nil) then
        self.ViewDesktopH.UiDesktopHGoldPool:delayGoldHEnPool(self)
    end
end

---------------------------------------
function UiDesktopHGold:_reset()
    if self.TweenMove ~= nil then
        self.TweenMove:Kill()
        self.TweenMove = nil
    end
    if (self.GCoGold ~= nil and self.GCoGold.displayObject.gameObject ~= nil) then
        self.GCoGold:SetXY(10000, 10000)
    end
end