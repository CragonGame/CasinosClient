-- Copyright(c) Cragon. All rights reserved.
-- 商城界面相关，含充值（调用UCenter充值接口），含钱包Wallet

---------------------------------------
ControllerTrade = class(ControllerBase)

---------------------------------------
function ControllerTrade:ctor(this, controller_data, controller_name)
end

---------------------------------------
function ControllerTrade:OnCreate()
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.ControllerDesktop = self.ControllerMgr:GetController("DesktopTexas")
    self.ControllerDesktopH = self.ControllerMgr:GetController("DesktopH")
    self.ControllerActor = self.ControllerMgr:GetController("Actor")
    self.ControllerUCenter = self.ControllerMgr:GetController("UCenter")
    self.ViewMgr:BindEvListener("EvUiRequestBuyGold", self)
    self.ViewMgr:BindEvListener("EvUiRequestBuyDiamond", self)
    self.ViewMgr:BindEvListener("EvUiRequestFirstRecharge", self)
    self.ViewMgr:BindEvListener("EvUiClickShop", self)
    self.ViewMgr:BindEvListener("EvEntityBuyVIP", self)
    self.ViewMgr:BindEvListener("EvUiBuyItem", self)
    self.ViewMgr:BindEvListener("EvUiSellItem", self)
    self.ViewMgr:BindEvListener("EvBuyRMBItemSuccess", self)
    self.ViewMgr:BindEvListener("EvUiRequestBuyItem", self)
    self.ViewMgr:BindEvListener("EvUiRequestGetMoney", self)
    self.ViewMgr:BindEvListener("EvPayWithIAPSuccess", self)
    self.ViewMgr:BindEvListener("EvUiRequestWebpay", self)
    self.ViewMgr:BindEvListener("EvUiRequestQuicktellerTransfers", self)

    local rpc = self.ControllerMgr.RPC
    local m_c = CommonMethodType
    rpc:RegRpcMethod1(m_c.TradeBuyItemResponse, function(response)
        self:OnTradeBuyItemResponse(response)
    end)
    rpc:RegRpcMethod1(m_c.TradeSellItemResponse, function(response)
        self:OnTradeSellItemResponse(response)
    end)
    rpc:RegRpcMethod3(m_c.TradeOrderNotify, function(result, is_firstrecharge, item_tbid)
        self:OnTradeOrderNotify(result, is_firstrecharge, item_tbid)
    end)
    rpc:RegRpcMethod1(m_c.WalletRechargeNotify, function(response)
        self:OnWalletRechargeNotify(response)
    end)
    rpc:RegRpcMethod1(m_c.WalletWithdrawNotify, function(response)
        self:OnWalletWithdrawNotify(response)
    end)
end

---------------------------------------
function ControllerTrade:OnDestroy()
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ControllerTrade:OnHandleEv(ev)
    if (ev.EventName == "EvUiRequestBuyGold") then
        self:RequestBuyItem(ev.buy_goldid, BuyItemForTarget.Me, "")
    elseif (ev.EventName == "EvUiRequestBuyDiamond") then
        self:BuyBillingItem(false, ev.buy_diamondid)
    elseif (ev.EventName == "EvUiRequestFirstRecharge") then
        self:BuyBillingItem(true, 0)
    elseif (ev.EventName == "EvUiClickShop") then
        self.ViewMgr:CreateView("Shop")
        local view_friend = self.ViewMgr:GetView("Friend")
        self.ViewMgr:DestroyView(view_friend)
    elseif (ev.EventName == "EvEntityBuyVIP") then
        self:BuyBillingItem(false, ev.buy_id)
    elseif (ev.EventName == "EvUiBuyItem") then
        local item_id = ev.item_id
        local target = BuyItemForTarget.Me
        if (ev.to_etguid == self.Guid) then
            target = BuyItemForTarget.Me
        else
            if (self.ControllerDesktop.DesktopBase ~= nil) then
                if (ev.to_etguid ~= nil and ev.to_etguid ~= "") then
                    target = BuyItemForTarget.DesktopOtherPlayer
                else
                    target = BuyItemForTarget.DesktopAllPlayer
                end
            else
                if (self.ControllerDesktopH.DesktopHBase ~= nil) then
                    if (ev.to_etguid ~= nil and ev.to_etguid ~= "") then
                        target = BuyItemForTarget.DesktopHOtherPlayer
                    else
                        target = BuyItemForTarget.DesktopHAllPlayer
                    end
                else
                    if (ev.to_etguid ~= nil and ev.to_etguid ~= "") then
                        target = BuyItemForTarget.OtherPlayer
                    end
                end
            end
        end
        self:RequestBuyItem(item_id, target, ev.to_etguid)
    elseif (ev.EventName == "EvUiSellItem") then
        local gift_objid = ev.item_objid
        self:RequestSellItem(gift_objid)
    elseif (ev.EventName == "EvBuyRMBItemSuccess") then
        local purchase_common = ev.purchase_common
        self:OnBuyRMBItemSuccess(purchase_common)
    elseif (ev.EventName == "EvUiRequestBuyItem") then
        local is_firstrecharge = ev.is_firstrecharge
        local item_tbid = ev.item_tbid
        ViewHelper:UiBeginWaiting(self.ControllerMgr.LanMgr:getLanValue("Buying"))

        if (ev.pay_type == "iap") then
            self:BuyItemByIAP(is_firstrecharge, item_tbid)
        else
            self.PayType = CS._ePayType.__CastFrom(ev.pay_type)
            --local buy_count = ev.item_count
            local payment_info = PayRequest:new(nil)
            payment_info.AppId = self.Context.Cfg.UCenterAppId
            payment_info.AccountId = self.ControllerActor.PropAccountId:get()
            payment_info.IsFirstRecharge = is_firstrecharge
            payment_info.ItemTbId = item_tbid
            local amount = 0
            local title = ""
            local body = ""
            if (is_firstrecharge) then
                amount = tonumber(TbDataHelper:GetCommonValue("FirstRechargePrice"))
                title = self.ControllerMgr.LanMgr:getLanValue("FirstRechargeTitle")
                body = self.ControllerMgr.LanMgr:getLanValue("FirstRechargeBody")
            else
                local tb_item = self.ControllerMgr.TbDataMgr:GetData("Item", item_tbid)
                amount = tb_item.Price
                title = self.ControllerMgr.LanMgr:getLanValue(tb_item.Name)
                body = self.ControllerMgr.LanMgr:getLanValue(tb_item.Desc)
            end
            payment_info.ItemName = title
            payment_info.Channel = ev.pay_type
            payment_info.Amount = tostring(amount * 100)
            payment_info.Currency = "cny"

            self.ControllerUCenter:payCreateCharge(payment_info,
                    function(status, response, error)
                        self:OnPayCreateCharge(status, response, error)
                    end
            )
        end
    elseif (ev.EventName == "EvPayWithIAPSuccess") then
        self:OnBuyRMBItemSuccess(ev.purchase)
    elseif (ev.EventName == "EvUiRequestWebpay") then
        local a = ev.Amount
        local url = "http://kingnigeria-ucenter.cragon.cn:81/nigeriawebpay" .. "?amount=" .. a
        self.ControllerMgr.UniWebView:Load(url)
        self.ControllerMgr.UniWebView:Show()
        self.ControllerMgr.UniWebView:SetShowToolbar(true, false, false)
        --self.ControllerUCenter:nigWebpayRequestUrl( function(status, response, error)
        --    self:OnNigWebpayRequestUrl(status, response, error)
        --end)
    elseif (ev.EventName == "EvUiRequestGetMoney") then
        local r = NigQuicktellerTransRequest:new(nil)
        r.amount = ev.GetMoneyNum
        r.toAccountNumber = ev.toAccountNumber
        r.cbnCode = ev.cbnCode
        r.receiverLastName = ev.receiverLastName
        r.receiverOtherName = ev.receiverOtherName
        self.ControllerUCenter:quicktellerTransfers(r, function(status, response, error)
            self:OnQuicktellerTransfers(status, response, error)
        end)
        --elseif (ev.EventName == "EvUiRequestGetMoney")
        --then
        --    local r = WalletWithdrawRequest:new(nil)
        --    r.Amount = ev.GetMoneyNum
        --    r.Channel = ""
        --    r.MoneyType = self.Context.Cfg.CurrentMoneyType
        --    self.ControllerMgr.RPC:RPC1(CommonMethodType.WalletWithdrawRequest, r:getData4Pack())
    end
end

---------------------------------------
function ControllerTrade:OnTradeBuyItemResponse(response1)
    local response = BuyItemResponse:new(nil)
    response:setData(response1)
    if (response.result == ProtocolResult.Success) then
        local tb_item = self.ControllerMgr.TbDataMgr:GetData("Item", response.buyitem_tbid)
        if (tb_item.UnitType == "GiftNormal" or tb_item.UnitType == "Consume") then
            ViewHelper:UiShowInfoSuccess(string.format(self.ControllerMgr.LanMgr:getLanValue("BuySuccess"), self.ControllerMgr.LanMgr:getLanValue(tb_item.Name)))
        end
    elseif (response.result == ProtocolResult.ChipNotEnough) then
        local tips = string.format(self.ControllerMgr.LanMgr:getLanValue("NotEnoughBuyFail"),
                self.ViewMgr.LanMgr:getLanValue("Chip"))
        ViewHelper:UiShowInfoFailed(tips)
    elseif (response.result == ProtocolResult.DiamondNotEnough) then
        local tips = string.format(self.ControllerMgr.LanMgr:getLanValue("NotEnoughBuyFail"),
                self.ViewMgr.LanMgr:getLanValue("Coin"))
        ViewHelper:UiShowInfoFailed(tips)
    else
        ViewHelper:UiShowInfoFailed(self.ControllerMgr.LanMgr:getLanValue("BuyGoodsFail"))
    end
end

---------------------------------------
function ControllerTrade:OnTradeSellItemResponse(response1)
    local response = SellItemResponse:new(nil)
    response:setData(response1)
    if (response.result == ProtocolResult.Success) then
        ViewHelper:UiShowInfoSuccess(self.ControllerMgr.LanMgr:getLanValue("SellGoodsSuccess"))
    end
end

---------------------------------------
function ControllerTrade:OnTradeOrderNotify(result, is_firstrecharge, item_tbid)
    print("OnTradeOrderNotify ")
    local item_name = self.ControllerMgr.LanMgr:getLanValue("FirstRechargeTitle")
    if (is_firstrecharge == false) then
        local tb_item = self.ControllerMgr.TbDataMgr:GetData("Item", item_tbid)
        item_name = self.ControllerMgr.LanMgr:getLanValue(tb_item.Name)
    end
    if (result == ProtocolResult.Success) then
        print("OnTradeOrderNotify success")
        local format_info = self.ControllerMgr.LanMgr:getLanValue("BuyItemSuccess")
        ViewHelper:UiShowInfoSuccess(string.format(format_info, item_name))
    else
        print("OnTradeOrderNotify failed")
        local format_info = self.ControllerMgr.LanMgr:getLanValue("BuyItemFailed")
        ViewHelper:UiShowInfoFailed(string.format(format_info, item_name))
    end
end

---------------------------------------
function ControllerTrade:OnWalletRechargeNotify(response1)
    local response = WalletRechargeNotify:new(nil)
    response:setData(response1)
    if (response.Result == WalletResult.Success) then
        ViewHelper:UiShowInfoSuccess(self.ControllerMgr.LanMgr:getLanValue("BuySuccess1"))
    else
        ViewHelper:UiShowInfoFailed(self.ControllerMgr.LanMgr:getLanValue("BuyFail"))
    end
end

---------------------------------------
function ControllerTrade:OnWalletWithdrawNotify(response1)
    local response = WalletWithdrawNotify:new(nil)
    response:setData(response1)
    if (response.Result == WalletResult.Success) then
        ViewHelper:UiShowInfoSuccess(self.ControllerMgr.LanMgr:getLanValue("WalletWithdrawSuccess"))
    else
        ViewHelper:UiShowInfoFailed(self.ControllerMgr.LanMgr:getLanValue("WalletWithdrawFailed"))
    end
end

---------------------------------------
function ControllerTrade:OnBuyRMBItemSuccess(purchase_common)
    local purchase = PurchaseCommon:new(nil)
    purchase.OrderId = purchase_common.OrderId
    purchase.PackageName = purchase_common.PackageName
    purchase.Sku = purchase_common.Sku
    purchase.PurchaseTime = purchase_common.PurchaseTime
    purchase.PurchaseState = purchase_common.PurchaseState
    purchase.Token = purchase_common.Token
    purchase.Receipt = purchase_common.Receipt
    --self.ControllerMgr.RPC:RPC1(CommonMethodType.TradeBuyRMBItemSuccessRequest, purchase:getData4Pack())
end

---------------------------------------
function ControllerTrade:RequestBuyItem(buyitem_tbid, target_type, target_etguid)
    local request = BuyItemRequest:new(nil)
    request.buyitem_tbid = buyitem_tbid
    request.target_type = target_type
    request.target_etguid = target_etguid
    print("CommonMethodType.TradeBuyItemRequest")
    self.ControllerMgr.RPC:RPC1(CommonMethodType.TradeBuyItemRequest, request:getData4Pack())
end

---------------------------------------
function ControllerTrade:RequestSellItem(item_objid)
    self.ControllerMgr.RPC:RPC1(CommonMethodType.TradeSellItemRequest, item_objid)
end

---------------------------------------
function ControllerTrade:BuyItem(charge_data)
    --CS.Pay.pay("", charge_data, CS._ePayType.iap)
end

---------------------------------------
function ControllerTrade:BuyBillingItem(is_first_recharge, tb_id)
    --if (self.CasinosContext.UnityAndroid == true)
    --then
    if self.Context.Cfg.ChipIconSolustion ~= 1 then
        local view_paytype = self.ViewMgr:CreateView("PayType")
        view_paytype:BuyItem(is_first_recharge, tb_id)
    else
        if is_first_recharge == false then
            local tb_item = self.ControllerMgr.TbDataMgr:GetData("Item", tb_id)
            local r = WalletRechargeRequest:new(nil)
            r.Amount = tb_item.Price
            r.Channel = ""
            r.MoneyType = self.Context.Cfg.CurrentMoneyType
            self.ControllerMgr.RPC:RPC1(CommonMethodType.WalletRechargeRequest, r:getData4Pack())
        end
    end
    --elseif (self.CasinosContext.UnityIOS == true)
    --then
    --    ViewHelper:UiBeginWaiting(self.ControllerMgr.LanMgr:getLanValue("Buying"))
    --    self:BuyItemByIAP(is_first_recharge, tb_id)
    --end
end

---------------------------------------
function ControllerTrade:BuyItemByIAP(is_first_recharge, tb_id)
    local sku = ""
    if (is_first_recharge) then
        sku = TbDataHelper:GetCommonValue("FirstRechargeStoreSKU")
    else
        local billing_item = self.ControllerMgr.TbDataMgr:GetData("UnitBilling", tb_id)
        sku = billing_item.StoreSKU
    end

    if (sku ~= nil and sku ~= "") then
        --CS.Pay.pay(sku, "", CS._ePayType.iap)
    end
end

---------------------------------------
function ControllerTrade:OnPayCreateCharge(status, response, error)
    if (status == UCenterResponseStatus.Success) then
        if (self.CasinosContext.UnityIOS == true) then
            self.CasinosContext:setNativeOperate(CS.Casinos.NativeOperateType.__CastFrom('Pay'))
        end

        if (response ~= nil) then
            local url_s = ""
            if CS.Casinos.CasinosContext.Instance.UnityIOS then
                url_s = self.Context.Cfg.PayUrlScheme
            end
            CS.Pay.pay("", response.itemName, self.PayType, tonumber(response.amount), response.chargeId, "", url_s)
        end
    else
        ViewHelper:UiShowInfoFailed(self.ControllerMgr.LanMgr:getLanValue("CreateChargeError"))
    end
end

---------------------------------------
function ControllerTrade:OnNigWebpayRequestUrl(status, response, error)
end

---------------------------------------
function ControllerTrade:OnQuicktellerTransfers(status, response, error)
    if status == UCenterResponseStatus.Success and response.result == 1 then
        ViewHelper:UiShowInfoSuccess(string.format("你提取现金%s成功!", response.request.amount))
    else
        ViewHelper:UiShowInfoFailed("你提取现金失败!")
    end
end

---------------------------------------
ControllerTradeFactory = class(ControllerFactory)

function ControllerTradeFactory:GetName()
    return 'Trade'
end

function ControllerTradeFactory:CreateController(controller_data)
    local ctrl_name = self:GetName()
    local ctrl = ControllerTrade:new(controller_data, ctrl_name)
    return ctrl
end