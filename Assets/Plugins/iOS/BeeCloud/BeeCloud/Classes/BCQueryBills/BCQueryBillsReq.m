//
//  BCQueryBillsReq.m
//  BCPaySDK
//
//  Created by Ewenlong03 on 15/7/27.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "BCQueryBillsReq.h"
#import "BCPayUtil.h"

#pragma mark query request
@implementation BCQueryBillsReq

- (instancetype)init {
    self = [super init];
    if (self) {
        self.type = BCObjsTypeQueryBillsReq;
        self.skip = 0;
        self.limit = 10;
        self.startTime = @"";
        self.endTime = @"";
        self.billNo = @"";
        self.billStatus = BillStatusAll;
        self.needMsgDetail = NO;
    }
    return self;
}

#pragma mark Query Bills

- (void)queryBillsReq {
    [BCPayCache sharedInstance].bcResp = [[BCQueryBillsResp alloc] initWithReq:self];
    
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
    parameters[@"need_detail"] = @(self.needMsgDetail);
    if (cType.isValid) {
        parameters[@"channel"] = cType;
    }
    parameters[@"skip"] = [NSNumber numberWithInteger:self.skip];
    parameters[@"limit"] =  self.limit > 50 ? @50 : [NSNumber numberWithInteger:self.limit];
    
    NSMutableDictionary *preparepara = [BCPayUtil getWrappedParametersForGetRequest:parameters];
    
    BCHTTPSessionManager *manager = [BCPayUtil getBCHTTPSessionManager];
    __weak BCQueryBillsReq *weakSelf = self;
    [manager GET:[BCPayUtil getBestHostWithFormat: kRestApiQueryBills] parameters:preparepara progress:nil
         success:^(NSURLSessionTask *task, id response) {
             BCPayLog(@"resp = %@", response);
             [weakSelf doQueryBillsResponse:(NSDictionary *)response];
         } failure:^(NSURLSessionTask *operation, NSError *error) {
             [BCPayUtil doErrorResponse:kNetWorkError];
         }];
}

- (BCQueryBillsResp *)doQueryBillsResponse:(NSDictionary *)response {
    BCQueryBillsResp *resp = (BCQueryBillsResp *)[BCPayCache sharedInstance].bcResp;
    resp.resultCode = [response integerValueForKey:kKeyResponseResultCode defaultValue:BCErrCodeCommon];
    resp.resultMsg = [response stringValueForKey:kKeyResponseResultMsg defaultValue:kUnknownError];
    resp.errDetail = [response stringValueForKey:kKeyResponseErrDetail defaultValue:kUnknownError];
    resp.results = [self parseBillsResults:response];
    resp.count = resp.results.count;
    [BCPayCache beeCloudDoResponse];
    return resp;
}

- (NSMutableArray *)parseBillsResults:(NSDictionary *)dic {
    NSMutableArray *array = [NSMutableArray arrayWithCapacity:10];
    for (NSDictionary *result in [dic arrayValueForKey:@"bills" defaultValue:nil]) {
        BCQueryBillResult *bill = [[BCQueryBillResult alloc] initWithResult:result];
        if (bill) {
            [array addObject:bill];
        }
    }
    return array;
}

@end
