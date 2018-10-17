//
//  PaymentControl.h
//  PaymentControl
//
//  Created by qcao on 15/10/20.
//  Copyright © 2015年 China Unionpay Co.,Ltd. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>


typedef void (^UPPaymentResultBlock)(NSString* code, NSDictionary* data);


@interface UPPaymentControl : NSObject


/**
 *  创建支付单例服务
 *
 *  @return 返回单例对象
 */

+ (UPPaymentControl *)defaultControl;


/**
 *  支付接口
 *
 *  @param tn             订单信息
 *  @param schemeStr      调用支付的app注册在info.plist中的scheme
 *  @param mode           支付环境
 *  @param viewController 启动支付控件的viewController
 *  @return 返回成功失败
 */
- (BOOL)startPay:(NSString*)tn
      fromScheme:(NSString *)schemeStr
            mode:(NSString*)mode
  viewController:(UIViewController*)viewController;



/**
 *  APP是否已安装检测接口，通过该接口得知用户是否安装银联支付的APP。
 *
 *  @return 返回是否已经安装了银联支付APP
 */


- (BOOL)isPaymentAppInstalled;

/**
 *  处理钱包或者独立快捷app支付跳回商户app携带的支付结果Url
 *
 *  @param url              支付结果url，传入后由SDK解析
 *  @param completionBlock  结果回调，保证跳转钱包支付过程中，即使调用方app被系统kill时，能通过这个回调取到支付结果。
 */

- (void)handlePaymentResult:(NSURL*)url completeBlock:(UPPaymentResultBlock)completionBlock;

@end
