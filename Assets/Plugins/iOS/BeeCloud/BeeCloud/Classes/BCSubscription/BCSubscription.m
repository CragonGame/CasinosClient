//
//  BCSubscription.m
//  BCPay
//
//  Created by Ewenlong03 on 16/8/4.
//  Copyright © 2016年 BeeCloud. All rights reserved.
//

#import "BCSubscription.h"


@implementation BCSubscription

+ (instancetype)sharedInstance {
    static dispatch_once_t onceToken;
    static BCSubscription *instance = nil;
    dispatch_once(&onceToken, ^{
        instance = [[BCSubscription alloc] init];
    });
    return instance;
}

+ (void)setSubDelegate:(id<BCSubscriptionDelegate>)delegate {
    [BCSubscription sharedInstance].delegate = delegate;
}

+ (void)smsReq:(NSString *)phone {
    if (phone.isValidMobile) {
        NSMutableDictionary *params = [BCPayUtil prepareParametersForRequest];
        BCHTTPSessionManager *manager = [BCPayUtil getBCHTTPSessionManager];
        
        params[@"phone"] = phone;
        [manager POST:[NSString stringWithFormat:@"%@/sms",subscription_host] parameters:params progress:nil success:^(NSURLSessionDataTask * _Nonnull task, id  _Nullable responseObject) {
            NSMutableDictionary *response = [NSMutableDictionary dictionaryWithDictionary:(NSDictionary *)responseObject];
            response[@"type"] = @(BCSubTypeSMS);
            [BCSubscription doSubscriptionResponse:response];
        } failure:^(NSURLSessionDataTask * _Nullable task, NSError * _Nonnull error) {
            [BCSubscription doSubscriptionErrorResponse:kNetWorkError];
        }];
    }
}

+ (void)subscriptionBanks {
    
    NSMutableDictionary *params = [BCPayUtil prepareParametersForRequest];
    BCHTTPSessionManager *manager = [BCPayUtil getBCHTTPSessionManager];
    
    [manager GET:[NSString stringWithFormat:@"%@/subscription_banks", subscription_host] parameters:params progress:nil success:^(NSURLSessionDataTask * _Nonnull task, id  _Nullable responseObject) {
        NSMutableDictionary *response = [NSMutableDictionary dictionaryWithDictionary:(NSDictionary *)responseObject];
        response[@"type"] = @(BCSubTypeBanks);
        [BCSubscription doSubscriptionResponse:response];
    } failure:^(NSURLSessionDataTask * _Nullable task, NSError * _Nonnull error) {
        [BCSubscription doSubscriptionErrorResponse:kNetWorkError];
    }];
}

+ (void)subscriptionCancel:(NSString *)sub_id {
    NSMutableDictionary *params = [BCPayUtil prepareParametersForRequest];
    BCHTTPSessionManager *manager = [BCPayUtil getBCHTTPSessionManager];
    
    [manager DELETE:[NSString stringWithFormat:@"%@/subscription/%@", subscription_host,sub_id] parameters:params success:^(NSURLSessionDataTask * _Nonnull task, id  _Nullable responseObject) {
        NSMutableDictionary *response = [NSMutableDictionary dictionaryWithDictionary:(NSDictionary *)responseObject];
        response[@"type"] = @(BCSubTypeSubscriptionCancel);
        [BCSubscription doSubscriptionResponse:response];
    } failure:^(NSURLSessionDataTask * _Nullable task, NSError * _Nonnull error) {
        [BCSubscription doSubscriptionErrorResponse:kNetWorkError];
    }];
}

+ (void)doSubscriptionErrorResponse:(NSString *)errMsg {
    NSMutableDictionary *resp = [NSMutableDictionary dictionaryWithCapacity:10];
    resp[@"resultCode"] = @(BCErrCodeCommon);
    resp[@"resultMsg"] = errMsg;
    resp[@"errDetail"] = errMsg;
    
    [BCSubscription doSubscriptionResponse:resp];
}

+ (void)doSubscriptionResponse:(NSMutableDictionary *)response {
    
    BCSubscription *shared = [BCSubscription sharedInstance];
    
    if (shared.delegate && [shared.delegate respondsToSelector:@selector(onBCSubscriptionResp:)]) {
        [shared.delegate onBCSubscriptionResp:response];
    }
}


@end
