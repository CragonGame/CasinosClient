//
//  BCPayReqTest.m
//  BCPay
//
//  Created by Ewenlong03 on 15/11/27.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "BCTestHeader.h"
#import "BCPayReq.h"

@interface BCPayReqTest : XCTestCase {
    BCPayReq *req;
}

@end

@implementation BCPayReqTest

- (void)setUp {
    [super setUp];
    // Put setup code here. This method is called before the invocation of each test method in the class.
    req = [[BCPayReq alloc] init];
    [BCPayCache sharedInstance].bcResp = [[BCPayResp alloc] initWithReq:req];
}

- (void)tearDown {
    // Put teardown code here. This method is called after the invocation of each test method in the class.
    [super tearDown];
    [BCPayCache sharedInstance].bcResp = nil;
}

- (void)test_checkParametersForReqPay {
    
    req.title = @"";
    XCTAssertFalse([req checkParametersForReqPay]);
    
    req.title = @"123456781234567812345678123456781";
    XCTAssertFalse([req checkParametersForReqPay]);
    
    req.title = @"比可网络比可网络比可网络比可网络比可网络";
    XCTAssertFalse([req checkParametersForReqPay]);
    
    req.title = @"test";
    req.totalFee = @"aaa";
    XCTAssertFalse([req checkParametersForReqPay]);
    
    req.totalFee = @"";
    XCTAssertFalse([req checkParametersForReqPay]);
    req.totalFee = @"-1";
    XCTAssertFalse([req checkParametersForReqPay]);
    req.totalFee = @"99.00";
    XCTAssertFalse([req checkParametersForReqPay]);
    
    req.totalFee = @"1";
    req.billNo = @"1212";
    XCTAssertFalse([req checkParametersForReqPay]);
    
    req.billNo = @"abcdefgh&*&^";
    XCTAssertFalse([req checkParametersForReqPay]);
    
    req.billNo = @"123456781234567812345678123456781";
    XCTAssertFalse([req checkParametersForReqPay]);
    
    req.billNo = @"2015110616001200";
    req.channel = PayChannelAliApp;
    req.scheme = @"";
    XCTAssertFalse([req checkParametersForReqPay]);
    
    req.channel = PayChannelUnApp;
    req.viewController = nil;
    XCTAssertFalse([req checkParametersForReqPay]);
    
    req.channel = PayChannelWxApp;//因为没有安装微信，所以check失败
    XCTAssertFalse([req checkParametersForReqPay]);
    
    req.channel = PayChannelBaiduApp;
    XCTAssertTrue([req checkParametersForReqPay]);
}

- (void)test_doPayAction {
    
    req.channel = PayChannelWxApp;
    XCTAssertFalse([req doPayAction:@{}]);
    
    req.channel = PayChannelAliApp;
    req.scheme = @"test";
    XCTAssertFalse([req doPayAction:@{}]);
    
    req.channel = PayChannelUnApp;
    req.viewController = [[UIViewController alloc] init];
    XCTAssertFalse([req doPayAction:@{}]);
    
    req.channel = PayChannelBaiduApp;
    XCTAssertFalse([req doPayAction:@{}]);
    
    req.channel = PayChannelBaiduApp;
    XCTAssertFalse([req doPayAction:@{@"orderinfo":@"test"}]);
    
    req.channel = PayChannelBaiduApp;
    XCTAssertTrue([req doPayAction:@{@"orderInfo":@"test"}]);
    
    req.channel = PayChannelAli;
    XCTAssertFalse([req doPayAction:@{}]);
}



@end
