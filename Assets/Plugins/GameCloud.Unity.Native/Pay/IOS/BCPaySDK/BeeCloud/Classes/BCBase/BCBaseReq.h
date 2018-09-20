//
//  BCBaseReq.h
//  BCPaySDK
//
//  Created by Ewenlong03 on 15/7/27.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "BCPayConstant.h"

#pragma mark BCBaseReq
/**
 *  所有请求事件的基类,具体请参考BCRequest目录
 */
@interface BCBaseReq : NSObject
/**
 *  请求事件类型
 */
@property (nonatomic, assign) BCObjsType type;//100

@end
