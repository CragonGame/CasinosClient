//
//  BeeCloudTest.m
//  BCPay
//
//  Created by Ewenlong03 on 15/11/24.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "BCTestHeader.h"

@interface BeeCloudTest : XCTestCase

@end

@implementation BeeCloudTest

- (void)setUp {
    [super setUp];
    // Put setup code here. This method is called before the invocation of each test method in the class.
}

- (void)tearDown {
    // Put teardown code here. This method is called after the invocation of each test method in the class.
    [super tearDown];
    [BCPayCache sharedInstance].appId = @"";
    [BCPayCache sharedInstance].appSecret = @"";
}

- (void)test_initBeeCloud {
    XCTAssertFalse([BeeCloud initWithAppID:@"" andAppSecret:@""]);
    XCTAssertFalse([BeeCloud initWithAppID:TESTAPPID andAppSecret:@""]);
    XCTAssertFalse([BeeCloud initWithAppID:@"" andAppSecret:TESTAPPSECRET]);
    XCTAssertTrue([BeeCloud initWithAppID:TESTAPPID andAppSecret:TESTAPPSECRET]);
}

- (void)test_initWeChat {
    XCTAssertFalse([BeeCloud initWeChatPay:@""]);
    XCTAssertTrue([BeeCloud initWeChatPay:@"wxf1aa465362b4c8f1"]);
}

- (void)test_initPayPal {
    XCTAssertFalse([BeeCloud initPayPal:@"" secret:@"" sandbox:YES]);
    XCTAssertFalse([BeeCloud initPayPal:@"AVT1Ch18aTIlUJIeeCxvC7ZKQYHczGwiWm8jOwhrREc4a5FnbdwlqEB4evlHPXXUA67RAAZqZM0H8TCR" secret:@"" sandbox:YES]);
    XCTAssertFalse([BeeCloud initPayPal:@"" secret:@"EL-fkjkEUyxrwZAmrfn46awFXlX-h2nRkyCVhhpeVdlSRuhPJKXx3ZvUTTJqPQuAeomXA8PZ2MkX24vF" sandbox:YES]);
    XCTAssertTrue([BeeCloud initPayPal:@"AVT1Ch18aTIlUJIeeCxvC7ZKQYHczGwiWm8jOwhrREc4a5FnbdwlqEB4evlHPXXUA67RAAZqZM0H8TCR" secret:@"EL-fkjkEUyxrwZAmrfn46awFXlX-h2nRkyCVhhpeVdlSRuhPJKXx3ZvUTTJqPQuAeomXA8PZ2MkX24vF" sandbox:YES]);
}

@end
