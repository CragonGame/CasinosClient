-- Copyright(c) Cragon. All rights reserved.

ModelReaderBase = {	}

function ModelReaderBase:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self	
    return o
end