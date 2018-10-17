//
//  NotificationService.m
//  NotificationService
//
//  Created by gexin on 16/10/10.
//  Copyright © 2016年 Gexin Interactive (Beijing) Network Technology Co.,LTD. All rights reserved.
//

#import "NotificationService.h"
#import <GTExtensionSDK/GeTuiExtSdk.h>
#import <AVFoundation/AVFoundation.h>

@interface NotificationService ()

@property (nonatomic, strong) void (^contentHandler)(UNNotificationContent *contentToDeliver);
@property (nonatomic, strong) UNMutableNotificationContent *bestAttemptContent;

@end

@implementation NotificationService

- (void)didReceiveNotificationRequest:(UNNotificationRequest *)request withContentHandler:(void (^)(UNNotificationContent *_Nonnull))contentHandler {
    
    self.contentHandler = contentHandler;
    self.bestAttemptContent = [request.content mutableCopy];

    NSLog(@"----将APNs信息交由个推处理----");

    [GeTuiExtSdk handelNotificationServiceRequest:request
                          withAttachmentsComplete:^(NSArray *attachments, NSArray *errors) {

                              //TODO:用户可以在这里处理通知样式的修改，eg:修改标题
                              //self.bestAttemptContent.title = [NSString stringWithFormat:@"%@ [Success]", self.bestAttemptContent.title];
                              
                              //TODO:内容语音播报
                              [self playContent:self.bestAttemptContent.body];
                              
                              self.bestAttemptContent.attachments = attachments; //设置通知中的多媒体附件
                              NSLog(@"处理个推APNs展示遇到错误：%@", errors);    //如果APNs处理有错误，可以在这里查看相关错误详情

                              self.contentHandler(self.bestAttemptContent); //展示推送的回调处理需要放到个推回执完成的回调中
                          }];
}


-(void) playContent:(NSString*) apnTitle {
    //初始化语音播报
    AVSpeechSynthesizer * av = [[AVSpeechSynthesizer alloc]init];
    //设置播报的内容
    AVSpeechUtterance * utterance = [[AVSpeechUtterance alloc]initWithString:apnTitle];
    //设置语言类别
    AVSpeechSynthesisVoice * voiceType = [AVSpeechSynthesisVoice voiceWithLanguage:@"zh-CN"];
    utterance.voice = voiceType;
    //设置播报语速
    utterance.rate = 0.5;
    [av speakUtterance:utterance];
}

- (void)serviceExtensionTimeWillExpire {
    
    //销毁SDK，释放资源
    [GeTuiExtSdk destory];
    
    //测试时查看超时情况
    //self.bestAttemptContent.title = [NSString stringWithFormat:@"%@ [Timeout]", self.bestAttemptContent.title];
    
    // Called just before the extension will be terminated by the system.
    // Use this as an opportunity to deliver your "best attempt" at modified content, otherwise the original push payload will be used.
    self.contentHandler(self.bestAttemptContent);
}

@end
