//
//  BCSubQueryPlan.h
//  BCPay
//
//  Created by Ewenlong03 on 16/8/8.
//  Copyright © 2016年 BeeCloud. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "BCSubscription.h"

@interface BCSubscriptionQuery : NSObject

/**
 *  查询某个时间点之前创建的计划或者订阅的记录
 */
@property (nonatomic, assign) long created_before;
/**
 *  查询某个时间点之后创建的计划或者订阅的记录
 */
@property (nonatomic, assign) long created_after;
/**
 *  If count_only is 'true', only the total count of all objects that match your filters will be returned. default 'false'
 */
@property (nonatomic, assign) BOOL count_only;
/**
 *  Skip some objects for pagination.default 0
 */
@property (nonatomic, assign) NSUInteger skip;
/**
 *  Limit the number of objects.default 10.
 */
@property (nonatomic, assign) NSUInteger limit;
/**
 *  根据条件查询订阅技术的数目
 *
 *  @param plan_name      计划名称
 *  @param interval       订阅周期
 *  @param interval_count 周期数目
 *  @param trial_days     免费天数，冲订阅成功的第二天开始计算
 */
- (void)getPlansCount:(NSString *)plan_name interval:(NSString *)interval interval_count:(NSInteger)interval_count trial_days:(NSInteger)trial_days;
/**
 *  根据条件查询订阅计划详情
 *
 *  @param plan_name      计划名称
 *  @param interval       订阅周期
 *  @param interval_count 周期数目
 *  @param trial_days     免费天数，冲订阅成功的第二天开始计算
 */
- (void)getPlans:(NSString *)plan_name interval:(NSString *)interval interval_count:(NSInteger)interval_count trial_days:(NSInteger)trial_days;
/**
 *  根据条件查询订阅记录数目
 *
 *  @param buyer_id 用户id
 *  @param plan_id  计划id
 *  @param card_id  银行卡记录id
 */
- (void)getSubscriptionsCount:(NSString *)buyer_id plan_id:(NSString *)plan_id card_id:(NSString *)card_id;
/**
 *  根据条件查询订阅记录
 *
 *  @param buyer_id 用户id
 *  @param plan_id  计划id
 *  @param card_id  银行卡记录id
 */
- (void)getSubscriptions:(NSString *)buyer_id plan_id:(NSString *)plan_id card_id:(NSString *)card_id;

@end
