//
//  BCSubscription.h
//  BCPay
//
//  Created by Ewenlong03 on 16/8/4.
//  Copyright © 2016年 BeeCloud. All rights reserved.
//

#import "BCBaseReq.h"
#import "BCPayUtil.h"

@protocol BCSubscriptionDelegate <NSObject>

- (void)onBCSubscriptionResp:(NSMutableDictionary *)resp;

@end

static NSString * const subscription_host = @"https://apidynamic.beecloud.cn/2";

typedef NS_ENUM(NSInteger, BCSubType) {
    BCSubTypeSMS = 1,
    BCSubTypeBanks,
    
    BCSubTypeNewPlan = 10,
    BCSubTypePlans,
    BCSubTypePlansCount,
    
    BCSubTypeNewSubscription = 20,
    BCSubTypeSubscriptions,
    BCSubTypeSubscriptionsCount,
    BCSubTypeSubscriptionCancel
};

@interface BCSubscription : BCBaseReq

@property (nonatomic, weak) id<BCSubscriptionDelegate> delegate;

/**
 *  设置代理
 *
 *  @param delegate 代理
 */
+ (void)setSubDelegate:(id<BCSubscriptionDelegate>)delegate;
/**
 *  发送短信验证码
 *
 *  @param phone 手机号
 */
+ (void)smsReq:(NSString *)phone;
/**
 *  获取支持的银行列表
 */
+ (void)subscriptionBanks;
/**
 *  取消订阅
 *
 *  @param sub_id 订阅记录id
 */
+ (void)subscriptionCancel:(NSString *)sub_id;
/**
 *  返回请求结果
 *
 *  @param response 请求返回结果
 */
+ (void)doSubscriptionResponse:(NSMutableDictionary *)response;
/**
 *  请求出错返回结果
 *
 *  @param errMsg 返回请求出错结果
 */
+ (void)doSubscriptionErrorResponse:(NSString *)errMsg;


@end
