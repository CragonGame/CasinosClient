-- Copyright(c) Cragon. All rights reserved.
-- 魔法表情列表中的一个Item

---------------------------------------
ItemMagicExpIcon = {}

---------------------------------------
function ItemMagicExpIcon:new(o, i_profile, co_magicexp, exp_id)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.IUiPlayerProfile = i_profile
    o.ExpId = exp_id
    o.GLoaderIcon = co_magicexp:GetChild("LoaderExp").asLoader
    local tb_magicexp = o.IUiPlayerProfile.ViewMgr.TbDataMgr:GetData("UnitMagicExpression", exp_id)
    o.GLoaderIcon.icon = ViewHelper:FormatePackageImagePath("PlayerProfile", tb_magicexp.ExpIcon)
    co_magicexp.onClick:Add(
            function()
                o.IUiPlayerProfile:sendMagicExp(o.ExpId)
            end
    )
    return o
end