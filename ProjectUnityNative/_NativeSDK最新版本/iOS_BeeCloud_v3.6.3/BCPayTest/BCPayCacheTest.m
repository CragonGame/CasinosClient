//
//  BCPayCacheTest.m
//  BCPay
//
//  Created by Ewenlong03 on 15/12/24.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "BCPayCache.h"
#import "BeeCloud.h"

@interface BCPayCacheTest : XCTestCase<BeeCloudDelegate>

@end

@implementation BCPayCacheTest

- (void)setUp {
    [super setUp];
    // Put setup code here. This method is called before the invocation of each test method in the class.
}

- (void)tearDown {
    // Put teardown code here. This method is called after the invocation of each test method in the class.
    [super tearDown];
    [BeeCloud setBeeCloudDelegate:nil];
}

- (void)test_beecloudDoResponse {
    XCTAssertFalse([BCPayCache beeCloudDoResponse]);
    
    [BeeCloud setBeeCloudDelegate:self];
    
    XCTAssertTrue([BCPayCache beeCloudDoResponse]);
}

- (void)onBeeCloudResp:(BCBaseResp *)resp {
    
}

@end
