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
    unsafe class UnityEngine_ScreenCapture_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(UnityEngine.ScreenCapture);
            args = new Type[]{typeof(System.String)};
            method = type.GetMethod("CaptureScreenshot", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, CaptureScreenshot_0);
            args = new Type[]{typeof(System.String), typeof(System.Int32)};
            method = type.GetMethod("CaptureScreenshot", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, CaptureScreenshot_1);
            args = new Type[]{typeof(System.String), typeof(UnityEngine.ScreenCapture.StereoScreenCaptureMode)};
            method = type.GetMethod("CaptureScreenshot", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, CaptureScreenshot_2);
            args = new Type[]{};
            method = type.GetMethod("CaptureScreenshotAsTexture", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, CaptureScreenshotAsTexture_3);
            args = new Type[]{typeof(System.Int32)};
            method = type.GetMethod("CaptureScreenshotAsTexture", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, CaptureScreenshotAsTexture_4);
            args = new Type[]{typeof(UnityEngine.ScreenCapture.StereoScreenCaptureMode)};
            method = type.GetMethod("CaptureScreenshotAsTexture", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, CaptureScreenshotAsTexture_5);





        }


        static StackObject* CaptureScreenshot_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.String @filename = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            UnityEngine.ScreenCapture.CaptureScreenshot(@filename);

            return __ret;
        }

        static StackObject* CaptureScreenshot_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Int32 @superSize = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.String @filename = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            UnityEngine.ScreenCapture.CaptureScreenshot(@filename, @superSize);

            return __ret;
        }

        static StackObject* CaptureScreenshot_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.ScreenCapture.StereoScreenCaptureMode @stereoCaptureMode = (UnityEngine.ScreenCapture.StereoScreenCaptureMode)typeof(UnityEngine.ScreenCapture.StereoScreenCaptureMode).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.String @filename = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            UnityEngine.ScreenCapture.CaptureScreenshot(@filename, @stereoCaptureMode);

            return __ret;
        }

        static StackObject* CaptureScreenshotAsTexture_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = UnityEngine.ScreenCapture.CaptureScreenshotAsTexture();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* CaptureScreenshotAsTexture_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Int32 @superSize = ptr_of_this_method->Value;


            var result_of_this_method = UnityEngine.ScreenCapture.CaptureScreenshotAsTexture(@superSize);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* CaptureScreenshotAsTexture_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.ScreenCapture.StereoScreenCaptureMode @stereoCaptureMode = (UnityEngine.ScreenCapture.StereoScreenCaptureMode)typeof(UnityEngine.ScreenCapture.StereoScreenCaptureMode).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = UnityEngine.ScreenCapture.CaptureScreenshotAsTexture(@stereoCaptureMode);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }





    }
}
