//
//  BCSandboxAdapter.m
//  BCPay
//
//  Created by Ewenlong03 on 15/12/2.
//  Copyright © 2015年 BeeCloud. All rights reserved.
//

#import "BCSandboxAdapter.h"
#import "BeeCloudAdapterProtocol.h"
#import "BCPayUtil.h"
#import "BCPayCache.h"
#import "PaySandboxViewController.h"

@interface BCSandboxAdapter () <BeeCloudAdapterDelegate>

@end

@implementation BCSandboxAdapter

- (BOOL)sandboxPay {
    
    BCPayReq *req = (BCPayReq *)[BCPayCache sharedInstance].bcResp.request;
    
    if (req.viewController) {
        PaySandboxViewController *view = [[PaySandboxViewController alloc] init];
        [req.viewController presentViewController:view animated:YES completion:^{
        }];
        return YES;
    }
    return NO;
}

@end
