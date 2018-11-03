-- Copyright(c) Cragon. All rights reserved.
-- 一个Item

---------------------------------------
ItemDesktopHSetCardType = {}

---------------------------------------
function ItemDesktopHSetCardType:new(o)
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
function ItemDesktopHSetCardType:OnCreate(co_card_type, betpot_index,
                                          ui_desktoph, ui_card_type)
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
                self:onClick()
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
function ItemDesktopHSetCardType:resetCardType()
    self.GComboBoxCardType.text = self.RandomCardTypeName
end

---------------------------------------
function ItemDesktopHSetCardType:onClick()
    local card_type = self.MapCardType[self.GComboBoxCardType.text]
    if (card_type ~= nil) then
        if (card_type ~= self.AllCardTypeCount)
        then
            self.ViewDesktopHSetCardType:setCurrentType(self.BetPotIndex, card_type);
        end
    end
end