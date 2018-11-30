-- Copyright(c) Cragon. All rights reserved.
-- 礼物商城

---------------------------------------
ViewGiftShop = class(ViewBase)

---------------------------------------
function ViewGiftShop:ctor()
    self.mDesktopGiftTypeld = 200
    self.mNotDesktopGiftTypeld = 100
    self.Tween = nil
end

---------------------------------------
function ViewGiftShop:OnCreate()
    self.Tween = ViewHelper:PopUi(self.ComUi, self.ViewMgr.LanMgr:getLanValue("GiftShop"))
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.ControllerActor = self.ViewMgr.ControllerMgr:GetController("Actor")
    self.ViewPool = self.ViewMgr:GetView("Pool")
    local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
    local btn_close = com_bg:GetChild("BtnClose").asButton
    btn_close.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:_onClickBtnClose()
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
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end
    self.ViewMgr:UnbindEvListener(self)
    self.ViewPool:itemGiftAllEnque()
end

---------------------------------------
function ViewGiftShop:OnHandleEv(ev)
    if (ev ~= nil) then
        if (ev.EventName == "EvUiBuyItem") then
            self:_onClickBtnClose()
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

    self.mCurrentGiftType:_onClick()
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
        ui_item_gift:SetGift(key, true, false, self.mToEtGuid, "", "", nil)
    end
end

---------------------------------------
function ViewGiftShop:closeCurrentGiftType()
    if (self.mCurrentGiftType ~= nil) then
        self.mCurrentGiftType.mControlSelect.selectedIndex = 0
    end
end

---------------------------------------
function ViewGiftShop:_onClickBtnClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewGiftShopFactory = class(ViewFactory)

---------------------------------------
function ViewGiftShopFactory:CreateView()
    local view = ViewGiftShop:new()
    return view
end