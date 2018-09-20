//
//  NativeMgr.m
//  Unity-iPhone
//
//  Created by yong li on 2017/5/23.
//
//

#import "NativeMgr.h"
#import "WXApi.h"

@implementation NativeMgr
@synthesize mNativeOperate;

static NativeMgr *native_mgr = nil;

+(instancetype)Instance
{
    if(native_mgr == nil)
    {
        native_mgr = [[self alloc] init];
    }
    
    return native_mgr;
}

-(NSString*)getCurrentNativeOperate
{
	return mNativeOperate;
}

-(void)nativeOperate:(const char*)operate_type
{
    mNativeOperate= [[NSString alloc] initWithUTF8String:operate_type];
}
@end

extern "C" void nativeOperate(char* operate_type)
{
    NSString* type = [[NSString alloc] initWithUTF8String:operate_type];
    NSLog([[NSString alloc] initWithFormat:@"%@%@", @"login__nativeOperate__",type]);
    NativeMgr *app = [NativeMgr Instance];
    
    [app nativeOperate:operate_type];
}
