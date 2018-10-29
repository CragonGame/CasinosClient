//
//  BCQueryRefundsCountResp.m
//  BCPay
//
//  Created by Ewenlong03 on 15/11/26.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BCQueryRefundsCountResp.h"
#import "BCQueryRefundsCountReq.h"

@implementation BCQueryRefundsCountResp

- (instancetype)initWithReq:(BCQueryRefundsCountReq *)request {
    self = [super initWithReq:request];
    if (self) {
        self.type = BCObjsTypeQueryRefundsCountResp;
        self.count = 0;
    }
    return self;
}

@end
