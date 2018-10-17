//
//  QRCodeViewController.m
//  BCPay
//
//  Created by Ewenlong03 on 15/9/16.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "QRCodeViewController.h"
#import "NSString+IsValid.h"
#import "GenQrCode.h"

@interface QRCodeViewController () {
    UIButton *btn;
    UIImageView *codeView;
}

@end

@implementation QRCodeViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view.
    self.view.backgroundColor = [UIColor whiteColor];
    UITapGestureRecognizer *tap = [[UITapGestureRecognizer alloc] initWithTarget:self action:@selector(dismissMyself)];
    [self.view addGestureRecognizer:tap];
    
    if (self.resp.codeurl.isValid) {
        UIImage *qrCode = [GenQrCode createQRForString:_resp.codeurl withSize:250.0f];
       
        codeView = [[UIImageView alloc] initWithFrame:CGRectMake(20, 80, self.view.bounds.size.width-40, self.view.bounds.size.width-40) ];
        codeView.image = qrCode;
        [self.view addSubview:codeView];
        
        btn = [[UIButton alloc] initWithFrame:CGRectMake(20, 80, self.view.bounds.size.width-40, self.view.bounds.size.width-40)];
        [btn setTitle:@"用户已支付" forState:UIControlStateNormal];
        btn.backgroundColor = [UIColor redColor];
        [btn setTitleColor:[UIColor whiteColor] forState:UIControlStateNormal];
        [btn addTarget:self action:@selector(haveBeScaned) forControlEvents:UIControlEventTouchUpInside];
        [self.view addSubview:btn];
    }
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void)viewWillAppear:(BOOL)animated {
    btn.frame = CGRectMake(20, codeView.frame.origin.y+codeView.frame.size.height+20, self.view.bounds.size.width-40, 40);
}

- (void)dismissMyself {
//    [self dismissViewControllerAnimated:YES completion:nil];
    [self.navigationController popViewControllerAnimated:YES];
}

- (void)haveBeScaned {
    if (_delegate && [_delegate respondsToSelector:@selector(qrCodeBeScaned:)]) {
        [_delegate qrCodeBeScaned:_resp];
    }
    [self dismissMyself];
}

/*
#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    // Get the new view controller using [segue destinationViewController].
    // Pass the selected object to the new view controller.
}
*/

@end
