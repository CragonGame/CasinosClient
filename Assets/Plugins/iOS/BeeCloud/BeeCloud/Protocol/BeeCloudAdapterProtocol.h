//
//  BeeCloudAdapterProtocol.h
//  BeeCloud
//
//  Created by Ewenlong03 on 15/9/9.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#ifndef BeeCloud_BeeCloudAdapterProtocol_h
#define BeeCloud_BeeCloudAdapterProtocol_h

#import <Foundation/Foundation.h>
#import "BeeCloud.h"

@protocol BeeCloudAdapterDelegate <NSObject>

@optional
- (BOOL)registerWeChat:(NSString *)appid;
- (BOOL)isWXAppInstalled;
- (void)registerPayPal:(NSString *)clientID secret:(NSString *)secret sandbox:(BOOL)isSandbox;
- (BOOL)handleOpenUrl:(NSURL *)url;

- (BOOL)wxPay:(NSMutableDictionary *)dic;
- (BOOL)aliPay:(NSMutableDictionary *)dic;
- (BOOL)unionPay:(NSMutableDictionary *)dic;
- (BOOL)applePay:(NSMutableDictionary *)dic;
- (NSString *)baiduPay:(NSMutableDictionary *)dic;
- (BOOL)sandboxPay;
- (BOOL)canMakeApplePayments:(NSUInteger)cardType;

- (void)payPal:(NSMutableDictionary *)dic;
- (void)payPalVerify:(NSMutableDictionary *)dic;

- (void)offlinePay:(NSMutableDictionary *)dic;
- (void)offlineStatus:(NSMutableDictionary *)dic;
- (void)offlineRevert:(NSMutableDictionary *)dic;
//BC_WX_APP
- (void)initBCWXPay:(NSString *)wxAppId;
- (void)bcWXPay:(NSMutableDictionary *)dic;

@end

#endif
