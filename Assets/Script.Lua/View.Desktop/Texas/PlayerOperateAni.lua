-- Copyright(c) Cragon. All rights reserved.

PlayerOperateAni = {}

function PlayerOperateAni:new(o, com_ui)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.CasinosContext = CS.Casinos.CasinosContext.Instance
    o.TransitionShowAutoOperate = com_ui:GetTransition("TransitionShowAutoOperate")
    o.TransitionShowOther = com_ui:GetTransition("TransitionShowOther")
    o.AutoOperateIsPlayed = false
    o.ShowOtherIsPlayed = false
    o.AutoOperateIsPlayState = true
    o.ShowOtherIsPlayState = true

    return o
end

-- 本人托管操作条隐藏动画播完后，紧接着播放操作条显示动画
function PlayerOperateAni:showAutoOperate(play_single)
    if self.AutoOperateIsPlayed == false or self.AutoOperateIsPlayState then
        if self.TransitionShowOther.playing then
            self.TransitionShowOther:Stop(true,true)
        end
        if self.TransitionShowAutoOperate.playing then
            self.TransitionShowAutoOperate:Stop(true,true)
        end

        self.AutoOperateIsPlayed = true
        if play_single then
            self.TransitionShowAutoOperate:Play()
        else
            if self.ShowOtherIsPlayed == false or self.ShowOtherIsPlayState == false then
                self.ShowOtherIsPlayState = true
                self.ShowOtherIsPlayed = true
                self.TransitionShowOther:PlayReverse(
                        function()
                            self.TransitionShowAutoOperate:Play()
                        end)
            else
                self.TransitionShowAutoOperate:Play()
            end
        end

        self.AutoOperateIsPlayState = false
    end
end

-- 本人操作条隐藏动画播完后，紧接着播放托管操作条显示动画
function PlayerOperateAni:showOtherOperate(play_single)
    if self.ShowOtherIsPlayed == false or self.ShowOtherIsPlayState then
        if self.TransitionShowOther.playing then
            self.TransitionShowOther:Stop(true,true)
        end
        if self.TransitionShowAutoOperate.playing then
            self.TransitionShowAutoOperate:Stop(true,true)
        end

        self.ShowOtherIsPlayed = true
        if play_single then
            self.TransitionShowOther:Play()
        else
            if self.AutoOperateIsPlayed == false or self.AutoOperateIsPlayState == false then
                self.AutoOperateIsPlayState = true
                self.AutoOperateIsPlayed = true
                self.TransitionShowAutoOperate:PlayReverse(
                        function()
                            self.TransitionShowOther:Play()
                        end)
            end
        end

        self.ShowOtherIsPlayState = false
    end
end

function PlayerOperateAni:reset()
    self.TransitionShowOther:PlayReverse()
    self.TransitionShowAutoOperate:PlayReverse()
    self.TransitionShowAutoOperate:Stop(true, true)
    self.TransitionShowOther:Stop(true, true)
    self.AutoOperateIsPlayed = false
    self.ShowOtherIsPlayed = false
    self.AutoOperateIsPlayState = true
    self.ShowOtherIsPlayState = true
end