//
//  AppDelegate.m
//  BeeCloudDemo
//
//  Created by RInz on 15/2/5.
//  Copyright (c) 2015年 RInz. All rights reserved.
//

#import "AppDelegate.h"
#import "BeeCloud.h"


@interface AppDelegate ()

@end

@implementation AppDelegate

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions {

    /*
     如果使用BeeCloud控制台的APP Secret初始化，代表初始化生产环境；
     如果使用BeeCloud控制台的Test Secret初始化，代表初始化沙箱测试环境;
     测试账号 appid: c5d1cba1-5e3f-4ba0-941d-9b0a371fe719
     appSecret: 39a7a518-9ac8-4a9e-87bc-7885f33cf18c
     testSecret: 4bfdd244-574d-4bf3-b034-0c751ed34fee
     由于支付宝的政策原因，测试账号的支付宝支付不能在生产环境中使用，带来不便，敬请原谅！
     ApplePay 不支持模拟器运行；
     */ 
    [BeeCloud initWithAppID:@"c5d1cba1-5e3f-4ba0-941d-9b0a371fe719" andAppSecret:@"4bfdd244-574d-4bf3-b034-0c751ed34fee"];
//    [BeeCloud initWithAppID:@"c5d1cba1-5e3f-4ba0-941d-9b0a371fe719" andAppSecret:@"4bfdd244-574d-4bf3-b034-0c751ed34fee" sandbox:YES];
    
    //初始化微信官方APP支付
    //此处的微信appid必须是在微信开放平台创建的移动应用的appid，且必须与在『BeeCloud控制台-》微信APP支付』配置的"应用APPID"一致，否则会出现『跳转到微信客户端后只显示一个确定按钮的现象』。
    [BeeCloud initWeChatPay:@"wxf1aa465362b4c8f1"];
    
    //初始化BC微信APP支付
//    [BeeCloud initBCWXPay:@"wxf1aa465362b4c8f1"];
    
    //初始化PayPal
    [BeeCloud initPayPal:@"AVT1Ch18aTIlUJIeeCxvC7ZKQYHczGwiWm8jOwhrREc4a5FnbdwlqEB4evlHPXXUA67RAAZqZM0H8TCR"
                  secret:@"EL-fkjkEUyxrwZAmrfn46awFXlX-h2nRkyCVhhpeVdlSRuhPJKXx3ZvUTTJqPQuAeomXA8PZ2MkX24vF"
                 sandbox:YES];
    return YES;
}

- (BOOL)application:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(NSString *)sourceApplication annotation:(id)annotation {
    if (![BeeCloud handleOpenUrl:url]) {
        //handle其他类型的url
    }
    return YES;
}

//iOS9之后官方推荐用此方法
- (BOOL)application:(UIApplication *)app openURL:(NSURL *)url options:(NSDictionary<NSString *,id> *)options {
    NSLog(@"options %@", options);
    if (![BeeCloud handleOpenUrl:url]) {
        //handle其他类型的url
    }
    return YES;
}

@end
