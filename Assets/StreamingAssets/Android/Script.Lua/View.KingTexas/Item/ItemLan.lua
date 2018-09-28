-- Copyright(c) Cragon. All rights reserved.
-- 多语言切换界面中的一个Item

ItemLan = {}

function ItemLan:new(o,com,tb_id,lan_mgr)
	o = o or {}
    setmetatable(o,self)
	self.__index = self
	o.CasinosContext = CS.Casinos.CasinosContext.Instance
	o.TipsKey = "ConfirmChangeLan"
    o.ChangeSuccessTipsKey = "ChangeLanSuccess"
	o.LanMgr  = lan_mgr
	local loader_lan = com:GetChild("LoadLanguage").asLoader
    local text_name = com:GetChild("TextLanguage").asTextField
    o.TbId = tb_id
    local tb_lan = o.CasinosContext.TbDataMgrLua:GetData("Lans",o.TbId)
    text_name.text = o.LanMgr:getLanValue(tb_lan.LanKey)
    local pack_name = o.LanMgr:getLanPackageName()
    local icon_url = CS.FairyGUI.UIPackage.GetItemURL(pack_name, tb_lan.LanIcon)
    loader_lan.icon = icon_url
    com.onClick:Add(
		function()
			o:onClickItem()
		end
	)
	return o
end

function ItemLan:onClickItem()
	local info = self.LanMgr:getLanValue(self.TipsKey)
    local msg_box = ViewHelper:UiShowMsgBox(info,
					function()
						local tb_lan = self.CasinosContext.TbDataMgrLua:GetData("Lans",self.TbId)
						local view_mgr = ViewMgr:new(nil)
						local ev = view_mgr:getEv("EvUiChangeLan")
						if(ev == nil)
						then
							ev = EvUiChangeLan:new(nil)
						end
                        ev.lan = tb_lan.LanType
                        view_mgr:sendEv(ev)
					end                                     
					)
end

