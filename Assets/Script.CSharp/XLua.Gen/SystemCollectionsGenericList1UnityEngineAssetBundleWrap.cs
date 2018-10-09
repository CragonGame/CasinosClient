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
    public class SystemCollectionsGenericList_1_UnityEngineAssetBundle_Wrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(System.Collections.Generic.List<UnityEngine.AssetBundle>);
			Utils.BeginObjectRegister(type, L, translator, 0, 29, 2, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Add", _m_Add);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddRange", _m_AddRange);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AsReadOnly", _m_AsReadOnly);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BinarySearch", _m_BinarySearch);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Clear", _m_Clear);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Contains", _m_Contains);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CopyTo", _m_CopyTo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Exists", _m_Exists);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Find", _m_Find);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FindAll", _m_FindAll);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FindIndex", _m_FindIndex);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FindLast", _m_FindLast);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FindLastIndex", _m_FindLastIndex);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ForEach", _m_ForEach);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetEnumerator", _m_GetEnumerator);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetRange", _m_GetRange);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IndexOf", _m_IndexOf);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Insert", _m_Insert);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InsertRange", _m_InsertRange);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LastIndexOf", _m_LastIndexOf);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Remove", _m_Remove);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveAll", _m_RemoveAll);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveAt", _m_RemoveAt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveRange", _m_RemoveRange);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Reverse", _m_Reverse);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Sort", _m_Sort);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToArray", _m_ToArray);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TrimExcess", _m_TrimExcess);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TrueForAll", _m_TrueForAll);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Capacity", _g_get_Capacity);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Count", _g_get_Count);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "Capacity", _s_set_Capacity);
            
			
			Utils.EndObjectRegister(type, L, translator, __CSIndexer, __NewIndexer,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					System.Collections.Generic.List<UnityEngine.AssetBundle> gen_ret = new System.Collections.Generic.List<UnityEngine.AssetBundle>();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && translator.Assignable<System.Collections.Generic.IEnumerable<UnityEngine.AssetBundle>>(L, 2))
				{
					System.Collections.Generic.IEnumerable<UnityEngine.AssetBundle> _collection = (System.Collections.Generic.IEnumerable<UnityEngine.AssetBundle>)translator.GetObject(L, 2, typeof(System.Collections.Generic.IEnumerable<UnityEngine.AssetBundle>));
					
					System.Collections.Generic.List<UnityEngine.AssetBundle> gen_ret = new System.Collections.Generic.List<UnityEngine.AssetBundle>(_collection);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))
				{
					int _capacity = LuaAPI.xlua_tointeger(L, 2);
					
					System.Collections.Generic.List<UnityEngine.AssetBundle> gen_ret = new System.Collections.Generic.List<UnityEngine.AssetBundle>(_capacity);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to System.Collections.Generic.List<UnityEngine.AssetBundle> constructor!");
            
        }
        
		
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int __CSIndexer(RealStatePtr L)
        {
			try {
			    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				
				if (translator.Assignable<System.Collections.Generic.List<UnityEngine.AssetBundle>>(L, 1) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))
				{
					
					System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
					int index = LuaAPI.xlua_tointeger(L, 2);
					LuaAPI.lua_pushboolean(L, true);
					translator.Push(L, gen_to_be_invoked[index]);
					return 2;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
			
            LuaAPI.lua_pushboolean(L, false);
			return 1;
        }
		
        
		
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int __NewIndexer(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			try {
				
				if (translator.Assignable<System.Collections.Generic.List<UnityEngine.AssetBundle>>(L, 1) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) && translator.Assignable<UnityEngine.AssetBundle>(L, 3))
				{
					
					System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
					int key = LuaAPI.xlua_tointeger(L, 2);
					gen_to_be_invoked[key] = (UnityEngine.AssetBundle)translator.GetObject(L, 3, typeof(UnityEngine.AssetBundle));
					LuaAPI.lua_pushboolean(L, true);
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
			
			LuaAPI.lua_pushboolean(L, false);
            return 1;
        }
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Add(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.AssetBundle _item = (UnityEngine.AssetBundle)translator.GetObject(L, 2, typeof(UnityEngine.AssetBundle));
                    
                    gen_to_be_invoked.Add( _item );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddRange(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Generic.IEnumerable<UnityEngine.AssetBundle> _collection = (System.Collections.Generic.IEnumerable<UnityEngine.AssetBundle>)translator.GetObject(L, 2, typeof(System.Collections.Generic.IEnumerable<UnityEngine.AssetBundle>));
                    
                    gen_to_be_invoked.AddRange( _collection );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AsReadOnly(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.AssetBundle> gen_ret = gen_to_be_invoked.AsReadOnly(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BinarySearch(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.AssetBundle>(L, 2)) 
                {
                    UnityEngine.AssetBundle _item = (UnityEngine.AssetBundle)translator.GetObject(L, 2, typeof(UnityEngine.AssetBundle));
                    
                        int gen_ret = gen_to_be_invoked.BinarySearch( _item );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.AssetBundle>(L, 2)&& translator.Assignable<System.Collections.Generic.IComparer<UnityEngine.AssetBundle>>(L, 3)) 
                {
                    UnityEngine.AssetBundle _item = (UnityEngine.AssetBundle)translator.GetObject(L, 2, typeof(UnityEngine.AssetBundle));
                    System.Collections.Generic.IComparer<UnityEngine.AssetBundle> _comparer = (System.Collections.Generic.IComparer<UnityEngine.AssetBundle>)translator.GetObject(L, 3, typeof(System.Collections.Generic.IComparer<UnityEngine.AssetBundle>));
                    
                        int gen_ret = gen_to_be_invoked.BinarySearch( _item, _comparer );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<UnityEngine.AssetBundle>(L, 4)&& translator.Assignable<System.Collections.Generic.IComparer<UnityEngine.AssetBundle>>(L, 5)) 
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    int _count = LuaAPI.xlua_tointeger(L, 3);
                    UnityEngine.AssetBundle _item = (UnityEngine.AssetBundle)translator.GetObject(L, 4, typeof(UnityEngine.AssetBundle));
                    System.Collections.Generic.IComparer<UnityEngine.AssetBundle> _comparer = (System.Collections.Generic.IComparer<UnityEngine.AssetBundle>)translator.GetObject(L, 5, typeof(System.Collections.Generic.IComparer<UnityEngine.AssetBundle>));
                    
                        int gen_ret = gen_to_be_invoked.BinarySearch( _index, _count, _item, _comparer );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Collections.Generic.List<UnityEngine.AssetBundle>.BinarySearch!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Clear(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Clear(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Contains(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.AssetBundle _item = (UnityEngine.AssetBundle)translator.GetObject(L, 2, typeof(UnityEngine.AssetBundle));
                    
                        bool gen_ret = gen_to_be_invoked.Contains( _item );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CopyTo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.AssetBundle[]>(L, 2)) 
                {
                    UnityEngine.AssetBundle[] _array = (UnityEngine.AssetBundle[])translator.GetObject(L, 2, typeof(UnityEngine.AssetBundle[]));
                    
                    gen_to_be_invoked.CopyTo( _array );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.AssetBundle[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.AssetBundle[] _array = (UnityEngine.AssetBundle[])translator.GetObject(L, 2, typeof(UnityEngine.AssetBundle[]));
                    int _arrayIndex = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.CopyTo( _array, _arrayIndex );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.AssetBundle[]>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    UnityEngine.AssetBundle[] _array = (UnityEngine.AssetBundle[])translator.GetObject(L, 3, typeof(UnityEngine.AssetBundle[]));
                    int _arrayIndex = LuaAPI.xlua_tointeger(L, 4);
                    int _count = LuaAPI.xlua_tointeger(L, 5);
                    
                    gen_to_be_invoked.CopyTo( _index, _array, _arrayIndex, _count );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Collections.Generic.List<UnityEngine.AssetBundle>.CopyTo!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Exists(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Predicate<UnityEngine.AssetBundle> _match = translator.GetDelegate<System.Predicate<UnityEngine.AssetBundle>>(L, 2);
                    
                        bool gen_ret = gen_to_be_invoked.Exists( _match );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Find(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Predicate<UnityEngine.AssetBundle> _match = translator.GetDelegate<System.Predicate<UnityEngine.AssetBundle>>(L, 2);
                    
                        UnityEngine.AssetBundle gen_ret = gen_to_be_invoked.Find( _match );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindAll(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Predicate<UnityEngine.AssetBundle> _match = translator.GetDelegate<System.Predicate<UnityEngine.AssetBundle>>(L, 2);
                    
                        System.Collections.Generic.List<UnityEngine.AssetBundle> gen_ret = gen_to_be_invoked.FindAll( _match );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindIndex(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Predicate<UnityEngine.AssetBundle>>(L, 2)) 
                {
                    System.Predicate<UnityEngine.AssetBundle> _match = translator.GetDelegate<System.Predicate<UnityEngine.AssetBundle>>(L, 2);
                    
                        int gen_ret = gen_to_be_invoked.FindIndex( _match );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Predicate<UnityEngine.AssetBundle>>(L, 3)) 
                {
                    int _startIndex = LuaAPI.xlua_tointeger(L, 2);
                    System.Predicate<UnityEngine.AssetBundle> _match = translator.GetDelegate<System.Predicate<UnityEngine.AssetBundle>>(L, 3);
                    
                        int gen_ret = gen_to_be_invoked.FindIndex( _startIndex, _match );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Predicate<UnityEngine.AssetBundle>>(L, 4)) 
                {
                    int _startIndex = LuaAPI.xlua_tointeger(L, 2);
                    int _count = LuaAPI.xlua_tointeger(L, 3);
                    System.Predicate<UnityEngine.AssetBundle> _match = translator.GetDelegate<System.Predicate<UnityEngine.AssetBundle>>(L, 4);
                    
                        int gen_ret = gen_to_be_invoked.FindIndex( _startIndex, _count, _match );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Collections.Generic.List<UnityEngine.AssetBundle>.FindIndex!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindLast(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Predicate<UnityEngine.AssetBundle> _match = translator.GetDelegate<System.Predicate<UnityEngine.AssetBundle>>(L, 2);
                    
                        UnityEngine.AssetBundle gen_ret = gen_to_be_invoked.FindLast( _match );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindLastIndex(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Predicate<UnityEngine.AssetBundle>>(L, 2)) 
                {
                    System.Predicate<UnityEngine.AssetBundle> _match = translator.GetDelegate<System.Predicate<UnityEngine.AssetBundle>>(L, 2);
                    
                        int gen_ret = gen_to_be_invoked.FindLastIndex( _match );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Predicate<UnityEngine.AssetBundle>>(L, 3)) 
                {
                    int _startIndex = LuaAPI.xlua_tointeger(L, 2);
                    System.Predicate<UnityEngine.AssetBundle> _match = translator.GetDelegate<System.Predicate<UnityEngine.AssetBundle>>(L, 3);
                    
                        int gen_ret = gen_to_be_invoked.FindLastIndex( _startIndex, _match );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Predicate<UnityEngine.AssetBundle>>(L, 4)) 
                {
                    int _startIndex = LuaAPI.xlua_tointeger(L, 2);
                    int _count = LuaAPI.xlua_tointeger(L, 3);
                    System.Predicate<UnityEngine.AssetBundle> _match = translator.GetDelegate<System.Predicate<UnityEngine.AssetBundle>>(L, 4);
                    
                        int gen_ret = gen_to_be_invoked.FindLastIndex( _startIndex, _count, _match );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Collections.Generic.List<UnityEngine.AssetBundle>.FindLastIndex!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ForEach(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Action<UnityEngine.AssetBundle> _action = translator.GetDelegate<System.Action<UnityEngine.AssetBundle>>(L, 2);
                    
                    gen_to_be_invoked.ForEach( _action );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetEnumerator(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        System.Collections.Generic.List<UnityEngine.AssetBundle>.Enumerator gen_ret = gen_to_be_invoked.GetEnumerator(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRange(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    int _count = LuaAPI.xlua_tointeger(L, 3);
                    
                        System.Collections.Generic.List<UnityEngine.AssetBundle> gen_ret = gen_to_be_invoked.GetRange( _index, _count );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IndexOf(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.AssetBundle>(L, 2)) 
                {
                    UnityEngine.AssetBundle _item = (UnityEngine.AssetBundle)translator.GetObject(L, 2, typeof(UnityEngine.AssetBundle));
                    
                        int gen_ret = gen_to_be_invoked.IndexOf( _item );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.AssetBundle>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.AssetBundle _item = (UnityEngine.AssetBundle)translator.GetObject(L, 2, typeof(UnityEngine.AssetBundle));
                    int _index = LuaAPI.xlua_tointeger(L, 3);
                    
                        int gen_ret = gen_to_be_invoked.IndexOf( _item, _index );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.AssetBundle>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.AssetBundle _item = (UnityEngine.AssetBundle)translator.GetObject(L, 2, typeof(UnityEngine.AssetBundle));
                    int _index = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                        int gen_ret = gen_to_be_invoked.IndexOf( _item, _index, _count );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Collections.Generic.List<UnityEngine.AssetBundle>.IndexOf!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Insert(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    UnityEngine.AssetBundle _item = (UnityEngine.AssetBundle)translator.GetObject(L, 3, typeof(UnityEngine.AssetBundle));
                    
                    gen_to_be_invoked.Insert( _index, _item );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InsertRange(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    System.Collections.Generic.IEnumerable<UnityEngine.AssetBundle> _collection = (System.Collections.Generic.IEnumerable<UnityEngine.AssetBundle>)translator.GetObject(L, 3, typeof(System.Collections.Generic.IEnumerable<UnityEngine.AssetBundle>));
                    
                    gen_to_be_invoked.InsertRange( _index, _collection );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LastIndexOf(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.AssetBundle>(L, 2)) 
                {
                    UnityEngine.AssetBundle _item = (UnityEngine.AssetBundle)translator.GetObject(L, 2, typeof(UnityEngine.AssetBundle));
                    
                        int gen_ret = gen_to_be_invoked.LastIndexOf( _item );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.AssetBundle>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.AssetBundle _item = (UnityEngine.AssetBundle)translator.GetObject(L, 2, typeof(UnityEngine.AssetBundle));
                    int _index = LuaAPI.xlua_tointeger(L, 3);
                    
                        int gen_ret = gen_to_be_invoked.LastIndexOf( _item, _index );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.AssetBundle>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.AssetBundle _item = (UnityEngine.AssetBundle)translator.GetObject(L, 2, typeof(UnityEngine.AssetBundle));
                    int _index = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                        int gen_ret = gen_to_be_invoked.LastIndexOf( _item, _index, _count );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Collections.Generic.List<UnityEngine.AssetBundle>.LastIndexOf!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Remove(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.AssetBundle _item = (UnityEngine.AssetBundle)translator.GetObject(L, 2, typeof(UnityEngine.AssetBundle));
                    
                        bool gen_ret = gen_to_be_invoked.Remove( _item );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveAll(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Predicate<UnityEngine.AssetBundle> _match = translator.GetDelegate<System.Predicate<UnityEngine.AssetBundle>>(L, 2);
                    
                        int gen_ret = gen_to_be_invoked.RemoveAll( _match );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveAt(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.RemoveAt( _index );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveRange(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    int _count = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.RemoveRange( _index, _count );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Reverse(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.Reverse(  );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    int _count = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.Reverse( _index, _count );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Collections.Generic.List<UnityEngine.AssetBundle>.Reverse!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Sort(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.Sort(  );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Collections.Generic.IComparer<UnityEngine.AssetBundle>>(L, 2)) 
                {
                    System.Collections.Generic.IComparer<UnityEngine.AssetBundle> _comparer = (System.Collections.Generic.IComparer<UnityEngine.AssetBundle>)translator.GetObject(L, 2, typeof(System.Collections.Generic.IComparer<UnityEngine.AssetBundle>));
                    
                    gen_to_be_invoked.Sort( _comparer );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Comparison<UnityEngine.AssetBundle>>(L, 2)) 
                {
                    System.Comparison<UnityEngine.AssetBundle> _comparison = translator.GetDelegate<System.Comparison<UnityEngine.AssetBundle>>(L, 2);
                    
                    gen_to_be_invoked.Sort( _comparison );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Collections.Generic.IComparer<UnityEngine.AssetBundle>>(L, 4)) 
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    int _count = LuaAPI.xlua_tointeger(L, 3);
                    System.Collections.Generic.IComparer<UnityEngine.AssetBundle> _comparer = (System.Collections.Generic.IComparer<UnityEngine.AssetBundle>)translator.GetObject(L, 4, typeof(System.Collections.Generic.IComparer<UnityEngine.AssetBundle>));
                    
                    gen_to_be_invoked.Sort( _index, _count, _comparer );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Collections.Generic.List<UnityEngine.AssetBundle>.Sort!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToArray(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        UnityEngine.AssetBundle[] gen_ret = gen_to_be_invoked.ToArray(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TrimExcess(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.TrimExcess(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TrueForAll(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Predicate<UnityEngine.AssetBundle> _match = translator.GetDelegate<System.Predicate<UnityEngine.AssetBundle>>(L, 2);
                    
                        bool gen_ret = gen_to_be_invoked.TrueForAll( _match );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Capacity(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.Capacity);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Count(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.Count);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Capacity(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Collections.Generic.List<UnityEngine.AssetBundle> gen_to_be_invoked = (System.Collections.Generic.List<UnityEngine.AssetBundle>)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Capacity = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
