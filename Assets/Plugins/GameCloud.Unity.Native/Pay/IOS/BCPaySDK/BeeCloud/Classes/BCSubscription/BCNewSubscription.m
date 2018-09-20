//
//  BCSubNewScription.m
//  BCPay
//
//  Created by Ewenlong03 on 16/8/8.
//  Copyright © 2016年 BeeCloud. All rights reserved.
//

#import "BCNewSubscription.h"

@implementation BCNewSubscription

- (instancetype)init {
    self = [super init];
    if (self) {
        self.buyer_id = @"";
        self.plan_id = @"";
        self.sms_id = @"";
        self.sms_code = @"";
        self.card_id = @"";
        self.bank_name = @"";
        self.card_no = @"";
        self.id_name = @"";
        self.id_no = @"";
        self.amount = 1.0;
        self.trial_end = 0;
    }
    return self;
}

- (void)newSubscription {
    NSMutableDictionary *params = [BCPayUtil prepareParametersForRequest];
    BCHTTPSessionManager *manager = [BCPayUtil getBCHTTPSessionManager];
    
    if (self.buyer_id.isValid) {
        params[@"buyer_id"] = self.buyer_id;
    }
    if (self.plan_id.isValid) {
        params[@"plan_id"] = self.plan_id;
    }
    if (self.sms_id.isValid ) {
        params[@"sms_id"] = self.sms_id;
    }
    if (self.sms_code.isValid ) {
        params[@"sms_code"] = self.sms_code;
    }
    if (self.card_id.isValid ) {
        params[@"card_id"] = self.card_id;
    }
    if (self.bank_name.isValid) {
        params[@"bank_name"] =self.bank_name;
    }
    if (self.card_no.isValid ) {
        params[@"card_no"] = self.card_no;
    }
    if (self.id_name.isValid ) {
        params[@"id_name"] = self.id_name;
    }
    if (self.id_no.isValid ) {
        params[@"id_no"] = self.id_no;
    }
    if (self.mobile.isValid ) {
        params[@"mobile"] = self.mobile;
    }
    params[@"trial_end"] = @(self.trial_end);
    params[@"amount"] = @(self.amount);
    
    [manager POST:@"https://apidynamic.beecloud.cn/2/subscription" parameters:params progress:nil success:^(NSURLSessionDataTask * _Nonnull task, id  _Nullable responseObject) {
        NSMutableDictionary *response = [NSMutableDictionary dictionaryWithDictionary:(NSDictionary *)responseObject];
        response[@"type"] = @(BCSubTypeNewSubscription);
        [BCSubscription doSubscriptionResponse:response];
    } failure:^(NSURLSessionDataTask * _Nullable task, NSError * _Nonnull error) {
        [BCSubscription doSubscriptionErrorResponse:kNetWorkError];
    }];

}

@end
