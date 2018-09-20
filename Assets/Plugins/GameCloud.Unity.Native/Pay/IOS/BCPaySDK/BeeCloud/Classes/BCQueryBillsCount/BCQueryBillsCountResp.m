//
//  BCQueryBillsCountResp.m
//  BCPay
//
//  Created by Ewenlong03 on 15/11/26.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BCQueryBillsCountResp.h"
#import "BCQueryBillsCountReq.h"

@implementation BCQueryBillsCountResp

- (instancetype)initWithReq:(BCQueryBillsCountReq *)request {
    self = [super initWithReq:request];
    if (self) {
        self.type = BCObjsTypeQueryBillsCountResp;
        self.count = 0;
    }
    return self;
}

@end
