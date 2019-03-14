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
    unsafe class Casinos_MbAsyncLoadAssets_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(Casinos.MbAsyncLoadAssets);
            args = new Type[]{typeof(System.String), typeof(System.Action<System.String>)};
            method = type.GetMethod("SendUrl", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SendUrl_0);
            args = new Type[]{typeof(System.String), typeof(System.String), typeof(System.Action<System.Int32, System.String>)};
            method = type.GetMethod("PostUrl", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, PostUrl_1);
            args = new Type[]{typeof(System.String), typeof(UnityEngine.WWWForm), typeof(System.Action<System.Int32, System.String>)};
            method = type.GetMethod("PostUrlWithFormData", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, PostUrlWithFormData_2);
            args = new Type[]{typeof(System.String), typeof(System.Action<UnityEngine.AssetBundle>)};
            method = type.GetMethod("LocalLoadAssetBundleAsync", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, LocalLoadAssetBundleAsync_3);
            args = new Type[]{typeof(System.String), typeof(System.String), typeof(System.Action<UnityEngine.Texture>)};
            method = type.GetMethod("LocalLoadTextureFromAbAsync", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, LocalLoadTextureFromAbAsync_4);
            args = new Type[]{typeof(System.String), typeof(System.Action<System.String>)};
            method = type.GetMethod("WWWLoadTextAsync", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, WWWLoadTextAsync_5);
            args = new Type[]{typeof(System.Collections.Generic.List<System.String>), typeof(System.Action<System.Collections.Generic.List<System.String>>)};
            method = type.GetMethod("WWWLoadTextListAsync", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, WWWLoadTextListAsync_6);
            args = new Type[]{typeof(System.String), typeof(System.Action<UnityEngine.Texture>)};
            method = type.GetMethod("WWWLoadTextureAsync", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, WWWLoadTextureAsync_7);
            args = new Type[]{typeof(System.String), typeof(System.Action<UnityEngine.AssetBundle>)};
            method = type.GetMethod("WWWLoadAssetBundleAsync", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, WWWLoadAssetBundleAsync_8);



            app.RegisterCLRCreateDefaultInstance(type, () => new Casinos.MbAsyncLoadAssets());
            app.RegisterCLRCreateArrayInstance(type, s => new Casinos.MbAsyncLoadAssets[s]);

            args = new Type[]{};
            method = type.GetConstructor(flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Ctor_0);

        }


        static StackObject* SendUrl_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action<System.String> @cb = (System.Action<System.String>)typeof(System.Action<System.String>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.String @url = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            Casinos.MbAsyncLoadAssets instance_of_this_method = (Casinos.MbAsyncLoadAssets)typeof(Casinos.MbAsyncLoadAssets).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SendUrl(@url, @cb);

            return __ret;
        }

        static StackObject* PostUrl_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 4);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action<System.Int32, System.String> @cb = (System.Action<System.Int32, System.String>)typeof(System.Action<System.Int32, System.String>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.String @post_data = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            System.String @url = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 4);
            Casinos.MbAsyncLoadAssets instance_of_this_method = (Casinos.MbAsyncLoadAssets)typeof(Casinos.MbAsyncLoadAssets).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.PostUrl(@url, @post_data, @cb);

            return __ret;
        }

        static StackObject* PostUrlWithFormData_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 4);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action<System.Int32, System.String> @cb = (System.Action<System.Int32, System.String>)typeof(System.Action<System.Int32, System.String>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            UnityEngine.WWWForm @form_data = (UnityEngine.WWWForm)typeof(UnityEngine.WWWForm).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            System.String @url = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 4);
            Casinos.MbAsyncLoadAssets instance_of_this_method = (Casinos.MbAsyncLoadAssets)typeof(Casinos.MbAsyncLoadAssets).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.PostUrlWithFormData(@url, @form_data, @cb);

            return __ret;
        }

        static StackObject* LocalLoadAssetBundleAsync_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action<UnityEngine.AssetBundle> @cb = (System.Action<UnityEngine.AssetBundle>)typeof(System.Action<UnityEngine.AssetBundle>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.String @url = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            Casinos.MbAsyncLoadAssets instance_of_this_method = (Casinos.MbAsyncLoadAssets)typeof(Casinos.MbAsyncLoadAssets).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.LocalLoadAssetBundleAsync(@url, @cb);

            return __ret;
        }

        static StackObject* LocalLoadTextureFromAbAsync_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 4);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action<UnityEngine.Texture> @cb = (System.Action<UnityEngine.Texture>)typeof(System.Action<UnityEngine.Texture>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.String @name = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            System.String @url = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 4);
            Casinos.MbAsyncLoadAssets instance_of_this_method = (Casinos.MbAsyncLoadAssets)typeof(Casinos.MbAsyncLoadAssets).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.LocalLoadTextureFromAbAsync(@url, @name, @cb);

            return __ret;
        }

        static StackObject* WWWLoadTextAsync_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action<System.String> @cb = (System.Action<System.String>)typeof(System.Action<System.String>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.String @url = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            Casinos.MbAsyncLoadAssets instance_of_this_method = (Casinos.MbAsyncLoadAssets)typeof(Casinos.MbAsyncLoadAssets).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.WWWLoadTextAsync(@url, @cb);

            return __ret;
        }

        static StackObject* WWWLoadTextListAsync_6(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action<System.Collections.Generic.List<System.String>> @cb = (System.Action<System.Collections.Generic.List<System.String>>)typeof(System.Action<System.Collections.Generic.List<System.String>>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Collections.Generic.List<System.String> @list_url = (System.Collections.Generic.List<System.String>)typeof(System.Collections.Generic.List<System.String>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            Casinos.MbAsyncLoadAssets instance_of_this_method = (Casinos.MbAsyncLoadAssets)typeof(Casinos.MbAsyncLoadAssets).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.WWWLoadTextListAsync(@list_url, @cb);

            return __ret;
        }

        static StackObject* WWWLoadTextureAsync_7(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action<UnityEngine.Texture> @cb = (System.Action<UnityEngine.Texture>)typeof(System.Action<UnityEngine.Texture>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.String @url = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            Casinos.MbAsyncLoadAssets instance_of_this_method = (Casinos.MbAsyncLoadAssets)typeof(Casinos.MbAsyncLoadAssets).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.WWWLoadTextureAsync(@url, @cb);

            return __ret;
        }

        static StackObject* WWWLoadAssetBundleAsync_8(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action<UnityEngine.AssetBundle> @cb = (System.Action<UnityEngine.AssetBundle>)typeof(System.Action<UnityEngine.AssetBundle>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.String @url = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            Casinos.MbAsyncLoadAssets instance_of_this_method = (Casinos.MbAsyncLoadAssets)typeof(Casinos.MbAsyncLoadAssets).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.WWWLoadAssetBundleAsync(@url, @cb);

            return __ret;
        }




        static StackObject* Ctor_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);

            var result_of_this_method = new Casinos.MbAsyncLoadAssets();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


    }
}
