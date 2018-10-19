-- Copyright(c) Cragon. All rights reserved.

ItemCountryCode = {}

function ItemCountryCode:new(o,com,view_mgr,ui_choose_code,is_last)
	o = o or {}
    setmetatable(o, self)
    self.__index = self
	o.ViewMgr = view_mgr
    o.UiChooseCountryCode = ui_choose_code
	o.Com = com
	o.Com.onClick:Clear()
	o.Com.onClick:Add(
		function()
			o:onClickOperate()
		end
	)
    o.GTextCountryCode = o.Com:GetChild("TextCountryCode").asTextField
	o.ControllerShowLine = o.Com:GetController("ControllerShowLine")
	local show_index = 0
	if is_last then
		show_index = 1
	end
	o.ControllerShowLine.selectedIndex = show_index

	return o
end

function ItemCountryCode:setDetail(k,v)
	self.CountryKey = k
	self.CountryValue = v
	local t = {}
	table.insert(t,"+")
	table.insert(t,v["Code"])
	table.insert(t," ")
	table.insert(t,self.ViewMgr.LanMgr:getLanValue(k))
    self.GTextCountryCode.text = table.concat(t)
	t = {}
	table.insert(t,k)
	table.insert(t," ")
	table.insert(t,"+")
	table.insert(t,v["Code"])
	self.KeyAndCodeFormat = table.concat(t)
end

function ItemCountryCode:onClickOperate()
	local ev = self.ViewMgr:getEv("EvUiChooseCountry")
	if(ev == nil)
	then
		ev = EvUiChooseCountry:new(nil)
	end
	ev.CountryKey = self.CountryKey
	ev.CountryCode = self.CountryValue["Code"]
	ev.KeyAndCodeFormat = self.KeyAndCodeFormat
	self.ViewMgr:sendEv(ev)
    self.UiChooseCountryCode:setCurrentCode(self.CountryKey,false)
end