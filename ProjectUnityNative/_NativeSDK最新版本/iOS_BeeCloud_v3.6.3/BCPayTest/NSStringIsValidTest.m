//
//  NSStringIsValidTest.m
//  BCPay
//
//  Created by joseph on 15/11/7.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "NSString+IsValid.h"
#import "BCTestHeader.h"


@interface NSStringIsValidTest : XCTestCase

@end

@implementation NSStringIsValidTest

- (void)setUp {
    [super setUp];
    // Put setup code here. This method is called before the invocation of each test method in the class.
}

- (void)tearDown {
    // Put teardown code here. This method is called after the invocation of each test method in the class.
    [super tearDown];
}

- (void)testIsValid {
    NSString *testString = @"";
    XCTAssertFalse(testString.isValid);
    
    testString = nil;
    XCTAssertFalse(testString.isValid);
    
    testString = @"BeeCloud";
    XCTAssertTrue(testString.isValid);
}

- (void)testIsPureInt {
    NSString *testString = @"";
    XCTAssertFalse(testString.isPureInt);
    
    testString = @"a";
    XCTAssertFalse(testString.isPureInt);
    
    testString = @"12a";
    XCTAssertFalse(testString.isPureInt);
    
    testString = @"123.1";
    XCTAssertFalse(testString.isPureInt);
    
    testString = @"1221";
    XCTAssertTrue(testString.isPureInt);
    
    testString = @"0123";
    XCTAssertTrue(testString.isPureInt);
}

- (void)testIsValidTraceNo {
    NSString *testString = @"";
    XCTAssertFalse(testString.isValidTraceNo);
    
    testString = @"ad1_27A";
    XCTAssertFalse(testString.isValidTraceNo);
    
    testString = @"%AADAAddd";
    XCTAssertFalse(testString.isValidTraceNo);
    
    testString = @"12535671a";
    XCTAssertTrue(testString.isValidTraceNo);
}

- (void)testIsPureFloat {
    NSString *testString = @"";
    XCTAssertFalse(testString.isPureFloat);
    
    testString = @" ";
    XCTAssertFalse(testString.isPureFloat);
    
    testString = @"123";
    XCTAssertTrue(testString.isPureFloat);
    
    testString = @"123.000001";
    XCTAssertTrue(testString.isPureFloat);
}

- (void)testIsValidUUID {
    NSString *testString = @"";
    XCTAssertFalse(testString.isValidUUID);
    
    testString = nil;
    XCTAssertFalse(testString.isValidUUID);
    
    testString = TESTAPPID;
    XCTAssertFalse([testString stringByReplacingOccurrencesOfString:@"-" withString:@""].isValidUUID);
    
    testString = TESTAPPID;
    XCTAssertTrue(testString.isValidUUID);
}

@end
