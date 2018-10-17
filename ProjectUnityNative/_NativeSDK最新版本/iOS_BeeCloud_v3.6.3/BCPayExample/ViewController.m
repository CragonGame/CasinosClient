//
//  ViewController.m
//  BeeCloudDemo
//
//  Created by RInz on 15/2/5.
//  Copyright (c) 2015年 RInz. All rights reserved.
//

#import "ViewController.h"
#import "QueryResultViewController.h"
#import "BCNetworking.h"
#import "BCOffinePay.h"
#import "GenQrCode.h"
#import "QRCodeViewController.h"
#import "ScanViewController.h"
#import "PayChannelCell.h"
#import "BDWalletSDKMainManager.h"

@interface ViewController ()<BeeCloudDelegate, SCanViewDelegate, QRCodeDelegate,BDWalletSDKMainManagerDelegate> {
//    PayPalConfiguration * _payPalConfig;
//    PayPalPayment *_completedPayment;
    PayChannel currentChannel;
    NSMutableArray *channelList;
    NSString * billTitle;
}

@end

@implementation ViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    self.view.backgroundColor = [UIColor whiteColor];
    
    if (self.actionType == 0) {
        self.title = @"支付";
        self.title = @"我的订单";
    } else if (self.actionType == 1) {
        self.title = @"查询支付订单";
    } else if (self.actionType == 2) {
        self.title = @"查询退款订单";
    }
    
    NSArray *live = @[@{@"channel":@"线上支付",
                      @"subChannel":@[@{@"sub":@(PayChannelWxApp), @"img":@"wx", @"title":@"微信支付"},
                                      @{@"sub":@(PayChannelBCWXApp), @"img":@"wx", @"title":@"BC微信支付"},
                                      @{@"sub":@(PayChannelAliApp), @"img":@"ali", @"title":@"支付宝"},
                                      @{@"sub":@(PayChannelBCAliApp), @"img":@"ali", @"title":@"BC支付宝"},
                                      @{@"sub":@(PayChannelUnApp), @"img":@"un", @"title":@"银联在线"},
                                      @{@"sub":@(PayChannelBCApp), @"img":@"beepay", @"title":@"BeePay"},
                                      @{@"sub":@(PayChannelBaiduApp), @"img":@"baidu", @"title":@"百度钱包"},
                                      @{@"sub":@(PayChannelPayPal), @"img":@"paypal", @"title":@"PayPal"},
                                      @{@"sub":@(PayChannelApplePayTest), @"img":@"apple", @"title":@"ApplePay"} //相关文档 http://help.beecloud.cn/hc/kb/article/177410/?from=draft
                                      ]},
                    @{@"channel":@"线下收款",
                      @"subChannel":@[@{@"sub":@(PayChannelWxNative), @"img":@"wx", @"title":@"微信扫码"},
                                      @{@"sub":@(PayChannelWxScan), @"img":@"wx", @"title":@"微信刷卡"},
                                      @{@"sub":@(PayChannelAliOfflineQrcode), @"img":@"ali", @"title":@"支付宝扫码"},
                                      @{@"sub":@(PayChannelAliScan), @"img":@"ali", @"title":@"支付宝刷卡"}]},
                      @{@"channel":@"BC线下收款",
                        @"subChannel":@[@{@"sub":@(PayChannelBCNative), @"img":@"wx", @"title":@"微信扫码"},
                                        @{@"sub":@(PayChannelBCWxScan), @"img":@"wx", @"title":@"微信刷卡"},
                                        @{@"sub":@(PayChannelBCAliQrcode), @"img":@"ali", @"title":@"支付宝扫码"},
                                        @{@"sub":@(PayChannelBCAliScan), @"img":@"ali", @"title":@"支付宝刷卡"}]}
                ];
    
    NSArray *sandbox = @[@{@"channel":@"线上支付",
                        @"subChannel":@[@{@"sub":@(PayChannelWxApp), @"img":@"wx", @"title":@"微信支付"},
                                        @{@"sub":@(PayChannelBCWXApp), @"img":@"wx", @"title":@"BC微信支付"},
                                        @{@"sub":@(PayChannelAliApp), @"img":@"ali", @"title":@"支付宝"},
                                        @{@"sub":@(PayChannelUnApp), @"img":@"un", @"title":@"银联在线"},
                                        @{@"sub":@(PayChannelBaiduApp), @"img":@"baidu", @"title":@"百度钱包"},
                                        @{@"sub":@(PayChannelApplePay), @"img":@"apple", @"title":@"ApplePay"}
                                        ]}];
    channelList = [NSMutableArray arrayWithArray:[BeeCloud getCurrentMode] ? sandbox : live];
    
    billTitle = [BeeCloud getCurrentMode] ? @"iOS Demo Sandbox" : @"iOS Demo Live";
    self.orderList = nil;
    
}

- (void)viewWillAppear:(BOOL)animated {
#pragma mark - 设置delegate
    [BeeCloud setBeeCloudDelegate:self];
}

#pragma mark - 微信、支付宝、银联、百度钱包

- (void)doPay:(PayChannel)channel {
    NSString *billno = [self genBillNo];
    NSMutableDictionary *dict = [NSMutableDictionary dictionaryWithObjectsAndKeys:@"value",@"key", nil];
    /**
        按住键盘上的option键，点击参数名称，可以查看参数说明
     **/
    BCPayReq *payReq = [[BCPayReq alloc] init];
    /**
     *  支付渠道，PayChannelWxApp,PayChannelAliApp,PayChannelUnApp,PayChannelBaiduApp
     */
    payReq.channel = channel; //支付渠道
    payReq.title = billTitle;//订单标题
    payReq.totalFee = @"10";//订单价格; channel为BC_APP的时候最小值为100，即1元
    payReq.billNo = billno;//商户自定义订单号
    payReq.scheme = @"payDemo";//URL Scheme,在Info.plist中配置; 支付宝,银联必有参数
    payReq.billTimeOut = 300;//订单超时时间
    payReq.viewController = self; //银联支付和Sandbox环境必填
    payReq.cardType = 0; //0 表示不区分卡类型；1 表示只支持借记卡；2 表示支持信用卡；默认为0
    payReq.optional = dict;//商户业务扩展参数，会在webhook回调时返回
    [BeeCloud sendBCReq:payReq];
}

- (void)doOfflinePay:(PayChannel)channel authCode:(NSString *)authcode {
    NSString *billno = [self genBillNo];
    NSMutableDictionary *dict = [NSMutableDictionary dictionaryWithObjectsAndKeys:@"value",@"key", nil];
    
    /**
     按住键盘上的option键，点击参数名称，可以查看参数说明
     **/
    BCOfflinePayReq *payReq = [[BCOfflinePayReq alloc] init];
    payReq.channel = channel; //支付渠道，支持WX_NATIVE、WX_SCAN、ALI_OFFLINE_QRCODE、ALI_SCAN
    payReq.title = @"Offline Pay";//订单标题
    payReq.totalFee = @"1"; //订单价格
    payReq.billNo = billno; //商户自定义订单号
    payReq.authcode = authcode; //支付授权码(ALI_SCAN,WX_SCAN时必需)，通过扫码用户的支付宝钱包(付款)、微信钱包(刷卡)获取
    payReq.terminalId = @"BeeCloud617"; //自定义扫码设备号
    payReq.storeId = @"BeeCloud618";//自定义店铺编号
    payReq.optional = dict;//用于商户业务扩展参数，会在webhook回调时返回
    [BeeCloud sendBCReq:payReq];
}

#pragma mark - PayPal Pay
/*
- (void)doPayPal {
    BCPayPalReq *payReq = [[BCPayPalReq alloc] init];
    
    _payPalConfig = [[PayPalConfiguration alloc] init];
    _payPalConfig.acceptCreditCards = YES;
    _payPalConfig.merchantName = @"Awesome Shirts, Inc.";
    _payPalConfig.merchantPrivacyPolicyURL = [NSURL URLWithString:@"https://www.paypal.com/webapps/mpp/ua/privacy-full"];
    _payPalConfig.merchantUserAgreementURL = [NSURL URLWithString:@"https://www.paypal.com/webapps/mpp/ua/useragreement-full"];
    
    _payPalConfig.languageOrLocale = [NSLocale preferredLanguages][0];
    
    _payPalConfig.payPalShippingAddressOption = PayPalShippingAddressOptionPayPal;
    
    PayPalItem *item1 = [PayPalItem itemWithName:@"Old jeans with holes"
                                    withQuantity:2
                                       withPrice:[NSDecimalNumber decimalNumberWithString:@"84.99"]
                                    withCurrency:@"USD"
                                         withSku:@"Hip-00037"];
    
    PayPalItem *item2 = [PayPalItem itemWithName:@"Free rainbow patch"
                                    withQuantity:1
                                       withPrice:[NSDecimalNumber decimalNumberWithString:@"0.00"]
                                    withCurrency:@"USD"
                                         withSku:@"Hip-00066"];
    
    PayPalItem *item3 = [PayPalItem itemWithName:@"Long-sleeve plaid shirt (mustache not included)"
                                    withQuantity:1
                                       withPrice:[NSDecimalNumber decimalNumberWithString:@"37.99"]
                                    withCurrency:@"USD"
                                         withSku:@"Hip-00291"];
    
    payReq.items = @[item1, item2, item3];
    payReq.shipping = @"5.00";
    payReq.tax = @"2.50";
    payReq.shortDesc = billTitle;
    payReq.viewController = self;
    payReq.payConfig = _payPalConfig;
    
    [BeeCloud sendBCReq:payReq];
    
}

#pragma mark - PayPal Verify
- (void)doPayPalVerify {
    BCPayPalVerifyReq *req = [[BCPayPalVerifyReq alloc] init];
    req.payment = _completedPayment;
    req.optional = @{@"key1":@"value1"};
    [BeeCloud sendBCReq:req];
}

#pragma mark - PayPalPaymentDelegate

- (void)payPalPaymentViewController:(PayPalPaymentViewController *)paymentViewController didCompletePayment:(PayPalPayment *)completedPayment {
    NSLog(@"PayPal Payment Success! %@", completedPayment.description);
    
    _completedPayment = completedPayment;
    
    [self doPayPalVerify];
    
    [self dismissViewControllerAnimated:YES completion:nil];
}

- (void)payPalPaymentDidCancel:(PayPalPaymentViewController *)paymentViewController {
    
    [self dismissViewControllerAnimated:YES completion:nil];
}
 */

#pragma mark - BCPay回调

- (void)onBeeCloudResp:(BCBaseResp *)resp {
    
    switch (resp.type) {
        case BCObjsTypePayResp:
        {
            // 支付请求响应
            BCPayResp *tempResp = (BCPayResp *)resp;
            if (tempResp.resultCode == 0) {
                BCPayReq *payReq = (BCPayReq *)resp.request;
                //百度钱包比较特殊需要用户用获取到的orderInfo，调用百度钱包SDK发起支付
                if (payReq.channel == PayChannelBaiduApp && ![BeeCloud getCurrentMode]) {
                    [[BDWalletSDKMainManager getInstance] doPayWithOrderInfo:tempResp.paySource[@"orderInfo"] params:nil delegate:self];
                } else {
                    //微信、支付宝、银联支付成功
                    [self showAlertView:resp.resultMsg];
                }
            } else {
                //支付取消或者支付失败
                [self showAlertView:[NSString stringWithFormat:@"%@ : %@",tempResp.resultMsg, tempResp.errDetail]];
            }
        }
            break;
        case BCObjsTypeQueryBillsResp:
        {
            BCQueryBillsResp *tempResp = (BCQueryBillsResp *)resp;
            if (resp.resultCode == 0) {
                if (tempResp.count == 0) {
                    [self showAlertView:@"未找到相关订单信息"];
                } else {
                    self.orderList = tempResp;
                    [self performSegueWithIdentifier:@"queryResult" sender:self];
                }
            } else {
                [self showAlertView:[NSString stringWithFormat:@"%@ : %@",tempResp.resultMsg, tempResp.errDetail]];
            }
        }
            break;
        case BCObjsTypeQueryRefundsResp:
        {
            BCQueryRefundsResp *tempResp = (BCQueryRefundsResp *)resp;
            if (resp.resultCode == 0) {
                if (tempResp.count == 0) {
                    [self showAlertView:@"未找到相关订单信息"];
                } else {
                    self.orderList = tempResp;
                    [self performSegueWithIdentifier:@"queryResult" sender:self];
                }
            } else {
                [self showAlertView:[NSString stringWithFormat:@"%@ : %@",tempResp.resultMsg, tempResp.errDetail]];
            }
        }
            break;
        
        case BCObjsTypeOfflinePayResp:
        {
            BCOfflinePayResp *tempResp = (BCOfflinePayResp *)resp;
            if (resp.resultCode == 0) {
                BCOfflinePayReq *payReq = (BCOfflinePayReq *)tempResp.request;
                switch (payReq.channel) {
                    case PayChannelAliOfflineQrcode:
                    case PayChannelWxNative:
                    case PayChannelBCNative:
                    case PayChannelBCAliQrcode:
                        if (tempResp.codeurl.isValid) {
                            QRCodeViewController *qrCodeView = [[QRCodeViewController alloc] init];
                            qrCodeView.resp = tempResp;
                            qrCodeView.delegate = self;
                            [self.navigationController pushViewController:qrCodeView animated:YES];
                        }
                        break;
                    case PayChannelAliScan:
                    case PayChannelWxScan:
                    case PayChannelBCWxScan:
                    case PayChannelBCAliScan:
                    {
                        BCOfflineStatusReq *req = [[BCOfflineStatusReq alloc] init];
                        req.channel = payReq.channel;
                        req.billNo = payReq.billNo;
                        [BeeCloud sendBCReq:req];
                    }
                        break;
                    default:
                        break;
                }
            } else {
                [self showAlertView:[NSString stringWithFormat:@"%@ : %@",tempResp.resultMsg, tempResp.errDetail]];
            }
        }
            break;
        case BCObjsTypeOfflineBillStatusResp:
        {
            static int queryTimes = 1;
            BCOfflineStatusResp *tempResp = (BCOfflineStatusResp *)resp;
            if (tempResp.resultCode == 0) {
                if (!tempResp.payResult && queryTimes < 3) {
                    queryTimes++;
                    [BeeCloud sendBCReq:tempResp.request];
                } else {
                    [self showAlertView:tempResp.payResult?@"支付成功":@"支付失败"];
                    //                BCOfflineRevertReq *req = [[BCOfflineRevertReq alloc] init];
                    //                req.channel = tempResp.request.channel;
                    //                req.billno = tempResp.request.billno;
                    //                [BeeCloud sendBCReq:req];
                    queryTimes = 1;
                }
                
            } else {
                [self showAlertView:[NSString stringWithFormat:@"%@ : %@",tempResp.resultMsg, tempResp.errDetail]];
            }
        }
            break;
        case BCObjsTypeOfflineRevertResp:
        {
#pragma mark - 线下撤销订单响应事件类型，包含WX_SCAN,ALI_SCAN,ALI_OFFLINE_QRCODE
            BCOfflineRevertResp *tempResp = (BCOfflineRevertResp *)resp;
            if (resp.resultCode == 0) {
                [self showAlertView:tempResp.revertStatus?@"撤销成功":@"撤销失败"];
            } else {
                [self showAlertView:[NSString stringWithFormat:@"%@ : %@",tempResp.resultMsg, tempResp.errDetail]];
            }
        }
            break;
        default:
        {
            if (resp.resultCode == 0) {
                [self showAlertView:resp.resultMsg];
            } else {
                [self showAlertView:[NSString stringWithFormat:@"%@ : %@",resp.resultMsg, resp.errDetail]];
            }
        }
            break;
    }
}

- (void)showAlertView:(NSString *)msg {
    UIAlertView* alert = [[UIAlertView alloc]initWithTitle:@"提示" message:msg delegate:nil cancelButtonTitle:@"确定" otherButtonTitles:nil, nil];
    [alert show];
}

#pragma mark - 订单查询

- (void)doQuery:(PayChannel)channel {
    
    if (self.actionType == 1) {
        BCQueryBillsReq *req = [[BCQueryBillsReq alloc] init];
        req.channel = channel;
        req.billStatus = BillStatusOnlySuccess;
        req.needMsgDetail = YES;
        //   req.billno = @"20150901104138656";//订单号
        //  req.startTime = @"2015-10-22 00:00";//订单时间
        // req.endTime = @"2015-10-23 00:00";//订单时间
        req.skip = 0;//
        req.limit = 10;
        [BeeCloud sendBCReq:req];
    } else if (self.actionType == 2) {
        BCQueryRefundsReq *req = [[BCQueryRefundsReq alloc] init];
        req.channel = channel;
        req.needApproved = NeedApprovalAll;
        //  req.billno = @"20150722164700237";
        //  req.starttime = @"2015-07-21 00:00";
        // req.endtime = @"2015-07-23 12:00";
        //req.refundno = @"20150709173629127";
        req.skip = 0;
        req.limit = 10;
        [BeeCloud sendBCReq:req];
    }
}

#pragma maek tableView Delegate

- (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView {
    return channelList.count;
}

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    NSArray *sub = channelList[section][@"subChannel"];
    return sub.count;
}

- (NSString *)tableView:(UITableView *)tableView titleForHeaderInSection:(NSInteger)section {
    return  channelList[section][@"channel"];
}

- (CGFloat)tableView:(UITableView *)tableView heightForHeaderInSection:(NSInteger)section {
    return 30;
}

- (CGFloat)tableView:(UITableView *)tableView heightForRowAtIndexPath:(NSIndexPath *)indexPath {
    return 60.0f;
}

- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    static NSString *simpleTableIdentifier = @"payChannelCell";
    
    PayChannelCell *cell = [tableView dequeueReusableCellWithIdentifier:simpleTableIdentifier];
    
    if (cell == nil) {
        cell = [[PayChannelCell alloc] initWithStyle:UITableViewCellStyleDefault reuseIdentifier:simpleTableIdentifier];
    }
    NSDictionary *row = channelList[indexPath.section][@"subChannel"][indexPath.row];
    cell.cImg.image = [UIImage imageNamed:row[@"img"]];
    cell.title.text = row[@"title"];
    
    return cell;
}

- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath {
    NSArray *row = channelList[indexPath.section][@"subChannel"];
    PayChannel channel = [row[indexPath.row][@"sub"] integerValue];
    if (self.actionType == 0) {
        switch (channel) {
            case PayChannelWxApp:
            case PayChannelAliApp:
            case PayChannelUnApp:
            case PayChannelBaiduApp:
            case PayChannelApplePay:
            case PayChannelApplePayTest:
            case PayChannelBCApp:
            case PayChannelBCWXApp:
            case PayChannelBCAliApp:
                [self doPay:channel];
                break;
            case PayChannelWxNative:
            case PayChannelAliOfflineQrcode:
            case PayChannelBCAliQrcode:
            case PayChannelBCNative:
                [self doOfflinePay:channel authCode:@""];
                break;
            case PayChannelWxScan:
            case PayChannelAliScan:
            case PayChannelBCAliScan:
            case PayChannelBCWxScan:
                currentChannel = channel;
#if TARGET_IPHONE_SIMULATOR
                [self showAlertView:@"模拟器不能打开相机"];
#elif TARGET_OS_IPHONE
                [self showScanViewController];
#endif
                break;
            case PayChannelPayPal:
            case PayChannelPayPalSandbox:
//                [self doPayPal];
                break;
            default:
                break;
        }
    } else {
        switch (channel) {
            case PayChannelWxScan:
                [self doQuery:PayChannelWx];
                break;
            case PayChannelAliScan:
            case PayChannelAliOfflineQrcode:
                [self doQuery:PayChannelAli];
                break;
            default:
                [self doQuery:channel];
                break;
        }
    }
    
    [tableView deselectRowAtIndexPath:indexPath animated:YES];
}

/**
 *  打开摄像头，扫描用户的二维码
 */
- (void)showScanViewController {
    ScanViewController *scanView = [[ScanViewController alloc] init];
    scanView.delegate = self;
    [self presentViewController:scanView animated:YES completion:nil];
}

/**
 *  获得支付授权码，发起支付
 *
 *  @param authCode 支付授权码
 */
- (void)scanWithAuthCode:(NSString *)authCode {
    [self doOfflinePay:currentChannel authCode:authCode];
}

/**
 *  用户付款后，查询订单状态
 *
 *  @param resp 支付结果
 */
- (void)qrCodeBeScaned:(BCOfflinePayResp *)resp {
    BCOfflineStatusReq *req = [[BCOfflineStatusReq alloc] init];
    BCOfflinePayReq *payReq = (BCOfflinePayReq *)resp.request;
    req.channel = payReq.channel;
    req.billNo = payReq.billNo;
    [BeeCloud sendBCReq:req];
}

#pragma mark - prepare segue
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    QueryResultViewController *viewController = (QueryResultViewController *)segue.destinationViewController;
    if([segue.identifier isEqualToString:@"queryResult"]) {
        viewController.resp = self.orderList;
    }
}

#pragma mark - 生成订单号
- (NSString *)genBillNo {
    NSDateFormatter *formatter = [[NSDateFormatter alloc] init];
    [formatter setDateFormat:@"yyyyMMddHHmmssSSS"];
    return [formatter stringFromDate:[NSDate date]];
}

- (void)setHideTableViewCell:(UITableView *)tableView {
    UIView *view = [[UIView alloc] init];
    view.backgroundColor = [UIColor clearColor];
    tableView.tableFooterView = view;
}

#pragma mark - Baidu Delegate
- (void)BDWalletPayResultWithCode:(int)statusCode payDesc:(NSString *)payDescs {
    NSString *status = @"";
    switch (statusCode) {
        case 0:
            status = @"支付成功";
            break;
        case 1:
            status = @"支付中";
            break;
        case 2:
            status = @"支付取消";
            break;
        default:
            break;
    }
    [self showAlertView:status];
}

- (void)logEventId:(NSString *)eventId eventDesc:(NSString *)eventDesc {
}

@end
