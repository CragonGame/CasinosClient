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
                o:_onClick()
            end
    )
    return o
end

---------------------------------------
function UiLotteryTicketBetOperateItem:SetOperateInfo(tb_operate_id, operate_value, can_operate, is_currentoperate)
    self.mTbOperateId = tb_operate_id
    self.mCanOperate = can_operate
    self.mIsCurrentOperate = is_currentoperate
    self.GBtnBetOperate.text = UiChipShowHelper:getGoldShowStr2(operate_value, self.ViewLotteryTicket.ViewMgr.LanMgr.LanBase)
    self.GBtnBetOperate.enabled = self.mCanOperate
    self:_setIsCurrentOperate(self.mIsCurrentOperate)
end

---------------------------------------
function UiLotteryTicketBetOperateItem:SetIsCurrentOperate(is_currentoperate)
    self.mIsCurrentOperate = is_currentoperate
    self:_setIsCurrentOperate(self.mIsCurrentOperate)
end

---------------------------------------
function UiLotteryTicketBetOperateItem:SetCanOperate(can_operate)
    self.mCanOperate = can_operate
    self.GBtnBetOperate.enabled = self.mCanOperate
end

---------------------------------------
function UiLotteryTicketBetOperateItem:_setIsCurrentOperate()
end

---------------------------------------
function UiLotteryTicketBetOperateItem:_onClick()
    local ev = self.ViewLotteryTicket.ViewMgr:GetEv("EvLotteryTicketClickBetOperateType")
    if (ev == nil) then
        ev = EvLotteryTicketClickBetOperateType:new(nil)
    end
    ev.tb_bet_operateid = self.mTbOperateId
    self.ViewLotteryTicket.ViewMgr:SendEv(ev)
end