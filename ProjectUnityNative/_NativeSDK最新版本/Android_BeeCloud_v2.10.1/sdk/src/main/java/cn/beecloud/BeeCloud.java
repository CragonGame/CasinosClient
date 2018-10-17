/**
 * BeeCloud.java
 *
 * Created by xuanzhui on 2015/7/27.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud;

/**
 * 全局参数配置类
 * 建议在主activity中初始化
 */
public class BeeCloud {
    /**
     * BeeCloud Android SDK 版本
     */
    public static final String BEECLOUD_ANDROID_SDK_VERSION = "2.10.1";

    /**
     * 设置AppId和secret(从BeeCloud网站的控制台获得)，并进行一系列异步的初始化
     * 如果是上线版本，secret需要填APP Secret，对于测试模式应填Test Secret，
     * 推荐在主Activity的onCreate函数中调用，或者在Application中初始化
     *
     * @param appId     App ID obtained from BeeCloud website.
     * @param secret    Secret obtained from BeeCloud website.
     */
    public static void setAppIdAndSecret(String appId, String secret) {
        BCCache instance = BCCache.getInstance();
        instance.appId = appId;
        instance.secret = secret;
    }

    /**
     * 修改所有网络请求的超时时间，单位是毫秒，默认为10秒.
     *
     * @param connectTimeout 超时时间，单位为毫秒，例如5000表示5秒.
     */
    public static void setConnectTimeout(Integer connectTimeout) {
        BCCache.getInstance().connectTimeout = connectTimeout;
    }

    /**
     * 设置测试模式，如果不设置将默认为正式版本
     * @param sandbox true表示设置为测试模式，false表示为正式版本
     */
    public static void setSandbox(boolean sandbox){
        BCCache.getInstance().isTestMode = sandbox;
    }
}
