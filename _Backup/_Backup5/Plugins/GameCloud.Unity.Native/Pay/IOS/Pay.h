//
//  Pay.h
//  Unity-iPhone
//
//  Created by yong li on 2017/5/23.
//
//

#ifndef Pay_h
#define Pay_h


#endif /* Pay_h */

@interface Pay : NSObject
+(instancetype)Instance;
-(void)payInitIos:(const char*)beecloud_id BCSecret:(const char*)beecloud_secret WeChatId:(const char*)wechat_id;
-(void)useTestModeIos:(bool) use_testmode;
-(void)payIos:(const char*)bill_title PayType:(int)pay_type Fee:(int)bill_totalfee Num:(const char*)bill_num BuyId:(const char*)buy_id UrlScheme:(const char*)url_scheme;
@end
