//
//  ViewController.h
//  BeeCloudDemo
//
//  Created by RInz on 15/2/5.
//  Copyright (c) 2015年 RInz. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "BeeCloud.h"

@interface ViewController : UITableViewController<UIAlertViewDelegate, BeeCloudDelegate>

@property (strong, nonatomic) BCBaseResp *orderList;

@property  (assign, nonatomic) NSInteger actionType;//0:pay;1:query;

@end

