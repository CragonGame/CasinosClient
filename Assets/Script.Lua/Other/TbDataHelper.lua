-- Copyright (c) Cragon. All rights reserved.

---------------------------------------
TbDataHelper = {}

---------------------------------------
function TbDataHelper:new(o, tb_datamgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    if (self.Instance == nil) then
        self.Instance = o
        self.MapTbDataCommon = {}
        local t_c = tb_datamgr:GetMapData("Common")
        for i, v in pairs(t_c) do
            self.MapTbDataCommon[v.Key] = v.Value
        end

        self.MapTbDataDesktopFastBet = {}
        local t_fasts = tb_datamgr:GetMapData("DesktopFastBet")
        for i, v in pairs(t_fasts) do
            local t = {}
            table.insert(t, v.DesktopId)
            table.insert(t, v.DesktopType)
            table.insert(t, tostring(v.IsPreflop))
            local key = table.concat(t)
            local t_fast = {}
            local f_1 = DesktopFastBetInfo:new()
            f_1.DesktopFastBetType = v.Type1
            f_1.DesktopFastBetValue = v.Value1
            table.insert(t_fast, f_1)
            local f_2 = DesktopFastBetInfo:new()
            f_2.DesktopFastBetType = v.Type2
            f_2.DesktopFastBetValue = v.Value2
            table.insert(t_fast, f_2)
            local f_3 = DesktopFastBetInfo:new()
            f_3.DesktopFastBetType = v.Type3
            f_3.DesktopFastBetValue = v.Value3
            table.insert(t_fast, f_3)
            local f_4 = DesktopFastBetInfo:new()
            f_4.DesktopFastBetType = v.Type4
            f_4.DesktopFastBetValue = v.Value4
            table.insert(t_fast, f_4)
            local f_5 = DesktopFastBetInfo:new()
            f_5.DesktopFastBetType = v.Type5
            f_5.DesktopFastBetValue = v.Value5
            table.insert(t_fast, f_5)
            local f_6 = DesktopFastBetInfo:new()
            f_6.DesktopFastBetType = v.Type6
            f_6.DesktopFastBetValue = v.Value6
            table.insert(t_fast, f_6)
            local f_7 = DesktopFastBetInfo:new()
            f_7.DesktopFastBetType = v.Type7
            f_7.DesktopFastBetValue = v.Value7
            table.insert(t_fast, f_7)
            local f_8 = DesktopFastBetInfo:new()
            f_8.DesktopFastBetType = v.Type8
            f_8.DesktopFastBetValue = v.Value8
            table.insert(t_fast, f_8)
            self.MapTbDataDesktopFastBet[key] = t_fast
        end

        self.MapTbDataTexasRaiseBlinds = {}
        local t_blinds = tb_datamgr:GetMapData("TexasRaiseBlinds")
        for i, v in pairs(t_blinds) do
            local t_b = self.MapTbDataTexasRaiseBlinds[v.BlindType]
            if t_b == nil then
                t_b = {}
            end
            t_b[v.BlindId] = v
            self.MapTbDataTexasRaiseBlinds[v.BlindType] = t_b
        end

		self.MapTbDataTexasSnowBallRewardInfo = {}
		local t_info = tb_datamgr:GetMapData("TexasSnowBallRewardInfo")
		for i = 1,75 do
			local temp = {}
			for k = 1,#t_info do
				if(t_info[k].TableId == i)
				then
					table.insert(temp,t_info[k])
				end
			end
			table.sort(temp,
				function(a,b)
					return a.StartRank < b.StartRank
				end
			)
			-- 测试函数
			--[[local total = 0
			for a = 1,#temp do
				if(temp[a].EndRank ~= 0)
				then
					total = total + temp[a].RewardRatio * (temp[a].EndRank - temp[a].StartRank + 1)
				else
					total = total + temp[a].RewardRatio
				end
			end
			print(total .. "................." .. i)]]
			self.MapTbDataTexasSnowBallRewardInfo[i] = temp
		end
    end

    return self.Instance
end

---------------------------------------
function TbDataHelper:GetCommonValue(common_key)
    return self.MapTbDataCommon[common_key]
end

---------------------------------------
function TbDataHelper:GetDesktopFastBet(key)
    -- key DesktopId  DesktopType  IsPreflop
    local t = self.MapTbDataDesktopFastBet[key]
    local t_v = nil
    if t ~= nil then
        t_v = {}
        for i, v in pairs(t) do
            t_v[i] = v
        end
    end
    return t_v
end

---------------------------------------
function TbDataHelper:GetAllTexasRaiseBlindsByType(blind_type)
    return self.MapTbDataTexasRaiseBlinds[blind_type]
end

---------------------------------------
function TbDataHelper:GetTexasRaiseBlindByTypeAndId(blind_type,id)
    local t_b = self.MapTbDataTexasRaiseBlinds[blind_type]
    local tb_blinds = nil
    if t_b ~= nil then
        tb_blinds = t_b[id]
    end

    return tb_blinds
end

---------------------------------------
function TbDataHelper:GetTexasSnowBallRewradInfoByTableId(table_id)
	return self.MapTbDataTexasSnowBallRewardInfo[table_id]
end