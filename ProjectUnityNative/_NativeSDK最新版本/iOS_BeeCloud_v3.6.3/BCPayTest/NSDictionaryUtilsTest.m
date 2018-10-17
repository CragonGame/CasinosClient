//
//  NSDictionaryUtilsTest.m
//  BCPay
//
//  Created by joseph on 15/11/7.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "NSDictionary+Utils.h"

@interface NSDictionaryUtilsTest : XCTestCase

@end

@implementation NSDictionaryUtilsTest

- (void)setUp {
    [super setUp];
    // Put setup code here. This method is called before the invocation of each test method in the class.
}

- (void)tearDown {
    // Put teardown code here. This method is called after the invocation of each test method in the class.
    [super tearDown];
}

- (void)testValueForKeyIsArray {
    NSDictionary *testDic = @{@"test":@[@"a",@"b"]};
    XCTAssertTrue([testDic valueForKeyIsArray:@"test"]);
    
    testDic = @{@"test":@1};
    XCTAssertFalse([testDic valueForKeyIsArray:@"test"]);
    XCTAssertFalse([testDic valueForKeyIsArray:@"testError"]);
}

- (void)testValueForKeyIsString {
    NSDictionary *testDic = @{@"test":@"a"};
    XCTAssertTrue([testDic valueForKeyIsString:@"test"]);
    
    testDic = @{@"test":@1};
    XCTAssertFalse([testDic valueForKeyIsString:@"test"]);
    XCTAssertFalse([testDic valueForKeyIsString:@"testError"]);
}

- (void)testValueForKeyIsNumber {
    NSDictionary *testDic = @{@"test":@1};
    XCTAssertTrue([testDic valueForKeyIsNumber:@"test"]);
    
    testDic = @{@"test":@"a"};
    XCTAssertFalse([testDic valueForKeyIsNumber:@"test"]);
    XCTAssertFalse([testDic valueForKeyIsNumber:@"testError"]);
}

- (void)testIntegerValueForKey {
    NSDictionary *testDic = @{@"test":@1};
    XCTAssertTrue([testDic integerValueForKey:@"test" defaultValue:0] == 1);
    XCTAssertFalse([testDic integerValueForKey:@"testError" defaultValue:0] == 1);
    
    testDic = @{@"test":@"a"};
    XCTAssertTrue([testDic integerValueForKey:@"test" defaultValue:0] == 0);
}

- (void)testLonglongValueForKey {
    NSDictionary *testDic = @{@"test":@1445112111234};
    XCTAssertTrue([testDic longlongValueForKey:@"test" defaultValue:(long long)1] == 1445112111234);
    XCTAssertFalse([testDic longlongValueForKey:@"testError" defaultValue:(long long)1] == 1445112111234);
    
    testDic = @{@"test":@"1"};
    XCTAssertTrue([testDic longlongValueForKey:@"test" defaultValue:0] == 0);
}

- (void)testFloatValueForKey {
    NSDictionary *testDic = @{@"test":@1.0};
    XCTAssertTrue([testDic floatValueForKey:@"test" defaultValue:2.0] == 1.0);
    XCTAssertTrue([testDic floatValueForKey:@"testError" defaultValue:2.0] == 2.0);
    
    testDic = @{@"test":@"1"};
    XCTAssertTrue([testDic floatValueForKey:@"test" defaultValue:0] == 0);
}

- (void)testBoolValueForKey {
    NSDictionary *testDic = @{@"test":@YES};
    XCTAssertTrue([testDic boolValueForKey:@"test" defaultValue:NO]);
    XCTAssertTrue([testDic boolValueForKey:@"testError" defaultValue:YES]);
    
    testDic = @{@"test":@"1"};
    XCTAssertTrue([testDic boolValueForKey:@"test" defaultValue:YES]);
}

- (void)testStringValueForKey {
    NSDictionary *testDic = @{@"test":@"yes"};
    XCTAssertTrue([[testDic stringValueForKey:@"test" defaultValue:@"1"] isEqualToString:@"yes"]);
    XCTAssertTrue([[testDic stringValueForKey:@"testError" defaultValue:@"1"] isEqualToString:@"1"]);
    
    testDic = @{@"test":@1};
    XCTAssertTrue([[testDic stringValueForKey:@"test" defaultValue:@"1"] isEqualToString:@"1"]);
}

- (void)testArrayValueForKey {
    NSDictionary *testDic = @{@"test":@[@"a",@"b"]};
    XCTAssertNotNil([testDic arrayValueForKey:@"test" defaultValue:nil]);
    XCTAssertNil([testDic arrayValueForKey:@"testError" defaultValue:nil]);
    
    testDic = @{@"test":@1};
    XCTAssertNil([testDic arrayValueForKey:@"test" defaultValue:nil]);
}

- (void)testDictValueForKey {
    NSDictionary *testDic = @{@"test":@{@"a":@"b"}};
    XCTAssertNotNil([testDic dictValueForKey:@"test" defaultValue:nil]);
    XCTAssertNil([testDic dictValueForKey:@"testError" defaultValue:nil]);
    
    testDic = @{@"test":@1};
    XCTAssertNil([testDic dictValueForKey:@"test" defaultValue:nil]);
}

@end
