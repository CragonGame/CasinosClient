//
//  SubscriptionViewController.m
//  BCPay
//
//  Created by Ewenlong03 on 16/8/5.
//  Copyright © 2016年 BeeCloud. All rights reserved.
//

#import "SubscriptionViewController.h"
#import "BCSubscription.h"
#import "BCSubscriptionQuery.h"
#import "BCNewSubscription.h"

@interface SubscriptionViewController ()<BCSubscriptionDelegate, UITableViewDelegate, UITableViewDataSource>
@property (strong, nonatomic) IBOutlet UITableView *planTable;
@property (nonatomic, retain) BCSubscription *sub;
@property (nonatomic, retain) NSArray *plans;
@end

@implementation SubscriptionViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    
    _planTable.delegate = self;
    _planTable.dataSource = self;
    
    self.title = @"订阅支付";

    [BCSubscription setSubDelegate:self];
    
    BCSubscriptionQuery *query = [[BCSubscriptionQuery alloc] init];
//    [query getPlansCount:@"" interval:@"" interval_count:1 trial_days:0];
    [query getPlans:@"" interval:@"" interval_count:1 trial_days:0];
//    [query getSubscriptions:@"" plan_id:@"" card_id:@""];
    
//    [BCSubscription smsReq:@"18654155015"];
//    [BCSubscription subscriptionBanks];
    
    // Do any additional setup after loading the view.
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
            case 11:
                _plans = resp[@"plans"];
                [_planTable reloadData];
                break;
            default:
                NSLog(@"%@", resp);
                break;
        }
    } else {
        NSLog(@"%@", resp);
    }
}

- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    
}

/*
#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    // Get the new view controller using [segue destinationViewController].
    // Pass the selected object to the new view controller.
}
*/

- (CGFloat)tableView:(UITableView *)tableView heightForRowAtIndexPath:(NSIndexPath *)indexPath {
    return 80;
}

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    return _plans.count;
}

- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    static NSString *plan_cell = @"planCell";
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:plan_cell];
    if (cell == nil) {
        cell = [[UITableViewCell alloc] initWithStyle:UITableViewCellStyleDefault reuseIdentifier:plan_cell];
    }
    cell.accessoryType = UITableViewCellAccessoryDisclosureIndicator;
    NSDictionary *plan_detail = (NSDictionary *)[_plans objectAtIndex:indexPath.row];
    NSLog(@"%@", [plan_detail objectForKey:@"fee"]);
    cell.textLabel.numberOfLines = 0;
    cell.textLabel.text = [NSString stringWithFormat:@"%@\n价格:%@",plan_detail[@"name"],plan_detail[@"fee"]];
    
    return cell;
}

- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath {
    [self performSegueWithIdentifier:@"newSub" sender:self];
}

@end
