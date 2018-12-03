-- Copyright (c) Cragon. All rights reserved.

---------------------------------------
EvUiLoginDeleteGuest = EventBase:new(nil)

function EvUiLoginDeleteGuest:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiLoginDeleteGuest"
    return o
end

function EvUiLoginDeleteGuest:Reset()
end

---------------------------------------
EvUiLoginRequestGetPwd = EventBase:new(nil)

function EvUiLoginRequestGetPwd:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiLoginRequestGetPwd"
    self.super_pwd = nil
    return o
end

function EvUiLoginRequestGetPwd:Reset()
    self.super_pwd = nil
end

---------------------------------------
EvUiReportFriend = EventBase:new(nil)

function EvUiReportFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiReportFriend"
    self.friend_etguid = nil
    self.report_type = nil
    return o
end

function EvUiReportFriend:Reset()
    self.friend_etguid = nil
    self.report_type = nil
end

---------------------------------------
EvUiCloseActivityPopupBox = EventBase:new(nil)

function EvUiCloseActivityPopupBox:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiCloseActivityPopupBox"
    return o
end

function EvUiCloseActivityPopupBox:Reset()
end

---------------------------------------
EvUiRequestFriendAddOrRemove = EventBase:new(nil)

function EvUiRequestFriendAddOrRemove:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestFriendAddOrRemove"
    self.is_add = nil
    self.friend_guid = nil
    self.friend_nickname = nil
    return o
end

function EvUiRequestFriendAddOrRemove:Reset()
    self.is_add = nil
    self.friend_guid = nil
    self.friend_nickname = nil
end

---------------------------------------
EvUiRequestMailRead = EventBase:new(nil)

function EvUiRequestMailRead:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestMailRead"
    self.mail_guid = nil
    return o
end

function EvUiRequestMailRead:Reset()
    self.mail_guid = nil
end

---------------------------------------
EvUiSellItem = EventBase:new(nil)

function EvUiSellItem:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiSellItem"
    self.item_objid = nil
    return o
end

function EvUiSellItem:Reset()
    self.item_objid = nil
end

---------------------------------------
EvUiRemoveItem = EventBase:new(nil)

function EvUiRemoveItem:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRemoveItem"
    self.obj_id = nil
    return o
end

function EvUiRemoveItem:Reset()
    self.obj_id = nil
end

---------------------------------------
EvUiRequestResetPwd = EventBase:new(nil)

function EvUiRequestResetPwd:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestResetPwd"
    self.phone = nil
    self.formatphone = nil
    self.phone_code = nil
    self.new_pwd = nil
    return o
end

function EvUiRequestResetPwd:Reset()
    self.phone = nil
    self.formatphone = nil
    self.phone_code = nil
    self.new_pwd = nil
end

---------------------------------------
EvUiLogin = EventBase:new(nil)

function EvUiLogin:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiLogin"
    self.login_type = 0-- 0 Acc 1 Guest 2 WeiXin
    self.acc = nil
    self.pwd = nil
    self.remeber_pwd = false
    self.phone = nil

    return o
end

function EvUiLogin:Reset()
    self.login_type = 0
    self.acc = nil
    self.pwd = nil
    self.remeber_pwd = false
    self.phone = nil
end

---------------------------------------
EvUiLoginSuccessEx = EventBase:new(nil)

function EvUiLoginSuccessEx:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiLoginSuccessEx"
    self.token = nil

    return o
end

function EvUiLoginSuccessEx:Reset()
    self.token = nil
end

---------------------------------------
EvUiLoginClickBtnRegister = EventBase:new(nil)

function EvUiLoginClickBtnRegister:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiLoginClickBtnRegister"
    self.AccountName = nil
    self.Password = nil
    self.SuperPassword = nil
    self.Email = nil
    self.Identity = nil
    self.Phone = nil
    self.Name = nil
    self.Device = nil
    self.PhoneNum = nil
    self.SecurityCode = nil
    self.IsRegister = false
    self.PhoneVerificationCode = nil
    self.FormatPhone = nil
    return o
end

function EvUiLoginClickBtnRegister:Reset()
    self.AccountName = nil
    self.Password = nil
    self.SuperPassword = nil
    self.Email = nil
    self.Identity = nil
    self.Phone = nil
    self.Name = nil
    self.Device = nil
    self.PhoneNum = nil
    self.SecurityCode = nil
    self.IsRegister = false
    self.PhoneVerificationCode = nil
    self.FormatPhone = nil
end

---------------------------------------
EvUiRequestGetPhoneCode = EventBase:new(nil)

function EvUiRequestGetPhoneCode:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestGetPhoneCode"
    self.Phone = nil
    self.Reson = 0

    return o
end

function EvUiRequestGetPhoneCode:Reset()
    self.Phone = nil
    self.Reson = 0
end

---------------------------------------
EvUiChooseCountry = EventBase:new(nil)

function EvUiChooseCountry:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiChooseCountry"
    self.CountryKey = nil
    self.CountryCode = nil
    self.KeyAndCodeFormat = nil
    return o
end

function EvUiChooseCountry:Reset()
    self.CountryKey = nil
    self.CountryCode = nil
    self.KeyAndCodeFormat = nil
end

---------------------------------------
EvUiLoginClickBtnVisiter = EventBase:new(nil)

function EvUiLoginClickBtnVisiter:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiLoginClickBtnVisiter"
    return o
end

function EvUiLoginClickBtnVisiter:Reset()
end

---------------------------------------
EvUiLoginClickBtnFacebook = EventBase:new(nil)

function EvUiLoginClickBtnFacebook:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiLoginClickBtnFacebook"
    return o
end

function EvUiLoginClickBtnFacebook:Reset()
end

---------------------------------------
EvUiSendSecurityCode = EventBase:new(nil)

function EvUiSendSecurityCode:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiSendSecurityCode"
    self.PhoneNum = nil
    return o
end

function EvUiSendSecurityCode:Reset()
    self.PhoneNum = nil
end

---------------------------------------
EvRegisterDestroy = EventBase:new(nil)

function EvRegisterDestroy:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvRegisterDestroy"
    return o
end

function EvRegisterDestroy:Reset()
end

---------------------------------------
EvUiRequestLockSystemChat = EventBase:new(nil)

function EvUiRequestLockSystemChat:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestLockSystemChat"
    self.requestLock = false
    return o
end

function EvUiRequestLockSystemChat:Reset()
    self.requestLock = false
end

---------------------------------------
EvUiRequestLockPlayerChat = EventBase:new(nil)

function EvUiRequestLockPlayerChat:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestLockPlayerChat"
    self.player_guid = nil
    self.requestLock = false
    return o
end

function EvUiRequestLockPlayerChat:Reset()
    self.player_guid = nil
    self.requestLock = false
end

---------------------------------------
EvUiRequestMailRecvAttachment = EventBase:new(nil)

function EvUiRequestMailRecvAttachment:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestMailRecvAttachment"
    self.mail_guid = nil
    return o
end

function EvUiRequestMailRecvAttachment:Reset()
    self.mail_guid = nil
end

---------------------------------------
EvUiRequestBuyItem = EventBase:new(nil)

function EvUiRequestBuyItem:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestBuyItem"
    self.item_tbid = nil
    self.is_firstrecharge = nil
    self.item_count = nil
    self.pay_type = nil
    return o
end

function EvUiRequestBuyItem:Reset()
    self.item_tbid = nil
    self.is_firstrecharge = nil
    self.item_count = nil
    self.pay_type = nil
end

---------------------------------------
EvUiRequestBuyDiamond = EventBase:new(nil)

function EvUiRequestBuyDiamond:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestBuyDiamond"
    self.buy_diamondid = nil
    return o
end

function EvUiRequestBuyDiamond:Reset()
    self.buy_diamondid = nil
end

---------------------------------------
EvUiBuyItem = EventBase:new(nil)

function EvUiBuyItem:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiBuyItem"
    self.item_id = nil
    self.to_etguid = nil
    return o
end

function EvUiBuyItem:Reset()
    self.item_id = nil
    self.to_etguid = nil
end

---------------------------------------
EvUiDesktopHStandUp = EventBase:new(nil)

function EvUiDesktopHStandUp:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvViewDesktopHStandup"
    self.item_id = nil
    self.to_etguid = nil
    return o
end

function EvUiDesktopHStandUp:Reset()
    self.item_id = nil
    self.to_etguid = nil
end

---------------------------------------
EvUiRequestBuyGold = EventBase:new(nil)

function EvUiRequestBuyGold:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestBuyGold"
    self.buy_goldid = nil
    return o
end

function EvUiRequestBuyGold:Reset()
    self.buy_goldid = nil
end

---------------------------------------
EvUiRequestWebpay = EventBase:new(nil)

function EvUiRequestWebpay:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestWebpay"
    self.Amount = 0
    return o
end

function EvUiRequestWebpay:Reset()
    self.Amount = 0
end

---------------------------------------
EvUiRequestQuicktellerTransfers = EventBase:new(nil)

function EvUiRequestQuicktellerTransfers:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestQuicktellerTransfers"
    return o
end

function EvUiRequestQuicktellerTransfers:Reset()
end

---------------------------------------
EvUiRequestGetMoney = EventBase:new(nil)

function EvUiRequestGetMoney:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestGetMoney"
    self.GetMoneyNum = nil
    self.toAccountNumber = nil
    self.cbnCode = nil
    self.receiverLastName = nil
    self.receiverOtherName = nil

    return o
end

function EvUiRequestGetMoney:Reset()
    self.GetMoneyNum = nil
    self.toAccountNumber = nil
    self.cbnCode = nil
    self.receiverLastName = nil
    self.receiverOtherName = nil
end

---------------------------------------
EvUiSendSecurityCode = EventBase:new(nil)

function EvUiSendSecurityCode:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiSendSecurityCode"
    self.phone_num = nil
    return o
end

function EvUiSendSecurityCode:Reset()
    self.phone_num = nil
end

---------------------------------------
EvUiAgreeAddFriend = EventBase:new(nil)

function EvUiAgreeAddFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiAgreeAddFriend"
    self.from_etguid = nil
    self.ev = nil
    return o
end

function EvUiAgreeAddFriend:Reset()
    self.from_etguid = nil
    self.ev = nil
end

---------------------------------------
EvUiRefuseAddFriend = EventBase:new(nil)

function EvUiRefuseAddFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRefuseAddFriend"
    self.from_etguid = nil
    self.ev = nil
    return o
end

function EvUiRefuseAddFriend:Reset()
    self.from_etguid = nil
    self.ev = nil
end

---------------------------------------
EvUiRequestBankDeposit = EventBase:new(nil)

function EvUiRequestBankDeposit:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestBankDeposit"
    self.deposit_chip = nil
    return o
end

function EvUiRequestBankDeposit:Reset()
    self.deposit_chip = nil
end

---------------------------------------
EvUiRequestBankWithdraw = EventBase:new(nil)

function EvUiRequestBankWithdraw:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestBankWithdraw"
    self.withdraw_chip = nil
    return o
end

function EvUiRequestBankWithdraw:Reset()
    self.withdraw_chip = nil
end

---------------------------------------
EvUiClickConfirmChipTransaction = EventBase:new(nil)

function EvUiClickConfirmChipTransaction:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickConfirmChipTransaction"
    self.send_target_etguid = nil
    self.chip = nil
    return o
end

function EvUiClickConfirmChipTransaction:Reset()
    self.send_target_etguid = nil
    self.chip = nil
end

---------------------------------------
EvUiSetUnSendDesktopMsg = EventBase:new(nil)

function EvUiSetUnSendDesktopMsg:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiSetUnSendDesktopMsg"
    self.text = nil
    return o
end

function EvUiSetUnSendDesktopMsg:Reset()
    self.text = nil
end

---------------------------------------
EvUiSendMsg = EventBase:new(nil)

function EvUiSendMsg:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiSendMsg"
    self.chat_msg = nil
    return o
end

function EvUiSendMsg:Reset()
    self.chat_msg = nil
end

---------------------------------------
EvUiChatConfirmRead = EventBase:new(nil)

function EvUiChatConfirmRead:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiChatConfirmRead"
    self.friend_etguid = nil
    self.msg_id = nil
    return o
end

function EvUiChatConfirmRead:Reset()
    self.friend_etguid = nil
    self.msg_id = nil
end

---------------------------------------
EvUiSendFeedbackMsg = EventBase:new(nil)

function EvUiSendFeedbackMsg:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiSendFeedbackMsg"
    self.chat_msg = nil
    return o
end

function EvUiSendFeedbackMsg:Reset()
    self.chat_msg = nil
end

---------------------------------------
EvUiFeedbackConfirmRead = EventBase:new(nil)

function EvUiFeedbackConfirmRead:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiFeedbackConfirmRead"
    return o
end

function EvUiFeedbackConfirmRead:Reset()
end

---------------------------------------
EvUiClickViewInDesk = EventBase:new(nil)

function EvUiClickViewInDesk:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickViewInDesk"
    self. desktop_filter = nil
    self.seat_index = nil
    return o
end

function EvUiClickViewInDesk:Reset()
    self. desktop_filter = nil
    self.seat_index = nil
end

---------------------------------------
EvUiClickChooseFriendChatTarget = EventBase:new(nil)

function EvUiClickChooseFriendChatTarget:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickChooseFriendChatTarget"
    return o
end

function EvUiClickChooseFriendChatTarget:Reset()
end

---------------------------------------
EvUiClickChooseFriend = EventBase:new(nil)

function EvUiClickChooseFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickChooseFriend"
    self.friend_info = nil
    self.is_choosechat = false
    self.is_recommand = false
    return o
end

function EvUiClickChooseFriend:Reset()
    self.friend_info = nil
    self.is_choosechat = false
    self.is_recommand = false
end

---------------------------------------
EvUiCurrentChatTargetChange = EventBase:new(nil)

function EvUiCurrentChatTargetChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiCurrentChatTargetChange"
    self.current_chattarget = nil
    return o
end

function EvUiCurrentChatTargetChange:Reset()
    self.current_chattarget = nil
end

---------------------------------------
EvUiClickConfirmChipTransaction = EventBase:new(nil)

function EvUiClickConfirmChipTransaction:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickConfirmChipTransaction"
    self.send_target_etguid = nil
    self.chip = nil
    return o
end

function EvUiClickConfirmChipTransaction:Reset()
    self.send_target_etguid = nil
    self.chip = nil
end

---------------------------------------
EvUiCreateMainUi = EventBase:new(nil)

function EvUiCreateMainUi:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiCreateMainUi"
    return o
end

---------------------------------------
EvUiRequestUpdatePublicMatchPlayerNum = EventBase:new(nil)

function EvUiRequestUpdatePublicMatchPlayerNum:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestUpdatePublicMatchPlayerNum"
    return o
end

---------------------------------------
EvUiRequestUpdatePrivateMatchPlayerNum = EventBase:new(nil)

function EvUiRequestUpdatePrivateMatchPlayerNum:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestUpdatePrivateMatchPlayerNum"
    return o
end

---------------------------------------
EvUiRequestCreatePrivateMatch = EventBase:new(nil)

function EvUiRequestCreatePrivateMatch:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestCreatePrivateMatch"
    o.CreateMatchInfo = nil
    return o
end

function EvUiRequestCreatePrivateMatch:Reset()
    self.CreateMatchInfo = nil
end

---------------------------------------
EvUiClickCreateDeskTop = EventBase:new(nil)

function EvUiClickCreateDeskTop:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickCreateDeskTop"
    self.create_info = nil
    return o
end

function EvUiClickCreateDeskTop:Reset()
    self.create_info = nil
end

---------------------------------------
EvUiCreateExchangeChip = EventBase:new(nil)

function EvUiCreateExchangeChip:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiCreateExchangeChip"
    return o
end

function EvUiCreateExchangeChip:Reset()
    self.create_info = nil
end

---------------------------------------
EvUiFindFriend = EventBase:new(nil)

function EvUiFindFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiFindFriend"
    self.search_filter = nil
    return o
end

function EvUiFindFriend:Reset()
    self.search_filter = nil
end

---------------------------------------
EvUiAddFriend = EventBase:new(nil)

function EvUiAddFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiAddFriend"
    self.friend_etguid = nil
    return o
end

function EvUiAddFriend:Reset()
    self.friend_etguid = nil
end

---------------------------------------
EvUiDeleteFriend = EventBase:new(nil)

function EvUiDeleteFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiDeleteFriend"
    self.friend_etguid = nil
    return o
end

function EvUiDeleteFriend:Reset()
    self.friend_etguid = nil
end

---------------------------------------
EvUiClickSearchDesk = EventBase:new(nil)

function EvUiClickSearchDesk:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickSearchDesk"
    self.desktop_searchfilter = nil
    return o
end

function EvUiClickSearchDesk:Reset()
    self.desktop_searchfilter = nil
end

---------------------------------------
EvUiRequestGetCurrentFriendPlayDesk = EventBase:new(nil)

function EvUiRequestGetCurrentFriendPlayDesk:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestGetCurrentFriendPlayDesk"
    self.player_guid = nil
    return o
end

function EvUiRequestGetCurrentFriendPlayDesk:Reset()
    self.player_guid = nil
end

---------------------------------------
EvUiClickSearchFriendsDesk = EventBase:new(nil)

function EvUiClickSearchFriendsDesk:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickSearchFriendsDesk"
    self.friend_state = nil
    return o
end

function EvUiClickSearchFriendsDesk:Reset()
    self.friend_state = nil
end

---------------------------------------
EvUiClickChangePlayerNickName = EventBase:new(nil)

function EvUiClickChangePlayerNickName:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickChangePlayerNickName"
    self.new_name = nil
    return o
end

function EvUiClickChangePlayerNickName:Reset()
    self.new_name = nil
end

---------------------------------------
EvUiClickChipTransaction = EventBase:new(nil)

function EvUiClickChipTransaction:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickChipTransaction"
    self.send_target_etguid = nil
    return o
end

function EvUiClickChipTransaction:Reset()
    self.send_target_etguid = nil
end

---------------------------------------
EvUiClickChangePlayerIndividualSignature = EventBase:new(nil)

function EvUiClickChangePlayerIndividualSignature:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickChangePlayerIndividualSignature"
    self.new_individual_signature = nil
    return o
end

function EvUiClickChangePlayerIndividualSignature:Reset()
    self.new_individual_signature = nil
end

---------------------------------------
EvUiClickChangePlayerIndividualSignature = EventBase:new(nil)

function EvUiClickChangePlayerIndividualSignature:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickChangePlayerIndividualSignature"
    self.new_individual_signature = nil
    return o
end

function EvUiClickChangePlayerIndividualSignature:Reset()
    self.new_individual_signature = nil
end

---------------------------------------
EvUiClickLogin = EventBase:new(nil)

function EvUiClickLogin:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickLogin"
    self.new_individual_signature = nil
    return o
end

function EvUiClickLogin:Reset()
    self.new_individual_signature = nil
end

---------------------------------------
EvUiClickInviteFriend = EventBase:new(nil)

function EvUiClickInviteFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickInviteFriend"
    return o
end

function EvUiClickInviteFriend:Reset()
end

---------------------------------------
EvUiClickShop = EventBase:new(nil)

function EvUiClickShop:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickShop"
    return o
end

function EvUiClickShop:Reset()
end

---------------------------------------
EvUiClickVip = EventBase:new(nil)

function EvUiClickVip:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickVip"
    return o
end

function EvUiClickVip:Reset()
end

---------------------------------------
EvUiClickHelp = EventBase:new(nil)

function EvUiClickHelp:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickHelp"
    return o
end

function EvUiClickHelp:Reset()
end

---------------------------------------
EvUiGetRankingGold = EventBase:new(nil)

function EvUiGetRankingGold:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiGetRankingGold"
    return o
end

function EvUiGetRankingGold:Reset()
end

---------------------------------------
EvUiGetRankingDiamond = EventBase:new(nil)

function EvUiGetRankingDiamond:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiGetRankingDiamond"
    return o
end

function EvUiGetRankingDiamond:Reset()
end

---------------------------------------
EvUiGetRankingWinGold = EventBase:new(nil)

function EvUiGetRankingWinGold:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiGetRankingWinGold"
    return o
end

function EvUiGetRankingWinGold:Reset()
end

---------------------------------------
EvUiGetRankingRedEnvelopes = EventBase:new(nil)

function EvUiGetRankingRedEnvelopes:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiGetRankingRedEnvelopes"
    return o
end

function EvUiGetRankingRedEnvelopes:Reset()
end

---------------------------------------
EvUiClickRefreshIPAddress = EventBase:new(nil)

function EvUiClickRefreshIPAddress:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickRefreshIPAddress"
    return o
end

function EvUiClickRefreshIPAddress:Reset()
end

---------------------------------------
EvUiRequestFirstRecharge = EventBase:new(nil)

function EvUiRequestFirstRecharge:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestFirstRecharge"
    return o
end

function EvUiRequestFirstRecharge:Reset()
end

---------------------------------------
EvUiClickDeleteFriendChatRecord = EventBase:new(nil)

function EvUiClickDeleteFriendChatRecord:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickDeleteFriendChatRecord"
    self.friend_etguid = nil
    return o
end

function EvUiClickDeleteFriendChatRecord:Reset()
    self.friend_etguid = nil
end

---------------------------------------
EvUiClickChooseFriend = EventBase:new(nil)

function EvUiClickChooseFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickChooseFriend"
    self.friend_info = nil
    self.is_choosechat = nil
    self.is_recommand = nil
    return o
end

function EvUiClickChooseFriend:Reset()
    self.friend_info = nil
    self.is_choosechat = nil
    self.is_recommand = nil
end

---------------------------------------
EvUiInviteFriendPlayTogether = EventBase:new(nil)

function EvUiInviteFriendPlayTogether:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiInviteFriendPlayTogether"
    self.friend_guid = nil
    self.friend_desktopguid = nil
    return o
end

function EvUiInviteFriendPlayTogether:Reset()
    self.friend_guid = nil
    self.friend_desktopguid = nil
end

---------------------------------------
EvUiChangeLan = EventBase:new(nil)

function EvUiChangeLan:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiChangeLan"
    self.lan = nil
    return o
end

function EvUiChangeLan:Reset()
    self.lan = nil
end

---------------------------------------
EvUiClickPlayInDesk = EventBase:new(nil)

function EvUiClickPlayInDesk:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickPlayInDesk"
    self.desk_etguid = nil
    self.desktop_filter = nil
    self.seat_index = nil
    return o
end

function EvUiClickPlayInDesk:Reset()
    self.desk_etguid = nil
    self.desktop_filter = nil
    self.seat_index = nil
end

---------------------------------------
EvUiClickViewInDesk = EventBase:new(nil)

function EvUiClickViewInDesk:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickViewInDesk"
    self.desk_etguid = nil
    self.desktop_filter = nil
    self.seat_index = nil
    return o
end

function EvUiClickViewInDesk:Reset()
    self.desk_etguid = nil
    self.desktop_filter = nil
    self.seat_index = nil
end

---------------------------------------
EvUiRequestPublicMatchList = EventBase:new(nil)

function EvUiRequestPublicMatchList:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestPublicMatchList"
    return o
end

function EvUiRequestPublicMatchList:Reset()
end

---------------------------------------
EvUiRequestPrivateMatchList = EventBase:new(nil)

function EvUiRequestPrivateMatchList:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestPrivateMatchList"
    return o
end

function EvUiRequestPrivateMatchList:Reset()
end

---------------------------------------
-- 请求报名比赛
EvUiRequestSignUpMatch = EventBase:new()

function EvUiRequestSignUpMatch:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestSignUpMatch"
    self.MatchGuid = nil
    return o
end

function EvUiRequestSignUpMatch:Reset()
    self.MatchGuid = nil
end

---------------------------------------
-- 请求进入比赛
EvUiRequestEnterMatch = EventBase:new()

function EvUiRequestEnterMatch:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestEnterMatch"
    self.MatchGuid = nil

    return o
end

function EvUiRequestEnterMatch:Reset()
    self.MatchGuid = nil
end

---------------------------------------
-- 请求获取比赛详细信息
EvUiRequestMatchDetailedInfo = EventBase:new(nil)

function EvUiRequestMatchDetailedInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestMatchDetailedInfo"
    o.MatchGuid = nil
    o.MatchType = nil
    return o
end

function EvUiRequestMatchDetailedInfo:Reset()
    self.MatchGuid = nil
end

---------------------------------------
EvUiRequestGetMatchDetailedInfoByInvitation = EventBase:new(nil)

function EvUiRequestGetMatchDetailedInfoByInvitation:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestGetMatchDetailedInfoByInvitation"
    o.InvitationCode = nil
    return o
end

function EvUiRequestGetMatchDetailedInfoByInvitation:Reset()
    self.InvitationCode = nil
end

---------------------------------------
EvUiRequestCancelSignupMatch = EventBase:new(nil)

function EvUiRequestCancelSignupMatch:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestCancelSignupMatch"
    o.MatchGuid = nil
    return o
end

function EvUiRequestCancelSignupMatch:Reset()
    self.MatchGuid = nil
end

---------------------------------------
EvRequesetGetExchangeCodeItem = EventBase:new(nil)

function EvRequesetGetExchangeCodeItem:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvRequesetGetExchangeCodeItem"
    self.acc = nil
    self.pwd = nil
    return o
end

function EvRequesetGetExchangeCodeItem:Reset()
    self.acc = nil
    self.pwd = nil
end

---------------------------------------
EvCreateGiftShop = EventBase:new(nil)

function EvCreateGiftShop:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvCreateGiftShop"
    self.is_tmp_gift = false
    self.not_indesktop = false
    self.to_player_etguid = nil
    return o
end

function EvCreateGiftShop:Reset()
    self.is_tmp_gift = false
    self.not_indesktop = false
    self.to_player_etguid = nil
end

---------------------------------------
EvLoadPlayerIconSuccess = EventBase:new(nil)

function EvLoadPlayerIconSuccess:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvLoadPlayerIconSuccess"
    self.et_guid = nil
    self.icon = nil
    self.fariy_t = nil
    return o
end

function EvLoadPlayerIconSuccess:Reset()
    self.et_guid = nil
    self.icon = nil
    self.fariy_t = nil
end

---------------------------------------
EvEntityGetRankingGold = EventBase:new(nil)

function EvEntityGetRankingGold:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGetRankingGold"
    self.ListRanking = {}
    return o
end

function EvEntityGetRankingGold:Reset()
    self.ListRanking = {}
end

---------------------------------------
EvEntityGetRankingDimond = EventBase:new(nil)

function EvEntityGetRankingDimond:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGetRankingDimond"
    self.ListRanking = {}
    return o
end

function EvEntityGetRankingDimond:Reset()
    self.ListRanking = {}
end

---------------------------------------
EvEntityReceiceMarquee = EventBase:new(nil)

function EvEntityReceiceMarquee:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityReceiceMarquee"
    self.im_marquee = nil
    return o
end

function EvEntityReceiceMarquee:Reset()
    self.im_marquee = nil
end

---------------------------------------
EvEntityGetPlayerInfoOther = EventBase:new(nil)

function EvEntityGetPlayerInfoOther:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGetPlayerInfoOther"
    self.player_info = nil
    self.ticket = nil
    return o
end

function EvEntityGetPlayerInfoOther:Reset()
    self.player_info = nil
    self.ticket = nil
end

---------------------------------------
EvEntityBagDeleteItem = EventBase:new(nil)

function EvEntityBagDeleteItem:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityBagDeleteItem"
    self.item_objid = nil
    return o
end

function EvEntityBagDeleteItem:Reset()
    self.item_objid = nil
end

---------------------------------------
EvEntityBagAddItem = EventBase:new(nil)

function EvEntityBagAddItem:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityBagAddItem"
    self.item = nil
    return o
end

function EvEntityBagAddItem:Reset()
    self.item = nil
end

---------------------------------------
EvEntityGoldChanged = EventBase:new(nil)

function EvEntityGoldChanged:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGoldChanged"
    self.change_reason = nil
    self.gold_acc = nil
    self.delta_gold = nil
    self.user_data = nil
    return o
end

function EvEntityGoldChanged:Reset()
    self.change_reason = nil
    self.gold_acc = nil
    self.delta_gold = nil
    self.user_data = nil
end

---------------------------------------
EvEntityDiamondChanged = EventBase:new(nil)

function EvEntityDiamondChanged:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDiamondChanged"
    return o
end

function EvEntityDiamondChanged:Reset()
end

---------------------------------------
EvEntityPointChanged = EventBase:new(nil)

function EvEntityPointChanged:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityPointChanged"
    return o
end

function EvEntityPointChanged:Reset()
end

---------------------------------------
EvEntityBankGoldChange = EventBase:new(nil)

function EvEntityBankGoldChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityBankGoldChange"
    self.bank_gold = nil
    self.gold_acc = nil
    return o
end

function EvEntityBankGoldChange:Reset()
    self.bank_gold = nil
    self.gold_acc = nil
end

---------------------------------------
EvEntityFriendOnlineStateChange = EventBase:new(nil)

function EvEntityFriendOnlineStateChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityFriendOnlineStateChange"
    self.player_info = nil
    return o
end

function EvEntityFriendOnlineStateChange:Reset()
    self.player_info = nil
end

---------------------------------------
EvEntityNotifyDeleteFriend = EventBase:new(nil)

function EvEntityNotifyDeleteFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityNotifyDeleteFriend"
    self.friend_etguid = nil
    self.map_friend = nil
    return o
end

function EvEntityNotifyDeleteFriend:Reset()
    self.friend_etguid = nil
    self.map_friend = nil
end

---------------------------------------
EvEntityDeleteFriendChatRecordSuccess = EventBase:new(nil)

function EvEntityDeleteFriendChatRecordSuccess:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDeleteFriendChatRecordSuccess"
    self.friend_etguid = nil
    return o
end

function EvEntityDeleteFriendChatRecordSuccess:Reset()
    self.friend_etguid = nil
end

---------------------------------------
EvEntityReceiveFriendSingleChat = EventBase:new(nil)

function EvEntityReceiveFriendSingleChat:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityReceiveFriendSingleChat"
    self.chat_msg = nil
    self.friend_etguid = nil
    return o
end

function EvEntityReceiveFriendSingleChat:Reset()
    self.chat_msg = nil
    self.friend_etguid = nil
end

---------------------------------------
EvEntityReceiveFriendChats = EventBase:new(nil)

function EvEntityReceiveFriendChats:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityReceiveFriendChats"
    self.list_allchats = nil
    self.friend_etguid = nil
    return o
end

function EvEntityReceiveFriendChats:Reset()
    self.list_allchats = nil
    self.friend_etguid = nil
end

---------------------------------------
EvEntityReceiveFeedbackChat = EventBase:new(nil)

function EvEntityReceiveFeedbackChat:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityReceiveFeedbackChat"
    self.chat_msg = nil
    return o
end

function EvEntityReceiveFeedbackChat:Reset()
    self.chat_msg = nil
end

---------------------------------------
EvEntityReceiveFeedbackChats = EventBase:new(nil)

function EvEntityReceiveFeedbackChats:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityReceiveFeedbackChats"
    self.list_allchats = nil
    return o
end

function EvEntityReceiveFeedbackChats:Reset()
    self.list_allchats = nil
end

---------------------------------------
EvEntityChatRecordRequestResult = EventBase:new(nil)

function EvEntityChatRecordRequestResult:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityChatRecordRequestResult"
    self.list_allchats = nil
    self.friend_etguid = nil
    return o
end

function EvEntityChatRecordRequestResult:Reset()
    self.list_allchats = nil
    self.friend_etguid = nil
end

---------------------------------------
EvEntityPlayerGiveChipQueryRangeRequestResult = EventBase:new(nil)

function EvEntityPlayerGiveChipQueryRangeRequestResult:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityPlayerGiveChipQueryRangeRequestResult"
    self.give_chip_min = nil
    self.give_chip_max = nil
    self.is_success = nil
    return o
end

function EvEntityPlayerGiveChipQueryRangeRequestResult:Reset()
    self.give_chip_min = nil
    self.give_chip_max = nil
    self.is_success = nil
end

---------------------------------------
EvEntityRefreshFriendList = EventBase:new(nil)

function EvEntityRefreshFriendList:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityRefreshFriendList"
    self.map_friendinfo = nil
    return o
end

function EvEntityRefreshFriendList:Reset()
    self.map_friendinfo = nil
end

---------------------------------------
EvEntityRefreshFriendInfo = EventBase:new(nil)

function EvEntityRefreshFriendInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityRefreshFriendInfo"
    self.player_info = nil
    return o
end

function EvEntityRefreshFriendInfo:Reset()
    self.player_info = nil
end

---------------------------------------
EvEntityFriendGoldChange = EventBase:new(nil)

function EvEntityFriendGoldChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityFriendGoldChange"
    self.friend_guid = nil
    self.current_gold = 0
    return o
end

function EvEntityFriendGoldChange:Reset()
    self.friend_guid = nil
    self.current_gold = 0
end

---------------------------------------
EvEntityFindFriend = EventBase:new(nil)

function EvEntityFindFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityFindFriend"
    self.list_friend_item = nil
    return o
end

function EvEntityFindFriend:Reset()
    self.list_friend_item = nil
end

---------------------------------------
EvEntityGetLobbyDeskList = EventBase:new(nil)

function EvEntityGetLobbyDeskList:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGetLobbyDeskList"
    self.list_desktop = nil
    return o
end

function EvEntityGetLobbyDeskList:Reset()
    self.list_desktop = nil
end

---------------------------------------
EvEntitySearchDesktopFollowFriend = EventBase:new(nil)

function EvEntitySearchDesktopFollowFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntitySearchDesktopFollowFriend"
    self.desktop_info = nil
    return o
end

function EvEntitySearchDesktopFollowFriend:Reset()
    self.desktop_info = nil
end

---------------------------------------
EvEntitySearchPlayingFriend = EventBase:new(nil)

function EvEntitySearchPlayingFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntitySearchPlayingFriend"
    self.list_playerinfo = nil
    return o
end

function EvEntitySearchPlayingFriend:Reset()
    self.list_playerinfo = nil
end

---------------------------------------
EvEntityGetPlayerModuleDataSuccess = EventBase:new(nil)

function EvEntityGetPlayerModuleDataSuccess:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGetPlayerModuleDataSuccess"
    self.player_moduledata = nil
    return o
end

function EvEntityGetPlayerModuleDataSuccess:Reset()
    self.player_moduledata = nil
end

---------------------------------------
EvEntityPlayerInfoChanged = EventBase:new(nil)

function EvEntityPlayerInfoChanged:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityPlayerInfoChanged"
    self.controller_actor = nil
    return o
end

function EvEntityPlayerInfoChanged:Reset()
    self.controller_actor = nil
end

---------------------------------------
EvEntityCurrentTmpGiftChange = EventBase:new(nil)

function EvEntityCurrentTmpGiftChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityCurrentTmpGiftChange"
    return o
end

function EvEntityCurrentTmpGiftChange:Reset()
end

---------------------------------------
EvEntityGetRankingDiamond = EventBase:new(nil)

function EvEntityGetRankingDiamond:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGetRankingDiamond"
    self.list_ranking = nil
    return o
end

function EvEntityGetRankingDiamond:Reset()
    self.list_ranking = nil
end

---------------------------------------
EvEntityGetRankingGold = EventBase:new(nil)

function EvEntityGetRankingGold:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGetRankingGold"
    self.list_rank = nil
    return o
end

function EvEntityGetRankingGold:Reset()
    self.list_rank = nil
end

---------------------------------------
EvEntityGetRankingWinGold = EventBase:new(nil)

function EvEntityGetRankingWinGold:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGetRankingWinGold"
    self.list_ranking = nil
    return o
end

function EvEntityGetRankingWinGold:Reset()
    self.list_ranking = nil
end

---------------------------------------
EvEntityGetRankingRedEnvelopes = EventBase:new(nil)

function EvEntityGetRankingRedEnvelopes:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGetRankingRedEnvelopes"
    self.list_ranking = nil
    return o
end

function EvEntityGetRankingRedEnvelopes:Reset()
    self.list_ranking = nil
end

---------------------------------------
EvEntityBuyVIP = EventBase:new(nil)

function EvEntityBuyVIP:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityBuyVIP"
    self.buy_id = nil
    return o
end

function EvEntityBuyVIP:Reset()
    self.buy_id = nil
end

---------------------------------------
EvGetPicUpLoadSuccess = EventBase:new(nil)

function EvGetPicUpLoadSuccess:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvGetPicUpLoadSuccess"
    return o
end

function EvGetPicUpLoadSuccess:Reset()
end

---------------------------------------
EvRequestGetPlayerModuleData = EventBase:new(nil)

function EvRequestGetPlayerModuleData:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvRequestGetPlayerModuleData"
    self.factory_name = nil
    return o
end

function EvRequestGetPlayerModuleData:Reset()
    self.factory_name = nil
end

---------------------------------------
-- 福利视图，点击领取在线奖励按钮
EvViewRewardClickBtnOnlineReward = EventBase:new(nil)

function EvViewRewardClickBtnOnlineReward:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvViewRewardClickBtnOnlineReward"
    return o
end

function EvViewRewardClickBtnOnlineReward:Reset()
end

---------------------------------------
-- 福利视图，点击领取定时奖励按钮
EvViewRewardClickBtnTimingReward = EventBase:new(nil)

function EvViewRewardClickBtnTimingReward:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvViewRewardClickBtnTimingReward"
    return o
end

function EvViewRewardClickBtnTimingReward:Reset()
end

---------------------------------------
EvEntityPlayerInitDone = EventBase:new(nil)
function EvEntityPlayerInitDone:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityPlayerInitDone"
    return o
end

function EvEntityPlayerInitDone:Reset()
end

---------------------------------------
EvEntityRecommendPlayerList = EventBase:new(nil)

function EvEntityRecommendPlayerList:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityRecommendPlayerList"
    self.list_recommend = nil
    return o
end

function EvEntityRecommendPlayerList:Reset()
    self.list_recommend = nil
end

---------------------------------------
EvEntitySetOnLinePlayerNum = EventBase:new(nil)

function EvEntitySetOnLinePlayerNum:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntitySetOnLinePlayerNum"
    self.online_num = 0
    return o
end

function EvEntitySetOnLinePlayerNum:Reset()
    self.online_num = 0
end

---------------------------------------
EvEntityOnGrowRewardSnapshot = EventBase:new(nil)

function EvEntityOnGrowRewardSnapshot:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityOnGrowRewardSnapshot"
    self.grow_data = nil
    return o
end

function EvEntityOnGrowRewardSnapshot:Reset()
    self.grow_data = nil
end

---------------------------------------
EvUiClickChatmsg = EventBase:new(nil)

function EvUiClickChatmsg:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickChatmsg"
    return o
end

function EvUiClickChatmsg:Reset()
end

---------------------------------------
EvUiClickFriend = EventBase:new(nil)

function EvUiClickFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickFriend"
    return o
end

function EvUiClickFriend:Reset()
end

---------------------------------------
EvEntityGoldChanged = EventBase:new(nil)

function EvEntityGoldChanged:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGoldChanged"
    self.change_reason = nil
    self.delta_gold = 0
    self.user_data = nil
    return o
end

function EvEntityGoldChanged:Reset()
    self.change_reason = nil
    self.delta_gold = 0
    self.user_data = nil
end

---------------------------------------
-- DesktopH
EvUiClickDesktopHundred = EventBase:new(nil)

function EvUiClickDesktopHundred:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvViewClickDesktopH"
    self.factory_name = ""
    return o
end

function EvUiClickDesktopHundred:Reset()
    self.factory_name = ""
end

---------------------------------------
EvEntityPlayerEnterDesktopH = EventBase:new(nil)

function EvEntityPlayerEnterDesktopH:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityPlayerEnterDesktopH"
    return o
end

function EvEntityPlayerEnterDesktopH:Reset()
end

---------------------------------------
EvDesktopHundredChangeCardsType = EventBase:new(nil)

function EvDesktopHundredChangeCardsType:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvDesktopHundredChangeCardsType"
    self.map_card_types = nil
    return o
end

function EvDesktopHundredChangeCardsType:Reset()
    self.map_card_types = nil
end

---------------------------------------
EvUiClickLeaveDesktopHundred = EventBase:new(nil)

function EvUiClickLeaveDesktopHundred:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvViewClickLeaveDesktopH"
    return o
end

function EvUiClickLeaveDesktopHundred:Reset()
end

---------------------------------------
EvUiRequestGetGrowSnapShot = EventBase:new(nil)

function EvUiRequestGetGrowSnapShot:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestGetGrowSnapShot"
    return o
end

function EvUiRequestGetGrowSnapShot:Reset()
end

---------------------------------------
EvEntityDesktopHGameEndState = EventBase:new(nil)

function EvEntityDesktopHGameEndState:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHGameEndState"
    return o
end

function EvEntityDesktopHGameEndState:Reset()
end

---------------------------------------
EvDesktopHGetBetReward = EventBase:new(nil)

function EvDesktopHGetBetReward:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvDesktopHGetBetReward"
    self.factory_name = nil
    return o
end

function EvDesktopHGetBetReward:Reset()
    self.factory_name = nil
end

---------------------------------------
EvEntityInitBetReward = EventBase:new(nil)

function EvEntityInitBetReward:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityInitBetReward"
    self.init_dailybet_reward = nil
    return o
end

function EvEntityInitBetReward:Reset()
    self.init_dailybet_reward = nil
end

---------------------------------------
EvEntityDesktopHChangeBeBankerPlayerList = EventBase:new(nil)

function EvEntityDesktopHChangeBeBankerPlayerList:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHChangeBeBankerPlayerList"
    self.list_bebanker = nil
    self.banker_player = nil
    self.is_bankplayer = false
    return o
end

function EvEntityDesktopHChangeBeBankerPlayerList:Reset()
    self.list_bebanker = nil
    self.banker_player = nil
    self.is_bankplayer = false
end

---------------------------------------
EvEntityDesktopHChangeBankerPlayer = EventBase:new(nil)

function EvEntityDesktopHChangeBankerPlayer:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHChangeBankerPlayer"
    self.list_bebankplayer = nil
    self.banker_player = nil
    self.is_bankplayer = false
    return o
end

function EvEntityDesktopHChangeBankerPlayer:Reset()
    self.list_bebankplayer = nil
    self.banker_player = nil
    self.is_bankplayer = false
end

---------------------------------------
EvEntityDesktopHBankerPlayerGoldChange = EventBase:new(nil)

function EvEntityDesktopHBankerPlayerGoldChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHBankerPlayerGoldChange"
    self.banker_player = nil
    return o
end

function EvEntityDesktopHBankerPlayerGoldChange:Reset()
    self.banker_player = nil
end

---------------------------------------
EvUiDesktopHSeatDown = EventBase:new(nil)

function EvUiDesktopHSeatDown:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvViewDesktopHSitdown"
    self.seat_index = 0
    self.min_golds = 0
    return o
end

function EvUiDesktopHSeatDown:Reset()
    self.seat_index = 0
    self.min_golds = 0
end

---------------------------------------
EvDesktopHClickRewardPotBtn = EventBase:new(nil)

function EvDesktopHClickRewardPotBtn:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvDesktopHClickRewardPotBtn"
    return o
end

function EvDesktopHClickRewardPotBtn:Reset()
end

---------------------------------------
EvDesktopHClickBetOperateType = EventBase:new(nil)

function EvDesktopHClickBetOperateType:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvViewDesktopHClickBetOperateType"
    self.tb_bet_operateid = nil
    return o
end

function EvDesktopHClickBetOperateType:Reset()
    self.tb_bet_operateid = nil
end

---------------------------------------
EvDesktopHGetBetReward = EventBase:new(nil)

function EvDesktopHGetBetReward:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvDesktopHGetBetReward"
    return o
end

function EvDesktopHGetBetReward:Reset()
end

---------------------------------------
EvDesktopHBet = EventBase:new(nil)

function EvDesktopHBet:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvDesktopHBet"
    self.bet_betpot_index = 0
    return o
end

function EvDesktopHBet:Reset()
    self.bet_betpot_index = 0
end

---------------------------------------
EvDesktopHRepeatBet = EventBase:new(nil)

function EvDesktopHRepeatBet:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvDesktopHRepeatBet"
    return o
end

function EvDesktopHRepeatBet:Reset()
end

---------------------------------------
EvDesktopHInitBetReward = EventBase:new(nil)

function EvDesktopHInitBetReward:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvDesktopHInitBetReward"
    self.factory_name = nil
    return o
end

function EvDesktopHInitBetReward:Reset()
    self.factory_name = nil
end

---------------------------------------
EvEntityDesktopHChangeSeatPlayer = EventBase:new(nil)

function EvEntityDesktopHChangeSeatPlayer:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHChangeSeatPlayer"
    self.map_seat_player = {}
    return o
end

function EvEntityDesktopHChangeSeatPlayer:Reset()
    self.map_seat_player = {}
end

---------------------------------------
EvEntityDesktopHBetOperateTypeChange = EventBase:new(nil)

function EvEntityDesktopHBetOperateTypeChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHBetOperateTypeChange"
    self.map_changeoperate = nil
    return o
end

function EvEntityDesktopHBetOperateTypeChange:Reset()
    self.map_changeoperate = nil
end

---------------------------------------
EvEntityDesktopHCurrentBetOperateTypeChange = EventBase:new(nil)

function EvEntityDesktopHCurrentBetOperateTypeChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHCurrentBetOperateTypeChange"
    return o
end

function EvEntityDesktopHCurrentBetOperateTypeChange:Reset()
end

---------------------------------------
EvEntityDesktopHBet = EventBase:new(nil)

function EvEntityDesktopHBet:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHBet"
    self.bet_potindex = 0
    self.bet_golds = 0
    self.already_betgolds = 0
    return o
end

function EvEntityDesktopHBet:Reset()
    self.bet_potindex = 0
    self.bet_golds = 0
    self.already_betgolds = 0
end

---------------------------------------
EvEntityDesktopHUpdateBetPotBetInfo = EventBase:new(nil)

function EvEntityDesktopHUpdateBetPotBetInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHUpdateBetPotBetInfo"
    self.map_betpot_betdeltainfo = nil
    self.map_standplayer_betdeltainfo = nil
    self.list_seatplayer_betinfo = nil
    return o
end

function EvEntityDesktopHUpdateBetPotBetInfo:Reset()
    self.map_betpot_betdeltainfo = nil
    self.map_standplayer_betdeltainfo = nil
    self.list_seatplayer_betinfo = nil
end

---------------------------------------
EvEntityDesktopHBetFailed = EventBase:new(nil)

function EvEntityDesktopHBetFailed:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHBetFailed"
    self.map_self_betgolds = nil
    return o
end

function EvEntityDesktopHBetFailed:Reset()
    self.map_self_betgolds = nil
end

---------------------------------------
EvEntityDesktopHChangeBankerPlayer = EventBase:new(nil)

function EvEntityDesktopHChangeBankerPlayer:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHChangeBankerPlayer"
    self.banker_player = nil
    return o
end

function EvEntityDesktopHChangeBankerPlayer:Reset()
    self.banker_player = nil
end

---------------------------------------
EvEntityDesktopHBankerPlayerGoldChange = EventBase:new(nil)

function EvEntityDesktopHBankerPlayerGoldChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHBankerPlayerGoldChange"
    self.map_seat_player = nil
    return o
end

function EvEntityDesktopHBankerPlayerGoldChange:Reset()
    self.map_seat_player = nil
end

---------------------------------------
EvEntityDesktopHSeatPlayerGoldChanged = EventBase:new(nil)

function EvEntityDesktopHSeatPlayerGoldChanged:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHSeatPlayerGoldChanged"
    self.map_seatplayer_golds = nil
    return o
end

function EvEntityDesktopHSeatPlayerGoldChanged:Reset()
    self.map_seatplayer_golds = nil
end

---------------------------------------
EvEntityRecvChatFromDesktopH = EventBase:new(nil)

function EvEntityRecvChatFromDesktopH:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityRecvChatFromDesktopH"
    self.chat_info = nil
    return o
end

function EvEntityRecvChatFromDesktopH:Reset()
    self.chat_info = nil
end

---------------------------------------
EvEntityDesktopHReadyState = EventBase:new(nil)

function EvEntityDesktopHReadyState:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHReadyState"
    self.left_tm = 0
    self.map_userdata = nil
    return o
end

function EvEntityDesktopHReadyState:Reset()
    self.left_tm = 0
    self.map_userdata = nil
end

---------------------------------------
EvEntityDesktopHBetState = EventBase:new(nil)

function EvEntityDesktopHBetState:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHBetState"
    self.left_tm = 0
    self.max_total_bet_gold = 0
    self.map_betrepeatinfo = nil
    return o
end

function EvEntityDesktopHBetState:Reset()
    self.left_tm = 0
    self.max_total_bet_gold = 0
    self.map_betrepeatinfo = nil
end

---------------------------------------
EvEntityDesktopHGameRestState = EventBase:new(nil)

function EvEntityDesktopHGameRestState:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHGameRestState"
    self.left_tm = 0
    return o
end

function EvEntityDesktopHGameRestState:Reset()
    self.left_tm = 0
end

---------------------------------------
EvEntityDesktopHBuyItem = EventBase:new(nil)

function EvEntityDesktopHBuyItem:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHBuyItem"
    self.sender_guid = nil
    self.map_items = nil
    return o
end

function EvEntityDesktopHBuyItem:Reset()
    self.sender_guid = nil
    self.map_items = nil
end

---------------------------------------
EvEntityDesktopHGetRewardPotInfo = EventBase:new(nil)

function EvEntityDesktopHGetRewardPotInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHGetRewardPotInfo"
    self.total_info = nil
    self.reward_totalgolds = 0
    return o
end

function EvEntityDesktopHGetRewardPotInfo:Reset()
    self.total_info = nil
    self.reward_totalgolds = 0
end

---------------------------------------
EvDesktopHClickBeBankPlayerBtn = EventBase:new(nil)

function EvDesktopHClickBeBankPlayerBtn:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvDesktopHClickBeBankPlayerBtn"
    self.bebank_mingolds = 0
    self.take_stack = 0
    return o
end

function EvDesktopHClickBeBankPlayerBtn:Reset()
    self.bebank_mingolds = 0
    self.take_stack = 0
end

---------------------------------------
-- Desktop
EvUiClickExitDesk = EventBase:new(nil)

function EvUiClickExitDesk:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickExitDesk"
    return o
end

function EvUiClickExitDesk:Reset()
end

---------------------------------------
EvUiClickInviteFriendPlay = EventBase:new(nil)

function EvUiClickInviteFriendPlay:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickInviteFriendPlay"
    return o
end

function EvUiClickInviteFriendPlay:Reset()
end

---------------------------------------
EvUiClickWaitWhile = EventBase:new(nil)

function EvUiClickWaitWhile:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickWaitWhile"
    return o
end

function EvUiClickWaitWhile:Reset()
end

---------------------------------------
EvUiClickOB = EventBase:new(nil)

function EvUiClickOB:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickOB"
    return o
end

function EvUiClickOB:Reset()
end

---------------------------------------
EvUiRequestLockAllSpectator = EventBase:new(nil)

function EvUiRequestLockAllSpectator:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestLockAllSpectator"
    self.requestLock = false
    return o
end

function EvUiRequestLockAllSpectator:Reset()
    self.requestLock = false
end

---------------------------------------
EvUiRequestLockAllDesktopPlayer = EventBase:new(nil)

function EvUiRequestLockAllDesktopPlayer:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestLockAllDesktopPlayer"
    self.requestLock = false
    return o
end

function EvUiRequestLockAllDesktopPlayer:Reset()
    self.requestLock = false
end

---------------------------------------
EvUiClickPlayerReturn = EventBase:new(nil)

function EvUiClickPlayerReturn:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickPlayerReturn"
    return o
end

function EvUiClickPlayerReturn:Reset()
end

---------------------------------------
EvUiClickRaise = EventBase:new(nil)

function EvUiClickRaise:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickRaise"
    self.raise_gold = 0
    return o
end

function EvUiClickRaise:Reset()
    self.raise_gold = 0
end

---------------------------------------
-- 请求改变收货地址
EvUiRequestEditReceiverAddress = EventBase:new(nil)

function EvUiRequestEditReceiverAddress:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestEditReceiverAddress"
    o.Address = nil
    return o
end

function EvUiRequestEditReceiverAddress:Reset()
    self.Address = nil
end

---------------------------------------
EvUiClickCancelAutoAction = EventBase:new(nil)

function EvUiClickCancelAutoAction:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickCancelAutoAction"
    return o
end

function EvUiClickCancelAutoAction:Reset()
end

---------------------------------------
EvUiClickCall = EventBase:new(nil)

function EvUiClickCall:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickCall"
    return o
end

function EvUiClickCall:Reset()
end

---------------------------------------
EvUiClickAutoAction = EventBase:new(nil)

function EvUiClickAutoAction:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickAutoAction"
    self.auto_action_type = nil
    return o
end

function EvUiClickAutoAction:Reset()
    self.auto_action_type = nil
end

---------------------------------------
EvUiClickCheck = EventBase:new(nil)

function EvUiClickCheck:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickCheck"
    return o
end

function EvUiClickCheck:Reset()
end

---------------------------------------
EvUiClickFlod = EventBase:new(nil)

function EvUiClickFlod:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickFlod"
    return o
end

function EvUiClickFlod:Reset()
end

---------------------------------------
EvEntityRecvChatFromDesktop = EventBase:new(nil)

function EvEntityRecvChatFromDesktop:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityRecvChatFromDesktop"
    self.chat_info = nil
    return o
end

function EvEntityRecvChatFromDesktop:Reset()
    self.chat_info = nil
end

---------------------------------------
EvCurrentWinner = EventBase:new(nil)

function EvCurrentWinner:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvCurrentWinner"
    self.player = nil
    return o
end

function EvCurrentWinner:Reset()
    self.player = nil
end

---------------------------------------
EvUiClickDesktop = EventBase:new(nil)

function EvUiClickDesktop:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickDesktop"
    return o
end

function EvUiClickDesktop:Reset()
end

---------------------------------------
EvCommonCardShowEnd = EventBase:new(nil)

function EvCommonCardShowEnd:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvCommonCardShowEnd"
    return o
end

function EvCommonCardShowEnd:Reset()
end

---------------------------------------
EvCommonCardDealEnd = EventBase:new(nil)

function EvCommonCardDealEnd:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvCommonCardDealEnd"
    return o
end

function EvCommonCardDealEnd:Reset()
end

---------------------------------------
EvUiClickShop = EventBase:new(nil)

function EvUiClickShop:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickShop"
    return o
end

function EvUiClickShop:Reset()
end

---------------------------------------
EvUiClickFriend = EventBase:new(nil)

function EvUiClickFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickFriend"
    return o
end

function EvUiClickFriend:Reset()
end

---------------------------------------
EvUiDesktopClickLockChat = EventBase:new(nil)

function EvUiDesktopClickLockChat:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiDesktopClickLockChat"
    return o
end

function EvUiDesktopClickLockChat:Reset()
end

---------------------------------------
EvUiClickChatmsg = EventBase:new(nil)

function EvUiClickChatmsg:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickChatmsg"
    return o
end

function EvUiClickChatmsg:Reset()
end

---------------------------------------
EvUiClickSeat = EventBase:new(nil)

function EvUiClickSeat:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickSeat"
    self.seat_index = 0
    return o
end

function EvUiClickSeat:Reset()
    self.seat_index = 0
end

---------------------------------------
EvUiClickInviteFriendPlay = EventBase:new(nil)

function EvUiClickInviteFriendPlay:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickInviteFriendPlay"
    return o
end

function EvUiClickInviteFriendPlay:Reset()
end

---------------------------------------
EvEntityReceiveFriendChats = EventBase:new(nil)

function EvEntityReceiveFriendChats:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityReceiveFriendChats"
    return o
end

function EvEntityReceiveFriendChats:Reset()
end

---------------------------------------
EvEntityUnreadChatsChanged = EventBase:new(nil)

function EvEntityUnreadChatsChanged:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityUnreadChatsChanged"
    return o
end

function EvEntityUnreadChatsChanged:Reset()
end

---------------------------------------
EvEntityMailListInit = EventBase:new(nil)

function EvEntityMailListInit:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityMailListInit"
    return o
end

function EvEntityMailListInit:Reset()
end

---------------------------------------
EvEntityMailAdd = EventBase:new(nil)

function EvEntityMailAdd:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityMailAdd"
    return o
end

function EvEntityMailAdd:Reset()
end

---------------------------------------
EvEntityMailDelete = EventBase:new(nil)

function EvEntityMailDelete:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityMailDelete"
    return o
end

function EvEntityMailDelete:Reset()
end

---------------------------------------
EvEntityMailUpdate = EventBase:new(nil)

function EvEntityMailUpdate:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityMailUpdate"
    return o
end

function EvEntityMailUpdate:Reset()
end

---------------------------------------
--Desktop
EvEntityPlayerEnterDesktop = EventBase:new(nil)

function EvEntityPlayerEnterDesktop:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityPlayerEnterDesktop"
    return o
end

function EvEntityPlayerEnterDesktop:Reset()
end

---------------------------------------
EvEntityPlayerEnterDesktopFailed = EventBase:new(nil)

function EvEntityPlayerEnterDesktopFailed:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityPlayerEnterDesktopFailed"
    return o
end

function EvEntityPlayerEnterDesktopFailed:Reset()
end

---------------------------------------
EvEntityRecvChatFromDesktop = EventBase:new(nil)

function EvEntityRecvChatFromDesktop:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityRecvChatFromDesktop"
    self.chat_info = nil
    return o
end

function EvEntityRecvChatFromDesktop:Reset()
    self.chat_info = nil
end

---------------------------------------
EvEntityDesktopSnapshotNotify = EventBase:new(nil)

function EvEntityDesktopSnapshotNotify:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopSnapshotNotify"
    self.desktop = nil
    self.desktop_data = nil
    self.is_init = false
    return o
end

function EvEntityDesktopSnapshotNotify:Reset()
    self.desktop = nil
    self.desktop_data = nil
    self.is_init = false
end

---------------------------------------
EvEntityDesktopIdleNotify = EventBase:new(nil)

function EvEntityDesktopIdleNotify:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopIdleNotify"
    return o
end

function EvEntityDesktopIdleNotify:Reset()
end

---------------------------------------
EvEntityDesktopPreFlopNotify = EventBase:new(nil)

function EvEntityDesktopPreFlopNotify:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopPreFlopNotify"
    return o
end

function EvEntityDesktopPreFlopNotify:Reset()
end

---------------------------------------
EvEntityDesktopFlopNotify = EventBase:new(nil)

function EvEntityDesktopFlopNotify:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopFlopNotify"
    self.first_card = nil
    self.second_card = nil
    self.third_card = nil
    self.bet_player_count = 0
    return o
end

function EvEntityDesktopFlopNotify:Reset()
    self.first_card = nil
    self.second_card = nil
    self.third_card = nil
    self.bet_player_count = 0
end

---------------------------------------
EvEntityDesktopTurnNotify = EventBase:new(nil)

function EvEntityDesktopTurnNotify:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopTurnNotify"
    self.turn_card = nil
    self.bet_player_count = 0
    return o
end

function EvEntityDesktopTurnNotify:Reset()
    self.turn_card = nil
    self.bet_player_count = 0
end

---------------------------------------
EvEntityDesktopRiverNotify = EventBase:new(nil)

function EvEntityDesktopRiverNotify:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopRiverNotify"
    self.river_card = nil
    self.bet_player_count = 0
    return o
end

function EvEntityDesktopRiverNotify:Reset()
    self.river_card = nil
    self.bet_player_count = 0
end

---------------------------------------
EvEntityDesktopShowdownNotify = EventBase:new(nil)

function EvEntityDesktopShowdownNotify:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopShowdownNotify"
    self.desktop_showdown = nil
    return o
end

function EvEntityDesktopShowdownNotify:Reset()
    self.desktop_showdown = nil
end

---------------------------------------
EvEntityDesktopGameEndNotifyTexas = EventBase:new(nil)

function EvEntityDesktopGameEndNotifyTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopGameEndNotifyTexas"
    self.list_winner = nil
    return o
end

function EvEntityDesktopGameEndNotifyTexas:Reset()
    self.list_winner = nil
end

---------------------------------------
EvEntityDesktopPlayerSit = EventBase:new(nil)

function EvEntityDesktopPlayerSit:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopPlayerSit"
    self.guid = ""
    self.account_id = ""
    self.nick_name = ""
    self.icon_name = ""
    self.vip_level = 0
    return o
end

function EvEntityDesktopPlayerSit:Reset()
    self.guid = ""
    self.account_id = ""
    self.nick_name = ""
    self.icon_name = ""
    self.vip_level = 0
end

---------------------------------------
EvEntityDesktopPlayerLeaveChair = EventBase:new(nil)

function EvEntityDesktopPlayerLeaveChair:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopPlayerLeaveChair"
    self.guid = ""
    return o
end

function EvEntityDesktopPlayerLeaveChair:Reset()
    self.guid = ""
end

---------------------------------------
EvEntityBagUpdateItem = EventBase:new(nil)

function EvEntityBagUpdateItem:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityBagUpdateItem"
    self.item = nil
    return o
end

function EvEntityBagUpdateItem:Reset()
    self.item = nil
end

---------------------------------------
EvOpenBag = EventBase:new(nil)

function EvOpenBag:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvOpenBag"
    return o
end

function EvOpenBag:Reset()
end

---------------------------------------
EvEntityRequestGetLotteryTicketData = EventBase:new(nil)

function EvEntityRequestGetLotteryTicketData:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityRequestGetLotteryTicketData"
    return o
end

function EvEntityRequestGetLotteryTicketData:Reset()
end

---------------------------------------
EvEntityLotteryTicketBetOperateTypeChange = EventBase:new(nil)

function EvEntityLotteryTicketBetOperateTypeChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityLotteryTicketBetOperateTypeChange"
    self.map_changeoperate = nil
    return o
end

function EvEntityLotteryTicketBetOperateTypeChange:Reset()
    self.map_changeoperate = nil
end

---------------------------------------
EvEntityLotteryTicketUpdateBetPotBetInfo = EventBase:new(nil)

function EvEntityLotteryTicketUpdateBetPotBetInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityLotteryTicketUpdateBetPotBetInfo"
    self.map_allbetpot = {}
    self.map_self_betinfo = {}
    return o
end

function EvEntityLotteryTicketUpdateBetPotBetInfo:Reset()
    self.map_allbetpot = {}
    self.map_self_betinfo = {}
end

---------------------------------------
EvEntityGetLotteryTicketDataSuccess = EventBase:new(nil)

function EvEntityGetLotteryTicketDataSuccess:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGetLotteryTicketDataSuccess"
    self.lotteryticket_data = nil
    return o
end

function EvEntityGetLotteryTicketDataSuccess:Reset()
    self.lotteryticket_data = nil
end

---------------------------------------
EvEntityLotteryTicketGameEndState = EventBase:new(nil)

function EvEntityLotteryTicketGameEndState:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityLotteryTicketGameEndState"
    self.gameend_detail = nil
    self.me_wingold = 0
    return o
end

function EvEntityLotteryTicketGameEndState:Reset()
    self.gameend_detail = nil
    self.me_wingold = 0
end

---------------------------------------
EvEntityLotteryTicketGameEndStateSimple = EventBase:new(nil)

function EvEntityLotteryTicketGameEndStateSimple:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityLotteryTicketGameEndStateSimple"
    return o
end

function EvEntityLotteryTicketGameEndStateSimple:Reset()
end

---------------------------------------
EvEntityLotteryTicketBetState = EventBase:new(nil)

function EvEntityLotteryTicketBetState:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityLotteryTicketBetState"
    self.map_betrepeatinfo = {}
    return o
end

function EvEntityLotteryTicketBetState:Reset()
    self.map_betrepeatinfo = {}
end

---------------------------------------
EvLotteryTicketClickRewardPotBtn = EventBase:new(nil)

function EvLotteryTicketClickRewardPotBtn:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvLotteryTicketClickRewardPotBtn"
    return o
end

function EvLotteryTicketClickRewardPotBtn:Reset()
end

---------------------------------------
EvLotteryTicketBet = EventBase:new(nil)

function EvLotteryTicketBet:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvLotteryTicketBet"
    self.bet_betpot_index = 0
    return o
end

function EvLotteryTicketBet:Reset()
    self.bet_betpot_index = 0
end

---------------------------------------
EvEntityLotteryTicketGetRewardPotInfo = EventBase:new(nil)

function EvEntityLotteryTicketGetRewardPotInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityLotteryTicketGetRewardPotInfo"
    self.list_playerinfo = nil
    self.reward_totalgolds = 0
    return o
end

function EvEntityLotteryTicketGetRewardPotInfo:Reset()
    self.list_playerinfo = nil
    self.reward_totalgolds = 0
end

---------------------------------------
EvUiClickLeaveLotteryTicket = EventBase:new(nil)

function EvUiClickLeaveLotteryTicket:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickLeaveLotteryTicket"
    return o
end

function EvUiClickLeaveLotteryTicket:Reset()
end

---------------------------------------
EvEntityLotteryTicketUpdateTm = EventBase:new(nil)

function EvEntityLotteryTicketUpdateTm:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityLotteryTicketUpdateTm"
    self.tm = 0
    return o
end

function EvEntityLotteryTicketUpdateTm:Reset()
    self.tm = 0
end

---------------------------------------
EvLotteryTicketClickBetOperateType = EventBase:new(nil)

function EvLotteryTicketClickBetOperateType:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvLotteryTicketClickBetOperateType"
    self.tb_bet_operateid = 0
    return o
end

function EvLotteryTicketClickBetOperateType:Reset()
    self.tb_bet_operateid = 0
end

---------------------------------------
EvEntityLotteryTicketCurrentBetOperateTypeChange = EventBase:new(nil)

function EvEntityLotteryTicketCurrentBetOperateTypeChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityLotteryTicketCurrentBetOperateTypeChange"
    return o
end

function EvEntityLotteryTicketCurrentBetOperateTypeChange:Reset()
end

---------------------------------------
EvEntityLotteryTicketBet = EventBase:new(nil)

function EvEntityLotteryTicketBet:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityLotteryTicketBet"
    self.already_bet_chips = 0
    self.bet_potindex = 0
    return o
end

function EvEntityLotteryTicketBet:Reset()
    self.already_bet_chips = 0
    self.bet_potindex = 0
end

---------------------------------------
EvLotteryTicketRepeatBet = EventBase:new(nil)

function EvLotteryTicketRepeatBet:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvLotteryTicketRepeatBet"
    return o
end

function EvLotteryTicketRepeatBet:Reset()
end

---------------------------------------
EvRequestSendMarquee = EventBase:new(nil)

function EvRequestSendMarquee:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvRequestSendMarquee"
    self.msg = nil
    return o
end

function EvRequestSendMarquee:Reset()
    self.msg = nil
end

---------------------------------------
EvEntityGetDesktopData = EventBase:new(nil)

function EvEntityGetDesktopData:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGetDesktopData"
    return o
end

function EvEntityGetDesktopData:Reset()
end

---------------------------------------
EvEntityRequestGetDesktopHData = EventBase:new(nil)

function EvEntityRequestGetDesktopHData:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityRequestGetDesktopHData"
    return o
end

function EvEntityRequestGetDesktopHData:Reset()
end

---------------------------------------
EvEntitySetPublicMatchLsit = EventBase:new(nil)

function EvEntitySetPublicMatchLsit:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntitySetPublicMatchLsit"
    o.MatchType = nil
    o.ListMatch = nil
    o.SelfMatchNum = 0
    return o
end

function EvEntitySetPublicMatchLsit:Reset()
    self.MatchType = nil
    self.ListMatch = nil
    self.SelfMatchNum = 0
end

---------------------------------------
EvEntitySetPrivateMatchLsit = EventBase:new(nil)

function EvEntitySetPrivateMatchLsit:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntitySetPrivateMatchLsit"
    self.ListMatch = nil
    self.ListApplyMatchGuid = nil
    return o
end

function EvEntitySetPrivateMatchLsit:Reset()
    self.ListMatch = nil
    self.ListApplyMatchGuid = nil
end

---------------------------------------
EvEntityUpdatePublicMatchPlayerNum = EventBase:new(nil)

function EvEntityUpdatePublicMatchPlayerNum:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityUpdatePublicMatchPlayerNum"
    o.ListMatchNum = nil
    return o
end

function EvEntityUpdatePublicMatchPlayerNum:Reset()
    self.ListMatchNum = nil
end

---------------------------------------
EvEntityUpdatePrivateMatchPlayerNum = EventBase:new(nil)

function EvEntityUpdatePrivateMatchPlayerNum:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityUpdatePrivateMatchPlayerNum"
    o.ListMatchNum = nil
    return o
end

function EvEntityUpdatePrivateMatchPlayerNum:Reset()
    self.ListMatchNum = nil
end

---------------------------------------
EvEntityMTTUpdateRealtimeInfo = EventBase:new(nil)

function EvEntityMTTUpdateRealtimeInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityMTTUpdateRealtimeInfo"
    o.RealtimeInfo = nil
    o.blind_type = 0
    return o
end

function EvEntityMTTUpdateRealtimeInfo:Reset()
    self.RealtimeInfo = nil
    self.blind_type = 0
end

---------------------------------------
EvEntityMTTUpdateRaiseBlindTm = EventBase:new(nil)

function EvEntityMTTUpdateRaiseBlindTm:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityMTTUpdateRaiseBlindTm"
    o.RaiseBlindTm = 0
    return o
end

function EvEntityMTTUpdateRaiseBlindTm:Reset()
    self.RaiseBlindTm = 0
end

---------------------------------------
EvEntityMTTPlayerRebuyOrAddonRefresh = EventBase:new(nil)

function EvEntityMTTPlayerRebuyOrAddonRefresh:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityMTTPlayerRebuyOrAddonRefresh"
    o.can_rebuy = false
    o.can_addon = false
    return o
end

function EvEntityMTTPlayerRebuyOrAddonRefresh:Reset()
    self.can_rebuy = false
    self.can_addon = false
end

---------------------------------------
EvUiMTTCreateRebuyOrAddOn = EventBase:new(nil)

function EvUiMTTCreateRebuyOrAddOn:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiMTTCreateRebuyOrAddOn"
    return o
end

function EvUiMTTCreateRebuyOrAddOn:Reset()
end

---------------------------------------
EvMTTPauseChanged = EventBase:new(nil)

function EvMTTPauseChanged:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvMTTPauseChanged"
    self.pause_info = nil
    return o
end

function EvMTTPauseChanged:Reset()
    self.pause_info = nil
end

---------------------------------------
EvEntitySignUpSucceed = EventBase:new(nil)

function EvEntitySignUpSucceed:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntitySignUpSucceed"
    o.MatchGuid = nil
    return o
end

function EvEntitySignUpSucceed:Reset()
    self.MatchGuid = nil
end

---------------------------------------
-- 响应取消报名比赛
EvEntityResponseCancelSignUpMatch = {}
function EvEntityResponseCancelSignUpMatch:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityResponseCancelSignUpMatch"
    o.MatchGuid = nil
    return o
end

function EvEntityResponseCancelSignUpMatch:Reset()
    self.MatchGuid = nil
end

---------------------------------------
EvEntitySetMatchDetailedInfo = EventBase:new(nil)

function EvEntitySetMatchDetailedInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntitySetMatchDetailedInfo"
    o.MatchDetailedInfo = nil
    return o
end

function EvEntitySetMatchDetailedInfo:Reset()
    self.MatchDetailedInfo = nil
end

---------------------------------------
EvEntitySetRaiseBlindTbInfo = EventBase:new(nil)

function EvEntitySetRaiseBlindTbInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntitySetRaiseBlindTbInfo"
    o.raise_blind_info = nil
    o.current_raiseblind_tbid = 0
    return o
end

function EvEntitySetRaiseBlindTbInfo:Reset()
    self.raise_blind_info = nil
    self.current_raiseblind_tbid = 0
end

---------------------------------------
EvEntityMatchGameOver = EventBase:new(nil)

function EvEntityMatchGameOver:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityMatchGameOver"
    o.game_over = nil
    return o
end

function EvEntityMatchGameOver:Reset()
    self.game_over = nil
end

---------------------------------------
EvEntitySelfIsOB = EventBase:new(nil)

function EvEntitySelfIsOB:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntitySelfIsOB"
    return o
end

function EvEntitySelfIsOB:Reset()
end

---------------------------------------
EvEntitySelfAutoActionChange = EventBase:new(nil)

function EvEntitySelfAutoActionChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntitySelfAutoActionChange"
    o.is_autoaction = false
    return o
end

function EvEntitySelfAutoActionChange:Reset()
    o.is_autoaction = false
end

---------------------------------------
EvUiClickFastBet = EventBase:new(nil)

function EvUiClickFastBet:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickFastBet"
    self.bet_value = 0
    return o
end

function EvUiClickFastBet:Reset()
    self.bet_value = 0
end

---------------------------------------
EvUiClickShowCard = EventBase:new(nil)

function EvUiClickShowCard:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickShowCard"
    self.click_card1 = false
    return o
end

function EvUiClickShowCard:Reset()
    self.click_card1 = false
end

---------------------------------------
EvUiChooseUCenter = EventBase:new(nil)

function EvUiChooseUCenter:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiChooseUCenter"
    self.ucenter = ""
    return o
end

function EvUiChooseUCenter:Reset()
    self.ucenter = ""
end

---------------------------------------
EvUiChooseGateWay = EventBase:new(nil)

function EvUiChooseGateWay:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiChooseGateWay"
    self.gateway = ""
    return o
end

function EvUiChooseGateWay:Reset()
    self.gateway = ""
end

---------------------------------------
EvUiPotMainChanged = EventBase:new(nil)

function EvUiPotMainChanged:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiPotMainChanged"
    self.pot_mian = 0
    return o
end

function EvUiPotMainChanged:Reset()
    self.pot_mian = 0
end

---------------------------------------
-- 响应获取收货地址
EvEntityResponseGetReceiverAddress = EventBase:new(nil)

function EvEntityResponseGetReceiverAddress:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityResponseGetReceiverAddress"
    o.Address = nil
    return o
end

function EvEntityResponseGetReceiverAddress:Reset()
    self.Address = nil
end

---------------------------------------
-- 请求获取收货地址
EvUiRequestGetReceiverAddress = EventBase:new(nil)

function EvUiRequestGetReceiverAddress:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestGetReceiverAddress"
    return o
end

function EvUiRequestGetReceiverAddress:Reset()
end

---------------------------------------
-- 请求使用道具
EvUiRequestOperateItem = EventBase:new(nil)

function EvUiRequestOperateItem:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestOperateItem"
    o.ItemObjId = nil
    return o
end

function EvUiRequestOperateItem:Reset()
    o.ItemObjId = nil
end

---------------------------------------
-- 控制台命令
EvConsoleCmd = EventBase:new(nil)

function EvConsoleCmd:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvConsoleCmd"
    o.ListParam = nil
    return o
end

function EvConsoleCmd:Reset()
    self.ListParam = nil
end

---------------------------------------
-- 更新玩家信息
EvEntityUpdatePlayerPoint = EventBase:new(nil)

function EvEntityUpdatePlayerPoint:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityUpdatePlayerPoint"
    o.Point = 0
    return o
end

function EvEntityUpdatePlayerPoint:Reset()
    self.Point = 0
end

---------------------------------------
EvClickShare = EventBase:new(nil)

function EvClickShare:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvClickShare"
    self.ShareType = 0
    return o
end

function EvClickShare:Reset()
    self.ShareType = 0
end

---------------------------------------
EvPayWithIAPSuccess = EventBase:new(nil)

function EvPayWithIAPSuccess:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvPayWithIAPSuccess"
    o.purchase = nil
    return o
end

function EvPayWithIAPSuccess:Reset()
    self.purchase = nil
end

---------------------------------------
EvGetPicSuccess = EventBase:new(nil)

function EvGetPicSuccess:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvGetPicSuccess"
    o.pic_data = nil
    return o
end

function EvGetPicSuccess:Reset()
    self.pic_data = nil
end

---------------------------------------
EvCheckIdCard = EventBase:new(nil)

function EvCheckIdCard:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvCheckIdCard"
    o.name = nil
    o.id_card = nil
    return o
end

function EvCheckIdCard:Reset()
    self.name = nil
    self.id_card = nil
end

---------------------------------------
EvEntityIsFirstRechargeChanged = EventBase:new(nil)

function EvEntityIsFirstRechargeChanged:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityIsFirstRechargeChanged"
    return o
end

function EvEntityIsFirstRechargeChanged:Reset()
end

---------------------------------------
EvClickIconWithNickName = EventBase:new(nil)

function EvClickIconWithNickName:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvClickIconWithNickName"
    o.player = nil
    return o
end

function EvClickIconWithNickName:Reset()
    self.player = nil
end

---------------------------------------
EvRemoveMatch = EventBase:new(nil)

function EvRemoveMatch:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvRemoveMatch"
    o.MatchGuid = nil
    return o
end

function EvRemoveMatch:Reset()
    self.MatchGuid = nil
end

---------------------------------------
EvUpdatePlayerScore = EventBase:new(nil)

function EvUpdatePlayerScore:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUpdatePlayerScore"
    o.PlayerGuid = nil
    o.Score = 0
    return o
end

function EvUpdatePlayerScore:Reset()
    self.PlayerGuid = nil
    self.Score = 0
end

---------------------------------------
EvBindWeChat = EventBase:new(nil)

function EvBindWeChat:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvBindWechat"
    o.ItemObjId = nil
    return o
end

function EvBindWeChat:Reset()
    self.ItemObjId = nil
end

---------------------------------------
EvBindWeChatSuccess = EventBase:new(nil)

function EvBindWeChatSuccess:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvBindWeChatSuccess"
    o.IsSuccess = false
    o.WeChatOpenId = nil
    o.WeChatName = nil
    return o
end

function EvBindWeChatSuccess:Reset()
    self.IsSuccess = false
    self.WeChatOpenId = nil
    self.WeChatName = nil
end

---------------------------------------
EvUnbindWeChat = EventBase:new(nil)

function EvUnbindWeChat:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUnbindWechat"
    return o
end

function EvUnbindWeChat:Reset()
end

---------------------------------------
EvUnBindWeChatSuccess = EventBase:new(nil)

function EvUnBindWeChatSuccess:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUnBindWeChatSuccess"
    o.IsSuccess = false
    return o
end

function EvUnBindWeChatSuccess:Reset()
    self.IsSuccess = false
end