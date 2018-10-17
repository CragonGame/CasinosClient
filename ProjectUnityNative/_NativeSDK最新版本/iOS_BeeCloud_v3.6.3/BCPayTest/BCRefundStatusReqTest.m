//
//  BCRefundStatusReqTest.m
//  BCPay
//
//  Created by Ewenlong03 on 15/11/27.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "BCTestHeader.h"
#import "BCRefundStatusReq.h"

@interface BCRefundStatusReqTest : XCTestCase {
    BCRefundStatusReq *req;
}

@end

@implementation BCRefundStatusReqTest

- (void)setUp {
    [super setUp];
    // Put setup code here. This method is called before the invocation of each test method in the class.
    req = [[BCRefundStatusReq alloc] init];
    [BCPayCache sharedInstance].bcResp = [[BCRefundStatusResp alloc] initWithReq:req];
}

- (void)tearDown {
    // Put teardown code here. This method is called after the invocation of each test method in the class.
    [super tearDown];
    [BCPayCache sharedInstance].bcResp = nil;
}

- (void)test_doQueryRefundStatus {
    
    BCRefundStatusResp *resp = [req doQueryRefundStatus:nil];
    XCTAssertNil(resp.refundStatus);
    
    resp = [req doQueryRefundStatus:@{@"refund":@"success"}];
    XCTAssertNotEqual(@"success",resp.refundStatus);
    
    resp = [req doQueryRefundStatus:@{@"refund_status":@"success"}];
    XCTAssertEqual(@"success",resp.refundStatus);
    
}

@end
