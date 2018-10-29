//
//  AliPayAdapter.m
//  BeeCloud
//
//  Created by Ewenlong03 on 15/9/9.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "AliPayAdapter.h"
#import "BeeCloudAdapterProtocol.h"
#import <AlipaySDK/AlipaySDK.h>

@interface AliPayAdapter ()<BeeCloudAdapterDelegate>

@end

@implementation AliPayAdapter

+ (instancetype)sharedInstance {
    static dispatch_once_t onceToken;
    static AliPayAdapter *instance = nil;
    dispatch_once(&onceToken, ^{
        instance = [[AliPayAdapter alloc] init];
    });
    return instance;
}

- (BOOL)handleOpenUrl:(NSURL *)url {
    [[AlipaySDK defaultService] processOrderWithPaymentResult:url standbyCallback:^(NSDictionary *resultDic) {
        [[AliPayAdapter sharedInstance] processOrderForAliPay:resultDic];
    }];
    return YES;
}

- (BOOL)aliPay:(NSMutableDictionary *)dic {
    
    NSString *orderString = [dic stringValueForKey:@"order_string" defaultValue:@""];
    if (orderString.isValid) {
        [[AlipaySDK defaultService] payOrder:orderString fromScheme:[dic stringValueForKey:@"scheme" defaultValue:@""]
                                    callback:^(NSDictionary *resultDic) {
                                        [[AliPayAdapter sharedInstance] processOrderForAliPay:resultDic];
                                    }];
        return YES;
    }
    return NO;
}

#pragma mark - Implementation AliPayDelegate

- (void)processOrderForAliPay:(NSDictionary *)resultDic {
    int status = [resultDic[@"resultStatus"] intValue];
    
    NSString *strMsg;
    int errcode = 0;
    switch (status) {
        case 9000:
            strMsg = @"支付成功";
            errcode = BCErrCodeSuccess;
            break;
        case 4000:
        case 6002:
            strMsg = @"支付失败";
            errcode = BCErrCodeSentFail;
            break;
        case 6001:
            strMsg = @"支付取消";
            errcode = BCErrCodeUserCancel;
            break;
        default:
            strMsg = @"未知错误";
            errcode = BCErrCodeUnsupport;
            break;
    }
    BCPayResp *resp = (BCPayResp *)[BCPayCache sharedInstance].bcResp;
    resp.resultCode = errcode;
    resp.resultMsg = strMsg;
    resp.errDetail = strMsg;
    resp.paySource = resultDic;
    [BCPayCache beeCloudDoResponse];
}

@end
