//
//  BCQueryBillsCountReqTest.m
//  BCPay
//
//  Created by Ewenlong03 on 15/11/27.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "BCTestHeader.h"
#import "BCQueryRefundsCountResp.h"

@interface BCQueryBillsCountReqTest : XCTestCase {
    BCQueryBillsCountReq *req;
}

@end

@implementation BCQueryBillsCountReqTest

- (void)setUp {
    [super setUp];
    // Put setup code here. This method is called before the invocation of each test method in the class.
    req = [[BCQueryBillsCountReq alloc] init];
    [BCPayCache sharedInstance].bcResp = [[BCQueryBillsCountResp alloc] initWithReq:req];
}

- (void)tearDown {
    // Put teardown code here. This method is called after the invocation of each test method in the class.
    [super tearDown];
    [BCPayCache sharedInstance].bcResp = nil;
}

- (void)test_doQueryBillsCountResponse {
    BCQueryBillsCountResp * resp = [req doQueryBillsCountResponse:nil];
    XCTAssertFalse(resp.count > 0);
    
    resp = [req doQueryBillsCountResponse:@{}];
    XCTAssertFalse(resp.count > 0);
    
    resp = [req doQueryBillsCountResponse:@{@"countq":@""}];
    XCTAssertFalse(resp.count > 0);
    
    resp = [req doQueryBillsCountResponse:@{@"count":@"0"}];
    XCTAssertFalse(resp.count > 0);
    
    resp = [req doQueryBillsCountResponse:@{@"count":@1}];
    XCTAssertTrue(resp.count > 0);
    
}

@end
