-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
PlayerShowTips = {}

---------------------------------------
function PlayerShowTips:new(o, player_info)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.CasinosContext = CS.Casinos.CasinosContext.Instance
    o.PlayerInfo = player_info
    local com_ui = o.PlayerInfo.ComUi
    local p_helper = ParticleHelper:new(nil)

    o.ComPoint = com_ui:GetChild("ComPoint").asCom
    local controller_point = o.ComPoint:GetController("ControllerTips")
    controller_point.selectedIndex = 0
    local point_pp = o.ComPoint:GetChild("ParticalParent").asGraph
    local particle1 = p_helper:GetParticel("showtips.ab")
    local p_1 = CS.UnityEngine.Object.Instantiate(particle1:LoadAsset("ShowTips"))
    point_pp:SetNativeObject(CS.FairyGUI.GoWrapper(p_1))
    o.TextPoint = o.ComPoint:GetChild("TextPoint").asTextField
    ViewHelper:SetGObjectVisible(false, o.ComPoint)

    o.ComWinGold = com_ui:GetChild("ComWinGold").asCom
    local controller_wingold = o.ComWinGold:GetController("ControllerTips")
    controller_wingold.selectedIndex = 1
    local wingold_pp = o.ComWinGold:GetChild("ParticalParent").asGraph
    local particle2 = p_helper:GetParticel("showtips.ab")
    local p_2 = CS.UnityEngine.Object.Instantiate(particle2:LoadAsset("ShowTips"))
    wingold_pp:SetNativeObject(CS.FairyGUI.GoWrapper(p_2))
    o.TextWinGold = o.ComWinGold:GetChild("TextWinGold").asTextField
    ViewHelper:SetGObjectVisible(false, o.ComWinGold)

    o.ComExp = com_ui:GetChild("ComExp").asCom
    local controller_exp = o.ComExp:GetController("ControllerTips")
    controller_exp.selectedIndex = 2
    local exp_pp = o.ComExp:GetChild("ParticalParent").asGraph
    local particle3 = p_helper:GetParticel("showtips.ab")
    local p_3 = CS.UnityEngine.Object.Instantiate(particle3:LoadAsset("ShowTips"))
    exp_pp:SetNativeObject(CS.FairyGUI.GoWrapper(p_3))
    o.TextExp = o.ComExp:GetChild("TextExp").asTextField
    ViewHelper:SetGObjectVisible(false, o.ComExp)

    return o
end

---------------------------------------
function PlayerShowTips:showWinGold(win_gold)
    ViewHelper:SetGObjectVisible(false, self.ComWinGold)
    local t = {}
    table.insert(t, "+")
    table.insert(t, win_gold)
    local tips = table.concat(t)
    self.TextWinGold.text = tips
    self.TweenWinGold = self.ComWinGold:TweenMoveY(-20, 1.5):SetDelay(0.5):SetEase(CS.FairyGUI.EaseType.ExpoOut):OnStart(
            function()
                ViewHelper:SetGObjectVisible(true, self.ComWinGold)
            end):OnComplete(
            function()
                self.ComWinGold.y = 59
                self.TextWinGold.text = ""
                ViewHelper:SetGObjectVisible(false, self.ComWinGold)
            end)
end

---------------------------------------
function PlayerShowTips:showExpAndPoint(exp, point)
    if exp > 0 then
        ViewHelper:SetGObjectVisible(true, self.ComExp)
        local t = {}
        table.insert(t, "jy")
        table.insert(t, "+")
        table.insert(t, exp)
        self.TextExp.text = table.concat(t)
        self.TweenExp = self.ComExp:TweenMoveY(-16, 1):SetEase(CS.FairyGUI.EaseType.BackOut):OnComplete(
                function()
                    self.ComExp.y = 59
                    self.TextExp.text = ""
                    ViewHelper:SetGObjectVisible(false, self.ComExp)
                end)
    end
    if point > 0 then
        ViewHelper:SetGObjectVisible(false, self.ComPoint)
        t = {}
        table.insert(t, "+")
        table.insert(t, point)
        self.TextPoint.text = table.concat(t)
        self.TweenPoint = self.ComPoint:TweenMoveY(-16, 1):SetEase(CS.FairyGUI.EaseType.BackOut):SetDelay(0.2):OnStart(
                function()
                    ViewHelper:SetGObjectVisible(true, self.ComPoint)
                end):OnComplete(
                function()
                    self.ComPoint.y = 59
                    self.TextPoint.text = ""
                    ViewHelper:SetGObjectVisible(false, self.ComPoint)
                end)
    end
end

---------------------------------------
function PlayerShowTips:reset()
    if self.TweenWinGold ~= nil then
        self.TweenWinGold:Kill(false)
    end
    if self.TweenExp ~= nil then
        self.TweenExp:Kill(false)
    end
    if self.TweenPoint ~= nil then
        self.TweenPoint:Kill(false)
    end
    self.ComPoint.y = 59
    self.TextPoint.text = ""
    ViewHelper:SetGObjectVisible(false, self.ComPoint)
    self.ComWinGold.y = 59
    self.TextWinGold.text = ""
    ViewHelper:SetGObjectVisible(false, self.ComWinGold)
    self.ComExp.y = 59
    self.TextExp.text = ""
    ViewHelper:SetGObjectVisible(false, self.ComExp)
end