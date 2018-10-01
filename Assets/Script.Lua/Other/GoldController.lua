-- Copyright (c) Cragon. All rights reserved.

---------------------------------------
GoldController = {}

---------------------------------------
function GoldController:new(o,setgold)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
    local controller_mgr = ControllerMgr:new(nil)
    o.ControllerActor = controller_mgr:GetController("Actor")
    o.ControllerDesktopH = controller_mgr:GetController("DesktopH")
    o.Gold = o.ControllerActor.PropGoldAcc:get()
    o.MapDeltaGold = {}
    o.SetGold = setgold
    o.SetGold(o.Gold)

    return o
end

---------------------------------------
function GoldController:goldChange(change_reason, delta_gold, user_data)
    local can_changegold = false

    if (change_reason == GoldAccChangeReason.DesktopHWin)
    then
        if (user_data ~= self.ControllerDesktopH.DesktopHGuid)
        then
            can_changegold = true
        end
    elseif (change_reason == GoldAccChangeReason.DesktopHLoose)
    then
        if (user_data ~= self.ControllerDesktopH.DesktopHGuid)
        then
            can_changegold = true
        else
            if (self.ControllerDesktopH.IsBankPlayer == false)
            then
                can_changegold = true
            end
        end
    else
        can_changegold = true
    end

    if (can_changegold == true)
    then
        self.Gold = self.Gold + delta_gold
        self.SetGold(self.Gold)
    else
        self.MapDeltaGold[change_reason] = delta_gold
    end
end

---------------------------------------
function GoldController:addDeltaGold(change_reason)
    local delta_gold = self.MapDeltaGold[change_reason]
    if(delta_gold ~= nil)
    then
        self.MapDeltaGold[change_reason] = nil
        self.Gold = self.Gold + delta_gold
        self.SetGold(self.Gold)
    end
end

---------------------------------------
function GoldController:refreshGold(gold)
    self.Gold = gold
    self.SetGold(self.Gold)
    self.MapDeltaGold = {}
end