//
//  BCQBillsResult.h
//  BCPaySDK
//
//  Created by Ewenlong03 on 15/7/27.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "BCBaseResult.h"

#pragma mark BCQueryBillResult

/**
 *  支付订单查询结果
 */
@interface BCQueryBillResult : BCBaseResult
/**
 *  订单支付结果;YES表示支付成功,NO表示支付失败
 */
@property (nonatomic, assign) BOOL payResult;
/**
 *  渠道交易号
 */
@property (nonatomic, retain) NSString *tradeNo;
/**
 *  订单撤销结果(WX_SCAN, ALI_SCAN, ALI_OFFLINE_QRCODE渠道是此参数有意义),YES表示撤销成功,NO表示撤销失败
 */
@property (nonatomic, assign) BOOL revertResult;
/**
 *  是否已经退款，YES代表已经退款
 */
@property (nonatomic, assign) BOOL refundResult;

@end
