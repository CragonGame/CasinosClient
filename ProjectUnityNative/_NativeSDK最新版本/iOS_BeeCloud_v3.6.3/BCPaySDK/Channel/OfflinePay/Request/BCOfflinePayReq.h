//
//  BCOfflinePayReq.h
//  BCPay
//
//  Created by Ewenlong03 on 15/9/16.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "BCBaseReq.h"

@interface BCOfflinePayReq : BCBaseReq //type = 105

/**
 *  支付渠道(WX_NATIVE,WX_SCAN,ALI_OFFLINE_QRCODE,ALI_SCAN)
 */
@property (nonatomic, assign) PayChannel channel;
/**
 *  订单描述,32个字节内,最长16个汉字
 */
@property (nonatomic, retain) NSString *title;
/**
 *  支付金额,以分为单位,必须为整数,100表示1元
 */
@property (nonatomic, retain) NSString *totalFee;
/**
 *  商户系统内部的订单号,8~32位数字和/或字母组合,确保在商户系统中唯一
 */
@property (nonatomic, retain) NSString *billNo;
#pragma mark channel=ALI_SCAN或WX_SCAN时authcode必填
/**
 *  当商户用扫码枪扫用户的条形码时得到的字符串
 */
@property (nonatomic, retain) NSString *authcode;
/**
 *  扩展参数,可以传入任意数量的key/value对来补充对业务逻辑的需求;此参数会在webhook回调中返回;
 */
@property (nonatomic, retain) NSMutableDictionary *optional;

#pragma mark channel=ALI_SCAN时需要根据实际情况传递以下两个
/**
 *  商户机具终端编号,最长32位.若机具商接入,terminalid(机具终端编号)必填,storeid(商户门店编号)选填
 */
@property (nonatomic, retain) NSString *terminalId;
/**
 *  商户门店编号,最长32位.若系统商接入,storeid(商户的门店编号)必填,terminalid(机具终端编号)选填
 */
@property (nonatomic, retain) NSString *storeId;


@end
