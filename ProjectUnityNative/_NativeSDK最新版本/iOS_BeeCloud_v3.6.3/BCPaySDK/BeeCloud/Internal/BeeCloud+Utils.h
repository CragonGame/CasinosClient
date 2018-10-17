//
//  BeeCloud+Utils.h
//  BCPay
//
//  Created by joseph on 15/11/6.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BeeCloud.h"

@interface BeeCloud (Utils)

/**
 *  微信、支付宝、银联、百度钱包支付
 *
 *  @param req 支付请求
 */
- (void)reqPay:(BCPayReq *)req;

/**
 *  发起预退款
 *
 *  @param req 预退款请求
 */
- (void)reqPreRefund:(BCPreRefundReq *)req;

/**
 *  线下支付事件。支持WX_NATIVE、WX_SCAN、ALI_OFFLINE_QRCODE、ALI_SCAN
 *
 *  @param req 线下支付请求
 */
- (void)reqOfflinePay:(id)req;

/**
 *  线下支付订单状态查询
 *
 *  @param req 线下支付订单查询请求
 */
- (void)reqOfflineBillStatus:(id)req;

/**
 *  线下支付订单撤消。支持WX_SCAN、ALI_OFFLINE_QRCODE、ALI_SCAN
 *
 *  @param req 线下支付订单撤消请求
 */
- (void)reqOfflineBillRevert:(id)req;

/**
 *  PayPal支付
 *
 *  @param req PayPal支付请求
 */
- (void)reqPayPal:(BCPayPalReq *)req;

/**
 *  PayPal验证
 *
 *  @param req PayPal验证请求
 */
- (void)reqPayPalVerify:(BCPayPalVerifyReq *)req;

/**
 *  查询支付订单
 *
 *  @param req 查询订单请求
 */
- (void)reqQueryBills:(BCQueryBillsReq *)req;

/**
 *  查询满足条件的支付订单数目
 *
 *  @param req 满足的条件结构
 */
- (void)reqBillsCount:(BCQueryBillsCountReq *)req;

/**
 *  根据订单记录objectId查询单笔支付订单
 *
 *  @param req 查询单笔支付订单请求
 */
- (void)reqQueryBillById:(BCQueryBillByIdReq *)req;

/**
 *  查询退款订单
 *
 *  @param req 查询退款订单请求
 */
- (void)reqQueryRefunds:(BCQueryRefundsReq *)req;

/**
 *  查询满足条件的退款订单数目
 *
 *  @param req 满足的条件结构
 */
- (void)reqRefundsCount:(BCQueryRefundsCountReq *)req;

/**
 *  根据订单记录objectId查询退款订单
 *
 *  @param req 查询单笔退款订单请求
 */
- (void)reqQueryRefundById:(BCQueryRefundByIdReq *)req;
/**
 *  查询退款状态。目前仅支持WX_APP
 *
 *  @param req 查询退款状态请求
 */
- (void)reqRefundStatus:(BCRefundStatusReq *)req;

@end
