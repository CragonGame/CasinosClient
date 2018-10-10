-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
LanEn = LanBase:new(nil)

---------------------------------------
function LanEn:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    if (self.Instance == nil)
    then
        self.Instance = o
        self.MapLan = {}
        self.StrFormatTemp = {}
        self.mGoldThousand = 1000
        self.mGoldMillion = 1000000
        self.mGoldFormatLength = 3
    end

    return self.Instance
end

---------------------------------------
function LanEn:getValue(key)
    return self.MapLan[key]
end

---------------------------------------
function LanEn:parseLanKeyValue(tb_datamgr)
    local map_data = tb_datamgr:GetMapData("LanEn")
    if (map_data == nil)
    then
        return
    end
    for i, v in pairs(map_data) do
        self.MapLan[v.LanguageKey] = v.LanguageValue
    end
end

---------------------------------------
function LanEn:getLanPackageName()
    return "LanEn"
end

---------------------------------------
function LanEn:getGoldShowStr(gold, show_short_way, precision_length)
    local show_str = ""

    if (show_short_way == false)
    then
        show_str = tostring(gold)
        local l = string.len(show_str)
        local multiple, remainder = math.modf(l / self.mGoldFormatLength)
        remainder = remainder * self.mGoldFormatLength
        for i = multiple, 1, -1 do
            if (remainder == 0 and i == multiple)
            then
            else
                local index = 0
                if (remainder == 0)
                then
                    index = i * self.mGoldFormatLength + remainder
                else
                    index = i * self.mGoldFormatLength + remainder - self.mGoldFormatLength
                end

                show_str = CS.Casinos.LuaHelper.insertToStr(show_str, index, " ")
            end
        end
    else
        local gold_thousand_multiple, gold_thousand_remainder = math.modf(gold / self.mGoldThousand)
        self.StrFormatTemp = {}
        table.insert(self.StrFormatTemp, "0.")
        for i = 1, precision_length do
            table.insert(self.StrFormatTemp, "0")
        end

        if (gold_thousand_multiple > 0 and gold_thousand_multiple < 1000)
        then
            local g = gold / self.mGoldThousand
            show_str = CS.Casinos.LuaHelper.formatNumToStr(g, table.concat(self.StrFormatTemp))
            self.StrFormatTemp = {}
            table.insert(self.StrFormatTemp, show_str)
            table.insert(self.StrFormatTemp, "K")
        elseif (gold_thousand_multiple >= 1000)
        then
            local gold_million_multiple, gold_million_remainder = math.modf(gold / self.mGoldMillion)

            if (gold_million_multiple < 1000)
            then
                local g = gold / self.mGoldMillion
                show_str = CS.Casinos.LuaHelper.formatNumToStr(g, table.concat(self.StrFormatTemp))
                self.StrFormatTemp = {}
                table.insert(self.StrFormatTemp, show_str)
                table.insert(self.StrFormatTemp, "M")
            else
                local g = gold / (self.mGoldMillion * self.mGoldThousand)
                show_str = CS.Casinos.LuaHelper.formatNumToStr(g, table.concat(self.StrFormatTemp))
                self.StrFormatTemp = {}
                table.insert(self.StrFormatTemp, show_str)
                table.insert(self.StrFormatTemp, "B")
            end
        else
            show_str = tostring(gold)
            self.StrFormatTemp = {}
            table.insert(self.StrFormatTemp, show_str)
        end

        show_str = table.concat(self.StrFormatTemp)
        self.StrFormatTemp = {}
    end

    return show_str
end

---------------------------------------
function LanEn:getGoldShowStr2(gold, show_short_way, precision_length)
    local show_str = ""

    if (show_short_way == false)
    then
        show_str = tostring(gold)
        local multiple, remainder = math.modf(string.len(show_str) / self.mGoldFormatLength)
        remainder = remainder * self.mGoldFormatLength
        for i = multiple, 1, -1 do
            if (remainder == 0 and i == multiple)
            then
            else
                local index = 0
                if (remainder == 0)
                then
                    index = i * self.mGoldFormatLength + remainder
                else
                    index = i * self.mGoldFormatLength + remainder - self.mGoldFormatLength
                end

                show_str = CS.Casinos.LuaHelper.insertToStr(show_str, index, " ")
            end
        end
    else
        local gold_thousand_multiple, gold_thousand_remainder = math.modf(gold / self.mGoldThousand)
        self.StrFormatTemp = {}
        table.insert(self.StrFormatTemp, "0.")
        for i = 1, precision_length do
            table.insert(self.StrFormatTemp, "0")
        end

        if (gold_thousand_multiple > 0 and gold_thousand_multiple < 1000)
        then
            local g = gold / self.mGoldThousand
            show_str = CS.Casinos.LuaHelper.formatNumToStr(g, table.concat(self.StrFormatTemp))
            self.StrFormatTemp = {}
            table.insert(self.StrFormatTemp, show_str)
            table.insert(self.StrFormatTemp, "K")
        elseif (gold_thousand_multiple >= 1000)
        then
            local g = gold / self.mGoldMillion
            show_str = CS.Casinos.LuaHelper.formatNumToStr(g, table.concat(self.StrFormatTemp))
            self.StrFormatTemp = {}
            table.insert(self.StrFormatTemp, show_str)
            table.insert(self.StrFormatTemp, "M")
        else
            show_str = tostring(gold)
            self.StrFormatTemp = {}
            table.insert(self.StrFormatTemp, show_str)
        end

        show_str = table.concat(self.StrFormatTemp)
        self.StrFormatTemp = {}
    end

    return show_str
end