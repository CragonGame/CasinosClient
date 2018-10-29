//
//  CustomAppController.m
//  Unity-iPhone
//
//  Created by cooper on 2018/6/15.
//

#import "UnityAppController.h"
#import "OpenInstallSDK.h"
#import "OpenInstallUnity3DCallBack.h"


@interface CustomAppController : UnityAppController

@end
//如果用户使用了IMPL_APP_CONTROLLER_SUBCLASS宏生成自己的customAppController,请删除该文件，并在自己的customAppController中添加如下初始化和拉起回调
IMPL_APP_CONTROLLER_SUBCLASS(CustomAppController)

@implementation CustomAppController

-(BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions{
    
    [OpenInstallSDK initWithDelegate:[OpenInstallUnity3DCallBack defaultManager]];
    
    [super application:application didFinishLaunchingWithOptions:launchOptions];
    
    return YES;
}

#pragma mark------如果用户有使用该方法，请在将下面注释打开：[super application:application continueUserActivity:userActivity restorationHandler:restorationHandler];
-(BOOL)application:(UIApplication *)application continueUserActivity:(NSUserActivity *)userActivity restorationHandler:(void (^)(NSArray * _Nullable))restorationHandler{
    
    if ([OpenInstallSDK continueUserActivity:userActivity]) {
        
        return YES;
    }
    //    [super application:application continueUserActivity:userActivity restorationHandler:restorationHandler];
    return YES;
}
#pragma mark------如果用户使用了iOS9.0新API<code>application:openURL:options:</code>，请在新API中添加if ([OpenInstallSDK handLinkURL:url]) return YES;回调判断
-(BOOL)application:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(NSString *)sourceApplication annotation:(id)annotation{
    
    //判断是否通过OpenInstall URL Scheme 唤起App
    if  ([OpenInstallSDK handLinkURL:url]){//必写
        return YES;
    }
    [super application:application openURL:url sourceApplication:sourceApplication annotation:annotation];
    
    //其他第三方回调；
    return YES;
}

@end
