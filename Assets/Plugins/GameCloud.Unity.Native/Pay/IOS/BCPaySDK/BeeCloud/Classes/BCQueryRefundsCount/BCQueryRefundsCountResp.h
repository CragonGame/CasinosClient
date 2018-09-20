//
//  BCQueryRefundsCountResp.h
//  BCPay
//
//  Created by Ewenlong03 on 15/11/26.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BCBaseResp.h"

@interface BCQueryRefundsCountResp : BCBaseResp

/**
 *  满足条件的退款订单总数
 */
@property (nonatomic, assign) NSUInteger count;

@end
