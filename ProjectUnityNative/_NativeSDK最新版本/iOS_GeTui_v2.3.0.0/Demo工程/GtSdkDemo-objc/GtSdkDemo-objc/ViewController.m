//
//  ViewController.m
//  GtSdkDemo
//
//  Created by 赵伟 on 15/9/28.
//  Copyright © 2015年 赵伟. All rights reserved.
//

#import "AppDelegate.h"
#import "ViewController.h"

@interface ViewController ()

@end

@implementation ViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view, typically from a nib.
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (IBAction)clickActionButton:(UIButton *)sender {
    switch (sender.tag) {
        case 20: { // 启动/停止

            if ([GeTuiSdk status] == SdkStatusStoped) {
                AppDelegate *delegate = (AppDelegate *) [[UIApplication sharedApplication] delegate];
                [GeTuiSdk startSdkWithAppId:kGtAppId appKey:kGtAppKey appSecret:kGtAppSecret delegate:delegate];

                NSLog(@"\n\n>>>[GeTui]:%@\n\n", @"启动APP");
            } else if ([GeTuiSdk status] == SdkStatusStarted) {
                [GeTuiSdk destroy];

                NSLog(@"\n\n>>>[GeTui]:%@\n\n", @"停止APP");
            }

            break;
        }
        case 21: { // 获取状态
            if ([GeTuiSdk status] == SdkStatusStarted) {
                NSLog(@"\n\n>>>[GeTui status]:%@\n\n", @"启动");
            } else if ([GeTuiSdk status] == SdkStatusStarting) {
                NSLog(@"\n\n>>>[GeTui status]:%@\n\n", @"正在启动");
            } else if ([GeTuiSdk status] == SdkStatusStoped) {
                NSLog(@"\n\n>>>[GeTui status]:%@\n\n", @"停止");
            }

            break;
        }
        case 22: { // 获取CID
            NSLog(@"\n\n>>>[GeTui clientId]:%@\n\n", [GeTuiSdk clientId]);

            break;
        }
        case 23: { // 获取个推SDK版本
            NSLog(@"\n\n>>>[GeTui version]:%@\n\n", [GeTuiSdk version]);

            break;
        }
        case 24: { // 注册DeviceToken
            [GeTuiSdk registerDeviceToken:nil];

            NSLog(@"调用注册DeviceToken");
            break;
        }
        case 25: { // 设置标签

            NSString *tagName = @"个推,推送,iOS";
            NSArray *tagNames = [tagName componentsSeparatedByString:@","];
            if (![GeTuiSdk setTags:tagNames]) {
                UIAlertView *alertView = [[UIAlertView alloc] initWithTitle:@"Failed" message:@"设置失败" delegate:nil cancelButtonTitle:@"OK" otherButtonTitles:nil];
                [alertView show];
            }

            NSLog(@"调用设置标签");
            break;
        }
        case 26: { // 绑定别名
            [GeTuiSdk bindAlias:@"个推" andSequenceNum:@"805318ab532e18c6c"];

            NSLog(@"调用绑定别名");
            break;
        }
        case 27: { // 取消绑定别名
            [GeTuiSdk unbindAlias:@"个推" andSequenceNum:@"805318ab532e18c6c" andIsSelf:YES];

            NSLog(@"调用取消绑定别名");
            break;
        }
        case 28: { // 发送消息
            NSString *content = @"测试个推发消息功能";
            NSError *err = nil;
            [GeTuiSdk sendMessage:[content dataUsingEncoding:NSUTF8StringEncoding] error:&err];
            if (err) {
                UIAlertView *alertView = [[UIAlertView alloc] initWithTitle:@"Failed" message:[NSString stringWithFormat:@"send failed:%@", [err localizedDescription]] delegate:nil cancelButtonTitle:@"OK" otherButtonTitles:nil];
                [alertView show];
            }

            NSLog(@"调用发送消息");
            break;
        }
        case 29: {
            int badgeValue = 1;
            [GeTuiSdk setBadge:badgeValue];
            [[UIApplication sharedApplication] setApplicationIconBadgeNumber:badgeValue];
            break;
        }
        case 30: {
            [GeTuiSdk resetBadge];
            [[UIApplication sharedApplication] setApplicationIconBadgeNumber:0];
            break;
        }


        default:
            break;
    }
}


@end
