//
//  GeTuiExtSdk.h
//  GtExtensionSdk
//
//  Created by gexin on 16/9/14.
//  Copyright © 2016年 getui. All rights reserved.
//
//  GTExtensionSDK-Version:2.1.0

#import <Foundation/Foundation.h>
#import <UserNotifications/UserNotifications.h>

@interface GeTuiExtSdk : NSObject

/**
 *  只统计APNs到达情况，不下载多媒体资源
 */
+ (void)handelNotificationServiceRequest:(UNNotificationRequest *)request withComplete:(void (^)(void))completeBlock __attribute((deprecated("建议使用多媒体接口")));
/**
 *  统计APNs到达情况和多媒体推送支持, 建议使用该接口
 */
+ (void)handelNotificationServiceRequest:(UNNotificationRequest *)request withAttachmentsComplete:(void (^)(NSArray *attachments, NSArray *errors))completeBlock;

/**
 * sdk销毁，资源释放
 */
+ (void)destory;

@end
