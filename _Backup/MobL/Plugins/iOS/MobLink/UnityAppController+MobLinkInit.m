//
//  UnityAppController+MobLinkInit.m
//  Unity-iPhone
//
//  Created by 陈剑东 on 2017/4/21.
//
//

#import "UnityAppController+MobLinkInit.h"
#import <MobLink/MobLink.h>
#import "MobLinkUnityCallback.h"
@implementation UnityAppController (MobLinkInit)

+ (void)initialize
{
    [MobLink setDelegate:[MobLinkUnityCallback defaultCallBack]];
}

@end
