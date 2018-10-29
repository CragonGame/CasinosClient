//
//  ThirdPartyLogin.h
//  Unity-iPhone
//
//  Created by yong li on 2017/5/23.
//
//

#ifndef ThirdPartyLogin_h
#define ThirdPartyLogin_h


#endif /* ThirdPartyLogin_h */

enum  LoginType {
    WeChat           = 0,
};

@interface ThirdPartyLogin : NSObject
@property (nonatomic) NSString* mAppId;
@property (nonatomic) NSString* mParam;
+(instancetype)Instance;
-(void)loginIsSuccess:(Boolean)is_success loginMsg:(NSString*)msg;
-(void)setAppId:(const char*)str;
@end
