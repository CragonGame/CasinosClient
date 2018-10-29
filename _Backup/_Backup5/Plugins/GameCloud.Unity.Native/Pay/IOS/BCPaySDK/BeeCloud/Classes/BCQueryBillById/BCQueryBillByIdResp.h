//
//  BCQueryBillByIdResp.h
//  BCPay
//
//  Created by Ewenlong03 on 15/11/26.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BCBaseResp.h"
#import "BCQueryBillResult.h"

@interface BCQueryBillByIdResp : BCBaseResp

/**
 *  支付订单记录，
 */
@property (nonatomic, retain) BCQueryBillResult *bill;

@end
