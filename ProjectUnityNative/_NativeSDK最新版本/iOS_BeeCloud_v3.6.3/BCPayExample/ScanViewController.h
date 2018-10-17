//
//  ScanViewController.h
//  NewProject
//
//  Created by Ewenlong on 15-09-29.
//  Copyright (c) 2013年 Steven. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <AVFoundation/AVFoundation.h>

@protocol SCanViewDelegate <NSObject>

- (void)scanWithAuthCode:(NSString *)authCode ;

@end

@interface ScanViewController : UIViewController<AVCaptureMetadataOutputObjectsDelegate> {
    int num;
    BOOL upOrdown;
    NSTimer * timer;
}

@property (strong,nonatomic)AVCaptureDevice * device;
@property (strong,nonatomic)AVCaptureDeviceInput * input;
@property (strong,nonatomic)AVCaptureMetadataOutput * output;
@property (strong,nonatomic)AVCaptureSession * session;
@property (strong,nonatomic)AVCaptureVideoPreviewLayer * preview;
@property (nonatomic, retain) UIImageView * line;
@property (nonatomic, weak) id<SCanViewDelegate> delegate;
@end
