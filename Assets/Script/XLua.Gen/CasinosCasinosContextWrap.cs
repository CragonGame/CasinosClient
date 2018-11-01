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
    public class CasinosCasinosContextWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Casinos.CasinosContext);
			Utils.BeginObjectRegister(type, L, translator, 0, 16, 25, 12);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Update", _m_Update);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Close", _m_Close);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Launch", _m_Launch);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnApplicationPause", _m_OnApplicationPause);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnApplicationFocus", _m_OnApplicationFocus);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReportLogWithDeviceId", _m_ReportLogWithDeviceId);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReportLogWithPlayerId", _m_ReportLogWithPlayerId);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetNativeOperate", _m_SetNativeOperate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearSB", _m_ClearSB);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AppendStrWithSB", _m_AppendStrWithSB);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Play", _m_Play);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BgVolumeChange", _m_BgVolumeChange);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetAudioSource", _m_GetAudioSource);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DestroyAllSceneSound", _m_DestroyAllSceneSound);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StopAllSceneSound", _m_StopAllSceneSound);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FreeAudioSource", _m_FreeAudioSource);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "TimerShaft", _g_get_TimerShaft);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Stopwatch", _g_get_Stopwatch);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "MemoryStream", _g_get_MemoryStream);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SB", _g_get_SB);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "AsyncAssetLoaderMgr", _g_get_AsyncAssetLoaderMgr);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "AsyncAssetLoadGroup", _g_get_AsyncAssetLoadGroup);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PathMgr", _g_get_PathMgr);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PlayerPrefs", _g_get_PlayerPrefs);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Config", _g_get_Config);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "TextureMgr", _g_get_TextureMgr);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LuaMgr", _g_get_LuaMgr);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NetMgr", _g_get_NetMgr);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SpineMgr", _g_get_SpineMgr);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NativeMgr", _g_get_NativeMgr);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NativeAPIMsgReceiverListner", _g_get_NativeAPIMsgReceiverListner);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LoginType", _g_get_LoginType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UseHttps", _g_get_UseHttps);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CanReportLog", _g_get_CanReportLog);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CanReportLogDeviceId", _g_get_CanReportLogDeviceId);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CanReportLogPlayerId", _g_get_CanReportLogPlayerId);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UnityAndroid", _g_get_UnityAndroid);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UnityIOS", _g_get_UnityIOS);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsEditor", _g_get_IsEditor);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsSqliteUnity", _g_get_IsSqliteUnity);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "TbDataMgrLua", _g_get_TbDataMgrLua);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "NativeMgr", _s_set_NativeMgr);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "NativeAPIMsgReceiverListner", _s_set_NativeAPIMsgReceiverListner);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LoginType", _s_set_LoginType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UseHttps", _s_set_UseHttps);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CanReportLog", _s_set_CanReportLog);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CanReportLogDeviceId", _s_set_CanReportLogDeviceId);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CanReportLogPlayerId", _s_set_CanReportLogPlayerId);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UnityAndroid", _s_set_UnityAndroid);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UnityIOS", _s_set_UnityIOS);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsEditor", _s_set_IsEditor);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsSqliteUnity", _s_set_IsSqliteUnity);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "TbDataMgrLua", _s_set_TbDataMgrLua);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 1, 0);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Instance", _g_get_Instance);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 3 && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2) && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3))
				{
					bool _force_use_resouceslaunch = LuaAPI.lua_toboolean(L, 2);
					bool _force_use_dataoss = LuaAPI.lua_toboolean(L, 3);
					
					Casinos.CasinosContext gen_ret = new Casinos.CasinosContext(_force_use_resouceslaunch, _force_use_dataoss);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Casinos.CasinosContext constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Update(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _elapsed_tm = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.Update( _elapsed_tm );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Close(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Close(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Launch(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Launch(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnApplicationPause(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _pause = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.OnApplicationPause( _pause );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnApplicationFocus(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _focus_status = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.OnApplicationFocus( _focus_status );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReportLogWithDeviceId(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    string _message = LuaAPI.lua_tostring(L, 3);
                    string _stackTrace = LuaAPI.lua_tostring(L, 4);
                    
                    gen_to_be_invoked.ReportLogWithDeviceId( _name, _message, _stackTrace );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReportLogWithPlayerId(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    string _player_id = LuaAPI.lua_tostring(L, 3);
                    string _message = LuaAPI.lua_tostring(L, 4);
                    string _stackTrace = LuaAPI.lua_tostring(L, 5);
                    
                    gen_to_be_invoked.ReportLogWithPlayerId( _name, _player_id, _message, _stackTrace );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetNativeOperate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _native_operate = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.SetNativeOperate( _native_operate );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearSB(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ClearSB(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AppendStrWithSB(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string[] _strs = translator.GetParams<string>(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.AppendStrWithSB( _strs );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Play(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _file_name = LuaAPI.lua_tostring(L, 2);
                    Casinos._eSoundLayer _sound_layer;translator.Get(L, 3, out _sound_layer);
                    
                    gen_to_be_invoked.Play( _file_name, _sound_layer );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BgVolumeChange(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _volume = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.BgVolumeChange( _volume );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAudioSource(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        System.Collections.Generic.List<UnityEngine.AudioSource> gen_ret = gen_to_be_invoked.GetAudioSource(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DestroyAllSceneSound(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.DestroyAllSceneSound(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StopAllSceneSound(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.StopAllSceneSound(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FreeAudioSource(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.AudioSource _audio_src = (UnityEngine.AudioSource)translator.GetObject(L, 2, typeof(UnityEngine.AudioSource));
                    
                    gen_to_be_invoked.FreeAudioSource( _audio_src );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Instance(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Casinos.CasinosContext.Instance);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_TimerShaft(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.TimerShaft);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Stopwatch(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Stopwatch);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MemoryStream(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.MemoryStream);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SB(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.SB);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AsyncAssetLoaderMgr(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.AsyncAssetLoaderMgr);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AsyncAssetLoadGroup(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.AsyncAssetLoadGroup);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PathMgr(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.PathMgr);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PlayerPrefs(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.PlayerPrefs);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Config(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Config);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_TextureMgr(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.TextureMgr);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LuaMgr(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.LuaMgr);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NetMgr(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.NetMgr);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SpineMgr(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.SpineMgr);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NativeMgr(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.NativeMgr);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NativeAPIMsgReceiverListner(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.NativeAPIMsgReceiverListner);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LoginType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.PushCasinos_eLoginType(L, gen_to_be_invoked.LoginType);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UseHttps(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.UseHttps);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CanReportLog(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.CanReportLog);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CanReportLogDeviceId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.CanReportLogDeviceId);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CanReportLogPlayerId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.CanReportLogPlayerId);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UnityAndroid(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.UnityAndroid);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UnityIOS(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.UnityIOS);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsEditor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsEditor);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsSqliteUnity(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsSqliteUnity);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_TbDataMgrLua(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.TbDataMgrLua);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_NativeMgr(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.NativeMgr = (Casinos.NativeMgr)translator.GetObject(L, 2, typeof(Casinos.NativeMgr));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_NativeAPIMsgReceiverListner(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.NativeAPIMsgReceiverListner = (Casinos.NativeAPIMsgReceiverListener)translator.GetObject(L, 2, typeof(Casinos.NativeAPIMsgReceiverListener));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LoginType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                Casinos._eLoginType gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.LoginType = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UseHttps(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.UseHttps = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CanReportLog(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.CanReportLog = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CanReportLogDeviceId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.CanReportLogDeviceId = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CanReportLogPlayerId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.CanReportLogPlayerId = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UnityAndroid(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.UnityAndroid = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UnityIOS(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.UnityIOS = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsEditor(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.IsEditor = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsSqliteUnity(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.IsSqliteUnity = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_TbDataMgrLua(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Casinos.CasinosContext gen_to_be_invoked = (Casinos.CasinosContext)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.TbDataMgrLua = (XLua.LuaTable)translator.GetObject(L, 2, typeof(XLua.LuaTable));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
