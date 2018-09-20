//
//  BCQueryRefundsResp.m
//  BCPay
//
//  Created by Ewenlong03 on 15/11/26.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BCQueryRefundsResp.h"
#import "BCQueryRefundsReq.h"

@implementation BCQueryRefundsResp

- (instancetype)initWithReq:(BCQueryRefundsReq *)request {
    self = [super initWithReq:request];
    if (self) {
        self.type = BCObjsTypeQueryRefundsResp;
    }
    return self;
}

@end
