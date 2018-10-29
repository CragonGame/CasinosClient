//
//  BCPreRefundReq.m
//  BCPay
//
//  Created by Ewenlong03 on 16/6/22.
//  Copyright © 2016年 BeeCloud. All rights reserved.
//

#import "BCPreRefundReq.h"
#import "BCPayUtil.h"
#import "BCPreRefundResp.h"

@implementation BCPreRefundReq

- (instancetype)init {
    self = [super init];
    if (self) {
        self.type = BCObjsTypePreRefundReq;
        self.channel = PayChannelNone;
        self.refundNo = @"";
        self.billNo = @"";
        self.refundFee = @"";
    }
    return self;
}

- (void)preRefundReq {
    [BCPayCache sharedInstance].bcResp = [[BCPreRefundResp alloc] initWithReq:self];
    
    if (![self checkParametersForPreRefund]) return ;
    
    NSString *cType = [BCPayUtil getChannelString: self.channel];
    
    NSMutableDictionary *parameters = [BCPayUtil prepareParametersForRequest];
    if (parameters == nil) {
        [BCPayUtil doErrorResponse:@"请检查是否全局初始化"];
        return;
    }
    
    if (![cType isEqualToString:@""]) {
        parameters[@"channel"] = [cType componentsSeparatedByString:@"_"][0];
    }
    parameters[@"refund_no"] = self.refundNo;
    parameters[@"refund_fee"] = [NSNumber numberWithInteger:[self.refundFee integerValue]];
    parameters[@"bill_no"] = self.billNo;
    
    if (self.optional) {
        parameters[@"optional"] = self.optional;
    }
    
    parameters[@"need_approval"] = @(YES);
    
    BCHTTPSessionManager *manager = [BCPayUtil getBCHTTPSessionManager];
    
    [manager POST:[BCPayUtil getBestHostWithFormat:kRestApiRefund] parameters:parameters progress:nil
          success:^(NSURLSessionTask *task, id response) {
              if ([response integerValueForKey:kKeyResponseResultCode defaultValue:BCErrCodeCommon] != 0) {
                  [BCPayUtil getErrorInResponse:(NSDictionary *)response];
              } else {
                  [BCPayCache sharedInstance].bcResp.bcId = [(NSDictionary *)response stringValueForKey:@"id" defaultValue:@""];
                  [BCPayCache sharedInstance].bcResp.resultCode = [response integerValueForKey:kKeyResponseResultCode defaultValue:BCErrCodeCommon];
                  [BCPayCache sharedInstance].bcResp.resultMsg = [response stringValueForKey:kKeyResponseResultMsg defaultValue:kUnknownError];
                  [BCPayCache sharedInstance].bcResp.errDetail = [response stringValueForKey:kKeyResponseErrDetail defaultValue:kUnknownError];
                  [BCPayCache beeCloudDoResponse];
              }
          } failure:^(NSURLSessionTask *operation, NSError *error) {
              [BCPayUtil doErrorResponse:kNetWorkError];
          }];

}

- (BOOL)checkParametersForPreRefund {
    if ([BCPayCache sharedInstance].sandbox) {
        [BCPayUtil doErrorResponse:@"当前为沙箱测试模式，不支持预退款功能"];
        return NO;
    } else if (!self.refundFee.isValid || !self.refundFee.isPureInt) {
        [BCPayUtil doErrorResponse:@"refundFee 以分为单位，必须是只包含数值的字符串"];
        return NO;
    } else if (!self.billNo.isValid || !self.billNo.isValidTraceNo || (self.billNo.length < 8) || (self.billNo.length > 32)) {
        [BCPayUtil doErrorResponse:@"billNo 必须是长度8~32位字母和/或数字组合成的字符串"];
        return NO;
    } else if (![self checkRefundNo])  {
        [BCPayUtil doErrorResponse:@"refundNo 格式必须为退款日期(8位) + 流水号(3~24 位)"];
        return NO;
    }
    return YES;
}

- (BOOL)checkRefundNo {
    if (!self.refundNo.isValid || !self.refundNo.isValidTraceNo || (self.refundNo.length < 11) || (self.refundNo.length > 32)) {
        return NO;
    }
    
    NSString *prefix = [self.refundNo substringToIndex:8];
    
    NSDateFormatter *dateFormatter = [[NSDateFormatter alloc] init];
    [dateFormatter setDateFormat:@"yyyyMMdd"];
    NSString *now = [dateFormatter stringFromDate:[NSDate date]];
    
    return [prefix isEqualToString:now];
}


@end
