//
//  BCRefundStateResp.h
//  BCPaySDK
//
//  Created by Ewenlong03 on 15/7/27.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "BCBaseResp.h"


/**
 *  查询退款订单状态的响应
 */
@interface BCRefundStatusResp : BCBaseResp
/**
 *  退款的状态
 *  SUCCESS     退款成功
 *  FAIL        退款失败
 *  PROCESSING  退款进行中
 *  NOTSURE     未确定，需要商户原退款单号重新发起
 *  CHANGE      转入代发，退款到银行发现用户的卡作废或者冻结了，导致原路退款银行卡失败，资金回流到商户的现金帐号，需要商户人工干预，通过线下或者财付通转账的方式进行退款。
 */
@property (nonatomic, retain) NSString *refundStatus;

@end
