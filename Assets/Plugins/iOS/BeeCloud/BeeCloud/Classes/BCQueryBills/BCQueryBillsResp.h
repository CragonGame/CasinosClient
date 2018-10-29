//
//  BCQueryBillsResp.h
//  BCPaySDK
//
//  Created by Ewenlong03 on 15/7/27.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "BCBaseResp.h"

#pragma mark BCQueryBillsResp
/**
 *  查询订单的响应，包括支付、退款订单
 */
@interface BCQueryBillsResp : BCBaseResp
/**
 *  查询到得结果数量
 */
@property (nonatomic, assign) NSUInteger count;
/**
 *  每个节点是BCQueryBillResult类型的实例
 */
@property (nonatomic, retain) NSMutableArray *results;

@end
