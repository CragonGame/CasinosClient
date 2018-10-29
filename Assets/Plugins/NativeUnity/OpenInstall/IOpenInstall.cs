namespace io.openinstall.unity
{
    public abstract class IOpenInstall
    {
        // 常量
        public const string OPENINSTALL_OBJECT = "OpenInstall";
        public const string OPENINSTALL_WAKEUP_CALLBACK = "_wakeupCallback";
        public const string OPENINSTALL_INSTALL_CALLBACK = "_installCallback";

        // 获取安装数据
        public virtual void getInstall(int s) {

        }
        // 拉起回调
        public virtual void registerWakeupCallback()
        {

        }
        // 注册上报
        public virtual void reportRegister()
        {

        }
        // 效果点上报
        public virtual void reportEffectPoint(string pointID, long pointValue)
        {

        }

    }
}

