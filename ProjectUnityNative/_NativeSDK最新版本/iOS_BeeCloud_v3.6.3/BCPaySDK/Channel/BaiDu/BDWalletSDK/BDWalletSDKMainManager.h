//
//  BDWalletSDKMainManager.h
//  BaiduWalletSDK
//
//  Created by lushuang on 14-3-13.
//  Copyright (c) 2014年 baidu. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>


// 登录类型
typedef enum BDWalletSDK_LoginType
{
    BDWalletSDK_Login_Type_BDUSS        = 0,    // 登录类型 BDUSS 由pass登录返回的
    BDWalletSDK_Login_Type_ACCESS_TOKEN = 1,    // 登录类型 ACCESS_TOKEN，由开发平台返回的
}BDWalletSDKLoginType;

// 进入SDK返回的错误类型
typedef enum BDWalletSDK_Error_Type {
    BDWalletSDK_Error_None              = 0,// 无错
    BDWalletSDK_Error_Net               = 1,// 网络异常
    BDWalletSDK_Error_OrderInfo         = 2,// orderInfo异常
    BDWalletSDK_Error_InvalidDelegate   = 3,// 传入无效Delegate
    BDWalletSDK_Error_Unlogin           = 4,// 未登录
    BDWalletSDK_Error_Other             = 5,// 其他未知错误
}BDWalletSDKErrorType;


@protocol BDWalletSDKMainManagerDelegate <NSObject>
@required

// 统计接口
- (void)logEventId:(NSString*)eventId eventDesc:(NSString*)eventDesc;

@optional

// 应用版本号
- (NSString*)getAppVersion;
// 支付回调接口
-(void)BDWalletPayResultWithCode:(int)statusCode payDesc:(NSString*)payDescs;

@end


@interface BDWalletSDKMainManager : NSObject
// 可选参数
@property (nonatomic, assign) BOOL leaveBDWalletSDKAnimate;// 离开sdk是否需要动画,默认为YES

// statusBarStyle
@property (assign, nonatomic)UIStatusBarStyle statusBarStyle;
@property (assign, nonatomic)UIStatusBarStyle oldStatusBarStyle;

// navgationBar
@property (nonatomic, strong) UIColor *bdWalletSDKNavColor;// nav背景色
@property (nonatomic, strong) UIColor *bdWalletNavTitleColor;// title颜色
@property (nonatomic, strong) UIImage *bdWalletNavBackNormalImage;// 返回键Normal
@property (nonatomic, strong) UIImage *bdWalletNavBackHighlightImage;// 返回键highlight
@property (nonatomic, strong) UIImage *bdWalletNavBgImage;// navbar背景
@property(nonatomic, assign)BOOL isHomePresentModel;  //钱包首页仅用Present打开

// 必须设置的参数
@property (atomic, weak) id<BDWalletSDKMainManagerDelegate> delegate;// delegate 需要是一个 viewController

// 接口
+(BDWalletSDKMainManager*)getInstance;

// 支付接口,delegateT需要是一个viewController
-(BDWalletSDKErrorType)doPayWithOrderInfo:(NSString*)orderInfo params:(NSDictionary*)params delegate:(id<BDWalletSDKMainManagerDelegate>)delegateT;


@end



