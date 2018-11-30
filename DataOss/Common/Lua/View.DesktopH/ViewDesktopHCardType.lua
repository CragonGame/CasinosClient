-- Copyright(c) Cragon. All rights reserved.
-- 牌型界面，后续与普通桌中牌型界面命名调成一致

---------------------------------------
ViewDesktopHCardType = class(ViewBase)

---------------------------------------
function ViewDesktopHCardType:ctor()
    self.GCoCardType = nil
end

---------------------------------------
function ViewDesktopHCardType:OnCreate()
    self.ComUi.onClick:Add(
            function()
                self:_onClickCoCardType()
            end
    )
end

---------------------------------------
function ViewDesktopHCardType:showCardType(co_cardtype)
    self.GCoCardType = co_cardtype
    self.ComUi:AddChild(self.GCoCardType)
    self.GCoCardType:SetXY(-self.GCoCardType.width, self.ComUi.height / 2 - self.GCoCardType.height / 2)
    self.GCoCardType:TweenMoveX(0, 0.5)
end

---------------------------------------
function ViewDesktopHCardType:_onClickCoCardType()
    self.GCoCardType:TweenMoveX(-self.GCoCardType.width, 0.5):OnComplete(
            function()
                self.ViewMgr:DestroyView(self)
            end
    )
end

---------------------------------------
ViewDesktopHCardTypeFactory = class(ViewFactory)

---------------------------------------
function ViewDesktopHCardTypeFactory:CreateView()
    local view = ViewDesktopHCardType:new()
    return view
end