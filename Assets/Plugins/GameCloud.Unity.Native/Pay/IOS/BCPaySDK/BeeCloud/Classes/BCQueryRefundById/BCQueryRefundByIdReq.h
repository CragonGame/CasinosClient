//
//  BCQueryRefundByIdReq.h
//  BCPay
//
//  Created by Ewenlong03 on 15/11/25.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BCBaseReq.h"
#import "BCQueryRefundByIdResp.h"

@interface BCQueryRefundByIdReq : BCBaseReq

/**
 *  退款订单记录id
 */
@property (nonatomic, retain) NSString *objectId;

#pragma mark - Functions

/**
 *  初始化
 *
 *  @param objectId 退款订单id
 *
 *  @return BCQueryRefundByIdReq实例
 */
- (instancetype)initWithObjectId:(NSString *)objectId;

/**
 *  发起根据objectId查询退款记录
 */
- (void)queryRefundByIdReq;

/**
 *  解析并返回退款订单数据
 *
 *  @param response BeeCloud服务返回的查询结果
 *
 *  @return 查询结果
 */
- (BCQueryRefundByIdResp *)doQueryRefundByIdResponse:(NSDictionary *)response;

@end
