//
//  BCAuthReq.m
//  BCPay
//
//  Created by Ewenlong03 on 16/9/20.
//  Copyright © 2016年 BeeCloud. All rights reserved.
//

#import "BCAuthReq.h"

@implementation BCAuthReq

- (instancetype)init {
    self = [super init];
    if (self) {
        self.type = BCObjsTypeAuthReq;
        self.name = @"";
        self.idNo = @"";
    }
    return self;
}

+ (void)authReqWithName:(NSString *)name idNo:(NSString *)idNo {
    [[BCAuthReq alloc] authReqWithName:name idNo:idNo];
}

- (void)authReqWithName:(NSString *)name idNo:(NSString *)idNo {
    self.name = name;
    self.idNo = idNo;
    [self authReq];
}

- (void)authReq {
    
    NSMutableDictionary *parameters = [BCPayUtil prepareParametersForRequest];
    if (parameters == nil) {
        [BCPayUtil doErrorResponse:@"请检查是否全局初始化"];
        return;
    }
    if (!self.name.isValid) {
        [BCPayUtil doErrorResponse:@"姓名错误"];
        return;
    }
    if (!self.idNo.isValid) {
        [BCPayUtil doErrorResponse:@"身份证号错误"];
        return;
    }
    parameters[@"name"] = self.name;
    parameters[@"id_no"] = self.idNo;
    
    BCHTTPSessionManager *manager = [BCPayUtil getBCHTTPSessionManager];
    
    [manager POST:[NSString stringWithFormat:@"%@/2/auth", kBCHost] parameters:parameters progress:nil
          success:^(NSURLSessionTask *task, id response) {
              [BCPayUtil getErrorInResponse:(NSDictionary *)response];
          } failure:^(NSURLSessionTask *operation, NSError *error) {
              [BCPayUtil doErrorResponse:kNetWorkError];
          }];
    
}

@end
