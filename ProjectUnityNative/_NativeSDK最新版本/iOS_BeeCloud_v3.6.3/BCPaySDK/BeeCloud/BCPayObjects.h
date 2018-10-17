//
//  BCObjects.h
//  BCPaySDK
//
//  Created by Ewenlong03 on 15/7/27.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#ifndef BCPaySDK_BCPayObjects_h
#define BCPaySDK_BCPayObjects_h

#import "BCBaseReq.h"
#import "BCBaseResp.h"

#import "BCPayReq.h"
#import "BCPayResp.h"

#import "BCPreRefundReq.h"
#import "BCPreRefundResp.h"

#import "BCQueryBillsReq.h"
#import "BCQueryBillsResp.h"

#import "BCQueryBillByIdReq.h"
#import "BCQueryBillByIdResp.h"

#import "BCQueryBillsCountReq.h"
#import "BCQueryBillsCountResp.h"

#import "BCQueryRefundsReq.h"
#import "BCQueryRefundsResp.h"

#import "BCQueryRefundByIdReq.h"
#import "BCQueryRefundByIdResp.h"

#import "BCQueryRefundsCountReq.h"
#import "BCQueryRefundsCountResp.h"

#import "BCRefundStatusReq.h"
#import "BCRefundStatusResp.h"

#import "BCBaseResult.h"
#import "BCQueryBillResult.h"
#import "BCQueryRefundResult.h"

#import "BCPayPalReq.h"
#import "BCPayPalVerifyReq.h"

#import "BCCategory.h"
#import "BCSubscription.h"
#import "BCAuthReq.h"

#pragma mark - BeeCloudDelegate

@protocol BeeCloudDelegate <NSObject>
@required
/**
 *  不同类型的请求，对应不同的响应
 *
 *  @param resp 响应体
 */
- (void)onBeeCloudResp:(BCBaseResp *)resp;

@end

#endif
