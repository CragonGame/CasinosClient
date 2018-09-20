//
//  BeeCloud+Utils.m
//  BCPay
//
//  Created by joseph on 15/11/6.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BeeCloud+Utils.h"
#import "BeeCloudAdapter.h"

@implementation BeeCloud (Utils)

#pragma mark Pay Request

- (void)reqPay:(BCPayReq *)req {
    [req payReq];
}

- (void)reqPreRefund:(BCPreRefundReq *)req {
    [req preRefundReq];
}

#pragma mark - 条件查询支付订单

- (void)reqQueryBills:(BCQueryBillsReq *)req {
    [req queryBillsReq];
}

#pragma mark - 查询符合条件的订单数目
- (void)reqBillsCount:(BCQueryBillsCountReq *)req {
    [req queryBillsCountReq];
}

#pragma mark - 条件查询退款订单

- (void)reqQueryRefunds:(BCQueryRefundsReq *)req {
    [req queryRefundsReq];
}

#pragma mark - 查询符合条件的退款数目 

- (void)reqRefundsCount:(BCQueryRefundsCountReq *)req {
    [req queryRefundsCountReq];
}

#pragma mark - 根据id查询支付订单

- (void)reqQueryBillById:(BCQueryBillByIdReq *)req {
    [req queryBillByIdReq];
}

#pragma mark - 根据id查询退款订单

- (void)reqQueryRefundById:(BCQueryRefundByIdReq *)req {
    [req queryRefundByIdReq];
}

#pragma mark - 查询退款状态

- (void)reqRefundStatus:(BCRefundStatusReq *)req {
    [req refundStatusReq];
}

#pragma mark - Offline Pay

- (void)reqOfflinePay:(id)req {
    [BeeCloudAdapter beeCloudOfflinePay:[NSMutableDictionary dictionaryWithObjectsAndKeys:req, kAdapterOffline, nil]];
}

#pragma mark - OffLine BillStatus

- (void)reqOfflineBillStatus:(id)req {
    [BeeCloudAdapter beeCloudOfflineStatus:[NSMutableDictionary dictionaryWithObjectsAndKeys:req, kAdapterOffline, nil]];
}

#pragma mark - OffLine BillRevert

- (void)reqOfflineBillRevert:(id)req {
    [BeeCloudAdapter beeCloudOfflineRevert:[NSMutableDictionary dictionaryWithObjectsAndKeys:req, kAdapterOffline, nil]];
}

#pragma mark PayPal

- (void)reqPayPal:(BCPayPalReq *)req {
    [BeeCloudAdapter beeCloudPayPal:[NSMutableDictionary dictionaryWithObjectsAndKeys:req, kAdapterPayPal,nil]];
}

- (void)reqPayPalVerify:(BCPayPalVerifyReq *)req {
    [BeeCloudAdapter beeCloudPayPalVerify:[NSMutableDictionary dictionaryWithObjectsAndKeys:req, kAdapterPayPal, nil]];
}

@end
