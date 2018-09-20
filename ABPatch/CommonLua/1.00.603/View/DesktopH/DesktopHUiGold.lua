DesktopHUiGold = {
    MOVE_CHIP_TM = 0.5,
    MOVE_SOUND = "jettonmoney",
    MAX_CHIP_MOVE_TM = 1,
    MAX_CHIP_MOVE_INTERVAL_TM = 0.05,
}

function DesktopHUiGold:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
    o.GCoGold = nil
    o.SortOrderOffset = 0
    o.MoveEndCallback = nil
    o.MoveStartCallback = nil
    o.Tweener = nil
    o.FTaskerReset = nil
    o.FTaskReset = nil
    o.MoveSound = nil
    o.AutoEndEnPool = false
    o.ParentSortOrder = 0
    o.ViewDesktopH = nil

    return o
end

function DesktopHUiGold:onCreate()
    local view_mgr = ViewMgr:new(nil)
    self.ViewDesktopH = view_mgr:getView("DesktopH")
    self.GCoGold = CS.FairyGUI.UIPackage.CreateObject(self.ViewDesktopH:getDesktopBasePackageName(), "Gold" .. self.ViewDesktopH.FactoryName)
    self.ViewDesktopH.GCoDesktopHPoolParent:AddChild(self.GCoGold)
    self.ParentSortOrder = self.ViewDesktopH.ComUi.sortingOrder
    self:_reset()
    self.FTaskReset = CS.Casinos.FTask(0)
    self.FTaskerReset = CS.Casinos.FTasker()
end

function DesktopHUiGold:setGoldSortOrderOffset(offset)
    self.SortOrderOffset = offset
    self.GCoGold.sortingOrder = self.ParentSortOrder + offset
end

function DesktopHUiGold:initMove(from, to, move_time,
                                 move_sound, move_end, move_start ,
                                 auto_end_enpool, delay_tm, fix_pos)
    self.MoveEndCallback = move_end
    self.MoveStartCallback = move_start
    local f_x = 0
    local f_y = 0
    if (fix_pos)
    then
        f_x = from.x - self.GCoGold.width / 2
        f_y = from.y - self.GCoGold.height / 2
    else
        f_x = from.x
        f_y = from.y
    end
    self.GCoGold:SetXY(f_x,f_y)
    local to1 = CS.Casinos.LuaHelper.GetVector2(to.x - self.GCoGold.width / 2,to.y - self.GCoGold.height / 2)
    self.Tweener = self.GCoGold:TweenMove(to1, move_time)
    self.Tweener = self.Tweener:SetDelay(delay_tm)
    self.Tweener =  self.Tweener:OnStart(
            function()
                self:_moveStart()
            end)
    self.Tweener =  self.Tweener:OnComplete(
            function()
                self:_moveEnd()
            end )
    self.GCoGold.visible = true
    self.MoveSound = move_sound
    self.AutoEndEnPool = auto_end_enpool
end

function DesktopHUiGold:setPostion(pos)
    local p_x = pos.x - self.GCoGold.width / 2
    local p_y = pos.y - self.GCoGold.height / 2
    self.GCoGold:SetXY(p_x,p_y)
end

function DesktopHUiGold:reset()
    self:_reset()
    self.MoveEndCallback = nil
    self.MoveStartCallback = nil
end

function DesktopHUiGold:needDelayEnPool(after_tm)
    self.ViewDesktopH.DesktopHGoldPool:goldHNeedDelayEnPool(self)
    self.FTaskReset:startAutoTask(after_tm)
    self.FTaskerReset:whenAll(nil,
            function(map_param)
                self:_reset1(map_param)
            end
    , self.FTaskReset)
    CS.Casinos.FTMgr.Instance:startTask(self.FTaskerReset)
end

function DesktopHUiGold:_moveStart()
    if (self.MoveStartCallback ~= nil)
    then
        self.MoveStartCallback()
    end

    if (CS.System.String.IsNullOrEmpty(self.MoveSound) == false)
    then
        CS.Casinos.CasinosContext.Instance:play(self.MoveSound, CS.Casinos._eSoundLayer.LayerReplace)
    end
end

function DesktopHUiGold:_moveEnd()
    if (self.MoveEndCallback ~= nil)
    then
        self.MoveEndCallback()
    end

    if (self.AutoEndEnPool)
    then
        self.ViewDesktopH.DesktopHGoldPool:goldHEnPool(self)
    end
end

function DesktopHUiGold:_reset1(map_param)
    if (self.GCoGold ~= nil and self.GCoGold.displayObject.gameObject ~= nil)
    then
        self.ViewDesktopH.DesktopHGoldPool:delayGoldHEnPool(self)
    end
end

function DesktopHUiGold:_reset()
    if (self.FTaskerReset ~= nil)
    then
        self.FTaskerReset:cancelTask()
    end
    if self.Tweener ~= nil then
        self.Tweener:Kill(true)
    end
    if (self.GCoGold ~= nil and self.GCoGold.displayObject.gameObject ~= nil)
    then
        self.GCoGold:SetXY(10000,10000)
    end
end