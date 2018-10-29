//
//  BCQRefundResult.m
//  BCPaySDK
//
//  Created by Ewenlong03 on 15/7/27.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "BCQueryRefundResult.h"

@implementation BCQueryRefundResult

- (instancetype) initWithResult:(NSDictionary *)dic {
    self = [super initWithResult:dic];
    if (self) {
        self.type = BCObjsTypeRefundResults;
        if (dic) {
            self.refundNo = [dic stringValueForKey:@"refund_no" defaultValue:@""];
            self.refundFee = [dic integerValueForKey:@"refund_fee" defaultValue:0];
            self.finish = [dic boolValueForKey:@"finish" defaultValue:NO];
            self.result = [dic boolValueForKey:@"result" defaultValue:NO];
        }
    }
    return self;
}

@end
