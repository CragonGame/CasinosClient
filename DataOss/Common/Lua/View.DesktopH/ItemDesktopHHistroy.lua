-- Copyright(c) Cragon. All rights reserved.
-- 持有10个历史元素

---------------------------------------
ItemDesktopHHistroy = {}

---------------------------------------
function ItemDesktopHHistroy:new(o, com_history, desktoph, betpot_index, list_history)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ViewDesktoph = desktoph
    o.GCoHistroy = com_history
    o.GLoaderBetPotIcon = o.GCoHistroy:GetChild("LoaderBetPotIcon").asLoader
    o.GLoaderBetPotIcon.icon = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(o.ViewDesktoph:getDesktopBasePackageName(), "PotSmall" .. betpot_index)
    o.MapHistory = {}
    for i = 1, 10 do
        o.MapHistory[i] = o.GCoHistroy:GetChild("Loader" .. i).asLoader
    end
    o:refreshHistory(list_history)
    return o
end

---------------------------------------
function ItemDesktopHHistroy:refreshHistory(list_history)
    local index = 1
    for i = 1, #list_history do
        local l = list_history[i]
        local win_loose = "Loose"
        if (l == true) then
            win_loose = "Win"
        end
        local icon_name = CS.Casinos.UiHelperCasinos.FormatePackageImagePath("DesktopHHistory", win_loose)
        if (UseLan == true) then
            local pack_name = self.ViewDesktoph.ViewMgr.LanMgr:getLanPackageName()
            icon_name = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(pack_name, win_loose)
        end
        local loader = self.MapHistory[index]
        loader.icon = icon_name
        index = index + 1
    end
end