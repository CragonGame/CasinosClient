//
//  GetuiForUnity.m
//  Unity3D
//
//  Created by dazq on 16/7/1.
//  Copyright © 2016年 dzq. All rights reserved.
//

#import "GetuiForUnity.h"
#import <UIKit/UIKit.h>
static NSString *gameObjectName = nil;
@implementation GetuiForUnity

//message tools

+ (void)sendU3dMessage:(NSString *)messageName param:(id)dict {
    NSString *param = @"";
    if ([dict isKindOfClass:[NSDictionary class]]) {
        param = [self DataTOjsonString:dict];
    } else {
        param = dict;
    }
    if (!gameObjectName) {
        gameObjectName = @"Main Camera";
    }
    UnitySendMessage([gameObjectName UTF8String], [messageName UTF8String], [param UTF8String]);
}

+ (NSString *)DataTOjsonString:(id)object {
    NSString *jsonString = nil;
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:object
                                                       options:NSJSONWritingPrettyPrinted // Pass 0 if you don't care about the readability of the generated string
                                                         error:&error];
    if (!jsonData) {
        NSLog(@"Got an error: %@", error);
    } else {
        jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    }
    return jsonString;
}

#pragma mark - Getui Delegate

- (void)GeTuiSdkDidRegisterClient:(NSString *)clientId {
    [GetuiForUnity sendU3dMessage:@"onReceiveClientId" param:clientId];
}
- (void)GeTuiSdkDidReceivePayloadData:(NSData *)payloadData andTaskId:(NSString *)taskId andMsgId:(NSString *)msgId andOffLine:(BOOL)offLine fromGtAppId:(NSString *)appId {
    // [4]: 收到个推消息
    NSString *payloadMsg = nil;
    if (payloadData) {
        payloadMsg = [[NSString alloc] initWithData:payloadData encoding:NSUTF8StringEncoding];
    }
    NSDictionary *ret = [NSDictionary dictionaryWithObjectsAndKeys:
                                          @"payload", @"type",
                                          taskId, @"taskId",
                                          msgId, @"messageId",
                                          payloadMsg, @"payload", nil];
    [GetuiForUnity sendU3dMessage:@"onReceiveMessage" param:ret];
}

- (void)GeTuiSdkDidOccurError:(NSError *)error {

    [GetuiForUnity sendU3dMessage:@"GeTuiSdkDidOccurError" param:[NSString stringWithFormat:@"%@", error]];
}

- (void)GeTuiSdkDidSetPushMode:(BOOL)isModeOff error:(NSError *)error {
    [GetuiForUnity sendU3dMessage:@"GeTuiSdkDidSetPushMode" param:!isModeOff ? @"true" : @"false"];
}

- (void)GeTuiSDkDidNotifySdkState:(SdkStatus)aStatus {
    [GetuiForUnity sendU3dMessage:@"GeTuiSDkDidNotifySdkState" param:[NSString stringWithFormat:@"%d", aStatus]];
}

- (void)GeTuiSdkDidAliasAction:(NSString *)action result:(BOOL)isSuccess sequenceNum:(NSString *)aSn error:(NSError *)aError {

    NSDictionary *ret = [NSDictionary dictionaryWithObjectsAndKeys:
                                          action, @"action",
                                          isSuccess ? @"true" : @"false", @"result",
                                          aSn, @"sequenceNum",
                                          aError, @"error", nil];

    [GetuiForUnity sendU3dMessage:@"GeTuiSdkDidAliasAction" param:ret];
}


/**
 *  设置接收 UnitySendMessage 的 GameObject
 *
 *  @param GameObjectName GameObject 名称
 */
- (void)setListenerGameObject:(NSString *)GameObjectName {
    gameObjectName = GameObjectName;
}
/** 注册用户通知 */
+ (void)registerUserNotification {

    /*
     注册通知(推送)
     申请App需要接受来自服务商提供推送消息
     */

    // 判读系统版本是否是“iOS 8.0”以上
    if ([[[UIDevice currentDevice] systemVersion] floatValue] >= 8.0 ||
        [UIApplication instancesRespondToSelector:@selector(registerUserNotificationSettings:)]) {

        // 定义用户通知类型(Remote.远程 - Badge.标记 Alert.提示 Sound.声音)
        UIUserNotificationType types = UIUserNotificationTypeAlert | UIUserNotificationTypeBadge | UIUserNotificationTypeSound;

        // 定义用户通知设置
        UIUserNotificationSettings *settings = [UIUserNotificationSettings settingsForTypes:types categories:nil];

        // 注册用户通知 - 根据用户通知设置
        [[UIApplication sharedApplication] registerUserNotificationSettings:settings];
        [[UIApplication sharedApplication] registerForRemoteNotifications];
    } else { // iOS8.0 以前远程推送设置方式
        // 定义远程通知类型(Remote.远程 - Badge.标记 Alert.提示 Sound.声音)
        UIRemoteNotificationType myTypes = UIRemoteNotificationTypeBadge | UIRemoteNotificationTypeAlert | UIRemoteNotificationTypeSound;

        // 注册远程通知 -根据远程通知类型
        [[UIApplication sharedApplication] registerForRemoteNotificationTypes:myTypes];
    }
}

#pragma mark - VOIP related

// 实现 PKPushRegistryDelegate 协议方法

/** 系统返回VOIPToken，并提交个推服务器 */

- (void)pushRegistry:(PKPushRegistry *)registry didUpdatePushCredentials:(PKPushCredentials *)credentials forType:(NSString *)type {
    NSString *voiptoken = [credentials.token.description stringByTrimmingCharactersInSet:[NSCharacterSet characterSetWithCharactersInString:@"<>"]];
    voiptoken = [voiptoken stringByReplacingOccurrencesOfString:@" " withString:@""];
    NSLog(@"\n>>>[VoIP Token]:%@\n\n",voiptoken);
    //向个推服务器注册 VoipToken
    [GeTuiSdk registerVoipToken:voiptoken];
}

/** 接收VOIP推送中的payload进行业务逻辑处理（一般在这里调起本地通知实现连续响铃、接收视频呼叫请求等操作），并执行个推VOIP回执统计 */
- (void)pushRegistry:(PKPushRegistry *)registry didReceiveIncomingPushWithPayload:(PKPushPayload *)payload forType:(NSString *)type {
    //个推VOIP回执统计
    [GeTuiSdk handleVoipNotification:payload.dictionaryPayload];
    
    //TODO:接受 VoIP 推送中的 payload 内容进行具体业务逻辑处理
    NSLog(@"[VoIP Payload]:%@,%@", payload, payload.dictionaryPayload);
    
    NSDictionary *ret = [NSDictionary dictionaryWithObjectsAndKeys:
                         [NSNumber numberWithInteger:1], @"result",
                         @"voipPayload", @"type",
                         payload.dictionaryPayload[@"payload"], @"payload",
                         payload.dictionaryPayload[@"_gmid_"], @"gmid",  nil];
    [GetuiForUnity sendU3dMessage:@"onReceiveVoIPMessage" param:ret];
}

@end

static GetuiForUnity *delegateObject = nil;

// Converts C style string to NSString
NSString *GTCreateNSString(const char *string) {
    if (string)
        return [NSString stringWithUTF8String:string];
    else
        return [NSString stringWithUTF8String:""];
}

// Helper method to create C string copy
char *GTMakeStringCopy(const char *string) {
    if (string == NULL)
        return NULL;

    char *res = (char *) malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

// When native code plugin is implemented in .mm / .cpp file, then functions
// should be surrounded with extern "C" block to conform C function naming rules

// By default mono string marshaler creates .Net string for returned UTF-8 C string
// and calls free for returned value, thus returned strings should be allocated on heap
#if defined(__cplusplus)
extern "C" {
#endif
void _StartSDK(const char *appId, const char *appKey, const char *appSecret) {
    if (delegateObject == nil)
        delegateObject = [[GetuiForUnity alloc] init];

    [GeTuiSdk startSdkWithAppId:GTCreateNSString(appId) appKey:GTCreateNSString(appKey) appSecret:GTCreateNSString(appSecret) delegate:delegateObject];
}
void _registerUserNotification() {
    [GetuiForUnity registerUserNotification];
}
void _setListenerGameObject(const char *gameObjectName) {
    if (delegateObject == nil)
        delegateObject = [[GetuiForUnity alloc] init];
    [delegateObject setListenerGameObject:GTCreateNSString(gameObjectName)];
}

void _registerDeviceToken(const char *token) {
    NSString *deviceToken = GTCreateNSString(token);
    [GeTuiSdk registerDeviceToken:deviceToken];
}

const char *_clientId(const char *alias) {
    return GTMakeStringCopy([[GeTuiSdk clientId] UTF8String]);
}

void _destroy() {
    [GeTuiSdk destroy];
}
void _resume() {
    [GeTuiSdk resume];
}
void _bindAlias(const char *alias, const char *aSn) {
    [GeTuiSdk bindAlias:GTCreateNSString(alias) andSequenceNum:GTCreateNSString(aSn)];
}
const int _status() {
    return [GeTuiSdk status];
}
void _unBindAlias(const char *alias, const char *aSn) {
    [GeTuiSdk unbindAlias:GTCreateNSString(alias) andSequenceNum:GTCreateNSString(aSn) andIsSelf:YES];
}
const char *_version() {
    return GTMakeStringCopy([[GeTuiSdk version] UTF8String]);
}
const bool _setTag(const char *tags) {
    NSArray *tagsArray = [GTCreateNSString(tags) componentsSeparatedByString:@","];
    return [GeTuiSdk setTags:tagsArray];
}
void _setPushMode(const bool isValue) {
    [GeTuiSdk setPushModeForOff:!isValue];
}
void _runBackgroundEnable(const bool isEnable) {
    [GeTuiSdk runBackgroundEnable:isEnable];
}
/**
     *  设置渠道
     *  备注：SDK可以未启动就调用该方法
     *
     *  SDK-1.5.0+
     *
     *  @param aChannelId 渠道值，可以为空值
     */
void _setChannelId(const char *aChannelId) {
    [GeTuiSdk setChannelId:GTCreateNSString(aChannelId)];
}
    
void _voipRegistration() {
    dispatch_queue_t mainQueue = dispatch_get_main_queue();
    PKPushRegistry *voipRegistry = [[PKPushRegistry alloc] initWithQueue:mainQueue];
    voipRegistry.delegate = delegateObject;
    // Set the push type to VoIP
    voipRegistry.desiredPushTypes = [NSSet setWithObject:PKPushTypeVoIP];
}

#if defined(__cplusplus)
}
#endif


