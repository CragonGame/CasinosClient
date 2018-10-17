//
//  BCPayTests.m
//  BCPayTests
//
//  Created by joseph on 15/11/5.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "BCTestHeader.h"
#import "BeeCloud+Utils.h"

@interface BeeCloudUtilsTest : XCTestCase {
    BeeCloud *instance;
}

@end

@implementation BeeCloudUtilsTest

- (void)setUp {
    [super setUp];
    // Put setup code here. This method is called before the invocation of each test method in the class.
    [BeeCloud initWithAppID:TESTAPPID andAppSecret:TESTAPPSECRET];
}

- (void)tearDown {
    // Put teardown code here. This method is called after the invocation of each test method in the class.
    [super tearDown];
}


@end
