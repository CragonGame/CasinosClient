using System;
using System.Collections.Generic;
using System.Reflection;

namespace ILRuntime.Runtime.Generated
{
    class CLRBindings
    {
        /// <summary>
        /// Initialize the CLR binding, please invoke this AFTER CLR Redirection registration
        /// </summary>
        public static void Initialize(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            System_Int16_Binding.Register(app);
            System_UInt16_Binding.Register(app);
            System_Int32_Binding.Register(app);
            System_UInt32_Binding.Register(app);
            System_Single_Binding.Register(app);
            System_Double_Binding.Register(app);
            System_Int64_Binding.Register(app);
            System_UInt64_Binding.Register(app);
            System_Object_Binding.Register(app);
            System_String_Binding.Register(app);
            System_Array_Binding.Register(app);
            UnityEngine_Vector2_Binding.Register(app);
            UnityEngine_Vector3_Binding.Register(app);
            UnityEngine_Quaternion_Binding.Register(app);
            UnityEngine_GameObject_Binding.Register(app);
            UnityEngine_Object_Binding.Register(app);
            UnityEngine_Transform_Binding.Register(app);
            UnityEngine_RectTransform_Binding.Register(app);
            UnityEngine_Time_Binding.Register(app);
            UnityEngine_Debug_Binding.Register(app);
            MessagePack_Resolvers_StandardResolver_Binding.Register(app);
            MessagePack_Resolvers_AttributeFormatterResolver_Binding.Register(app);
            MessagePack_Resolvers_BuiltinResolver_Binding.Register(app);
            MessagePack_Resolvers_PrimitiveObjectResolver_Binding.Register(app);
            MessagePack_Formatters_BitArrayFormatter_Binding.Register(app);
            MessagePack_Formatters_BooleanArrayFormatter_Binding.Register(app);
            MessagePack_Formatters_BooleanFormatter_Binding.Register(app);
            MessagePack_Formatters_ByteArrayFormatter_Binding.Register(app);
            MessagePack_Formatters_ByteArraySegmentFormatter_Binding.Register(app);
            MessagePack_Formatters_ByteFormatter_Binding.Register(app);
            MessagePack_Formatters_CharArrayFormatter_Binding.Register(app);
            MessagePack_Formatters_CharFormatter_Binding.Register(app);
            MessagePack_Formatters_DateTimeArrayFormatter_Binding.Register(app);
            MessagePack_Formatters_DateTimeFormatter_Binding.Register(app);
            MessagePack_Formatters_DecimalFormatter_Binding.Register(app);
            MessagePack_Formatters_DoubleArrayFormatter_Binding.Register(app);
            MessagePack_Formatters_DoubleFormatter_Binding.Register(app);
            MessagePack_Formatters_Int16ArrayFormatter_Binding.Register(app);
            MessagePack_Formatters_Int16Formatter_Binding.Register(app);
            MessagePack_Formatters_Int32ArrayFormatter_Binding.Register(app);
            MessagePack_Formatters_Int32Formatter_Binding.Register(app);
            MessagePack_Formatters_Int64ArrayFormatter_Binding.Register(app);
            MessagePack_Formatters_Int64Formatter_Binding.Register(app);
            MessagePack_Formatters_NilFormatter_Binding.Register(app);
            MessagePack_Formatters_NullableBooleanFormatter_Binding.Register(app);
            MessagePack_Formatters_PrimitiveObjectFormatter_Binding.Register(app);
            MessagePack_Formatters_SByteArrayFormatter_Binding.Register(app);
            MessagePack_Formatters_SByteFormatter_Binding.Register(app);
            MessagePack_Formatters_SingleArrayFormatter_Binding.Register(app);
            MessagePack_Formatters_SingleFormatter_Binding.Register(app);
            MessagePack_Formatters_StringBuilderFormatter_Binding.Register(app);
            MessagePack_Formatters_TimeSpanFormatter_Binding.Register(app);
            MessagePack_Formatters_UInt16ArrayFormatter_Binding.Register(app);
            MessagePack_Formatters_UInt16Formatter_Binding.Register(app);
            MessagePack_Formatters_UInt32ArrayFormatter_Binding.Register(app);
            MessagePack_Formatters_UInt32Formatter_Binding.Register(app);
            MessagePack_Formatters_UInt64ArrayFormatter_Binding.Register(app);
            MessagePack_Formatters_UInt64Formatter_Binding.Register(app);
            MessagePack_Formatters_UriFormatter_Binding.Register(app);
            MessagePack_Formatters_VersionFormatter_Binding.Register(app);
            MessagePack_IFormatterResolver_Binding.Register(app);
            MessagePack_IgnoreMemberAttribute_Binding.Register(app);
            MessagePack_KeyAttribute_Binding.Register(app);
            MessagePack_MessagePackObjectAttribute_Binding.Register(app);
            MessagePack_MessagePackSerializer_Binding.Register(app);
            MsgPack_Binding.Register(app);
            System_Collections_Generic_List_1_ILTypeInstance_Binding.Register(app);
        }

        /// <summary>
        /// Release the CLR binding, please invoke this BEFORE ILRuntime Appdomain destroy
        /// </summary>
        public static void Shutdown(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
        }
    }
}
