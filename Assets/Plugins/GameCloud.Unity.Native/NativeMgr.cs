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
            nativeOperate(operate_type.ToString());
        }

        //-------------------------------------------------------------------------
        #region DllImport
        [DllImport("__Internal")]
        private static extern void nativeOperate(string operate_type);
        #endregion
    }

    //-------------------------------------------------------------------------
    public enum NativeOperateType
    {
        Pay,
        Login,
    }
}