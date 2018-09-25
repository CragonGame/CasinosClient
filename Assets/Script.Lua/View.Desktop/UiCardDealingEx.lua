UiCardDealingEx = {}

function UiCardDealingEx:new(o,com_card)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
    o.GComCard = com_card
    o.MoveFrom  = nil
    o.MoveTo  = nil
    o.ScaleTo  = nil
    o.RotateTo  = nil
    o.MoveTime  = nil
    o.MoveSound  = nil
    o.TweenerPos  = nil
    o.TweenerScale  = nil
    o.MoveEndCallBack  = nil
    o.TextureMgr = nil
    o.GLoaderCard = nil
    o.TweenerRotate = nil

    return o
end

function UiCardDealingEx:init(move_from, move_to, resize_to, rotate_to,move_time, move_sound, move_end)
    self.MoveFrom = move_from
    self.MoveTo = move_to
    self.ResizeTo = resize_to
    self.RotateTo = rotate_to
    self.MoveTime = move_time
    self.MoveSound = move_sound
    self.MoveEndCallBack = move_end
end

function UiCardDealingEx:deal(call_back)
    CS.Casinos.UiHelper.setGObjectVisible(true,self.GComCard)
    self.TweenerPos = CS.Casinos.UiDoTweenHelper.TweenMove(self.GComCard, self.MoveFrom, self.MoveTo, self.MoveTime, true):OnComplete(
            function()
                self:moveEnd()
            end
    )

    self.TweenerScale =  self.GComCard:TweenResize(self.ResizeTo,self.MoveTime)-- CS.Casinos.UiDoTweenHelper.TweenResize(self.GComCard, self.GComCard.size, self.ResizeTo, self.MoveTime,false)
    self.TweenerRotate = CS.Casinos.UiDoTweenHelper.TweenRotate(self.GComCard, self.GComCard.rotation, self.RotateTo, self.MoveTime)
    if (CS.System.String.IsNullOrEmpty(self.MoveSound) == false)
    then
        CS.Casinos.CasinosContext.Instance:Play(self.MoveSound, CS.Casinos._eSoundLayer.LayerNormal)
    end
end

function UiCardDealingEx:reset(with_ani, call_back)
    self:killTween(self.TweenerPos,false)
    self:killTween(self.TweenerScale,false)
    self:killTween(self.TweenerRotate,false)
    CS.Casinos.UiHelper.setGObjectVisible(false,self.GComCard)
    self.MoveFrom = CS.Casinos.LuaHelper.GetVector2(0,0)
    self.MoveTo = CS.Casinos.LuaHelper.GetVector2(0,0)
    self.MoveEndCallBack = nil
    if (self.GComCard ~= nil and self.GComCard.displayObject.gameObject ~= nil)
    then
        self.GComCard:SetXY(0,0)
    end
end

function UiCardDealingEx:moveEnd()
    if (self.MoveEndCallBack ~= nil)
    then
        self.MoveEndCallBack()
        self.MoveEndCallBack = nil
    end
end

function UiCardDealingEx:killTween(tweener, is_complete)
    if (tweener ~= nil)
    then
        tweener:Kill(is_complete)
        tweener = nil
    end
end