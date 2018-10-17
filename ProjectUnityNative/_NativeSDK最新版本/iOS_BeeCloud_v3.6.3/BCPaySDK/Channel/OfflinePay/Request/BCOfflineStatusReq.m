//
//  BCOfflineStatusReq.m
//  BCPay
//
//  Created by Ewenlong03 on 15/9/17.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BCOfflineStatusReq.h"

@implementation BCOfflineStatusReq

- (instancetype)init {
    self = [super init];
    if (self) {
        self.type = BCObjsTypeOfflineBillStatusReq;
        self.billNo = @"";
    }
    return self;
}

@end
