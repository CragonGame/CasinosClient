Prop = {}

function Prop:new(v)
	o = {}  
    setmetatable(o,self)  
    self.__index = self
	o.Value = v
	o.OnChanged = nil

    return o
end

function Prop:get()
	return self.Value
end

function Prop:set(value)
	self.Value = value
	if(self.OnChanged ~= nil)
	then
		self.OnChanged()
	end
end