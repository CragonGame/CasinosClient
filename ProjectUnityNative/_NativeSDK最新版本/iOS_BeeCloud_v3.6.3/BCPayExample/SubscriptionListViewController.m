//
//  SubscriptionListViewController.m
//  BCPay
//
//  Created by Ewenlong03 on 16/8/15.
//  Copyright © 2016年 BeeCloud. All rights reserved.
//

#import "SubscriptionListViewController.h"
#import "BCSubscriptionQuery.h"

@interface SubscriptionListViewController ()<UITableViewDelegate,UITableViewDataSource,BCSubscriptionDelegate,UIAlertViewDelegate>
@property (strong, nonatomic) IBOutlet UITableView *subTable;
@property (strong, nonatomic) NSArray *subList;
@property (strong, nonatomic) NSString *cancel_id;
@end

@implementation SubscriptionListViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view.
    _subTable.delegate = self;
    _subTable.dataSource = self;
    
    [BCSubscription setSubDelegate:self];
    BCSubscriptionQuery *query = [[BCSubscriptionQuery alloc] init];
    [query getSubscriptions:@"hwl" plan_id:@"" card_id:@""];
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
            case BCSubTypeSubscriptions:
                _subList = resp[@"subscriptions"];
                [_subTable reloadData];
                break;
            case BCSubTypeSubscriptionCancel:
            {
                UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"取消订阅" message:@"取消订阅成功" delegate:nil cancelButtonTitle:@"确定" otherButtonTitles:nil];
                [alert show];
            }
                break;
            default:
                NSLog(@"%@", resp);
                break;
        }
    } else {
        NSLog(@"%@", resp);
    }
}

- (CGFloat)tableView:(UITableView *)tableView heightForRowAtIndexPath:(NSIndexPath *)indexPath {
    return 80;
}

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    return _subList.count;
}

- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    static NSString *plan_cell = @"planCell";
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:plan_cell];
    if (cell == nil) {
        cell = [[UITableViewCell alloc] initWithStyle:UITableViewCellStyleDefault reuseIdentifier:plan_cell];
    }
    cell.accessoryType = UITableViewCellAccessoryDisclosureIndicator;
    NSDictionary *sub_detail = (NSDictionary *)[_subList objectAtIndex:indexPath.row];
    
    cell.textLabel.numberOfLines = 0;
    cell.textLabel.text = [NSString stringWithFormat:@"%@\n%@",sub_detail[@"buyer_id"],[sub_detail[@"valid"] integerValue]==0?@"订阅失败":@"订阅成功"];
    
    return cell;
}

- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath {
    NSDictionary *sub_detail = (NSDictionary *)[_subList objectAtIndex:indexPath.row];
    if ([sub_detail[@"valid"] integerValue] == 1) {
        _cancel_id = sub_detail[@"id"];
        UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"取消订阅" message:@"确认取消此订阅吗？" delegate:self cancelButtonTitle:@"关闭" otherButtonTitles:@"取消", nil];
        [alert show];
    } else {
        UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"取消订阅" message:@"不支持此操作" delegate:nil cancelButtonTitle:@"关闭" otherButtonTitles:nil];
        [alert show];
    }
}

- (void)alertView:(UIAlertView *)alertView clickedButtonAtIndex:(NSInteger)buttonIndex {
    if (buttonIndex == 0) {
        
    } else {
        [BCSubscription subscriptionCancel:_cancel_id];
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
