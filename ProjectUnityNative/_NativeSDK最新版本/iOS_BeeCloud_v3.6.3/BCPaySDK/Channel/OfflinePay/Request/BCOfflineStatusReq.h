//
//  BCOfflineStatusReq.h
//  BCPay
//
//  Created by Ewenlong03 on 15/9/17.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BCBaseReq.h"

@interface BCOfflineStatusReq : BCBaseReq
/**
 *  发起支付时商户自定义的订单号
 */
@property (nonatomic, retain) NSString *billNo;
/**
 *  支付渠道(WX_NATIVE, WX_SCAN, ALI_OFFLINE_QRCODE, ALI_SCAN)
 */
@property (nonatomic, assign) PayChannel channel;

@end
