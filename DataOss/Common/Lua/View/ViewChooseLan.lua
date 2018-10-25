-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewChooseLan = ViewBase:new()

---------------------------------------
function ViewChooseLan:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.ViewMgr = nil
    self.GoUi = nil
    self.ComUi = nil
    self.Panel = nil
    self.UILayer = nil
    self.InitDepth = nil
    self.ViewKey = nil
    return o
end

---------------------------------------
function ViewChooseLan:OnCreate()
    local com = self.ComUi:GetChild("CoShade").asCom
    com.onClick:Add(
            function()
                self:onClickBtnReturn()
            end
    )
    self.GListMultipleLanguage = self.ComUi:GetChild("ListMultipleLanguage").asList
    for key, value in pairs(CS.Casinos.CasinosContext.Instance.TbDataMgrLua:GetMapData("Lans"))
    do
        local item = self.GListMultipleLanguage:AddItemFromPool().asCom
        local item_lan = ItemLan:new(nil, item, key, self.ViewMgr.LanMgr)
    end
    local bg = self.ComUi:GetChild("Bg")
    if (bg ~= nil) then
        ViewHelper:MakeUiBgFiteScreen(ViewMgr.STANDARD_WIDTH, ViewMgr.STANDARD_HEIGHT, self.ComUi.width, self.ComUi.height, bg.width, bg.height, bg, BgAttachMode.Center)
    end
    local btn_return = self.ComUi:GetChild("BtnReturn").asButton
    btn_return.onClick:Add(
            function()
                self:onClickBtnReturn()
            end
    )
end

---------------------------------------
function ViewChooseLan:onClickBtnReturn()
    self.ViewMgr:DestroyView(self)
    local ev = self.ViewMgr:GetEv("EvUiCreateMainUi")
    if (ev == nil) then
        ev = EvUiCreateMainUi:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
ViewChooseLanFactory = ViewFactory:new()

---------------------------------------
function ViewChooseLanFactory:new(o, ui_package_name, ui_component_name,
                                  ui_layer, is_single, fit_screen)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.PackageName = ui_package_name
    self.ComponentName = ui_component_name
    self.UILayer = ui_layer
    self.IsSingle = is_single
    self.FitScreen = fit_screen
    return o
end

---------------------------------------
function ViewChooseLanFactory:CreateView()
    local view = ViewChooseLan:new(nil)
    return view
end