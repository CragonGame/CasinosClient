//
//  BCSubQueryPlan.m
//  BCPay
//
//  Created by Ewenlong03 on 16/8/8.
//  Copyright © 2016年 BeeCloud. All rights reserved.
//

#import "BCSubscriptionQuery.h"

@implementation BCSubscriptionQuery

- (instancetype)init {
    self = [super init];
    if (self) {
        self.created_before = 0;
        self.created_after = 0;
        self.skip = 0;
        self.limit = 10;
        self.count_only = NO;
    }
    return self;
}

- (void)getPlansCount:(NSString *)plan_name interval:(NSString *)interval interval_count:(NSInteger)interval_count trial_days:(NSInteger)trial_days {
    self.count_only = YES;
    [self getPlans:plan_name interval:interval interval_count:interval_count trial_days:trial_days];
}

- (void)getPlans:(NSString *)plan_name interval:(NSString *)interval interval_count:(NSInteger)interval_count trial_days:(NSInteger)trial_days {
    NSMutableDictionary *params = [BCPayUtil prepareParametersForRequest];
    BCHTTPSessionManager *manager = [BCPayUtil getBCHTTPSessionManager];
    
    if (self.created_before > 0) params[@"created_before"] = @(self.created_before);
    if (self.created_after > 0) params[@"created_after"] = @(self.created_after);
    params[@"count_only"] = self.count_only?@"true":@"false";
    params[@"skip"] = @(self.skip);
    params[@"limit"] = @(self.limit);
    
    if (plan_name.isValid) params[@"name_with_substring"] = plan_name;
    if (interval.isValid) params[@"interval"] = interval;
    if (interval_count > 0) params[@"interval_count"] = @(interval_count);
    if (trial_days > 0) params[@"trial_days"] = @(trial_days);
    
    [manager GET:[NSString stringWithFormat:@"%@/plan", subscription_host] parameters:params progress:nil success:^(NSURLSessionDataTask * _Nonnull task, id  _Nullable responseObject) {
        NSMutableDictionary *response = [NSMutableDictionary dictionaryWithDictionary:(NSDictionary *)responseObject];
        response[@"type"] = self.count_only?@(BCSubTypePlansCount):@(BCSubTypePlans);
        [BCSubscription doSubscriptionResponse:response];
    } failure:^(NSURLSessionDataTask * _Nullable task, NSError * _Nonnull error) {
        [BCSubscription doSubscriptionErrorResponse:kNetWorkError];
    }];
}

- (void)getSubscriptionsCount:(NSString *)buyer_id plan_id:(NSString *)plan_id card_id:(NSString *)card_id {
    self.count_only = YES;
    [self getSubscriptions:buyer_id plan_id:plan_id card_id:card_id];
}

- (void)getSubscriptions:(NSString *)buyer_id plan_id:(NSString *)plan_id card_id:(NSString *)card_id {
    NSMutableDictionary *params = [BCPayUtil prepareParametersForRequest];
    BCHTTPSessionManager *manager = [BCPayUtil getBCHTTPSessionManager];
    
    if (self.created_before > 0) params[@"created_before"] = @(self.created_before);
    if (self.created_after > 0) params[@"created_after"] = @(self.created_after);
    params[@"count_only"] = self.count_only?@"true":@"false";
    params[@"skip"] = @(self.skip);
    params[@"limit"] = @(self.limit);
    
    if (buyer_id.isValid) {
        params[@"buyer_id"] = buyer_id;
    }
    if (plan_id.isValid) {
        params[@"plan_id"] = plan_id;
    }
    if (card_id.isValid ) {
        params[@"card_id"] = card_id;
    }
    
    __weak BCSubscriptionQuery *weakself = self;
    [manager GET:[NSString stringWithFormat:@"%@/subscription", subscription_host] parameters:params progress:nil success:^(NSURLSessionDataTask * _Nonnull task, id  _Nullable responseObject) {
        NSMutableDictionary *response = [NSMutableDictionary dictionaryWithDictionary:(NSDictionary *)responseObject];
        response[@"type"] = weakself.count_only?@(BCSubTypeSubscriptionsCount):@(BCSubTypeSubscriptions);
        [BCSubscription doSubscriptionResponse:response];
    } failure:^(NSURLSessionDataTask * _Nullable task, NSError * _Nonnull error) {
        [BCSubscription doSubscriptionErrorResponse:kNetWorkError];
    }];
    
}

@end
