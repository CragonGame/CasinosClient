//
//  BCBaseResp.m
//  BCPaySDK
//
//  Created by Ewenlong03 on 15/7/27.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "BCBaseResp.h"

#pragma mark base response
@implementation BCBaseResp

- (instancetype)initWithReq:(BCBaseReq *)request {
    self = [super init];
    if (self) {
        self.type = BCObjsTypeBaseResp;
        self.request = request;
        self.resultCode = BCErrCodeCommon;
        self.resultMsg = @"";
        self.errDetail = @"";
    }
    return self;
}
@end
