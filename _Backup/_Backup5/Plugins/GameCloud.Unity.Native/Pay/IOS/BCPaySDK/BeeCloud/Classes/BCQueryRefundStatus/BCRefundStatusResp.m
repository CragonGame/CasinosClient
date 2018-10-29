//
//  BCRefundStateResp.m
//  BCPaySDK
//
//  Created by Ewenlong03 on 15/7/27.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "BCRefundStatusResp.h"
#import "BCRefundStatusReq.h"

@implementation BCRefundStatusResp

- (instancetype)initWithReq:(BCRefundStatusReq *)request {
    self = [super initWithReq:request];
    if (self) {
        self.type = BCObjsTypeRefundStatusResp;
    }
    return self;
}

@end
