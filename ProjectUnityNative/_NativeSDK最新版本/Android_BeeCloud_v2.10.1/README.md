## BeeCloud Android SDK (Open Source)

[![Build Status](https://travis-ci.org/beecloud/beecloud-android.svg)](https://travis-ci.org/beecloud/beecloud-android) ![license](https://img.shields.io/badge/license-MIT-brightgreen.svg) ![version](https://img.shields.io/badge/version-v2.10.1-blue.svg)

## 简介

本项目的官方GitHub地址是 [https://github.com/beecloud/beecloud-android](https://github.com/beecloud/beecloud-android)

SDK支持以下支付渠道:

 * 微信APP
 * 支付宝APP
 * 银联在线APP
 * 百度钱包

包含预退款、订阅支付、鉴权和相关的查询功能。
还提供了线下收款功能(包括微信扫码、微信刷卡、支付宝扫码、支付宝条形码)，订单状态的查询以及订单撤销。 

## 流程

下图为整个支付的流程:
![pic](http://7xavqo.com1.z0.glb.clouddn.com/UML01.png)



步骤①：**（在App端）发送订单信息**

做完这一步之后就会跳到相应的支付页面（如微信app中），让用户继续后续的支付步骤

步骤②：**（在App端）处理同步回调结果**

付款完成或取消之后，会回到客户app中，需要做相应界面展示的更新（比如弹出框告诉用户"支付成功"或"支付失败")。非常不推荐用同步回调的结果来作为最终的支付结果，因为同步回调可能（虽然可能性不大）出现结果不准确的情况，最终支付结果应以下面的异步回调为准。

步骤③：**（在客户服务端）处理异步回调结果（[Webhook](https://beecloud.cn/doc/?index=webhook)）**
 
付款完成之后，根据客户在BeeCloud后台的设置，BeeCloud会向客户服务端发送一个Webhook请求，里面包括了数字签名，订单号，订单金额等一系列信息。客户需要在服务端依据规则要验证**数字签名是否正确，购买的产品与订单金额是否匹配，这两个验证缺一不可**。验证结束后即可开始走支付完成后的逻辑。  

## 导入SDK
1. 添加依赖  
  
>1. 推荐通过添加`model`的方式（适用于`gradle`，推荐直接使用`Android Studio`）
引入`sdk model`，在`project`的`settings.gradle`中`include ':sdk'`，并在需要支付的`model`（比如本项目中的`demo`） `build.gradle`中添加依赖`compile project(':sdk')`，需要JDK版本7或以上。

>2. 对于需要以`jar`方式引入的情况  
添加第三方的支付类，在`beecloud-android\sdk`目录下  
`gson-x.x.jar`为必须引入的jar，  
`zxing-x.x.x.jar`为生成二维码必须引入的jar，  
微信支付(`WX_APP`和`BC_WX_APP `)需要引入`wechat-sdk-android-with-mta-x.x.x.jar`，  
支付宝(`ALI_APP`和`BC_ALI_APP`)需要引入`alipaySdk-xxx.jar`，  
银联需要引入`UPPayAssistEx.jar`、`UPPayPluginExPro.jar`，  
百度钱包支付需要引入`Cashier_SDK-v4.2.0.jar`，  
最后添加`beecloud android sdk`：`beecloud-x.x.x.jar`，和其同级目录下依赖的`okhttp-x.x.x.jar`，`okio-x.x.x.jar`

2.对于微信APP支付，需要注意你的`AndroidManifest.xml`中`package`需要和微信平台创建的移动应用`应用包名`保持一致，否则会遭遇[`一般错误`](http://help.beecloud.cn/hc/kb/article/157111/)  

3.对于银联支付需要将`beecloud-android\sdk\manualres\unionpay\data.bin`引入你的工程`assets`目录下；同时根据需求将同目录下的so文件添加到工程  

4.对于百度钱包支付，需要
>1. 将`beecloud-android\sdk\manualres\baidupay\res`添加到你的`res`目录下；
>2. 另外，对于使用`Android Studio`的用户，需要将`beecloud-android\sdk\manualres\baidupay\`目录下的`armeabi`文件夹拷贝到`src\main\jniLibs`目录下，如果没有`jniLibs`目录，请手动创建；对用使用`Eclipse`的用户，需要将`beecloud-android\sdk\manualres\baidupay\`目录下的`armeabi`文件夹拷贝到`libs`目录下。  


## 使用方法
> 具体使用请参考项目中的`demo`

### 1.初始化支付参数
请参考`demo`中的`ShoppingCartActivity.java`  
  
>1 在主activity或者application的onCreate函数中初始化BeeCloud账户中的AppID和secret，并设置是否开启测试模式（如果不设置默认不开启）；注意：如果是测试模式，secret应该填Test Secret，如果是上线版本，应该填App Secret，例如  

```java
//开启测试模式
BeeCloud.setSandbox(true);
//此处第二个参数是控制台的test secret
BeeCloud.setAppIdAndSecret("c5d1cba1-5e3f-4ba0-941d-9b0a371fe719",
        "4bfdd244-574d-4bf3-b034-0c751ed34fee");
```    
>2 如果用到微信APP支付，在用到微信支付的Activity的onCreate函数里调用以下函数，第二个参数需要换成你自己的微信AppID，例如  

```java
BCPay.initWechatPay(ShoppingCartActivity.this, "wxf1aa465362b4c8f1");
```    
  
### 2. 在`AndroidManifest.xml`中添加`permission`
```java
<!-- for all -->
<uses-permission android:name="android.permission.INTERNET" />
<uses-permission android:name="android.permission.ACCESS_WIFI_STATE" />
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />

<!-- for union pay -->
<uses-permission android:name="android.permission.READ_PHONE_STATE" />
<uses-permission android:name="org.simalliance.openmobileapi.SMARTCARD" />
<uses-permission android:name="android.permission.NFC" />
<uses-feature android:name="android.hardware.nfc.hce"/>

<!-- for Baidu -->
<uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" />
<uses-permission android:name="android.permission.WRITE_SETTINGS" />
<uses-permission android:name="android.permission.READ_SMS" />
```

### 3. 在`AndroidManifest.xml`中注册`activity`
> 如果开启测试模式，需要添加（上线版本不需要）  

```java
<activity
    android:name="cn.beecloud.BCMockPayActivity"
    android:screenOrientation="portrait"
    android:theme="@android:style/Theme.Translucent.NoTitleBar" />
```

> 对于微信APP支付，需要添加  

```java
<activity
    android:name="cn.beecloud.BCWechatPaymentActivity"
    android:launchMode="singleTop"
    android:theme="@android:style/Theme.Translucent.NoTitleBar" />

<activity-alias
    android:name=".wxapi.WXPayEntryActivity"
    android:exported="true"
    android:targetActivity="cn.beecloud.BCWechatPaymentActivity" />
```
> 对于支付宝，需要添加  

```java
<activity
    android:name="com.alipay.sdk.app.H5PayActivity"
    android:configChanges="orientation|keyboardHidden|navigation"
    android:exported="false"
    android:screenOrientation="behind"
    android:windowSoftInputMode="adjustResize|stateHidden" />
```
> 对于银联，需要添加  

```java
<activity
    android:name="cn.beecloud.BCUnionPaymentActivity"
    android:configChanges="orientation|keyboardHidden"
    android:excludeFromRecents="true"
    android:launchMode="singleTop"
    android:screenOrientation="portrait"
    android:theme="@android:style/Theme.Translucent.NoTitleBar"
    android:windowSoftInputMode="adjustResize" />
    
<uses-library android:name="org.simalliance.openmobileapi" android:required="false"/>

<activity
    android:name="com.unionpay.uppay.PayActivity"
    android:label="@string/app_name"
    android:screenOrientation="portrait"
    android:configChanges="orientation|keyboardHidden"
    android:excludeFromRecents="true"
    android:windowSoftInputMode="adjustResize"/>

<activity
    android:name="com.unionpay.UPPayWapActivity"
    android:configChanges="orientation|keyboardHidden"
    android:screenOrientation="portrait"
    android:windowSoftInputMode="adjustResize"/>
```

> 对于百度钱包，由于需要添加的activity数量众多，请参考demo中的AndroidManifest.xml  

> 对于微信wap，需要添加  

```java
<activity
        android:name="cn.beecloud.BCWXWapPaymentActivity"
        android:screenOrientation="portrait"
        android:theme="@android:style/Theme.Translucent.NoTitleBar" />
```

### 4.支付

请查看`doc`中的`API`，支付类`BCPay`，参照`demo`中`ShoppingCartActivity`

**原型：** 

通过`BCPay`的实例，以`reqPaymentAsync`方法发起所有支持的支付请求，该方法的调用请参考demo支付示例，BCPay.PayParams参数请参阅[API](https://beecloud.cn/doc/api/beecloud-android/cn/beecloud/BCPay.PayParams.html)。  

参数中channelType可以是`WX_APP`(微信APP)、`ALI_APP`(支付宝APP)、`UN_APP`(银联APP)、`BD_APP`(百度钱包APP)、`BC_APP`(BeePay快捷APP)、`BC_WX_APP`(BeePay微信APP)、`BC_WX_WAP`(BeePay微信WAP)、`BC_ALI_APP`(BeePay支付宝APP)    

参数依次为
> payParam        BCPay.PayParams类型  
> callback        支付完成后的回调入口

在回调函数中将`BCResult`转化成`BCPayResult`之后做后续处理  
**调用：（以微信为例）**

```java
//定义回调
BCCallback bcCallback = new BCCallback() {
    @Override
    public void done(final BCResult bcResult) {
        //此处根据业务需要处理支付结果
        final BCPayResult bcPayResult = (BCPayResult)bcResult;

        switch (bcPayResult.getResult()) {
            case BCPayResult.RESULT_SUCCESS:
                //用户支付成功
                break;
            case BCPayResult.RESULT_CANCEL:
                //用户取消支付"
                break;
            case BCPayResult.RESULT_FAIL:
                //支付失败
        }
    }
};

//创建支付参数类
BCPay.PayParams payParam = new BCPay.PayParams();

// 发起的渠道类型
payParam.channelType = BCReqParams.BCChannelTypes.WX_APP;

//商品描述, 32个字节内, 汉字以2个字节计
payParam.billTitle = "安卓微信支付测试";

//支付金额，以分为单位，必须是正整数
payParam.billTotalFee = 10;

//商户自定义订单号
payParam.billNum = "unique bill number";

// 商家自定义的消费者Id，可选参数，传入将分析用户行为，请参照 #商户消费者系统# 上传商户消费者ID
payParam.buyerId = "merchant-buyer-id";

// 卡券Id，可选参数，请参照 #营销卡券# 分发卡券
payParams.couponId = "coupon-id";    

//发起支付
BCPay.getInstance(context).reqPaymentAsync(payParam,
        bcCallback);
```


### 5.线下支付
请查看`doc`中的`API`，线下支付类`BCOfflinePay`，参照`demo`中`QRCodeEntryActivity`和其关联的activity；一般用于线下门店通过出示二维码由用户扫描付款，或者通过用户出示的付款码收款。  
线下支付基本流程：
> 1 通过二维码或者付款码发起支付；  
> 2 支付结束后调用查询接口确认支付结果。  

**原型：** 

通过`BCOfflinePay`的实例，以`reqQRCodeAsync`方法请求生成支付二维码。   
通过`BCOfflinePay`的实例，以`reqOfflinePayAsync`方法通过获取到的付款码发起收款  

参数依次为
> payParam        [BCOfflinePay.PayParams类型](https://beecloud.cn/doc/api/beecloud-android/cn/beecloud/BCOfflinePay.PayParams.html)  
> callback        支付完成后的回调入口


- 请求生成支付二维码

二维码包含`WX_NATIVE`(微信扫码)、`ALI_OFFLINE_QRCODE`(支付宝扫码)、`BC_NATIVE`(BeePay微信扫码)、`BC_ALI_QRCODE`(BeePay支付宝扫码)  

专有参数
> genQRCode       是否生成QRCode Bitmap
> 如果为false，请自行根据getQrCodeRawContent返回的URL，使用BCPay.generateBitmap方法生成支付二维码，你也可以使用自己熟悉的二维码生成工具  
> qrCodeWidth     如果生成二维码(genQRCode为true), QRCode的宽度(以px为单位), null则使用默认参数360px

在回调函数中将`BCResult`转化成`BCQRCodeResult`之后做后续处理  

**调用：**  

```java
BCOfflinePay.getInstance().reqQRCodeAsync(
        payParam,
        new BCCallback() {
            @Override
            public void done(BCResult bcResult) {

                final BCQRCodeResult bcqrCodeResult = (BCQRCodeResult) bcResult;

                //resultCode为0表示请求成功
                if (bcqrCodeResult.getResultCode() == 0) {
                    //如果你设置了生成二维码参数为true那么此处可以获取二维码
                    qrCodeBitMap = bcqrCodeResult.getQrCodeBitmap();

                    //否则通过 bcqrCodeResult.getQrCodeRawContent() 获取二维码的内容，自己去生成对应的二维码

                } else {
                    //deal with err msg
                }
            }
        });
```


- 通过付款码发起收款

支持`WX_SCAN`(微信刷卡), `ALI_SCAN`(支付宝刷卡)、`BC_WX_SCAN`(BeePay微信刷卡)、`BC_ALI_SCAN`(BeePay支付宝刷卡)  

专有参数
> authCode        用户出示的收款码  
> terminalId      机具终端编号，支付宝扫码(ALI_SCAN)的选填参数  
> storeId         商户门店编号，支付宝扫码(ALI_SCAN)的选填参数

在回调函数中将`BCResult`转化成`BCPayResult`之后做后续处理  

**调用：**  

```java
BCOfflinePay.getInstance().reqOfflinePayAsync(
        payParam,
        new BCCallback(){...});
```

* **关于订单的撤销**

支持WX\_SCAN, ALI\_OFFLINE\_QRCODE, ALI\_SCAN   
订单撤销后，用户将不能继续支付，这和退款是不同的操作，具体请参考`GenQRCodeActivity`  

```java
BCOfflinePay.getInstance().reqRevertBillAsync(
    channelType,
    billNum,        //需要撤销的订单号
    new BCCallback(){...})
```

* **关于支付宝内嵌二维码**
  
支付宝内嵌二维码属于线上产品，支付结果会及时反馈，并不需要额外的查询操作，具体可以参考`QRCodeEntryActivity`和`ALIQRCodeActivity`，注意需要通过`BCPay`调用  

```java
BCPay.getInstance(QRCodeEntryActivity.this).reqAliInlineQRCodeAsync(
		"支付宝内嵌二维码支付测试",   	//商品描述
        1,                            	//总金额, 以分为单位, 必须是正整数
        billNum,      	                //流水号
        mapOptional,                    //扩展参数
        "https://beecloud.cn/",  		//returnUrl，支付成功之后的返回url
        "1",                          	//qrPayMode，二维码类型
        new BCCallback(){...});
```
请求生成支付宝内嵌支付二维码的特有参数说明
> returnUrl       支付成功后的同步跳转页面, 必填  
> qrPayMode       支付宝内嵌二维码类型
>> 可选项  
   "0": 订单码-简约前置模式, 对应 iframe 宽度不能小于 600px, 高度不能小于 300px  
   "1": 订单码-前置模式, 对应 iframe 宽度不能小于 300px, 高度不能小于 600px  
   "3": 订单码-迷你前置模式, 对应 iframe 宽度不能小于 75px, 高度不能小于 75px  
   
### 6.预退款
预退款结束后仍然需要在server端发起`预退款批量审核`，以达到真实退款的目的。  
  
**原型：**

通过构造`BCPay`的实例，使用`prefund`方法发起预退款；在回调函数中将`BCResult`转化成`BCRefundResult`之后做后续处理；可参照`demo`中`BillListActivity`  
  
**调用：**

```java
BCPay.RefundParams params = new BCPay.RefundParams();
//退款单号
params.refundNum = "20160621162753241";
//需要退款的订单号
params.billNum = "20160621162735489";
//退款金额，必须为正整数，单位为分
params.refundFee = 10;
BCPay.getInstance(YourActivity.this).prefund(params, bccallback);
```

   
### 7.查询

* **查询支付订单**

请查看`doc`中的`API`，查询类`BCQuery`，参照`demo`中`BillListActivity`

**原型：**

通过构造`BCQuery`的实例，使用`queryBillsAsync`方法发起支付查询，`channel`指代何种支付方式，为`BCReqParams.BCChannelTypes.ALL`时则查询所有的支付渠道订单；在回调函数中将`BCResult`转化成`BCQueryBillsResult`之后做后续处理

**调用：**

```java
//回调入口
final BCCallback bcCallback = new BCCallback() {
    @Override
    public void done(BCResult bcResult) {
    	//根据需求处理结果数据

        final BCQueryBillsResult bcQueryResult = (BCQueryBillsResult) bcResult;

        //resultCode为0表示请求成功
        if (bcQueryResult.getResultCode() == 0) {
			//订单列表
	        bills = bcQueryResult.getBills();
        } else {
            //deal with err msg
        }
    }
};

//发起查询请求
BCQuery.getInstance().queryBillsAsync(
    BCReqParams.BCChannelTypes.UN_APP,      //渠道
    null,                                   //订单号
    startTime.getTime(),                    //订单生成时间
    endTime.getTime(),                      //订单完成时间
    2,                                      //跳过满足条件的前2条数据
    15,                                     //返回满足条件的15条数据
    bcCallback);
```

* **查询退款订单**

请查看`doc`中的`API`，查询类`BCQuery`，参照`demo`中`RefundOrdersActivity`

**原型：**

通过构造`BCQuery`的实例，使用`queryRefundsAsync`方法发起退款查询，`channel`指代何种支付方式，为`BCReqParams.BCChannelTypes.ALL`时则查询所有的支付渠道退款订单；在回调函数中将`BCResult`转化成`BCQueryRefundsResult`之后做后续处理

**调用：**  
同上，首先初始化回调入口BCCallback  

```java
BCQuery.getInstance().queryRefundsAsync(
    BCReqParams.BCChannelTypes.UN,          //渠道
    null,                                   //订单号
    null,                                   //商户退款流水号
    startTime.getTime(),                    //退款订单生成时间
    endTime.getTime(),                      //退款订单完成时间
    1,                                      //跳过满足条件的前1条数据
    15,                                     //返回满足条件的15条数据
    new BCCallback(){...});
```

* **查询订单数目**

请查看`doc`中的`API`，查询类`BCQuery`，参照`demo`中`BillListActivity`和`RefundOrdersActivity`

**原型：**

通过构造`BCQuery`的实例，使用`queryBillsCountAsync`方法发起支付订单数目查询，使用`queryRefundsCountAsync`方法发起退款订单数目查询；在回调函数中将`BCResult`转化成`BCQueryCountResult`之后做后续处理

**调用：**  
以查询支付订单数目为例  

```java
BCQuery.QueryParams params = new BCQuery.QueryParams();

//以下为可用的限制参数
//渠道类型
params.channel = BCReqParams.BCChannelTypes.ALL;

//支付单号
//params.billNum = "your bill number";

//订单是否支付成功
params.payResult = Boolean.TRUE;

//限制起始时间
params.startTime = startTime.getTime();

//限制结束时间
params.endTime = endTime.getTime();

BCQuery.getInstance().queryBillsCountAsync(params, new BCCallback(){...});
```

* **查询订单退款状态**

请查看`doc`中的`API`，查询类`BCQuery`，参照`demo`中`RefundStatusActivity`

**原型：**

通过构造`BCQuery`的实例，使用`queryRefundStatusAsync`方法发起支付查询，该方法所有参数都必填，`channel`指代何种支付方式，目前由于第三方API的限制仅支持微信、易宝、快钱和百度；在回调函数中将`BCResult`转化成`BCRefundStatus`之后做后续处理

**调用：**  
同上，首先初始化回调入口BCCallback  

```java
BCQuery.getInstance().queryRefundStatusAsync(
    BCReqParams.BCChannelTypes.WX,     //目前仅支持WX、YEE、KUAIQIAN、BD
    "20150520refund001",                   //退款单号
    new BCCallback(){...});                           //回调入口
```

* **根据ID查询订单**

请查看`doc`中的`API`，查询类`BCQuery`，注意此处的ID不是订单号，是在发起支付或者退款的时候返回的唯一标识符。

**原型：**

通过`BCQuery`的实例，以`queryBillByIDAsync`方法发起支付订单查询，查询结果转化成`BCQueryBillResult`做后续处理，请参照`demo`中`ShoppingCartActivity`。  
通过`BCQuery`的实例，以`queryRefundByIDAsync`方法发起退款订单查询，查询结果转化成`BCQueryRefundResult`做后续处理，请参照`demo`中`RefundOrdersActivity`。  

**调用：**  
同上，首先初始化回调入口BCCallback  

```java
BCQuery.getInstance().queryBillByIDAsync(
                id,
                new BCCallback(){...});
```

* **查询线下订单状态**

请查看`doc`中的`API`，查询类`BCQuery`，参照`demo`中`GenQRCodeActivity`

**原型：**

通过`BCQuery`的实例，以`queryOfflineBillStatusAsync`方法发起支付订单查询，查询结果转化成`BCBillStatus`做后续处理，请参照`demo`中`ShoppingCartActivity`。   

**调用：**  
同上，首先初始化回调入口BCCallback  

```java
BCQuery.getInstance().queryOfflineBillStatusAsync(
        channelType,		//渠道类型
        billNum,			//支付订单号
        new BCCallback() {
            @Override
            public void done(BCResult result) {
                BCBillStatus billStatus = (BCBillStatus) result;
                //表示支付成功
                if (billStatus.getResultCode() == 0 &&
                        billStatus.getPayResult()) {
                    //TODO
                } else {
                    //deal with err msg
                }
            }
        }
);
```
  
### 8.订阅支付
先查看[订阅系统说明文档](https://github.com/beecloud/beecloud-rest-api/blob/master/subscription/%E8%AE%A2%E9%98%85%E7%B3%BB%E7%BB%9F%E8%AF%B4%E6%98%8E%E6%96%87%E6%A1%A3.md)了解基础概念，开发过程中如果对相关变量命名有疑惑，查看[API References](https://beecloud.cn/doc/api/beecloud-android/)  
  
* **查询计划列表**
  
**原型：**

通过`BCQuery`的实例，以`queryPlans`方法查询计划列表，查询结果转化成`BCPlanListResult`后通过`getPlans`获取计划列表，请参照`demo`中`SubscribeActivity`。  
`queryPlans`的参数`BCPlanCriteria`可以设置只查询总个数 `setCountOnly(true)`，结果可以通过`getTotalCount`获取。  

**调用：**  
  
同上，首先初始化回调入口BCCallback  

```java
// 参照API添加查询计划限制条件
BCPlanCriteria criteria = new BCPlanCriteria();
// 比如限制最多返回8条记录
criteria.setLimit(8);
// 比如只返回计划名包含"plan"的
criteria.setNameWithSubstring("plan");

BCQuery.getInstance().queryPlans(criteria,
        new BCCallback() {...});
```
  
* **订阅支付支持的银行列表**
  
**原型：**

通过`BCQuery`的实例，以`subscriptionSupportedBanks`方法查询，操作结果转化成`BCSubscriptionBanksResult`做后续处理，请参照`demo`中`SubscribeActivity`。  
如果查询成功，结果中包含支持的常用银行列表和所有支持的银行列表，后者包含前者。    

**调用：**  
  
同上，首先初始化回调入口BCCallback  

```java
BCQuery.getInstance().subscriptionSupportedBanks(new BCCallback() {...});
```
  
* **发送验证码**
  
**原型：**

通过`BCPay`的实例，以`sendSmsCode`方法发送验证码，操作结果转化成`BCSmsResult`做后续处理，请参照`demo`中`SubscribeActivity`。  
如果发送成功，返回结果包含`smsId`，同时发送`smsCode`到用户手机。    

**调用：**  
  
同上，首先初始化回调入口BCCallback  

```java
BCPay.getInstance(this).sendSmsCode("接收验证码的手机号",new BCCallback() {...});
```
  
* **发起订阅**
  
**原型：**

通过`BCPay`的实例，以`subscribe`方法发起订阅，操作结果转化成`BCSubscriptionResult`做后续处理，请参照`demo`中`SubscribeActivity`。  
`subscribe`的验证码参数可以通过上文的`sendSmsCode`获取，返回结果包含`BCSubscription`对象，如果该对象`isValid`返回`true`表明本次订阅已经即时生效，否则你还需要等待webhook推送最终审核结果。    

**调用：**  
  
同上，首先初始化回调入口BCCallback  

```java
BCSubscription subscription = new BCSubscription();
subscription.setBuyerId("buyer id");
subscription.setPlanId("plan id");
subscription.setBankName("like中国银行");
subscription.setCardNum("bank card number");
subscription.setIdName("身份证姓名");
subscription.setIdNum("身份证号");
subscription.setMobile("和银行卡绑定的手机号");

// 第四个参数用于后期优惠码，目前请填null
BCPay.getInstance(this).subscribe(subscription, smsId, smsCode, null,
		new BCCallback() {...});
```
  
* **取消订阅**
  
**原型：**

通过`BCPay`的实例，以`cancelSubscription`方法发送取消请求，操作结果转化成`BCObjectIdResult`做后续处理，请参照`demo`中`SubscriptionListActivity`。  
如果请求成功，返回结果`getResultCode`应当为`0`，`getId`返回本次取消的订阅id。    

**调用：**  
  
同上，首先初始化回调入口BCCallback  

```java
BCPay.getInstance(this).cancelSubscription("需要取消的订阅id",new BCCallback() {...});
```

* **查询订阅列表**
  
**原型：**

通过`BCQuery`的实例，以`querySubscriptions`方法查询计划列表，查询结果转化成`BCSubscriptionListResult`后通过`getSubscriptions`获取订阅列表，请参照`demo`中`SubscriptionListActivity`。  
`querySubscriptions`的参数`BCSubscriptionCriteria`可以设置只查询总个数 `setCountOnly(true)`，结果可以通过`getTotalCount`获取。  

**调用：**  
  
同上，首先初始化回调入口BCCallback  

```java
// 参照API添加查询订阅限制条件
BCSubscriptionCriteria criteria = new BCSubscriptionCriteria();
// 比如限制最多返回10条记录
criteria.setLimit(10);
// 比如只返回订阅用户id是ohmy的记录
criteria.setBuyerId("ohmy");

BCQuery.getInstance().querySubscriptions(criteria,
        new BCCallback() {...});
```
  

###  9. 鉴权
**原型：**

通过`BCValidationUtil.verifyCardFactors`方法鉴权，结果转化成`BCCardVerifyResult`做后续处理，请参照`demo`中`VerifyCardActivity`。  

**调用：**  
  
同上，首先初始化回调入口BCCallback  

```java
// 二要素鉴权
BCValidationUtil.verifyCardFactors(
    "姓名",
    "身份证号码",
    new BCCallback() { ... });
```

###  10. 商户消费者系统
该功能支持商户上传自定义的消费者Id，控制台会对消费者行为进行分析  
在初始化`BeeCloud.setAppIdAndSecret`之后，`new BCMerchantBuyerService()`，调用以下函数，所有函数都是同步返回结果：  

* 单个消费者注册接口调用 `addMerchantBuyer`  
* 消费者批量导入接口调用 `batchAddMerchantBuyers`  
* 商家消费者批量查询接口调用 `getMerchantBuyers`  


###  11. 营销卡券系统
提供商家营销功能，商家通过接口给顾客发券，顾客付款时自动抵扣  

在初始化`BeeCloud.setAppIdAndSecret`之后，`new BCSalesService()`，调用以下函数，所有函数都是同步返回结果：  

* 创建卡券模板，即定义满减规则等，在控制台操作
* 根据Id查询卡券模板 `queryCouponTemplate`
* 根据条件查询卡券模板 `queryCouponTemplates`
* 根据Id查询卡券 `queryCoupon`
* 根据条件查询卡券 `queryCoupons`
* 发放卡券 `createCoupon`    


  
## Demo
考虑到个人的开发习惯，本项目提供了`Android Studio`和`Eclipse ADT`两种工程的`demo`，为了使demo顺利运行，请注意以下细节
>1. 对于使用`Android Studio`的开发人员，下载源码后可以将`demo_eclipse`移除，`Import Project`的时候选择`beecloud-android`，`sdk`为`demo`的依赖`model`，`gradle`会自动关联。
>2. 对于使用`Eclipse ADT`的开发人员，`Import Project`的时候选择`beecloud-android`下的`demo_eclipse`，该`demo`下面已经添加所有需要的`jar`。

## ProGuard
请根据自己引进的jar做增删  

```
# 第三方库的申明，注意在Android Studio中不需要 

# BeeCloud及依赖jar  
-libraryjars libs/beecloud-x.x.x.jar  
-libraryjars libs/gson-2.4.jar  
-libraryjars libs/zxing-3.2.0.jar  

# 支付宝  
-libraryjars libs/alipaySdk-xxx.jar  

# 微信  
-libraryjars libs/libammsdk.jar  

# 银联  
-libraryjars libs/UPPayAssistEx.jar  
-libraryjars libs/UPPayPluginExPro.jar  

# 百度  
-libraryjars libs/Cashier_SDK-v4.2.0.jar  


# 以下是Android Studio和Eclipse都必须的

# BeeCloud  
-dontwarn cn.beecloud.**  
-dontwarn com.alipay.**  
-dontwarn com.baidu.**  
-dontwarn com.tencent.**  

# 保留类签名声明  
-keepattributes Signature  

# BeeCloud  
-keep class cn.beecloud.** { *; }  
-keep class com.google.** { *; }  

# 支付宝  
-keep class com.alipay.** { *; }   

# 微信  
-keep class com.tencent.** { *; }  

# 银联  
-keep class com.unionpay.** { *; }  

# 百度  
-keep class com.baidu.** { *; }  
-keep class com.dianxinos.** { *; }  

-dontwarn okhttp3.**  
-dontwarn okio.**  

-keep interface okhttp3.** { *; }  
-keep interface okio.** { *; }  

-keep class okhttp3.** { *; }  
-keep class okio.** { *; }  
```


## 常见问题
请查看[帮助中心](http://help.beecloud.cn/hc/)
