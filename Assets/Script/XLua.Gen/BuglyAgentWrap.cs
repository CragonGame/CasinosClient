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
    public class BuglyAgentWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(BuglyAgent);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 17, 3, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "ConfigCrashReporter", _m_ConfigCrashReporter_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "InitWithAppId", _m_InitWithAppId_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EnableExceptionHandler", _m_EnableExceptionHandler_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RegisterLogCallback", _m_RegisterLogCallback_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLogCallbackExtrasHandler", _m_SetLogCallbackExtrasHandler_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReportException", _m_ReportException_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "UnregisterLogCallback", _m_UnregisterLogCallback_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetUserId", _m_SetUserId_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetScene", _m_SetScene_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AddSceneData", _m_AddSceneData_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ConfigDebugMode", _m_ConfigDebugMode_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ConfigAutoQuitApplication", _m_ConfigAutoQuitApplication_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ConfigAutoReportLogLevel", _m_ConfigAutoReportLogLevel_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ConfigDefault", _m_ConfigDefault_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DebugLog", _m_DebugLog_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "PrintLog", _m_PrintLog_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "PluginVersion", _g_get_PluginVersion);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "IsInitialized", _g_get_IsInitialized);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "AutoQuitApplicationAfterReport", _g_get_AutoQuitApplicationAfterReport);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					BuglyAgent gen_ret = new BuglyAgent();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to BuglyAgent constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ConfigCrashReporter_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    int _type = LuaAPI.xlua_tointeger(L, 1);
                    int _logLevel = LuaAPI.xlua_tointeger(L, 2);
                    
                    BuglyAgent.ConfigCrashReporter( _type, _logLevel );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitWithAppId_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _appId = LuaAPI.lua_tostring(L, 1);
                    
                    BuglyAgent.InitWithAppId( _appId );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EnableExceptionHandler_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    BuglyAgent.EnableExceptionHandler(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RegisterLogCallback_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    BuglyAgent.LogCallbackDelegate _handler = translator.GetDelegate<BuglyAgent.LogCallbackDelegate>(L, 1);
                    
                    BuglyAgent.RegisterLogCallback( _handler );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLogCallbackExtrasHandler_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.Func<System.Collections.Generic.Dictionary<string, string>> _handler = translator.GetDelegate<System.Func<System.Collections.Generic.Dictionary<string, string>>>(L, 1);
                    
                    BuglyAgent.SetLogCallbackExtrasHandler( _handler );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReportException_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Exception>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    System.Exception _e = (System.Exception)translator.GetObject(L, 1, typeof(System.Exception));
                    string _message = LuaAPI.lua_tostring(L, 2);
                    
                    BuglyAgent.ReportException( _e, _message );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string _name = LuaAPI.lua_tostring(L, 1);
                    string _message = LuaAPI.lua_tostring(L, 2);
                    string _stackTrace = LuaAPI.lua_tostring(L, 3);
                    
                    BuglyAgent.ReportException( _name, _message, _stackTrace );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to BuglyAgent.ReportException!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnregisterLogCallback_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    BuglyAgent.LogCallbackDelegate _handler = translator.GetDelegate<BuglyAgent.LogCallbackDelegate>(L, 1);
                    
                    BuglyAgent.UnregisterLogCallback( _handler );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetUserId_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _userId = LuaAPI.lua_tostring(L, 1);
                    
                    BuglyAgent.SetUserId( _userId );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetScene_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    int _sceneId = LuaAPI.xlua_tointeger(L, 1);
                    
                    BuglyAgent.SetScene( _sceneId );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddSceneData_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _key = LuaAPI.lua_tostring(L, 1);
                    string _value = LuaAPI.lua_tostring(L, 2);
                    
                    BuglyAgent.AddSceneData( _key, _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ConfigDebugMode_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    bool _enable = LuaAPI.lua_toboolean(L, 1);
                    
                    BuglyAgent.ConfigDebugMode( _enable );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ConfigAutoQuitApplication_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    bool _autoQuit = LuaAPI.lua_toboolean(L, 1);
                    
                    BuglyAgent.ConfigAutoQuitApplication( _autoQuit );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ConfigAutoReportLogLevel_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    LogSeverity _level;translator.Get(L, 1, out _level);
                    
                    BuglyAgent.ConfigAutoReportLogLevel( _level );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ConfigDefault_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _channel = LuaAPI.lua_tostring(L, 1);
                    string _version = LuaAPI.lua_tostring(L, 2);
                    string _user = LuaAPI.lua_tostring(L, 3);
                    long _delay = LuaAPI.lua_toint64(L, 4);
                    
                    BuglyAgent.ConfigDefault( _channel, _version, _user, _delay );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DebugLog_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _tag = LuaAPI.lua_tostring(L, 1);
                    string _format = LuaAPI.lua_tostring(L, 2);
                    object[] _args = translator.GetParams<object>(L, 3);
                    
                    BuglyAgent.DebugLog( _tag, _format, _args );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PrintLog_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    LogSeverity _level;translator.Get(L, 1, out _level);
                    string _format = LuaAPI.lua_tostring(L, 2);
                    object[] _args = translator.GetParams<object>(L, 3);
                    
                    BuglyAgent.PrintLog( _level, _format, _args );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PluginVersion(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, BuglyAgent.PluginVersion);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsInitialized(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, BuglyAgent.IsInitialized);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AutoQuitApplicationAfterReport(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, BuglyAgent.AutoQuitApplicationAfterReport);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
