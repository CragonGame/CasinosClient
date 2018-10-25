-- Copyright(c) Cragon. All rights reserved.
--成长奖励，快照

BGrowData = {}

function BGrowData:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.GetRewardGold = 0 -- 已领金币
    o.CanGetRewardGold = 0 -- 可领金币
    o.NextGetRewardLeftTm = 0 -- 到下次可领金币剩余时间，单位秒
    o.NextGetRewardGold = 0 -- 到下次增加的可领取金币值（delta值），为0表示已长满
    o.CurLevel = 0 -- 当前等级，由充值点数决定
    o.ListGetGrowRewardRecord = {} -- 个人领取成长奖励记录，最多10条
    return o
end

function BGrowData:setData(data)
    self.GetRewardGold = data[1]
    self.CanGetRewardGold = data[2]
    self.NextGetRewardLeftTm = data[3]
    self.NextGetRewardGold = data[4]
    self.CurLevel = data[5]
    if (#data[6] > 0)
    then
        for i = 1, #data[6] do
            local temp = BGetGrowRecored:new(nil)
            temp:setData(data[6][i])
            table.insert(self.ListGetGrowRewardRecord, temp)
        end
    end
end

--成长奖励，个人领取记录
BGetGrowRecored = {}
function BGetGrowRecored:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Gold = 0
    o.Dt = nil
    return o
end

function BGetGrowRecored:setData(data)
    self.Gold = data[1] --领取金币
    self.Dt = data[2] --领取时间
end
	
