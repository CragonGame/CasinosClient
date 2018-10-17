//
//  BCPayResp.h
//  BCPaySDK
//
//  Created by Ewenlong03 on 15/7/27.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//
#import "BCBaseResp.h"
#import "BCPayReq.h"

#pragma mark BCPayResp
/**
 *  支付请求的响应
 */
@interface BCPayResp : BCBaseResp  //type=201;
/**
 *  部分渠道(Ali,Baidu)回调会返回一些信息。
 *  Ali是@{@"resultStatus":@"9000",
           @"memo": @"",
           @"result": @"partner="2088101568358171"&seller_id= "xxx@beecloud.cn"&out_trade_no="2015111712120048"&subject="test"&body="test"
     "&total_fee="0.01"&it_b_pay= "30m"&..."}
 *  Baidu是结构为@{@"orderInfo":@"...."};其中orderInfo对应的value为前台支付必须;
 */
@property (nonatomic, retain) NSDictionary *paySource;

@end
