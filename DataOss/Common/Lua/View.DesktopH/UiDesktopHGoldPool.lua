-- Copyright(c) Cragon. All rights reserved.
-- 管理DesktopHUiGold

---------------------------------------
UiDesktopHGoldPool = {}

---------------------------------------
function UiDesktopHGoldPool:new()
    local o = {}
    setmetatable(o, { __index = self })
    o.MaxGoldSortOrderOffset = 1
    o.QueUiGold = {}
    o.ListDelayEnqueGold = {}

    local init_gold_count = 450
    for i = 0, init_gold_count - 1 do
        local ui_desktoph_gold = UiDesktopHGold:new()
        ui_desktoph_gold:OnCreate()

        table.insert(o.QueUiGold, ui_desktoph_gold)
        local l = #o.QueUiGold
        if (l > o.MaxGoldSortOrderOffset) then
            o.MaxGoldSortOrderOffset = l
        end
        ui_desktoph_gold:SetGoldSortOrderOffset(o.MaxGoldSortOrderOffset)
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
    local ui_desktoph_gold = nil
    local l = #self.QueUiGold
    if (l == 0) then
        l = #self.ListDelayEnqueGold
        if (l > 0) then
            ui_desktoph_gold = table.remove(self.ListDelayEnqueGold, 1)
            ui_desktoph_gold:Reset()
        else
            ui_desktoph_gold = UiDesktopHGold:new()
            ui_desktoph_gold:OnCreate()
        end
        self.MaxGoldSortOrderOffset = self.MaxGoldSortOrderOffset + 1
        ui_desktoph_gold:SetGoldSortOrderOffset(self.MaxGoldSortOrderOffset)
    else
        ui_desktoph_gold = table.remove(self.QueUiGold, 1)
    end
    return ui_desktoph_gold
end

---------------------------------------
function UiDesktopHGoldPool:goldHNeedDelayEnPool(ui_desktoph_gold)
    table.insert(self.ListDelayEnqueGold, ui_desktoph_gold)
end

---------------------------------------
function UiDesktopHGoldPool:goldHEnPool(ui_desktoph_gold)
    self:_goldHEnPool(ui_desktoph_gold)
end

---------------------------------------
function UiDesktopHGoldPool:delayGoldHEnPool(ui_desktoph_gold)
    self:_goldHEnPool(ui_desktoph_gold)
    LuaHelper:TableRemoveV(self.ListDelayEnqueGold, ui_desktoph_gold)
end

---------------------------------------
function UiDesktopHGoldPool:_goldHEnPool(ui_desktoph_gold)
    if (self.QueUiGold == nil) then
        return
    end
    local sortorder_offset = ui_desktoph_gold.SortOrderOffset
    if (sortorder_offset > self.MaxGoldSortOrderOffset) then
        self.MaxGoldSortOrderOffset = sortorder_offset
    else
        self.MaxGoldSortOrderOffset = self.MaxGoldSortOrderOffset + 1
    end
    uigold_h:Reset()
    table.insert(self.QueUiGold, ui_desktoph_gold)
    uigold_h:SetGoldSortOrderOffset(self.MaxGoldSortOrderOffset)
end