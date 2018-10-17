//
//  BCPayPalAccessTokenReq.m
//  BCPay
//
//  Created by Ewenlong03 on 15/8/28.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "BCPayPalVerifyReq.h"

@implementation BCPayPalVerifyReq

- (instancetype)init {
    self = [super init];
    if (self) {
        self.type = BCObjsTypePayPalVerify;
        self.payment = nil;
        self.optional = nil;
    }
    return self;
}

@end
