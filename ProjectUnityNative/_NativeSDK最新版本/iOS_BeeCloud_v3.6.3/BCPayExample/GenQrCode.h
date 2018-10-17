//
//  GenQrCode.h
//  BCPay
//
//  Created by Ewenlong03 on 15/9/16.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

@interface GenQrCode : NSObject

+ (UIImage *)createQRForString:(NSString *)qrString  withSize:(CGFloat)size ;
+ (UIImage*)imageBlackToTransparent:(UIImage*)image withRed:(CGFloat)red andGreen:(CGFloat)green andBlue:(CGFloat)blue ;

@end
