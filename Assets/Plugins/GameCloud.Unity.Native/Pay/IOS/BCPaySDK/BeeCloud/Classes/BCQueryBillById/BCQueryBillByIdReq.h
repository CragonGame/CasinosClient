//
//  BCQueryBillByIdReq.h
//  BCPay
//
//  Created by Ewenlong03 on 15/11/25.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BCBaseReq.h"
#import "BCQueryBillByIdResp.h"

@interface BCQueryBillByIdReq : BCBaseReq

/**
 *  支付订单记录id
 */
@property (nonatomic, retain) NSString *objectId;

#pragma mark - Functions

/**
 *  初始化
 *
 *  @param objectId 支付订单id
 *
 *  @return BCQueryBillByIdReq实例
 */
- (instancetype)initWithObjectId:(NSString *)objectId;

/**
 *  发起根据objectId查询支付订单请求
 */
- (void)queryBillByIdReq;

/**
 *  解析并返回支付订单数据
 *
 *  @param response BeeCloud服务返回的查询结果
 *
 *  @return 查询结果
 */
- (BCQueryBillByIdResp *)doQueryBillByIdResponse:(NSDictionary *)response;

@end
