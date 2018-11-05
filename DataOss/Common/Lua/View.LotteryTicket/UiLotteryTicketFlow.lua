-- Copyright(c) Cragon. All rights reserved.
-- 时时彩动画流程，所有的GTween动画都在此类中

---------------------------------------
UiLotteryTicketFlow = {}

---------------------------------------
function UiLotteryTicketFlow:new(o, lottery_ticket)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.Context = Context
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    o.ViewLotteryTicket = lottery_ticket
    return o
end

---------------------------------------
function UiLotteryTicketFlow:Create()
end

---------------------------------------
function UiLotteryTicketFlow:Destroy()
end