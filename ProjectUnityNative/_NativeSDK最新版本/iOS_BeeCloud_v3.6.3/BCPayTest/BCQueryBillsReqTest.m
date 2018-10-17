//
//  BCQueryBillsReqTest.m
//  BCPay
//
//  Created by Ewenlong03 on 15/11/27.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "BCTestHeader.h"
#import "BCQueryBillsReq.h"

@interface BCQueryBillsReqTest : XCTestCase {
    BCQueryBillsReq *req;
}

@end

@implementation BCQueryBillsReqTest

- (void)setUp {
    [super setUp];
    // Put setup code here. This method is called before the invocation of each test method in the class.
    req = [[BCQueryBillsReq alloc] init];
    [BCPayCache sharedInstance].bcResp = [[BCQueryBillsResp alloc] initWithReq:req];
}

- (void)tearDown {
    // Put teardown code here. This method is called after the invocation of each test method in the class.
    [super tearDown];
     [BCPayCache sharedInstance].bcResp = nil;
}

- (void)test_doQueryResponse {

    BCQueryBillsResp *resp = [req doQueryBillsResponse:@{@"result_code":@0,@"result_msg":@"OK",@"err_detail":@""}];
    XCTAssertFalse(resp.count > 0);
    
    resp = [req doQueryBillsResponse:@{@"result_code":@0,@"result_msg":@"OK",@"err_detail":@"", @"count":@0}];
    XCTAssertTrue(resp.count==0);
}

- (void)test_parseBillsResults {
    
    NSMutableArray *results = [req parseBillsResults:@{}];
    XCTAssertFalse(results.count > 0);
    
    results = [req parseBillsResults:@{@"bills":@[@{@"title":@"test",@"spay_result":@YES}]}];
    XCTAssertNotNil(results);
    
    results = [req parseBillsResults:@{@"bills":@[@{@"title":@"test",@"spay_result":@YES}]}];
    XCTAssertNotNil(results);
}

@end
