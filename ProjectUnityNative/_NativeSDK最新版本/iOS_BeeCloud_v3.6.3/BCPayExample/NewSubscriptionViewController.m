//
//  NewSubscriptionViewController.m
//  BCPay
//
//  Created by Ewenlong03 on 16/8/12.
//  Copyright © 2016年 BeeCloud. All rights reserved.
//

#import "NewSubscriptionViewController.h"
#import "BCNewSubscription.h"

@interface NewSubscriptionViewController ()<BCSubscriptionDelegate>
    @property (weak, nonatomic) IBOutlet UITextView *commentTxt;

@end

@implementation NewSubscriptionViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view.
    self.edgesForExtendedLayout = UIRectEdgeNone;
    self.extendedLayoutIncludesOpaqueBars = NO;
    self.commentTxt.layer.borderColor = [UIColor grayColor].CGColor;
    self.commentTxt.layer.borderWidth = 1;
    self.commentTxt.layer.cornerRadius = 10;
    self.commentTxt.text = @"订阅支付流程:\n \
    第一步、选定订阅计划;在BeeCloud官网控制台创建订阅计划。\n \
    第二步、采集用户银行卡信息或使用之前此用户绑定的卡ID创建订阅计划；创建/取消订阅计划需要使用到四个接口：\n \
      1、获取端验证码\n \
          [BCSubscription smsReq:@\"phone\"];\n\r \
      2、获取银行列表\n \
          [BCSubscription subscriptionBanks]\n\r \
      3、创建订阅\n \
          BCSubNewSubscription *newSub = [[BCSubNewSubscription alloc] init];\n \
          newSub.buyer_id = @\"buyer_id\";\n \
          newSub.plan_id = @\"plan_id\"; \n \
          newSub.bank_name = @\"bank_name\"; \n \
          newSub.card_no = @\"card_no\"; \n \
          newSub.id_no = @\"id_no\"; \n \
          newSub.id_name = @\"id_name\";\n \
          newSub.sms_id = @\"sms_id\"; \n \
          newSub.sms_code = @\"sms_code\"; \n \
          [newSub newSubscription]; \n \
       4、取消订阅\n \
          [BCSubscription subscriptionCancel:subscription_id];\n \
            \n \
    具体需要的参数请参考每个接口的定义";
    
    [BCSubscription setSubDelegate:self];
//    [BCSubscription smsReq:@"18654155015"];

}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void)onBCSubscriptionResp:(NSMutableDictionary *)resp {
    NSLog(@"%@", resp);
    NSInteger type = [resp[@"type"] integerValue];
    if ([resp[@"resultCode"] integerValue] == 0) {
        switch(type) {
            case BCSubTypeSMS:
            {
                BCNewSubscription *newSub = [[BCNewSubscription alloc] init];
                newSub.buyer_id = @"hwl";
                newSub.plan_id = @"399df68d-04c7-4d19-9459-11098ed32add";
                newSub.bank_name = @"中国银行";
                newSub.card_no = @"6217856101016319795";
                newSub.id_no = @"32072419881028005X";
                newSub.id_name = @"黄文龙";
                newSub.mobile = @"18654155015";
                newSub.sms_id = resp[@"sms_id"];
                newSub.sms_code = @"sms_code";
                
                [newSub newSubscription];
            }
                break;
            case BCSubTypeNewSubscription:
                NSLog(@"%@", resp);
                break;
            case 11:
                break;
            default:
                NSLog(@"%@", resp);
                break;
        }
    } else {
        NSLog(@"%@", resp);
    }
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
