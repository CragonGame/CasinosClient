//
//  OpenInstallGetInstallData.m
//  Unity-iPhone
//
//  Created by yong li on 2017/5/23.
//
//

#import "OpenInstallGetInstallData.h"
#import "OpenInstallSDK.h"

@implementation OpenInstallGetInstallData

static OpenInstallGetInstallData *open_install = nil;

+(instancetype) Instance
{
    if(open_install == nil)
    {
        open_install = [[self alloc] init];
    }
    
    return open_install;
}

-(void)GetInstallData
{
    [[OpenInstallSDK defaultManager] getInstallParmsCompleted:^(OpeninstallData*_Nullable appData) {
        if (appData != nil && appData.data != nil) {
            NSError *parseError = nil;
            NSData *jsonData = [NSJSONSerialization dataWithJSONObject:appData.data options:NSJSONWritingPrettyPrinted error:&parseError];
            NSString* json_m = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
            NSString *paramsStr = [NSString stringWithFormat:@"%@%@%@%@%@",@"{\"Data\":",json_m,@",\"ChannelCode\":\"",appData.channelCode,@"\"}"];
            UnitySendMessage("OpenInstallReceiver","OpenInstallInstallResult",[paramsStr UTF8String]);
        }
    }];
}
@end

extern "C" void GetInstallData()
{
    OpenInstallGetInstallData *app = [OpenInstallGetInstallData Instance];
    
    [app GetInstallData];
}
