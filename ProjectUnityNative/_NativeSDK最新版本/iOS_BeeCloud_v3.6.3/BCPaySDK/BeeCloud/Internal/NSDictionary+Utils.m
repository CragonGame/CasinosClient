//
//  NSDictionary+Utils.m
//  BCPay
//
//  Created by Ewenlong03 on 15/9/25.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "NSDictionary+Utils.h"

@implementation NSDictionary (Utils)

/**
 * @brief 如果akey找不到，返回@"" (防止出现nil，使程序崩溃)
 *
 * @param aKey 字典key值
 *
 * @return 字典value
 */

/**
 *  获取字典指定的key的数值字符
 *
 *  @param aKey key
 *
 *  @return  value值如果为nil或者null会返回0字符串
 */
- (NSInteger)integerValueForKey:(NSString*)key defaultValue:(NSInteger)defaultValue {
    if ([self valueForKeyIsNumber:key]) {
        return [[self valueForKey:key] integerValue];
    }
    return defaultValue;
}
- (long long)longlongValueForKey:(NSString*)key defaultValue:(long long)defaultValue {
    if ([self valueForKeyIsNumber:key]) {
        return [[self valueForKey:key] longLongValue];
    }
    return defaultValue;
}
- (float)floatValueForKey:(NSString*)key defaultValue:(float)defaultValue {
    if ([self valueForKeyIsNumber:key]) {
        return [[self valueForKey:key] floatValue];
    }
    return defaultValue;
}

- (BOOL)boolValueForKey:(NSString*)key defaultValue:(BOOL)defaultValue {
    if ([self valueForKeyIsNumber:key]) {
        return [[self valueForKey:key] boolValue];
    }
    return defaultValue;
}

- (NSString *)stringValueForKey:(NSString*)key defaultValue:(NSString*)defaultValue {
    if ([self valueForKeyIsString:key]) {
        return [self valueForKey:key];
    }
    return defaultValue;
}

- (NSArray *)arrayValueForKey:(NSString *)key defaultValue:(NSArray *)defaultValue {
    if ([self valueForKeyIsArray:key]) {
        return [self valueForKey:key];
    }
    return defaultValue;
}

- (NSDictionary *)dictValueForKey:(NSString *)key defaultValue:(NSDictionary *)defaultValue {
    if ([self valueForKeyIsNSDictionary:key]) {
        return [self valueForKey:key];
    }
    return defaultValue;
}

///**
// * @brief 替换&nbsp;为空
// *
// * @param aKey 字典key值
// *
// * @return 字典value
// */
//
//- (NSString *)replaceNBSPforKey:(id)aKey {
//    NSString *value = [self objectForKey:aKey];
//    
//    if (!value) {
//        value = @"";
//    }
//    NSString* str = [value stringByReplacingOccurrencesOfString:@"&nbsp;" withString:@" "] ;
//    
//    return [NSString stringWithFormat:@"%@",str];
//}

- (BOOL)valueForKeyIsArray:(NSString *)key {
    return [self valueForKeyIsType:key type:[NSArray class]];
}

- (BOOL)valueForKeyIsString:(NSString *)key {
    return [self valueForKeyIsType:key type:[NSString class]];
}

- (BOOL)valueForKeyIsNumber:(NSString *)key {
    return [self valueForKeyIsType:key type:[NSNumber class]];
}

- (BOOL)valueForKeyIsNSDictionary:(NSString *)key {
    return [self valueForKeyIsType:key type:[NSDictionary class]];
}

- (BOOL)valueForKeyIsType:(NSString *)key type:(Class)kClass {
    id value = [self objectForKey:key];
    if (value == nil || [value isKindOfClass:[NSNull class]] || ![value isKindOfClass:kClass]) {
        return NO;
    }
    return YES;
}

@end
