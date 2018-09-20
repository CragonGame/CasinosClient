//
//  BCPreRefundReq.h
//  BCPay
//
//  Created by Ewenlong03 on 16/6/22.
//  Copyright © 2016年 BeeCloud. All rights reserved.
//

#import "BCBaseReq.h"

@interface BCPreRefundReq : BCBaseReq
/**
 *  移动应用内正确的退款流程：
 *     用户在客户端发起预退款请求，
 *     商户在管理端审核预退款请求，满足条件即调用退款接口完成退款；
 */

/**
 *  退款渠道
 */
@property (nonatomic, assign) PayChannel channel;
/**
 *  退款订单号，格式为:退款日期(8位) + 流水号(3~24 位)。请自行确保在商户系统中唯一，且退款日期必须是发起退款的当天日期,同一退款单号不可重复提交，否则会造成退款单重复。
 *  流水号可以接受数字或英文字符，建议使用数字，但不可接受“000”
 */
@property (nonatomic, retain) NSString *refundNo;
/**
 *  需要退款的商户订单号，同一笔订单，支持多次退款。
 */
@property (nonatomic, retain) NSString *billNo;
/**
 *  退款金额，以分为单位，不能大于支付金额，
 */
@property (nonatomic, assign) NSString *refundFee;
/**
 *  扩展参数，用于补充商户的业务逻辑，会在退款成功的异步通知中返回此参数
 */
@property (nonatomic, retain) NSMutableDictionary *optional;
/**
 *  发起预退款请求
 */
- (void)preRefundReq;
/**
 *  检查预退款请求参数
 *
 *  @return 满足条件返回YES
 */
- (BOOL)checkParametersForPreRefund;
/**
 *  检查退款单号
 *
 *  @return 符合格式要求返回YES
 */
- (BOOL)checkRefundNo;

@end
