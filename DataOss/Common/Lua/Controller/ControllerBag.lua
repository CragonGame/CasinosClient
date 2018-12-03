-- Copyright(c) Cragon. All rights reserved.
-- 只剩MsgPack版的ItemData1

---------------------------------------
ControllerBag = class(ControllerBase)

---------------------------------------
function ControllerBag:ctor(this, controller_data, controller_name)
end

---------------------------------------
function ControllerBag:OnCreate()
    self.Rpc = self.ControllerMgr.Rpc
    self.MC = CommonMethodType
    -- 背包中所有道具推送给Client的通知
    self.Rpc:RegRpcMethod1(self.MC.BagItemPush2ClientNotify, function(list_item)
        self:s2cBagItemPush2ClientNotify(list_item)
    end)
    -- 临时礼物变更通知
    self.Rpc:RegRpcMethod1(self.MC.BagGiftChangedNotify, function(item_data)
        self:s2cBagGiftChangedNotify(item_data)
    end)
    -- 通知删除道具
    self.Rpc:RegRpcMethod2(self.MC.BagDeleteItemNotify, function(result, item_objid)
        self:s2cBagDeleteItemNotify(result, item_objid)
    end)
    -- 通知添加道具
    self.Rpc:RegRpcMethod1(self.MC.BagAddItemNotify, function(item_data)
        self:s2cBagAddItemNotify(item_data)
    end)
    -- 通知更新道具
    self.Rpc:RegRpcMethod1(self.MC.BagUpdateItemNotify, function(item_data)
        self:s2cBagUpdateItemNotify(item_data)
    end)
    self.Rpc:RegRpcMethod1(self.MC.BagOperateItemNotify, function(item_data)
        self:OnBagOperateItemNotify(item_data)
    end)

    self.ViewMgr:BindEvListener("EvUiRemoveItem", self)
    self.ViewMgr:BindEvListener("EvUiRequestOperateItem", self)
    self.ViewMgr:BindEvListener("EvBindWechat", self)
    self.ViewMgr:BindEvListener("EvUiLoginSuccessEx", self)
    self.ViewMgr:BindEvListener("EvOpenBag", self)

    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.MapItem = {}
    self.ListItemGiftNormal = {}
    self.ListItemConsume = {}
    self.CurrentGift = nil
    self:haveNewItem()
    local view_main = self.ViewMgr:GetView("Main")
    if view_main ~= nil then
        view_main:RefreshNewItem()
    end
end

---------------------------------------
function ControllerBag:OnDestroy()
    self.ViewMgr:UnbindEvListener(self)
    self.MapItem = {}
    self.MapItem = nil
    self.ListItemGiftNormal = {}
    self.ListItemGiftNormal = nil
    self.ListItemConsume = {}
    self.ListItemConsume = nil
    self.CurrentGift = nil
end

---------------------------------------
function ControllerBag:OnHandleEv(ev)
    if (ev.EventName == "EvUiRemoveItem")
    then
        local obj_id = ev.obj_id
        self:RequestRemoveGift(obj_id)
    elseif (ev.EventName == "EvUiRequestOperateItem")
    then
        self:requestOperateItem("", ev.ItemObjId)
    elseif (ev.EventName == "EvBindWechat")
    then
        self.BindAndUseItemObjId = ev.ItemObjId
    elseif (ev.EventName == "EvUiLoginSuccessEx")
    then
        if self.BindAndUseItemObjId ~= nil then
            self:requestOperateItem("", self.BindAndUseItemObjId)
            self.BindAndUseItemObjId = nil
        end
    elseif (ev.EventName == "EvOpenBag")
    then
        local newitem_key = "NewItem"
        if (CS.UnityEngine.PlayerPrefs.HasKey(newitem_key))
        then
            CS.UnityEngine.PlayerPrefs.DeleteKey(newitem_key)
        end
        self:haveNewItem()
    end
end

---------------------------------------
function ControllerBag:s2cBagItemPush2ClientNotify(list_item)
    if (list_item ~= nil and #list_item > 0)
    then
        local eb_data_mgr = self.ControllerMgr.TbDataMgr
        for i = 1, #list_item do
            local data = ItemData1:new(nil)
            data:setData(list_item[i])
            local item = Item:new(nil, eb_data_mgr, data)
            self.MapItem[item.ItemData.item_objid] = item
            if (item.UnitLink.UnitType == "GiftNormal")
            then
                table.insert(self.ListItemGiftNormal, item)
            elseif (item.UnitLink.UnitType == "Consume" or item.UnitLink.UnitType == "GoodsVoucher" or item.UnitLink.UnitType == "GoldPackage" or item.UnitLink.UnitType == "WechatRedEnvelopes")
            then
                table.insert(self.ListItemConsume, item)
            end
        end
    end
    self:sortListBagItem()
end

---------------------------------------
function ControllerBag:s2cBagGiftChangedNotify(item_data)
    local data = nil
    if (item_data ~= nil)
    then
        data = ItemData1:new(nil)
        data:setData(item_data)
    end
    if (data == nil or CS.System.String.IsNullOrEmpty(data.item_objid) or data.map_unit_data == nil)
    then
        self.CurrentGift = nil
    else
        local eb_data_mgr = self.ControllerMgr.TbDataMgr
        self.CurrentGift = Item:new(nil, eb_data_mgr, data)
    end

    local ev = self:GetEv("EvEntityCurrentTmpGiftChange")
    if (ev == nil)
    then
        ev = EvEntityCurrentTmpGiftChange:new(nil)
    end
    self:SendEv(ev)
end

---------------------------------------
function ControllerBag:s2cBagDeleteItemNotify(result, item_objid)
    if (result ~= ProtocolResult.Success)
    then
        ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("RemoveItemFailed"))
        return
    end
    for key, value in pairs(self.MapItem) do
        if (key == item_objid)
        then
            value = nil
            key = nil
            break
        end
    end
    self:removeItemFromList(self.ListItemGiftNormal, item_objid)
    self:removeItemFromList(self.ListItemConsume, item_objid)
    local ev = self:GetEv("EvEntityBagDeleteItem")
    if (ev == nil)
    then
        ev = EvEntityBagDeleteItem:new(nil)
    end
    ev.item_objid = item_objid
    self:SendEv(ev)
end

---------------------------------------
function ControllerBag:OnBagOperateItemNotify(result)
    self:s2cOnOperateItem(result)
end

---------------------------------------
function ControllerBag:s2cOnOperateItem(result)
    local n_d = ItemOperateNotifyData:new(nil)
    n_d:setData(result)
    if (n_d.result ~= ProtocolResult.Success)
    then
        ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("UseItemFailed"))
        return
    end
    ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("UseItemSuccess"))

    --[[local item = nil
    for key,value in pairs(self.MapItem) do
        if(key == result.item_objid)
        then
            item = value
            break
        end
    end
    if (item == nil)
    then
        return
    end
    local ev = self:GetEv("EvEntityBagOperateItem")
    if(ev == nil)
    then
        ev = EvEntityBagOperateItem:new(nil)
    end
    ev.item = item
    self:SendEv(ev)]]
end

---------------------------------------
function ControllerBag:s2cBagAddItemNotify(item_data)
    local data = ItemData1:new(nil)
    data:setData(item_data)
    local eb_data_mgr = self.ControllerMgr.TbDataMgr
    local item = Item:new(nil, eb_data_mgr, data)
    self.MapItem[item.ItemData.item_objid] = item
    ViewHelper:UiShowInfoSuccess(string.format(self.ViewMgr.LanMgr:getLanValue("ReceiveItems"), self.ViewMgr.LanMgr:getLanValue(item.TbDataItem.Name)))
    if (item.UnitLink.UnitType == "GiftNormal")
    then
        table.insert(self.ListItemGiftNormal, item)
        self:sortListBagItem()
    elseif (item.UnitLink.UnitType == "Consume" or item.UnitLink.UnitType == "GoodsVoucher" or item.UnitLink.UnitType == "GoldPackage" or item.UnitLink.UnitType == "WechatRedEnvelopes")
    then
        table.insert(self.ListItemConsume, item)
    end

    if (item.UnitLink.UnitType == "GiftNormal")
    then
        local gift_normal = item.UnitLink

        if (self.Guid ~= gift_normal.GivePlayerEtGuid)
        then
            if (gift_normal.GiveBy ~= self.Guid)
            then
                local msg = CS.System.String.Format("%s%s：%s", gift_normal.GiveBy, self.ViewMgr.LanMgr:getLanValue("SendItems"),
                        item.TbDataItem.Name)
                local msg_box = self.ViewMgr:CreateView("MsgBox")
                msg_box:showMsgBox1(self.ViewMgr.LanMgr:getLanValue("ReceiveItems"), msg, nil)
            end
        end
    end

    local newitem_key = "NewItem"
    local newitem_num = 0
    if (CS.UnityEngine.PlayerPrefs.HasKey(newitem_key))
    then
        newitem_num = CS.UnityEngine.PlayerPrefs.GetInt(newitem_key)
    end
    newitem_num = newitem_num + 1
    CS.UnityEngine.PlayerPrefs.SetInt(newitem_key, newitem_num)

    self:haveNewItem()
    local ev = self:GetEv("EvEntityBagAddItem")
    if (ev == nil)
    then
        ev = EvEntityBagAddItem:new(nil)
    end
    ev.item = item
    self:SendEv(ev)
end

---------------------------------------
function ControllerBag:s2cBagUpdateItemNotify(item_data)
    local data = ItemData1:new(nil)
    data:setData(item_data)
    local eb_data_mgr = self.ControllerMgr.TbDataMgr
    local item = Item:new(nil, eb_data_mgr, data)
    self.MapItem[item.ItemData.item_objid] = item
    self:updateItem(self.ListItemGiftNormal, item)
    self:updateItem(self.ListItemConsume, item)
    local msg = self.ViewMgr.LanMgr:getLanValue("UpdateProp") .. self.ControllerMgr.LanMgr:getLanValue(item.TbDataItem.Name)
    ViewHelper:UiShowInfoSuccess(msg)

    local ev = self:GetEv("EvEntityBagUpdateItem")
    if (ev == nil)
    then
        ev = EvEntityBagUpdateItem:new(nil)
    end
    ev.item = item
    self:SendEv(ev)

    return item
end

---------------------------------------
-- 请求使用道具
function ControllerBag:requestOperateItem(operate_id, item_objid)
    local item_operate = ItemOperate:new(nil)
    item_operate.operate_id = operate_id
    item_operate.item_objid = item_objid
    self.Rpc:RPC1(self.MC.BagOperateItemRequest, item_operate:getData4Pack())
end

---------------------------------------
function ControllerBag:getAlreadyHaveItemCount(item_tbid)
    local item_count = 0
    for key, value in pairs(self.MapItem) do
        if (value.TbDataItem.Id == item_tbid)
        then
            item_count = item_count + value.ItemData.count
        end
    end

    return item_count
end

---------------------------------------
function ControllerBag:RequestRemoveGift(item_objid)
    self.Rpc:RPC1(self.MC.BagDeleteItemRequest, item_objid)
end

---------------------------------------
-- 普通礼物根据价格排序显示
function ControllerBag:sortListBagItem()
    table.sort(self.ListItemGiftNormal,
            function(x, y)
                local price_typex = x.TbDataItem.PriceType
                local price_typey = y.TbDataItem.PriceType
                if (price_typex ~= price_typey)
                then
                    return price_typex > price_typey
                else
                    local price_x = x.TbDataItem.Price
                    local price_y = y.TbDataItem.Price
                    if (price_x ~= price_y)
                    then
                        return price_x > price_y
                    else
                        local gift_normalx = x.UnitLink
                        local gift_normaly = y.UnitLink
                        return gift_normalx.CreateTime > gift_normaly.CreateTime
                    end
                end
            end
    )
end

---------------------------------------
function ControllerBag:removeItemFromList(list_item, item_objid)
    local item = nil
    local item_key = nil
    for key, value in pairs(list_item) do
        if (value.ItemData.item_objid == item_objid)
        then
            item = value
            item_key = key
            break
        end
    end
    if (item ~= nil)
    then
        table.remove(list_item, item_key)
    end
end

---------------------------------------
function ControllerBag:updateItem(list_item, item)
    local item_index = nil
    for key, value in pairs(list_item) do
        if (value.ItemData.item_objid == item.ItemData.item_objid)
        then
            item_index = key
            break
        end
    end
    if (item_index ~= nil)
    then
        table.remove(list_item, item_index)
        table.insert(list_item, item_index, item)
        self:sortListBagItem()
    end
end

---------------------------------------
function ControllerBag:haveItem(tb_id)
    local l = self.ListItemConsume
    local have_i = false
    local item = nil
    for i, v in pairs(l) do
        if v.TbDataItem.Id == tb_id then
            have_i = true
            item = v
            break
        end
    end

    return have_i, item
end

---------------------------------------
function ControllerBag:haveNewItem()
    local newitem_key = "NewItem"
    local newitem_num = 0
    if (CS.UnityEngine.PlayerPrefs.HasKey(newitem_key))
    then
        newitem_num = CS.UnityEngine.PlayerPrefs.GetInt(newitem_key)
    end
    if newitem_num > 0 then
        self.HaveNewItem = true
    else
        self.HaveNewItem = false
    end
end

---------------------------------------
ControllerBagFactory = class(ControllerFactory)

function ControllerBagFactory:GetName()
    return 'Bag'
end

function ControllerBagFactory:CreateController(controller_data)
    local ctrl_name = self:GetName()
    local ctrl = ControllerBag:new(controller_data, ctrl_name)
    return ctrl
end