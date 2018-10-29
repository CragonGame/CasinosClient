//
//  WXPayAdapter.m
//  BeeCloud
//
//  Created by Ewenlong03 on 15/9/9.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "WXPayAdapter.h"
#import "WXApi.h"
#import "BeeCloudAdapterProtocol.h"
#import "BCPayUtil.h"

@interface WXPayAdapter ()<BeeCloudAdapterDelegate, WXApiDelegate>

@end

@implementation WXPayAdapter

+ (instancetype)sharedInstance {
    static dispatch_once_t onceToken;
    static WXPayAdapter *instance = nil;
    dispatch_once(&onceToken, ^{
        instance = [[WXPayAdapter alloc] init];
    });
    return instance;
}

- (BOOL)registerWeChat:(NSString *)appid {
    return [WXApi registerApp:appid];
}

- (BOOL)handleOpenUrl:(NSURL *)url {
    return [WXApi handleOpenURL:url delegate:[WXPayAdapter sharedInstance]];
}

- (BOOL)isWXAppInstalled {
    return [WXApi isWXAppInstalled];
}

- (BOOL)wxPay:(NSMutableDictionary *)dic {
    
    PayReq *request = [[PayReq alloc] init];
    request.partnerId = [dic stringValueForKey:@"partner_id" defaultValue:@""];
    request.prepayId = [dic stringValueForKey:@"prepay_id" defaultValue:@""];
    request.package = [dic stringValueForKey:@"package" defaultValue:@""];
    request.nonceStr = [dic stringValueForKey:@"nonce_str" defaultValue:@""];
    NSString *time = [dic stringValueForKey:@"timestamp" defaultValue:@""];
    request.timeStamp = time.intValue;
    request.sign = [dic stringValueForKey:@"pay_sign" defaultValue:@""];
    return [WXApi sendReq:request];
}

#pragma mark - Implementation WXApiDelegate

- (void)onResp:(BaseResp *)resp {
    
    if ([resp isKindOfClass:[PayResp class]]) {
        PayResp *tempResp = (PayResp *)resp;
        NSString *strMsg = nil;
        int errcode = 0;
        switch (tempResp.errCode) {
            case WXSuccess:
                strMsg = @"支付成功";
                errcode = BCErrCodeSuccess;
                break;
            case WXErrCodeUserCancel:
                strMsg = @"支付取消";
                errcode = BCErrCodeUserCancel;
                break;
            default:
                strMsg = @"支付失败";
                errcode = BCErrCodeSentFail;
                break;
        }
        NSString *result = tempResp.errStr.isValid?[NSString stringWithFormat:@"%@,%@",strMsg,tempResp.errStr]:strMsg;
        BCPayResp *resp = (BCPayResp *)[BCPayCache sharedInstance].bcResp;
        resp.resultCode = errcode;
        resp.resultMsg = result;
        resp.errDetail = result;
        [BCPayCache beeCloudDoResponse];
    }
}

@end
