//// Copyright(c) Cragon. All rights reserved.

//namespace Casinos
//{
//    using System;
//    using System.Collections;
//    using System.Collections.Generic;
//    using UnityEngine;
//    using FairyGUI;
//    using GameCloud.Unity.Common;

//    //-----------------------------------------------------------------------------
//    public class EvUiSendSecurityCode : EntityEvent
//    {
//        public EvUiSendSecurityCode() : base() { }
//        public string phone_num;
//    }

//    //-----------------------------------------------------------------------------
//    public class EvUiBindWeiChat : EntityEvent
//    {
//        public EvUiBindWeiChat() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    public class EvUiBindPhone : EntityEvent
//    {
//        public EvUiBindPhone() : base() { }
//        public string phone_num;
//        public string security_code;
//    }

//    //-----------------------------------------------------------------------------
//    public class EvUiUnBindPhone : EntityEvent
//    {
//        public EvUiUnBindPhone() : base() { }
//        public string phone_num;
//        public string security_code;
//    }

//    //-----------------------------------------------------------------------------
//    // 销毁找回密码界面
//    public class EvResetPwdDestroy : EntityEvent
//    {
//        public EvResetPwdDestroy() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // 销毁注册界面
//    public class EvRegisterDestroy : EntityEvent
//    {
//        public EvRegisterDestroy() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击主界面商店
//    public class EvUiClickShop : EntityEvent
//    {
//        public EvUiClickShop() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击主界面好友
//    public class EvUiClickFriend : EntityEvent
//    {
//        public EvUiClickFriend() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，桌内点击锁定界面
//    public class EvUiDesktopClickLockChat : EntityEvent
//    {
//        public EvUiDesktopClickLockChat() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击主界面聊天
//    public class EvUiClickChatmsg : EntityEvent
//    {
//        public EvUiClickChatmsg() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击选择好友聊天对象
//    public class EvUiClickChooseFriendChatTarget : EntityEvent
//    {
//        public EvUiClickChooseFriendChatTarget() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    public class EvUiCurrentChatTargetChange : EntityEvent
//    {
//        public EvUiCurrentChatTargetChange() : base() { }
//        public string current_chattarget;
//    }

//    //-----------------------------------------------------------------------------
//    public class EvUiRequestFriendAddOrRemove : EntityEvent
//    {
//        public EvUiRequestFriendAddOrRemove() : base() { }
//        public bool is_add;
//        public string friend_guid;
//        public string friend_nickname;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击主界面帮助
//    public class EvUiClickHelp : EntityEvent
//    {
//        public EvUiClickHelp() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击主界面设置
//    public class EvUiClickEdit : EntityEvent
//    {
//        public EvUiClickEdit() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击主界面登录
//    public class EvUiClickLogin : EntityEvent
//    {
//        public EvUiClickLogin() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击百人桌
//    public class EvUiClickDesktopHundred : EntityEvent
//    {
//        public EvUiClickDesktopHundred() : base() { }
//        public string factory_name;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，离开百人桌
//    public class EvUiClickLeaveDesktopHundred : EntityEvent
//    {
//        public EvUiClickLeaveDesktopHundred() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击Vip
//    public class EvUiClickVip : EntityEvent
//    {
//        public EvUiClickVip() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击邀请好友
//    public class EvUiClickInviteFriend : EntityEvent
//    {
//        public EvUiClickInviteFriend() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    public class EvUiClickInviteFriendPlay : EntityEvent
//    {
//        public EvUiClickInviteFriendPlay() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击坐下(下注游戏)
//    public class EvUiClickSeat : EntityEvent
//    {
//        public EvUiClickSeat() : base() { }
//        public byte seat_index;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击离开桌子
//    public class EvUiClickExitDesk : EntityEvent
//    {
//        public EvUiClickExitDesk() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击盖牌
//    public class EvUiClickFlod : EntityEvent
//    {
//        public EvUiClickFlod() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击让牌
//    public class EvUiClickCheck : EntityEvent
//    {
//        public EvUiClickCheck() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击跟注
//    public class EvUiClickCall : EntityEvent
//    {
//        public EvUiClickCall() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击加注
//    public class EvUiClickRaise : EntityEvent
//    {
//        public EvUiClickRaise() : base() { }
//        public long raise_gold;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击暂时离开
//    public class EvUiClickWaitWhile : EntityEvent
//    {
//        public EvUiClickWaitWhile() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击回到桌子
//    public class EvUiClickPlayerReturn : EntityEvent
//    {
//        public EvUiClickPlayerReturn() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击站起
//    public class EvUiClickOB : EntityEvent
//    {
//        public EvUiClickOB() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击预操作
//    public class EvUiClickAutoAction : EntityEvent
//    {
//        public EvUiClickAutoAction() : base() { }
//        public PlayerAutoActionTypeTexas auto_action_type;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击取消预操作
//    public class EvUiClickCancelAutoAction : EntityEvent
//    {
//        public EvUiClickCancelAutoAction() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击看牌
//    public class EvUiClickSeeCard : EntityEvent
//    {
//        public EvUiClickSeeCard() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击准备比牌
//    public class EvUiClickReadyCompareCard : EntityEvent
//    {
//        public EvUiClickReadyCompareCard() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击比牌
//    public class EvUiClickCompareCard : EntityEvent
//    {
//        public EvUiClickCompareCard() : base() { }
//        public string player_guid;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击跟到底
//    public class EvUiClickAutoAlwaysCall : EntityEvent
//    {
//        public EvUiClickAutoAlwaysCall() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击火拼下注
//    public class EvUiClickFireBet : EntityEvent
//    {
//        public EvUiClickFireBet() : base() { }
//        public long value;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击准备
//    public class EvUiClickReady : EntityEvent
//    {
//        public EvUiClickReady() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    public class EvUiClickDesktop : EntityEvent
//    {
//        public EvUiClickDesktop() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击查找朋友桌
//    public class EvUiClickSearchFriendsDesk : EntityEvent
//    {
//        public EvUiClickSearchFriendsDesk() : base() { }
//        public _eFriendStateClient friend_state;
//    }

//    //-----------------------------------------------------------------------------
//    //Ui消息，点击离开大厅
//    public class EvUiClickLeaveLobby : EntityEvent
//    {
//        public EvUiClickLeaveLobby() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    //Ui消息，点击更改玩家昵称
//    public class EvUiClickChangePlayerNickName : EntityEvent
//    {
//        public EvUiClickChangePlayerNickName() : base() { }
//        public string new_name;
//    }

//    //-----------------------------------------------------------------------------
//    //Ui消息，点击更改玩家签名
//    public class EvUiClickChangePlayerIndividualSignature : EntityEvent
//    {
//        public EvUiClickChangePlayerIndividualSignature() : base() { }
//        public string new_individual_signature;
//    }

//    //-----------------------------------------------------------------------------
//    //Ui消息，点击更改皮肤
//    public class EvUiClickChangePlayerProfileSkin : EntityEvent
//    {
//        public EvUiClickChangePlayerProfileSkin() : base() { }
//        public int skin_id;
//    }

//    //-----------------------------------------------------------------------------
//    //Ui消息，点击刷新IPAddress
//    public class EvUiClickRefreshIPAddress : EntityEvent
//    {
//        public EvUiClickRefreshIPAddress() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    //Ui消息，查找好友
//    public class EvUiFindFriend : EntityEvent
//    {
//        public EvUiFindFriend() : base() { }
//        public string search_filter;
//    }

//    //-----------------------------------------------------------------------------
//    //Ui消息，添加好友
//    public class EvUiAddFriend : EntityEvent
//    {
//        public EvUiAddFriend() : base() { }
//        public string friend_etguid;
//    }

//    //-----------------------------------------------------------------------------
//    //Ui消息，移除好友
//    public class EvUiDeleteFriend : EntityEvent
//    {
//        public EvUiDeleteFriend() : base() { }
//        public string friend_etguid;
//    }

//    //-----------------------------------------------------------------------------
//    //Ui消息，确认读取好友聊天记录
//    public class EvUiChatConfirmRead : EntityEvent
//    {
//        public EvUiChatConfirmRead() : base() { }
//        public string friend_etguid;
//        public ulong msg_id;
//    }

//    //-----------------------------------------------------------------------------
//    //Ui消息，删除好友聊天记录
//    public class EvUiClickDeleteFriendChatRecord : EntityEvent
//    {
//        public EvUiClickDeleteFriendChatRecord() : base() { }
//        public string friend_etguid;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击发送筹码
//    public class EvUiClickChipTransaction : EntityEvent
//    {
//        public EvUiClickChipTransaction() : base() { }
//        public string send_target_etguid;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，确认发送筹码
//    public class EvUiClickConfirmChipTransaction : EntityEvent
//    {
//        public EvUiClickConfirmChipTransaction() : base() { }
//        public string send_target_etguid;
//        public long chip;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击请求系统事件
//    public class EvUiRequestShowSystemEvent : EntityEvent
//    {
//        public EvUiRequestShowSystemEvent() : base() { }
//    }


//    //-----------------------------------------------------------------------------
//    // Ui消息，点击查询该好友所在牌桌
//    public class EvUiRequestGetCurrentFriendPlayDesk : EntityEvent
//    {
//        public EvUiRequestGetCurrentFriendPlayDesk() : base() { }
//        public string player_guid;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，发送消息
//    public class EvUiSetUnSendDesktopMsg : EntityEvent
//    {
//        public EvUiSetUnSendDesktopMsg() : base() { }
//        public string text;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，邀请好友一起玩
//    public class EvUiInviteFriendPlayTogether : EntityEvent
//    {
//        public EvUiInviteFriendPlayTogether() : base() { }
//        public string friend_guid;
//        public string friend_desktopguid;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，创建主界面
//    public class EvUiCreateMainUi : EntityEvent
//    {
//        public EvUiCreateMainUi() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，获取筹码排行
//    public class EvUiGetRankingGold : EntityEvent
//    {
//        public EvUiGetRankingGold() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，获取金币排行
//    public class EvUiGetRankingDiamond : EntityEvent
//    {
//        public EvUiGetRankingDiamond() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，获取等级排行
//    public class EvUiGetRankingLevel : EntityEvent
//    {
//        public EvUiGetRankingLevel() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，获取礼物排行
//    public class EvUiGetRankingGift : EntityEvent
//    {
//        public EvUiGetRankingGift() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，获取豪胜排行
//    public class EvUiGetRankingWinGold : EntityEvent
//    {
//        public EvUiGetRankingWinGold() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，创建兑换筹码界面
//    public class EvUiCreateExchangeChip : EntityEvent
//    {
//        public EvUiCreateExchangeChip() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，请求获取商店筹码
//    public class EvUiRequestGetShopChipList : EntityEvent
//    {
//        public EvUiRequestGetShopChipList() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，请求获取商店金币
//    public class EvUiRequestGetShopCoinList : EntityEvent
//    {
//        public EvUiRequestGetShopCoinList() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，请求购买筹码
//    public class EvUiRequestBuyGold : EntityEvent
//    {
//        public EvUiRequestBuyGold() : base() { }
//        public int buy_goldid;//所买筹码方案id        
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，请求购买金币
//    public class EvUiRequestBuyDiamond : EntityEvent
//    {
//        public EvUiRequestBuyDiamond() : base() { }
//        public int buy_diamondid;//所买金币方案id        
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，请求首充
//    public class EvUiRequestFirstRecharge : EntityEvent
//    {
//        public EvUiRequestFirstRecharge() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，请求购买物品
//    public class EvUiRequestBuyItem : EntityEvent
//    {
//        public EvUiRequestBuyItem() : base() { }
//        public int item_tbid;
//        public bool is_firstrecharge;
//        public int item_count;
//        public _ePayType pay_type;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，请求获取排行玩家信息
//    public class EvUiRequestGetRankPlayerInfo : EntityEvent
//    {
//        public EvUiRequestGetRankPlayerInfo() : base() { }
//        public string player_guid;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，请求好友详细信息
//    public class EvUiRequestPlayerInfoFriend : EntityEvent
//    {
//        public EvUiRequestPlayerInfoFriend() : base() { }
//        public string player_guid;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击购买物品
//    public class EvUiBuyItem : EntityEvent
//    {
//        public EvUiBuyItem() : base() { }
//        public int item_id;
//        public string to_etguid;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击出售物品
//    public class EvUiSellItem : EntityEvent
//    {
//        public EvUiSellItem() : base() { }
//        public string item_objid;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击移除礼物
//    public class EvUiRemoveItem : EntityEvent
//    {
//        public EvUiRemoveItem() : base() { }
//        public string obj_id;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，请求屏蔽或打开某人消息
//    public class EvUiRequestLockPlayerChat : EntityEvent
//    {
//        public EvUiRequestLockPlayerChat() : base() { }
//        public string player_guid;
//        public bool requestLock;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，请求屏蔽或打开所有桌上玩家
//    public class EvUiRequestLockAllDesktopPlayer : EntityEvent
//    {
//        public EvUiRequestLockAllDesktopPlayer() : base() { }
//        public bool requestLock;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，请求屏蔽或打开所有本桌旁观者
//    public class EvUiRequestLockAllSpectator : EntityEvent
//    {
//        public EvUiRequestLockAllSpectator() : base() { }
//        public bool requestLock;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，请求屏蔽或打开系统消息
//    public class EvUiRequestLockSystemChat : EntityEvent
//    {
//        public EvUiRequestLockSystemChat() : base() { }
//        public bool requestLock;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，请求取钱
//    public class EvUiRequestBankWithdraw : EntityEvent
//    {
//        public EvUiRequestBankWithdraw() : base() { }
//        public long withdraw_chip;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，请求存钱
//    public class EvUiRequestBankDeposit : EntityEvent
//    {
//        public EvUiRequestBankDeposit() : base() { }
//        public long deposit_chip;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，请求获取成长奖励快照
//    public class EvUiRequestGetGrowSnapShot : EntityEvent
//    {
//        public EvUiRequestGetGrowSnapShot() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，请求领取成长奖励
//    public class EvUiRequestGetGrowReward : EntityEvent
//    {
//        public EvUiRequestGetGrowReward() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    //消息，加载好友头像成功
//    public class EvLoadPlayerIconSuccess : EntityEvent
//    {
//        public EvLoadPlayerIconSuccess() : base() { }
//        public string et_guid;
//        public Texture icon;
//        public NTexture fariy_t;
//    }

//    //-----------------------------------------------------------------------------
//    //消息，加载好友头像成功
//    public class EvCreateGiftShop : EntityEvent
//    {
//        public EvCreateGiftShop() : base() { }
//        public bool is_tmp_gift;
//        public bool not_indesktop;
//        public string to_player_etguid;
//    }

//    //-----------------------------------------------------------------------------
//    // 购买付费道具成功
//    public class EvBuyRMBItemSuccess : EntityEvent
//    {
//        public EvBuyRMBItemSuccess() : base() { }
//        public OnePF.Purchase purchase;
//    }

//    //#if UNITY_EDITOR ||  USE_DESKTOPTEXAS
//    //    //-----------------------------------------------------------------------------
//    //    // 当前赢家
//    //    public class EvCurrentWinner : EntityEvent
//    //    {
//    //        public EvCurrentWinner() : base() { }
//    //        public DesktopPlayerTexas player;
//    //    }
//    //#endif

//    //-----------------------------------------------------------------------------
//    // 翻牌结束
//    public class EvCommonCardShowEnd : EntityEvent
//    {
//        public EvCommonCardShowEnd() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // 发牌结束
//    public class EvCommonCardDealEnd : EntityEvent
//    {
//        public EvCommonCardDealEnd() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // 点击百人桌坐庄按钮
//    public class EvDesktopHClickBeBankPlayerBtn : EntityEvent
//    {
//        public EvDesktopHClickBeBankPlayerBtn() : base() { }
//        public long bebank_mingolds;
//        public long take_stack;
//    }

//    //-----------------------------------------------------------------------------
//    // 点击百人桌奖池
//    public class EvDesktopHClickRewardPotBtn : EntityEvent
//    {
//        public EvDesktopHClickRewardPotBtn() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // 点击百人桌大厅玩家
//    public class EvDesktopHClickStandPlayerBtn : EntityEvent
//    {
//        public EvDesktopHClickStandPlayerBtn() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // 百人桌更改下注大小
//    public class EvDesktopHClickBetOperateType : EntityEvent
//    {
//        public EvDesktopHClickBetOperateType() : base() { }
//        public int tb_bet_operateid;
//    }

//    //-----------------------------------------------------------------------------
//    // 百人桌下注
//    public class EvDesktopHBet : EntityEvent
//    {
//        public EvDesktopHBet() : base() { }
//        public byte bet_betpot_index;
//    }

//    //-----------------------------------------------------------------------------
//    // 百人桌重复下注
//    public class EvDesktopHRepeatBet : EntityEvent
//    {
//        public EvDesktopHRepeatBet() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // 百人桌坐下
//    public class EvUiDesktopHSeatDown : EntityEvent
//    {
//        public EvUiDesktopHSeatDown() : base() { }
//        public byte seat_index;
//        public long min_golds;
//    }

//    //-----------------------------------------------------------------------------
//    // 百人桌离座
//    public class EvUiDesktopHStandUp : EntityEvent
//    {
//        public EvUiDesktopHStandUp() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // 百人桌更改牌型
//    public class EvDesktopHundredChangeCardsType : EntityEvent
//    {
//        public EvDesktopHundredChangeCardsType() : base() { }
//        public Dictionary<byte, byte> map_card_types;
//    }

//    //-----------------------------------------------------------------------------
//    // 初始化下注奖励
//    public class EvDesktopHInitBetReward : EntityEvent
//    {
//        public EvDesktopHInitBetReward() : base() { }
//        public string factory_name;
//    }

//    //-----------------------------------------------------------------------------
//    // 获取下注奖励
//    public class EvDesktopHGetBetReward : EntityEvent
//    {
//        public EvDesktopHGetBetReward() : base() { }
//        public string factory_name;
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，点击时时彩
//    public class EvUiClickLotteryTicket : EntityEvent
//    {
//        public EvUiClickLotteryTicket() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // 点击时时彩奖池
//    public class EvLotteryTicketClickRewardPotBtn : EntityEvent
//    {
//        public EvLotteryTicketClickRewardPotBtn() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // 时时彩更改下注大小
//    public class EvLotteryTicketClickBetOperateType : EntityEvent
//    {
//        public EvLotteryTicketClickBetOperateType() : base() { }
//        public int tb_bet_operateid;
//    }

//    //-----------------------------------------------------------------------------
//    // 百人桌下注
//    public class EvLotteryTicketBet : EntityEvent
//    {
//        public EvLotteryTicketBet() : base() { }
//        public byte bet_betpot_index;
//    }

//    //-----------------------------------------------------------------------------
//    // 百人桌重复下注
//    public class EvLotteryTicketRepeatBet : EntityEvent
//    {
//        public EvLotteryTicketRepeatBet() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // Ui消息，离开时时彩
//    public class EvUiClickLeaveLotteryTicket : EntityEvent
//    {
//        public EvUiClickLeaveLotteryTicket() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    public class EvRequestGetPlayerModuleData : EntityEvent
//    {
//        public EvRequestGetPlayerModuleData() : base() { }
//        public string factory_name;
//    }

//    //-----------------------------------------------------------------------------
//    public class EvRequestGetOnLineReward : EntityEvent
//    {
//        public EvRequestGetOnLineReward() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    public class EvOnGetOnLineReward : EntityEvent
//    {
//        public EvOnGetOnLineReward() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    public class EvRequestSendMarquee : EntityEvent
//    {
//        public EvRequestSendMarquee() : base() { }
//        public string msg;
//    }

//    //-----------------------------------------------------------------------------
//    // 请求获取兑换码物品
//    public class EvRequesetGetExchangeCodeItem : EntityEvent
//    {
//        public EvRequesetGetExchangeCodeItem() : base() { }
//        public string acc;
//        public string pwd;
//    }

//    //-----------------------------------------------------------------------------
//    public class EvUiRequestMailRead : EntityEvent
//    {
//        public EvUiRequestMailRead() : base() { }
//        public string mail_guid;
//    }

//    //-----------------------------------------------------------------------------
//    public class EvUiRequestMailRecvAttachment : EntityEvent
//    {
//        public EvUiRequestMailRecvAttachment() : base() { }
//        public string mail_guid;
//    }

//    //-----------------------------------------------------------------------------
//    public class EvUiRequestGetActivity : EntityEvent
//    {
//        public EvUiRequestGetActivity() : base() { }
//    }

//    //-------------------------------------------------------------------------
//    public class EvUiChangeLan : EntityEvent
//    {
//        public EvUiChangeLan() : base() { }
//        public string lan;
//    }

//    //-----------------------------------------------------------------------------
//    // 请求进入森林舞会桌子
//    public class EvUiForestPartyEnterDesktop : EntityEvent
//    {
//        public EvUiForestPartyEnterDesktop() : base() { }
//        public int RoomTbId;
//    }

//    //-----------------------------------------------------------------------------
//    // 请求离开森林舞会桌子
//    public class EvUiRequestLeaveForestParty : EntityEvent
//    {
//        public EvUiRequestLeaveForestParty() : base() { }
//    }


//    //-----------------------------------------------------------------------------
//    // 请求改变下注选项
//    public class EvUiForestPartyChangeBetOperate : EntityEvent
//    {
//        public EvUiForestPartyChangeBetOperate() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // 请求下注
//    public class EvUiForestPartyBet : EntityEvent
//    {
//        public EvUiForestPartyBet() : base() { }
//        public byte BetIndex;
//    }

//    //-----------------------------------------------------------------------------
//    // 请求改变自动下注状态
//    public class EvUiForestPartyChangeBetRepeatState : EntityEvent
//    {
//        public EvUiForestPartyChangeBetRepeatState() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // 请求重复下注
//    public class EvUiForestPartyBetRepeat : EntityEvent
//    {
//        public EvUiForestPartyBetRepeat() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // 请求上庄
//    public class EvUiForestPartyBeBanker : EntityEvent
//    {
//        public EvUiForestPartyBeBanker() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    // 请求下庄
//    public class EvUiForestPartyNotBeBanker : EntityEvent
//    {
//        public EvUiForestPartyNotBeBanker() : base() { }
//    }

//    //-------------------------------------------------------------------------
//    public class EvUiRequestChangeDesk : EntityEvent
//    {
//        public EvUiRequestChangeDesk() : base() { }
//    }

//    //-----------------------------------------------------------------------------
//    public class EvUiUnChooseCreateDesktopEnterLimit : EntityEvent
//    {
//        public EvUiUnChooseCreateDesktopEnterLimit() : base() { }
//    }
//}