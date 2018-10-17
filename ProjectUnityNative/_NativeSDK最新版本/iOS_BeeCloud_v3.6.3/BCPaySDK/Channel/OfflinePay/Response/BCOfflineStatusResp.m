//
//  BCOfflineStatusResp.m
//  BCPay
//
//  Created by Ewenlong03 on 15/9/17.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BCOfflineStatusResp.h"

@implementation BCOfflineStatusResp

- (instancetype)initWithReq:(BCBaseReq *)request {
    self = [super init];
    if (self) {
        self.type = BCObjsTypeOfflineBillStatusResp;
        self.payResult = NO;
        self.request = request;
    }
    return self;
}

@end
