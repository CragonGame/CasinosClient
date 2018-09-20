-- Copyright (c) Cragon. All rights reserved.

FileLoader = {
	Instance = nil,
	MainC = nil,
	FileWWW = nil,
	FunctionLoadPro = nil,
	FunctionLoadDown = nil,
	LoadDown = false
}

function FileLoader:new(o)
	 o = o or {}  
    setmetatable(o,self)  
    self.__index = self	
    if(self.Instance==nil)
	then
		self.Instance = o		
	end
    return self.Instance
end

function FileLoader:onCreate()
	self.MainC = MainC:new(nil)	
end

function FileLoader:StartDownload(need_loadfile,function_loadpro,function_loaddown)
	self.FileWWW = CS.UnityEngine.WWW(need_loadfile)
	self.FunctionLoadPro = function_loadpro
	self.FunctionLoadDown = function_loaddown
	self.LoadDown = false
end

function FileLoader:onUpdate(tm)
	if(self.LoadDown == true)
	then
		return
	end

	if(self.FileWWW.isDone)
	then
		if ((self.FileWWW.error == nil or (self.FileWWW.error ~= nil and string.len(self.FileWWW.error) <= 0)))
		then
			self.LoadDown = true
			if(self.FunctionLoadDown ~= nil)
			then
				self.FunctionLoadDown(self.FileWWW.bytes)
				self.FunctionLoadDown = nil
			end
			self.FileWWW = nil
		else
			CS.Casinos.UiHelperCasinos.UiShowPreLoading("FileLoader Error "..self.FileWWW.error.."  url = "..self.FileWWW.url,0)
			return
		end
	else
		if(self.FunctionLoadPro ~= nil)
		then
			self.FunctionLoadPro(self.FileWWW.progress)
		end
	end
end

function FileLoader:onRelease()
	print('FileLoader_Release')	
end