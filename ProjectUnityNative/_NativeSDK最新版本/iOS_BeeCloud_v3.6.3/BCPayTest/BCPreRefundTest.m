//
//  BCPreRefundTest.m
//  BCPay
//
//  Created by Ewenlong03 on 16/6/24.
//  Copyright © 2016年 BeeCloud. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "BCTestHeader.h"
#import "BCPreRefundReq.h"
#import "BCPreRefundResp.h"

@interface BCPreRefundTest : XCTestCase {
    BCPreRefundReq * req;
}

@end

@implementation BCPreRefundTest

- (void)setUp {
    [super setUp];
    // Put setup code here. This method is called before the invocation of each test method in the class.
//    [BeeCloud initWithAppID:TESTAPPID andAppSecret:TESTAPPSECRET];
    req = [[BCPreRefundReq alloc] init];
    [BCPayCache sharedInstance].bcResp = [[BCPreRefundResp alloc] init];
}

- (void)tearDown {
    // Put teardown code here. This method is called after the invocation of each test method in the class.
    [super tearDown];
}

- (NSString *)getNow {
    NSDateFormatter *dateFormatter = [[NSDateFormatter alloc] init];
    [dateFormatter setDateFormat:@"yyyyMMdd"];
    NSString *now = [dateFormatter stringFromDate:[NSDate date]];
    return now;
}

- (void)test_checkParametersForPreRefund {
    // This is an example of a functional test case.
    // Use XCTAssert and related functions to verify your tests produce the correct results.
    req.refundFee = @"aaa";
    XCTAssertFalse([req checkParametersForPreRefund]);
    req.refundFee = @"";
    XCTAssertFalse([req checkParametersForPreRefund]);
    req.refundFee = @"-1";
    XCTAssertFalse([req checkParametersForPreRefund]);
    req.refundFee = @"99.00";
    XCTAssertFalse([req checkParametersForPreRefund]);
    
    req.refundFee = @"1";
    req.billNo = @"";
    XCTAssertFalse([req checkParametersForPreRefund]);
    req.billNo = @"1";
    XCTAssertFalse([req checkParametersForPreRefund]);
    req.billNo = @"12345678_";
    XCTAssertFalse([req checkParametersForPreRefund]);
    req.billNo = @"123456781234567812345678123456781";
    XCTAssertFalse([req checkParametersForPreRefund]);
    
    req.billNo = @"123456789";
    req.refundNo = @"";
    XCTAssertFalse([req checkParametersForPreRefund]);
    req.refundNo = @"1";
    XCTAssertFalse([req checkParametersForPreRefund]);
    req.refundNo = @"12345678_";
    XCTAssertFalse([req checkParametersForPreRefund]);
    req.refundNo = @"123456781234567812345678123456781";
    XCTAssertFalse([req checkParametersForPreRefund]);
    req.refundNo = @"20160621111";
    XCTAssertFalse([req checkParametersForPreRefund]);
    
    req.refundNo = [NSString stringWithFormat:@"%@02691",[self getNow]];
    XCTAssertTrue([req checkParametersForPreRefund]);
}

@end
