ItemLotteryTicketCard = {}

function ItemLotteryTicketCard:new(o,co_card,lottery_ticket)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	o.GCoCard = co_card
    o.GLoaderCard = o.GCoCard:GetChild("LoaderCard").asLoader
    o.GImageCardBack = o.GCoCard:GetChild("CardBack").asImage
    o.ViewLotteryTicket = lottery_ticket
    o.GLoaderCard.visible = false
    o.GImageCardBack.visible = true
	return o
end

function ItemLotteryTicketCard:showCard(card_data)
	if (card_data == nil)
	then
		return
	end
    if (CS.DG.Tweening.DOTween.IsTweening(self.GImageCardBack))
	then
		return
	end
	local toOpen = false
	if(self.GLoaderCard.visible)
	then
		toOpen = false
	else
		toOpen = true
	end
    self.TweenerTurnCard = CS.DG.Tweening.DOTween.To(
		function()
			return 0
		end,
		function(x)
			if (toOpen)
			then
				self.GImageCardBack.rotationY = x
				self.GLoaderCard.rotationY = -180 + x
				if (x > 90)
				then
					self.GLoaderCard.visible = true
					self.GImageCardBack.visible = false
					local card_name = string.format("%u",card_data.suit) .. "_" .. string.format("%u",card_data.type)
					self.GLoaderCard.icon = CS.Casinos.CasinosContext.Instance.PathMgr:combinePersistentDataPath
                      (CS.Casinos.UiHelperCasinos:getABCardResourceTitlePath() .. tostring(card_name) .. ".ab")
				end
			end
		end,
		180, 0.8
	):SetTarget(self.GImageCardBack):SetEase(CS.DG.Tweening.Ease.OutQuad):OnComplete(
		function()
			self.TweenerTurnCard = nil
		end
	)
	CS.Casinos.CasinosContext.Instance:Play("desk_new_card", CS.Casinos._eSoundLayer.LayerNormal)
end

function ItemLotteryTicketCard:resetCard()
	if (self.TweenerTurnCard ~= nil)
	then
		self.TweenerTurnCard:Kill()
        self.TweenerTurnCard = nil
	end
    self.GLoaderCard.visible = false
    self.GLoaderCard.rotationY = 0
    self.GImageCardBack.visible = true
    self.GImageCardBack.rotationY = 0
end