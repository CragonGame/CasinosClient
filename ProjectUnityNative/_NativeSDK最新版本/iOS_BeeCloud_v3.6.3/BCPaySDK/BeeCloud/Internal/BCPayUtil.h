//
//  BCPayUtil.h
//  BCPay
//
//  Created by Ewenlong03 on 15/7/9.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "BCNetworking.h"
#import "BCPayConstant.h"
#import "BCPayCache.h"
#import "BCPayObjects.h"

@interface BCPayUtil : NSObject

/** @name util functions*/

/*!
 A wrapper for BCHTTPSessionManager.
 */
+ (BCHTTPSessionManager *)getBCHTTPSessionManager;

/**
 *  Get wrapped parameters in the format of "para" to a map for GET REST APIs.
 *
 *  @param parameters map
 *
 *  @return new map
 */
+ (NSMutableDictionary *)getWrappedParametersForGetRequest:(NSDictionary *) parameters;

/**
 *  prepare parameters
 *
 *  @param block result block
 *
 *  @return default request map
 */
+ (NSMutableDictionary *)prepareParametersForRequest;

/**
 *  生成签名
 *
 *  @param timeStamp 当前时间戳
 *
 *  @return 签名
 */
+ (NSString *)getAppSignature:(NSString *)timeStamp;

/**
 *  获取url的类型，微信或者支付宝
 *
 *  @param url 渠道返回的url
 *
 *  @return 微信或者支付宝
 */
+ (BCPayUrlType)getUrlType:(NSURL *)url;

/**
 *  getBestHost
 *
 *  @param format url
 *
 *  @return url
 */
+ (NSString *)getBestHostWithFormat:(NSString *)format;

/**
 *  get Channel String
 *
 *  @param channel enum PayChannel
 *
 *  @return Channel String
 */
+ (NSString *)getChannelString:(PayChannel)channel;

/**
 *  执行错误回调
 *
 *  @param errMsg 错误信息
 */
+ (BCBaseResp *)doErrorResponse:(NSString *)errMsg;

/**
 *  服务端返回错误，执行错误回调
 *
 *  @param response 服务端返回参数
 */
+ (BCBaseResp *)getErrorInResponse:(NSDictionary *)response;

#pragma mark Util Functions
/**
 *  Generate a random UUID in the format of "550e8400-e29b-41d4-a716-446655440000", in lower case.
 *
 *  @return The generated UUID string in lower case.
 */
+ (NSString *)generateRandomUUID;

/**
 *  Convert int64 (long long) value of millisecond to NSDate.
 *
 *  @param millisecond Millisecond in int64 (long long).
 *
 *  @return NSDate object converted to.
 */
+ (NSDate *)millisecondToDate:(long long)millisecond;

/**
 *  Convert int64 (long long) value of millisecond to @"yyyy-MM-dd HH:mm".
 *
 *  @param millisecond Millisecond in int64 (long long).
 *
 *  @return NSDate object converted to.
 */
+ (NSString *)millisecondToDateString:(long long)millisecond;

/**
 *  Convert NSDate to int64 (long long) for the value of millisecond.
 *
 *  @param date NSDate object to be converted.
 *
 *  @return Number of milliseconds in the format of 1400000000000.
 */
+ (long long)dateToMillisecond:(NSDate *)date;

/**
 *  Convert date String @"yyyy-MM-dd HH:mm" to int64 (long long) for the value of millisecond.
 *
 *  @param date NSDate object to be converted.
 *
 *  @return Number of milliseconds in the format of 1400000000000.
 */
+ (long long)dateStringToMillisecond:(NSString *)string;

/**
 *  Converts a string in the format of "2014-02-25 14:27" to NSDate.
 *
 *  @param string The date string with format defined in kBCDateFormat.
 *
 *  @return NSDate object converted to.
 */
+ (NSDate *)stringToDate:(NSString *)string;

/**
 *  Converts a NSDate to a string in the format of "2014-02-25 14:27:44.037 GMT-08:00", where the time zone corresponds
 *  to your local timezone.
 *
 *  @param date NSDate object to be converted.
 *
 *  @return The date string with format defined in kBCDateFormat.
 */
+ (NSString *)dateToString:(NSDate *)date;

/**
 *  Converts a common string to MD5.
 *
 *  @param string common string.
 *
 *  @return MD5 string.
 */
+ (NSString *)stringToMD5:(NSString *)string;

/**
 *  check the email.
 *
 *  @param email email.
 *
 *  @return YES if it is valid.
 */
+ (BOOL)isValidEmail:(NSString *)email;

/**
 *  check the mobile number
 *
 *  @param mobile mobile number
 *
 *  @return YES if it is valid
 */
+ (BOOL)isValidMobile:(NSString *)mobile;

/**
 *  Check whether a unichar is a letter 'a' to 'z' or 'A' to 'Z'.
 *
 *  @param ch Character of type unichar to be checked.
 *
 *  @return YES if it is a letter; NO otherwise.
 */
+ (BOOL)isLetter:(unichar)ch;

/**
 *  Check whether a unichar is a digit '0' to '9'.
 *
 *  @param ch Character of type unichar to be checked.
 *
 *  @return YES if is a digit; NO otherwise.
 */
+ (BOOL)isDigit:(unichar)ch;

/**
 *  A string's bytes
 *
 *  @param str string
 *
 *  @return the string's bytes
 */
+ (NSUInteger)getBytes:(NSString *)str;

@end

FOUNDATION_EXPORT void BCPayLog(NSString *format,...) NS_FORMAT_FUNCTION(1,2) ;
