-- Copyright(c) Cragon. All rights reserved.
--道具点击后的详情对话框显示，不仅是Gift，是所有Item类型的对话框
--普通桌给所有人购买并赠送礼物对话框也是该View管理

---------------------------------------
ViewGiftDetail = ViewBase:new()

---------------------------------------
function ViewGiftDetail:new(o)
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
function ViewGiftDetail:OnCreate()
    ViewHelper:PopUi(self.ComUi)
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.ControllerActor = self.ViewMgr.ControllerMgr:GetController("Actor")
    self.ControllerDesktop = self.ViewMgr.ControllerMgr:GetController("DesktopTexas")
    self.ControllerPlayer = self.ViewMgr.ControllerMgr:GetController("Player")
    self.mInDesktop = (self.ControllerDesktop.DesktopBase ~= nil)
    local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:onClickCloseBtn()
            end
    )
    self.GTextTitle = self.ComUi:GetChild("Title").asTextField
    self.GTextTips = self.ComUi:GetChild("Tips").asTextField
    local comGift = self.ComUi:GetChild("Gift").asCom
    self.GLoadGiftBg = comGift:GetChild("Bg").asLoader
    self.GloadIcon = comGift:GetChild("LoaderIcon").asLoader
    self.GCOmWindowNotDesktop = self.ComUi:GetChild("WindowNeedConfirm").asGroup
    self.GComWindowDesktop = self.ComUi:GetChild("WindowNeedConfirmTwo").asGroup
    self.GBtnConfirmCenter = self.ComUi:GetChild("ConfirmCenterBtn").asButton
    self.GBtnConfirmCenter.onClick:Add(
            function()
                self:onClickBtnConfirmNotInDesktop()
            end
    )
    self.GTextConfirmCenter = self.GBtnConfirmCenter:GetChild("Title").asTextField
    self.GBtnConfirmAllBuy = self.ComUi:GetChild("ConfirmAllBuy").asButton
    self.GBtnConfirmAllBuy.onClick:Add(
            function()
                self:onClickBtnConfirmBuyAllDesktop()
            end
    )
    self.GTextConfirmAllBuy = self.GBtnConfirmAllBuy:GetChild("Title").asTextField
    self.GBtnConfirmOneBuy = self.ComUi:GetChild("ConfirmOneBuyBtn").asButton
    self.GBtnConfirmOneBuy.onClick:Add(
            function()
                self:onClickBtnConfirmBuyOneInDesktop()
            end
    )
    self.GTextConfirmOneBuy = self.GBtnConfirmOneBuy:GetChild("Title").asTextField
end

---------------------------------------
-- is_buygift，true=买，false=卖
-- to_etguid, 买给单人，买给全桌
-- from_name 来源玩家的昵称
-- gift_belong, 来源玩家PlayerGuid
function ViewGiftDetail:SetGift(gift_id, is_buygift, is_mine, to_etguid, from_name, gift_belong, item)
    self.mItem = item
    self.mBuyGift = is_buygift
    self.mTbGiftId = gift_id
    self.mToEtGuid = to_etguid
    self.mIsGoodsVoucher = false
    local tb_gift = self.CasinosContext.TbDataMgrLua:GetData("Item", self.mTbGiftId)
    local bg_name = ""
    local show_windowdesktop = self.mInDesktop
    if (tb_gift.UnitType == "GiftTmp") then
        bg_name = "BlackBg"
    elseif (tb_gift.UnitType == "GiftNormal") then
        bg_name = "CommonBlueLightBg"
        show_windowdesktop = false
    end
    self.GComWindowDesktop.visible = show_windowdesktop
    if (show_windowdesktop) then
        self.GCOmWindowNotDesktop.visible = false
    else
        self.GCOmWindowNotDesktop.visible = true
    end
    self.GLoadGiftBg.url = CS.FairyGUI.UIPackage.GetItemURL("Common", bg_name)
    self.GloadIcon.icon = self.CasinosContext.PathMgr.DirAbItem .. string.lower(tb_gift.Icon) .. ".ab"

    local title = self.ViewMgr.LanMgr:getLanValue("DeleteGoods")
    local confirm_text = self.ViewMgr.LanMgr:getLanValue("Delete")
    local buy_price_long = math.floor(tb_gift.Price * (tb_gift.Discount / 100))
    local buy_price = UiChipShowHelper:getGoldShowStr(buy_price_long, self.ViewMgr.LanMgr.LanBase)
    local all_buyprice = ""
    local all_buyprice_long = 0
    if (show_windowdesktop) then
        all_buyprice_long = (#self.ControllerDesktop.DesktopBase:getAllValidPlayer()) * buy_price_long
        all_buyprice = UiChipShowHelper:getGoldShowStr(all_buyprice_long, self.ViewMgr.LanMgr.LanBase)
    end
    local sell_price = UiChipShowHelper:getGoldShowStr(math.floor(tb_gift.Price / 10 * (tb_gift.Discount / 100)), self.ViewMgr.LanMgr.LanBase)
    if (tb_gift.PriceType == PriceType.Gold) then
        buy_price = buy_price .. self.ViewMgr.LanMgr:getLanValue("Diamonds")
        sell_price = sell_price .. self.ViewMgr.LanMgr:getLanValue("Diamonds")
        all_buyprice = all_buyprice .. self.ViewMgr.LanMgr:getLanValue("Diamonds")
    end
    local other_playergift = false
    local gift_detail = self:getGiftDescribe()
    local buy_all = ""
    local is_red = false
    if (self.mBuyGift) then
        title = self.ViewMgr.LanMgr:getLanValue("BuyTempGift")
        confirm_text = self.ViewMgr.LanMgr:getLanValue("Buy") .. ":" .. buy_price
        buy_all = self.ViewMgr.LanMgr:getLanValue("BuyEveryone") .. ":" .. all_buyprice
    else
        if (gift_belong == self.ControllerPlayer.Guid) then
            if (is_mine) then
                from_name = self.ControllerActor.PropNickName:get()
                confirm_text = self.ViewMgr.LanMgr:getLanValue("Sell") .. ":" .. sell_price
                self.mSellGift = true
            else
                title = self.ViewMgr.LanMgr:getLanValue("ItemDetail")
                other_playergift = true
            end
            if from_name ~= nil then
                gift_detail = gift_detail .. ("\n" .. self.ViewMgr.LanMgr:getLanValue("From") .. from_name)
            end
        end
    end
    if (tb_gift.UnitType == "GoodsVoucher") then
        title = self.ViewMgr.LanMgr:getLanValue(self.ViewMgr.LanMgr:getLanValue("Voucher"))
        gift_detail = self.ViewMgr.LanMgr:getLanValue("ExchangeTips")
        confirm_text = self.ViewMgr.LanMgr:getLanValue("Exchange")
        other_playergift = false
        self.mIsGoodsVoucher = true
    elseif (tb_gift.UnitType == "WechatRedEnvelopes") then
        is_red = true
        title = self.ViewMgr.LanMgr:getLanValue("ItemDetail")
        local wechat_id = self.ControllerActor.WeChatOpenId:get()
        local wechat_name = self.ControllerActor.WeChatName:get()
        gift_detail = string.format(gift_detail, wechat_name, item.UnitLink.Value)
        if (CS.System.String.IsNullOrEmpty(wechat_id)) then
            local t1 = self.ViewMgr.LanMgr:getLanValue("GetRedEnvelopesTip")
            local t2 = self.ViewMgr.LanMgr:getLanValue("BindWeChatTip")
            gift_detail = gift_detail .. t1 .. t2
            confirm_text = self.ViewMgr.LanMgr:getLanValue("BindWeChat")
            self.mNeedBindWeChat = true
        else
            confirm_text = self.ViewMgr.LanMgr:getLanValue("GetRedEnvelopes")
            self.mGetRedEnvelopes = true
        end
    end
    if (other_playergift) then
        self.GBtnConfirmCenter.visible = false
        self.GTextConfirmCenter.visible = false
    else
        if (show_windowdesktop) then
            self.GTextConfirmOneBuy.text = confirm_text
            self.GTextConfirmAllBuy.text = buy_all
        else
            self.GBtnConfirmCenter.visible = true
            self.GTextConfirmCenter.visible = true
            self.GTextConfirmCenter.text = confirm_text
        end
    end
    --if is_red then
    --	self.GBtnConfirmCenter.visible = true
    --	self.GTextConfirmCenter.visible = true
    --	self.GTextConfirmCenter.text = confirm_text
    --end
    ViewHelper:SetUiTitle(self.GTextTitle, title)
    self.GTextTips.text = gift_detail
end

---------------------------------------
-- 单独买卖按钮，即不在桌内时买卖按钮
function ViewGiftDetail:onClickBtnConfirmNotInDesktop()
    if (self.mBuyGift) then
        local ev = self.ViewMgr:GetEv("EvUiBuyItem")
        if (ev == nil) then
            ev = EvUiBuyItem:new(nil)
        end
        ev.item_id = self.mTbGiftId
        ev.to_etguid = self.mToEtGuid
        self.ViewMgr:SendEv(ev)
    elseif (self.mSellGift) then
        local ev = self.ViewMgr:GetEv("EvUiSellItem")
        if (ev == nil) then
            ev = EvUiSellItem:new(nil)
        end
        ev.item_objid = self.mItem.ItemData.item_objid
        self.ViewMgr:SendEv(ev)
    elseif (self.mIsGoodsVoucher) then
        -- 请求兑换实物
        local ev = self.ViewMgr:GetEv("EvUiRequestOperateItem")
        if (ev == nil) then
            ev = EvUiRequestOperateItem:new(nil)
        end
        ev.ItemObjId = self.mItem.ItemData.item_objid
        self.ViewMgr:SendEv(ev)
    elseif (self.mNeedBindWeChat) then
        local ev = self.ViewMgr:GetEv("EvBindWeChat")
        if (ev == nil) then
            ev = EvBindWeChat:new(nil)
        end
        ev.ItemObjId = self.mItem.ItemData.item_objid
        self.ViewMgr:SendEv(ev)
    elseif (self.mGetRedEnvelopes) then
        local ev = self.ViewMgr:GetEv("EvUiRequestOperateItem")
        if (ev == nil) then
            ev = EvUiRequestOperateItem:new(nil)
        end
        ev.ItemObjId = self.mItem.ItemData.item_objid
        self.ViewMgr:SendEv(ev)
    else
        local ev = self.ViewMgr:GetEv("EvUiRemoveItem")
        if (ev == nil) then
            ev = EvUiRemoveItem:new(nil)
        end
        ev.obj_id = self.mItem.ItemData.item_objid
        self.ViewMgr:SendEv(ev)
    end
    self:onClickCloseBtn()
end

---------------------------------------
-- 桌内购买并送单人按钮
function ViewGiftDetail:onClickBtnConfirmBuyOneInDesktop()
    local ev = self.ViewMgr:GetEv("EvUiBuyItem")
    if (ev == nil) then
        ev = EvUiBuyItem:new(nil)
    end
    ev.item_id = self.mTbGiftId
    ev.to_etguid = self.mToEtGuid
    self.ViewMgr:SendEv(ev)
    self:onClickCloseBtn()
end

---------------------------------------
-- 桌内购买并送全桌按钮
function ViewGiftDetail:onClickBtnConfirmBuyAllDesktop()
    local ev = self.ViewMgr:GetEv("EvUiBuyItem")
    if (ev == nil) then
        ev = EvUiBuyItem:new(nil)
    end
    ev.item_id = self.mTbGiftId
    ev.to_etguid = ""
    self.ViewMgr:SendEv(ev)
    self:onClickCloseBtn()
end

---------------------------------------
function ViewGiftDetail:getGiftDescribe()
    local tb_gift = self.CasinosContext.TbDataMgrLua:GetData("Item", self.mTbGiftId)
    local describe = self.ViewMgr.LanMgr:getLanValue(tb_gift.Desc)
    return string.gsub(describe, "��", "\n")
end

---------------------------------------
function ViewGiftDetail:onClickCloseBtn()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewGiftDetailFactory = ViewFactory:new()

---------------------------------------
function ViewGiftDetailFactory:new(o, ui_package_name, ui_component_name, ui_layer, is_single, fit_screen)
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
function ViewGiftDetailFactory:CreateView()
    local view = ViewGiftDetail:new(nil)
    return view
end