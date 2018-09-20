KingTexasListener = {
    Instance = nil,
    MainC = nil,
    ControllerMgr = nil,
    ViewMgr = nil,
    EventSys = nil,
    LuaHelper = nil,
    TbDataMgr = nil,
    TbDataMgrBase = nil,
}

function KingTexasListener:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    if (self.Instance == nil)
    then
        self.Instance = o
    end
    return self.Instance
end

function KingTexasListener:onCreate()
    print("KingTexasListener:onCreate")
    self.ResourcesRowPathRoot = "Resources.KingTexasRaw/"

    --local uni_g = CS.UnityEngine.GameObject("UniWebView")
    --self.UniWebView = CS.Casinos.LuaHelper.AddUniWebViewToObj(uni_g)
    --self.UniWebView.Frame = CS.UnityEngine.Rect(0, 0, CS.UnityEngine.Screen.width, CS.UnityEngine.Screen.height)
    self:_regLuaFilePath()

    self.MainC = MainC:new(nil)
    self.Lanch = ControllerLaunch:new(nil)
    if self.MainC:isReload() then
        self.Lanch.CanCheckLoadDataDone = true
        self.Lanch.InitializeDone = false
        CS.Casinos.CasinosContext.Instance.AsyncAssetLoadGroup:releaseAssetBundle()
    end
    self.CreateLogin = false
    self.LoadTbDataDone = false
    --MainC:doString("LuaHelper")
    --self.LuaHelper = LuaHelper:new(nil)
    MainC:doString("TexasHelper")

    MainC:doString("TbDataMgr")
    self.TbDataMgr = TbDataMgr:new(nil)
    self.TbDataMgr:onCreate()
    MainC:doString("TbDataBase")
    MainC:doString("TbDataMgrBase")
    self:RegTbData()

    local t_db = {}
    local list_db = CS.Casinos.CasinosContext.Instance.UserConfig:getDBName()
    for i = 0, list_db.Count - 1 do
        t_db[i] = CS.Casinos.CasinosContext.Instance.PathMgr:combinePersistentDataPath("Resources.KingTexasRaw/Config/" .. list_db[i] .. ".db")
    end
    self.TbDataMgrBase = TbDataMgrBase:new(nil, self.TbDataMgr, t_db,
            function()
                self:LoadDataDone()
            end
    )

    MainC:doString("EventSys")
    self.EventSys = EventSys:new(nil)
    self.EventSys:onCreate()

    MainC:doString("ViewMgr")
    self.ViewMgr = ViewMgr:new(nil)
    self.ViewMgr:onCreate("Resources.KingTexas/Ui/", "Resources.KingTexasRaw/")
    self.ViewMgr.TbDataMgr = self.TbDataMgr
    self:_regView()
    MainC:doString("ViewHelper")
    ViewHelper:new(nil)

    MainC:doString("LanBase")
    MainC:doString("LanEn")
    MainC:doString("LanMgr")
    MainC:doString("LanZh")

    MainC:doString("UiChipShowHelper")
    self.UiChipShowHelper = UiChipShowHelper:new(nil)
    --MainC:doString("ParticleHelper")

    MainC:doString("MessagePack")
    MainC:doString("json")
    self.Json = require("json")
    MainC:doString("RPC")
    local rpc = RPC:new(nil)

    MainC:doString("ControllerMgr")
    self.ControllerMgr = ControllerMgr:new(nil)
    self.ControllerMgr:OnCreate()
    self.ControllerMgr.TbDataMgr = self.TbDataMgr
    self.ControllerMgr.RPC = rpc
    self.ControllerMgr.Listener = self
    self:_regController()
    self.ViewMgr.ControllerMgr = self.ControllerMgr
    self.ViewMgr.RPC = rpc
    self.ViewMgr.Listener = self

    self:_addUiPackage()


end

function KingTexasListener:onDestroy()
    if (self.ViewMgr ~= nil)
    then
        self.ViewMgr:onDestroy()
    end

    if (self.ControllerMgr ~= nil)
    then
        self.ControllerMgr:OnDestroy()
    end
end

function KingTexasListener:onUpdate(tm)
    if (self.ViewMgr ~= nil)
    then
        self.ViewMgr:onUpdate(tm)
    end

    if (self.ControllerMgr ~= nil)
    then
        self.ControllerMgr:OnUpdate(tm)
    end

    if (self.TbDataMgr ~= nil)
    then
        self.TbDataMgr:onUpdate(tm)
    end

    if (self.LoadTbDataDone == true and self.Lanch.InitializeDone == true and self.CreateLogin == false)
    then
        self.CreateLogin = true
        self.ControllerMgr:CreateController("UCenter", nil, nil)
        self.ControllerMgr:CreateController("Login", nil, nil)
    end

    if self.PicCapture ~= nil then
        self.PicCapture:OnUpdate(tm)
    end

    if (CS.UnityEngine.Input.GetKeyDown(CS.UnityEngine.KeyCode.Escape))
    then
        local ui_quit = self.ViewMgr:getView("QuitOrBack")
        if (ui_quit == nil)
        then
            ui_quit = self.ViewMgr:createView("QuitOrBack")
            if ui_quit ~= nil then
                ui_quit:setQuitTimeAndIfCanBackGameAndTips(10, true, "")
            end
        end
    end
end

function KingTexasListener.OnApplicationPause(is_pause)
    print("OnApplicationPause   "..tostring(is_pause))
    if (is_pause == false)
    then
        local lis = KingTexasListener:new(nil)
        local ev = lis.ViewMgr:getEv("EvEntityGetDesktopData")
        if (ev == nil)
        then
            ev = EvEntityGetDesktopData:new(nil)
        end
        lis.ViewMgr:sendEv(ev)

        local ev1 = lis.ViewMgr:getEv("EvEntityRequestGetDesktopHData")
        if (ev1 == nil)
        then
            ev1 = EvEntityRequestGetDesktopHData:new(nil)
        end
        lis.ViewMgr:sendEv(ev1)

        CS.OpenInstall.GetWakeUp()
    end
end

function KingTexasListener:CreateController(table_data)

end

function KingTexasListener:_regLuaFilePath()
    local casinos_context = CS.Casinos.CasinosContext.Instance
    casinos_context:regLuaFilePath("MessagePack/", "MessagePack")
    casinos_context:regLuaFilePath("Json/", "json")
    casinos_context:regLuaFilePath("Lan/", "LanBase", "LanEn", "LanMgr", "LanZh")
    casinos_context:regLuaFilePath("Controller/", "ControllerMgr", "ControllerBase", "ControllerFactory",
            "ControllerActivity", "ControllerActor", "ControllerBag", "ControllerDesk", "ControllerDeskH", "ControllerGrow",
            "ControllerIM", "ControllerLobby", "ControllerLotteryTicket", "ControllerMarquee", "ControllerPlayer",
            "ControllerRanking", "ControllerTrade", "ControllerLogin", "ControllerUCenter", "ControllerMTT", "Prop", "OnLineReward","TimingReward")
    casinos_context:regLuaFilePath("Item/", "Item", "ItemData1", "Unit", "UnitBilling", "UnitConsume", "UnitGiftNormal","UnitRedEnvelopes", "UnitGiftTmp", "UnitMagicExpression",
            "UnitGoodsVoucher","UnitGoldPackage","ItemActivity")
    casinos_context:regLuaFilePath("IM/", "IMChat","IMFeedback", "IMChatRecord", "IMFriendList", "IMMailBox")
    casinos_context:regLuaFilePath("Desktop/", "DesktopPlayerTexas", "DesktopBase", "DesktopTexas", "DesktopHelperBase", "DesktopHelperTexas",
            "DesktopTypeBase", "DesktopTexasClassic", "DesktopTexasMTT")
    casinos_context:regLuaFilePath("DesktopH/", "DesktopHBase", "DesktopHTexas")
    casinos_context:regLuaFilePath("Model/", "ModelBase", "ModelReaderBase", "ModelCommon", "ModelAccount", "ModelUCenter", "ModelTrade", "ModelDesktop",
            "ModelDesktopTexas", "ModelDesktopTexasMatch","ModelActor","ModelActivity","ModelGrow","ModelPlayer","ModelRank","ModelMarquee","ModelWallet"
    ,"ModelBag","ModelDesktopH","ModelIM","ModelIMClientEvent","ModelIMMailBox","ModelLotteryTicket","ModelPlayer","ModelPlayerInfo")
    casinos_context:regLuaFilePath("View/", "ViewMgr", "ViewFactory", "ViewBase", "ViewLoading", "ViewMsgBox", "ViewSecurityCode", "ViewDesktopChatParent"
    , "ViewFloatMsg", "ViewHeadIcon", "ViewHeadIconBig", "ViewMailDetail", "ViewNotice", "ViewPayType", "ViewVIPSign", "ViewWaitingCountDown"
    , "ViewPermanentPosMsg", "ViewPool", "ViewShootingText", "ViewTakePhoto", "ViewWaiting", "ViewWaitingCountDown", "ViewOnlineReward","ViewTimingReward",
            "UiMainPlayerInfo", "ViewActivityPopUpBox","ViewShare","ViewShareType","UiResetPwd","UiRegister","UiChooseCountryCode","ViewIdCardCheck")
    casinos_context:regLuaFilePath("View/Desktop/", "DealerEx", "UiCardCommonEx", "UiCardDealingEx", "UiChipEx", "UiChipMgrEx", "ViewPlayerGiftAndVIP"
    , "ViewDesktopTypeBase", "ViewMTTGameResult", "ViewMTTProcess")
    casinos_context:regLuaFilePath("View/Desktop/Texas/", "PlayerSeatWidgetControllerTexas","PlayerShowTips","PlayerOperateAni", "ViewDesktopHintsTexas", "ViewDesktopMenuTexas", "ViewDesktopPlayerInfoTexas",
            "ViewDesktopPlayerOperateTexas", "DesktopFastBet","ViewDesktopTexas", "ViewHandCardTexas", "ViewLockChatTexas", "ViewPotTexasPoker", "ViewTexasClassic", "ViewTexasMTT")
    casinos_context:regLuaFilePath("View/DesktopH/", "DesktopHBankPlayer", "DesktopHBetPot", "DesktopHCard", "DesktopHCards", "DesktopHCardTypeBase", "DesktopHCardTypeTexas"
    , "DesktopHChair", "DesktopHDealer", "DesktopHGoldPool", "DesktopHRewardPot", "DesktopHSelf", "DesktopHStandPlayer"
    , "DesktopHUiGold", "ViewDesktopH", "ViewDesktopHBankList", "ViewDesktopHBase", "ViewDesktopHBetReward", "ViewDesktopHCardType", "ViewDesktopHHelp"
    , "ViewDesktopHHistory", "ViewDesktopHMenu", "ViewDesktopHResult", "ViewDesktopHSetCardType", "ViewDesktopHTexas")
    casinos_context:regLuaFilePath("View/DesktopH/Item/", "ItemDesktopHBeBankPlayerInfo", "ItemDesktopHBetOperate", "ItemDesktopHBetPot", "ItemDesktopHBetReward", "ItemDesktopHGameEndPotResult"
    , "ItemDesktopHGameEndWinPlayer", "ItemDesktopHHistroy", "ItemDesktopHSeat", "ItemDesktopHSetCardType", "ItemUiDesktopHTongPei", "ItemUiDesktopHTongSha")
    --[[casinos_context:regLuaFilePath("View/ForestParty/", "ForestPartyBanker","ForestPartyCommon","ViewForestParty","ViewForestPartyBet","ViewForestPartyInfo","ViewForestPartyMenu"
                                    ,"ViewForestPartyResult","ViewForestPartyRule")
    casinos_context:regLuaFilePath("View/ForestParty/Item/", "ItemForestPartyBetOperate","ItemForestPartyLotteryResult")]]--
    casinos_context:regLuaFilePath("View/LotteryTicket/", "ViewLotteryTicket", "ViewLotteryTicketBase", "ViewLotteryTicketGFlower", "ViewLotteryTicketRewardPot", "ViewLotteryTicketTexas")
    casinos_context:regLuaFilePath("View/LotteryTicket/Item/", "ItemLotteryTicketBetOperate", "ItemLotteryTicketBetPot", "ItemLotteryTicketCard"
    , "ItemLotteryTicketHistory", "ItemLotteryTicketRewardPotPlayerInfo")
    casinos_context:regLuaFilePath("View/Item/", "ItemAttachment", "ItemMttRewardItem","ItemBetChipRange", "ItemChatEx", "ItemChatPresetMsg", "ItemDailyReward"
    , "ItemDesktopHintsInfo", "ItemDesktopRaiseOperateGFlower", "ItemHeadIcon", "ItemHeadIconWithNickName", "ItemMagicExpIcon", "ItemMagicExpSender", "ItemMail"
    , "ItemNotice", "ItemNumOperate", "ItemPlayerChatLock", "ItemPlayerReport", "ItemChatDesktop","ItemCountryCode")
    casinos_context:regLuaFilePath("TbData/", "TbDataMgr", "TbDataBase", "TbDataMgrBase", "TbDataHintsInfoTexas", "TbDataActorLevel", "TbDataCommon", "TbDataDailyReward", "TbDataExpression",
            "TbDataItemType", "TbDataLanEn", "TbDataLans", "TbDataLanZh", "TbDataItem", "TbDataDesktopGFlowerBetOperate", "TbDataDesktopHBetPotGFlower",
            "TbDataLotteryTicket", "TbDataLotteryTicketBetOperate", "TbDataLotteryTicketGoldPercent", "TbDataOnlineReward", "TbDataPresetMsg", "TbDataUnitBilling",
            "TbDataUnitConsume", "TbDataUnitGiftNormal","TbDataUnitRedEnvelopes", "TbDataUnitGiftTmp", "TbDataUnitMagicExpression","TbDataUnitGoldPackage","TbDataUnitGoodsVoucher","TbDataVIPLevel", "TbDataConfigGFlowerDesktop",
            "TbDataDesktopGFlowerCreateInfo", "TbDataDesktopInfoGFlower", "TbDataConfigTexasDesktop", "TbDataDesktopInfoTexas",
            "TbDataCfigGFlowerDesktopH", "TbDataCfigGFlowerDesktopHGoldPercent", "TbDataCfigGFlowerDesktopHSysBank", "TbDataDesktopHBetOperateGFlower",
            "TbDataDesktopHInfoGFlower", "TbDataCfigNiuNiuDesktopH", "TbDataCfigNiuNiuDesktopHGoldPercent", "TbDataCfigNiuNiuDesktopHSysBank", "TbDataDesktopHBetOperateNiuNiu",
            "TbDataDesktopHBetPotNiuNiu", "TbDataDesktopHInfoNiuNiu", "TbDataCfigTexasDesktopH", "TbDataCfigTexasDesktopHGoldPercent", "TbDataCfigTexasDesktopHSysBank",
            "TbDataDesktopHBetOperateTexas", "TbDataDesktopHBetPotTexas", "TbDataDesktopHInfoTexas", "TbDataCfigZhongFBDesktopH", "TbDataCfigZhongFBDesktopHGoldPercent",
            "TbDataCfigZhongFBDesktopHSysBank", "TbDataDesktopHBetOperateZhongFB", "TbDataDesktopHBetPotZhongFB", "TbDataDesktopHInfoZhongFB", "TbDataDesktopHBetReward",
            "TbDataDesktopFastBet","TbDataTexasRaiseBlinds","TbDataTexasSnowBallRewardPlayerNum","TbDataTexasSnowBallRewardInfo")
    --"TbDataForestPartyAnimalList","TbDataForestPartyBetOperate","TbDataForestPartyBetPotMultiple","TbDataForestPartyDesktop","TbDataForestPartyLottery",
    casinos_context:regLuaFilePath("Event/", "EventSys", "EventBase", "EventView")
    casinos_context:regLuaFilePath("Helper/", "CommonLuaLoader", "ProjectDataLoader", "WWWLoader", "TexasHelper", "ViewHelper",
            "TbDataHelper", "GoldController", "UiChipShowHelper", "RPC", "CasinoHelper","Native","PicCapture")--"LuaHelper",
    casinos_context:regLuaFilePath("Resources.KingTexasRaw/Lua/View/", "ViewLogin", "ViewAbout", "ViewAgreeOrDisAddFriendRequest", "ViewBag", "ViewBank", "ViewChat"
    , "ViewChatChooseTarget", "ViewChatFriend", "ViewChipOperate", "ViewChooseLan", "ViewCreateDesktop", "ViewDailyReward"
    , "ViewDesktopChatExpression", "ViewDesktopHRewardPot", "ViewEdit", "ViewFriend", "ViewFriendOnLine", "ViewGiftDetail"
    , "ViewGiftShop", "ViewGoldTree", "ViewInviteFriendPlay", "ViewLobby", "ViewMail", "ViewMain", "ViewFeedback","ViewPlayerInfo", "ViewQuitOrBack"
    , "ViewRanking", "ViewRechargeFirst", "ViewResetPwd", "ViewShop", "ViewPlayerProfile", "ViewActivityCenter", "ViewMatchLobby"
    , "ViewApplySucceed", "ViewClub","ViewCreateMatch","ViewBlindTable","ViewClubHelp","ViewJoinMatch","ViewGetChipEffect","ViewMatchInfo","ViewEnterMatchNotify"
    , "ViewSnowBallReward","ViewEditAddress","ViewPurse")
    casinos_context:regLuaFilePath("Resources.KingTexasRaw/Lua/View/Item/", "ItemChatTargetInfo", "ItemChooseChatTargetInfo", "ItemDesktopHRewardPotPlayerInfo", "ItemGift", "ItemGiftType", "ItemInviteFriendPlay"
    , "ItemLan", "ItemLobbyDesk", "ItemLobbyDeskPlayInfo", "ItemMainOperate", "ItemRank", "ItemReportPlayerOperate", "ItemShowFriendSimple","ItemAnte"
    , "ItemUiShopConsume", "ItemUiShopDiamond", "ItemUiShopGold", "ItemUiShopVIPInfo", "ItemVIPBuyInfo", "ItemActivityTitle","ItemUiShopGoods","ItemUiPurseGold")
    casinos_context:regLuaFilePath("Resources.KingTexasRaw/Lua/View/Item/Mtt/", "ItemMatchType", "ItemMatchInfo","ItemBlind","ItemMatchInfoBlind","ItemSnowBallRewardInfo","ItemSnowBallRewardPlayerNum"
    ,"ItemMatchInfoReward","ItemMatchInfoRank")
end

function KingTexasListener:_regView()
    MainC:doString("ViewLoading")
    local view_loading_fac = ViewLoadingFactory:new(nil, "Loading", "Loading", "Loading", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("Loading", view_loading_fac)
    MainC:doString("ViewMsgBox")
    local view_msgbox_fac = ViewMsgBoxFactory:new(nil, "Common", "MsgBox", "Waiting", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("MsgBox", view_msgbox_fac)
    MainC:doString("ViewLogin")
    MainC:doString("UiResetPwd")
    MainC:doString("UiRegister")
    MainC:doString("UiChooseCountryCode")
    local view_login_fac = ViewLoginFactory:new(nil, "Login", "Login", "Background", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("Login", view_login_fac)
    MainC:doString("ViewAbout")
    local view_about_fac = ViewAboutFactory:new(nil, "About", "About", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("About", view_about_fac)
    MainC:doString("ViewDesktopChatParent")
    local view_chatparent_fac = ViewDesktopChatParentFactory:new(nil, "DesktopChatParent", "DesktopChatParent", "DesktopChat", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("DesktopChatParent", view_chatparent_fac)
    MainC:doString("ViewFloatMsg")
    local view_floatmsg_fac = ViewFloatMsgFactory:new(nil, "Common", "FloatMsg", "Waiting", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("FloatMsg", view_floatmsg_fac)
    MainC:doString("ViewMailDetail")
    local view_maildetail_fac = ViewMailDetailFactory:new(nil, "MailDetail", "MailDetail", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("MailDetail", view_maildetail_fac)
    MainC:doString("ViewNotice")
    local view_notice_fac = ViewNoticeFactory:new(nil, "Notice", "Notice", "Loading", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("Notice", view_notice_fac)
    MainC:doString("ViewPayType")
    local view_paytype_fac = ViewPayTypeFactory:new(nil, "PayType", "PayType", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("PayType", view_paytype_fac)
    MainC:doString("ViewPermanentPosMsg")
    local view_permanent_fac = ViewPermanentPosMsgFactory:new(nil, "Common", "PermanentPosMsg", "Loading", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("PermanentPosMsg", view_permanent_fac)
    MainC:doString("ViewPool")
    local view_pool_fac = ViewPoolFactory:new(nil, "Pool", "Pool", "None", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("Pool", view_pool_fac)
    MainC:doString("ViewShootingText")
    local view_shooting_fac = ViewShootingTextFactory:new(nil, "ShootingText", "ShootingText", "ShootingText", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("ShootingText", view_shooting_fac)
    MainC:doString("ViewTakePhoto")
    local view_takephoto_fac = ViewTakePhotoFactory:new(nil, "TakePhoto", "TakePhoto", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("TakePhoto", view_takephoto_fac)
    MainC:doString("ViewWaiting")
    local view_waiting_fac = ViewWaitingFactory:new(nil, "Common", "Waiting", "Waiting", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("Waiting", view_waiting_fac)
    MainC:doString("ViewWaitingCountDown")
    local view_waiting1_fac = ViewWaitingCountDownFactory:new(nil, "Common", "WaitingCountDown", "Waiting", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("WaitingCountDown", view_waiting1_fac)
    MainC:doString("ViewLotteryTicket")
    MainC:doString("ViewLotteryTicketBase")
    MainC:doString("ViewLotteryTicketRewardPot")
    MainC:doString("ViewLotteryTicketTexas")
    MainC:doString("ItemLotteryTicketBetOperate")
    MainC:doString("ItemLotteryTicketBetPot")
    MainC:doString("ItemLotteryTicketCard")
    MainC:doString("ItemLotteryTicketHistory")
    MainC:doString("ItemLotteryTicketRewardPotPlayerInfo")
    local view_lottery_fac = ViewLotteryTicketFactory:new(nil, "LotteryTicket", "LotteryTicket", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("LotteryTicket", view_lottery_fac)
    MainC:doString("ViewDesktopTexas")
    MainC:doString("PlayerSeatWidgetControllerTexas")
    MainC:doString("PlayerOperateAni")
    MainC:doString("PlayerShowTips")
    MainC:doString("ViewHandCardTexas")
    MainC:doString("ViewPotTexasPoker")
    MainC:doString("ViewDesktopTypeBase")
    MainC:doString("ViewTexasClassic")
    MainC:doString("ViewTexasMTT")
    MainC:doString("DealerEx")
    MainC:doString("ViewMTTGameResult")
    MainC:doString("ItemMttRewardItem")
    local view_mttresult_fac = ViewMTTGameResultFactory:new(nil, "MTTGameResult", "MTTGameResult", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("MTTGameResult", view_mttresult_fac)
    MainC:doString("ViewMTTProcess")
    local view_mttprocess_fac = ViewMTTProcessFactory:new(nil, "MTTProcess", "MTTProcess", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("MTTProcess", view_mttprocess_fac)
    MainC:doString("UiCardCommonEx")
    MainC:doString("UiCardDealingEx")
    MainC:doString("UiChipEx")
    MainC:doString("UiChipMgrEx")
    MainC:doString("ViewPlayerGiftAndVIP")
    MainC:doString("DesktopPlayerTexas")
    MainC:doString("DesktopBase")
    MainC:doString("DesktopTexas")
    MainC:doString("DesktopHelperBase")
    MainC:doString("DesktopHelperTexas")
    MainC:doString("DesktopTypeBase")
    MainC:doString("DesktopTexasClassic")
    MainC:doString("DesktopTexasMTT")
    local view_desktoptexas_fac = ViewDesktopTexasFactory:new(nil, "Desktop", "Desktop", "Background", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("DesktopTexas", view_desktoptexas_fac)
    MainC:doString("ViewDesktopPlayerInfoTexas")
    local view_desktopplayerinfotexas_fac = ViewDesktopPlayerInfoTexasFactory:new(nil, "DesktopPlayerInfo", "DesktopPlayerInfo", "SceneActor", false, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("DesktopPlayerInfoTexas", view_desktopplayerinfotexas_fac)
    MainC:doString("ViewDesktopPlayerOperateTexas")
    MainC:doString("DesktopFastBet")
    local view_desktopoperatetexas_fac = ViewDesktopPlayerOperateTexasFactory:new(nil, "DesktopPlayerOperate", "DesktopPlayerOperate", "PlayerOperateUi", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("DesktopPlayerOperateTexas", view_desktopoperatetexas_fac)
    MainC:doString("ViewLockChatTexas")
    local view_lockchattexas_fac = ViewLockChatTexasFactory:new(nil, "LockChat", "LockChat", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("LockChatTexas", view_lockchattexas_fac)
    MainC:doString("ViewDesktopHintsTexas")
    local view_desktophintstexas_fac = ViewDesktopHintsTexasFactory:new(nil, "DesktopHints", "DesktopHints", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("DesktopHintsTexas", view_desktophintstexas_fac)
    MainC:doString("ViewDesktopMenuTexas")
    local view_desktopmenutexas_fac = ViewDesktopMenuTexasFactory:new(nil, "DesktopMenu", "DesktopMenu", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("DesktopMenuTexas", view_desktopmenutexas_fac)
    MainC:doString("ViewDesktopChatExpression")
    local view_desktopchatexp_fac = ViewDesktopChatExpressionFactory:new(nil, "ChatExPression", "ChatExPression", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("ChatExPression", view_desktopchatexp_fac)
    MainC:doString("ViewDesktopH")
    MainC:doString("ViewDesktopHBase")
    MainC:doString("ViewDesktopHTexas")
    MainC:doString("DesktopHBase")
    MainC:doString("DesktopHTexas")
    MainC:doString("DesktopHUiGold")
    MainC:doString("DesktopHDealer")
    MainC:doString("DesktopHBankPlayer")
    MainC:doString("DesktopHCard")
    MainC:doString("DesktopHCards")
    MainC:doString("DesktopHCardTypeBase")
    MainC:doString("DesktopHCardTypeTexas")
    MainC:doString("DesktopHGoldPool")
    MainC:doString("DesktopHRewardPot")
    MainC:doString("DesktopHSelf")
    MainC:doString("DesktopHStandPlayer")
    MainC:doString("DesktopHChair")
    MainC:doString("DesktopHChair")
    MainC:doString("GoldController")
    MainC:doString("DesktopHBetPot")
    local view_desktoph_fac = ViewDesktopHFactory:new(nil, "DesktopH", "DesktopH", "Background", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("DesktopH", view_desktoph_fac)
    MainC:doString("ViewDesktopHBankList")
    local view_desktophbanklist_fac = ViewDesktopHBankListFactory:new(nil, "DesktopHBankPlayerList", "DesktopHBankPlayerList", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("DesktopHBankList", view_desktophbanklist_fac)
    MainC:doString("ViewDesktopHBetReward")
    local view_desktophbetreward_fac = ViewDesktopHBetRewardFactory:new(nil, "DesktopHBetReward", "DesktopHBetReward", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("DesktopHBetReward", view_desktophbetreward_fac)
    MainC:doString("ViewDesktopHCardType")
    local view_desktophcardtype_fac = ViewDesktopHCardTypeFactory:new(nil, "DesktopHCardType", "DesktopHCardType", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("DesktopHCardType", view_desktophcardtype_fac)
    MainC:doString("ViewDesktopHHelp")
    local view_desktophhelp_fac = ViewDesktopHHelpFactory:new(nil, "DesktopHHelp", "DesktopHHelp", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("DesktopHHelp", view_desktophhelp_fac)
    MainC:doString("ViewDesktopHHistory")
    local view_desktophhistory_fac = ViewDesktopHHistoryFactory:new(nil, "DesktopHHistory", "DesktopHHistory", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("DesktopHHistory", view_desktophhistory_fac)
    MainC:doString("ViewDesktopHMenu")
    local view_desktophmenu_fac = ViewDesktopHMenuFactory:new(nil, "DesktopHMenu", "DesktopHMenu", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("DesktopHMenu", view_desktophmenu_fac)
    MainC:doString("ViewDesktopHResult")
    local view_desktophresult_fac = ViewDesktopHResultFactory:new(nil, "DesktopHResult", "DesktopHResult", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("DesktopHResult", view_desktophresult_fac)
    MainC:doString("ViewDesktopHSetCardType")
    local view_desktophsetcardtype_fac = ViewDesktopHSetCardTypeFactory:new(nil, "DesktopHSetCardType", "DesktopHSetCardType", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("DesktopHSetCardType", view_desktophsetcardtype_fac)
    MainC:doString("ViewDesktopHRewardPot")
    local view_desktophrewardpot_fac = ViewDesktopHRewardPotFactory:new(nil, "DesktopHRewardPot", "DesktopHRewardPot", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("DesktopHRewardPot", view_desktophrewardpot_fac)
    MainC:doString("ViewAgreeOrDisAddFriendRequest")
    local view_agreefriend_fac = ViewAgreeOrDisAddFriendRequestFactory:new(nil, "AgreeOrDisAddFriendRequest", "AgreeOrDisAddFriendRequest", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("AgreeOrDisAddFriendRequest", view_agreefriend_fac)
    MainC:doString("ViewBag")
    local view_bag_fac = ViewBagFactory:new(nil, "Bag", "Bag", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("Bag", view_bag_fac)
    MainC:doString("ViewBank")
    local view_bank_fac = ViewBankFactory:new(nil, "Bank", "Bank", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("Bank", view_bank_fac)
    MainC:doString("ViewChat")
    local view_chat_fac = ViewChatFactory:new(nil, "Chat", "Chat", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("Chat", view_chat_fac)
    MainC:doString("ViewChatChooseTarget")
    local view_chatchoose_fac = ViewChatChooseTargetFactory:new(nil, "ChatChooseTarget", "ChatChooseTarget", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("ChatChooseTarget", view_chatchoose_fac)
    MainC:doString("ViewChatFriend")
    local view_chatfriend_fac = ViewChatFriendFactory:new(nil, "ChatFriend", "ChatFriend", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("ChatFriend", view_chatfriend_fac)
    MainC:doString("ViewChipOperate")
    local view_chipoperate_fac = ViewChipOperateFactory:new(nil, "ChipOperate", "ChipOperate", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("ChipOperate", view_chipoperate_fac)
    MainC:doString("ViewChooseLan")
    local view_chooselan_fac = ViewChooseLanFactory:new(nil, "ChooseLan", "ChooseLan", "Background", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("ChooseLan", view_chooselan_fac)
    MainC:doString("ViewShare")
    local view_share_fac = ViewShareFactory:new(nil, "Share", "Share", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("Share", view_share_fac)
    MainC:doString("ViewShareType")
    local view_sharetype_fac = ViewShareTypeFactory:new(nil, "ShareType", "ShareType", "NomalUi", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("ShareType", view_sharetype_fac)
    MainC:doString("ViewCreateDesktop")
    local view_createdesktop_fac = ViewCreateDesktopFactory:new(nil, "CreateDeskTop", "CreateDeskTop", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("CreateDeskTop", view_createdesktop_fac)
    MainC:doString("ViewDailyReward")
    local view_dailyreward_fac = ViewDailyRewardFactory:new(nil, "DailyReward", "DailyReward", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("DailyReward", view_dailyreward_fac)
    MainC:doString("ViewEdit")
    local view_edit_fac = ViewEditFactory:new(nil, "Edit", "Edit", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("Edit", view_edit_fac)
    MainC:doString("ViewFriend")
    local view_friend_fac = ViewFriendFactory:new(nil, "Friend", "Friend", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("Friend", view_friend_fac)
    MainC:doString("ViewFriendOnLine")
    local view_friendonline_fac = ViewFriendOnLineFactory:new(nil, "FriendOnLine", "FriendOnLine", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("FriendOnLine", view_friendonline_fac)
    MainC:doString("ViewGiftDetail")
    local view_giftdetail_fac = ViewGiftDetailFactory:new(nil, "GiftDetail", "GiftDetail", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("GiftDetail", view_giftdetail_fac)
    MainC:doString("ViewGiftShop")
    local view_giftshop_fac = ViewGiftShopFactory:new(nil, "GiftShop", "GiftShop", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("GiftShop", view_giftshop_fac)
    MainC:doString("ViewGoldTree")
    local view_goldtree_fac = ViewGoldTreeFactory:new(nil, "GoldTree", "GoldTree", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("GoldTree", view_goldtree_fac)
    MainC:doString("ViewInviteFriendPlay")
    local view_invite_fac = ViewInviteFriendPlayFactory:new(nil, "InviteFriendPlay", "InviteFriendPlay", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("InviteFriendPlay", view_invite_fac)
    MainC:doString("ViewIdCardCheck")
    local id_check_fac = ViewIdCardCheckFactory:new(nil, "IdCardCheck", "IdCardCheck", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("IdCardCheck", id_check_fac)
    MainC:doString("ViewLobby")
    local view_classicmodel_fac = ViewLobbyFactory:new(nil, "ClassicModel", "ClassicModel", "Background", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("ClassicModel", view_classicmodel_fac)
    MainC:doString("ViewMail")
    local view_viewmail_fac = ViewMailFactory:new(nil, "Mail", "Mail", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("Mail", view_viewmail_fac)
    MainC:doString("ViewMain")
    MainC:doString("ViewOnlineReward")
    MainC:doString("ViewTimingReward")
    MainC:doString("UiMainPlayerInfo")
    local view_viewmain_fac = ViewMainFactory:new(nil, "Main", "Main", "Background", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("Main", view_viewmain_fac)
    MainC:doString("ViewFeedback")
    local view_viewfeedback_fac = ViewFeedbackFactory:new(nil, "Feedback", "Feedback", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("Feedback", view_viewfeedback_fac)
    MainC:doString("ViewPlayerInfo")
    local view_playerinfo_fac = ViewPlayerInfoFactory:new(nil, "PlayerInfo", "PlayerInfo", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("PlayerInfo", view_playerinfo_fac)
    MainC:doString("ViewPlayerProfile")
    local view_playerprofile_fac = ViewPlayerProfileFactory:new(nil, "PlayerProfile", "PlayerProfile", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("PlayerProfile", view_playerprofile_fac)
    MainC:doString("ViewActivityCenter")
    local view_activitycenter_fac = ViewActivityCenterFactory:new(nil, "ActivityCenter", "ActivityCenter", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("ActivityCenter", view_activitycenter_fac)
    MainC:doString("ViewQuitOrBack")
    local view_back_fac = ViewQuitOrBackFactory:new(nil, "QuitOrBack", "QuitOrBack", "QuitGame", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("QuitOrBack", view_back_fac)
    MainC:doString("ViewRanking")
    local view_ranking_fac = ViewRankingFactory:new(nil, "Ranking", "Ranking", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("Ranking", view_ranking_fac)
    MainC:doString("ViewRechargeFirst")
    local view_recharge_fac = ViewRechargeFirstFactory:new(nil, "RechargeFirst", "RechargeFirst", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("RechargeFirst", view_recharge_fac)
    MainC:doString("ViewResetPwd")
    local view_resetpwd_fac = ViewResetPwdFactory:new(nil, "ResetPwd", "ResetPwd", "NomalUi", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("ResetPwd", view_resetpwd_fac)
    MainC:doString("ViewShop")
    local view_shop_fac = ViewShopFactory:new(nil, "Shop", "Shop", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("Shop", view_shop_fac)
    MainC:doString("ViewPurse")
    local view_purse_fac = ViewPurseFactory:new(nil, "Purse", "Purse", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("Purse", view_purse_fac)
    MainC:doString("ViewHeadIcon")
    MainC:doString("ViewVIPSign")
    MainC:doString("ViewHeadIconBig")
    local view_headionbig_fac = ViewHeadIconBigFactory:new(nil, "Common", "CoHeadIconBig", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("HeadIconBig", view_headionbig_fac)
    MainC:doString("ViewActivityPopUpBox")
    local view_activity_popupbox_fac = ViewActivityPopUpBoxFactory:new(nil, "ActivityPopUpBox", "ActivityPopUpBox", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("ActivityPopUpBox", view_activity_popupbox_fac)
    MainC:doString("ViewMatchLobby")
    local view_matchlobby_fac = ViewMatchLobbyFactory:new(nil, "MatchLobby", "MatchLobby", "Background", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("MatchLobby", view_matchlobby_fac)
    MainC:doString("ViewApplySucceed")
    local view_applysucceed_fac = ViewApplySucceedFactory:new(nil, "ApplySucceed", "ApplySucceed", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("ApplySucceed", view_applysucceed_fac)
    MainC:doString("ViewMatchInfo")
    local view_matchinfo_fac = ViewMatchInfoFactory:new(nil, "MatchInfo", "MatchInfo", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("MatchInfo", view_matchinfo_fac)
    MainC:doString("ViewClub")
    local view_club_fac = ViewClubFactory:new(nil, "Club", "Club", "Background", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("Club", view_club_fac)
    MainC:doString("ViewCreateMatch")
    local view_creatematch = ViewCreateMatchFactory:new(nil, "CreateMatch", "CreateMatch", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("CreateMatch", view_creatematch)
    MainC:doString("ViewBlindTable")
    local view_blindtable = ViewBlindTableFactory:new(nil, "BlindTable", "BlindTable", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("BlindTable", view_blindtable)
    MainC:doString("ViewClubHelp")
    local view_clubhelp = ViewClubHelpFactory:new(nil, "ClubHelp", "ClubHelp", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("ClubHelp", view_clubhelp)
    MainC:doString("ViewJoinMatch")
    local view_joinmatch = ViewJoinMatchFactory:new(nil, "JoinMatch", "JoinMatch", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("JoinMatch", view_joinmatch)
    MainC:doString("ViewGetChipEffect")
    local view_getchipeffect = ViewGetChipEffectFactory:new(nil, "GetChipEffect", "GetChipEffect", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("GetChipEffect", view_getchipeffect)
    MainC:doString("ViewEnterMatchNotify")
    local view_enterMatchNotify = ViewEnterMatchNotifyFactory:new(nil, "EnterMatchNotify", "EnterMatchNotify", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("EnterMatchNotify", view_enterMatchNotify)
    MainC:doString("ViewSnowBallReward")
    local view_snowBallRewardFactory = ViewSnowBallRewardFactory:new(nil, "SnowBallReward", "SnowBallReward", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("SnowBallReward", view_snowBallRewardFactory)
    MainC:doString("ViewEditAddress")
    local view_editAddressFactory = ViewEditAddressFactory:new(nil, "EditAddress", "EditAddress", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:regView("EditAddress", view_editAddressFactory)
    --Item
    MainC:doString("ItemDesktopHBeBankPlayerInfo")
    MainC:doString("ItemDesktopHBetOperate")
    MainC:doString("ItemDesktopHBetPot")
    MainC:doString("ItemDesktopHBetReward")
    MainC:doString("ItemDesktopHGameEndPotResult")
    MainC:doString("ItemDesktopHGameEndWinPlayer")
    MainC:doString("ItemDesktopHHistroy")
    MainC:doString("ItemDesktopHSeat")
    MainC:doString("ItemDesktopHSetCardType")
    MainC:doString("ItemUiDesktopHTongPei")
    MainC:doString("ItemUiDesktopHTongSha")
    MainC:doString("ItemLotteryTicketBetOperate")
    MainC:doString("ItemLotteryTicketBetPot")
    MainC:doString("ItemLotteryTicketCard")
    MainC:doString("ItemLotteryTicketHistory")
    MainC:doString("ItemLotteryTicketRewardPotPlayerInfo")
    MainC:doString("ItemAttachment")
    MainC:doString("ItemBetChipRange")
    MainC:doString("ItemChatEx")
    MainC:doString("ItemChatPresetMsg")
    MainC:doString("ItemDailyReward")
    MainC:doString("ItemDesktopHintsInfo")
    MainC:doString("ItemDesktopRaiseOperateGFlower")
    MainC:doString("ItemHeadIcon")
    MainC:doString("ItemHeadIconWithNickName")
    MainC:doString("ItemMagicExpIcon")
    MainC:doString("ItemMagicExpSender")
    MainC:doString("ItemMail")
    MainC:doString("ItemNotice")
    MainC:doString("ItemNumOperate")
    MainC:doString("ItemPlayerChatLock")
    MainC:doString("ItemPlayerReport")
    MainC:doString("ItemChatDesktop")
    MainC:doString("ItemCountryCode")
    MainC:doString("ItemChatTargetInfo")
    MainC:doString("ItemChooseChatTargetInfo")
    MainC:doString("ItemDesktopHRewardPotPlayerInfo")
    MainC:doString("ItemAnte")
    MainC:doString("ItemGift")
    MainC:doString("ItemGiftType")
    MainC:doString("ItemInviteFriendPlay")
    MainC:doString("ItemLan")
    MainC:doString("ItemLobbyDesk")
    MainC:doString("ItemLobbyDeskPlayInfo")
    MainC:doString("ItemMainOperate")
    MainC:doString("ItemRank")
    MainC:doString("ItemReportPlayerOperate")
    MainC:doString("ItemShowFriendSimple")
    MainC:doString("ItemUiShopConsume")
    MainC:doString("ItemUiShopDiamond")
    MainC:doString("ItemUiShopGold")
    MainC:doString("ItemUiShopVIPInfo")
    MainC:doString("ItemUiShopVIPInfo")
    MainC:doString("ItemVIPBuyInfo")
    MainC:doString("ItemActivity")
    MainC:doString("ItemActivityTitle")
    MainC:doString("ItemUiShopGoods")
    MainC:doString("ItemUiPurseGold")
    MainC:doString("ItemMatchType")
    MainC:doString("ItemMatchInfo")
    MainC:doString("ItemBlind")
    MainC:doString("ItemMatchInfoBlind")
    MainC:doString("ItemMatchInfoReward")
    MainC:doString("ItemSnowBallRewardPlayerNum")
    MainC:doString("ItemSnowBallRewardInfo")
    MainC:doString("ItemMatchInfoRank")
end

function KingTexasListener:_regController()
    MainC:doString("ControllerLogin")
    local con_login_fac = ControllerLoginFactory:new(nil)
    self.ControllerMgr:RegController("Login", con_login_fac)
    MainC:doString("ControllerUCenter")
    local con_ucenter_fac = ControllerUCenterFactory:new(nil)
    self.ControllerMgr:RegController("UCenter", con_ucenter_fac)
    MainC:doString("ControllerActivity")
    local con_activity_fac = ControllerActivityFactory:new(nil)
    self.ControllerMgr:RegController("Activity", con_activity_fac)
    MainC:doString("ControllerActor")
    local con_actor_fac = ControllerActorFactory:new(nil)
    self.ControllerMgr:RegController("Actor", con_actor_fac)
    MainC:doString("ControllerBag")
    local con_bag_fac = ControllerBagFactory:new(nil)
    self.ControllerMgr:RegController("Bag", con_bag_fac)
    MainC:doString("ControllerDesk")
    local con_desk_fac = ControllerDeskFactory:new(nil)
    self.ControllerMgr:RegController("Desk", con_desk_fac)
    MainC:doString("ControllerDeskH")
    local con_deskh_fac = ControllerDeskHFactory:new(nil)
    self.ControllerMgr:RegController("DeskH", con_deskh_fac)
    MainC:doString("ControllerGrow")
    local con_grow_fac = ControllerGrowFactory:new(nil)
    self.ControllerMgr:RegController("Grow", con_grow_fac)
    MainC:doString("ControllerIM")
    MainC:doString("IMChat")
    MainC:doString("IMFeedback")
    MainC:doString("IMChatRecord")
    MainC:doString("IMFriendList")
    MainC:doString("IMMailBox")
    local con_im_fac = ControllerIMFactory:new(nil)
    self.ControllerMgr:RegController("IM", con_im_fac)
    MainC:doString("ControllerLobby")
    local con_lobby_fac = ControllerLobbyFactory:new(nil)
    self.ControllerMgr:RegController("Lobby", con_lobby_fac)
    MainC:doString("ControllerLotteryTicket")
    local con_lottery_fac = ControllerLotteryTicketFactory:new(nil)
    self.ControllerMgr:RegController("LotteryTicket", con_lottery_fac)
    MainC:doString("ControllerMarquee")
    local con_marquee_fac = ControllerMarqueeFactory:new(nil)
    self.ControllerMgr:RegController("Marquee", con_marquee_fac)
    MainC:doString("ControllerPlayer")
    local con_player_fac = ControllerPlayerFactory:new(nil)
    self.ControllerMgr:RegController("Player", con_player_fac)
    MainC:doString("ControllerRanking")
    local con_ranking_fac = ControllerRankingFactory:new(nil)
    self.ControllerMgr:RegController("Ranking", con_ranking_fac)
    MainC:doString("ControllerTrade")
    local con_trade_fac = ControllerTradeFactory:new(nil)
    self.ControllerMgr:RegController("Trade", con_trade_fac)
    MainC:doString("ControllerMTT")
    local con_mtt = ControllerMTTFactory:new(nil)
    self.ControllerMgr:RegController("Mtt", con_mtt)
    MainC:doString("Prop")
    MainC:doString("Item")
    MainC:doString("ItemData1")
    MainC:doString("Unit")
    MainC:doString("UnitBilling")
    MainC:doString("UnitConsume")
    MainC:doString("UnitGoodsVoucher")
    MainC:doString("UnitGoldPackage")
    MainC:doString("UnitGiftNormal")
    MainC:doString("UnitRedEnvelopes")
    MainC:doString("UnitGiftTmp")
    MainC:doString("UnitMagicExpression")
    MainC:doString("OnLineReward")
    MainC:doString("TimingReward")
end

function KingTexasListener:_addUiPackage()
    self:addUiPackage("LotteryTicket")
    self:addUiPackage("Chat")
    self:addUiPackage("ChatFriend")
    self:addUiPackage("About")
    self:addUiPackage("Mail")
    self:addUiPackage("PayType")
    self:addUiPackage("Common")
    self:addUiPackage("ChooseLan")
    self:addUiPackage("Share")
    self:addUiPackage("ShareType")
    self:addUiPackage("Loading")
    self:addUiPackage("LanZh")
    if CS.Casinos.CasinosContext.Instance.UnityAndroid then
        self:addUiPackage("LanEn")
        self:addUiPackage("LanZhAndroid")
    end
    self:addUiPackage("Pool")
    self:addUiPackage("ChatChooseTarget")
    self:addUiPackage("ChatExPression")
    self:addUiPackage("CreateDeskTop")
    self:addUiPackage("AgreeOrDisAddFriendRequest")
    self:addUiPackage("Bank")
    self:addUiPackage("ChipOperate")
    self:addUiPackage("QuitOrBack")
    self:addUiPackage("Login")
    self:addUiPackage("Main")
    self:addUiPackage("Bag")
    self:addUiPackage("MailDetail")
    self:addUiPackage("DailyReward")
    self:addUiPackage("Desktop")
    self:addUiPackage("DesktopChatParent")
    self:addUiPackage("DesktopPlayerInfo")
    self:addUiPackage("DesktopPlayerOperate")
    local d_m = "DeskTopMenu"
    if CS.Casinos.CasinosContext.Instance.UnityAndroid then
        d_m = "DesktopMenu"
    end
    self:addUiPackage(d_m)
    self:addUiPackage("DesktopHints")
    self:addUiPackage("MTTGameResult")
    self:addUiPackage("MTTProcess")
    self:addUiPackage("Friend")
    self:addUiPackage("Feedback")
    self:addUiPackage("InviteFriendPlay")
    self:addUiPackage("IdCardCheck")
    self:addUiPackage("ResetPwd")
    self:addUiPackage("Ranking")
    self:addUiPackage("DesktopH")
    self:addUiPackage("DesktopHBetReward")
    self:addUiPackage("DesktopHTexas")
    self:addUiPackage("DesktopHBankPlayerList")
    self:addUiPackage("DesktopHCardType")
    self:addUiPackage("DesktopHHistory")
    self:addUiPackage("DesktopHRewardPot")
    self:addUiPackage("DesktopHMenu")
    self:addUiPackage("DesktopHHelp")
    self:addUiPackage("DesktopHResult")
    self:addUiPackage("DesktopHSetCardType")
    self:addUiPackage("DesktopHTongSha")
    self:addUiPackage("DesktopHTongPei")
    self:addUiPackage("Edit")
    self:addUiPackage("FriendOnLine")
    self:addUiPackage("GiftDetail")
    self:addUiPackage("GiftShop")
    self:addUiPackage("LockChat")
    self:addUiPackage("PlayerProfile")
    self:addUiPackage("PlayerInfo")
    self:addUiPackage("Notice")
    self:addUiPackage("ShootingText")
    self:addUiPackage("Shop")
    self:addUiPackage("Purse")
    self:addUiPackage("TakePhoto")
    self:addUiPackage("ClassicModel")
    self:addUiPackage("RechargeFirst")
    self:addUiPackage("ActivityPopUpBox")
    self:addUiPackage("ActivityCenter")
    self:addUiPackage("MatchLobby")
    self:addUiPackage("ApplySucceed")
    self:addUiPackage("Club")
    self:addUiPackage("CreateMatch")
    self:addUiPackage("BlindTable")
    self:addUiPackage("ClubHelp")
    self:addUiPackage("JoinMatch")
    self:addUiPackage("GetChipEffect")
    self:addUiPackage("MatchInfo")
    self:addUiPackage("EnterMatchNotify")
    self:addUiPackage("SnowBallReward")
    self:addUiPackage("EditAddress")

    --[[s = self.ViewMgr:getUiPackagePath("DesktopGFlower")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("DesktopMenuGFlower")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("DesktopPlayerInfoGFlower")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("DesktopPlayerOperateGFlower")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("LobbyGFlower")]]

    --[[s = self.ViewMgr:getUiPackagePath("ForestParty")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("ForestPartyMenu")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("ForestPartyRule")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("ForestPartyInfo")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("ForestPartyBet")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("ForestPartyResult")
    casinos_context.Listener:AddUiPackage(s)]]--
end

function KingTexasListener:RegTbData()
    MainC:doString("TbDataHintsInfoTexas")
    self.TbDataMgr:RegTbDataFac("HintsInfoTexas", TbDataFactoryHintsInfoTexas:new(nil))
    MainC:doString("TbDataActorLevel")
    self.TbDataMgr:RegTbDataFac("ActorLevel", TbDataFactoryActorLevel:new(nil))
    MainC:doString("TbDataCommon")
    self.TbDataMgr:RegTbDataFac("Common", TbDataFactoryCommon:new(nil))
    MainC:doString("TbDataDailyReward")
    self.TbDataMgr:RegTbDataFac("DailyReward", TbDataFactoryDailyReward:new(nil))
    MainC:doString("TbDataExpression")
    self.TbDataMgr:RegTbDataFac("Expression", TbDataFactoryExpression:new(nil))
    MainC:doString("TbDataItem")
    self.TbDataMgr:RegTbDataFac("Item", TbDataFactoryItem:new(nil))
    MainC:doString("TbDataItemType")
    self.TbDataMgr:RegTbDataFac("ItemType", TbDataFactoryItemType:new(nil))
    MainC:doString("TbDataLanEn")
    self.TbDataMgr:RegTbDataFac("LanEn", TbDataFactoryLanEn:new(nil))
    MainC:doString("TbDataLans")
    self.TbDataMgr:RegTbDataFac("Lans", TbDataFactoryLans:new(nil))
    MainC:doString("TbDataLanZh")
    self.TbDataMgr:RegTbDataFac("LanZh", TbDataFactoryLanZh:new(nil))
    MainC:doString("TbDataLotteryTicket")
    self.TbDataMgr:RegTbDataFac("LotteryTicket", TbDataFactoryLotteryTicket:new(nil))
    MainC:doString("TbDataLotteryTicketBetOperate")
    self.TbDataMgr:RegTbDataFac("LotteryTicketBetOperate", TbDataFactoryLotteryTicketBetOperate:new(nil))
    MainC:doString("TbDataLotteryTicketGoldPercent")
    self.TbDataMgr:RegTbDataFac("LotteryTicketGoldPercent", TbDataFactoryLotteryTicketGoldPercent:new(nil))
    MainC:doString("TbDataOnlineReward")
    self.TbDataMgr:RegTbDataFac("OnlineReward", TbDataFactoryOnlineReward:new(nil))
    MainC:doString("TbDataPresetMsg")
    self.TbDataMgr:RegTbDataFac("PresetMsg", TbDataFactoryPresetMsg:new(nil))
    MainC:doString("TbDataUnitBilling")
    self.TbDataMgr:RegTbDataFac("UnitBilling", TbDataFactoryUnitBilling:new(nil))
    MainC:doString("TbDataUnitConsume")
    self.TbDataMgr:RegTbDataFac("UnitConsume", TbDataFactoryUnitConsume:new(nil))
    MainC:doString("TbDataUnitGoodsVoucher")
    self.TbDataMgr:RegTbDataFac("UnitGoodsVoucher", TbDataFactoryUnitGoodsVoucher:new(nil))
    MainC:doString("TbDataUnitGoldPackage")
    self.TbDataMgr:RegTbDataFac("UnitGoldPackage", TbDataFactoryUnitGoldPackage:new(nil))
    MainC:doString("TbDataUnitGiftNormal")
    self.TbDataMgr:RegTbDataFac("UnitGiftNormal", TbDataFactoryUnitGiftNormal:new(nil))
    MainC:doString("TbDataUnitRedEnvelopes")
    self.TbDataMgr:RegTbDataFac("UnitRedEnvelopes", TbDataFactoryUnitRedEnvelopes:new(nil))
    MainC:doString("TbDataUnitGiftTmp")
    self.TbDataMgr:RegTbDataFac("UnitGiftTmp", TbDataFactoryUnitGiftTmp:new(nil))
    MainC:doString("TbDataUnitMagicExpression")
    self.TbDataMgr:RegTbDataFac("UnitMagicExpression", TbDataFactoryUnitMagicExpression:new(nil))
    MainC:doString("TbDataVIPLevel")
    self.TbDataMgr:RegTbDataFac("VIPLevel", TbDataFactoryVIPLevel:new(nil))
    MainC:doString("TbDataConfigTexasDesktop")
    self.TbDataMgr:RegTbDataFac("ConfigTexasDesktop", TbDataFactoryConfigTexasDesktop:new(nil))
    MainC:doString("TbDataDesktopInfoTexas")
    self.TbDataMgr:RegTbDataFac("DesktopInfoTexas", TbDataFactoryDesktopInfoTexas:new(nil))
    MainC:doString("TbDataCfigTexasDesktopH")
    self.TbDataMgr:RegTbDataFac("CfigTexasDesktopH", TbDataFactoryCfigTexasDesktopH:new(nil))
    MainC:doString("TbDataCfigTexasDesktopHGoldPercent")
    self.TbDataMgr:RegTbDataFac("CfigTexasDesktopHGoldPercent", TbDataFactoryCfigTexasDesktopHGoldPercent:new(nil))
    MainC:doString("TbDataCfigTexasDesktopHSysBank")
    self.TbDataMgr:RegTbDataFac("CfigTexasDesktopHSysBank", TbDataFactoryCfigTexasDesktopHSysBank:new(nil))
    MainC:doString("TbDataDesktopHBetOperateTexas")
    self.TbDataMgr:RegTbDataFac("DesktopHBetOperateTexas", TbDataFactoryDesktopHBetOperateTexas:new(nil))
    MainC:doString("TbDataDesktopHBetPotTexas")
    self.TbDataMgr:RegTbDataFac("DesktopHBetPotTexas", TbDataFactoryDesktopHBetPotTexas:new(nil))
    MainC:doString("TbDataDesktopHInfoTexas")
    self.TbDataMgr:RegTbDataFac("DesktopHInfoTexas", TbDataFactoryDesktopHInfoTexas:new(nil))
    MainC:doString("TbDataDesktopHBetReward")
    self.TbDataMgr:RegTbDataFac("DesktopHBetReward", TbDataFactoryDesktopHBetReward:new(nil))
    MainC:doString("TbDataTexasRaiseBlinds")
    self.TbDataMgr:RegTbDataFac("TexasRaiseBlinds", TbDataFactoryTexasRaiseBlinds:new(nil))
    MainC:doString("TbDataDesktopFastBet")
    self.TbDataMgr:RegTbDataFac("DesktopFastBet", TbDataFactoryDesktopFastBet:new(nil))
    MainC:doString("TbDataTexasSnowBallRewardPlayerNum")
    self.TbDataMgr:RegTbDataFac("TexasSnowBallRewardPlayerNum", TbDataFactoryTexasSnowBallRewardPlayerNum:new(nil))
    MainC:doString("TbDataTexasSnowBallRewardInfo")
    self.TbDataMgr:RegTbDataFac("TexasSnowBallRewardInfo", TbDataFactoryTexasSnowBallRewardInfo:new(nil))





    --[[MainC:doString("TbDataConfigGFlowerDesktop")
self.TbDataMgr:RegTbDataFac("ConfigGFlowerDesktop",TbDataFactoryConfigGFlowerDesktop:new(nil))
MainC:doString("TbDataDesktopGFlowerBetOperate")
self.TbDataMgr:RegTbDataFac("DesktopGFlowerBetOperate",TbDataFactoryDesktopGFlowerBetOperate:new(nil))
MainC:doString("TbDataDesktopGFlowerCreateInfo")
self.TbDataMgr:RegTbDataFac("DesktopGFlowerCreateInfo",TbDataFactoryDesktopGFlowerCreateInfo:new(nil))
MainC:doString("TbDataDesktopInfoGFlower")
self.TbDataMgr:RegTbDataFac("DesktopInfoGFlower",TbDataFactoryDesktopInfoGFlower:new(nil))
MainC:doString("TbDataCfigGFlowerDesktopH")
self.TbDataMgr:RegTbDataFac("CfigGFlowerDesktopH",TbDataFactoryCfigGFlowerDesktopH:new(nil))
MainC:doString("TbDataCfigGFlowerDesktopHGoldPercent")
self.TbDataMgr:RegTbDataFac("CfigGFlowerDesktopHGoldPercent",TbDataFactoryCfigGFlowerDesktopHGoldPercent:new(nil))
MainC:doString("TbDataCfigGFlowerDesktopHSysBank")
self.TbDataMgr:RegTbDataFac("CfigGFlowerDesktopHSysBank",TbDataFactoryCfigGFlowerDesktopHSysBank:new(nil))
MainC:doString("TbDataDesktopHBetOperateGFlower")
self.TbDataMgr:RegTbDataFac("DesktopHBetOperateGFlower",TbDataFactoryDesktopHBetOperateGFlower:new(nil))
MainC:doString("TbDataDesktopHBetPotGFlower")
self.TbDataMgr:RegTbDataFac("DesktopHBetPotGFlower",TbDataFactoryDesktopHBetPotGFlower:new(nil))
MainC:doString("TbDataDesktopHInfoGFlower")
self.TbDataMgr:RegTbDataFac("DesktopHInfoGFlower",TbDataFactoryDesktopHInfoGFlower:new(nil))
MainC:doString("TbDataCfigZhongFBDesktopH")
self.TbDataMgr:RegTbDataFac("CfigZhongFBDesktopH",TbDataFactoryCfigZhongFBDesktopH:new(nil))
MainC:doString("TbDataCfigZhongFBDesktopHGoldPercent")
self.TbDataMgr:RegTbDataFac("CfigZhongFBDesktopHGoldPercent",TbDataFactoryCfigZhongFBDesktopHGoldPercent:new(nil))
MainC:doString("TbDataCfigZhongFBDesktopHSysBank")
self.TbDataMgr:RegTbDataFac("CfigZhongFBDesktopHSysBank",TbDataFactoryCfigZhongFBDesktopHSysBank:new(nil))
MainC:doString("TbDataDesktopHBetOperateZhongFB")
self.TbDataMgr:RegTbDataFac("DesktopHBetOperateZhongFB",TbDataFactoryDesktopHBetOperateZhongFB:new(nil))
MainC:doString("TbDataDesktopHBetPotZhongFB")
self.TbDataMgr:RegTbDataFac("DesktopHBetPotZhongFB",TbDataFactoryDesktopHBetPotZhongFB:new(nil))
MainC:doString("TbDataDesktopHInfoZhongFB")
self.TbDataMgr:RegTbDataFac("DesktopHInfoZhongFB",TbDataFactoryDesktopHInfoZhongFB:new(nil))
MainC:doString("TbDataCfigNiuNiuDesktopH")
self.TbDataMgr:RegTbDataFac("CfigNiuNiuDesktopH",TbDataFactoryCfigNiuNiuDesktopH:new(nil))
MainC:doString("TbDataCfigNiuNiuDesktopHGoldPercent")
self.TbDataMgr:RegTbDataFac("CfigNiuNiuDesktopHGoldPercent",TbDataFactoryCfigNiuNiuDesktopHGoldPercent:new(nil))
MainC:doString("TbDataCfigNiuNiuDesktopHSysBank")
self.TbDataMgr:RegTbDataFac("CfigNiuNiuDesktopHSysBank",TbDataFactoryCfigNiuNiuDesktopHSysBank:new(nil))
MainC:doString("TbDataDesktopHBetOperateNiuNiu")
self.TbDataMgr:RegTbDataFac("DesktopHBetOperateNiuNiu",TbDataFactoryDesktopHBetOperateNiuNiu:new(nil))
MainC:doString("TbDataDesktopHBetPotNiuNiu")
self.TbDataMgr:RegTbDataFac("DesktopHBetPotNiuNiu",TbDataFactoryDesktopHBetPotNiuNiu:new(nil))
MainC:doString("TbDataDesktopHInfoNiuNiu")
self.TbDataMgr:RegTbDataFac("DesktopHInfoNiuNiu",TbDataFactoryDesktopHInfoNiuNiu:new(nil))
MainC:doString("TbDataForestPartyAnimalList")
self.TbDataMgr:RegTbDataFac("ForestPartyAnimalList",TbDataFactoryForestPartyAnimalList:new(nil))
MainC:doString("TbDataForestPartyBetOperate")
self.TbDataMgr:RegTbDataFac("ForestPartyBetOperate",TbDataFactoryForestPartyBetOperate:new(nil))
MainC:doString("TbDataForestPartyBetPotMultiple")
self.TbDataMgr:RegTbDataFac("ForestPartyBetPotMultiple",TbDataFactoryForestPartyBetPotMultiple:new(nil))
MainC:doString("TbDataForestPartyDesktop")
self.TbDataMgr:RegTbDataFac("ForestPartyDesktop",TbDataFactoryForestPartyDesktop:new(nil))
MainC:doString("TbDataForestPartyLottery")
self.TbDataMgr:RegTbDataFac("ForestPartyLottery",TbDataFactoryForestPartyLottery:new(nil))]]--
end

function KingTexasListener:LoadDataDone()
    MainC:doString("TbDataHelper")
    TbDataHelper:new(nil, self.TbDataMgr)
    MainC:doString("ModelCommon")
    MainC:doString("ModelAccount")
    MainC:doString("ModelActor")
    MainC:doString("ModelWallet")
    MainC:doString("ModelUCenter")
    MainC:doString("ModelTrade")
    MainC:doString("ModelDesktop")
    MainC:doString("ModelDesktopTexas")
    MainC:doString("ModelDesktopTexasMatch")
    MainC:doString("ModelActivity")
    MainC:doString("ModelGrow")
    MainC:doString("ModelPlayer")
    MainC:doString("ModelRank")
    MainC:doString("ModelMarquee")
    MainC:doString("ModelWallet")
    MainC:doString("ModelBag")
    MainC:doString("ModelDesktopH")
    MainC:doString("ModelIM")
    MainC:doString("ModelIMClientEvent")
    MainC:doString("ModelIMMailBox")
    MainC:doString("ModelLotteryTicket")
    MainC:doString("ModelPlayer")
    MainC:doString("ModelPlayerInfo")

    self.LanMgr = LanMgr:new(nil)
    self.LanMgr:parseLanKeyValue()
    self.ViewMgr.LanMgr = self.LanMgr
    self.ControllerMgr.LanMgr = self.LanMgr
    MainC:doString("CasinoHelper")
    CasinoHelper:new(nil, self.ViewMgr.RPC.MessagePack, self.LanMgr)
    MainC:doString("Native")
    Native:new(nil,self.ViewMgr,self)
    MainC:doString("PicCapture")
    self.PicCapture = PicCapture:new(nil,self.ViewMgr,self)
    self.LoadTbDataDone = true
    --local mgr = ControllerMgr:new(nil)
    --self.TbDataMgr:ParseTableAllData("HintsInfoTexas")
    --[[data_mgr.ParseTableAllData<TbDataLanEn>(_eTableName.LanEn.ToString());
    data_mgr.ParseTableAllData<TbDataLanZh>(_eTableName.LanZh.ToString());
    data_mgr.ParseTableAllData<TbDataLans>(_eTableName.Lans.ToString());
    data_mgr.ParseTableAllData<TbDataLotteryTicketBetOperate>(_eTableName.LotteryTicketBetOperate.ToString());
    data_mgr.ParseTableAllData<TbDataLotteryTicketGoldPercent>(_eTableName.LotteryTicketGoldPercent.ToString());
    data_mgr.ParseTableAllData<TbDataActorLevel>(_eTableName.ActorLevel.ToString());
    data_mgr.ParseTableAllData<TbDataDesktopHBetReward>(_eTableName.DesktopHBetReward.ToString());
    data_mgr.ParseTableAllData<TbDataDesktopInfoTexas>(_eTableName.DesktopInfoTexas.ToString());
    data_mgr.ParseTableAllData<TbDataDesktopHInfoTexas>(_eTableName.DesktopHInfoTexas.ToString());
    data_mgr.ParseTableAllData<TbDataDesktopHBetPotTexas>(_eTableName.DesktopHBetPotTexas.ToString());
    data_mgr.ParseTableAllData<TbDataDesktopHBetOperateTexas>(_eTableName.DesktopHBetOperateTexas.ToString());
    data_mgr.ParseTableAllData<TbDataCfigTexasDesktopHGoldPercent>(_eTableName.CfigTexasDesktopHGoldPercent.ToString());
    data_mgr.ParseTableAllData<TbDataCfigTexasDesktopH>(_eTableName.CfigTexasDesktopH.ToString());
    data_mgr.ParseTableAllData<TbDataCfigTexasDesktopHSysBank>(_eTableName.CfigTexasDesktopHSysBank.ToString());
    data_mgr.ParseTableAllData<TbDataExpression>(_eTableName.Expression.ToString());
    data_mgr.ParseTableAllData<TbDataCommon>(_eTableName.Common.ToString());
    data_mgr.ParseTableAllData<TbDataDailyReward>(_eTableName.DailyReward.ToString());
    data_mgr.ParseTableAllData<TbDataItem>(_eTableName.Item.ToString());
    data_mgr.ParseTableAllData<TbDataItemType>(_eTableName.ItemType.ToString());
    data_mgr.ParseTableAllData<TbDataPresetMsg>(_eTableName.PresetMsg.ToString());
    data_mgr.ParseTableAllData<TbDataMustBetDesktopInfoTexas>(_eTableName.MustBetDesktopInfoTexas.ToString());
    data_mgr.ParseTableAllData<TbDataUnitBilling>(_eTableName.UnitBilling.ToString());
    data_mgr.ParseTableAllData<TbDataUnitGiftNormal>(_eTableName.UnitGiftNormal.ToString());
    data_mgr.ParseTableAllData<TbDataUnitGiftTmp>(_eTableName.UnitGiftTmp.ToString());
    data_mgr.ParseTableAllData<TbDataVIPLevel>(_eTableName.VIPLevel.ToString());
    data_mgr.ParseTableAllData<TbDataUnitMagicExpression>(_eTableName.UnitMagicExpression.ToString());
    data_mgr.ParseTableAllData<TbDataForestPartyDesktop>(_eTableName.ForestPartyDesktop.ToString());
    data_mgr.ParseTableAllData<TbDataForestPartyBetOperate>(_eTableName.ForestPartyBetOperate.ToString());
    data_mgr.ParseTableAllData<TbDataForestPartyBetPotMultiple>(_eTableName.ForestPartyBetPotMultiple.ToString());
    data_mgr.ParseTableAllData<TbDataDesktopGFlowerBetOperate>(_eTableName.DesktopGFlowerBetOperate.ToString());
    data_mgr.ParseTableAllData<TbDataDesktopInfoGFlower>(_eTableName.DesktopInfoGFlower.ToString());--]]
end

function KingTexasListener:addUiPackage(name)
    local p = CS.FairyGUI.UIPackage.GetByName(name)
    if p ~= nil then
        CS.FairyGUI.UIPackage.RemovePackage(name,true)
    else
        local s = self.ViewMgr:getUiPackagePath(name)
        CS.Casinos.CasinosContext.Instance.Listener:AddUiPackage(s)
    end
end