-- Copyright (c) Cragon. All rights reserved.

---------------------------------------
WWWLoader = {
	Instance = nil,
	MainC = nil,	
	TableNeedLoadFile = {},	
	TableWWW = {},
	--TableWWWDone = {},
	FunctionLoadPro = nil,
	FunctionLoadOneFileDown = nil,
	FunctionLoadDown = nil,
	CurrentIndex = 0,
	TotalCount = 0,
	LoadDown = false
}

---------------------------------------
function WWWLoader:new(o)
	 o = o or {}  
    setmetatable(o,self)  
    self.__index = self	
    if(self.Instance==nil)
	then
		self.Instance = o		
	end
    return self.Instance
end

---------------------------------------
function WWWLoader:onCreate()
	self.MainC = MainC:new(nil)	
end

---------------------------------------
function WWWLoader:StartDownload(table_need_loadfile,function_loadpro,function_loadonefile_down,function_loaddown)
	self.FunctionLoadPro = function_loadpro
	self.FunctionLoadOneFileDown = function_loadonefile_down
	self.FunctionLoadDown = function_loaddown
	self.MainC.LuaHelper:CloneTableData(table_need_loadfile,self.TableNeedLoadFile)
	self.TotalCount = self.MainC.LuaHelper:GetTableCount(self.TableNeedLoadFile)
	self.LoadDown = false
end

---------------------------------------
function WWWLoader:onUpdate(tm)
	if(self.LoadDown == true)
	then
		return
	end

	local need_loadfilecount = self.MainC.LuaHelper:GetTableCount(self.TableNeedLoadFile)
	if(need_loadfilecount > 0)
	then			
		local loadingfile_wwwcount = self.MainC.LuaHelper:GetTableCount(self.TableWWW)		
		if(loadingfile_wwwcount < 5)
		then
			local need_load_file_key,need_load_file_value = self.MainC.LuaHelper:GetAndRemoveTableFirstEle(self.TableNeedLoadFile)			
			need_loadfilecount = need_loadfilecount - 1
			need_load_file_value = CS.Casinos.CasinoHelper.FormalUrlWithRandomVersion(need_load_file_value)
			local www = CS.UnityEngine.WWW(need_load_file_value)
			self.TableWWW[need_load_file_key] = www
			self.CurrentIndex = self.TotalCount - need_loadfilecount
			if(self.FunctionLoadPro ~= nil)
			then
				self.FunctionLoadPro(self.CurrentIndex,self.TotalCount)				
			end
		end
	else
		local loadingfile_wwwcount = self.MainC.LuaHelper:GetTableCount(self.TableWWW)
		if(loadingfile_wwwcount == 0 and need_loadfilecount == 0)
		then			
			self.LoadDown = true
			if(self.FunctionLoadDown ~= nil)
			then
				self.FunctionLoadDown()
				self.FunctionLoadDown = nil
			end
			self.FunctionLoadPro = nil
			self.FunctionLoadOneFileDown = nil
		end
	end

	for key,value in pairs(self.TableWWW) do		
		if(value.isDone)
		then
			if ((value.error == nil or (value.error ~= nil and string.len(value.error) <= 0)))
			then				
				if(self.FunctionLoadOneFileDown ~= nil)
				then
					self.FunctionLoadOneFileDown(key,value.bytes)
				end				
				self.TableWWW[key] = nil
			else
				CS.Casinos.UiHelperCasinos.UiShowPreLoading("WWWLoader Error "..value.error.."  url = "..value.url,0)	
				return
			end
		end		
	end
end

---------------------------------------
function WWWLoader:onRelease()
	print('WWWLoader_Release')	
end