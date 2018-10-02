-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
UiCardDealingEx = {}

---------------------------------------
function UiCardDealingEx:new(o, com_card)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.GComCard = com_card
    o.MoveFrom = nil
    o.MoveTo = nil
    o.ScaleTo = nil
    o.RotateTo = nil
    o.MoveTime = nil
    o.MoveSound = nil
    o.TweenerPos = nil
    o.TweenerScale = nil
    o.MoveEndCallBack = nil
    o.TextureMgr = nil
    o.GLoaderCard = nil
    o.TweenerRotate = nil
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    return o
end

---------------------------------------
function UiCardDealingEx:init(move_from, move_to, resize_to, rotate_to, move_time, move_sound, moveend_callback)
    self.MoveFrom = move_from
    self.MoveTo = move_to
    self.ResizeTo = resize_to
    self.RotateTo = rotate_to
    self.MoveTime = move_time
    self.MoveSound = move_sound
    self.MoveEndCallBack = moveend_callback
end

---------------------------------------
function UiCardDealingEx:deal(call_back)
    CS.Casinos.UiHelper.setGObjectVisible(true, self.GComCard)
    self.TweenerPos = self.GComCard:TweenMove(self.MoveTo, self.MoveTime):SetSnapping(true):OnComplete(
            function()
                self:moveEnd()
            end
    )

    self.TweenerScale = self.GComCard:TweenResize(self.ResizeTo, self.MoveTime)-- CS.Casinos.UiDoTweenHelper.TweenResize(self.GComCard, self.GComCard.size, self.ResizeTo, self.MoveTime,false)
    self.TweenerRotate = CS.FairyGUI.GTween.To(self.GComCard.rotationY, self.RotateTo, self.MoveTime):SetTarget(self.GComCard, CS.FairyGUI.TweenPropType.RotationY)
    if (CS.System.String.IsNullOrEmpty(self.MoveSound) == false) then
        self.CasinosContext:Play(self.MoveSound, CS.Casinos._eSoundLayer.LayerNormal)
    end
end

---------------------------------------
function UiCardDealingEx:reset(with_ani, call_back)
    self:killTween(self.TweenerPos, false)
    self:killTween(self.TweenerScale, false)
    self:killTween(self.TweenerRotate, false)
    CS.Casinos.UiHelper.setGObjectVisible(false, self.GComCard)
    self.MoveFrom = CS.Casinos.LuaHelper.GetVector2(0, 0)
    self.MoveTo = CS.Casinos.LuaHelper.GetVector2(0, 0)
    self.MoveEndCallBack = nil
    if (self.GComCard ~= nil and self.GComCard.displayObject.gameObject ~= nil) then
        self.GComCard:SetXY(0, 0)
    end
end

---------------------------------------
function UiCardDealingEx:moveEnd()
    if (self.MoveEndCallBack ~= nil) then
        self.MoveEndCallBack()
        self.MoveEndCallBack = nil
    end
end

---------------------------------------
function UiCardDealingEx:killTween(tweener, is_complete)
    if (tweener ~= nil) then
        tweener:Kill(false)
        tweener = nil
    end
end