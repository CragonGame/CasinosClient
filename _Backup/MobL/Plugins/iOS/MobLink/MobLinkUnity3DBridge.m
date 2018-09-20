//
//  MobLinkUnity3DBridge.m
//  Unity-iPhone
//
//  Created by 陈剑东 on 2017/4/17.
//
//

#import "MobLinkUnity3DBridge.h"
#import "MobLinkUnityCallback.h"
#import <MobLink/MobLink.h>
#import <MobLink/MLSDKScene.h>
#import <MOBFoundation/MOBFJson.h>

#import "UnityAppController.h"

static MobLinkUnityCallback *_callback = nil;
#if defined (__cplusplus)
extern "C" {
#endif

    extern void __iosMobLinkGetMobId (void *scenepath, void *source, void *params);
    
    void __iosMobLinkGetMobId (void *path, void *source, void *params)
    {
        NSString *thePath = [NSString stringWithCString:path encoding:NSUTF8StringEncoding];
        NSString *theSource = [NSString stringWithCString:source encoding:NSUTF8StringEncoding];
        NSString *theParamsStr = [NSString stringWithCString:params encoding:NSUTF8StringEncoding];
        NSDictionary *theParams = [MOBFJson objectFromJSONString:theParamsStr];

        MLSDKScene *scene = [[MLSDKScene alloc] initWithMLSDKPath:thePath source:theSource params:theParams];
        [MobLink getMobId:scene result:^(NSString *mobid) {
            
            NSString *str  = @"";
            if (mobid)
            {
                NSDictionary *result = @{@"mobid" : mobid};
                str = [MOBFJson jsonStringFromObject:result];
            }
            
            UnitySendMessage([@"MobLink" UTF8String], "_MobIdCallback", [str UTF8String]);
        }];
        
    }
    
#if defined (__cplusplus)
}
#endif
@implementation MobLinkUnity3DBridge

@end
