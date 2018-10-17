Pod::Spec.new do |s|

	s.name         = 'BeeCloud'
	s.version      = '3.6.3'
	s.summary      = 'BeeCloud 让支付更简单'
	s.homepage     = 'http://beecloud.cn'
	s.license      = 'MIT'
	s.author       = { 'LacusRInz' => 'zhihaoq@beecloud.cn' }
	s.platform     = :ios, '7.0'
	s.source       = { :git => 'https://github.com/beecloud/beecloud-ios.git', :tag => 'v3.6.3'}
	s.requires_arc = true
	s.default_subspecs = "Core", "Alipay", "Wx", "UnionPay"
	
	s.subspec 'Core' do |core|
		core.source_files = 'BCPaySDK/BeeCloud/**/*.{h,m}'
		core.requires_arc = true
		core.ios.library = 'c++', 'stdc++', 'z'
		core.frameworks = 'CFNetwork', 'SystemConfiguration', 'Security'
		core.resource = 'BCPaySDK/BeeCloud/SandBox/*.bundle'
		core.xcconfig = { 'OTHER_LDFLAGS' => '-ObjC' }
	end

	s.subspec 'Alipay' do |alipay|
		alipay.frameworks = 'CoreMotion' , 'CoreTelephony'
		alipay.vendored_frameworks = 'BCPaySDK/Channel/AliPay/AlipaySDK.framework'
		alipay.source_files = 'BCPaySDK/Channel/AliPay/AliPayAdapter/*.{h,m}', 'BCPaySDK/Channel/AliPay/*.h'
		alipay.dependency 'BeeCloud/Core'
	end

	s.subspec 'Wx' do |wx|
		wx.frameworks = 'CoreTelephony'
		wx.vendored_libraries = 'BCPaySDK/Channel/WXPay/libWeChatSDK.a'
		wx.source_files = 'BCPaySDK/Channel/WXPay/WXPayAdapter/*.{h,m}', 'BCPaySDK/Channel/WXPay/*.h'
		wx.ios.library = 'sqlite3'		
		wx.dependency 'BeeCloud/Core'
	end

	s.subspec 'UnionPay' do |unionpay|
	    unionpay.frameworks = 'QuartzCore'
		unionpay.vendored_libraries = 'BCPaySDK/Channel/UnionPay/libPaymentControl.a'
		unionpay.source_files = 'BCPaySDK/Channel/UnionPay/UnionPayAdapter/*.{h,m}', 'BCPaySDK/Channel/UnionPay/*.h'
		unionpay.dependency 'BeeCloud/Core'
	end

	s.subspec 'ApplePay' do |apple|
	    apple.frameworks = 'QuartzCore','PassKit'
		apple.vendored_libraries = 'BCPaySDK/Channel/ApplePay/libUPAPayPlugin.a'
		apple.source_files = 'BCPaySDK/Channel/ApplePay/ApplePayAdapter/*.{h,m,mm}', 'BCPaySDK/Channel/ApplePay/*.h'
		apple.dependency 'BeeCloud/Core'
	end

	s.subspec 'Offline' do |offline|
		offline.source_files = 'BCPaySDK/Channel/OfflinePay/**/*.{h,m}'
		offline.requires_arc = true
		offline.dependency 'BeeCloud/Core'
	end

	s.subspec 'Baidu' do |baidu|
    baidu.frameworks = 'CoreTelephony', 'AddressBook', 'AddressBookUI', 'AudioToolbox', 'CoreAudio', 'CoreGraphics', 'ImageIO', 'MapKit', 'MessageUI', 'MobileCoreServices', 'QuartzCore'
    baidu.source_files = 'BCPaySDK/Channel/Baidu/Dependency/**/*.{h,m}', 'BCPaySDK/Channel/Baidu/BaiduAdapter/*.{h,m}','BCPaySDK/Channel/Baidu/BDWalletSDK/*.{h,m}'
    baidu.resource = 'BCPaySDK/Channel/Baidu/**/*.bundle'
    baidu.vendored_libraries = 'BCPaySDK/Channel/Baidu/**/*.a'
    baidu.dependency 'BeeCloud/Core'
    end
	
end
