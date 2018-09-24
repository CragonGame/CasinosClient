-- Copyright (c) Cragon. All rights reserved.

---------------------------------------
EvUiLoginDeleteGuest = EventBase:new(nil) -- ui��Ϣ�������¼����ɾ���ο��˺�

function EvUiLoginDeleteGuest:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiLoginDeleteGuest"
    return o
end

function EvUiLoginDeleteGuest:reset()
end

---------------------------------------
EvUiLoginRequestGetPwd = EventBase:new(nil)-- Ui��Ϣ�������һ�����

function EvUiLoginRequestGetPwd:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiLoginRequestGetPwd"
    self.super_pwd = nil
    return o
end

function EvUiLoginRequestGetPwd:reset()
    self.super_pwd = nil
end

---------------------------------------
EvUiReportFriend = EventBase:new(nil)-- Ui��Ϣ�������һ�����

function EvUiReportFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiReportFriend"
    self.friend_etguid = nil
    self.report_type = nil
    return o
end

function EvUiReportFriend:reset()
    self.friend_etguid = nil
    self.report_type = nil
end

---------------------------------------
EvUiCloseActivityPopUpBox = EventBase:new(nil)-- Ui��Ϣ�������һ�����

function EvUiCloseActivityPopUpBox:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiCloseActivityPopUpBox"
    return o
end

function EvUiCloseActivityPopUpBox:reset()
end

---------------------------------------
EvUiRequestFriendAddOrRemove = EventBase:new(nil)-- Ui��Ϣ�������һ�����

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

function EvUiRequestFriendAddOrRemove:reset()
    self.is_add = nil
    self.friend_guid = nil
    self.friend_nickname = nil
end

---------------------------------------
EvUiRequestMailRead = EventBase:new(nil)-- Ui��Ϣ�������һ�����

function EvUiRequestMailRead:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestMailRead"
    self.mail_guid = nil
    return o
end

function EvUiRequestMailRead:reset()
    self.mail_guid = nil
end

EvUiSellItem = EventBase:new(nil)-- Ui��Ϣ�������һ�����

function EvUiSellItem:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiSellItem"
    self.item_objid = nil
    return o
end

function EvUiSellItem:reset()
    self.item_objid = nil
end

EvUiRemoveItem = EventBase:new(nil)-- Ui��Ϣ�������һ�����

function EvUiRemoveItem:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRemoveItem"
    self.obj_id = nil
    return o
end

function EvUiRemoveItem:reset()
    self.obj_id = nil
end

EvUiRequestResetPwd = EventBase:new(nil)-- Ui��Ϣ��������������

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

function EvUiRequestResetPwd:reset()
    self.phone = nil
    self.formatphone = nil
    self.phone_code = nil
    self.new_pwd = nil
end

EvUiLogin = EventBase:new(nil)-- Ui��Ϣ�������½�����½��ť
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

function EvUiLogin:reset()
    self.login_type = 0
    self.acc = nil
    self.pwd = nil
    self.remeber_pwd = false
    self.phone = nil
end

EvUiLoginSuccessEx = EventBase:new(nil)
function EvUiLoginSuccessEx:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiLoginSuccessEx"
    self.token = nil

    return o
end

function EvUiLoginSuccessEx:reset()
    self.token = nil
end

EvUiLoginClickBtnRegister = EventBase:new(nil)-- Ui��Ϣ�������½����ע�ᰴť

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

function EvUiLoginClickBtnRegister:reset()
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

EvUiRequestGetPhoneCode = EventBase:new(nil)-- Ui��Ϣ�������½����ע�ᰴť

function EvUiRequestGetPhoneCode:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestGetPhoneCode"
    self.Phone = nil
    self.Reson = 0

    return o
end

function EvUiRequestGetPhoneCode:reset()
    self.Phone = nil
    self.Reson = 0
end

EvUiChooseCountry = EventBase:new(nil)-- Ui��Ϣ�������½����ע�ᰴť

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

function EvUiChooseCountry:reset()
    self.CountryKey = nil
    self.CountryCode = nil
    self.KeyAndCodeFormat = nil
end

EvUiLoginClickBtnVisiter = EventBase:new(nil)-- Ui��Ϣ�������½�����οͰ�ť

function EvUiLoginClickBtnVisiter:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiLoginClickBtnVisiter"
    return o
end

function EvUiLoginClickBtnVisiter:reset()
end

EvUiLoginClickBtnFacebook = EventBase:new(nil)-- Ui��Ϣ�������½����Facebook��ť

function EvUiLoginClickBtnFacebook:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiLoginClickBtnFacebook"
    return o
end

function EvUiLoginClickBtnFacebook:reset()
end


-- Ui��Ϣ�������½����Facebook��ť
EvUiSendSecurityCode = EventBase:new(nil)

function EvUiSendSecurityCode:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiSendSecurityCode"
    self.PhoneNum = nil
    return o
end

function EvUiSendSecurityCode:reset()
    self.PhoneNum = nil
end


-- Ui��Ϣ�������½����Facebook��ť
EvRegisterDestroy = EventBase:new(nil)

function EvRegisterDestroy:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvRegisterDestroy"
    return o
end

function EvRegisterDestroy:reset()
end


-- Ui��Ϣ��������סϵͳ��Ϣ
EvUiRequestLockSystemChat = EventBase:new(nil)

function EvUiRequestLockSystemChat:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestLockSystemChat"
    self.requestLock = false
    return o
end

function EvUiRequestLockSystemChat:reset()
    self.requestLock = false
end


-- Ui��Ϣ��������ס�����Ϣ
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

function EvUiRequestLockPlayerChat:reset()
    self.player_guid = nil
    self.requestLock = false
end

--Ui��Ϣ,�����ʼ�����
EvUiRequestMailRecvAttachment = EventBase:new(nil)

function EvUiRequestMailRecvAttachment:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestMailRecvAttachment"
    self.mail_guid = nil
    return o
end

function EvUiRequestMailRecvAttachment:reset()
    self.mail_guid = nil
end


--Ui��Ϣ,��������Ʒ
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

function EvUiRequestBuyItem:reset()
    self.item_tbid = nil
    self.is_firstrecharge = nil
    self.item_count = nil
    self.pay_type = nil
end

EvUiRequestBuyDiamond = EventBase:new(nil)

function EvUiRequestBuyDiamond:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestBuyDiamond"
    self.buy_diamondid = nil
    return o
end

function EvUiRequestBuyDiamond:reset()
    self.buy_diamondid = nil
end

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

function EvUiBuyItem:reset()
    self.item_id = nil
    self.to_etguid = nil
end

EvUiDesktopHStandUp = EventBase:new(nil)

function EvUiDesktopHStandUp:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiDesktopHStandUp"
    self.item_id = nil
    self.to_etguid = nil
    return o
end

function EvUiDesktopHStandUp:reset()
    self.item_id = nil
    self.to_etguid = nil
end

EvUiRequestBuyGold = EventBase:new(nil)

function EvUiRequestBuyGold:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestBuyGold"
    self.buy_goldid = nil
    return o
end

function EvUiRequestBuyGold:reset()
    self.buy_goldid = nil
end

EvUiRequestWebpay = EventBase:new(nil)

function EvUiRequestWebpay:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestWebpay"
    self.Amount = 0
    return o
end

function EvUiRequestWebpay:reset()
    self.Amount = 0
end

EvUiRequestQuicktellerTransfers = EventBase:new(nil)

function EvUiRequestQuicktellerTransfers:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestQuicktellerTransfers"
    return o
end

function EvUiRequestQuicktellerTransfers:reset()
end

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

function EvUiRequestGetMoney:reset()
    self.GetMoneyNum = nil
    self.toAccountNumber = nil
    self.cbnCode = nil
    self.receiverLastName = nil
    self.receiverOtherName = nil
end


--Ui��Ϣ,������֤��
EvUiSendSecurityCode = EventBase:new(nil)

function EvUiSendSecurityCode:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiSendSecurityCode"
    self.phone_num = nil
    return o
end

function EvUiSendSecurityCode:reset()
    self.phone_num = nil
end



--Ui��Ϣ,ͬ����Ӻ���
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

function EvUiAgreeAddFriend:reset()
    self.from_etguid = nil
    self.ev = nil
end



--Ui��Ϣ,�ܾ���Ӻ���
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

function EvUiRefuseAddFriend:reset()
    self.from_etguid = nil
    self.ev = nil
end



--Ui��Ϣ�������Ǯ
EvUiRequestBankDeposit = EventBase:new(nil)

function EvUiRequestBankDeposit:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestBankDeposit"
    self.deposit_chip = nil
    return o
end

function EvUiRequestBankDeposit:reset()
    self.deposit_chip = nil
end



--Ui��Ϣ������ȡǮ
EvUiRequestBankWithdraw = EventBase:new(nil)

function EvUiRequestBankWithdraw:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestBankWithdraw"
    self.withdraw_chip = nil
    return o
end

function EvUiRequestBankWithdraw:reset()
    self.withdraw_chip = nil
end



--Ui��Ϣ��ȷ�Ϸ��ͳ���
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

function EvUiClickConfirmChipTransaction:reset()
    self.send_target_etguid = nil
    self.chip = nil
end


--Ui��Ϣ��������Ϣ
EvUiSetUnSendDesktopMsg = EventBase:new(nil)

function EvUiSetUnSendDesktopMsg:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiSetUnSendDesktopMsg"
    self.text = nil
    return o
end

function EvUiSetUnSendDesktopMsg:reset()
    self.text = nil
end


--Ui��Ϣ��������Ϣ
EvUiSendMsg = EventBase:new(nil)

function EvUiSendMsg:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiSendMsg"
    self.chat_msg = nil
    return o
end

function EvUiSendMsg:reset()
    self.chat_msg = nil
end



--Ui��Ϣ��ȷ�϶�ȡ���������¼
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

function EvUiChatConfirmRead:reset()
    self.friend_etguid = nil
    self.msg_id = nil
end

EvUiSendFeedbackMsg = EventBase:new(nil)
function EvUiSendFeedbackMsg:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiSendFeedbackMsg"
    self.chat_msg = nil
    return o
end

function EvUiSendFeedbackMsg:reset()
    self.chat_msg = nil
end

EvUiFeedbackConfirmRead = EventBase:new(nil)
function EvUiFeedbackConfirmRead:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiFeedbackConfirmRead"
    return o
end

function EvUiFeedbackConfirmRead:reset()
end


--Ui��Ϣ���������������View��
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

function EvUiClickViewInDesk:reset()
    self. desktop_filter = nil
    self.seat_index = nil
end



--Ui��Ϣ�����ѡ������������
EvUiClickChooseFriendChatTarget = EventBase:new(nil)

function EvUiClickChooseFriendChatTarget:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickChooseFriendChatTarget"
    return o
end

function EvUiClickChooseFriendChatTarget:reset()
end

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

function EvUiClickChooseFriend:reset()
    self.friend_info = nil
    self.is_choosechat = false
    self.is_recommand = false
end



--Ui��Ϣ��
EvUiCurrentChatTargetChange = EventBase:new(nil)

function EvUiCurrentChatTargetChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiCurrentChatTargetChange"
    self.current_chattarget = nil
    return o
end

function EvUiCurrentChatTargetChange:reset()
    self.current_chattarget = nil
end



--Ui��Ϣ��ȷ�Ϸ��ͳ���
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

function EvUiClickConfirmChipTransaction:reset()
    self.send_target_etguid = nil
    self.chip = nil
end



--Ui��Ϣ������������
EvUiCreateMainUi = EventBase:new(nil)

function EvUiCreateMainUi:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiCreateMainUi"
    return o
end

EvUiRequestUpdatePublicMatchPlayerNum = EventBase:new(nil)

function EvUiRequestUpdatePublicMatchPlayerNum:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestUpdatePublicMatchPlayerNum"
    return o
end

EvUiRequestUpdatePrivateMatchPlayerNum = EventBase:new(nil)

function EvUiRequestUpdatePrivateMatchPlayerNum:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestUpdatePrivateMatchPlayerNum"
    return o
end

EvUiRequestCreatePrivateMatch = EventBase:new(nil)
function EvUiRequestCreatePrivateMatch:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestCreatePrivateMatch"
    o.CreateMatchInfo = nil
    return o
end

function EvUiRequestCreatePrivateMatch:reset()
    self.CreateMatchInfo = nil
end
--Ui��Ϣ�������������
EvUiClickCreateDeskTop = EventBase:new(nil)

function EvUiClickCreateDeskTop:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickCreateDeskTop"
    self.create_info = nil
    return o
end

function EvUiClickCreateDeskTop:reset()
    self.create_info = nil
end



--Ui��Ϣ�������һ��������
EvUiCreateExchangeChip = EventBase:new(nil)

function EvUiCreateExchangeChip:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiCreateExchangeChip"
    return o
end

function EvUiCreateExchangeChip:reset()
    self.create_info = nil
end




--Ui��Ϣ�����Һ���
EvUiFindFriend = EventBase:new(nil)

function EvUiFindFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiFindFriend"
    self.search_filter = nil
    return o
end

function EvUiFindFriend:reset()
    self.search_filter = nil
end

EvUiAddFriend = EventBase:new(nil)

function EvUiAddFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiAddFriend"
    self.friend_etguid = nil
    return o
end

function EvUiAddFriend:reset()
    self.friend_etguid = nil
end

EvUiDeleteFriend = EventBase:new(nil)

function EvUiDeleteFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiDeleteFriend"
    self.friend_etguid = nil
    return o
end

function EvUiDeleteFriend:reset()
    self.friend_etguid = nil
end



--Ui��Ϣ�����Һ���
EvUiClickSearchDesk = EventBase:new(nil)

function EvUiClickSearchDesk:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickSearchDesk"
    self.desktop_searchfilter = nil
    return o
end

function EvUiClickSearchDesk:reset()
    self.desktop_searchfilter = nil
end



--Ui��Ϣ�������ѯ�ú�����������
EvUiRequestGetCurrentFriendPlayDesk = EventBase:new(nil)

function EvUiRequestGetCurrentFriendPlayDesk:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestGetCurrentFriendPlayDesk"
    self.player_guid = nil
    return o
end

function EvUiRequestGetCurrentFriendPlayDesk:reset()
    self.player_guid = nil
end



--Ui��Ϣ���������������
EvUiClickSearchFriendsDesk = EventBase:new(nil)

function EvUiClickSearchFriendsDesk:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickSearchFriendsDesk"
    self.friend_state = nil
    return o
end

function EvUiClickSearchFriendsDesk:reset()
    self.friend_state = nil
end



--Ui��Ϣ�������������ǳ�
EvUiClickChangePlayerNickName = EventBase:new(nil)

function EvUiClickChangePlayerNickName:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickChangePlayerNickName"
    self.new_name = nil
    return o
end

function EvUiClickChangePlayerNickName:reset()
    self.new_name = nil
end

EvUiClickChipTransaction = EventBase:new(nil)

function EvUiClickChipTransaction:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickChipTransaction"
    self.send_target_etguid = nil
    return o
end

function EvUiClickChipTransaction:reset()
    self.send_target_etguid = nil
end




--Ui��Ϣ������������ǩ��
EvUiClickChangePlayerIndividualSignature = EventBase:new(nil)

function EvUiClickChangePlayerIndividualSignature:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickChangePlayerIndividualSignature"
    self.new_individual_signature = nil
    return o
end

function EvUiClickChangePlayerIndividualSignature:reset()
    self.new_individual_signature = nil
end



--Ui��Ϣ������������ǩ��
EvUiClickChangePlayerIndividualSignature = EventBase:new(nil)

function EvUiClickChangePlayerIndividualSignature:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickChangePlayerIndividualSignature"
    self.new_individual_signature = nil
    return o
end

function EvUiClickChangePlayerIndividualSignature:reset()
    self.new_individual_signature = nil
end



--Ui��Ϣ������������¼
EvUiClickLogin = EventBase:new(nil)

function EvUiClickLogin:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickLogin"
    self.new_individual_signature = nil
    return o
end

function EvUiClickLogin:reset()
    self.new_individual_signature = nil
end



--Ui��Ϣ������������
EvUiClickInviteFriend = EventBase:new(nil)

function EvUiClickInviteFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickInviteFriend"
    return o
end

function EvUiClickInviteFriend:reset()
end


--Ui��Ϣ������������̵�
EvUiClickShop = EventBase:new(nil)

function EvUiClickShop:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickShop"
    return o
end

function EvUiClickShop:reset()
end


--Ui��Ϣ�����Vip
EvUiClickVip = EventBase:new(nil)

function EvUiClickVip:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickVip"
    return o
end

function EvUiClickVip:reset()
end



--Ui��Ϣ��������������
EvUiClickHelp = EventBase:new(nil)

function EvUiClickHelp:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickHelp"
    return o
end

function EvUiClickHelp:reset()
end



--Ui��Ϣ����ȡ��������
EvUiGetRankingGold = EventBase:new(nil)

function EvUiGetRankingGold:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiGetRankingGold"
    return o
end

function EvUiGetRankingGold:reset()
end



--Ui��Ϣ����ȡ�������
EvUiGetRankingDiamond = EventBase:new(nil)

function EvUiGetRankingDiamond:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiGetRankingDiamond"
    return o
end

function EvUiGetRankingDiamond:reset()
end


--Ui��Ϣ����ȡ��ʤ����
EvUiGetRankingWinGold = EventBase:new(nil)

function EvUiGetRankingWinGold:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiGetRankingWinGold"
    return o
end

function EvUiGetRankingWinGold:reset()
end

EvUiGetRankingRedEnvelopes = EventBase:new(nil)

function EvUiGetRankingRedEnvelopes:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiGetRankingRedEnvelopes"
    return o
end

function EvUiGetRankingRedEnvelopes:reset()
end


--Ui��Ϣ�����ˢ��IPAddress
EvUiClickRefreshIPAddress = EventBase:new(nil)

function EvUiClickRefreshIPAddress:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickRefreshIPAddress"
    return o
end

function EvUiClickRefreshIPAddress:reset()
end


--Ui��Ϣ�������׳�
EvUiRequestFirstRecharge = EventBase:new(nil)

function EvUiRequestFirstRecharge:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestFirstRecharge"
    return o
end

function EvUiRequestFirstRecharge:reset()
end



--Ui��Ϣ��ɾ�����������¼
EvUiClickDeleteFriendChatRecord = EventBase:new(nil)

function EvUiClickDeleteFriendChatRecord:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickDeleteFriendChatRecord"
    self.friend_etguid = nil
    return o
end

function EvUiClickDeleteFriendChatRecord:reset()
    self.friend_etguid = nil
end



--Ui��Ϣ
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

function EvUiClickChooseFriend:reset()
    self.friend_info = nil
    self.is_choosechat = nil
    self.is_recommand = nil
end



--Ui��Ϣ,�������һ����
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

function EvUiInviteFriendPlayTogether:reset()
    self.friend_guid = nil
    self.friend_desktopguid = nil
end



--Ui��Ϣ���ı�����
EvUiChangeLan = EventBase:new(nil)

function EvUiChangeLan:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiChangeLan"
    self.lan = nil
    return o
end

function EvUiChangeLan:reset()
    self.lan = nil
end



--Ui��Ϣ���������������Play��
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

function EvUiClickPlayInDesk:reset()
    self.desk_etguid = nil
    self.desktop_filter = nil
    self.seat_index = nil
end



--Ui��Ϣ���������������View��
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

function EvUiClickViewInDesk:reset()
    self.desk_etguid = nil
    self.desktop_filter = nil
    self.seat_index = nil
end

EvUiRequestPublicMatchList = EventBase:new(nil)

function EvUiRequestPublicMatchList:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestPublicMatchList"
    return o
end

function EvUiRequestPublicMatchList:reset()
end

EvUiRequestPrivateMatchList = EventBase:new(nil)

function EvUiRequestPrivateMatchList:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestPrivateMatchList"
    return o
end

function EvUiRequestPrivateMatchList:reset()
end



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

function EvUiRequestSignUpMatch:reset()
    self.MatchGuid = nil
end



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

function EvUiRequestEnterMatch:reset()
    self.MatchGuid = nil
end



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

function EvUiRequestMatchDetailedInfo:reset()
    self.MatchGuid = nil
end

EvUiRequestGetMatchDetailedInfoByInvitation = EventBase:new(nil)

function EvUiRequestGetMatchDetailedInfoByInvitation:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestGetMatchDetailedInfoByInvitation"
    o.InvitationCode = nil
    return o
end

function EvUiRequestGetMatchDetailedInfoByInvitation:reset()
    self.InvitationCode = nil
end

EvUiRequestCancelSignupMatch = EventBase:new(nil)

function EvUiRequestCancelSignupMatch:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestCancelSignupMatch"
    o.MatchGuid = nil
    return o
end

function EvUiRequestCancelSignupMatch:reset()
    self.MatchGuid = nil
end

--�����ȡ�һ�����Ʒ
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

function EvRequesetGetExchangeCodeItem:reset()
    self.acc = nil
    self.pwd = nil
end

--
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

function EvCreateGiftShop:reset()
    self.is_tmp_gift = false
    self.not_indesktop = false
    self.to_player_etguid = nil
end


--��Ϣ�����غ���ͷ��ɹ�
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

function EvLoadPlayerIconSuccess:reset()
    self.et_guid = nil
    self.icon = nil
    self.fariy_t = nil
end

-- Entity��Ϣ����ȡ��������
EvEntityGetRankingGold = EventBase:new(nil)

function EvEntityGetRankingGold:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGetRankingGold"
    self.ListRanking = {}
    return o
end

function EvEntityGetRankingGold:reset()
    self.ListRanking = {}
end

-- Entity��Ϣ����ȡ�������
EvEntityGetRankingDimond = EventBase:new(nil)

function EvEntityGetRankingDimond:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGetRankingDimond"
    self.ListRanking = {}
    return o
end

function EvEntityGetRankingDimond:reset()
    self.ListRanking = {}
end


-- Entity��Ϣ���յ��������Ϣ
EvEntityReceiceMarquee = EventBase:new(nil)

function EvEntityReceiceMarquee:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityReceiceMarquee"
    self.im_marquee = nil
    return o
end

function EvEntityReceiceMarquee:reset()
    self.im_marquee = nil
end



-- Entity��Ϣ����ȡ�����Ϣ
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

function EvEntityGetPlayerInfoOther:reset()
    self.player_info = nil
    self.ticket = nil
end


--Entity��Ϣ��ɾ��������Ʒ
EvEntityBagDeleteItem = EventBase:new(nil)

function EvEntityBagDeleteItem:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityBagDeleteItem"
    self.item_objid = nil
    return o
end

function EvEntityBagDeleteItem:reset()
    self.item_objid = nil
end


--Entity��Ϣ����ӱ�����Ʒ
EvEntityBagAddItem = EventBase:new(nil)

function EvEntityBagAddItem:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityBagAddItem"
    self.item = nil
    return o
end

function EvEntityBagAddItem:reset()
    self.item = nil
end

--Entity��Ϣ���������
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

function EvEntityGoldChanged:reset()
    self.change_reason = nil
    self.gold_acc = nil
    self.delta_gold = nil
    self.user_data = nil
end



--Entity��Ϣ, ��Ҹ���
EvEntityDiamondChanged = EventBase:new(nil)

function EvEntityDiamondChanged:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDiamondChanged"
    return o
end

function EvEntityDiamondChanged:reset()
end

EvEntityPointChanged = EventBase:new(nil)

function EvEntityPointChanged:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityPointChanged"
    return o
end

function EvEntityPointChanged:reset()
end

--Entity��Ϣ�����г������
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

function EvEntityBankGoldChange:reset()
    self.bank_gold = nil
    self.gold_acc = nil
end






--Entity��Ϣ��֪ͨ��������״̬���
EvEntityFriendOnlineStateChange = EventBase:new(nil)

function EvEntityFriendOnlineStateChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityFriendOnlineStateChange"
    self.player_info = nil
    return o
end

function EvEntityFriendOnlineStateChange:reset()
    self.player_info = nil
end



--Entity��Ϣ��֪ͨɾ������
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

function EvEntityNotifyDeleteFriend:reset()
    self.friend_etguid = nil
    self.map_friend = nil
end



--Entity��Ϣ��ɾ������������Ϣ�ɹ�
EvEntityDeleteFriendChatRecordSuccess = EventBase:new(nil)

function EvEntityDeleteFriendChatRecordSuccess:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDeleteFriendChatRecordSuccess"
    self.friend_etguid = nil
    return o
end

function EvEntityDeleteFriendChatRecordSuccess:reset()
    self.friend_etguid = nil
end



--Entity��Ϣ���յ����������¼
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

function EvEntityReceiveFriendSingleChat:reset()
    self.chat_msg = nil
    self.friend_etguid = nil
end




--Entity��Ϣ���յ����������¼
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

function EvEntityReceiveFriendChats:reset()
    self.list_allchats = nil
    self.friend_etguid = nil
end

EvEntityReceiveFeedbackChat = EventBase:new(nil)
function EvEntityReceiveFeedbackChat:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityReceiveFeedbackChat"
    self.chat_msg = nil
    return o
end

function EvEntityReceiveFeedbackChat:reset()
    self.chat_msg = nil
end

EvEntityReceiveFeedbackChats = EventBase:new(nil)
function EvEntityReceiveFeedbackChats:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityReceiveFeedbackChats"
    self.list_allchats = nil
    return o
end

function EvEntityReceiveFeedbackChats:reset()
    self.list_allchats = nil
end


--Entity��Ϣ����ȡ�����¼
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

function EvEntityChatRecordRequestResult:reset()
    self.list_allchats = nil
    self.friend_etguid = nil
end



--Entity��Ϣ����ȡ�����¼
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

function EvEntityPlayerGiveChipQueryRangeRequestResult:reset()
    self.give_chip_min = nil
    self.give_chip_max = nil
    self.is_success = nil
end



--Entity��Ϣ��֪ͨ������Ϣˢ��
EvEntityRefreshFriendList = EventBase:new(nil)

function EvEntityRefreshFriendList:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityRefreshFriendList"
    self.map_friendinfo = nil
    return o
end

function EvEntityRefreshFriendList:reset()
    self.map_friendinfo = nil
end



--Entity��Ϣ��֪ͨ������Ϣˢ��
EvEntityRefreshFriendInfo = EventBase:new(nil)

function EvEntityRefreshFriendInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityRefreshFriendInfo"
    self.player_info = nil
    return o
end

function EvEntityRefreshFriendInfo:reset()
    self.player_info = nil
end

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

function EvEntityFriendGoldChange:reset()
    self.friend_guid = nil
    self.current_gold = 0
end



--Entity��Ϣ��֪ͨ���Һ���
EvEntityFindFriend = EventBase:new(nil)

function EvEntityFindFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityFindFriend"
    self.list_friend_item = nil
    return o
end

function EvEntityFindFriend:reset()
    self.list_friend_item = nil
end



--Entity��Ϣ����ȡ���������б�
EvEntityGetLobbyDeskList = EventBase:new(nil)

function EvEntityGetLobbyDeskList:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGetLobbyDeskList"
    self.list_desktop = nil
    return o
end

function EvEntityGetLobbyDeskList:reset()
    self.list_desktop = nil
end



--Entity��Ϣ����ȡ�������������б�
EvEntitySearchDesktopFollowFriend = EventBase:new(nil)

function EvEntitySearchDesktopFollowFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntitySearchDesktopFollowFriend"
    self.desktop_info = nil
    return o
end

function EvEntitySearchDesktopFollowFriend:reset()
    self.desktop_info = nil
end



--Entity��Ϣ�����������
EvEntitySearchPlayingFriend = EventBase:new(nil)

function EvEntitySearchPlayingFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntitySearchPlayingFriend"
    self.list_playerinfo = nil
    return o
end

function EvEntitySearchPlayingFriend:reset()
    self.list_playerinfo = nil
end



--Entity��Ϣ�����������
EvEntityGetPlayerModuleDataSuccess = EventBase:new(nil)

function EvEntityGetPlayerModuleDataSuccess:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGetPlayerModuleDataSuccess"
    self.player_moduledata = nil
    return o
end

function EvEntityGetPlayerModuleDataSuccess:reset()
    self.player_moduledata = nil
end



--Entity��Ϣ���Ƽ�����
EvEntityPlayerInfoChanged = EventBase:new(nil)

function EvEntityPlayerInfoChanged:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityPlayerInfoChanged"
    self.controller_actor = nil
    return o
end

function EvEntityPlayerInfoChanged:reset()
    self.controller_actor = nil
end



--Entity��Ϣ����ǰ��ʱ����ı�
EvEntityCurrentTmpGiftChange = EventBase:new(nil)

function EvEntityCurrentTmpGiftChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityCurrentTmpGiftChange"
    return o
end

function EvEntityCurrentTmpGiftChange:reset()
end


--Entity��Ϣ����ȡ�������
EvEntityGetRankingDiamond = EventBase:new(nil)

function EvEntityGetRankingDiamond:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGetRankingDiamond"
    self.list_ranking = nil
    return o
end

function EvEntityGetRankingDiamond:reset()
    self.list_ranking = nil
end



--Entity��Ϣ����ȡ��������
EvEntityGetRankingGold = EventBase:new(nil)

function EvEntityGetRankingGold:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGetRankingGold"
    self.list_rank = nil
    return o
end

function EvEntityGetRankingGold:reset()
    self.list_rank = nil
end



--Entity��Ϣ����ȡ��ʤ����
EvEntityGetRankingWinGold = EventBase:new(nil)

function EvEntityGetRankingWinGold:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGetRankingWinGold"
    self.list_ranking = nil
    return o
end

function EvEntityGetRankingWinGold:reset()
    self.list_ranking = nil
end

EvEntityGetRankingRedEnvelopes = EventBase:new(nil)

function EvEntityGetRankingRedEnvelopes:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGetRankingRedEnvelopes"
    self.list_ranking = nil
    return o
end

function EvEntityGetRankingRedEnvelopes:reset()
    self.list_ranking = nil
end


--Entity��Ϣ������VIP
EvEntityBuyVIP = EventBase:new(nil)

function EvEntityBuyVIP:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityBuyVIP"
    self.buy_id = nil
    return o
end

function EvEntityBuyVIP:reset()
    self.buy_id = nil
end



--��Ϣ��ͼƬ�ϴ��ɹ�
EvGetPicUpLoadSuccess = EventBase:new(nil)

function EvGetPicUpLoadSuccess:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvGetPicUpLoadSuccess"
    return o
end

function EvGetPicUpLoadSuccess:reset()
end


--��Ϣ
EvRequestGetPlayerModuleData = EventBase:new(nil)

function EvRequestGetPlayerModuleData:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvRequestGetPlayerModuleData"
    self.factory_name = nil
    return o
end

function EvRequestGetPlayerModuleData:reset()
    self.factory_name = nil
end

EvOnGetOnLineReward = EventBase:new(nil)
function EvOnGetOnLineReward:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvOnGetOnLineReward"
    return o
end

function EvOnGetOnLineReward:reset()
end

--��Ϣ
EvEntityRefreshLeftOnlineRewardTm = EventBase:new(nil)

function EvEntityRefreshLeftOnlineRewardTm:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityRefreshLeftOnlineRewardTm"
    self.left_reward_second = 0
    return o
end

function EvEntityRefreshLeftOnlineRewardTm:reset()
    self.left_reward_second = 0
end

--��Ϣ
EvRequestGetOnLineReward = EventBase:new(nil)
function EvRequestGetOnLineReward:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvRequestGetOnLineReward"
    return o
end

function EvRequestGetOnLineReward:reset()
end

--��Ϣ
EvEntityCanGetOnlineReward = EventBase:new(nil)
function EvEntityCanGetOnlineReward:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityCanGetOnlineReward"
    self.can_getreward = false
    return o
end

function EvEntityCanGetOnlineReward:reset()
    self.can_getreward = false
end

EvRequestGetTimingReward = EventBase:new(nil)
function EvRequestGetTimingReward:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvRequestGetTimingReward"
    return o
end

function EvRequestGetTimingReward:reset()
end

EvEntityCanGetTimingReward = EventBase:new(nil)
function EvEntityCanGetTimingReward:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityCanGetTimingReward"
    self.can_getreward = false
    return o
end

function EvEntityCanGetTimingReward:reset()
    self.can_getreward = false
end

EvClickShowReward = EventBase:new(nil)
function EvClickShowReward:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvClickShowReward"
    return o
end

function EvClickShowReward:reset()
end

EvEntityPlayerInitDone = EventBase:new(nil)

function EvEntityPlayerInitDone:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityPlayerInitDone"
    return o
end

function EvEntityPlayerInitDone:reset()
end

EvEntityRecommendPlayerList = EventBase:new(nil)

function EvEntityRecommendPlayerList:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityRecommendPlayerList"
    self.list_recommend = nil
    return o
end

function EvEntityRecommendPlayerList:reset()
    self.list_recommend = nil
end

EvEntitySetOnLinePlayerNum = EventBase:new(nil)

function EvEntitySetOnLinePlayerNum:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntitySetOnLinePlayerNum"
    self.online_num = 0
    return o
end

function EvEntitySetOnLinePlayerNum:reset()
    self.online_num = 0
end

EvEntityOnGrowRewardSnapshot = EventBase:new(nil)

function EvEntityOnGrowRewardSnapshot:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityOnGrowRewardSnapshot"
    self.grow_data = nil
    return o
end

function EvEntityOnGrowRewardSnapshot:reset()
    self.grow_data = nil
end

EvUiClickChatmsg = EventBase:new(nil)

function EvUiClickChatmsg:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickChatmsg"
    return o
end

function EvUiClickChatmsg:reset()
end

EvUiClickFriend = EventBase:new(nil)

function EvUiClickFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickFriend"
    return o
end

function EvUiClickFriend:reset()
end

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

function EvEntityGoldChanged:reset()
    self.change_reason = nil
    self.delta_gold = 0
    self.user_data = nil
end







-- DesktopH



EvUiClickDesktopHundred = EventBase:new(nil)

function EvUiClickDesktopHundred:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickDesktopHundred"
    self.factory_name = ""
    return o
end

function EvUiClickDesktopHundred:reset()
    self.factory_name = ""
end

EvEntityPlayerEnterDesktopH = EventBase:new(nil)

function EvEntityPlayerEnterDesktopH:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityPlayerEnterDesktopH"
    return o
end

function EvEntityPlayerEnterDesktopH:reset()
end

EvDesktopHundredChangeCardsType = EventBase:new(nil)

function EvDesktopHundredChangeCardsType:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvDesktopHundredChangeCardsType"
    self.map_card_types = nil
    return o
end

function EvDesktopHundredChangeCardsType:reset()
    self.map_card_types = nil
end

EvUiClickLeaveDesktopHundred = EventBase:new(nil)

function EvUiClickLeaveDesktopHundred:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickLeaveDesktopHundred"
    return o
end

function EvUiClickLeaveDesktopHundred:reset()
end

EvUiRequestGetGrowSnapShot = EventBase:new(nil)

function EvUiRequestGetGrowSnapShot:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestGetGrowSnapShot"
    return o
end

function EvUiRequestGetGrowSnapShot:reset()
end

EvEntityDesktopHGameEndState = EventBase:new(nil)

function EvEntityDesktopHGameEndState:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHGameEndState"
    return o
end

function EvEntityDesktopHGameEndState:reset()
end

EvDesktopHGetBetReward = EventBase:new(nil)

function EvDesktopHGetBetReward:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvDesktopHGetBetReward"
    self.factory_name = nil
    return o
end

function EvDesktopHGetBetReward:reset()
    self.factory_name = nil
end

EvEntityInitBetReward = EventBase:new(nil)

function EvEntityInitBetReward:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityInitBetReward"
    self.init_dailybet_reward = nil
    return o
end

function EvEntityInitBetReward:reset()
    self.init_dailybet_reward = nil
end

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

function EvEntityDesktopHChangeBeBankerPlayerList:reset()
    self.list_bebanker = nil
    self.banker_player = nil
    self.is_bankplayer = false
end

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

function EvEntityDesktopHChangeBankerPlayer:reset()
    self.list_bebankplayer = nil
    self.banker_player = nil
    self.is_bankplayer = false
end

EvEntityDesktopHBankerPlayerGoldChange = EventBase:new(nil)

function EvEntityDesktopHBankerPlayerGoldChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHBankerPlayerGoldChange"
    self.banker_player = nil
    return o
end

function EvEntityDesktopHBankerPlayerGoldChange:reset()
    self.banker_player = nil
end

EvUiDesktopHSeatDown = EventBase:new(nil)

function EvUiDesktopHSeatDown:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiDesktopHSeatDown"
    self.seat_index = 0
    self.min_golds = 0
    return o
end

function EvUiDesktopHSeatDown:reset()
    self.seat_index = 0
    self.min_golds = 0
end

EvDesktopHClickRewardPotBtn = EventBase:new(nil)

function EvDesktopHClickRewardPotBtn:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvDesktopHClickRewardPotBtn"
    return o
end

function EvDesktopHClickRewardPotBtn:reset()
end

EvDesktopHClickBetOperateType = EventBase:new(nil)

function EvDesktopHClickBetOperateType:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvDesktopHClickBetOperateType"
    self.tb_bet_operateid = nil
    return o
end

function EvDesktopHClickBetOperateType:reset()
    self.tb_bet_operateid = nil
end

EvDesktopHGetBetReward = EventBase:new(nil)

function EvDesktopHGetBetReward:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvDesktopHGetBetReward"
    return o
end

function EvDesktopHGetBetReward:reset()
end

EvDesktopHBet = EventBase:new(nil)

function EvDesktopHBet:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvDesktopHBet"
    self.bet_betpot_index = 0
    return o
end

function EvDesktopHBet:reset()
    self.bet_betpot_index = 0
end

EvDesktopHRepeatBet = EventBase:new(nil)

function EvDesktopHRepeatBet:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvDesktopHRepeatBet"
    return o
end

function EvDesktopHRepeatBet:reset()
end

EvDesktopHInitBetReward = EventBase:new(nil)

function EvDesktopHInitBetReward:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvDesktopHInitBetReward"
    self.factory_name = nil
    return o
end

function EvDesktopHInitBetReward:reset()
    self.factory_name = nil
end

EvEntityDesktopHChangeSeatPlayer = EventBase:new(nil)

function EvEntityDesktopHChangeSeatPlayer:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHChangeSeatPlayer"
    self.map_seat_player = {}
    return o
end

function EvEntityDesktopHChangeSeatPlayer:reset()
    self.map_seat_player = {}
end

EvEntityDesktopHBetOperateTypeChange = EventBase:new(nil)

function EvEntityDesktopHBetOperateTypeChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHBetOperateTypeChange"
    self.map_changeoperate = nil
    return o
end

function EvEntityDesktopHBetOperateTypeChange:reset()
    self.map_changeoperate = nil
end

EvEntityDesktopHCurrentBetOperateTypeChange = EventBase:new(nil)

function EvEntityDesktopHCurrentBetOperateTypeChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHCurrentBetOperateTypeChange"
    return o
end

function EvEntityDesktopHCurrentBetOperateTypeChange:reset()
end

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

function EvEntityDesktopHBet:reset()
    self.bet_potindex = 0
    self.bet_golds = 0
    self.already_betgolds = 0
end

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

function EvEntityDesktopHUpdateBetPotBetInfo:reset()
    self.map_betpot_betdeltainfo = nil
    self.map_standplayer_betdeltainfo = nil
    self.list_seatplayer_betinfo = nil
end

EvEntityDesktopHBetFailed = EventBase:new(nil)

function EvEntityDesktopHBetFailed:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHBetFailed"
    self.map_self_betgolds = nil
    return o
end

function EvEntityDesktopHBetFailed:reset()
    self.map_self_betgolds = nil
end

EvEntityDesktopHChangeBankerPlayer = EventBase:new(nil)

function EvEntityDesktopHChangeBankerPlayer:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHChangeBankerPlayer"
    self.banker_player = nil
    return o
end

function EvEntityDesktopHChangeBankerPlayer:reset()
    self.banker_player = nil
end

EvEntityDesktopHBankerPlayerGoldChange = EventBase:new(nil)

function EvEntityDesktopHBankerPlayerGoldChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHBankerPlayerGoldChange"
    self.map_seat_player = nil
    return o
end

function EvEntityDesktopHBankerPlayerGoldChange:reset()
    self.map_seat_player = nil
end

EvEntityDesktopHSeatPlayerGoldChanged = EventBase:new(nil)

function EvEntityDesktopHSeatPlayerGoldChanged:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHSeatPlayerGoldChanged"
    self.map_seatplayer_golds = nil
    return o
end

function EvEntityDesktopHSeatPlayerGoldChanged:reset()
    self.map_seatplayer_golds = nil
end

EvEntityRecvChatFromDesktopH = EventBase:new(nil)

function EvEntityRecvChatFromDesktopH:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityRecvChatFromDesktopH"
    self.chat_info = nil
    return o
end

function EvEntityRecvChatFromDesktopH:reset()
    self.chat_info = nil
end

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

function EvEntityDesktopHReadyState:reset()
    self.left_tm = 0
    self.map_userdata = nil
end

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

function EvEntityDesktopHBetState:reset()
    self.left_tm = 0
    self.max_total_bet_gold = 0
    self.map_betrepeatinfo = nil
end

EvEntityDesktopHGameRestState = EventBase:new(nil)

function EvEntityDesktopHGameRestState:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopHGameRestState"
    self.left_tm = 0
    return o
end

function EvEntityDesktopHGameRestState:reset()
    self.left_tm = 0
end

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

function EvEntityDesktopHBuyItem:reset()
    self.sender_guid = nil
    self.map_items = nil
end

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

function EvEntityDesktopHGetRewardPotInfo:reset()
    self.total_info = nil
    self.reward_totalgolds = 0
end

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

function EvDesktopHClickBeBankPlayerBtn:reset()
    self.bebank_mingolds = 0
    self.take_stack = 0
end





-- Desktop
EvUiClickExitDesk = EventBase:new(nil)

function EvUiClickExitDesk:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickExitDesk"
    return o
end

function EvUiClickExitDesk:reset()
end

EvUiClickInviteFriendPlay = EventBase:new(nil)

function EvUiClickInviteFriendPlay:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickInviteFriendPlay"
    return o
end

function EvUiClickInviteFriendPlay:reset()
end

EvUiClickWaitWhile = EventBase:new(nil)

function EvUiClickWaitWhile:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickWaitWhile"
    return o
end

function EvUiClickWaitWhile:reset()
end

EvUiClickOB = EventBase:new(nil)

function EvUiClickOB:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickOB"
    return o
end

function EvUiClickOB:reset()
end

EvUiRequestLockAllSpectator = EventBase:new(nil)

function EvUiRequestLockAllSpectator:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestLockAllSpectator"
    self.requestLock = false
    return o
end

function EvUiRequestLockAllSpectator:reset()
    self.requestLock = false
end

EvUiRequestLockAllDesktopPlayer = EventBase:new(nil)

function EvUiRequestLockAllDesktopPlayer:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestLockAllDesktopPlayer"
    self.requestLock = false
    return o
end

function EvUiRequestLockAllDesktopPlayer:reset()
    self.requestLock = false
end

EvUiClickPlayerReturn = EventBase:new(nil)

function EvUiClickPlayerReturn:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickPlayerReturn"
    return o
end

function EvUiClickPlayerReturn:reset()
end

EvUiClickRaise = EventBase:new(nil)

function EvUiClickRaise:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickRaise"
    self.raise_gold = 0
    return o
end

function EvUiClickRaise:reset()
    self.raise_gold = 0
end

--请求改变收货地址
EvUiRequestEditReceiverAddress = EventBase:new(nil)

function EvUiRequestEditReceiverAddress:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestEditReceiverAddress"
    o.Address = nil

    return o
end

function EvUiRequestEditReceiverAddress:reset()
    self.Address = nil
end

function EvUiRequestEditReceiverAddress:reset()
    self.Address = nil
end

EvUiClickCancelAutoAction = EventBase:new(nil)

function EvUiClickCancelAutoAction:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickCancelAutoAction"
    return o
end

function EvUiClickCancelAutoAction:reset()
end

EvUiClickCall = EventBase:new(nil)

function EvUiClickCall:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickCall"
    return o
end

function EvUiClickCall:reset()
end

EvUiClickAutoAction = EventBase:new(nil)

function EvUiClickAutoAction:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickAutoAction"
    self.auto_action_type = nil
    return o
end

function EvUiClickAutoAction:reset()
    self.auto_action_type = nil
end

EvUiClickCheck = EventBase:new(nil)

function EvUiClickCheck:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickCheck"
    return o
end

function EvUiClickCheck:reset()
end

EvUiClickFlod = EventBase:new(nil)

function EvUiClickFlod:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickFlod"
    return o
end

function EvUiClickFlod:reset()
end

EvEntityRecvChatFromDesktop = EventBase:new(nil)

function EvEntityRecvChatFromDesktop:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityRecvChatFromDesktop"
    self.chat_info = nil
    return o
end

function EvEntityRecvChatFromDesktop:reset()
    self.chat_info = nil
end

EvCurrentWinner = EventBase:new(nil)

function EvCurrentWinner:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvCurrentWinner"
    self.player = nil
    return o
end

function EvCurrentWinner:reset()
    self.player = nil
end

EvUiClickDesktop = EventBase:new(nil)

function EvUiClickDesktop:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickDesktop"
    return o
end

function EvUiClickDesktop:reset()
end

EvCommonCardShowEnd = EventBase:new(nil)

function EvCommonCardShowEnd:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvCommonCardShowEnd"
    return o
end

function EvCommonCardShowEnd:reset()
end

EvCommonCardDealEnd = EventBase:new(nil)

function EvCommonCardDealEnd:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvCommonCardDealEnd"
    return o
end

function EvCommonCardDealEnd:reset()
end

EvUiClickShop = EventBase:new(nil)

function EvUiClickShop:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickShop"
    return o
end

function EvUiClickShop:reset()
end

EvUiClickFriend = EventBase:new(nil)

function EvUiClickFriend:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickFriend"
    return o
end

function EvUiClickFriend:reset()
end

EvUiDesktopClickLockChat = EventBase:new(nil)

function EvUiDesktopClickLockChat:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiDesktopClickLockChat"
    return o
end

function EvUiDesktopClickLockChat:reset()
end

EvUiClickChatmsg = EventBase:new(nil)

function EvUiClickChatmsg:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickChatmsg"
    return o
end

function EvUiClickChatmsg:reset()
end

EvUiClickSeat = EventBase:new(nil)

function EvUiClickSeat:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickSeat"
    self.seat_index = 0
    return o
end

function EvUiClickSeat:reset()
    self.seat_index = 0
end

EvUiClickInviteFriendPlay = EventBase:new(nil)

function EvUiClickInviteFriendPlay:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickInviteFriendPlay"
    return o
end

function EvUiClickInviteFriendPlay:reset()
end

EvEntityReceiveFriendChats = EventBase:new(nil)

function EvEntityReceiveFriendChats:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityReceiveFriendChats"
    return o
end

function EvEntityReceiveFriendChats:reset()
end

EvEntityUnreadChatsChanged = EventBase:new(nil)

function EvEntityUnreadChatsChanged:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityUnreadChatsChanged"
    return o
end

function EvEntityUnreadChatsChanged:reset()
end

EvEntityMailListInit = EventBase:new(nil)

function EvEntityMailListInit:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityMailListInit"
    return o
end

function EvEntityMailListInit:reset()
end

EvEntityMailAdd = EventBase:new(nil)

function EvEntityMailAdd:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityMailAdd"
    return o
end

function EvEntityMailAdd:reset()
end

EvEntityMailDelete = EventBase:new(nil)

function EvEntityMailDelete:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityMailDelete"
    return o
end

function EvEntityMailDelete:reset()
end

EvEntityMailUpdate = EventBase:new(nil)

function EvEntityMailUpdate:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityMailUpdate"
    return o
end

function EvEntityMailUpdate:reset()
end




--Desktop

EvEntityPlayerEnterDesktop = EventBase:new(nil)

function EvEntityPlayerEnterDesktop:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityPlayerEnterDesktop"
    return o
end

function EvEntityPlayerEnterDesktop:reset()
end

EvEntityPlayerEnterDesktopFailed = EventBase:new(nil)

function EvEntityPlayerEnterDesktopFailed:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityPlayerEnterDesktopFailed"
    return o
end

function EvEntityPlayerEnterDesktopFailed:reset()
end

EvEntityRecvChatFromDesktop = EventBase:new(nil)

function EvEntityRecvChatFromDesktop:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityRecvChatFromDesktop"
    self.chat_info = nil
    return o
end

function EvEntityRecvChatFromDesktop:reset()
    self.chat_info = nil
end

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

function EvEntityDesktopSnapshotNotify:reset()
    self.desktop = nil
    self.desktop_data = nil
    self.is_init = false
end

EvEntityDesktopIdleNotify = EventBase:new(nil)

function EvEntityDesktopIdleNotify:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopIdleNotify"
    return o
end

function EvEntityDesktopIdleNotify:reset()
end

EvEntityDesktopPreFlopNotify = EventBase:new(nil)

function EvEntityDesktopPreFlopNotify:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopPreFlopNotify"
    return o
end

function EvEntityDesktopPreFlopNotify:reset()
end

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

function EvEntityDesktopFlopNotify:reset()
    self.first_card = nil
    self.second_card = nil
    self.third_card = nil
    self.bet_player_count = 0
end

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

function EvEntityDesktopTurnNotify:reset()
    self.turn_card = nil
    self.bet_player_count = 0
end

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

function EvEntityDesktopRiverNotify:reset()
    self.river_card = nil
    self.bet_player_count = 0
end

EvEntityDesktopShowdownNotify = EventBase:new(nil)

function EvEntityDesktopShowdownNotify:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopShowdownNotify"
    self.desktop_showdown = nil

    return o
end

function EvEntityDesktopShowdownNotify:reset()
    self.desktop_showdown = nil
end

EvEntityDesktopGameEndNotifyTexas = EventBase:new(nil)

function EvEntityDesktopGameEndNotifyTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopGameEndNotifyTexas"
    self.list_winner = nil
    return o
end

function EvEntityDesktopGameEndNotifyTexas:reset()
    self.list_winner = nil
end

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

function EvEntityDesktopPlayerSit:reset()
    self.guid = ""
    self.account_id = ""
    self.nick_name = ""
    self.icon_name = ""
    self.vip_level = 0
end

EvEntityDesktopPlayerLeaveChair = EventBase:new(nil)

function EvEntityDesktopPlayerLeaveChair:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityDesktopPlayerLeaveChair"
    self.guid = ""
    return o
end

function EvEntityDesktopPlayerLeaveChair:reset()
    self.guid = ""
end

EvEntityBagUpdateItem = EventBase:new(nil)

function EvEntityBagUpdateItem:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityBagUpdateItem"
    self.item = nil
    return o
end

function EvEntityBagUpdateItem:reset()
    self.item = nil
end

EvOpenBag = EventBase:new(nil)

function EvOpenBag:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvOpenBag"
    return o
end

function EvOpenBag:reset()
end

EvEntityRequestGetLotteryTicketData = EventBase:new(nil)

function EvEntityRequestGetLotteryTicketData:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityRequestGetLotteryTicketData"
    return o
end

function EvEntityRequestGetLotteryTicketData:reset()
end

EvEntityLotteryTicketBetOperateTypeChange = EventBase:new(nil)

function EvEntityLotteryTicketBetOperateTypeChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityLotteryTicketBetOperateTypeChange"
    self.map_changeoperate = nil
    return o
end

function EvEntityLotteryTicketBetOperateTypeChange:reset()
    self.map_changeoperate = nil
end

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

function EvEntityLotteryTicketUpdateBetPotBetInfo:reset()
    self.map_allbetpot = {}
    self.map_self_betinfo = {}
end

EvEntityGetLotteryTicketDataSuccess = EventBase:new(nil)

function EvEntityGetLotteryTicketDataSuccess:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGetLotteryTicketDataSuccess"
    self.lotteryticket_data = nil
    return o
end

function EvEntityGetLotteryTicketDataSuccess:reset()
    self.lotteryticket_data = nil
end

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

function EvEntityLotteryTicketGameEndState:reset()
    self.gameend_detail = nil
    self.me_wingold = 0
end

EvEntityLotteryTicketGameEndStateSimple = EventBase:new(nil)

function EvEntityLotteryTicketGameEndStateSimple:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityLotteryTicketGameEndStateSimple"
    return o
end

function EvEntityLotteryTicketGameEndStateSimple:reset()
end

EvEntityLotteryTicketBetState = EventBase:new(nil)

function EvEntityLotteryTicketBetState:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityLotteryTicketBetState"
    self.map_betrepeatinfo = {}
    return o
end

function EvEntityLotteryTicketBetState:reset()
    self.map_betrepeatinfo = {}
end

EvLotteryTicketClickRewardPotBtn = EventBase:new(nil)

function EvLotteryTicketClickRewardPotBtn:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvLotteryTicketClickRewardPotBtn"
    return o
end

function EvLotteryTicketClickRewardPotBtn:reset()
end

EvLotteryTicketBet = EventBase:new(nil)

function EvLotteryTicketBet:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvLotteryTicketBet"
    self.bet_betpot_index = 0
    return o
end

function EvLotteryTicketBet:reset()
    self.bet_betpot_index = 0
end

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

function EvEntityLotteryTicketGetRewardPotInfo:reset()
    self.list_playerinfo = nil
    self.reward_totalgolds = 0
end

EvUiClickLeaveLotteryTicket = EventBase:new(nil)

function EvUiClickLeaveLotteryTicket:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickLeaveLotteryTicket"
    return o
end

function EvUiClickLeaveLotteryTicket:reset()
end

EvEntityLotteryTicketUpdateTm = EventBase:new(nil)

function EvEntityLotteryTicketUpdateTm:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityLotteryTicketUpdateTm"
    self.tm = 0

    return o
end

function EvEntityLotteryTicketUpdateTm:reset()
    self.tm = 0
end

EvLotteryTicketClickBetOperateType = EventBase:new(nil)

function EvLotteryTicketClickBetOperateType:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvLotteryTicketClickBetOperateType"
    self.tb_bet_operateid = 0

    return o
end

function EvLotteryTicketClickBetOperateType:reset()
    self.tb_bet_operateid = 0
end

EvEntityLotteryTicketCurrentBetOperateTypeChange = EventBase:new(nil)

function EvEntityLotteryTicketCurrentBetOperateTypeChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityLotteryTicketCurrentBetOperateTypeChange"

    return o
end

function EvEntityLotteryTicketCurrentBetOperateTypeChange:reset()
end

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

function EvEntityLotteryTicketBet:reset()
    self.already_bet_chips = 0
    self.bet_potindex = 0
end

EvLotteryTicketRepeatBet = EventBase:new(nil)
function EvLotteryTicketRepeatBet:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvLotteryTicketRepeatBet"

    return o
end

function EvLotteryTicketRepeatBet:reset()
end

EvRequestSendMarquee = EventBase:new(nil)

function EvRequestSendMarquee:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvRequestSendMarquee"
    self.msg = nil
    return o
end

function EvRequestSendMarquee:reset()
    self.msg = nil
end

EvEntityGetDesktopData = EventBase:new(nil)

function EvEntityGetDesktopData:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityGetDesktopData"
    return o
end

function EvEntityGetDesktopData:reset()
end

EvEntityRequestGetDesktopHData = EventBase:new(nil)

function EvEntityRequestGetDesktopHData:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityRequestGetDesktopHData"
    return o
end

function EvEntityRequestGetDesktopHData:reset()
end

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

function EvEntitySetPublicMatchLsit:reset()
    self.MatchType = nil
    self.ListMatch = nil
    self.SelfMatchNum = 0
end

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

function EvEntitySetPrivateMatchLsit:reset()
    self.ListMatch = nil
    self.ListApplyMatchGuid = nil
end

EvEntityUpdatePublicMatchPlayerNum = EventBase:new(nil)

function EvEntityUpdatePublicMatchPlayerNum:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityUpdatePublicMatchPlayerNum"
    o.ListMatchNum = nil

    return o
end

function EvEntityUpdatePublicMatchPlayerNum:reset()
    self.ListMatchNum = nil
end

EvEntityUpdatePrivateMatchPlayerNum = EventBase:new(nil)

function EvEntityUpdatePrivateMatchPlayerNum:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityUpdatePrivateMatchPlayerNum"
    o.ListMatchNum = nil

    return o
end

function EvEntityUpdatePrivateMatchPlayerNum:reset()
    self.ListMatchNum = nil
end

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

function EvEntityMTTUpdateRealtimeInfo:reset()
    self.RealtimeInfo = nil
    self.blind_type = 0
end

EvEntityMTTUpdateRaiseBlindTm = EventBase:new(nil)

function EvEntityMTTUpdateRaiseBlindTm:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityMTTUpdateRaiseBlindTm"
    o.RaiseBlindTm = 0

    return o
end

function EvEntityMTTUpdateRaiseBlindTm:reset()
    self.RaiseBlindTm = 0
end

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

function EvEntityMTTPlayerRebuyOrAddonRefresh:reset()
    self.can_rebuy = false
    self.can_addon = false
end

EvUiMTTCreateRebuyOrAddOn = EventBase:new(nil)

function EvUiMTTCreateRebuyOrAddOn:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiMTTCreateRebuyOrAddOn"

    return o
end

function EvUiMTTCreateRebuyOrAddOn:reset()
end

EvMTTPauseChanged = EventBase:new(nil)

function EvMTTPauseChanged:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvMTTPauseChanged"
    self.pause_info = nil

    return o
end

function EvMTTPauseChanged:reset()
    self.pause_info = nil
end

EvEntitySignUpSucceed = EventBase:new(nil)

function EvEntitySignUpSucceed:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntitySignUpSucceed"
    o.MatchGuid = nil

    return o
end

function EvEntitySignUpSucceed:reset()
    self.MatchGuid = nil
end

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

function EvEntityResponseCancelSignUpMatch:reset()
    self.MatchGuid = nil
end

EvEntitySetMatchDetailedInfo = EventBase:new(nil)

function EvEntitySetMatchDetailedInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntitySetMatchDetailedInfo"
    o.MatchDetailedInfo = nil

    return o
end

function EvEntitySetMatchDetailedInfo:reset()
    self.MatchDetailedInfo = nil
end

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

function EvEntitySetRaiseBlindTbInfo:reset()
    self.raise_blind_info = nil
    self.current_raiseblind_tbid = 0
end

EvEntityMatchGameOver = EventBase:new(nil)

function EvEntityMatchGameOver:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityMatchGameOver"
    o.game_over = nil

    return o
end

function EvEntityMatchGameOver:reset()
    self.game_over = nil
end

EvEntitySelfIsOB = EventBase:new(nil)

function EvEntitySelfIsOB:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntitySelfIsOB"

    return o
end

function EvEntitySelfIsOB:reset()
end

EvEntitySelfAutoActionChange = EventBase:new(nil)

function EvEntitySelfAutoActionChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntitySelfAutoActionChange"
    o.is_autoaction = false

    return o
end

function EvEntitySelfAutoActionChange:reset()
    o.is_autoaction = false
end

EvUiClickFastBet = EventBase:new(nil)

function EvUiClickFastBet:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickFastBet"
    self.bet_value = 0
    return o
end

function EvUiClickFastBet:reset()
    self.bet_value = 0
end

EvUiClickShowCard = EventBase:new(nil)

function EvUiClickShowCard:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiClickShowCard"
    self.click_card1 = false

    return o
end

function EvUiClickShowCard:reset()
    self.click_card1 = false
end

EvUiChooseUCenter = EventBase:new(nil)

function EvUiChooseUCenter:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiChooseUCenter"
    self.ucenter = ""

    return o
end

function EvUiChooseUCenter:reset()
    self.ucenter = ""
end

EvUiChooseGateWay = EventBase:new(nil)

function EvUiChooseGateWay:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiChooseGateWay"
    self.gateway = ""

    return o
end

function EvUiChooseGateWay:reset()
    self.gateway = ""
end

EvUiPotMainChanged = EventBase:new(nil)

function EvUiPotMainChanged:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiPotMainChanged"
    self.pot_mian = 0

    return o
end

function EvUiPotMainChanged:reset()
    self.pot_mian = 0
end


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

function EvEntityResponseGetReceiverAddress:reset()
    self.Address = nil
end


-- 请求获取收货地址
EvUiRequestGetReceiverAddress = EventBase:new(nil)

function EvUiRequestGetReceiverAddress:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUiRequestGetReceiverAddress"

    return o
end

function EvUiRequestGetReceiverAddress:reset()
end

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

function EvUiRequestOperateItem:reset()
    o.ItemObjId = nil
end

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

function EvConsoleCmd:reset()
    self.ListParam = nil
end

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

function EvEntityUpdatePlayerPoint:reset()
    self.Point = 0
end

EvClickShare = EventBase:new(nil)
function EvClickShare:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvClickShare"
    self.ShareType = 0

    return o
end

function EvClickShare:reset()
    self.ShareType = 0
end

EvPayWithIAPSuccess = EventBase:new(nil)
function EvPayWithIAPSuccess:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvPayWithIAPSuccess"
    o.purchase = nil

    return o
end

function EvPayWithIAPSuccess:reset()
    self.purchase = nil
end

EvGetPicSuccess = EventBase:new(nil)
function EvGetPicSuccess:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvGetPicSuccess"
    o.pic_data = nil

    return o
end

function EvGetPicSuccess:reset()
    self.pic_data = nil
end

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

function EvCheckIdCard:reset()
    self.name = nil
    self.id_card = nil
end

EvEntityIsFirstRechargeChanged = EventBase:new(nil)
function EvEntityIsFirstRechargeChanged:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvEntityIsFirstRechargeChanged"

    return o
end

function EvEntityIsFirstRechargeChanged:reset()
end

EvClickIconWithNickName = EventBase:new(nil)
function EvClickIconWithNickName:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvClickIconWithNickName"
    o.player = nil

    return o
end

function EvClickIconWithNickName:reset()
    self.player = nil
end

EvRemoveMatch = EventBase:new(nil)
function EvRemoveMatch:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvRemoveMatch"
    o.MatchGuid = nil

    return o
end

function EvRemoveMatch:reset()
    self.MatchGuid = nil
end

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

function EvUpdatePlayerScore:reset()
    self.PlayerGuid = nil
    self.Score = 0
end

EvBindWeChat = EventBase:new(nil)
function EvBindWeChat:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvBindWeChat"
    o.ItemObjId = nil

    return o
end

function EvBindWeChat:reset()
    self.ItemObjId = nil
end

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

function EvBindWeChatSuccess:reset()
    self.IsSuccess = false
    self.WeChatOpenId = nil
    self.WeChatName = nil
end

EvUnbindWeChat = EventBase:new(nil)
function EvUnbindWeChat:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUnbindWeChat"

    return o
end

function EvUnbindWeChat:reset()
end

EvUnBindWeChatSuccess = EventBase:new(nil)
function EvUnBindWeChatSuccess:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvUnBindWeChatSuccess"
    o.IsSuccess = false

    return o
end

function EvUnBindWeChatSuccess:reset()
    self.IsSuccess = false
end