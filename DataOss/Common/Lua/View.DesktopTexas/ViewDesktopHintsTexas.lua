-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewDesktopHintsTexas = class(ViewBase)

---------------------------------------
function ViewDesktopHintsTexas:ctor()
    self.TransitiOnCreate = nil
    self.GHintsList = nil
end

---------------------------------------
function ViewDesktopHintsTexas:OnCreate()
    self.TransitiOnCreate = self.ComUi:GetTransition("TransitionCreate")
    self.TransitiOnCreate:Play()
    self.GHintsList = self.ComUi:GetChild("Grid").asList
    local com_shade = self.ComUi:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
    local co_content = self.ComUi:GetChild("Content").asCom
    local btn_close = co_content:GetChild("BtnClose").asButton
    btn_close.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
    self:_initDesktopHintInfo()
end

---------------------------------------
function ViewDesktopHintsTexas:_initDesktopHintInfo()
    local l = self.ViewMgr.TbDataMgr:GetMapData("HintsInfoTexas")
    for k, v in pairs(l) do
        local com = self.GHintsList:AddItemFromPool().asCom
        self.GHintsList:AddChild(com)
        local hint = ItemDesktopHintsInfo:new(nil, k, com, self.ViewMgr)
        hint:setDesktopHintInfo(v)
    end
end

---------------------------------------
function ViewDesktopHintsTexas:_onClickBtnClose()
    self.TransitiOnCreate:PlayReverse(
            function()
                self.ViewMgr:DestroyView(self)
            end
    )
end

---------------------------------------
ViewDesktopHintsTexasFactory = class(ViewFactory)

---------------------------------------
function ViewDesktopHintsTexasFactory:CreateView()
    local view = ViewDesktopHintsTexas:new()
    return view
end