//
//  BCQueryRefundsReqTest.m
//  BCPay
//
//  Created by Ewenlong03 on 15/11/27.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "BCTestHeader.h"
#import "BCQueryRefundsReq.h"

@interface BCQueryRefundsReqTest : XCTestCase {
    BCQueryRefundsReq *req ;
}

@end

@implementation BCQueryRefundsReqTest

- (void)setUp {
    [super setUp];
    // Put setup code here. This method is called before the invocation of each test method in the class.
    req = [[BCQueryRefundsReq alloc] init];
    [BCPayCache sharedInstance].bcResp = [[BCQueryRefundsResp alloc] initWithReq:req];
}

- (void)tearDown {
    // Put teardown code here. This method is called after the invocation of each test method in the class.
    [super tearDown];
    [BCPayCache sharedInstance].bcResp = nil;
}

- (void)test_doQueryResponse {
    
    BCQueryRefundsResp *resp = [req doQueryRefundsResponse:@{@"result_code":@0,@"result_msg":@"OK",@"err_detail":@""}];
    XCTAssertFalse(resp.count > 0);
    
    resp = [req doQueryRefundsResponse:@{@"result_code":@0,@"result_msg":@"OK",@"err_detail":@"", @"count":@0}];
    XCTAssertTrue(resp.count == 0);
}

- (void)test_parseRefundsResults {
    
    NSMutableArray *results = [req parseRefundsResults:@{}];
    XCTAssertFalse(results.count > 0);
    
    results = [req parseRefundsResults:@{@"refunds":@[@{@"title":@"test",@"refund_no":@"2015010202"}]}];
    XCTAssertNotNil(results);
    
    results = [req parseRefundsResults:@{@"refunds":@[@{@"title":@"test",@"refund_no":@"2015010202"}]}];
    XCTAssertNotNil(results);
}

@end
