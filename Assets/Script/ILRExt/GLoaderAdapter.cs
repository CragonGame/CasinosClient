//using UnityEngine;
//using System.Collections.Generic;
//using ILRuntime.Other;
//using System;
//using System.Collections;
//using ILRuntime.Runtime.Enviorment;
//using ILRuntime.Runtime.Intepreter;
//using ILRuntime.CLR.Method;

//public class GLoaderAdapter : CrossBindingAdaptor
//{
//    public override Type BaseCLRType
//    {
//        get
//        {
//            return typeof(FairyGUI.GLoader);
//        }
//    }

//    //public override Type[] BaseCLRTypes
//    //{
//    //    get
//    //    {
//    //        //跨域继承只能有1个Adapter，因此应该尽量避免一个类同时实现多个外部接口，对于coroutine来说是IEnumerator<object>,IEnumerator和IDisposable，
//    //        //ILRuntime虽然支持，但是一定要小心这种用法，使用不当很容易造成不可预期的问题
//    //        //日常开发如果需要实现多个DLL外部接口，请在Unity这边先做一个基类实现那些个接口，然后继承那个基类
//    //        return new Type[] { typeof(FairyGUI.GLoader), typeof(FairyGUI.GObject) };
//    //    }
//    //}

//    public override Type AdaptorType
//    {
//        get
//        {
//            return typeof(Adaptor);
//        }
//    }

//    public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
//    {
//        return new Adaptor(appdomain, instance);
//    }

//    public class Adaptor : FairyGUI.GLoader, CrossBindingAdaptorType
//    {
//        ILTypeInstance instance;
//        ILRuntime.Runtime.Enviorment.AppDomain appdomain;

//        public Adaptor()
//        {
//        }

//        public Adaptor(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
//        {
//            this.appdomain = appdomain;
//            this.instance = instance;
//        }

//        public ILTypeInstance ILInstance { get { return instance; } set { instance = value; } }

//        public ILRuntime.Runtime.Enviorment.AppDomain AppDomain { get { return appdomain; } set { appdomain = value; } }

//        IMethod mLoadExternalMethod;
//        bool mLoadExternalMethodGot;
//        override protected void LoadExternal()
//        {
//            if (!mLoadExternalMethodGot)
//            {
//                mLoadExternalMethod = instance.Type.GetMethod("LoadExternal", 0);
//                mLoadExternalMethodGot = true;
//            }

//            if (mLoadExternalMethod != null)
//            {
//                appdomain.Invoke(mLoadExternalMethod, instance, null);
//            }
//        }

//        public override string ToString()
//        {
//            IMethod m = appdomain.ObjectType.GetMethod("ToString", 0);
//            m = instance.Type.GetVirtualMethod(m);
//            if (m == null || m is ILMethod)
//            {
//                return instance.ToString();
//            }
//            else
//            {
//                return instance.Type.FullName;
//            }
//        }
//    }
//}
