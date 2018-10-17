
###下载包目录结构说明:###

>
> * bugly\_plugin_*.unitypackage － Plugin脚本
> * BuglySDK － Plugin依赖的原生SDK组件(iOS和Android)，可根据需要替换.unitypackage中默认的原生SDK组件
> * BuglyUnitySample - Unity工程示例，演示Plugin初始化及C#异常捕获

###注意事项:###
>
> 1. *.unitypackage已包含原生SDK组件，双击导入所有文件即可
> 2. 如果Unity项目导出的Xcode工程的C++ Standard Library配置为libstdc++，请从BuglySDK/iOS目录使用对应的Bugly.framework进行替换
> 3. 如果Unity项目导出的Android工程编译后无法捕获Native崩溃，请注释脚本中的初始化代码，在Android工程的入口Java类中调用原生接口初始化(Unity 5.x导出的Android工程的入口Java类为UnityPlayerActivity，Unity 4.x导出的Android工程的入口Java类为UnityPlayerNativeActivity)

