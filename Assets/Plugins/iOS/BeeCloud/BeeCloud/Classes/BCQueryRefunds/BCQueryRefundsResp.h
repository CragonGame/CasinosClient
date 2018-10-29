//
//  BCQueryRefundsResp.h
//  BCPay
//
//  Created by Ewenlong03 on 15/11/26.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BCBaseResp.h"

#pragma mark BCQueryRefundsResp
/**
 *  查询订单的响应，包括支付、退款订单
 */
@interface BCQueryRefundsResp : BCBaseResp
/**
 *  查询到得结果数量
 */
@property (nonatomic, assign) NSInteger count;
/**
 *  每个节点是BCQueryRefundResult类型的实例
 */
@property (nonatomic, retain) NSMutableArray *results;

@end
