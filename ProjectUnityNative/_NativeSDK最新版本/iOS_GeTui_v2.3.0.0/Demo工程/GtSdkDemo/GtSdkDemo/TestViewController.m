//
//  TestFunctionViewController.m
//  Demo
//
//  Created by 赵伟 on 15/8/10.
//
//

#import "AppDelegate.h"
#import "TestViewController.h"

@interface TestViewController ()

@property (strong, nonatomic) IBOutlet UIScrollView *baseScrollView;

@property (strong, nonatomic) IBOutlet UITextField *showMsgTextField;
@property (strong, nonatomic) IBOutlet UITextField *deviceTokenTextField;
@property (strong, nonatomic) IBOutlet UITextField *tagsTextField;
@property (strong, nonatomic) IBOutlet UITextField *aliasTextField;
@property (strong, nonatomic) IBOutlet UITextField *messageTextField;
@property (strong, nonatomic) IBOutlet UITextField *badgeTextField;


@end

@implementation TestViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view from its nib.
}

- (void)viewWillAppear:(BOOL)animated {
    [super viewWillAppear:animated];

    [self.baseScrollView setContentSize:CGSizeMake(self.view.bounds.size.width, 1100)];
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

/*
#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    // Get the new view controller using [segue destinationViewController].
    // Pass the selected object to the new view controller.
}
*/

- (void)dealloc {
    NSLog(@"%@ %@", NSStringFromClass(self.class), @"dealloc");
}

- (IBAction)clickActionButton:(UIButton *)sender {
    switch (sender.tag) {
        case 18: { // 版本
            _showMsgTextField.text = [GeTuiSdk version];
            break;
        }
        case 19: { // clientId
            _showMsgTextField.text = [GeTuiSdk clientId];
            break;
        }
        case 20: { // status
            if ([GeTuiSdk status] == SdkStatusStarted) {
                _showMsgTextField.text = @"启动";
            } else if ([GeTuiSdk status] == SdkStatusStarting) {
                _showMsgTextField.text = @"正在启动";
            } else if ([GeTuiSdk status] == SdkStatusStoped) {
                _showMsgTextField.text = @"停止";
            }
            break;
        }
        case 21: { // 注册DeviceToken
            [GeTuiSdk registerDeviceToken:_deviceTokenTextField.text];

            [self showTos];

            break;
        }
        case 22: { // 设置标签
            NSString *tagName = _tagsTextField.text;
            NSArray *tagNames = [tagName componentsSeparatedByString:@","];
            if (![GeTuiSdk setTags:tagNames]) {
                UIAlertView *alertView = [[UIAlertView alloc] initWithTitle:@"Failed" message:@"设置失败" delegate:nil cancelButtonTitle:@"OK" otherButtonTitles:nil];
                [alertView show];
            } else {
                [self showTos];
            }

            break;
        }
        case 23: { // 绑定别名
            [GeTuiSdk bindAlias:_aliasTextField.text andSequenceNum:[NSString stringWithFormat:@"%@-sign", _aliasTextField.text]];

            [self showTos];
            break;
        }
        case 24: { // 取消绑定别名
            [GeTuiSdk unbindAlias:_aliasTextField.text andSequenceNum:[NSString stringWithFormat:@"%@-sign", _aliasTextField.text] andIsSelf:YES];

            [self showTos];
            break;
        }
        case 25: { // 发送消息
            NSString *content = _messageTextField.text;
            NSError *err = nil;
            [GeTuiSdk sendMessage:[content dataUsingEncoding:NSUTF8StringEncoding] error:&err];
            if (err) {
                UIAlertView *alertView = [[UIAlertView alloc] initWithTitle:@"Failed" message:[NSString stringWithFormat:@"send failed:%@", [err localizedDescription]] delegate:nil cancelButtonTitle:@"OK" otherButtonTitles:nil];
                [alertView show];
            } else {
                [self showTos];
            }

            break;
        }
        case 26: { // 清空通知

            [GeTuiSdk clearAllNotificationForNotificationBar];
            _showMsgTextField.text = @"清空成功";

            break;
        }
        case 29: {

            NSString *badgeText = [_badgeTextField text];
            int badgeValue = [badgeText intValue];
            badgeValue = badgeValue >= 0 ? badgeValue : 0;

            [GeTuiSdk setBadge:badgeValue];
            [[UIApplication sharedApplication] setApplicationIconBadgeNumber:badgeValue];

            [self showTos];
            break;
        }
        default:
            break;
    }
}

// 关闭键盘
- (IBAction)didEndOnExit:(UITextField *)sender {
    [sender resignFirstResponder];
}

- (void)showTos {
    UIAlertView *alertView = [[UIAlertView alloc] initWithTitle:@"提示" message:@"点击成功" delegate:nil cancelButtonTitle:@"OK" otherButtonTitles:nil];
    [alertView show];
}

@end
