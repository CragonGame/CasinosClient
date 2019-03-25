//using UnityEngine;
//using System.Collections.Generic;
//using ILRuntime.Other;
//using System;
//using System.Collections;
//using ILRuntime.Runtime.Enviorment;
//using ILRuntime.Runtime.Intepreter;
//using ILRuntime.CLR.Method;

//public class IComparableAdapter : CrossBindingAdaptor
//{
//    public override Type BaseCLRType
//    {
//        get
//        {
//            return typeof(IComparable<in T>);
//        }
//    }

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

//    public class Adaptor : IComparable<T>, CrossBindingAdaptorType
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

//        //IMethod mStartMethod;
//        //bool mStartMethodGot;
//        //int CompareTo(T other)
//        //{
//        //    if (!mStartMethodGot)
//        //    {
//        //        mStartMethod = instance.Type.GetMethod("CompareTo", 1);
//        //        mStartMethodGot = true;
//        //    }

//        //    if (mStartMethod != null)
//        //    {
//        //        appdomain.Invoke(mStartMethod, instance, null);
//        //    }
//        //}

//        public override string ToString()
//        {
//            IMethod m = appdomain.ObjectType.GetMethod("ToString", 0);
//            m = instance.Type.GetVirtualMethod(m);
//            if (m == null || m is ILMethod)
//            {
//                return instance.ToString();
//            }
//            else
//                return instance.Type.FullName;
//        }
//    }
//}
