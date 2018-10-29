//
//  BCQueryResp.m
//  BCPaySDK
//
//  Created by Ewenlong03 on 15/7/27.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "BCQueryBillsResp.h"
#import "BCQueryBillsReq.h"

@implementation BCQueryBillsResp

- (instancetype)initWithReq:(BCQueryBillsReq *)request {
    self = [super initWithReq:request];
    if (self) {
        self.type = BCObjsTypeQueryBillsResp;
    }
    return self;
}
@end
