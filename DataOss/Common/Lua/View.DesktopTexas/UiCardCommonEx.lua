-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
UiCardCommonEx = {
    RotateTime = 0.2
}

---------------------------------------
function UiCardCommonEx:new(o, com_card)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.CasinosContext = CS.Casinos.CasinosContext.Instance
    o.Card = nil
    o.ResetCallBack = nil
    o.LoaderTicket = nil
    o.TextureMgr = CS.Casinos.CasinosContext.Instance.TextureMgr
    o.GComCard = com_card
    o.GLoaderCard = CS.Casinos.LuaHelper.GLoaderCastToGLoaderEx(o.GComCard:GetChild("LoaderCard").asLoader)
    o.GImageCardBack = o.GComCard:GetChild("CardBack").asImage
    o.GImageCardHighLight = o.GComCard:GetChild("ImageCardHighLight").asImage
    o.Name = o.GComCard.name
    o:hideHightLight()
    return o
end

---------------------------------------
function UiCardCommonEx:setCardData(card)
    self.Card = card
    if (self.GComCard.displayObject.gameObject ~= nil) then
        self.GComCard.rotationY = 0
    end
    self:hideHightLight()
end

---------------------------------------
-- 公共牌翻牌
function UiCardCommonEx:show(with_animation, call_back)
    if (self.Card == nil) then
        ViewHelper:SetGObjectVisible(true, self.GComCard, self.GImageCardBack)
        ViewHelper:SetGObjectVisible(false, self.GLoaderCard)
    else
        ViewHelper:SetGObjectVisible(true, self.GComCard, self.GImageCardBack)
        local card_name = tostring(self.Card.Suit) .. "_" .. tostring(self.Card.Type)
        local l_card_name = string.lower(card_name)
        local ab_card_name = self.CasinosContext.PathMgr.DirAbCard .. l_card_name .. ".ab"
        self.LoaderTicket = self.CasinosContext.TextureMgr:getTexture(l_card_name, ab_card_name,
                function(tick, t)
                    if (self.GComCard == nil or self.GComCard.displayObject.gameObject == nil) then
                        return
                    end
                    if (self.LoaderTicket ~= tick) then
                        return
                    end
                    self.LoaderTicket = nil
                    self.GLoaderCard.color = CS.UnityEngine.Color.white
                    self.GLoaderCard.texture = CS.FairyGUI.NTexture(t)

                    if (with_animation == true) then
                        self.TweenerRotate = CS.FairyGUI.GTween.To(self.GImageCardBack.rotationY, 90, UiCardCommonEx.RotateTime)
                                               :SetTarget(self.GImageCardBack, CS.FairyGUI.TweenPropType.RotationY)
                                               :SetEase(CS.FairyGUI.EaseType.Linear)
                                               :OnComplete(
                                function()
                                    ViewHelper:SetGObjectVisible(false, self.GImageCardBack)
                                    ViewHelper:SetGObjectVisible(true, self.GLoaderCard)
                                    self.GImageCardBack.rotationY = 180
                                    self.GLoaderCard.rotationY = 90
                                    self.TweenerRotate = CS.FairyGUI.GTween.To(90, 0, UiCardCommonEx.RotateTime)
                                                           :SetTarget(self.GLoaderCard, CS.FairyGUI.TweenPropType.RotationY)
                                                           :SetEase(CS.FairyGUI.EaseType.Linear)
                                                           :OnComplete(
                                            function()
                                                self.GLoaderCard.rotationY = 0
                                                if (call_back ~= nil) then
                                                    call_back()
                                                end
                                            end
                                    )
                                end
                        )
                        self.CasinosContext:Play("fapaia", CS.Casinos._eSoundLayer.LayerNormal)
                    else
                        ViewHelper:SetGObjectVisible(true, self.GComCard, self.GLoaderCard)
                        ViewHelper:SetGObjectVisible(false, self.GImageCardBack)
                        self.GComCard.rotationY = 0
                        if (call_back ~= nil) then
                            call_back()
                        end
                    end
                end
        )
    end
end

---------------------------------------
-- 3张公共牌发牌
function UiCardCommonEx:deal(call_back)
    ViewHelper:SetGObjectVisible(true, self.GComCard, self.GImageCardBack)
    self:killTween(self.TweenerRotate)

    self.TweenerRotate = CS.FairyGUI.GTween.To(self.GImageCardBack.rotationY, 180, UiCardCommonEx.RotateTime)
                           :SetTarget(self.GImageCardBack, CS.FairyGUI.TweenPropType.RotationY)
                           :SetEase(CS.FairyGUI.EaseType.Linear)
                           :OnComplete(
            function()
                self.GImageCardBack.rotationY = 180
                if (call_back ~= nil) then
                    call_back()
                end
            end)
    self.CasinosContext:Play("fapaia", CS.Casinos._eSoundLayer.LayerNormal)
end

---------------------------------------
function UiCardCommonEx:reset(with_ani, call_bak)
    if (with_ani == false) then
        self:_reset()
        return
    end

    if (self.GComCard == nil or self.GComCard.displayObject.gameObject == nil) then
        return
    end

    self:killTween(self.TweenerRotate)
    self.TweenerRotate = CS.FairyGUI.GTween.To(self.GComCard.rotationY, 90, UiCardCommonEx.RotateTime)
                           :SetTarget(self.GComCard, CS.FairyGUI.TweenPropType.RotationY)
                           :SetEase(CS.FairyGUI.EaseType.Linear)
                           :OnComplete(
            function()
                self:_reset()
            end)
    self.CasinosContext:Play("fapaia", CS.Casinos._eSoundLayer.LayerNormal)
    self.Card = nil
    self.ResetCallBack = call_bak
end

---------------------------------------
function UiCardCommonEx:showHighLight(need_showhighlight, list_cards_data, list_cards_all_data, is_end)
    if (self.GComCard == nil or self.GComCard.displayObject.gameObject == nil) then
        return
    end

    local show_highlight = false
    if (need_showhighlight) then
        for i, v in pairs(list_cards_data) do
            if (self.Card ~= nil and self.Card.Type == v.type and self.Card.Suit == v.suit) then
                show_highlight = true
                break
            end
        end
    end

    local card_color = CS.UnityEngine.Color.white
    if (is_end) then
        local not_dark = false
        for i, v in pairs(list_cards_all_data) do
            if (self.Card ~= nil and self.Card.Type == v.type and self.Card.Suit == v.suit) then
                not_dark = true
                break
            end
        end
        if (not_dark == false) then
            card_color = CS.UnityEngine.Color.gray
        else
            card_color = CS.UnityEngine.Color.white
        end
    else
        card_color = CS.UnityEngine.Color.white
    end

    ViewHelper:SetGObjectVisible(show_highlight, self.GImageCardHighLight)
    self.GLoaderCard.color = card_color
end

---------------------------------------
function UiCardCommonEx:hideHightLight()
    if (self.GImageCardHighLight.displayObject.gameObject ~= nil) then
        ViewHelper:SetGObjectVisible(false, self.GImageCardHighLight)
        self.GLoaderCard.color = CS.UnityEngine.Color.white
    end
end

---------------------------------------
function UiCardCommonEx:_reset()
    self:killTween(self.TweenerRotate)
    self.LoaderTicket = nil
    if (self.GComCard.displayObject.gameObject ~= nil) then
        self.GLoaderCard.icon = nil
        self.GComCard.rotationY = 0
        ViewHelper:SetGObjectVisible(false, self.GComCard, self.GLoaderCard, self.GImageCardHighLight)
        self.GLoaderCard.color = CS.UnityEngine.Color.white
    end
    if (self.ResetCallBack ~= nil) then
        self.ResetCallBack(self)
        self.ResetCallBack = nil
    end
end

---------------------------------------
function UiCardCommonEx:killTween(tweener)
    --local is_com = false
    --if (is_complete == nil) then
    --    is_com = is_complete
    --end
    if (tweener ~= nil) then
        tweener:Kill(false)
        tweener = nil
    end
end