//
//  BCProtocol.h
//  BeeCloud
//
//  Created by Ewenlong03 on 15/9/9.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "BeeCloud.h"

@interface BeeCloudAdapter : NSObject

+ (BOOL)beeCloudRegisterWeChat:(NSString *)appid;
+ (BOOL)beeCloudIsWXAppInstalled;
+ (void)beeCloudRegisterPayPal:(NSString *)clientID secret:(NSString *)secret sandbox:(BOOL)isSandbox;
+ (BOOL)beeCloud:(NSString *)object handleOpenUrl:(NSURL *)url;

+ (BOOL)beeCloudWXPay:(NSMutableDictionary *)dic;
+ (BOOL)beeCloudAliPay:(NSMutableDictionary *)dic;
+ (BOOL)beeCloudUnionPay:(NSMutableDictionary *)dic;
+ (BOOL)beeCloudApplePay:(NSMutableDictionary *)dic;
+ (NSString *)beeCloudBaiduPay:(NSMutableDictionary *)dic;
+ (BOOL)beecloudSandboxPay;
+ (BOOL)beecloudCanMakeApplePayments:(NSUInteger)cardType;

+ (void)beeCloudPayPal:(NSMutableDictionary *)dic;
+ (void)beeCloudPayPalVerify:(NSMutableDictionary *)dic;
+ (void)beeCloudOfflinePay:(NSMutableDictionary *)dic;
+ (void)beeCloudOfflineStatus:(NSMutableDictionary *)dic;
+ (void)beeCloudOfflineRevert:(NSMutableDictionary *)dic;

+ (void)beeCloudInitBCWXPay:(NSString *)wxAppId;
+ (void)beeCloudBCWXPay:(NSMutableDictionary *)dic;


@end
