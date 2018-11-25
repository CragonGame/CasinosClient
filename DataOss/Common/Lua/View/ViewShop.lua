-- Copyright(c) Cragon. All rights reserved.
-- 商城界面，与ControllerTrade交互

---------------------------------------
GoodsType = {
    Gift1 = 1, --礼券兑换
    Gift2 = 2, --礼品券兑换
    Gift3 = 3, --积分兑换
}

---------------------------------------
ViewShop = ViewBase:new()

---------------------------------------
function ViewShop:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.Context = Context
    self.ViewMgr = nil
    self.GoUi = nil
    self.ComUi = nil
    self.Panel = nil
    self.UILayer = nil
    self.InitDepth = nil
    self.ViewKey = nil
    self.TemporaryHideItemId = 14001
    return o
end

---------------------------------------
function ViewShop:OnCreate()
    self.ViewMgr:BindEvListener("EvEntityGoldChanged", self)
    self.ViewMgr:BindEvListener("EvEntityDiamondChanged", self)
    self.ViewMgr:BindEvListener("EvEntityPointChanged", self)
    self.GTransitionShow = self.ComUi:GetTransition("TransitionShow")
    self.GTransitionShow:Play()
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.ControllerActor = self.ViewMgr.ControllerMgr:GetController("Actor")
    self.MapShopDiamond = {}
    self.MapShopGold = {}
    self.MapShopItemCosume = {}
    self.MapShopItemVip = {}
    self.ControllerShop = self.ComUi:GetController("ControllerShop")
    local btn_return = self.ComUi:GetChild("BtnReturn").asButton
    btn_return.onClick:Add(
            function()
                self:onClickBtnReturn()
            end
    )

    local com_tabGift = self.ComUi:GetChild("ComTabPoint").asCom
    self.GControllerTabGift = com_tabGift:GetController("ControllerTab")
    local btn_gift1 = com_tabGift:GetChild("BtnGift1").asButton
    btn_gift1.onClick:Add(
            function(a)
                self:setListGoods(GoodsType.Gift1)
            end
    )
    local btn_gift2 = com_tabGift:GetChild("BtnGift2").asButton
    btn_gift2.onClick:Add(
            function(a)
                self:setListGoods(GoodsType.Gift2)
            end
    )
    local btn_starPoint = com_tabGift:GetChild("BtnStarPoint").asButton
    btn_starPoint.onClick:Add(
            function(a)
                self:setListGoods(GoodsType.Gift3)
            end
    )
    local com_point = com_tabGift:GetChild("ComPointAndStar").asCom
    self.GTextPoint = com_point:GetChild("TextPoint").asTextField
    local btn_levelupVip = self.ComUi:GetChild("Lan_Btn_Upgrade").asButton
    btn_levelupVip.onClick:Add(
            function()
                self:onClickUpdateVIP()
            end
    )

    local btn_editAddress = self.ComUi:GetChild("Lan_Btn_ReceivingAddress").asButton
    btn_editAddress.onClick:Add(
            function()
                self:onClickBtnEditAddress()
            end
    )

    self.GListChip = self.ComUi:GetChild("ListChip").asList
    self.GListDiamond = self.ComUi:GetChild("ListDiamond").asList
    self.GListConsume = self.ComUi:GetChild("ListConsume").asList
    self.GListGoods = self.ComUi:GetChild("ListGoods").asList
    self.GlistVip = self.ComUi:GetChild("ListVip").asList
    local co_vipcurrent = self.ComUi:GetChild("CoVIPCurrent").asCom
    self.VIPSignCurrent = ViewVIPSign:new(nil, co_vipcurrent)
    local co_vipnext = self.ComUi:GetChild("CoVIPNext").asCom
    self.VIPSignNext = ViewVIPSign:new(nil, co_vipnext)
    self.GTextVIPTips = self.ComUi:GetChild("VIPTips").asTextField
    self.GProVIP = self.ComUi:GetChild("ProVIP").asProgress
    self.ChipIconSolustion = self.ComUi:GetController("ChipIconSolustion")
    self.ChipIconSolustion.selectedIndex = self.Context.Cfg.ChipIconSolustion
    local group_0 = self.ComUi:GetChild("GroupTab0").asGroup
    local group_1 = self.ComUi:GetChild("GroupTab1").asGroup
    local group = group_0
    local group_0_ex = self.ComUi:GetChild("Group0").asGroup
    local group_1_ex = self.ComUi:GetChild("Group1").asGroup
    local group_ex = group_0_ex
    if self.Context.Cfg.ChipIconSolustion == 0 then
        local btn_tabdiomand = self.ComUi:GetChild("BtnTabDiamond").asButton
        btn_tabdiomand.onClick:Add(
                function()
                    self:onClickBtnDiomand()
                end
        )
        local btn_tabchip = self.ComUi:GetChild("BtnTabChip").asButton
        btn_tabchip.onClick:Add(
                function()
                    self:onClickBtnTabChip()
                end
        )
        self.ControllerShop.selectedIndex = 1
        self:createDiamond()
    else
        group = group_1
        group_ex = group_1_ex
        self.ControllerShop.selectedIndex = 2
        self:createConsume()
    end
    local com_taball = self.ComUi:GetChildInGroup(group, "ComTabAll").asCom
    self.ControllerTab = com_taball:GetController("ControllerTab")
    local btn_tabconsume = self.ComUi:GetChildInGroup(group, "BtnTabConsume").asButton
    btn_tabconsume.onClick:Add(
            function()
                self:onClickBtnTabConsume()
            end
    )
    local btn_tabvip = self.ComUi:GetChildInGroup(group, "BtnTabVip").asButton
    btn_tabvip.onClick:Add(
            function()
                self:onClickBtnVip()
            end
    )
    local btn_tabGift = self.ComUi:GetChildInGroup(group, "BtnTabGift").asButton
    btn_tabGift.onClick:Add(
            function()
                self:onClickBtnTabGift()
            end
    )

    local btn_addChip = self.ComUi:GetChildInGroup(group_ex, "BtnAddChip").asButton
    btn_addChip.onClick:Add(
            function()
                self:onClickBtnTabChip()
            end
    )
    local btn_addDiamond = self.ComUi:GetChildInGroup(group_ex, "BtnAddDiamond").asButton
    btn_addDiamond.onClick:Add(
            function()
                self:onClickBtnDiomand()
            end
    )
    self.GTextSelfGold = btn_addChip:GetChild("TextChipAmount").asTextField
    self.GTextSelfDiamond = btn_addDiamond:GetChild("TextDiamondAmount").asTextField
    self:setPlayerGoldAndDiamond()
    local bg = self.ComUi:GetChild("Bg")
    if (bg ~= nil)
    then
        ViewHelper:MakeUiBgFiteScreen(ViewMgr.STANDARD_WIDTH, ViewMgr.STANDARD_HEIGHT, self.ComUi.width, self.ComUi.height, bg.width, bg.height, bg, BgAttachMode.Center)
    end
end

---------------------------------------
function ViewShop:OnDestroy()
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ViewShop:OnHandleEv(ev)
    if (ev.EventName == "EvEntityGoldChanged") then
        self:setPlayerGoldAndDiamond()
    elseif (ev.EventName == "EvEntityDiamondChanged") then
        self:setPlayerGoldAndDiamond()
    elseif (ev.EventName == "EvEntityPointChanged") then
        self:setPlayerGoldAndDiamond()
    end
end

---------------------------------------
function ViewShop:showItem()
    self:onClickBtnTabConsume()
end

---------------------------------------
function ViewShop:showGold()
    self:onClickBtnTabChip()
end

---------------------------------------
function ViewShop:showDiamond()
    self:onClickBtnDiomand()
end

---------------------------------------
function ViewShop:showVIP()
    self:onClickBtnVip()
end

---------------------------------------
function ViewShop:setPlayerGoldAndDiamond()
    self.GTextSelfGold.text = UiChipShowHelper:getGoldShowStr(self.ControllerActor.PropGoldAcc:get(), self.ViewMgr.LanMgr.LanBase)
    self.GTextSelfDiamond.text = UiChipShowHelper:getGoldShowStr(self.ControllerActor.PropDiamond:get(), self.ViewMgr.LanMgr.LanBase, false)
    self.GTextPoint.text = UiChipShowHelper:getGoldShowStr(self.ControllerActor.PropPoint:get(), self.ViewMgr.LanMgr.LanBase, false)
end

---------------------------------------
function ViewShop:onClickBtnReturn()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewShop:onClickBtnDiomand()
    if self.Context.Cfg.ChipIconSolustion == 1 then
        self.ViewMgr:CreateView("Purse")
    else
        self.ControllerShop.selectedIndex = 1
        self.ControllerTab.selectedIndex = 0
        self:createDiamond()
    end
end

---------------------------------------
function ViewShop:onClickBtnTabChip()
    if self.Context.Cfg.ChipIconSolustion == 1 then
        self.ViewMgr:CreateView("Purse")
    else
        self.ControllerShop.selectedIndex = 0
        self.ControllerTab.selectedIndex = 1
        self:createChip()
    end
end

---------------------------------------
function ViewShop:onClickBtnTabConsume()
    self.ControllerShop.selectedIndex = 2
    local s_index = 2
    if self.Context.Cfg.ChipIconSolustion == 1 then
        s_index = 0
    end
    self.ControllerTab.selectedIndex = s_index
    self:createConsume()
end

---------------------------------------
function ViewShop:onClickBtnVip()
    self.ControllerShop.selectedIndex = 3
    local s_index = 3
    if self.Context.Cfg.ChipIconSolustion == 1 then
        s_index = 1
    end
    self.ControllerTab.selectedIndex = s_index
    self:createVIP()
    self.VIPSignCurrent:setVIPLevel(self.ControllerActor.PropVIPLevel:get(), true)
    local next_level = -1
    local next_needtotalexp = 0
    local next_alltotalexp = 0
    local return_value = nil
    next_level, next_needtotalexp, next_alltotalexp = self.ControllerActor:getNextVIPInfo()
    local provip_value = 0
    local tips = ""
    if (next_level ~= -1) then
        self.VIPSignNext:setVIPLevel(next_level, true)
        local level_cur = self.ControllerActor.PropVIPLevel:get()
        local tb_actorlevel_cur = self.CasinosContext.TbDataMgrLua:GetData("VIPLevel", level_cur)
        local exp_cur = self.ControllerActor.PropRechargePoint:get()
        if (tb_actorlevel_cur ~= nil) then
            exp_cur = exp_cur - tb_actorlevel_cur.VIPPoint
            next_alltotalexp = next_alltotalexp - tb_actorlevel_cur.VIPPoint
        end
        provip_value = (exp_cur * 100) / next_alltotalexp
        tips = string.format(self.ViewMgr.LanMgr:getLanValue("ShopVipTips"), next_needtotalexp, "VIP" .. next_level)
    end

    self.GProVIP.value = provip_value
    self.GTextVIPTips.text = tips
end

---------------------------------------
function ViewShop:onClickBtnTabGift()
    self.ControllerShop.selectedIndex = 4
    local s_index = 4
    if self.Context.Cfg.ChipIconSolustion == 1 then
        s_index = 2
    end
    self.ControllerTab.selectedIndex = s_index
    self:setListGoods(GoodsType.Gift1)
end

---------------------------------------
function ViewShop:createDiamond()
    if (LuaHelper:GetTableCount(self.MapShopDiamond) > 0) then
        return
    end
    local map_tbitem = self.CasinosContext.TbDataMgrLua:GetMapData("Item")
    for key, value in pairs(map_tbitem) do
        local tb_item = value
        if (tb_item.UnitType == "Billing") then
            local tb_itemtype = self.CasinosContext.TbDataMgrLua:GetData("ItemType", tb_item.ItemTypeTbId)
            if (tb_itemtype.TypeName == "Diamond") then
                local co_diamond = self.GListDiamond:AddItemFromPool().asCom
                local ui_diamond = ItemUiShopDiamond:new(nil, self, co_diamond, tb_item)
                self.MapShopDiamond[key] = ui_diamond
            end
        end
    end
end

---------------------------------------
function ViewShop:createChip()
    if (LuaHelper:GetTableCount(self.MapShopGold) > 0) then
        return
    end
    local map_tbitem = self.CasinosContext.TbDataMgrLua:GetMapData("Item")
    local t = {}
    for i, v in pairs(map_tbitem) do
        table.insert(t, v)
    end
    table.sort(t, function(first, second)
        if first.Price ~= second.Price then
            return first.Price < second.Price
        end
        return first.Price < second.Price
    end)
    for key, value in pairs(t) do
        local tb_item = value
        if (tb_item.UnitType == "GoldPackage") then
            if (tb_item.PriceType == PriceType.Gold) then
                local co_gold = self.GListChip:AddItemFromPool().asCom
                local ui_diamond = ItemUiShopGold:new(nil, self, co_gold, tb_item)
                self.MapShopGold[key] = ui_diamond
            end
        end
    end
end

---------------------------------------
function ViewShop:createConsume()
    if (LuaHelper:GetTableCount(self.MapShopItemCosume) > 0) then
        return
    end
    local map_tbitem = self.CasinosContext.TbDataMgrLua:GetMapData("Item")
    for key, value in pairs(map_tbitem) do
        local tb_item = value
        if (tb_item.UnitType == "Consume" and tb_item.Id ~= self.TemporaryHideItemId) then
            local co_item = self.GListConsume:AddItemFromPool().asCom
            local ui_itemcosume = ItemUiShopConsume:new(nil, self, co_item, tb_item)
            self.MapShopItemCosume[key] = ui_itemcosume
        end
    end
end

---------------------------------------
function ViewShop:createVIP()
    if (LuaHelper:GetTableCount(self.MapShopItemVip) > 0) then
        return
    end

    local map_tbvip = self.CasinosContext.TbDataMgrLua:GetMapData("VIPLevel")
    for key, value in pairs(map_tbvip) do
        local tb_item = value
        local co_vip = self.GlistVip:AddItemFromPool().asCom
        local ui_vip = ItemUiShopVIPInfo:new(nil, self, co_vip, tb_item)
        self.MapShopItemVip[key] = ui_vip
    end
end

---------------------------------------
function ViewShop:onClickUpdateVIP()
    self:onClickBtnDiomand()
end

---------------------------------------
function ViewShop:onClickBtnEditAddress()
    self.ViewMgr:CreateView("EditAddress")
end

---------------------------------------
function ViewShop:setListGoods(goodsType)
    self.GListGoods:RemoveChildrenToPool()
    if (goodsType == GoodsType.Gift1) then
        self.GControllerTabGift:SetSelectedIndex(0)
        local map_tbitem = self.CasinosContext.TbDataMgrLua:GetMapData("Item")
        local t = {}
        for i, v in pairs(map_tbitem) do
            table.insert(t, v)
        end
        table.sort(t, function(first, second)
            if first.Price ~= second.Price then
                return first.Price < second.Price
            end
            return first.Price < second.Price
        end)

        for key, value in pairs(t) do
            local tb_item = value
            if (tb_item.UnitType == "GoodsVoucher") then
                local com = self.GListGoods:AddItemFromPool().asCom
                local discirbe = {}
                discirbe[1] = "[size=25]\n[/size]" -- 空25像素
                discirbe[2] = "[size=25]"
                discirbe[3] = self.ViewMgr.LanMgr:getLanValue(tb_item.Name)
                discirbe[4] = "[/size]\n[size=18][color=#A4BCEE]兑换券 x1[/color][/size]"
                local url = CS.Casinos.CasinosContext.Instance.PathMgr.DirAbItem .. tostring(tb_item.Icon) .. ".ab"
                local btn_title = self.ViewMgr.LanMgr:getLanValue("Exchange")
                local item = ItemUiShopGoods:new(nil, com, table.concat(discirbe), url, btn_title,
                        function()
                            local controller_bag = self.ViewMgr.ControllerMgr:GetController("Bag")
                            local have_i, item_ex = controller_bag:haveItem(tb_item.Id)
                            if (have_i) then
                                --请求使用实物兑换券兑换实物
                                local ev = self.ViewMgr:GetEv("EvUiRequestOperateItem")
                                if (ev == nil) then
                                    ev = EvUiRequestOperateItem:new(nil)
                                end
                                ev.ItemObjId = item_ex.ItemData.item_objid
                                self.ViewMgr:SendEv(ev)
                            else
                                ViewHelper:UiShowInfoFailed(self.ViewMgr.ControllerMgr.LanMgr:getLanValue("NotEnoughVoucher"))
                            end
                        end
                )
            end
        end
    elseif (goodsType == GoodsType.Gift2) then
        self.GControllerTabGift:SetSelectedIndex(1)
    elseif (goodsType == GoodsType.Gift3) then
        self.GControllerTabGift:SetSelectedIndex(2)
        local map_tbitem = self.CasinosContext.TbDataMgrLua:GetMapData("Item")
        local t = {}
        for i, v in pairs(map_tbitem) do
            table.insert(t, v)
        end
        table.sort(t, function(first, second)
            if first.Price ~= second.Price then
                return first.Price < second.Price
            end
            return first.Price < second.Price
        end)
        for key, value in pairs(t) do
            local tb_item = value
            if (tb_item.PriceType == 4) then
                local list_dataChip = {}
                local list_dataCertificate = {}
                local com = self.GListGoods:AddItemFromPool().asCom
                if (tb_item.UnitType == "GoldPackage") then
                    local unit_data = self.CasinosContext.TbDataMgrLua:GetData("UnitGoldPackage", tb_item.Id)
                    local gold_value = unit_data.GoldValue
                    local discirbe = {}
                    discirbe[1] = "[size=25]\n送<img src='ui://Common/WhiteChipIcon'width = '31'height = '31'/>"
                    discirbe[2] = tostring(gold_value)
                    discirbe[3] = "万[/size]\n"
                    --discirbe[4] = "[size=5]\n \n[size]" -- 空5像素
                    discirbe[4] = "[size=18][color=#A4BCEE]需要积分 x"
                    discirbe[5] = tostring(tb_item.Price)
                    discirbe[6] = "万[/color][/size]"
                    local url = self.CasinosContext.PathMgr.DirAbItem .. tostring(tb_item.Icon) .. ".ab"
                    local btn_title = self.ViewMgr.LanMgr:getLanValue("Exchange")
                    local item = ItemUiShopGoods:new(nil, com, table.concat(discirbe), url, btn_title,
                            function()
                                --请求使用积分兑换筹码
                                local ev = self.ViewMgr:GetEv("EvUiBuyItem")
                                if (ev == nil) then
                                    ev = EvUiBuyItem:new(nil)
                                end
                                ev.item_id = tb_item.Id
                                ev.to_etguid = self.ControllerActor.Guid
                                self.ViewMgr:SendEv(ev)
                            end
                    )
                elseif (tb_item.UnitType == "GoodsVoucher") then
                    local discirbe = {}
                    discirbe[1] = "[size=25]\n[/size]" -- 空25像素
                    discirbe[2] = "[size=25]"
                    discirbe[3] = self.ViewMgr.LanMgr:getLanValue(tb_item.Name)
                    discirbe[4] = "[/size]\n"
                    --discirbe[5] = "[size=5]\n \n[size]" -- 空5像素
                    discirbe[5] = "[size=18][color=#A4BCEE]需要积分 x"
                    discirbe[6] = tostring(tb_item.Price)
                    discirbe[7] = "万[/color][/size]"
                    local url = CS.Casinos.CasinosContext.Instance.PathMgr.DirAbItem .. tostring(tb_item.Icon) .. ".ab"
                    local btn_title = self.ViewMgr.LanMgr:getLanValue("Exchange")
                    local item = ItemUiShopGoods:new(nil, com, table.concat(discirbe), url, btn_title,
                            function()
                                --请求使用积分兑换实物兑换券
                                local ev = self.ViewMgr:GetEv("EvUiBuyItem")
                                if (ev == nil) then
                                    ev = EvUiBuyItem:new(nil)
                                end
                                ev.item_id = tb_item.Id
                                ev.to_etguid = self.ControllerActor.Guid
                                self.ViewMgr:SendEv(ev)
                            end
                    )
                end
            end
        end
    end
end

---------------------------------------
ViewShopFactory = ViewFactory:new()

---------------------------------------
function ViewShopFactory:new(o, ui_package_name, ui_component_name, ui_layer, is_single, fit_screen)
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
function ViewShopFactory:CreateView()
    local view = ViewShop:new(nil)
    return view
end