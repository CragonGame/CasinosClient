-- Copyright(c) Cragon. All rights reserved.
require('TbDataBase')
require('TbDataActorLevel')
require('TbDataCfigTexasDesktopH')
require('TbDataCfigTexasDesktopHGoldPercent')
require('TbDataCfigTexasDesktopHSysBank')
require('TbDataCommon')
require('TbDataConfigTexasDesktop')
require('TbDataDailyReward')
require('TbDataDesktopFastBet')
require('TbDataDesktopHBetOperateTexas')
require('TbDataDesktopHBetPotTexas')
require('TbDataDesktopHBetReward')
require('TbDataDesktopHInfoTexas')
require('TbDataDesktopInfoTexas')
require('TbDataExpression')
require('TbDataHintsInfoTexas')
require('TbDataItem')
require('TbDataItemType')
require('TbDataLanEn')
require('TbDataLans')
require('TbDataLanZh')
require('TbDataLotteryTicket')
require('TbDataLotteryTicketBetOperate')
require('TbDataLotteryTicketGoldPercent')
require('TbDataOnlineReward')
require('TbDataPresetMsg')
require('TbDataTexasRaiseBlinds')
require('TbDataTexasSnowBallRewardInfo')
require('TbDataTexasSnowBallRewardPlayerNum')
require('TbDataUnitBilling')
require('TbDataUnitConsume')
require('TbDataUnitGiftNormal')
require('TbDataUnitGiftTmp')
require('TbDataUnitGoldPackage')
require('TbDataUnitGoodsVoucher')
require('TbDataUnitMagicExpression')
require('TbDataUnitRedEnvelopes')
require('TbDataVIPLevel')

---------------------------------------
TbDataMgr = {
    Instance = nil,
    MapTbDataFac = {},
    MapData = {},
    Sqlite = nil,
    FileStream = nil,
    QueLoadTbName = {},
    FinishedCallBack = nil,
    CasinosContext = CS.Casinos.CasinosContext.Instance,
    Context = Context,
}

---------------------------------------
function TbDataMgr:Setup()
    self.CasinosContext.TbDataMgrLua = self
    if (self.CasinosContext.IsSqliteUnity) then
        self.Sqlite = CS.GameCloud.Unity.Common.SqliteUnity()
    else
        self.Sqlite = CS.GameCloud.Unity.Common.SqliteWin()
    end

    self:RegTbDataFac("ActorLevel", TbDataFactoryActorLevel:new(nil))
    self:RegTbDataFac("CfigTexasDesktopH", TbDataFactoryCfigTexasDesktopH:new(nil))
    self:RegTbDataFac("CfigTexasDesktopHGoldPercent", TbDataFactoryCfigTexasDesktopHGoldPercent:new(nil))
    self:RegTbDataFac("CfigTexasDesktopHSysBank", TbDataFactoryCfigTexasDesktopHSysBank:new(nil))
    self:RegTbDataFac("Common", TbDataFactoryCommon:new(nil))
    self:RegTbDataFac("ConfigTexasDesktop", TbDataFactoryConfigTexasDesktop:new(nil))
    self:RegTbDataFac("DailyReward", TbDataFactoryDailyReward:new(nil))
    self:RegTbDataFac("DesktopFastBet", TbDataFactoryDesktopFastBet:new(nil))
    self:RegTbDataFac("DesktopHBetOperateTexas", TbDataFactoryDesktopHBetOperateTexas:new(nil))
    self:RegTbDataFac("DesktopHBetPotTexas", TbDataFactoryDesktopHBetPotTexas:new(nil))
    self:RegTbDataFac("DesktopHBetReward", TbDataFactoryDesktopHBetReward:new(nil))
    self:RegTbDataFac("DesktopHInfoTexas", TbDataFactoryDesktopHInfoTexas:new(nil))
    self:RegTbDataFac("DesktopInfoTexas", TbDataFactoryDesktopInfoTexas:new(nil))
    self:RegTbDataFac("Expression", TbDataFactoryExpression:new(nil))
    self:RegTbDataFac("HintsInfoTexas", TbDataFactoryHintsInfoTexas:new(nil))
    self:RegTbDataFac("Item", TbDataFactoryItem:new(nil))
    self:RegTbDataFac("ItemType", TbDataFactoryItemType:new(nil))
    self:RegTbDataFac("LanEn", TbDataFactoryLanEn:new(nil))
    self:RegTbDataFac("Lans", TbDataFactoryLans:new(nil))
    self:RegTbDataFac("LanZh", TbDataFactoryLanZh:new(nil))
    self:RegTbDataFac("LotteryTicket", TbDataFactoryLotteryTicket:new(nil))
    self:RegTbDataFac("LotteryTicketBetOperate", TbDataFactoryLotteryTicketBetOperate:new(nil))
    self:RegTbDataFac("LotteryTicketGoldPercent", TbDataFactoryLotteryTicketGoldPercent:new(nil))
    self:RegTbDataFac("OnlineReward", TbDataFactoryOnlineReward:new(nil))
    self:RegTbDataFac("PresetMsg", TbDataFactoryPresetMsg:new(nil))
    self:RegTbDataFac("TexasRaiseBlinds", TbDataFactoryTexasRaiseBlinds:new(nil))
    self:RegTbDataFac("TexasSnowBallRewardInfo", TbDataFactoryTexasSnowBallRewardInfo:new(nil))
    self:RegTbDataFac("TexasSnowBallRewardPlayerNum", TbDataFactoryTexasSnowBallRewardPlayerNum:new(nil))
    self:RegTbDataFac("UnitBilling", TbDataFactoryUnitBilling:new(nil))
    self:RegTbDataFac("UnitConsume", TbDataFactoryUnitConsume:new(nil))
    self:RegTbDataFac("UnitGiftNormal", TbDataFactoryUnitGiftNormal:new(nil))
    self:RegTbDataFac("UnitGiftTmp", TbDataFactoryUnitGiftTmp:new(nil))
    self:RegTbDataFac("UnitGoldPackage", TbDataFactoryUnitGoldPackage:new(nil))
    self:RegTbDataFac("UnitGoodsVoucher", TbDataFactoryUnitGoodsVoucher:new(nil))
    self:RegTbDataFac("UnitMagicExpression", TbDataFactoryUnitMagicExpression:new(nil))
    self:RegTbDataFac("UnitRedEnvelopes", TbDataFactoryUnitRedEnvelopes:new(nil))
    self:RegTbDataFac("VIPLevel", TbDataFactoryVIPLevel:new(nil))

    local list_db_filename = {}
    for i, v in pairs(self.Context.Cfg.TbFileList) do
        list_db_filename[i] = self.CasinosContext.PathMgr.DirRawRoot .. "TbData/" .. v .. ".db"
    end

    for i, v in pairs(list_db_filename) do
        local open_db = self.Sqlite:openDb(v)
        if (open_db == false) then
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
    if (fac == nil) then
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
    if (map_data == nil) then
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
    if (list_data.Count <= 0) then
        return nil
    end
    return list_data
end