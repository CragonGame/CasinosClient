//
//  BCPreRefundResp.m
//  BCPay
//
//  Created by Ewenlong03 on 16/6/22.
//  Copyright © 2016年 BeeCloud. All rights reserved.
//

#import "BCPreRefundResp.h"
#import "BCPreRefundReq.h"

@implementation BCPreRefundResp

- (instancetype)initWithReq:(BCPreRefundReq *)request {
    self = [super initWithReq:request];
    if (self) {
        self.type = BCObjsTypePreRefundResp;
    }
    return self;
}

@end
