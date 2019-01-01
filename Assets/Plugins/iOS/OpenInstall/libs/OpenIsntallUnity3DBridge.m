//
//  OpenIsntallUnity3DBridge.m
//  Unity-iPhone
//
//  Created by cooper on 2018/6/14.
//

#import "OpenIsntallUnity3DBridge.h"
#import "OpenInstallUnity3DCallBack.h"
#import "OpenInstallSDK.h"

@implementation OpenIsntallUnity3DBridge

#if defined (__cplusplus)
extern "C" {
#endif
    
    void _openInstallGetInstall(int s)
    {
        [[OpenInstallSDK defaultManager] getInstallParmsWithTimeoutInterval:s completed:^(OpeninstallData * _Nullable appData) {
            
            NSDictionary *installDicResult = @{@"channelCode":appData.channelCode?:@"",@"bindData":appData.data?:@""};
            NSString *installJsonStr = [OpenIsntallUnity3DBridge jsonStringWithObject:installDicResult];
            UnitySendMessage([@"OpenInstall" UTF8String], "_installCallback", [installJsonStr UTF8String]);
        }];
    }
    
    void _openInstallRegisterWakeUpHanndler()
    {
        OpenInstallUnity3DCallBack *callBack = [OpenInstallUnity3DCallBack defaultManager];
        callBack.isRegister = YES;
        if(callBack.wakeUpJson.length != 0)
        {
            UnitySendMessage([@"OpenInstall" UTF8String], "_wakeupCallback", [callBack.wakeUpJson UTF8String]);
            callBack.wakeUpJson = nil;
        }
    }

    void _openInstallReportRegister()
    {
        [OpenInstallSDK reportRegister];
    }
    
    void _openInstallReportEffectPoint(char*pointId,long pointValue)
    {
        NSString *pointID = [NSString stringWithCString:pointId encoding:NSASCIIStringEncoding];
        [[OpenInstallSDK defaultManager] reportEffectPoint:pointID effectValue:pointValue];
    }
    
#if defined (__cplusplus)
}
#endif

+ (NSString *)jsonStringWithObject:(id)jsonObject{
    
    id arguments = (jsonObject == nil ? [NSNull null] : jsonObject);
    
    NSArray* argumentsWrappedInArr = [NSArray arrayWithObject:arguments];
    
    NSString* argumentsJSON = [self cp_JSONString:argumentsWrappedInArr];
    
    argumentsJSON = [argumentsJSON substringWithRange:NSMakeRange(1, [argumentsJSON length] - 2)];
    
    return argumentsJSON;
}
+ (NSString *)cp_JSONString:(NSArray *)array{
    NSError *error = nil;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:array
                                                       options:0
                                                         error:&error];
    
    NSString *jsonString = [[NSString alloc] initWithData:jsonData
                                                 encoding:NSUTF8StringEncoding];
    
    if ([jsonString length] > 0 && error == nil){
        return jsonString;
    }else{
        return @"";
    }
}

@end
