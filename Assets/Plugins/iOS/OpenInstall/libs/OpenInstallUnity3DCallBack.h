//
//  OpenInstallUnity3DCallBack.h
//  Unity-iPhone
//
//  Created by cooper on 2018/6/14.
//

#import <Foundation/Foundation.h>

@interface OpenInstallUnity3DCallBack : NSObject

@property (nonatomic, copy)NSString *wakeUpJson;
@property (nonatomic, assign)BOOL isRegister;

+ (OpenInstallUnity3DCallBack *)defaultManager;

@end
