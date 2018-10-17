//
//  AppDelegate.h
//  GtSdkDemo
//
//  Created by 赵伟 on 15/9/28.
//  Copyright © 2015年 赵伟. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <PushKit/PushKit.h> //VOIP支持需要导入PushKit库,实现 PKPushRegistryDelegate
#import <GTSDK/GeTuiSdk.h>

#define kGtAppId           @"iMahVVxurw6BNr7XSn9EF2"
#define kGtAppKey          @"yIPfqwq6OMAPp6dkqgLpG5"
#define kGtAppSecret       @"G0aBqAD6t79JfzTB6Z5lo5"

@interface AppDelegate : UIResponder <UIApplicationDelegate, PKPushRegistryDelegate, GeTuiSdkDelegate>

@property (strong, nonatomic) UIWindow *window;


@end

