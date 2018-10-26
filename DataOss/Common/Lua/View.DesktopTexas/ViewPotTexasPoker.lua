-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewPotTexasPoker = {}

---------------------------------------
function ViewPotTexasPoker:new(o, view_desktop)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ViewDesktop = view_desktop
    local com_ui = o.ViewDesktop.ComUi
    o.ControllerPot = com_ui:GetController("ControllerPot")
    o.PotTitle = com_ui:GetChild("Lan_Text_Pot:").asTextField
    o.TextPotChipAllValue = com_ui:GetChild("TextPotChipAllValue").asTextField
    o.TPots = {}

    local group1 = com_ui:GetChild("GroupPot1").asGroup
    local view_group1 = ViewPotGroup1:new(nil, com_ui, group1, o.ViewDesktop.ViewMgr)
    table.insert(o.TPots, view_group1)

    local group2 = com_ui:GetChild("GroupPot2").asGroup
    local view_group2 = ViewPotGroup2:new(nil, com_ui, group2, o.ViewDesktop.ViewMgr)
    table.insert(o.TPots, view_group2)

    local group3 = com_ui:GetChild("GroupPot3").asGroup
    local view_group3 = ViewPotGroup3:new(nil, com_ui, group3, o.ViewDesktop.ViewMgr)
    table.insert(o.TPots, view_group3)

    local group4 = com_ui:GetChild("GroupPot4").asGroup
    local view_group4 = ViewPotGroup4:new(nil, com_ui, group4, o.ViewDesktop.ViewMgr)
    table.insert(o.TPots, view_group4)

    local group5 = com_ui:GetChild("GroupPot5").asGroup
    local view_group5 = ViewPotGroup5:new(nil, com_ui, group5, o.ViewDesktop.ViewMgr)
    table.insert(o.TPots, view_group5)

    local group6 = com_ui:GetChild("GroupPot6").asGroup
    local view_group6 = ViewPotGroup6:new(nil, com_ui, group6, o.ViewDesktop.ViewMgr)
    table.insert(o.TPots, view_group6)

    local group7 = com_ui:GetChild("GroupPot7").asGroup
    local view_group7 = ViewPotGroup7:new(nil, com_ui, group7, o.ViewDesktop.ViewMgr)
    table.insert(o.TPots, view_group7)

    local group8 = com_ui:GetChild("GroupPot8").asGroup
    local view_group8 = ViewPotGroup8:new(nil, com_ui, group8, o.ViewDesktop.ViewMgr)
    table.insert(o.TPots, view_group8)

    local group9 = com_ui:GetChild("GroupPot9").asGroup
    local view_group9 = ViewPotGroup9:new(nil, com_ui, group9, o.ViewDesktop.ViewMgr)
    table.insert(o.TPots, view_group9)

    o.CurrentGroup = o.TPots[1]
    o.CurrentGroup:reset()

    return o
end

---------------------------------------
--function ViewPotTexasPoker:setTexasChipEnterPot(chip_enterpot)
--    self:showAllPotValue(chip_enterpot.pot_total)
--    local list_pot_count = #chip_enterpot.list_pot
--    self.ControllerPot.selectedIndex = list_pot_count - 1
--    self.CurrentGroup = self.TPots[list_pot_count]
--    self.CurrentGroup:setValue(chip_enterpot.list_pot)
--end

---------------------------------------
function ViewPotTexasPoker:setSnapShotData(pot_total, list_pot)
    self:showAllPotValue(pot_total)
    local pot_count = #list_pot
    if list_pot ~= nil and pot_count > 0 then
        self.ControllerPot.selectedIndex = pot_count - 1
        self.CurrentGroup = self.TPots[pot_count]
        self.CurrentGroup:setValue(list_pot)
        self.CurrentGroup:showValue()
    end
end

---------------------------------------
function ViewPotTexasPoker:showAllPotValue(pot_total)
    if pot_total > 0 then
        ViewHelper:SetGObjectVisible(true, self.PotTitle)
        ViewHelper:SetGObjectVisible(true, self.TextPotChipAllValue)
        self.TextPotChipAllValue.text = UiChipShowHelper:getGoldShowStr(pot_total, self.ViewDesktop.ViewMgr.LanMgr.LanBase, true, 1)
    end
end

---------------------------------------
function ViewPotTexasPoker:getPotPos(index)
    return self.CurrentGroup:getPotPos(index)
end

---------------------------------------
function ViewPotTexasPoker:showPotValue()
    local list_pot = self.ViewDesktop.Desktop.ListPot
    local list_pot_count = #list_pot
    local group = self.TPots[list_pot_count]
    if group ~= nil then
        self.ControllerPot.selectedIndex = list_pot_count - 1
        self.CurrentGroup = group
        self.CurrentGroup:setValue(list_pot)
        self.CurrentGroup:showValue()
    end
end

---------------------------------------
function ViewPotTexasPoker:resetPot()
    ViewHelper:SetGObjectVisible(false, self.PotTitle)
    ViewHelper:SetGObjectVisible(false, self.TextPotChipAllValue)
    self.TextPotChipAllValue.text = ""
    self.CurrentGroup:reset()
    self.CurrentGroup = self.TPots[1]
end

---------------------------------------
function ViewPotTexasPoker:resetViewPot(pot_index)
    self.CurrentGroup:resetViewPot(pot_index)
end

---------------------------------------
ViewPotGroup1 = {}

---------------------------------------
function ViewPotGroup1:new(o, com_ui, group, view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.TViewPot = {}
    o.GroupPot = group
    o.ComUi = com_ui
    o.ViewMgr = view_mgr
    local g1_viewpot1 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot1").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot1)
    return o
end

---------------------------------------
function ViewPotGroup1:setSingleValue(value)
    local view_pot = self.TViewPot[1]
    view_pot:setPotValue(value)
end

---------------------------------------
function ViewPotGroup1:setValue(list_pots)
    for i, v in pairs(list_pots) do
        local view_pot = self.TViewPot[i]
        if view_pot ~= nil then
            view_pot:setPotValue(v)
        end
    end
end

---------------------------------------
function ViewPotGroup1:getPotPos(index)
    local view_pot = self.TViewPot[index]
    if view_pot ~= nil then
        local pos = view_pot.GLoaderChipSign.xy
        local from = view_pot.GComPotChip:TransformPoint(pos, self.ComUi)
        return from
    else
        return nil
    end
end

---------------------------------------
function ViewPotGroup1:showValue()
    for i, v in pairs(self.TViewPot) do
        v:showValue()
    end
end

---------------------------------------
function ViewPotGroup1:reset()
    for i, v in pairs(self.TViewPot) do
        v:resetPot()
    end
end

---------------------------------------
function ViewPotGroup1:resetViewPot(pot_index)
    local view_pot = self.TViewPot[pot_index]
    if view_pot ~= nil then
        view_pot:resetPot()
    end
end

---------------------------------------
ViewPotGroup2 = {}

---------------------------------------
function ViewPotGroup2:new(o, com_ui, group, view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.TViewPot = {}
    o.GroupPot = group
    o.ComUi = com_ui
    o.ViewMgr = view_mgr
    local g1_viewpot1 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot1").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot1)
    local g1_viewpot2 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot2").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot2)
    return o
end

---------------------------------------
function ViewPotGroup2:setValue(list_pots)
    for i, v in pairs(list_pots) do
        local view_pot = self.TViewPot[i]
        if view_pot ~= nil then
            view_pot:setPotValue(v)
        end
    end
end

---------------------------------------
function ViewPotGroup2:getPotPos(index)
    local view_pot = self.TViewPot[index]
    if view_pot ~= nil then
        local pos = view_pot.GLoaderChipSign.xy
        local from = view_pot.GComPotChip:TransformPoint(pos, self.ComUi)
        return from
    else
        return nil
    end
end

---------------------------------------
function ViewPotGroup2:showValue()
    for i, v in pairs(self.TViewPot) do
        v:showValue()
    end
end

---------------------------------------
function ViewPotGroup2:reset()
    for i, v in pairs(self.TViewPot) do
        v:resetPot()
    end
end

---------------------------------------
function ViewPotGroup2:resetViewPot(pot_index)
    local view_pot = self.TViewPot[pot_index]
    if view_pot ~= nil then
        view_pot:resetPot()
    end
end

---------------------------------------
ViewPotGroup3 = {}

---------------------------------------
function ViewPotGroup3:new(o, com_ui, group, view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.TViewPot = {}
    o.GroupPot = group
    o.ComUi = com_ui
    o.ViewMgr = view_mgr
    local g1_viewpot1 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot1").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot1)
    local g1_viewpot2 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot2").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot2)
    local g1_viewpot3 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot3").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot3)

    return o
end

---------------------------------------
function ViewPotGroup3:setValue(list_pots)
    for i, v in pairs(list_pots) do
        local view_pot = self.TViewPot[i]
        if view_pot ~= nil then
            view_pot:setPotValue(v)
        end
    end
end

---------------------------------------
function ViewPotGroup3:getPotPos(index)
    local view_pot = self.TViewPot[index]
    if view_pot ~= nil then
        local pos = view_pot.GLoaderChipSign.xy
        local from = view_pot.GComPotChip:TransformPoint(pos, self.ComUi)
        return from
    else
        return nil
    end
end

---------------------------------------
function ViewPotGroup3:showValue()
    for i, v in pairs(self.TViewPot) do
        v:showValue()
    end
end

---------------------------------------
function ViewPotGroup3:reset()
    for i, v in pairs(self.TViewPot) do
        v:resetPot()
    end
end

---------------------------------------
function ViewPotGroup3:resetViewPot(pot_index)
    local view_pot = self.TViewPot[pot_index]
    if view_pot ~= nil then
        view_pot:resetPot()
    end
end

---------------------------------------
ViewPotGroup4 = {}

---------------------------------------
function ViewPotGroup4:new(o, com_ui, group, view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.TViewPot = {}
    o.GroupPot = group
    o.ComUi = com_ui
    o.ViewMgr = view_mgr
    local g1_viewpot1 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot1").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot1)
    local g1_viewpot2 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot2").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot2)
    local g1_viewpot3 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot3").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot3)
    local g1_viewpot4 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot4").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot4)

    return o
end

---------------------------------------
function ViewPotGroup4:setValue(list_pots)
    for i, v in pairs(list_pots) do
        local view_pot = self.TViewPot[i]
        if view_pot ~= nil then
            view_pot:setPotValue(v)
        end
    end
end

---------------------------------------
function ViewPotGroup4:getPotPos(index)
    local view_pot = self.TViewPot[index]
    if view_pot ~= nil then
        local pos = view_pot.GLoaderChipSign.xy
        local from = view_pot.GComPotChip:TransformPoint(pos, self.ComUi)
        return from
    else
        return nil
    end
end

---------------------------------------
function ViewPotGroup4:showValue()
    for i, v in pairs(self.TViewPot) do
        v:showValue()
    end
end

---------------------------------------
function ViewPotGroup4:reset()
    for i, v in pairs(self.TViewPot) do
        v:resetPot()
    end
end

---------------------------------------
function ViewPotGroup4:resetViewPot(pot_index)
    local view_pot = self.TViewPot[pot_index]
    if view_pot ~= nil then
        view_pot:resetPot()
    end
end

---------------------------------------
ViewPotGroup5 = {}

---------------------------------------
function ViewPotGroup5:new(o, com_ui, group, view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.TViewPot = {}
    o.GroupPot = group
    o.ComUi = com_ui
    o.ViewMgr = view_mgr
    local g1_viewpot1 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot1").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot1)
    local g1_viewpot2 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot2").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot2)
    local g1_viewpot3 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot3").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot3)
    local g1_viewpot4 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot4").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot4)
    local g1_viewpot5 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot5").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot5)

    return o
end

---------------------------------------
function ViewPotGroup5:setValue(list_pots)
    for i, v in pairs(list_pots) do
        local view_pot = self.TViewPot[i]
        if view_pot ~= nil then
            view_pot:setPotValue(v)
        end
    end
end

---------------------------------------
function ViewPotGroup5:getPotPos(index)
    local view_pot = self.TViewPot[index]
    if view_pot ~= nil then
        local pos = view_pot.GLoaderChipSign.xy
        local from = view_pot.GComPotChip:TransformPoint(pos, self.ComUi)
        return from
    else
        return nil
    end
end

---------------------------------------
function ViewPotGroup5:showValue()
    for i, v in pairs(self.TViewPot) do
        v:showValue()
    end
end

---------------------------------------
function ViewPotGroup5:reset()
    for i, v in pairs(self.TViewPot) do
        v:resetPot()
    end
end

---------------------------------------
function ViewPotGroup5:resetViewPot(pot_index)
    local view_pot = self.TViewPot[pot_index]
    if view_pot ~= nil then
        view_pot:resetPot()
    end
end

---------------------------------------
ViewPotGroup6 = {}

---------------------------------------
function ViewPotGroup6:new(o, com_ui, group, view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.TViewPot = {}
    o.GroupPot = group
    o.ComUi = com_ui
    o.ViewMgr = view_mgr
    local g1_viewpot1 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot1").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot1)
    local g1_viewpot2 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot2").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot2)
    local g1_viewpot3 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot3").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot3)
    local g1_viewpot4 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot4").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot4)
    local g1_viewpot5 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot5").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot5)
    local g1_viewpot6 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot6").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot6)

    return o
end

---------------------------------------
function ViewPotGroup6:setValue(list_pots)
    for i, v in pairs(list_pots) do
        local view_pot = self.TViewPot[i]
        if view_pot ~= nil then
            view_pot:setPotValue(v)
        end
    end
end

---------------------------------------
function ViewPotGroup6:getPotPos(index)
    local view_pot = self.TViewPot[index]
    if view_pot ~= nil then
        local pos = view_pot.GLoaderChipSign.xy
        local from = view_pot.GComPotChip:TransformPoint(pos, self.ComUi)
        return from
    else
        return nil
    end
end

---------------------------------------
function ViewPotGroup6:showValue()
    for i, v in pairs(self.TViewPot) do
        v:showValue()
    end
end

---------------------------------------
function ViewPotGroup6:reset()
    for i, v in pairs(self.TViewPot) do
        v:resetPot()
    end
end

---------------------------------------
function ViewPotGroup6:resetViewPot(pot_index)
    local view_pot = self.TViewPot[pot_index]
    if view_pot ~= nil then
        view_pot:resetPot()
    end
end

---------------------------------------
ViewPotGroup7 = {}

---------------------------------------
function ViewPotGroup7:new(o, com_ui, group, view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.TViewPot = {}
    o.GroupPot = group
    o.ComUi = com_ui
    o.ViewMgr = view_mgr
    local g1_viewpot1 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot1").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot1)
    local g1_viewpot2 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot2").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot2)
    local g1_viewpot3 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot3").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot3)
    local g1_viewpot4 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot4").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot4)
    local g1_viewpot5 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot5").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot5)
    local g1_viewpot6 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot6").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot6)
    local g1_viewpot7 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot7").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot7)

    return o
end

---------------------------------------
function ViewPotGroup7:setValue(list_pots)
    for i, v in pairs(list_pots) do
        local view_pot = self.TViewPot[i]
        if view_pot ~= nil then
            view_pot:setPotValue(v)
        end
    end
end

---------------------------------------
function ViewPotGroup7:getPotPos(index)
    local view_pot = self.TViewPot[index]
    if view_pot ~= nil then
        local pos = view_pot.GLoaderChipSign.xy
        local from = view_pot.GComPotChip:TransformPoint(pos, self.ComUi)
        return from
    else
        return nil
    end
end

---------------------------------------
function ViewPotGroup7:showValue()
    for i, v in pairs(self.TViewPot) do
        v:showValue()
    end
end

---------------------------------------
function ViewPotGroup7:reset()
    for i, v in pairs(self.TViewPot) do
        v:resetPot()
    end
end

---------------------------------------
function ViewPotGroup7:resetViewPot(pot_index)
    local view_pot = self.TViewPot[pot_index]
    if view_pot ~= nil then
        view_pot:resetPot()
    end
end

---------------------------------------
ViewPotGroup8 = {}

---------------------------------------
function ViewPotGroup8:new(o, com_ui, group, view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.TViewPot = {}
    o.GroupPot = group
    o.ComUi = com_ui
    o.ViewMgr = view_mgr
    local g1_viewpot1 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot1").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot1)
    local g1_viewpot2 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot2").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot2)
    local g1_viewpot3 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot3").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot3)
    local g1_viewpot4 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot4").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot4)
    local g1_viewpot5 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot5").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot5)
    local g1_viewpot6 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot6").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot6)
    local g1_viewpot7 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot7").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot7)
    local g1_viewpot8 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot8").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot8)

    return o
end

---------------------------------------
function ViewPotGroup8:setValue(list_pots)
    for i, v in pairs(list_pots) do
        local view_pot = self.TViewPot[i]
        if view_pot ~= nil then
            view_pot:setPotValue(v)
        end
    end
end

---------------------------------------
function ViewPotGroup8:getPotPos(index)
    local view_pot = self.TViewPot[index]
    if view_pot ~= nil then
        local pos = view_pot.GLoaderChipSign.xy
        local from = view_pot.GComPotChip:TransformPoint(pos, self.ComUi)
        return from
    else
        return nil
    end
end

---------------------------------------
function ViewPotGroup8:showValue()
    for i, v in pairs(self.TViewPot) do
        v:showValue()
    end
end

---------------------------------------
function ViewPotGroup8:reset()
    for i, v in pairs(self.TViewPot) do
        v:resetPot()
    end
end

---------------------------------------
function ViewPotGroup8:resetViewPot(pot_index)
    local view_pot = self.TViewPot[pot_index]
    if view_pot ~= nil then
        view_pot:resetPot()
    end
end

---------------------------------------
ViewPotGroup9 = {}

---------------------------------------
function ViewPotGroup9:new(o, com_ui, group, view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.TViewPot = {}
    o.GroupPot = group
    o.ComUi = com_ui
    o.ViewMgr = view_mgr
    local g1_viewpot1 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot1").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot1)
    local g1_viewpot2 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot2").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot2)
    local g1_viewpot3 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot3").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot3)
    local g1_viewpot4 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot4").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot4)
    local g1_viewpot5 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot5").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot5)
    local g1_viewpot6 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot6").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot6)
    local g1_viewpot7 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot7").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot7)
    local g1_viewpot8 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot8").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot8)
    local g1_viewpot9 = ViewPot:new(nil, com_ui:GetChildInGroup(group, "ComPot9").asCom, view_mgr)
    table.insert(o.TViewPot, g1_viewpot9)

    return o
end

---------------------------------------
function ViewPotGroup9:setValue(list_pots)
    for i, v in pairs(list_pots) do
        local view_pot = self.TViewPot[i]
        if view_pot ~= nil then
            view_pot:setPotValue(v)
        end
    end
end

---------------------------------------
function ViewPotGroup9:getPotPos(index)
    local view_pot = self.TViewPot[index]
    if view_pot ~= nil then
        local pos = view_pot.GLoaderChipSign.xy
        local from = view_pot.GComPotChip:TransformPoint(pos, self.ComUi)
        return from
    else
        return nil
    end
end

---------------------------------------
function ViewPotGroup9:showValue()
    for i, v in pairs(self.TViewPot) do
        v:showValue()
    end
end

---------------------------------------
function ViewPotGroup9:reset()
    for i, v in pairs(self.TViewPot) do
        v:resetPot()
    end
end

---------------------------------------
function ViewPotGroup9:resetViewPot(pot_index)
    local view_pot = self.TViewPot[pot_index]
    if view_pot ~= nil then
        view_pot:resetPot()
    end
end

---------------------------------------
ViewPot = {}

---------------------------------------
function ViewPot:new(o, com_chip, view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ViewMgr = view_mgr
    o.GComPotChip = com_chip
    o.GTextPotChipValue = com_chip:GetChild("TextPotChipValue").asTextField
    o.GLoaderChipSign = com_chip:GetChild("GLoaderChipSign").asLoader

    return o
end

---------------------------------------
function ViewPot:showValue()
    self.GComPotChip.visible = true
end

---------------------------------------
function ViewPot:setPotValue(value)
    if (value <= 0) then
        self.GComPotChip.visible = false
        return
    end

    self.TotalValue = value
    self.GComPotChip.visible = false
    self.GTextPotChipValue.text = UiChipShowHelper:getGoldShowStr(self.TotalValue, self.ViewMgr.LanMgr.LanBase, true, 1)
    self:_setPotLoader()
end

---------------------------------------
function ViewPot:resetPot()
    self.TotalValue = 0
    self.GTextPotChipValue.text = ""
    self.GComPotChip.visible = false
end

---------------------------------------
function ViewPot:_setPotLoader()
end