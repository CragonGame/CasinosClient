-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
-- 单个Item
UiDesktopHSetCardTypeItem = {}

---------------------------------------
function UiDesktopHSetCardTypeItem:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ViewMgr = nil
    o.GoUi = nil
    o.ComUi = nil
    o.Panel = nil
    o.UILayer = nil
    o.InitDepth = nil
    o.ViewKey = nil
    o.GComboBoxCardType = nil
    o.GTextPotTitle = nil
    o.ViewDesktopH = nil
    o.ViewDesktopHSetCardType = nil
    o.BetPotIndex = nil
    o.AllCardTypeCount = nil
    o.MapCardType = nil
    o.RandomCardTypeName = "随机"
    return o
end

---------------------------------------
function UiDesktopHSetCardTypeItem:OnCreate(co_card_type, betpot_index, ui_desktoph, ui_card_type)
    self.GComboBoxCardType = co_card_type
    self.GTextPotTitle = self.GComboBoxCardType:GetChild("PotTitle").asTextField
    self.BetPotIndex = betpot_index
    self.ViewDesktopH = ui_desktoph
    self.ViewDesktopHSetCardType = ui_card_type
    self.MapCardType = {}
    local all_card = self.ViewDesktopH.UiDesktopHBase:getAllCardType()
    local all_card_l = #all_card + 2
    self.AllCardTypeCount = all_card_l
    local items = {}--new string[all_card.Count + 1]
    local f = all_card[0]
    items[0] = f
    self.MapCardType[f] = 0
    local index = 1
    for i, v in pairs(all_card) do
        items[index] = v
        index = index + 1
        self.MapCardType[v] = i
    end

    items[all_card_l] = self.RandomCardTypeName
    self.MapCardType[self.RandomCardTypeName] = all_card_l
    self.GComboBoxCardType.items = items
    self.GComboBoxCardType.onChanged:Add(
            function()
                self:_onClick()
            end
    )
    self.GComboBoxCardType.text = items[all_card_l]
    local title_name = ""
    if (self.BetPotIndex == 255) then
        title_name = "庄家"
    else
        title_name = "池" .. self.BetPotIndex
    end
    self.GTextPotTitle.text = title_name
end

---------------------------------------
function UiDesktopHSetCardTypeItem:resetCardType()
    self.GComboBoxCardType.text = self.RandomCardTypeName
end

---------------------------------------
function UiDesktopHSetCardTypeItem:_onClick()
    local card_type = self.MapCardType[self.GComboBoxCardType.text]
    if (card_type ~= nil) then
        if (card_type ~= self.AllCardTypeCount) then
            self.ViewDesktopHSetCardType:setCurrentType(self.BetPotIndex, card_type);
        end
    end
end

---------------------------------------
-- 整个对话框
ViewDesktopHSetCardType = class(ViewBase)

---------------------------------------
function ViewDesktopHSetCardType:ctor()
    self.ViewDesktopH = nil
    self.GCoBankCardTypeParent = nil
    self.GListPotCardType = nil
    self.GBtnConfirmSetCardType = nil
    self.BankItemDesktopHSetCardType = nil
    self.MapPotItemDesktopHSetCardType = nil
    self.MapCardsType = nil
    self.BankPlayerPotIndex = 255
end

---------------------------------------
function ViewDesktopHSetCardType:OnCreate()
    self.ViewDesktopH = self.ViewMgr:GetView("DesktopH")
    self.GCoBankCardTypeParent = self.ComUi:GetChild("CoBankCardTypeParent").asCom
    self.GListPotCardType = self.ComUi:GetChild("ListPotCardType").asList
    self.GBtnConfirmSetCardType = self.ComUi:GetChild("BtnConfirm").asButton
    self.GBtnConfirmSetCardType.onClick:Add(
            function()
                self:_setCardType()
            end
    )
    local co_shade = self.ComUi:GetChild("CoShade").asCom
    co_shade.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
    self.MapPotItemDesktopHSetCardType = {}
    self.MapCardsType = {}
    local co_bankcardtype = CS.FairyGUI.UIPackage.CreateObject("DesktopHSetCardType", "ComboBoxCardType").asComboBox
    self.GCoBankCardTypeParent:AddChild(co_bankcardtype)
    self.BankItemDesktopHSetCardType = UiDesktopHSetCardTypeItem:new(nil)
    self.BankItemDesktopHSetCardType:OnCreate(co_bankcardtype, self.BankPlayerPotIndex, self.ViewDesktopH, self)

    for i = 0, self.ViewDesktopH.ControllerDesktopH.DesktopHBase:getMaxBetpotIndex() do
        local co_item = self.GListPotCardType:AddItemFromPool().asComboBox
        local l = UiDesktopHSetCardTypeItem:new(nil)
        l:OnCreate(co_item, i, self.ViewDesktopH, self)
        self.MapPotItemDesktopHSetCardType[i] = l
    end
end

---------------------------------------
function ViewDesktopHSetCardType:setCurrentType(pot_index, card_type)
    self.MapCardsType[tostring(pot_index)] = card_type
end

---------------------------------------
function ViewDesktopHSetCardType:_setCardType()
    if (self.ViewDesktopH.ControllerPlayer.IsGm == false) then
        return
    end

    local ev = self:GetEv("EvDesktopHundredChangeCardsType")
    if (ev == nil) then
        ev = EvDesktopHundredChangeCardsType:new(nil)
    end
    ev.map_card_types = self.MapCardsType
    self:SendEv(ev)

    self:_onClickBtnClose()
end

---------------------------------------
function ViewDesktopHSetCardType:_onClickBtnClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewDesktopHSetCardTypeFactory = class(ViewFactory)

---------------------------------------
function ViewDesktopHSetCardTypeFactory:CreateView()
    local view = ViewDesktopHSetCardType:new()
    return view
end