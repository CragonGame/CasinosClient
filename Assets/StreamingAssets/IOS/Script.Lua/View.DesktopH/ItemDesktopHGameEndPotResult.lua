-- Copyright(c) Cragon. All rights reserved.
-- 结算对话框中的5个Item中的一个

ItemDesktopHGameEndPotResult = {}

function ItemDesktopHGameEndPotResult:new(o,ui_desktoph,com,list_card,is_win,pot_index)
    o = o or {}
    setmetatable(o,self)
    self.__index = self	
    o.GCom = com
	local c_name = ""
    for i, v in pairs(list_card) do
        c_name = "Card" .. tostring(i)
        local l_card = o.GCom:GetChild(c_name)
        if (l_card ~= nil)
        then
            local gloader_card = l_card.asLoader
            local card_name = ui_desktoph.UiDesktopHBase:getCardResName(v)
            gloader_card.icon = CS.Casinos.CasinosContext.Instance.PathMgr:combinePersistentDataPath
            (CS.Casinos.UiHelperCasinos.getABCardResourceTitlePath() .. string.lower(card_name) .. ".ab")
        end
    end

            o.GLoaderBankSign = o.GCom:GetChild("LoaderBankSign").asLoader
            local bank_sign = ""
			if(pot_index == 255) then
				 local packageName = ui_desktoph:getDesktopBasePackageName()
                    if (UseLan == true) then
                        packageName = ui_desktoph.ViewMgr.LanMgr:getLanPackageName()
                    end
                    bank_sign = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(packageName, "BankSign")
			elseif(pot_index == 0) then
				bank_sign = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(ui_desktoph:getDesktopBasePackageName(), "Pot0Sign")
			elseif(pot_index == 1) then
				bank_sign = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(ui_desktoph:getDesktopBasePackageName(), "Pot1Sign")
			elseif(pot_index == 2) then
				bank_sign = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(ui_desktoph:getDesktopBasePackageName(), "Pot2Sign")
			elseif(pot_index == 3) then
				bank_sign = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(ui_desktoph:getDesktopBasePackageName(), "Pot3Sign")
			end
           
            o.GLoaderBankSign.icon = bank_sign
            o.GLoaderWinLoose = o.GCom:GetChild("LoaderWinLoose").asLoader
    local win_loose = "LooseSign"
    if(is_win) then
        win_loose = "WinSign"
    end
            local win_sign = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(ui_desktoph:getDesktopBasePackageName(), win_loose)
            if (UseLan == true) then
                local pack_name = ui_desktoph.ViewMgr.LanMgr:getLanPackageName()
                win_sign = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(pack_name, win_loose)
            end
            o.GLoaderWinLoose.icon = win_sign

    return o
end