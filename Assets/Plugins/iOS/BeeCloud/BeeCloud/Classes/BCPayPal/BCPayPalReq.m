//
//  BCPayPalReq.m
//  BCPay
//
//  Created by Ewenlong03 on 15/8/27.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "BCPayPalReq.h"

@implementation BCPayPalReq

- (instancetype)init {
    self = [super init];
    if (self) {
        self.type = BCObjsTypePayPal;
        self.items = nil;
        self.shipping = @"0";
        self.tax = @"0";
        self.payConfig = nil;
        self.viewController = nil;
    }
    return self;
}

@end
