//
//  ViewController.m
//  BuglySDKDemo
//
//  Created by mqq on 14/11/25.
//  Copyright (c) 2014年 Tencent. All rights reserved.
//

#import "ViewController.h"

#import <Bugly/Bugly.h>

#import <sys/sysctl.h>

#import <string>
using namespace std;

#define std_string(_cs_) ((_cs_ == NULL) ? std::string() : std::string(_cs_))

@interface ViewController ()
@property (weak, nonatomic) IBOutlet UILabel *labelTip;
@property (weak, nonatomic) IBOutlet UIButton *buttonCrashObjC;
@property (weak, nonatomic) IBOutlet UIButton *buttonCrashCXX;
@property (weak, nonatomic) IBOutlet UIButton *buttonCrashCaught;
@property (weak, nonatomic) IBOutlet UIButton *buttonHardTask;
@property (weak, nonatomic) IBOutlet UILabel *labelWarning;
@property (weak, nonatomic) IBOutlet UIImageView *imageWarning;

@end

@implementation ViewController

- (NSAttributedString *)buildWaringString {
    NSMutableAttributedString *attributedString = [[NSMutableAttributedString alloc] initWithString:@"注意: Xcode处于调试模式\n请断开调试模式再次启动App后触发崩溃测试。"];
    return [[NSAttributedString alloc] initWithAttributedString:attributedString];
}

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view, typically from a nib.

    BLYLogWarn(@"%s , %s", __FILE__, __FUNCTION__);
    
//    bool debugMode = debugger_should_exit();
    bool debugMode = NO;
    if (debugMode) {
        [self.labelWarning setAttributedText:[self buildWaringString]];
        [self.labelWarning setNumberOfLines:4];
    }

    
    [self.imageWarning setImage:[UIImage imageNamed:@"ImageWarning"]];
    
    [self.labelWarning setHidden:!debugMode];
    [self.imageWarning setHidden:!debugMode];
    
    [self.buttonCrashObjC setEnabled:!debugMode];
    [self.buttonCrashCXX setEnabled:!debugMode];
    [self.buttonCrashCaught setEnabled:!debugMode];
    [self.buttonHardTask setEnabled:!debugMode];
    
    [self.buttonCrashObjC addTarget:self action:@selector(onNSExceptionButtonClick) forControlEvents:UIControlEventTouchUpInside];
    [self.buttonCrashCXX addTarget:self action:@selector(onSignalButtonClick) forControlEvents:UIControlEventTouchUpInside];
    [self.buttonCrashCaught addTarget:self action:@selector(onCaughtExceptionButtonClick) forControlEvents:UIControlEventTouchUpInside];
    [self.buttonHardTask addTarget:self action:@selector(onHardTaskButtonClick) forControlEvents:UIControlEventTouchUpInside];
}

- (void)onNSExceptionButtonClick {

    [self testNSException];
}

- (void)onSignalButtonClick {
    
    [self testSignalException];

}

- (void)onCaughtExceptionButtonClick {
    @try {
        NSArray * array = @[@"1", @"2"];
        
        NSLog(@"print %@", [array objectAtIndex:2]);
    }
    @catch (NSException *exception) {
        NSLog(@"catch a exception");
        // Report caught an NSException object
        [Bugly reportException:exception];
    }
    @finally {
        
    }
    
    dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
        sleep(1);
        // Report a customized exception object
        [Bugly reportException:[NSException exceptionWithName:@"自定义错误类型" reason:@"测试自定义错误上报" userInfo:nil]];
        
        sleep(2);
        
        NSError * error = nil;
        [[NSFileManager defaultManager] attributesOfItemAtPath:@"路径不存在" error:&error];
        // Report an NSError object
        [Bugly reportError:error];
    });
}

- (void)testNSException {
    NSLog(@"it will throw an NSException ");
    NSArray * array = @[];
    NSLog(@"the element is %@", array[1]);
}

- (void)testSignalException {
    NSLog(@"test signal exception");
    NSString * null = nil;
    NSLog(@"print the nil string %s", [null UTF8String]);
    NSLog(@"print the nil string to c string: %s", std::string([null UTF8String]).c_str());
}

- (void)onHardTaskButtonClick {
    // TEST code: make a STUCK scene data
    [self testHardTask];
}

- (void)testHardTask {
    NSLog(@"do hard test");
    
    [NSThread sleepForTimeInterval:3.0];
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}


static bool debugger_should_exit (void) {
#if !TARGET_OS_IPHONE
    return false;
#endif
    
    struct kinfo_proc info;
    size_t info_size = sizeof(info);
    int name[4];
    
    name[0] = CTL_KERN;
    name[1] = KERN_PROC;
    name[2] = KERN_PROC_PID;
    name[3] = getpid();
    
    if (sysctl(name, 4, &info, &info_size, NULL, 0) == -1) {
        NSLog(@"sysctl() failed: %s", strerror(errno));
        return false;
    }
    
    if ((info.kp_proc.p_flag & P_TRACED) != 0)
        return true;
    
    return false;
}

@end
