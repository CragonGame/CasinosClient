UiChipMgrEx = {
    MoveTm = 0.2
}

function UiChipMgrEx:new(o, move_chip_onebyone)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.MoveChipOneByOne = move_chip_onebyone
    o.ListChipDisabledPool = {}
    o.ListChipEnabled = {}
    o.CurrentEnabledChip = nil
    o.ListWinChips = {}
    o.Tm = 0
    for i = 1, 9 do
        local win_golds = WinGolds:new(nil, o)
        table.insert(o.ListWinChips,win_golds)
    end

    return o
end

function UiChipMgrEx:update(tm)
    if (self.MoveChipOneByOne and self.CurrentEnabledChip ~= nil)
    then
        self.Tm = self.Tm + tm
        if (self.Tm >= UiChipMgrEx.MoveTm)
        then
            self:_currentChipMoveEnd()
        end
    end
end

function UiChipMgrEx:destroy()
    self.ListChipDisabledPool = {}
    self.ListChipDisabledPool = nil
    self.ListChipEnabled = {}
    self.CurrentEnabledChip = nil
end

function UiChipMgrEx:resetChips()
    if (self.ListChipEnabled == nil)
    then
        return
    end

    if (self.CurrentEnabledChip ~= nil)
    then
        self.CurrentEnabledChip:reset()
        local have = LuaHelper:TableContainsV(self.ListChipDisabledPool, self.CurrentEnabledChip)
        if (have == false)
        then
            table.insert(self.ListChipDisabledPool, self.CurrentEnabledChip)
        end
        self.CurrentEnabledChip = nil
    end
    for k, v in pairs(self.ListChipEnabled) do
        v:reset()
        local have = LuaHelper:TableContainsV(self.ListChipDisabledPool, self.CurrentEnabledChip)
        if (have == false)
        then
            table.insert(self.ListChipDisabledPool, self.CurrentEnabledChip)
        end
    end

    self.ListChipEnabled = {}
    for i, v in pairs(self.ListWinChips) do
        v:reset()
    end
end

function UiChipMgrEx:chipEnquee(chip)
    if chip.ChipMoveType == CS.Casinos.ChipMoveType.RunOutOfMainPot then
        chip:moveEnd()
        chip:reset()
    else
        if self.MoveChipOneByOne then
            if (chip == self.CurrentEnabledChip)
            then
                self:_currentChipMoveEnd()
            else
                LuaHelper:TableRemoveV(self.ListChipEnabled, chip)
                chip:reset()
                table.insert(self.ListChipDisabledPool, chip)
            end
        else
            chip:moveEnd()
            chip:reset()
            local have = LuaHelper:TableContainsV(self.ListChipDisabledPool, chip)
            if (have == false)
            then
                table.insert(self.ListChipDisabledPool, chip)
            end
        end
    end
end

function UiChipMgrEx:_currentChipMoveEnd()
    if (self.ListChipEnabled == nil)
    then
        return
    end

    self.Tm = 0
    self.CurrentEnabledChip:moveEnd()
    self.CurrentEnabledChip:reset()
    local have = LuaHelper:TableContainsV(self.ListChipDisabledPool, self.CurrentEnabledChip)
    if (have == false)
    then
        table.insert(self.ListChipDisabledPool, self.CurrentEnabledChip)
    end
    self.CurrentEnabledChip = nil

    local l = #self.ListChipEnabled
    if (l > 0)
    then
        self.CurrentEnabledChip = table.remove(self.ListChipEnabled, 1)
        if (self.CurrentEnabledChip ~= nil)
        then
            self.CurrentEnabledChip:moveAndScale()
        end
    end
end

function UiChipMgrEx:moveChip(from, to, move_time, move_sound, chip_type, parent, move_end, start_movenotify)
    local chip = nil

    local l = #self.ListChipDisabledPool
    if (l > 0)
    then
        chip = table.remove(self.ListChipDisabledPool, 1)
    else
        local com_chip = CS.FairyGUI.UIPackage.CreateObject("Common", "ComChip")
        chip = UiChipEx:new(nil, com_chip.asCom, self)
    end
    local com_c = chip:getChipCom()
    parent:AddChild(com_c)

    chip:init(from, to, move_time, 0, move_sound, chip_type, move_end, start_movenotify)

    if (self.MoveChipOneByOne == true)
    then
        local have = LuaHelper:TableContainsV(self.ListChipEnabled, chip)
        if (have == false)
        then
            table.insert(self.ListChipEnabled, chip)
        end

        if (self.CurrentEnabledChip == nil)
        then
            local l = #self.ListChipEnabled
            if (l > 0)
            then
                self.CurrentEnabledChip = table.remove(self.ListChipEnabled, 1)
                self.CurrentEnabledChip:moveAndScale()
            end
        end
    else
        chip:moveAndScale()
    end

    return chip
end

function UiChipMgrEx:moveWinChip(index,from, to, move_time, move_sound, chip_type, parent, move_end, start_movenotify)
    local l = self.ListWinChips[index]
    if l~= nil then
        l:moveGolds(from, to, move_time, move_sound, chip_type, parent, move_end, start_movenotify)
    end
end

WinGolds = {}
function WinGolds:new(o, chip_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.TGolds = {}
    local com_chip1 = CS.FairyGUI.UIPackage.CreateObject("Desktop", "ComChip1")
    local chip1 = UiChipEx:new(nil, com_chip1.asCom, chip_mgr)
    table.insert(o.TGolds, chip1)
    local com_chip2 = CS.FairyGUI.UIPackage.CreateObject("Desktop", "ComChip2")
    local chip2 = UiChipEx:new(nil, com_chip2.asCom, chip_mgr)
    table.insert(o.TGolds, chip2)
    local com_chip3 = CS.FairyGUI.UIPackage.CreateObject("Desktop", "ComChip3")
    local chip3 = UiChipEx:new(nil, com_chip3.asCom, chip_mgr)
    table.insert(o.TGolds, chip3)
    local com_chip4 = CS.FairyGUI.UIPackage.CreateObject("Desktop", "ComChip4")
    local chip4 = UiChipEx:new(nil, com_chip4.asCom, chip_mgr)
    table.insert(o.TGolds, chip4)
    local com_chip5 = CS.FairyGUI.UIPackage.CreateObject("Desktop", "ComChip5")
    local chip5 = UiChipEx:new(nil, com_chip5.asCom, chip_mgr)
    table.insert(o.TGolds, chip5)
    local com_chip6 = CS.FairyGUI.UIPackage.CreateObject("Desktop", "ComChip6")
    local chip6 = UiChipEx:new(nil, com_chip6.asCom, chip_mgr)
    table.insert(o.TGolds, chip6)
    local com_chip7 = CS.FairyGUI.UIPackage.CreateObject("Desktop", "ComChip7")
    local chip7 = UiChipEx:new(nil, com_chip7.asCom, chip_mgr)
    table.insert(o.TGolds, chip7)

    return o
end

function WinGolds:moveGolds(from, to, move_time, move_sound, chip_type, parent, move_end, start_movenotify)
    local delay = 0
    local delay_t = 1 / 7
    for i, v in pairs(self.TGolds) do
        local com_c = v:getChipCom()
        parent:AddChild(com_c)
        if delay == 0 then
            v:init(from, to, move_time, delay, move_sound, chip_type, move_end, start_movenotify)
        else
            v:init(from, to, move_time, delay, move_sound, chip_type, nil, nil)
        end

        v:moveAndScale()
        delay = delay + delay_t
    end
end

function WinGolds:reset()
    for i, v in pairs(self.TGolds) do
        v:reset()
    end
end