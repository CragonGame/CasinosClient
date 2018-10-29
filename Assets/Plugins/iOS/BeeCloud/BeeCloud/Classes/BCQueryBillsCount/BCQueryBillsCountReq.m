//
//  BCQueryBillsCount.m
//  BCPay
//
//  Created by Ewenlong03 on 15/11/26.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BCQueryBillsCountReq.h"
#import "BCPayUtil.h"

@implementation BCQueryBillsCountReq

- (instancetype)init {
    self = [super init];
    if (self) {
        self.type = BCObjsTypeQueryBillsCountReq;
        self.billNo = @"";
        self.startTime = @"";
        self.endTime = @"";
        self.billStatus = BillStatusAll;
    }
    return self;
}

- (void)queryBillsCountReq {
    [BCPayCache sharedInstance].bcResp = [[BCQueryBillsCountResp alloc] initWithReq:self];
    
    NSString *cType = [BCPayUtil getChannelString:self.channel];
    
    NSMutableDictionary *parameters = [BCPayUtil prepareParametersForRequest];
    if (parameters == nil) {
        [BCPayUtil doErrorResponse:@"请检查是否全局初始化"];
        return;
    }
    
    if (self.billNo.isValid) {
        parameters[@"bill_no"] = self.billNo;
    }
    if (self.startTime.isValid) {
        parameters[@"start_time"] = [NSNumber numberWithLongLong:[BCPayUtil dateStringToMillisecond:self.startTime]];
    }
    if (self.endTime.isValid) {
        parameters[@"end_time"] = [NSNumber numberWithLongLong:[BCPayUtil dateStringToMillisecond:self.endTime]];
    }
    if (self.billStatus != BillStatusAll) {
        parameters[@"spay_result"] = self.billStatus == BillStatusOnlySuccess ? @YES : @NO;
    }
    if (cType.isValid) {
        parameters[@"channel"] = cType;
    }
    
    NSMutableDictionary *preparepara = [BCPayUtil getWrappedParametersForGetRequest:parameters];
    
    BCHTTPSessionManager *manager = [BCPayUtil getBCHTTPSessionManager];
    __weak BCQueryBillsCountReq *weakSelf = self;
    [manager GET:[BCPayUtil getBestHostWithFormat:kRestApiQueryBillsCount] parameters:preparepara progress:nil
         success:^(NSURLSessionTask *task, id response) {
             BCPayLog(@"resp = %@", response);
             [weakSelf doQueryBillsCountResponse:(NSDictionary *)response];
         } failure:^(NSURLSessionTask *operation, NSError *error) {
             [BCPayUtil doErrorResponse:kNetWorkError];
         }];
}

- (BCQueryBillsCountResp *)doQueryBillsCountResponse:(NSDictionary *)response {
    BCQueryBillsCountResp *resp = (BCQueryBillsCountResp *)[BCPayCache sharedInstance].bcResp;
    resp.resultCode = [response integerValueForKey:kKeyResponseResultCode defaultValue:BCErrCodeCommon];
    resp.resultMsg = [response stringValueForKey:kKeyResponseResultMsg defaultValue:kUnknownError];
    resp.errDetail = [response stringValueForKey:kKeyResponseErrDetail defaultValue:kUnknownError];
    resp.count = [response integerValueForKey:@"count" defaultValue:0];
    [BCPayCache beeCloudDoResponse];
    return resp;
}

@end
