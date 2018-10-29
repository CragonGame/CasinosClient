//
//  BCQRefundReq.h
//  BCPaySDK
//
//  Created by Ewenlong03 on 15/7/27.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "BCQueryRefundsReq.h"
#import "BCQueryRefundsResp.h"

#pragma mark BCQueryRefundsReq
/**
 *  根据条件查询退款记录
 */
@interface BCQueryRefundsReq : BCBaseReq //type=103;
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
 *  根据是否是预退款状态查询
 */
@property (nonatomic, assign) NSUInteger needApproved;
/**
 *  是否需要退款成功后渠道异步通知的详细信息，YES代表需要返回。注：只有退款成功的订单，才会有messageDetail
 */
@property (nonatomic, assign) BOOL needMsgDetail;
/**
 *  跳过skip条数据，从第(skip+1)条开始查询。默认为0
 */
@property (nonatomic, assign) NSInteger skip;
/**
 *  查询多少条订单记录;最大不超过50条,大于50的,计为50;默认为10
 */
@property (nonatomic, assign) NSInteger limit;
/**
 *  发起退款时商户自定义的退款单号
 */
@property (nonatomic, retain) NSString *refundNo;

#pragma mark - Functions 

/**
 *  发起查询退款
 */
- (void)queryRefundsReq;

/**
 *  解析从BeeCloud服务返回的数据
 *
 *  @param response 从BeeCloud服务返回的数据
 *
 *  @return 查询到的退款记录
 */
- (BCQueryRefundsResp *)doQueryRefundsResponse:(NSDictionary *)response;

/**
 *  本地化退款记录列表
 *
 *  @param dic 退款记录列表
 *
 *  @return 退款记录列表
 */
- (NSMutableArray *)parseRefundsResults:(NSDictionary *)dic;

@end
