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
    setmetatable(o, self)
    self.__index = self
    if (self.Instance == nil)
    then
        self.Instance = o
    end
    CS.Casinos.CasinosContext.Instance.TbDataMgrLua = self
    return self.Instance
end

---------------------------------------
function TbDataMgr:Setup(list_db_filename)
    if (CS.Casinos.CasinosContext.Instance.IsSqliteUnity)
    then
        self.Sqlite = CS.GameCloud.Unity.Common.SqliteUnity()
    else
        self.Sqlite = CS.GameCloud.Unity.Common.SqliteWin()
    end

    for i, v in pairs(list_db_filename) do
        local open_db = self.Sqlite:openDb(v)
        if (open_db == false)
        then
            print("TbDataMgr:Setup() failed! Can not Open File! db_filename=" .. v)
            return
        end

        local list_tablename = self:_loadAllTableName()
        for i = 0, list_tablename.Count - 1 do
            local tb_name = list_tablename[i]
            local list_tb_data = self:_loadTable(tb_name)
            self:ParseTableAllData(tb_name, list_tb_data)
        end
        self.Sqlite:closeDb()
    end
end

---------------------------------------
function TbDataMgr:RegTbDataFac(tb_name, fac)
    self.MapTbDataFac[tb_name] = fac
end

---------------------------------------
function TbDataMgr:ParseTableAllData(table_name, list_t)
    local map_data = {}
    self.MapData[table_name] = map_data
    local fac = self.MapTbDataFac[table_name]
    if (fac == nil)
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
function TbDataMgr:GetData(table_name, id)
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
    local str_query_select = "SELECT * FROM " .. table_name .. ";"
    local list_data = self.Sqlite:getTableData(str_query_select)
    if (list_data.Count <= 0)
    then
        return nil
    end
    return list_data
end