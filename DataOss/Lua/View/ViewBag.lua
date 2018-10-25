-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewBag = ViewBase:new()

---------------------------------------
function ViewBag:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.ViewMgr = nil
    self.GoUi = nil
    self.ComUi = nil
    self.Panel = nil
    self.UILayer = nil
    self.InitDepth = nil
    self.ViewKey = nil
    return o
end

---------------------------------------
function ViewBag:onCreate()
    ViewHelper:PopUi(self.ComUi, self.ViewMgr.LanMgr:getLanValue("Bag1"))
    self.ControllerBag = self.ViewMgr.ControllerMgr:GetController("Bag")
    self.ControllerPlayer = self.ViewMgr.ControllerMgr:GetController("Player")
    self.ViewPool = self.ViewMgr:GetView("Pool")
    local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
    local btn_close = com_bg:GetChild("BtnClose").asButton
    btn_close.onClick:Add(
            function()
                self:onClickBtnClose()
            end
    )
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:onClickBtnClose()
            end
    )
    local btn_shopMore = self.ComUi:GetChild("Lan_Btn_BuyMore").asButton
    btn_shopMore.onClick:Add(
            function()
                self:onClickBtnShopMore()
            end
    )
    self.GControllerBag = self.ComUi:GetController("ControllerBag")
    self.GControllerBag.selectedIndex = 1
    self.ControllerTips = self.ComUi:GetController("ControllerTips")
    self:showTips()
    local btn_gift = self.ComUi:GetChild("BtnGift").asButton
    btn_gift.onClick:Add(
            function()
                self:onClickBtnGift()
            end
    )
    local btn_item = self.ComUi:GetChild("BtnItem").asButton
    btn_item.onClick:Add(
            function()
                self:onClickBtnItem()
            end
    )
    self.ListGiftNormal = self.ComUi:GetChild("ListGiftNormal").asList
    self.ListGiftNormal:SetVirtual()
    self.ListGiftNormal.itemRenderer = function(a, b)
        self:rendererGiftNormal(a, b)
    end
    self.ListGiftNormal.numItems = #self.ControllerBag.ListItemGiftNormal
    self.ViewMgr:BindEvListener("EvEntityBagDeleteItem", self)
    self.ViewMgr:BindEvListener("EvEntityBagAddItem", self)
end

---------------------------------------
function ViewBag:onDestroy()
    self.ViewMgr:UnbindEvListener(self)
    self.ViewPool:itemGiftAllEnque()
end

---------------------------------------
function ViewBag:onHandleEv(ev)
    if (ev ~= nil) then
        if (ev.EventName == "EvEntityBagDeleteItem") then
            self:updateBag()
        elseif (ev.EventName == "EvEntityBagAddItem") then
            self:updateBag()
        end
    end
end

---------------------------------------
function ViewBag:rendererGiftNormal(index, item)
    if (self.GControllerBag.selectedIndex == 0) then
        if (#self.ControllerBag.ListItemConsume > index) then
            local gift = self.ControllerBag.ListItemConsume[index + 1]
            if (gift.UnitLink.UnitType == "GoldPackage") then
                -- 自动使用金币袋子
                local ev = self.ViewMgr:GetEv("EvUiRequestUseProp")
                if (ev == nil) then
                    ev = EvUiRequestUseProp:new(nil)
                end
                self.ViewMgr:SendEv(ev)
            end
            local com = CS.Casinos.LuaHelper.GObjectCastToGCom(item)
            local item_gift = self.ViewPool:getItemGift(com)
            item_gift:init(com, self.ViewMgr.LanMgr)
            item_gift:setGift(gift.TbDataItem.Id, false, self.ControllerPlayer.Guid == gift.UnitLink.GiveEtGuid,
                    self.ControllerPlayer.Guid, gift.UnitLink.GiveBy, self.ControllerPlayer.Guid, gift)
        end
    else
        if (#self.ControllerBag.ListItemGiftNormal > index) then
            local gift = self.ControllerBag.ListItemGiftNormal[index + 1]
            local com = CS.Casinos.LuaHelper.GObjectCastToGCom(item)
            local item_gift = self.ViewPool:getItemGift(com)
            item_gift:init(com, self.ViewMgr.LanMgr)
            item_gift:setGift(gift.TbDataItem.Id, false, self.ControllerPlayer.Guid == gift.UnitLink.GivePlayerEtGuid,
                    self.ControllerPlayer.Guid, gift.UnitLink.GiveBy, self.ControllerPlayer.Guid, gift)
        end
    end
end

---------------------------------------
function ViewBag:updateBag()
    if (self.GControllerBag.selectedIndex == 0) then
        self.ListGiftNormal.numItems = #self.ControllerBag.ListItemConsume
    else
        self.ListGiftNormal.numItems = #self.ControllerBag.ListItemGiftNormal
    end
    self:showTips()
end

---------------------------------------
function ViewBag:showTips()
    if (self.GControllerBag.selectedIndex == 0 and #self.ControllerBag.ListItemConsume == 0) then
        self.ControllerTips.selectedIndex = 2
    elseif (self.GControllerBag.selectedIndex == 1 and #self.ControllerBag.ListItemGiftNormal == 0) then
        self.ControllerTips.selectedIndex = 1
    else
        self.ControllerTips.selectedIndex = 0
    end
end

---------------------------------------
function ViewBag:onClickBtnShopMore()
    local ev = self.ViewMgr:GetEv("EvCreateGiftShop")
    if (ev == nil) then
        ev = EvCreateGiftShop:new(nil)
    end
    ev.is_tmp_gift = false
    ev.not_indesktop = true
    ev.to_player_etguid = self.ControllerPlayer.Guid
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ViewBag:onClickBtnGift()
    self.GControllerBag.selectedIndex = 1
    self:updateBag()
    self:showTips()
end

---------------------------------------
function ViewBag:onClickBtnItem()
    self.GControllerBag.selectedIndex = 0
    self:updateBag()
    self:showTips()
end

---------------------------------------
function ViewBag:onClickBtnClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewBagFactory = ViewFactory:new()

---------------------------------------
function ViewBagFactory:new(o, ui_package_name, ui_component_name, ui_layer, is_single, fit_screen)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.PackageName = ui_package_name
    self.ComponentName = ui_component_name
    self.UILayer = ui_layer
    self.IsSingle = is_single
    self.FitScreen = fit_screen
    return o
end

---------------------------------------
function ViewBagFactory:CreateView()
    local view = ViewBag:new(nil)
    return view
end