//
//  GetuiForUnity.h
//  Unity3D
//
//  Created by dazq on 16/7/1.
//  Copyright © 2016年 dzq. All rights reserved.
//

#import "GeTuiSdk.h"
#import <Foundation/Foundation.h>
#import <PushKit/PushKit.h>

#if defined(__cplusplus)
extern "C" {
#endif
extern void UnitySendMessage(const char *, const char *, const char *);
#if defined(__cplusplus)
}
#endif

@interface GetuiForUnity : NSObject <GeTuiSdkDelegate,PKPushRegistryDelegate>

+ (void)sendU3dMessage:(NSString *)messageName param:(id)dict;
- (void)setListenerGameObject:(NSString *)GameObjectName;
+ (void)registerUserNotification;
@end
