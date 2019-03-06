using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class MessagePack_Resolvers_AttributeFormatterResolver_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(MessagePack.Resolvers.AttributeFormatterResolver);

            field = type.GetField("Instance", flag);
            app.RegisterCLRFieldGetter(field, get_Instance_0);
            app.RegisterCLRFieldSetter(field, set_Instance_0);


            app.RegisterCLRCreateArrayInstance(type, s => new MessagePack.Resolvers.AttributeFormatterResolver[s]);


        }



        static object get_Instance_0(ref object o)
        {
            return MessagePack.Resolvers.AttributeFormatterResolver.Instance;
        }
        static void set_Instance_0(ref object o, object v)
        {
            MessagePack.Resolvers.AttributeFormatterResolver.Instance = (MessagePack.IFormatterResolver)v;
        }



    }
}
