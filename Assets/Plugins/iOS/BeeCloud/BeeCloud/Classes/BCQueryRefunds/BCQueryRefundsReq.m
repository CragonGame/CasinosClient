//
//  BCQRefundReq.m
//  BCPaySDK
//
//  Created by Ewenlong03 on 15/7/27.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "BCQueryRefundsReq.h"

#import "BCPayUtil.h"

#pragma mark query refund request
@implementation BCQueryRefundsReq

- (instancetype)init {
    self = [super init];
    if (self) {
        self.type = BCObjsTypeQueryRefundsReq;
        self.refundNo = @"";
        self.needApproved = NeedApprovalAll;
        self.needMsgDetail = NO;
    }
    return self;
}

#pragma mark - QueryRefunds

- (void)queryRefundsReq {
    [BCPayCache sharedInstance].bcResp = [[BCQueryRefundsResp alloc] initWithReq:self];
    
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
    parameters[@"need_detail"] = @(self.needMsgDetail);
    
    parameters[@"skip"] = [NSNumber numberWithInteger:self.skip];
    parameters[@"limit"] =  self.limit > 50 ? @50 : [NSNumber numberWithInteger:self.limit];
    
    NSMutableDictionary *preparepara = [BCPayUtil getWrappedParametersForGetRequest:parameters];
    
    BCHTTPSessionManager *manager = [BCPayUtil getBCHTTPSessionManager];
    __weak BCQueryRefundsReq *weakSelf = self;
    [manager GET:[BCPayUtil getBestHostWithFormat:kRestApiQueryRefunds] parameters:preparepara progress:nil
         success:^(NSURLSessionTask *operation, id response) {
             BCPayLog(@"resp = %@", response);
             [weakSelf doQueryRefundsResponse:(NSDictionary *)response];
         } failure:^(NSURLSessionTask *operation, NSError *error) {
             [BCPayUtil doErrorResponse:kNetWorkError];
         }];
}

- (BCQueryRefundsResp *)doQueryRefundsResponse:(NSDictionary *)response {
    BCQueryRefundsResp *resp = (BCQueryRefundsResp *)[BCPayCache sharedInstance].bcResp;
    resp.resultCode = [response integerValueForKey:kKeyResponseResultCode defaultValue:BCErrCodeCommon];
    resp.resultMsg = [response stringValueForKey:kKeyResponseResultMsg defaultValue:kUnknownError];
    resp.errDetail = [response stringValueForKey:kKeyResponseErrDetail defaultValue:kUnknownError];
    resp.results = [self parseRefundsResults:response];
    resp.count = resp.results.count;
    [BCPayCache beeCloudDoResponse];
    return resp;
}

- (NSMutableArray *)parseRefundsResults:(NSDictionary *)dic {
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:10];
    for (NSDictionary *result in [dic arrayValueForKey:@"refunds" defaultValue:nil]) {
        BCQueryRefundResult *refund = [[BCQueryRefundResult alloc] initWithResult:result];
        if (refund) {
            [array addObject:refund];
        }
    } ;
    return array;
}

@end
