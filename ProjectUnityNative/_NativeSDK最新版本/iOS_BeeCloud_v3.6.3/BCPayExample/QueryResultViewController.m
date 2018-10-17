//
//  QueryResultViewController.m
//  BCPaySDK
//
//  Created by Ewenlong03 on 15/7/27.
//  Copyright (c) 2015年 BeeCloud. All rights reserved.
//

#import "QueryResultViewController.h"
#import "BeeCloud.h"
#import "BCPayUtil.h"

@interface QueryResultViewController ()<BeeCloudDelegate>
@property (nonatomic, strong) NSMutableArray *dataList;

@end

@implementation QueryResultViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view.
    self.resultTableView.delegate = self;
    self.resultTableView.dataSource = self;
    [BeeCloud setBeeCloudDelegate:self];
    
    if (self.resp.type == BCObjsTypeQueryBillsResp) {
        self.dataList = ((BCQueryBillsResp *)self.resp).results;
        BCQueryBillsReq *request = (BCQueryBillsReq *)self.resp.request;
        BCQueryBillsCountReq *req = [[BCQueryBillsCountReq alloc] init];
        req.channel = request.channel;
        req.billNo = request.billNo;
        req.billStatus = request.billStatus;
        req.startTime = request.startTime;
        req.endTime = request.endTime;
        [BeeCloud sendBCReq:req];
    } else if (self.resp.type == BCObjsTypeQueryRefundsResp) {
        self.dataList= ((BCQueryRefundsResp *)self.resp).results;
        BCQueryRefundsReq *request = (BCQueryRefundsReq *)self.resp.request;
        BCQueryRefundsCountReq *req = [[BCQueryRefundsCountReq alloc] init];
        req.channel = request.channel;
        req.billNo = request.billNo;
        req.needApproved = request.needApproved;
        req.refundNo = request.billNo;
        req.startTime = request.startTime;
        req.endTime = request.endTime;
        [BeeCloud sendBCReq:req];
    }
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    return self.dataList.count;
}

- (CGFloat)tableView:(UITableView *)tableView heightForRowAtIndexPath:(NSIndexPath *)indexPath {
    return 200.0f;
}

- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    static NSString *cellIdentifier = @"orderCell";
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:cellIdentifier];
    if (cell == nil) {
        cell = [[UITableViewCell alloc] initWithStyle:UITableViewCellStyleDefault reuseIdentifier:cellIdentifier];
    }
    NSString *cellString = @"";
    if (self.resp.type == BCObjsTypeQueryBillsResp) {
        cell  = [tableView dequeueReusableCellWithIdentifier:cellIdentifier];
        if (cell == nil) {
            cell = [[UITableViewCell alloc] initWithStyle:UITableViewCellStyleSubtitle reuseIdentifier:cellIdentifier];
        }
        BCQueryBillResult *result = (BCQueryBillResult *)[self.dataList objectAtIndex:indexPath.row];
        
        cellString = [NSString stringWithFormat:@"订单标题:%@\n渠道:%@\n金额:%ld\n交易时间:%@\n交易订单号:%@\n交易状态:%@\n是否撤销:%@", result.title, result.subChannel, (long)result.totalFee,[BCPayUtil millisecondToDateString:result.createTime],result.billNo,result.payResult?@"成功":@"失败",result.revertResult?@"是":@"否"];
        
    } else if (self.resp.type == BCObjsTypeQueryRefundsResp) {
       
        BCQueryRefundResult *result = (BCQueryRefundResult *)[self.dataList objectAtIndex:indexPath.row];
        
        cellString = [NSString stringWithFormat:@"订单标题:%@\n渠道:%@\n总金额:%ld 退款金额:%ld\n交易时间:%@\n交易订单号:%@\n退款单号:%@\n退款是否成功状态:%@\n退款是否完成:%@", result.title,result.subChannel, (long)result.totalFee, (long)result.refundFee,[BCPayUtil millisecondToDateString:result.createTime],result.billNo,result.refundNo, result.result?@"成功":@"失败",result.finish?@"完成":@"未完成"];
    }
    cell.textLabel.numberOfLines = 0;
    cell.textLabel.text = cellString;
    
    return cell;
}

- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath {
    BCBaseResult *result = [self.dataList objectAtIndex:indexPath.row];
    if (result.type == BCObjsTypeBillResults) {
       
        BCQueryBillResult *bill = (BCQueryBillResult *)result;
        [BeeCloud sendBCReq:[[BCQueryBillByIdReq alloc] initWithObjectId:bill.objectId]];
        
//        BCPreRefundReq *req = [[BCPreRefundReq alloc] init];
//        req.billNo = bill.billNo;
//        req.refundNo = @"201606241290120";
//        req.refundFee = [NSString stringWithFormat:@"%@", @(bill.totalFee)];
//        [BeeCloud sendBCReq:req];
        
    } else if (result.type == BCObjsTypeRefundResults) {
        
        BCQueryRefundResult *refund = (BCQueryRefundResult *)result;
        [BeeCloud sendBCReq:[[BCQueryRefundByIdReq alloc ] initWithObjectId:refund.objectId]];
    }
}

- (void)onBeeCloudResp:(BCBaseResp *)resp {
    if (resp.type == BCObjsTypeQueryBillByIdResp) {
        BCQueryBillByIdResp *tempResp = (BCQueryBillByIdResp *)resp;
        if (tempResp.resultCode == 0) {
            BCQueryBillResult *result = tempResp.bill;
            NSString *string = [NSString stringWithFormat:@"订单标题:%@\n渠道:%@     金额:%ld\n交易时间:%@\n交易订单号:%@\n交易状态:%@\n,是否撤销:%@\n,optional:%@\n, msgDetail: %@", result.title, result.subChannel, (long)result.totalFee,[BCPayUtil millisecondToDateString:result.createTime],result.billNo,result.payResult?@"成功":@"失败",result.revertResult?@"是":@"否",result.optional, result.msgDetail];
            UIAlertView * alert = [[UIAlertView alloc] initWithTitle:@"QueryById" message:string delegate:self cancelButtonTitle:@"OK" otherButtonTitles:nil, nil];
            [alert show];
        }
        
    } else if (resp.type == BCObjsTypeQueryRefundByIdResp) {
        BCQueryRefundByIdResp *tempResp = (BCQueryRefundByIdResp *)resp;
        if (tempResp.resultCode == 0) {
            BCQueryRefundResult *result = tempResp.refund;
            NSString *string = [NSString stringWithFormat:@"订单标题:%@\n渠道:%@\n总金额:%ld 退款金额:%ld\n交易时间:%@\n交易订单号:%@\n退款单号:%@\n退款是否成功状态:%@\n退款是否完成:%@\nMessageDetail:%@", result.title,result.subChannel, (long)result.totalFee, (long)result.refundFee,[BCPayUtil millisecondToDateString:result.createTime],result.billNo,result.refundNo, result.result?@"成功":@"失败",result.finish?@"完成":@"未完成", result.msgDetail];
            UIAlertView * alert = [[UIAlertView alloc] initWithTitle:@"QueryById" message:string delegate:self cancelButtonTitle:@"OK" otherButtonTitles:nil, nil];
            [alert show];
        }
    } else if (resp.type == BCObjsTypeQueryBillsCountResp) {
        BCQueryBillsCountResp *tempResp = (BCQueryBillsCountResp *)resp;
        if (tempResp.resultCode == 0) {
            self.title = [NSString stringWithFormat:@"满足条件的订单共 %lu", (unsigned long)tempResp.count];
        }
    } else if (resp.type == BCObjsTypeQueryRefundsCountResp) {
        BCQueryRefundsCountResp *tempResp = (BCQueryRefundsCountResp *)resp;
        if (tempResp.resultCode == 0) {
            self.title = [NSString stringWithFormat:@"满足条件的订单共 %lu", (unsigned long)tempResp.count];
        }
    } else if (resp.type == BCObjsTypePreRefundResp) {
        BCPreRefundResp *tempResp = (BCPreRefundResp *)resp;
        NSString *string = [NSString stringWithFormat:@"%@, %@, %@", tempResp.resultMsg, tempResp.errDetail, tempResp.bcId];
        UIAlertView * alert = [[UIAlertView alloc] initWithTitle:@"preRefund" message:string delegate:self cancelButtonTitle:@"OK" otherButtonTitles:nil, nil];
        [alert show];
    }
}

@end
