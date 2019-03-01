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
    public class SystemTextRegularExpressionsRegexWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(System.Text.RegularExpressions.Regex);
			Utils.BeginObjectRegister(type, L, translator, 0, 10, 3, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToString", _m_ToString);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetGroupNames", _m_GetGroupNames);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetGroupNumbers", _m_GetGroupNumbers);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GroupNameFromNumber", _m_GroupNameFromNumber);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GroupNumberFromName", _m_GroupNumberFromName);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsMatch", _m_IsMatch);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Match", _m_Match);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Matches", _m_Matches);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Replace", _m_Replace);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Split", _m_Split);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Options", _g_get_Options);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "MatchTimeout", _g_get_MatchTimeout);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RightToLeft", _g_get_RightToLeft);
            
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 10, 1, 1);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Escape", _m_Escape_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Unescape", _m_Unescape_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsMatch", _m_IsMatch_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Match", _m_Match_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Matches", _m_Matches_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Replace", _m_Replace_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Split", _m_Split_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CompileToAssembly", _m_CompileToAssembly_xlua_st_);
            
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "InfiniteMatchTimeout", System.Text.RegularExpressions.Regex.InfiniteMatchTimeout);
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "CacheSize", _g_get_CacheSize);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "CacheSize", _s_set_CacheSize);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 2 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING))
				{
					string _pattern = LuaAPI.lua_tostring(L, 2);
					
					System.Text.RegularExpressions.Regex gen_ret = new System.Text.RegularExpressions.Regex(_pattern);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 3 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && translator.Assignable<System.Text.RegularExpressions.RegexOptions>(L, 3))
				{
					string _pattern = LuaAPI.lua_tostring(L, 2);
					System.Text.RegularExpressions.RegexOptions _options;translator.Get(L, 3, out _options);
					
					System.Text.RegularExpressions.Regex gen_ret = new System.Text.RegularExpressions.Regex(_pattern, _options);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 4 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && translator.Assignable<System.Text.RegularExpressions.RegexOptions>(L, 3) && translator.Assignable<System.TimeSpan>(L, 4))
				{
					string _pattern = LuaAPI.lua_tostring(L, 2);
					System.Text.RegularExpressions.RegexOptions _options;translator.Get(L, 3, out _options);
					System.TimeSpan _matchTimeout;translator.Get(L, 4, out _matchTimeout);
					
					System.Text.RegularExpressions.Regex gen_ret = new System.Text.RegularExpressions.Regex(_pattern, _options, _matchTimeout);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to System.Text.RegularExpressions.Regex constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Escape_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = System.Text.RegularExpressions.Regex.Escape( _str );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Unescape_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = System.Text.RegularExpressions.Regex.Unescape( _str );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToString(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Text.RegularExpressions.Regex gen_to_be_invoked = (System.Text.RegularExpressions.Regex)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string gen_ret = gen_to_be_invoked.ToString(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetGroupNames(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Text.RegularExpressions.Regex gen_to_be_invoked = (System.Text.RegularExpressions.Regex)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string[] gen_ret = gen_to_be_invoked.GetGroupNames(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetGroupNumbers(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Text.RegularExpressions.Regex gen_to_be_invoked = (System.Text.RegularExpressions.Regex)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        int[] gen_ret = gen_to_be_invoked.GetGroupNumbers(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GroupNameFromNumber(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Text.RegularExpressions.Regex gen_to_be_invoked = (System.Text.RegularExpressions.Regex)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _i = LuaAPI.xlua_tointeger(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.GroupNameFromNumber( _i );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GroupNumberFromName(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Text.RegularExpressions.Regex gen_to_be_invoked = (System.Text.RegularExpressions.Regex)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        int gen_ret = gen_to_be_invoked.GroupNumberFromName( _name );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsMatch_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    string _pattern = LuaAPI.lua_tostring(L, 2);
                    
                        bool gen_ret = System.Text.RegularExpressions.Regex.IsMatch( _input, _pattern );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.RegularExpressions.RegexOptions>(L, 3)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    string _pattern = LuaAPI.lua_tostring(L, 2);
                    System.Text.RegularExpressions.RegexOptions _options;translator.Get(L, 3, out _options);
                    
                        bool gen_ret = System.Text.RegularExpressions.Regex.IsMatch( _input, _pattern, _options );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.RegularExpressions.RegexOptions>(L, 3)&& translator.Assignable<System.TimeSpan>(L, 4)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    string _pattern = LuaAPI.lua_tostring(L, 2);
                    System.Text.RegularExpressions.RegexOptions _options;translator.Get(L, 3, out _options);
                    System.TimeSpan _matchTimeout;translator.Get(L, 4, out _matchTimeout);
                    
                        bool gen_ret = System.Text.RegularExpressions.Regex.IsMatch( _input, _pattern, _options, _matchTimeout );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Text.RegularExpressions.Regex.IsMatch!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsMatch(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Text.RegularExpressions.Regex gen_to_be_invoked = (System.Text.RegularExpressions.Regex)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 2);
                    
                        bool gen_ret = gen_to_be_invoked.IsMatch( _input );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 2);
                    int _startat = LuaAPI.xlua_tointeger(L, 3);
                    
                        bool gen_ret = gen_to_be_invoked.IsMatch( _input, _startat );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Text.RegularExpressions.Regex.IsMatch!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Match_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    string _pattern = LuaAPI.lua_tostring(L, 2);
                    
                        System.Text.RegularExpressions.Match gen_ret = System.Text.RegularExpressions.Regex.Match( _input, _pattern );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.RegularExpressions.RegexOptions>(L, 3)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    string _pattern = LuaAPI.lua_tostring(L, 2);
                    System.Text.RegularExpressions.RegexOptions _options;translator.Get(L, 3, out _options);
                    
                        System.Text.RegularExpressions.Match gen_ret = System.Text.RegularExpressions.Regex.Match( _input, _pattern, _options );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.RegularExpressions.RegexOptions>(L, 3)&& translator.Assignable<System.TimeSpan>(L, 4)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    string _pattern = LuaAPI.lua_tostring(L, 2);
                    System.Text.RegularExpressions.RegexOptions _options;translator.Get(L, 3, out _options);
                    System.TimeSpan _matchTimeout;translator.Get(L, 4, out _matchTimeout);
                    
                        System.Text.RegularExpressions.Match gen_ret = System.Text.RegularExpressions.Regex.Match( _input, _pattern, _options, _matchTimeout );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Text.RegularExpressions.Regex.Match!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Match(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Text.RegularExpressions.Regex gen_to_be_invoked = (System.Text.RegularExpressions.Regex)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 2);
                    
                        System.Text.RegularExpressions.Match gen_ret = gen_to_be_invoked.Match( _input );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 2);
                    int _startat = LuaAPI.xlua_tointeger(L, 3);
                    
                        System.Text.RegularExpressions.Match gen_ret = gen_to_be_invoked.Match( _input, _startat );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 2);
                    int _beginning = LuaAPI.xlua_tointeger(L, 3);
                    int _length = LuaAPI.xlua_tointeger(L, 4);
                    
                        System.Text.RegularExpressions.Match gen_ret = gen_to_be_invoked.Match( _input, _beginning, _length );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Text.RegularExpressions.Regex.Match!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Matches_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    string _pattern = LuaAPI.lua_tostring(L, 2);
                    
                        System.Text.RegularExpressions.MatchCollection gen_ret = System.Text.RegularExpressions.Regex.Matches( _input, _pattern );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.RegularExpressions.RegexOptions>(L, 3)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    string _pattern = LuaAPI.lua_tostring(L, 2);
                    System.Text.RegularExpressions.RegexOptions _options;translator.Get(L, 3, out _options);
                    
                        System.Text.RegularExpressions.MatchCollection gen_ret = System.Text.RegularExpressions.Regex.Matches( _input, _pattern, _options );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.RegularExpressions.RegexOptions>(L, 3)&& translator.Assignable<System.TimeSpan>(L, 4)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    string _pattern = LuaAPI.lua_tostring(L, 2);
                    System.Text.RegularExpressions.RegexOptions _options;translator.Get(L, 3, out _options);
                    System.TimeSpan _matchTimeout;translator.Get(L, 4, out _matchTimeout);
                    
                        System.Text.RegularExpressions.MatchCollection gen_ret = System.Text.RegularExpressions.Regex.Matches( _input, _pattern, _options, _matchTimeout );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Text.RegularExpressions.Regex.Matches!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Matches(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Text.RegularExpressions.Regex gen_to_be_invoked = (System.Text.RegularExpressions.Regex)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 2);
                    
                        System.Text.RegularExpressions.MatchCollection gen_ret = gen_to_be_invoked.Matches( _input );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 2);
                    int _startat = LuaAPI.xlua_tointeger(L, 3);
                    
                        System.Text.RegularExpressions.MatchCollection gen_ret = gen_to_be_invoked.Matches( _input, _startat );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Text.RegularExpressions.Regex.Matches!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Replace_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    string _pattern = LuaAPI.lua_tostring(L, 2);
                    string _replacement = LuaAPI.lua_tostring(L, 3);
                    
                        string gen_ret = System.Text.RegularExpressions.Regex.Replace( _input, _pattern, _replacement );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.RegularExpressions.MatchEvaluator>(L, 3)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    string _pattern = LuaAPI.lua_tostring(L, 2);
                    System.Text.RegularExpressions.MatchEvaluator _evaluator = translator.GetDelegate<System.Text.RegularExpressions.MatchEvaluator>(L, 3);
                    
                        string gen_ret = System.Text.RegularExpressions.Regex.Replace( _input, _pattern, _evaluator );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.RegularExpressions.RegexOptions>(L, 4)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    string _pattern = LuaAPI.lua_tostring(L, 2);
                    string _replacement = LuaAPI.lua_tostring(L, 3);
                    System.Text.RegularExpressions.RegexOptions _options;translator.Get(L, 4, out _options);
                    
                        string gen_ret = System.Text.RegularExpressions.Regex.Replace( _input, _pattern, _replacement, _options );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.RegularExpressions.MatchEvaluator>(L, 3)&& translator.Assignable<System.Text.RegularExpressions.RegexOptions>(L, 4)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    string _pattern = LuaAPI.lua_tostring(L, 2);
                    System.Text.RegularExpressions.MatchEvaluator _evaluator = translator.GetDelegate<System.Text.RegularExpressions.MatchEvaluator>(L, 3);
                    System.Text.RegularExpressions.RegexOptions _options;translator.Get(L, 4, out _options);
                    
                        string gen_ret = System.Text.RegularExpressions.Regex.Replace( _input, _pattern, _evaluator, _options );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.RegularExpressions.RegexOptions>(L, 4)&& translator.Assignable<System.TimeSpan>(L, 5)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    string _pattern = LuaAPI.lua_tostring(L, 2);
                    string _replacement = LuaAPI.lua_tostring(L, 3);
                    System.Text.RegularExpressions.RegexOptions _options;translator.Get(L, 4, out _options);
                    System.TimeSpan _matchTimeout;translator.Get(L, 5, out _matchTimeout);
                    
                        string gen_ret = System.Text.RegularExpressions.Regex.Replace( _input, _pattern, _replacement, _options, _matchTimeout );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.RegularExpressions.MatchEvaluator>(L, 3)&& translator.Assignable<System.Text.RegularExpressions.RegexOptions>(L, 4)&& translator.Assignable<System.TimeSpan>(L, 5)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    string _pattern = LuaAPI.lua_tostring(L, 2);
                    System.Text.RegularExpressions.MatchEvaluator _evaluator = translator.GetDelegate<System.Text.RegularExpressions.MatchEvaluator>(L, 3);
                    System.Text.RegularExpressions.RegexOptions _options;translator.Get(L, 4, out _options);
                    System.TimeSpan _matchTimeout;translator.Get(L, 5, out _matchTimeout);
                    
                        string gen_ret = System.Text.RegularExpressions.Regex.Replace( _input, _pattern, _evaluator, _options, _matchTimeout );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Text.RegularExpressions.Regex.Replace!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Replace(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Text.RegularExpressions.Regex gen_to_be_invoked = (System.Text.RegularExpressions.Regex)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 2);
                    string _replacement = LuaAPI.lua_tostring(L, 3);
                    
                        string gen_ret = gen_to_be_invoked.Replace( _input, _replacement );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.RegularExpressions.MatchEvaluator>(L, 3)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 2);
                    System.Text.RegularExpressions.MatchEvaluator _evaluator = translator.GetDelegate<System.Text.RegularExpressions.MatchEvaluator>(L, 3);
                    
                        string gen_ret = gen_to_be_invoked.Replace( _input, _evaluator );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 2);
                    string _replacement = LuaAPI.lua_tostring(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                        string gen_ret = gen_to_be_invoked.Replace( _input, _replacement, _count );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.RegularExpressions.MatchEvaluator>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 2);
                    System.Text.RegularExpressions.MatchEvaluator _evaluator = translator.GetDelegate<System.Text.RegularExpressions.MatchEvaluator>(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                        string gen_ret = gen_to_be_invoked.Replace( _input, _evaluator, _count );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 2);
                    string _replacement = LuaAPI.lua_tostring(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    int _startat = LuaAPI.xlua_tointeger(L, 5);
                    
                        string gen_ret = gen_to_be_invoked.Replace( _input, _replacement, _count, _startat );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.RegularExpressions.MatchEvaluator>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 2);
                    System.Text.RegularExpressions.MatchEvaluator _evaluator = translator.GetDelegate<System.Text.RegularExpressions.MatchEvaluator>(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    int _startat = LuaAPI.xlua_tointeger(L, 5);
                    
                        string gen_ret = gen_to_be_invoked.Replace( _input, _evaluator, _count, _startat );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Text.RegularExpressions.Regex.Replace!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Split_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    string _pattern = LuaAPI.lua_tostring(L, 2);
                    
                        string[] gen_ret = System.Text.RegularExpressions.Regex.Split( _input, _pattern );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.RegularExpressions.RegexOptions>(L, 3)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    string _pattern = LuaAPI.lua_tostring(L, 2);
                    System.Text.RegularExpressions.RegexOptions _options;translator.Get(L, 3, out _options);
                    
                        string[] gen_ret = System.Text.RegularExpressions.Regex.Split( _input, _pattern, _options );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.RegularExpressions.RegexOptions>(L, 3)&& translator.Assignable<System.TimeSpan>(L, 4)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    string _pattern = LuaAPI.lua_tostring(L, 2);
                    System.Text.RegularExpressions.RegexOptions _options;translator.Get(L, 3, out _options);
                    System.TimeSpan _matchTimeout;translator.Get(L, 4, out _matchTimeout);
                    
                        string[] gen_ret = System.Text.RegularExpressions.Regex.Split( _input, _pattern, _options, _matchTimeout );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Text.RegularExpressions.Regex.Split!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Split(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Text.RegularExpressions.Regex gen_to_be_invoked = (System.Text.RegularExpressions.Regex)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 2);
                    
                        string[] gen_ret = gen_to_be_invoked.Split( _input );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 2);
                    int _count = LuaAPI.xlua_tointeger(L, 3);
                    
                        string[] gen_ret = gen_to_be_invoked.Split( _input, _count );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    string _input = LuaAPI.lua_tostring(L, 2);
                    int _count = LuaAPI.xlua_tointeger(L, 3);
                    int _startat = LuaAPI.xlua_tointeger(L, 4);
                    
                        string[] gen_ret = gen_to_be_invoked.Split( _input, _count, _startat );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Text.RegularExpressions.Regex.Split!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CompileToAssembly_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Text.RegularExpressions.RegexCompilationInfo[]>(L, 1)&& translator.Assignable<System.Reflection.AssemblyName>(L, 2)) 
                {
                    System.Text.RegularExpressions.RegexCompilationInfo[] _regexinfos = (System.Text.RegularExpressions.RegexCompilationInfo[])translator.GetObject(L, 1, typeof(System.Text.RegularExpressions.RegexCompilationInfo[]));
                    System.Reflection.AssemblyName _assemblyname = (System.Reflection.AssemblyName)translator.GetObject(L, 2, typeof(System.Reflection.AssemblyName));
                    
                    System.Text.RegularExpressions.Regex.CompileToAssembly( _regexinfos, _assemblyname );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Text.RegularExpressions.RegexCompilationInfo[]>(L, 1)&& translator.Assignable<System.Reflection.AssemblyName>(L, 2)&& translator.Assignable<System.Reflection.Emit.CustomAttributeBuilder[]>(L, 3)) 
                {
                    System.Text.RegularExpressions.RegexCompilationInfo[] _regexinfos = (System.Text.RegularExpressions.RegexCompilationInfo[])translator.GetObject(L, 1, typeof(System.Text.RegularExpressions.RegexCompilationInfo[]));
                    System.Reflection.AssemblyName _assemblyname = (System.Reflection.AssemblyName)translator.GetObject(L, 2, typeof(System.Reflection.AssemblyName));
                    System.Reflection.Emit.CustomAttributeBuilder[] _attributes = (System.Reflection.Emit.CustomAttributeBuilder[])translator.GetObject(L, 3, typeof(System.Reflection.Emit.CustomAttributeBuilder[]));
                    
                    System.Text.RegularExpressions.Regex.CompileToAssembly( _regexinfos, _assemblyname, _attributes );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<System.Text.RegularExpressions.RegexCompilationInfo[]>(L, 1)&& translator.Assignable<System.Reflection.AssemblyName>(L, 2)&& translator.Assignable<System.Reflection.Emit.CustomAttributeBuilder[]>(L, 3)&& (LuaAPI.lua_isnil(L, 4) || LuaAPI.lua_type(L, 4) == LuaTypes.LUA_TSTRING)) 
                {
                    System.Text.RegularExpressions.RegexCompilationInfo[] _regexinfos = (System.Text.RegularExpressions.RegexCompilationInfo[])translator.GetObject(L, 1, typeof(System.Text.RegularExpressions.RegexCompilationInfo[]));
                    System.Reflection.AssemblyName _assemblyname = (System.Reflection.AssemblyName)translator.GetObject(L, 2, typeof(System.Reflection.AssemblyName));
                    System.Reflection.Emit.CustomAttributeBuilder[] _attributes = (System.Reflection.Emit.CustomAttributeBuilder[])translator.GetObject(L, 3, typeof(System.Reflection.Emit.CustomAttributeBuilder[]));
                    string _resourceFile = LuaAPI.lua_tostring(L, 4);
                    
                    System.Text.RegularExpressions.Regex.CompileToAssembly( _regexinfos, _assemblyname, _attributes, _resourceFile );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Text.RegularExpressions.Regex.CompileToAssembly!");
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CacheSize(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, System.Text.RegularExpressions.Regex.CacheSize);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Options(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Text.RegularExpressions.Regex gen_to_be_invoked = (System.Text.RegularExpressions.Regex)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Options);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MatchTimeout(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Text.RegularExpressions.Regex gen_to_be_invoked = (System.Text.RegularExpressions.Regex)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.MatchTimeout);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RightToLeft(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Text.RegularExpressions.Regex gen_to_be_invoked = (System.Text.RegularExpressions.Regex)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.RightToLeft);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CacheSize(RealStatePtr L)
        {
		    try {
                
			    System.Text.RegularExpressions.Regex.CacheSize = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
