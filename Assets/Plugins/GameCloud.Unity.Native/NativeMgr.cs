// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class NativeMgr
    {
        //-------------------------------------------------------------------------
        public void nativeOperate(NativeOperateType operate_type)
        {
#if UNITY_IOS
            nativeOperate(operate_type.ToString());
#endif
        }

#if UNITY_IOS
        //-------------------------------------------------------------------------
        #region DllImport
        [DllImport("__Internal")]
        private static extern void nativeOperate(string operate_type);
        #endregion
#endif
    }

    //-------------------------------------------------------------------------
    public enum NativeOperateType
    {
        Pay,
        Login,
    }
}