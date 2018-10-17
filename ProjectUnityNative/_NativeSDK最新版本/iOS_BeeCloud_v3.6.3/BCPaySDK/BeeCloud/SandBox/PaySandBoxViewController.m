//
//  PaySandboxViewController.m
//  BCPay
//
//  Created by Ewenlong03 on 15/11/30.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "PaySandboxViewController.h"
#import "BeeCloud.h"

@interface PaySandboxViewController () {
    UIStatusBarStyle statusStyle;
    
    NSInteger resultCode;
    NSString *resultMsg;
    
    UIActivityIndicatorView *loading;
}

@end

@implementation PaySandboxViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    
    resultCode = BCErrCodeCommon;
    resultMsg = @"支付失败";
    
    BCPayReq *req = (BCPayReq *)[BCPayCache sharedInstance].bcResp.request;
    
    self.view.backgroundColor = [UIColor colorWithRed:245.0/255.0 green:245.0/255.0 blue:245.0/255.0 alpha:1];
    if ([UIApplication sharedApplication].statusBarStyle != UIStatusBarStyleLightContent) {
        statusStyle = [UIApplication sharedApplication].statusBarStyle;
        [UIApplication sharedApplication].statusBarStyle = UIStatusBarStyleLightContent;
    }
    
    UIView *titleView = [[UIView alloc] initWithFrame:CGRectMake(0, 0, self.view.frame.size.width, 64)];
    titleView.backgroundColor = [UIColor colorWithRed:255.0/255.0 green:108.0/255.0 blue:44.0/255.0 alpha:1];
    [self.view addSubview:titleView];
    
    UILabel *titleLab = [[UILabel alloc] initWithFrame:CGRectMake(0, 24, self.view.frame.size.width, 18)];
    titleLab.textAlignment = NSTextAlignmentCenter;
    titleLab.textColor = [UIColor whiteColor];
    titleLab.font = [UIFont systemFontOfSize:14];
    titleLab.text = @"确认支付";
    
    UILabel *subTitleLab  = [[UILabel alloc] initWithFrame:CGRectMake(0, 42, self.view.frame.size.width, 18)];
    subTitleLab.textAlignment = NSTextAlignmentCenter;
    subTitleLab.textColor = [UIColor whiteColor];
    subTitleLab.font = [UIFont systemFontOfSize:12];
    subTitleLab.text = @"BeeCloud沙箱支付";
    
    [titleView addSubview:titleLab];
    [titleView addSubview:subTitleLab];
    
    UIButton *leftBtn = [[UIButton alloc] initWithFrame:CGRectMake(10, 22, 60, 40)];
    leftBtn.backgroundColor = [UIColor clearColor];
    [leftBtn setTitle:@"取消" forState:UIControlStateNormal];
    leftBtn.titleLabel.font = [UIFont systemFontOfSize:16];
    leftBtn.contentHorizontalAlignment = UIControlContentHorizontalAlignmentLeft;
    [leftBtn addTarget:self action:@selector(back) forControlEvents:UIControlEventTouchUpInside];
    [titleView addSubview:leftBtn];
    
    UIButton *rightBtn = [[UIButton alloc] initWithFrame:CGRectMake(self.view.frame.size.width-40, 27, 30, 30)];
    rightBtn.backgroundColor = [UIColor clearColor];
    [rightBtn setImage:[self parseChannel:req.channel] forState:UIControlStateNormal];
    rightBtn.titleLabel.font = [UIFont systemFontOfSize:16];
    rightBtn.contentHorizontalAlignment = UIControlContentHorizontalAlignmentRight;
    [titleView addSubview:rightBtn];
    
    UILabel *billTitle = [[UILabel alloc] initWithFrame:CGRectMake(0, 80, self.view.frame.size.width, 30)];
    billTitle.text = req.title;
    billTitle.font = [UIFont systemFontOfSize:22];
    billTitle.textAlignment = NSTextAlignmentCenter;
    [self.view addSubview:billTitle];
    
    UILabel *totalFee = [[UILabel alloc] initWithFrame:CGRectMake(0, 120, self.view.frame.size.width, 40)];
    totalFee.textAlignment = NSTextAlignmentCenter;
    totalFee.font = [UIFont systemFontOfSize:50];
    totalFee.text = [NSString stringWithFormat:@"￥%.2f", [req.totalFee intValue] * 0.01];
    [self.view addSubview:totalFee];
    
    UILabel *billno = [[UILabel alloc] initWithFrame:CGRectMake(0, 170, self.view.frame.size.width, 30)];
    billno.text = req.billNo;
    billno.textAlignment = NSTextAlignmentCenter;
    billno.font = [UIFont systemFontOfSize:16];
    [self.view addSubview:billno];
    
    UIView *bgView = [[UIView alloc] initWithFrame:CGRectMake(-2, 210, self.view.frame.size.width+4, 60)];
    bgView.backgroundColor = [UIColor whiteColor];
    bgView.layer.borderWidth = 2;
    bgView.layer.borderColor = [UIColor colorWithRed:0.88 green:0.88 blue:0.88 alpha:1].CGColor;
    
    UILabel *merchant = [[UILabel alloc] initWithFrame:CGRectMake(20, 10, self.view.frame.size.width/2 - 20, 40)];
    merchant.text = @"收款方";
    merchant.textColor = [UIColor colorWithRed:132.0/255 green:132.0/255 blue:132.0/255 alpha:1];
    merchant.font = [UIFont systemFontOfSize:18];
    [bgView addSubview:merchant];
    
    UILabel *merName = [[UILabel alloc] initWithFrame:CGRectMake(self.view.frame.size.width/2, 10, self.view.frame.size.width/2 - 20, 40)];
    merName.text = @"BeeCloud沙箱";
    merName.textColor = [UIColor blackColor];
    merName.font = [UIFont systemFontOfSize:18];
    merName.textAlignment = NSTextAlignmentRight;
    [bgView addSubview:merName];
    
    [self.view addSubview:bgView];
    
    UIButton *payBtn = [[UIButton alloc] initWithFrame:CGRectMake(20, 320, self.view.frame.size.width-40,   50)];
    payBtn.backgroundColor = [UIColor colorWithRed:1.0 green:108.0/255.0 blue:44.0/255.0 alpha:1];
    payBtn.layer.cornerRadius = 5;
    [payBtn setTitle:@"立即支付" forState:UIControlStateNormal];
    [payBtn addTarget:self action:@selector(pay) forControlEvents:UIControlEventTouchUpInside];
    [self.view addSubview:payBtn];
    
    UILabel *comment = [[UILabel alloc] initWithFrame:CGRectMake(0, self.view.frame.size.height-40, self.view.frame.size.width, 30)];
    comment.text = @"BeeCloud沙箱支付不产生真实交易";
    comment.textAlignment = NSTextAlignmentCenter;
    comment.font = [UIFont systemFontOfSize:15];
    comment.textColor = [UIColor colorWithRed:1.0 green:108.0/255.0 blue:44.0/255.0 alpha:1];//[UIColor colorWithRed:0.75 green:0.75 blue:0.75 alpha:1];
    [self.view addSubview:comment];
    
    loading = [[UIActivityIndicatorView alloc] initWithFrame:CGRectMake(0, 0, 80, 80)];
    loading.backgroundColor = [UIColor colorWithRed:214.0/255 green:214.0/255 blue:214.0/255 alpha:0.6];
    loading.layer.cornerRadius = 5;
    loading.center = self.view.center;
    [loading setActivityIndicatorViewStyle:UIActivityIndicatorViewStyleWhiteLarge];
    [self.view addSubview:loading];
}

- (UIImage *)parseChannel:(PayChannel)channel {
    
    if (channel == PayChannelWxApp) {
        return [self imagesNamedFromCustomBundle:@"wx"];
    }
    if (channel == PayChannelAliApp) {
        return [self imagesNamedFromCustomBundle:@"ali"];
    }
    if (channel == PayChannelUnApp || channel == PayChannelBCApp) {
        return [self imagesNamedFromCustomBundle:@"un"];
    }
    if (channel == PayChannelBaiduApp) {
        return [self imagesNamedFromCustomBundle:@"bd"];
    }
    return nil;
}

- (UIImage *)imagesNamedFromCustomBundle:(NSString *)imgName {
    NSString *bundlePath = [[NSBundle mainBundle].resourcePath stringByAppendingPathComponent:@"BeeCloud.bundle"];
    NSBundle *bundle = [NSBundle bundleWithPath:bundlePath];
    NSString *img_path = [bundle pathForResource:imgName ofType:@"png"];
    return [UIImage imageWithContentsOfFile:img_path];
}

- (void)viewWillDisappear:(BOOL)animated {
    [UIApplication sharedApplication].statusBarStyle = statusStyle;
    BCPayResp *resp = (BCPayResp *)[BCPayCache sharedInstance].bcResp;
    resp.resultCode = resultCode;
    resp.resultMsg = resultMsg;
    resp.errDetail = resultMsg;
    [BCPayCache beeCloudDoResponse];
}

- (void)back {
    resultCode = BCErrCodeUserCancel;
    resultMsg = @"支付取消";
    [loading stopAnimating];
    [self dismissViewControllerAnimated:YES completion:^{
        
    }];
}

- (void)pay {
    
    [loading startAnimating];
    
    NSString *host = [NSString stringWithFormat:@"%@%@/%@", [BCPayUtil getBestHostWithFormat:kRestApiSandboxNotify], [BCPayCache sharedInstance].appId, [BCPayCache sharedInstance].bcResp.bcId];
    NSLog(@"sandboxPay id = %@", [BCPayCache sharedInstance].bcResp.bcId);
    BCHTTPSessionManager *manager = [BCPayUtil getBCHTTPSessionManager];
    __weak PaySandboxViewController *weakSelf = self;
    [manager GET:host parameters:nil progress:nil
         success:^(NSURLSessionTask *task, id response) {
             BCPayLog(@"resp = %@", response);
             [weakSelf doNotifyResponse:(NSDictionary *)response];
         } failure:^(NSURLSessionTask *operation, NSError *error) {
             [loading stopAnimating];
             [BCPayUtil doErrorResponse:kNetWorkError];
         }];
}

- (void)doNotifyResponse:(NSDictionary *)response {
    [loading stopAnimating];
    if ([response integerValueForKey:kKeyResponseResultCode defaultValue:BCErrCodeCommon] == BCErrCodeSuccess) {
        resultMsg = @"支付成功";
        resultCode = BCErrCodeSuccess;
    }
    [self dismissViewControllerAnimated:YES completion:^{
        
    }];
}

@end
