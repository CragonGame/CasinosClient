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
    unsafe class System_Environment_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(System.Environment);
            args = new Type[]{};
            method = type.GetMethod("get_CommandLine", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_CommandLine_0);
            args = new Type[]{};
            method = type.GetMethod("get_CurrentDirectory", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_CurrentDirectory_1);
            args = new Type[]{typeof(System.String)};
            method = type.GetMethod("set_CurrentDirectory", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_CurrentDirectory_2);
            args = new Type[]{};
            method = type.GetMethod("get_CurrentManagedThreadId", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_CurrentManagedThreadId_3);
            args = new Type[]{};
            method = type.GetMethod("get_ExitCode", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_ExitCode_4);
            args = new Type[]{typeof(System.Int32)};
            method = type.GetMethod("set_ExitCode", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_ExitCode_5);
            args = new Type[]{};
            method = type.GetMethod("get_HasShutdownStarted", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_HasShutdownStarted_6);
            args = new Type[]{};
            method = type.GetMethod("get_MachineName", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_MachineName_7);
            args = new Type[]{};
            method = type.GetMethod("get_NewLine", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_NewLine_8);
            args = new Type[]{};
            method = type.GetMethod("get_OSVersion", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_OSVersion_9);
            args = new Type[]{};
            method = type.GetMethod("get_StackTrace", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_StackTrace_10);
            args = new Type[]{};
            method = type.GetMethod("get_SystemDirectory", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_SystemDirectory_11);
            args = new Type[]{};
            method = type.GetMethod("get_TickCount", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_TickCount_12);
            args = new Type[]{};
            method = type.GetMethod("get_UserDomainName", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_UserDomainName_13);
            args = new Type[]{};
            method = type.GetMethod("get_UserInteractive", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_UserInteractive_14);
            args = new Type[]{};
            method = type.GetMethod("get_UserName", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_UserName_15);
            args = new Type[]{};
            method = type.GetMethod("get_Version", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_Version_16);
            args = new Type[]{};
            method = type.GetMethod("get_WorkingSet", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_WorkingSet_17);
            args = new Type[]{typeof(System.Int32)};
            method = type.GetMethod("Exit", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Exit_18);
            args = new Type[]{typeof(System.String)};
            method = type.GetMethod("ExpandEnvironmentVariables", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, ExpandEnvironmentVariables_19);
            args = new Type[]{};
            method = type.GetMethod("GetCommandLineArgs", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, GetCommandLineArgs_20);
            args = new Type[]{typeof(System.String)};
            method = type.GetMethod("GetEnvironmentVariable", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, GetEnvironmentVariable_21);
            args = new Type[]{};
            method = type.GetMethod("GetEnvironmentVariables", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, GetEnvironmentVariables_22);
            args = new Type[]{typeof(System.Environment.SpecialFolder)};
            method = type.GetMethod("GetFolderPath", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, GetFolderPath_23);
            args = new Type[]{typeof(System.Environment.SpecialFolder), typeof(System.Environment.SpecialFolderOption)};
            method = type.GetMethod("GetFolderPath", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, GetFolderPath_24);
            args = new Type[]{};
            method = type.GetMethod("GetLogicalDrives", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, GetLogicalDrives_25);
            args = new Type[]{typeof(System.String), typeof(System.EnvironmentVariableTarget)};
            method = type.GetMethod("GetEnvironmentVariable", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, GetEnvironmentVariable_26);
            args = new Type[]{typeof(System.EnvironmentVariableTarget)};
            method = type.GetMethod("GetEnvironmentVariables", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, GetEnvironmentVariables_27);
            args = new Type[]{typeof(System.String), typeof(System.String)};
            method = type.GetMethod("SetEnvironmentVariable", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetEnvironmentVariable_28);
            args = new Type[]{typeof(System.String), typeof(System.String), typeof(System.EnvironmentVariableTarget)};
            method = type.GetMethod("SetEnvironmentVariable", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetEnvironmentVariable_29);
            args = new Type[]{typeof(System.String)};
            method = type.GetMethod("FailFast", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, FailFast_30);
            args = new Type[]{typeof(System.String), typeof(System.Exception)};
            method = type.GetMethod("FailFast", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, FailFast_31);
            args = new Type[]{};
            method = type.GetMethod("get_Is64BitOperatingSystem", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_Is64BitOperatingSystem_32);
            args = new Type[]{};
            method = type.GetMethod("get_SystemPageSize", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_SystemPageSize_33);
            args = new Type[]{};
            method = type.GetMethod("get_Is64BitProcess", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_Is64BitProcess_34);
            args = new Type[]{};
            method = type.GetMethod("get_ProcessorCount", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_ProcessorCount_35);





        }


        static StackObject* get_CommandLine_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.CommandLine;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_CurrentDirectory_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.CurrentDirectory;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_CurrentDirectory_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.String @value = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            System.Environment.CurrentDirectory = value;

            return __ret;
        }

        static StackObject* get_CurrentManagedThreadId_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.CurrentManagedThreadId;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* get_ExitCode_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.ExitCode;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* set_ExitCode_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Int32 @value = ptr_of_this_method->Value;


            System.Environment.ExitCode = value;

            return __ret;
        }

        static StackObject* get_HasShutdownStarted_6(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.HasShutdownStarted;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }

        static StackObject* get_MachineName_7(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.MachineName;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_NewLine_8(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.NewLine;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_OSVersion_9(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.OSVersion;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_StackTrace_10(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.StackTrace;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_SystemDirectory_11(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.SystemDirectory;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_TickCount_12(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.TickCount;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* get_UserDomainName_13(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.UserDomainName;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_UserInteractive_14(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.UserInteractive;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }

        static StackObject* get_UserName_15(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.UserName;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_Version_16(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.Version;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_WorkingSet_17(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.WorkingSet;

            __ret->ObjectType = ObjectTypes.Long;
            *(long*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* Exit_18(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Int32 @exitCode = ptr_of_this_method->Value;


            System.Environment.Exit(@exitCode);

            return __ret;
        }

        static StackObject* ExpandEnvironmentVariables_19(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.String @name = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = System.Environment.ExpandEnvironmentVariables(@name);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* GetCommandLineArgs_20(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.GetCommandLineArgs();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* GetEnvironmentVariable_21(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.String @variable = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = System.Environment.GetEnvironmentVariable(@variable);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* GetEnvironmentVariables_22(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.GetEnvironmentVariables();

            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* GetFolderPath_23(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Environment.SpecialFolder @folder = (System.Environment.SpecialFolder)typeof(System.Environment.SpecialFolder).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = System.Environment.GetFolderPath(@folder);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* GetFolderPath_24(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Environment.SpecialFolderOption @option = (System.Environment.SpecialFolderOption)typeof(System.Environment.SpecialFolderOption).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Environment.SpecialFolder @folder = (System.Environment.SpecialFolder)typeof(System.Environment.SpecialFolder).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = System.Environment.GetFolderPath(@folder, @option);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* GetLogicalDrives_25(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.GetLogicalDrives();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* GetEnvironmentVariable_26(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.EnvironmentVariableTarget @target = (System.EnvironmentVariableTarget)typeof(System.EnvironmentVariableTarget).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.String @variable = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = System.Environment.GetEnvironmentVariable(@variable, @target);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* GetEnvironmentVariables_27(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.EnvironmentVariableTarget @target = (System.EnvironmentVariableTarget)typeof(System.EnvironmentVariableTarget).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = System.Environment.GetEnvironmentVariables(@target);

            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* SetEnvironmentVariable_28(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.String @value = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.String @variable = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            System.Environment.SetEnvironmentVariable(@variable, @value);

            return __ret;
        }

        static StackObject* SetEnvironmentVariable_29(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.EnvironmentVariableTarget @target = (System.EnvironmentVariableTarget)typeof(System.EnvironmentVariableTarget).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.String @value = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            System.String @variable = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            System.Environment.SetEnvironmentVariable(@variable, @value, @target);

            return __ret;
        }

        static StackObject* FailFast_30(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.String @message = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            System.Environment.FailFast(@message);

            return __ret;
        }

        static StackObject* FailFast_31(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Exception @exception = (System.Exception)typeof(System.Exception).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.String @message = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            System.Environment.FailFast(@message, @exception);

            return __ret;
        }

        static StackObject* get_Is64BitOperatingSystem_32(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.Is64BitOperatingSystem;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }

        static StackObject* get_SystemPageSize_33(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.SystemPageSize;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* get_Is64BitProcess_34(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.Is64BitProcess;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }

        static StackObject* get_ProcessorCount_35(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = System.Environment.ProcessorCount;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }





    }
}
