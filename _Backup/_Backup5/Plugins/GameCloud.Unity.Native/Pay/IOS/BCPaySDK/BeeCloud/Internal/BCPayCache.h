//
//  BCPayCache.h
//  BeeCloud SDK
//
//  Created by Junxian Huang on 2/27/14.
//  Copyright (c) 2014 BeeCloud Inc. All rights reserved.
//
#import <Foundation/Foundation.h>
#import "BCBaseResp.h"
/*!
 This header file is *NOT* included in the public release.
 */

/**
 *  BCCache stores system settings and content caches.
 */
@interface BCPayCache : NSObject

/**
 *  App key obtained when registering this app in BeeCloud website. Change this value via [BeeCloud setAppKey:];
 */
@property (nonatomic, strong) NSString *appId;

/**
 *  生产环境密钥
 */
@property (nonatomic, strong) NSString *appSecret;

/**
 *  YES表示沙箱环境，不产生真实交易；NO表示生产环境，产生真实交易
 *  默认为NO，生产环境
 */
@property (nonatomic, assign) BOOL sandbox;

/**
 *  PayPal client ID
 */
@property (nonatomic, strong) NSString *payPalClientID;

/**
 *  PayPal secret
 */
@property (nonatomic, strong) NSString *payPalSecret;

/**
 *  PayPal Sandbox Client ID
 */
@property (nonatomic, assign) BOOL isPayPalSandbox;


/**
 *  Default network timeout in seconds for all network requests. Change this value via [BeeCloud setNetworkTimeout:];
 */
@property (nonatomic) NSTimeInterval networkTimeout;

/**
 *  Mark whether print log message.
 */
@property (nonatomic, assign) BOOL willPrintLogMsg;

/**
 *  base response instance
 */
@property (nonatomic, strong) BCBaseResp *bcResp;

/**
 *  Get the sharedInstance of BCCache.
 *
 *  @return BCCache shared instance.
 */
+ (instancetype)sharedInstance;

/**
 *  BeeCloud response
 */
+ (BOOL)beeCloudDoResponse;

@end
