//
//  BCPayReq.m
//  BCPaySDK
//
//  Created by Ewenlong03 on 15/7/27.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "BCPayReq.h"
#import "BCPayUtil.h"
#import "BeeCloudAdapter.h"

#pragma mark pay request

@implementation BCPayReq

- (instancetype)init {
    self = [super init];
    if (self) {
        self.type = BCObjsTypePayReq;
        self.channel = PayChannelNone;
        self.title = @"";
        self.totalFee = @"";
        self.billNo = @"";
        self.scheme = @"";
        self.viewController = nil;
        self.cardType = 0;
        self.billTimeOut = 0;
    }
    return self;
}

- (void)payReq {
    [BCPayCache sharedInstance].bcResp = [[BCPayResp alloc] initWithReq:self];
    
    if (![self checkParametersForReqPay]) return;
    
    NSString *cType = [BCPayUtil getChannelString:self.channel];
    
    NSMutableDictionary *parameters = [BCPayUtil prepareParametersForRequest];
    if (parameters == nil) {
        [BCPayUtil doErrorResponse:@"请检查是否全局初始化"];
        return;
    }
    parameters[@"channel"] = cType;
    parameters[@"total_fee"] = [NSNumber numberWithInteger:[self.totalFee integerValue]];
    parameters[@"bill_no"] = self.billNo;
    parameters[@"title"] = self.title;
    
    if (self.notify_url) {
        parameters[@"notify_url"] = self.notify_url;
    }
    
    if (self.billTimeOut > 0) {
        parameters[@"bill_timeout"] = @(self.billTimeOut);
    }
    
    if (self.optional) {
        parameters[@"optional"] = self.optional;
    }
    
    if (self.analysis) {
        parameters[@"analysis"] = self.analysis;
    }
    
    BCHTTPSessionManager *manager = [BCPayUtil getBCHTTPSessionManager];
    __weak BCPayReq *weakSelf = self;
    
    [manager POST:[BCPayUtil getBestHostWithFormat:kRestApiPay] parameters:parameters progress:nil
          success:^(NSURLSessionTask *task, id response) {
              if ([response integerValueForKey:kKeyResponseResultCode defaultValue:BCErrCodeCommon] != 0) {
                  [BCPayUtil getErrorInResponse:(NSDictionary *)response];
              } else {
                  if ([BCPayCache sharedInstance].sandbox) {
                      [weakSelf doPayActionInSandbox:(NSDictionary *)response];
                  } else {
                      [weakSelf doPayAction:(NSDictionary *)response];
                  }
              }
          } failure:^(NSURLSessionTask *operation, NSError *error) {
              [BCPayUtil doErrorResponse:kNetWorkError];
          }];
}

#pragma mark - Pay Action

- (BOOL)doPayAction:(NSDictionary *)response {
    BOOL bSendPay = NO;
    if (response) {
        NSMutableDictionary *dic = [NSMutableDictionary dictionaryWithDictionary:
                                    (NSDictionary *)response];
        if (self.channel == PayChannelAliApp || self.channel == PayChannelUnApp || self.channel == PayChannelBCAliApp) {
            [dic setObject:self.scheme forKey:@"scheme"];
        }
        if (self.channel == PayChannelUnApp || self.channel == PayChannelApplePay ||
                   self.channel == PayChannelApplePayTest || self.channel == PayChannelBCApp) {
            [dic setObject:self.viewController forKey:@"viewController"];
            if (self.channel == PayChannelApplePayTest || self.channel == PayChannelApplePay) {
                [dic setObject:@(self.channel) forKey:@"channel"];
            }
        }
        [BCPayCache sharedInstance].bcResp.bcId = [dic objectForKey:@"id"];
        switch (self.channel) {
            case PayChannelWxApp:
            case PayChannelBCWXApp:
                bSendPay = [BeeCloudAdapter beeCloudWXPay:dic];
                break;
            case PayChannelAliApp:
            case PayChannelBCAliApp:
                bSendPay = [BeeCloudAdapter beeCloudAliPay:dic];
                break;
            case PayChannelBCApp:
            case PayChannelUnApp:
                bSendPay = [BeeCloudAdapter beeCloudUnionPay:dic];
                break;
            case PayChannelBaiduApp:
                bSendPay = [BeeCloudAdapter beeCloudBaiduPay:dic].isValid;
                break;
            case PayChannelApplePayTest:
            case PayChannelApplePay:
                NSLog(@"applePay %@", dic);
                bSendPay = [BeeCloudAdapter beeCloudApplePay:dic];
                break;
            default:
                break;
        }
    }
    return bSendPay;
}

- (BOOL)doPayActionInSandbox:(NSDictionary *)response {
    
    if (response) {
        NSMutableDictionary *dic = [NSMutableDictionary dictionaryWithDictionary:
                                    (NSDictionary *)response];
        [BCPayCache sharedInstance].bcResp.bcId = [dic objectForKey:@"id"];
        [BeeCloudAdapter beecloudSandboxPay];
        return YES;
    }
    return NO;
}

- (BOOL)checkParametersForReqPay {
    if (!self.title.isValid || [BCPayUtil getBytes:self.title] > 32) {
        [BCPayUtil doErrorResponse:@"title 必须是长度不大于32个字节,最长16个汉字的字符串的合法字符串"];
        return NO;
    } else if (!self.totalFee.isValid || !self.totalFee.isPureInt) {
        [BCPayUtil doErrorResponse:@"totalFee 以分为单位，必须是只包含数值的字符串"];
        return NO;
    } else if (!self.billNo.isValid || !self.billNo.isValidTraceNo || (self.billNo.length < 8) || (self.billNo.length > 32)) {
        [BCPayUtil doErrorResponse:@"billNo 必须是长度8~32位字母和/或数字组合成的字符串"];
        return NO;
    } else if ((self.channel == PayChannelAliApp || self.channel == PayChannelUnApp) && !self.scheme.isValid) {
        [BCPayUtil doErrorResponse:@"scheme 不是合法的字符串，将导致无法从支付宝钱包返回应用"];
        return NO;
    } else if ((self.channel == PayChannelUnApp || self.channel == PayChannelApplePay ||
                self.channel == PayChannelBCApp ) && (self.viewController == nil)) {
        [BCPayUtil doErrorResponse:@"viewController 不合法，将导致无法正常支付"];
        return NO;
    } else if ([BeeCloud getCurrentMode] && (self.viewController == nil)) {
        [BCPayUtil doErrorResponse:@"viewController 不合法，将导致无法正常支付"];
        return NO;
    } else if (((self.channel == PayChannelWxApp || self.channel == PayChannelBCWXApp) && ![BeeCloudAdapter beeCloudIsWXAppInstalled]) && ![BeeCloud getCurrentMode]) {
        [BCPayUtil doErrorResponse:@"未找到微信客户端，请先下载安装"];
        return NO;
    } else if ((self.channel == PayChannelApplePay || self.channel == PayChannelApplePayTest) && ![BeeCloud canMakeApplePayments:self.cardType]) {
        switch (self.cardType) {
            case 0:
                [BCPayUtil doErrorResponse:@"此设备不支持Apple Pay"];
                break;
            case 1:
                [BCPayUtil doErrorResponse:@"不支持借记卡"];
                break;
            case 2:
                [BCPayUtil doErrorResponse:@"不支持信用卡"];
                break;
        }
        return NO;
    }
    return YES;
}

@end
