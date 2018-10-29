//
//  BeeCloudAdapaterProtocol.m
//  BeeCloud
//
//  Created by Ewenlong03 on 15/9/9.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "BeeCloudAdapter.h"
#import "BeeCloudAdapterProtocol.h"
#import "BCPayCache.h"

@implementation BeeCloudAdapter

+ (BOOL)beeCloudRegisterWeChat:(NSString *)appid {
    id adapter = [[NSClassFromString(kAdapterWXPay) alloc] init];
    if (adapter && [adapter respondsToSelector:@selector(registerWeChat:)]) {
        return [adapter registerWeChat:appid];
    }
    return NO;
}

+ (BOOL)beeCloudIsWXAppInstalled {
    id adapter = [[NSClassFromString(kAdapterWXPay) alloc] init];
    if (adapter && [adapter respondsToSelector:@selector(isWXAppInstalled)]) {
        return [adapter isWXAppInstalled];
    }
    return NO;
}

+ (void)beeCloudRegisterPayPal:(NSString *)clientID secret:(NSString *)secret sandbox:(BOOL)isSandbox {
    id adapter = [[NSClassFromString(kAdapterPayPal) alloc] init];
    if (adapter && [adapter respondsToSelector:@selector(registerPayPal:secret:sandbox:)]) {
        [adapter registerPayPal:clientID secret:secret sandbox:isSandbox];
    }
}

+ (BOOL)beeCloud:(NSString *)object handleOpenUrl:(NSURL *)url {
    id adapter = [[NSClassFromString(object) alloc] init];
    if (adapter && [adapter respondsToSelector:@selector(handleOpenUrl:)]) {
        return [adapter handleOpenUrl:url];
    }
    return NO;
}

+ (BOOL)beeCloudWXPay:(NSMutableDictionary *)dic {
    id adapter = [[NSClassFromString(kAdapterWXPay) alloc] init];
    if (adapter && [adapter respondsToSelector:@selector(wxPay:)]) {
         return [adapter wxPay:dic];
    }
    return NO;
}

+ (BOOL)beeCloudAliPay:(NSMutableDictionary *)dic {
    id adapter = [[NSClassFromString(kAdapterAliPay) alloc] init];
    if (adapter && [adapter respondsToSelector:@selector(aliPay:)]) {
        return [adapter aliPay:dic];
    }
    return NO;
}

+ (BOOL)beeCloudUnionPay:(NSMutableDictionary *)dic {
    id adapter = [[NSClassFromString(kAdapterUnionPay) alloc] init];
    if (adapter && [adapter respondsToSelector:@selector(unionPay:)]) {
        return [adapter unionPay:dic];
    }
    return NO;
}

+ (BOOL)beecloudCanMakeApplePayments:(NSUInteger)cardType {
    id adapter = [[NSClassFromString(kAdapterApplePay) alloc] init];
    if (adapter && [adapter respondsToSelector:@selector(canMakeApplePayments:)]) {
        return [adapter canMakeApplePayments:cardType];
    }
    return NO;
}

+ (BOOL)beeCloudApplePay:(NSMutableDictionary *)dic {
    id adapter = [[NSClassFromString(kAdapterApplePay) alloc] init];
    if (adapter && [adapter respondsToSelector:@selector(applePay:)]) {
        return [adapter applePay:dic];
    }
    return NO;
}

+ (NSString *)beeCloudBaiduPay:(NSMutableDictionary *)dic {
    id adapter = [[NSClassFromString(kAdapterBaidu) alloc] init];
    if (adapter && [adapter respondsToSelector:@selector(baiduPay:)]) {
        return [adapter baiduPay:dic];
    }
    return nil;
}

+ (BOOL)beecloudSandboxPay {
    id adapter = [[NSClassFromString(kAdapterSandbox) alloc] init];
    if (adapter && [adapter respondsToSelector:@selector(sandboxPay)]) {
        return [adapter sandboxPay];
    }
    return NO;
}

+ (void)beeCloudPayPal:(NSMutableDictionary *)dic {
    id adapter = [[NSClassFromString(kAdapterPayPal) alloc] init];
    if (adapter && [adapter respondsToSelector:@selector(payPal:)]) {
        [adapter payPal:dic];
    }
}

+ (void)beeCloudPayPalVerify:(NSMutableDictionary *)dic {
    id adapter = [[NSClassFromString(kAdapterPayPal) alloc] init];
    if (adapter && [adapter respondsToSelector:@selector(payPalVerify:)]) {
        [adapter payPalVerify:dic];
    }
}

+ (void)beeCloudOfflinePay:(NSMutableDictionary *)dic {
    id adapter = [[NSClassFromString(kAdapterOffline) alloc] init];
    if (adapter && [adapter respondsToSelector:@selector(offlinePay:)]) {
        [adapter offlinePay:dic];
    }
}

+ (void)beeCloudOfflineStatus:(NSMutableDictionary *)dic {
    id adapter = [[NSClassFromString(kAdapterOffline) alloc] init];
    if (adapter && [adapter respondsToSelector:@selector(offlineStatus:)]) {
        [adapter offlineStatus:dic];
    }
}

+ (void)beeCloudOfflineRevert:(NSMutableDictionary *)dic {
    id adapter = [[NSClassFromString(kAdapterOffline) alloc] init];
    if (adapter && [adapter respondsToSelector:@selector(offlineRevert:)]) {
        [adapter offlineRevert:dic];
    }
}

+ (void)beeCloudInitBCWXPay:(NSString *)wxAppId {
    id adapter = [[NSClassFromString(kAdapterBCWXPay) alloc] init];
    if (adapter && [adapter respondsToSelector:@selector(initBCWXPay:)]) {
        [adapter initBCWXPay:wxAppId];
    }
}

+ (void)beeCloudBCWXPay:(NSMutableDictionary *)dic {
    id adapter = [[NSClassFromString(kAdapterBCWXPay) alloc] init];
    if (adapter && [adapter respondsToSelector:@selector(bcWXPay:)]) {
        [adapter bcWXPay:dic];
    }
}


@end
