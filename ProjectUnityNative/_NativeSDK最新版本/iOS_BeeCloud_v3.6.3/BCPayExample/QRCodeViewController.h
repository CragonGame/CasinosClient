//
//  QRCodeViewController.h
//  BCPay
//
//  Created by Ewenlong03 on 15/9/16.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "BCOfflinePayResp.h"

@protocol QRCodeDelegate <NSObject>

- (void)qrCodeBeScaned:(BCOfflinePayResp *)resp;

@end

@interface QRCodeViewController : UIViewController

@property (nonatomic, strong) BCOfflinePayResp *resp;
@property (nonatomic, weak) id<QRCodeDelegate> delegate;
@end
