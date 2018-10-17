/**
 * BCException.java
 *
 * Created by xuanzhui on 2015/7/29.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud;

/**
 * 异常类
 */
public class BCException extends Exception {
    public BCException(String exceptionMsg){
        super(exceptionMsg);
    }
}
