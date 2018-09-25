-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
TbDataMgr = {
	Instance = nil,
	MapTbDataFac = {},
	MapData = {},	
	Sqlite = nil,
	FileStream = nil,	
	QueLoadTbName = {},
	FinishedCallBack = nil,
}

---------------------------------------
function TbDataMgr:new(o)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	if(self.Instance == nil)
	then
		self.Instance = o
	end

	return self.Instance
end

---------------------------------------
--function TbDataMgr:onCreate()
--	CS.Casinos.CasinosContext.Instance.TbDataMgrLua = self
--end

---------------------------------------
function TbDataMgr:Setup(db_filename, finished_callback)                    
	self.FinishedCallBack = finished_callback

	if(CS.Casinos.CasinosContext.Instance.IsSqliteUnity)	
	then
		self.Sqlite = CS.GameCloud.Unity.Common.SqliteUnity()
	else
		self.Sqlite = CS.GameCloud.Unity.Common.SqliteWin()
	end
	
	local open_db = self.Sqlite:openDb(db_filename)
	if (open_db == false)
	then
		print("TbDataMgr__".. db_filename)
	    print("EbDataMgr.setup() failed! Can not Open File! db_filename=" .. db_filename)
	    return
	end

	local list_tablename = self:_loadAllTableName()
	for i = 0, list_tablename.Count - 1 do  		
		self.QueLoadTbName[i] = list_tablename[i]
	end 	
end

---------------------------------------
function TbDataMgr:Close()
end

---------------------------------------
function TbDataMgr:onUpdate(tm)
	 local tb_count = LuaHelper:GetTableCount(self.QueLoadTbName)	 
     if (tb_count > 0)
     then
         local tb_key,tb_value = LuaHelper:GetAndRemoveTableFirstEle(self.QueLoadTbName)
		 local list_tb_data = self:_loadTable(tb_value)               
		 self:ParseTableAllData(tb_value,list_tb_data)		 
     else        
         if (self.FinishedCallBack ~= nil)
         then
             self.Sqlite:closeDb()
             local call_back = self.FinishedCallBack
             self.FinishedCallBack = nil
             call_back()
         end
     end
end

---------------------------------------
function TbDataMgr:RegTbDataFac(tb_name,fac)
	self.MapTbDataFac[tb_name] = fac
end

---------------------------------------
function TbDataMgr:ParseTableAllData(table_name,list_t)            
     local map_data = {}
     self.MapData[table_name] = map_data
	 local fac = self.MapTbDataFac[table_name]     
	 if(fac == nil)
	 then
		return
	 end

	 for i = 0, list_t.Count - 1 do  		
		local data = list_t[i]		
		local tb_data = fac:createTbData(data.Id)         
        tb_data:load(data.ListDataInfo)
        map_data[tb_data.Id] = tb_data
	 end            
end

---------------------------------------
function TbDataMgr:GetData(table_name,id)            
    local map_data = self.MapData[table_name]
    if (map_data == nil)
    then
        return nil
    end
	local tb_data = map_data[id]                        
    return tb_data
end

---------------------------------------
function TbDataMgr:GetMapData(table_name)
    local map_data = self.MapData[table_name]
    return map_data
end

---------------------------------------
function TbDataMgr:_loadAllTableName()                    
    local str_query = "SELECT * FROM sqlite_master;"
	local list_tablename = self.Sqlite:getAllTableName(str_query)
    return list_tablename
end

---------------------------------------
function TbDataMgr:_loadTable(table_name)
    local str_query_select = "SELECT * FROM "..table_name ..";"	
	local list_data = self.Sqlite:getTableData(str_query_select)
        if(list_data.Count <= 0)
        then
            return nil
        end             
	return list_data
end