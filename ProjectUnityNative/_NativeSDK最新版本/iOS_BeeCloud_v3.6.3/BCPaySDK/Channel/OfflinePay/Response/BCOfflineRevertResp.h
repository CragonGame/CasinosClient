//
//  BCOfflineRevertResp.h
//  BCPay
//
//  Created by Ewenlong03 on 15/9/17.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BCBaseResp.h"
#import "BCOfflineRevertReq.h"

@interface BCOfflineRevertResp : BCBaseResp
/**
 *  订单撤销状态
 *  YES 表示撤销成功
 *  NO  表示撤销失败
 *  撤销失败的订单可重新发起撤销请求
 */
@property (nonatomic, assign) BOOL revertStatus;

@end
