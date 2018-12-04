-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewChooseLan = class(ViewBase)

---------------------------------------
function ViewChooseLan:ctor()
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
function ViewChooseLan:OnDestroy()
end

---------------------------------------
function ViewChooseLan:onClickBtnReturn()
    self.ViewMgr:DestroyView(self)
    local ev = self:GetEv("EvUiCreateMainUi")
    if (ev == nil) then
        ev = EvUiCreateMainUi:new(nil)
    end
    self:SendEv(ev)
end

---------------------------------------
ViewChooseLanFactory = class(ViewFactory)

---------------------------------------
function ViewChooseLanFactory:CreateView()
    local view = ViewChooseLan:new()
    return view
end