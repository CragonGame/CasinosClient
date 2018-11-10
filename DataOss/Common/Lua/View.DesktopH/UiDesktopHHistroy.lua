-- Copyright(c) Cragon. All rights reserved.
-- 持有10个历史元素

---------------------------------------
UiDesktopHHistroy = {}

---------------------------------------
function UiDesktopHHistroy:new(com_history, desktoph, betpot_index, list_history)
    local o = {}
    setmetatable(o, { __index = self })
    --self.__index = self
    --setmetatable(o, self)
    o.Context = Context
    o.ViewDesktoph = desktoph
    o.GCoHistroy = com_history
    o.GLoaderBetPotIcon = o.GCoHistroy:GetChild("LoaderBetPotIcon").asLoader
    o.GLoaderBetPotIcon.icon = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(o.ViewDesktoph:getDesktopBasePackageName(), "PotSmall" .. betpot_index)
    o.MapHistory = {}
    for i = 1, 10 do
        o.MapHistory[i] = o.GCoHistroy:GetChild("Loader" .. i).asLoader
    end
    o:_refreshHistory(list_history)
    return o
end

---------------------------------------
function UiDesktopHHistroy:_refreshHistory(list_history)
    local index = 1
    for i = 1, #list_history do
        local l = list_history[i]
        local win_loose = "Loose"
        if (l == true) then
            win_loose = "Win"
        end
        local icon_name = CS.Casinos.UiHelperCasinos.FormatePackageImagePath("DesktopHHistory", win_loose)
        if (self.Context.Cfg.UseLan == true) then
            local pack_name = self.ViewDesktoph.ViewMgr.LanMgr:getLanPackageName()
            icon_name = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(pack_name, win_loose)
        end
        local loader = self.MapHistory[index]
        loader.icon = icon_name
        index = index + 1
    end
end