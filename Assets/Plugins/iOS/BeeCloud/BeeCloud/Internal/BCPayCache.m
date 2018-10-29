//
//  BCCache.m
//  BeeCloud SDK
//
//  Created by Junxian Huang on 2/27/14.
//  Copyright (c) 2014 BeeCloud Inc. All rights reserved.
//

#import "BCPayCache.h"
#import "BCPayConstant.h"
#import "BeeCloud.h"

@implementation BCPayCache

+ (instancetype)sharedInstance {
    static dispatch_once_t onceToken;
    __strong static BCPayCache *instance = nil;
    dispatch_once(&onceToken, ^{
        instance = [[BCPayCache alloc] init];
        
        instance.appId = nil;
        instance.appSecret = nil;
        instance.sandbox = NO;
        
        instance.payPalClientID = nil;
        instance.payPalSecret = nil;
        instance.isPayPalSandbox = NO;
        
        instance.bcResp = [[BCBaseResp alloc] init];
        
        instance.networkTimeout = 5.0;
        instance.willPrintLogMsg = NO;
        
    });
    return instance;
}

+ (BOOL)beeCloudDoResponse {
    id<BeeCloudDelegate> delegate = [BeeCloud getBeeCloudDelegate];
    if (delegate && [delegate respondsToSelector:@selector(onBeeCloudResp:)]) {
        [delegate onBeeCloudResp:[BCPayCache sharedInstance].bcResp];
        return YES;
    }
    return NO;
}

@end
