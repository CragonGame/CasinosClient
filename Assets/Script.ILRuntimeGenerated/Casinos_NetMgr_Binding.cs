using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Linq;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class Casinos_NetMgr_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(Casinos.NetMgr);
            args = new Type[]{};
            method = type.GetMethod("get_OnRpcMethod", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_OnRpcMethod_0);
            args = new Type[]{typeof(System.Action<System.UInt16, System.Byte[]>)};
            method = type.GetMethod("set_OnRpcMethod", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_OnRpcMethod_1);
            args = new Type[]{};
            method = type.GetMethod("get_OnSocketConnected", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_OnSocketConnected_2);
            args = new Type[]{typeof(System.Action)};
            method = type.GetMethod("set_OnSocketConnected", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_OnSocketConnected_3);
            args = new Type[]{};
            method = type.GetMethod("get_OnSocketClosed", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_OnSocketClosed_4);
            args = new Type[]{typeof(System.Action)};
            method = type.GetMethod("set_OnSocketClosed", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_OnSocketClosed_5);
            args = new Type[]{};
            method = type.GetMethod("get_OnSocketError", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_OnSocketError_6);
            args = new Type[]{typeof(System.Action)};
            method = type.GetMethod("set_OnSocketError", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_OnSocketError_7);
            args = new Type[]{};
            method = type.GetMethod("Close", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Close_8);
            args = new Type[]{typeof(System.Single)};
            method = type.GetMethod("Update", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Update_9);
            args = new Type[]{typeof(System.String), typeof(System.Int32)};
            method = type.GetMethod("Connect", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Connect_10);
            args = new Type[]{};
            method = type.GetMethod("Disconnect", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Disconnect_11);
            args = new Type[]{typeof(System.UInt16), typeof(System.Byte[])};
            method = type.GetMethod("Send", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Send_12);



            app.RegisterCLRCreateDefaultInstance(type, () => new Casinos.NetMgr());
            app.RegisterCLRCreateArrayInstance(type, s => new Casinos.NetMgr[s]);

            args = new Type[]{};
            method = type.GetConstructor(flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Ctor_0);

        }


        static StackObject* get_OnRpcMethod_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            Casinos.NetMgr instance_of_this_method = (Casinos.NetMgr)typeof(Casinos.NetMgr).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.OnRpcMethod;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_OnRpcMethod_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action<System.UInt16, System.Byte[]> @value = (System.Action<System.UInt16, System.Byte[]>)typeof(System.Action<System.UInt16, System.Byte[]>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            Casinos.NetMgr instance_of_this_method = (Casinos.NetMgr)typeof(Casinos.NetMgr).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.OnRpcMethod = value;

            return __ret;
        }

        static StackObject* get_OnSocketConnected_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            Casinos.NetMgr instance_of_this_method = (Casinos.NetMgr)typeof(Casinos.NetMgr).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.OnSocketConnected;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_OnSocketConnected_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action @value = (System.Action)typeof(System.Action).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            Casinos.NetMgr instance_of_this_method = (Casinos.NetMgr)typeof(Casinos.NetMgr).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.OnSocketConnected = value;

            return __ret;
        }

        static StackObject* get_OnSocketClosed_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            Casinos.NetMgr instance_of_this_method = (Casinos.NetMgr)typeof(Casinos.NetMgr).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.OnSocketClosed;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_OnSocketClosed_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action @value = (System.Action)typeof(System.Action).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            Casinos.NetMgr instance_of_this_method = (Casinos.NetMgr)typeof(Casinos.NetMgr).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.OnSocketClosed = value;

            return __ret;
        }

        static StackObject* get_OnSocketError_6(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            Casinos.NetMgr instance_of_this_method = (Casinos.NetMgr)typeof(Casinos.NetMgr).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.OnSocketError;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_OnSocketError_7(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action @value = (System.Action)typeof(System.Action).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            Casinos.NetMgr instance_of_this_method = (Casinos.NetMgr)typeof(Casinos.NetMgr).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.OnSocketError = value;

            return __ret;
        }

        static StackObject* Close_8(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            Casinos.NetMgr instance_of_this_method = (Casinos.NetMgr)typeof(Casinos.NetMgr).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Close();

            return __ret;
        }

        static StackObject* Update_9(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Single @tm = *(float*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            Casinos.NetMgr instance_of_this_method = (Casinos.NetMgr)typeof(Casinos.NetMgr).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Update(@tm);

            return __ret;
        }

        static StackObject* Connect_10(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Int32 @port = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.String @ip = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            Casinos.NetMgr instance_of_this_method = (Casinos.NetMgr)typeof(Casinos.NetMgr).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Connect(@ip, @port);

            return __ret;
        }

        static StackObject* Disconnect_11(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            Casinos.NetMgr instance_of_this_method = (Casinos.NetMgr)typeof(Casinos.NetMgr).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Disconnect();

            return __ret;
        }

        static StackObject* Send_12(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Byte[] @data = (System.Byte[])typeof(System.Byte[]).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.UInt16 @method_id = (ushort)ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            Casinos.NetMgr instance_of_this_method = (Casinos.NetMgr)typeof(Casinos.NetMgr).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Send(@method_id, @data);

            return __ret;
        }




        static StackObject* Ctor_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);

            var result_of_this_method = new Casinos.NetMgr();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


    }
}
