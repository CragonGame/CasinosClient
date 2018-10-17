//
//  ViewController.swift
//  BuglyDemoSwift
//
//  Created by Ben Xu on 15/9/10.
//  Copyright © 2015年 Tencent. All rights reserved.
//

import UIKit

class ViewController: UIViewController {

    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
        BLogInfo("Test %@", "xxxx");
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }

    @IBAction func crash(sender: AnyObject) {
        NSLog("Test crash");
        let array = []
        print(array[1])
    }

    
}

