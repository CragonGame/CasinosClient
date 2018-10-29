//
//  NSString+NSString_IsValid.m
//  BCPay
//
//  Created by Ewenlong03 on 15/8/28.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "NSString+IsValid.h"

@implementation NSString (IsValid)

- (BOOL)isValid {
    if (self == nil || (NSNull *)self == [NSNull null] || self.length == 0 ) return NO;
    return YES;
}

- (BOOL)isValidUUID {
    if (!self.isValid || self.length != 36) return NO;
    for (NSUInteger i = 0; i < self.length; i++) {
        unichar ch = [self characterAtIndex:i];
        if (i == 8 || i == 13 || i == 18 || i == 23) {
            if (ch != '-')
                return NO;
        } else {
            if (!([BCPayUtil isDigit:ch] || (ch >= 'a' && ch <= 'f') || (ch >= 'A' && ch <= 'F')))
                return NO;
        }
    }
    return YES;
}

- (BOOL)isValidTraceNo {
    if (!self.isValid) return NO;
    for (NSUInteger i = 0; i < self.length; i++) {
        unichar ch = [self characterAtIndex:i];
        // Invalid character.
        if (![BCPayUtil isLetter:ch] && ![BCPayUtil isDigit:ch]) return NO;
    }
    return YES;
}

- (BOOL)isPureInt {
    NSScanner *scan = [NSScanner scannerWithString:self];
    int val;
    return [scan scanInt:&val] && [scan isAtEnd];
}

- (BOOL)isPureFloat {
    NSScanner *scan = [NSScanner scannerWithString:self];
    float val;
    return [scan scanFloat:&val] && [scan isAtEnd];
}

- (BOOL)isValidMobile {
    NSString *phoneRegex = @"^([0|86|17951]?(13[0-9])|(15[^4,\\D])|(17[678])|(18[0,0-9]))\\d{8}$";
    NSPredicate *phoneTest = [NSPredicate predicateWithFormat:@"SELF MATCHES %@",phoneRegex];
    return [phoneTest evaluateWithObject:self];
}

@end
