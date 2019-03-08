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
    unsafe class FairyGUI_GButton_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(FairyGUI.GButton);
            args = new Type[]{};
            method = type.GetMethod("get_pageOption", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_pageOption_0);
            args = new Type[]{};
            method = type.GetMethod("get_onChanged", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_onChanged_1);
            args = new Type[]{};
            method = type.GetMethod("get_icon", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_icon_2);
            args = new Type[]{typeof(System.String)};
            method = type.GetMethod("set_icon", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_icon_3);
            args = new Type[]{};
            method = type.GetMethod("get_title", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_title_4);
            args = new Type[]{typeof(System.String)};
            method = type.GetMethod("set_title", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_title_5);
            args = new Type[]{};
            method = type.GetMethod("get_text", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_text_6);
            args = new Type[]{typeof(System.String)};
            method = type.GetMethod("set_text", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_text_7);
            args = new Type[]{};
            method = type.GetMethod("get_selectedIcon", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_selectedIcon_8);
            args = new Type[]{typeof(System.String)};
            method = type.GetMethod("set_selectedIcon", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_selectedIcon_9);
            args = new Type[]{};
            method = type.GetMethod("get_selectedTitle", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_selectedTitle_10);
            args = new Type[]{typeof(System.String)};
            method = type.GetMethod("set_selectedTitle", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_selectedTitle_11);
            args = new Type[]{};
            method = type.GetMethod("get_titleColor", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_titleColor_12);
            args = new Type[]{typeof(UnityEngine.Color)};
            method = type.GetMethod("set_titleColor", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_titleColor_13);
            args = new Type[]{};
            method = type.GetMethod("get_color", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_color_14);
            args = new Type[]{typeof(UnityEngine.Color)};
            method = type.GetMethod("set_color", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_color_15);
            args = new Type[]{};
            method = type.GetMethod("get_titleFontSize", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_titleFontSize_16);
            args = new Type[]{typeof(System.Int32)};
            method = type.GetMethod("set_titleFontSize", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_titleFontSize_17);
            args = new Type[]{};
            method = type.GetMethod("get_selected", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_selected_18);
            args = new Type[]{typeof(System.Boolean)};
            method = type.GetMethod("set_selected", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_selected_19);
            args = new Type[]{};
            method = type.GetMethod("get_mode", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_mode_20);
            args = new Type[]{typeof(FairyGUI.ButtonMode)};
            method = type.GetMethod("set_mode", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_mode_21);
            args = new Type[]{};
            method = type.GetMethod("get_relatedController", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_relatedController_22);
            args = new Type[]{typeof(FairyGUI.Controller)};
            method = type.GetMethod("set_relatedController", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, set_relatedController_23);
            args = new Type[]{typeof(System.Boolean)};
            method = type.GetMethod("FireClick", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, FireClick_24);
            args = new Type[]{};
            method = type.GetMethod("GetTextField", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, GetTextField_25);
            args = new Type[]{typeof(FairyGUI.Controller)};
            method = type.GetMethod("HandleControllerChanged", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, HandleControllerChanged_26);
            args = new Type[]{typeof(FairyGUI.Utils.ByteBuffer), typeof(System.Int32)};
            method = type.GetMethod("Setup_AfterAdd", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Setup_AfterAdd_27);

            field = type.GetField("sound", flag);
            app.RegisterCLRFieldGetter(field, get_sound_0);
            app.RegisterCLRFieldSetter(field, set_sound_0);
            field = type.GetField("soundVolumeScale", flag);
            app.RegisterCLRFieldGetter(field, get_soundVolumeScale_1);
            app.RegisterCLRFieldSetter(field, set_soundVolumeScale_1);
            field = type.GetField("changeStateOnClick", flag);
            app.RegisterCLRFieldGetter(field, get_changeStateOnClick_2);
            app.RegisterCLRFieldSetter(field, set_changeStateOnClick_2);
            field = type.GetField("linkedPopup", flag);
            app.RegisterCLRFieldGetter(field, get_linkedPopup_3);
            app.RegisterCLRFieldSetter(field, set_linkedPopup_3);
            field = type.GetField("UP", flag);
            app.RegisterCLRFieldGetter(field, get_UP_4);
            field = type.GetField("DOWN", flag);
            app.RegisterCLRFieldGetter(field, get_DOWN_5);
            field = type.GetField("OVER", flag);
            app.RegisterCLRFieldGetter(field, get_OVER_6);
            field = type.GetField("SELECTED_OVER", flag);
            app.RegisterCLRFieldGetter(field, get_SELECTED_OVER_7);
            field = type.GetField("DISABLED", flag);
            app.RegisterCLRFieldGetter(field, get_DISABLED_8);
            field = type.GetField("SELECTED_DISABLED", flag);
            app.RegisterCLRFieldGetter(field, get_SELECTED_DISABLED_9);


            app.RegisterCLRCreateDefaultInstance(type, () => new FairyGUI.GButton());
            app.RegisterCLRCreateArrayInstance(type, s => new FairyGUI.GButton[s]);

            args = new Type[]{};
            method = type.GetConstructor(flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Ctor_0);

        }


        static StackObject* get_pageOption_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.pageOption;

            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_onChanged_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.onChanged;

            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* get_icon_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.icon;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_icon_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.String @value = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.icon = value;

            return __ret;
        }

        static StackObject* get_title_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.title;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_title_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.String @value = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.title = value;

            return __ret;
        }

        static StackObject* get_text_6(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.text;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_text_7(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.String @value = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.text = value;

            return __ret;
        }

        static StackObject* get_selectedIcon_8(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.selectedIcon;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_selectedIcon_9(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.String @value = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.selectedIcon = value;

            return __ret;
        }

        static StackObject* get_selectedTitle_10(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.selectedTitle;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_selectedTitle_11(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.String @value = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.selectedTitle = value;

            return __ret;
        }

        static StackObject* get_titleColor_12(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.titleColor;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_titleColor_13(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Color @value = (UnityEngine.Color)typeof(UnityEngine.Color).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.titleColor = value;

            return __ret;
        }

        static StackObject* get_color_14(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.color;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_color_15(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Color @value = (UnityEngine.Color)typeof(UnityEngine.Color).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.color = value;

            return __ret;
        }

        static StackObject* get_titleFontSize_16(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.titleFontSize;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* set_titleFontSize_17(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Int32 @value = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.titleFontSize = value;

            return __ret;
        }

        static StackObject* get_selected_18(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.selected;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }

        static StackObject* set_selected_19(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Boolean @value = ptr_of_this_method->Value == 1;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.selected = value;

            return __ret;
        }

        static StackObject* get_mode_20(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.mode;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_mode_21(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.ButtonMode @value = (FairyGUI.ButtonMode)typeof(FairyGUI.ButtonMode).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.mode = value;

            return __ret;
        }

        static StackObject* get_relatedController_22(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.relatedController;

            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* set_relatedController_23(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.Controller @value = (FairyGUI.Controller)typeof(FairyGUI.Controller).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.relatedController = value;

            return __ret;
        }

        static StackObject* FireClick_24(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Boolean @downEffect = ptr_of_this_method->Value == 1;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.FireClick(@downEffect);

            return __ret;
        }

        static StackObject* GetTextField_25(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.GetTextField();

            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* HandleControllerChanged_26(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            FairyGUI.Controller @c = (FairyGUI.Controller)typeof(FairyGUI.Controller).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.HandleControllerChanged(@c);

            return __ret;
        }

        static StackObject* Setup_AfterAdd_27(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Int32 @beginPos = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            FairyGUI.Utils.ByteBuffer @buffer = (FairyGUI.Utils.ByteBuffer)typeof(FairyGUI.Utils.ByteBuffer).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            FairyGUI.GButton instance_of_this_method = (FairyGUI.GButton)typeof(FairyGUI.GButton).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Setup_AfterAdd(@buffer, @beginPos);

            return __ret;
        }


        static object get_sound_0(ref object o)
        {
            return ((FairyGUI.GButton)o).sound;
        }
        static void set_sound_0(ref object o, object v)
        {
            ((FairyGUI.GButton)o).sound = (FairyGUI.NAudioClip)v;
        }
        static object get_soundVolumeScale_1(ref object o)
        {
            return ((FairyGUI.GButton)o).soundVolumeScale;
        }
        static void set_soundVolumeScale_1(ref object o, object v)
        {
            ((FairyGUI.GButton)o).soundVolumeScale = (System.Single)v;
        }
        static object get_changeStateOnClick_2(ref object o)
        {
            return ((FairyGUI.GButton)o).changeStateOnClick;
        }
        static void set_changeStateOnClick_2(ref object o, object v)
        {
            ((FairyGUI.GButton)o).changeStateOnClick = (System.Boolean)v;
        }
        static object get_linkedPopup_3(ref object o)
        {
            return ((FairyGUI.GButton)o).linkedPopup;
        }
        static void set_linkedPopup_3(ref object o, object v)
        {
            ((FairyGUI.GButton)o).linkedPopup = (FairyGUI.GObject)v;
        }
        static object get_UP_4(ref object o)
        {
            return FairyGUI.GButton.UP;
        }
        static object get_DOWN_5(ref object o)
        {
            return FairyGUI.GButton.DOWN;
        }
        static object get_OVER_6(ref object o)
        {
            return FairyGUI.GButton.OVER;
        }
        static object get_SELECTED_OVER_7(ref object o)
        {
            return FairyGUI.GButton.SELECTED_OVER;
        }
        static object get_DISABLED_8(ref object o)
        {
            return FairyGUI.GButton.DISABLED;
        }
        static object get_SELECTED_DISABLED_9(ref object o)
        {
            return FairyGUI.GButton.SELECTED_DISABLED;
        }


        static StackObject* Ctor_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);

            var result_of_this_method = new FairyGUI.GButton();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


    }
}
