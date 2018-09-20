PreViewBase = {
	PreViewMgr = nil,
	GoUi = nil,
	ComUi = nil,
	Panel = nil,
	UILayer = nil,
	InitDepth = nil,
	ViewKey = nil
}

function  PreViewBase:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
    return o
end

function PreViewBase:onCreate()	
end

function PreViewBase:onDestroy()	
end

function PreViewBase:onUpdate(tm)	
end

function PreViewBase:onHandleEv(ev)	
end