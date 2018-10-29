//
//  BCQueryRefundByIdResp.m
//  BCPay
//
//  Created by Ewenlong03 on 15/11/26.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BCQueryRefundByIdResp.h"
#import "BCQueryRefundByIdReq.h"

@implementation BCQueryRefundByIdResp

- (instancetype)initWithReq:(BCQueryRefundByIdReq *)request {
    self = [super initWithReq:request];
    if (self) {
        self.type = BCObjsTypeQueryRefundByIdResp;
        self.refund = nil;
    }
    return self;
}

@end
