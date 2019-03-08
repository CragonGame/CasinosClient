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
    unsafe class FairyGUI_TweenValue_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(FairyGUI.TweenValue);
            args = new Type[]{};
            method = type.GetMethod("get_vec2", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_vec2_0);
            args = new Type[]{typeof(UnityEngine.Vector2)};
            method = type.GetMethod("set_vec2", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_vec2_1);
            args = new Type[]{};
            method = type.GetMethod("get_vec3", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_vec3_2);
            args = new Type[]{typeof(UnityEngine.Vector3)};
            method = type.GetMethod("set_vec3", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_vec3_3);
            args = new Type[]{};
            method = type.GetMethod("get_vec4", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_vec4_4);
            args = new Type[]{typeof(UnityEngine.Vector4)};
            method = type.GetMethod("set_vec4", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_vec4_5);
            args = new Type[]{};
            method = type.GetMethod("get_color", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_color_6);
            args = new Type[]{typeof(UnityEngine.Color)};
            method = type.GetMethod("set_color", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_color_7);
            args = new Type[]{typeof(System.Int32)};
            method = type.GetMethod("get_Item", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_Item_8);
            args = new Type[]{typeof(System.Int32), typeof(System.Single)};
            method = type.GetMethod("set_Item", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_Item_9);
            args = new Type[]{};
            method = type.GetMethod("SetZero", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetZero_10);

            field = type.GetField("x", flag);
            app.RegisterCLRFieldGetter(field, get_x_0);
            app.RegisterCLRFieldSetter(field, set_x_0);
            field = type.GetField("y", flag);
            app.RegisterCLRFieldGetter(field, get_y_1);
            app.RegisterCLRFieldSetter(field, set_y_1);
            field = type.GetField("z", flag);
            app.RegisterCLRFieldGetter(field, get_z_2);
            app.RegisterCLRFieldSetter(field, set_z_2);
            field = type.GetField("w", flag);
            app.RegisterCLRFieldGetter(field, get_w_3);
            app.RegisterCLRFieldSetter(field, set_w_3);
            field = type.GetField("d", flag);
            app.RegisterCLRFieldGetter(field, get_d_4);
            app.RegisterCLRFieldSetter(field, set_d_4);


            app.RegisterCLRCreateDefaultInstance(type, () => new FairyGUI.TweenValue());
            app.RegisterCLRCreateArrayInstance(type, s => new FairyGUI.TweenValue[s]);

            args = new Type[]{};
            method = type.GetConstructor(flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Ctor_0);

        }


        static StackObject* get_vec2_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.TweenValue instance_of_this_method = (FairyGUI.TweenValue)typeof(FairyGUI.TweenValue).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.vec2;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_vec2_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Vector2 @value = (UnityEngine.Vector2)typeof(UnityEngine.Vector2).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.TweenValue instance_of_this_method = (FairyGUI.TweenValue)typeof(FairyGUI.TweenValue).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.vec2 = value;

            return __ret;
        }

        static StackObject* get_vec3_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.TweenValue instance_of_this_method = (FairyGUI.TweenValue)typeof(FairyGUI.TweenValue).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.vec3;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_vec3_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Vector3 @value = (UnityEngine.Vector3)typeof(UnityEngine.Vector3).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.TweenValue instance_of_this_method = (FairyGUI.TweenValue)typeof(FairyGUI.TweenValue).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.vec3 = value;

            return __ret;
        }

        static StackObject* get_vec4_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.TweenValue instance_of_this_method = (FairyGUI.TweenValue)typeof(FairyGUI.TweenValue).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.vec4;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_vec4_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Vector4 @value = (UnityEngine.Vector4)typeof(UnityEngine.Vector4).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.TweenValue instance_of_this_method = (FairyGUI.TweenValue)typeof(FairyGUI.TweenValue).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.vec4 = value;

            return __ret;
        }

        static StackObject* get_color_6(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.TweenValue instance_of_this_method = (FairyGUI.TweenValue)typeof(FairyGUI.TweenValue).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.color;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_color_7(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Color @value = (UnityEngine.Color)typeof(UnityEngine.Color).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.TweenValue instance_of_this_method = (FairyGUI.TweenValue)typeof(FairyGUI.TweenValue).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.color = value;

            return __ret;
        }

        static StackObject* get_Item_8(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Int32 @index = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.TweenValue instance_of_this_method = (FairyGUI.TweenValue)typeof(FairyGUI.TweenValue).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method[index];

            __ret->ObjectType = ObjectTypes.Float;
            *(float*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* set_Item_9(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Single @value = *(float*)&ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Int32 @index = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            FairyGUI.TweenValue instance_of_this_method = (FairyGUI.TweenValue)typeof(FairyGUI.TweenValue).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method[index] = value;

            return __ret;
        }

        static StackObject* SetZero_10(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.TweenValue instance_of_this_method = (FairyGUI.TweenValue)typeof(FairyGUI.TweenValue).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetZero();

            return __ret;
        }


        static object get_x_0(ref object o)
        {
            return ((FairyGUI.TweenValue)o).x;
        }
        static void set_x_0(ref object o, object v)
        {
            ((FairyGUI.TweenValue)o).x = (System.Single)v;
        }
        static object get_y_1(ref object o)
        {
            return ((FairyGUI.TweenValue)o).y;
        }
        static void set_y_1(ref object o, object v)
        {
            ((FairyGUI.TweenValue)o).y = (System.Single)v;
        }
        static object get_z_2(ref object o)
        {
            return ((FairyGUI.TweenValue)o).z;
        }
        static void set_z_2(ref object o, object v)
        {
            ((FairyGUI.TweenValue)o).z = (System.Single)v;
        }
        static object get_w_3(ref object o)
        {
            return ((FairyGUI.TweenValue)o).w;
        }
        static void set_w_3(ref object o, object v)
        {
            ((FairyGUI.TweenValue)o).w = (System.Single)v;
        }
        static object get_d_4(ref object o)
        {
            return ((FairyGUI.TweenValue)o).d;
        }
        static void set_d_4(ref object o, object v)
        {
            ((FairyGUI.TweenValue)o).d = (System.Double)v;
        }


        static StackObject* Ctor_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);

            var result_of_this_method = new FairyGUI.TweenValue();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


    }
}
