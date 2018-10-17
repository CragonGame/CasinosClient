//
//  BCApplePayAdapter.m
//  BCPay
//
//  Created by Ewenlong03 on 16/2/19.
//  Copyright © 2016年 BeeCloud. All rights reserved.
//

#import "ApplePayAdapter.h"
#import "BeeCloudAdapterProtocol.h"
#import "UPAPayPlugin.h"
#import <PassKit/PassKit.h>

@interface BCApplePayAdapter ()<BeeCloudAdapterDelegate, UPAPayPluginDelegate>

@end

@implementation BCApplePayAdapter

+ (instancetype)sharedInstance {
    static dispatch_once_t onceToken;
    static BCApplePayAdapter *instance = nil;
    dispatch_once(&onceToken, ^{
        instance = [[BCApplePayAdapter alloc] init];
    });
    return instance;
}

- (BOOL)canMakeApplePayments:(NSUInteger)cardType {
    BOOL status = NO;
    double version = [[UIDevice currentDevice].systemVersion doubleValue];
    if (version <= 9.2f) return status;
    switch(cardType) {
        case 0:
        {
            status = [PKPaymentAuthorizationViewController canMakePaymentsUsingNetworks:@[PKPaymentNetworkChinaUnionPay]];
            break;
        }
        case 1:
        {
            PKMerchantCapability merchantCapabilities = PKMerchantCapability3DS | PKMerchantCapabilityEMV | PKMerchantCapabilityDebit;
            status = [PKPaymentAuthorizationViewController canMakePaymentsUsingNetworks:@[PKPaymentNetworkChinaUnionPay] capabilities:merchantCapabilities];
            break;
        }
        case 2:
        {
            PKMerchantCapability merchantCapabilities = PKMerchantCapability3DS | PKMerchantCapabilityEMV | PKMerchantCapabilityCredit;
            status = [PKPaymentAuthorizationViewController canMakePaymentsUsingNetworks:@[PKPaymentNetworkChinaUnionPay] capabilities:merchantCapabilities];
            break;
        }
        default:
            status = [PKPaymentAuthorizationViewController canMakePaymentsUsingNetworks:@[PKPaymentNetworkChinaUnionPay]];
            break;
    }
    return status;
}

- (BOOL)applePay:(NSMutableDictionary *)dic {
    if ([self canMakeApplePayments:0]) {
        NSString *tn = [dic stringValueForKey:@"tn" defaultValue:@""];
        NSLog(@"apple tn = %@", dic);
        PayChannel channel = (PayChannel)[dic integerValueForKey:@"channel" defaultValue:0];
        NSString *mode = channel==PayChannelApplePay?@"00":@"01";
        if (tn.isValid) {
            dispatch_async(dispatch_get_main_queue(), ^{
                [UPAPayPlugin startPay:tn mode:mode viewController:dic[@"viewController"] delegate:[BCApplePayAdapter sharedInstance] andAPMechantID:dic[@"apple_mer_id"]];
            });
            return YES;
        }
    }
    return NO;
}

#pragma mark - Implementation ApplePayDelegate

- (void)UPAPayPluginResult:(UPPayResult *)payResult {
    int errcode = BCErrCodeSentFail;
    NSString *strMsg = @"支付失败";
    
    switch (payResult.paymentResultStatus) {
        case UPPaymentResultStatusSuccess: {
            strMsg = @"支付成功";
            errcode = BCErrCodeSuccess;
            break;
        }
        case UPPaymentResultStatusFailure:
            break;
        case UPPaymentResultStatusCancel: {
            strMsg = @"支付取消";
            break;
        }
        case UPPaymentResultStatusUnknownCancel: {
            strMsg = @"支付取消,交易已发起,状态不确定,商户需查询商户后台确认支付状态";
            break;
        }
    }
    
    BCPayResp *resp = (BCPayResp *)[BCPayCache sharedInstance].bcResp;
    resp.resultCode = errcode;
    resp.resultMsg = strMsg;
    resp.errDetail = payResult.errorDescription.isValid?payResult.errorDescription:strMsg;
    resp.paySource = @{@"otherInfo": payResult.otherInfo.isValid?payResult.otherInfo:@""};
    [BCPayCache beeCloudDoResponse];
    
}

@end
