//
//  AppDelegate.swift
//  BuglyDemoSwift
//
//  Created by Ben Xu on 15/9/10.
//  Copyright © 2015年 Tencent. All rights reserved.
//

import UIKit

@UIApplicationMain
class AppDelegate: UIResponder, UIApplicationDelegate {

    var window: UIWindow?


    func application(application: UIApplication, didFinishLaunchingWithOptions launchOptions: [NSObject: AnyObject]?) -> Bool {
        // Override point for customization after application launch.
        
        // Get the default config
        let config = BuglyConfig()
        #if DEBUG
        config.debugMode = true
        #endif
        config.reportLogLevel = BuglyLogLevel.Warn
        
        config.channel = "Bugly"
        
        Bugly.startWithAppId("900001055", config: config)
        
        Bugly.setTag(1799);
        
        Bugly.setUserIdentifier(UIDevice.currentDevice().name)
        
        Bugly.setUserValue(NSProcessInfo.processInfo().processName, forKey: "Process")
        
        BLogError("Swift Log Print %@", "Test")
        BLogWarn("Swift Log Print %@", "Test")
        
        self.performSelectorInBackground(#selector(AppDelegate.logInBackground), withObject: nil)
        return true
    }

    func logInBackground(){
    
        while(1 > 0) {
            BLogError("Test %@", "Error")
        
            BLogWarn("Test %@", "WARN")
            
            sleep(1)
        }
    }
    
    func applicationWillResignActive(application: UIApplication) {
        // Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
        // Use this method to pause ongoing tasks, disable timers, and throttle down OpenGL ES frame rates. Games should use this method to pause the game.
    }

    func applicationDidEnterBackground(application: UIApplication) {
        // Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later.
        // If your application supports background execution, this method is called instead of applicationWillTerminate: when the user quits.
    }

    func applicationWillEnterForeground(application: UIApplication) {
        // Called as part of the transition from the background to the inactive state; here you can undo many of the changes made on entering the background.
    }

    func applicationDidBecomeActive(application: UIApplication) {
        // Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.
    }

    func applicationWillTerminate(application: UIApplication) {
        // Called when the application is about to terminate. Save data if appropriate. See also applicationDidEnterBackground:.
    }


}

