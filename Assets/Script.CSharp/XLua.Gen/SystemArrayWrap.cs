#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class SystemArrayWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(System.Array);
			Utils.BeginObjectRegister(type, L, translator, 0, 10, 7, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CopyTo", _m_CopyTo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Clone", _m_Clone);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetLongLength", _m_GetLongLength);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetValue", _m_GetValue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetValue", _m_SetValue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetEnumerator", _m_GetEnumerator);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetLength", _m_GetLength);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetLowerBound", _m_GetLowerBound);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetUpperBound", _m_GetUpperBound);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Initialize", _m_Initialize);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "LongLength", _g_get_LongLength);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsFixedSize", _g_get_IsFixedSize);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsReadOnly", _g_get_IsReadOnly);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsSynchronized", _g_get_IsSynchronized);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SyncRoot", _g_get_SyncRoot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Length", _g_get_Length);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Rank", _g_get_Rank);
            
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 10, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateInstance", _m_CreateInstance_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "BinarySearch", _m_BinarySearch_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Copy", _m_Copy_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IndexOf", _m_IndexOf_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LastIndexOf", _m_LastIndexOf_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Reverse", _m_Reverse_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Sort", _m_Sort_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Clear", _m_Clear_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ConstrainedCopy", _m_ConstrainedCopy_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "System.Array does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateInstance_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Type>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    System.Type _elementType = (System.Type)translator.GetObject(L, 1, typeof(System.Type));
                    int _length = LuaAPI.xlua_tointeger(L, 2);
                    
                        System.Array gen_ret = System.Array.CreateInstance( _elementType, _length );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Type>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    System.Type _elementType = (System.Type)translator.GetObject(L, 1, typeof(System.Type));
                    int _length1 = LuaAPI.xlua_tointeger(L, 2);
                    int _length2 = LuaAPI.xlua_tointeger(L, 3);
                    
                        System.Array gen_ret = System.Array.CreateInstance( _elementType, _length1, _length2 );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<System.Type>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    System.Type _elementType = (System.Type)translator.GetObject(L, 1, typeof(System.Type));
                    int _length1 = LuaAPI.xlua_tointeger(L, 2);
                    int _length2 = LuaAPI.xlua_tointeger(L, 3);
                    int _length3 = LuaAPI.xlua_tointeger(L, 4);
                    
                        System.Array gen_ret = System.Array.CreateInstance( _elementType, _length1, _length2, _length3 );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count >= 1&& translator.Assignable<System.Type>(L, 1)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) || LuaAPI.lua_isint64(L, 2)))) 
                {
                    System.Type _elementType = (System.Type)translator.GetObject(L, 1, typeof(System.Type));
                    long[] _lengths = translator.GetParams<long>(L, 2);
                    
                        System.Array gen_ret = System.Array.CreateInstance( _elementType, _lengths );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count >= 1&& translator.Assignable<System.Type>(L, 1)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))) 
                {
                    System.Type _elementType = (System.Type)translator.GetObject(L, 1, typeof(System.Type));
                    int[] _lengths = translator.GetParams<int>(L, 2);
                    
                        System.Array gen_ret = System.Array.CreateInstance( _elementType, _lengths );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Type>(L, 1)&& translator.Assignable<int[]>(L, 2)&& translator.Assignable<int[]>(L, 3)) 
                {
                    System.Type _elementType = (System.Type)translator.GetObject(L, 1, typeof(System.Type));
                    int[] _lengths = (int[])translator.GetObject(L, 2, typeof(int[]));
                    int[] _lowerBounds = (int[])translator.GetObject(L, 3, typeof(int[]));
                    
                        System.Array gen_ret = System.Array.CreateInstance( _elementType, _lengths, _lowerBounds );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Array.CreateInstance!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CopyTo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Array gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<System.Array>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    System.Array _array = (System.Array)translator.GetObject(L, 2, typeof(System.Array));
                    int _index = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.CopyTo( _array, _index );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Array>(L, 2)&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) || LuaAPI.lua_isint64(L, 3))) 
                {
                    System.Array _array = (System.Array)translator.GetObject(L, 2, typeof(System.Array));
                    long _index = LuaAPI.lua_toint64(L, 3);
                    
                    gen_to_be_invoked.CopyTo( _array, _index );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Array.CopyTo!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Clone(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Array gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        object gen_ret = gen_to_be_invoked.Clone(  );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BinarySearch_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<object>(L, 2)) 
                {
                    System.Array _array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    object _value = translator.GetObject(L, 2, typeof(object));
                    
                        int gen_ret = System.Array.BinarySearch( _array, _value );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<System.Array>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<object>(L, 4)) 
                {
                    System.Array _array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    int _length = LuaAPI.xlua_tointeger(L, 3);
                    object _value = translator.GetObject(L, 4, typeof(object));
                    
                        int gen_ret = System.Array.BinarySearch( _array, _index, _length, _value );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<object>(L, 2)&& translator.Assignable<System.Collections.IComparer>(L, 3)) 
                {
                    System.Array _array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    object _value = translator.GetObject(L, 2, typeof(object));
                    System.Collections.IComparer _comparer = (System.Collections.IComparer)translator.GetObject(L, 3, typeof(System.Collections.IComparer));
                    
                        int gen_ret = System.Array.BinarySearch( _array, _value, _comparer );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& translator.Assignable<System.Array>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<object>(L, 4)&& translator.Assignable<System.Collections.IComparer>(L, 5)) 
                {
                    System.Array _array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    int _length = LuaAPI.xlua_tointeger(L, 3);
                    object _value = translator.GetObject(L, 4, typeof(object));
                    System.Collections.IComparer _comparer = (System.Collections.IComparer)translator.GetObject(L, 5, typeof(System.Collections.IComparer));
                    
                        int gen_ret = System.Array.BinarySearch( _array, _index, _length, _value, _comparer );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Array.BinarySearch!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Copy_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<System.Array>(L, 2)&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) || LuaAPI.lua_isint64(L, 3))) 
                {
                    System.Array _sourceArray = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    System.Array _destinationArray = (System.Array)translator.GetObject(L, 2, typeof(System.Array));
                    long _length = LuaAPI.lua_toint64(L, 3);
                    
                    System.Array.Copy( _sourceArray, _destinationArray, _length );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<System.Array>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    System.Array _sourceArray = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    System.Array _destinationArray = (System.Array)translator.GetObject(L, 2, typeof(System.Array));
                    int _length = LuaAPI.xlua_tointeger(L, 3);
                    
                    System.Array.Copy( _sourceArray, _destinationArray, _length );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& translator.Assignable<System.Array>(L, 1)&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) || LuaAPI.lua_isint64(L, 2))&& translator.Assignable<System.Array>(L, 3)&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) || LuaAPI.lua_isint64(L, 4))&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5) || LuaAPI.lua_isint64(L, 5))) 
                {
                    System.Array _sourceArray = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    long _sourceIndex = LuaAPI.lua_toint64(L, 2);
                    System.Array _destinationArray = (System.Array)translator.GetObject(L, 3, typeof(System.Array));
                    long _destinationIndex = LuaAPI.lua_toint64(L, 4);
                    long _length = LuaAPI.lua_toint64(L, 5);
                    
                    System.Array.Copy( _sourceArray, _sourceIndex, _destinationArray, _destinationIndex, _length );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& translator.Assignable<System.Array>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Array>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    System.Array _sourceArray = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    int _sourceIndex = LuaAPI.xlua_tointeger(L, 2);
                    System.Array _destinationArray = (System.Array)translator.GetObject(L, 3, typeof(System.Array));
                    int _destinationIndex = LuaAPI.xlua_tointeger(L, 4);
                    int _length = LuaAPI.xlua_tointeger(L, 5);
                    
                    System.Array.Copy( _sourceArray, _sourceIndex, _destinationArray, _destinationIndex, _length );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Array.Copy!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLongLength(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Array gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _dimension = LuaAPI.xlua_tointeger(L, 2);
                    
                        long gen_ret = gen_to_be_invoked.GetLongLength( _dimension );
                        LuaAPI.lua_pushint64(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetValue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Array gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) || LuaAPI.lua_isint64(L, 2))) 
                {
                    long _index = LuaAPI.lua_toint64(L, 2);
                    
                        object gen_ret = gen_to_be_invoked.GetValue( _index );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                        object gen_ret = gen_to_be_invoked.GetValue( _index );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) || LuaAPI.lua_isint64(L, 2))&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) || LuaAPI.lua_isint64(L, 3))) 
                {
                    long _index1 = LuaAPI.lua_toint64(L, 2);
                    long _index2 = LuaAPI.lua_toint64(L, 3);
                    
                        object gen_ret = gen_to_be_invoked.GetValue( _index1, _index2 );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int _index1 = LuaAPI.xlua_tointeger(L, 2);
                    int _index2 = LuaAPI.xlua_tointeger(L, 3);
                    
                        object gen_ret = gen_to_be_invoked.GetValue( _index1, _index2 );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) || LuaAPI.lua_isint64(L, 2))&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) || LuaAPI.lua_isint64(L, 3))&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) || LuaAPI.lua_isint64(L, 4))) 
                {
                    long _index1 = LuaAPI.lua_toint64(L, 2);
                    long _index2 = LuaAPI.lua_toint64(L, 3);
                    long _index3 = LuaAPI.lua_toint64(L, 4);
                    
                        object gen_ret = gen_to_be_invoked.GetValue( _index1, _index2, _index3 );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    int _index1 = LuaAPI.xlua_tointeger(L, 2);
                    int _index2 = LuaAPI.xlua_tointeger(L, 3);
                    int _index3 = LuaAPI.xlua_tointeger(L, 4);
                    
                        object gen_ret = gen_to_be_invoked.GetValue( _index1, _index2, _index3 );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count >= 1&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) || LuaAPI.lua_isint64(L, 2)))) 
                {
                    long[] _indices = translator.GetParams<long>(L, 2);
                    
                        object gen_ret = gen_to_be_invoked.GetValue( _indices );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count >= 1&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))) 
                {
                    int[] _indices = translator.GetParams<int>(L, 2);
                    
                        object gen_ret = gen_to_be_invoked.GetValue( _indices );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Array.GetValue!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IndexOf_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<object>(L, 2)) 
                {
                    System.Array _array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    object _value = translator.GetObject(L, 2, typeof(object));
                    
                        int gen_ret = System.Array.IndexOf( _array, _value );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<object>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    System.Array _array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    object _value = translator.GetObject(L, 2, typeof(object));
                    int _startIndex = LuaAPI.xlua_tointeger(L, 3);
                    
                        int gen_ret = System.Array.IndexOf( _array, _value, _startIndex );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<object>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    System.Array _array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    object _value = translator.GetObject(L, 2, typeof(object));
                    int _startIndex = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                        int gen_ret = System.Array.IndexOf( _array, _value, _startIndex, _count );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Array.IndexOf!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LastIndexOf_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<object>(L, 2)) 
                {
                    System.Array _array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    object _value = translator.GetObject(L, 2, typeof(object));
                    
                        int gen_ret = System.Array.LastIndexOf( _array, _value );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<object>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    System.Array _array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    object _value = translator.GetObject(L, 2, typeof(object));
                    int _startIndex = LuaAPI.xlua_tointeger(L, 3);
                    
                        int gen_ret = System.Array.LastIndexOf( _array, _value, _startIndex );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<object>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    System.Array _array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    object _value = translator.GetObject(L, 2, typeof(object));
                    int _startIndex = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                        int gen_ret = System.Array.LastIndexOf( _array, _value, _startIndex, _count );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Array.LastIndexOf!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Reverse_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<System.Array>(L, 1)) 
                {
                    System.Array _array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    
                    System.Array.Reverse( _array );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Array>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    System.Array _array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    int _length = LuaAPI.xlua_tointeger(L, 3);
                    
                    System.Array.Reverse( _array, _index, _length );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Array.Reverse!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetValue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Array gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<object>(L, 2)&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) || LuaAPI.lua_isint64(L, 3))) 
                {
                    object _value = translator.GetObject(L, 2, typeof(object));
                    long _index = LuaAPI.lua_toint64(L, 3);
                    
                    gen_to_be_invoked.SetValue( _value, _index );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<object>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    object _value = translator.GetObject(L, 2, typeof(object));
                    int _index = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.SetValue( _value, _index );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<object>(L, 2)&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) || LuaAPI.lua_isint64(L, 3))&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) || LuaAPI.lua_isint64(L, 4))) 
                {
                    object _value = translator.GetObject(L, 2, typeof(object));
                    long _index1 = LuaAPI.lua_toint64(L, 3);
                    long _index2 = LuaAPI.lua_toint64(L, 4);
                    
                    gen_to_be_invoked.SetValue( _value, _index1, _index2 );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<object>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    object _value = translator.GetObject(L, 2, typeof(object));
                    int _index1 = LuaAPI.xlua_tointeger(L, 3);
                    int _index2 = LuaAPI.xlua_tointeger(L, 4);
                    
                    gen_to_be_invoked.SetValue( _value, _index1, _index2 );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& translator.Assignable<object>(L, 2)&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) || LuaAPI.lua_isint64(L, 3))&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) || LuaAPI.lua_isint64(L, 4))&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5) || LuaAPI.lua_isint64(L, 5))) 
                {
                    object _value = translator.GetObject(L, 2, typeof(object));
                    long _index1 = LuaAPI.lua_toint64(L, 3);
                    long _index2 = LuaAPI.lua_toint64(L, 4);
                    long _index3 = LuaAPI.lua_toint64(L, 5);
                    
                    gen_to_be_invoked.SetValue( _value, _index1, _index2, _index3 );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& translator.Assignable<object>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    object _value = translator.GetObject(L, 2, typeof(object));
                    int _index1 = LuaAPI.xlua_tointeger(L, 3);
                    int _index2 = LuaAPI.xlua_tointeger(L, 4);
                    int _index3 = LuaAPI.xlua_tointeger(L, 5);
                    
                    gen_to_be_invoked.SetValue( _value, _index1, _index2, _index3 );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count >= 2&& translator.Assignable<object>(L, 2)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 3) || (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) || LuaAPI.lua_isint64(L, 3)))) 
                {
                    object _value = translator.GetObject(L, 2, typeof(object));
                    long[] _indices = translator.GetParams<long>(L, 3);
                    
                    gen_to_be_invoked.SetValue( _value, _indices );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count >= 2&& translator.Assignable<object>(L, 2)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 3) || LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3))) 
                {
                    object _value = translator.GetObject(L, 2, typeof(object));
                    int[] _indices = translator.GetParams<int>(L, 3);
                    
                    gen_to_be_invoked.SetValue( _value, _indices );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Array.SetValue!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Sort_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<System.Array>(L, 1)) 
                {
                    System.Array _array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    
                    System.Array.Sort( _array );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Array>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    System.Array _array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    int _length = LuaAPI.xlua_tointeger(L, 3);
                    
                    System.Array.Sort( _array, _index, _length );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<System.Collections.IComparer>(L, 2)) 
                {
                    System.Array _array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    System.Collections.IComparer _comparer = (System.Collections.IComparer)translator.GetObject(L, 2, typeof(System.Collections.IComparer));
                    
                    System.Array.Sort( _array, _comparer );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<System.Array>(L, 2)) 
                {
                    System.Array _keys = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    System.Array _items = (System.Array)translator.GetObject(L, 2, typeof(System.Array));
                    
                    System.Array.Sort( _keys, _items );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<System.Array>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Collections.IComparer>(L, 4)) 
                {
                    System.Array _array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    int _length = LuaAPI.xlua_tointeger(L, 3);
                    System.Collections.IComparer _comparer = (System.Collections.IComparer)translator.GetObject(L, 4, typeof(System.Collections.IComparer));
                    
                    System.Array.Sort( _array, _index, _length, _comparer );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<System.Array>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    System.Array _keys = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    System.Array _items = (System.Array)translator.GetObject(L, 2, typeof(System.Array));
                    int _index = LuaAPI.xlua_tointeger(L, 3);
                    int _length = LuaAPI.xlua_tointeger(L, 4);
                    
                    System.Array.Sort( _keys, _items, _index, _length );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<System.Array>(L, 2)&& translator.Assignable<System.Collections.IComparer>(L, 3)) 
                {
                    System.Array _keys = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    System.Array _items = (System.Array)translator.GetObject(L, 2, typeof(System.Array));
                    System.Collections.IComparer _comparer = (System.Collections.IComparer)translator.GetObject(L, 3, typeof(System.Collections.IComparer));
                    
                    System.Array.Sort( _keys, _items, _comparer );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& translator.Assignable<System.Array>(L, 1)&& translator.Assignable<System.Array>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.Collections.IComparer>(L, 5)) 
                {
                    System.Array _keys = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    System.Array _items = (System.Array)translator.GetObject(L, 2, typeof(System.Array));
                    int _index = LuaAPI.xlua_tointeger(L, 3);
                    int _length = LuaAPI.xlua_tointeger(L, 4);
                    System.Collections.IComparer _comparer = (System.Collections.IComparer)translator.GetObject(L, 5, typeof(System.Collections.IComparer));
                    
                    System.Array.Sort( _keys, _items, _index, _length, _comparer );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Array.Sort!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetEnumerator(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Array gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        System.Collections.IEnumerator gen_ret = gen_to_be_invoked.GetEnumerator(  );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLength(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Array gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _dimension = LuaAPI.xlua_tointeger(L, 2);
                    
                        int gen_ret = gen_to_be_invoked.GetLength( _dimension );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLowerBound(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Array gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _dimension = LuaAPI.xlua_tointeger(L, 2);
                    
                        int gen_ret = gen_to_be_invoked.GetLowerBound( _dimension );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetUpperBound(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Array gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _dimension = LuaAPI.xlua_tointeger(L, 2);
                    
                        int gen_ret = gen_to_be_invoked.GetUpperBound( _dimension );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Clear_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.Array _array = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    int _length = LuaAPI.xlua_tointeger(L, 3);
                    
                    System.Array.Clear( _array, _index, _length );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ConstrainedCopy_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.Array _sourceArray = (System.Array)translator.GetObject(L, 1, typeof(System.Array));
                    int _sourceIndex = LuaAPI.xlua_tointeger(L, 2);
                    System.Array _destinationArray = (System.Array)translator.GetObject(L, 3, typeof(System.Array));
                    int _destinationIndex = LuaAPI.xlua_tointeger(L, 4);
                    int _length = LuaAPI.xlua_tointeger(L, 5);
                    
                    System.Array.ConstrainedCopy( _sourceArray, _sourceIndex, _destinationArray, _destinationIndex, _length );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Initialize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Array gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Initialize(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LongLength(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Array gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushint64(L, gen_to_be_invoked.LongLength);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsFixedSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Array gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsFixedSize);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsReadOnly(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Array gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsReadOnly);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsSynchronized(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Array gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsSynchronized);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SyncRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Array gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.SyncRoot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Length(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Array gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.Length);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Rank(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Array gen_to_be_invoked = (System.Array)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.Rank);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
