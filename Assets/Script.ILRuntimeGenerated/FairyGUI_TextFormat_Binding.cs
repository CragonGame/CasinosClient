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
    unsafe class FairyGUI_TextFormat_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(FairyGUI.TextFormat);
            args = new Type[]{typeof(System.UInt32)};
            method = type.GetMethod("SetColor", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetColor_0);
            args = new Type[]{typeof(FairyGUI.TextFormat)};
            method = type.GetMethod("EqualStyle", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, EqualStyle_1);
            args = new Type[]{typeof(FairyGUI.TextFormat)};
            method = type.GetMethod("CopyFrom", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, CopyFrom_2);

            field = type.GetField("size", flag);
            app.RegisterCLRFieldGetter(field, get_size_0);
            app.RegisterCLRFieldSetter(field, set_size_0);
            field = type.GetField("font", flag);
            app.RegisterCLRFieldGetter(field, get_font_1);
            app.RegisterCLRFieldSetter(field, set_font_1);
            field = type.GetField("color", flag);
            app.RegisterCLRFieldGetter(field, get_color_2);
            app.RegisterCLRFieldSetter(field, set_color_2);
            field = type.GetField("lineSpacing", flag);
            app.RegisterCLRFieldGetter(field, get_lineSpacing_3);
            app.RegisterCLRFieldSetter(field, set_lineSpacing_3);
            field = type.GetField("letterSpacing", flag);
            app.RegisterCLRFieldGetter(field, get_letterSpacing_4);
            app.RegisterCLRFieldSetter(field, set_letterSpacing_4);
            field = type.GetField("bold", flag);
            app.RegisterCLRFieldGetter(field, get_bold_5);
            app.RegisterCLRFieldSetter(field, set_bold_5);
            field = type.GetField("underline", flag);
            app.RegisterCLRFieldGetter(field, get_underline_6);
            app.RegisterCLRFieldSetter(field, set_underline_6);
            field = type.GetField("italic", flag);
            app.RegisterCLRFieldGetter(field, get_italic_7);
            app.RegisterCLRFieldSetter(field, set_italic_7);
            field = type.GetField("gradientColor", flag);
            app.RegisterCLRFieldGetter(field, get_gradientColor_8);
            app.RegisterCLRFieldSetter(field, set_gradientColor_8);
            field = type.GetField("align", flag);
            app.RegisterCLRFieldGetter(field, get_align_9);
            app.RegisterCLRFieldSetter(field, set_align_9);
            field = type.GetField("specialStyle", flag);
            app.RegisterCLRFieldGetter(field, get_specialStyle_10);
            app.RegisterCLRFieldSetter(field, set_specialStyle_10);


            app.RegisterCLRCreateDefaultInstance(type, () => new FairyGUI.TextFormat());
            app.RegisterCLRCreateArrayInstance(type, s => new FairyGUI.TextFormat[s]);

            args = new Type[]{};
            method = type.GetConstructor(flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Ctor_0);

        }


        static StackObject* SetColor_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.UInt32 @value = (uint)ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.TextFormat instance_of_this_method = (FairyGUI.TextFormat)typeof(FairyGUI.TextFormat).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetColor(@value);

            return __ret;
        }

        static StackObject* EqualStyle_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.TextFormat @aFormat = (FairyGUI.TextFormat)typeof(FairyGUI.TextFormat).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.TextFormat instance_of_this_method = (FairyGUI.TextFormat)typeof(FairyGUI.TextFormat).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.EqualStyle(@aFormat);

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }

        static StackObject* CopyFrom_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.TextFormat @source = (FairyGUI.TextFormat)typeof(FairyGUI.TextFormat).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.TextFormat instance_of_this_method = (FairyGUI.TextFormat)typeof(FairyGUI.TextFormat).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.CopyFrom(@source);

            return __ret;
        }


        static object get_size_0(ref object o)
        {
            return ((FairyGUI.TextFormat)o).size;
        }
        static void set_size_0(ref object o, object v)
        {
            ((FairyGUI.TextFormat)o).size = (System.Int32)v;
        }
        static object get_font_1(ref object o)
        {
            return ((FairyGUI.TextFormat)o).font;
        }
        static void set_font_1(ref object o, object v)
        {
            ((FairyGUI.TextFormat)o).font = (System.String)v;
        }
        static object get_color_2(ref object o)
        {
            return ((FairyGUI.TextFormat)o).color;
        }
        static void set_color_2(ref object o, object v)
        {
            ((FairyGUI.TextFormat)o).color = (UnityEngine.Color)v;
        }
        static object get_lineSpacing_3(ref object o)
        {
            return ((FairyGUI.TextFormat)o).lineSpacing;
        }
        static void set_lineSpacing_3(ref object o, object v)
        {
            ((FairyGUI.TextFormat)o).lineSpacing = (System.Int32)v;
        }
        static object get_letterSpacing_4(ref object o)
        {
            return ((FairyGUI.TextFormat)o).letterSpacing;
        }
        static void set_letterSpacing_4(ref object o, object v)
        {
            ((FairyGUI.TextFormat)o).letterSpacing = (System.Int32)v;
        }
        static object get_bold_5(ref object o)
        {
            return ((FairyGUI.TextFormat)o).bold;
        }
        static void set_bold_5(ref object o, object v)
        {
            ((FairyGUI.TextFormat)o).bold = (System.Boolean)v;
        }
        static object get_underline_6(ref object o)
        {
            return ((FairyGUI.TextFormat)o).underline;
        }
        static void set_underline_6(ref object o, object v)
        {
            ((FairyGUI.TextFormat)o).underline = (System.Boolean)v;
        }
        static object get_italic_7(ref object o)
        {
            return ((FairyGUI.TextFormat)o).italic;
        }
        static void set_italic_7(ref object o, object v)
        {
            ((FairyGUI.TextFormat)o).italic = (System.Boolean)v;
        }
        static object get_gradientColor_8(ref object o)
        {
            return ((FairyGUI.TextFormat)o).gradientColor;
        }
        static void set_gradientColor_8(ref object o, object v)
        {
            ((FairyGUI.TextFormat)o).gradientColor = (UnityEngine.Color32[])v;
        }
        static object get_align_9(ref object o)
        {
            return ((FairyGUI.TextFormat)o).align;
        }
        static void set_align_9(ref object o, object v)
        {
            ((FairyGUI.TextFormat)o).align = (FairyGUI.AlignType)v;
        }
        static object get_specialStyle_10(ref object o)
        {
            return ((FairyGUI.TextFormat)o).specialStyle;
        }
        static void set_specialStyle_10(ref object o, object v)
        {
            ((FairyGUI.TextFormat)o).specialStyle = (FairyGUI.TextFormat.SpecialStyle)v;
        }


        static StackObject* Ctor_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);

            var result_of_this_method = new FairyGUI.TextFormat();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


    }
}
