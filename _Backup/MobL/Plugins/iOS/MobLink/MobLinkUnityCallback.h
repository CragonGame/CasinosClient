//
//  MobLinkUnityCallback.h
//  Unity-iPhone
//
//  Created by 陈剑东 on 2017/4/17.
//
//

#import <Foundation/Foundation.h>
#import <MobLink/MobLink.h>

@interface MobLinkUnityCallback : NSObject <IMLSDKRestoreDelegate>

+ (MobLinkUnityCallback *)defaultCallBack;

@end
