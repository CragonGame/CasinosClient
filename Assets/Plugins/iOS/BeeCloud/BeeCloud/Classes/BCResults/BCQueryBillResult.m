//
//  BCQBillsResult.m
//  BCPaySDK
//
//  Created by Ewenlong03 on 15/7/27.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "BCQueryBillResult.h"

@implementation BCQueryBillResult

- (instancetype) initWithResult:(NSDictionary *)dic {
    self = [super initWithResult:dic];
    if (self) {
        self.type = BCObjsTypeBillResults;
        self.payResult = [dic boolValueForKey:@"spay_result" defaultValue:NO];
        self.tradeNo = [dic stringValueForKey:@"trade_no" defaultValue:@""];
        self.revertResult = [dic boolValueForKey:@"revert_result" defaultValue:NO];
        self.refundResult = [dic boolValueForKey:@"refund_result" defaultValue:NO];
    }
    return self;
}


@end
