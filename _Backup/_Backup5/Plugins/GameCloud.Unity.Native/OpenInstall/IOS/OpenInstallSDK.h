//
//  OpenInstallSDK.h
//  OpenInstallSDK
//
//  Created by toby on 16/8/11.
//  Copyright © 2016年 toby. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "OpeninstallData.h"

@protocol OpenInstallDelegate<NSObject>

@optional

#pragma mark - 请使用最新API：<code>getInstallParmsCompleted</code>
/**
 * 安装时获取h5页面动态参数（如果是渠道链接，渠道编号会一起返回）
 * @param params 动态参数
 * @param error 错误回调
 */
- (void)getInstallParamsFromOpenInstall:(nullable NSDictionary *)params withError:(nullable NSError *)error;//已废弃


///----------------------
/// @name 一键拉起的回调方法
///----------------------

#pragma mark - 请使用最新API：<code>getWakeUpParams</code>
/**
 * 唤醒时获取h5页面动态参数（如果是渠道链接，渠道编号会一起返回）
 * @param params 动态参数
 * @param error 错误回调
 */
- (void)getWakeUpParamsFromOpenInstall:(nullable NSDictionary *)params withError:(nullable NSError *)error;//已废弃


#pragma mark - add in v2.2.0
/**
 * 唤醒时获取h5页面动态参数（如果是渠道链接，渠道编号会一起返回）
 * @param appData 动态参数对象
 */
- (void)getWakeUpParams:(nullable OpeninstallData *)appData;

@end



@interface OpenInstallSDK : NSObject


/**
 * SDK单例,returns a previously instantiated singleton instance of the API.
 */
+(instancetype _Nullable )defaultManager;


///-------------
/// @name 初始化
///-------------


#pragma mark - added in v2.2.0 请在Info.plist中配置应用的appkey

/**
 * 初始化OpenInstall SDK
 * ***调用该方法前，需在Info.plist文件中配置键值对,键为com.openinstall.APP_KEY不能修改，值为相应的应用的appKey，可在openinstall官方后台查看***
 
 <key>com.openinstall.APP_KEY</key>
 <string>你的appKey</string>
 
 * @param delegate 委托方法所在的类的对象
 */
+(void)initWithDelegate:(id _Nonnull)delegate;


#pragma mark - Deprecated in v2.2.0（已废弃）

/**
 * 初始化OpenInstall SDK (已废弃,请参考方法 initwithDelegate: 已使用该初始化方法的用户也可继续使用)
 *
 * @param appKey 控制中心创建应用获取appKey
 * @param delegate 委托方法(getInstallParamsFromOpenInstall和 getWakeUpParamsFromOpenInstall)所在的类的对象
 */
+(void)setAppKey:(nonnull NSString *)appKey withDelegate:(nullable id)delegate __deprecated_msg("Deprecated in v2.2.0，请参考方法<code>initwithDelegate</code>");


///----------------------
/// @name 获取安装的动态参数
///----------------------


#pragma mark - added in v2.2.0

/**
 * 开发者在需要获取用户安装app后由web网页传递过来的”动态参数“（如邀请码、游戏房间号，渠道编号等）时调用该方法,可第一时间返回数据，可在任意位置调用
 *
 * 默认回调超时时间为5秒(s)，如无特殊需求，请用此方法，否则可使用高级API
 *
 * @param completedBlock 回调block，在主线程（UI线程）回调
 *
 * @discussion
 * ***开发者不要自行保存参数!!!如果获取动态参数成功，SDK会把参数保存在本地***
 * ***该方法可重复获取参数，如需在首次安装才获取安装参数，请自行判断***
 */
-(void)getInstallParmsCompleted:(void (^_Nullable)(OpeninstallData*_Nullable appData))completedBlock;

/**
 * 开发者在需要获取用户安装app后由web网页传递过来的”动态参数“（如邀请码、游戏房间号，渠道编号等）时调用该方法,可第一时间返回数据，可在任意位置调用
 *
 * @param timeoutInterval 可设置回调超时时间，单位秒(s)
 * @param completedBlock 回调block，在主线程（UI线程）回调
 *
 * @discussion
 * ***开发者不要自行保存参数!!!如果获取动态参数成功，SDK会把参数保存在本地***
 * ***该方法可重复获取参数，如需在首次安装才获取安装参数，请自行判断***
 */
-(void)getInstallParmsWithTimeoutInterval:(NSTimeInterval)timeoutInterval
                                completed:(void (^_Nullable)(OpeninstallData*_Nullable appData))completedBlock;



#pragma mark - Deprecated in v2.1.1（已废弃）
/**
 * 开发者直接获取动态参数,初始化之后调用（参数的正常获取时间1-2秒内）
 * @return NSDictionary
 */
+(NSDictionary*_Nullable)getOpenInstallParams __deprecated_msg("Deprecated in v2.1.1，请参考方法<code>getInstallParmsCompleted</code>");


///---------------------
/// @name 一键拉起回调处理
///---------------------

/**
 * 处理 URI schemes
 * @param URL 系统回调传回的URL
 * @return bool URL是否被OpenInstall识别
 */
+(BOOL)handLinkURL:(NSURL *_Nullable)URL;


/**
 * 处理 通用链接
 * 通过 Universal Link 启动应用时会调用 application:(UIApplication *)application continueUserActivity:(NSUserActivity *)userActivity restorationHandler:(void(^)(NSArray * __nullable
 restorableObjects))restorationHandler ,在此方法中调用  [OpenInstallSDK continueUserActivity:userActivity]
 * @param userActivity 存储了页面信息，包括url
 * @return bool URL是否被OpenInstall识别
 */
+(BOOL)continueUserActivity:(NSUserActivity*_Nullable)userActivity;



///--------------
/// @name 统计相关
///--------------


/**
 * 注册量统计
 *
 * 使用openinstall 控制中心提供的渠道统计时，在App用户注册完成后调用，可以统计渠道注册量。
 * 必须在注册成功的时再调用该方法，避免重复调用，否则可能导致注册统计不准
 */
+(void)reportRegister;


#pragma mark - added in v2.2.0
/**
 * 渠道效果统计
 *
 * 目前SDK采用定时上报策略，时间间隔由服务器控制
 * e.g.可统计用户支付消费情况,点击次数等
 *
 * @param effectID 效果点ID
 * @param effectValue 效果点值（如果是人民币金额，请以分为计量单位）
 */
-(void)reportEffectPoint:(NSString *_Nonnull)effectID effectValue:(long)effectValue;


@end

