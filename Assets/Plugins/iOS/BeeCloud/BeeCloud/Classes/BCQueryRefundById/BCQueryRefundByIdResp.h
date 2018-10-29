//
//  BCQueryRefundByIdResp.h
//  BCPay
//
//  Created by Ewenlong03 on 15/11/26.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BCBaseResp.h"
#import "BCQueryRefundResult.h"

@interface BCQueryRefundByIdResp : BCBaseResp

/**
 *  退款订单记录
 */
@property (nonatomic, retain) BCQueryRefundResult *refund;

@end
