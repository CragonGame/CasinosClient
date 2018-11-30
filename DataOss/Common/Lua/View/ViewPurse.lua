-- Copyright(c) Cragon. All rights reserved.
-- Nigeria钱包界面

---------------------------------------
ViewPurse = class(ViewBase)

---------------------------------------
function ViewPurse:ctor()
    self.TemporaryHideItemId = 14001
end

---------------------------------------
function ViewPurse:OnCreate()
    self.ViewMgr:BindEvListener("EvEntityGoldChanged", self)
    self.ViewMgr:BindEvListener("EvEntityDiamondChanged", self)
    self.GTransitionShow = self.ComUi:GetTransition("TransitionShow")
    self.GTransitionShow:Play()
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.ControllerActor = self.ViewMgr.ControllerMgr:GetController("Actor")
    self.MapShopGold = {}
    self.ControllerShop = self.ComUi:GetController("ControllerShop")
    local btn_return = self.ComUi:GetChild("BtnReturn").asButton
    btn_return.onClick:Add(
            function()
                self:onClickBtnReturn()
            end
    )

    local btn_getmoney = self.ComUi:GetChild("Lan_Btn_GetCash").asButton
    btn_getmoney.onClick:Add(
            function()
                self:onClickBtnGetMoney()
            end
    )

    local com_input = self.ComUi:GetChild("ComPutIn").asCom
    self.GComPutIn = com_input:GetChild("Num").asTextInput

    self.GListChip = self.ComUi:GetChild("ListChip").asList
    local btn_tabdiomand = self.ComUi:GetChild("BtnTabGetMoney").asButton
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
    self:createChip()
    local com_taball = self.ComUi:GetChild("ComTabAll").asCom
    self.ControllerTab = com_taball:GetController("ControllerTab")
    local btn_addChip = self.ComUi:GetChild("BtnAddChip").asButton
    self.GTextSelfGold = btn_addChip:GetChild("TextChipAmount").asTextField
    local btn_addDiamond = self.ComUi:GetChild("BtnAddDiamond").asButton
    self.GTextSelfDiamond = btn_addDiamond:GetChild("TextDiamondAmount").asTextField
    self:setPlayerGoldAndDiamond()
    local bg = self.ComUi:GetChild("Bg")
    if (bg ~= nil) then
        ViewHelper:MakeUiBgFiteScreen(ViewMgr.STANDARD_WIDTH, ViewMgr.STANDARD_HEIGHT, self.ComUi.width, self.ComUi.height, bg.width, bg.height, bg, BgAttachMode.Center)
    end
end

---------------------------------------
function ViewPurse:OnDestroy()
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ViewPurse:OnHandleEv(ev)
    if (ev.EventName == "EvEntityGoldChanged") then
        self:setPlayerGoldAndDiamond()
    elseif (ev.EventName == "EvEntityDiamondChanged") then
        self:setPlayerGoldAndDiamond()
    end
end

---------------------------------------
function ViewPurse:showGold()
    self:onClickBtnTabChip()
end

---------------------------------------
function ViewPurse:setPlayerGoldAndDiamond()
    self.GTextSelfGold.text = UiChipShowHelper:getGoldShowStr(self.ControllerActor.PropGoldAcc:get(), self.ViewMgr.LanMgr.LanBase)
    self.GTextSelfDiamond.text = UiChipShowHelper:getGoldShowStr(self.ControllerActor.PropDiamond:get(), self.ViewMgr.LanMgr.LanBase, false)
end

---------------------------------------
function ViewPurse:onClickBtnReturn()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewPurse:onClickBtnGetMoney()
    local n = tonumber(self.GComPutIn.text)
    if (n == nil) then
        ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("EnterNumTips"))
        return
    end

    local ev = self.ViewMgr:GetEv("EvUiRequestGetMoney")
    if (ev == nil) then
        ev = EvUiRequestGetMoney:new(nil)
    end
    ev.GetMoneyNum = n
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ViewPurse:onClickBtnDiomand()
    self.ControllerShop.selectedIndex = 1
    self.ControllerTab.selectedIndex = 1
    --self:createDiamond()
end

---------------------------------------
function ViewPurse:onClickBtnTabChip()
    self.ControllerShop.selectedIndex = 0
    self.ControllerTab.selectedIndex = 0
    self:createChip()
end

---------------------------------------
function ViewPurse:createChip()
    if (LuaHelper:GetTableCount(self.MapShopGold) > 0) then
        return
    end
    local map_tbitem = self.CasinosContext.TbDataMgrLua:GetMapData("Item")
    for key, value in pairs(map_tbitem) do
        local tb_item = value
        if (tb_item.UnitType == "Billing") then
            local tb_itemtype = self.CasinosContext.TbDataMgrLua:GetData("ItemType", tb_item.ItemTypeTbId)
            if (tb_itemtype.TypeName == "Gold") then
                local co_gold = self.GListChip:AddItemFromPool().asCom
                local ui_diamond = ItemUiPurseGold:new(nil, self, co_gold, tb_item)
                self.MapShopGold[key] = ui_diamond
            end
        end
    end
end

---------------------------------------
ViewPurseFactory = class(ViewFactory)

---------------------------------------
function ViewPurseFactory:CreateView()
    local view = ViewPurse:new()
    return view
end