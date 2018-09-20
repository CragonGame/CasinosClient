-- Copyright (c) Cragon. All rights reserved.

UiChipShowHelper = {}

function UiChipShowHelper:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
    if(self.Instance == nil)
    then
        self.Instance = o
        self.StrFormatTemp = {}
        self.mGoldThousand = 1000
        self.mGoldMillion = 10000
        self.mGoldFormatLength = 3
    end

    return self.Instance
end

function UiChipShowHelper:getGoldShowStr(gold,lan_base,show_short_way1,precision_length1)
    local show_str = ""
    local show_short_way = true
    if(show_short_way1 ~= nil)
    then
        show_short_way = show_short_way1
    end

    local precision_length = 2
    if(precision_length1 ~= nil)
    then
        precision_length = precision_length1
    end

    if (lan_base == nil)
    then
        if (show_short_way == false)
        then
            show_str = tostring(gold)
            local l = string.len(show_str)
            local multiple,remainder = math.modf(l/self.mGoldFormatLength)
            remainder = remainder*self.mGoldFormatLength
            for i = multiple, 1,-1 do
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

                    show_str = CS.Casinos.LuaHelper.insertToStr(show_str,index," ")
                end
            end
        else
            local gold_thousand_multiple,gold_thousand_remainder = math.modf(gold/self.mGoldThousand)
            self.StrFormatTemp = {}
            self.StrFormatTemp[0] = "0."
            for i = 1, precision_length do
                self.StrFormatTemp[i] = "0"
            end

            if (gold_thousand_multiple > 0 and gold_thousand_multiple < 10)
            then
                local g = gold / self.mGoldThousand
                show_str = CS.Casinos.LuaHelper.formatNumToStr(g,table.concat(self.StrFormatTemp))
                self.StrFormatTemp = {}
                self.StrFormatTemp[0] = show_str
                self.StrFormatTemp[1] = "千"
            elseif (gold_thousand_multiple >= 10)
            then
                local gold_million_multiple,gold_million_remainder = math.modf(gold/self.mGoldMillion)

                if (gold_million_multiple < 9999)
                then
                    local g = gold / self.mGoldMillion
                    show_str = CS.Casinos.LuaHelper.formatNumToStr(g,table.concat(self.StrFormatTemp))
                    self.StrFormatTemp = {}
                    self.StrFormatTemp[0] = show_str
                    self.StrFormatTemp[1] = "万"
                else
                    local g = gold / (self.mGoldMillion * self.mGoldMillion)
                    show_str = CS.Casinos.LuaHelper.formatNumToStr(g,table.concat(self.StrFormatTemp))
                    self.StrFormatTemp = {}
                    self.StrFormatTemp[0] = show_str
                    self.StrFormatTemp[1] = "亿"
                end
            else
                show_str = tostring(gold)
                self.StrFormatTemp = {}
                self.StrFormatTemp[0]= show_str
            end

            show_str = table.concat(self.StrFormatTemp)
            self.StrFormatTemp = {}
        end
    else
        show_str = lan_base:getGoldShowStr(gold, show_short_way, precision_length)
    end

    return show_str
end

function UiChipShowHelper:getGoldShowStr2(gold,lan_base,show_short_way1,precision_length1)
    local show_str = ""

    local show_short_way = true
    if(show_short_way1 ~= nil)
    then
        show_short_way = show_short_way1
    end

    local precision_length = 2
    if(precision_length1 ~= nil)
    then
        precision_length = precision_length1
    end

    if (lan_base == nil)
    then
        if (show_short_way == false)
        then
            show_str = tostring(gold)
            local multiple,remainder = math.modf(string.len(show_str)/self.mGoldFormatLength)
            remainder = remainder*self.mGoldFormatLength
            for i = multiple, 1,-1 do
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

                    show_str = CS.Casinos.LuaHelper.insertToStr(show_str,index," ")
                end
            end
        else
            local gold_thousand_multiple,gold_thousand_remainder = math.modf(gold /self.mGoldThousand)
            self.StrFormatTemp = {}
            self.StrFormatTemp[0] = "0."
            for i = 1, precision_length do
                self.StrFormatTemp[i] = "0"
            end

            if (gold_thousand_multiple > 0 and gold_thousand_multiple < 10)
            then
                local g = gold / self.mGoldThousand
                show_str = CS.Casinos.LuaHelper.formatNumToStr(g,table.concat(self.StrFormatTemp))
                self.StrFormatTemp = {}
                self.StrFormatTemp[0] =show_str
                self.StrFormatTemp[1] ="千"
            elseif (gold_thousand_multiple >= 10)
            then
                local g = gold / self.mGoldMillion
                show_str = CS.Casinos.LuaHelper.formatNumToStr(g,table.concat(self.StrFormatTemp))
                self.StrFormatTemp = {}
                self.StrFormatTemp[0] =show_str
                self.StrFormatTemp[1] ="万"
            else
                show_str = tostring(gold)
                self.StrFormatTemp = {}
                self.StrFormatTemp[0] =show_str
            end

            show_str = table.concat(self.StrFormatTemp)
            self.StrFormatTemp = {}
        end
    else
        show_str = lan_base:getGoldShowStr2(gold, show_short_way, precision_length)
    end

    return show_str
end

function UiChipShowHelper:getGoldShowStr3(gold)
    if(gold<10000)
    then
        return tostring(gold)
    else
        local temp = nil
        if gold % 10000 == 0 then
            temp = tostring(math.ceil(gold/10000))
        else
            temp = string.format("%0.1f",gold/10000)
        end
        return temp .. "万"
    end

end

function UiChipShowHelper:getValideGold(gold)
    local valide_gold = math.ceil(gold)
    self.StrFormatTemp = {}
    local str_gold = tostring(valide_gold)
    if (string.len(str_gold) >= 3)
    then
        local new_str_gold_less = ""
        table.insert(self.StrFormatTemp,"1")
        for i = 1, string.len(str_gold) - 2 do
            table.insert(self.StrFormatTemp,"0")
        end

        new_str_gold_less = table.concat(self.StrFormatTemp)

        local new_gold_less = CS.System.Int64.Parse(new_str_gold_less)

        local multiple,l =  math.modf(valide_gold / new_gold_less)
        valide_gold = multiple * new_gold_less
    end
    self.StrFormatTemp = {}

    return valide_gold
end
