-- Copyright(c) Cragon. All rights reserved.
-- 管理礼物图标，魔法表情，弹幕Item，私聊群聊消息Item，私聊聊天对象

---------------------------------------
ViewPool = ViewBase:new()

---------------------------------------
function ViewPool:new(o)
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
    return o
end

---------------------------------------
function ViewPool:OnCreate()
    self.ItemShootingTextPool = ItemShootingTextPool:new(nil, self)
    if (CS.Casinos.CasinosContext.Instance.Config.HaveMagicExp == true) then
        self.ItemMagicExpPool = ItemMagicExpSenderPool:new(nil, self)
    end
    self.ItemChatExPool = ItemChatExPool:new(nil, self)
    self.ItemChatTargetInfoPool = ItemChatTargetInfoPool:new(nil, self)
    self.ItemGiftPool = ItemGiftPool:new(nil, self)
    self.GoUi:SetActive(false)
end

---------------------------------------
function ViewPool:getShootingTextItem()
    local item = self.ItemShootingTextPool:getShootingTextItem()
    return item
end

---------------------------------------
function ViewPool:shootingTextEnque(item)
    self.ItemShootingTextPool:shootingTextEnque(item)
end

---------------------------------------
function ViewPool:getMagicExpSender()
    local item = self.ItemMagicExpPool:getMagicSender()
    return item
end

---------------------------------------
function ViewPool:magicExpEnque(item)
    self.ItemMagicExpPool:magicExpEnque(item)
end

---------------------------------------
function ViewPool:getItemChatEx(obj)
    local item = self.ItemChatExPool:getChatEx(obj)
    return item
end

---------------------------------------
function ViewPool:itemChatExEnque(item)
    self.ItemMagicExpPool:itemChatExEnque(item)
end

---------------------------------------
function ViewPool:getItemChatTargetInfo()
    local item = self.ItemChatTargetInfoPool:getChatTargetInfo()
    return item
end

---------------------------------------
function ViewPool:chatTargetInfoEnque(item)
    self.ItemChatTargetInfoPool:chatTargetInfoEnque(item)
end

---------------------------------------
function ViewPool:getItemGift(obj)
    local item = self.ItemGiftPool:getItemGift(obj)
    return item
end

---------------------------------------
function ViewPool:itemGiftEnque(item)
    self.ItemGiftPool:itemGiftEnque(item)
end

---------------------------------------
function ViewPool:itemGiftAllEnque()
    self.ItemGiftPool:itemGiftAllEnque()
end

---------------------------------------
ItemMagicExpSenderPool = {}

---------------------------------------
function ItemMagicExpSenderPool:new(o, ui_pool)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.UiPool = ui_pool
    self.Que = {}
    for i = 1, 20 do
        self:createMagicExp()
    end
    return o
end

---------------------------------------
function ItemMagicExpSenderPool:getMagicSender()
    if (#self.Que <= 0) then
        self:createMagicExp()
    end
    local item = table.remove(self.Que, 1)
    return item
end

---------------------------------------
function ItemMagicExpSenderPool:magicExpEnque(item)
    if (item.GCoMagicExpSender.displayObject ~= nil and item.GCoMagicExpSender.displayObject.gameObject ~= nil) then
        self.UiPool.ComUi:AddChild(item.GCoMagicExpSender)
        table.insert(self.Que, item)
    end
end

---------------------------------------
function ItemMagicExpSenderPool:createMagicExp()
    local item_shootingtext = ItemMagicExpSender:new(nil, self.UiPool.ViewMgr)
    self.UiPool.ComUi:AddChild(item_shootingtext.GCoMagicExpSender)
    table.insert(self.Que, item_shootingtext)
end

---------------------------------------
ItemShootingTextPool = {}

---------------------------------------
function ItemShootingTextPool:new(o, ui_pool)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.UiPool = ui_pool
    self.QueShootingText = {}
    for i = 1, 50 do
        self:createShootingText()
    end
    return o
end

---------------------------------------
function ItemShootingTextPool:getShootingTextItem()
    if (#self.QueShootingText <= 0) then
        self:createShootingText()
    end
    local item = table.remove(self.QueShootingText, 1)
    item.displayObject.gameObject:SetActive(true)
    item.visible = true
    return item
end

---------------------------------------
function ItemShootingTextPool:shootingTextEnque(item)
    if (item.Com.displayObject ~= nil and item.Com.displayObject.gameObject ~= nil
            and self.UiPool.ComUi.displayObject ~= nil
            and self.UiPool.ComUi.displayObject.gameObject ~= nil) then
        self.UiPool.ComUi:AddChild(item.Com)
        item:Reset()
        item.Com.displayObject.gameObject:SetActive(false)
        table.insert(self.QueShootingText, item)
    end
end

---------------------------------------
-- 内部函数，外部直接使用getShootingTextItem
function ItemShootingTextPool:createShootingText()
    local co_text = CS.FairyGUI.UIPackage.CreateObject("Common", "ComChatText").asCom
    local item_shootingtext = ItemChatEx:new(nil)
    item_shootingtext:setObj(co_text)
    co_text.displayObject.gameObject:SetActive(false)
    self.UiPool.ComUi:AddChild(co_text)
    table.insert(self.QueShootingText, item_shootingtext)
end

---------------------------------------
ItemChatExPool = {}

---------------------------------------
function ItemChatExPool:new(o, ui_pool)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.UiPool = ui_pool
    self.UsedItem = {}
    self.Que = {}
    for i = 1, 20 do
        self:createChatEx()
    end
    return o
end

---------------------------------------
function ItemChatExPool:getChatEx(obj)
    local item = self.UsedItem[obj]
    if (item == nil) then
        if (#self.Que <= 0) then
            self:createChatEx()
        end
        item = table.remove(self.Que, 1)
        self.UsedItem[obj] = item
        item:setObj(obj)
    end
    return item
end

---------------------------------------
function ItemChatExPool:chatExEnque(item)
    item:reset1()
    LuaHelper:TableRemoveV(self.UsedItem, item)
    table.insert(self.Que, item)
end

---------------------------------------
function ItemChatExPool:createChatEx()
    local item_shootingtext = ItemChatEx:new(nil)
    table.insert(self.Que, item_shootingtext)
end

---------------------------------------
ItemChatTargetInfoPool = {}

---------------------------------------
function ItemChatTargetInfoPool:new(o, ui_pool)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.UiPool = ui_pool
    self.Que = {}
    for i = 1, 20 do
        self:createChatTargetInfo()
    end
    return o
end

---------------------------------------
function ItemChatTargetInfoPool:getChatTargetInfo()
    if (#self.Que <= 0) then
        self:createChatTargetInfo()
    end
    local item = table.remove(self.Que, 1)
    return item
end

---------------------------------------
function ItemChatTargetInfoPool:chatTargetInfoEnque(item)
    item:Reset()
    table.insert(self.Que, item)
end

---------------------------------------
function ItemChatTargetInfoPool:createChatTargetInfo()
    local co_text = CS.FairyGUI.UIPackage.CreateObject("ChatFriend", "ComChatTarget").asCom
    local item_shootingtext = ItemChatTargetInfo:new(nil, co_text)
    table.insert(self.Que, item_shootingtext)
end

---------------------------------------
ItemGiftPool = {}

---------------------------------------
function ItemGiftPool:new(o, ui_pool)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.UiPool = ui_pool
    self.UsedItem = {}
    self.Que = {}
    for i = 1, 20 do
        self:createItemGift()
    end
    return o
end

---------------------------------------
function ItemGiftPool:getItemGift(obj)
    local item = self.UsedItem[obj]
    if (item == nil) then
        if (#self.Que <= 0) then
            self:createItemGift()
        end
        item = table.remove(self.Que, 1)
        self.UsedItem[obj] = item
    end
    return item
end

---------------------------------------
function ItemGiftPool:itemGiftEnque(item)
    item:Reset()
    LuaHelper:TableRemoveV(self.UsedItem, item)
    table.insert(self.Que, item)
end

---------------------------------------
function ItemGiftPool:itemGiftAllEnque()
    for i, v in pairs(self.UsedItem) do
        v:Reset()
        table.insert(self.Que, v)
    end
    self.UsedItem = {}
end

---------------------------------------
function ItemGiftPool:createItemGift()
    local item_shootingtext = ItemGift:new(nil)
    table.insert(self.Que, item_shootingtext)
end

---------------------------------------
ViewPoolFactory = ViewFactory:new()

---------------------------------------
function ViewPoolFactory:new(o, ui_package_name, ui_component_name, ui_layer, is_single, fit_screen)
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
function ViewPoolFactory:CreateView()
    local view = ViewPool:new(nil)
    return view
end