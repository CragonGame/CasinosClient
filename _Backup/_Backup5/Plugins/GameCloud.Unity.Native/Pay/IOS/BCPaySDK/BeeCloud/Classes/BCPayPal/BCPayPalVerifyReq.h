//
//  BCPayPalAccessTokenReq.h
//  BCPay
//
//  Created by Ewenlong03 on 15/8/28.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "BCBaseReq.h"

#pragma mark BCPayPalVerifyReq

@interface BCPayPalVerifyReq : BCBaseReq

/**
 *  支付实例
 */
@property (nonatomic, strong) id payment;
/**
 *  扩展参数,可以传入任意数量的key/value对来补充对业务逻辑的需求
 */
@property (nonatomic, strong) NSDictionary *optional;

@end
