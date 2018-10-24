-- Copyright(c) Cragon. All rights reserved.
-- 举报玩家中的一个Item

---------------------------------------
ItemPlayerReport = {}

---------------------------------------
function ItemPlayerReport:New(i_profile, co_reportbtn, report_type)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ViewPlayerProfile = i_profile
    o.GLoaderReportType = co_reportbtn:GetChild("LoaderReportTitle").asLoader
    o.ReportPlayerType = report_type
    local operate_name = "Report" .. report_type:Tostring()
    o.GLoaderReportType.icon = CS.Casinos.UiHelperCasinos:FormatePackageImagePath("PlayerProfile", operate_name)
    co_reportbtn.onClick:Add(
            function()
                o.ViewPlayerProfile:reportFriend(o.ViewPlayerProfile.PlayerGuid, o.ReportPlayerType)
            end
    )
    return o
end