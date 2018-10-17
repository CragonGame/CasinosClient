//
//  BuglyLogMacro.swift
//  BuglySDKDemo
//
//  Created by Yeelik on 15/11/4.
//  Copyright © 2015年 Tencent. All rights reserved.
//
//
//  This file only to convert the BuglyLog to swift function
//

import Foundation

public func BLogError(format: String, _ args: CVarArgType...){
    BLYLogv(BuglyLogLevel.Error, format, getVaList(args))
    
}

public func BLogWarn(format: String, _ args: CVarArgType...){
     BLYLogv(BuglyLogLevel.Warn, format, getVaList(args))
}

public func BLogInfo(format: String, _ args: CVarArgType...){
     BLYLogv(BuglyLogLevel.Info, format, getVaList(args))
}

public func BLogDebug(format: String, _ args: CVarArgType...){
     BLYLogv(BuglyLogLevel.Debug, format, getVaList(args))
}
