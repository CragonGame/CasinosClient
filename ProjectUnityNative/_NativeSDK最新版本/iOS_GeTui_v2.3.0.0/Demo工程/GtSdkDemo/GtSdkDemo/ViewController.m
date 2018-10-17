//
//  ViewController.m
//  Demo
//
//  Created by CoLcY on 11-12-29.
//  Copyright (c) 2011年 Gexin Interactive (Beijing) Network Technology Co.,LTD. All rights reserved.
//

#import "AppDelegate.h"
#import "TestViewController.h"
#import "ViewController.h"

@implementation ViewController

- (void)viewDidLoad {
    [super viewDidLoad];

    mResponseView.editable = NO;
    if ([[[UIDevice currentDevice] systemVersion] floatValue] >= 7.0) {
        mResponseView.layoutManager.allowsNonContiguousLayout = NO;
    }

    [mAppIDView setText:kGtAppId];
    [mAppKeyView setText:kGtAppKey];
    [mAppSecretView setText:kGtAppSecret];

    NSString *offPushMode = [[NSUserDefaults standardUserDefaults] objectForKey:@"OffPushMode"];
    if (offPushMode) {
        BOOL flg = [offPushMode intValue] == 1;
        [self updateModeOffButton:flg];
    }
}

- (void)viewDidAppear:(BOOL)animated {
    [super viewDidAppear:animated];
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
}

- (void)dealloc {
    NSLog(@"%@ %@", NSStringFromClass(self.class), @"dealloc");
}

#pragma mark - 页面显示

- (void)updateStatusView:(AppDelegate *)delegate {
    if ([GeTuiSdk status] == SdkStatusStarted) {
        [mStatusView setText:@"已启动"];
        [mStartupView setTitle:@"停止" forState:UIControlStateNormal];
        [mClientIDView setText:[GeTuiSdk clientId]];
    } else if ([GeTuiSdk status] == SdkStatusStarting) {
        [mStatusView setText:@"正在启动"];
        [mStartupView setTitle:@"正在启动" forState:UIControlStateNormal];
        [mClientIDView setText:@""];
    } else {
        [mStatusView setText:@"已停止"];
        [mStartupView setTitle:@"启动" forState:UIControlStateNormal];
        [mClientIDView setText:@""];
    }
}

- (void)updateModeOffButton:(BOOL)isModeOff {
    if (isModeOff) {
        [pushStatusLabel setText:@"关闭"];
        [offPushButton setTitle:@"开启推送" forState:UIControlStateNormal];
    } else {
        [pushStatusLabel setText:@"开启"];
        [offPushButton setTitle:@"关闭推送" forState:UIControlStateNormal];
    }
    isModeOff_ = isModeOff;
    [[NSUserDefaults standardUserDefaults] setObject:isModeOff_ ? @"1" : @"0" forKey:@"OffPushMode"];
}

#pragma mark - 页面按钮点击事件

/// 点击关闭推送按钮
- (IBAction)onSetModeButton:(id)sender {
    if ([GeTuiSdk status] == SdkStatusStarted) {
        [GeTuiSdk setPushModeForOff:!isModeOff_];
    } else {
        [self logMsg:@"GeTuiSDK未启动"];
    }
}

/// 点击开始/关闭按钮
- (IBAction)onStartupOrStop:(id)sender {
    AppDelegate *delegate = (AppDelegate *) [[UIApplication sharedApplication] delegate];
    if ([GeTuiSdk status] == SdkStatusStoped) {
        [GeTuiSdk startSdkWithAppId:kGtAppId appKey:kGtAppKey appSecret:kGtAppSecret delegate:delegate];
    } else if ([GeTuiSdk status] == SdkStatusStarted || [GeTuiSdk status] == SdkStatusStarting) {
        [GeTuiSdk destroy]; // 停止SDK
    }
}

/// 点击清空日志按钮
- (IBAction)onClearLog:(id)sender {
    [mResponseView setText:nil];
    [mResponseView scrollRangeToVisible:NSMakeRange([mResponseView.text length], 0)];
}

/// 点击更多按钮
- (IBAction)onTestMore:(id)sender {
    UIViewController *funcsView = [[TestViewController alloc] initWithNibName:@"TestViewController" bundle:nil];
    [self.navigationController pushViewController:funcsView animated:YES];
}

/// 关闭键盘
- (IBAction)didKeyword:(id)sender {
    [self.view endEditing:YES];
}

#pragma mark - 日志

- (void)logMsg:(NSString *)aMsg {
    NSString *response = [NSString stringWithFormat:@"%@\n%@", mResponseView.text, aMsg];
    [mResponseView setText:response];

    [mResponseView scrollRangeToVisible:NSMakeRange([mResponseView.text length], 0)];
}

@end
