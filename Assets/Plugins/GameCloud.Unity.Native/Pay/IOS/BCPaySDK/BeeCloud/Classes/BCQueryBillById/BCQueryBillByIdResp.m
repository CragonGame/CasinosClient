//
//  BCQueryBillByIdResp.m
//  BCPay
//
//  Created by Ewenlong03 on 15/11/26.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BCQueryBillByIdResp.h"
#import "BCQueryBillByIdReq.h"

@implementation BCQueryBillByIdResp

- (instancetype)initWithReq:(BCQueryBillByIdReq *)request {
    self = [super initWithReq:request];
    if (self) {
        self.type = BCObjsTypeQueryBillByIdResp;
        self.bill = nil;
    }
    return self;
}

@end
