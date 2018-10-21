-- Copyright (c) Cragon. All rights reserved.

---------------------------------------
LuaHelper = {}

---------------------------------------
function LuaHelper:new(o)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	if(self.Instance==nil)
	then
		self.Instance = o
		math.randomseed(os.time())
	end

	return self.Instance
end

---------------------------------------
function LuaHelper:SplitStr(full_str, separator)
	local start_index = 1
	local split_index = 1
	local split_array = {}
	while true do
		local find_lastindex = string.find(full_str, separator, start_index)
		if not find_lastindex
		then
			split_array[split_index] = string.sub(full_str, start_index, string.len(full_str))
			break
		end
		split_array[split_index] = string.sub(full_str, start_index, find_lastindex - 1)
		start_index = find_lastindex + string.len(separator)
		split_index = split_index + 1
	end
	return split_array
end

---------------------------------------
function LuaHelper:GetTableCount(t)
	local count = 0
	for k, v in pairs(t) do
		count = count + 1
	end
	return count
end

---------------------------------------
function LuaHelper:GetAndRemoveTableFirstEle(t)
	local key = nil
	local value = nil

	for k, v in pairs(t) do
		key = k
		value = v
		break
	end

	t[key] = nil

	return key,value
end

---------------------------------------
function LuaHelper:GetTableFirstEle(t)
	local key = nil
	local value = nil

	for k, v in pairs(t) do
		key = k
		value = v
		break
	end

	return key,value
end

---------------------------------------
function LuaHelper:TableContainsV(t,value)
	local have_v = false
	for k, v in pairs(t) do
		if(v == value)
		then
			have_v = true
			break
		end
	end

	return have_v
end

---------------------------------------
function LuaHelper:TableRemoveV(t,value)
	local key = nil
	for k, v in pairs(t) do
		if(v == value)
		then
			key = k
			break
		end
	end

	if(key ~= nil)
	then
		t[key] = nil
	end
end

---------------------------------------
function LuaHelper:CloneTableData(table_resource,table_target)
	if(table_resource ~= nil) then
		for k, v in pairs(table_resource) do
			table_target[k] = v
		end
	end
end

---------------------------------------
function LuaHelper:ReverseTable(table)
	local temp = {}
	for i = 1,#table do
		temp[i] = table[#table - i + 1]
	end
	return temp
end

---------------------------------------
function LuaHelper:GetUrlWithRandomNum(url)
	local n = math.random(10000)
	local new_url  = url..'?'..tostring(n)
	return new_url
end

---------------------------------------
function LuaHelper:GetRandomNum(num_max)
	local n = math.random(num_max)
	return n
end

---------------------------------------
function LuaHelper:DeleteAllTableEle(t)
	for k,v in pairs(t) do
		t[k] = nil
	end
end