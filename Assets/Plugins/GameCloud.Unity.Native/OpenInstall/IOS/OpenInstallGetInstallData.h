//
//  OpenInstallGetInstallData.h
//  Unity-iPhone
//
//  Created by yong li on 2017/5/23.
//
//

#ifndef OpenInstallGetInstallData_h
#define OpenInstallGetInstallData_h


#endif /* OpenInstallGetInstallData_h */

enum  LoginType {
    WeChat           = 0,
};

@interface OpenInstallGetInstallData : NSObject
+(instancetype)Instance;
-(void)GetInstallData;
@end
