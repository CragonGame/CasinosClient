//
//  OfflineAdapter.m
//  BCPay
//
//  Created by Ewenlong03 on 15/9/18.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "OfflineAdapter.h"
#import "BCPayUtil.h"
#import "BCPayCache.h"
#import "BeeCloudAdapterProtocol.h"
#import "BCOffinePay.h"

@interface OfflineAdapter ()<BeeCloudAdapterDelegate>
@end

@implementation OfflineAdapter

+ (instancetype)sharedInstance {
    static dispatch_once_t onceToken;
    static OfflineAdapter *instance = nil;
    dispatch_once(&onceToken, ^{
        instance = [[OfflineAdapter alloc] init];
    });
    return instance;
}

- (void)offlinePay:(NSMutableDictionary *)dic {
    BCOfflinePayReq *req = (BCOfflinePayReq *)[dic objectForKey:kAdapterOffline];
    [BCPayCache sharedInstance].bcResp = [[BCOfflinePayResp alloc] initWithReq:req];
    
    if (![self checkParameters:req]) return;
    
    NSString *cType = [BCPayUtil getChannelString:req.channel];
    
    NSMutableDictionary *parameters = [BCPayUtil prepareParametersForRequest];
    if (parameters == nil) {
        [self doErrorResponse:@"请检查是否全局初始化"];
        return;
    }
    
    parameters[@"channel"] = cType;
    parameters[@"total_fee"] = [NSNumber numberWithInteger:[req.totalFee integerValue]];
    parameters[@"bill_no"] = req.billNo;
    parameters[@"title"] = req.title;
    if (req.channel == PayChannelWxScan || req.channel == PayChannelAliScan ||
        req.channel == PayChannelBCAliScan || req.channel == PayChannelBCWxScan) {
        parameters[@"auth_code"] = req.authcode;
    }
    if (req.channel == PayChannelAliScan) {
        if (req.terminalId.isValid) {
            parameters[@"terminal_id"] = req.terminalId;
        }
        if (req.storeId.isValid) {
            parameters[@"store_id"] = req.storeId;
        }
    }
    if (req.optional) {
        parameters[@"optional"] = req.optional;
    }
    
    BCHTTPSessionManager *manager = [BCPayUtil getBCHTTPSessionManager];
    __weak OfflineAdapter *weakSelf = [OfflineAdapter sharedInstance];
    NSString *host = [BCPayUtil getBestHostWithFormat:kRestApiOfflinePay];
    if(req.channel == PayChannelBCNative) {
        host = [BCPayUtil getBestHostWithFormat:kRestApiPay];
    }
    [manager POST:host parameters:parameters progress:nil
          success:^(NSURLSessionTask *task, id response) {
              
              NSDictionary *source = (NSDictionary *)response;
              BCPayLog(@"channel=%@,resp=%@", cType, response);
              BCOfflinePayResp *resp = (BCOfflinePayResp *)[BCPayCache sharedInstance].bcResp;
              resp.resultCode = [source integerValueForKey:kKeyResponseResultCode defaultValue:BCErrCodeCommon];
              resp.resultMsg = [source stringValueForKey:kKeyResponseResultMsg defaultValue:kUnknownError];
              resp.errDetail = [source stringValueForKey:kKeyResponseErrDetail defaultValue:kUnknownError];
              if (resp.resultCode == 0) {
                  if (req.channel == PayChannelAliOfflineQrcode || req.channel == PayChannelWxNative || req.channel == PayChannelBCAliQrcode || req.channel == PayChannelBCNative) {
                      resp.codeurl = [source stringValueForKey:kKeyResponseCodeUrl defaultValue:@""];
                  }
              }
              [BCPayCache beeCloudDoResponse];
              
          } failure:^(NSURLSessionTask *operation, NSError *error) {
              [weakSelf doErrorResponse:kNetWorkError];
          }];
}

#pragma mark - OffLine BillStatus

- (void)offlineStatus:(NSMutableDictionary *)dic {
    BCOfflineStatusReq *req = (BCOfflineStatusReq *)[dic objectForKey:kAdapterOffline];
    [BCPayCache sharedInstance].bcResp = [[BCOfflineStatusResp alloc] initWithReq:req];
    if (req == nil) {
        [self doErrorResponse:@"请求结构体不合法"];
        return;
    } else if (!req.billNo.isValid || !req.billNo.isValidTraceNo || (req.billNo.length < 8) || (req.billNo.length > 32)) {
        [self doErrorResponse:@"billno 必须是长度8~32位字母和/或数字组合成的字符串"];
        return;
    }
    
    NSString *cType = [BCPayUtil getChannelString:req.channel];
    
    NSMutableDictionary *parameters = [BCPayUtil prepareParametersForRequest];
    if (parameters == nil) {
        [self doErrorResponse:@"请检查是否全局初始化"];
        return;
    }
    
    parameters[@"channel"] = cType;
    parameters[@"bill_no"] = req.billNo;
    
    BCHTTPSessionManager *manager = [BCPayUtil getBCHTTPSessionManager];
    __weak OfflineAdapter *weakSelf = [OfflineAdapter sharedInstance];
    [manager POST:[BCPayUtil getBestHostWithFormat:kRestApiOfflineBillStatus] parameters:parameters progress:nil
          success:^(NSURLSessionTask *operation, id response) {
              
              BCPayLog(@"channel=%@,resp=%@", cType, response);
              BCOfflineStatusResp *resp = (BCOfflineStatusResp *)[BCPayCache sharedInstance].bcResp;
              resp.resultCode = [response integerValueForKey:kKeyResponseResultCode defaultValue:BCErrCodeCommon];
              resp.resultMsg = [response stringValueForKey:kKeyResponseResultMsg defaultValue:kUnknownError];
              resp.errDetail = [response stringValueForKey:kKeyResponseErrDetail defaultValue:kUnknownError];
              if (resp.resultCode == 0) {
                  resp.payResult = [response boolValueForKey:KKeyResponsePayResult defaultValue:NO];
              }
              [BCPayCache beeCloudDoResponse];
          } failure:^(NSURLSessionTask *operation, NSError *error) {
              [weakSelf doErrorResponse:kNetWorkError];
          }];
}

#pragma mark - OffLine BillRevert

- (void)offlineRevert:(NSMutableDictionary *)dic {
    BCOfflineRevertReq *req = (BCOfflineRevertReq *)[dic objectForKey:kAdapterOffline];
    [BCPayCache sharedInstance].bcResp = [[BCOfflineRevertResp alloc] initWithReq:req];
    if (req == nil) {
        [self doErrorResponse:@"请求结构体不合法"];
        return;
    } else if (!req.billNo.isValid || !req.billNo.isValidTraceNo || (req.billNo.length < 8) || (req.billNo.length > 32)) {
        [self doErrorResponse:@"billno 必须是长度8~32位字母和/或数字组合成的字符串"];
        return;
    }
    
    NSString *cType = [BCPayUtil getChannelString:req.channel];
    
    NSMutableDictionary *parameters = [BCPayUtil prepareParametersForRequest];
    if (parameters == nil) {
        [self doErrorResponse:@"请检查是否全局初始化"];
        return;
    }
    
    parameters[@"channel"] = cType;
    parameters[@"method"] = @"REVERT";
    
    BCHTTPSessionManager *manager = [BCPayUtil getBCHTTPSessionManager];
    __weak OfflineAdapter *weakSelf = [OfflineAdapter sharedInstance];
    [manager POST:[[BCPayUtil getBestHostWithFormat:kRestApiOfflineBillRevert] stringByAppendingString:req.billNo] parameters:parameters progress:nil
          success:^(NSURLSessionTask *operation, id response) {
    
              BCPayLog(@"channel=%@,resp=%@", cType, response);
              BCOfflineRevertResp *resp = (BCOfflineRevertResp *)[BCPayCache sharedInstance].bcResp;
              resp.resultCode = [response integerValueForKey:kKeyResponseResultCode defaultValue:BCErrCodeCommon];
              resp.resultMsg = [response stringValueForKey:kKeyResponseResultMsg defaultValue:kUnknownError];
              resp.errDetail = [response stringValueForKey:kKeyResponseErrDetail defaultValue:kUnknownError];
              if (resp.resultCode == 0) {
                  resp.revertStatus = [response boolValueForKey:kKeyResponseRevertResult defaultValue:NO];
              }
              [BCPayCache beeCloudDoResponse];
          } failure:^(NSURLSessionTask *operation, NSError *error) {
              [weakSelf doErrorResponse:kNetWorkError];
          }];
}

- (BOOL)checkParameters:(BCBaseReq *)request {
    
    if (request == nil) {
        [self doErrorResponse:@"请求结构体不合法"];
    } else if (request.type == BCObjsTypeOfflinePayReq) {
        BCOfflinePayReq *req = (BCOfflinePayReq *)request;
        if (!req.title.isValid || [BCPayUtil getBytes:req.title] > 32) {
            [self doErrorResponse:@"title 必须是长度不大于32个字节,最长16个汉字的字符串的合法字符串"];
            return NO;
        } else if (!req.totalFee.isValid || !req.totalFee.isPureInt) {
            [self doErrorResponse:@"totalfee 以分为单位，必须是只包含数值的字符串"];
            return NO;
        } else if (!req.billNo.isValid || !req.billNo.isValidTraceNo || (req.billNo.length < 8) || (req.billNo.length > 32)) {
            [self doErrorResponse:@"billno 必须是长度8~32位字母和/或数字组合成的字符串"];
            return NO;
        } else if ((req.channel == PayChannelWxScan || req.channel == PayChannelAliScan ||
                    req.channel == PayChannelBCAliScan || req.channel == PayChannelBCWxScan) && !req.authcode.isValid) {
            [self doErrorResponse:@"authcode 不是合法的字符串"];
            return NO;
        } else if ((req.channel == PayChannelAliScan) && (!req.terminalId.isValid || !req.storeId.isValid)) {
            [self doErrorResponse:@"terminalid或storeid 不是合法的字符串"];
            return NO;
        }
        return YES;
    }
    return NO ;
}

- (void)doErrorResponse:(NSString *)errMsg {
    BCOfflineStatusResp *resp = (BCOfflineStatusResp *)[BCPayCache sharedInstance].bcResp;
    resp.resultCode = BCErrCodeCommon;
    resp.resultMsg = errMsg;
    resp.errDetail = errMsg;
    [BCPayCache beeCloudDoResponse];
}

@end
