//
//  BCRefundStateReq.m
//  BCPaySDK
//
//  Created by Ewenlong03 on 15/7/27.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "BCRefundStatusReq.h"
#import "BCPayUtil.h"

@implementation BCRefundStatusReq

- (instancetype)init {
    self = [super init];
    if (self) {
        self.type = BCObjsTypeRefundStatusReq;
        self.refundNo = @"";
    }
    return self;
}

#pragma mark Refund Status For WeChat

- (void)refundStatusReq {
    
    [BCPayCache sharedInstance].bcResp = [[BCRefundStatusResp alloc] initWithReq:self];
    
    NSMutableDictionary *parameters = [BCPayUtil prepareParametersForRequest];
    if (parameters == nil) {
        [BCPayUtil doErrorResponse:@"请检查是否全局初始化"];
        return;
    }
    NSString *cType = [BCPayUtil getChannelString:self.channel];
    
    if (!self.refundNo.isValid || !cType.isValid) {
        [BCPayUtil doErrorResponse:@"请求参数不合法"];
        return;
    }
    parameters[@"refund_no"] = self.refundNo;
    parameters[@"channel"] = cType;
    
    NSMutableDictionary *preparepara = [BCPayUtil getWrappedParametersForGetRequest:parameters];
    
    BCHTTPSessionManager *manager = [BCPayUtil getBCHTTPSessionManager];
    __weak BCRefundStatusReq *weakSelf = self;
    
    [manager GET:[BCPayUtil getBestHostWithFormat:kRestApiRefundState] parameters:preparepara progress:nil
         success:^(NSURLSessionTask *task, id response) {
             [weakSelf doQueryRefundStatus:(NSDictionary *)response];
         } failure:^(NSURLSessionTask *operation, NSError *error) {
             [BCPayUtil doErrorResponse:kNetWorkError];
         }];
}

- (BCRefundStatusResp *)doQueryRefundStatus:(NSDictionary *)dic {
    BCRefundStatusResp *resp = (BCRefundStatusResp *)[BCPayCache sharedInstance].bcResp;
    resp.resultCode = [dic integerValueForKey:kKeyResponseResultCode defaultValue:BCErrCodeCommon];
    resp.resultMsg = [dic stringValueForKey:kKeyResponseResultMsg defaultValue:kUnknownError];
    resp.errDetail = [dic stringValueForKey:kKeyResponseErrDetail defaultValue:kUnknownError];
    resp.refundStatus = [dic stringValueForKey:@"refund_status" defaultValue:@""];
    [BCPayCache beeCloudDoResponse];
    return resp;
}

@end
