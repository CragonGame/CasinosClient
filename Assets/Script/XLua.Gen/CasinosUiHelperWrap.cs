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
    public class CasinosUiHelperWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Casinos.UiHelper);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 6, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "checkTime", _m_checkTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "timeIsSameMinute", _m_timeIsSameMinute_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "getLocalTm", _m_getLocalTm_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "getLocalTmToString", _m_getLocalTmToString_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "addEllipsisToStr", _m_addEllipsisToStr_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "Casinos.UiHelper does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_checkTime_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    float _time = (float)LuaAPI.lua_tonumber(L, 1);
                    bool _time_out = LuaAPI.lua_toboolean(L, 2);
                    
                        string gen_ret = Casinos.UiHelper.checkTime( _time, ref _time_out );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    LuaAPI.lua_pushboolean(L, _time_out);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_timeIsSameMinute_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.DateTime _time1;translator.Get(L, 1, out _time1);
                    System.DateTime _time2;translator.Get(L, 2, out _time2);
                    
                        bool gen_ret = Casinos.UiHelper.timeIsSameMinute( _time1, _time2 );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getLocalTm_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.DateTime _tm;translator.Get(L, 1, out _tm);
                    
                        System.DateTime gen_ret = Casinos.UiHelper.getLocalTm( _tm );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_getLocalTmToString_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.DateTime _tm;translator.Get(L, 1, out _tm);
                    
                        string gen_ret = Casinos.UiHelper.getLocalTmToString( _tm );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_addEllipsisToStr_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    int _max_length = LuaAPI.xlua_tointeger(L, 2);
                    int _show_length = LuaAPI.xlua_tointeger(L, 3);
                    
                        string gen_ret = Casinos.UiHelper.addEllipsisToStr( _str, _max_length, _show_length );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
