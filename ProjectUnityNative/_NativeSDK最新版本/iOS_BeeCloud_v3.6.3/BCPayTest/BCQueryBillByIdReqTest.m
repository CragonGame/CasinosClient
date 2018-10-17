//
//  BCQueryBillByIdReqTest.m
//  BCPay
//
//  Created by Ewenlong03 on 15/11/27.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "BCTestHeader.h"
#import "BCQueryBillByIdReq.h"

@interface BCQueryBillByIdReqTest : XCTestCase {
    BCQueryBillByIdReq *req;
}

@end

@implementation BCQueryBillByIdReqTest

- (void)setUp {
    [super setUp];
    // Put setup code here. This method is called before the invocation of each test method in the class.
    req = [[BCQueryBillByIdReq alloc] init];
    [BCPayCache sharedInstance].bcResp = [[BCQueryBillByIdResp alloc] initWithReq:req];
}

- (void)tearDown {
    // Put teardown code here. This method is called after the invocation of each test method in the class.
    [super tearDown];
    [BCPayCache sharedInstance].bcResp = nil;
}

- (void)test_doQueryBillByIdResponse {
    BCQueryBillByIdResp *resp = [req doQueryBillByIdResponse:nil];
    
    XCTAssertNil(resp.bill);
    
    resp = [req doQueryBillByIdResponse:@{}];
    XCTAssertNil(resp.bill);
    
    resp = [req doQueryBillByIdResponse:@{@"pay": @""}];
    XCTAssertNil(resp.bill);
    
    resp = [req doQueryBillByIdResponse:@{@"pay": @{}}];
    XCTAssertNotNil(resp.bill);
}

@end
