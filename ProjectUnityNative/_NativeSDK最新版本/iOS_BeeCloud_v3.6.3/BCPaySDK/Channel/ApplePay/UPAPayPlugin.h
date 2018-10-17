//
//  UPPayPluginEx.h
//  UPPayPluginEx
//
//  Created by wxzhao on 12-10-10.
//  Copyright (c) 2012年 China UnionPay. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "UPAPayPluginDelegate.h"

@interface UPAPayPlugin : NSObject

/**
 *  支付接口
 *
 *  @param tn             订单信息
 *  @param mode           接入模式,标识商户以何种方式调用支付控件,00生产环境，01测试环境
 *  @param viewController 启动支付控件的viewController
 *  @param delegate       实现 UPAPayPluginDelegate 方法的 UIViewController
 *  @param mID            苹果公司分配的商户号,表示调用Apple Pay所需要的MerchantID;
 *  @return 返回函数调用结果，成功或失败
 */
+ (BOOL)startPay:(NSString*)tn
            mode:(NSString*)mode
  viewController:(UIViewController*)viewController
        delegate:(id<UPAPayPluginDelegate>)delegate
  andAPMechantID:(NSString* )mID;

@end
