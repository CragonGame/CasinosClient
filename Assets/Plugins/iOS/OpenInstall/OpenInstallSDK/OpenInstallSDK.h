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

#pragma mark - 推荐使用最新API：<code>getInstallParmsCompleted</code>
/**
 * 安装时获取h5页面动态参数（如果是渠道链接，渠道编号会一起返回）
 * @param params 动态参数
 * @param error 错误回调
 * @discussion 老版本sdk升级过来可延用该api
 */
- (void)getInstallParamsFromOpenInstall:(nullable NSDictionary *)params withError:(nullable NSError *)error;


///----------------------
/// @name 一键拉起的回调方法
///----------------------

#pragma mark - 推荐使用最新API：<code>getWakeUpParams</code>
/**
 * 唤醒时获取h5页面动态参数（如果是渠道链接，渠道编号会一起返回）
 * @param params 动态参数
 * @param error 错误回调
 * @discussion 老版本sdk升级过来可延用该api
 */
- (void)getWakeUpParamsFromOpenInstall:(nullable NSDictionary *)params withError:(nullable NSError *)error;


#pragma mark - add in v2.2.0
/**
 * 唤醒时获取h5页面动态参数（如果是渠道链接，渠道编号会一起返回）
 * @param appData 动态参数对象
 */
- (void)getWakeUpParams:(nullable OpeninstallData *)appData;

@end



@interface OpenInstallSDK : NSObject

/**
 * 获取sdk当前版本号,add in v2.2.1
 */
+ (NSString *_Nullable)sdkVersion;


/**
 * SDK单例,returns a previously instantiated singleton instance of the API.
 */
+(instancetype _Nullable)defaultManager;


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
+(void)initWithDelegate:(id<OpenInstallDelegate> _Nonnull)delegate;


#pragma mark - Deprecated in v2.2.0（已废弃）

/**
 * 初始化OpenInstall SDK (已废弃,请参考方法 initwithDelegate: 已使用该初始化方法的用户也可继续使用)
 *
 * @param appKey 控制中心创建应用获取appKey
 * @param delegate 委托方法(getInstallParamsFromOpenInstall和 getWakeUpParamsFromOpenInstall)所在的类的对象
 * @discussion 老版本sdk升级过来可延用该api
 */
+(void)setAppKey:(nonnull NSString *)appKey withDelegate:(nullable id<OpenInstallDelegate>)delegate __deprecated_msg("Deprecated in v2.2.0，请参考方法<code>initwithDelegate</code>");


///----------------------
/// @name 获取安装的动态参数
///----------------------

#pragma mark - added in v2.2.0

/**
 * 开发者在需要获取用户安装app后由web网页传递过来的”动态参数“（如邀请码、游戏房间号，渠道编号等）时调用该方法,可第一时间返回数据，可在任意位置调用
 *
 * v2.2.1后默认回调超时时长由5秒(s)修改为为8秒(s)，如无特殊需求，请用此方法，否则可使用高级API
 *
 * @param completedBlock 回调block，在主线程（UI线程）回调
 *
 * @discussion
 1、不要自己保存动态安装参数，在每次需要用到参数时，请调用该方法去获取；
 2、该方法默认超时为8秒，尽量写在业务场景需要参数的位置调用（在业务场景时，网络一般都是畅通的），例如，可以选择在用户注册成功后调用该方法获取参数，对用户进行奖励。原因是iOS首次安装、首次启动的app，会询问用户获取网络权限，用户允许后SDK才能正常联网去获取参数。如果调用过早，可能导致网络权限还未允许就被调用，导致参数无法及时拿到，误以为参数不存在（此时getInstallParmsCompleted法已超时，回调返回空）；
 3. 如果是业务需要，必须在application:didFinishLaunchingWithOptions方法中获取参数，可调用下面高级API，修改超时时长，比如15秒或更长，如果只是拿参数在后台“悄悄地”进行数据统计的情况，超时时长设置为半个小时或更长都是ok的，根据需要来。
 
 * ***该方法可重复获取参数，如需在首次安装才获取安装参数，请自行判断，参考https://www.openinstall.io/doc/ios_sdk_faq.html***
 */
-(void)getInstallParmsCompleted:(void (^_Nullable)(OpeninstallData*_Nullable appData))completedBlock;


/**
 * 开发者在需要获取用户安装app后由web网页传递过来的”动态参数“（如邀请码、游戏房间号，渠道编号等）时调用该方法,可第一时间返回数据，可在任意位置调用
 *
 * @param timeoutInterval 可设置回调超时时长，单位秒(s)
 * @param completedBlock 回调block，在主线程（UI线程）回调
 *
 * @discussion
 * ***开发者不要自行保存参数!!!如果获取动态参数成功，SDK会把参数保存在本地***
 * ***该方法可重复获取参数，如需在首次安装才获取安装参数，请自行判断，参考https://www.openinstall.io/doc/ios_sdk_faq.html***
 */
-(void)getInstallParmsWithTimeoutInterval:(NSTimeInterval)timeoutInterval
                                completed:(void (^_Nullable)(OpeninstallData*_Nullable appData))completedBlock;



#pragma mark - Deprecated in v2.1.1（已废弃）
/**
 * 开发者直接获取动态参数,初始化之后调用（参数的正常获取时间1-2秒内）
 * @return NSDictionary
 */
+(NSDictionary *_Nullable)getOpenInstallParams __deprecated_msg("Deprecated in v2.1.1，请参考方法<code>getInstallParmsCompleted</code>");


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
 * @param userActivity 存储了页面信息，包括url
 * @return bool URL是否被OpenInstall识别
 */
+(BOOL)continueUserActivity:(NSUserActivity *_Nullable)userActivity;



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

