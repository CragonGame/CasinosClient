//
//  BCQueryRefundsCount.h
//  BCPay
//
//  Created by Ewenlong03 on 15/11/26.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BCBaseReq.h"
#import "BCQueryRefundsCountResp.h"

@interface BCQueryRefundsCountReq : BCBaseReq
/**
 *  支付渠道(具体支持渠道请参考Enum PayChannel)
 */
@property (nonatomic, assign) PayChannel channel;
/**
 *  发起支付时商家自定义的订单号
 */
@property (nonatomic, retain) NSString *billNo;
/**
 *  订单创建时间,@"yyyyMMddHHmm"格式; 例如2015年11月17日 00:00,@"201511170000"
 */
@property (nonatomic, assign) NSString *startTime;
/**
 *  订单创建时间,@"yyyyMMddHHmm"格式; 例如2015年11月17日 12:00,@"201511171200"
 */
@property (nonatomic, assign) NSString *endTime;
/**
 *  发起退款时商户自定义的退款单号
 */
@property (nonatomic, retain) NSString *refundNo;
/**
 *  根据是否是预退款状态查询
 */
@property (nonatomic, assign) NeedApproval needApproved;

#pragma mark - Functions 

/**
 *  查询满足条件的退款订单数目
 */
- (void)queryRefundsCountReq;

/**
 *  解析从BeeCloud服务返回的数据
 *
 *  @param response 从BeeCloud服务返回的数据
 *
 *  @return 查询结果
 */
- (BCQueryRefundsCountResp *)doQueryRefundsCountResponse:(NSDictionary *)response;

@end
