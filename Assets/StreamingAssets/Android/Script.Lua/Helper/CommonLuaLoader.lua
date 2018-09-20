-- Copyright (c) Cragon. All rights reserved.

CommonLuaLoader = {
	Instance = nil,
	DataVersion = nil,
	DataVersionKey = nil,
	CommonLuaFileListTxtName = nil,
	DataFileURLRoot = nil,
	FunctionLoadDown = nil,
	DataFileListPathLocal = nil,	
	MainC = nil,
	WWWCommonLuaDataList = nil,
	RemoteDataFileListBytes = nil,
	TableRemoteData = {},
	TableLocalData = {},
	TableNeedLoadFileInfo = {},
	RemoteDataFileList = nil	
}

function CommonLuaLoader:new(o)
	 o = o or {}  
    setmetatable(o,self)  
    self.__index = self	
    
    if(self.Instance==nil)
	then
		self.Instance = o
	end

    return self.Instance
end

function CommonLuaLoader:onCreate(data_version,commonlua_filelist_name,commonlua_root_url,local_commonlua_versionkey,function_loaddown)	
	self.DataVersion = data_version
	self.CommonLuaFileListTxtName = commonlua_filelist_name
	self.DataFileURLRoot = commonlua_root_url
	self.DataVersionKey = local_commonlua_versionkey
	self.FunctionLoadDown = function_loaddown
	self.MainC = MainC:new(nil)	

	local path_mgr = CS.Casinos.CasinosContext.Instance.PathMgr
	self.DataFileListPathLocal = path_mgr:combinePersistentDataPath('CommonLuaFileList.txt',true)	
	local filelist_info_local = CS.Casinos.LuaHelper.readAllText(self.DataFileListPathLocal)		
	if(filelist_info_local~=nil)
	then		
		self:ParseDataFileList(filelist_info_local, true)
	end	

	local datafilelist_remoteurl = self:CombineRemoteDataPath(self.CommonLuaFileListTxtName)	
	datafilelist_remoteurl = CS.Casinos.CasinoHelper.FormalUrlWithRandomVersion(datafilelist_remoteurl)
	self.WWWCommonLuaDataList = CS.UnityEngine.WWW(datafilelist_remoteurl)		
end

function CommonLuaLoader:onUpdate(tm)
	if(self.WWWCommonLuaDataList == nil)
	then        
	return
	end
		
	if (self.WWWCommonLuaDataList.isDone == true)
	then		
        if ((self.WWWCommonLuaDataList.error == nil or (self.WWWCommonLuaDataList.error ~= nil and string.len(self.WWWCommonLuaDataList.error) <= 0)))
		then			
			self.RemoteDataFileListBytes = self.WWWCommonLuaDataList.bytes			
			self:ParseDataFileList(self.WWWCommonLuaDataList.text,false)						
						
			self.TableNeedLoadFileInfo = self:_getNeedLoadAssetAndDeleteOldAsset()			
			local need_load_count = self.MainC.LuaHelper:GetTableCount(self.TableNeedLoadFileInfo)
			if(need_load_count > 0)
			then
				self.MainC.WWWLoader:StartDownload(self.TableNeedLoadFileInfo,self.LoadPro,self.LoadOneFileDown,self.LoadDown)
			else
				self.LoadDown()
			end
            self.WWWCommonLuaDataList = nil	
		else
			CS.Casinos.UiHelperCasinos.UiShowPreLoading("WWWCommonLuaDataList Error "..self.WWWCommonLuaDataList.error.."  url = "..self.WWWCommonLuaDataList.url,0)	
		end			
	end
end

function CommonLuaLoader:onRelease()
	print('WWWLoader_Release')
end

function CommonLuaLoader:ParseDataFileList(data_filelist_text,is_local)	
	local split_array = self.MainC.LuaHelper:SplitStr(data_filelist_text,'\n')
	for key,value in pairs(split_array) do
		 if (string.len(value) > 0)
		 then
			 local info_array = self.MainC.LuaHelper:SplitStr(value,' ')
			 local info_array_count = self.MainC.LuaHelper:GetTableCount(info_array)
			 if (is_local == true)
			 then
				 if (info_array_count == 2)
				 then
					self.TableLocalData[info_array[1]] = info_array[2]
				 end
			 else
				 if (info_array_count == 2)
				 then
					self.TableRemoteData[info_array[1]] = info_array[2]
				 end
			 end			
		 end           
	end        
end

function CommonLuaLoader:CombineRemoteDataPath(path)
	return self.DataFileURLRoot..path	
end

function CommonLuaLoader.LoadPro(current_index,total)
	local pro = current_index / total	
	local tips = "正在更新数据，请稍候"
	if(CS.Casinos.CasinosContext.Instance.UseLan == true)
	then
		local lan = CS.Casinos.CasinosContext.Instance.CurrentLan
		if(lan == "English")
		then
			tips = "Updating data, please wait"
		else
			if(lan == "Chinese" or lan == "ChineseSimplified")
			then
				tips = "正在更新数据，请稍候"
			end
		end
	end
	CS.Casinos.UiHelperCasinos.UiShowPreLoading(tips,pro * 100)	
end

function CommonLuaLoader.LoadOneFileDown(key,file_bytes)
	-- 写文件				
	local path_mgr = CS.Casinos.CasinosContext.Instance.PathMgr
	local local_file_path =	path_mgr:combinePersistentDataPath(key,true)
	--print("local_file_path     " .. local_file_path)
	CS.Casinos.LuaHelper.writeFile(file_bytes,local_file_path)
end

function CommonLuaLoader.LoadDown()	
	local loader = CommonLuaLoader:new(nil)
	local path_mgr = CS.Casinos.CasinosContext.Instance.PathMgr
	local local_file_path =	path_mgr:combinePersistentDataPath("CommonLuaFileList.txt",true)	
	CS.Casinos.LuaHelper.writeFile(loader.RemoteDataFileListBytes,local_file_path)
	CS.UnityEngine.PlayerPrefs.SetString(loader.DataVersionKey,loader.DataVersion)
	if(loader.FunctionLoadDown ~= nil)
	then
		loader.FunctionLoadDown()
	end
end

function CommonLuaLoader:_getNeedLoadAssetAndDeleteOldAsset()			
	local needloadasset_array = {} 	
	local same_asset_array = {}

	for key,value in pairs(self.TableRemoteData) do
		local datamd5_local = self.TableLocalData[key]
		if(datamd5_local ~= nil and string.len(datamd5_local) > 0)       
		then
			 if (datamd5_local == value)
			 then
				same_asset_array[key] = value
			 else		
				local data_remoteurl = self:CombineRemoteDataPath(key)
				needloadasset_array[key] = data_remoteurl				
			 end                
		else						
			local data_remoteurl = self:CombineRemoteDataPath(key)
			--print("data_remoteurl    "..data_remoteurl)
			needloadasset_array[key]= data_remoteurl			
		end     
	end      

	for key,value in pairs(self.TableLocalData) do
		 local same_asset = same_asset_array[key]
		 if(same_asset==nil)	
		 then
			local need_del_file = CS.Casinos.CasinosContext.Instance.PathMgr:combinePersistentDataPath(key,true)
			os.remove(need_del_file)						
		 end			
	end    

	return needloadasset_array
end