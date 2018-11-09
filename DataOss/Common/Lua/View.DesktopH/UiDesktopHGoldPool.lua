-- Copyright(c) Cragon. All rights reserved.
-- 管理DesktopHUiGold

---------------------------------------
UiDesktopHGoldPool = {}

---------------------------------------
function UiDesktopHGoldPool:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.MaxGoldSortOrderOffset = 1
    o.QueUiGold = {}
    o.ListDelayEnqueGold = {}
    local InitGoldCount = 450
    for i = 0, InitGoldCount - 1 do
        local gold_h = UiDesktopHGold:new(nil)
        gold_h:OnCreate()
        table.insert(o.QueUiGold, gold_h)
        local l = #o.QueUiGold
        if (l > o.MaxGoldSortOrderOffset) then
            o.MaxGoldSortOrderOffset = l
        end
        gold_h:SetGoldSortOrderOffset(o.MaxGoldSortOrderOffset)
    end
    return o
end

---------------------------------------
function UiDesktopHGoldPool:Destroy()
    self.QueUiGold = {}
    self.ListDelayEnqueGold = {}
end

---------------------------------------
function UiDesktopHGoldPool:Reset()
    for k, v in pairs(self.ListDelayEnqueGold) do
        self:goldHEnPool(v)
    end
    self.ListDelayEnqueGold = {}
end

---------------------------------------
function UiDesktopHGoldPool:getMaxGoldSortOrder()
    return self.MaxGoldSortOrderOffset
end

---------------------------------------
function UiDesktopHGoldPool:getGoldH()
    local ui_gold = nil
    local l = #self.QueUiGold
    if (l == 0) then
        l = #self.ListDelayEnqueGold
        if (l > 0) then
            ui_gold = table.remove(self.ListDelayEnqueGold, 1)
            ui_gold:Reset()
        else
            ui_gold = UiDesktopHGold:new(nil)
            ui_gold:OnCreate()
        end
        self.MaxGoldSortOrderOffset = self.MaxGoldSortOrderOffset + 1
        ui_gold:SetGoldSortOrderOffset(self.MaxGoldSortOrderOffset)
    else
        ui_gold = table.remove(self.QueUiGold, 1)
    end
    return ui_gold
end

---------------------------------------
function UiDesktopHGoldPool:goldHNeedDelayEnPool(uigold_h)
    table.insert(self.ListDelayEnqueGold, uigold_h)
end

---------------------------------------
function UiDesktopHGoldPool:goldHEnPool(uigold_h)
    self:_goldHEnPool(uigold_h)
end

---------------------------------------
function UiDesktopHGoldPool:delayGoldHEnPool(uigold_h)
    self:_goldHEnPool(uigold_h)
    LuaHelper:TableRemoveV(self.ListDelayEnqueGold, uigold_h)
end

---------------------------------------
function UiDesktopHGoldPool:_goldHEnPool(uigold_h)
    if (self.QueUiGold == nil) then
        return
    end
    local sortorder_offset = uigold_h.SortOrderOffset
    if (sortorder_offset > self.MaxGoldSortOrderOffset) then
        self.MaxGoldSortOrderOffset = sortorder_offset
    else
        self.MaxGoldSortOrderOffset = self.MaxGoldSortOrderOffset + 1
    end
    uigold_h:Reset()
    table.insert(self.QueUiGold, uigold_h)
    uigold_h:SetGoldSortOrderOffset(self.MaxGoldSortOrderOffset)
end