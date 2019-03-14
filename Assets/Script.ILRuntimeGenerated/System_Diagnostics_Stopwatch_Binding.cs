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
    unsafe class System_Diagnostics_Stopwatch_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(System.Diagnostics.Stopwatch);
            args = new Type[]{};
            method = type.GetMethod("GetTimestamp", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, GetTimestamp_0);
            args = new Type[]{};
            method = type.GetMethod("StartNew", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, StartNew_1);
            args = new Type[]{};
            method = type.GetMethod("get_Elapsed", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_Elapsed_2);
            args = new Type[]{};
            method = type.GetMethod("get_ElapsedMilliseconds", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_ElapsedMilliseconds_3);
            args = new Type[]{};
            method = type.GetMethod("get_ElapsedTicks", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_ElapsedTicks_4);
            args = new Type[]{};
            method = type.GetMethod("get_IsRunning", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_IsRunning_5);
            args = new Type[]{};
            method = type.GetMethod("Reset", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Reset_6);
            args = new Type[]{};
            method = type.GetMethod("Start", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Start_7);
            args = new Type[]{};
            method = type.GetMethod("Stop", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Stop_8);
            args = new Type[]{};
            method = type.GetMethod("Restart", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Restart_9);

            field = type.GetField("Frequency", flag);
            app.RegisterCLRFieldGetter(field, get_Frequency_0);
            field = type.GetField("IsHighResolution", flag);
            app.RegisterCLRFieldGetter(field, get_IsHighResolution_1);


            app.RegisterCLRCreateDefaultInstance(type, () => new System.Diagnostics.Stopwatch());
            app.RegisterCLRCreateArrayInstance(type, s => new System.Diagnostics.Stopwatch[s]);

            args = new Type[]{};
            method = type.GetConstructor(flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Ctor_0);

        }


        static StackObject* GetTimestamp_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Diagnostics.Stopwatch.GetTimestamp();

            __ret->ObjectType = ObjectTypes.Long;
            *(long*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* StartNew_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Diagnostics.Stopwatch.StartNew();

            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_Elapsed_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Diagnostics.Stopwatch instance_of_this_method = (System.Diagnostics.Stopwatch)typeof(System.Diagnostics.Stopwatch).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.Elapsed;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_ElapsedMilliseconds_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Diagnostics.Stopwatch instance_of_this_method = (System.Diagnostics.Stopwatch)typeof(System.Diagnostics.Stopwatch).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.ElapsedMilliseconds;

            __ret->ObjectType = ObjectTypes.Long;
            *(long*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* get_ElapsedTicks_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Diagnostics.Stopwatch instance_of_this_method = (System.Diagnostics.Stopwatch)typeof(System.Diagnostics.Stopwatch).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.ElapsedTicks;

            __ret->ObjectType = ObjectTypes.Long;
            *(long*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* get_IsRunning_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Diagnostics.Stopwatch instance_of_this_method = (System.Diagnostics.Stopwatch)typeof(System.Diagnostics.Stopwatch).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.IsRunning;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }

        static StackObject* Reset_6(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Diagnostics.Stopwatch instance_of_this_method = (System.Diagnostics.Stopwatch)typeof(System.Diagnostics.Stopwatch).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Reset();

            return __ret;
        }

        static StackObject* Start_7(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Diagnostics.Stopwatch instance_of_this_method = (System.Diagnostics.Stopwatch)typeof(System.Diagnostics.Stopwatch).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Start();

            return __ret;
        }

        static StackObject* Stop_8(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Diagnostics.Stopwatch instance_of_this_method = (System.Diagnostics.Stopwatch)typeof(System.Diagnostics.Stopwatch).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Stop();

            return __ret;
        }

        static StackObject* Restart_9(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Diagnostics.Stopwatch instance_of_this_method = (System.Diagnostics.Stopwatch)typeof(System.Diagnostics.Stopwatch).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Restart();

            return __ret;
        }


        static object get_Frequency_0(ref object o)
        {
            return System.Diagnostics.Stopwatch.Frequency;
        }
        static object get_IsHighResolution_1(ref object o)
        {
            return System.Diagnostics.Stopwatch.IsHighResolution;
        }


        static StackObject* Ctor_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);

            var result_of_this_method = new System.Diagnostics.Stopwatch();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


    }
}
