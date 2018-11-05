-- Copyright(c) Cragon. All rights reserved.
-- 礼物商城

---------------------------------------
ViewGiftShop = ViewBase:new()

---------------------------------------
function ViewGiftShop:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.mDesktopGiftTypeld = 200
    self.mNotDesktopGiftTypeld = 100
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
function ViewGiftShop:OnCreate()
    ViewHelper:PopUi(self.ComUi, self.ViewMgr.LanMgr:getLanValue("GiftShop"))
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.ControllerActor = self.ViewMgr.ControllerMgr:GetController("Actor")
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
    self.GTextChips = self.ComUi:GetChild("TextChips").asTextField
    self.GTextCoin = self.ComUi:GetChild("TextCoin").asTextField
    self.GListPanelGiftType = self.ComUi:GetChild("PanelGiftType").asList
    self.GListPanelGift = self.ComUi:GetChild("PanelGift").asList
    self.mMapGiftType = {}
    self.ViewMgr:BindEvListener("EvUiBuyItem", self)
end

---------------------------------------
function ViewGiftShop:OnDestroy()
    self.ViewMgr:UnbindEvListener(self)
    self.ViewPool:itemGiftAllEnque()
end

---------------------------------------
function ViewGiftShop:OnHandleEv(ev)
    if (ev ~= nil) then
        if (ev.EventName == "EvUiBuyItem") then
            self:onClickBtnClose()
        end
    end
end

---------------------------------------
function ViewGiftShop:setGiftShopInfo(desktop_gift, from_etguid, to_etguid)
    self.mFromEtCuid = from_etguid
    self.mToEtGuid = to_etguid
    self.GTextChips.text = UiChipShowHelper:getGoldShowStr(self.ControllerActor.PropGoldAcc:get(), self.ViewMgr.LanMgr.LanBase)
    self.GTextCoin.text = UiChipShowHelper:getGoldShowStr(self.ControllerActor.PropDiamond:get(), self.ViewMgr.LanMgr.LanBase)
    for key, value in pairs(self.CasinosContext.TbDataMgrLua:GetMapData("ItemType")) do
        local tb_item = value
        if (tb_item.ParentTbId ~= self.mNotDesktopGiftTypeld and tb_item.ParentTbId ~= self.mDesktopGiftTypeld) then
        elseif ((tb_item.ParentTbId == self.mNotDesktopGiftTypeld and desktop_gift == false) or (tb_item.ParentTbId == self.mDesktopGiftTypeld and desktop_gift)) then
        else
            local gift_type_item = self.GListPanelGiftType:AddItemFromPool().asCom
            local ui_item_gift_type = ItemGiftType:new(nil, gift_type_item, self)
            ui_item_gift_type:setGiftType(tb_item.Id, self.mFromEtCuid, self.mToEtGuid)
            self.mMapGiftType[tb_item.Id] = ui_item_gift_type
            if (self.mCurrentGiftType == nil) then
                self.mCurrentGiftType = self.mMapGiftType[tb_item.Id]
            end
        end
    end

    self.mCurrentGiftType:onClick()
end

---------------------------------------
function ViewGiftShop:setCurrentGiftType(tb_itemtype)
    self.GListPanelGift:RemoveChildrenToPool()
    self.mCurrentGiftType = tb_itemtype
    local tb_giftType = self.mCurrentGiftType:getTbDataItemType()
    if (tb_giftType == nil) then
        return
    end
    for key, value in pairs(tb_giftType:getCurrentTypeItems(self.CasinosContext.TbDataMgrLua)) do
        local com_item_gift = self.GListPanelGift:AddItemFromPool().asCom
        local ui_item_gift = self.ViewPool:getItemGift(com_item_gift)
        ui_item_gift:init(com_item_gift, self.ViewMgr.LanMgr)
        ui_item_gift:setGift(key, true, false, self.mToEtGuid, "", "", nil)
    end
end

---------------------------------------
function ViewGiftShop:closeCurrentGiftType()
    if (self.mCurrentGiftType ~= nil) then
        self.mCurrentGiftType.mControlSelect.selectedIndex = 0
    end
end

---------------------------------------
function ViewGiftShop:onClickBtnClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewGiftShopFactory = ViewFactory:new()

---------------------------------------
function ViewGiftShopFactory:new(o, ui_package_name, ui_component_name, ui_layer, is_single, fit_screen)
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
function ViewGiftShopFactory:CreateView()
    local view = ViewGiftShop:new(nil)
    return view
end