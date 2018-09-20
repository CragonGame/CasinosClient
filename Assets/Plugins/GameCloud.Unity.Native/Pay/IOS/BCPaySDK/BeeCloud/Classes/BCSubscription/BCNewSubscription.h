//
//  BCSubNewScription.h
//  BCPay
//
//  Created by Ewenlong03 on 16/8/8.
//  Copyright © 2016年 BeeCloud. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "BCSubscription.h"

@interface BCNewSubscription : NSObject
/**
 *  订阅的buyer ID，可以是用户email，也可以是商户系统中的用户ID；必填
 */
@property (nonatomic, retain) NSString *buyer_id;
/**
 *  创建的订阅计划ID；必填
 */
@property (nonatomic, retain) NSString *plan_id;
/**
 *  短信验证码id,使用[BCSubscription smsReq:(phone)]获得; 必填
 */
@property (nonatomic, retain) NSString *sms_id;
/**
 *  短信验证码,使用[BCSubscription smsReq:(phone)]获得; 必填
 */
@property (nonatomic, retain) NSString *sms_code;
/**
 *  订阅用户之前绑定过的银行卡id，这个参数会在订阅成功后返回，商户可以与buyer_id关联后存储在商户系统中
 *  card_id 与 {bank_name, card_no, id_name, id_no, mobile} 二者必填其一
 */
@property (nonatomic, retain) NSString *card_id;
/**
 *  用户银行卡银行名称，通过[BCSubscription subscriptionBanks];获得支持的银行
 */
@property (nonatomic, retain) NSString *bank_name;
/**
 *  用户银行卡号
 */
@property (nonatomic, retain) NSString *card_no;
/**
 *  用户真实姓名
 */
@property (nonatomic, retain) NSString *id_name;
/**
 *  用户身份证号
 */
@property (nonatomic, retain) NSString *id_no;
/**
 *  用户银行卡绑定的手机号
 */
@property (nonatomic, retain) NSString *mobile;
/**
 *  订阅的计划数量，默认1.0
 */
@property (nonatomic, assign) double amount;
/**
 *  可以试用到的日期，时间戳，默认0
 */
@property (nonatomic, assign) long trial_end;
/**
 *  商户扩展参数
 */
@property (nonatomic, retain) NSMutableDictionary *optional;
/**
 *  创建新的订阅
 */
- (void)newSubscription;

@end
