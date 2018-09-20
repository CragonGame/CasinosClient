DesktopHCard = {}

function DesktopHCard:new(o, dealer, is_bankplayer)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    local view_mgr = ViewMgr:new(nil)
    o.ViewDesktopH = view_mgr:getView("DesktopH")
    o.Dealer = dealer
    o.IsBankPlayer = is_bankplayer
    local l = o.ViewDesktopH:getDesktopBasePackageName()
    o.GCoCard = CS.FairyGUI.UIPackage.CreateObject(l, "CoCard" .. o.ViewDesktopH.FactoryName).asCom
    o.GLoaderCard = o.GCoCard:GetChild("LoaderCard").asLoader
    o.GImageCardBack = o.GCoCard:GetChild("CardBack").asImage
    local ani = o.GCoCard:GetChild("AniTurnCard")
    if (ani ~= nil)
    then
        o.AniTurnCard = ani.asMovieClip
    end
    o.ViewDesktopH.GCoDesktopHPoolParent:AddChild(o.GCoCard)
    o.CardData = nil
    o.CardIndex = 0
    o.GCoParent = nil
    o.DealerPos = nil
    o.Tweener = nil
    o.IsReset = false
    o.MoveCardWidthPercent = 0
    o.ViewDesktopH.GCoDesktopHPoolParent:AddChild(o.GCoCard)
    local pos = CS.UnityEngine.Vector2()
    pos.x = 10000
    pos.y = 10000
    o.GCoCard.xy = pos
    return o
end

function DesktopHCard:setCardData(card_data)
    self.CardData = card_data
    local card_name = self.ViewDesktopH.UiDesktopHBase:getCardResName(self.CardData)
    self.GLoaderCard.icon = CS.Casinos.CasinosContext.Instance.PathMgr:combinePersistentDataPath(CS.Casinos.UiHelperCasinos:getABCardResourceTitlePath() .. string.lower(card_name) .. ".ab")
end

function DesktopHCard:setDealCardData(dealer_pos, card_index)
    local dealerpos_x = dealer_pos.x
    dealerpos_x = dealerpos_x - self.GCoCard.width / 2
    local dealer_p = CS.UnityEngine.Vector2()
    dealer_p.x = dealerpos_x
    dealer_p.y = dealer_pos.y
    self.DealerPos = dealer_p
    self.CardIndex = card_index
    self.GCoCard.sortingOrder = self.ViewDesktopH.ComUi.sortingOrder + card_index
end

function DesktopHCard:setParent(co_parent)
    self.GCoParent = co_parent
end

function DesktopHCard:dealCardToPosThenTranslation(to_pos, move_cardwidth_percent)
    self.MoveCardWidthPercent = move_cardwidth_percent
    self.GLoaderCard.visible = false
    self.GImageCardBack.visible = true
    self.GCoCard.xy = self.DealerPos

    self.Tweener = self.GCoCard:TweenMove(to_pos, 0.3):SetDelay(0.2):OnComplete(
            function()
                self:_translationCard()
            end
    )

    CS.Casinos.CasinosContext.Instance:play("desk_new_card", CS.Casinos._eSoundLayer.LayerNormal)
end

function DesktopHCard:dealCardToPosAtFirstTm(card_index, from, to_pos, delay, move_cardwidth_percent)
    self.GLoaderCard.visible = false
    self.GImageCardBack.visible = true
    self.CardIndex = card_index
    self.GCoCard.xy = from
    local to = to_pos
    local x = to.x + self.GCoCard.width * move_cardwidth_percent * self.CardIndex
    to.x = x
    to.z = 1

    self.Tweener = self.GCoCard:TweenMove(to, 0.3):SetDelay(delay):OnComplete(
            function()
                self:_setParent()
            end
    )

    CS.Casinos.CasinosContext.Instance:play("desk_new_card", CS.Casinos._eSoundLayer.LayerNormal)
end

function DesktopHCard:dealCardAtPos2(to_pos, move_cardwidth_percent)
    self.MoveCardWidthPercent = move_cardwidth_percent
    self.GLoaderCard.visible = false
    self.GImageCardBack.visible = true

    local pos = CS.UnityEngine.Vector2()
    if (self.GCoParent ~= nil)
    then
        self.GCoParent:AddChild(self.GCoCard)
        local x = self.GCoCard.width * self.MoveCardWidthPercent * self.CardIndex
        pos.x = x
        pos.y = 0
    else
        pos = to_pos
    end

    self.GCoCard.xy = pos
end

function DesktopHCard:showCard(action)
    self.GImageCardBack.rotationY = 0
    self.GLoaderCard.rotationY = 0
    self.GLoaderCard.visible = false
    self.GImageCardBack.visible = true
    self.IsReset = false
    if (self.CardData == nil)
    then
        return
    end

    if (self.AniTurnCard ~= nil)
    then
        self.AniTurnCard.visible = true
        self.AniTurnCard:SetPlaySettings(1, -1, 1, -1)
        self.GImageCardBack.visible = false
        self.AniTurnCard.onPlayEnd:Add(
                function()
                    self.AniTurnCard.visible = false

                    if (self.IsReset)
                    then
                        return
                    end

                    self.GLoaderCard.visible = true
                    CS.Casinos.CasinosContext.Instance:play(self.ViewDesktopH.UiDesktopHBase:getTurnCardSound(), CS.Casinos._eSoundLayer.LayerNormal)
                    action()
                end
        )
        self.AniTurnCard.playing = true
    else
        if (CS.DG.Tweening.DOTween.IsTweening(self.GImageCardBack))
        then
            return
        end
        local toOpen = false
        if (self.GLoaderCard.visible == true)
        then
            toOpen = false
        else
            toOpen = true
        end
        local tweener = CS.DG.Tweening.DOTween.To(
                function()
                    local t = math.ceil(0)
                    return t
                end,
                function(x)
                    if (self.IsReset == true)
                    then
                        return
                    end

                    if (toOpen)
                    then
                        if (self.IsReset == true)
                        then
                            return
                        end

                        self.GImageCardBack.rotationY = x
                        self.GLoaderCard.rotationY = -180 + x
                        if (x > 90)
                        then
                            self.GLoaderCard.visible = true
                            self.GImageCardBack.visible = false
                        end
                    else
                        if (self.IsReset == true)
                        then
                            return
                        end

                        self.GImageCardBack.rotationY = -180 + x
                        self.GLoaderCard.rotationY = x
                        if (x > 90)
                        then
                            self.GLoaderCard.visible = false
                            self.GImageCardBack.visible = true
                        end
                    end
                end
        , 180, 0.8)
        tweener:SetTarget(self.GImageCardBack):SetEase(CS.DG.Tweening.Ease.OutQuad):OnComplete(
                function()
                    if (self.IsReset == true)
                    then
                        return
                    end

                    if (action ~= nil)
                    then
                        action()
                    end
                end
        )

        CS.Casinos.CasinosContext.Instance:play(self.ViewDesktopH.UiDesktopHBase:getTurnCardSound(), CS.Casinos._eSoundLayer.LayerNormal)
    end
end

function DesktopHCard:resetCard()
    self.IsReset = true
    self:_resetCard()
    self.Dealer:setResetCard(self, IsBankPlayer)
end

function DesktopHCard:_translationCard()
    local to = self.GCoCard.xy
    local x = to.x + self.GCoCard.width * self.MoveCardWidthPercent * self.CardIndex
    self.Tweener = self.GCoCard:TweenMoveX(x, 0.3):OnComplete(
            function()
                self:_setParent()
            end
    )
end

function DesktopHCard:_setParent()
    if (self.GCoParent ~= nil)
    then
        self.GCoParent:AddChild(self.GCoCard)
        local pos = CS.UnityEngine.Vector2()
        local x = self.GCoCard.width * self.MoveCardWidthPercent * self.CardIndex
        pos.x = x
        pos.y = 0
        self.GCoCard.xy = pos
    end
end

function DesktopHCard:_resetCard()
    self.GCoParent = nil
    self.CardData = nil
    if (self.GCoCard ~= nil and self.GCoCard.displayObject.gameObject ~= nil)
    then
        if (self.Tweener ~= nil)
        then
            self.Tweener:Kill()
            self.Tweener = nil
        end
        self.ViewDesktopH.GCoDesktopHPoolParent:AddChild(self.GCoCard)
        local pos = CS.UnityEngine.Vector2()
        pos.x = 10000
        pos.y = 10000
        self.GCoCard.xy = pos
    end
    if (self.AniTurnCard ~= nil)
    then
        self.AniTurnCard.onPlayEnd:Clear()
        self.AniTurnCard.visible = false
    end
    if (self.GLoaderCard ~= nil and self.GLoaderCard.displayObject.gameObject ~= nil)
    then
        self.GLoaderCard.rotationY = 0
        self.GLoaderCard.visible = false
        self.GLoaderCard.icon = nil
    end
    if (self.GImageCardBack ~= nil and self.GImageCardBack.displayObject.gameObject ~= nil)
    then
        self.GImageCardBack.rotationY = 0
        self.GImageCardBack.visible = true
    end
end