//
//  BaiduAdapter.m
//  BCPay
//
//  Created by Ewenlong03 on 15/9/23.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BaiduAdapter.h"
#import "BeeCloudAdapterProtocol.h"
#import "BCPayUtil.h"
#import "BDWalletSDKMainManager.h"

static NSString * const kBaiduOrderInfo = @"orderInfo";

@interface BaiduAdapter ()<BeeCloudAdapterDelegate>
@end

@implementation BaiduAdapter

- (NSString *)baiduPay:(NSMutableDictionary *)dic {
    BCPayResp *resp = (BCPayResp *)[BCPayCache sharedInstance].bcResp;
    resp.resultCode = [dic integerValueForKey:kKeyResponseResultCode defaultValue:BCErrCodeCommon];
    resp.resultMsg = [dic stringValueForKey:kKeyResponseResultMsg defaultValue:kUnknownError];
    resp.errDetail = [dic stringValueForKey:kKeyResponseErrDetail defaultValue:kUnknownError];
    NSString *orderInfo = [dic stringValueForKey:kBaiduOrderInfo defaultValue:@""];
    resp.paySource = [NSDictionary dictionaryWithObjectsAndKeys:orderInfo, kBaiduOrderInfo,nil];
    [BCPayCache beeCloudDoResponse];
    return orderInfo;
}

@end
