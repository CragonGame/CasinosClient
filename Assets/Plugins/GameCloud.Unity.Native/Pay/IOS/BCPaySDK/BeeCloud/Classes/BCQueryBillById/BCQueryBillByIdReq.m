//
//  BCQueryBillByIdReq.m
//  BCPay
//
//  Created by Ewenlong03 on 15/11/25.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BCQueryBillByIdReq.h"

#import "BCPayUtil.h"

@implementation BCQueryBillByIdReq

- (instancetype)init {
    self = [super init];
    if (self) {
        self.type = BCObjsTypeQueryBillByIdReq;
        self.objectId = @"";
    }
    return self;
}

- (instancetype)initWithObjectId:(NSString *)objectId {
    BCQueryBillByIdReq *req = [[BCQueryBillByIdReq alloc] init];
    req.objectId = objectId;
    return req;
}

#pragma mark - QueryBillById

- (void)queryBillByIdReq {
    [BCPayCache sharedInstance].bcResp = [[BCQueryBillByIdResp alloc] initWithReq:self];
    
    if (!self.objectId.isValidUUID) {
        [BCPayUtil doErrorResponse:@"objectId 不合法"];
        return;
    }
    NSMutableDictionary *parameters = [BCPayUtil prepareParametersForRequest];
    if (parameters == nil) {
        [BCPayUtil doErrorResponse:@"请检查是否全局初始化"];
        return;
    }
    NSMutableDictionary *preparepara = [BCPayUtil getWrappedParametersForGetRequest:parameters];
    
    NSString *preHost = [BCPayUtil getBestHostWithFormat:kRestApiQueryBillById];
    NSString *host = [NSString stringWithFormat:@"%@%@", preHost, self.objectId];
    
    BCHTTPSessionManager *manager = [BCPayUtil getBCHTTPSessionManager];
    __weak BCQueryBillByIdReq *weakSelf = self;
    [manager GET:host parameters:preparepara progress:nil
         success:^(NSURLSessionTask *task, id response) {
             [weakSelf doQueryBillByIdResponse:(NSDictionary *)response];
         } failure:^(NSURLSessionTask *operation, NSError *error) {
             [BCPayUtil doErrorResponse:kNetWorkError];
         }];
}

- (BCQueryBillByIdResp *)doQueryBillByIdResponse:(NSDictionary *)response {
    if (response) {
        BCQueryBillByIdResp *resp = (BCQueryBillByIdResp *)[BCPayCache sharedInstance].bcResp;
        resp.resultCode = [response integerValueForKey:kKeyResponseResultCode defaultValue:BCErrCodeCommon];
        resp.resultMsg = [response stringValueForKey:kKeyResponseResultMsg defaultValue:kUnknownError];
        resp.errDetail = [response stringValueForKey:kKeyResponseErrDetail defaultValue:kUnknownError];
        NSDictionary *bill = [response dictValueForKey:@"pay" defaultValue:nil];
        if (bill) {
            resp.bill = [[BCQueryBillResult alloc] initWithResult:bill];
        }
        [BCPayCache beeCloudDoResponse];
        return resp;
    }
    return nil;
}

@end
