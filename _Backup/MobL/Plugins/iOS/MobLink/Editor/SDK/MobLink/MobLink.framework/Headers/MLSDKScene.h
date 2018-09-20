//
//  MLSDKScene.h
//  MobLink
//
//  Created by chenjd on 16/11/14.
//  Copyright © 2016年 Mob. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface MLSDKScene : NSObject

/**
 路径
 */
@property (nonatomic, copy) NSString *path;

/**
 来源标识
 */
@property (nonatomic, copy) NSString *source;

/**
 自定义参数
 */
@property (nonatomic, strong)  NSDictionary *params;

/**
 MobId
 */
@property (nonatomic, copy, readonly) NSString *mobid;

/**
 场景信息初始化

 @param path 路径,应传入需要恢复的控制器所设定的路径,即控制器在实现UIViewController+MLSDKRestore里面的+[MLSDKPath]时所返回的值。
 @param source 来源标识
 @param params 自定义参数,可传入自定义键值对
 @return 场景对象
 */
- (instancetype)initWithMLSDKPath:(NSString *)path source:(NSString *)source params:(NSDictionary *)params;

@end
