-- Copyright(c) Cragon. All rights reserved.

ItemLotteryTicketHistory = {}

function ItemLotteryTicketHistory:new(o,lottery_ticket,com_history,is_new,rank_type)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	o.GCoHistroy = com_history
    o.GLoaderHistory = o.GCoHistroy:GetChild("LoaderHistory").asLoader
    o.GImageNewSign = o.GCoHistroy:GetChild("NewSign").asImage
    local type_name = lottery_ticket.ViewLotteryTicketBase:getCardTypeName(rank_type)
    local packageName = lottery_ticket.LotteryTicketPackName
    if (UseLan == true) then
		packageName = lottery_ticket.ViewMgr.LanMgr:getLanPackageName()
	end
    o.GLoaderHistory.icon = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(packageName, type_name)
    o.GImageNewSign.visible = is_new
	return o
end

