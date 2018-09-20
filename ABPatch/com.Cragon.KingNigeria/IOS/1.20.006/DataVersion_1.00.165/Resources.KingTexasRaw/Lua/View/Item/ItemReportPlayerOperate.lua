ItemReportPlayerOperate = {}

function ItemReportPlayerOperate:new(o,com,view_playerprofile)
	o = o or {}
    setmetatable(o,self)
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

function ItemReportPlayerOperate:setReportType(report_type,friend_etguid)
	 self.mReportPlayerType = report_type
     self.mFriendEtGuid = friend_etguid

     local operate_name = ""
	 if(report_type == ReportPlayerType.AbuseBehavior)
	 then
		operate_name = self.ViewPlayerProfile.ViewMgr.LanMgr:getLanValue("辱骂他人", "AbuseOthers")
	 elseif(report_type == ReportPlayerType.GoldTransaction)
	 then
		operate_name = self.ViewPlayerProfile.ViewMgr.LanMgr:getLanValue("非法交易", "IllegalDeal")
	 elseif(report_type == ReportPlayerType.PornIcon)
	 then
		operate_name = self.ViewPlayerProfile.ViewMgr.LanMgr:getLanValue("色情头像", "PornPic")
	 elseif(report_type == ReportPlayerType.Cheat)
	 then
		operate_name = self.ViewPlayerProfile.ViewMgr.LanMgr:getLanValue("使用外挂", "UsePlug-in")
	 end
	 self.GTextReportContent.text = operate_name
end

function ItemReportPlayerOperate:onClick()
	local ev = self.ViewPlayerProfile.ViewMgr:getEv("EvUiReportFriend")
	if(ev == nil)
	then
		ev = EvUiReportFriend:new(nil)
	end
    ev.friend_etguid = self.mFriendEtGuid
    ev.report_type = self.mReportPlayerType
    self.ViewPlayerProfile.ViewMgr:sendEv(ev)
	self.ViewPlayerProfile:HideComReportPlayer()
end


