-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ItemReportPlayerOperate = {}

---------------------------------------
function ItemReportPlayerOperate:new(o, com, view_playerprofile)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ViewPlayerProfile = view_playerprofile
    com.onClick:Add(
            function()
                o:onClick()
            end
    )
    o.GTextReportContent = com:GetChild("TextReportContent").asTextField
    o.mReportPlayer = report
    return o
end

---------------------------------------
function ItemReportPlayerOperate:setReportType(report_type, friend_etguid)
    self.mReportPlayerType = report_type
    self.mFriendEtGuid = friend_etguid

    local operate_name = ""
    if (report_type == ReportPlayerType.AbuseBehavior) then
        operate_name = self.ViewPlayerProfile.ViewMgr.LanMgr:getLanValue("AbuseOthers")
    elseif (report_type == ReportPlayerType.GoldTransaction) then
        operate_name = self.ViewPlayerProfile.ViewMgr.LanMgr:getLanValue("IllegalDeal")
    elseif (report_type == ReportPlayerType.PornIcon) then
        operate_name = self.ViewPlayerProfile.ViewMgr.LanMgr:getLanValue("PornPic")
    elseif (report_type == ReportPlayerType.Cheat) then
        operate_name = self.ViewPlayerProfile.ViewMgr.LanMgr:getLanValue("UsePlug-in")
    end
    self.GTextReportContent.text = operate_name
end

---------------------------------------
function ItemReportPlayerOperate:onClick()
    local ev = self.ViewPlayerProfile.ViewMgr:GetEv("EvUiReportFriend")
    if (ev == nil) then
        ev = EvUiReportFriend:new(nil)
    end
    ev.friend_etguid = self.mFriendEtGuid
    ev.report_type = self.mReportPlayerType
    self.ViewPlayerProfile.ViewMgr:SendEv(ev)
    self.ViewPlayerProfile:HideComReportPlayer()
end