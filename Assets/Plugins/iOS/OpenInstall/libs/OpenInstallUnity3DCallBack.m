//
//  OpenInstallUnity3DCallBack.m
//  Unity-iPhone
//
//  Created by cooper on 2018/6/14.
//

#import "OpenInstallUnity3DCallBack.h"
#import "OpenInstallSDK.h"
#import "OpenIsntallUnity3DBridge.h"

@implementation OpenInstallUnity3DCallBack

static OpenInstallUnity3DCallBack * _obj = nil;
+ (OpenInstallUnity3DCallBack *)defaultManager
{
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _obj = [[OpenInstallUnity3DCallBack alloc] init];
    });
    return _obj;
}

-(void)getWakeUpParams:(OpeninstallData *)appData{
    
    NSString *channelID = @"";
    NSString *datas = @"";
    if (appData.data) {
        datas = [OpenIsntallUnity3DBridge jsonStringWithObject:appData.data];
    }
    if (appData.channelCode) {
        channelID = appData.channelCode;
    }
    NSDictionary *wakeUpDicResult = @{@"channelCode":channelID,@"bindData":datas};
    NSString *wakeUpJsonStr = [OpenIsntallUnity3DBridge jsonStringWithObject:wakeUpDicResult];

    if (self.isRegister) {
        UnitySendMessage([@"OpenInstall" UTF8String], "_wakeupCallback", [wakeUpJsonStr UTF8String]);
        self.wakeUpJson = nil;
    }else{
        self.wakeUpJson = wakeUpJsonStr;
    }

}

@end
