//
//  BCPayResp.m
//  BCPaySDK
//
//  Created by Ewenlong03 on 15/7/27.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "BCPayResp.h"

#pragma mark pay response
@implementation BCPayResp

- (instancetype)initWithReq:(BCPayReq *)request {
    self = [super initWithReq:request];
    if (self) {
        self.type = BCObjsTypePayResp;
    }
    return self;
}

@end
