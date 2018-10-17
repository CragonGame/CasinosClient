/**
 * BCCache.java
 *
 * Created by xuanzhui on 2015/7/27.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
*/
package cn.beecloud;

import android.content.Context;
import android.content.SharedPreferences;
import android.util.Log;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

/**
 * 配置缓存类
 * 单例模式
 */
public class BCCache {

    private static BCCache instance;

    private static final String separator = "#@#";

    /**
     * BeeCloud控制台注册的App Id
     */
    public String appId;

    /**
     * BeeCloud控制台注册的App Secret(如果是正式版本)，或者Test Secret(如果是测试模式)
     */
    public String secret;

    /**
     * 是否为测试模式
     */
    public boolean isTestMode;

    /**
     * 微信App Id
     */
    public String wxAppId;

    /**
     * 网络请求timeout时间
     * 以毫秒为单位
     */
    public Integer connectTimeout;

    /**
     * 暂存每次支付结束后的返回的订单唯一标识
     */
    public String billID;

    /**
     * 线程池
     */
    public static ExecutorService executorService = Executors.newCachedThreadPool();

    private BCCache() {
    }

    /**
     * 唯一获取实例的方法
     * @return  BCCache实例
     */
    public synchronized static BCCache getInstance() {
        if (instance == null) {
            instance = new BCCache();

            instance.appId = null;
            instance.secret = null;
            instance.wxAppId = null;

            instance.connectTimeout = 10000;
        }

        return instance;
    }

    private String joinStrings(List<String> strings){
        if (strings == null || strings.size() == 0)
            return null;

        StringBuilder joined = new StringBuilder();
        int cnt = 0;
        for (String s : strings) {
            joined.append(s);

            if (++cnt < strings.size())
                joined.append(separator);
        }

        return joined.toString();
    }

}
