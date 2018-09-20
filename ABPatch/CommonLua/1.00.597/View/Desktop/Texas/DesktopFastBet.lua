DesktopFastBet = {}

function DesktopFastBet:new(o, btn, index, view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.BtnFastBet = btn
    o.Index = index
    o.ViewMgr = view_mgr
    btn.onClick:Add(
            function()
                o:_onClickFastBet()
            end)

    return o
end

function DesktopFastBet:setValue(fast_betinfo)
    self.FastInfo = fast_betinfo
    local title = {}
    local bet_value = ""
    if fast_betinfo.DesktopFastBetValue == 0.33333 then
        bet_value = "1/3"
    elseif fast_betinfo.DesktopFastBetValue == 0.66666 then
        bet_value = "2/3"
    elseif fast_betinfo.DesktopFastBetValue == 0.5 then
        bet_value = "1/2"
    else
        local int_v = math.floor(fast_betinfo.DesktopFastBetValue)
        local m = fast_betinfo.DesktopFastBetValue - int_v
        if m == 0 then
            bet_value = int_v
        else
            bet_value = tostring(fast_betinfo.DesktopFastBetValue)
        end
    end
    if fast_betinfo.DesktopFastBetType == DesktopFastBetType.BigBlindMuilty then
        table.insert(title, bet_value)
        table.insert(title, " [size=16]X[/size] ")
        table.insert(title, self.ViewMgr.LanMgr:getLanValue("BigBlindEx"))
    elseif fast_betinfo.DesktopFastBetType == DesktopFastBetType.BetPotMuilty then
        table.insert(title, bet_value)
        table.insert(title, " [size=16]X[/size] ")
        table.insert(title, self.ViewMgr.LanMgr:getLanValue("Pot"))
    elseif fast_betinfo.DesktopFastBetType == DesktopFastBetType.ConstValue then
        table.insert(title, fast_betinfo.DesktopFastBetValue)
    end
    self.BtnFastBet.title = table.concat(title)
    local enabled = fast_betinfo.BigThanStack == false
    local a = 0.5
    if enabled then
        a = 1
    end
    self.BtnFastBet.alpha = a
    self.BtnFastBet.touchable = enabled
end

function DesktopFastBet:_onClickFastBet()
    local ev = self.ViewMgr:getEv("EvUiClickFastBet")
    if (ev == nil)
    then
        ev = EvUiClickFastBet:new(nil)
    end
    ev.bet_value = self.FastInfo.NeedBetValue
    self.ViewMgr:sendEv(ev)
end