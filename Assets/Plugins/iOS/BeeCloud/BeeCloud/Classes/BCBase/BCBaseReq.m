//
//  BCBaseReq.m
//  BCPaySDK
//
//  Created by Ewenlong03 on 15/7/27.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "BCBaseReq.h"

#pragma makr base request
@implementation BCBaseReq

- (instancetype)init {
    self = [super init];
    if (self) {
        self.type = BCObjsTypeBaseReq;
    }
    return self;
}

@end
