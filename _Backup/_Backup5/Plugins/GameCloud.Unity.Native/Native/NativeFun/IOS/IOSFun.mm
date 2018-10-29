//
//  IOSFun.m
//  Unity-iPhone
//
//  Created by yong li on 2017/5/23.
//
//

#import "IOSFun.h"

@implementation IOSFun

static IOSFun *ios_fun = nil;

+(instancetype) Instance
{
    if(ios_fun == nil)
    {
        ios_fun = [[self alloc] init];
    }
    
    return ios_fun;
}

-(char*)getCountryCodeIos
{
    NSLocale *locale  = [NSLocale currentLocale];
    NSString *countryCode = [locale objectForKey:NSLocaleCountryCode];
    const char* c_c =[countryCode UTF8String];
    char* res = (char*)malloc(strlen(c_c)+1);
    strcpy(res, c_c);
    
    return res;
}
@end

extern "C" char* getCountryCodeIos()
{
    IOSFun *app = [IOSFun Instance];
    return [app getCountryCodeIos];
}
