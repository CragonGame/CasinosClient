-- Copyright(c) Cragon. All rights reserved.
-- 单个预设下注值

---------------------------------------
UiLotteryTicketBetOperateItem = {}

---------------------------------------
function UiLotteryTicketBetOperateItem:new(o, bet_operat, view_lotteryticket)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.Context = Context
    o.ViewLotteryTicket = view_lotteryticket
    o.GBtnBetOperate = bet_operat
    o.GBtnBetOperate.onClick:Add(
            function()
                o:onClick()
            end
    )
    return o
end

---------------------------------------
function UiLotteryTicketBetOperateItem:setOperateInfo(tb_operate_id, operate_value, can_operate, is_currentoperate)
    self.mTbOperateId = tb_operate_id
    self.mCanOperate = can_operate
    self.mIsCurrentOperate = is_currentoperate
    self.GBtnBetOperate.text = UiChipShowHelper:getGoldShowStr2(operate_value, self.ViewLotteryTicket.ViewMgr.LanMgr.LanBase)
    self:setCanOperate(self.mCanOperate)
    self:_setIsCurrentOperate(self.mIsCurrentOperate)
end

---------------------------------------
function UiLotteryTicketBetOperateItem:setIsCurrentOperate(is_currentoperate)
    self.mIsCurrentOperate = is_currentoperate
    self:_setIsCurrentOperate(self.mIsCurrentOperate)
end

---------------------------------------
function UiLotteryTicketBetOperateItem:setcanOperate(can_operate)
    self.mCanOperate = can_operate
    self:setCanOperate(self.mCanOperate)
end

---------------------------------------
function UiLotteryTicketBetOperateItem:isCurrentOperate()
    return self.mIsCurrentOperate
end

---------------------------------------
function UiLotteryTicketBetOperateItem:setCanOperate(can_operate)
    self.GBtnBetOperate.enabled = can_operate
end

---------------------------------------
function UiLotteryTicketBetOperateItem:_setIsCurrentOperate()
end

---------------------------------------
function UiLotteryTicketBetOperateItem:onClick()
    local ev = self.ViewLotteryTicket.ViewMgr:GetEv("EvLotteryTicketClickBetOperateType")
    if (ev == nil) then
        ev = EvLotteryTicketClickBetOperateType:new(nil)
    end
    ev.tb_bet_operateid = self.mTbOperateId
    self.ViewLotteryTicket.ViewMgr:SendEv(ev)
end