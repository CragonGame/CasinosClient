//
//  NSString+NSString_IsValid.h
//  BCPay
//
//  Created by Ewenlong03 on 15/8/28.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "BCPayUtil.h"

@interface NSString (IsValid)
- (BOOL)isValid;
- (BOOL)isPureInt;
- (BOOL)isValidTraceNo;
- (BOOL)isPureFloat;
- (BOOL)isValidUUID;
- (BOOL)isValidMobile;

@end
