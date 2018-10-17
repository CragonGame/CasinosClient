//
//  Header.h
//  UPAPayPlugin
//
//  Created by zhangyi on 10/27/15.
//  Copyright © 2015 UnionPay. All rights reserved.
//

#import <Foundation/Foundation.h>


typedef NS_ENUM(NSInteger,UPPaymentResultStatus) {

    UPPaymentResultStatusSuccess,        //支付成功
    UPPaymentResultStatusFailure,        //支付失败
    UPPaymentResultStatusCancel,         //支付取消
    UPPaymentResultStatusUnknownCancel   //支付取消，交易已发起，状态不确定，商户需查询商户后台确认支付状态
};


@interface UPPayResult:NSObject

//支付结果状态，具体见UPPaymentResultStatus定义
@property UPPaymentResultStatus paymentResultStatus;

//支付失败时候的错误原因，如可用余额不足[1000051]“,此信息前半部 分为文字错误信息,后 7 位为错误应答码。当支付成功或支付取消时候errorDescription取值为 nil。
@property (nonatomic,strong) NSString* errorDescription;

//目前表示成功支付时包含的优惠信息
@property (nonatomic,strong) NSString* otherInfo;

@end



@protocol UPAPayPluginDelegate <NSObject>
/**
 *  支付结果回调函数
 *
 *  @param payResult   以UPPayResult结构向商户返回支付结果
 */
-(void) UPAPayPluginResult:(UPPayResult *) payResult;
@end