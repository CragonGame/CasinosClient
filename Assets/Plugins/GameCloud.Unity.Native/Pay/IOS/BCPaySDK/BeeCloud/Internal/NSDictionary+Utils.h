//
//  NSDictionary+Utils.h
//  BCPay
//
//  Created by Ewenlong03 on 15/9/25.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface NSDictionary (Utils)

- (NSInteger)integerValueForKey:(NSString*)key defaultValue:(NSInteger)defaultValue;
- (long long)longlongValueForKey:(NSString*)key defaultValue:(long long)defaultValue;
- (float)floatValueForKey:(NSString*)key defaultValue:(float)defaultValue;
- (BOOL)boolValueForKey:(NSString*)key defaultValue:(BOOL)defaultValue;
- (NSString *)stringValueForKey:(NSString*)key defaultValue:(NSString*)defaultValue;
- (NSArray *)arrayValueForKey:(NSString *)key defaultValue:(NSArray *)defaultValue;
- (NSDictionary *)dictValueForKey:(NSString *)key defaultValue:(NSDictionary *)defaultValue;

- (BOOL)valueForKeyIsArray:(NSString*)key;
- (BOOL)valueForKeyIsString:(NSString*)key;
- (BOOL)valueForKeyIsNumber:(NSString*)key;

@end
