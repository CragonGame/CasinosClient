-- Copyright(c) Cragon. All rights reserved.

BActivity = {}

function BActivity:new(o)
	o = o or {}
    setmetatable(o, self)
    self.__index = self
	o.Id = nil --Id
    o.Enable = false
    o.Title = nil --标题
    o.Context = nil --内容
    o.IsHot = false --是否是热点
end

function BActivity:setData(data)
	self.Id = data[1]
    self.Enable = data[2]
    self.Title = data[3] 
    self.Context = data[4] 
    self.IsHot = data[5]
end