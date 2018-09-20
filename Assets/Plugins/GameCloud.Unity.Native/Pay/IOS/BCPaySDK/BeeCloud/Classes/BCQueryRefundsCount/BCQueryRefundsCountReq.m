//
//  BCQueryRefundsCount.m
//  BCPay
//
//  Created by Ewenlong03 on 15/11/26.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BCQueryRefundsCountReq.h"
#import "BCPayUtil.h"

@implementation BCQueryRefundsCountReq

- (instancetype)init {
    self = [super init];
    if (self) {
        self.type = BCObjsTypeQueryRefundsCountReq;
        self.refundNo = @"";
        self.billNo = @"";
        self.startTime = @"";
        self.endTime = @"";
        self.needApproved = NeedApprovalAll;
    }
    return self;
}

- (void)queryRefundsCountReq {
    [BCPayCache sharedInstance].bcResp = [[BCQueryRefundsCountResp alloc] initWithReq:self];
    
    NSString *cType = [BCPayUtil getChannelString:self.channel];
    
    NSMutableDictionary *parameters = [BCPayUtil prepareParametersForRequest];
    if (parameters == nil) {
        [BCPayUtil doErrorResponse:@"请检查是否全局初始化"];
        return;
    }
    
    if (self.billNo.isValid) {
        parameters[@"bill_no"] = self.billNo;
    }
    if (self.refundNo.isValid) {
        parameters[@"refund_no"] = self.refundNo;
    }
    if (self.startTime.isValid) {
        parameters[@"start_time"] = [NSNumber numberWithLongLong:[BCPayUtil dateStringToMillisecond:self.startTime]];
    }
    if (self.endTime.isValid) {
        parameters[@"end_time"] = [NSNumber numberWithLongLong:[BCPayUtil dateStringToMillisecond:self.endTime]];
    }
    if (cType.isValid) {
        parameters[@"channel"] = cType;
    }
    if (self.needApproved != NeedApprovalAll) {
        parameters[@"need_approval"] = self.needApproved == NeedApprovalOnlyTrue ? @YES : @NO;
    }
    
    NSMutableDictionary *preparepara = [BCPayUtil getWrappedParametersForGetRequest:parameters];
    
    BCHTTPSessionManager *manager = [BCPayUtil getBCHTTPSessionManager];
    __weak BCQueryRefundsCountReq *weakSelf = self;
    [manager GET:[BCPayUtil getBestHostWithFormat:kRestApiQueryRefundsCount] parameters:preparepara progress:nil
         success:^(NSURLSessionTask *task, id response) {
             BCPayLog(@"resp = %@", response);
             [weakSelf doQueryRefundsCountResponse:(NSDictionary *)response];
         } failure:^(NSURLSessionTask *operation, NSError *error) {
             [BCPayUtil doErrorResponse:kNetWorkError];
         }];
}

- (BCQueryRefundsCountResp *)doQueryRefundsCountResponse:(NSDictionary *)response {
    BCQueryRefundsCountResp *resp = (BCQueryRefundsCountResp *)[BCPayCache sharedInstance].bcResp;
    resp.resultCode = [response integerValueForKey:kKeyResponseResultCode defaultValue:BCErrCodeCommon];
    resp.resultMsg = [response stringValueForKey:kKeyResponseResultMsg defaultValue:kUnknownError];
    resp.errDetail = [response stringValueForKey:kKeyResponseErrDetail defaultValue:kUnknownError];
    resp.count = [response integerValueForKey:@"count" defaultValue:0];
    [BCPayCache beeCloudDoResponse];
    return resp;
}

@end
