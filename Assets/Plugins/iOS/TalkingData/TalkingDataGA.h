//
//  TalkingDataGA.h
//  TalkingDataGA
//
//  Created by Biao Hou on 11-11-14.
//  Copyright (c) 2011年 tendcloud. All rights reserved.
//

#import <Foundation/Foundation.h>


@interface TalkingDataGA: NSObject

/**
 *	@method	onStart     初始化统计实例     请在application:didFinishLaunchingWithOptions:方法里调用
 *	@param 	appId       appId           类型:NSString     应用的唯一标识，统计后台注册得到
 *  @param 	channelId   渠道名(可选)      类型:NSString     如“app store”
 */
+ (void)onStart:(NSString *)appId withChannelId:(NSString *)channelId;

/**
 *	@method	onEvent     自定义事件
 *	@param 	eventId     事件ID    类型:NSString
 *	@param 	eventData   事件参数   类型:键值对(key只支持NSString, value支持NSString和NSNumber)
 */
+ (void)onEvent:(NSString *)eventId eventData:(NSDictionary *)eventData;

/**
 *  @method setLatitude 设置经纬度
 *  @param  latitude    纬度      类型:double
 *  @param  longitude   经度      类型:double
 */
+ (void)setLatitude:(double)latitude longitude:(double)longitude;

/**
 *  @method getDeviceId 获取设备id
 */
+ (NSString *)getDeviceId;

/**
 *  @method setDeviceToken 设置DeviceToken
 *  @param  deviceToken    从Apple获取的DeviceToken
 */
+ (void)setDeviceToken:(NSData *)deviceToken;

/**
 *  @method handleTDGAPushMessage 处理来自TalkingData的Push消息
 *  @param  message               收到的消息
 *  @return YES 来自TalkingData的消息，SDK已处理    NO 其他来源消息，开发者需自行处理
 */
+ (BOOL)handleTDGAPushMessage:(NSDictionary *)message;

/**
 *  @method setVerboseLogDisabled 设置不显示日志  如发布时不需显示日志，应当最先调用该方法
 */
+ (void)setVerboseLogDisabled;

/**
 *  @method setSdkType 设置SDK类型  区分开发框架使用，开发者请勿调用
 */
+ (void)setSDKFramework:(int)tag;

@end





@interface TDGAAccount : NSObject

/**
 *	@method	setAccount  设置帐号
 *	@param 	accountId   帐号id    类型:NSString
 */
+ (TDGAAccount *)setAccount:(NSString *)accountId;

/**
 *	@method	setAccountName  设置帐号名称
 *	@param 	accountName     账户名称    类型:NSString
 */
- (void)setAccountName:(NSString *)accountName;

/**
 *	@method	setAccountType  设置帐号类型
 *	@param 	accountType     账户类型        类型TDGAAccountType
 */
- (void)setAccountType:(int)accountType;

/**
 *	@method	setLevel    设置帐号等级
 *	@param 	level       升级之后的等级     类型:int
 */
- (void)setLevel:(int)level;

/**
 *	@method	setGender   设置性别
 *	@param 	gender      性别      类型:TDGAGender
 */
- (void)setGender:(int)gender;

/**
 *	@method	setAge  设置年龄
 *	@param 	age     年龄      类型:int
 */
- (void)setAge:(int)age;

/**
 *	@method	setGameServer   设置区服
 *	@param  gameServer      区服      类型:NSString
 */
- (void)setGameServer:(NSString *)gameServer;

@end





@interface TDGAMission : NSObject

/**
 *	@method	onBegin     开始一项任务
 *	@param 	missionId   任务名称    类型:NSString
 */
+ (void)onBegin:(NSString *)missionId;

/**
 *	@method	onCompleted 完成一项任务
 *	@param 	missionId   任务名称    类型:NSString
 */
+ (void)onCompleted:(NSString *)missionId;

/**
 *	@method	onFailed    一项任务失败
 *	@param 	missionId   任务名称    类型:NSString
 *	@param 	failedCause 失败原因    类型:NSString
 */
+ (void)onFailed:(NSString *)missionId failedCause:(NSString *)cause;

@end





@interface TDGAVirtualCurrency : NSObject

/**
 *	@method	onChargeRequst          虚拟币充值请求
 *	@param 	orderId                 订单id        类型:NSString
 *	@param 	iapId                   充值包id      类型:NSString
 *	@param 	currencyAmount          现金金额      类型:double
 *	@param 	currencyType            币种          类型:NSString
 *	@param 	virtualCurrencyAmount   虚拟币金额    类型:double
 *	@param 	paymentType             支付类型      类型:NSString
 */
+ (void)onChargeRequst:(NSString *)orderId
                 iapId:(NSString *)iapId
        currencyAmount:(double)currencyAmount
          currencyType:(NSString *)currencyType
 virtualCurrencyAmount:(double)virtualCurrencyAmount
           paymentType:(NSString *)paymentType;

/**
 *	@method	onChargeRequst          虚拟币充值请求
 *	@param 	orderId                 订单id        类型:NSString
 */
+ (void)onChargeSuccess:(NSString *)orderId;

/**
 *  @method onReward                虚拟币赠送
 *  @param  virtualCurrencyAmount   虚拟币金额         类型:double
 *  @param  reason                  赠送虚拟币的原因    类型:NSString
 */
+ (void)onReward:(double)virtualCurrencyAmount reason:(NSString *)reason;

@end





@interface TDGAItem : NSObject

/**
 *	@method	onPurchase  虚拟物品购买
 *	@param 	item        道具           类型:NSString
 *	@param 	number      道具个数        类型:int
 *	@param 	price       道具单价        类型:double
 */
+ (void)onPurchase:(NSString *)item itemNumber:(int)number priceInVirtualCurrency:(double)price;

/**
 *	@method	onPurchase  虚拟物品消耗
 *	@param 	item        道具           类型:NSString
 *	@param 	number      道具个数        类型:int
 */
+ (void)onUse:(NSString *)item itemNumber:(int)number;

@end
