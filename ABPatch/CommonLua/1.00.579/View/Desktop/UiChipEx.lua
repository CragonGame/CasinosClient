UiChipEx = {}

function UiChipEx:new(o, com_chip, chip_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.GComChip = com_chip
    o.ChipMgr = chip_mgr
    o.GLoaderChip = nil
    o.From = nil
    o.To = nil
    o.MoveTime = nil
    o.MoveSound = nil
    o.TweenerMove = nil
    o.TweenerScale = nil
    o.ChipMoveType = nil
    o.MoveEndCallBack = nil
    o.StartMove = nil

    o:_resetChip()

    return o
end

function UiChipEx:init(from, to, move_time,delay,
                       move_sound, chip_type, move_end, start_movenotify)
    if (self.GComChip == nil)
    then
        self:_resetChip()
    end

    ViewHelper:setGObjectVisible(false, self.GComChip)
    self.From = from
    self.To = to
    self.MoveTime = move_time
    self.Delay = delay
    self.MoveSound = move_sound
    self.ChipMoveType = chip_type
    self.MoveEndCallBack = move_end
    self.StartMove = start_movenotify
end

function UiChipEx:reset()
    if (self.GComChip == nil)
    then
        self:_resetChip()
        return
    end

    self.From = CS.UnityEngine.Vector2.zero
    self.To = CS.UnityEngine.Vector2.zero
    self:_resetChip()
    ViewHelper:setGObjectVisible(false, self.GComChip)
    self.MoveEndCallBack = nil
    if (self.StartMove ~= nil)
    then
        self.StartMove()
        self.StartMove = nil
    end
end

function UiChipEx:getChipType()
    return self.ChipMoveType
end

function UiChipEx:getChipCom()
    return self.GComChip
end

function UiChipEx:moveAndScale()
    if (self.GComChip == nil)
    then
        self:_resetChip()
        return
    end

    if (self.StartMove ~= nil)
    then
        self.StartMove()
        self.StartMove = nil
    end

    CS.Casinos.UiHelper.setGObjectVisible(true, self.GComChip)
    self.TweenerMove = CS.Casinos.UiDoTweenHelper.TweenMove(self.GComChip, self.From, self.To, self.MoveTime, true):SetDelay(self.Delay):SetEase(CS.DG.Tweening.Ease.InOutSine):OnComplete(
            function()
                self.ChipMgr:chipEnquee(self)
            end)
    --self.TweenerScale = CS.Casinos.UiDoTweenHelper.TweenScale(self.GComChip, CS.UnityEngine.Vector2.zero, CS.UnityEngine.Vector2.one, self.MoveTime)
end

function UiChipEx:move(from, to, move_end)
    if (self.GComChip == nil)
    then
        self:_resetChip()
        return
    end

    self.TweenerMove = CS.Casinos.UiDoTweenHelper.TweenMove(self.GComChip, from, to, self.MoveTime, true):OnComplete(move_end)
end

function UiChipEx:moveEnd()
    if (self.MoveEndCallBack ~= nil)
    then
        self.MoveEndCallBack()
        self.MoveEndCallBack = nil
    end
end

function UiChipEx:_resetChip()
    if (self.TweenerMove ~= nil)
    then
        self.TweenerMove:Kill()
        self.TweenerMove = nil
    end
    if (self.TweenerScale ~= nil)
    then
        self.TweenerScale:Kill()
        self.TweenerScale = nil
    end
    self.GComChip:SetXY(10000,10000)
    self.GComChip.scale = CS.UnityEngine.Vector2.one
end