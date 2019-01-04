//
//  TalkingDataGA.mm
//  TalkingData
//
//  Created by Biao Hou on 12-6-21.
//  Copyright (c) 2012年 __MyCompanyName__. All rights reserved.
//

#import "TalkingDataGA.h"

// Converts C style string to NSString
static NSString *tdgaCreateNSString(const char *string) {
	if (string)
		return [NSString stringWithUTF8String:string];
	else
		return nil;
}

static TDGAAccount *tdgaAccount = nil;
static char *tdgaDeviceId = nil;

extern "C" {

#pragma GCC diagnostic ignored "-Wmissing-prototypes"
    
    void tdgaOnStart(const char *appId, const char *channelId) {
        [TalkingDataGA onStart:tdgaCreateNSString(appId) withChannelId:tdgaCreateNSString(channelId)];
    }
    
    void tdgaOnEvent(const char *eventId, const char *keys[], const char *stringValues[], double numberValues[], int count) {
        NSMutableDictionary *dic = [NSMutableDictionary dictionary];
        for (int i = 0; i < count; i++) {
            if (keys[i] != NULL) {
                if (stringValues[i] != NULL) {
                    [dic setObject:tdgaCreateNSString(stringValues[i]) forKey:tdgaCreateNSString(keys[i])];
                } else {
                    [dic setObject:[NSNumber numberWithDouble:numberValues[i]] forKey:tdgaCreateNSString(keys[i])];
                }
            }
        }
        [TalkingDataGA onEvent:tdgaCreateNSString(eventId) eventData:dic];
    }
    
    void tdgaSetLocation(double latitude, double longitude) {
        [TalkingDataGA setLatitude:latitude longitude:longitude];
    }
    
    const char *tdgaGetDeviceId() {
        if (NULL == tdgaDeviceId) {
            NSString *deviceId = [TalkingDataGA getDeviceId];
            tdgaDeviceId = (char *)calloc(deviceId.length + 1, sizeof(char));
            strcpy(tdgaDeviceId, deviceId.UTF8String);
        }
        
        return tdgaDeviceId;
    }
    
    void tdgaSetDeviceToken(const char *deviceToken) {
        NSString *token = tdgaCreateNSString(deviceToken);
        [TalkingDataGA setDeviceToken:(NSData *)token];
    }
    
    void tdgaHandlePushMessage(const char *message) {
        NSString *val = tdgaCreateNSString(message);
        NSDictionary *dic = [NSDictionary dictionaryWithObject:val forKey:@"sign"];
        [TalkingDataGA handleTDGAPushMessage:dic];
    }
    
    void tdgaSetVerboseLogDisabled() {
        [TalkingDataGA setVerboseLogDisabled];
    }
    
    void tdgaSetSDKFramework(int tag) {
        [TalkingDataGA setSDKFramework:tag];
    }
    
    void tdgaSetAccount(const char *accountId) {
        tdgaAccount = [TDGAAccount setAccount:tdgaCreateNSString(accountId)];
    }
    
    void tdgaSetAccountName(const char *accountName) {
        if (nil != tdgaAccount) {
            [tdgaAccount setAccountName:tdgaCreateNSString(accountName)];
        }
    }
    
    void tdgaSetAccountType(int accountType) {
        if (nil != tdgaAccount) {
            [tdgaAccount setAccountType:accountType];
        }
    }
    
    void tdgaSetLevel(int level) {
        if (nil != tdgaAccount) {
            [tdgaAccount setLevel:level];
        }
    }
    
    void tdgaSetGender(int gender) {
        if (nil != tdgaAccount) {
            [tdgaAccount setGender:gender];
        }
    }
    
    void tdgaSetAge(int age) {
        if (nil != tdgaAccount) {
            [tdgaAccount setAge:age];
        }
    }
    
    void tdgaSetGameServer(const char *gameServer) {
        if (nil != tdgaAccount) {
            [tdgaAccount setGameServer:tdgaCreateNSString(gameServer)];
        }
    }
    
    void tdgaOnBegin(const char *missionId) {
        [TDGAMission onBegin:tdgaCreateNSString(missionId)];
    }
    
    void tdgaOnCompleted(const char *missionId) {
        [TDGAMission onCompleted:tdgaCreateNSString(missionId)];
    }
    
    void tdgaOnFailed(const char *missionId, const char *failedCause) {
        [TDGAMission onFailed:tdgaCreateNSString(missionId) failedCause:tdgaCreateNSString(failedCause)];
    }
    
    void tdgaOnChargeRequst(const char *orderId, const char *iapId, double currencyAmount, const char *currencyType, double virtualCurrencyAmount, const char *paymentType) {
        [TDGAVirtualCurrency onChargeRequst:tdgaCreateNSString(orderId) iapId:tdgaCreateNSString(iapId) currencyAmount:currencyAmount currencyType:tdgaCreateNSString(currencyType) virtualCurrencyAmount:virtualCurrencyAmount paymentType:tdgaCreateNSString(paymentType)];
    }
    
    void tdgaOnChargSuccess(const char *orderId) {
        [TDGAVirtualCurrency onChargeSuccess:tdgaCreateNSString(orderId)];
    }
    
    void tdgaOnReward(double virtualCurrencyAmount, const char *reason) {
        [TDGAVirtualCurrency onReward:virtualCurrencyAmount reason:tdgaCreateNSString(reason)];
    }
    
    void tdgaOnPurchase(const char *item, int itemNumber, double priceInVirtualCurrency) {
        [TDGAItem onPurchase:tdgaCreateNSString(item) itemNumber:itemNumber priceInVirtualCurrency:priceInVirtualCurrency];
    }
    
    void tdgaOnUse(const char *item, int itemNumber) {
        [TDGAItem onUse:tdgaCreateNSString(item) itemNumber:itemNumber];
    }
    
#pragma GCC diagnostic warning "-Wmissing-prototypes"
}
