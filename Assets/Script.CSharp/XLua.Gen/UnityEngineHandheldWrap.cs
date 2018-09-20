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
    public class UnityEngineHandheldWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UnityEngine.Handheld);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 8, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "PlayFullScreenMovie", _m_PlayFullScreenMovie_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Vibrate", _m_Vibrate_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetActivityIndicatorStyle", _m_SetActivityIndicatorStyle_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetActivityIndicatorStyle", _m_GetActivityIndicatorStyle_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "StartActivityIndicator", _m_StartActivityIndicator_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "StopActivityIndicator", _m_StopActivityIndicator_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ClearShaderCache", _m_ClearShaderCache_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					UnityEngine.Handheld gen_ret = new UnityEngine.Handheld();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Handheld constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlayFullScreenMovie_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        bool gen_ret = UnityEngine.Handheld.PlayFullScreenMovie( _path );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.Color>(L, 2)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.Color _bgColor;translator.Get(L, 2, out _bgColor);
                    
                        bool gen_ret = UnityEngine.Handheld.PlayFullScreenMovie( _path, _bgColor );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.Color>(L, 2)&& translator.Assignable<UnityEngine.FullScreenMovieControlMode>(L, 3)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.Color _bgColor;translator.Get(L, 2, out _bgColor);
                    UnityEngine.FullScreenMovieControlMode _controlMode;translator.Get(L, 3, out _controlMode);
                    
                        bool gen_ret = UnityEngine.Handheld.PlayFullScreenMovie( _path, _bgColor, _controlMode );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.Color>(L, 2)&& translator.Assignable<UnityEngine.FullScreenMovieControlMode>(L, 3)&& translator.Assignable<UnityEngine.FullScreenMovieScalingMode>(L, 4)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.Color _bgColor;translator.Get(L, 2, out _bgColor);
                    UnityEngine.FullScreenMovieControlMode _controlMode;translator.Get(L, 3, out _controlMode);
                    UnityEngine.FullScreenMovieScalingMode _scalingMode;translator.Get(L, 4, out _scalingMode);
                    
                        bool gen_ret = UnityEngine.Handheld.PlayFullScreenMovie( _path, _bgColor, _controlMode, _scalingMode );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Handheld.PlayFullScreenMovie!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Vibrate_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    UnityEngine.Handheld.Vibrate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetActivityIndicatorStyle_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<UnityEngine.iOS.ActivityIndicatorStyle>(L, 1)) 
                {
                    UnityEngine.iOS.ActivityIndicatorStyle _style;translator.Get(L, 1, out _style);
                    
                    UnityEngine.Handheld.SetActivityIndicatorStyle( _style );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& translator.Assignable<UnityEngine.AndroidActivityIndicatorStyle>(L, 1)) 
                {
                    UnityEngine.AndroidActivityIndicatorStyle _style;translator.Get(L, 1, out _style);
                    
                    UnityEngine.Handheld.SetActivityIndicatorStyle( _style );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Handheld.SetActivityIndicatorStyle!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetActivityIndicatorStyle_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        int gen_ret = UnityEngine.Handheld.GetActivityIndicatorStyle(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StartActivityIndicator_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    UnityEngine.Handheld.StartActivityIndicator(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StopActivityIndicator_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    UnityEngine.Handheld.StopActivityIndicator(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearShaderCache_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    UnityEngine.Handheld.ClearShaderCache(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
