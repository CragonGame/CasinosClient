//
//  BCOfflineRevertReq.m
//  BCPay
//
//  Created by Ewenlong03 on 15/9/17.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BCOfflineRevertReq.h"

@implementation BCOfflineRevertReq

- (instancetype)init {
    self = [super init];
    if (self) {
        self.type = BCObjsTypeOfflineRevertReq;
    }
    return self;
}

@end
