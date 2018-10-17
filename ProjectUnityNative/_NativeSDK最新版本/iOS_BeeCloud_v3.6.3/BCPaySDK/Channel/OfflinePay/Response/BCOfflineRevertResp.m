//
//  BCOfflineRevertResp.m
//  BCPay
//
//  Created by Ewenlong03 on 15/9/17.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BCOfflineRevertResp.h"

@implementation BCOfflineRevertResp

- (instancetype)initWithReq:(BCBaseReq *)request {
    self = [super init];
    if (self) {
        self.type = BCObjsTypeOfflineRevertResp;
        self.revertStatus = NO;
        self.request = request;
    }
    return self;
}

@end
