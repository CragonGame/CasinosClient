EventBase = {
	EventName = nil
}

function EventBase:new(o)
	 o = o or {}  
    setmetatable(o,self)  
    self.__index = self	
    return o
end

function EventBase:reset()
	
end