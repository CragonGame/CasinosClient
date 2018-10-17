//
//  BCQueryRefundByIdReqTest.m
//  BCPay
//
//  Created by Ewenlong03 on 15/11/27.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "BCTestHeader.h"
#import "BCQueryRefundByIdReq.h"

@interface BCQueryRefundByIdReqTest : XCTestCase {
    BCQueryRefundByIdReq *req;
}

@end

@implementation BCQueryRefundByIdReqTest

- (void)setUp {
    [super setUp];
    // Put setup code here. This method is called before the invocation of each test method in the class.
    req = [[BCQueryRefundByIdReq alloc] init];
    [BCPayCache sharedInstance].bcResp = [[BCQueryRefundByIdResp alloc] initWithReq:req];
}

- (void)tearDown {
    // Put teardown code here. This method is called after the invocation of each test method in the class.
    [super tearDown];
    [BCPayCache sharedInstance].bcResp = nil;
}

- (void)test_doQueryRefundByIdResponse {
    BCQueryRefundByIdResp *resp = [req doQueryRefundByIdResponse:nil];
    XCTAssertNil(resp.refund);
    
    resp = [req doQueryRefundByIdResponse:@{}];
    XCTAssertNil(resp.refund);
    
    resp = [req doQueryRefundByIdResponse:@{@"pay": @""}];
    XCTAssertNil(resp.refund);
    
    resp = [req doQueryRefundByIdResponse:@{@"pay": @{}}];
    XCTAssertNil(resp.refund);
    
    resp = [req doQueryRefundByIdResponse:@{@"refund": @""}];
    XCTAssertNil(resp.refund);
    
    resp = [req doQueryRefundByIdResponse:@{@"refund": @{}}];
    XCTAssertNotNil(resp.refund);
}

@end
