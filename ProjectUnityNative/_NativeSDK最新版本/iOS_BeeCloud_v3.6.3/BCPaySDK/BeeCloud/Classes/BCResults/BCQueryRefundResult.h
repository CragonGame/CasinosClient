//
//  BCQRefundResult.h
//  BCPaySDK
//
//  Created by Ewenlong03 on 15/7/27.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "BCBaseResult.h"

#pragma mark BCQueryRefundResult

/**
 *  退款订单查询结果
 */
@interface BCQueryRefundResult : BCBaseResult
/**
 *  发起退款时商户自定义的退款单号
 */
@property (nonatomic, retain) NSString *refundNo;
/**
 *  退款金额，以分为单位的正整数
 */
@property (nonatomic, assign) NSInteger refundFee;
/**
 *  退款是否结束
 *  YES 表示本次退款操作已经完成，结果可能是退款成功，也可能退款失败;
 *  NO  表示退款操作还在进行中
 */
@property (nonatomic, assign) BOOL      finish;
/**
 *  退款结果
 *  YES 表示退款成功.资金的到账时间跟支付方式有关,如果余额支付，退款到账是即时的。如果是银行卡支付，需要等待银行处理。
 *  NO  表示退款失败
 */
@property (nonatomic, assign) BOOL      result;

@end
