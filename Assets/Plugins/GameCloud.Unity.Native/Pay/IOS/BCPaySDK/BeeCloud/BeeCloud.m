//
//  BeeCloud.m
//  BeeCloud
//
//  Created by Ewenlong03 on 15/9/7.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//
#import "BeeCloud.h"

#import "BCPayCache.h"
#import "BeeCloudAdapter.h"
#import "BeeCloud+Utils.h"

@interface BeeCloud ()
@property (nonatomic, weak) id<BeeCloudDelegate> delegate;
@end


@implementation BeeCloud

+ (instancetype)sharedInstance {
    static dispatch_once_t onceToken;
    static BeeCloud *instance = nil;
    dispatch_once(&onceToken, ^{
        instance = [[BeeCloud alloc] init];
    });
    return instance;
}

+ (BOOL)initWithAppID:(NSString *)appId andAppSecret:(NSString *)appSecret {
    BCPayCache *instance = [BCPayCache sharedInstance];
    if (appId.isValid && appSecret.isValid) {
        instance.appId = appId;
        instance.appSecret = appSecret;
        return YES;
    }
    return NO;
}

+ (BOOL)initWithAppID:(NSString *)appId andAppSecret:(NSString *)appSecret sandbox:(BOOL)isSandbox {
    BOOL flag = [BeeCloud initWithAppID:appId andAppSecret:appSecret];
    if (flag) {
        [BeeCloud setSandboxMode:isSandbox];
    }
    return flag;
}

+ (void)setSandboxMode:(BOOL)sandbox {
    [BCPayCache sharedInstance].sandbox = sandbox;
}

+ (BOOL)getCurrentMode {
    return [BCPayCache sharedInstance].sandbox;
}

+ (BOOL)initWeChatPay:(NSString *)wxAppID {
    if (!wxAppID.isValid) {
        return NO;
    }
    return [BeeCloudAdapter beeCloudRegisterWeChat:wxAppID];
}

+ (BOOL)initPayPal:(NSString *)clientID secret:(NSString *)secret sandbox:(BOOL)isSandbox {
    
    if(clientID.isValid && secret.isValid) {
        BCPayCache *instance = [BCPayCache sharedInstance];
        instance.payPalClientID = clientID;
        instance.payPalSecret = secret;
        instance.isPayPalSandbox = isSandbox;
        
        [BeeCloudAdapter beeCloudRegisterPayPal:clientID secret:secret sandbox:isSandbox];
        return YES;
    }
    return NO;
}

+ (void)setBeeCloudDelegate:(id<BeeCloudDelegate>)delegate {
    [BeeCloud sharedInstance].delegate = delegate;
}

+ (id<BeeCloudDelegate>)getBeeCloudDelegate {
    return [BeeCloud sharedInstance].delegate;
}

+ (BOOL)handleOpenUrl:(NSURL *)url {
    if (BCPayUrlWeChat == [BCPayUtil getUrlType:url]) {
        return [BeeCloudAdapter beeCloud:kAdapterWXPay handleOpenUrl:url];
    } else if (BCPayUrlAlipay == [BCPayUtil getUrlType:url]) {
        return [BeeCloudAdapter beeCloud:kAdapterAliPay handleOpenUrl:url];
    } else if (BCPayUrlUnionPay == [BCPayUtil getUrlType:url]) {
        return [BeeCloudAdapter beeCloud:kAdapterUnionPay handleOpenUrl:url];
    }
    return NO;
}

+ (BOOL)canMakeApplePayments:(NSUInteger)cardType {
    return [BeeCloudAdapter beecloudCanMakeApplePayments:cardType];
}

+ (NSString *)getBCApiVersion {
    return kApiVersion;
}

+ (void)setWillPrintLog:(BOOL)flag {
    [BCPayCache sharedInstance].willPrintLogMsg = flag;
}

+ (void)setNetworkTimeout:(NSTimeInterval)time {
    [BCPayCache sharedInstance].networkTimeout = time;
}

+ (void)initBCWXPay:(NSString *)wxAppId {
    [BeeCloudAdapter beeCloudRegisterWeChat:wxAppId];
    [BeeCloudAdapter beeCloudInitBCWXPay:wxAppId];
}

+ (BOOL)sendBCReq:(BCBaseReq *)req {
    BeeCloud *instance = [BeeCloud sharedInstance];
    BOOL bSend = YES;
    switch (req.type) {
        case BCObjsTypePayReq: //支付(微信、支付宝、银联、百度钱包)
            [instance reqPay:(BCPayReq *)req];
            break;
        case BCObjsTypePreRefundReq: //预退款
            [instance reqPreRefund:(BCPreRefundReq *)req];
            break;
        case BCObjsTypeQueryBillsReq://条件查询支付订单
            [instance reqQueryBills:(BCQueryBillsReq *)req];
            break;
        case BCObjsTypeQueryBillsCountReq:
            [instance reqBillsCount:(BCQueryBillsCountReq *)req];
            break;
        case BCObjsTypeQueryBillByIdReq://根据id查询支付订单
            [instance reqQueryBillById:(BCQueryBillByIdReq *)req];
            break;
        case BCObjsTypeQueryRefundsReq://条件查询退款订单
            [instance reqQueryRefunds:(BCQueryRefundsReq *)req];
            break;
        case BCObjsTypeQueryRefundsCountReq:
            [instance reqRefundsCount:(BCQueryRefundsCountReq *)req];
            break;
        case BCObjsTypeQueryRefundByIdReq://根据id查询退款订单
            [instance reqQueryRefundById:(BCQueryRefundByIdReq *)req];
            break;
        case BCObjsTypeRefundStatusReq://查询退款状态，目前只支持微信、百度
            [instance reqRefundStatus:(BCRefundStatusReq *)req];
            break;
        case BCObjsTypePayPal:
            [instance  reqPayPal:(BCPayPalReq *)req];
            break;
        case BCObjsTypePayPalVerify:
            [instance reqPayPalVerify:(BCPayPalVerifyReq *)req];
            break;
        case BCObjsTypeOfflinePayReq:
            [instance reqOfflinePay:req];
            break;
        case BCObjsTypeOfflineBillStatusReq:
            [instance reqOfflineBillStatus:req];
            break;
        case BCObjsTypeOfflineRevertReq:
            [instance reqOfflineBillRevert:req];
            break;
        default:
            bSend = NO;
            break;
    }
    return bSend;
}

@end
