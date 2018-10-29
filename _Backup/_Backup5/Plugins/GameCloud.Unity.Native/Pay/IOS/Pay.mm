//
//  Pay.m
//  Unity-iPhone
//
//  Created by yong li on 2017/5/23.
//
//

#import "Pay.h"
#import "BeeCloud.h"

@implementation Pay

static Pay *pay = nil;

+(instancetype) Instance
{
    if(pay == nil)
    {
        pay = [[self alloc] init];
    }
    
    return pay;
}

-(void)payInitIos:(const char*)beecloud_id BCSecret:(const char*)beecloud_secret WeChatId:(const char*)wechat_id
{
    NSString* b_id = [[NSString alloc] initWithUTF8String:beecloud_id];
    NSString* b_c = [[NSString alloc] initWithUTF8String:beecloud_secret];
    NSString* w_id = [[NSString alloc] initWithUTF8String:wechat_id];
    [BeeCloud initWithAppID:b_id andAppSecret:b_c];
    [BeeCloud initWeChatPay:w_id];
}

-(void)useTestModeIos:(bool) use_testmode
{
    [BeeCloud setSandboxMode:use_testmode];
}

-(void)payIos:(const char*)bill_title PayType:(int)pay_type Fee:(int)bill_totalfee Num:(const char*)bill_num BuyId:(const char*)buy_id UrlScheme:(const char*)url_scheme
{
    NSString *bill_t = [[NSString alloc] initWithUTF8String:bill_title];
    NSString *bill_no = [[NSString alloc] initWithUTF8String:bill_num];
    NSString *b_id = [[NSString alloc] initWithUTF8String:buy_id];
    NSString *u_s = [[NSString alloc] initWithUTF8String:url_scheme];
    NSString *b_f = [NSString stringWithFormat:@"%d",bill_totalfee];
    PayChannel p_c = PayChannelWxApp;
    if(pay_type == 0)
    {
        p_c = PayChannelWxApp;
    }
    else if(pay_type == 1)
    {
        p_c = PayChannelAliApp;
    }
    
    BCPayReq *payReq = [[BCPayReq alloc] init];
    payReq.channel = p_c; //支付渠道
    payReq.title = bill_t;//订单标题
    payReq.totalFee = b_f;//订单价格; channel为BC_APP的时候最小值为100，即1元
    payReq.billNo = bill_no;//商户自定义订单号
    payReq.scheme = u_s;//URL Scheme,在Info.plist中配置; 支付宝,银联必有参数
    payReq.billTimeOut = 300;//订单超时时间
    payReq.cardType = 0; //0 表示不区分卡类型；1 表示只支持借记卡；2 表示支持信用卡；默认为0
    [BeeCloud sendBCReq:payReq];
}
@end

extern "C" void payInitIos(char* beecloud_id,char* beecloud_secret,char* wechat_id)
{
    Pay *app = [Pay Instance];
    
    [app payInitIos:beecloud_id BCSecret:beecloud_secret WeChatId:wechat_id];
}

extern "C" void useTestModeIos(bool use_testmode)
{
    Pay *app = [Pay Instance];
    [app useTestModeIos:use_testmode];
}

extern "C" void payIos(char* bill_title,int pay_type,int bill_totalfee,char* bill_num,char* buy_id,char* url_scheme)
{
    Pay *app = [Pay Instance];
    
    [app payIos:bill_title PayType:pay_type Fee:bill_totalfee Num:bill_num BuyId:buy_id UrlScheme:url_scheme];
}
