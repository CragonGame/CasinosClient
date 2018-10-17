//
//  BCPayUtilTest.m
//  BCPay
//
//  Created by joseph on 15/11/7.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import <XCTest/XCTest.h>
#import "BCPayUtil.h"
#import "BCTestHeader.h"

@interface BCPayUtilTest : XCTestCase

@end

@implementation BCPayUtilTest

- (void)setUp {
    [super setUp];
    // Put setup code here. This method is called before the invocation of each test method in the class.
    [BeeCloud initWithAppID:TESTAPPID andAppSecret:TESTAPPSECRET];
    [BCPayCache sharedInstance].bcResp = [[BCBaseResp alloc] init];
}

- (void)tearDown {
    // Put teardown code here. This method is called after the invocation of each test method in the class.
    [super tearDown];
    [BCPayCache sharedInstance].bcResp = nil;
    [BCPayCache sharedInstance].appId = @"";
    [BCPayCache sharedInstance].appSecret = @"";
}

- (void)testGetAFHTTPSessionManager {
    XCTAssertNotNil([BCPayUtil getBCHTTPSessionManager]);
}

- (void)testGetWrappedParametersForGetRequest {
    NSMutableDictionary * getParams = [BCPayUtil getWrappedParametersForGetRequest:@{@"key1": @"key2"}];
    XCTAssertTrue([[getParams allKeys] containsObject:@"para"]);
}

- (void)testPrepareParametersForRequest {
    NSMutableDictionary *params = [BCPayUtil prepareParametersForRequest];
    XCTAssertNotNil(params);
    XCTAssertNotNil([params objectForKey:@"app_id"]);
    XCTAssertNotNil([params objectForKey:@"app_sign"]);
    XCTAssertNotNil([params objectForKey:@"timestamp"]);
    [BCPayCache sharedInstance].appId = @"";
    [BCPayCache sharedInstance].appSecret = @"";
    XCTAssertNil([BCPayUtil prepareParametersForRequest]);
    [BeeCloud initWithAppID:TESTAPPID andAppSecret:TESTAPPSECRET];
}

- (void)testGetAppSignature {
    NSNumber *timeStamp = [NSNumber numberWithLongLong:[BCPayUtil dateToMillisecond:[NSDate date]]];
    NSString *appSign = [BCPayUtil getAppSignature:[NSString stringWithFormat:@"%@",timeStamp]];
    XCTAssertNotNil(appSign);
    
    [BCPayCache sharedInstance].appId = @"";
    appSign = [BCPayUtil getAppSignature:[NSString stringWithFormat:@"%@",timeStamp]];
    XCTAssertNil(appSign);
    
    appSign = [BCPayUtil getAppSignature:[NSString stringWithFormat:@"%@",timeStamp]];
    XCTAssertNil(appSign);
    
    [BeeCloud initWithAppID:TESTAPPID andAppSecret:TESTAPPSECRET];
}

- (void)testGetUrlType {
    NSURL *url = [NSURL URLWithString:@"wxtest://pay?test=yes"];
    XCTAssertTrue(BCPayUrlWeChat == [BCPayUtil getUrlType:url]);
    
    url = [NSURL URLWithString:@"alipay://safepay?test=yes"];
    XCTAssertTrue(BCPayUrlAlipay == [BCPayUtil getUrlType:url]);
    
    url = [NSURL URLWithString:@"beecloud://pay?test=no"];
    XCTAssertTrue(BCPayUrlUnknown == [BCPayUtil getUrlType:url]);
}

- (void)testGetBestHostWithFormat {
    [BeeCloud setSandboxMode:NO];
    XCTAssertFalse([[BCPayUtil getBestHostWithFormat:kRestApiPay] rangeOfString:@"sandbox"].length == 7);
    
    [BCPayCache sharedInstance].sandbox = YES;
    XCTAssertTrue([[BCPayUtil getBestHostWithFormat:kRestApiPay] rangeOfString:@"sandbox"].length == 7);
    [BCPayCache sharedInstance].sandbox = NO;
}

- (void)testGetChannelString {
    XCTAssertEqualObjects(@"WX", [BCPayUtil getChannelString:PayChannelWx]);
    XCTAssertEqualObjects(@"WX_APP", [BCPayUtil getChannelString:PayChannelWxApp]);
    XCTAssertEqualObjects(@"WX_NATIVE", [BCPayUtil getChannelString:PayChannelWxNative]);
    XCTAssertEqualObjects(@"WX_JSAPI", [BCPayUtil getChannelString:PayChannelWxJsApi]);
    XCTAssertEqualObjects(@"WX_SCAN", [BCPayUtil getChannelString:PayChannelWxScan]);
    
    XCTAssertEqualObjects(@"ALI", [BCPayUtil getChannelString:PayChannelAli]);
    XCTAssertEqualObjects(@"ALI_APP", [BCPayUtil getChannelString:PayChannelAliApp]);
    XCTAssertEqualObjects(@"ALI_WEB", [BCPayUtil getChannelString:PayChannelAliWeb]);
    XCTAssertEqualObjects(@"ALI_WAP", [BCPayUtil getChannelString:PayChannelAliWap]);
    XCTAssertEqualObjects(@"ALI_QRCODE", [BCPayUtil getChannelString:PayChannelAliQrCode]);
    XCTAssertEqualObjects(@"ALI_OFFLINE_QRCODE", [BCPayUtil getChannelString:PayChannelAliOfflineQrCode]);
    XCTAssertEqualObjects(@"ALI_SCAN", [BCPayUtil getChannelString:PayChannelAliScan]);
    
    XCTAssertEqualObjects(@"UN", [BCPayUtil getChannelString:PayChannelUn]);
    XCTAssertEqualObjects(@"UN_APP", [BCPayUtil getChannelString:PayChannelUnApp]);
    XCTAssertEqualObjects(@"UN_WEB", [BCPayUtil getChannelString:PayChannelUnWeb]);
    
    XCTAssertEqualObjects(@"BD", [BCPayUtil getChannelString:PayChannelBaidu]);
    XCTAssertEqualObjects(@"BD_APP", [BCPayUtil getChannelString:PayChannelBaiduApp]);
    XCTAssertEqualObjects(@"BD_WEB", [BCPayUtil getChannelString:PayChannelBaiduWeb]);
    XCTAssertEqualObjects(@"BD_WAP", [BCPayUtil getChannelString:PayChannelBaiduWap]);
    
    XCTAssertEqualObjects(@"PAYPAL", [BCPayUtil getChannelString:PayChannelPayPal]);
    XCTAssertEqualObjects(@"PAYPAL_LIVE", [BCPayUtil getChannelString:PayChannelPayPalLive]);
    XCTAssertEqualObjects(@"PAYPAL_SANDBOX", [BCPayUtil getChannelString:PayChannelPayPalSandbox]);
    
    XCTAssertEqualObjects(@"", [BCPayUtil getChannelString:60]);
}

- (void)testDoErrorResponse {
    BCBaseResp *resp = [BCPayUtil doErrorResponse:@"BeeCloud"];
    XCTAssertEqual(@"BeeCloud", resp.resultMsg);
    XCTAssertEqual(@"BeeCloud", resp.errDetail);
    XCTAssertTrue(resp.resultCode == BCErrCodeCommon);
}

- (void)testGetErrorInResponse {
    
    BCBaseResp *resp = [BCPayUtil getErrorInResponse:@{}];
    XCTAssertFalse(resp.resultCode == BCErrCodeSuccess);
    XCTAssertNotEqual(resp.resultMsg, @"OK");
    XCTAssertNotEqual(resp.errDetail, @"");
    
    resp = [BCPayUtil getErrorInResponse:@{@"resultCode":@0,@"resultMsg":@"OK",@"errDetail":@""}];
    XCTAssertFalse(resp.resultCode == BCErrCodeSuccess);
    XCTAssertNotEqual(resp.resultMsg, @"OK");
    XCTAssertNotEqual(resp.errDetail, @"");
    
    resp = [BCPayUtil getErrorInResponse:@{@"result_code":@0,@"result_msg":@"OK",@"err_detail":@""}];
    XCTAssertTrue(resp.resultCode == BCErrCodeSuccess);
    XCTAssertEqual(resp.resultMsg, @"OK");
    XCTAssertEqual(resp.errDetail, @"");
}

- (void)testGenerateRandomUUID {
    XCTAssertEqual([BCPayUtil generateRandomUUID].length, 36);
}

- (void)testMillisecondToDate {
    NSString * dateString1 = [BCPayUtil dateToString:[NSDate date]];
    long long timeStamp = [BCPayUtil dateStringToMillisecond:dateString1];
    NSString *dateString2 = [BCPayUtil millisecondToDateString:timeStamp];
    XCTAssertEqualObjects(dateString1, dateString2);
}

- (void)testDateToString {
    XCTAssertNil([BCPayUtil dateToString:nil]);
    XCTAssertNotNil([BCPayUtil dateToString:[NSDate date]]);
}

- (void)testMillisecondToDateString {
    XCTAssertNotNil([BCPayUtil millisecondToDateString:[BCPayUtil dateToMillisecond:[NSDate date]]]);
}

- (void)testDateStringToMillisecond {
    XCTAssertTrue([BCPayUtil dateStringToMillisecond:@""] == 0);
    XCTAssertTrue([BCPayUtil dateStringToMillisecond:@"2015-12-24 11:24"] != 0);
}

- (void)testStringToMD5 {
    XCTAssertEqualObjects([BCPayUtil stringToMD5:@""], @"");
    XCTAssertEqualObjects([BCPayUtil stringToMD5:nil], @"");
    
    XCTAssertNotEqualObjects([BCPayUtil stringToMD5:@"test"], @"");
    [BeeCloud setWillPrintLog:YES];
    BCPayLog(@"Do %s", __func__);
}

- (void)testIsValidEmail {
    XCTAssertFalse([BCPayUtil isValidEmail:@"12?/@beecloud.cn"]);
    XCTAssertFalse([BCPayUtil isValidEmail:@"dwojodwjo.cn"]);
    XCTAssertFalse([BCPayUtil isValidEmail:@"@beecloud.cn"]);
    
    XCTAssertTrue([BCPayUtil isValidEmail:@"hwl@beecloud.cn"]);
}

- (void)testIsValidMobile {
    XCTAssertFalse([BCPayUtil isValidMobile:@"0125363"]);
    XCTAssertFalse([BCPayUtil isValidMobile:@"1381562711a"]);
    XCTAssertFalse([BCPayUtil isValidMobile:@"33815627115"]);
    XCTAssertTrue([BCPayUtil isValidMobile:@"13305120512"]);
}

- (void)testIsLetter {
    XCTAssertFalse([BCPayUtil isLetter:'&']);
    XCTAssertFalse([BCPayUtil isLetter:'1']);
    XCTAssertTrue([BCPayUtil isLetter:'A']);
}

- (void)testIsDigit {
    XCTAssertFalse([BCPayUtil isDigit:'a']);
    XCTAssertFalse([BCPayUtil isDigit:'*']);
    XCTAssertTrue([BCPayUtil isDigit:'1']);
}

- (void)testGetBytes {
    XCTAssertTrue([BCPayUtil getBytes:@""] == 0);
    XCTAssertTrue([BCPayUtil getBytes:nil] == 0);
    XCTAssertTrue([BCPayUtil getBytes:@"wo"] == 2);
    XCTAssertTrue([BCPayUtil getBytes:@"我"] == 2);
    XCTAssertFalse([BCPayUtil getBytes:@"1"] == 2);
}

@end
