-- Copyright(c) Cragon. All rights reserved.
-- 每个小头像都使用的头像基础类，目前没有归池管

---------------------------------------
ViewHeadIcon = {}

---------------------------------------
function ViewHeadIcon:new(o, co_headicon, click_callback, load_icon_down)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.GCoHeadIcon = co_headicon
    o.EventCallback0 = click_callback
    o.GCoHeadIcon.onClick:Add(
            function()
                o:onClickHeadIcon()
            end
    )
    local com_loader = o.GCoHeadIcon:GetChild("ComLoaderIcon").asCom
    o.GLoaderPlayerIcon = com_loader:GetChild("LoaderIcon").asLoader
    local loader_playericon = CS.Casinos.LuaHelper.GLoaderCastToGLoaderEx(o.GLoaderPlayerIcon)
    loader_playericon.LoaderDoneCallBack = load_icon_down
    local co_vipsign = o.GCoHeadIcon:GetChild("CoVIPSign")
    if (co_vipsign ~= nil)
    then
        o.ViewVIPSign = ViewVIPSign:new(nil, co_vipsign.asCom)
    end
    local bank_sign = o.GCoHeadIcon:GetChild("BankPlayerSign")
    if (bank_sign ~= nil)
    then
        o.GImageBankPlayerSign = bank_sign.asImage
    end
    o.ControllerShowShade = o.GCoHeadIcon:GetController("ControllerShowShade")
    local loader_currentgift = o.GCoHeadIcon:GetChild("LoaderCurrentGift")
    if (loader_currentgift ~= nil)
    then
        o.GLoaderCurrentGift = loader_currentgift.asLoader
        o.GLoaderCurrentGift.visible = false
    end

    return o
end

---------------------------------------
function ViewHeadIcon:setPlayerInfoDesktopH(player_info, is_bank)
    local icon = player_info.PlayerInfoCommon.IconName
    self:setPlayerIcon(icon, player_info.PlayerInfoCommon.AccountId)
    if (self.ViewVIPSign ~= nil)
    then
        self.ViewVIPSign:setVIPLevel(player_info.PlayerInfoCommon.VIPLevel)
    end
    if (self.GImageBankPlayerSign ~= nil)
    then
        self.GImageBankPlayerSign.visible = is_bank
    end
end

---------------------------------------
function ViewHeadIcon:setPlayerInfo(icon, account_id, vip_level, is_online)
    self:setPlayerIcon(icon, account_id)
    if (self.ViewVIPSign ~= nil)
    then
        self.ViewVIPSign:setVIPLevel(vip_level)
    end
    if (self.GImageBankPlayerSign ~= nil)
    then
        self.GImageBankPlayerSign.visible = false
    end
    local online = true
    if (is_online ~= nil)
    then
        online = is_online
    end
    if (online)
    then
        self:showShade(false)
    else
        self:showShade(true)
    end
end

---------------------------------------
function ViewHeadIcon:setPlayerInfo1(icon, account_id, vip_level, gift_tbid, is_online)
    self:setPlayerIcon(icon, account_id)
    if (self.ViewVIPSign ~= nil)
    then
        self.ViewVIPSign:setVIPLevel(vip_level)
    end
    if (self.GImageBankPlayerSign ~= nil)
    then
        self.GImageBankPlayerSign.visible = false
    end
    local online = true
    if (is_online ~= nil)
    then
        online = is_online
    end
    if (online)
    then
        self:showShade(false)
    else
        self:showShade(true)
    end
    self:setGift(gift_tbid)
end

---------------------------------------
function ViewHeadIcon:setGift(gift_tbid)
    if (gift_tbid == 0)
    then
        self.GLoaderCurrentGift.icon = nil
        return
    end
    if (self.GLoaderCurrentGift ~= nil)
    then
        local tb_item = CS.Casinos.CasinosContext.Instance.TbDataMgrLua:GetData("Item", gift_tbid)
        if (tb_item == nil)
        then
            return
        end
        self.GLoaderCurrentGift.icon = CS.Casinos.CasinosContext.Instance.PathMgr:combinePersistentDataPath(CS.Casinos.UiHelperCasinos:getABItemResourceTitlePath() .. string.lower(tb_item.Icon) .. ".ab")
        self.GLoaderCurrentGift.visible = true
    end
end

---------------------------------------
function ViewHeadIcon:showShade(show_shade)
    if (ControllerShowShade ~= nil)
    then
        if (show_shade)
        then
            self.ControllerShowShade.selectedIndex = 1
        else
            self.ControllerShowShade.selectedIndex = 0
        end
    end
end

---------------------------------------
function ViewHeadIcon:setVipLevel(vip_level)
    if (self.ViewVIPSign ~= nil)
    then
        self.ViewVIPSign:setVIPLevel(vip_level)
    end
end

---------------------------------------
function ViewHeadIcon:setLotteryWinMaxPlayerInfo(lastround_winmax_playerinfo)
    self:setPlayerIcon(lastround_winmax_playerinfo.Icon, lastround_winmax_playerinfo.PlayerId)
    if (self.ViewVIPSign ~= nil)
    then
        self.ViewVIPSign:setVIPLevel(lastround_winmax_playerinfo.VIPLevel)
    end
    if (self.GImageBankPlayerSign ~= nil)
    then
        self.GImageBankPlayerSign.visible = false
    end
end

---------------------------------------
function ViewHeadIcon:setMainPlayerInfo(controller_actor)
    self:setPlayerIcon(controller_actor.PropIcon:get(), tostring(controller_actor.PropAccountId:get()))
    if (self.ViewVIPSign ~= nil)
    then
        self.ViewVIPSign:setVIPLevel(controller_actor.PropVIPLevel:get())
    end
    if (self.GImageBankPlayerSign ~= nil)
    then
        self.GImageBankPlayerSign.visible = false
    end
end

---------------------------------------
function ViewHeadIcon:setIcon1(icon)
    self.GLoaderPlayerIcon.visible = true
    self.GLoaderPlayerIcon.icon = icon
end

---------------------------------------
function ViewHeadIcon:setIcon(icon_name, acount_id)
    self:setPlayerIcon(icon_name, acount_id)
end

---------------------------------------
function ViewHeadIcon:setIcon2(t)
    self.GLoaderPlayerIcon.texture = CS.FairyGUI.NTexture(t)
end

---------------------------------------
function ViewHeadIcon:setIcon3(t)
    self.GLoaderPlayerIcon.texture = t
end

---------------------------------------
function ViewHeadIcon:hideIcon()
    self.GCoHeadIcon.visible = false
end

---------------------------------------
function ViewHeadIcon:setPlayerIcon(icon, account_id)
    self.GCoHeadIcon.visible = true
    self.GLoaderPlayerIcon.icon = nil
    if (icon ~= nil and string.len(icon) > 0)
    then
        self.GLoaderPlayerIcon.icon = Context:CalcBotIconUrl(true, icon)
    else
        if (account_id ~= nil and string.len(account_id) > 0)
        then
            local icon_resource_name = ""
            local temp_table = CS.Casinos.LuaHelper.getIconName(true, account_id, icon_resource_name)
            icon = temp_table[1]
            if (icon ~= nil and string.len(icon) > 0)
            then
                self.GLoaderPlayerIcon.icon = PlayerIconDomain .. icon
            end
        end
    end
end

---------------------------------------
function ViewHeadIcon:onClickHeadIcon()
    if (self.EventCallback0 ~= nil)
    then
        self:EventCallback0()
    end
end