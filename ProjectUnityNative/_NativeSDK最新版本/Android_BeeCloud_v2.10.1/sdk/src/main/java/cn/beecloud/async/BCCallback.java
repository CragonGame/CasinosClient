/**
 * BCCallback.java
 *
 * Created by xuanzhui on 2015/7/27.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.async;

/**
 *  用于操作完成之后的回调
 */
public interface BCCallback {
    /**
     * 操作完成后的回调接口
     * @param result    包含支付或者查询结果信息
     */
    void done(BCResult result);
}
