//
//  BCOfflinePayResp.m
//  BCPay
//
//  Created by Ewenlong03 on 15/9/16.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "BCOfflinePayResp.h"

@implementation BCOfflinePayResp

- (instancetype)initWithReq:(BCBaseReq *)request {
    self = [super init];
    if (self) {
        self.type = BCObjsTypeOfflinePayResp;
        self.request = request;
    }
    return self;
}

@end
