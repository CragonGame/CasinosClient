//
//  BCQueryRefundsCountReqTest.m
//  BCPay
//
//  Created by Ewenlong03 on 15/11/27.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "BCTestHeader.h"
#import "BCQueryRefundsCountReq.h"

@interface BCQueryRefundsCountReqTest : XCTestCase {
    BCQueryRefundsCountReq *req;
}

@end

@implementation BCQueryRefundsCountReqTest

- (void)setUp {
    [super setUp];
    // Put setup code here. This method is called before the invocation of each test method in the class.
    req = [[BCQueryRefundsCountReq alloc] init];
    [BCPayCache sharedInstance].bcResp = [[BCQueryRefundsCountResp alloc] initWithReq:req];
}

- (void)tearDown {
    // Put teardown code here. This method is called after the invocation of each test method in the class.
    [super tearDown];
    [BCPayCache sharedInstance].bcResp = nil;
}

- (void)test {
    BCQueryRefundsCountResp * resp = [req doQueryRefundsCountResponse:nil];
    XCTAssertFalse(resp.count > 0);
    
    resp = [req doQueryRefundsCountResponse:@{}];
    XCTAssertFalse(resp.count > 0);
    
    resp = [req doQueryRefundsCountResponse:@{@"countq":@""}];
    XCTAssertFalse(resp.count > 0);
    
    resp = [req doQueryRefundsCountResponse:@{@"count":@"0"}];
    XCTAssertFalse(resp.count > 0);
    
    resp = [req doQueryRefundsCountResponse:@{@"count":@1}];
    XCTAssertTrue(resp.count > 0);
}

@end
