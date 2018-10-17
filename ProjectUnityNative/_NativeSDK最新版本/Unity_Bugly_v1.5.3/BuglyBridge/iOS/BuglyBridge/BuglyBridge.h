//
//  BuglyBridge.h
//  BuglyAgent
//
//  Created by Yeelik on 15/11/25.
//  Copyright © 2015年 Bugly. All rights reserved.
//
//  Version: 1.3.3
//

#import <Foundation/Foundation.h>

#pragma mark - Interface for Bridge

#ifdef __cplusplus
extern "C"{
#endif
    
    /**
     *    @brief  初始化
     *
     *    @param appId 应用标识
     *    @param debug 是否开启debug模式，开启后会在控制台打印调试信息，默认为NO
     *    @param level 自定义日志上报级别，使用SDK接口打印的日志会跟崩溃信息一起上报，默认为Info(即Info、Warning、Error级别的日志都会上报)
     *    Debug=4,Info=3,Warnning=2,Error=1,Off=0
     */
    void _BuglyInit(const char * appId, bool debug, int level);
    
    /**
     *    @brief  设置用户唯一标识
     *
     *    @param userId
     */
    void _BuglySetUserId(const char * userId);
    
    /**
     *    @brief  设置自定义标签
     *
     *    @param tag
     */
    void _BuglySetTag(int tag);
    
    /**
     *    @brief  设置自定义键值对数据
     *
     *    @param key
     *    @param value
     */
    void _BuglySetKeyValue(const char * key, const char * value);
    
    /**
     *    @brief  自定义异常数据上报
     *
     *    @param type
     *    @param name       异常类型
     *    @param reason     异常原因
     *    @param stackTrace 异常堆栈
     *    @param extras     附加数据
     *    @param quit       上报后是否退出应用
     */
    void _BuglyReportException(int type, const char * name, const char * reason, const char * stackTrace, const char * extras, bool quit);
    
    /**
     *    @brief  设置默认的应用配置，在初始化之前调用
     *
     *    @param channel  渠道
     *    @param version  应用版本
     *    @param user     用户
     *    @param deviceId 设备唯一标识
     */
    void _BuglyDefaultConfig(const char * channel, const char * version, const char *user, const char * deviceId);
    
    /**
     *    @brief  自定义日志打印接口
     *
     *    @param level 日志级别, 1=Error、2=Warning、3=Info、4=Debug
     *    @param tag   日志标签
     *    @param log   日志内容
     */
    void _BuglyLogMessage(int level, const char * tag, const char * log);
    
    /**
     *    @brief  设置崩溃上报组件的类别
     *
     *    @param type 0＝Default、1=Bugly、2=MSDK、3=IMSDK
     */
    void _BuglyConfigCrashReporterType(int type);
    
    /**
     *    @brief  设置额外的配置信息
     *
     *    @param key
     *    @param value
     */
    void _BuglySetExtraConfig(const char *key, const char * value);
    
#ifdef __cplusplus
} // extern "C"
#endif

#pragma mark -
