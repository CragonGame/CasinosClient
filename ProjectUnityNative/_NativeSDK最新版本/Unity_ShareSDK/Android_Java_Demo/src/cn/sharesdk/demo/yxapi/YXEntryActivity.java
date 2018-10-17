/*
 * 官网地站:http://www.mob.com
 * 技术支持QQ: 4006852216
 * 官方微信:ShareSDK   （如果发布新版本的话，我们将会第一时间通过微信将版本更新内容推送给您。如果使用过程中有任何问题，也可以通过微信与我们取得联系，我们将会在24小时内给予回复）
 *
 * Copyright (c) 2013年 mob.com. All rights reserved.
 */

package cn.sharesdk.demo.yxapi;

import cn.sharesdk.yixin.utils.YXMessage;
import cn.sharesdk.yixin.utils.YixinHandlerActivity;

/** 易信客户端回调activity示例 */
public class YXEntryActivity extends YixinHandlerActivity {

	/** 处理易信向第三方应用发起的消息 */
	public void onReceiveMessageFromYX(YXMessage msg) {
		// 从易信网第三方应用发送消息的处理，从这个方法开始，不过当前易信似乎还没有
		// 提供这样子的功能入口，因此这个方法暂时没有作用，但是如果易信客户端开放了
		// 入口，这个方法就可以接收到消息
	}

}
