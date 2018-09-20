TestEvent = EventBase:new(nil)

function TestEvent:new(o)
    o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	self.EventName = "TestEvent"
	self.TestValue = 2
	self.TestContent = "TestContent can not be chinese"
	print("TestEvent__²âÊÔÊÂ¼ş__new__self.TestValue__"..tostring(self.TestValue)..
		"__self.TestContent__"..self.TestContent.."__self.EventName__"..self.EventName)
    return o
end

function TestEvent:reset()
	self.TestValue = nil
	self.TestContent = nil
end