//
//  ThirdPartyLogin.m
//  Unity-iPhone
//
//  Created by yong li on 2017/5/23.
//
//

#import "ThirdPartyLogin.h"
#import "WXApi.h"

@implementation ThirdPartyLogin
@synthesize mAppId;
@synthesize mParam;

static ThirdPartyLogin *thirdparty_login = nil;

+(instancetype) Instance
{
    if(thirdparty_login == nil)
    {
        thirdparty_login = [[self alloc] init];
    }
    
    return thirdparty_login;
}

-(void)loginIsSuccess:(Boolean)is_success loginMsg:(NSString*)msg
{
    if(is_success)
    {
        NSString *real_token = [NSString stringWithFormat:@"%@%@%@",mParam,@"|",msg];
        UnitySendMessage("ThirdPartyLoginReceiver","loginSuccess",[real_token UTF8String]);
    }
    else
    {
        UnitySendMessage("ThirdPartyLoginReceiver","loginFail",[msg UTF8String]);
    }
}

-(void)setAppId:(const char*)str
{
    mAppId= [[NSString alloc] initWithUTF8String:str];
    [WXApi registerApp:mAppId];
    if(![WXApi isWXAppInstalled])
    {
        [self loginIsSuccess:false loginMsg:@"10000"];
    }
}

-(void)login:(int) login_type loginState:(char*) state loginParam:(char*) param
{
    mParam= [[NSString alloc] initWithUTF8String:param];
    if (login_type == 0) {
        if([WXApi isWXAppInstalled])
        {
            SendAuthReq *req = [[SendAuthReq alloc] init];
            req.scope = @"snsapi_userinfo";
            req.state = [[NSString alloc] initWithUTF8String:state];
            [WXApi sendReq:req];
        }
        else
        {
            [self loginIsSuccess:false loginMsg:@"10000"];
        }
    }
}
@end

extern "C" void init(char* app_id)
{
    NSLog( @"init__");
    ThirdPartyLogin *app = [ThirdPartyLogin Instance];
    
    [app setAppId:app_id];
}

extern "C" void login(int login_type,char* state,char* param)
{
    NSLog( @"login__login_type__");
    ThirdPartyLogin *app = [ThirdPartyLogin Instance];
    
    [app login:login_type loginState:state loginParam:param];
}
